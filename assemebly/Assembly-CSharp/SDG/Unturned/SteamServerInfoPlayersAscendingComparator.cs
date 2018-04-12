using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x02000680 RID: 1664
	public class SteamServerInfoPlayersAscendingComparator : IComparer<SteamServerInfo>
	{
		// Token: 0x06003080 RID: 12416 RVA: 0x0013EBE2 File Offset: 0x0013CFE2
		public int Compare(SteamServerInfo a, SteamServerInfo b)
		{
			return b.players - a.players;
		}
	}
}
