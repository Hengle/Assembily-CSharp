using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x02000682 RID: 1666
	public class SteamServerInfoPingAscendingComparator : IComparer<SteamServerInfo>
	{
		// Token: 0x06003084 RID: 12420 RVA: 0x0013EC10 File Offset: 0x0013D010
		public int Compare(SteamServerInfo a, SteamServerInfo b)
		{
			return a.ping - b.ping;
		}
	}
}
