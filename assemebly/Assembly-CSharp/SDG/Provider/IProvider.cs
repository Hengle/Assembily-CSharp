using System;
using SDG.Provider.Services.Achievements;
using SDG.Provider.Services.Browser;
using SDG.Provider.Services.Cloud;
using SDG.Provider.Services.Community;
using SDG.Provider.Services.Multiplayer;
using SDG.Provider.Services.Statistics;
using SDG.Provider.Services.Store;
using SDG.Provider.Services.Translation;
using SDG.Provider.Services.Web;

namespace SDG.Provider
{
	// Token: 0x02000319 RID: 793
	public interface IProvider
	{
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001696 RID: 5782
		IAchievementsService achievementsService { get; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001697 RID: 5783
		IBrowserService browserService { get; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001698 RID: 5784
		ICloudService cloudService { get; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001699 RID: 5785
		ICommunityService communityService { get; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600169A RID: 5786
		TempSteamworksEconomy economyService { get; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600169B RID: 5787
		TempSteamworksMatchmaking matchmakingService { get; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600169C RID: 5788
		IMultiplayerService multiplayerService { get; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600169D RID: 5789
		IStatisticsService statisticsService { get; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600169E RID: 5790
		IStoreService storeService { get; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600169F RID: 5791
		ITranslationService translationService { get; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060016A0 RID: 5792
		IWebService webService { get; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060016A1 RID: 5793
		TempSteamworksWorkshop workshopService { get; }

		// Token: 0x060016A2 RID: 5794
		void intialize();

		// Token: 0x060016A3 RID: 5795
		void update();

		// Token: 0x060016A4 RID: 5796
		void shutdown();
	}
}
