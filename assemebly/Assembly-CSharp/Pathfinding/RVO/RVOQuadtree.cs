using System;
using Pathfinding.RVO.Sampled;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x02000042 RID: 66
	public class RVOQuadtree
	{
		// Token: 0x06000305 RID: 773 RVA: 0x00017F88 File Offset: 0x00016388
		public void Clear()
		{
			this.nodes[0] = default(RVOQuadtree.Node);
			this.filledNodes = 1;
			this.maxRadius = 0f;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00017FC1 File Offset: 0x000163C1
		public void SetBounds(Rect r)
		{
			this.bounds = r;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00017FCC File Offset: 0x000163CC
		public int GetNodeIndex()
		{
			if (this.filledNodes == this.nodes.Length)
			{
				RVOQuadtree.Node[] array = new RVOQuadtree.Node[this.nodes.Length * 2];
				for (int i = 0; i < this.nodes.Length; i++)
				{
					array[i] = this.nodes[i];
				}
				this.nodes = array;
			}
			this.nodes[this.filledNodes] = default(RVOQuadtree.Node);
			this.nodes[this.filledNodes].child00 = this.filledNodes;
			this.filledNodes++;
			return this.filledNodes - 1;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0001808C File Offset: 0x0001648C
		public void Insert(Agent agent)
		{
			int num = 0;
			Rect r = this.bounds;
			Vector2 vector = new Vector2(agent.position.x, agent.position.z);
			agent.next = null;
			this.maxRadius = Math.Max(agent.radius, this.maxRadius);
			int num2 = 0;
			for (;;)
			{
				num2++;
				if (this.nodes[num].child00 == num)
				{
					if (this.nodes[num].count < 15 || num2 > 10)
					{
						break;
					}
					RVOQuadtree.Node node = this.nodes[num];
					node.child00 = this.GetNodeIndex();
					node.child01 = this.GetNodeIndex();
					node.child10 = this.GetNodeIndex();
					node.child11 = this.GetNodeIndex();
					this.nodes[num] = node;
					this.nodes[num].Distribute(this.nodes, r);
				}
				if (this.nodes[num].child00 != num)
				{
					Vector2 center = r.center;
					if (vector.x > center.x)
					{
						if (vector.y > center.y)
						{
							num = this.nodes[num].child11;
							r = Rect.MinMaxRect(center.x, center.y, r.xMax, r.yMax);
						}
						else
						{
							num = this.nodes[num].child10;
							r = Rect.MinMaxRect(center.x, r.yMin, r.xMax, center.y);
						}
					}
					else if (vector.y > center.y)
					{
						num = this.nodes[num].child01;
						r = Rect.MinMaxRect(r.xMin, center.y, center.x, r.yMax);
					}
					else
					{
						num = this.nodes[num].child00;
						r = Rect.MinMaxRect(r.xMin, r.yMin, center.x, center.y);
					}
				}
			}
			this.nodes[num].Add(agent);
			RVOQuadtree.Node[] array = this.nodes;
			int num3 = num;
			array[num3].count = array[num3].count + 1;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000182FA File Offset: 0x000166FA
		public void Query(Vector2 p, float radius, Agent agent)
		{
			this.QueryRec(0, p, radius, agent, this.bounds);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00018310 File Offset: 0x00016710
		private float QueryRec(int i, Vector2 p, float radius, Agent agent, Rect r)
		{
			if (this.nodes[i].child00 == i)
			{
				for (Agent agent2 = this.nodes[i].linkedList; agent2 != null; agent2 = agent2.next)
				{
					float num = agent.InsertAgentNeighbour(agent2, radius * radius);
					if (num < radius * radius)
					{
						radius = Mathf.Sqrt(num);
					}
				}
			}
			else
			{
				Vector2 center = r.center;
				if (p.x - radius < center.x)
				{
					if (p.y - radius < center.y)
					{
						radius = this.QueryRec(this.nodes[i].child00, p, radius, agent, Rect.MinMaxRect(r.xMin, r.yMin, center.x, center.y));
					}
					if (p.y + radius > center.y)
					{
						radius = this.QueryRec(this.nodes[i].child01, p, radius, agent, Rect.MinMaxRect(r.xMin, center.y, center.x, r.yMax));
					}
				}
				if (p.x + radius > center.x)
				{
					if (p.y - radius < center.y)
					{
						radius = this.QueryRec(this.nodes[i].child10, p, radius, agent, Rect.MinMaxRect(center.x, r.yMin, r.xMax, center.y));
					}
					if (p.y + radius > center.y)
					{
						radius = this.QueryRec(this.nodes[i].child11, p, radius, agent, Rect.MinMaxRect(center.x, center.y, r.xMax, r.yMax));
					}
				}
			}
			return radius;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000184F9 File Offset: 0x000168F9
		public void DebugDraw()
		{
			this.DebugDrawRec(0, this.bounds);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00018508 File Offset: 0x00016908
		private void DebugDrawRec(int i, Rect r)
		{
			Debug.DrawLine(new Vector3(r.xMin, 0f, r.yMin), new Vector3(r.xMax, 0f, r.yMin), Color.white);
			Debug.DrawLine(new Vector3(r.xMax, 0f, r.yMin), new Vector3(r.xMax, 0f, r.yMax), Color.white);
			Debug.DrawLine(new Vector3(r.xMax, 0f, r.yMax), new Vector3(r.xMin, 0f, r.yMax), Color.white);
			Debug.DrawLine(new Vector3(r.xMin, 0f, r.yMax), new Vector3(r.xMin, 0f, r.yMin), Color.white);
			if (this.nodes[i].child00 != i)
			{
				Vector2 center = r.center;
				this.DebugDrawRec(this.nodes[i].child11, Rect.MinMaxRect(center.x, center.y, r.xMax, r.yMax));
				this.DebugDrawRec(this.nodes[i].child10, Rect.MinMaxRect(center.x, r.yMin, r.xMax, center.y));
				this.DebugDrawRec(this.nodes[i].child01, Rect.MinMaxRect(r.xMin, center.y, center.x, r.yMax));
				this.DebugDrawRec(this.nodes[i].child00, Rect.MinMaxRect(r.xMin, r.yMin, center.x, center.y));
			}
			for (Agent agent = this.nodes[i].linkedList; agent != null; agent = agent.next)
			{
				Debug.DrawLine(this.nodes[i].linkedList.position + Vector3.up, agent.position + Vector3.up, new Color(1f, 1f, 0f, 0.5f));
			}
		}

		// Token: 0x04000258 RID: 600
		private const int LeafSize = 15;

		// Token: 0x04000259 RID: 601
		private float maxRadius;

		// Token: 0x0400025A RID: 602
		private RVOQuadtree.Node[] nodes = new RVOQuadtree.Node[42];

		// Token: 0x0400025B RID: 603
		private int filledNodes = 1;

		// Token: 0x0400025C RID: 604
		private Rect bounds;

		// Token: 0x02000043 RID: 67
		private struct Node
		{
			// Token: 0x0600030D RID: 781 RVA: 0x0001876E File Offset: 0x00016B6E
			public void Add(Agent agent)
			{
				agent.next = this.linkedList;
				this.linkedList = agent;
			}

			// Token: 0x0600030E RID: 782 RVA: 0x00018784 File Offset: 0x00016B84
			public void Distribute(RVOQuadtree.Node[] nodes, Rect r)
			{
				Vector2 center = r.center;
				while (this.linkedList != null)
				{
					Agent next = this.linkedList.next;
					if (this.linkedList.position.x > center.x)
					{
						if (this.linkedList.position.z > center.y)
						{
							nodes[this.child11].Add(this.linkedList);
						}
						else
						{
							nodes[this.child10].Add(this.linkedList);
						}
					}
					else if (this.linkedList.position.z > center.y)
					{
						nodes[this.child01].Add(this.linkedList);
					}
					else
					{
						nodes[this.child00].Add(this.linkedList);
					}
					this.linkedList = next;
				}
				this.count = 0;
			}

			// Token: 0x0400025D RID: 605
			public int child00;

			// Token: 0x0400025E RID: 606
			public int child01;

			// Token: 0x0400025F RID: 607
			public int child10;

			// Token: 0x04000260 RID: 608
			public int child11;

			// Token: 0x04000261 RID: 609
			public byte count;

			// Token: 0x04000262 RID: 610
			public Agent linkedList;
		}
	}
}
