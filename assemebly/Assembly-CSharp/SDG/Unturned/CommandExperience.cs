using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000448 RID: 1096
	public class CommandExperience : Command
	{
		// Token: 0x06001DE3 RID: 7651 RVA: 0x000A1D78 File Offset: 0x000A0178
		public CommandExperience(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("ExperienceCommandText");
			this._info = this.localization.format("ExperienceInfoText");
			this._help = this.localization.format("ExperienceHelpText");
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x000A1DD4 File Offset: 0x000A01D4
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
			if (componentsFromSerial.Length < 1 || componentsFromSerial.Length > 2)
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
			uint num;
			if (!uint.TryParse(componentsFromSerial[(!flag) ? 1 : 0], out num))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					componentsFromSerial[(!flag) ? 1 : 0]
				}));
				return;
			}
			steamPlayer.player.skills.askAward(num);
			CommandWindow.Log(this.localization.format("ExperienceText", new object[]
			{
				steamPlayer.playerID.playerName,
				num
			}));
		}
	}
}
