using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x02000683 RID: 1667
	public class SteamServerInfoPingDescendingComparator : IComparer<SteamServerInfo>
	{
		// Token: 0x06003086 RID: 12422 RVA: 0x0013EC27 File Offset: 0x0013D027
		public int Compare(SteamServerInfo a, SteamServerInfo b)
		{
			return b.ping - a.ping;
		}
	}
}
