﻿using System;
using SDG.Framework.Modules;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000457 RID: 1111
	public class CommandModules : Command
	{
		// Token: 0x06001E02 RID: 7682 RVA: 0x000A3254 File Offset: 0x000A1654
		public CommandModules(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("ModulesCommandText");
			this._info = this.localization.format("ModulesInfoText");
			this._help = this.localization.format("ModulesHelpText");
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x000A32B0 File Offset: 0x000A16B0
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (ModuleHook.modules.Count == 0)
			{
				CommandWindow.LogError(this.localization.format("NoModulesErrorText"));
				return;
			}
			CommandWindow.Log(this.localization.format("ModulesText"));
			CommandWindow.Log(this.localization.format("SeparatorText"));
			for (int i = 0; i < ModuleHook.modules.Count; i++)
			{
				Module module = ModuleHook.modules[i];
				if (module != null)
				{
					ModuleConfig config = module.config;
					if (config != null)
					{
						Local local = Localization.tryRead(config.DirectoryPath, false);
						CommandWindow.Log(local.format("Name"));
						CommandWindow.Log(this.localization.format("Version", new object[]
						{
							config.Version
						}));
						CommandWindow.Log(local.format("Description"));
						CommandWindow.Log(this.localization.format("SeparatorText"));
					}
				}
			}
		}
	}
}
