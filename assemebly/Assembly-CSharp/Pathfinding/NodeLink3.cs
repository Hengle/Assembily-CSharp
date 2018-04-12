using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000028 RID: 40
	[AddComponentMenu("Pathfinding/Link3")]
	public class NodeLink3 : GraphModifier
	{
		// Token: 0x060001CB RID: 459 RVA: 0x00011FA0 File Offset: 0x000103A0
		public static NodeLink3 GetNodeLink(GraphNode node)
		{
			NodeLink3 result;
			NodeLink3.reference.TryGetValue(node, out result);
			return result;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00011FBC File Offset: 0x000103BC
		public Transform StartTransform
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00011FC4 File Offset: 0x000103C4
		public Transform EndTransform
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00011FCC File Offset: 0x000103CC
		public GraphNode StartNode
		{
			get
			{
				return this.startNode;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00011FD4 File Offset: 0x000103D4
		public GraphNode EndNode
		{
			get
			{
				return this.endNode;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00011FDC File Offset: 0x000103DC
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

		// Token: 0x060001D1 RID: 465 RVA: 0x00012014 File Offset: 0x00010414
		public void InternalOnPostScan()
		{
			if (AstarPath.active.astarData.pointGraph == null)
			{
				AstarPath.active.astarData.AddGraph(new PointGraph());
			}
			this.startNode = AstarPath.active.astarData.pointGraph.AddNode<NodeLink3Node>(new NodeLink3Node(AstarPath.active), (Int3)this.StartTransform.position);
			this.startNode.link = this;
			this.endNode = AstarPath.active.astarData.pointGraph.AddNode<NodeLink3Node>(new NodeLink3Node(AstarPath.active), (Int3)this.EndTransform.position);
			this.endNode.link = this;
			this.connectedNode1 = null;
			this.connectedNode2 = null;
			if (this.startNode == null || this.endNode == null)
			{
				this.startNode = null;
				this.endNode = null;
				return;
			}
			this.postScanCalled = true;
			NodeLink3.reference[this.startNode] = this;
			NodeLink3.reference[this.endNode] = this;
			this.Apply(true);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0001212C File Offset: 0x0001052C
		public override void OnGraphsPostUpdate()
		{
			if (!AstarPath.active.isScanning)
			{
				if (this.connectedNode1 != null && this.connectedNode1.Destroyed)
				{
					this.connectedNode1 = null;
				}
				if (this.connectedNode2 != null && this.connectedNode2.Destroyed)
				{
					this.connectedNode2 = null;
				}
				if (!this.postScanCalled)
				{
					this.OnPostScan();
				}
				else
				{
					this.Apply(false);
				}
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000121AC File Offset: 0x000105AC
		protected override void OnEnable()
		{
			base.OnEnable();
			if (AstarPath.active != null && AstarPath.active.astarData != null && AstarPath.active.astarData.pointGraph != null)
			{
				this.OnGraphsPostUpdate();
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000121F8 File Offset: 0x000105F8
		protected override void OnDisable()
		{
			base.OnDisable();
			this.postScanCalled = false;
			if (this.startNode != null)
			{
				NodeLink3.reference.Remove(this.startNode);
			}
			if (this.endNode != null)
			{
				NodeLink3.reference.Remove(this.endNode);
			}
			if (this.startNode != null && this.endNode != null)
			{
				this.startNode.RemoveConnection(this.endNode);
				this.endNode.RemoveConnection(this.startNode);
				if (this.connectedNode1 != null && this.connectedNode2 != null)
				{
					this.startNode.RemoveConnection(this.connectedNode1);
					this.connectedNode1.RemoveConnection(this.startNode);
					this.endNode.RemoveConnection(this.connectedNode2);
					this.connectedNode2.RemoveConnection(this.endNode);
				}
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000122DC File Offset: 0x000106DC
		private void RemoveConnections(GraphNode node)
		{
			node.ClearConnections(true);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000122E5 File Offset: 0x000106E5
		[ContextMenu("Recalculate neighbours")]
		private void ContextApplyForce()
		{
			if (Application.isPlaying)
			{
				this.Apply(true);
				if (AstarPath.active != null)
				{
					AstarPath.active.FloodFill();
				}
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00012314 File Offset: 0x00010714
		public void Apply(bool forceNewCheck)
		{
			NNConstraint none = NNConstraint.None;
			none.distanceXZ = true;
			int graphIndex = (int)this.startNode.GraphIndex;
			none.graphMask = ~(1 << graphIndex);
			bool flag = true;
			NNInfo nearest = AstarPath.active.GetNearest(this.StartTransform.position, none);
			flag &= (nearest.node == this.connectedNode1 && nearest.node != null);
			this.connectedNode1 = (nearest.node as MeshNode);
			this.clamped1 = nearest.clampedPosition;
			if (this.connectedNode1 != null)
			{
				Debug.DrawRay((Vector3)this.connectedNode1.position, Vector3.up * 5f, Color.red);
			}
			NNInfo nearest2 = AstarPath.active.GetNearest(this.EndTransform.position, none);
			flag &= (nearest2.node == this.connectedNode2 && nearest2.node != null);
			this.connectedNode2 = (nearest2.node as MeshNode);
			this.clamped2 = nearest2.clampedPosition;
			if (this.connectedNode2 != null)
			{
				Debug.DrawRay((Vector3)this.connectedNode2.position, Vector3.up * 5f, Color.cyan);
			}
			if (this.connectedNode2 == null || this.connectedNode1 == null)
			{
				return;
			}
			this.startNode.SetPosition((Int3)this.StartTransform.position);
			this.endNode.SetPosition((Int3)this.EndTransform.position);
			if (flag && !forceNewCheck)
			{
				return;
			}
			this.RemoveConnections(this.startNode);
			this.RemoveConnections(this.endNode);
			uint cost = (uint)Mathf.RoundToInt((float)((Int3)(this.StartTransform.position - this.EndTransform.position)).costMagnitude * this.costFactor);
			this.startNode.AddConnection(this.endNode, cost);
			this.endNode.AddConnection(this.startNode, cost);
			Int3 rhs = this.connectedNode2.position - this.connectedNode1.position;
			for (int i = 0; i < this.connectedNode1.GetVertexCount(); i++)
			{
				Int3 vertex = this.connectedNode1.GetVertex(i);
				Int3 vertex2 = this.connectedNode1.GetVertex((i + 1) % this.connectedNode1.GetVertexCount());
				if (Int3.DotLong((vertex2 - vertex).Normal2D(), rhs) <= 0L)
				{
					for (int j = 0; j < this.connectedNode2.GetVertexCount(); j++)
					{
						Int3 vertex3 = this.connectedNode2.GetVertex(j);
						Int3 vertex4 = this.connectedNode2.GetVertex((j + 1) % this.connectedNode2.GetVertexCount());
						if (Int3.DotLong((vertex4 - vertex3).Normal2D(), rhs) >= 0L)
						{
							if ((double)Int3.Angle(vertex4 - vertex3, vertex2 - vertex) > 2.9670598109563189)
							{
								float num = 0f;
								float num2 = 1f;
								num2 = Math.Min(num2, AstarMath.NearestPointFactor(vertex, vertex2, vertex3));
								num = Math.Max(num, AstarMath.NearestPointFactor(vertex, vertex2, vertex4));
								if (num2 >= num)
								{
									Vector3 vector = (Vector3)(vertex2 - vertex) * num + (Vector3)vertex;
									Vector3 vector2 = (Vector3)(vertex2 - vertex) * num2 + (Vector3)vertex;
									this.startNode.portalA = vector;
									this.startNode.portalB = vector2;
									this.endNode.portalA = vector2;
									this.endNode.portalB = vector;
									this.connectedNode1.AddConnection(this.startNode, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped1 - this.StartTransform.position)).costMagnitude * this.costFactor));
									this.connectedNode2.AddConnection(this.endNode, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped2 - this.EndTransform.position)).costMagnitude * this.costFactor));
									this.startNode.AddConnection(this.connectedNode1, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped1 - this.StartTransform.position)).costMagnitude * this.costFactor));
									this.endNode.AddConnection(this.connectedNode2, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped2 - this.EndTransform.position)).costMagnitude * this.costFactor));
									return;
								}
								Debug.LogError(string.Concat(new object[]
								{
									"Wait wut!? ",
									num,
									" ",
									num2,
									" ",
									vertex,
									" ",
									vertex2,
									" ",
									vertex3,
									" ",
									vertex4,
									"\nTODO, fix this error"
								}));
							}
						}
					}
				}
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000128B0 File Offset: 0x00010CB0
		private void DrawCircle(Vector3 o, float r, int detail, Color col)
		{
			Vector3 from = new Vector3(Mathf.Cos(0f) * r, 0f, Mathf.Sin(0f) * r) + o;
			Gizmos.color = col;
			for (int i = 0; i <= detail; i++)
			{
				float f = (float)i * 3.14159274f * 2f / (float)detail;
				Vector3 vector = new Vector3(Mathf.Cos(f) * r, 0f, Mathf.Sin(f) * r) + o;
				Gizmos.DrawLine(from, vector);
				from = vector;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0001293C File Offset: 0x00010D3C
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

		// Token: 0x060001DA RID: 474 RVA: 0x000129E3 File Offset: 0x00010DE3
		public virtual void OnDrawGizmosSelected()
		{
			this.OnDrawGizmos(true);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000129EC File Offset: 0x00010DEC
		public void OnDrawGizmos()
		{
			this.OnDrawGizmos(false);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000129F8 File Offset: 0x00010DF8
		public void OnDrawGizmos(bool selected)
		{
			Color color = (!selected) ? NodeLink3.GizmosColor : NodeLink3.GizmosColorSelected;
			if (this.StartTransform != null)
			{
				this.DrawCircle(this.StartTransform.position, 0.4f, 10, color);
			}
			if (this.EndTransform != null)
			{
				this.DrawCircle(this.EndTransform.position, 0.4f, 10, color);
			}
			if (this.StartTransform != null && this.EndTransform != null)
			{
				Gizmos.color = color;
				this.DrawGizmoBezier(this.StartTransform.position, this.EndTransform.position);
				if (selected)
				{
					Vector3 normalized = Vector3.Cross(Vector3.up, this.EndTransform.position - this.StartTransform.position).normalized;
					this.DrawGizmoBezier(this.StartTransform.position + normalized * 0.1f, this.EndTransform.position + normalized * 0.1f);
					this.DrawGizmoBezier(this.StartTransform.position - normalized * 0.1f, this.EndTransform.position - normalized * 0.1f);
				}
			}
		}

		// Token: 0x04000167 RID: 359
		protected static Dictionary<GraphNode, NodeLink3> reference = new Dictionary<GraphNode, NodeLink3>();

		// Token: 0x04000168 RID: 360
		public Transform end;

		// Token: 0x04000169 RID: 361
		public float costFactor = 1f;

		// Token: 0x0400016A RID: 362
		public bool oneWay;

		// Token: 0x0400016B RID: 363
		private NodeLink3Node startNode;

		// Token: 0x0400016C RID: 364
		private NodeLink3Node endNode;

		// Token: 0x0400016D RID: 365
		private MeshNode connectedNode1;

		// Token: 0x0400016E RID: 366
		private MeshNode connectedNode2;

		// Token: 0x0400016F RID: 367
		private Vector3 clamped1;

		// Token: 0x04000170 RID: 368
		private Vector3 clamped2;

		// Token: 0x04000171 RID: 369
		private bool postScanCalled;

		// Token: 0x04000172 RID: 370
		private static readonly Color GizmosColor = new Color(0.807843149f, 0.533333361f, 0.1882353f, 0.5f);

		// Token: 0x04000173 RID: 371
		private static readonly Color GizmosColorSelected = new Color(0.921568632f, 0.482352942f, 0.1254902f, 1f);
	}
}
