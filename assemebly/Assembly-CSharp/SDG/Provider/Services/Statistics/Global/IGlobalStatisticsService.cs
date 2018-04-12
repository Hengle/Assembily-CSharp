using System;

namespace SDG.Provider.Services.Statistics.Global
{
	// Token: 0x0200033F RID: 831
	public interface IGlobalStatisticsService : IService
	{
		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06001712 RID: 5906
		// (remove) Token: 0x06001713 RID: 5907
		event GlobalStatisticsRequestReady onGlobalStatisticsRequestReady;

		// Token: 0x06001714 RID: 5908
		bool getStatistic(string name, out long data);

		// Token: 0x06001715 RID: 5909
		bool getStatistic(string name, out double data);

		// Token: 0x06001716 RID: 5910
		bool requestStatistics();
	}
}
