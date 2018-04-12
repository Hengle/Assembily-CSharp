using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000470 RID: 1136
	public class CommandUnban : Command
	{
		// Token: 0x06001E37 RID: 7735 RVA: 0x000A5318 File Offset: 0x000A3718
		public CommandUnban(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("UnbanCommandText");
			this._info = this.localization.format("UnbanInfoText");
			this._help = this.localization.format("UnbanHelpText");
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x000A5374 File Offset: 0x000A3774
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			if (!Provider.isServer)
			{
				CommandWindow.LogError(this.localization.format("NotRunningErrorText"));
				return;
			}
			CSteamID csteamID;
			if (!PlayerTool.tryGetSteamID(parameter, out csteamID))
			{
				CommandWindow.LogError(this.localization.format("InvalidSteamIDErrorText", new object[]
				{
					parameter
				}));
				return;
			}
			if (!SteamBlacklist.unban(csteamID))
			{
				CommandWindow.LogError(this.localization.format("NoPlayerErrorText", new object[]
				{
					csteamID
				}));
				return;
			}
			CommandWindow.Log(this.localization.format("UnbanText", new object[]
			{
				csteamID
			}));
		}
	}
}
