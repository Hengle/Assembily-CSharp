using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x02000678 RID: 1656
	public class SteamPlayerGroupAscendingComparator : IComparer<SteamPlayer>
	{
		// Token: 0x0600304C RID: 12364 RVA: 0x0013E28C File Offset: 0x0013C68C
		public int Compare(SteamPlayer a, SteamPlayer b)
		{
			if (b.player.quests.groupID.m_SteamID > a.player.quests.groupID.m_SteamID)
			{
				return 1;
			}
			if (b.player.quests.groupID.m_SteamID < a.player.quests.groupID.m_SteamID)
			{
				return -1;
			}
			return 0;
		}
	}
}
