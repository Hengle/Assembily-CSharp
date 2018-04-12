﻿using System;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001E4 RID: 484
	public class LandscapeUtility
	{
		// Token: 0x06000EA7 RID: 3751 RVA: 0x0006508C File Offset: 0x0006348C
		public static void cleanHeightmapCoord(ref LandscapeCoord tileCoord, ref HeightmapCoord heightmapCoord)
		{
			if (heightmapCoord.x < 0)
			{
				tileCoord.y--;
				heightmapCoord.x += Landscape.HEIGHTMAP_RESOLUTION;
			}
			if (heightmapCoord.y < 0)
			{
				tileCoord.x--;
				heightmapCoord.y += Landscape.HEIGHTMAP_RESOLUTION;
			}
			if (heightmapCoord.x >= Landscape.HEIGHTMAP_RESOLUTION)
			{
				tileCoord.y++;
				heightmapCoord.x -= Landscape.HEIGHTMAP_RESOLUTION;
			}
			if (heightmapCoord.y >= Landscape.HEIGHTMAP_RESOLUTION)
			{
				tileCoord.x++;
				heightmapCoord.y -= Landscape.HEIGHTMAP_RESOLUTION;
			}
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00065154 File Offset: 0x00063554
		public static void cleanSplatmapCoord(ref LandscapeCoord tileCoord, ref SplatmapCoord splatmapCoord)
		{
			if (splatmapCoord.x < 0)
			{
				tileCoord.y--;
				splatmapCoord.x += Landscape.SPLATMAP_RESOLUTION;
			}
			if (splatmapCoord.y < 0)
			{
				tileCoord.x--;
				splatmapCoord.y += Landscape.SPLATMAP_RESOLUTION;
			}
			if (splatmapCoord.x >= Landscape.SPLATMAP_RESOLUTION)
			{
				tileCoord.y++;
				splatmapCoord.x -= Landscape.SPLATMAP_RESOLUTION;
			}
			if (splatmapCoord.y >= Landscape.SPLATMAP_RESOLUTION)
			{
				tileCoord.x++;
				splatmapCoord.y -= Landscape.SPLATMAP_RESOLUTION;
			}
		}
	}
}
