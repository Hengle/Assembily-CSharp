using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000669 RID: 1641
	public class SteamAdminID
	{
		// Token: 0x06002F9D RID: 12189 RVA: 0x0013B3EF File Offset: 0x001397EF
		public SteamAdminID(CSteamID newPlayerID, CSteamID newJudgeID)
		{
			this._playerID = newPlayerID;
			this.judgeID = newJudgeID;
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002F9E RID: 12190 RVA: 0x0013B405 File Offset: 0x00139805
		public CSteamID playerID
		{
			get
			{
				return this._playerID;
			}
		}

		// Token: 0x04001F87 RID: 8071
		private CSteamID _playerID;

		// Token: 0x04001F88 RID: 8072
		public CSteamID judgeID;
	}
}
