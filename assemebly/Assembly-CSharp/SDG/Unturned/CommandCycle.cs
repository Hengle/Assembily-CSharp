using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000444 RID: 1092
	public class CommandCycle : Command
	{
		// Token: 0x06001DDB RID: 7643 RVA: 0x000A16F8 File Offset: 0x0009FAF8
		public CommandCycle(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("CycleCommandText");
			this._info = this.localization.format("CycleInfoText");
			this._help = this.localization.format("CycleHelpText");
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x000A1754 File Offset: 0x0009FB54
		protected override void execute(CSteamID executorID, string parameter)
		{
			uint num;
			if (!uint.TryParse(parameter, out num))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					parameter
				}));
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
			LightingManager.cycle = num;
			CommandWindow.Log(this.localization.format("CycleText", new object[]
			{
				num
			}));
		}
	}
}
