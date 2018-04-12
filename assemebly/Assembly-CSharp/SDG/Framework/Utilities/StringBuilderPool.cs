using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace SDG.Framework.Utilities
{
	// Token: 0x0200030D RID: 781
	public static class StringBuilderPool
	{
		// Token: 0x0600164B RID: 5707 RVA: 0x00084A26 File Offset: 0x00082E26
		public static StringBuilder claim()
		{
			Pool<StringBuilder> pool = StringBuilderPool.pool;
			if (StringBuilderPool.<>f__mg$cache0 == null)
			{
				StringBuilderPool.<>f__mg$cache0 = new Pool<StringBuilder>.PoolClaimHandler(StringBuilderPool.handlePoolClaim);
			}
			return pool.claim(StringBuilderPool.<>f__mg$cache0);
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00084A4F File Offset: 0x00082E4F
		public static void release(StringBuilder sb)
		{
			Pool<StringBuilder> pool = StringBuilderPool.pool;
			if (StringBuilderPool.<>f__mg$cache1 == null)
			{
				StringBuilderPool.<>f__mg$cache1 = new Pool<StringBuilder>.PoolReleasedHandler(StringBuilderPool.handlePoolRelease);
			}
			pool.release(sb, StringBuilderPool.<>f__mg$cache1);
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x00084A79 File Offset: 0x00082E79
		private static StringBuilder handlePoolClaim(Pool<StringBuilder> pool)
		{
			return new StringBuilder();
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00084A80 File Offset: 0x00082E80
		private static void handlePoolRelease(Pool<StringBuilder> pool, StringBuilder sb)
		{
			sb.Length = 0;
		}

		// Token: 0x04000C34 RID: 3124
		private static Pool<StringBuilder> pool = new Pool<StringBuilder>();

		// Token: 0x04000C35 RID: 3125
		[CompilerGenerated]
		private static Pool<StringBuilder>.PoolClaimHandler <>f__mg$cache0;

		// Token: 0x04000C36 RID: 3126
		[CompilerGenerated]
		private static Pool<StringBuilder>.PoolReleasedHandler <>f__mg$cache1;
	}
}
