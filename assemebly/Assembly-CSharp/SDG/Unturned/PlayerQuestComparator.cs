using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x02000635 RID: 1589
	public class PlayerQuestComparator : IComparer<PlayerQuest>
	{
		// Token: 0x06002D71 RID: 11633 RVA: 0x00125917 File Offset: 0x00123D17
		public int Compare(PlayerQuest a, PlayerQuest b)
		{
			return (int)(a.id - b.id);
		}
	}
}
