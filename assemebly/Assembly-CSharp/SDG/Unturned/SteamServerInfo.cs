using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000685 RID: 1669
	public class SteamServerInfo
	{
		// Token: 0x06003089 RID: 12425 RVA: 0x0013EC70 File Offset: 0x0013D070
		public SteamServerInfo(gameserveritem_t data)
		{
			this._steamID = data.m_steamID;
			this._ip = data.m_NetAdr.GetIP();
			this._port = data.m_NetAdr.GetConnectionPort();
			this._name = data.GetServerName();
			if (OptionsSettings.filter)
			{
				this._name = ChatManager.filter(this.name);
			}
			this._map = data.GetMap();
			string gameTags = data.GetGameTags();
			if (gameTags.Length > 0)
			{
				this._isPvP = (gameTags.IndexOf("PVP") != -1);
				this._hasCheats = (gameTags.IndexOf("CHEATS") != -1);
				this._isWorkshop = (gameTags.IndexOf("WORK") != -1);
				if (gameTags.IndexOf("EASY") != -1)
				{
					this._mode = EGameMode.EASY;
				}
				else if (gameTags.IndexOf("HARD") != -1)
				{
					this._mode = EGameMode.HARD;
				}
				else
				{
					this._mode = EGameMode.NORMAL;
				}
				if (gameTags.IndexOf("FIRST") != -1)
				{
					this._cameraMode = ECameraMode.FIRST;
				}
				else if (gameTags.IndexOf("THIRD") != -1)
				{
					this._cameraMode = ECameraMode.THIRD;
				}
				else if (gameTags.IndexOf("BOTH") != -1)
				{
					this._cameraMode = ECameraMode.BOTH;
				}
				else
				{
					this._cameraMode = ECameraMode.VEHICLE;
				}
				if (gameTags.IndexOf("GOLDONLY") != -1)
				{
					this._isPro = true;
				}
				else
				{
					this._isPro = false;
				}
				this.IsBattlEyeSecure = (gameTags.IndexOf("BATTLEYE_ON") != -1);
				int num = gameTags.IndexOf(",GAMEMODE:");
				int num2 = gameTags.IndexOf(",", num + 1);
				if (num != -1 && num2 != -1)
				{
					num += 10;
					this.gameMode = gameTags.Substring(num, num2 - num);
				}
				else
				{
					this.gameMode = null;
				}
			}
			else
			{
				this._isPvP = true;
				this._hasCheats = false;
				this._mode = EGameMode.NORMAL;
				this._cameraMode = ECameraMode.FIRST;
				this._isPro = true;
				this.IsBattlEyeSecure = false;
				this.gameMode = null;
			}
			this._ping = data.m_nPing;
			this._players = data.m_nPlayers;
			this._maxPlayers = data.m_nMaxPlayers;
			this._isPassworded = data.m_bPassword;
			this.IsVACSecure = data.m_bSecure;
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x0013EED4 File Offset: 0x0013D2D4
		public SteamServerInfo(string newName, EGameMode newMode, bool newVACSecure, bool newBattlEyeEnabled, bool newPro)
		{
			this._name = newName;
			if (OptionsSettings.filter)
			{
				this._name = ChatManager.filter(this.name);
			}
			this._mode = newMode;
			this.IsVACSecure = newVACSecure;
			this.IsBattlEyeSecure = newBattlEyeEnabled;
			this._isPro = newPro;
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x0600308B RID: 12427 RVA: 0x0013EF27 File Offset: 0x0013D327
		public CSteamID steamID
		{
			get
			{
				return this._steamID;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x0600308C RID: 12428 RVA: 0x0013EF2F File Offset: 0x0013D32F
		public uint ip
		{
			get
			{
				return this._ip;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x0013EF37 File Offset: 0x0013D337
		public ushort port
		{
			get
			{
				return this._port;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600308E RID: 12430 RVA: 0x0013EF3F File Offset: 0x0013D33F
		public string name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x0600308F RID: 12431 RVA: 0x0013EF47 File Offset: 0x0013D347
		public string map
		{
			get
			{
				return this._map;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06003090 RID: 12432 RVA: 0x0013EF4F File Offset: 0x0013D34F
		public bool isPvP
		{
			get
			{
				return this._isPvP;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06003091 RID: 12433 RVA: 0x0013EF57 File Offset: 0x0013D357
		public bool hasCheats
		{
			get
			{
				return this._hasCheats;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x0013EF5F File Offset: 0x0013D35F
		public bool isWorkshop
		{
			get
			{
				return this._isWorkshop;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x0013EF67 File Offset: 0x0013D367
		public EGameMode mode
		{
			get
			{
				return this._mode;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06003094 RID: 12436 RVA: 0x0013EF6F File Offset: 0x0013D36F
		public ECameraMode cameraMode
		{
			get
			{
				return this._cameraMode;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x0013EF77 File Offset: 0x0013D377
		public int ping
		{
			get
			{
				return this._ping;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x0013EF7F File Offset: 0x0013D37F
		public int players
		{
			get
			{
				return this._players;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x0013EF87 File Offset: 0x0013D387
		public int maxPlayers
		{
			get
			{
				return this._maxPlayers;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x0013EF8F File Offset: 0x0013D38F
		public bool isPassworded
		{
			get
			{
				return this._isPassworded;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x0013EF97 File Offset: 0x0013D397
		// (set) Token: 0x0600309A RID: 12442 RVA: 0x0013EF9F File Offset: 0x0013D39F
		public bool IsVACSecure { get; private set; }

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x0600309B RID: 12443 RVA: 0x0013EFA8 File Offset: 0x0013D3A8
		// (set) Token: 0x0600309C RID: 12444 RVA: 0x0013EFB0 File Offset: 0x0013D3B0
		public bool IsBattlEyeSecure { get; private set; }

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x0013EFB9 File Offset: 0x0013D3B9
		public bool isPro
		{
			get
			{
				return this._isPro;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x0013EFC1 File Offset: 0x0013D3C1
		// (set) Token: 0x0600309F RID: 12447 RVA: 0x0013EFC9 File Offset: 0x0013D3C9
		public string gameMode { get; protected set; }

		// Token: 0x060030A0 RID: 12448 RVA: 0x0013EFD4 File Offset: 0x0013D3D4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Name: ",
				this.name,
				" Map: ",
				this.map,
				" PvP: ",
				this.isPvP,
				" Mode: ",
				this.mode,
				" Ping: ",
				this.ping,
				" Players: ",
				this.players,
				"/",
				this.maxPlayers,
				" Passworded: ",
				this.isPassworded
			});
		}

		// Token: 0x04002003 RID: 8195
		private CSteamID _steamID;

		// Token: 0x04002004 RID: 8196
		private uint _ip;

		// Token: 0x04002005 RID: 8197
		private ushort _port;

		// Token: 0x04002006 RID: 8198
		private string _name;

		// Token: 0x04002007 RID: 8199
		private string _map;

		// Token: 0x04002008 RID: 8200
		private bool _isPvP;

		// Token: 0x04002009 RID: 8201
		private bool _hasCheats;

		// Token: 0x0400200A RID: 8202
		private bool _isWorkshop;

		// Token: 0x0400200B RID: 8203
		private EGameMode _mode;

		// Token: 0x0400200C RID: 8204
		private ECameraMode _cameraMode;

		// Token: 0x0400200D RID: 8205
		private int _ping;

		// Token: 0x0400200E RID: 8206
		private int _players;

		// Token: 0x0400200F RID: 8207
		private int _maxPlayers;

		// Token: 0x04002010 RID: 8208
		private bool _isPassworded;

		// Token: 0x04002013 RID: 8211
		private bool _isPro;
	}
}
