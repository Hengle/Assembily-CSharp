using System;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace SDG.Provider.Services.Community
{
	// Token: 0x0200031E RID: 798
	public interface ICommunityService : IService
	{
		// Token: 0x060016AF RID: 5807
		void setStatus(string status);

		// Token: 0x060016B0 RID: 5808
		Texture2D getIcon(int id);

		// Token: 0x060016B1 RID: 5809
		Texture2D getIcon(CSteamID steamID);

		// Token: 0x060016B2 RID: 5810
		SteamGroup getCachedGroup(CSteamID steamID);

		// Token: 0x060016B3 RID: 5811
		SteamGroup[] getGroups();

		// Token: 0x060016B4 RID: 5812
		bool checkGroup(CSteamID steamID);
	}
}
