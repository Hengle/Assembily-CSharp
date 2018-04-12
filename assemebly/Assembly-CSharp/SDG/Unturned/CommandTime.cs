using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200046D RID: 1133
	public class CommandTime : Command
	{
		// Token: 0x06001E30 RID: 7728 RVA: 0x000A4F90 File Offset: 0x000A3390
		public CommandTime(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("TimeCommandText");
			this._info = this.localization.format("TimeInfoText");
			this._help = this.localization.format("TimeHelpText");
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x000A4FEC File Offset: 0x000A33EC
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Provider.isServer)
			{
				CommandWindow.LogError(this.localization.format("NotRunningErrorText"));
				return;
			}
			if (Provider.isServer && Level.info.type == ELevelType.HORDE)
			{
				CommandWindow.LogError(this.localization.format("HordeErrorText"));
				return;
			}
			if (Provider.isServer && Level.info.type == ELevelType.ARENA)
			{
				CommandWindow.LogError(this.localization.format("ArenaErrorText"));
				return;
			}
			uint num;
			if (!uint.TryParse(parameter, out num))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					parameter
				}));
				return;
			}
			LightingManager.time = num;
			CommandWindow.Log(this.localization.format("TimeText", new object[]
			{
				num
			}));
		}
	}
}
