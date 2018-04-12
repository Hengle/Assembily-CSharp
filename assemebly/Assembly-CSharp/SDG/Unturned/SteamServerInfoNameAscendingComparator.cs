using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x0200067C RID: 1660
	public class SteamServerInfoNameAscendingComparator : IComparer<SteamServerInfo>
	{
		// Token: 0x06003078 RID: 12408 RVA: 0x0013EB76 File Offset: 0x0013CF76
		public int Compare(SteamServerInfo a, SteamServerInfo b)
		{
			return a.name.CompareTo(b.name);
		}
	}
}
