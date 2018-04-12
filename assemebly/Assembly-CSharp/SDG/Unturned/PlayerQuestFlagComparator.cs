using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x02000637 RID: 1591
	public class PlayerQuestFlagComparator : IComparer<PlayerQuestFlag>
	{
		// Token: 0x06002D78 RID: 11640 RVA: 0x00125977 File Offset: 0x00123D77
		public int Compare(PlayerQuestFlag a, PlayerQuestFlag b)
		{
			return (int)(a.id - b.id);
		}
	}
}
