using System;

namespace SDG.Provider.Services.Statistics.User
{
	// Token: 0x02000341 RID: 833
	public interface IUserStatisticsService : IService
	{
		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06001719 RID: 5913
		// (remove) Token: 0x0600171A RID: 5914
		event UserStatisticsRequestReady onUserStatisticsRequestReady;

		// Token: 0x0600171B RID: 5915
		bool getStatistic(string name, out int data);

		// Token: 0x0600171C RID: 5916
		bool setStatistic(string name, int data);

		// Token: 0x0600171D RID: 5917
		bool getStatistic(string name, out float data);

		// Token: 0x0600171E RID: 5918
		bool setStatistic(string name, float data);

		// Token: 0x0600171F RID: 5919
		bool requestStatistics();
	}
}
