using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000460 RID: 1120
	public class CommandPvE : Command
	{
		// Token: 0x06001E15 RID: 7701 RVA: 0x000A3C30 File Offset: 0x000A2030
		public CommandPvE(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("PvECommandText");
			this._info = this.localization.format("PvEInfoText");
			this._help = this.localization.format("PvEHelpText");
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000A3C8C File Offset: 0x000A208C
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
			Provider.isPvP = false;
			CommandWindow.Log(this.localization.format("PvEText"));
		}
	}
}
