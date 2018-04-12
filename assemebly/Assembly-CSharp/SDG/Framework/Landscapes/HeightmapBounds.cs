using System;
using UnityEngine;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001CE RID: 462
	public struct HeightmapBounds
	{
		// Token: 0x06000DD6 RID: 3542 RVA: 0x000616C3 File Offset: 0x0005FAC3
		public HeightmapBounds(HeightmapCoord newMin, HeightmapCoord newMax)
		{
			this.min = newMin;
			this.max = newMax;
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x000616D4 File Offset: 0x0005FAD4
		public HeightmapBounds(LandscapeCoord tileCoord, Bounds worldBounds)
		{
			int new_x = Mathf.Clamp(Mathf.FloorToInt((worldBounds.min.z - (float)tileCoord.y * Landscape.TILE_SIZE) / Landscape.TILE_SIZE * (float)Landscape.HEIGHTMAP_RESOLUTION_MINUS_ONE), 0, Landscape.HEIGHTMAP_RESOLUTION_MINUS_ONE);
			int new_x2 = Mathf.Clamp(Mathf.CeilToInt((worldBounds.max.z - (float)tileCoord.y * Landscape.TILE_SIZE) / Landscape.TILE_SIZE * (float)Landscape.HEIGHTMAP_RESOLUTION_MINUS_ONE), 0, Landscape.HEIGHTMAP_RESOLUTION_MINUS_ONE);
			int new_y = Mathf.Clamp(Mathf.FloorToInt((worldBounds.min.x - (float)tileCoord.x * Landscape.TILE_SIZE) / Landscape.TILE_SIZE * (float)Landscape.HEIGHTMAP_RESOLUTION_MINUS_ONE), 0, Landscape.HEIGHTMAP_RESOLUTION_MINUS_ONE);
			int new_y2 = Mathf.Clamp(Mathf.CeilToInt((worldBounds.max.x - (float)tileCoord.x * Landscape.TILE_SIZE) / Landscape.TILE_SIZE * (float)Landscape.HEIGHTMAP_RESOLUTION_MINUS_ONE), 0, Landscape.HEIGHTMAP_RESOLUTION_MINUS_ONE);
			this.min = new HeightmapCoord(new_x, new_y);
			this.max = new HeightmapCoord(new_x2, new_y2);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x000617F4 File Offset: 0x0005FBF4
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

		// Token: 0x040008FD RID: 2301
		public HeightmapCoord min;

		// Token: 0x040008FE RID: 2302
		public HeightmapCoord max;
	}
}
