﻿using System;
using System.Collections.Generic;
using System.Text;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000E2 RID: 226
	public class MultiTargetPath : ABPath
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x00048665 File Offset: 0x00046A65
		public MultiTargetPath()
		{
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00048689 File Offset: 0x00046A89
		[Obsolete("Please use the Construct method instead")]
		public MultiTargetPath(Vector3[] startPoints, Vector3 target, OnPathDelegate[] callbackDelegates, OnPathDelegate callbackDelegate = null) : this(target, startPoints, callbackDelegates, callbackDelegate)
		{
			this.inverted = true;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0004869D File Offset: 0x00046A9D
		[Obsolete("Please use the Construct method instead")]
		public MultiTargetPath(Vector3 start, Vector3[] targets, OnPathDelegate[] callbackDelegates, OnPathDelegate callbackDelegate = null)
		{
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000486C4 File Offset: 0x00046AC4
		public static MultiTargetPath Construct(Vector3[] startPoints, Vector3 target, OnPathDelegate[] callbackDelegates, OnPathDelegate callback = null)
		{
			MultiTargetPath multiTargetPath = MultiTargetPath.Construct(target, startPoints, callbackDelegates, callback);
			multiTargetPath.inverted = true;
			return multiTargetPath;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000486E4 File Offset: 0x00046AE4
		public static MultiTargetPath Construct(Vector3 start, Vector3[] targets, OnPathDelegate[] callbackDelegates, OnPathDelegate callback = null)
		{
			MultiTargetPath path = PathPool<MultiTargetPath>.GetPath();
			path.Setup(start, targets, callbackDelegates, callback);
			return path;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00048704 File Offset: 0x00046B04
		protected void Setup(Vector3 start, Vector3[] targets, OnPathDelegate[] callbackDelegates, OnPathDelegate callback)
		{
			this.inverted = false;
			this.callback = callback;
			this.callbacks = callbackDelegates;
			this.targetPoints = targets;
			this.originalStartPoint = start;
			this.startPoint = start;
			this.startIntPoint = (Int3)start;
			if (targets.Length == 0)
			{
				base.Error();
				base.LogError("No targets were assigned to the MultiTargetPath");
				return;
			}
			this.endPoint = targets[0];
			this.originalTargetPoints = new Vector3[this.targetPoints.Length];
			for (int i = 0; i < this.targetPoints.Length; i++)
			{
				this.originalTargetPoints[i] = this.targetPoints[i];
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000487C2 File Offset: 0x00046BC2
		protected override void Recycle()
		{
			PathPool<MultiTargetPath>.Recycle(this);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000487CC File Offset: 0x00046BCC
		public override void OnEnterPool()
		{
			if (this.vectorPaths != null)
			{
				for (int i = 0; i < this.vectorPaths.Length; i++)
				{
					if (this.vectorPaths[i] != null)
					{
						ListPool<Vector3>.Release(this.vectorPaths[i]);
					}
				}
			}
			this.vectorPaths = null;
			this.vectorPath = null;
			if (this.nodePaths != null)
			{
				for (int j = 0; j < this.nodePaths.Length; j++)
				{
					if (this.nodePaths[j] != null)
					{
						ListPool<GraphNode>.Release(this.nodePaths[j]);
					}
				}
			}
			this.nodePaths = null;
			this.path = null;
			base.OnEnterPool();
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00048878 File Offset: 0x00046C78
		public override void ReturnPath()
		{
			if (base.error)
			{
				if (this.callbacks != null)
				{
					for (int i = 0; i < this.callbacks.Length; i++)
					{
						if (this.callbacks[i] != null)
						{
							this.callbacks[i](this);
						}
					}
				}
				if (this.callback != null)
				{
					this.callback(this);
				}
				return;
			}
			bool flag = false;
			Vector3 originalStartPoint = this.originalStartPoint;
			Vector3 startPoint = this.startPoint;
			GraphNode startNode = this.startNode;
			for (int j = 0; j < this.nodePaths.Length; j++)
			{
				this.path = this.nodePaths[j];
				if (this.path != null)
				{
					base.CompleteState = PathCompleteState.Complete;
					flag = true;
				}
				else
				{
					base.CompleteState = PathCompleteState.Error;
				}
				if (this.callbacks != null && this.callbacks[j] != null)
				{
					this.vectorPath = this.vectorPaths[j];
					if (this.inverted)
					{
						this.endPoint = startPoint;
						this.endNode = startNode;
						this.startNode = this.targetNodes[j];
						this.startPoint = this.targetPoints[j];
						this.originalEndPoint = originalStartPoint;
						this.originalStartPoint = this.originalTargetPoints[j];
					}
					else
					{
						this.endPoint = this.targetPoints[j];
						this.originalEndPoint = this.originalTargetPoints[j];
						this.endNode = this.targetNodes[j];
					}
					this.callbacks[j](this);
					this.vectorPaths[j] = this.vectorPath;
				}
			}
			if (flag)
			{
				base.CompleteState = PathCompleteState.Complete;
				if (!this.pathsForAll)
				{
					this.path = this.nodePaths[this.chosenTarget];
					this.vectorPath = this.vectorPaths[this.chosenTarget];
					if (this.inverted)
					{
						this.endPoint = startPoint;
						this.endNode = startNode;
						this.startNode = this.targetNodes[this.chosenTarget];
						this.startPoint = this.targetPoints[this.chosenTarget];
						this.originalEndPoint = originalStartPoint;
						this.originalStartPoint = this.originalTargetPoints[this.chosenTarget];
					}
					else
					{
						this.endPoint = this.targetPoints[this.chosenTarget];
						this.originalEndPoint = this.originalTargetPoints[this.chosenTarget];
						this.endNode = this.targetNodes[this.chosenTarget];
					}
				}
			}
			else
			{
				base.CompleteState = PathCompleteState.Error;
			}
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00048B54 File Offset: 0x00046F54
		public void FoundTarget(PathNode nodeR, int i)
		{
			nodeR.flag1 = false;
			this.Trace(nodeR);
			this.vectorPaths[i] = this.vectorPath;
			this.nodePaths[i] = this.path;
			this.vectorPath = ListPool<Vector3>.Claim();
			this.path = ListPool<GraphNode>.Claim();
			this.targetsFound[i] = true;
			this.targetNodeCount--;
			if (!this.pathsForAll)
			{
				base.CompleteState = PathCompleteState.Complete;
				this.chosenTarget = i;
				this.targetNodeCount = 0;
				return;
			}
			if (this.targetNodeCount <= 0)
			{
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			if (this.heuristicMode == MultiTargetPath.HeuristicMode.MovingAverage)
			{
				Vector3 vector = Vector3.zero;
				int num = 0;
				for (int j = 0; j < this.targetPoints.Length; j++)
				{
					if (!this.targetsFound[j])
					{
						vector += (Vector3)this.targetNodes[j].position;
						num++;
					}
				}
				if (num > 0)
				{
					vector /= (float)num;
				}
				this.hTarget = (Int3)vector;
				this.RebuildOpenList();
			}
			else if (this.heuristicMode == MultiTargetPath.HeuristicMode.MovingMidpoint)
			{
				Vector3 vector2 = Vector3.zero;
				Vector3 vector3 = Vector3.zero;
				bool flag = false;
				for (int k = 0; k < this.targetPoints.Length; k++)
				{
					if (!this.targetsFound[k])
					{
						if (!flag)
						{
							vector2 = (Vector3)this.targetNodes[k].position;
							vector3 = (Vector3)this.targetNodes[k].position;
							flag = true;
						}
						else
						{
							vector2 = Vector3.Min((Vector3)this.targetNodes[k].position, vector2);
							vector3 = Vector3.Max((Vector3)this.targetNodes[k].position, vector3);
						}
					}
				}
				Int3 hTarget = (Int3)((vector2 + vector3) * 0.5f);
				this.hTarget = hTarget;
				this.RebuildOpenList();
			}
			else if (this.heuristicMode == MultiTargetPath.HeuristicMode.Sequential && this.sequentialTarget == i)
			{
				float num2 = 0f;
				for (int l = 0; l < this.targetPoints.Length; l++)
				{
					if (!this.targetsFound[l])
					{
						float sqrMagnitude = (this.targetNodes[l].position - this.startNode.position).sqrMagnitude;
						if (sqrMagnitude > num2)
						{
							num2 = sqrMagnitude;
							this.hTarget = (Int3)this.targetPoints[l];
							this.sequentialTarget = l;
						}
					}
				}
				this.RebuildOpenList();
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00048DF8 File Offset: 0x000471F8
		protected void RebuildOpenList()
		{
			BinaryHeapM heap = this.pathHandler.GetHeap();
			for (int i = 0; i < heap.numberOfItems; i++)
			{
				PathNode node = heap.GetNode(i);
				node.H = base.CalculateHScore(node.node);
				heap.SetF(i, node.F);
			}
			this.pathHandler.RebuildHeap();
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00048E5C File Offset: 0x0004725C
		public override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint, this.startHint);
			this.startNode = nearest.node;
			if (this.startNode == null)
			{
				base.LogError("Could not find start node for multi target path");
				base.Error();
				return;
			}
			if (!this.startNode.Walkable)
			{
				base.LogError("Nearest node to the start point is not walkable");
				base.Error();
				return;
			}
			PathNNConstraint pathNNConstraint = this.nnConstraint as PathNNConstraint;
			if (pathNNConstraint != null)
			{
				pathNNConstraint.SetStart(nearest.node);
			}
			this.vectorPaths = new List<Vector3>[this.targetPoints.Length];
			this.nodePaths = new List<GraphNode>[this.targetPoints.Length];
			this.targetNodes = new GraphNode[this.targetPoints.Length];
			this.targetsFound = new bool[this.targetPoints.Length];
			this.targetNodeCount = this.targetPoints.Length;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int i = 0; i < this.targetPoints.Length; i++)
			{
				NNInfo nearest2 = AstarPath.active.GetNearest(this.targetPoints[i], this.nnConstraint);
				this.targetNodes[i] = nearest2.node;
				this.targetPoints[i] = nearest2.clampedPosition;
				if (this.targetNodes[i] != null)
				{
					flag3 = true;
					this.endNode = this.targetNodes[i];
				}
				bool flag4 = false;
				if (nearest2.node != null && nearest2.node.Walkable)
				{
					flag = true;
				}
				else
				{
					flag4 = true;
				}
				if (nearest2.node != null && nearest2.node.Area == this.startNode.Area)
				{
					flag2 = true;
				}
				else
				{
					flag4 = true;
				}
				if (flag4)
				{
					this.targetsFound[i] = true;
					this.targetNodeCount--;
				}
			}
			this.startPoint = nearest.clampedPosition;
			this.startIntPoint = (Int3)this.startPoint;
			if (this.startNode == null || !flag3)
			{
				base.LogError(string.Concat(new string[]
				{
					"Couldn't find close nodes to either the start or the end (start = ",
					(this.startNode == null) ? "not found" : "found",
					" end = ",
					(!flag3) ? "none found" : "at least one found",
					")"
				}));
				base.Error();
				return;
			}
			if (!this.startNode.Walkable)
			{
				base.LogError("The node closest to the start point is not walkable");
				base.Error();
				return;
			}
			if (!flag)
			{
				base.LogError("No target nodes were walkable");
				base.Error();
				return;
			}
			if (!flag2)
			{
				base.LogError("There are no valid paths to the targets");
				base.Error();
				return;
			}
			if (this.pathsForAll)
			{
				if (this.heuristicMode == MultiTargetPath.HeuristicMode.None)
				{
					this.heuristic = Heuristic.None;
					this.heuristicScale = 0f;
				}
				else if (this.heuristicMode == MultiTargetPath.HeuristicMode.Average || this.heuristicMode == MultiTargetPath.HeuristicMode.MovingAverage)
				{
					Vector3 vector = Vector3.zero;
					for (int j = 0; j < this.targetNodes.Length; j++)
					{
						vector += (Vector3)this.targetNodes[j].position;
					}
					vector /= (float)this.targetNodes.Length;
					this.hTarget = (Int3)vector;
				}
				else if (this.heuristicMode == MultiTargetPath.HeuristicMode.Midpoint || this.heuristicMode == MultiTargetPath.HeuristicMode.MovingMidpoint)
				{
					Vector3 vector2 = Vector3.zero;
					Vector3 vector3 = Vector3.zero;
					bool flag5 = false;
					for (int k = 0; k < this.targetPoints.Length; k++)
					{
						if (!this.targetsFound[k])
						{
							if (!flag5)
							{
								vector2 = (Vector3)this.targetNodes[k].position;
								vector3 = (Vector3)this.targetNodes[k].position;
								flag5 = true;
							}
							else
							{
								vector2 = Vector3.Min((Vector3)this.targetNodes[k].position, vector2);
								vector3 = Vector3.Max((Vector3)this.targetNodes[k].position, vector3);
							}
						}
					}
					Vector3 ob = (vector2 + vector3) * 0.5f;
					this.hTarget = (Int3)ob;
				}
				else if (this.heuristicMode == MultiTargetPath.HeuristicMode.Sequential)
				{
					float num = 0f;
					for (int l = 0; l < this.targetNodes.Length; l++)
					{
						if (!this.targetsFound[l])
						{
							float sqrMagnitude = (this.targetNodes[l].position - this.startNode.position).sqrMagnitude;
							if (sqrMagnitude > num)
							{
								num = sqrMagnitude;
								this.hTarget = (Int3)this.targetPoints[l];
								this.sequentialTarget = l;
							}
						}
					}
				}
			}
			else
			{
				this.heuristic = Heuristic.None;
				this.heuristicScale = 0f;
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00049398 File Offset: 0x00047798
		public override void Initialize()
		{
			for (int i = 0; i < this.targetNodes.Length; i++)
			{
				if (this.startNode == this.targetNodes[i])
				{
					PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
					this.FoundTarget(pathNode, i);
				}
				else if (this.targetNodes[i] != null)
				{
					this.pathHandler.GetPathNode(this.targetNodes[i]).flag1 = true;
				}
			}
			AstarPath.OnPathPostSearch = (OnPathDelegate)Delegate.Combine(AstarPath.OnPathPostSearch, new OnPathDelegate(this.ResetFlags));
			if (this.targetNodeCount <= 0)
			{
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			PathNode pathNode2 = this.pathHandler.GetPathNode(this.startNode);
			pathNode2.node = this.startNode;
			pathNode2.pathID = this.pathID;
			pathNode2.parent = null;
			pathNode2.cost = 0u;
			pathNode2.G = base.GetTraversalCost(this.startNode);
			pathNode2.H = base.CalculateHScore(this.startNode);
			this.startNode.Open(this, pathNode2, this.pathHandler);
			this.searchedNodes++;
			if (this.pathHandler.HeapEmpty())
			{
				base.LogError("No open points, the start node didn't open any nodes");
				base.Error();
				return;
			}
			this.currentR = this.pathHandler.PopNode();
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000494FC File Offset: 0x000478FC
		public void ResetFlags(Path p)
		{
			AstarPath.OnPathPostSearch = (OnPathDelegate)Delegate.Remove(AstarPath.OnPathPostSearch, new OnPathDelegate(this.ResetFlags));
			if (p != this)
			{
				Debug.LogError("This should have been cleared after it was called on 'this' path. Was it not called? Or did the delegate reset not work?");
			}
			for (int i = 0; i < this.targetNodes.Length; i++)
			{
				if (this.targetNodes[i] != null)
				{
					this.pathHandler.GetPathNode(this.targetNodes[i]).flag1 = false;
				}
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0004957C File Offset: 0x0004797C
		public override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				if (this.currentR.flag1)
				{
					for (int i = 0; i < this.targetNodes.Length; i++)
					{
						if (!this.targetsFound[i] && this.currentR.node == this.targetNodes[i])
						{
							this.FoundTarget(this.currentR, i);
							if (base.CompleteState != PathCompleteState.NotCalculated)
							{
								break;
							}
						}
					}
					if (this.targetNodeCount <= 0)
					{
						base.CompleteState = PathCompleteState.Complete;
						break;
					}
				}
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.pathHandler.HeapEmpty())
				{
					base.CompleteState = PathCompleteState.Complete;
					break;
				}
				this.currentR = this.pathHandler.PopNode();
				if (num > 500)
				{
					if (DateTime.UtcNow.Ticks >= targetTick)
					{
						return;
					}
					num = 0;
				}
				num++;
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0004969C File Offset: 0x00047A9C
		protected override void Trace(PathNode node)
		{
			base.Trace(node);
			if (this.inverted)
			{
				int num = this.path.Count / 2;
				for (int i = 0; i < num; i++)
				{
					GraphNode value = this.path[i];
					this.path[i] = this.path[this.path.Count - i - 1];
					this.path[this.path.Count - i - 1] = value;
				}
				for (int j = 0; j < num; j++)
				{
					Vector3 value2 = this.vectorPath[j];
					this.vectorPath[j] = this.vectorPath[this.vectorPath.Count - j - 1];
					this.vectorPath[this.vectorPath.Count - j - 1] = value2;
				}
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0004978C File Offset: 0x00047B8C
		public override string DebugString(PathLog logMode)
		{
			if (logMode == PathLog.None || (!base.error && logMode == PathLog.OnlyErrors))
			{
				return string.Empty;
			}
			StringBuilder debugStringBuilder = this.pathHandler.DebugStringBuilder;
			debugStringBuilder.Length = 0;
			debugStringBuilder.Append((!base.error) ? "Path Completed : " : "Path Failed : ");
			debugStringBuilder.Append("Computation Time ");
			debugStringBuilder.Append(this.duration.ToString((logMode != PathLog.Heavy) ? "0.00" : "0.000"));
			debugStringBuilder.Append(" ms Searched Nodes ");
			debugStringBuilder.Append(this.searchedNodes);
			if (!base.error)
			{
				debugStringBuilder.Append("\nLast Found Path Length ");
				debugStringBuilder.Append((this.path != null) ? this.path.Count.ToString() : "Null");
				if (logMode == PathLog.Heavy)
				{
					debugStringBuilder.Append("\nSearch Iterations " + this.searchIterations);
					debugStringBuilder.Append("\nPaths (").Append(this.targetsFound.Length).Append("):");
					for (int i = 0; i < this.targetsFound.Length; i++)
					{
						debugStringBuilder.Append("\n\n\tPath " + i).Append(" Found: ").Append(this.targetsFound[i]);
						GraphNode graphNode = (this.nodePaths[i] != null) ? this.nodePaths[i][this.nodePaths[i].Count - 1] : null;
						if (this.nodePaths[i] != null)
						{
							debugStringBuilder.Append("\n\t\tLength: ");
							debugStringBuilder.Append(this.nodePaths[i].Count);
							if (graphNode != null)
							{
								PathNode pathNode = this.pathHandler.GetPathNode(this.endNode);
								if (pathNode != null)
								{
									debugStringBuilder.Append("\n\t\tEnd Node");
									debugStringBuilder.Append("\n\t\t\tG: ");
									debugStringBuilder.Append(pathNode.G);
									debugStringBuilder.Append("\n\t\t\tH: ");
									debugStringBuilder.Append(pathNode.H);
									debugStringBuilder.Append("\n\t\t\tF: ");
									debugStringBuilder.Append(pathNode.F);
									debugStringBuilder.Append("\n\t\t\tPoint: ");
									StringBuilder stringBuilder = debugStringBuilder;
									Vector3 endPoint = this.endPoint;
									stringBuilder.Append(endPoint.ToString());
									debugStringBuilder.Append("\n\t\t\tGraph: ");
									debugStringBuilder.Append(this.endNode.GraphIndex);
								}
								else
								{
									debugStringBuilder.Append("\n\t\tEnd Node: Null");
								}
							}
						}
					}
					debugStringBuilder.Append("\nStart Node");
					debugStringBuilder.Append("\n\tPoint: ");
					StringBuilder stringBuilder2 = debugStringBuilder;
					Vector3 endPoint2 = this.endPoint;
					stringBuilder2.Append(endPoint2.ToString());
					debugStringBuilder.Append("\n\tGraph: ");
					debugStringBuilder.Append(this.startNode.GraphIndex);
					debugStringBuilder.Append("\nBinary Heap size at completion: ");
					debugStringBuilder.AppendLine((this.pathHandler.GetHeap() != null) ? (this.pathHandler.GetHeap().numberOfItems - 2).ToString() : "Null");
				}
			}
			if (base.error)
			{
				debugStringBuilder.Append("\nError: ");
				debugStringBuilder.Append(base.errorLog);
				debugStringBuilder.AppendLine();
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

		// Token: 0x04000635 RID: 1589
		public OnPathDelegate[] callbacks;

		// Token: 0x04000636 RID: 1590
		public GraphNode[] targetNodes;

		// Token: 0x04000637 RID: 1591
		protected int targetNodeCount;

		// Token: 0x04000638 RID: 1592
		public bool[] targetsFound;

		// Token: 0x04000639 RID: 1593
		public Vector3[] targetPoints;

		// Token: 0x0400063A RID: 1594
		public Vector3[] originalTargetPoints;

		// Token: 0x0400063B RID: 1595
		public List<Vector3>[] vectorPaths;

		// Token: 0x0400063C RID: 1596
		public List<GraphNode>[] nodePaths;

		// Token: 0x0400063D RID: 1597
		public int endsFound;

		// Token: 0x0400063E RID: 1598
		public bool pathsForAll = true;

		// Token: 0x0400063F RID: 1599
		public int chosenTarget = -1;

		// Token: 0x04000640 RID: 1600
		public int sequentialTarget;

		// Token: 0x04000641 RID: 1601
		public MultiTargetPath.HeuristicMode heuristicMode = MultiTargetPath.HeuristicMode.Sequential;

		// Token: 0x04000642 RID: 1602
		public bool inverted = true;

		// Token: 0x020000E3 RID: 227
		public enum HeuristicMode
		{
			// Token: 0x04000644 RID: 1604
			None,
			// Token: 0x04000645 RID: 1605
			Average,
			// Token: 0x04000646 RID: 1606
			MovingAverage,
			// Token: 0x04000647 RID: 1607
			Midpoint,
			// Token: 0x04000648 RID: 1608
			MovingMidpoint,
			// Token: 0x04000649 RID: 1609
			Sequential
		}
	}
}
