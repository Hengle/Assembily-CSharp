using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000445 RID: 1093
	public class CommandDay : Command
	{
		// Token: 0x06001DDD RID: 7645 RVA: 0x000A1818 File Offset: 0x0009FC18
		public CommandDay(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("DayCommandText");
			this._info = this.localization.format("DayInfoText");
			this._help = this.localization.format("DayHelpText");
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x000A1874 File Offset: 0x0009FC74
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
			LightingManager.time = (uint)(LightingManager.cycle * LevelLighting.transition);
			CommandWindow.Log(this.localization.format("DayText"));
		}
	}
}
