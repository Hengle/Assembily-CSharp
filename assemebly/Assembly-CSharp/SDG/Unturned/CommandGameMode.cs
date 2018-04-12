using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200044B RID: 1099
	public class CommandGameMode : Command
	{
		// Token: 0x06001DE9 RID: 7657 RVA: 0x000A21D0 File Offset: 0x000A05D0
		public CommandGameMode(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("GameModeCommandText");
			this._info = this.localization.format("GameModeInfoText");
			this._help = this.localization.format("GameModeHelpText");
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x000A222C File Offset: 0x000A062C
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
			Provider.selectedGameModeName = parameter;
			CommandWindow.Log(this.localization.format("GameModeText", new object[]
			{
				parameter
			}));
		}
	}
}
