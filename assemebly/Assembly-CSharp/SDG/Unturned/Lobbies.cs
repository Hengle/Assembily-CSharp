using System;
using System.Runtime.CompilerServices;
using SDG.Framework.Debug;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200065C RID: 1628
	public static class Lobbies
	{
		// Token: 0x06002ECC RID: 11980 RVA: 0x00131DEC File Offset: 0x001301EC
		static Lobbies()
		{
			if (Lobbies.<>f__mg$cache0 == null)
			{
				Lobbies.<>f__mg$cache0 = new CallResult<LobbyCreated_t>.APIDispatchDelegate(Lobbies.onLobbyCreated);
			}
			Lobbies.lobbyCreated = CallResult<LobbyCreated_t>.Create(Lobbies.<>f__mg$cache0);
			if (Lobbies.<>f__mg$cache1 == null)
			{
				Lobbies.<>f__mg$cache1 = new Callback<LobbyEnter_t>.DispatchDelegate(Lobbies.onLobbyEnter);
			}
			Lobbies.lobbyEnter = Callback<LobbyEnter_t>.Create(Lobbies.<>f__mg$cache1);
			if (Lobbies.<>f__mg$cache2 == null)
			{
				Lobbies.<>f__mg$cache2 = new Callback<GameLobbyJoinRequested_t>.DispatchDelegate(Lobbies.onGameLobbyJoinRequested);
			}
			Lobbies.gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(Lobbies.<>f__mg$cache2);
			if (Lobbies.<>f__mg$cache3 == null)
			{
				Lobbies.<>f__mg$cache3 = new Callback<PersonaStateChange_t>.DispatchDelegate(Lobbies.onPersonaStateChange);
			}
			Lobbies.personaStateChange = Callback<PersonaStateChange_t>.Create(Lobbies.<>f__mg$cache3);
			if (Lobbies.<>f__mg$cache4 == null)
			{
				Lobbies.<>f__mg$cache4 = new Callback<LobbyGameCreated_t>.DispatchDelegate(Lobbies.onLobbyGameCreated);
			}
			Lobbies.lobbyGameCreated = Callback<LobbyGameCreated_t>.Create(Lobbies.<>f__mg$cache4);
			if (Lobbies.<>f__mg$cache5 == null)
			{
				Lobbies.<>f__mg$cache5 = new Callback<LobbyChatUpdate_t>.DispatchDelegate(Lobbies.onLobbyChatUpdate);
			}
			Lobbies.lobbyChatUpdate = Callback<LobbyChatUpdate_t>.Create(Lobbies.<>f__mg$cache5);
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x00131EE3 File Offset: 0x001302E3
		public static bool canOpenInvitations
		{
			get
			{
				return SteamUtils.IsOverlayEnabled();
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06002ECE RID: 11982 RVA: 0x00131EEA File Offset: 0x001302EA
		// (set) Token: 0x06002ECF RID: 11983 RVA: 0x00131EF1 File Offset: 0x001302F1
		public static bool isHost { get; private set; }

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002ED0 RID: 11984 RVA: 0x00131EF9 File Offset: 0x001302F9
		// (set) Token: 0x06002ED1 RID: 11985 RVA: 0x00131F00 File Offset: 0x00130300
		public static bool inLobby { get; private set; }

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002ED2 RID: 11986 RVA: 0x00131F08 File Offset: 0x00130308
		// (set) Token: 0x06002ED3 RID: 11987 RVA: 0x00131F0F File Offset: 0x0013030F
		public static CSteamID currentLobby { get; private set; }

		// Token: 0x06002ED4 RID: 11988 RVA: 0x00131F18 File Offset: 0x00130318
		private static void onLobbyCreated(LobbyCreated_t callback, bool io)
		{
			Terminal.print(string.Concat(new object[]
			{
				"Lobby created: ",
				callback.m_eResult,
				" ",
				callback.m_ulSteamIDLobby,
				" ",
				io
			}), null, Provider.STEAM_IC, Provider.STEAM_DC, true);
			Lobbies.isHost = true;
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x00131F88 File Offset: 0x00130388
		private static void onLobbyEnter(LobbyEnter_t callback)
		{
			Terminal.print(string.Concat(new object[]
			{
				"Lobby entered: ",
				callback.m_bLocked,
				" ",
				callback.m_ulSteamIDLobby,
				" ",
				callback.m_EChatRoomEnterResponse,
				" ",
				callback.m_rgfChatPermissions
			}), null, Provider.STEAM_IC, Provider.STEAM_DC, true);
			Lobbies.inLobby = true;
			Lobbies.currentLobby = new CSteamID(callback.m_ulSteamIDLobby);
			Lobbies.triggerLobbiesRefreshed();
			Lobbies.triggerLobbiesEntered();
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x00132030 File Offset: 0x00130430
		private static void onGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
		{
			Terminal.print(string.Concat(new object[]
			{
				"Lobby join requested: ",
				callback.m_steamIDLobby,
				" ",
				callback.m_steamIDFriend
			}), null, Provider.STEAM_IC, Provider.STEAM_DC, true);
			if (Provider.isConnected)
			{
				return;
			}
			Lobbies.joinLobby(callback.m_steamIDLobby);
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x0013209E File Offset: 0x0013049E
		private static void onPersonaStateChange(PersonaStateChange_t callback)
		{
			if (Lobbies.currentLobby == CSteamID.Nil)
			{
				return;
			}
			Lobbies.triggerLobbiesRefreshed();
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x001320BC File Offset: 0x001304BC
		private static void onLobbyGameCreated(LobbyGameCreated_t callback)
		{
			Terminal.print(string.Concat(new object[]
			{
				"Lobby game created: ",
				callback.m_ulSteamIDLobby,
				" ",
				callback.m_unIP,
				" ",
				callback.m_usPort,
				" ",
				callback.m_ulSteamIDGameServer
			}), null, Provider.STEAM_IC, Provider.STEAM_DC, true);
			Provider.provider.matchmakingService.connect(new SteamConnectionInfo(callback.m_unIP, callback.m_usPort, string.Empty));
			Provider.provider.matchmakingService.autoJoinServerQuery = true;
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x00132178 File Offset: 0x00130578
		private static void onLobbyChatUpdate(LobbyChatUpdate_t callback)
		{
			Terminal.print(string.Concat(new object[]
			{
				"Lobby chat update: ",
				callback.m_ulSteamIDLobby,
				" ",
				callback.m_ulSteamIDMakingChange,
				" ",
				callback.m_ulSteamIDUserChanged,
				" ",
				callback.m_rgfChatMemberStateChange
			}), null, Provider.STEAM_IC, Provider.STEAM_DC, true);
			Lobbies.triggerLobbiesRefreshed();
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x00132204 File Offset: 0x00130604
		private static void triggerLobbiesRefreshed()
		{
			Provider.updateRichPresence();
			LobbiesRefreshedHandler lobbiesRefreshedHandler = Lobbies.lobbiesRefreshed;
			if (lobbiesRefreshedHandler != null)
			{
				lobbiesRefreshedHandler();
			}
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x00132228 File Offset: 0x00130628
		private static void triggerLobbiesEntered()
		{
			LobbiesEnteredHandler lobbiesEnteredHandler = Lobbies.lobbiesEntered;
			if (lobbiesEnteredHandler != null)
			{
				lobbiesEnteredHandler();
			}
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x00132248 File Offset: 0x00130648
		public static void createLobby()
		{
			Terminal.print("Create lobby", null, Provider.STEAM_IC, Provider.STEAM_DC, true);
			SteamAPICall_t hAPICall = SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePrivate, 24);
			Lobbies.lobbyCreated.Set(hAPICall, null);
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x00132280 File Offset: 0x00130680
		public static void joinLobby(CSteamID newLobby)
		{
			if (Lobbies.inLobby)
			{
				Lobbies.leaveLobby();
			}
			Terminal.print("Join lobby: " + newLobby, null, Provider.STEAM_IC, Provider.STEAM_DC, true);
			SteamMatchmaking.JoinLobby(newLobby);
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x001322BC File Offset: 0x001306BC
		public static void linkLobby(uint ip, ushort port)
		{
			if (!Lobbies.isHost)
			{
				return;
			}
			Terminal.print(string.Concat(new object[]
			{
				"Link lobby: ",
				ip,
				" ",
				port
			}), null, Provider.STEAM_IC, Provider.STEAM_DC, true);
			SteamMatchmaking.SetLobbyGameServer(Lobbies.currentLobby, ip, port, CSteamID.Nil);
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x00132323 File Offset: 0x00130723
		public static void leaveLobby()
		{
			if (!Lobbies.inLobby)
			{
				return;
			}
			Terminal.print("Leave lobby", null, Provider.STEAM_IC, Provider.STEAM_DC, true);
			Lobbies.isHost = false;
			Lobbies.inLobby = false;
			SteamMatchmaking.LeaveLobby(Lobbies.currentLobby);
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x0013235C File Offset: 0x0013075C
		public static int getLobbyMemberCount()
		{
			return SteamMatchmaking.GetNumLobbyMembers(Lobbies.currentLobby);
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x00132368 File Offset: 0x00130768
		public static CSteamID getLobbyMemberByIndex(int index)
		{
			return SteamMatchmaking.GetLobbyMemberByIndex(Lobbies.currentLobby, index);
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x00132375 File Offset: 0x00130775
		public static void openInvitations()
		{
			SteamFriends.ActivateGameOverlayInviteDialog(Lobbies.currentLobby);
		}

		// Token: 0x04001EDB RID: 7899
		public static LobbiesRefreshedHandler lobbiesRefreshed;

		// Token: 0x04001EDC RID: 7900
		public static LobbiesEnteredHandler lobbiesEntered;

		// Token: 0x04001EE0 RID: 7904
		private static CallResult<LobbyCreated_t> lobbyCreated;

		// Token: 0x04001EE1 RID: 7905
		private static Callback<LobbyEnter_t> lobbyEnter;

		// Token: 0x04001EE2 RID: 7906
		private static Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;

		// Token: 0x04001EE3 RID: 7907
		private static Callback<PersonaStateChange_t> personaStateChange;

		// Token: 0x04001EE4 RID: 7908
		private static Callback<LobbyGameCreated_t> lobbyGameCreated;

		// Token: 0x04001EE5 RID: 7909
		private static Callback<LobbyChatUpdate_t> lobbyChatUpdate;

		// Token: 0x04001EE6 RID: 7910
		[CompilerGenerated]
		private static CallResult<LobbyCreated_t>.APIDispatchDelegate <>f__mg$cache0;

		// Token: 0x04001EE7 RID: 7911
		[CompilerGenerated]
		private static Callback<LobbyEnter_t>.DispatchDelegate <>f__mg$cache1;

		// Token: 0x04001EE8 RID: 7912
		[CompilerGenerated]
		private static Callback<GameLobbyJoinRequested_t>.DispatchDelegate <>f__mg$cache2;

		// Token: 0x04001EE9 RID: 7913
		[CompilerGenerated]
		private static Callback<PersonaStateChange_t>.DispatchDelegate <>f__mg$cache3;

		// Token: 0x04001EEA RID: 7914
		[CompilerGenerated]
		private static Callback<LobbyGameCreated_t>.DispatchDelegate <>f__mg$cache4;

		// Token: 0x04001EEB RID: 7915
		[CompilerGenerated]
		private static Callback<LobbyChatUpdate_t>.DispatchDelegate <>f__mg$cache5;
	}
}
