using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x02000681 RID: 1665
	public class SteamServerInfoPlayersDescendingComparator : IComparer<SteamServerInfo>
	{
		// Token: 0x06003082 RID: 12418 RVA: 0x0013EBF9 File Offset: 0x0013CFF9
		public int Compare(SteamServerInfo a, SteamServerInfo b)
		{
			return a.players - b.players;
		}
	}
}
