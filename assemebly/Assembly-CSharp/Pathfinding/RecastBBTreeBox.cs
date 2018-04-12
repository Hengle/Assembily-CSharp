using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000A6 RID: 166
	public class RecastBBTreeBox
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x000358FC File Offset: 0x00033CFC
		public RecastBBTreeBox(RecastBBTree tree, RecastMeshObj mesh)
		{
			this.mesh = mesh;
			Vector3 min = mesh.bounds.min;
			Vector3 max = mesh.bounds.max;
			this.rect = Rect.MinMaxRect(min.x, min.z, max.x, max.z);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00035955 File Offset: 0x00033D55
		public bool Contains(Vector3 p)
		{
			return this.rect.Contains(p);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00035964 File Offset: 0x00033D64
		public void WriteChildren(int level)
		{
			for (int i = 0; i < level; i++)
			{
				Console.Write("  ");
			}
			if (this.mesh != null)
			{
				Console.WriteLine("Leaf ");
			}
			else
			{
				Console.WriteLine("Box ");
				this.c1.WriteChildren(level + 1);
				this.c2.WriteChildren(level + 1);
			}
		}

		// Token: 0x040004B5 RID: 1205
		public Rect rect;

		// Token: 0x040004B6 RID: 1206
		public RecastMeshObj mesh;

		// Token: 0x040004B7 RID: 1207
		public RecastBBTreeBox c1;

		// Token: 0x040004B8 RID: 1208
		public RecastBBTreeBox c2;
	}
}
