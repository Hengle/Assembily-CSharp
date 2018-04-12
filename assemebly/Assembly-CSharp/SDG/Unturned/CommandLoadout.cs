using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000452 RID: 1106
	public class CommandLoadout : Command
	{
		// Token: 0x06001DF7 RID: 7671 RVA: 0x000A2ADC File Offset: 0x000A0EDC
		public CommandLoadout(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("LoadoutCommandText");
			this._info = this.localization.format("LoadoutInfoText");
			this._help = this.localization.format("LoadoutHelpText");
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x000A2B38 File Offset: 0x000A0F38
		protected override void execute(CSteamID executorID, string parameter)
		{
			string[] componentsFromSerial = Parser.getComponentsFromSerial(parameter, '/');
			if (componentsFromSerial.Length < 1)
			{
				CommandWindow.LogError(this.localization.format("InvalidParameterErrorText"));
				return;
			}
			byte b;
			if (!byte.TryParse(componentsFromSerial[0], out b) || (b != 255 && b > 10))
			{
				CommandWindow.LogError(this.localization.format("InvalidSkillsetIDErrorText", new object[]
				{
					componentsFromSerial[0]
				}));
				return;
			}
			ushort[] array = new ushort[componentsFromSerial.Length - 1];
			for (int i = 1; i < componentsFromSerial.Length; i++)
			{
				ushort num;
				if (!ushort.TryParse(componentsFromSerial[i], out num))
				{
					CommandWindow.LogError(this.localization.format("InvalidItemIDErrorText", new object[]
					{
						componentsFromSerial[i]
					}));
					return;
				}
				array[i - 1] = num;
			}
			if (b == 255)
			{
				PlayerInventory.loadout = array;
			}
			else
			{
				PlayerInventory.skillsets[(int)b] = array;
			}
			CommandWindow.Log(this.localization.format("LoadoutText", new object[]
			{
				b
			}));
		}
	}
}
