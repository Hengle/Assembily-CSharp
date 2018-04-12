using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000454 RID: 1108
	public class CommandMap : Command
	{
		// Token: 0x06001DFB RID: 7675 RVA: 0x000A2E9C File Offset: 0x000A129C
		public CommandMap(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("MapCommandText");
			this._info = this.localization.format("MapInfoText");
			this._help = this.localization.format("MapHelpText");
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x000A2EF8 File Offset: 0x000A12F8
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			if (!Level.exists(parameter))
			{
				CommandWindow.LogError(this.localization.format("NoMapErrorText", new object[]
				{
					parameter
				}));
				return;
			}
			if (Provider.isServer)
			{
				CommandWindow.LogError(this.localization.format("RunningErrorText"));
				return;
			}
			Provider.map = parameter;
			CommandWindow.Log(this.localization.format("MapText", new object[]
			{
				parameter
			}));
		}
	}
}
