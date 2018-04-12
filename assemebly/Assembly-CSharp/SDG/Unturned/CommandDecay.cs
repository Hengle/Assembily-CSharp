using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000447 RID: 1095
	public class CommandDecay : Command
	{
		// Token: 0x06001DE1 RID: 7649 RVA: 0x000A1C74 File Offset: 0x000A0074
		public CommandDecay(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("DecayCommandText");
			this._info = this.localization.format("DecayInfoText");
			this._help = this.localization.format("DecayHelpText");
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x000A1CD0 File Offset: 0x000A00D0
		protected override void execute(CSteamID executorID, string parameter)
		{
			string[] componentsFromSerial = Parser.getComponentsFromSerial(parameter, '/');
			if (componentsFromSerial.Length != 2)
			{
				CommandWindow.LogError(this.localization.format("InvalidParameterErrorText"));
				return;
			}
			uint num;
			if (!uint.TryParse(componentsFromSerial[0], out num))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					parameter
				}));
				return;
			}
			uint num2;
			if (!uint.TryParse(componentsFromSerial[1], out num2))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					parameter
				}));
				return;
			}
			CommandWindow.Log(this.localization.format("DecayText"));
		}
	}
}
