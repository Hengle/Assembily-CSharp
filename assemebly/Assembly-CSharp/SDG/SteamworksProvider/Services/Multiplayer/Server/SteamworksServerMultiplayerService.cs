using System;
using System.IO;
using SDG.Provider.Services;
using SDG.Provider.Services.Community;
using SDG.Provider.Services.Multiplayer;
using SDG.Provider.Services.Multiplayer.Server;
using SDG.SteamworksProvider.Services.Community;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Multiplayer.Server
{
	// Token: 0x02000373 RID: 883
	public class SteamworksServerMultiplayerService : Service, IServerMultiplayerService, IService
	{
		// Token: 0x06001827 RID: 6183 RVA: 0x00088E9C File Offset: 0x0008729C
		public SteamworksServerMultiplayerService(SteamworksAppInfo newAppInfo)
		{
			this.appInfo = newAppInfo;
			this.buffer = new byte[1024];
			this.stream = new MemoryStream(this.buffer);
			this.reader = new BinaryReader(this.stream);
			this.writer = new BinaryWriter(this.stream);
			SteamworksServerMultiplayerService.p2pSessionRequest = Callback<P2PSessionRequest_t>.CreateGameServer(new Callback<P2PSessionRequest_t>.DispatchDelegate(this.onP2PSessionRequest));
			SteamworksServerMultiplayerService.steamServersConnected = Callback<SteamServersConnected_t>.CreateGameServer(new Callback<SteamServersConnected_t>.DispatchDelegate(this.onSteamServersConnected));
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x00088F25 File Offset: 0x00087325
		// (set) Token: 0x06001829 RID: 6185 RVA: 0x00088F2D File Offset: 0x0008732D
		public IServerInfo serverInfo { get; protected set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x00088F36 File Offset: 0x00087336
		// (set) Token: 0x0600182B RID: 6187 RVA: 0x00088F3E File Offset: 0x0008733E
		public bool isHosting { get; protected set; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x00088F47 File Offset: 0x00087347
		// (set) Token: 0x0600182D RID: 6189 RVA: 0x00088F4F File Offset: 0x0008734F
		public MemoryStream stream { get; protected set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x00088F58 File Offset: 0x00087358
		// (set) Token: 0x0600182F RID: 6191 RVA: 0x00088F60 File Offset: 0x00087360
		public BinaryReader reader { get; protected set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001830 RID: 6192 RVA: 0x00088F69 File Offset: 0x00087369
		// (set) Token: 0x06001831 RID: 6193 RVA: 0x00088F71 File Offset: 0x00087371
		public BinaryWriter writer { get; protected set; }

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x06001832 RID: 6194 RVA: 0x00088F7C File Offset: 0x0008737C
		// (remove) Token: 0x06001833 RID: 6195 RVA: 0x00088FB4 File Offset: 0x000873B4
		public event ServerMultiplayerServiceReadyHandler ready;

		// Token: 0x06001834 RID: 6196 RVA: 0x00088FEC File Offset: 0x000873EC
		public void open(uint ip, ushort port, ESecurityMode security)
		{
			if (this.isHosting)
			{
				return;
			}
			EServerMode eServerMode = EServerMode.eServerModeInvalid;
			if (security != ESecurityMode.LAN)
			{
				if (security != ESecurityMode.SECURE)
				{
					if (security == ESecurityMode.INSECURE)
					{
						eServerMode = EServerMode.eServerModeAuthentication;
					}
				}
				else
				{
					eServerMode = EServerMode.eServerModeAuthenticationAndSecure;
				}
			}
			else
			{
				eServerMode = EServerMode.eServerModeNoAuthentication;
			}
			if (!GameServer.Init(ip, port + 2, port, port + 1, eServerMode, "1.0.0.0"))
			{
				throw new Exception("GameServer API initialization failed!");
			}
			SteamGameServer.SetDedicatedServer(this.appInfo.isDedicated);
			SteamGameServer.SetGameDescription(this.appInfo.name);
			SteamGameServer.SetProduct(this.appInfo.name);
			SteamGameServer.SetModDir(this.appInfo.name);
			SteamGameServer.LogOnAnonymous();
			SteamGameServer.EnableHeartbeats(true);
			this.isHosting = true;
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x000890AC File Offset: 0x000874AC
		public void close()
		{
			if (!this.isHosting)
			{
				return;
			}
			SteamGameServer.EnableHeartbeats(false);
			SteamGameServer.LogOff();
			GameServer.Shutdown();
			this.isHosting = false;
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x000890D4 File Offset: 0x000874D4
		public bool read(out ICommunityEntity entity, byte[] data, out ulong length, int channel)
		{
			entity = SteamworksCommunityEntity.INVALID;
			length = 0UL;
			uint num;
			if (!SteamGameServerNetworking.IsP2PPacketAvailable(out num, channel) || (ulong)num > (ulong)((long)data.Length))
			{
				return false;
			}
			CSteamID newSteamID;
			if (!SteamGameServerNetworking.ReadP2PPacket(data, num, out num, out newSteamID, channel))
			{
				return false;
			}
			entity = new SteamworksCommunityEntity(newSteamID);
			length = (ulong)num;
			return true;
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00089128 File Offset: 0x00087528
		public void write(ICommunityEntity entity, byte[] data, ulong length)
		{
			SteamworksCommunityEntity steamworksCommunityEntity = (SteamworksCommunityEntity)entity;
			CSteamID steamID = steamworksCommunityEntity.steamID;
			SteamGameServerNetworking.SendP2PPacket(steamID, data, (uint)length, EP2PSend.k_EP2PSendUnreliable, 0);
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00089150 File Offset: 0x00087550
		public void write(ICommunityEntity entity, byte[] data, ulong length, ESendMethod method, int channel)
		{
			SteamworksCommunityEntity steamworksCommunityEntity = (SteamworksCommunityEntity)entity;
			CSteamID steamID = steamworksCommunityEntity.steamID;
			switch (method)
			{
			case ESendMethod.RELIABLE:
				SteamGameServerNetworking.SendP2PPacket(steamID, data, (uint)length, EP2PSend.k_EP2PSendReliableWithBuffering, channel);
				return;
			case ESendMethod.RELIABLE_NODELAY:
				SteamGameServerNetworking.SendP2PPacket(steamID, data, (uint)length, EP2PSend.k_EP2PSendReliable, channel);
				return;
			case ESendMethod.UNRELIABLE:
				SteamGameServerNetworking.SendP2PPacket(steamID, data, (uint)length, EP2PSend.k_EP2PSendUnreliable, channel);
				return;
			case ESendMethod.UNRELIABLE_NODELAY:
				SteamGameServerNetworking.SendP2PPacket(steamID, data, (uint)length, EP2PSend.k_EP2PSendUnreliableNoDelay, channel);
				return;
			default:
				return;
			}
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x000891C0 File Offset: 0x000875C0
		private void onP2PSessionRequest(P2PSessionRequest_t callback)
		{
			CSteamID steamIDRemote = callback.m_steamIDRemote;
			SteamGameServerNetworking.AcceptP2PSessionWithUser(steamIDRemote);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x000891DC File Offset: 0x000875DC
		private void onSteamServersConnected(SteamServersConnected_t callback)
		{
			if (this.ready != null)
			{
				this.ready();
			}
		}

		// Token: 0x04000CE3 RID: 3299
		private byte[] buffer;

		// Token: 0x04000CE4 RID: 3300
		private SteamworksAppInfo appInfo;

		// Token: 0x04000CE5 RID: 3301
		private static Callback<P2PSessionRequest_t> p2pSessionRequest;

		// Token: 0x04000CE6 RID: 3302
		private static Callback<SteamServersConnected_t> steamServersConnected;
	}
}
