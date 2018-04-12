using System;
using SDG.Provider.Services;
using SDG.Provider.Services.Achievements;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Achievements
{
	// Token: 0x02000365 RID: 869
	public class SteamworksAchievementsService : Service, IAchievementsService, IService
	{
		// Token: 0x060017CD RID: 6093 RVA: 0x0008840E File Offset: 0x0008680E
		public bool getAchievement(string name, out bool has)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return SteamUserStats.GetAchievement(name, out has);
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00088428 File Offset: 0x00086828
		public bool setAchievement(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			bool result = SteamUserStats.SetAchievement(name);
			SteamUserStats.StoreStats();
			return result;
		}
	}
}
