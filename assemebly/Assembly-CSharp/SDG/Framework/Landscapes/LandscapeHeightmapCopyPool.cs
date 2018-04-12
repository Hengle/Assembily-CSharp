using System;
using System.Runtime.CompilerServices;
using SDG.Framework.Utilities;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001DB RID: 475
	public static class LandscapeHeightmapCopyPool
	{
		// Token: 0x06000E41 RID: 3649 RVA: 0x000632C0 File Offset: 0x000616C0
		public static void empty()
		{
			LandscapeHeightmapCopyPool.pool.empty();
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x000632CC File Offset: 0x000616CC
		public static void warmup(uint count)
		{
			Pool<float[,]> pool = LandscapeHeightmapCopyPool.pool;
			if (LandscapeHeightmapCopyPool.<>f__mg$cache0 == null)
			{
				LandscapeHeightmapCopyPool.<>f__mg$cache0 = new Pool<float[,]>.PoolClaimHandler(LandscapeHeightmapCopyPool.handlePoolClaim);
			}
			pool.warmup(count, LandscapeHeightmapCopyPool.<>f__mg$cache0);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x000632F6 File Offset: 0x000616F6
		public static float[,] claim()
		{
			Pool<float[,]> pool = LandscapeHeightmapCopyPool.pool;
			if (LandscapeHeightmapCopyPool.<>f__mg$cache1 == null)
			{
				LandscapeHeightmapCopyPool.<>f__mg$cache1 = new Pool<float[,]>.PoolClaimHandler(LandscapeHeightmapCopyPool.handlePoolClaim);
			}
			return pool.claim(LandscapeHeightmapCopyPool.<>f__mg$cache1);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0006331F File Offset: 0x0006171F
		public static void release(float[,] copy)
		{
			Pool<float[,]> pool = LandscapeHeightmapCopyPool.pool;
			if (LandscapeHeightmapCopyPool.<>f__mg$cache2 == null)
			{
				LandscapeHeightmapCopyPool.<>f__mg$cache2 = new Pool<float[,]>.PoolReleasedHandler(LandscapeHeightmapCopyPool.handlePoolRelease);
			}
			pool.release(copy, LandscapeHeightmapCopyPool.<>f__mg$cache2);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00063349 File Offset: 0x00061749
		private static float[,] handlePoolClaim(Pool<float[,]> pool)
		{
			return new float[Landscape.HEIGHTMAP_RESOLUTION, Landscape.HEIGHTMAP_RESOLUTION];
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0006335A File Offset: 0x0006175A
		private static void handlePoolRelease(Pool<float[,]> pool, float[,] copy)
		{
		}

		// Token: 0x0400091C RID: 2332
		private static Pool<float[,]> pool = new Pool<float[,]>();

		// Token: 0x0400091D RID: 2333
		[CompilerGenerated]
		private static Pool<float[,]>.PoolClaimHandler <>f__mg$cache0;

		// Token: 0x0400091E RID: 2334
		[CompilerGenerated]
		private static Pool<float[,]>.PoolClaimHandler <>f__mg$cache1;

		// Token: 0x0400091F RID: 2335
		[CompilerGenerated]
		private static Pool<float[,]>.PoolReleasedHandler <>f__mg$cache2;
	}
}
