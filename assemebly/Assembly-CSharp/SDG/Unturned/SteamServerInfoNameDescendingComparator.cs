using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x0200067D RID: 1661
	public class SteamServerInfoNameDescendingComparator : IComparer<SteamServerInfo>
	{
		// Token: 0x0600307A RID: 12410 RVA: 0x0013EB91 File Offset: 0x0013CF91
		public int Compare(SteamServerInfo a, SteamServerInfo b)
		{
			return b.name.CompareTo(a.name);
		}
	}
}
