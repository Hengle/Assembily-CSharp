using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000465 RID: 1125
	public class CommandResetConfig : Command
	{
		// Token: 0x06001E20 RID: 7712 RVA: 0x000A4450 File Offset: 0x000A2850
		public CommandResetConfig(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("ResetConfigCommandText");
			this._info = this.localization.format("ResetConfigInfoText");
			this._help = this.localization.format("ResetConfigHelpText");
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x000A44AC File Offset: 0x000A28AC
		protected override void execute(CSteamID executorID, string parameter)
		{
			Provider.resetConfig();
			CommandWindow.Log(this.localization.format("ResetConfigText"));
		}
	}
}
