using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000027 RID: 39
	public class NodeLink3Node : PointNode
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00011E61 File Offset: 0x00010261
		public NodeLink3Node(AstarPath active) : base(active)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00011E6C File Offset: 0x0001026C
		public override bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			if (this.connections.Length < 2)
			{
				return false;
			}
			if (this.connections.Length != 2)
			{
				throw new Exception("Invalid NodeLink3Node. Expected 2 connections, found " + this.connections.Length);
			}
			if (left != null)
			{
				left.Add(this.portalA);
				right.Add(this.portalB);
			}
			return true;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00011ED4 File Offset: 0x000102D4
		public GraphNode GetOther(GraphNode a)
		{
			if (this.connections.Length < 2)
			{
				return null;
			}
			if (this.connections.Length != 2)
			{
				throw new Exception("Invalid NodeLink3Node. Expected 2 connections, found " + this.connections.Length);
			}
			return (a != this.connections[0]) ? (this.connections[0] as NodeLink3Node).GetOtherInternal(this) : (this.connections[1] as NodeLink3Node).GetOtherInternal(this);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00011F55 File Offset: 0x00010355
		private GraphNode GetOtherInternal(GraphNode a)
		{
			if (this.connections.Length < 2)
			{
				return null;
			}
			return (a != this.connections[0]) ? this.connections[0] : this.connections[1];
		}

		// Token: 0x04000164 RID: 356
		public NodeLink3 link;

		// Token: 0x04000165 RID: 357
		public Vector3 portalA;

		// Token: 0x04000166 RID: 358
		public Vector3 portalB;
	}
}
