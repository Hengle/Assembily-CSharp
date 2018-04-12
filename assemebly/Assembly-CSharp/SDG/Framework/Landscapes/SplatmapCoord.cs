using System;
using UnityEngine;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001E6 RID: 486
	public struct SplatmapCoord : IEquatable<SplatmapCoord>
	{
		// Token: 0x06000EAC RID: 3756 RVA: 0x000653A8 File Offset: 0x000637A8
		public SplatmapCoord(int new_x, int new_y)
		{
			this.x = new_x;
			this.y = new_y;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x000653B8 File Offset: 0x000637B8
		public SplatmapCoord(LandscapeCoord tileCoord, Vector3 worldPosition)
		{
			this.x = Mathf.Clamp(Mathf.FloorToInt((worldPosition.z - (float)tileCoord.y * Landscape.TILE_SIZE) / Landscape.TILE_SIZE * (float)Landscape.SPLATMAP_RESOLUTION), 0, Landscape.SPLATMAP_RESOLUTION_MINUS_ONE);
			this.y = Mathf.Clamp(Mathf.FloorToInt((worldPosition.x - (float)tileCoord.x * Landscape.TILE_SIZE) / Landscape.TILE_SIZE * (float)Landscape.SPLATMAP_RESOLUTION), 0, Landscape.SPLATMAP_RESOLUTION_MINUS_ONE);
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00065437 File Offset: 0x00063837
		public static bool operator ==(SplatmapCoord a, SplatmapCoord b)
		{
			return a.x == b.x && a.y == b.y;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0006545F File Offset: 0x0006385F
		public static bool operator !=(SplatmapCoord a, SplatmapCoord b)
		{
			return !(a == b);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0006546C File Offset: 0x0006386C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			SplatmapCoord splatmapCoord = (SplatmapCoord)obj;
			return this.x.Equals(splatmapCoord.x) && this.y.Equals(splatmapCoord.y);
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x000654B4 File Offset: 0x000638B4
		public override int GetHashCode()
		{
			return this.x ^ this.y;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x000654C4 File Offset: 0x000638C4
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

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00065520 File Offset: 0x00063920
		public bool Equals(SplatmapCoord other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x0400093E RID: 2366
		public int x;

		// Token: 0x0400093F RID: 2367
		public int y;
	}
}
