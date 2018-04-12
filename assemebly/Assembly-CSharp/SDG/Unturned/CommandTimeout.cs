using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200046E RID: 1134
	public class CommandTimeout : Command
	{
		// Token: 0x06001E32 RID: 7730 RVA: 0x000A50D0 File Offset: 0x000A34D0
		public CommandTimeout(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("TimeoutCommandText");
			this._info = this.localization.format("TimeoutInfoText");
			this._help = this.localization.format("TimeoutHelpText");
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x000A512C File Offset: 0x000A352C
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			ushort num;
			if (!ushort.TryParse(parameter, out num))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					parameter
				}));
				return;
			}
			if (num < CommandTimeout.MIN_NUMBER)
			{
				CommandWindow.LogError(this.localization.format("MinNumberErrorText", new object[]
				{
					CommandTimeout.MIN_NUMBER
				}));
				return;
			}
			if (num > CommandTimeout.MAX_NUMBER)
			{
				CommandWindow.LogError(this.localization.format("MaxNumberErrorText", new object[]
				{
					CommandTimeout.MAX_NUMBER
				}));
				return;
			}
			if (Provider.configData != null)
			{
				Provider.configData.Server.Max_Ping_Milliseconds = (uint)num;
			}
			CommandWindow.Log(this.localization.format("TimeoutText", new object[]
			{
				num
			}));
		}

		// Token: 0x040011DB RID: 4571
		private static readonly ushort MIN_NUMBER = 50;

		// Token: 0x040011DC RID: 4572
		private static readonly ushort MAX_NUMBER = 10000;
	}
}
