using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200045D RID: 1117
	public class CommandPermits : Command
	{
		// Token: 0x06001E0F RID: 7695 RVA: 0x000A38E0 File Offset: 0x000A1CE0
		public CommandPermits(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("PermitsCommandText");
			this._info = this.localization.format("PermitsInfoText");
			this._help = this.localization.format("PermitsHelpText");
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x000A393C File Offset: 0x000A1D3C
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			if (SteamWhitelist.list.Count == 0)
			{
				CommandWindow.LogError(this.localization.format("NoPermitsErrorText"));
				return;
			}
			CommandWindow.Log(this.localization.format("PermitsText"));
			for (int i = 0; i < SteamWhitelist.list.Count; i++)
			{
				SteamWhitelistID steamWhitelistID = SteamWhitelist.list[i];
				CommandWindow.Log(this.localization.format("PermitNameText", new object[]
				{
					steamWhitelistID.steamID,
					steamWhitelistID.tag
				}));
				CommandWindow.Log(this.localization.format("PermitJudgeText", new object[]
				{
					steamWhitelistID.judgeID
				}));
			}
		}
	}
}
