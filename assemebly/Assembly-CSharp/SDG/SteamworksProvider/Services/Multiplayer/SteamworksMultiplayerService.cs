using System;
using SDG.Provider.Services;
using SDG.Provider.Services.Multiplayer;
using SDG.Provider.Services.Multiplayer.Client;
using SDG.Provider.Services.Multiplayer.Server;
using SDG.SteamworksProvider.Services.Multiplayer.Client;
using SDG.SteamworksProvider.Services.Multiplayer.Server;

namespace SDG.SteamworksProvider.Services.Multiplayer
{
	// Token: 0x02000374 RID: 884
	public class SteamworksMultiplayerService : IMultiplayerService, IService
	{
		// Token: 0x0600183B RID: 6203 RVA: 0x000891F4 File Offset: 0x000875F4
		public SteamworksMultiplayerService(SteamworksAppInfo newAppInfo)
		{
			this.appInfo = newAppInfo;
			this.serverMultiplayerService = new SteamworksServerMultiplayerService(this.appInfo);
			if (!this.appInfo.isDedicated)
			{
				this.clientMultiplayerService = new SteamworksClientMultiplayerService();
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x0008922F File Offset: 0x0008762F
		// (set) Token: 0x0600183D RID: 6205 RVA: 0x00089237 File Offset: 0x00087637
		public IClientMultiplayerService clientMultiplayerService { get; protected set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x00089240 File Offset: 0x00087640
		// (set) Token: 0x0600183F RID: 6207 RVA: 0x00089248 File Offset: 0x00087648
		public IServerMultiplayerService serverMultiplayerService { get; protected set; }

		// Token: 0x06001840 RID: 6208 RVA: 0x00089251 File Offset: 0x00087651
		public void initialize()
		{
			this.serverMultiplayerService.initialize();
			if (!this.appInfo.isDedicated)
			{
				this.clientMultiplayerService.initialize();
			}
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00089279 File Offset: 0x00087679
		public void update()
		{
			this.serverMultiplayerService.update();
			if (!this.appInfo.isDedicated)
			{
				this.clientMultiplayerService.update();
			}
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x000892A1 File Offset: 0x000876A1
		public void shutdown()
		{
			this.serverMultiplayerService.shutdown();
			if (!this.appInfo.isDedicated)
			{
				this.clientMultiplayerService.shutdown();
			}
		}

		// Token: 0x04000CE9 RID: 3305
		private SteamworksAppInfo appInfo;
	}
}
