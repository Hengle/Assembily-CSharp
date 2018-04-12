using System;
using SDG.Provider;
using SDG.Provider.Services.Achievements;
using SDG.Provider.Services.Browser;
using SDG.Provider.Services.Cloud;
using SDG.Provider.Services.Community;
using SDG.Provider.Services.Multiplayer;
using SDG.Provider.Services.Statistics;
using SDG.Provider.Services.Store;
using SDG.Provider.Services.Translation;
using SDG.Provider.Services.Web;
using SDG.SteamworksProvider.Services.Achievements;
using SDG.SteamworksProvider.Services.Browser;
using SDG.SteamworksProvider.Services.Cloud;
using SDG.SteamworksProvider.Services.Community;
using SDG.SteamworksProvider.Services.Multiplayer;
using SDG.SteamworksProvider.Services.Statistics;
using SDG.SteamworksProvider.Services.Store;
using SDG.SteamworksProvider.Services.Translation;
using SDG.SteamworksProvider.Services.Web;
using Steamworks;

namespace SDG.SteamworksProvider
{
	// Token: 0x02000380 RID: 896
	public class SteamworksProvider : IProvider
	{
		// Token: 0x0600188E RID: 6286 RVA: 0x00089A84 File Offset: 0x00087E84
		public SteamworksProvider(SteamworksAppInfo newAppInfo)
		{
			this.appInfo = newAppInfo;
			this.constructServices();
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x00089A99 File Offset: 0x00087E99
		// (set) Token: 0x06001890 RID: 6288 RVA: 0x00089AA1 File Offset: 0x00087EA1
		public IAchievementsService achievementsService { get; protected set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x00089AAA File Offset: 0x00087EAA
		// (set) Token: 0x06001892 RID: 6290 RVA: 0x00089AB2 File Offset: 0x00087EB2
		public IBrowserService browserService { get; protected set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x00089ABB File Offset: 0x00087EBB
		// (set) Token: 0x06001894 RID: 6292 RVA: 0x00089AC3 File Offset: 0x00087EC3
		public ICloudService cloudService { get; protected set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x00089ACC File Offset: 0x00087ECC
		// (set) Token: 0x06001896 RID: 6294 RVA: 0x00089AD4 File Offset: 0x00087ED4
		public ICommunityService communityService { get; protected set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x00089ADD File Offset: 0x00087EDD
		// (set) Token: 0x06001898 RID: 6296 RVA: 0x00089AE5 File Offset: 0x00087EE5
		public TempSteamworksEconomy economyService { get; protected set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x00089AEE File Offset: 0x00087EEE
		// (set) Token: 0x0600189A RID: 6298 RVA: 0x00089AF6 File Offset: 0x00087EF6
		public TempSteamworksMatchmaking matchmakingService { get; protected set; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x00089AFF File Offset: 0x00087EFF
		// (set) Token: 0x0600189C RID: 6300 RVA: 0x00089B07 File Offset: 0x00087F07
		public IMultiplayerService multiplayerService { get; protected set; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x00089B10 File Offset: 0x00087F10
		// (set) Token: 0x0600189E RID: 6302 RVA: 0x00089B18 File Offset: 0x00087F18
		public IStatisticsService statisticsService { get; protected set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x00089B21 File Offset: 0x00087F21
		// (set) Token: 0x060018A0 RID: 6304 RVA: 0x00089B29 File Offset: 0x00087F29
		public IStoreService storeService { get; protected set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x00089B32 File Offset: 0x00087F32
		// (set) Token: 0x060018A2 RID: 6306 RVA: 0x00089B3A File Offset: 0x00087F3A
		public ITranslationService translationService { get; protected set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x00089B43 File Offset: 0x00087F43
		// (set) Token: 0x060018A4 RID: 6308 RVA: 0x00089B4B File Offset: 0x00087F4B
		public IWebService webService { get; protected set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x00089B54 File Offset: 0x00087F54
		// (set) Token: 0x060018A6 RID: 6310 RVA: 0x00089B5C File Offset: 0x00087F5C
		public TempSteamworksWorkshop workshopService { get; protected set; }

		// Token: 0x060018A7 RID: 6311 RVA: 0x00089B68 File Offset: 0x00087F68
		public void intialize()
		{
			if (!this.appInfo.isDedicated)
			{
				if (SteamAPI.RestartAppIfNecessary((AppId_t)this.appInfo.id))
				{
					throw new Exception("Restarting app from Steam.");
				}
				if (!SteamAPI.Init())
				{
					throw new Exception("Steam API initialization failed.");
				}
			}
			this.initializeServices();
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00089BC5 File Offset: 0x00087FC5
		public void update()
		{
			GameServer.RunCallbacks();
			if (!this.appInfo.isDedicated)
			{
				SteamAPI.RunCallbacks();
			}
			this.updateServices();
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00089BE7 File Offset: 0x00087FE7
		public void shutdown()
		{
			if (!this.appInfo.isDedicated)
			{
				SteamAPI.Shutdown();
			}
			this.shutdownServices();
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00089C04 File Offset: 0x00088004
		private void constructServices()
		{
			this.achievementsService = new SteamworksAchievementsService();
			this.economyService = new TempSteamworksEconomy(this.appInfo);
			this.multiplayerService = new SteamworksMultiplayerService(this.appInfo);
			this.statisticsService = new SteamworksStatisticsService();
			this.webService = new SteamworksWebService();
			this.workshopService = new TempSteamworksWorkshop(this.appInfo);
			if (!this.appInfo.isDedicated)
			{
				this.browserService = new SteamworksBrowserService();
				this.cloudService = new SteamworksCloudService();
				this.communityService = new SteamworksCommunityService();
				this.matchmakingService = new TempSteamworksMatchmaking();
				this.storeService = new SteamworksStoreService(this.appInfo);
				this.translationService = new SteamworksTranslationService();
			}
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00089CC0 File Offset: 0x000880C0
		private void initializeServices()
		{
			this.achievementsService.initialize();
			this.multiplayerService.initialize();
			this.statisticsService.initialize();
			this.webService.initialize();
			if (!this.appInfo.isDedicated)
			{
				this.browserService.initialize();
				this.cloudService.initialize();
				this.communityService.initialize();
				this.storeService.initialize();
				this.translationService.initialize();
			}
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00089D40 File Offset: 0x00088140
		private void updateServices()
		{
			this.achievementsService.update();
			this.multiplayerService.update();
			this.statisticsService.update();
			this.webService.update();
			this.workshopService.update();
			if (!this.appInfo.isDedicated)
			{
				this.browserService.update();
				this.cloudService.update();
				this.communityService.update();
				this.storeService.update();
				this.translationService.update();
			}
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00089DCC File Offset: 0x000881CC
		private void shutdownServices()
		{
			this.achievementsService.shutdown();
			this.multiplayerService.shutdown();
			this.statisticsService.shutdown();
			this.webService.shutdown();
			if (!this.appInfo.isDedicated)
			{
				this.browserService.shutdown();
				this.cloudService.shutdown();
				this.communityService.shutdown();
				this.storeService.shutdown();
				this.translationService.shutdown();
			}
		}

		// Token: 0x04000D0C RID: 3340
		private SteamworksAppInfo appInfo;
	}
}
