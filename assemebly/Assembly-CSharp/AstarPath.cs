using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Pathfinding;
using Pathfinding.Util;
using UnityEngine;

// Token: 0x02000010 RID: 16
[AddComponentMenu("Pathfinding/Pathfinder")]
public class AstarPath : MonoBehaviour
{
	// Token: 0x17000009 RID: 9
	// (get) Token: 0x060000E2 RID: 226 RVA: 0x00009AE6 File Offset: 0x00007EE6
	public static Version Version
	{
		get
		{
			return new Version(3, 5, 9, 1);
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x060000E3 RID: 227 RVA: 0x00009AF2 File Offset: 0x00007EF2
	public Type[] graphTypes
	{
		get
		{
			return this.astarData.graphTypes;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x060000E4 RID: 228 RVA: 0x00009AFF File Offset: 0x00007EFF
	// (set) Token: 0x060000E5 RID: 229 RVA: 0x00009B22 File Offset: 0x00007F22
	public NavGraph[] graphs
	{
		get
		{
			if (this.astarData == null)
			{
				this.astarData = new AstarData();
			}
			return this.astarData.graphs;
		}
		set
		{
			if (this.astarData == null)
			{
				this.astarData = new AstarData();
			}
			this.astarData.graphs = value;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x060000E6 RID: 230 RVA: 0x00009B46 File Offset: 0x00007F46
	public float maxNearestNodeDistanceSqr
	{
		get
		{
			return this.maxNearestNodeDistance * this.maxNearestNodeDistance;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x060000E7 RID: 231 RVA: 0x00009B55 File Offset: 0x00007F55
	public PathHandler debugPathData
	{
		get
		{
			if (this.debugPath == null)
			{
				return null;
			}
			return this.debugPath.pathHandler;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x060000E8 RID: 232 RVA: 0x00009B6F File Offset: 0x00007F6F
	public static int NumParallelThreads
	{
		get
		{
			return (AstarPath.threadInfos == null) ? 0 : AstarPath.threadInfos.Length;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x060000E9 RID: 233 RVA: 0x00009B88 File Offset: 0x00007F88
	public static bool IsUsingMultithreading
	{
		get
		{
			if (AstarPath.threads != null && AstarPath.threads.Length > 0)
			{
				return true;
			}
			if (AstarPath.threads != null && AstarPath.threads.Length == 0 && AstarPath.threadEnumerator != null)
			{
				return false;
			}
			if (Application.isPlaying)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Not 'using threading' and not 'not using threading'... Are you sure pathfinding is set up correctly?\nIf scripts are reloaded in unity editor during play this could happen.\n",
					(AstarPath.threads == null) ? "NULL" : (string.Empty + AstarPath.threads.Length),
					" ",
					AstarPath.threadEnumerator != null
				}));
			}
			return false;
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060000EA RID: 234 RVA: 0x00009C3C File Offset: 0x0000803C
	public bool IsAnyGraphUpdatesQueued
	{
		get
		{
			return this.graphUpdateQueue != null && this.graphUpdateQueue.Count > 0;
		}
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00009C5C File Offset: 0x0000805C
	public string[] GetTagNames()
	{
		if (this.tagNames == null || this.tagNames.Length != 32)
		{
			this.tagNames = new string[32];
			for (int i = 0; i < this.tagNames.Length; i++)
			{
				this.tagNames[i] = string.Empty + i;
			}
			this.tagNames[0] = "Basic Ground";
		}
		return this.tagNames;
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00009CD4 File Offset: 0x000080D4
	public static string[] FindTagNames()
	{
		if (AstarPath.active != null)
		{
			return AstarPath.active.GetTagNames();
		}
		AstarPath astarPath = UnityEngine.Object.FindObjectOfType(typeof(AstarPath)) as AstarPath;
		if (astarPath != null)
		{
			AstarPath.active = astarPath;
			return astarPath.GetTagNames();
		}
		return new string[]
		{
			"There is no AstarPath component in the scene"
		};
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00009D38 File Offset: 0x00008138
	public ushort GetNextPathID()
	{
		if (this.nextFreePathID == 0)
		{
			this.nextFreePathID += 1;
			if (AstarPath.On65KOverflow != null)
			{
				OnVoidDelegate on65KOverflow = AstarPath.On65KOverflow;
				AstarPath.On65KOverflow = null;
				on65KOverflow();
			}
		}
		ushort result;
		this.nextFreePathID = (result = this.nextFreePathID) + 1;
		return result;
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00009D90 File Offset: 0x00008190
	private void OnDrawGizmos()
	{
		if (AstarPath.active == null)
		{
			AstarPath.active = this;
		}
		else if (AstarPath.active != this)
		{
			return;
		}
		if (this.graphs == null)
		{
			return;
		}
		if (this.pathQueue != null && this.pathQueue.AllReceiversBlocked && this.workItems.Count > 0)
		{
			return;
		}
		for (int i = 0; i < this.graphs.Length; i++)
		{
			if (this.graphs[i] != null)
			{
				if (this.graphs[i].drawGizmos)
				{
					this.graphs[i].OnDrawGizmos(this.showNavGraphs);
				}
			}
		}
		if (this.showNavGraphs)
		{
			this.euclideanEmbedding.OnDrawGizmos();
		}
		if (this.showUnwalkableNodes && this.showNavGraphs)
		{
			Gizmos.color = AstarColor.UnwalkableNode;
			GraphNodeDelegateCancelable del = new GraphNodeDelegateCancelable(this.DrawUnwalkableNode);
			for (int j = 0; j < this.graphs.Length; j++)
			{
				if (this.graphs[j] != null)
				{
					this.graphs[j].GetNodes(del);
				}
			}
		}
		if (this.OnDrawGizmosCallback != null)
		{
			this.OnDrawGizmosCallback();
		}
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00009EDF File Offset: 0x000082DF
	private bool DrawUnwalkableNode(GraphNode node)
	{
		if (!node.Walkable)
		{
			Gizmos.DrawCube((Vector3)node.position, Vector3.one * this.unwalkableNodeDebugSize);
		}
		return true;
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x00009F10 File Offset: 0x00008310
	private void OnGUI()
	{
		if (this.logPathResults == PathLog.InGame && this.inGameDebugPath != string.Empty)
		{
			GUI.Label(new Rect(5f, 5f, 400f, 600f), this.inGameDebugPath);
		}
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00009F64 File Offset: 0x00008364
	private static void AstarLog(string s)
	{
		if (object.ReferenceEquals(AstarPath.active, null))
		{
			Debug.Log("No AstarPath object was found : " + s);
			return;
		}
		if (AstarPath.active.logPathResults != PathLog.None && AstarPath.active.logPathResults != PathLog.OnlyErrors && Application.isEditor)
		{
			Debug.Log(s);
		}
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00009FC1 File Offset: 0x000083C1
	private static void AstarLogError(string s)
	{
		if (AstarPath.active == null)
		{
			Debug.Log("No AstarPath object was found : " + s);
			return;
		}
		if (AstarPath.active.logPathResults != PathLog.None)
		{
			Debug.LogError(s);
		}
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00009FFC File Offset: 0x000083FC
	private void LogPathResults(Path p)
	{
		if (this.logPathResults == PathLog.None || (this.logPathResults == PathLog.OnlyErrors && !p.error))
		{
			return;
		}
		string message = p.DebugString(this.logPathResults);
		if (this.logPathResults == PathLog.InGame)
		{
			this.inGameDebugPath = message;
		}
		else
		{
			Debug.Log(message);
		}
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x0000A058 File Offset: 0x00008458
	private void Update()
	{
		this.PerformBlockingActions(false, true);
		if (AstarPath.threadEnumerator != null)
		{
			try
			{
				AstarPath.threadEnumerator.MoveNext();
			}
			catch (Exception ex)
			{
				AstarPath.threadEnumerator = null;
				if (!(ex is ThreadControlQueue.QueueTerminationException))
				{
					Debug.LogException(ex);
					Debug.LogError("Unhandled exception during pathfinding. Terminating.");
					this.pathQueue.TerminateReceivers();
					try
					{
						this.pathQueue.PopNoBlock(false);
					}
					catch
					{
					}
				}
			}
		}
		this.ReturnPaths(true);
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x0000A0F4 File Offset: 0x000084F4
	private void PerformBlockingActions(bool force = false, bool unblockOnComplete = true)
	{
		if (this.pathQueue.AllReceiversBlocked)
		{
			this.ReturnPaths(false);
			if (AstarPath.OnThreadSafeCallback != null)
			{
				OnVoidDelegate onThreadSafeCallback = AstarPath.OnThreadSafeCallback;
				AstarPath.OnThreadSafeCallback = null;
				onThreadSafeCallback();
			}
			if (this.ProcessWorkItems(force) == 2)
			{
				this.workItemsQueued = false;
				if (unblockOnComplete)
				{
					if (this.euclideanEmbedding.dirty)
					{
						this.euclideanEmbedding.RecalculateCosts();
					}
					this.pathQueue.Unblock();
				}
			}
		}
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x0000A174 File Offset: 0x00008574
	public void QueueWorkItemFloodFill()
	{
		if (!this.pathQueue.AllReceiversBlocked)
		{
			throw new Exception("You are calling QueueWorkItemFloodFill from outside a WorkItem. This might cause unexpected behaviour.");
		}
		this.queuedWorkItemFloodFill = true;
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x0000A198 File Offset: 0x00008598
	public void EnsureValidFloodFill()
	{
		if (this.queuedWorkItemFloodFill)
		{
			this.FloodFill();
		}
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x0000A1AB File Offset: 0x000085AB
	public void AddWorkItem(AstarPath.AstarWorkItem itm)
	{
		this.workItems.Enqueue(itm);
		if (!this.workItemsQueued)
		{
			this.workItemsQueued = true;
			if (!this.isScanning)
			{
				AstarPath.InterruptPathfinding();
			}
		}
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x0000A1DC File Offset: 0x000085DC
	private int ProcessWorkItems(bool force)
	{
		if (!this.pathQueue.AllReceiversBlocked)
		{
			return 0;
		}
		if (this.processingWorkItems)
		{
			throw new Exception("Processing work items recursively. Please do not wait for other work items to be completed inside work items. If you think this is not caused by any of your scripts, this might be a bug.");
		}
		this.processingWorkItems = true;
		while (this.workItems.Count > 0)
		{
			AstarPath.AstarWorkItem astarWorkItem = this.workItems.Peek();
			if (astarWorkItem.init != null)
			{
				astarWorkItem.init();
				astarWorkItem.init = null;
			}
			bool flag;
			try
			{
				flag = (astarWorkItem.update == null || astarWorkItem.update(force));
			}
			catch
			{
				this.workItems.Dequeue();
				this.processingWorkItems = false;
				throw;
			}
			if (!flag)
			{
				if (force)
				{
					Debug.LogError("Misbehaving WorkItem. 'force'=true but the work item did not complete.\nIf force=true is passed to a WorkItem it should always return true.");
				}
				this.processingWorkItems = false;
				return 1;
			}
			this.workItems.Dequeue();
		}
		this.EnsureValidFloodFill();
		this.processingWorkItems = false;
		return 2;
	}

	// Token: 0x060000FA RID: 250 RVA: 0x0000A2E0 File Offset: 0x000086E0
	public void QueueGraphUpdates()
	{
		if (!this.isRegisteredForUpdate)
		{
			this.isRegisteredForUpdate = true;
			this.AddWorkItem(new AstarPath.AstarWorkItem
			{
				init = new OnVoidDelegate(this.QueueGraphUpdatesInternal),
				update = new Func<bool, bool>(this.ProcessGraphUpdates)
			});
		}
	}

	// Token: 0x060000FB RID: 251 RVA: 0x0000A334 File Offset: 0x00008734
	private IEnumerator DelayedGraphUpdate()
	{
		this.graphUpdateRoutineRunning = true;
		yield return new WaitForSeconds(this.maxGraphUpdateFreq - (Time.time - this.lastGraphUpdate));
		this.QueueGraphUpdates();
		this.graphUpdateRoutineRunning = false;
		yield break;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x0000A34F File Offset: 0x0000874F
	public void UpdateGraphs(Bounds bounds, float t)
	{
		this.UpdateGraphs(new GraphUpdateObject(bounds), t);
	}

	// Token: 0x060000FD RID: 253 RVA: 0x0000A35E File Offset: 0x0000875E
	public void UpdateGraphs(GraphUpdateObject ob, float t)
	{
		base.StartCoroutine(this.UpdateGraphsInteral(ob, t));
	}

	// Token: 0x060000FE RID: 254 RVA: 0x0000A370 File Offset: 0x00008770
	private IEnumerator UpdateGraphsInteral(GraphUpdateObject ob, float t)
	{
		yield return new WaitForSeconds(t);
		this.UpdateGraphs(ob);
		yield break;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x0000A399 File Offset: 0x00008799
	public void UpdateGraphs(Bounds bounds)
	{
		this.UpdateGraphs(new GraphUpdateObject(bounds));
	}

	// Token: 0x06000100 RID: 256 RVA: 0x0000A3A8 File Offset: 0x000087A8
	public void UpdateGraphs(GraphUpdateObject ob)
	{
		if (this.graphUpdateQueue == null)
		{
			this.graphUpdateQueue = new Queue<GraphUpdateObject>();
		}
		this.graphUpdateQueue.Enqueue(ob);
		if (this.limitGraphUpdates && Time.time - this.lastGraphUpdate < this.maxGraphUpdateFreq)
		{
			if (!this.graphUpdateRoutineRunning)
			{
				base.StartCoroutine(this.DelayedGraphUpdate());
			}
		}
		else
		{
			this.QueueGraphUpdates();
		}
	}

	// Token: 0x06000101 RID: 257 RVA: 0x0000A41C File Offset: 0x0000881C
	public void FlushGraphUpdates()
	{
		if (this.IsAnyGraphUpdatesQueued)
		{
			this.QueueGraphUpdates();
			this.FlushWorkItems(true, true);
		}
	}

	// Token: 0x06000102 RID: 258 RVA: 0x0000A437 File Offset: 0x00008837
	public void FlushWorkItems(bool unblockOnComplete = true, bool block = false)
	{
		this.BlockUntilPathQueueBlocked();
		this.PerformBlockingActions(block, unblockOnComplete);
	}

	// Token: 0x06000103 RID: 259 RVA: 0x0000A448 File Offset: 0x00008848
	private void QueueGraphUpdatesInternal()
	{
		this.isRegisteredForUpdate = false;
		bool flag = false;
		while (this.graphUpdateQueue.Count > 0)
		{
			GraphUpdateObject graphUpdateObject = this.graphUpdateQueue.Dequeue();
			if (graphUpdateObject.requiresFloodFill)
			{
				flag = true;
			}
			IEnumerator enumerator = this.astarData.GetUpdateableGraphs().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					IUpdatableGraph updatableGraph = (IUpdatableGraph)obj;
					NavGraph graph = updatableGraph as NavGraph;
					if (graphUpdateObject.nnConstraint == null || graphUpdateObject.nnConstraint.SuitableGraph(AstarPath.active.astarData.GetGraphIndex(graph), graph))
					{
						AstarPath.GUOSingle item = default(AstarPath.GUOSingle);
						item.order = AstarPath.GraphUpdateOrder.GraphUpdate;
						item.obj = graphUpdateObject;
						item.graph = updatableGraph;
						this.graphUpdateQueueRegular.Enqueue(item);
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
		if (flag)
		{
			AstarPath.GUOSingle item2 = default(AstarPath.GUOSingle);
			item2.order = AstarPath.GraphUpdateOrder.FloodFill;
			this.graphUpdateQueueRegular.Enqueue(item2);
		}
		this.debugPath = null;
		GraphModifier.TriggerEvent(GraphModifier.EventType.PreUpdate);
	}

	// Token: 0x06000104 RID: 260 RVA: 0x0000A578 File Offset: 0x00008978
	private bool ProcessGraphUpdates(bool force)
	{
		if (force)
		{
			this.processingGraphUpdatesAsync.WaitOne();
		}
		else if (!this.processingGraphUpdatesAsync.WaitOne(0))
		{
			return false;
		}
		if (this.graphUpdateQueueAsync.Count != 0)
		{
			throw new Exception("Queue should be empty at this stage");
		}
		while (this.graphUpdateQueueRegular.Count > 0)
		{
			AstarPath.GUOSingle item = this.graphUpdateQueueRegular.Peek();
			GraphUpdateThreading graphUpdateThreading = (item.order != AstarPath.GraphUpdateOrder.FloodFill) ? item.graph.CanUpdateAsync(item.obj) : GraphUpdateThreading.SeparateThread;
			bool flag = force;
			if (!Application.isPlaying || this.graphUpdateThread == null || !this.graphUpdateThread.IsAlive)
			{
				flag = true;
			}
			if (!flag && graphUpdateThreading == GraphUpdateThreading.SeparateAndUnityInit)
			{
				if (this.graphUpdateQueueAsync.Count > 0)
				{
					this.processingGraphUpdatesAsync.Reset();
					this.graphUpdateAsyncEvent.Set();
					return false;
				}
				item.graph.UpdateAreaInit(item.obj);
				this.graphUpdateQueueRegular.Dequeue();
				this.graphUpdateQueueAsync.Enqueue(item);
				this.processingGraphUpdatesAsync.Reset();
				this.graphUpdateAsyncEvent.Set();
				return false;
			}
			else if (!flag && graphUpdateThreading == GraphUpdateThreading.SeparateThread)
			{
				this.graphUpdateQueueRegular.Dequeue();
				this.graphUpdateQueueAsync.Enqueue(item);
			}
			else if (this.graphUpdateQueueAsync.Count > 0)
			{
				if (force)
				{
					throw new Exception("This should not happen");
				}
				this.processingGraphUpdatesAsync.Reset();
				this.graphUpdateAsyncEvent.Set();
				return false;
			}
			else
			{
				this.graphUpdateQueueRegular.Dequeue();
				if (item.order == AstarPath.GraphUpdateOrder.FloodFill)
				{
					this.FloodFill();
				}
				else
				{
					if (graphUpdateThreading == GraphUpdateThreading.SeparateAndUnityInit)
					{
						try
						{
							item.graph.UpdateAreaInit(item.obj);
						}
						catch (Exception arg)
						{
							Debug.LogError("Error while initializing GraphUpdates\n" + arg);
						}
					}
					try
					{
						item.graph.UpdateArea(item.obj);
					}
					catch (Exception arg2)
					{
						Debug.LogError("Error while updating graphs\n" + arg2);
					}
				}
			}
		}
		if (this.graphUpdateQueueAsync.Count > 0)
		{
			this.processingGraphUpdatesAsync.Reset();
			this.graphUpdateAsyncEvent.Set();
			return false;
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.PostUpdate);
		if (AstarPath.OnGraphsUpdated != null)
		{
			AstarPath.OnGraphsUpdated(this);
		}
		return true;
	}

	// Token: 0x06000105 RID: 261 RVA: 0x0000A80C File Offset: 0x00008C0C
	private void ProcessGraphUpdatesAsync(object _astar)
	{
		AstarPath astarPath = _astar as AstarPath;
		if (object.ReferenceEquals(astarPath, null))
		{
			Debug.LogError("ProcessGraphUpdatesAsync started with invalid parameter _astar (was no AstarPath object)");
			return;
		}
		while (!astarPath.pathQueue.IsTerminating)
		{
			this.graphUpdateAsyncEvent.WaitOne();
			if (astarPath.pathQueue.IsTerminating)
			{
				this.graphUpdateQueueAsync.Clear();
				this.processingGraphUpdatesAsync.Set();
				return;
			}
			while (this.graphUpdateQueueAsync.Count > 0)
			{
				AstarPath.GUOSingle guosingle = this.graphUpdateQueueAsync.Dequeue();
				try
				{
					if (guosingle.order == AstarPath.GraphUpdateOrder.GraphUpdate)
					{
						guosingle.graph.UpdateArea(guosingle.obj);
					}
					else
					{
						if (guosingle.order != AstarPath.GraphUpdateOrder.FloodFill)
						{
							throw new NotSupportedException(string.Empty + guosingle.order);
						}
						astarPath.FloodFill();
					}
				}
				catch (Exception arg)
				{
					Debug.LogError("Exception while updating graphs:\n" + arg);
				}
			}
			this.processingGraphUpdatesAsync.Set();
		}
	}

	// Token: 0x06000106 RID: 262 RVA: 0x0000A934 File Offset: 0x00008D34
	public void FlushThreadSafeCallbacks()
	{
		if (AstarPath.OnThreadSafeCallback == null)
		{
			return;
		}
		this.BlockUntilPathQueueBlocked();
		this.PerformBlockingActions(false, true);
	}

	// Token: 0x06000107 RID: 263 RVA: 0x0000A94F File Offset: 0x00008D4F
	[ContextMenu("Log Profiler")]
	public void LogProfiler()
	{
	}

	// Token: 0x06000108 RID: 264 RVA: 0x0000A951 File Offset: 0x00008D51
	[ContextMenu("Reset Profiler")]
	public void ResetProfiler()
	{
	}

	// Token: 0x06000109 RID: 265 RVA: 0x0000A954 File Offset: 0x00008D54
	public static int CalculateThreadCount(ThreadCount count)
	{
		if (count != ThreadCount.AutomaticLowLoad && count != ThreadCount.AutomaticHighLoad)
		{
			return (int)count;
		}
		int num = Mathf.Max(1, SystemInfo.processorCount);
		int num2 = SystemInfo.systemMemorySize;
		if (num2 <= 0)
		{
			Debug.LogError("Machine reporting that is has <= 0 bytes of RAM. This is definitely not true, assuming 1 GiB");
			num2 = 1024;
		}
		if (num <= 1)
		{
			return 0;
		}
		if (num2 <= 512)
		{
			return 0;
		}
		if (count == ThreadCount.AutomaticHighLoad)
		{
			if (num2 <= 1024)
			{
				num = Math.Min(num, 2);
			}
		}
		else
		{
			num /= 2;
			num = Mathf.Max(1, num);
			if (num2 <= 1024)
			{
				num = Math.Min(num, 2);
			}
			num = Math.Min(num, 6);
		}
		return num;
	}

	// Token: 0x0600010A RID: 266 RVA: 0x0000A9FC File Offset: 0x00008DFC
	public void Awake()
	{
		AstarPath.active = this;
		if (UnityEngine.Object.FindObjectsOfType(typeof(AstarPath)).Length > 1)
		{
			Debug.LogError("You should NOT have more than one AstarPath component in the scene at any time.\nThis can cause serious errors since the AstarPath component builds around a singleton pattern.");
		}
		base.useGUILayout = false;
		AstarPath.isEditor = Application.isEditor;
		if (AstarPath.OnAwakeSettings != null)
		{
			AstarPath.OnAwakeSettings();
		}
		GraphModifier.FindAllModifiers();
		RelevantGraphSurface.FindAllGraphSurfaces();
		int num = AstarPath.CalculateThreadCount(this.threadCount);
		AstarPath.threads = new Thread[num];
		AstarPath.threadInfos = new PathThreadInfo[Math.Max(num, 1)];
		this.pathQueue = new ThreadControlQueue(AstarPath.threadInfos.Length);
		for (int i = 0; i < AstarPath.threadInfos.Length; i++)
		{
			AstarPath.threadInfos[i] = new PathThreadInfo(i, this, new PathHandler(i, AstarPath.threadInfos.Length));
		}
		for (int j = 0; j < AstarPath.threads.Length; j++)
		{
			AstarPath.threads[j] = new Thread(new ParameterizedThreadStart(AstarPath.CalculatePathsThreaded));
			AstarPath.threads[j].Name = "Pathfinding Thread " + j;
			AstarPath.threads[j].IsBackground = true;
		}
		if (num == 0)
		{
			AstarPath.threadEnumerator = AstarPath.CalculatePaths(AstarPath.threadInfos[0]);
		}
		else
		{
			AstarPath.threadEnumerator = null;
		}
		for (int k = 0; k < AstarPath.threads.Length; k++)
		{
			if (this.logPathResults == PathLog.Heavy)
			{
				Debug.Log("Starting pathfinding thread " + k);
			}
			AstarPath.threads[k].Start(AstarPath.threadInfos[k]);
		}
		if (num != 0)
		{
			this.graphUpdateThread = new Thread(new ParameterizedThreadStart(this.ProcessGraphUpdatesAsync));
			this.graphUpdateThread.IsBackground = true;
			this.graphUpdateThread.Start(this);
		}
		this.Initialize();
		this.FlushWorkItems(true, false);
		this.euclideanEmbedding.dirty = true;
		if (this.scanOnStartup && (!this.astarData.cacheStartup || this.astarData.file_cachedStartup == null))
		{
			this.Scan();
		}
	}

	// Token: 0x0600010B RID: 267 RVA: 0x0000AC40 File Offset: 0x00009040
	public void VerifyIntegrity()
	{
		if (AstarPath.active != this)
		{
			throw new Exception("Singleton pattern broken. Make sure you only have one AstarPath object in the scene");
		}
		if (this.astarData == null)
		{
			throw new NullReferenceException("AstarData is null... Astar not set up correctly?");
		}
		if (this.astarData.graphs == null)
		{
			this.astarData.graphs = new NavGraph[0];
		}
		if (this.pathQueue == null && !Application.isPlaying)
		{
			this.pathQueue = new ThreadControlQueue(0);
		}
		if (AstarPath.threadInfos == null && !Application.isPlaying)
		{
			AstarPath.threadInfos = new PathThreadInfo[0];
		}
		if (AstarPath.IsUsingMultithreading)
		{
		}
	}

	// Token: 0x0600010C RID: 268 RVA: 0x0000ACEC File Offset: 0x000090EC
	public void SetUpReferences()
	{
		AstarPath.active = this;
		if (this.astarData == null)
		{
			this.astarData = new AstarData();
		}
		if (this.astarData.userConnections == null)
		{
			this.astarData.userConnections = new UserConnection[0];
		}
		if (this.colorSettings == null)
		{
			this.colorSettings = new AstarColor();
		}
		this.colorSettings.OnEnable();
	}

	// Token: 0x0600010D RID: 269 RVA: 0x0000AD58 File Offset: 0x00009158
	private void Initialize()
	{
		this.SetUpReferences();
		this.astarData.FindGraphTypes();
		this.astarData.Awake();
		this.astarData.UpdateShortcuts();
		for (int i = 0; i < this.astarData.graphs.Length; i++)
		{
			if (this.astarData.graphs[i] != null)
			{
				this.astarData.graphs[i].Awake();
			}
		}
	}

	// Token: 0x0600010E RID: 270 RVA: 0x0000ADD0 File Offset: 0x000091D0
	public void OnDestroy()
	{
		if (this.logPathResults == PathLog.Heavy)
		{
			Debug.Log("+++ AstarPath Component Destroyed - Cleaning Up Pathfinding Data +++");
		}
		if (AstarPath.active != this)
		{
			return;
		}
		this.pathQueue.TerminateReceivers();
		this.BlockUntilPathQueueBlocked();
		this.euclideanEmbedding.dirty = false;
		this.FlushWorkItems(true, false);
		if (this.logPathResults == PathLog.Heavy)
		{
			Debug.Log("Processing Eventual Work Items");
		}
		this.graphUpdateAsyncEvent.Set();
		if (AstarPath.threads != null)
		{
			for (int i = 0; i < AstarPath.threads.Length; i++)
			{
				if (!AstarPath.threads[i].Join(50))
				{
					Debug.LogError("Could not terminate pathfinding thread[" + i + "] in 50ms, trying Thread.Abort");
					AstarPath.threads[i].Abort();
				}
			}
		}
		if (this.logPathResults == PathLog.Heavy)
		{
			Debug.Log("Returning Paths");
		}
		this.ReturnPaths(false);
		AstarPath.pathReturnStack.PopAll();
		if (this.logPathResults == PathLog.Heavy)
		{
			Debug.Log("Destroying Graphs");
		}
		this.astarData.OnDestroy();
		if (this.logPathResults == PathLog.Heavy)
		{
			Debug.Log("Cleaning up variables");
		}
		this.floodStack = null;
		this.graphUpdateQueue = null;
		this.OnDrawGizmosCallback = null;
		AstarPath.OnAwakeSettings = null;
		AstarPath.OnGraphPreScan = null;
		AstarPath.OnGraphPostScan = null;
		AstarPath.OnPathPreSearch = null;
		AstarPath.OnPathPostSearch = null;
		AstarPath.OnPreScan = null;
		AstarPath.OnPostScan = null;
		AstarPath.OnLatePostScan = null;
		AstarPath.On65KOverflow = null;
		AstarPath.OnGraphsUpdated = null;
		AstarPath.OnThreadSafeCallback = null;
		AstarPath.threads = null;
		AstarPath.threadInfos = null;
		AstarPath.PathsCompleted = 0;
		AstarPath.active = null;
	}

	// Token: 0x0600010F RID: 271 RVA: 0x0000AF74 File Offset: 0x00009374
	public void FloodFill(GraphNode seed)
	{
		this.FloodFill(seed, this.lastUniqueAreaIndex + 1u);
		this.lastUniqueAreaIndex += 1u;
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0000AF94 File Offset: 0x00009394
	public void FloodFill(GraphNode seed, uint area)
	{
		if (area > 131071u)
		{
			Debug.LogError("Too high area index - The maximum area index is " + 131071u);
			return;
		}
		if (area < 0u)
		{
			Debug.LogError("Too low area index - The minimum area index is 0");
			return;
		}
		if (this.floodStack == null)
		{
			this.floodStack = new Stack<GraphNode>(1024);
		}
		Stack<GraphNode> stack = this.floodStack;
		stack.Clear();
		stack.Push(seed);
		seed.Area = area;
		while (stack.Count > 0)
		{
			stack.Pop().FloodFill(stack, area);
		}
	}

	// Token: 0x06000111 RID: 273 RVA: 0x0000B02C File Offset: 0x0000942C
	[ContextMenu("Flood Fill Graphs")]
	public void FloodFill()
	{
		this.queuedWorkItemFloodFill = false;
		if (this.astarData.graphs == null)
		{
			return;
		}
		uint area = 0u;
		this.lastUniqueAreaIndex = 0u;
		if (this.floodStack == null)
		{
			this.floodStack = new Stack<GraphNode>(1024);
		}
		Stack<GraphNode> stack = this.floodStack;
		for (int i = 0; i < this.graphs.Length; i++)
		{
			NavGraph navGraph = this.graphs[i];
			if (navGraph != null)
			{
				navGraph.GetNodes(delegate(GraphNode node)
				{
					node.Area = 0u;
					return true;
				});
			}
		}
		int smallAreasDetected = 0;
		bool warnAboutAreas = false;
		List<GraphNode> smallAreaList = ListPool<GraphNode>.Claim();
		for (int j = 0; j < this.graphs.Length; j++)
		{
			NavGraph navGraph2 = this.graphs[j];
			if (navGraph2 != null)
			{
				GraphNodeDelegateCancelable del = delegate(GraphNode node)
				{
					if (node.Walkable && node.Area == 0u)
					{
						uint area;
						area += 1u;
						area = area;
						if (area > 131071u)
						{
							if (smallAreaList.Count > 0)
							{
								GraphNode graphNode = smallAreaList[smallAreaList.Count - 1];
								area = graphNode.Area;
								smallAreaList.RemoveAt(smallAreaList.Count - 1);
								stack.Clear();
								stack.Push(graphNode);
								graphNode.Area = 131071u;
								while (stack.Count > 0)
								{
									stack.Pop().FloodFill(stack, 131071u);
								}
								smallAreasDetected++;
							}
							else
							{
								area -= 1u;
								area = area;
								warnAboutAreas = true;
							}
						}
						stack.Clear();
						stack.Push(node);
						int num = 1;
						node.Area = area;
						while (stack.Count > 0)
						{
							num++;
							stack.Pop().FloodFill(stack, area);
						}
						if (num < this.minAreaSize)
						{
							smallAreaList.Add(node);
						}
					}
					return true;
				};
				navGraph2.GetNodes(del);
			}
		}
		this.lastUniqueAreaIndex = area;
		if (warnAboutAreas)
		{
			Debug.LogError("Too many areas - The maximum number of areas is " + 131071u + ". Try raising the A* Inspector -> Settings -> Min Area Size value. Enable the optimization ASTAR_MORE_AREAS under the Optimizations tab.");
		}
		if (smallAreasDetected > 0)
		{
			AstarPath.AstarLog(string.Concat(new object[]
			{
				smallAreasDetected,
				" small areas were detected (fewer than ",
				this.minAreaSize,
				" nodes),these might have the same IDs as other areas, but it shouldn't affect pathfinding in any significant way (you might get All Nodes Searched as a reason for path failure).\nWhich areas are defined as 'small' is controlled by the 'Min Area Size' variable, it can be changed in the A* inspector-->Settings-->Min Area Size\nThe small areas will use the area id ",
				131071u
			}));
		}
		ListPool<GraphNode>.Release(smallAreaList);
	}

	// Token: 0x06000112 RID: 274 RVA: 0x0000B1D4 File Offset: 0x000095D4
	public int GetNewNodeIndex()
	{
		if (this.nodeIndexPool.Count > 0)
		{
			return this.nodeIndexPool.Pop();
		}
		return this.nextNodeIndex++;
	}

	// Token: 0x06000113 RID: 275 RVA: 0x0000B210 File Offset: 0x00009610
	public void InitializeNode(GraphNode node)
	{
		if (!this.pathQueue.AllReceiversBlocked)
		{
			throw new Exception("Trying to initialize a node when it is not safe to initialize any nodes. Must be done during a graph update");
		}
		if (AstarPath.threadInfos == null)
		{
			AstarPath.threadInfos = new PathThreadInfo[0];
		}
		for (int i = 0; i < AstarPath.threadInfos.Length; i++)
		{
			AstarPath.threadInfos[i].runData.InitializeNode(node);
		}
	}

	// Token: 0x06000114 RID: 276 RVA: 0x0000B27C File Offset: 0x0000967C
	public void DestroyNode(GraphNode node)
	{
		if (node.NodeIndex == -1)
		{
			return;
		}
		this.nodeIndexPool.Push(node.NodeIndex);
		if (AstarPath.threadInfos == null)
		{
			AstarPath.threadInfos = new PathThreadInfo[0];
		}
		for (int i = 0; i < AstarPath.threadInfos.Length; i++)
		{
			AstarPath.threadInfos[i].runData.DestroyNode(node);
		}
	}

	// Token: 0x06000115 RID: 277 RVA: 0x0000B2EC File Offset: 0x000096EC
	public void BlockUntilPathQueueBlocked()
	{
		if (this.pathQueue == null)
		{
			return;
		}
		this.pathQueue.Block();
		while (!this.pathQueue.AllReceiversBlocked)
		{
			if (AstarPath.IsUsingMultithreading)
			{
				Thread.Sleep(1);
			}
			else
			{
				AstarPath.threadEnumerator.MoveNext();
			}
		}
	}

	// Token: 0x06000116 RID: 278 RVA: 0x0000B345 File Offset: 0x00009745
	public void Scan()
	{
		this.ScanLoop(null);
	}

	// Token: 0x06000117 RID: 279 RVA: 0x0000B34E File Offset: 0x0000974E
	public void ScanSpecific(NavGraph graph)
	{
		this.ScanSpecific(graph, null);
	}

	// Token: 0x06000118 RID: 280 RVA: 0x0000B358 File Offset: 0x00009758
	public void ScanSpecific(NavGraph graph, OnScanStatus statusCallback)
	{
		if (this.graphs == null)
		{
			return;
		}
		this.isScanning = true;
		this.euclideanEmbedding.dirty = false;
		this.VerifyIntegrity();
		this.BlockUntilPathQueueBlocked();
		if (!Application.isPlaying)
		{
			GraphModifier.FindAllModifiers();
			RelevantGraphSurface.FindAllGraphSurfaces();
		}
		RelevantGraphSurface.UpdateAllPositions();
		this.astarData.UpdateShortcuts();
		if (statusCallback != null)
		{
			statusCallback(new Progress(0.05f, "Pre processing graphs"));
		}
		if (AstarPath.OnPreScan != null)
		{
			AstarPath.OnPreScan(this);
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.PreScan);
		DateTime utcNow = DateTime.UtcNow;
		for (int j = 0; j < this.graphs.Length; j++)
		{
			if (this.graphs[j] != null)
			{
				this.graphs[j].GetNodes(delegate(GraphNode node)
				{
					node.Destroy();
					return true;
				});
			}
		}
		int i;
		for (i = 0; i < this.graphs.Length; i++)
		{
			if (this.graphs[i] == graph)
			{
				if (graph != null)
				{
					if (AstarPath.OnGraphPreScan != null)
					{
						if (statusCallback != null)
						{
							statusCallback(new Progress(AstarMath.MapToRange(0.1f, 0.7f, (float)i / (float)this.graphs.Length), string.Concat(new object[]
							{
								"Scanning graph ",
								i + 1,
								" of ",
								this.graphs.Length,
								" - Pre processing"
							})));
						}
						AstarPath.OnGraphPreScan(graph);
					}
					float minp = AstarMath.MapToRange(0.1f, 0.7f, (float)i / (float)this.graphs.Length);
					float maxp = AstarMath.MapToRange(0.1f, 0.7f, ((float)i + 0.95f) / (float)this.graphs.Length);
					if (statusCallback != null)
					{
						statusCallback(new Progress(minp, string.Concat(new object[]
						{
							"Scanning graph ",
							i + 1,
							" of ",
							this.graphs.Length
						})));
					}
					OnScanStatus statusCallback2 = null;
					if (statusCallback != null)
					{
						statusCallback2 = delegate(Progress p)
						{
							p.progress = AstarMath.MapToRange(minp, maxp, p.progress);
							statusCallback(p);
						};
					}
					graph.ScanInternal(statusCallback2);
					graph.GetNodes(delegate(GraphNode node)
					{
						node.GraphIndex = (uint)i;
						return true;
					});
					if (AstarPath.OnGraphPostScan != null)
					{
						if (statusCallback != null)
						{
							statusCallback(new Progress(AstarMath.MapToRange(0.1f, 0.7f, ((float)i + 0.95f) / (float)this.graphs.Length), string.Concat(new object[]
							{
								"Scanning graph ",
								i + 1,
								" of ",
								this.graphs.Length,
								" - Post processing"
							})));
						}
						AstarPath.OnGraphPostScan(graph);
					}
					break;
				}
				if (statusCallback != null)
				{
					statusCallback(new Progress(AstarMath.MapTo(0.05f, 0.7f, ((float)i + 0.5f) / (float)(this.graphs.Length + 1)), string.Concat(new object[]
					{
						"Skipping graph ",
						i + 1,
						" of ",
						this.graphs.Length,
						" because it is null"
					})));
				}
			}
		}
		if (statusCallback != null)
		{
			statusCallback(new Progress(0.8f, "Post processing graphs"));
		}
		if (AstarPath.OnPostScan != null)
		{
			AstarPath.OnPostScan(this);
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.PostScan);
		this.ApplyLinks();
		try
		{
			this.FlushWorkItems(false, true);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		this.isScanning = false;
		if (statusCallback != null)
		{
			statusCallback(new Progress(0.9f, "Computing areas"));
		}
		this.FloodFill();
		this.VerifyIntegrity();
		if (statusCallback != null)
		{
			statusCallback(new Progress(0.95f, "Late post processing"));
		}
		if (AstarPath.OnLatePostScan != null)
		{
			AstarPath.OnLatePostScan(this);
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.LatePostScan);
		this.euclideanEmbedding.dirty = true;
		this.euclideanEmbedding.RecalculatePivots();
		this.PerformBlockingActions(true, true);
		this.lastScanTime = (float)(DateTime.UtcNow - utcNow).TotalSeconds;
		GC.Collect();
		AstarPath.AstarLog("Scanning - Process took " + (this.lastScanTime * 1000f).ToString("0") + " ms to complete");
	}

	// Token: 0x06000119 RID: 281 RVA: 0x0000B8B8 File Offset: 0x00009CB8
	public void ScanLoop(OnScanStatus statusCallback)
	{
		if (this.graphs == null)
		{
			return;
		}
		this.isScanning = true;
		this.euclideanEmbedding.dirty = false;
		this.VerifyIntegrity();
		this.BlockUntilPathQueueBlocked();
		if (!Application.isPlaying)
		{
			GraphModifier.FindAllModifiers();
			RelevantGraphSurface.FindAllGraphSurfaces();
		}
		RelevantGraphSurface.UpdateAllPositions();
		this.astarData.UpdateShortcuts();
		if (statusCallback != null)
		{
			statusCallback(new Progress(0.05f, "Pre processing graphs"));
		}
		if (AstarPath.OnPreScan != null)
		{
			AstarPath.OnPreScan(this);
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.PreScan);
		DateTime utcNow = DateTime.UtcNow;
		for (int j = 0; j < this.graphs.Length; j++)
		{
			if (this.graphs[j] != null)
			{
				this.graphs[j].GetNodes(delegate(GraphNode node)
				{
					node.Destroy();
					return true;
				});
			}
		}
		int i;
		for (i = 0; i < this.graphs.Length; i++)
		{
			NavGraph navGraph = this.graphs[i];
			if (navGraph == null)
			{
				if (statusCallback != null)
				{
					statusCallback(new Progress(AstarMath.MapTo(0.05f, 0.7f, ((float)i + 0.5f) / (float)(this.graphs.Length + 1)), string.Concat(new object[]
					{
						"Skipping graph ",
						i + 1,
						" of ",
						this.graphs.Length,
						" because it is null"
					})));
				}
			}
			else
			{
				if (AstarPath.OnGraphPreScan != null)
				{
					if (statusCallback != null)
					{
						statusCallback(new Progress(AstarMath.MapToRange(0.1f, 0.7f, (float)i / (float)this.graphs.Length), string.Concat(new object[]
						{
							"Scanning graph ",
							i + 1,
							" of ",
							this.graphs.Length,
							" - Pre processing"
						})));
					}
					AstarPath.OnGraphPreScan(navGraph);
				}
				float minp = AstarMath.MapToRange(0.1f, 0.7f, (float)i / (float)this.graphs.Length);
				float maxp = AstarMath.MapToRange(0.1f, 0.7f, ((float)i + 0.95f) / (float)this.graphs.Length);
				if (statusCallback != null)
				{
					statusCallback(new Progress(minp, string.Concat(new object[]
					{
						"Scanning graph ",
						i + 1,
						" of ",
						this.graphs.Length
					})));
				}
				OnScanStatus statusCallback2 = null;
				if (statusCallback != null)
				{
					statusCallback2 = delegate(Progress p)
					{
						p.progress = AstarMath.MapToRange(minp, maxp, p.progress);
						statusCallback(p);
					};
				}
				navGraph.ScanInternal(statusCallback2);
				navGraph.GetNodes(delegate(GraphNode node)
				{
					node.GraphIndex = (uint)i;
					return true;
				});
				if (AstarPath.OnGraphPostScan != null)
				{
					if (statusCallback != null)
					{
						statusCallback(new Progress(AstarMath.MapToRange(0.1f, 0.7f, ((float)i + 0.95f) / (float)this.graphs.Length), string.Concat(new object[]
						{
							"Scanning graph ",
							i + 1,
							" of ",
							this.graphs.Length,
							" - Post processing"
						})));
					}
					AstarPath.OnGraphPostScan(navGraph);
				}
			}
		}
		if (statusCallback != null)
		{
			statusCallback(new Progress(0.8f, "Post processing graphs"));
		}
		if (AstarPath.OnPostScan != null)
		{
			AstarPath.OnPostScan(this);
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.PostScan);
		this.ApplyLinks();
		try
		{
			this.FlushWorkItems(false, true);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		this.isScanning = false;
		if (statusCallback != null)
		{
			statusCallback(new Progress(0.9f, "Computing areas"));
		}
		this.FloodFill();
		this.VerifyIntegrity();
		if (statusCallback != null)
		{
			statusCallback(new Progress(0.95f, "Late post processing"));
		}
		if (AstarPath.OnLatePostScan != null)
		{
			AstarPath.OnLatePostScan(this);
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.LatePostScan);
		this.euclideanEmbedding.dirty = true;
		this.euclideanEmbedding.RecalculatePivots();
		this.PerformBlockingActions(true, true);
		this.lastScanTime = (float)(DateTime.UtcNow - utcNow).TotalSeconds;
		GC.Collect();
		AstarPath.AstarLog("Scanning - Process took " + (this.lastScanTime * 1000f).ToString("0") + " ms to complete");
	}

	// Token: 0x0600011A RID: 282 RVA: 0x0000BE14 File Offset: 0x0000A214
	public void ApplyLinks()
	{
		if (this.astarData.userConnections != null && this.astarData.userConnections.Length > 0)
		{
			Debug.LogWarning("<b>Deleting all links now</b>, but saving graph data in backup variable.\nCreating replacement links using the new system, stored under the <i>Links</i> GameObject.");
			GameObject gameObject = new GameObject("Links");
			Dictionary<Int3, GameObject> dictionary = new Dictionary<Int3, GameObject>();
			for (int i = 0; i < this.astarData.userConnections.Length; i++)
			{
				UserConnection userConnection = this.astarData.userConnections[i];
				GameObject gameObject2 = (!dictionary.ContainsKey((Int3)userConnection.p1)) ? new GameObject("Link " + i) : dictionary[(Int3)userConnection.p1];
				GameObject gameObject3 = (!dictionary.ContainsKey((Int3)userConnection.p2)) ? new GameObject("Link " + i) : dictionary[(Int3)userConnection.p2];
				gameObject2.transform.parent = gameObject.transform;
				gameObject3.transform.parent = gameObject.transform;
				dictionary[(Int3)userConnection.p1] = gameObject2;
				dictionary[(Int3)userConnection.p2] = gameObject3;
				gameObject2.transform.position = userConnection.p1;
				gameObject3.transform.position = userConnection.p2;
				NodeLink nodeLink = gameObject2.AddComponent<NodeLink>();
				nodeLink.end = gameObject3.transform;
				nodeLink.deleteConnection = !userConnection.enable;
			}
			this.astarData.userConnections = null;
			this.astarData.data_backup = this.astarData.GetData();
			throw new NotSupportedException("<b>Links have been deprecated</b>. Please use the component <b>NodeLink</b> instead. Create two GameObjects around the points you want to link, then press <b>Cmd+Alt+L</b> ( <b>Ctrl+Alt+L</b> on windows) to link them. See <b>Menubar -> Edit -> Pathfinding</b>.");
		}
	}

	// Token: 0x0600011B RID: 283 RVA: 0x0000BFD4 File Offset: 0x0000A3D4
	public static void WaitForPath(Path p)
	{
		if (AstarPath.active == null)
		{
			throw new Exception("Pathfinding is not correctly initialized in this scene (yet?). AstarPath.active is null.\nDo not call this function in Awake");
		}
		if (p == null)
		{
			throw new ArgumentNullException("Path must not be null");
		}
		if (AstarPath.active.pathQueue.IsTerminating)
		{
			return;
		}
		if (p.GetState() == PathState.Created)
		{
			throw new Exception("The specified path has not been started yet.");
		}
		AstarPath.waitForPathDepth++;
		if (AstarPath.waitForPathDepth == 5)
		{
			Debug.LogError("You are calling the WaitForPath function recursively (maybe from a path callback). Please don't do this.");
		}
		if (p.GetState() < PathState.ReturnQueue)
		{
			if (AstarPath.IsUsingMultithreading)
			{
				while (p.GetState() < PathState.ReturnQueue)
				{
					if (AstarPath.active.pathQueue.IsTerminating)
					{
						AstarPath.waitForPathDepth--;
						throw new Exception("Pathfinding Threads seems to have crashed.");
					}
					Thread.Sleep(1);
					AstarPath.active.PerformBlockingActions(false, true);
				}
			}
			else
			{
				while (p.GetState() < PathState.ReturnQueue)
				{
					if (AstarPath.active.pathQueue.IsEmpty && p.GetState() != PathState.Processing)
					{
						AstarPath.waitForPathDepth--;
						throw new Exception("Critical error. Path Queue is empty but the path state is '" + p.GetState() + "'");
					}
					AstarPath.threadEnumerator.MoveNext();
					AstarPath.active.PerformBlockingActions(false, true);
				}
			}
		}
		AstarPath.active.ReturnPaths(false);
		AstarPath.waitForPathDepth--;
	}

	// Token: 0x0600011C RID: 284 RVA: 0x0000C14D File Offset: 0x0000A54D
	[Obsolete("The threadSafe parameter has been deprecated")]
	public static void RegisterSafeUpdate(OnVoidDelegate callback, bool threadSafe)
	{
		AstarPath.RegisterSafeUpdate(callback);
	}

	// Token: 0x0600011D RID: 285 RVA: 0x0000C158 File Offset: 0x0000A558
	public static void RegisterSafeUpdate(OnVoidDelegate callback)
	{
		if (callback == null || !Application.isPlaying)
		{
			return;
		}
		if (AstarPath.active.pathQueue.AllReceiversBlocked)
		{
			AstarPath.active.pathQueue.Lock();
			try
			{
				if (AstarPath.active.pathQueue.AllReceiversBlocked)
				{
					callback();
					return;
				}
			}
			finally
			{
				AstarPath.active.pathQueue.Unlock();
			}
		}
		object obj = AstarPath.safeUpdateLock;
		lock (obj)
		{
			AstarPath.OnThreadSafeCallback = (OnVoidDelegate)Delegate.Combine(AstarPath.OnThreadSafeCallback, callback);
		}
		AstarPath.active.pathQueue.Block();
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0000C228 File Offset: 0x0000A628
	private static void InterruptPathfinding()
	{
		AstarPath.active.pathQueue.Block();
	}

	// Token: 0x0600011F RID: 287 RVA: 0x0000C23C File Offset: 0x0000A63C
	public static void StartPath(Path p, bool pushToFront = false)
	{
		if (object.ReferenceEquals(AstarPath.active, null))
		{
			Debug.LogError("There is no AstarPath object in the scene");
			return;
		}
		if (p.GetState() != PathState.Created)
		{
			throw new Exception(string.Concat(new object[]
			{
				"The path has an invalid state. Expected ",
				PathState.Created,
				" found ",
				p.GetState(),
				"\nMake sure you are not requesting the same path twice"
			}));
		}
		if (AstarPath.active.pathQueue.IsTerminating)
		{
			p.Error();
			p.LogError("No new paths are accepted");
			return;
		}
		if (AstarPath.active.graphs == null || AstarPath.active.graphs.Length == 0)
		{
			Debug.LogError("There are no graphs in the scene");
			p.Error();
			p.LogError("There are no graphs in the scene");
			Debug.LogError(p.errorLog);
			return;
		}
		p.Claim(AstarPath.active);
		p.AdvanceState(PathState.PathQueue);
		if (pushToFront)
		{
			AstarPath.active.pathQueue.PushFront(p);
		}
		else
		{
			AstarPath.active.pathQueue.Push(p);
		}
	}

	// Token: 0x06000120 RID: 288 RVA: 0x0000C35C File Offset: 0x0000A75C
	public void OnApplicationQuit()
	{
		if (this.logPathResults == PathLog.Heavy)
		{
			Debug.Log("+++ Application Quitting - Cleaning Up +++");
		}
		this.OnDestroy();
		if (AstarPath.threads == null)
		{
			return;
		}
		for (int i = 0; i < AstarPath.threads.Length; i++)
		{
			if (AstarPath.threads[i] != null && AstarPath.threads[i].IsAlive)
			{
				AstarPath.threads[i].Abort();
			}
		}
	}

	// Token: 0x06000121 RID: 289 RVA: 0x0000C3D4 File Offset: 0x0000A7D4
	public void ReturnPaths(bool timeSlice)
	{
		Path next = AstarPath.pathReturnStack.PopAll();
		if (this.pathReturnPop == null)
		{
			this.pathReturnPop = next;
		}
		else
		{
			Path next2 = this.pathReturnPop;
			while (next2.next != null)
			{
				next2 = next2.next;
			}
			next2.next = next;
		}
		long num = (!timeSlice) ? 0L : (DateTime.UtcNow.Ticks + 10000L);
		int num2 = 0;
		while (this.pathReturnPop != null)
		{
			Path path = this.pathReturnPop;
			this.pathReturnPop = this.pathReturnPop.next;
			path.next = null;
			path.ReturnPath();
			path.AdvanceState(PathState.Returned);
			path.ReleaseSilent(this);
			num2++;
			if (num2 > 5 && timeSlice)
			{
				num2 = 0;
				if (DateTime.UtcNow.Ticks >= num)
				{
					return;
				}
			}
		}
	}

	// Token: 0x06000122 RID: 290 RVA: 0x0000C4C4 File Offset: 0x0000A8C4
	private static void CalculatePathsThreaded(object _threadInfo)
	{
		PathThreadInfo pathThreadInfo;
		try
		{
			pathThreadInfo = (PathThreadInfo)_threadInfo;
		}
		catch (Exception ex)
		{
			Debug.LogError("Arguments to pathfinding threads must be of type ThreadStartInfo\n" + ex);
			throw new ArgumentException("Argument must be of type ThreadStartInfo", ex);
		}
		AstarPath astar = pathThreadInfo.astar;
		try
		{
			PathHandler runData = pathThreadInfo.runData;
			if (runData.nodes == null)
			{
				throw new NullReferenceException("NodeRuns must be assigned to the threadInfo.runData.nodes field before threads are started\nthreadInfo is an argument to the thread functions");
			}
			long num = (long)(astar.maxFrameTime * 10000f);
			long num2 = DateTime.UtcNow.Ticks + num;
			for (;;)
			{
				Path path = astar.pathQueue.Pop();
				num = (long)(astar.maxFrameTime * 10000f);
				path.PrepareBase(runData);
				path.AdvanceState(PathState.Processing);
				if (AstarPath.OnPathPreSearch != null)
				{
					AstarPath.OnPathPreSearch(path);
				}
				long ticks = DateTime.UtcNow.Ticks;
				long num3 = 0L;
				path.Prepare();
				if (!path.IsDone())
				{
					astar.debugPath = path;
					path.Initialize();
					while (!path.IsDone())
					{
						path.CalculateStep(num2);
						path.searchIterations++;
						if (path.IsDone())
						{
							break;
						}
						num3 += DateTime.UtcNow.Ticks - ticks;
						Thread.Sleep(0);
						ticks = DateTime.UtcNow.Ticks;
						num2 = ticks + num;
						if (astar.pathQueue.IsTerminating)
						{
							path.Error();
						}
					}
					num3 += DateTime.UtcNow.Ticks - ticks;
					path.duration = (float)num3 * 0.0001f;
				}
				path.Cleanup();
				astar.LogPathResults(path);
				if (path.immediateCallback != null)
				{
					path.immediateCallback(path);
				}
				if (AstarPath.OnPathPostSearch != null)
				{
					AstarPath.OnPathPostSearch(path);
				}
				AstarPath.pathReturnStack.Push(path);
				path.AdvanceState(PathState.ReturnQueue);
				if (DateTime.UtcNow.Ticks > num2)
				{
					Thread.Sleep(1);
					num2 = DateTime.UtcNow.Ticks + num;
				}
			}
		}
		catch (Exception ex2)
		{
			if (ex2 is ThreadAbortException || ex2 is ThreadControlQueue.QueueTerminationException)
			{
				if (astar.logPathResults == PathLog.Heavy)
				{
					Debug.LogWarning("Shutting down pathfinding thread #" + pathThreadInfo.threadIndex + " with Thread.Abort call");
				}
				return;
			}
			Debug.LogException(ex2);
			Debug.LogError("Unhandled exception during pathfinding. Terminating.");
			astar.pathQueue.TerminateReceivers();
		}
		Debug.LogError("Error : This part should never be reached.");
		astar.pathQueue.ReceiverTerminated();
	}

	// Token: 0x06000123 RID: 291 RVA: 0x0000C7A0 File Offset: 0x0000ABA0
	private static IEnumerator CalculatePaths(object _threadInfo)
	{
		PathThreadInfo threadInfo;
		try
		{
			threadInfo = (PathThreadInfo)_threadInfo;
		}
		catch (Exception ex)
		{
			Debug.LogError("Arguments to pathfinding threads must be of type ThreadStartInfo\n" + ex);
			throw new ArgumentException("Argument must be of type ThreadStartInfo", ex);
		}
		int numPaths = 0;
		PathHandler runData = threadInfo.runData;
		AstarPath astar = threadInfo.astar;
		if (runData.nodes == null)
		{
			throw new NullReferenceException("NodeRuns must be assigned to the threadInfo.runData.nodes field before threads are started\nthreadInfo is an argument to the thread functions");
		}
		long maxTicks = (long)(AstarPath.active.maxFrameTime * 10000f);
		long targetTick = DateTime.UtcNow.Ticks + maxTicks;
		for (;;)
		{
			Path p = null;
			bool blockedBefore = false;
			while (p == null)
			{
				try
				{
					p = astar.pathQueue.PopNoBlock(blockedBefore);
					if (p == null)
					{
						blockedBefore = true;
					}
				}
				catch (ThreadControlQueue.QueueTerminationException)
				{
					yield break;
				}
				if (p == null)
				{
					yield return null;
				}
			}
			maxTicks = (long)(AstarPath.active.maxFrameTime * 10000f);
			p.PrepareBase(runData);
			p.AdvanceState(PathState.Processing);
			if (AstarPath.OnPathPreSearch != null)
			{
				AstarPath.OnPathPreSearch(p);
			}
			numPaths++;
			long startTicks = DateTime.UtcNow.Ticks;
			long totalTicks = 0L;
			p.Prepare();
			if (!p.IsDone())
			{
				AstarPath.active.debugPath = p;
				p.Initialize();
				while (!p.IsDone())
				{
					p.CalculateStep(targetTick);
					p.searchIterations++;
					if (p.IsDone())
					{
						break;
					}
					totalTicks += DateTime.UtcNow.Ticks - startTicks;
					yield return null;
					startTicks = DateTime.UtcNow.Ticks;
					if (astar.pathQueue.IsTerminating)
					{
						p.Error();
					}
					targetTick = DateTime.UtcNow.Ticks + maxTicks;
				}
				totalTicks += DateTime.UtcNow.Ticks - startTicks;
				p.duration = (float)totalTicks * 0.0001f;
			}
			p.Cleanup();
			AstarPath.active.LogPathResults(p);
			if (p.immediateCallback != null)
			{
				p.immediateCallback(p);
			}
			if (AstarPath.OnPathPostSearch != null)
			{
				AstarPath.OnPathPostSearch(p);
			}
			AstarPath.pathReturnStack.Push(p);
			p.AdvanceState(PathState.ReturnQueue);
			if (DateTime.UtcNow.Ticks > targetTick)
			{
				yield return null;
				targetTick = DateTime.UtcNow.Ticks + maxTicks;
				numPaths = 0;
			}
		}
		yield break;
	}

	// Token: 0x06000124 RID: 292 RVA: 0x0000C7BB File Offset: 0x0000ABBB
	public NNInfo GetNearest(Vector3 position)
	{
		return this.GetNearest(position, NNConstraint.None);
	}

	// Token: 0x06000125 RID: 293 RVA: 0x0000C7C9 File Offset: 0x0000ABC9
	public NNInfo GetNearest(Vector3 position, NNConstraint constraint)
	{
		return this.GetNearest(position, constraint, null);
	}

	// Token: 0x06000126 RID: 294 RVA: 0x0000C7D4 File Offset: 0x0000ABD4
	public NNInfo GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
	{
		if (this.graphs == null)
		{
			return default(NNInfo);
		}
		float num = float.PositiveInfinity;
		NNInfo result = default(NNInfo);
		int num2 = -1;
		for (int i = 0; i < this.graphs.Length; i++)
		{
			NavGraph navGraph = this.graphs[i];
			if (navGraph != null)
			{
				if (constraint.SuitableGraph(i, navGraph))
				{
					NNInfo nninfo;
					if (this.fullGetNearestSearch)
					{
						nninfo = navGraph.GetNearestForce(position, constraint);
					}
					else
					{
						nninfo = navGraph.GetNearest(position, constraint);
					}
					if (nninfo.node != null)
					{
						float magnitude = (nninfo.clampedPosition - position).magnitude;
						if (this.prioritizeGraphs && magnitude < this.prioritizeGraphsLimit)
						{
							result = nninfo;
							num2 = i;
							break;
						}
						if (magnitude < num)
						{
							num = magnitude;
							result = nninfo;
							num2 = i;
						}
					}
				}
			}
		}
		if (num2 == -1)
		{
			return result;
		}
		if (result.constrainedNode != null)
		{
			result.node = result.constrainedNode;
			result.clampedPosition = result.constClampedPosition;
		}
		if (!this.fullGetNearestSearch && result.node != null && !constraint.Suitable(result.node))
		{
			NNInfo nearestForce = this.graphs[num2].GetNearestForce(position, constraint);
			if (nearestForce.node != null)
			{
				result = nearestForce;
			}
		}
		if (!constraint.Suitable(result.node) || (constraint.constrainDistance && (result.clampedPosition - position).sqrMagnitude > this.maxNearestNodeDistanceSqr))
		{
			return default(NNInfo);
		}
		return result;
	}

	// Token: 0x06000127 RID: 295 RVA: 0x0000C99C File Offset: 0x0000AD9C
	public GraphNode GetNearest(Ray ray)
	{
		if (this.graphs == null)
		{
			return null;
		}
		float minDist = float.PositiveInfinity;
		GraphNode nearestNode = null;
		Vector3 lineDirection = ray.direction;
		Vector3 lineOrigin = ray.origin;
		for (int i = 0; i < this.graphs.Length; i++)
		{
			NavGraph navGraph = this.graphs[i];
			navGraph.GetNodes(delegate(GraphNode node)
			{
				Vector3 vector = (Vector3)node.position;
				Vector3 a = lineOrigin + Vector3.Dot(vector - lineOrigin, lineDirection) * lineDirection;
				float num = Mathf.Abs(a.x - vector.x);
				num *= num;
				if (num > minDist)
				{
					return true;
				}
				num = Mathf.Abs(a.z - vector.z);
				num *= num;
				if (num > minDist)
				{
					return true;
				}
				float sqrMagnitude = (a - vector).sqrMagnitude;
				if (sqrMagnitude < minDist)
				{
					minDist = sqrMagnitude;
					nearestNode = node;
				}
				return true;
			});
		}
		return nearestNode;
	}

	// Token: 0x0400008D RID: 141
	public static readonly AstarPath.AstarDistribution Distribution = AstarPath.AstarDistribution.WebsiteDownload;

	// Token: 0x0400008E RID: 142
	public static readonly string Branch = "master_Pro";

	// Token: 0x0400008F RID: 143
	public static readonly bool HasPro = true;

	// Token: 0x04000090 RID: 144
	public AstarData astarData;

	// Token: 0x04000091 RID: 145
	public static AstarPath active;

	// Token: 0x04000092 RID: 146
	public bool showNavGraphs = true;

	// Token: 0x04000093 RID: 147
	public bool showUnwalkableNodes = true;

	// Token: 0x04000094 RID: 148
	public GraphDebugMode debugMode;

	// Token: 0x04000095 RID: 149
	public float debugFloor;

	// Token: 0x04000096 RID: 150
	public float debugRoof = 20000f;

	// Token: 0x04000097 RID: 151
	public bool showSearchTree;

	// Token: 0x04000098 RID: 152
	public float unwalkableNodeDebugSize = 0.3f;

	// Token: 0x04000099 RID: 153
	public PathLog logPathResults = PathLog.Normal;

	// Token: 0x0400009A RID: 154
	public float maxNearestNodeDistance = 100f;

	// Token: 0x0400009B RID: 155
	public bool scanOnStartup = true;

	// Token: 0x0400009C RID: 156
	public bool fullGetNearestSearch;

	// Token: 0x0400009D RID: 157
	public bool prioritizeGraphs;

	// Token: 0x0400009E RID: 158
	public float prioritizeGraphsLimit = 1f;

	// Token: 0x0400009F RID: 159
	public AstarColor colorSettings;

	// Token: 0x040000A0 RID: 160
	[SerializeField]
	protected string[] tagNames;

	// Token: 0x040000A1 RID: 161
	public Heuristic heuristic = Heuristic.Euclidean;

	// Token: 0x040000A2 RID: 162
	public float heuristicScale = 1f;

	// Token: 0x040000A3 RID: 163
	public ThreadCount threadCount;

	// Token: 0x040000A4 RID: 164
	public float maxFrameTime = 1f;

	// Token: 0x040000A5 RID: 165
	public int minAreaSize;

	// Token: 0x040000A6 RID: 166
	public bool limitGraphUpdates = true;

	// Token: 0x040000A7 RID: 167
	public float maxGraphUpdateFreq = 0.2f;

	// Token: 0x040000A8 RID: 168
	public static int PathsCompleted = 0;

	// Token: 0x040000A9 RID: 169
	public static long TotalSearchedNodes = 0L;

	// Token: 0x040000AA RID: 170
	public static long TotalSearchTime = 0L;

	// Token: 0x040000AB RID: 171
	public float lastScanTime;

	// Token: 0x040000AC RID: 172
	public Path debugPath;

	// Token: 0x040000AD RID: 173
	public string inGameDebugPath;

	// Token: 0x040000AE RID: 174
	public bool isScanning;

	// Token: 0x040000AF RID: 175
	private bool graphUpdateRoutineRunning;

	// Token: 0x040000B0 RID: 176
	private bool isRegisteredForUpdate;

	// Token: 0x040000B1 RID: 177
	public static OnVoidDelegate OnAwakeSettings;

	// Token: 0x040000B2 RID: 178
	public static OnGraphDelegate OnGraphPreScan;

	// Token: 0x040000B3 RID: 179
	public static OnGraphDelegate OnGraphPostScan;

	// Token: 0x040000B4 RID: 180
	public static OnPathDelegate OnPathPreSearch;

	// Token: 0x040000B5 RID: 181
	public static OnPathDelegate OnPathPostSearch;

	// Token: 0x040000B6 RID: 182
	public static OnScanDelegate OnPreScan;

	// Token: 0x040000B7 RID: 183
	public static OnScanDelegate OnPostScan;

	// Token: 0x040000B8 RID: 184
	public static OnScanDelegate OnLatePostScan;

	// Token: 0x040000B9 RID: 185
	public static OnScanDelegate OnGraphsUpdated;

	// Token: 0x040000BA RID: 186
	public static OnVoidDelegate On65KOverflow;

	// Token: 0x040000BB RID: 187
	private static OnVoidDelegate OnThreadSafeCallback;

	// Token: 0x040000BC RID: 188
	public OnVoidDelegate OnDrawGizmosCallback;

	// Token: 0x040000BD RID: 189
	[Obsolete]
	public OnVoidDelegate OnGraphsWillBeUpdated;

	// Token: 0x040000BE RID: 190
	[Obsolete]
	public OnVoidDelegate OnGraphsWillBeUpdated2;

	// Token: 0x040000BF RID: 191
	[NonSerialized]
	public Queue<GraphUpdateObject> graphUpdateQueue;

	// Token: 0x040000C0 RID: 192
	[NonSerialized]
	public Stack<GraphNode> floodStack;

	// Token: 0x040000C1 RID: 193
	private ThreadControlQueue pathQueue = new ThreadControlQueue(0);

	// Token: 0x040000C2 RID: 194
	private static Thread[] threads;

	// Token: 0x040000C3 RID: 195
	private Thread graphUpdateThread;

	// Token: 0x040000C4 RID: 196
	private static PathThreadInfo[] threadInfos = new PathThreadInfo[0];

	// Token: 0x040000C5 RID: 197
	private static IEnumerator threadEnumerator;

	// Token: 0x040000C6 RID: 198
	private static LockFreeStack pathReturnStack = new LockFreeStack();

	// Token: 0x040000C7 RID: 199
	public EuclideanEmbedding euclideanEmbedding = new EuclideanEmbedding();

	// Token: 0x040000C8 RID: 200
	public bool showGraphs;

	// Token: 0x040000C9 RID: 201
	public static bool isEditor = true;

	// Token: 0x040000CA RID: 202
	public uint lastUniqueAreaIndex;

	// Token: 0x040000CB RID: 203
	private static readonly object safeUpdateLock = new object();

	// Token: 0x040000CC RID: 204
	private float lastGraphUpdate = -9999f;

	// Token: 0x040000CD RID: 205
	private ushort nextFreePathID = 1;

	// Token: 0x040000CE RID: 206
	private Queue<AstarPath.AstarWorkItem> workItems = new Queue<AstarPath.AstarWorkItem>();

	// Token: 0x040000CF RID: 207
	private bool workItemsQueued;

	// Token: 0x040000D0 RID: 208
	private bool queuedWorkItemFloodFill;

	// Token: 0x040000D1 RID: 209
	private bool processingWorkItems;

	// Token: 0x040000D2 RID: 210
	private AutoResetEvent graphUpdateAsyncEvent = new AutoResetEvent(false);

	// Token: 0x040000D3 RID: 211
	private ManualResetEvent processingGraphUpdatesAsync = new ManualResetEvent(true);

	// Token: 0x040000D4 RID: 212
	private Queue<AstarPath.GUOSingle> graphUpdateQueueAsync = new Queue<AstarPath.GUOSingle>();

	// Token: 0x040000D5 RID: 213
	private Queue<AstarPath.GUOSingle> graphUpdateQueueRegular = new Queue<AstarPath.GUOSingle>();

	// Token: 0x040000D6 RID: 214
	private int nextNodeIndex = 1;

	// Token: 0x040000D7 RID: 215
	private Stack<int> nodeIndexPool = new Stack<int>();

	// Token: 0x040000D8 RID: 216
	private static int waitForPathDepth = 0;

	// Token: 0x040000D9 RID: 217
	private Path pathReturnPop;

	// Token: 0x02000011 RID: 17
	public enum AstarDistribution
	{
		// Token: 0x040000DE RID: 222
		WebsiteDownload,
		// Token: 0x040000DF RID: 223
		AssetStore
	}

	// Token: 0x02000012 RID: 18
	public struct AstarWorkItem
	{
		// Token: 0x0600012C RID: 300 RVA: 0x0000CAA2 File Offset: 0x0000AEA2
		public AstarWorkItem(Func<bool, bool> update)
		{
			this.init = null;
			this.update = update;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000CAB2 File Offset: 0x0000AEB2
		public AstarWorkItem(OnVoidDelegate init, Func<bool, bool> update)
		{
			this.init = init;
			this.update = update;
		}

		// Token: 0x040000E0 RID: 224
		public OnVoidDelegate init;

		// Token: 0x040000E1 RID: 225
		public Func<bool, bool> update;
	}

	// Token: 0x02000013 RID: 19
	private enum GraphUpdateOrder
	{
		// Token: 0x040000E3 RID: 227
		GraphUpdate,
		// Token: 0x040000E4 RID: 228
		FloodFill
	}

	// Token: 0x02000014 RID: 20
	private struct GUOSingle
	{
		// Token: 0x040000E5 RID: 229
		public AstarPath.GraphUpdateOrder order;

		// Token: 0x040000E6 RID: 230
		public IUpdatableGraph graph;

		// Token: 0x040000E7 RID: 231
		public GraphUpdateObject obj;
	}
}
