﻿using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000461 RID: 1121
	public class CommandQuest : Command
	{
		// Token: 0x06001E17 RID: 7703 RVA: 0x000A3CE0 File Offset: 0x000A20E0
		public CommandQuest(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("QuestCommandText");
			this._info = this.localization.format("QuestInfoText");
			this._help = this.localization.format("QuestHelpText");
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x000A3D3C File Offset: 0x000A213C
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
			ushort num;
			if (!ushort.TryParse(componentsFromSerial[(!flag) ? 1 : 0], out num))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					componentsFromSerial[(!flag) ? 1 : 0]
				}));
				return;
			}
			steamPlayer.player.quests.sendAddQuest(num);
			CommandWindow.Log(this.localization.format("QuestText", new object[]
			{
				steamPlayer.playerID.playerName,
				num
			}));
		}
	}
}
