using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200043F RID: 1087
	public class CommandBans : Command
	{
		// Token: 0x06001DD0 RID: 7632 RVA: 0x000A1138 File Offset: 0x0009F538
		public CommandBans(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("BansCommandText");
			this._info = this.localization.format("BansInfoText");
			this._help = this.localization.format("BansHelpText");
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x000A1194 File Offset: 0x0009F594
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			if (SteamBlacklist.list.Count == 0)
			{
				CommandWindow.LogError(this.localization.format("NoBansErrorText"));
				return;
			}
			CommandWindow.Log(this.localization.format("BansText"));
			for (int i = 0; i < SteamBlacklist.list.Count; i++)
			{
				SteamBlacklistID steamBlacklistID = SteamBlacklist.list[i];
				CommandWindow.Log(this.localization.format("BanNameText", new object[]
				{
					steamBlacklistID.playerID
				}));
				CommandWindow.Log(this.localization.format("BanJudgeText", new object[]
				{
					steamBlacklistID.judgeID
				}));
				CommandWindow.Log(this.localization.format("BanStatusText", new object[]
				{
					steamBlacklistID.reason,
					steamBlacklistID.duration,
					steamBlacklistID.getTime()
				}));
			}
		}
	}
}
