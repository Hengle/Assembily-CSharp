using System;
using System.Runtime.CompilerServices;

namespace SDG.Framework.Utilities
{
	// Token: 0x0200030B RID: 779
	public static class PoolablePool<T> where T : IPoolable
	{
		// Token: 0x0600163F RID: 5695 RVA: 0x0008484E File Offset: 0x00082C4E
		public static void empty()
		{
			PoolablePool<T>.pool.empty();
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0008485A File Offset: 0x00082C5A
		public static void warmup(uint count)
		{
			Pool<T> pool = PoolablePool<T>.pool;
			if (PoolablePool<T>.<>f__mg$cache0 == null)
			{
				PoolablePool<T>.<>f__mg$cache0 = new Pool<T>.PoolClaimHandler(PoolablePool<T>.handlePoolClaim);
			}
			pool.warmup(count, PoolablePool<T>.<>f__mg$cache0);
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00084884 File Offset: 0x00082C84
		public static T claim()
		{
			Pool<T> pool = PoolablePool<T>.pool;
			if (PoolablePool<T>.<>f__mg$cache1 == null)
			{
				PoolablePool<T>.<>f__mg$cache1 = new Pool<T>.PoolClaimHandler(PoolablePool<T>.handlePoolClaim);
			}
			T result = pool.claim(PoolablePool<T>.<>f__mg$cache1);
			result.poolClaim();
			return result;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x000848C7 File Offset: 0x00082CC7
		public static void release(T poolable)
		{
			Pool<T> pool = PoolablePool<T>.pool;
			if (PoolablePool<T>.<>f__mg$cache2 == null)
			{
				PoolablePool<T>.<>f__mg$cache2 = new Pool<T>.PoolReleasedHandler(PoolablePool<T>.handlePoolRelease);
			}
			pool.release(poolable, PoolablePool<T>.<>f__mg$cache2);
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x000848F1 File Offset: 0x00082CF1
		private static T handlePoolClaim(Pool<T> pool)
		{
			return Activator.CreateInstance<T>();
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x000848F8 File Offset: 0x00082CF8
		private static void handlePoolRelease(Pool<T> pool, T poolable)
		{
			poolable.poolRelease();
		}

		// Token: 0x04000C2E RID: 3118
		private static Pool<T> pool = new Pool<T>();

		// Token: 0x04000C2F RID: 3119
		[CompilerGenerated]
		private static Pool<T>.PoolClaimHandler <>f__mg$cache0;

		// Token: 0x04000C30 RID: 3120
		[CompilerGenerated]
		private static Pool<T>.PoolClaimHandler <>f__mg$cache1;

		// Token: 0x04000C31 RID: 3121
		[CompilerGenerated]
		private static Pool<T>.PoolReleasedHandler <>f__mg$cache2;
	}
}
