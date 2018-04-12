using System;
using SDG.Provider.Services;
using SDG.Provider.Services.Community;
using SDG.Provider.Services.Statistics.User;
using SDG.SteamworksProvider.Services.Community;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Statistics.User
{
	// Token: 0x02000378 RID: 888
	public class SteamworksUserStatisticsService : Service, IUserStatisticsService, IService
	{
		// Token: 0x0600185E RID: 6238 RVA: 0x00089512 File Offset: 0x00087912
		public SteamworksUserStatisticsService()
		{
			this.userStatsReceivedCallback = Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate(this.onUserStatsReceived));
		}

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x0600185F RID: 6239 RVA: 0x00089534 File Offset: 0x00087934
		// (remove) Token: 0x06001860 RID: 6240 RVA: 0x0008956C File Offset: 0x0008796C
		public event UserStatisticsRequestReady onUserStatisticsRequestReady;

		// Token: 0x06001861 RID: 6241 RVA: 0x000895A2 File Offset: 0x000879A2
		private void triggerUserStatisticsRequestReady(ICommunityEntity entityID)
		{
			if (this.onUserStatisticsRequestReady != null)
			{
				this.onUserStatisticsRequestReady(entityID);
			}
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x000895BB File Offset: 0x000879BB
		public bool getStatistic(string name, out int data)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return SteamUserStats.GetStat(name, out data);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x000895D8 File Offset: 0x000879D8
		public bool setStatistic(string name, int data)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			bool result = SteamUserStats.SetStat(name, data);
			SteamUserStats.StoreStats();
			return result;
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x00089605 File Offset: 0x00087A05
		public bool getStatistic(string name, out float data)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return SteamUserStats.GetStat(name, out data);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00089620 File Offset: 0x00087A20
		public bool setStatistic(string name, float data)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			bool result = SteamUserStats.SetStat(name, data);
			SteamUserStats.StoreStats();
			return result;
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0008964D File Offset: 0x00087A4D
		public bool requestStatistics()
		{
			SteamUserStats.RequestCurrentStats();
			return true;
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x00089658 File Offset: 0x00087A58
		private void onUserStatsReceived(UserStatsReceived_t callback)
		{
			if (callback.m_nGameID != (ulong)SteamUtils.GetAppID().m_AppId)
			{
				return;
			}
			SteamworksCommunityEntity entityID = new SteamworksCommunityEntity(callback.m_steamIDUser);
			this.triggerUserStatisticsRequestReady(entityID);
		}

		// Token: 0x04000CF4 RID: 3316
		private Callback<UserStatsReceived_t> userStatsReceivedCallback;
	}
}
