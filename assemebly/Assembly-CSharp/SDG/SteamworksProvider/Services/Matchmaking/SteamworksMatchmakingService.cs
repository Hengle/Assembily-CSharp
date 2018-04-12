using System;
using System.Collections.Generic;
using SDG.Provider.Services;
using SDG.Provider.Services.Matchmaking;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Matchmaking
{
	// Token: 0x0200036F RID: 879
	public class SteamworksMatchmakingService : Service, IMatchmakingService, IService
	{
		// Token: 0x0600180B RID: 6155 RVA: 0x00088C2C File Offset: 0x0008702C
		public IServerInfoRequestHandle requestServerInfo(uint ip, ushort port, ServerInfoRequestReadyCallback callback)
		{
			SteamworksServerInfoRequestHandle steamworksServerInfoRequestHandle = new SteamworksServerInfoRequestHandle(callback);
			ISteamMatchmakingPingResponse steamMatchmakingPingResponse = new ISteamMatchmakingPingResponse(new ISteamMatchmakingPingResponse.ServerResponded(steamworksServerInfoRequestHandle.onServerResponded), new ISteamMatchmakingPingResponse.ServerFailedToRespond(steamworksServerInfoRequestHandle.onServerFailedToRespond));
			steamworksServerInfoRequestHandle.pingResponse = steamMatchmakingPingResponse;
			HServerQuery query = SteamMatchmakingServers.PingServer(ip, port + 1, steamMatchmakingPingResponse);
			steamworksServerInfoRequestHandle.query = query;
			SteamworksMatchmakingService.serverInfoRequestHandles.Add(steamworksServerInfoRequestHandle);
			return steamworksServerInfoRequestHandle;
		}

		// Token: 0x04000CD0 RID: 3280
		public static List<SteamworksServerInfoRequestHandle> serverInfoRequestHandles;
	}
}
