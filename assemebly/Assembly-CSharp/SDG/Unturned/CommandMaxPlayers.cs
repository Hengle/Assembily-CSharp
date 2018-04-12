using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000455 RID: 1109
	public class CommandMaxPlayers : Command
	{
		// Token: 0x06001DFD RID: 7677 RVA: 0x000A2F80 File Offset: 0x000A1380
		public CommandMaxPlayers(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("MaxPlayersCommandText");
			this._info = this.localization.format("MaxPlayersInfoText");
			this._help = this.localization.format("MaxPlayersHelpText");
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x000A2FDC File Offset: 0x000A13DC
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			byte b;
			if (!byte.TryParse(parameter, out b))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					parameter
				}));
				return;
			}
			if (b < CommandMaxPlayers.MIN_NUMBER)
			{
				CommandWindow.LogError(this.localization.format("MinNumberErrorText", new object[]
				{
					CommandMaxPlayers.MIN_NUMBER
				}));
				return;
			}
			if (b > CommandMaxPlayers.MAX_NUMBER)
			{
				CommandWindow.LogError(this.localization.format("MaxNumberErrorText", new object[]
				{
					CommandMaxPlayers.MAX_NUMBER
				}));
				return;
			}
			if (b > CommandMaxPlayers.MAX_NUMBER / 2)
			{
				CommandWindow.LogWarning(this.localization.format("RecommendedNumberErrorText", new object[]
				{
					(int)(CommandMaxPlayers.MAX_NUMBER / 2)
				}));
			}
			Provider.maxPlayers = b;
			CommandWindow.Log(this.localization.format("MaxPlayersText", new object[]
			{
				b
			}));
		}

		// Token: 0x040011D6 RID: 4566
		public static readonly byte MIN_NUMBER = 1;

		// Token: 0x040011D7 RID: 4567
		public static readonly byte MAX_NUMBER = 48;
	}
}
