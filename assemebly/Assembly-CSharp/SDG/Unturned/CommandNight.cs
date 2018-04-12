using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000459 RID: 1113
	public class CommandNight : Command
	{
		// Token: 0x06001E07 RID: 7687 RVA: 0x000A34D4 File Offset: 0x000A18D4
		public CommandNight(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("NightCommandText");
			this._info = this.localization.format("NightInfoText");
			this._help = this.localization.format("NightHelpText");
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x000A3530 File Offset: 0x000A1930
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
			LightingManager.time = (uint)(LightingManager.cycle * (LevelLighting.bias + LevelLighting.transition));
			CommandWindow.Log(this.localization.format("NightText"));
		}
	}
}
