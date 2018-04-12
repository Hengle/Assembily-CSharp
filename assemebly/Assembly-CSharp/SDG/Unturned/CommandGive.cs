﻿using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200044C RID: 1100
	public class CommandGive : Command
	{
		// Token: 0x06001DEB RID: 7659 RVA: 0x000A228C File Offset: 0x000A068C
		public CommandGive(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("GiveCommandText");
			this._info = this.localization.format("GiveInfoText");
			this._help = this.localization.format("GiveHelpText");
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x000A22E8 File Offset: 0x000A06E8
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
				CommandWindow.LogError(this.localization.format("InvalidItemIDErrorText", new object[]
				{
					componentsFromSerial[(!flag) ? 1 : 0]
				}));
				return;
			}
			byte b = 1;
			if (flag)
			{
				if (componentsFromSerial.Length > 1 && !byte.TryParse(componentsFromSerial[1], out b))
				{
					CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
					{
						componentsFromSerial[1]
					}));
					return;
				}
			}
			else if (componentsFromSerial.Length > 2 && !byte.TryParse(componentsFromSerial[2], out b))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					componentsFromSerial[2]
				}));
				return;
			}
			if (!ItemTool.tryForceGiveItem(steamPlayer.player, num, b))
			{
				CommandWindow.LogError(this.localization.format("NoItemIDErrorText", new object[]
				{
					num
				}));
				return;
			}
			CommandWindow.Log(this.localization.format("GiveText", new object[]
			{
				steamPlayer.playerID.playerName,
				num,
				b
			}));
		}
	}
}
