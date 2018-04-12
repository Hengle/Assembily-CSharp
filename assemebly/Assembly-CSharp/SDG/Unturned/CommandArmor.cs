using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200043D RID: 1085
	public class CommandArmor : Command
	{
		// Token: 0x06001DCC RID: 7628 RVA: 0x000A0E1C File Offset: 0x0009F21C
		public CommandArmor(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("ArmorCommandText");
			this._info = this.localization.format("ArmorInfoText");
			this._help = this.localization.format("ArmorHelpText");
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x000A0E78 File Offset: 0x0009F278
		protected override void execute(CSteamID executorID, string parameter)
		{
			string[] componentsFromSerial = Parser.getComponentsFromSerial(parameter, '/');
			if (componentsFromSerial.Length != 2)
			{
				CommandWindow.LogError(this.localization.format("InvalidParameterErrorText"));
				return;
			}
			float num;
			if (!float.TryParse(componentsFromSerial[0], out num))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					parameter
				}));
				return;
			}
			float num2;
			if (!float.TryParse(componentsFromSerial[1], out num2))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					parameter
				}));
				return;
			}
			CommandWindow.Log(this.localization.format("ArmorText"));
		}
	}
}
