using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200043E RID: 1086
	public class CommandBan : Command
	{
		// Token: 0x06001DCE RID: 7630 RVA: 0x000A0F20 File Offset: 0x0009F320
		public CommandBan(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("BanCommandText");
			this._info = this.localization.format("BanInfoText");
			this._help = this.localization.format("BanHelpText");
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x000A0F7C File Offset: 0x0009F37C
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
			if (componentsFromSerial.Length != 1 && componentsFromSerial.Length != 2 && componentsFromSerial.Length != 3)
			{
				CommandWindow.LogError(this.localization.format("InvalidParameterErrorText"));
				return;
			}
			CSteamID csteamID;
			if (!PlayerTool.tryGetSteamID(componentsFromSerial[0], out csteamID))
			{
				CommandWindow.LogError(this.localization.format("NoPlayerErrorText", new object[]
				{
					componentsFromSerial[0]
				}));
				return;
			}
			P2PSessionState_t p2PSessionState_t;
			uint ip;
			if (SteamGameServerNetworking.GetP2PSessionState(csteamID, out p2PSessionState_t))
			{
				ip = p2PSessionState_t.m_nRemoteIP;
			}
			else
			{
				ip = 0u;
			}
			if (componentsFromSerial.Length == 1)
			{
				SteamBlacklist.ban(csteamID, ip, executorID, this.localization.format("BanTextReason"), SteamBlacklist.PERMANENT);
				CommandWindow.Log(this.localization.format("BanTextPermanent", new object[]
				{
					csteamID
				}));
			}
			else if (componentsFromSerial.Length == 2)
			{
				SteamBlacklist.ban(csteamID, ip, executorID, componentsFromSerial[1], SteamBlacklist.PERMANENT);
				CommandWindow.Log(this.localization.format("BanTextPermanent", new object[]
				{
					csteamID
				}));
			}
			else
			{
				uint num;
				if (!uint.TryParse(componentsFromSerial[2], out num))
				{
					CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
					{
						componentsFromSerial[2]
					}));
					return;
				}
				SteamBlacklist.ban(csteamID, ip, executorID, componentsFromSerial[1], num);
				CommandWindow.Log(this.localization.format("BanText", new object[]
				{
					csteamID,
					num
				}));
			}
		}
	}
}
