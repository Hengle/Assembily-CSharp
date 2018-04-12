using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200046B RID: 1131
	public class CommandSync : Command
	{
		// Token: 0x06001E2C RID: 7724 RVA: 0x000A4AEC File Offset: 0x000A2EEC
		public CommandSync(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("SyncCommandText");
			this._info = this.localization.format("SyncInfoText");
			this._help = this.localization.format("SyncHelpText");
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x000A4B48 File Offset: 0x000A2F48
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
			PlayerSavedata.hasSync = true;
			CommandWindow.Log(this.localization.format("SyncText"));
		}
	}
}
