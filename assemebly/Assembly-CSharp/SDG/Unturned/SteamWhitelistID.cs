using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000687 RID: 1671
	public class SteamWhitelistID
	{
		// Token: 0x060030A9 RID: 12457 RVA: 0x0013F2EF File Offset: 0x0013D6EF
		public SteamWhitelistID(CSteamID newSteamID, string newTag, CSteamID newJudgeID)
		{
			this._steamID = newSteamID;
			this.tag = newTag;
			this.judgeID = newJudgeID;
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x0013F30C File Offset: 0x0013D70C
		public CSteamID steamID
		{
			get
			{
				return this._steamID;
			}
		}

		// Token: 0x04002017 RID: 8215
		private CSteamID _steamID;

		// Token: 0x04002018 RID: 8216
		public string tag;

		// Token: 0x04002019 RID: 8217
		public CSteamID judgeID;
	}
}
