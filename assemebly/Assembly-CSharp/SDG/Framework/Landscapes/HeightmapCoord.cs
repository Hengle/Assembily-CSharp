using System;
using UnityEngine;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001CF RID: 463
	public struct HeightmapCoord : IEquatable<HeightmapCoord>
	{
		// Token: 0x06000DD9 RID: 3545 RVA: 0x00061850 File Offset: 0x0005FC50
		public HeightmapCoord(int new_x, int new_y)
		{
			this.x = new_x;
			this.y = new_y;
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00061860 File Offset: 0x0005FC60
		public HeightmapCoord(LandscapeCoord tileCoord, Vector3 worldPosition)
		{
			this.x = Mathf.Clamp(Mathf.RoundToInt((worldPosition.z - (float)tileCoord.y * Landscape.TILE_SIZE) / Landscape.TILE_SIZE * (float)Landscape.HEIGHTMAP_RESOLUTION_MINUS_ONE), 0, Landscape.HEIGHTMAP_RESOLUTION_MINUS_ONE);
			this.y = Mathf.Clamp(Mathf.RoundToInt((worldPosition.x - (float)tileCoord.x * Landscape.TILE_SIZE) / Landscape.TILE_SIZE * (float)Landscape.HEIGHTMAP_RESOLUTION_MINUS_ONE), 0, Landscape.HEIGHTMAP_RESOLUTION_MINUS_ONE);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x000618DF File Offset: 0x0005FCDF
		public static bool operator ==(HeightmapCoord a, HeightmapCoord b)
		{
			return a.x == b.x && a.y == b.y;
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00061907 File Offset: 0x0005FD07
		public static bool operator !=(HeightmapCoord a, HeightmapCoord b)
		{
			return !(a == b);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00061914 File Offset: 0x0005FD14
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			HeightmapCoord heightmapCoord = (HeightmapCoord)obj;
			return this.x == heightmapCoord.x && this.y == heightmapCoord.y;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00061954 File Offset: 0x0005FD54
		public override int GetHashCode()
		{
			return this.x ^ this.y;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00061964 File Offset: 0x0005FD64
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				'(',
				this.x.ToString(),
				", ",
				this.y.ToString(),
				')'
			});
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x000619C0 File Offset: 0x0005FDC0
		public bool Equals(HeightmapCoord other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x040008FF RID: 2303
		public int x;

		// Token: 0x04000900 RID: 2304
		public int y;
	}
}
