using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000449 RID: 1097
	public class CommandFilter : Command
	{
		// Token: 0x06001DE5 RID: 7653 RVA: 0x000A1F20 File Offset: 0x000A0320
		public CommandFilter(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("FilterCommandText");
			this._info = this.localization.format("FilterInfoText");
			this._help = this.localization.format("FilterHelpText");
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x000A1F7C File Offset: 0x000A037C
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
			Provider.filterName = true;
			CommandWindow.Log(this.localization.format("FilterText"));
		}
	}
}
