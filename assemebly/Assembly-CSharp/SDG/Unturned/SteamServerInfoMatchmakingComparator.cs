using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x02000684 RID: 1668
	public class SteamServerInfoMatchmakingComparator : IComparer<SteamServerInfo>
	{
		// Token: 0x06003088 RID: 12424 RVA: 0x0013EC40 File Offset: 0x0013D040
		public int Compare(SteamServerInfo a, SteamServerInfo b)
		{
			int num = b.players - a.players;
			if (num != 0)
			{
				return num;
			}
			return a.ping - b.ping;
		}
	}
}
