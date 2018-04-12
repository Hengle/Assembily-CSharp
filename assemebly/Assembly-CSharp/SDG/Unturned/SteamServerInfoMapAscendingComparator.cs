using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x0200067E RID: 1662
	public class SteamServerInfoMapAscendingComparator : IComparer<SteamServerInfo>
	{
		// Token: 0x0600307C RID: 12412 RVA: 0x0013EBAC File Offset: 0x0013CFAC
		public int Compare(SteamServerInfo a, SteamServerInfo b)
		{
			return a.map.CompareTo(b.map);
		}
	}
}
