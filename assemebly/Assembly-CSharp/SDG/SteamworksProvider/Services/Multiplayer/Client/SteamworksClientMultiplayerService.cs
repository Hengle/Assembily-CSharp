using System;
using System.IO;
using SDG.Provider.Services;
using SDG.Provider.Services.Community;
using SDG.Provider.Services.Multiplayer;
using SDG.Provider.Services.Multiplayer.Client;
using SDG.SteamworksProvider.Services.Community;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Multiplayer.Client
{
	// Token: 0x02000372 RID: 882
	public class SteamworksClientMultiplayerService : Service, IClientMultiplayerService, IService
	{
		// Token: 0x06001814 RID: 6164 RVA: 0x00088D48 File Offset: 0x00087148
		public SteamworksClientMultiplayerService()
		{
			this.buffer = new byte[1024];
			this.stream = new MemoryStream(this.buffer);
			this.reader = new BinaryReader(this.stream);
			this.writer = new BinaryWriter(this.stream);
			SteamworksClientMultiplayerService.p2pSessionRequest = Callback<P2PSessionRequest_t>.Create(new Callback<P2PSessionRequest_t>.DispatchDelegate(this.onP2PSessionRequest));
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x00088DB4 File Offset: 0x000871B4
		// (set) Token: 0x06001816 RID: 6166 RVA: 0x00088DBC File Offset: 0x000871BC
		public IServerInfo serverInfo { get; protected set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x00088DC5 File Offset: 0x000871C5
		// (set) Token: 0x06001818 RID: 6168 RVA: 0x00088DCD File Offset: 0x000871CD
		public bool isConnected { get; protected set; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x00088DD6 File Offset: 0x000871D6
		// (set) Token: 0x0600181A RID: 6170 RVA: 0x00088DDE File Offset: 0x000871DE
		public bool isAttempting { get; protected set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x00088DE7 File Offset: 0x000871E7
		// (set) Token: 0x0600181C RID: 6172 RVA: 0x00088DEF File Offset: 0x000871EF
		public MemoryStream stream { get; protected set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x00088DF8 File Offset: 0x000871F8
		// (set) Token: 0x0600181E RID: 6174 RVA: 0x00088E00 File Offset: 0x00087200
		public BinaryReader reader { get; protected set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x00088E09 File Offset: 0x00087209
		// (set) Token: 0x06001820 RID: 6176 RVA: 0x00088E11 File Offset: 0x00087211
		public BinaryWriter writer { get; protected set; }

		// Token: 0x06001821 RID: 6177 RVA: 0x00088E1A File Offset: 0x0008721A
		public void connect(IServerInfo newServerInfo)
		{
			this.serverInfo = newServerInfo;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00088E23 File Offset: 0x00087223
		public void disconnect()
		{
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00088E28 File Offset: 0x00087228
		public bool read(out ICommunityEntity entity, byte[] data, out ulong length, int channel)
		{
			entity = SteamworksCommunityEntity.INVALID;
			length = 0UL;
			uint num;
			if (!SteamNetworking.IsP2PPacketAvailable(out num, channel) || (ulong)num > (ulong)((long)data.Length))
			{
				return false;
			}
			CSteamID newSteamID;
			if (!SteamNetworking.ReadP2PPacket(data, num, out num, out newSteamID, channel))
			{
				return false;
			}
			entity = new SteamworksCommunityEntity(newSteamID);
			length = (ulong)num;
			return true;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00088E7C File Offset: 0x0008727C
		public void write(ICommunityEntity entity, byte[] data, ulong length)
		{
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00088E7E File Offset: 0x0008727E
		public void write(ICommunityEntity entity, byte[] data, ulong length, ESendMethod method, int channel)
		{
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00088E80 File Offset: 0x00087280
		private void onP2PSessionRequest(P2PSessionRequest_t callback)
		{
			CSteamID steamIDRemote = callback.m_steamIDRemote;
			SteamNetworking.AcceptP2PSessionWithUser(steamIDRemote);
		}

		// Token: 0x04000CDB RID: 3291
		private byte[] buffer;

		// Token: 0x04000CDC RID: 3292
		private static Callback<P2PSessionRequest_t> p2pSessionRequest;
	}
}
