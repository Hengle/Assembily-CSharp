using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000DC RID: 220
	public class ConstantPath : Path
	{
		// Token: 0x06000748 RID: 1864 RVA: 0x00047754 File Offset: 0x00045B54
		public ConstantPath()
		{
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0004775C File Offset: 0x00045B5C
		[Obsolete("Please use the Construct method instead")]
		public ConstantPath(Vector3 start, OnPathDelegate callbackDelegate)
		{
			throw new Exception("This constructor is obsolete, please use the Construct method instead");
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0004776E File Offset: 0x00045B6E
		[Obsolete("Please use the Construct method instead")]
		public ConstantPath(Vector3 start, int maxGScore, OnPathDelegate callbackDelegate)
		{
			throw new Exception("This constructor is obsolete, please use the Construct method instead");
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x00047780 File Offset: 0x00045B80
		public override bool FloodingPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00047784 File Offset: 0x00045B84
		public static ConstantPath Construct(Vector3 start, int maxGScore, OnPathDelegate callback = null)
		{
			ConstantPath path = PathPool<ConstantPath>.GetPath();
			path.Setup(start, maxGScore, callback);
			return path;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x000477A1 File Offset: 0x00045BA1
		protected void Setup(Vector3 start, int maxGScore, OnPathDelegate callback)
		{
			this.callback = callback;
			this.startPoint = start;
			this.originalStartPoint = this.startPoint;
			this.endingCondition = new EndingConditionDistance(this, maxGScore);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000477CA File Offset: 0x00045BCA
		public override void OnEnterPool()
		{
			base.OnEnterPool();
			if (this.allNodes != null)
			{
				ListPool<GraphNode>.Release(this.allNodes);
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000477E8 File Offset: 0x00045BE8
		protected override void Recycle()
		{
			PathPool<ConstantPath>.Recycle(this);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000477F0 File Offset: 0x00045BF0
		public override void Reset()
		{
			base.Reset();
			this.allNodes = ListPool<GraphNode>.Claim();
			this.endingCondition = null;
			this.originalStartPoint = Vector3.zero;
			this.startPoint = Vector3.zero;
			this.startNode = null;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00047830 File Offset: 0x00045C30
		public override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			this.startNode = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint).node;
			if (this.startNode == null)
			{
				base.Error();
				base.LogError("Could not find close node to the start point");
				return;
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00047890 File Offset: 0x00045C90
		public override void Initialize()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			pathNode.pathID = this.pathHandler.PathID;
			pathNode.parent = null;
			pathNode.cost = 0u;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			this.startNode.Open(this, pathNode, this.pathHandler);
			this.searchedNodes++;
			pathNode.flag1 = true;
			this.allNodes.Add(this.startNode);
			if (this.pathHandler.HeapEmpty())
			{
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			this.currentR = this.pathHandler.PopNode();
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00047960 File Offset: 0x00045D60
		public override void Cleanup()
		{
			int count = this.allNodes.Count;
			for (int i = 0; i < count; i++)
			{
				this.pathHandler.GetPathNode(this.allNodes[i]).flag1 = false;
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x000479A8 File Offset: 0x00045DA8
		public override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				if (this.endingCondition.TargetFound(this.currentR))
				{
					base.CompleteState = PathCompleteState.Complete;
					break;
				}
				if (!this.currentR.flag1)
				{
					this.allNodes.Add(this.currentR.node);
					this.currentR.flag1 = true;
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
					if (this.searchedNodes > 1000000)
					{
						throw new Exception("Probable infinite loop. Over 1,000,000 nodes searched");
					}
				}
				num++;
			}
		}

		// Token: 0x04000628 RID: 1576
		public GraphNode startNode;

		// Token: 0x04000629 RID: 1577
		public Vector3 startPoint;

		// Token: 0x0400062A RID: 1578
		public Vector3 originalStartPoint;

		// Token: 0x0400062B RID: 1579
		public List<GraphNode> allNodes;

		// Token: 0x0400062C RID: 1580
		public PathEndingCondition endingCondition;
	}
}
