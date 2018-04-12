using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200044A RID: 1098
	public class CommandFlag : Command
	{
		// Token: 0x06001DE7 RID: 7655 RVA: 0x000A1FD0 File Offset: 0x000A03D0
		public CommandFlag(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("FlagCommandText");
			this._info = this.localization.format("FlagInfoText");
			this._help = this.localization.format("FlagHelpText");
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x000A202C File Offset: 0x000A042C
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
			if (componentsFromSerial.Length < 2 || componentsFromSerial.Length > 3)
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
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					componentsFromSerial[(!flag) ? 1 : 0]
				}));
				return;
			}
			short num2;
			if (!short.TryParse(componentsFromSerial[(!flag) ? 2 : 1], out num2))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					componentsFromSerial[(!flag) ? 2 : 1]
				}));
				return;
			}
			steamPlayer.player.quests.sendSetFlag(num, num2);
			CommandWindow.Log(this.localization.format("FlagText", new object[]
			{
				steamPlayer.playerID.playerName,
				num,
				num2
			}));
		}
	}
}
