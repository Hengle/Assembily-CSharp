using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000058 RID: 88
	public class GraphUpdateObject
	{
		// Token: 0x06000370 RID: 880 RVA: 0x0001AC75 File Offset: 0x00019075
		public GraphUpdateObject()
		{
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001ACA4 File Offset: 0x000190A4
		public GraphUpdateObject(Bounds b)
		{
			this.bounds = b;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001ACDC File Offset: 0x000190DC
		public virtual void WillUpdateNode(GraphNode node)
		{
			if (this.trackChangedNodes && node != null)
			{
				if (this.changedNodes == null)
				{
					this.changedNodes = ListPool<GraphNode>.Claim();
					this.backupData = ListPool<uint>.Claim();
					this.backupPositionData = ListPool<Int3>.Claim();
				}
				this.changedNodes.Add(node);
				this.backupPositionData.Add(node.position);
				this.backupData.Add(node.Penalty);
				this.backupData.Add(node.Flags);
				GridNode gridNode = node as GridNode;
				if (gridNode != null)
				{
					this.backupData.Add((uint)gridNode.InternalGridFlags);
				}
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001AD84 File Offset: 0x00019184
		public virtual void RevertFromBackup()
		{
			if (!this.trackChangedNodes)
			{
				throw new InvalidOperationException("Changed nodes have not been tracked, cannot revert from backup");
			}
			if (this.changedNodes == null)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < this.changedNodes.Count; i++)
			{
				this.changedNodes[i].Penalty = this.backupData[num];
				num++;
				this.changedNodes[i].Flags = this.backupData[num];
				num++;
				GridNode gridNode = this.changedNodes[i] as GridNode;
				if (gridNode != null)
				{
					gridNode.InternalGridFlags = (ushort)this.backupData[num];
					num++;
				}
				this.changedNodes[i].position = this.backupPositionData[i];
			}
			ListPool<GraphNode>.Release(this.changedNodes);
			ListPool<uint>.Release(this.backupData);
			ListPool<Int3>.Release(this.backupPositionData);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001AE88 File Offset: 0x00019288
		public virtual void Apply(GraphNode node)
		{
			if (this.shape == null || this.shape.Contains(node))
			{
				node.Penalty = (uint)((ulong)node.Penalty + (ulong)((long)this.addPenalty));
				if (this.modifyWalkability)
				{
					node.Walkable = this.setWalkability;
				}
				if (this.modifyTag)
				{
					node.Tag = (uint)this.setTag;
				}
			}
		}

		// Token: 0x040002AE RID: 686
		public Bounds bounds;

		// Token: 0x040002AF RID: 687
		public bool requiresFloodFill = true;

		// Token: 0x040002B0 RID: 688
		public bool updatePhysics = true;

		// Token: 0x040002B1 RID: 689
		public bool resetPenaltyOnPhysics = true;

		// Token: 0x040002B2 RID: 690
		public bool updateErosion = true;

		// Token: 0x040002B3 RID: 691
		public NNConstraint nnConstraint = NNConstraint.None;

		// Token: 0x040002B4 RID: 692
		public int addPenalty;

		// Token: 0x040002B5 RID: 693
		public bool modifyWalkability;

		// Token: 0x040002B6 RID: 694
		public bool setWalkability;

		// Token: 0x040002B7 RID: 695
		public bool modifyTag;

		// Token: 0x040002B8 RID: 696
		public int setTag;

		// Token: 0x040002B9 RID: 697
		public bool trackChangedNodes;

		// Token: 0x040002BA RID: 698
		public List<GraphNode> changedNodes;

		// Token: 0x040002BB RID: 699
		private List<uint> backupData;

		// Token: 0x040002BC RID: 700
		private List<Int3> backupPositionData;

		// Token: 0x040002BD RID: 701
		public GraphUpdateShape shape;
	}
}
