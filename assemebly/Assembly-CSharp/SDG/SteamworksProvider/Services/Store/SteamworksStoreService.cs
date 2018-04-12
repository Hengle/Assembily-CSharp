using System;
using SDG.Provider.Services;
using SDG.Provider.Services.Economy;
using SDG.Provider.Services.Store;
using SDG.SteamworksProvider.Services.Economy;
using SDG.Unturned;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Store
{
	// Token: 0x0200037A RID: 890
	public class SteamworksStoreService : Service, IStoreService, IService
	{
		// Token: 0x0600186B RID: 6251 RVA: 0x000896B9 File Offset: 0x00087AB9
		public SteamworksStoreService(SteamworksAppInfo newAppInfo)
		{
			this.appInfo = newAppInfo;
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x000896C8 File Offset: 0x00087AC8
		public bool canOpenStore
		{
			get
			{
				return SteamUtils.IsOverlayEnabled();
			}
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x000896D0 File Offset: 0x00087AD0
		public void open(IStorePackageID packageID)
		{
			SteamworksStorePackageID steamworksStorePackageID = (SteamworksStorePackageID)packageID;
			AppId_t appID = steamworksStorePackageID.appID;
			SteamFriends.ActivateGameOverlayToStore(appID, EOverlayToStoreFlag.k_EOverlayToStoreFlag_None);
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x000896F4 File Offset: 0x00087AF4
		public void open(IEconomyItemDefinition itemDefinitionID)
		{
			SteamworksEconomyItemDefinition steamworksEconomyItemDefinition = (SteamworksEconomyItemDefinition)itemDefinitionID;
			SteamItemDef_t steamItemDef = steamworksEconomyItemDefinition.steamItemDef;
			SteamFriends.ActivateGameOverlayToWebPage(string.Concat(new object[]
			{
				"http://store.steampowered.com/itemstore/",
				this.appInfo.id,
				"/detail/",
				steamItemDef
			}));
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0008974C File Offset: 0x00087B4C
		public void open()
		{
			if (Provider.statusData.Stockpile.Has_New_Items)
			{
				SteamFriends.ActivateGameOverlayToWebPage("http://store.steampowered.com/itemstore/" + this.appInfo.id + "/browse/?filter=New");
			}
			else
			{
				SteamFriends.ActivateGameOverlayToWebPage("http://store.steampowered.com/itemstore/" + this.appInfo.id);
			}
		}

		// Token: 0x04000CF6 RID: 3318
		private SteamworksAppInfo appInfo;
	}
}
