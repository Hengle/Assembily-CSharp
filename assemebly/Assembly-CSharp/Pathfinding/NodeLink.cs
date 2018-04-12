using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000025 RID: 37
	[AddComponentMenu("Pathfinding/Link")]
	public class NodeLink : GraphModifier
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00011293 File Offset: 0x0000F693
		public Transform Start
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0001129B File Offset: 0x0000F69B
		public Transform End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000112A3 File Offset: 0x0000F6A3
		public override void OnPostScan()
		{
			if (AstarPath.active.isScanning)
			{
				this.InternalOnPostScan();
			}
			else
			{
				AstarPath.active.AddWorkItem(new AstarPath.AstarWorkItem(delegate(bool force)
				{
					this.InternalOnPostScan();
					return true;
				}));
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000112DA File Offset: 0x0000F6DA
		public void InternalOnPostScan()
		{
			this.Apply();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000112E2 File Offset: 0x0000F6E2
		public override void OnGraphsPostUpdate()
		{
			if (!AstarPath.active.isScanning)
			{
				AstarPath.active.AddWorkItem(new AstarPath.AstarWorkItem(delegate(bool force)
				{
					this.InternalOnPostScan();
					return true;
				}));
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00011310 File Offset: 0x0000F710
		public virtual void Apply()
		{
			if (this.Start == null || this.End == null || AstarPath.active == null)
			{
				return;
			}
			GraphNode node = AstarPath.active.GetNearest(this.Start.position).node;
			GraphNode node2 = AstarPath.active.GetNearest(this.End.position).node;
			if (node == null || node2 == null)
			{
				return;
			}
			if (this.deleteConnection)
			{
				node.RemoveConnection(node2);
				if (!this.oneWay)
				{
					node2.RemoveConnection(node);
				}
			}
			else
			{
				uint cost = (uint)Math.Round((double)((float)(node.position - node2.position).costMagnitude * this.costFactor));
				node.AddConnection(node2, cost);
				if (!this.oneWay)
				{
					node2.AddConnection(node, cost);
				}
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0001140C File Offset: 0x0000F80C
		public void OnDrawGizmos()
		{
			if (this.Start == null || this.End == null)
			{
				return;
			}
			Vector3 position = this.Start.position;
			Vector3 position2 = this.End.position;
			Gizmos.color = ((!this.deleteConnection) ? Color.green : Color.red);
			this.DrawGizmoBezier(position, position2);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0001147C File Offset: 0x0000F87C
		private void DrawGizmoBezier(Vector3 p1, Vector3 p2)
		{
			Vector3 vector = p2 - p1;
			if (vector == Vector3.zero)
			{
				return;
			}
			Vector3 rhs = Vector3.Cross(Vector3.up, vector);
			Vector3 vector2 = Vector3.Cross(vector, rhs).normalized;
			vector2 *= vector.magnitude * 0.1f;
			Vector3 p3 = p1 + vector2;
			Vector3 p4 = p2 + vector2;
			Vector3 from = p1;
			for (int i = 1; i <= 20; i++)
			{
				float t = (float)i / 20f;
				Vector3 vector3 = AstarMath.CubicBezier(p1, p3, p4, p2, t);
				Gizmos.DrawLine(from, vector3);
				from = vector3;
			}
		}

		// Token: 0x04000153 RID: 339
		public Transform end;

		// Token: 0x04000154 RID: 340
		public float costFactor = 1f;

		// Token: 0x04000155 RID: 341
		public bool oneWay;

		// Token: 0x04000156 RID: 342
		public bool deleteConnection;
	}
}
