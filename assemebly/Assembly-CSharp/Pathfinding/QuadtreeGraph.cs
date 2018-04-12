using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000096 RID: 150
	public class QuadtreeGraph : NavGraph
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0002DCB7 File Offset: 0x0002C0B7
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x0002DCBF File Offset: 0x0002C0BF
		public int Width { get; protected set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0002DCC8 File Offset: 0x0002C0C8
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0002DCD0 File Offset: 0x0002C0D0
		public int Height { get; protected set; }

		// Token: 0x06000535 RID: 1333 RVA: 0x0002DCD9 File Offset: 0x0002C0D9
		public override void GetNodes(GraphNodeDelegateCancelable del)
		{
			if (this.root == null)
			{
				return;
			}
			this.root.GetNodes(del);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0002DCF4 File Offset: 0x0002C0F4
		public bool CheckCollision(int x, int y)
		{
			Vector3 position = this.LocalToWorldPosition(x, y, 1);
			return !Physics.CheckSphere(position, this.nodeSize * 1.4142f, this.layerMask);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0002DD30 File Offset: 0x0002C130
		public int CheckNode(int xs, int ys, int width)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Checking Node ",
				xs,
				" ",
				ys,
				" width: ",
				width
			}));
			bool flag = this.map[xs + ys * this.Width];
			for (int i = xs; i < xs + width; i++)
			{
				for (int j = ys; j < ys + width; j++)
				{
					if (this.map[i + j * this.Width] != flag)
					{
						return -1;
					}
				}
			}
			return (!flag) ? 0 : 1;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0002DDE8 File Offset: 0x0002C1E8
		public override void ScanInternal(OnScanStatus statusCallback)
		{
			this.Width = 1 << this.editorWidthLog2;
			this.Height = 1 << this.editorHeightLog2;
			this.map = new BitArray(this.Width * this.Height);
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					this.map.Set(i + j * this.Width, this.CheckCollision(i, j));
				}
			}
			QuadtreeNodeHolder holder = new QuadtreeNodeHolder();
			this.CreateNodeRec(holder, 0, 0, 0);
			this.root = holder;
			this.RecalculateConnectionsRec(this.root, 0, 0, 0);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0002DEA0 File Offset: 0x0002C2A0
		public void RecalculateConnectionsRec(QuadtreeNodeHolder holder, int depth, int x, int y)
		{
			if (holder.node != null)
			{
				this.RecalculateConnections(holder, depth, x, y);
			}
			else
			{
				int num = 1 << Math.Min(this.editorHeightLog2, this.editorWidthLog2) - depth;
				this.RecalculateConnectionsRec(holder.c0, depth + 1, x, y);
				this.RecalculateConnectionsRec(holder.c1, depth + 1, x + num / 2, y);
				this.RecalculateConnectionsRec(holder.c2, depth + 1, x + num / 2, y + num / 2);
				this.RecalculateConnectionsRec(holder.c3, depth + 1, x, y + num / 2);
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0002DF39 File Offset: 0x0002C339
		public Vector3 LocalToWorldPosition(int x, int y, int width)
		{
			return new Vector3(((float)x + (float)width * 0.5f) * this.nodeSize, 0f, ((float)y + (float)width * 0.5f) * this.nodeSize);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0002DF6C File Offset: 0x0002C36C
		public void CreateNodeRec(QuadtreeNodeHolder holder, int depth, int x, int y)
		{
			int num = 1 << Math.Min(this.editorHeightLog2, this.editorWidthLog2) - depth;
			int num2;
			if (depth < this.minDepth)
			{
				num2 = -1;
			}
			else
			{
				num2 = this.CheckNode(x, y, num);
			}
			if (num2 == 1 || num2 == 0 || num == 1)
			{
				QuadtreeNode quadtreeNode = new QuadtreeNode(this.active);
				quadtreeNode.SetPosition((Int3)this.LocalToWorldPosition(x, y, num));
				quadtreeNode.Walkable = (num2 == 1);
				holder.node = quadtreeNode;
			}
			else
			{
				holder.c0 = new QuadtreeNodeHolder();
				holder.c1 = new QuadtreeNodeHolder();
				holder.c2 = new QuadtreeNodeHolder();
				holder.c3 = new QuadtreeNodeHolder();
				this.CreateNodeRec(holder.c0, depth + 1, x, y);
				this.CreateNodeRec(holder.c1, depth + 1, x + num / 2, y);
				this.CreateNodeRec(holder.c2, depth + 1, x + num / 2, y + num / 2);
				this.CreateNodeRec(holder.c3, depth + 1, x, y + num / 2);
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0002E080 File Offset: 0x0002C480
		public void RecalculateConnections(QuadtreeNodeHolder holder, int depth, int x, int y)
		{
			if (this.root == null)
			{
				throw new InvalidOperationException("Graph contains no nodes");
			}
			if (holder.node == null)
			{
				throw new ArgumentException("No leaf node specified. Holder has no node.");
			}
			int num = 1 << Math.Min(this.editorHeightLog2, this.editorWidthLog2) - depth;
			List<QuadtreeNode> list = new List<QuadtreeNode>();
			List<QuadtreeNode> arr = list;
			QuadtreeNodeHolder holder2 = this.root;
			int depth2 = 0;
			int x2 = 0;
			int y2 = 0;
			IntRect intRect = new IntRect(x, y, x + num, y + num);
			this.AddNeighboursRec(arr, holder2, depth2, x2, y2, intRect.Expand(0), holder.node);
			holder.node.connections = list.ToArray();
			holder.node.connectionCosts = new uint[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				uint costMagnitude = (uint)(list[i].position - holder.node.position).costMagnitude;
				holder.node.connectionCosts[i] = costMagnitude;
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0002E17C File Offset: 0x0002C57C
		public void AddNeighboursRec(List<QuadtreeNode> arr, QuadtreeNodeHolder holder, int depth, int x, int y, IntRect bounds, QuadtreeNode dontInclude)
		{
			int num = 1 << Math.Min(this.editorHeightLog2, this.editorWidthLog2) - depth;
			IntRect a = new IntRect(x, y, x + num, y + num);
			if (!IntRect.Intersects(a, bounds))
			{
				return;
			}
			if (holder.node != null)
			{
				if (holder.node != dontInclude)
				{
					arr.Add(holder.node);
				}
			}
			else
			{
				this.AddNeighboursRec(arr, holder.c0, depth + 1, x, y, bounds, dontInclude);
				this.AddNeighboursRec(arr, holder.c1, depth + 1, x + num / 2, y, bounds, dontInclude);
				this.AddNeighboursRec(arr, holder.c2, depth + 1, x + num / 2, y + num / 2, bounds, dontInclude);
				this.AddNeighboursRec(arr, holder.c3, depth + 1, x, y + num / 2, bounds, dontInclude);
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0002E25C File Offset: 0x0002C65C
		public QuadtreeNode QueryPoint(int qx, int qy)
		{
			if (this.root == null)
			{
				return null;
			}
			QuadtreeNodeHolder quadtreeNodeHolder = this.root;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			while (quadtreeNodeHolder.node == null)
			{
				int num4 = 1 << Math.Min(this.editorHeightLog2, this.editorWidthLog2) - num;
				if (qx >= num2 + num4 / 2)
				{
					num2 += num4 / 2;
					if (qy >= num3 + num4 / 2)
					{
						num3 += num4 / 2;
						quadtreeNodeHolder = quadtreeNodeHolder.c2;
					}
					else
					{
						quadtreeNodeHolder = quadtreeNodeHolder.c1;
					}
				}
				else if (qy >= num3 + num4 / 2)
				{
					num3 += num4 / 2;
					quadtreeNodeHolder = quadtreeNodeHolder.c3;
				}
				else
				{
					quadtreeNodeHolder = quadtreeNodeHolder.c0;
				}
				num++;
			}
			return quadtreeNodeHolder.node;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0002E31B File Offset: 0x0002C71B
		public override void OnDrawGizmos(bool drawNodes)
		{
			base.OnDrawGizmos(drawNodes);
			if (!drawNodes)
			{
				return;
			}
			if (this.root != null)
			{
				this.DrawRec(this.root, 0, 0, 0, Vector3.zero);
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0002E34C File Offset: 0x0002C74C
		public void DrawRec(QuadtreeNodeHolder h, int depth, int x, int y, Vector3 parentPos)
		{
			int num = 1 << Math.Min(this.editorHeightLog2, this.editorWidthLog2) - depth;
			Vector3 vector = this.LocalToWorldPosition(x, y, num);
			Debug.DrawLine(vector, parentPos, Color.red);
			if (h.node != null)
			{
				Debug.DrawRay(vector, Vector3.down, (!h.node.Walkable) ? Color.yellow : Color.green);
			}
			else
			{
				this.DrawRec(h.c0, depth + 1, x, y, vector);
				this.DrawRec(h.c1, depth + 1, x + num / 2, y, vector);
				this.DrawRec(h.c2, depth + 1, x + num / 2, y + num / 2, vector);
				this.DrawRec(h.c3, depth + 1, x, y + num / 2, vector);
			}
		}

		// Token: 0x0400043E RID: 1086
		public int editorWidthLog2 = 6;

		// Token: 0x0400043F RID: 1087
		public int editorHeightLog2 = 6;

		// Token: 0x04000442 RID: 1090
		public LayerMask layerMask = -1;

		// Token: 0x04000443 RID: 1091
		public float nodeSize = 1f;

		// Token: 0x04000444 RID: 1092
		public int minDepth = 3;

		// Token: 0x04000445 RID: 1093
		private QuadtreeNodeHolder root;

		// Token: 0x04000446 RID: 1094
		public Vector3 center;

		// Token: 0x04000447 RID: 1095
		private BitArray map;
	}
}
