using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200009E RID: 158
	public class BBTree
	{
		// Token: 0x0600058E RID: 1422 RVA: 0x00032E38 File Offset: 0x00031238
		public BBTree(INavmeshHolder graph)
		{
			this.graph = graph;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00032E53 File Offset: 0x00031253
		public Rect Size
		{
			get
			{
				return (this.count == 0) ? new Rect(0f, 0f, 0f, 0f) : this.arr[0].rect;
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00032E8F File Offset: 0x0003128F
		public void Clear()
		{
			this.count = 0;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00032E98 File Offset: 0x00031298
		private void EnsureCapacity(int c)
		{
			if (this.arr.Length < c)
			{
				BBTree.BBTreeBox[] array = new BBTree.BBTreeBox[Math.Max(c, (int)((float)this.arr.Length * 1.5f))];
				for (int i = 0; i < this.count; i++)
				{
					array[i] = this.arr[i];
				}
				this.arr = array;
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00032F0C File Offset: 0x0003130C
		private int GetBox(MeshNode node)
		{
			if (this.count >= this.arr.Length)
			{
				this.EnsureCapacity(this.count + 1);
			}
			this.arr[this.count] = new BBTree.BBTreeBox(this, node);
			this.count++;
			return this.count - 1;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00032F70 File Offset: 0x00031370
		public void Insert(MeshNode node)
		{
			int box = this.GetBox(node);
			if (box == 0)
			{
				return;
			}
			BBTree.BBTreeBox bbtreeBox = this.arr[box];
			int num = 0;
			BBTree.BBTreeBox bbtreeBox2;
			for (;;)
			{
				bbtreeBox2 = this.arr[num];
				bbtreeBox2.rect = BBTree.ExpandToContain(bbtreeBox2.rect, bbtreeBox.rect);
				if (bbtreeBox2.node != null)
				{
					break;
				}
				this.arr[num] = bbtreeBox2;
				float num2 = BBTree.ExpansionRequired(this.arr[bbtreeBox2.left].rect, bbtreeBox.rect);
				float num3 = BBTree.ExpansionRequired(this.arr[bbtreeBox2.right].rect, bbtreeBox.rect);
				if (num2 < num3)
				{
					num = bbtreeBox2.left;
				}
				else if (num3 < num2)
				{
					num = bbtreeBox2.right;
				}
				else
				{
					num = ((BBTree.RectArea(this.arr[bbtreeBox2.left].rect) >= BBTree.RectArea(this.arr[bbtreeBox2.right].rect)) ? bbtreeBox2.right : bbtreeBox2.left);
				}
			}
			bbtreeBox2.left = box;
			int box2 = this.GetBox(bbtreeBox2.node);
			bbtreeBox2.right = box2;
			bbtreeBox2.node = null;
			this.arr[num] = bbtreeBox2;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000330F0 File Offset: 0x000314F0
		public NNInfo Query(Vector3 p, NNConstraint constraint)
		{
			if (this.count == 0)
			{
				return new NNInfo(null);
			}
			NNInfo result = default(NNInfo);
			this.SearchBox(0, p, constraint, ref result);
			result.UpdateInfo();
			return result;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0003312C File Offset: 0x0003152C
		public NNInfo QueryCircle(Vector3 p, float radius, NNConstraint constraint)
		{
			if (this.count == 0)
			{
				return new NNInfo(null);
			}
			NNInfo result = new NNInfo(null);
			this.SearchBoxCircle(0, p, radius, constraint, ref result);
			result.UpdateInfo();
			return result;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00033167 File Offset: 0x00031567
		public NNInfo QueryClosest(Vector3 p, NNConstraint constraint, out float distance)
		{
			distance = float.PositiveInfinity;
			return this.QueryClosest(p, constraint, ref distance, new NNInfo(null));
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0003317F File Offset: 0x0003157F
		public NNInfo QueryClosestXZ(Vector3 p, NNConstraint constraint, ref float distance, NNInfo previous)
		{
			if (this.count == 0)
			{
				return previous;
			}
			this.SearchBoxClosestXZ(0, p, ref distance, constraint, ref previous);
			return previous;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000331A0 File Offset: 0x000315A0
		private void SearchBoxClosestXZ(int boxi, Vector3 p, ref float closestDist, NNConstraint constraint, ref NNInfo nnInfo)
		{
			BBTree.BBTreeBox bbtreeBox = this.arr[boxi];
			if (bbtreeBox.node != null)
			{
				Vector3 constClampedPosition = bbtreeBox.node.ClosestPointOnNodeXZ(p);
				float num = (constClampedPosition.x - p.x) * (constClampedPosition.x - p.x) + (constClampedPosition.z - p.z) * (constClampedPosition.z - p.z);
				if (constraint == null || constraint.Suitable(bbtreeBox.node))
				{
					if (nnInfo.constrainedNode == null)
					{
						nnInfo.constrainedNode = bbtreeBox.node;
						nnInfo.constClampedPosition = constClampedPosition;
						closestDist = (float)Math.Sqrt((double)num);
					}
					else if (num < closestDist * closestDist)
					{
						nnInfo.constrainedNode = bbtreeBox.node;
						nnInfo.constClampedPosition = constClampedPosition;
						closestDist = (float)Math.Sqrt((double)num);
					}
				}
			}
			else
			{
				if (BBTree.RectIntersectsCircle(this.arr[bbtreeBox.left].rect, p, closestDist))
				{
					this.SearchBoxClosestXZ(bbtreeBox.left, p, ref closestDist, constraint, ref nnInfo);
				}
				if (BBTree.RectIntersectsCircle(this.arr[bbtreeBox.right].rect, p, closestDist))
				{
					this.SearchBoxClosestXZ(bbtreeBox.right, p, ref closestDist, constraint, ref nnInfo);
				}
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00033302 File Offset: 0x00031702
		public NNInfo QueryClosest(Vector3 p, NNConstraint constraint, ref float distance, NNInfo previous)
		{
			if (this.count == 0)
			{
				return previous;
			}
			this.SearchBoxClosest(0, p, ref distance, constraint, ref previous);
			return previous;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00033320 File Offset: 0x00031720
		private void SearchBoxClosest(int boxi, Vector3 p, ref float closestDist, NNConstraint constraint, ref NNInfo nnInfo)
		{
			BBTree.BBTreeBox bbtreeBox = this.arr[boxi];
			if (bbtreeBox.node != null)
			{
				if (BBTree.NodeIntersectsCircle(bbtreeBox.node, p, closestDist))
				{
					Vector3 vector = bbtreeBox.node.ClosestPointOnNode(p);
					float sqrMagnitude = (vector - p).sqrMagnitude;
					if (constraint == null || constraint.Suitable(bbtreeBox.node))
					{
						if (nnInfo.constrainedNode == null)
						{
							nnInfo.constrainedNode = bbtreeBox.node;
							nnInfo.constClampedPosition = vector;
							closestDist = (float)Math.Sqrt((double)sqrMagnitude);
						}
						else if (sqrMagnitude < closestDist * closestDist)
						{
							nnInfo.constrainedNode = bbtreeBox.node;
							nnInfo.constClampedPosition = vector;
							closestDist = (float)Math.Sqrt((double)sqrMagnitude);
						}
					}
				}
			}
			else
			{
				if (BBTree.RectIntersectsCircle(this.arr[bbtreeBox.left].rect, p, closestDist))
				{
					this.SearchBoxClosest(bbtreeBox.left, p, ref closestDist, constraint, ref nnInfo);
				}
				if (BBTree.RectIntersectsCircle(this.arr[bbtreeBox.right].rect, p, closestDist))
				{
					this.SearchBoxClosest(bbtreeBox.right, p, ref closestDist, constraint, ref nnInfo);
				}
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0003346B File Offset: 0x0003186B
		public MeshNode QueryInside(Vector3 p, NNConstraint constraint)
		{
			if (this.count == 0)
			{
				return null;
			}
			return this.SearchBoxInside(0, p, constraint);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00033484 File Offset: 0x00031884
		private MeshNode SearchBoxInside(int boxi, Vector3 p, NNConstraint constraint)
		{
			BBTree.BBTreeBox bbtreeBox = this.arr[boxi];
			if (bbtreeBox.node != null)
			{
				if (bbtreeBox.node.ContainsPoint((Int3)p))
				{
					if (constraint == null || constraint.Suitable(bbtreeBox.node))
					{
						return bbtreeBox.node;
					}
				}
			}
			else
			{
				if (this.arr[bbtreeBox.left].rect.Contains(new Vector2(p.x, p.z)))
				{
					MeshNode meshNode = this.SearchBoxInside(bbtreeBox.left, p, constraint);
					if (meshNode != null)
					{
						return meshNode;
					}
				}
				if (this.arr[bbtreeBox.right].rect.Contains(new Vector2(p.x, p.z)))
				{
					MeshNode meshNode = this.SearchBoxInside(bbtreeBox.right, p, constraint);
					if (meshNode != null)
					{
						return meshNode;
					}
				}
			}
			return null;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0003358C File Offset: 0x0003198C
		private void SearchBoxCircle(int boxi, Vector3 p, float radius, NNConstraint constraint, ref NNInfo nnInfo)
		{
			BBTree.BBTreeBox bbtreeBox = this.arr[boxi];
			if (bbtreeBox.node != null)
			{
				if (BBTree.NodeIntersectsCircle(bbtreeBox.node, p, radius))
				{
					Vector3 vector = bbtreeBox.node.ClosestPointOnNode(p);
					float sqrMagnitude = (vector - p).sqrMagnitude;
					if (nnInfo.node == null)
					{
						nnInfo.node = bbtreeBox.node;
						nnInfo.clampedPosition = vector;
					}
					else if (sqrMagnitude < (nnInfo.clampedPosition - p).sqrMagnitude)
					{
						nnInfo.node = bbtreeBox.node;
						nnInfo.clampedPosition = vector;
					}
					if (constraint == null || constraint.Suitable(bbtreeBox.node))
					{
						if (nnInfo.constrainedNode == null)
						{
							nnInfo.constrainedNode = bbtreeBox.node;
							nnInfo.constClampedPosition = vector;
						}
						else if (sqrMagnitude < (nnInfo.constClampedPosition - p).sqrMagnitude)
						{
							nnInfo.constrainedNode = bbtreeBox.node;
							nnInfo.constClampedPosition = vector;
						}
					}
				}
				return;
			}
			if (BBTree.RectIntersectsCircle(this.arr[bbtreeBox.left].rect, p, radius))
			{
				this.SearchBoxCircle(bbtreeBox.left, p, radius, constraint, ref nnInfo);
			}
			if (BBTree.RectIntersectsCircle(this.arr[bbtreeBox.right].rect, p, radius))
			{
				this.SearchBoxCircle(bbtreeBox.right, p, radius, constraint, ref nnInfo);
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00033728 File Offset: 0x00031B28
		private void SearchBox(int boxi, Vector3 p, NNConstraint constraint, ref NNInfo nnInfo)
		{
			BBTree.BBTreeBox bbtreeBox = this.arr[boxi];
			if (bbtreeBox.node != null)
			{
				if (bbtreeBox.node.ContainsPoint((Int3)p))
				{
					if (nnInfo.node == null)
					{
						nnInfo.node = bbtreeBox.node;
					}
					else if (Mathf.Abs(((Vector3)bbtreeBox.node.position).y - p.y) < Mathf.Abs(((Vector3)nnInfo.node.position).y - p.y))
					{
						nnInfo.node = bbtreeBox.node;
					}
					if (constraint.Suitable(bbtreeBox.node))
					{
						if (nnInfo.constrainedNode == null)
						{
							nnInfo.constrainedNode = bbtreeBox.node;
						}
						else if (Mathf.Abs((float)bbtreeBox.node.position.y - p.y) < Mathf.Abs((float)nnInfo.constrainedNode.position.y - p.y))
						{
							nnInfo.constrainedNode = bbtreeBox.node;
						}
					}
				}
				return;
			}
			if (BBTree.RectContains(this.arr[bbtreeBox.left].rect, p))
			{
				this.SearchBox(bbtreeBox.left, p, constraint, ref nnInfo);
			}
			if (BBTree.RectContains(this.arr[bbtreeBox.right].rect, p))
			{
				this.SearchBox(bbtreeBox.right, p, constraint, ref nnInfo);
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000338D1 File Offset: 0x00031CD1
		public void OnDrawGizmos()
		{
			Gizmos.color = new Color(1f, 1f, 1f, 0.5f);
			if (this.count == 0)
			{
				return;
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00033900 File Offset: 0x00031D00
		private void OnDrawGizmos(int boxi, int depth)
		{
			BBTree.BBTreeBox bbtreeBox = this.arr[boxi];
			Vector3 a = new Vector3(bbtreeBox.rect.xMin, 0f, bbtreeBox.rect.yMin);
			Vector3 vector = new Vector3(bbtreeBox.rect.xMax, 0f, bbtreeBox.rect.yMax);
			Vector3 vector2 = (a + vector) * 0.5f;
			Vector3 size = (vector - vector2) * 2f;
			vector2.y += (float)depth * 0.2f;
			Gizmos.color = AstarMath.IntToColor(depth, 0.05f);
			Gizmos.DrawCube(vector2, size);
			if (bbtreeBox.node == null)
			{
				this.OnDrawGizmos(bbtreeBox.left, depth + 1);
				this.OnDrawGizmos(bbtreeBox.right, depth + 1);
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x000339EC File Offset: 0x00031DEC
		private static bool NodeIntersectsCircle(MeshNode node, Vector3 p, float radius)
		{
			return float.IsPositiveInfinity(radius) || (p - node.ClosestPointOnNode(p)).sqrMagnitude < radius * radius;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00033A20 File Offset: 0x00031E20
		private static bool RectIntersectsCircle(Rect r, Vector3 p, float radius)
		{
			if (float.IsPositiveInfinity(radius))
			{
				return true;
			}
			Vector3 vector = p;
			p.x = Math.Max(p.x, r.xMin);
			p.x = Math.Min(p.x, r.xMax);
			p.z = Math.Max(p.z, r.yMin);
			p.z = Math.Min(p.z, r.yMax);
			return (p.x - vector.x) * (p.x - vector.x) + (p.z - vector.z) * (p.z - vector.z) < radius * radius;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00033AE8 File Offset: 0x00031EE8
		private static bool RectContains(Rect r, Vector3 p)
		{
			return p.x >= r.xMin && p.x <= r.xMax && p.z >= r.yMin && p.z <= r.yMax;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00033B44 File Offset: 0x00031F44
		private static float ExpansionRequired(Rect r, Rect r2)
		{
			float num = Math.Min(r.xMin, r2.xMin);
			float num2 = Math.Max(r.xMax, r2.xMax);
			float num3 = Math.Min(r.yMin, r2.yMin);
			float num4 = Math.Max(r.yMax, r2.yMax);
			return (num2 - num) * (num4 - num3) - BBTree.RectArea(r);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00033BB0 File Offset: 0x00031FB0
		private static Rect ExpandToContain(Rect r, Rect r2)
		{
			float xmin = Math.Min(r.xMin, r2.xMin);
			float xmax = Math.Max(r.xMax, r2.xMax);
			float ymin = Math.Min(r.yMin, r2.yMin);
			float ymax = Math.Max(r.yMax, r2.yMax);
			return Rect.MinMaxRect(xmin, ymin, xmax, ymax);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00033C16 File Offset: 0x00032016
		private static float RectArea(Rect r)
		{
			return r.width * r.height;
		}

		// Token: 0x04000493 RID: 1171
		private BBTree.BBTreeBox[] arr = new BBTree.BBTreeBox[6];

		// Token: 0x04000494 RID: 1172
		private int count;

		// Token: 0x04000495 RID: 1173
		public INavmeshHolder graph;

		// Token: 0x0200009F RID: 159
		private struct BBTreeBox
		{
			// Token: 0x060005A7 RID: 1447 RVA: 0x00033C28 File Offset: 0x00032028
			public BBTreeBox(BBTree tree, MeshNode node)
			{
				this.node = node;
				Vector3 vector = (Vector3)node.GetVertex(0);
				Vector2 vector2 = new Vector2(vector.x, vector.z);
				Vector2 vector3 = vector2;
				for (int i = 1; i < node.GetVertexCount(); i++)
				{
					Vector3 vector4 = (Vector3)node.GetVertex(i);
					vector2.x = Math.Min(vector2.x, vector4.x);
					vector2.y = Math.Min(vector2.y, vector4.z);
					vector3.x = Math.Max(vector3.x, vector4.x);
					vector3.y = Math.Max(vector3.y, vector4.z);
				}
				this.rect = Rect.MinMaxRect(vector2.x, vector2.y, vector3.x, vector3.y);
				this.left = (this.right = -1);
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00033D26 File Offset: 0x00032126
			public bool IsLeaf
			{
				get
				{
					return this.node != null;
				}
			}

			// Token: 0x060005A9 RID: 1449 RVA: 0x00033D34 File Offset: 0x00032134
			public bool Contains(Vector3 p)
			{
				return this.rect.Contains(new Vector2(p.x, p.z));
			}

			// Token: 0x04000496 RID: 1174
			public Rect rect;

			// Token: 0x04000497 RID: 1175
			public MeshNode node;

			// Token: 0x04000498 RID: 1176
			public int left;

			// Token: 0x04000499 RID: 1177
			public int right;
		}
	}
}
