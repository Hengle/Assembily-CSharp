using System;
using System.Runtime.CompilerServices;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x020005BD RID: 1469
	public class SaveManager : SteamCaller
	{
		// Token: 0x06002918 RID: 10520 RVA: 0x000FB234 File Offset: 0x000F9634
		public static void save()
		{
			if (Level.info.type == ELevelType.SURVIVAL)
			{
				for (int i = 0; i < Provider.clients.Count; i++)
				{
					if (Provider.clients[i].model != null)
					{
						Player component = Provider.clients[i].model.GetComponent<Player>();
						component.save();
					}
				}
				VehicleManager.save();
				BarricadeManager.save();
				StructureManager.save();
				ObjectManager.save();
				LightingManager.save();
				GroupManager.save();
			}
			if (Dedicator.isDedicated)
			{
				SteamWhitelist.save();
				SteamBlacklist.save();
				SteamAdminlist.save();
			}
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000FB2D9 File Offset: 0x000F96D9
		private static void onServerShutdown()
		{
			if (Provider.isServer && Level.isLoaded)
			{
				SaveManager.save();
			}
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000FB2F4 File Offset: 0x000F96F4
		private static void onServerDisconnected(CSteamID steamID)
		{
			if (Provider.isServer && Level.isLoaded)
			{
				Player player = PlayerTool.getPlayer(steamID);
				if (player != null)
				{
					player.save();
				}
			}
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000FB330 File Offset: 0x000F9730
		private void Start()
		{
			Delegate onServerShutdown = Provider.onServerShutdown;
			if (SaveManager.<>f__mg$cache0 == null)
			{
				SaveManager.<>f__mg$cache0 = new Provider.ServerShutdown(SaveManager.onServerShutdown);
			}
			Provider.onServerShutdown = (Provider.ServerShutdown)Delegate.Combine(onServerShutdown, SaveManager.<>f__mg$cache0);
			Delegate onServerDisconnected = Provider.onServerDisconnected;
			if (SaveManager.<>f__mg$cache1 == null)
			{
				SaveManager.<>f__mg$cache1 = new Provider.ServerDisconnected(SaveManager.onServerDisconnected);
			}
			Provider.onServerDisconnected = (Provider.ServerDisconnected)Delegate.Combine(onServerDisconnected, SaveManager.<>f__mg$cache1);
		}

		// Token: 0x0400199E RID: 6558
		[CompilerGenerated]
		private static Provider.ServerShutdown <>f__mg$cache0;

		// Token: 0x0400199F RID: 6559
		[CompilerGenerated]
		private static Provider.ServerDisconnected <>f__mg$cache1;
	}
}
