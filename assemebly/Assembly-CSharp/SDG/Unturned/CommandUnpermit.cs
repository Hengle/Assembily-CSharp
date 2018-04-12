using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000471 RID: 1137
	public class CommandUnpermit : Command
	{
		// Token: 0x06001E39 RID: 7737 RVA: 0x000A5430 File Offset: 0x000A3830
		public CommandUnpermit(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("UnpermitCommandText");
			this._info = this.localization.format("UnpermitInfoText");
			this._help = this.localization.format("UnpermitHelpText");
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x000A548C File Offset: 0x000A388C
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
			if (!SteamWhitelist.unwhitelist(csteamID))
			{
				CommandWindow.LogError(this.localization.format("NoPlayerErrorText", new object[]
				{
					csteamID
				}));
				return;
			}
			CommandWindow.Log(this.localization.format("UnpermitText", new object[]
			{
				csteamID
			}));
		}
	}
}
