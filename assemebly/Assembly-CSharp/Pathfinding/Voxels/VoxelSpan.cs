using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020000B5 RID: 181
	public class VoxelSpan
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x00039A26 File Offset: 0x00037E26
		public VoxelSpan(uint b, uint t, int area)
		{
			this.bottom = b;
			this.top = t;
			this.area = area;
		}

		// Token: 0x0400050D RID: 1293
		public uint bottom;

		// Token: 0x0400050E RID: 1294
		public uint top;

		// Token: 0x0400050F RID: 1295
		public VoxelSpan next;

		// Token: 0x04000510 RID: 1296
		public int area;
	}
}
