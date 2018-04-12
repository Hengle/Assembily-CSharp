using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000458 RID: 1112
	public class CommandName : Command
	{
		// Token: 0x06001E04 RID: 7684 RVA: 0x000A33B8 File Offset: 0x000A17B8
		public CommandName(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("NameCommandText");
			this._info = this.localization.format("NameInfoText");
			this._help = this.localization.format("NameHelpText");
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x000A3414 File Offset: 0x000A1814
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			if (parameter.Length < (int)CommandName.MIN_LENGTH)
			{
				CommandWindow.LogError(this.localization.format("MinLengthErrorText", new object[]
				{
					CommandName.MIN_LENGTH
				}));
				return;
			}
			if (parameter.Length > (int)CommandName.MAX_LENGTH)
			{
				CommandWindow.LogError(this.localization.format("MaxLengthErrorText", new object[]
				{
					CommandName.MAX_LENGTH
				}));
				return;
			}
			Provider.serverName = parameter;
			CommandWindow.Log(this.localization.format("NameText", new object[]
			{
				parameter
			}));
		}

		// Token: 0x040011D8 RID: 4568
		private static readonly byte MIN_LENGTH = 5;

		// Token: 0x040011D9 RID: 4569
		private static readonly byte MAX_LENGTH = 50;
	}
}
