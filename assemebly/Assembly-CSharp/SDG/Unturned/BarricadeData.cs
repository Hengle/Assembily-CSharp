using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000586 RID: 1414
	public class BarricadeData
	{
		// Token: 0x06002710 RID: 10000 RVA: 0x000E8A24 File Offset: 0x000E6E24
		public BarricadeData(Barricade newBarricade, Vector3 newPoint, byte newAngle_X, byte newAngle_Y, byte newAngle_Z, ulong newOwner, ulong newGroup, uint newObjActiveDate)
		{
			this._barricade = newBarricade;
			this.point = newPoint;
			this.angle_x = newAngle_X;
			this.angle_y = newAngle_Y;
			this.angle_z = newAngle_Z;
			this._owner = newOwner;
			this._group = newGroup;
			this.objActiveDate = newObjActiveDate;
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x000E8A74 File Offset: 0x000E6E74
		public Barricade barricade
		{
			get
			{
				return this._barricade;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002712 RID: 10002 RVA: 0x000E8A7C File Offset: 0x000E6E7C
		public ulong owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002713 RID: 10003 RVA: 0x000E8A84 File Offset: 0x000E6E84
		public ulong group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x040018AD RID: 6317
		private Barricade _barricade;

		// Token: 0x040018AE RID: 6318
		public Vector3 point;

		// Token: 0x040018AF RID: 6319
		public byte angle_x;

		// Token: 0x040018B0 RID: 6320
		public byte angle_y;

		// Token: 0x040018B1 RID: 6321
		public byte angle_z;

		// Token: 0x040018B2 RID: 6322
		private ulong _owner;

		// Token: 0x040018B3 RID: 6323
		private ulong _group;

		// Token: 0x040018B4 RID: 6324
		public uint objActiveDate;
	}
}
