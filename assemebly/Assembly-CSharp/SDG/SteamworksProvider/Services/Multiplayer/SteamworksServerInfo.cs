using System;
using SDG.Provider.Services.Community;
using SDG.Provider.Services.Multiplayer;
using SDG.SteamworksProvider.Services.Community;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Multiplayer
{
	// Token: 0x02000375 RID: 885
	public class SteamworksServerInfo : IServerInfo
	{
		// Token: 0x06001843 RID: 6211 RVA: 0x000892CC File Offset: 0x000876CC
		public SteamworksServerInfo(gameserveritem_t server)
		{
			this.entity = new SteamworksCommunityEntity(server.m_steamID);
			this.name = server.GetServerName();
			this.players = (byte)server.m_nPlayers;
			this.capacity = (byte)server.m_nMaxPlayers;
			this.ping = server.m_nPing;
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x00089322 File Offset: 0x00087722
		// (set) Token: 0x06001845 RID: 6213 RVA: 0x0008932A File Offset: 0x0008772A
		public ICommunityEntity entity { get; protected set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x00089333 File Offset: 0x00087733
		// (set) Token: 0x06001847 RID: 6215 RVA: 0x0008933B File Offset: 0x0008773B
		public string name { get; protected set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x00089344 File Offset: 0x00087744
		// (set) Token: 0x06001849 RID: 6217 RVA: 0x0008934C File Offset: 0x0008774C
		public byte players { get; protected set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x00089355 File Offset: 0x00087755
		// (set) Token: 0x0600184B RID: 6219 RVA: 0x0008935D File Offset: 0x0008775D
		public byte capacity { get; protected set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x00089366 File Offset: 0x00087766
		// (set) Token: 0x0600184D RID: 6221 RVA: 0x0008936E File Offset: 0x0008776E
		public int ping { get; protected set; }
	}
}
