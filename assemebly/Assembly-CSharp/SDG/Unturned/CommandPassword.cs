using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200045B RID: 1115
	public class CommandPassword : Command
	{
		// Token: 0x06001E0B RID: 7691 RVA: 0x000A36D8 File Offset: 0x000A1AD8
		public CommandPassword(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("PasswordCommandText");
			this._info = this.localization.format("PasswordInfoText");
			this._help = this.localization.format("PasswordHelpText");
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x000A3734 File Offset: 0x000A1B34
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
			if (parameter.Length == 0)
			{
				Provider.serverPassword = string.Empty;
				CommandWindow.Log(this.localization.format("DisableText"));
				return;
			}
			Provider.serverPassword = parameter;
			CommandWindow.Log(this.localization.format("PasswordText", new object[]
			{
				parameter
			}));
		}
	}
}
