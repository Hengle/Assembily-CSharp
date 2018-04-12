using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200045A RID: 1114
	public class CommandOwner : Command
	{
		// Token: 0x06001E09 RID: 7689 RVA: 0x000A35EC File Offset: 0x000A19EC
		public CommandOwner(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("OwnerCommandText");
			this._info = this.localization.format("OwnerInfoText");
			this._help = this.localization.format("OwnerHelpText");
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x000A3648 File Offset: 0x000A1A48
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			if (Provider.isServer)
			{
				CommandWindow.LogError(this.localization.format("RunningErrorText"));
				return;
			}
			CSteamID csteamID;
			if (!PlayerTool.tryGetSteamID(parameter, out csteamID))
			{
				CommandWindow.LogError(this.localization.format("InvalidSteamIDErrorText", new object[]
				{
					parameter
				}));
				return;
			}
			SteamAdminlist.ownerID = csteamID;
			CommandWindow.Log(this.localization.format("OwnerText", new object[]
			{
				csteamID
			}));
		}
	}
}
