using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000442 RID: 1090
	public class CommandChatrate : Command
	{
		// Token: 0x06001DD6 RID: 7638 RVA: 0x000A1508 File Offset: 0x0009F908
		public CommandChatrate(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("ChatrateCommandText");
			this._info = this.localization.format("ChatrateInfoText");
			this._help = this.localization.format("ChatrateHelpText");
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x000A1564 File Offset: 0x0009F964
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			float num;
			if (!float.TryParse(parameter, out num))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					parameter
				}));
				return;
			}
			if (num < CommandChatrate.MIN_NUMBER)
			{
				CommandWindow.LogError(this.localization.format("MinNumberErrorText", new object[]
				{
					CommandChatrate.MIN_NUMBER
				}));
				return;
			}
			if (num > CommandChatrate.MAX_NUMBER)
			{
				CommandWindow.LogError(this.localization.format("MaxNumberErrorText", new object[]
				{
					CommandChatrate.MAX_NUMBER
				}));
				return;
			}
			ChatManager.chatrate = num;
			CommandWindow.Log(this.localization.format("ChatrateText", new object[]
			{
				num
			}));
		}

		// Token: 0x040011D4 RID: 4564
		private static readonly float MIN_NUMBER;

		// Token: 0x040011D5 RID: 4565
		private static readonly float MAX_NUMBER = 60f;
	}
}
