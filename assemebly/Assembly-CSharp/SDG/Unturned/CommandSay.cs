using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000467 RID: 1127
	public class CommandSay : Command
	{
		// Token: 0x06001E24 RID: 7716 RVA: 0x000A4540 File Offset: 0x000A2940
		public CommandSay(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("SayCommandText");
			this._info = this.localization.format("SayInfoText");
			this._help = this.localization.format("SayHelpText");
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x000A459C File Offset: 0x000A299C
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			if (!Provider.isServer)
			{
				CommandWindow.LogError(this.localization.format("NotRunningErrorText"));
				return;
			}
			string[] componentsFromSerial = Parser.getComponentsFromSerial(parameter, '/');
			if (componentsFromSerial.Length != 1 && componentsFromSerial.Length != 4)
			{
				CommandWindow.LogError(this.localization.format("InvalidParameterErrorText"));
				return;
			}
			if (componentsFromSerial.Length == 1)
			{
				ChatManager.say(componentsFromSerial[0], Palette.SERVER, false);
			}
			else if (componentsFromSerial.Length == 4)
			{
				byte b;
				if (!byte.TryParse(componentsFromSerial[1], out b))
				{
					CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
					{
						componentsFromSerial[0]
					}));
					return;
				}
				byte b2;
				if (!byte.TryParse(componentsFromSerial[2], out b2))
				{
					CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
					{
						componentsFromSerial[1]
					}));
					return;
				}
				byte b3;
				if (!byte.TryParse(componentsFromSerial[3], out b3))
				{
					CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
					{
						componentsFromSerial[2]
					}));
					return;
				}
				ChatManager.say(componentsFromSerial[0], new Color((float)b / 255f, (float)b2 / 255f, (float)b3 / 255f), false);
			}
		}
	}
}
