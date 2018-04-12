using System;
using UnityEngine;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001D9 RID: 473
	public struct LandscapeBounds
	{
		// Token: 0x06000E32 RID: 3634 RVA: 0x00063065 File Offset: 0x00061465
		public LandscapeBounds(LandscapeCoord newMin, LandscapeCoord newMax)
		{
			this.min = newMin;
			this.max = newMax;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00063075 File Offset: 0x00061475
		public LandscapeBounds(Bounds worldBounds)
		{
			this.min = new LandscapeCoord(worldBounds.min);
			this.max = new LandscapeCoord(worldBounds.max);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0006309C File Offset: 0x0006149C
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

		// Token: 0x04000917 RID: 2327
		public LandscapeCoord min;

		// Token: 0x04000918 RID: 2328
		public LandscapeCoord max;
	}
}
