﻿using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000446 RID: 1094
	public class CommandDebug : Command
	{
		// Token: 0x06001DDF RID: 7647 RVA: 0x000A192C File Offset: 0x0009FD2C
		public CommandDebug(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("DebugCommandText");
			this._info = this.localization.format("DebugInfoText");
			this._help = this.localization.format("DebugHelpText");
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x000A1988 File Offset: 0x0009FD88
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
			CommandWindow.Log(this.localization.format("DebugText"));
			CommandWindow.Log(this.localization.format("DebugIPPortText", new object[]
			{
				SteamGameServer.GetSteamID(),
				Parser.getIPFromUInt32(SteamGameServer.GetPublicIP()),
				Provider.port
			}));
			CommandWindow.Log(this.localization.format("DebugBytesSentText", new object[]
			{
				Provider.bytesSent + "B"
			}));
			CommandWindow.Log(this.localization.format("DebugBytesReceivedText", new object[]
			{
				Provider.bytesReceived + "B"
			}));
			CommandWindow.Log(this.localization.format("DebugAverageBytesSentText", new object[]
			{
				(uint)(Provider.bytesSent / Time.realtimeSinceStartup) + "B"
			}));
			CommandWindow.Log(this.localization.format("DebugAverageBytesReceivedText", new object[]
			{
				(uint)(Provider.bytesReceived / Time.realtimeSinceStartup) + "B"
			}));
			CommandWindow.Log(this.localization.format("DebugPacketsSentText", new object[]
			{
				Provider.packetsSent
			}));
			CommandWindow.Log(this.localization.format("DebugPacketsReceivedText", new object[]
			{
				Provider.packetsReceived
			}));
			CommandWindow.Log(this.localization.format("DebugAveragePacketsSentText", new object[]
			{
				(uint)(Provider.packetsSent / Time.realtimeSinceStartup)
			}));
			CommandWindow.Log(this.localization.format("DebugAveragePacketsReceivedText", new object[]
			{
				(uint)(Provider.packetsReceived / Time.realtimeSinceStartup)
			}));
			CommandWindow.Log(this.localization.format("DebugUPSText", new object[]
			{
				Mathf.CeilToInt((float)Provider.debugUPS / 50f * 100f)
			}));
			CommandWindow.Log(this.localization.format("DebugTPSText", new object[]
			{
				Mathf.CeilToInt((float)Provider.debugTPS / 50f * 100f)
			}));
			CommandWindow.Log(this.localization.format("DebugZombiesText", new object[]
			{
				ZombieManager.tickingZombies.Count
			}));
			CommandWindow.Log(this.localization.format("DebugAnimalsText", new object[]
			{
				AnimalManager.tickingAnimals.Count
			}));
		}
	}
}
