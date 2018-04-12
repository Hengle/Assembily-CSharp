using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000463 RID: 1123
	public class CommandRain : Command
	{
		// Token: 0x06001E1C RID: 7708 RVA: 0x000A4120 File Offset: 0x000A2520
		public CommandRain(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("RainCommandText");
			this._info = this.localization.format("RainInfoText");
			this._help = this.localization.format("RainHelpText");
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x000A417C File Offset: 0x000A257C
		protected override void execute(CSteamID executorID, string parameter)
		{
			string[] componentsFromSerial = Parser.getComponentsFromSerial(parameter, '/');
			if (componentsFromSerial.Length != 4)
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
			float num3;
			if (!float.TryParse(componentsFromSerial[2], out num3))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					parameter
				}));
				return;
			}
			float num4;
			if (!float.TryParse(componentsFromSerial[3], out num4))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					parameter
				}));
				return;
			}
			CommandWindow.Log(this.localization.format("RainText"));
		}
	}
}
