using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200045F RID: 1119
	public class CommandPort : Command
	{
		// Token: 0x06001E13 RID: 7699 RVA: 0x000A3B44 File Offset: 0x000A1F44
		public CommandPort(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("PortCommandText");
			this._info = this.localization.format("PortInfoText");
			this._help = this.localization.format("PortHelpText");
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x000A3BA0 File Offset: 0x000A1FA0
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			ushort num;
			if (!ushort.TryParse(parameter, out num))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
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
			Provider.port = num;
			CommandWindow.Log(this.localization.format("PortText", new object[]
			{
				num
			}));
		}
	}
}
