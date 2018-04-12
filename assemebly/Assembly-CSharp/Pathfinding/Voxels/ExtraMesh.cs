using System;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020000AE RID: 174
	public struct ExtraMesh
	{
		// Token: 0x06000601 RID: 1537 RVA: 0x000397AD File Offset: 0x00037BAD
		public ExtraMesh(Vector3[] v, int[] t, Bounds b)
		{
			this.matrix = Matrix4x4.identity;
			this.vertices = v;
			this.triangles = t;
			this.bounds = b;
			this.original = null;
			this.area = 0;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000397DD File Offset: 0x00037BDD
		public ExtraMesh(Vector3[] v, int[] t, Bounds b, Matrix4x4 matrix)
		{
			this.matrix = matrix;
			this.vertices = v;
			this.triangles = t;
			this.bounds = b;
			this.original = null;
			this.area = 0;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0003980C File Offset: 0x00037C0C
		public void RecalculateBounds()
		{
			Bounds bounds = new Bounds(this.matrix.MultiplyPoint3x4(this.vertices[0]), Vector3.zero);
			for (int i = 1; i < this.vertices.Length; i++)
			{
				bounds.Encapsulate(this.matrix.MultiplyPoint3x4(this.vertices[i]));
			}
			this.bounds = bounds;
		}

		// Token: 0x040004F7 RID: 1271
		public MeshFilter original;

		// Token: 0x040004F8 RID: 1272
		public int area;

		// Token: 0x040004F9 RID: 1273
		public Vector3[] vertices;

		// Token: 0x040004FA RID: 1274
		public int[] triangles;

		// Token: 0x040004FB RID: 1275
		public Bounds bounds;

		// Token: 0x040004FC RID: 1276
		public Matrix4x4 matrix;
	}
}
