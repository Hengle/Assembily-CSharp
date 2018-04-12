using System;
using System.Collections.Generic;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace SDG.Provider
{
	// Token: 0x0200034D RID: 845
	public class TempSteamworksMatchmaking
	{
		// Token: 0x0600175A RID: 5978 RVA: 0x00086BC8 File Offset: 0x00084FC8
		public TempSteamworksMatchmaking()
		{
			this.serverPingResponse = new ISteamMatchmakingPingResponse(new ISteamMatchmakingPingResponse.ServerResponded(this.onPingResponded), new ISteamMatchmakingPingResponse.ServerFailedToRespond(this.onPingFailedToRespond));
			this.serverListResponse = new ISteamMatchmakingServerListResponse(new ISteamMatchmakingServerListResponse.ServerResponded(this.onServerListResponded), new ISteamMatchmakingServerListResponse.ServerFailedToRespond(this.onServerListFailedToRespond), new ISteamMatchmakingServerListResponse.RefreshComplete(this.onRefreshComplete));
			this.playersResponse = new ISteamMatchmakingPlayersResponse(new ISteamMatchmakingPlayersResponse.AddPlayerToList(this.onAddPlayerToList), new ISteamMatchmakingPlayersResponse.PlayersFailedToRespond(this.onPlayersFailedToRespond), new ISteamMatchmakingPlayersResponse.PlayersRefreshComplete(this.onPlayersRefreshComplete));
			this.rulesResponse = new ISteamMatchmakingRulesResponse(new ISteamMatchmakingRulesResponse.RulesResponded(this.onRulesResponded), new ISteamMatchmakingRulesResponse.RulesFailedToRespond(this.onRulesFailedToRespond), new ISteamMatchmakingRulesResponse.RulesRefreshComplete(this.onRulesRefreshComplete));
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00086CD4 File Offset: 0x000850D4
		public void initializeMatchmaking()
		{
			if (this.matchmakingBestServer != null)
			{
				this.matchmakingIgnored.Add(this.matchmakingBestServer.steamID.m_SteamID);
			}
			this.matchmakingBestServer = null;
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x00086D12 File Offset: 0x00085112
		public ESteamServerList currentList
		{
			get
			{
				return this._currentList;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x00086D1A File Offset: 0x0008511A
		public List<SteamServerInfo> serverList
		{
			get
			{
				return this._serverList;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x00086D22 File Offset: 0x00085122
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x00086D2A File Offset: 0x0008512A
		public bool isAttemptingServerQuery { get; private set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x00086D33 File Offset: 0x00085133
		public IComparer<SteamServerInfo> serverInfoComparer
		{
			get
			{
				return this._serverInfoComparer;
			}
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00086D3B File Offset: 0x0008513B
		public void sortMasterServer(IComparer<SteamServerInfo> newServerInfoComparer)
		{
			this._serverInfoComparer = newServerInfoComparer;
			this.serverList.Sort(this.serverInfoComparer);
			if (this.onMasterServerResorted != null)
			{
				this.onMasterServerResorted();
			}
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00086D6B File Offset: 0x0008516B
		private void cleanupServerQuery()
		{
			if (this.serverQuery == HServerQuery.Invalid)
			{
				return;
			}
			SteamMatchmakingServers.CancelServerQuery(this.serverQuery);
			this.serverQuery = HServerQuery.Invalid;
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00086D99 File Offset: 0x00085199
		private void cleanupPlayersQuery()
		{
			if (this.playersQuery == HServerQuery.Invalid)
			{
				return;
			}
			SteamMatchmakingServers.CancelServerQuery(this.playersQuery);
			this.playersQuery = HServerQuery.Invalid;
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00086DC7 File Offset: 0x000851C7
		private void cleanupRulesQuery()
		{
			if (this.rulesQuery == HServerQuery.Invalid)
			{
				return;
			}
			SteamMatchmakingServers.CancelServerQuery(this.rulesQuery);
			this.rulesQuery = HServerQuery.Invalid;
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00086DF5 File Offset: 0x000851F5
		private void cleanupServerListRequest()
		{
			if (this.serverListRequest == HServerListRequest.Invalid)
			{
				return;
			}
			SteamMatchmakingServers.ReleaseRequest(this.serverListRequest);
			this.serverListRequest = HServerListRequest.Invalid;
			this.serverListRefreshIndex = -1;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00086E2A File Offset: 0x0008522A
		public void connect(SteamConnectionInfo info)
		{
			if (Provider.isConnected)
			{
				return;
			}
			this.connectionInfo = info;
			this.serverQueryAttempts = 0;
			this.isAttemptingServerQuery = true;
			this.autoJoinServerQuery = false;
			this.attemptServerQuery();
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x00086E59 File Offset: 0x00085259
		public void cancel()
		{
			if (!this.isAttemptingServerQuery)
			{
				return;
			}
			this.serverQueryAttempts = 10;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00086E70 File Offset: 0x00085270
		private void attemptServerQuery()
		{
			this.cleanupServerQuery();
			this.serverQuery = SteamMatchmakingServers.PingServer(this.connectionInfo.ip, this.connectionInfo.port + 1, this.serverPingResponse);
			this.serverQueryAttempts++;
			if (this.onAttemptUpdated != null)
			{
				this.onAttemptUpdated(this.serverQueryAttempts);
			}
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00086ED8 File Offset: 0x000852D8
		public void refreshMasterServer(ESteamServerList list, string filterMap, EPassword filterPassword, EWorkshop filterWorkshop, EPlugins filterPlugins, EAttendance filterAttendance, EVACProtectionFilter filterVACProtection, EBattlEyeProtectionFilter filterBattlEyeProtection, bool filterPro, ECombat filterCombat, ECheats filterCheats, EGameMode filterMode, ECameraMode filterCamera)
		{
			this._currentList = list;
			if (this.onMasterServerRemoved != null)
			{
				this.onMasterServerRemoved();
			}
			this.cleanupServerListRequest();
			this._serverList = new List<SteamServerInfo>();
			if (list == ESteamServerList.HISTORY)
			{
				this.serverListRequest = SteamMatchmakingServers.RequestHistoryServerList(Provider.APP_ID, new MatchMakingKeyValuePair_t[0], 0u, this.serverListResponse);
				return;
			}
			if (list == ESteamServerList.FAVORITES)
			{
				this.serverListRequest = SteamMatchmakingServers.RequestFavoritesServerList(Provider.APP_ID, new MatchMakingKeyValuePair_t[0], 0u, this.serverListResponse);
				return;
			}
			if (list == ESteamServerList.LAN)
			{
				this.serverListRequest = SteamMatchmakingServers.RequestLANServerList(Provider.APP_ID, this.serverListResponse);
				return;
			}
			this.filters = new List<MatchMakingKeyValuePair_t>();
			MatchMakingKeyValuePair_t item = default(MatchMakingKeyValuePair_t);
			item.m_szKey = "gamedir";
			item.m_szValue = "unturned";
			this.filters.Add(item);
			if (filterMap.Length > 0)
			{
				MatchMakingKeyValuePair_t item2 = default(MatchMakingKeyValuePair_t);
				item2.m_szKey = "map";
				item2.m_szValue = filterMap.ToLower();
				this.filters.Add(item2);
			}
			if (filterAttendance == EAttendance.EMPTY)
			{
				MatchMakingKeyValuePair_t item3 = default(MatchMakingKeyValuePair_t);
				item3.m_szKey = "noplayers";
				item3.m_szValue = "1";
				this.filters.Add(item3);
			}
			else if (filterAttendance == EAttendance.SPACE)
			{
				MatchMakingKeyValuePair_t item4 = default(MatchMakingKeyValuePair_t);
				item4.m_szKey = "notfull";
				item4.m_szValue = "1";
				this.filters.Add(item4);
				MatchMakingKeyValuePair_t item5 = default(MatchMakingKeyValuePair_t);
				item5.m_szKey = "hasplayers";
				item5.m_szValue = "1";
				this.filters.Add(item5);
			}
			MatchMakingKeyValuePair_t item6 = default(MatchMakingKeyValuePair_t);
			item6.m_szKey = "gamedataand";
			if (filterPassword == EPassword.YES)
			{
				item6.m_szValue = "PASS";
			}
			else if (filterPassword == EPassword.NO)
			{
				item6.m_szValue = "SSAP";
			}
			if (filterVACProtection == EVACProtectionFilter.Secure)
			{
				item6.m_szValue += ",";
				item6.m_szValue += "VAC_ON";
				MatchMakingKeyValuePair_t item7 = default(MatchMakingKeyValuePair_t);
				item7.m_szKey = "secure";
				item7.m_szValue = "1";
				this.filters.Add(item7);
			}
			else if (filterVACProtection == EVACProtectionFilter.Insecure)
			{
				item6.m_szValue += ",";
				item6.m_szValue += "VAC_OFF";
			}
			item6.m_szValue += ",";
			item6.m_szValue += Provider.APP_VERSION;
			this.filters.Add(item6);
			MatchMakingKeyValuePair_t item8 = default(MatchMakingKeyValuePair_t);
			item8.m_szKey = "gametagsand";
			if (filterWorkshop == EWorkshop.YES)
			{
				item8.m_szValue = "WORK";
			}
			else if (filterWorkshop == EWorkshop.NO)
			{
				item8.m_szValue = "KROW";
			}
			if (filterCombat == ECombat.PVP)
			{
				item8.m_szValue += ",PVP";
			}
			else if (filterCombat == ECombat.PVE)
			{
				item8.m_szValue += ",PVE";
			}
			if (filterCheats == ECheats.YES)
			{
				item8.m_szValue += ",CHEATS";
			}
			else if (filterCheats == ECheats.NO)
			{
				item8.m_szValue += ",STAEHC";
			}
			if (filterMode != EGameMode.ANY)
			{
				item8.m_szValue = item8.m_szValue + "," + filterMode.ToString();
			}
			if (filterCamera != ECameraMode.ANY)
			{
				item8.m_szValue = item8.m_szValue + "," + filterCamera.ToString();
			}
			if (filterPro)
			{
				item8.m_szValue += ",GOLDONLY";
			}
			else
			{
				item8.m_szValue += ",YLNODLOG";
			}
			if (filterBattlEyeProtection == EBattlEyeProtectionFilter.Secure)
			{
				item8.m_szValue += ",BATTLEYE_ON";
			}
			else if (filterBattlEyeProtection == EBattlEyeProtectionFilter.Insecure)
			{
				item8.m_szValue += ",BATTLEYE_OFF";
			}
			this.filters.Add(item8);
			if (list == ESteamServerList.INTERNET)
			{
				this.serverListRequest = SteamMatchmakingServers.RequestInternetServerList(Provider.APP_ID, this.filters.ToArray(), (uint)this.filters.Count, this.serverListResponse);
				return;
			}
			if (list == ESteamServerList.FRIENDS)
			{
				this.serverListRequest = SteamMatchmakingServers.RequestFriendsServerList(Provider.APP_ID, this.filters.ToArray(), (uint)this.filters.Count, this.serverListResponse);
				return;
			}
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x000873A6 File Offset: 0x000857A6
		public void refreshPlayers(uint ip, ushort port)
		{
			this.cleanupPlayersQuery();
			this.playersQuery = SteamMatchmakingServers.PlayerDetails(ip, port + 1, this.playersResponse);
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x000873C4 File Offset: 0x000857C4
		public void refreshRules(uint ip, ushort port)
		{
			this.cleanupRulesQuery();
			this.rulesMap = new Dictionary<string, string>();
			this.rulesQuery = SteamMatchmakingServers.ServerRules(ip, port + 1, this.rulesResponse);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x000873F0 File Offset: 0x000857F0
		private void onPingResponded(gameserveritem_t data)
		{
			this.isAttemptingServerQuery = false;
			this.cleanupServerQuery();
			if (data.m_nAppID == Provider.APP_ID.m_AppId)
			{
				SteamServerInfo steamServerInfo = new SteamServerInfo(data);
				if (!steamServerInfo.isPro || Provider.isPro)
				{
					if (!steamServerInfo.isPassworded || this.connectionInfo.password != string.Empty)
					{
						if (this.autoJoinServerQuery)
						{
							Provider.connect(steamServerInfo, this.connectionInfo.password);
						}
						else
						{
							MenuUI.closeAll();
							MenuUI.closeAlert();
							MenuPlayServerInfoUI.open(steamServerInfo, this.connectionInfo.password, MenuPlayServerInfoUI.EServerInfoOpenContext.CONNECT);
						}
					}
					else
					{
						Provider._connectionFailureInfo = ESteamConnectionFailureInfo.PASSWORD;
					}
				}
				else
				{
					Provider._connectionFailureInfo = ESteamConnectionFailureInfo.PRO_SERVER;
				}
			}
			else
			{
				Provider._connectionFailureInfo = ESteamConnectionFailureInfo.TIMED_OUT;
			}
			if (this.onTimedOut != null)
			{
				this.onTimedOut();
			}
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x000874DC File Offset: 0x000858DC
		private void onPingFailedToRespond()
		{
			if (this.serverQueryAttempts < 10)
			{
				this.attemptServerQuery();
			}
			else
			{
				this.isAttemptingServerQuery = false;
				this.cleanupServerQuery();
				Provider._connectionFailureInfo = ESteamConnectionFailureInfo.TIMED_OUT;
				if (this.onTimedOut != null)
				{
					this.onTimedOut();
				}
			}
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0008752C File Offset: 0x0008592C
		private void onServerListResponded(HServerListRequest request, int index)
		{
			if (request != this.serverListRequest)
			{
				return;
			}
			gameserveritem_t serverDetails = SteamMatchmakingServers.GetServerDetails(request, index);
			if (this.matchmakingIgnored.Contains(serverDetails.m_steamID.m_SteamID))
			{
				return;
			}
			SteamServerInfo steamServerInfo = new SteamServerInfo(serverDetails);
			if (index == this.serverListRefreshIndex)
			{
				if (this.onMasterServerQueryRefreshed != null)
				{
					this.onMasterServerQueryRefreshed(steamServerInfo);
				}
				return;
			}
			if (FilterSettings.filterPlugins == EPlugins.NO)
			{
				if (serverDetails.m_nBotPlayers != 0)
				{
					return;
				}
			}
			else if (FilterSettings.filterPlugins == EPlugins.YES)
			{
				if (serverDetails.m_nBotPlayers != 1)
				{
					return;
				}
			}
			else if (serverDetails.m_nBotPlayers > 1)
			{
				return;
			}
			if (steamServerInfo.maxPlayers < (int)CommandMaxPlayers.MIN_NUMBER)
			{
				return;
			}
			if (this.currentList == ESteamServerList.INTERNET)
			{
				if (steamServerInfo.maxPlayers > (int)(CommandMaxPlayers.MAX_NUMBER / 2))
				{
					return;
				}
			}
			else if (steamServerInfo.maxPlayers > (int)CommandMaxPlayers.MAX_NUMBER)
			{
				return;
			}
			if (PlaySettings.serversName != null && PlaySettings.serversName.Length > 1 && steamServerInfo.name.IndexOf(PlaySettings.serversName, StringComparison.OrdinalIgnoreCase) == -1)
			{
				return;
			}
			int num = this.serverList.BinarySearch(steamServerInfo, this.serverInfoComparer);
			if (num < 0)
			{
				num = ~num;
			}
			this.serverList.Insert(num, steamServerInfo);
			if (this.onMasterServerAdded != null)
			{
				this.onMasterServerAdded(num, steamServerInfo);
			}
			this.matchmakingBestServer = null;
			int num2 = 25;
			while (this.matchmakingBestServer == null && num2 <= OptionsSettings.maxMatchmakingPing)
			{
				int num3 = -1;
				foreach (SteamServerInfo steamServerInfo2 in this.serverList)
				{
					if (steamServerInfo2.players < OptionsSettings.minMatchmakingPlayers)
					{
						break;
					}
					if (steamServerInfo2.players != num3)
					{
						num3 = steamServerInfo2.players;
						if (steamServerInfo2.ping <= num2)
						{
							this.matchmakingBestServer = steamServerInfo2;
							break;
						}
					}
				}
				num2 += 25;
			}
			if (this.matchmakingProgressed != null)
			{
				this.matchmakingProgressed();
			}
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00087774 File Offset: 0x00085B74
		private void onServerListFailedToRespond(HServerListRequest request, int index)
		{
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00087778 File Offset: 0x00085B78
		private void onRefreshComplete(HServerListRequest request, EMatchMakingServerResponse response)
		{
			if (request == this.serverListRequest)
			{
				if (this.onMasterServerRefreshed != null)
				{
					this.onMasterServerRefreshed(response);
				}
				if (this.matchmakingFinished != null)
				{
					this.matchmakingFinished();
				}
				if (response == EMatchMakingServerResponse.eNoServersListedOnMasterServer || this.serverList.Count == 0)
				{
					Debug.Log("No servers found on the master server.");
					return;
				}
				if (response == EMatchMakingServerResponse.eServerFailedToRespond)
				{
					Debug.LogError("Failed to connect to the master server.");
					return;
				}
				if (response == EMatchMakingServerResponse.eServerResponded)
				{
					Debug.Log("Successfully refreshed the master server.");
					return;
				}
			}
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00087808 File Offset: 0x00085C08
		private void onAddPlayerToList(string name, int score, float time)
		{
			if (this.onPlayersQueryRefreshed != null)
			{
				this.onPlayersQueryRefreshed(name, score, time);
			}
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00087823 File Offset: 0x00085C23
		private void onPlayersFailedToRespond()
		{
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00087825 File Offset: 0x00085C25
		private void onPlayersRefreshComplete()
		{
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x00087827 File Offset: 0x00085C27
		private void onRulesResponded(string key, string value)
		{
			if (this.rulesMap == null)
			{
				return;
			}
			this.rulesMap.Add(key, value);
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00087842 File Offset: 0x00085C42
		private void onRulesFailedToRespond()
		{
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00087844 File Offset: 0x00085C44
		private void onRulesRefreshComplete()
		{
			if (this.onRulesQueryRefreshed != null)
			{
				this.onRulesQueryRefreshed(this.rulesMap);
			}
		}

		// Token: 0x04000C7E RID: 3198
		public TempSteamworksMatchmaking.MasterServerAdded onMasterServerAdded;

		// Token: 0x04000C7F RID: 3199
		public TempSteamworksMatchmaking.MasterServerRemoved onMasterServerRemoved;

		// Token: 0x04000C80 RID: 3200
		public TempSteamworksMatchmaking.MasterServerResorted onMasterServerResorted;

		// Token: 0x04000C81 RID: 3201
		public TempSteamworksMatchmaking.MasterServerRefreshed onMasterServerRefreshed;

		// Token: 0x04000C82 RID: 3202
		public TempSteamworksMatchmaking.MasterServerQueryRefreshed onMasterServerQueryRefreshed;

		// Token: 0x04000C83 RID: 3203
		public TempSteamworksMatchmaking.AttemptUpdated onAttemptUpdated;

		// Token: 0x04000C84 RID: 3204
		public TempSteamworksMatchmaking.TimedOut onTimedOut;

		// Token: 0x04000C85 RID: 3205
		public TempSteamworksMatchmaking.MatchmakingProgressedHandler matchmakingProgressed;

		// Token: 0x04000C86 RID: 3206
		public TempSteamworksMatchmaking.MatchmakingFinishedHandler matchmakingFinished;

		// Token: 0x04000C87 RID: 3207
		private HashSet<ulong> matchmakingIgnored = new HashSet<ulong>();

		// Token: 0x04000C88 RID: 3208
		public SteamServerInfo matchmakingBestServer;

		// Token: 0x04000C89 RID: 3209
		public TempSteamworksMatchmaking.PlayersQueryRefreshed onPlayersQueryRefreshed;

		// Token: 0x04000C8A RID: 3210
		public TempSteamworksMatchmaking.RulesQueryRefreshed onRulesQueryRefreshed;

		// Token: 0x04000C8B RID: 3211
		private SteamConnectionInfo connectionInfo;

		// Token: 0x04000C8C RID: 3212
		private ESteamServerList _currentList;

		// Token: 0x04000C8D RID: 3213
		private List<SteamServerInfo> _serverList;

		// Token: 0x04000C8E RID: 3214
		private List<MatchMakingKeyValuePair_t> filters;

		// Token: 0x04000C8F RID: 3215
		private ISteamMatchmakingPingResponse serverPingResponse;

		// Token: 0x04000C90 RID: 3216
		private ISteamMatchmakingServerListResponse serverListResponse;

		// Token: 0x04000C91 RID: 3217
		private ISteamMatchmakingPlayersResponse playersResponse;

		// Token: 0x04000C92 RID: 3218
		private ISteamMatchmakingRulesResponse rulesResponse;

		// Token: 0x04000C93 RID: 3219
		private HServerQuery playersQuery = HServerQuery.Invalid;

		// Token: 0x04000C94 RID: 3220
		private HServerQuery rulesQuery = HServerQuery.Invalid;

		// Token: 0x04000C95 RID: 3221
		private Dictionary<string, string> rulesMap;

		// Token: 0x04000C96 RID: 3222
		private HServerQuery serverQuery = HServerQuery.Invalid;

		// Token: 0x04000C97 RID: 3223
		private int serverQueryAttempts;

		// Token: 0x04000C99 RID: 3225
		public bool autoJoinServerQuery;

		// Token: 0x04000C9A RID: 3226
		private HServerListRequest serverListRequest = HServerListRequest.Invalid;

		// Token: 0x04000C9B RID: 3227
		private int serverListRefreshIndex = -1;

		// Token: 0x04000C9C RID: 3228
		private IComparer<SteamServerInfo> _serverInfoComparer = new SteamServerInfoPingAscendingComparator();

		// Token: 0x0200034E RID: 846
		// (Invoke) Token: 0x06001778 RID: 6008
		public delegate void MasterServerAdded(int insert, SteamServerInfo server);

		// Token: 0x0200034F RID: 847
		// (Invoke) Token: 0x0600177C RID: 6012
		public delegate void MasterServerRemoved();

		// Token: 0x02000350 RID: 848
		// (Invoke) Token: 0x06001780 RID: 6016
		public delegate void MasterServerResorted();

		// Token: 0x02000351 RID: 849
		// (Invoke) Token: 0x06001784 RID: 6020
		public delegate void MasterServerRefreshed(EMatchMakingServerResponse response);

		// Token: 0x02000352 RID: 850
		// (Invoke) Token: 0x06001788 RID: 6024
		public delegate void MasterServerQueryRefreshed(SteamServerInfo server);

		// Token: 0x02000353 RID: 851
		// (Invoke) Token: 0x0600178C RID: 6028
		public delegate void AttemptUpdated(int attempt);

		// Token: 0x02000354 RID: 852
		// (Invoke) Token: 0x06001790 RID: 6032
		public delegate void TimedOut();

		// Token: 0x02000355 RID: 853
		// (Invoke) Token: 0x06001794 RID: 6036
		public delegate void MatchmakingProgressedHandler();

		// Token: 0x02000356 RID: 854
		// (Invoke) Token: 0x06001798 RID: 6040
		public delegate void MatchmakingFinishedHandler();

		// Token: 0x02000357 RID: 855
		// (Invoke) Token: 0x0600179C RID: 6044
		public delegate void PlayersQueryRefreshed(string name, int score, float time);

		// Token: 0x02000358 RID: 856
		// (Invoke) Token: 0x060017A0 RID: 6048
		public delegate void RulesQueryRefreshed(Dictionary<string, string> rulesMap);
	}
}
