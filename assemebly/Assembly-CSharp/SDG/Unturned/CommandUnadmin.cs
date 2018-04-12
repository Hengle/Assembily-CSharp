using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200046F RID: 1135
	public class CommandUnadmin : Command
	{
		// Token: 0x06001E35 RID: 7733 RVA: 0x000A522C File Offset: 0x000A362C
		public CommandUnadmin(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("UnadminCommandText");
			this._info = this.localization.format("UnadminInfoText");
			this._help = this.localization.format("UnadminHelpText");
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x000A5288 File Offset: 0x000A3688
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
				CommandWindow.LogError(this.localization.format("NoPlayerErrorText", new object[]
				{
					parameter
				}));
				return;
			}
			SteamAdminlist.unadmin(csteamID);
			CommandWindow.Log(this.localization.format("UnadminText", new object[]
			{
				csteamID
			}));
		}
	}
}
