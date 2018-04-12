using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200043A RID: 1082
	public class CommandAdmins : Command
	{
		// Token: 0x06001DC6 RID: 7622 RVA: 0x000A0AA0 File Offset: 0x0009EEA0
		public CommandAdmins(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("AdminsCommandText");
			this._info = this.localization.format("AdminsInfoText");
			this._help = this.localization.format("AdminsHelpText");
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x000A0AFC File Offset: 0x0009EEFC
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			if (SteamAdminlist.list.Count == 0)
			{
				CommandWindow.LogError(this.localization.format("NoAdminsErrorText"));
				return;
			}
			CommandWindow.Log(this.localization.format("AdminsText"));
			for (int i = 0; i < SteamAdminlist.list.Count; i++)
			{
				SteamAdminID steamAdminID = SteamAdminlist.list[i];
				CommandWindow.Log(this.localization.format("AdminNameText", new object[]
				{
					steamAdminID.playerID
				}));
				CommandWindow.Log(this.localization.format("AdminJudgeText", new object[]
				{
					steamAdminID.judgeID
				}));
			}
		}
	}
}
