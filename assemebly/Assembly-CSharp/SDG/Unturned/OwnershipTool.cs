using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200073F RID: 1855
	internal class OwnershipTool
	{
		// Token: 0x0600344D RID: 13389 RVA: 0x001566F7 File Offset: 0x00154AF7
		public static bool checkToggle(ulong player, ulong group)
		{
			return !Dedicator.isDedicated && OwnershipTool.checkToggle(Provider.client, player, Player.player.quests.groupID, group);
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x00156720 File Offset: 0x00154B20
		public static bool checkToggle(CSteamID player_0, ulong player_1, CSteamID group_0, ulong group_1)
		{
			return (Provider.isServer && !Dedicator.isDedicated) || player_0.m_SteamID == player_1 || (group_0 != CSteamID.Nil && group_0.m_SteamID == group_1);
		}
	}
}
