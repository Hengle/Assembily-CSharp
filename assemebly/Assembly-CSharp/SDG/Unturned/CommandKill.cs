using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000451 RID: 1105
	public class CommandKill : Command
	{
		// Token: 0x06001DF5 RID: 7669 RVA: 0x000A29C4 File Offset: 0x000A0DC4
		public CommandKill(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("KillCommandText");
			this._info = this.localization.format("KillInfoText");
			this._help = this.localization.format("KillHelpText");
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x000A2A20 File Offset: 0x000A0E20
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Provider.isServer)
			{
				CommandWindow.LogError(this.localization.format("NotRunningErrorText"));
				return;
			}
			SteamPlayer steamPlayer;
			if (!PlayerTool.tryGetSteamPlayer(parameter, out steamPlayer))
			{
				CommandWindow.LogError(this.localization.format("NoPlayerErrorText", new object[]
				{
					parameter
				}));
				return;
			}
			if (steamPlayer.player != null)
			{
				EPlayerKill eplayerKill;
				steamPlayer.player.life.askDamage(101, Vector3.up * 101f, EDeathCause.KILL, ELimb.SKULL, executorID, out eplayerKill);
			}
			CommandWindow.Log(this.localization.format("KillText", new object[]
			{
				steamPlayer.playerID.playerName
			}));
		}
	}
}
