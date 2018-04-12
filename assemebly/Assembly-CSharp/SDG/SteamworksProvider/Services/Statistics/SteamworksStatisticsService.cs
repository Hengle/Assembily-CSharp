using System;
using SDG.Provider.Services;
using SDG.Provider.Services.Statistics;
using SDG.Provider.Services.Statistics.Global;
using SDG.Provider.Services.Statistics.User;
using SDG.SteamworksProvider.Services.Statistics.Global;
using SDG.SteamworksProvider.Services.Statistics.User;

namespace SDG.SteamworksProvider.Services.Statistics
{
	// Token: 0x02000377 RID: 887
	public class SteamworksStatisticsService : IStatisticsService, IService
	{
		// Token: 0x06001856 RID: 6230 RVA: 0x0008948A File Offset: 0x0008788A
		public SteamworksStatisticsService()
		{
			this.userStatisticsService = new SteamworksUserStatisticsService();
			this.globalStatisticsService = new SteamworksGlobalStatisticsService();
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x000894A8 File Offset: 0x000878A8
		// (set) Token: 0x06001858 RID: 6232 RVA: 0x000894B0 File Offset: 0x000878B0
		public IUserStatisticsService userStatisticsService { get; protected set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x000894B9 File Offset: 0x000878B9
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x000894C1 File Offset: 0x000878C1
		public IGlobalStatisticsService globalStatisticsService { get; protected set; }

		// Token: 0x0600185B RID: 6235 RVA: 0x000894CA File Offset: 0x000878CA
		public void initialize()
		{
			this.userStatisticsService.initialize();
			this.globalStatisticsService.initialize();
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x000894E2 File Offset: 0x000878E2
		public void update()
		{
			this.userStatisticsService.update();
			this.globalStatisticsService.update();
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x000894FA File Offset: 0x000878FA
		public void shutdown()
		{
			this.userStatisticsService.shutdown();
			this.globalStatisticsService.shutdown();
		}
	}
}
