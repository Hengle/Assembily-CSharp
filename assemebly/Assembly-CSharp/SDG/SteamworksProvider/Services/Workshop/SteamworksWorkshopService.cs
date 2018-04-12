using System;
using SDG.Provider.Services;
using SDG.Provider.Services.Workshop;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Workshop
{
	// Token: 0x0200037E RID: 894
	public class SteamworksWorkshopService : Service, IWorkshopService, IService
	{
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x000899F7 File Offset: 0x00087DF7
		public bool canOpenWorkshop
		{
			get
			{
				return SteamUtils.IsOverlayEnabled();
			}
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x000899FE File Offset: 0x00087DFE
		public void open(PublishedFileId_t id)
		{
			SteamFriends.ActivateGameOverlayToWebPage("http://steamcommunity.com/sharedfiles/filedetails/?id=" + id.m_PublishedFileId);
		}
	}
}
