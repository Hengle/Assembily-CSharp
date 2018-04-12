using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000476 RID: 1142
	public class CommandWhitelisted : Command
	{
		// Token: 0x06001E43 RID: 7747 RVA: 0x000A5CA4 File Offset: 0x000A40A4
		public CommandWhitelisted(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("WhitelistedCommandText");
			this._info = this.localization.format("WhitelistedInfoText");
			this._help = this.localization.format("WhitelistedHelpText");
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x000A5D00 File Offset: 0x000A4100
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
			Provider.isWhitelisted = true;
			CommandWindow.Log(this.localization.format("WhitelistedText"));
		}
	}
}
