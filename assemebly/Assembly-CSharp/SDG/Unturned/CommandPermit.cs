using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200045C RID: 1116
	public class CommandPermit : Command
	{
		// Token: 0x06001E0D RID: 7693 RVA: 0x000A37BC File Offset: 0x000A1BBC
		public CommandPermit(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("PermitCommandText");
			this._info = this.localization.format("PermitInfoText");
			this._help = this.localization.format("PermitHelpText");
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x000A3818 File Offset: 0x000A1C18
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			if (!Provider.isServer)
			{
				CommandWindow.LogError(this.localization.format("NotRunningErrorText"));
				return;
			}
			string[] componentsFromSerial = Parser.getComponentsFromSerial(parameter, '/');
			if (componentsFromSerial.Length != 2)
			{
				CommandWindow.LogError(this.localization.format("InvalidParameterErrorText"));
				return;
			}
			CSteamID csteamID;
			if (!PlayerTool.tryGetSteamID(componentsFromSerial[0], out csteamID))
			{
				CommandWindow.LogError(this.localization.format("InvalidSteamIDErrorText", new object[]
				{
					componentsFromSerial[0]
				}));
				return;
			}
			SteamWhitelist.whitelist(csteamID, componentsFromSerial[1], executorID);
			CommandWindow.Log(this.localization.format("PermitText", new object[]
			{
				csteamID,
				componentsFromSerial[1]
			}));
		}
	}
}
