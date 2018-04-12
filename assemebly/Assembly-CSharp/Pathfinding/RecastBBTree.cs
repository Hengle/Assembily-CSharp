using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000A5 RID: 165
	public class RecastBBTree
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x0003513C File Offset: 0x0003353C
		public void QueryInBounds(Rect bounds, List<RecastMeshObj> buffer)
		{
			RecastBBTreeBox recastBBTreeBox = this.root;
			if (recastBBTreeBox == null)
			{
				return;
			}
			this.QueryBoxInBounds(recastBBTreeBox, bounds, buffer);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00035160 File Offset: 0x00033560
		public void QueryBoxInBounds(RecastBBTreeBox box, Rect bounds, List<RecastMeshObj> boxes)
		{
			if (box.mesh != null)
			{
				if (this.RectIntersectsRect(box.rect, bounds))
				{
					boxes.Add(box.mesh);
				}
			}
			else
			{
				if (this.RectIntersectsRect(box.c1.rect, bounds))
				{
					this.QueryBoxInBounds(box.c1, bounds, boxes);
				}
				if (this.RectIntersectsRect(box.c2.rect, bounds))
				{
					this.QueryBoxInBounds(box.c2, bounds, boxes);
				}
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000351F0 File Offset: 0x000335F0
		public bool Remove(RecastMeshObj mesh)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (this.root == null)
			{
				return false;
			}
			bool result = false;
			Bounds bounds = mesh.GetBounds();
			Rect bounds2 = Rect.MinMaxRect(bounds.min.x, bounds.min.z, bounds.max.x, bounds.max.z);
			this.root = this.RemoveBox(this.root, mesh, bounds2, ref result);
			return result;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00035288 File Offset: 0x00033688
		private RecastBBTreeBox RemoveBox(RecastBBTreeBox c, RecastMeshObj mesh, Rect bounds, ref bool found)
		{
			if (!this.RectIntersectsRect(c.rect, bounds))
			{
				return c;
			}
			if (c.mesh == mesh)
			{
				found = true;
				return null;
			}
			if (c.mesh == null && !found)
			{
				c.c1 = this.RemoveBox(c.c1, mesh, bounds, ref found);
				if (c.c1 == null)
				{
					return c.c2;
				}
				if (!found)
				{
					c.c2 = this.RemoveBox(c.c2, mesh, bounds, ref found);
					if (c.c2 == null)
					{
						return c.c1;
					}
				}
				if (found)
				{
					c.rect = this.ExpandToContain(c.c1.rect, c.c2.rect);
				}
			}
			return c;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0003535C File Offset: 0x0003375C
		public void Insert(RecastMeshObj mesh)
		{
			RecastBBTreeBox recastBBTreeBox = new RecastBBTreeBox(this, mesh);
			if (this.root == null)
			{
				this.root = recastBBTreeBox;
				return;
			}
			RecastBBTreeBox recastBBTreeBox2 = this.root;
			for (;;)
			{
				recastBBTreeBox2.rect = this.ExpandToContain(recastBBTreeBox2.rect, recastBBTreeBox.rect);
				if (recastBBTreeBox2.mesh != null)
				{
					break;
				}
				float num = this.ExpansionRequired(recastBBTreeBox2.c1.rect, recastBBTreeBox.rect);
				float num2 = this.ExpansionRequired(recastBBTreeBox2.c2.rect, recastBBTreeBox.rect);
				if (num < num2)
				{
					recastBBTreeBox2 = recastBBTreeBox2.c1;
				}
				else if (num2 < num)
				{
					recastBBTreeBox2 = recastBBTreeBox2.c2;
				}
				else
				{
					recastBBTreeBox2 = ((this.RectArea(recastBBTreeBox2.c1.rect) >= this.RectArea(recastBBTreeBox2.c2.rect)) ? recastBBTreeBox2.c2 : recastBBTreeBox2.c1);
				}
			}
			recastBBTreeBox2.c1 = recastBBTreeBox;
			RecastBBTreeBox c = new RecastBBTreeBox(this, recastBBTreeBox2.mesh);
			recastBBTreeBox2.c2 = c;
			recastBBTreeBox2.mesh = null;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0003546D File Offset: 0x0003386D
		public void OnDrawGizmos()
		{
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00035470 File Offset: 0x00033870
		public void OnDrawGizmos(RecastBBTreeBox box)
		{
			if (box == null)
			{
				return;
			}
			Vector3 a = new Vector3(box.rect.xMin, 0f, box.rect.yMin);
			Vector3 vector = new Vector3(box.rect.xMax, 0f, box.rect.yMax);
			Vector3 vector2 = (a + vector) * 0.5f;
			Vector3 size = (vector - vector2) * 2f;
			Gizmos.DrawCube(vector2, size);
			this.OnDrawGizmos(box.c1);
			this.OnDrawGizmos(box.c2);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0003550C File Offset: 0x0003390C
		public void TestIntersections(Vector3 p, float radius)
		{
			RecastBBTreeBox box = this.root;
			this.TestIntersections(box, p, radius);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00035529 File Offset: 0x00033929
		public void TestIntersections(RecastBBTreeBox box, Vector3 p, float radius)
		{
			if (box == null)
			{
				return;
			}
			this.RectIntersectsCircle(box.rect, p, radius);
			this.TestIntersections(box.c1, p, radius);
			this.TestIntersections(box.c2, p, radius);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00035560 File Offset: 0x00033960
		public bool RectIntersectsRect(Rect r, Rect r2)
		{
			return r.xMax > r2.xMin && r.yMax > r2.yMin && r2.xMax > r.xMin && r2.yMax > r.yMin;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000355BC File Offset: 0x000339BC
		public bool RectIntersectsCircle(Rect r, Vector3 p, float radius)
		{
			return float.IsPositiveInfinity(radius) || this.RectContains(r, p) || this.XIntersectsCircle(r.xMin, r.xMax, r.yMin, p, radius) || this.XIntersectsCircle(r.xMin, r.xMax, r.yMax, p, radius) || this.ZIntersectsCircle(r.yMin, r.yMax, r.xMin, p, radius) || this.ZIntersectsCircle(r.yMin, r.yMax, r.xMax, p, radius);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0003566C File Offset: 0x00033A6C
		public bool RectContains(Rect r, Vector3 p)
		{
			return p.x >= r.xMin && p.x <= r.xMax && p.z >= r.yMin && p.z <= r.yMax;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000356C8 File Offset: 0x00033AC8
		public bool ZIntersectsCircle(float z1, float z2, float xpos, Vector3 circle, float radius)
		{
			double num = (double)(Math.Abs(xpos - circle.x) / radius);
			if (num > 1.0 || num < -1.0)
			{
				return false;
			}
			float num2 = (float)Math.Sqrt(1.0 - num * num) * radius;
			float val = circle.z - num2;
			num2 += circle.z;
			float num3 = Math.Min(num2, val);
			float num4 = Math.Max(num2, val);
			num3 = Mathf.Max(z1, num3);
			num4 = Mathf.Min(z2, num4);
			return num4 > num3;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0003575C File Offset: 0x00033B5C
		public bool XIntersectsCircle(float x1, float x2, float zpos, Vector3 circle, float radius)
		{
			double num = (double)(Math.Abs(zpos - circle.z) / radius);
			if (num > 1.0 || num < -1.0)
			{
				return false;
			}
			float num2 = (float)Math.Sqrt(1.0 - num * num) * radius;
			float val = circle.x - num2;
			num2 += circle.x;
			float num3 = Math.Min(num2, val);
			float num4 = Math.Max(num2, val);
			num3 = Mathf.Max(x1, num3);
			num4 = Mathf.Min(x2, num4);
			return num4 > num3;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000357F0 File Offset: 0x00033BF0
		public float ExpansionRequired(Rect r, Rect r2)
		{
			float num = Mathf.Min(r.xMin, r2.xMin);
			float num2 = Mathf.Max(r.xMax, r2.xMax);
			float num3 = Mathf.Min(r.yMin, r2.yMin);
			float num4 = Mathf.Max(r.yMax, r2.yMax);
			return (num2 - num) * (num4 - num3) - this.RectArea(r);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0003585C File Offset: 0x00033C5C
		public Rect ExpandToContain(Rect r, Rect r2)
		{
			float xmin = Mathf.Min(r.xMin, r2.xMin);
			float xmax = Mathf.Max(r.xMax, r2.xMax);
			float ymin = Mathf.Min(r.yMin, r2.yMin);
			float ymax = Mathf.Max(r.yMax, r2.yMax);
			return Rect.MinMaxRect(xmin, ymin, xmax, ymax);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000358C2 File Offset: 0x00033CC2
		public float RectArea(Rect r)
		{
			return r.width * r.height;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000358D4 File Offset: 0x00033CD4
		public new void ToString()
		{
			RecastBBTreeBox recastBBTreeBox = this.root;
			Stack<RecastBBTreeBox> stack = new Stack<RecastBBTreeBox>();
			stack.Push(recastBBTreeBox);
			recastBBTreeBox.WriteChildren(0);
		}

		// Token: 0x040004B4 RID: 1204
		public RecastBBTreeBox root;
	}
}
