using System;
using System.Runtime.CompilerServices;
using SDG.Framework.Utilities;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001E1 RID: 481
	public static class LandscapeSplatmapCopyPool
	{
		// Token: 0x06000E70 RID: 3696 RVA: 0x00063C56 File Offset: 0x00062056
		public static void empty()
		{
			LandscapeSplatmapCopyPool.pool.empty();
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00063C62 File Offset: 0x00062062
		public static void warmup(uint count)
		{
			Pool<float[,,]> pool = LandscapeSplatmapCopyPool.pool;
			if (LandscapeSplatmapCopyPool.<>f__mg$cache0 == null)
			{
				LandscapeSplatmapCopyPool.<>f__mg$cache0 = new Pool<float[,,]>.PoolClaimHandler(LandscapeSplatmapCopyPool.handlePoolClaim);
			}
			pool.warmup(count, LandscapeSplatmapCopyPool.<>f__mg$cache0);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00063C8C File Offset: 0x0006208C
		public static float[,,] claim()
		{
			Pool<float[,,]> pool = LandscapeSplatmapCopyPool.pool;
			if (LandscapeSplatmapCopyPool.<>f__mg$cache1 == null)
			{
				LandscapeSplatmapCopyPool.<>f__mg$cache1 = new Pool<float[,,]>.PoolClaimHandler(LandscapeSplatmapCopyPool.handlePoolClaim);
			}
			return pool.claim(LandscapeSplatmapCopyPool.<>f__mg$cache1);
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00063CB5 File Offset: 0x000620B5
		public static void release(float[,,] copy)
		{
			Pool<float[,,]> pool = LandscapeSplatmapCopyPool.pool;
			if (LandscapeSplatmapCopyPool.<>f__mg$cache2 == null)
			{
				LandscapeSplatmapCopyPool.<>f__mg$cache2 = new Pool<float[,,]>.PoolReleasedHandler(LandscapeSplatmapCopyPool.handlePoolRelease);
			}
			pool.release(copy, LandscapeSplatmapCopyPool.<>f__mg$cache2);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00063CDF File Offset: 0x000620DF
		private static float[,,] handlePoolClaim(Pool<float[,,]> pool)
		{
			return new float[Landscape.SPLATMAP_RESOLUTION, Landscape.SPLATMAP_RESOLUTION, Landscape.SPLATMAP_LAYERS];
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00063CF5 File Offset: 0x000620F5
		private static void handlePoolRelease(Pool<float[,,]> pool, float[,,] copy)
		{
		}

		// Token: 0x0400092D RID: 2349
		private static Pool<float[,,]> pool = new Pool<float[,,]>();

		// Token: 0x0400092E RID: 2350
		[CompilerGenerated]
		private static Pool<float[,,]>.PoolClaimHandler <>f__mg$cache0;

		// Token: 0x0400092F RID: 2351
		[CompilerGenerated]
		private static Pool<float[,,]>.PoolClaimHandler <>f__mg$cache1;

		// Token: 0x04000930 RID: 2352
		[CompilerGenerated]
		private static Pool<float[,,]>.PoolReleasedHandler <>f__mg$cache2;
	}
}
