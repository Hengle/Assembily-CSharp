using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x0200067F RID: 1663
	public class SteamServerInfoMapDescendingComparator : IComparer<SteamServerInfo>
	{
		// Token: 0x0600307E RID: 12414 RVA: 0x0013EBC7 File Offset: 0x0013CFC7
		public int Compare(SteamServerInfo a, SteamServerInfo b)
		{
			return b.map.CompareTo(a.map);
		}
	}
}
