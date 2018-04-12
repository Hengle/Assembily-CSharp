using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020000B3 RID: 179
	public struct CompactVoxelCell
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x000399B1 File Offset: 0x00037DB1
		public CompactVoxelCell(uint i, uint c)
		{
			this.index = i;
			this.count = c;
		}

		// Token: 0x04000507 RID: 1287
		public uint index;

		// Token: 0x04000508 RID: 1288
		public uint count;
	}
}
