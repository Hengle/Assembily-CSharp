using System;
using SDG.Provider.Services.Community;

namespace SDG.Provider.Services.Multiplayer
{
	// Token: 0x0200032D RID: 813
	public interface IServerInfo
	{
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060016D8 RID: 5848
		ICommunityEntity entity { get; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060016D9 RID: 5849
		string name { get; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060016DA RID: 5850
		byte players { get; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060016DB RID: 5851
		byte capacity { get; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060016DC RID: 5852
		int ping { get; }
	}
}
