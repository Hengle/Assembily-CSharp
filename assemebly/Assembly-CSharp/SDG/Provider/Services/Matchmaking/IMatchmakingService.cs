using System;

namespace SDG.Provider.Services.Matchmaking
{
	// Token: 0x0200032E RID: 814
	public interface IMatchmakingService : IService
	{
		// Token: 0x060016DD RID: 5853
		IServerInfoRequestHandle requestServerInfo(uint ip, ushort port, ServerInfoRequestReadyCallback callback);
	}
}
