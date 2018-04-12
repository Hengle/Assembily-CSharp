using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000440 RID: 1088
	public class CommandBind : Command
	{
		// Token: 0x06001DD2 RID: 7634 RVA: 0x000A12A0 File Offset: 0x0009F6A0
		public CommandBind(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("BindCommandText");
			this._info = this.localization.format("BindInfoText");
			this._help = this.localization.format("BindHelpText");
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x000A12FC File Offset: 0x0009F6FC
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			if (!Parser.checkIP(parameter))
			{
				CommandWindow.LogError(this.localization.format("InvalidIPErrorText", new object[]
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
			Provider.ip = Parser.getUInt32FromIP(parameter);
			CommandWindow.Log(this.localization.format("BindText", new object[]
			{
				parameter
			}));
		}
	}
}
