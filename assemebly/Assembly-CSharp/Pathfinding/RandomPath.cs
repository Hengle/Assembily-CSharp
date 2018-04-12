using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000E4 RID: 228
	public class RandomPath : ABPath
	{
		// Token: 0x06000786 RID: 1926 RVA: 0x00047B0B File Offset: 0x00045F0B
		public RandomPath()
		{
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00047B1E File Offset: 0x00045F1E
		public RandomPath(Vector3 start, int length, OnPathDelegate callback = null)
		{
			throw new Exception("This constructor is obsolete. Please use the pooling API and the Setup methods");
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00047B3B File Offset: 0x00045F3B
		public override bool FloodingPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00047B40 File Offset: 0x00045F40
		public override void Reset()
		{
			base.Reset();
			this.searchLength = 5000;
			this.spread = 5000;
			this.uniform = true;
			this.aimStrength = 0f;
			this.chosenNodeR = null;
			this.maxGScoreNodeR = null;
			this.maxGScore = 0;
			this.aim = Vector3.zero;
			this.nodesEvaluatedRep = 0;
			this.hasEndPoint = false;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00047BA9 File Offset: 0x00045FA9
		protected override void Recycle()
		{
			PathPool<RandomPath>.Recycle(this);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00047BB4 File Offset: 0x00045FB4
		public static RandomPath Construct(Vector3 start, int length, OnPathDelegate callback = null)
		{
			RandomPath path = PathPool<RandomPath>.GetPath();
			path.Setup(start, length, callback);
			return path;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00047BD4 File Offset: 0x00045FD4
		protected RandomPath Setup(Vector3 start, int length, OnPathDelegate callback)
		{
			this.callback = callback;
			this.searchLength = length;
			this.originalStartPoint = start;
			this.originalEndPoint = Vector3.zero;
			this.startPoint = start;
			this.endPoint = Vector3.zero;
			this.startIntPoint = (Int3)start;
			this.hasEndPoint = false;
			return this;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00047C28 File Offset: 0x00046028
		public override void ReturnPath()
		{
			if (this.path != null && this.path.Count > 0)
			{
				this.endNode = this.path[this.path.Count - 1];
				this.endPoint = (Vector3)this.endNode.position;
				this.originalEndPoint = this.endPoint;
				this.hTarget = this.endNode.position;
			}
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00047CBC File Offset: 0x000460BC
		public override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint, this.startHint);
			this.startPoint = nearest.clampedPosition;
			this.endPoint = this.startPoint;
			this.startIntPoint = (Int3)this.startPoint;
			this.hTarget = (Int3)this.aim;
			this.startNode = nearest.node;
			this.endNode = this.startNode;
			if (this.startNode == null || this.endNode == null)
			{
				base.LogError("Couldn't find close nodes to the start point");
				base.Error();
				return;
			}
			if (!this.startNode.Walkable)
			{
				base.LogError("The node closest to the start point is not walkable");
				base.Error();
				return;
			}
			this.heuristicScale = this.aimStrength;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00047DA4 File Offset: 0x000461A4
		public override void Initialize()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			if (this.searchLength + this.spread <= 0)
			{
				base.CompleteState = PathCompleteState.Complete;
				this.Trace(pathNode);
				return;
			}
			pathNode.pathID = this.pathID;
			pathNode.parent = null;
			pathNode.cost = 0u;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			this.startNode.Open(this, pathNode, this.pathHandler);
			this.searchedNodes++;
			if (this.pathHandler.HeapEmpty())
			{
				base.LogError("No open points, the start node didn't open any nodes");
				base.Error();
				return;
			}
			this.currentR = this.pathHandler.PopNode();
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00047E84 File Offset: 0x00046284
		public override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				if ((ulong)this.currentR.G >= (ulong)((long)this.searchLength))
				{
					this.nodesEvaluatedRep++;
					if (this.chosenNodeR == null)
					{
						this.chosenNodeR = this.currentR;
					}
					else if (this.rnd.NextDouble() <= (double)(1f / (float)this.nodesEvaluatedRep))
					{
						this.chosenNodeR = this.currentR;
					}
					if ((ulong)this.currentR.G >= (ulong)((long)(this.searchLength + this.spread)))
					{
						base.CompleteState = PathCompleteState.Complete;
						break;
					}
				}
				else if ((ulong)this.currentR.G > (ulong)((long)this.maxGScore))
				{
					this.maxGScore = (int)this.currentR.G;
					this.maxGScoreNodeR = this.currentR;
				}
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.pathHandler.HeapEmpty())
				{
					if (this.chosenNodeR != null)
					{
						base.CompleteState = PathCompleteState.Complete;
					}
					else if (this.maxGScoreNodeR != null)
					{
						this.chosenNodeR = this.maxGScoreNodeR;
						base.CompleteState = PathCompleteState.Complete;
					}
					else
					{
						base.LogError("Not a single node found to search");
						base.Error();
					}
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
			if (base.CompleteState == PathCompleteState.Complete)
			{
				this.Trace(this.chosenNodeR);
			}
		}

		// Token: 0x0400064A RID: 1610
		public int searchLength;

		// Token: 0x0400064B RID: 1611
		public int spread;

		// Token: 0x0400064C RID: 1612
		public bool uniform;

		// Token: 0x0400064D RID: 1613
		public float aimStrength;

		// Token: 0x0400064E RID: 1614
		private PathNode chosenNodeR;

		// Token: 0x0400064F RID: 1615
		private PathNode maxGScoreNodeR;

		// Token: 0x04000650 RID: 1616
		private int maxGScore;

		// Token: 0x04000651 RID: 1617
		public Vector3 aim;

		// Token: 0x04000652 RID: 1618
		private int nodesEvaluatedRep;

		// Token: 0x04000653 RID: 1619
		private System.Random rnd = new System.Random();
	}
}
