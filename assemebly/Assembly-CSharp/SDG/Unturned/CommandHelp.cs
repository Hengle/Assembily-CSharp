using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200044E RID: 1102
	public class CommandHelp : Command
	{
		// Token: 0x06001DEF RID: 7663 RVA: 0x000A2598 File Offset: 0x000A0998
		public CommandHelp(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("HelpCommandText");
			this._info = this.localization.format("HelpInfoText");
			this._help = this.localization.format("HelpHelpText");
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x000A25F4 File Offset: 0x000A09F4
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (parameter == string.Empty)
			{
				if (!Dedicator.isDedicated)
				{
					return;
				}
				CommandWindow.Log(this.localization.format("HelpText"));
				string text = string.Empty;
				for (int i = 0; i < Commander.commands.Count; i++)
				{
					text += Commander.commands[i].info;
					if (i < Commander.commands.Count - 1)
					{
						text += "\n";
					}
				}
				CommandWindow.Log(text);
			}
			else
			{
				for (int j = 0; j < Commander.commands.Count; j++)
				{
					if (parameter.ToLower() == Commander.commands[j].command.ToLower())
					{
						if (executorID == CSteamID.Nil)
						{
							CommandWindow.Log(Commander.commands[j].info);
							CommandWindow.Log(Commander.commands[j].help);
						}
						else
						{
							ChatManager.say(executorID, Commander.commands[j].info, Palette.SERVER, EChatMode.SAY, false);
							ChatManager.say(executorID, Commander.commands[j].help, Palette.SERVER, EChatMode.SAY, false);
						}
						return;
					}
				}
				if (executorID == CSteamID.Nil)
				{
					CommandWindow.Log(this.localization.format("NoCommandErrorText", new object[]
					{
						parameter
					}));
				}
				else
				{
					ChatManager.say(executorID, this.localization.format("NoCommandErrorText", new object[]
					{
						parameter
					}), Palette.SERVER, EChatMode.SAY, false);
				}
			}
		}
	}
}
