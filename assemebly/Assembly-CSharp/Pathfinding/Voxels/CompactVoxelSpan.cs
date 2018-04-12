using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020000B4 RID: 180
	public struct CompactVoxelSpan
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x000399C1 File Offset: 0x00037DC1
		public CompactVoxelSpan(ushort bottom, uint height)
		{
			this.con = 24u;
			this.y = bottom;
			this.h = height;
			this.reg = 0;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x000399E0 File Offset: 0x00037DE0
		public void SetConnection(int dir, uint value)
		{
			int num = dir * 6;
			this.con = (uint)(((ulong)this.con & (ulong)(~(63L << (num & 31)))) | (ulong)((ulong)(value & 63u) << num));
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00039A14 File Offset: 0x00037E14
		public int GetConnection(int dir)
		{
			return (int)this.con >> dir * 6 & 63;
		}

		// Token: 0x04000509 RID: 1289
		public ushort y;

		// Token: 0x0400050A RID: 1290
		public uint con;

		// Token: 0x0400050B RID: 1291
		public uint h;

		// Token: 0x0400050C RID: 1292
		public int reg;
	}
}
