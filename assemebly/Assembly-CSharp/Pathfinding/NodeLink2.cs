using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000026 RID: 38
	[AddComponentMenu("Pathfinding/Link2")]
	public class NodeLink2 : GraphModifier
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000E10C File Offset: 0x0000C50C
		public static NodeLink2 GetNodeLink(GraphNode node)
		{
			NodeLink2 result;
			NodeLink2.reference.TryGetValue(node, out result);
			return result;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000E128 File Offset: 0x0000C528
		public Transform StartTransform
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000E130 File Offset: 0x0000C530
		public Transform EndTransform
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000E138 File Offset: 0x0000C538
		public GraphNode StartNode
		{
			get
			{
				return this.startNode;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000E140 File Offset: 0x0000C540
		public GraphNode EndNode
		{
			get
			{
				return this.endNode;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000E148 File Offset: 0x0000C548
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

		// Token: 0x060001B8 RID: 440 RVA: 0x0000E180 File Offset: 0x0000C580
		public void InternalOnPostScan()
		{
			if (this.EndTransform == null || this.StartTransform == null)
			{
				return;
			}
			if (AstarPath.active.astarData.pointGraph == null)
			{
				AstarPath.active.astarData.AddGraph(new PointGraph());
			}
			NodeLink2 x;
			if (this.startNode != null && NodeLink2.reference.TryGetValue(this.startNode, out x) && x == this)
			{
				NodeLink2.reference.Remove(this.startNode);
			}
			NodeLink2 x2;
			if (this.endNode != null && NodeLink2.reference.TryGetValue(this.endNode, out x2) && x2 == this)
			{
				NodeLink2.reference.Remove(this.endNode);
			}
			this.startNode = AstarPath.active.astarData.pointGraph.AddNode((Int3)this.StartTransform.position);
			this.endNode = AstarPath.active.astarData.pointGraph.AddNode((Int3)this.EndTransform.position);
			this.connectedNode1 = null;
			this.connectedNode2 = null;
			if (this.startNode == null || this.endNode == null)
			{
				this.startNode = null;
				this.endNode = null;
				return;
			}
			this.postScanCalled = true;
			NodeLink2.reference[this.startNode] = this;
			NodeLink2.reference[this.endNode] = this;
			this.Apply(true);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000E310 File Offset: 0x0000C710
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

		// Token: 0x060001BA RID: 442 RVA: 0x0000E390 File Offset: 0x0000C790
		protected override void OnEnable()
		{
			base.OnEnable();
			if (AstarPath.active != null && AstarPath.active.astarData != null && AstarPath.active.astarData.pointGraph != null)
			{
				this.OnGraphsPostUpdate();
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000E3DC File Offset: 0x0000C7DC
		protected override void OnDisable()
		{
			base.OnDisable();
			this.postScanCalled = false;
			NodeLink2 x;
			if (this.startNode != null && NodeLink2.reference.TryGetValue(this.startNode, out x) && x == this)
			{
				NodeLink2.reference.Remove(this.startNode);
			}
			NodeLink2 x2;
			if (this.endNode != null && NodeLink2.reference.TryGetValue(this.endNode, out x2) && x2 == this)
			{
				NodeLink2.reference.Remove(this.endNode);
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

		// Token: 0x060001BC RID: 444 RVA: 0x0000E506 File Offset: 0x0000C906
		private void RemoveConnections(GraphNode node)
		{
			node.ClearConnections(true);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000E50F File Offset: 0x0000C90F
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

		// Token: 0x060001BE RID: 446 RVA: 0x0000E53C File Offset: 0x0000C93C
		public void Apply(bool forceNewCheck)
		{
			NNConstraint none = NNConstraint.None;
			int graphIndex = (int)this.startNode.GraphIndex;
			none.graphMask = ~(1 << graphIndex);
			this.startNode.SetPosition((Int3)this.StartTransform.position);
			this.endNode.SetPosition((Int3)this.EndTransform.position);
			this.RemoveConnections(this.startNode);
			this.RemoveConnections(this.endNode);
			uint cost = (uint)Mathf.RoundToInt((float)((Int3)(this.StartTransform.position - this.EndTransform.position)).costMagnitude * this.costFactor);
			this.startNode.AddConnection(this.endNode, cost);
			this.endNode.AddConnection(this.startNode, cost);
			if (this.connectedNode1 == null || forceNewCheck)
			{
				NNInfo nearest = AstarPath.active.GetNearest(this.StartTransform.position, none);
				this.connectedNode1 = (nearest.node as MeshNode);
				this.clamped1 = nearest.clampedPosition;
			}
			if (this.connectedNode2 == null || forceNewCheck)
			{
				NNInfo nearest2 = AstarPath.active.GetNearest(this.EndTransform.position, none);
				this.connectedNode2 = (nearest2.node as MeshNode);
				this.clamped2 = nearest2.clampedPosition;
			}
			if (this.connectedNode2 == null || this.connectedNode1 == null)
			{
				return;
			}
			this.connectedNode1.AddConnection(this.startNode, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped1 - this.StartTransform.position)).costMagnitude * this.costFactor));
			if (!this.oneWay)
			{
				this.connectedNode2.AddConnection(this.endNode, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped2 - this.EndTransform.position)).costMagnitude * this.costFactor));
			}
			if (!this.oneWay)
			{
				this.startNode.AddConnection(this.connectedNode1, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped1 - this.StartTransform.position)).costMagnitude * this.costFactor));
			}
			this.endNode.AddConnection(this.connectedNode2, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped2 - this.EndTransform.position)).costMagnitude * this.costFactor));
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000E7D8 File Offset: 0x0000CBD8
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

		// Token: 0x060001C0 RID: 448 RVA: 0x0000E864 File Offset: 0x0000CC64
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

		// Token: 0x060001C1 RID: 449 RVA: 0x0000E90B File Offset: 0x0000CD0B
		public virtual void OnDrawGizmosSelected()
		{
			this.OnDrawGizmos(true);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000E914 File Offset: 0x0000CD14
		public void OnDrawGizmos()
		{
			this.OnDrawGizmos(false);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000E920 File Offset: 0x0000CD20
		public void OnDrawGizmos(bool selected)
		{
			Color color = (!selected) ? NodeLink2.GizmosColor : NodeLink2.GizmosColorSelected;
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

		// Token: 0x04000157 RID: 343
		protected static Dictionary<GraphNode, NodeLink2> reference = new Dictionary<GraphNode, NodeLink2>();

		// Token: 0x04000158 RID: 344
		public Transform end;

		// Token: 0x04000159 RID: 345
		public float costFactor = 1f;

		// Token: 0x0400015A RID: 346
		public bool oneWay;

		// Token: 0x0400015B RID: 347
		private PointNode startNode;

		// Token: 0x0400015C RID: 348
		private PointNode endNode;

		// Token: 0x0400015D RID: 349
		private MeshNode connectedNode1;

		// Token: 0x0400015E RID: 350
		private MeshNode connectedNode2;

		// Token: 0x0400015F RID: 351
		private Vector3 clamped1;

		// Token: 0x04000160 RID: 352
		private Vector3 clamped2;

		// Token: 0x04000161 RID: 353
		private bool postScanCalled;

		// Token: 0x04000162 RID: 354
		private static readonly Color GizmosColor = new Color(0.807843149f, 0.533333361f, 0.1882353f, 0.5f);

		// Token: 0x04000163 RID: 355
		private static readonly Color GizmosColorSelected = new Color(0.921568632f, 0.482352942f, 0.1254902f, 1f);
	}
}
