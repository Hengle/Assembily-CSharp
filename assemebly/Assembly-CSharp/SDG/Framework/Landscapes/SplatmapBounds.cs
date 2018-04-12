using System;
using UnityEngine;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001E5 RID: 485
	public struct SplatmapBounds
	{
		// Token: 0x06000EA9 RID: 3753 RVA: 0x00065219 File Offset: 0x00063619
		public SplatmapBounds(SplatmapCoord newMin, SplatmapCoord newMax)
		{
			this.min = newMin;
			this.max = newMax;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0006522C File Offset: 0x0006362C
		public SplatmapBounds(LandscapeCoord tileCoord, Bounds worldBounds)
		{
			int new_x = Mathf.Clamp(Mathf.FloorToInt((worldBounds.min.z - (float)tileCoord.y * Landscape.TILE_SIZE) / Landscape.TILE_SIZE * (float)Landscape.SPLATMAP_RESOLUTION), 0, Landscape.SPLATMAP_RESOLUTION_MINUS_ONE);
			int new_x2 = Mathf.Clamp(Mathf.FloorToInt((worldBounds.max.z - (float)tileCoord.y * Landscape.TILE_SIZE) / Landscape.TILE_SIZE * (float)Landscape.SPLATMAP_RESOLUTION), 0, Landscape.SPLATMAP_RESOLUTION_MINUS_ONE);
			int new_y = Mathf.Clamp(Mathf.FloorToInt((worldBounds.min.x - (float)tileCoord.x * Landscape.TILE_SIZE) / Landscape.TILE_SIZE * (float)Landscape.SPLATMAP_RESOLUTION), 0, Landscape.SPLATMAP_RESOLUTION_MINUS_ONE);
			int new_y2 = Mathf.Clamp(Mathf.FloorToInt((worldBounds.max.x - (float)tileCoord.x * Landscape.TILE_SIZE) / Landscape.TILE_SIZE * (float)Landscape.SPLATMAP_RESOLUTION), 0, Landscape.SPLATMAP_RESOLUTION_MINUS_ONE);
			this.min = new SplatmapCoord(new_x, new_y);
			this.max = new SplatmapCoord(new_x2, new_y2);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0006534C File Offset: 0x0006374C
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

		// Token: 0x0400093C RID: 2364
		public SplatmapCoord min;

		// Token: 0x0400093D RID: 2365
		public SplatmapCoord max;
	}
}
