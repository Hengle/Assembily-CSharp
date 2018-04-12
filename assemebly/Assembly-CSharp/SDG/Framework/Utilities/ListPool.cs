using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SDG.Framework.Utilities
{
	// Token: 0x02000302 RID: 770
	public static class ListPool<T>
	{
		// Token: 0x06001611 RID: 5649 RVA: 0x00084248 File Offset: 0x00082648
		public static void empty()
		{
			ListPool<T>.pool.empty();
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x00084254 File Offset: 0x00082654
		public static void warmup(uint count)
		{
			Pool<List<T>> pool = ListPool<T>.pool;
			if (ListPool<T>.<>f__mg$cache0 == null)
			{
				ListPool<T>.<>f__mg$cache0 = new Pool<List<T>>.PoolClaimHandler(ListPool<T>.handlePoolClaim);
			}
			pool.warmup(count, ListPool<T>.<>f__mg$cache0);
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0008427E File Offset: 0x0008267E
		public static List<T> claim()
		{
			Pool<List<T>> pool = ListPool<T>.pool;
			if (ListPool<T>.<>f__mg$cache1 == null)
			{
				ListPool<T>.<>f__mg$cache1 = new Pool<List<T>>.PoolClaimHandler(ListPool<T>.handlePoolClaim);
			}
			return pool.claim(ListPool<T>.<>f__mg$cache1);
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x000842A7 File Offset: 0x000826A7
		public static void release(List<T> list)
		{
			Pool<List<T>> pool = ListPool<T>.pool;
			if (ListPool<T>.<>f__mg$cache2 == null)
			{
				ListPool<T>.<>f__mg$cache2 = new Pool<List<T>>.PoolReleasedHandler(ListPool<T>.handlePoolRelease);
			}
			pool.release(list, ListPool<T>.<>f__mg$cache2);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x000842D1 File Offset: 0x000826D1
		private static List<T> handlePoolClaim(Pool<List<T>> pool)
		{
			return new List<T>();
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x000842D8 File Offset: 0x000826D8
		private static void handlePoolRelease(Pool<List<T>> pool, List<T> list)
		{
			list.Clear();
		}

		// Token: 0x04000C25 RID: 3109
		private static Pool<List<T>> pool = new Pool<List<T>>();

		// Token: 0x04000C26 RID: 3110
		[CompilerGenerated]
		private static Pool<List<T>>.PoolClaimHandler <>f__mg$cache0;

		// Token: 0x04000C27 RID: 3111
		[CompilerGenerated]
		private static Pool<List<T>>.PoolClaimHandler <>f__mg$cache1;

		// Token: 0x04000C28 RID: 3112
		[CompilerGenerated]
		private static Pool<List<T>>.PoolReleasedHandler <>f__mg$cache2;
	}
}
