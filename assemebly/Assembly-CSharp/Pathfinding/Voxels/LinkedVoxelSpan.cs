using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020000AD RID: 173
	public struct LinkedVoxelSpan
	{
		// Token: 0x060005FF RID: 1535 RVA: 0x00039770 File Offset: 0x00037B70
		public LinkedVoxelSpan(uint bottom, uint top, int area)
		{
			this.bottom = bottom;
			this.top = top;
			this.area = area;
			this.next = -1;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0003978E File Offset: 0x00037B8E
		public LinkedVoxelSpan(uint bottom, uint top, int area, int next)
		{
			this.bottom = bottom;
			this.top = top;
			this.area = area;
			this.next = next;
		}

		// Token: 0x040004F3 RID: 1267
		public uint bottom;

		// Token: 0x040004F4 RID: 1268
		public uint top;

		// Token: 0x040004F5 RID: 1269
		public int next;

		// Token: 0x040004F6 RID: 1270
		public int area;
	}
}
