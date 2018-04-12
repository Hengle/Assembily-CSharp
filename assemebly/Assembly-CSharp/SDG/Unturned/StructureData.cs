using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005BE RID: 1470
	public class StructureData
	{
		// Token: 0x0600291C RID: 10524 RVA: 0x000FB3A0 File Offset: 0x000F97A0
		public StructureData(Structure newStructure, Vector3 newPoint, byte newAngle_X, byte newAngle_Y, byte newAngle_Z, ulong newOwner, ulong newGroup, uint newObjActiveDate)
		{
			this._structure = newStructure;
			this.point = newPoint;
			this.angle_x = newAngle_X;
			this.angle_y = newAngle_Y;
			this.angle_z = newAngle_Z;
			this._owner = newOwner;
			this._group = newGroup;
			this.objActiveDate = newObjActiveDate;
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x0600291D RID: 10525 RVA: 0x000FB3F0 File Offset: 0x000F97F0
		public Structure structure
		{
			get
			{
				return this._structure;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600291E RID: 10526 RVA: 0x000FB3F8 File Offset: 0x000F97F8
		public ulong owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x0600291F RID: 10527 RVA: 0x000FB400 File Offset: 0x000F9800
		public ulong group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x040019A0 RID: 6560
		private Structure _structure;

		// Token: 0x040019A1 RID: 6561
		public Vector3 point;

		// Token: 0x040019A2 RID: 6562
		public byte angle_x;

		// Token: 0x040019A3 RID: 6563
		public byte angle_y;

		// Token: 0x040019A4 RID: 6564
		public byte angle_z;

		// Token: 0x040019A5 RID: 6565
		private ulong _owner;

		// Token: 0x040019A6 RID: 6566
		private ulong _group;

		// Token: 0x040019A7 RID: 6567
		public uint objActiveDate;
	}
}
