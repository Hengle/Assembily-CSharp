using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200066C RID: 1644
	public class SteamBlacklistID
	{
		// Token: 0x06002FB2 RID: 12210 RVA: 0x0013BB37 File Offset: 0x00139F37
		public SteamBlacklistID(CSteamID newPlayerID, uint newIP, CSteamID newJudgeID, string newReason, uint newDuration, uint newBanned)
		{
			this._playerID = newPlayerID;
			this._ip = newIP;
			this.judgeID = newJudgeID;
			this.reason = newReason;
			this.duration = newDuration;
			this.banned = newBanned;
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x0013BB6C File Offset: 0x00139F6C
		public CSteamID playerID
		{
			get
			{
				return this._playerID;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x0013BB74 File Offset: 0x00139F74
		public uint ip
		{
			get
			{
				return this._ip;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002FB5 RID: 12213 RVA: 0x0013BB7C File Offset: 0x00139F7C
		public bool isExpired
		{
			get
			{
				return Provider.time > this.banned + this.duration;
			}
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x0013BB92 File Offset: 0x00139F92
		public uint getTime()
		{
			return this.duration - (Provider.time - this.banned);
		}

		// Token: 0x04001F90 RID: 8080
		private CSteamID _playerID;

		// Token: 0x04001F91 RID: 8081
		private uint _ip;

		// Token: 0x04001F92 RID: 8082
		public CSteamID judgeID;

		// Token: 0x04001F93 RID: 8083
		public string reason;

		// Token: 0x04001F94 RID: 8084
		public uint duration;

		// Token: 0x04001F95 RID: 8085
		public uint banned;
	}
}
