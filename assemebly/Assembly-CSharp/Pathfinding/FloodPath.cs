using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000DF RID: 223
	public class FloodPath : Path
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x000480FD File Offset: 0x000464FD
		[Obsolete("Please use the Construct method instead")]
		public FloodPath(Vector3 start, OnPathDelegate callbackDelegate)
		{
			this.Setup(start, callbackDelegate);
			this.heuristic = Heuristic.None;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0004811B File Offset: 0x0004651B
		public FloodPath()
		{
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0004812A File Offset: 0x0004652A
		public override bool FloodingPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0004812D File Offset: 0x0004652D
		public bool HasPathTo(GraphNode node)
		{
			return this.parents != null && this.parents.ContainsKey(node);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00048149 File Offset: 0x00046549
		public GraphNode GetParent(GraphNode node)
		{
			return this.parents[node];
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00048158 File Offset: 0x00046558
		public static FloodPath Construct(Vector3 start, OnPathDelegate callback = null)
		{
			FloodPath path = PathPool<FloodPath>.GetPath();
			path.Setup(start, callback);
			return path;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00048174 File Offset: 0x00046574
		public static FloodPath Construct(GraphNode start, OnPathDelegate callback = null)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			FloodPath path = PathPool<FloodPath>.GetPath();
			path.Setup(start, callback);
			return path;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000481A1 File Offset: 0x000465A1
		protected void Setup(Vector3 start, OnPathDelegate callback)
		{
			this.callback = callback;
			this.originalStartPoint = start;
			this.startPoint = start;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000481BF File Offset: 0x000465BF
		protected void Setup(GraphNode start, OnPathDelegate callback)
		{
			this.callback = callback;
			this.originalStartPoint = (Vector3)start.position;
			this.startNode = start;
			this.startPoint = (Vector3)start.position;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000481F8 File Offset: 0x000465F8
		public override void Reset()
		{
			base.Reset();
			this.originalStartPoint = Vector3.zero;
			this.startPoint = Vector3.zero;
			this.startNode = null;
			this.parents = new Dictionary<GraphNode, GraphNode>();
			this.saveParents = true;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0004822F File Offset: 0x0004662F
		protected override void Recycle()
		{
			PathPool<FloodPath>.Recycle(this);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00048238 File Offset: 0x00046638
		public override void Prepare()
		{
			if (this.startNode == null)
			{
				this.nnConstraint.tags = this.enabledTags;
				NNInfo nearest = AstarPath.active.GetNearest(this.originalStartPoint, this.nnConstraint);
				this.startPoint = nearest.clampedPosition;
				this.startNode = nearest.node;
			}
			else
			{
				this.startPoint = (Vector3)this.startNode.position;
			}
			if (this.startNode == null)
			{
				base.Error();
				base.LogError("Couldn't find a close node to the start point");
				return;
			}
			if (!this.startNode.Walkable)
			{
				base.Error();
				base.LogError("The node closest to the start point is not walkable");
				return;
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000482EC File Offset: 0x000466EC
		public override void Initialize()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			pathNode.pathID = this.pathHandler.PathID;
			pathNode.parent = null;
			pathNode.cost = 0u;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			this.parents[this.startNode] = null;
			this.startNode.Open(this, pathNode, this.pathHandler);
			this.searchedNodes++;
			if (this.pathHandler.HeapEmpty())
			{
				base.CompleteState = PathCompleteState.Complete;
			}
			this.currentR = this.pathHandler.PopNode();
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000483B8 File Offset: 0x000467B8
		public override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.saveParents)
				{
					this.parents[this.currentR.node] = this.currentR.parent.node;
				}
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
					if (this.searchedNodes > 1000000)
					{
						throw new Exception("Probable infinite loop. Over 1,000,000 nodes searched");
					}
				}
				num++;
			}
		}

		// Token: 0x0400062E RID: 1582
		public Vector3 originalStartPoint;

		// Token: 0x0400062F RID: 1583
		public Vector3 startPoint;

		// Token: 0x04000630 RID: 1584
		public GraphNode startNode;

		// Token: 0x04000631 RID: 1585
		public bool saveParents = true;

		// Token: 0x04000632 RID: 1586
		protected Dictionary<GraphNode, GraphNode> parents;
	}
}
