using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000456 RID: 1110
	public class CommandMode : Command
	{
		// Token: 0x06001E00 RID: 7680 RVA: 0x000A30FC File Offset: 0x000A14FC
		public CommandMode(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("ModeCommandText");
			this._info = this.localization.format("ModeInfoText");
			this._help = this.localization.format("ModeHelpText");
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x000A3158 File Offset: 0x000A1558
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			string text = parameter.ToLower();
			EGameMode mode;
			if (text == this.localization.format("ModeEasy").ToLower())
			{
				mode = EGameMode.EASY;
			}
			else if (text == this.localization.format("ModeNormal").ToLower())
			{
				mode = EGameMode.NORMAL;
			}
			else
			{
				if (!(text == this.localization.format("ModeHard").ToLower()))
				{
					CommandWindow.LogError(this.localization.format("NoModeErrorText", new object[]
					{
						text
					}));
					return;
				}
				mode = EGameMode.HARD;
			}
			if (Provider.isServer)
			{
				CommandWindow.LogError(this.localization.format("RunningErrorText"));
				return;
			}
			Provider.mode = mode;
			CommandWindow.Log(this.localization.format("ModeText", new object[]
			{
				text
			}));
		}
	}
}
