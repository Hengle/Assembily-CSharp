using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200067A RID: 1658
	public class SteamPlayerID
	{
		// Token: 0x06003068 RID: 12392 RVA: 0x0013E9C3 File Offset: 0x0013CDC3
		public SteamPlayerID(CSteamID newSteamID, byte newCharacterID, string newPlayerName, string newCharacterName, string newNickName, CSteamID newGroup)
		{
			this._steamID = newSteamID;
			this.characterID = newCharacterID;
			this._playerName = newPlayerName;
			this._characterName = newCharacterName;
			this.nickName = newNickName;
			this.group = newGroup;
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06003069 RID: 12393 RVA: 0x0013E9F8 File Offset: 0x0013CDF8
		public CSteamID steamID
		{
			get
			{
				return this._steamID;
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x0600306A RID: 12394 RVA: 0x0013EA00 File Offset: 0x0013CE00
		private string streamerName
		{
			get
			{
				if (Provider.streamerNames != null && Provider.streamerNames.Count > 0)
				{
					return Provider.streamerNames[(int)(this.steamID.m_SteamID % (ulong)((long)Provider.streamerNames.Count))];
				}
				return string.Empty;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x0600306B RID: 12395 RVA: 0x0013EA52 File Offset: 0x0013CE52
		public string playerName
		{
			get
			{
				if (OptionsSettings.streamer && this.steamID != Provider.user)
				{
					return this.streamerName;
				}
				return this._playerName;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600306C RID: 12396 RVA: 0x0013EA80 File Offset: 0x0013CE80
		// (set) Token: 0x0600306D RID: 12397 RVA: 0x0013EAAE File Offset: 0x0013CEAE
		public string characterName
		{
			get
			{
				if (OptionsSettings.streamer && this.steamID != Provider.user)
				{
					return this.streamerName;
				}
				return this._characterName;
			}
			set
			{
				this._characterName = value;
			}
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x0013EAB8 File Offset: 0x0013CEB8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.steamID,
				" ",
				this.characterID,
				" ",
				this.playerName
			});
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x0013EB05 File Offset: 0x0013CF05
		public static bool operator ==(SteamPlayerID playerID_0, SteamPlayerID playerID_1)
		{
			return playerID_0.steamID == playerID_1.steamID;
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x0013EB18 File Offset: 0x0013CF18
		public static bool operator !=(SteamPlayerID playerID_0, SteamPlayerID playerID_1)
		{
			return !(playerID_0 == playerID_1);
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x0013EB24 File Offset: 0x0013CF24
		public static string operator +(SteamPlayerID playerID, string text)
		{
			return playerID.steamID + text;
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x0013EB37 File Offset: 0x0013CF37
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x0013EB3F File Offset: 0x0013CF3F
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x04001FFB RID: 8187
		private CSteamID _steamID;

		// Token: 0x04001FFC RID: 8188
		public byte characterID;

		// Token: 0x04001FFD RID: 8189
		private string _playerName;

		// Token: 0x04001FFE RID: 8190
		private string _characterName;

		// Token: 0x04001FFF RID: 8191
		public string nickName;

		// Token: 0x04002000 RID: 8192
		public CSteamID group;
	}
}
