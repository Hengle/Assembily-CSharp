using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000035 RID: 53
	public abstract class Path
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00013ADC File Offset: 0x00011EDC
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00013AE4 File Offset: 0x00011EE4
		public PathCompleteState CompleteState
		{
			get
			{
				return this.pathCompleteState;
			}
			protected set
			{
				this.pathCompleteState = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00013AED File Offset: 0x00011EED
		public bool error
		{
			get
			{
				return this.CompleteState == PathCompleteState.Error;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00013AF8 File Offset: 0x00011EF8
		public string errorLog
		{
			get
			{
				return this._errorLog;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00013B00 File Offset: 0x00011F00
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00013B08 File Offset: 0x00011F08
		public int[] tagPenalties
		{
			get
			{
				return this.manualTagPenalties;
			}
			set
			{
				if (value == null || value.Length != 32)
				{
					this.manualTagPenalties = null;
					this.internalTagPenalties = Path.ZeroTagPenalties;
				}
				else
				{
					this.manualTagPenalties = value;
					this.internalTagPenalties = value;
				}
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00013B3F File Offset: 0x00011F3F
		public virtual bool FloodingPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00013B44 File Offset: 0x00011F44
		public float GetTotalLength()
		{
			if (this.vectorPath == null)
			{
				return float.PositiveInfinity;
			}
			float num = 0f;
			for (int i = 0; i < this.vectorPath.Count - 1; i++)
			{
				num += Vector3.Distance(this.vectorPath[i], this.vectorPath[i + 1]);
			}
			return num;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00013BAC File Offset: 0x00011FAC
		public IEnumerator WaitForPath()
		{
			if (this.GetState() == PathState.Created)
			{
				throw new InvalidOperationException("This path has not been started yet");
			}
			while (this.GetState() != PathState.Returned)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00013BC8 File Offset: 0x00011FC8
		public uint CalculateHScore(GraphNode node)
		{
			Heuristic heuristic = this.heuristic;
			uint val;
			uint val2;
			if (heuristic == Heuristic.Euclidean)
			{
				val = (uint)((float)(this.GetHTarget() - node.position).costMagnitude * this.heuristicScale);
				val2 = ((this.hTargetNode == null) ? 0u : AstarPath.active.euclideanEmbedding.GetHeuristic(node.NodeIndex, this.hTargetNode.NodeIndex));
				return Math.Max(val, val2);
			}
			if (heuristic == Heuristic.Manhattan)
			{
				Int3 position = node.position;
				val = (uint)((float)(Math.Abs(this.hTarget.x - position.x) + Math.Abs(this.hTarget.y - position.y) + Math.Abs(this.hTarget.z - position.z)) * this.heuristicScale);
				val2 = ((this.hTargetNode == null) ? 0u : AstarPath.active.euclideanEmbedding.GetHeuristic(node.NodeIndex, this.hTargetNode.NodeIndex));
				return Math.Max(val, val2);
			}
			if (heuristic != Heuristic.DiagonalManhattan)
			{
				return 0u;
			}
			Int3 @int = this.GetHTarget() - node.position;
			@int.x = Math.Abs(@int.x);
			@int.y = Math.Abs(@int.y);
			@int.z = Math.Abs(@int.z);
			int num = Math.Min(@int.x, @int.z);
			int num2 = Math.Max(@int.x, @int.z);
			val = (uint)((float)(14 * num / 10 + (num2 - num) + @int.y) * this.heuristicScale);
			val2 = ((this.hTargetNode == null) ? 0u : AstarPath.active.euclideanEmbedding.GetHeuristic(node.NodeIndex, this.hTargetNode.NodeIndex));
			return Math.Max(val, val2);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00013DB9 File Offset: 0x000121B9
		public uint GetTagPenalty(int tag)
		{
			return (uint)this.internalTagPenalties[tag];
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00013DC3 File Offset: 0x000121C3
		public Int3 GetHTarget()
		{
			return this.hTarget;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00013DCB File Offset: 0x000121CB
		public bool CanTraverse(GraphNode node)
		{
			return node.Walkable && (this.enabledTags >> (int)node.Tag & 1) != 0;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00013DF3 File Offset: 0x000121F3
		public uint GetTraversalCost(GraphNode node)
		{
			return this.GetTagPenalty((int)node.Tag) + node.Penalty;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00013E08 File Offset: 0x00012208
		public virtual uint GetConnectionSpecialCost(GraphNode a, GraphNode b, uint currentCost)
		{
			return currentCost;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00013E0B File Offset: 0x0001220B
		public bool IsDone()
		{
			return this.CompleteState != PathCompleteState.NotCalculated;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00013E1C File Offset: 0x0001221C
		public void AdvanceState(PathState s)
		{
			object obj = this.stateLock;
			lock (obj)
			{
				this.state = (PathState)Math.Max((int)this.state, (int)s);
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00013E64 File Offset: 0x00012264
		public PathState GetState()
		{
			return this.state;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00013E6C File Offset: 0x0001226C
		public void LogError(string msg)
		{
			if (AstarPath.isEditor || AstarPath.active.logPathResults != PathLog.None)
			{
				this._errorLog += msg;
			}
			if (AstarPath.active.logPathResults != PathLog.None && AstarPath.active.logPathResults != PathLog.InGame)
			{
				Debug.LogWarning(msg);
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00013EC9 File Offset: 0x000122C9
		public void ForceLogError(string msg)
		{
			this.Error();
			this._errorLog += msg;
			Debug.LogError(msg);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00013EE9 File Offset: 0x000122E9
		public void Log(string msg)
		{
			if (AstarPath.isEditor || AstarPath.active.logPathResults != PathLog.None)
			{
				this._errorLog += msg;
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00013F16 File Offset: 0x00012316
		public void Error()
		{
			this.CompleteState = PathCompleteState.Error;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00013F20 File Offset: 0x00012320
		private void ErrorCheck()
		{
			if (!this.hasBeenReset)
			{
				throw new Exception("The path has never been reset. Use pooling API or call Reset() after creating the path with the default constructor.");
			}
			if (this.recycled)
			{
				throw new Exception("The path is currently in a path pool. Are you sending the path for calculation twice?");
			}
			if (this.pathHandler == null)
			{
				throw new Exception("Field pathHandler is not set. Please report this bug.");
			}
			if (this.GetState() > PathState.Processing)
			{
				throw new Exception("This path has already been processed. Do not request a path with the same path object twice.");
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00013F86 File Offset: 0x00012386
		public virtual void OnEnterPool()
		{
			if (this.vectorPath != null)
			{
				ListPool<Vector3>.Release(this.vectorPath);
			}
			if (this.path != null)
			{
				ListPool<GraphNode>.Release(this.path);
			}
			this.vectorPath = null;
			this.path = null;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00013FC4 File Offset: 0x000123C4
		public virtual void Reset()
		{
			if (object.ReferenceEquals(AstarPath.active, null))
			{
				throw new NullReferenceException("No AstarPath object found in the scene. Make sure there is one or do not create paths in Awake");
			}
			this.hasBeenReset = true;
			this.state = PathState.Created;
			this.releasedNotSilent = false;
			this.pathHandler = null;
			this.callback = null;
			this._errorLog = string.Empty;
			this.pathCompleteState = PathCompleteState.NotCalculated;
			this.path = ListPool<GraphNode>.Claim();
			this.vectorPath = ListPool<Vector3>.Claim();
			this.currentR = null;
			this.duration = 0f;
			this.searchIterations = 0;
			this.searchedNodes = 0;
			this.nnConstraint = PathNNConstraint.Default;
			this.next = null;
			this.radius = 0;
			this.walkabilityMask = -1;
			this.height = 0;
			this.turnRadius = 0;
			this.speed = 0;
			this.heuristic = AstarPath.active.heuristic;
			this.heuristicScale = AstarPath.active.heuristicScale;
			this.pathID = 0;
			this.enabledTags = -1;
			this.tagPenalties = null;
			this.callTime = DateTime.UtcNow;
			this.pathID = AstarPath.active.GetNextPathID();
			this.hTarget = Int3.zero;
			this.hTargetNode = null;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000140F0 File Offset: 0x000124F0
		protected bool HasExceededTime(int searchedNodes, long targetTime)
		{
			return DateTime.UtcNow.Ticks >= targetTime;
		}

		// Token: 0x0600025E RID: 606
		protected abstract void Recycle();

		// Token: 0x0600025F RID: 607 RVA: 0x00014110 File Offset: 0x00012510
		public void Claim(object o)
		{
			if (object.ReferenceEquals(o, null))
			{
				throw new ArgumentNullException("o");
			}
			for (int i = 0; i < this.claimed.Count; i++)
			{
				if (object.ReferenceEquals(this.claimed[i], o))
				{
					throw new ArgumentException("You have already claimed the path with that object (" + o.ToString() + "). Are you claiming the path with the same object twice?");
				}
			}
			this.claimed.Add(o);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00014190 File Offset: 0x00012590
		public void ReleaseSilent(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			for (int i = 0; i < this.claimed.Count; i++)
			{
				if (object.ReferenceEquals(this.claimed[i], o))
				{
					this.claimed.RemoveAt(i);
					if (this.releasedNotSilent && this.claimed.Count == 0)
					{
						this.Recycle();
					}
					return;
				}
			}
			if (this.claimed.Count == 0)
			{
				throw new ArgumentException("You are releasing a path which is not claimed at all (most likely it has been pooled already). Are you releasing the path with the same object (" + o.ToString() + ") twice?");
			}
			throw new ArgumentException("You are releasing a path which has not been claimed with this object (" + o.ToString() + "). Are you releasing the path with the same object twice?");
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00014254 File Offset: 0x00012654
		public void Release(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			for (int i = 0; i < this.claimed.Count; i++)
			{
				if (object.ReferenceEquals(this.claimed[i], o))
				{
					this.claimed.RemoveAt(i);
					this.releasedNotSilent = true;
					if (this.claimed.Count == 0)
					{
						this.Recycle();
					}
					return;
				}
			}
			if (this.claimed.Count == 0)
			{
				throw new ArgumentException("You are releasing a path which is not claimed at all (most likely it has been pooled already). Are you releasing the path with the same object (" + o.ToString() + ") twice?");
			}
			throw new ArgumentException("You are releasing a path which has not been claimed with this object (" + o.ToString() + "). Are you releasing the path with the same object twice?");
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00014314 File Offset: 0x00012714
		protected virtual void Trace(PathNode from)
		{
			int num = 0;
			PathNode pathNode = from;
			while (pathNode != null)
			{
				pathNode = pathNode.parent;
				num++;
				if (num > 1024)
				{
					Debug.LogWarning("Inifinity loop? >1024 node path. Remove this message if you really have that long paths (Path.cs, Trace function)");
					break;
				}
			}
			if (this.path.Capacity < num)
			{
				this.path.Capacity = num;
			}
			if (this.vectorPath.Capacity < num)
			{
				this.vectorPath.Capacity = num;
			}
			pathNode = from;
			for (int i = 0; i < num; i++)
			{
				this.path.Add(pathNode.node);
				pathNode = pathNode.parent;
			}
			int num2 = num / 2;
			for (int j = 0; j < num2; j++)
			{
				GraphNode value = this.path[j];
				this.path[j] = this.path[num - j - 1];
				this.path[num - j - 1] = value;
			}
			for (int k = 0; k < num; k++)
			{
				this.vectorPath.Add((Vector3)this.path[k].position);
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00014450 File Offset: 0x00012850
		public virtual string DebugString(PathLog logMode)
		{
			if (logMode == PathLog.None || (!this.error && logMode == PathLog.OnlyErrors))
			{
				return string.Empty;
			}
			StringBuilder debugStringBuilder = this.pathHandler.DebugStringBuilder;
			debugStringBuilder.Length = 0;
			debugStringBuilder.Append((!this.error) ? "Path Completed : " : "Path Failed : ");
			debugStringBuilder.Append("Computation Time ");
			debugStringBuilder.Append(this.duration.ToString((logMode != PathLog.Heavy) ? "0.00 ms " : "0.000 ms "));
			debugStringBuilder.Append("Searched Nodes ");
			debugStringBuilder.Append(this.searchedNodes);
			if (!this.error)
			{
				debugStringBuilder.Append(" Path Length ");
				debugStringBuilder.Append((this.path != null) ? this.path.Count.ToString() : "Null");
				if (logMode == PathLog.Heavy)
				{
					debugStringBuilder.Append("\nSearch Iterations " + this.searchIterations);
				}
			}
			if (this.error)
			{
				debugStringBuilder.Append("\nError: ");
				debugStringBuilder.Append(this.errorLog);
			}
			if (logMode == PathLog.Heavy && !AstarPath.IsUsingMultithreading)
			{
				debugStringBuilder.Append("\nCallback references ");
				if (this.callback != null)
				{
					debugStringBuilder.Append(this.callback.Target.GetType().FullName).AppendLine();
				}
				else
				{
					debugStringBuilder.AppendLine("NULL");
				}
			}
			debugStringBuilder.Append("\nPath Number ");
			debugStringBuilder.Append(this.pathID);
			return debugStringBuilder.ToString();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00014608 File Offset: 0x00012A08
		public virtual void ReturnPath()
		{
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00014624 File Offset: 0x00012A24
		public void PrepareBase(PathHandler pathHandler)
		{
			if (pathHandler.PathID > this.pathID)
			{
				pathHandler.ClearPathIDs();
			}
			this.pathHandler = pathHandler;
			pathHandler.InitializeForPath(this);
			if (this.internalTagPenalties == null || this.internalTagPenalties.Length != 32)
			{
				this.internalTagPenalties = Path.ZeroTagPenalties;
			}
			try
			{
				this.ErrorCheck();
			}
			catch (Exception ex)
			{
				this.ForceLogError(string.Concat(new object[]
				{
					"Exception in path ",
					this.pathID,
					"\n",
					ex.ToString()
				}));
			}
		}

		// Token: 0x06000266 RID: 614
		public abstract void Prepare();

		// Token: 0x06000267 RID: 615 RVA: 0x000146D4 File Offset: 0x00012AD4
		public virtual void Cleanup()
		{
		}

		// Token: 0x06000268 RID: 616
		public abstract void Initialize();

		// Token: 0x06000269 RID: 617
		public abstract void CalculateStep(long targetTick);

		// Token: 0x04000191 RID: 401
		public PathHandler pathHandler;

		// Token: 0x04000192 RID: 402
		public OnPathDelegate callback;

		// Token: 0x04000193 RID: 403
		public OnPathDelegate immediateCallback;

		// Token: 0x04000194 RID: 404
		private PathState state;

		// Token: 0x04000195 RID: 405
		private object stateLock = new object();

		// Token: 0x04000196 RID: 406
		private PathCompleteState pathCompleteState;

		// Token: 0x04000197 RID: 407
		private string _errorLog = string.Empty;

		// Token: 0x04000198 RID: 408
		private GraphNode[] _path;

		// Token: 0x04000199 RID: 409
		private Vector3[] _vectorPath;

		// Token: 0x0400019A RID: 410
		public List<GraphNode> path;

		// Token: 0x0400019B RID: 411
		public List<Vector3> vectorPath;

		// Token: 0x0400019C RID: 412
		protected float maxFrameTime;

		// Token: 0x0400019D RID: 413
		protected PathNode currentR;

		// Token: 0x0400019E RID: 414
		public float duration;

		// Token: 0x0400019F RID: 415
		public int searchIterations;

		// Token: 0x040001A0 RID: 416
		public int searchedNodes;

		// Token: 0x040001A1 RID: 417
		public DateTime callTime;

		// Token: 0x040001A2 RID: 418
		public bool recycled;

		// Token: 0x040001A3 RID: 419
		protected bool hasBeenReset;

		// Token: 0x040001A4 RID: 420
		public NNConstraint nnConstraint = PathNNConstraint.Default;

		// Token: 0x040001A5 RID: 421
		public Path next;

		// Token: 0x040001A6 RID: 422
		public int radius;

		// Token: 0x040001A7 RID: 423
		public int walkabilityMask = -1;

		// Token: 0x040001A8 RID: 424
		public int height;

		// Token: 0x040001A9 RID: 425
		public int turnRadius;

		// Token: 0x040001AA RID: 426
		public int speed;

		// Token: 0x040001AB RID: 427
		public Heuristic heuristic;

		// Token: 0x040001AC RID: 428
		public float heuristicScale = 1f;

		// Token: 0x040001AD RID: 429
		public ushort pathID;

		// Token: 0x040001AE RID: 430
		protected GraphNode hTargetNode;

		// Token: 0x040001AF RID: 431
		protected Int3 hTarget;

		// Token: 0x040001B0 RID: 432
		public int enabledTags = -1;

		// Token: 0x040001B1 RID: 433
		private static readonly int[] ZeroTagPenalties = new int[32];

		// Token: 0x040001B2 RID: 434
		protected int[] internalTagPenalties;

		// Token: 0x040001B3 RID: 435
		protected int[] manualTagPenalties;

		// Token: 0x040001B4 RID: 436
		private List<object> claimed = new List<object>();

		// Token: 0x040001B5 RID: 437
		private bool releasedNotSilent;
	}
}
