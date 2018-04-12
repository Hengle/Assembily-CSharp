using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000689 RID: 1673
	public struct RegionBounds
	{
		// Token: 0x060030AD RID: 12461 RVA: 0x0013F45C File Offset: 0x0013D85C
		public RegionBounds(RegionCoord newMin, RegionCoord newMax)
		{
			this.min = newMin;
			this.max = newMax;
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x0013F46C File Offset: 0x0013D86C
		public RegionBounds(Bounds worldBounds)
		{
			this.min = new RegionCoord(worldBounds.min);
			this.max = new RegionCoord(worldBounds.max);
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x0013F494 File Offset: 0x0013D894
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				'[',
				this.min.ToString(),
				", ",
				this.max.ToString(),
				']'
			});
		}

		// Token: 0x0400202E RID: 8238
		public RegionCoord min;

		// Token: 0x0400202F RID: 8239
		public RegionCoord max;
	}
}
