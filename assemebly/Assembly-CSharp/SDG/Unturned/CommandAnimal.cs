using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200043C RID: 1084
	public class CommandAnimal : Command
	{
		// Token: 0x06001DCA RID: 7626 RVA: 0x000A0C4C File Offset: 0x0009F04C
		public CommandAnimal(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("AnimalCommandText");
			this._info = this.localization.format("AnimalInfoText");
			this._help = this.localization.format("AnimalHelpText");
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x000A0CA8 File Offset: 0x0009F0A8
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Provider.isServer)
			{
				CommandWindow.LogError(this.localization.format("NotRunningErrorText"));
				return;
			}
			if (!Provider.hasCheats)
			{
				CommandWindow.LogError(this.localization.format("CheatsErrorText"));
				return;
			}
			string[] componentsFromSerial = Parser.getComponentsFromSerial(parameter, '/');
			if (componentsFromSerial.Length < 1 || componentsFromSerial.Length > 3)
			{
				CommandWindow.LogError(this.localization.format("InvalidParameterErrorText"));
				return;
			}
			bool flag = false;
			SteamPlayer steamPlayer;
			if (!PlayerTool.tryGetSteamPlayer(componentsFromSerial[0], out steamPlayer))
			{
				steamPlayer = PlayerTool.getSteamPlayer(executorID);
				if (steamPlayer == null)
				{
					CommandWindow.LogError(this.localization.format("NoPlayerErrorText", new object[]
					{
						componentsFromSerial[0]
					}));
					return;
				}
				flag = true;
			}
			ushort num;
			if (!ushort.TryParse(componentsFromSerial[(!flag) ? 1 : 0], out num))
			{
				CommandWindow.LogError(this.localization.format("InvalidAnimalIDErrorText", new object[]
				{
					componentsFromSerial[(!flag) ? 1 : 0]
				}));
				return;
			}
			if (!AnimalManager.giveAnimal(steamPlayer.player, num))
			{
				CommandWindow.LogError(this.localization.format("NoAnimalIDErrorText", new object[]
				{
					num
				}));
				return;
			}
			CommandWindow.Log(this.localization.format("AnimalText", new object[]
			{
				steamPlayer.playerID.playerName,
				num
			}));
		}
	}
}
