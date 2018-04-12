using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000054 RID: 84
	public struct NNInfo
	{
		// Token: 0x06000362 RID: 866 RVA: 0x0001AB08 File Offset: 0x00018F08
		public NNInfo(GraphNode node)
		{
			this.node = node;
			this.constrainedNode = null;
			this.constClampedPosition = Vector3.zero;
			if (node != null)
			{
				this.clampedPosition = (Vector3)node.position;
			}
			else
			{
				this.clampedPosition = Vector3.zero;
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001AB55 File Offset: 0x00018F55
		public void SetConstrained(GraphNode constrainedNode, Vector3 clampedPosition)
		{
			this.constrainedNode = constrainedNode;
			this.constClampedPosition = clampedPosition;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001AB68 File Offset: 0x00018F68
		public void UpdateInfo()
		{
			if (this.node != null)
			{
				this.clampedPosition = (Vector3)this.node.position;
			}
			else
			{
				this.clampedPosition = Vector3.zero;
			}
			if (this.constrainedNode != null)
			{
				this.constClampedPosition = (Vector3)this.constrainedNode.position;
			}
			else
			{
				this.constClampedPosition = Vector3.zero;
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001ABD7 File Offset: 0x00018FD7
		public static explicit operator Vector3(NNInfo ob)
		{
			return ob.clampedPosition;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001ABE0 File Offset: 0x00018FE0
		public static explicit operator GraphNode(NNInfo ob)
		{
			return ob.node;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001ABE9 File Offset: 0x00018FE9
		public static explicit operator NNInfo(GraphNode ob)
		{
			return new NNInfo(ob);
		}

		// Token: 0x040002A6 RID: 678
		public GraphNode node;

		// Token: 0x040002A7 RID: 679
		public GraphNode constrainedNode;

		// Token: 0x040002A8 RID: 680
		public Vector3 clampedPosition;

		// Token: 0x040002A9 RID: 681
		public Vector3 constClampedPosition;
	}
}
