using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000468 RID: 1128
	public class CommandShutdown : Command
	{
		// Token: 0x06001E26 RID: 7718 RVA: 0x000A46E4 File Offset: 0x000A2AE4
		public CommandShutdown(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("ShutdownCommandText");
			this._info = this.localization.format("ShutdownInfoText");
			this._help = this.localization.format("ShutdownHelpText");
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x000A4740 File Offset: 0x000A2B40
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			string[] componentsFromSerial = Parser.getComponentsFromSerial(parameter, '/');
			if (componentsFromSerial.Length > 1)
			{
				CommandWindow.LogError(this.localization.format("InvalidParameterErrorText"));
				return;
			}
			if (componentsFromSerial.Length == 0)
			{
				Provider.shutdown();
			}
			else
			{
				int timer;
				if (!int.TryParse(componentsFromSerial[0], out timer))
				{
					CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
					{
						parameter
					}));
					return;
				}
				Provider.shutdown(timer);
				CommandWindow.LogError(this.localization.format("ShutdownText", new object[]
				{
					parameter
				}));
			}
		}
	}
}
