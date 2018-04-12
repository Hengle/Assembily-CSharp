using System;
using SDG.Provider.Services.Statistics.Global;
using SDG.Provider.Services.Statistics.User;

namespace SDG.Provider.Services.Statistics
{
	// Token: 0x02000340 RID: 832
	public interface IStatisticsService : IService
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001717 RID: 5911
		IUserStatisticsService userStatisticsService { get; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001718 RID: 5912
		IGlobalStatisticsService globalStatisticsService { get; }
	}
}
