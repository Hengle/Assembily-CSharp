using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000443 RID: 1091
	public class CommandCheats : Command
	{
		// Token: 0x06001DD9 RID: 7641 RVA: 0x000A1648 File Offset: 0x0009FA48
		public CommandCheats(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("CheatsCommandText");
			this._info = this.localization.format("CheatsInfoText");
			this._help = this.localization.format("CheatsHelpText");
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x000A16A4 File Offset: 0x0009FAA4
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
			Provider.hasCheats = true;
			CommandWindow.Log(this.localization.format("CheatsText"));
		}
	}
}
