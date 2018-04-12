using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000439 RID: 1081
	public class CommandAdmin : Command
	{
		// Token: 0x06001DC4 RID: 7620 RVA: 0x000A09B4 File Offset: 0x0009EDB4
		public CommandAdmin(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("AdminCommandText");
			this._info = this.localization.format("AdminInfoText");
			this._help = this.localization.format("AdminHelpText");
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000A0A10 File Offset: 0x0009EE10
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
			SteamAdminlist.admin(csteamID, executorID);
			CommandWindow.Log(this.localization.format("AdminText", new object[]
			{
				csteamID
			}));
		}
	}
}
