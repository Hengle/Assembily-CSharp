using System;
using SDG.Provider.Services;
using SDG.Provider.Services.Statistics.Global;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Statistics.Global
{
	// Token: 0x02000376 RID: 886
	public class SteamworksGlobalStatisticsService : Service, IGlobalStatisticsService, IService
	{
		// Token: 0x0600184E RID: 6222 RVA: 0x00089377 File Offset: 0x00087777
		public SteamworksGlobalStatisticsService()
		{
			this.globalStatsReceived = Callback<GlobalStatsReceived_t>.Create(new Callback<GlobalStatsReceived_t>.DispatchDelegate(this.onGlobalStatsReceived));
		}

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x0600184F RID: 6223 RVA: 0x00089398 File Offset: 0x00087798
		// (remove) Token: 0x06001850 RID: 6224 RVA: 0x000893D0 File Offset: 0x000877D0
		public event GlobalStatisticsRequestReady onGlobalStatisticsRequestReady;

		// Token: 0x06001851 RID: 6225 RVA: 0x00089406 File Offset: 0x00087806
		private void triggerGlobalStatisticsRequestReady()
		{
			if (this.onGlobalStatisticsRequestReady != null)
			{
				this.onGlobalStatisticsRequestReady();
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0008941E File Offset: 0x0008781E
		public bool getStatistic(string name, out long data)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return SteamUserStats.GetGlobalStat(name, out data);
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00089438 File Offset: 0x00087838
		public bool getStatistic(string name, out double data)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return SteamUserStats.GetGlobalStat(name, out data);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00089452 File Offset: 0x00087852
		public bool requestStatistics()
		{
			SteamUserStats.RequestGlobalStats(0);
			return true;
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0008945C File Offset: 0x0008785C
		private void onGlobalStatsReceived(GlobalStatsReceived_t callback)
		{
			if (callback.m_nGameID != (ulong)SteamUtils.GetAppID().m_AppId)
			{
				return;
			}
			this.triggerGlobalStatisticsRequestReady();
		}

		// Token: 0x04000CF0 RID: 3312
		private Callback<GlobalStatsReceived_t> globalStatsReceived;
	}
}
