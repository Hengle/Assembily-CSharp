using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000466 RID: 1126
	public class CommandSave : Command
	{
		// Token: 0x06001E22 RID: 7714 RVA: 0x000A44C8 File Offset: 0x000A28C8
		public CommandSave(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("SaveCommandText");
			this._info = this.localization.format("SaveInfoText");
			this._help = this.localization.format("SaveHelpText");
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x000A4524 File Offset: 0x000A2924
		protected override void execute(CSteamID executorID, string parameter)
		{
			SaveManager.save();
			CommandWindow.Log(this.localization.format("SaveText"));
		}
	}
}
