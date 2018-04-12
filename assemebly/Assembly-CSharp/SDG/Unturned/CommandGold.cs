using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200044D RID: 1101
	public class CommandGold : Command
	{
		// Token: 0x06001DED RID: 7661 RVA: 0x000A24E8 File Offset: 0x000A08E8
		public CommandGold(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("GoldCommandText");
			this._info = this.localization.format("GoldInfoText");
			this._help = this.localization.format("GoldHelpText");
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x000A2544 File Offset: 0x000A0944
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
			Provider.isGold = true;
			CommandWindow.Log(this.localization.format("GoldText"));
		}
	}
}
