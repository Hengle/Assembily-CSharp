using System;
using System.Text;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000DB RID: 219
	public class ABPath : Path
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x00046AD1 File Offset: 0x00044ED1
		[Obsolete("Use PathPool<T>.GetPath instead")]
		public ABPath(Vector3 start, Vector3 end, OnPathDelegate callbackDelegate)
		{
			this.Reset();
			this.Setup(start, end, callbackDelegate);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00046AF6 File Offset: 0x00044EF6
		public ABPath()
		{
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00046B0C File Offset: 0x00044F0C
		public static ABPath Construct(Vector3 start, Vector3 end, OnPathDelegate callback = null)
		{
			ABPath path = PathPool<ABPath>.GetPath();
			path.Setup(start, end, callback);
			return path;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00046B29 File Offset: 0x00044F29
		protected void Setup(Vector3 start, Vector3 end, OnPathDelegate callbackDelegate)
		{
			this.callback = callbackDelegate;
			this.UpdateStartEnd(start, end);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00046B3A File Offset: 0x00044F3A
		protected void UpdateStartEnd(Vector3 start, Vector3 end)
		{
			this.originalStartPoint = start;
			this.originalEndPoint = end;
			this.startPoint = start;
			this.endPoint = end;
			this.startIntPoint = (Int3)start;
			this.hTarget = (Int3)end;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00046B70 File Offset: 0x00044F70
		public override uint GetConnectionSpecialCost(GraphNode a, GraphNode b, uint currentCost)
		{
			if (this.startNode != null && this.endNode != null)
			{
				if (a == this.startNode)
				{
					return (uint)((double)(this.startIntPoint - ((b != this.endNode) ? b.position : this.hTarget)).costMagnitude * (currentCost * 1.0 / (double)(a.position - b.position).costMagnitude));
				}
				if (b == this.startNode)
				{
					return (uint)((double)(this.startIntPoint - ((a != this.endNode) ? a.position : this.hTarget)).costMagnitude * (currentCost * 1.0 / (double)(a.position - b.position).costMagnitude));
				}
				if (a == this.endNode)
				{
					return (uint)((double)(this.hTarget - b.position).costMagnitude * (currentCost * 1.0 / (double)(a.position - b.position).costMagnitude));
				}
				if (b == this.endNode)
				{
					return (uint)((double)(this.hTarget - a.position).costMagnitude * (currentCost * 1.0 / (double)(a.position - b.position).costMagnitude));
				}
			}
			else
			{
				if (a == this.startNode)
				{
					return (uint)((double)(this.startIntPoint - b.position).costMagnitude * (currentCost * 1.0 / (double)(a.position - b.position).costMagnitude));
				}
				if (b == this.startNode)
				{
					return (uint)((double)(this.startIntPoint - a.position).costMagnitude * (currentCost * 1.0 / (double)(a.position - b.position).costMagnitude));
				}
			}
			return currentCost;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00046DB8 File Offset: 0x000451B8
		public override void Reset()
		{
			base.Reset();
			this.startNode = null;
			this.endNode = null;
			this.startHint = null;
			this.endHint = null;
			this.originalStartPoint = Vector3.zero;
			this.originalEndPoint = Vector3.zero;
			this.startPoint = Vector3.zero;
			this.endPoint = Vector3.zero;
			this.calculatePartial = false;
			this.partialBestTarget = null;
			this.hasEndPoint = true;
			this.startIntPoint = default(Int3);
			this.hTarget = default(Int3);
			this.endNodeCosts = null;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00046E50 File Offset: 0x00045250
		public override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint, this.startHint);
			PathNNConstraint pathNNConstraint = this.nnConstraint as PathNNConstraint;
			if (pathNNConstraint != null)
			{
				pathNNConstraint.SetStart(nearest.node);
			}
			this.startPoint = nearest.clampedPosition;
			this.startIntPoint = (Int3)this.startPoint;
			this.startNode = nearest.node;
			if (this.hasEndPoint)
			{
				NNInfo nearest2 = AstarPath.active.GetNearest(this.endPoint, this.nnConstraint, this.endHint);
				this.endPoint = nearest2.clampedPosition;
				this.hTarget = (Int3)this.endPoint;
				this.endNode = nearest2.node;
				this.hTargetNode = this.endNode;
			}
			if (this.startNode == null && this.hasEndPoint && this.endNode == null)
			{
				base.Error();
				base.LogError("Couldn't find close nodes to the start point or the end point");
				return;
			}
			if (this.startNode == null)
			{
				base.Error();
				base.LogError("Couldn't find a close node to the start point");
				return;
			}
			if (this.endNode == null && this.hasEndPoint)
			{
				base.Error();
				base.LogError("Couldn't find a close node to the end point");
				return;
			}
			if (!this.startNode.Walkable)
			{
				base.Error();
				base.LogError("The node closest to the start point is not walkable");
				return;
			}
			if (this.hasEndPoint && !this.endNode.Walkable)
			{
				base.Error();
				base.LogError("The node closest to the end point is not walkable");
				return;
			}
			if (this.hasEndPoint && this.startNode.Area != this.endNode.Area)
			{
				base.Error();
				base.LogError(string.Concat(new object[]
				{
					"There is no valid path to the target (start area: ",
					this.startNode.Area,
					", target area: ",
					this.endNode.Area,
					")"
				}));
				return;
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00047078 File Offset: 0x00045478
		public override void Initialize()
		{
			if (this.startNode != null)
			{
				this.pathHandler.GetPathNode(this.startNode).flag2 = true;
			}
			if (this.endNode != null)
			{
				this.pathHandler.GetPathNode(this.endNode).flag2 = true;
			}
			if (this.hasEndPoint && this.startNode == this.endNode)
			{
				PathNode pathNode = this.pathHandler.GetPathNode(this.endNode);
				pathNode.node = this.endNode;
				pathNode.parent = null;
				pathNode.H = 0u;
				pathNode.G = 0u;
				this.Trace(pathNode);
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			PathNode pathNode2 = this.pathHandler.GetPathNode(this.startNode);
			pathNode2.node = this.startNode;
			pathNode2.pathID = this.pathHandler.PathID;
			pathNode2.parent = null;
			pathNode2.cost = 0u;
			pathNode2.G = base.GetTraversalCost(this.startNode);
			pathNode2.H = base.CalculateHScore(this.startNode);
			this.startNode.Open(this, pathNode2, this.pathHandler);
			this.searchedNodes++;
			this.partialBestTarget = pathNode2;
			if (this.pathHandler.HeapEmpty())
			{
				if (!this.calculatePartial)
				{
					base.Error();
					base.LogError("No open points, the start node didn't open any nodes");
					return;
				}
				base.CompleteState = PathCompleteState.Partial;
				this.Trace(this.partialBestTarget);
			}
			this.currentR = this.pathHandler.PopNode();
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00047208 File Offset: 0x00045608
		public override void Cleanup()
		{
			if (this.startNode != null)
			{
				this.pathHandler.GetPathNode(this.startNode).flag2 = false;
			}
			if (this.endNode != null)
			{
				this.pathHandler.GetPathNode(this.endNode).flag2 = false;
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0004725C File Offset: 0x0004565C
		public override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				if (this.currentR.node == this.endNode)
				{
					base.CompleteState = PathCompleteState.Complete;
					break;
				}
				if (this.currentR.H < this.partialBestTarget.H)
				{
					this.partialBestTarget = this.currentR;
				}
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.pathHandler.HeapEmpty())
				{
					base.Error();
					base.LogError("No open points, whole area searched");
					return;
				}
				this.currentR = this.pathHandler.PopNode();
				if (num > 500)
				{
					if (DateTime.UtcNow.Ticks >= targetTick)
					{
						return;
					}
					num = 0;
					if (this.searchedNodes > 1000000)
					{
						throw new Exception("Probable infinite loop. Over 1,000,000 nodes searched");
					}
				}
				num++;
			}
			if (base.CompleteState == PathCompleteState.Complete)
			{
				this.Trace(this.currentR);
			}
			else if (this.calculatePartial && this.partialBestTarget != null)
			{
				base.CompleteState = PathCompleteState.Partial;
				this.Trace(this.partialBestTarget);
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000473A8 File Offset: 0x000457A8
		public void ResetCosts(Path p)
		{
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000473AC File Offset: 0x000457AC
		public override string DebugString(PathLog logMode)
		{
			if (logMode == PathLog.None || (!base.error && logMode == PathLog.OnlyErrors))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append((!base.error) ? "Path Completed : " : "Path Failed : ");
			stringBuilder.Append("Computation Time ");
			stringBuilder.Append(this.duration.ToString((logMode != PathLog.Heavy) ? "0.00" : "0.000"));
			stringBuilder.Append(" ms Searched Nodes ");
			stringBuilder.Append(this.searchedNodes);
			if (!base.error)
			{
				stringBuilder.Append(" Path Length ");
				stringBuilder.Append((this.path != null) ? this.path.Count.ToString() : "Null");
				if (logMode == PathLog.Heavy)
				{
					stringBuilder.Append("\nSearch Iterations " + this.searchIterations);
					if (this.hasEndPoint && this.endNode != null)
					{
						PathNode pathNode = this.pathHandler.GetPathNode(this.endNode);
						stringBuilder.Append("\nEnd Node\n\tG: ");
						stringBuilder.Append(pathNode.G);
						stringBuilder.Append("\n\tH: ");
						stringBuilder.Append(pathNode.H);
						stringBuilder.Append("\n\tF: ");
						stringBuilder.Append(pathNode.F);
						stringBuilder.Append("\n\tPoint: ");
						StringBuilder stringBuilder2 = stringBuilder;
						Vector3 vector = this.endPoint;
						stringBuilder2.Append(vector.ToString());
						stringBuilder.Append("\n\tGraph: ");
						stringBuilder.Append(this.endNode.GraphIndex);
					}
					stringBuilder.Append("\nStart Node");
					stringBuilder.Append("\n\tPoint: ");
					StringBuilder stringBuilder3 = stringBuilder;
					Vector3 vector2 = this.startPoint;
					stringBuilder3.Append(vector2.ToString());
					stringBuilder.Append("\n\tGraph: ");
					if (this.startNode != null)
					{
						stringBuilder.Append(this.startNode.GraphIndex);
					}
					else
					{
						stringBuilder.Append("< null startNode >");
					}
				}
			}
			if (base.error)
			{
				stringBuilder.Append("\nError: ");
				stringBuilder.Append(base.errorLog);
			}
			if (logMode == PathLog.Heavy && !AstarPath.IsUsingMultithreading)
			{
				stringBuilder.Append("\nCallback references ");
				if (this.callback != null)
				{
					stringBuilder.Append(this.callback.Target.GetType().FullName).AppendLine();
				}
				else
				{
					stringBuilder.AppendLine("NULL");
				}
			}
			stringBuilder.Append("\nPath Number ");
			stringBuilder.Append(this.pathID);
			return stringBuilder.ToString();
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0004767D File Offset: 0x00045A7D
		protected override void Recycle()
		{
			PathPool<ABPath>.Recycle(this);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00047688 File Offset: 0x00045A88
		public Vector3 GetMovementVector(Vector3 point)
		{
			if (this.vectorPath == null || this.vectorPath.Count == 0)
			{
				return Vector3.zero;
			}
			if (this.vectorPath.Count == 1)
			{
				return this.vectorPath[0] - point;
			}
			float num = float.PositiveInfinity;
			int num2 = 0;
			for (int i = 0; i < this.vectorPath.Count - 1; i++)
			{
				Vector3 a = AstarMath.NearestPointStrict(this.vectorPath[i], this.vectorPath[i + 1], point);
				float sqrMagnitude = (a - point).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					num2 = i;
				}
			}
			return this.vectorPath[num2 + 1] - point;
		}

		// Token: 0x0400061A RID: 1562
		public bool recalcStartEndCosts = true;

		// Token: 0x0400061B RID: 1563
		public GraphNode startNode;

		// Token: 0x0400061C RID: 1564
		public GraphNode endNode;

		// Token: 0x0400061D RID: 1565
		public GraphNode startHint;

		// Token: 0x0400061E RID: 1566
		public GraphNode endHint;

		// Token: 0x0400061F RID: 1567
		public Vector3 originalStartPoint;

		// Token: 0x04000620 RID: 1568
		public Vector3 originalEndPoint;

		// Token: 0x04000621 RID: 1569
		public Vector3 startPoint;

		// Token: 0x04000622 RID: 1570
		public Vector3 endPoint;

		// Token: 0x04000623 RID: 1571
		protected bool hasEndPoint = true;

		// Token: 0x04000624 RID: 1572
		public Int3 startIntPoint;

		// Token: 0x04000625 RID: 1573
		public bool calculatePartial;

		// Token: 0x04000626 RID: 1574
		protected PathNode partialBestTarget;

		// Token: 0x04000627 RID: 1575
		protected int[] endNodeCosts;
	}
}
