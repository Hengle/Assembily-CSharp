using System;
using SDG.Provider.Services.Multiplayer.Client;
using SDG.Provider.Services.Multiplayer.Server;

namespace SDG.Provider.Services.Multiplayer
{
	// Token: 0x0200032C RID: 812
	public interface IMultiplayerService : IService
	{
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060016D6 RID: 5846
		IClientMultiplayerService clientMultiplayerService { get; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060016D7 RID: 5847
		IServerMultiplayerService serverMultiplayerService { get; }
	}
}
