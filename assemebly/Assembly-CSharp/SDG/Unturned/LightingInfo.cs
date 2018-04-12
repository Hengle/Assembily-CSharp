using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000560 RID: 1376
	public class LightingInfo
	{
		// Token: 0x06002605 RID: 9733 RVA: 0x000DF53D File Offset: 0x000DD93D
		public LightingInfo(Color[] newColors, float[] newSingles)
		{
			this._colors = newColors;
			this._singles = newSingles;
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x000DF553 File Offset: 0x000DD953
		public Color[] colors
		{
			get
			{
				return this._colors;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002607 RID: 9735 RVA: 0x000DF55B File Offset: 0x000DD95B
		public float[] singles
		{
			get
			{
				return this._singles;
			}
		}

		// Token: 0x040017B0 RID: 6064
		private Color[] _colors;

		// Token: 0x040017B1 RID: 6065
		private float[] _singles;
	}
}
