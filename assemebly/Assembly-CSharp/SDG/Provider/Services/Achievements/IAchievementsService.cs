using System;

namespace SDG.Provider.Services.Achievements
{
	// Token: 0x0200031A RID: 794
	public interface IAchievementsService : IService
	{
		// Token: 0x060016A5 RID: 5797
		bool getAchievement(string name, out bool has);

		// Token: 0x060016A6 RID: 5798
		bool setAchievement(string name);
	}
}
