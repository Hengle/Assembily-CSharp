using System;
using SDG.Provider.Services;
using SDG.Provider.Services.Browser;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Browser
{
	// Token: 0x02000366 RID: 870
	public class SteamworksBrowserService : Service, IBrowserService, IService
	{
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x0008845C File Offset: 0x0008685C
		public bool canOpenBrowser
		{
			get
			{
				return SteamUtils.IsOverlayEnabled();
			}
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00088463 File Offset: 0x00086863
		public void open(string url)
		{
			SteamFriends.ActivateGameOverlayToWebPage(url);
		}
	}
}
