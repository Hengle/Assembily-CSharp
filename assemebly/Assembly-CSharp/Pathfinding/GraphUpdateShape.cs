using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000016 RID: 22
	public class GraphUpdateShape
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000DE1D File Offset: 0x0000C21D
		// (set) Token: 0x0600013D RID: 317 RVA: 0x0000DE25 File Offset: 0x0000C225
		public Vector3[] points
		{
			get
			{
				return this._points;
			}
			set
			{
				this._points = value;
				if (this.convex)
				{
					this.CalculateConvexHull();
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000DE3F File Offset: 0x0000C23F
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000DE47 File Offset: 0x0000C247
		public bool convex
		{
			get
			{
				return this._convex;
			}
			set
			{
				if (this._convex != value && value)
				{
					this._convex = value;
					this.CalculateConvexHull();
				}
				else
				{
					this._convex = value;
				}
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000DE74 File Offset: 0x0000C274
		private void CalculateConvexHull()
		{
			if (this.points == null)
			{
				this._convexPoints = null;
				return;
			}
			this._convexPoints = Polygon.ConvexHull(this.points);
			for (int i = 0; i < this._convexPoints.Length; i++)
			{
				Debug.DrawLine(this._convexPoints[i], this._convexPoints[(i + 1) % this._convexPoints.Length], Color.green);
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000DEF8 File Offset: 0x0000C2F8
		public Bounds GetBounds()
		{
			if (this.points == null || this.points.Length == 0)
			{
				return default(Bounds);
			}
			Vector3 vector = this.points[0];
			Vector3 vector2 = this.points[0];
			for (int i = 0; i < this.points.Length; i++)
			{
				vector = Vector3.Min(vector, this.points[i]);
				vector2 = Vector3.Max(vector2, this.points[i]);
			}
			return new Bounds((vector + vector2) * 0.5f, vector2 - vector);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000DFB4 File Offset: 0x0000C3B4
		public bool Contains(GraphNode node)
		{
			Vector3 p = (Vector3)node.position;
			if (!this.convex)
			{
				return this._points != null && Polygon.ContainsPoint(this._points, p);
			}
			if (this._convexPoints == null)
			{
				return false;
			}
			int i = 0;
			int num = this._convexPoints.Length - 1;
			while (i < this._convexPoints.Length)
			{
				if (Polygon.Left(this._convexPoints[i], this._convexPoints[num], p))
				{
					return false;
				}
				num = i;
				i++;
			}
			return true;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000E05C File Offset: 0x0000C45C
		public bool Contains(Vector3 point)
		{
			if (!this.convex)
			{
				return this._points != null && Polygon.ContainsPoint(this._points, point);
			}
			if (this._convexPoints == null)
			{
				return false;
			}
			int i = 0;
			int num = this._convexPoints.Length - 1;
			while (i < this._convexPoints.Length)
			{
				if (Polygon.Left(this._convexPoints[i], this._convexPoints[num], point))
				{
					return false;
				}
				num = i;
				i++;
			}
			return true;
		}

		// Token: 0x040000FB RID: 251
		private Vector3[] _points;

		// Token: 0x040000FC RID: 252
		private Vector3[] _convexPoints;

		// Token: 0x040000FD RID: 253
		private bool _convex;
	}
}
