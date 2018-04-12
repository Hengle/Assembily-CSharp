using System;
using UnityEngine;

namespace SDG.Framework.Foliage
{
	// Token: 0x02000199 RID: 409
	public struct FoliageBounds
	{
		// Token: 0x06000BFE RID: 3070 RVA: 0x0005B8D4 File Offset: 0x00059CD4
		public FoliageBounds(FoliageCoord newMin, FoliageCoord newMax)
		{
			this.min = newMin;
			this.max = newMax;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0005B8E4 File Offset: 0x00059CE4
		public FoliageBounds(Bounds worldBounds)
		{
			this.min = new FoliageCoord(worldBounds.min);
			this.max = new FoliageCoord(worldBounds.max);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0005B90C File Offset: 0x00059D0C
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

		// Token: 0x04000874 RID: 2164
		public FoliageCoord min;

		// Token: 0x04000875 RID: 2165
		public FoliageCoord max;
	}
}
