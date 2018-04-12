using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200043B RID: 1083
	public class CommandAirdrop : Command
	{
		// Token: 0x06001DC8 RID: 7624 RVA: 0x000A0BC8 File Offset: 0x0009EFC8
		public CommandAirdrop(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("AirdropCommandText");
			this._info = this.localization.format("AirdropInfoText");
			this._help = this.localization.format("AirdropHelpText");
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x000A0C24 File Offset: 0x0009F024
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!LevelManager.hasAirdrop)
			{
				return;
			}
			LevelManager.airdropFrequency = 0u;
			CommandWindow.Log(this.localization.format("AirdropText"));
		}
	}
}
