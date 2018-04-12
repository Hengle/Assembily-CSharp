using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200044F RID: 1103
	public class CommandHideAdmins : Command
	{
		// Token: 0x06001DF1 RID: 7665 RVA: 0x000A27A8 File Offset: 0x000A0BA8
		public CommandHideAdmins(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("HideAdminsCommandText");
			this._info = this.localization.format("HideAdminsInfoText");
			this._help = this.localization.format("HideAdminsHelpText");
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x000A2804 File Offset: 0x000A0C04
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			if (Provider.isServer)
			{
				CommandWindow.LogError(this.localization.format("RunningErrorText"));
				return;
			}
			Provider.hideAdmins = true;
			CommandWindow.Log(this.localization.format("HideAdminsText"));
		}
	}
}
