using System;
using SDG.Provider.Services.Matchmaking;
using SDG.Provider.Services.Multiplayer;
using SDG.SteamworksProvider.Services.Multiplayer;

namespace SDG.SteamworksProvider.Services.Matchmaking
{
	// Token: 0x02000371 RID: 881
	public class SteamworksServerInfoRequestResult : IServerInfoRequestResult
	{
		// Token: 0x06001811 RID: 6161 RVA: 0x00088D28 File Offset: 0x00087128
		public SteamworksServerInfoRequestResult(SteamworksServerInfo newServerInfo)
		{
			this.serverInfo = newServerInfo;
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x00088D37 File Offset: 0x00087137
		// (set) Token: 0x06001813 RID: 6163 RVA: 0x00088D3F File Offset: 0x0008713F
		public IServerInfo serverInfo { get; protected set; }
	}
}
