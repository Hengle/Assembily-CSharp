using System;
using SDG.Provider.Services.Matchmaking;
using SDG.SteamworksProvider.Services.Multiplayer;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Matchmaking
{
	// Token: 0x02000370 RID: 880
	public class SteamworksServerInfoRequestHandle : IServerInfoRequestHandle
	{
		// Token: 0x0600180C RID: 6156 RVA: 0x00088C84 File Offset: 0x00087084
		public SteamworksServerInfoRequestHandle(ServerInfoRequestReadyCallback newCallback)
		{
			this.callback = newCallback;
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x00088C94 File Offset: 0x00087094
		public void onServerResponded(gameserveritem_t server)
		{
			SteamworksServerInfo newServerInfo = new SteamworksServerInfo(server);
			SteamworksServerInfoRequestResult result = new SteamworksServerInfoRequestResult(newServerInfo);
			this.triggerCallback(result);
			this.cleanupQuery();
			SteamworksMatchmakingService.serverInfoRequestHandles.Remove(this);
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00088CC8 File Offset: 0x000870C8
		public void onServerFailedToRespond()
		{
			SteamworksServerInfoRequestResult result = new SteamworksServerInfoRequestResult(null);
			this.triggerCallback(result);
			this.cleanupQuery();
			SteamworksMatchmakingService.serverInfoRequestHandles.Remove(this);
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00088CF5 File Offset: 0x000870F5
		public void triggerCallback(IServerInfoRequestResult result)
		{
			if (this.callback == null)
			{
				return;
			}
			this.callback(this, result);
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x00088D10 File Offset: 0x00087110
		private void cleanupQuery()
		{
			SteamMatchmakingServers.CancelServerQuery(this.query);
			this.query = HServerQuery.Invalid;
		}

		// Token: 0x04000CD1 RID: 3281
		public HServerQuery query;

		// Token: 0x04000CD2 RID: 3282
		public ISteamMatchmakingPingResponse pingResponse;

		// Token: 0x04000CD3 RID: 3283
		private ServerInfoRequestReadyCallback callback;
	}
}
