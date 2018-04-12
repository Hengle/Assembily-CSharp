using System;
using System.Net;
using System.Runtime.CompilerServices;
using SDG.Provider;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000773 RID: 1907
	public class MenuPlayConnectUI
	{
		// Token: 0x0600369A RID: 13978 RVA: 0x00175C5C File Offset: 0x0017405C
		public MenuPlayConnectUI()
		{
			MenuPlayConnectUI.localization = Localization.read("/Menu/Play/MenuPlayConnect.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Play/MenuPlayConnect/MenuPlayConnect.unity3d");
			MenuPlayConnectUI.container = new Sleek();
			MenuPlayConnectUI.container.positionOffset_X = 10;
			MenuPlayConnectUI.container.positionOffset_Y = 10;
			MenuPlayConnectUI.container.positionScale_Y = 1f;
			MenuPlayConnectUI.container.sizeOffset_X = -20;
			MenuPlayConnectUI.container.sizeOffset_Y = -20;
			MenuPlayConnectUI.container.sizeScale_X = 1f;
			MenuPlayConnectUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuPlayConnectUI.container);
			MenuPlayConnectUI.active = false;
			MenuPlayConnectUI.ipField = new SleekField();
			MenuPlayConnectUI.ipField.positionOffset_X = -100;
			MenuPlayConnectUI.ipField.positionOffset_Y = -75;
			MenuPlayConnectUI.ipField.positionScale_X = 0.5f;
			MenuPlayConnectUI.ipField.positionScale_Y = 0.5f;
			MenuPlayConnectUI.ipField.sizeOffset_X = 200;
			MenuPlayConnectUI.ipField.sizeOffset_Y = 30;
			MenuPlayConnectUI.ipField.maxLength = 64;
			MenuPlayConnectUI.ipField.addLabel(MenuPlayConnectUI.localization.format("IP_Field_Label"), ESleekSide.RIGHT);
			MenuPlayConnectUI.ipField.text = PlaySettings.connectIP;
			SleekField sleekField = MenuPlayConnectUI.ipField;
			if (MenuPlayConnectUI.<>f__mg$cache0 == null)
			{
				MenuPlayConnectUI.<>f__mg$cache0 = new Typed(MenuPlayConnectUI.onTypedIPField);
			}
			sleekField.onTyped = MenuPlayConnectUI.<>f__mg$cache0;
			MenuPlayConnectUI.container.add(MenuPlayConnectUI.ipField);
			MenuPlayConnectUI.portField = new SleekUInt16Field();
			MenuPlayConnectUI.portField.positionOffset_X = -100;
			MenuPlayConnectUI.portField.positionOffset_Y = -35;
			MenuPlayConnectUI.portField.positionScale_X = 0.5f;
			MenuPlayConnectUI.portField.positionScale_Y = 0.5f;
			MenuPlayConnectUI.portField.sizeOffset_X = 200;
			MenuPlayConnectUI.portField.sizeOffset_Y = 30;
			MenuPlayConnectUI.portField.addLabel(MenuPlayConnectUI.localization.format("Port_Field_Label"), ESleekSide.RIGHT);
			MenuPlayConnectUI.portField.state = PlaySettings.connectPort;
			SleekUInt16Field sleekUInt16Field = MenuPlayConnectUI.portField;
			if (MenuPlayConnectUI.<>f__mg$cache1 == null)
			{
				MenuPlayConnectUI.<>f__mg$cache1 = new TypedUInt16(MenuPlayConnectUI.onTypedPortField);
			}
			sleekUInt16Field.onTypedUInt16 = MenuPlayConnectUI.<>f__mg$cache1;
			MenuPlayConnectUI.container.add(MenuPlayConnectUI.portField);
			MenuPlayConnectUI.passwordField = new SleekField();
			MenuPlayConnectUI.passwordField.positionOffset_X = -100;
			MenuPlayConnectUI.passwordField.positionOffset_Y = 5;
			MenuPlayConnectUI.passwordField.positionScale_X = 0.5f;
			MenuPlayConnectUI.passwordField.positionScale_Y = 0.5f;
			MenuPlayConnectUI.passwordField.sizeOffset_X = 200;
			MenuPlayConnectUI.passwordField.sizeOffset_Y = 30;
			MenuPlayConnectUI.passwordField.addLabel(MenuPlayConnectUI.localization.format("Password_Field_Label"), ESleekSide.RIGHT);
			MenuPlayConnectUI.passwordField.replace = MenuPlayConnectUI.localization.format("Password_Field_Replace").ToCharArray()[0];
			MenuPlayConnectUI.passwordField.text = PlaySettings.connectPassword;
			SleekField sleekField2 = MenuPlayConnectUI.passwordField;
			if (MenuPlayConnectUI.<>f__mg$cache2 == null)
			{
				MenuPlayConnectUI.<>f__mg$cache2 = new Typed(MenuPlayConnectUI.onTypedPasswordField);
			}
			sleekField2.onTyped = MenuPlayConnectUI.<>f__mg$cache2;
			MenuPlayConnectUI.container.add(MenuPlayConnectUI.passwordField);
			MenuPlayConnectUI.connectButton = new SleekButtonIcon((Texture2D)bundle.load("Connect"));
			MenuPlayConnectUI.connectButton.positionOffset_X = -100;
			MenuPlayConnectUI.connectButton.positionOffset_Y = 45;
			MenuPlayConnectUI.connectButton.positionScale_X = 0.5f;
			MenuPlayConnectUI.connectButton.positionScale_Y = 0.5f;
			MenuPlayConnectUI.connectButton.sizeOffset_X = 200;
			MenuPlayConnectUI.connectButton.sizeOffset_Y = 30;
			MenuPlayConnectUI.connectButton.text = MenuPlayConnectUI.localization.format("Connect_Button");
			MenuPlayConnectUI.connectButton.tooltip = MenuPlayConnectUI.localization.format("Connect_Button_Tooltip");
			MenuPlayConnectUI.connectButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton = MenuPlayConnectUI.connectButton;
			if (MenuPlayConnectUI.<>f__mg$cache3 == null)
			{
				MenuPlayConnectUI.<>f__mg$cache3 = new ClickedButton(MenuPlayConnectUI.onClickedConnectButton);
			}
			sleekButton.onClickedButton = MenuPlayConnectUI.<>f__mg$cache3;
			MenuPlayConnectUI.container.add(MenuPlayConnectUI.connectButton);
			TempSteamworksMatchmaking matchmakingService = Provider.provider.matchmakingService;
			if (MenuPlayConnectUI.<>f__mg$cache4 == null)
			{
				MenuPlayConnectUI.<>f__mg$cache4 = new TempSteamworksMatchmaking.AttemptUpdated(MenuPlayConnectUI.onAttemptUpdated);
			}
			matchmakingService.onAttemptUpdated = MenuPlayConnectUI.<>f__mg$cache4;
			TempSteamworksMatchmaking matchmakingService2 = Provider.provider.matchmakingService;
			if (MenuPlayConnectUI.<>f__mg$cache5 == null)
			{
				MenuPlayConnectUI.<>f__mg$cache5 = new TempSteamworksMatchmaking.TimedOut(MenuPlayConnectUI.onTimedOut);
			}
			matchmakingService2.onTimedOut = MenuPlayConnectUI.<>f__mg$cache5;
			if (!MenuPlayConnectUI.isLaunched)
			{
				MenuPlayConnectUI.isLaunched = true;
				uint newIP;
				ushort newPort;
				string newPassword;
				ulong num;
				if (CommandLine.tryGetConnect(Environment.CommandLine, out newIP, out newPort, out newPassword))
				{
					SteamConnectionInfo steamConnectionInfo = new SteamConnectionInfo(newIP, newPort, newPassword);
					Debug.Log(string.Concat(new object[]
					{
						steamConnectionInfo.ip,
						" ",
						Parser.getIPFromUInt32(steamConnectionInfo.ip),
						" ",
						steamConnectionInfo.port,
						" ",
						steamConnectionInfo.password
					}));
					Provider.provider.matchmakingService.connect(steamConnectionInfo);
					MenuUI.openAlert(MenuPlayConnectUI.localization.format("Connecting"));
				}
				else if (CommandLine.tryGetLobby(Environment.CommandLine, out num))
				{
					Debug.Log("Lobby: " + num);
					Lobbies.joinLobby(new CSteamID(num));
				}
			}
			bundle.unload();
			MenuPlayConnectUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuPlayConnectUI.backButton.positionOffset_Y = -50;
			MenuPlayConnectUI.backButton.positionScale_Y = 1f;
			MenuPlayConnectUI.backButton.sizeOffset_X = 200;
			MenuPlayConnectUI.backButton.sizeOffset_Y = 50;
			MenuPlayConnectUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuPlayConnectUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton2 = MenuPlayConnectUI.backButton;
			if (MenuPlayConnectUI.<>f__mg$cache6 == null)
			{
				MenuPlayConnectUI.<>f__mg$cache6 = new ClickedButton(MenuPlayConnectUI.onClickedBackButton);
			}
			sleekButton2.onClickedButton = MenuPlayConnectUI.<>f__mg$cache6;
			MenuPlayConnectUI.backButton.fontSize = 14;
			MenuPlayConnectUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuPlayConnectUI.container.add(MenuPlayConnectUI.backButton);
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x00176262 File Offset: 0x00174662
		public static void connect(SteamConnectionInfo info)
		{
			Provider.provider.matchmakingService.connect(info);
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x00176274 File Offset: 0x00174674
		public static void open()
		{
			if (MenuPlayConnectUI.active)
			{
				return;
			}
			MenuPlayConnectUI.active = true;
			MenuPlayConnectUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x001762A1 File Offset: 0x001746A1
		public static void close()
		{
			if (!MenuPlayConnectUI.active)
			{
				return;
			}
			MenuPlayConnectUI.active = false;
			MenuSettings.save();
			MenuPlayConnectUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x001762D4 File Offset: 0x001746D4
		private static void onClickedConnectButton(SleekButton button)
		{
			if (MenuPlayConnectUI.ipField.text != string.Empty && MenuPlayConnectUI.portField.state != 0)
			{
				string text;
				if (MenuPlayConnectUI.ipField.text.ToLower() == "localhost")
				{
					text = "127.0.0.1";
				}
				else
				{
					IPAddress[] hostAddresses = Dns.GetHostAddresses(MenuPlayConnectUI.ipField.text);
					if (hostAddresses.Length != 1 || hostAddresses[0] == null)
					{
						return;
					}
					text = hostAddresses[0].ToString();
				}
				if (Parser.checkIP(text))
				{
					SteamConnectionInfo info = new SteamConnectionInfo(text, MenuPlayConnectUI.portField.state, MenuPlayConnectUI.passwordField.text);
					MenuSettings.save();
					MenuPlayConnectUI.connect(info);
				}
			}
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x0017638E File Offset: 0x0017478E
		private static void onTypedIPField(SleekField field, string text)
		{
			PlaySettings.connectIP = text;
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x00176396 File Offset: 0x00174796
		private static void onTypedPortField(SleekUInt16Field field, ushort state)
		{
			PlaySettings.connectPort = state;
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x0017639E File Offset: 0x0017479E
		private static void onTypedPasswordField(SleekField field, string text)
		{
			PlaySettings.connectPassword = text;
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x001763A6 File Offset: 0x001747A6
		private static void onAttemptUpdated(int attempt)
		{
			MenuUI.openAlert(MenuPlayConnectUI.localization.format("Connecting", new object[]
			{
				attempt
			}));
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x001763CC File Offset: 0x001747CC
		private static void onTimedOut()
		{
			if (Provider.connectionFailureInfo != ESteamConnectionFailureInfo.NONE)
			{
				ESteamConnectionFailureInfo connectionFailureInfo = Provider.connectionFailureInfo;
				Provider.resetConnectionFailure();
				if (connectionFailureInfo == ESteamConnectionFailureInfo.PRO_SERVER)
				{
					MenuUI.alert(MenuPlayConnectUI.localization.format("Pro_Server"));
				}
				else if (connectionFailureInfo == ESteamConnectionFailureInfo.PASSWORD)
				{
					MenuUI.alert(MenuPlayConnectUI.localization.format("Password"));
				}
				else if (connectionFailureInfo == ESteamConnectionFailureInfo.FULL)
				{
					MenuUI.alert(MenuPlayConnectUI.localization.format("Full"));
				}
				else if (connectionFailureInfo == ESteamConnectionFailureInfo.TIMED_OUT)
				{
					MenuUI.alert(MenuPlayConnectUI.localization.format("Timed_Out"));
				}
			}
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x0017646B File Offset: 0x0017486B
		private static void onClickedBackButton(SleekButton button)
		{
			MenuPlayUI.open();
			MenuPlayConnectUI.close();
		}

		// Token: 0x04002736 RID: 10038
		private static Local localization;

		// Token: 0x04002737 RID: 10039
		private static Sleek container;

		// Token: 0x04002738 RID: 10040
		public static bool active;

		// Token: 0x04002739 RID: 10041
		private static SleekButtonIcon backButton;

		// Token: 0x0400273A RID: 10042
		private static SleekField ipField;

		// Token: 0x0400273B RID: 10043
		private static SleekUInt16Field portField;

		// Token: 0x0400273C RID: 10044
		private static SleekField passwordField;

		// Token: 0x0400273D RID: 10045
		private static SleekButtonIcon connectButton;

		// Token: 0x0400273E RID: 10046
		private static bool isLaunched;

		// Token: 0x0400273F RID: 10047
		[CompilerGenerated]
		private static Typed <>f__mg$cache0;

		// Token: 0x04002740 RID: 10048
		[CompilerGenerated]
		private static TypedUInt16 <>f__mg$cache1;

		// Token: 0x04002741 RID: 10049
		[CompilerGenerated]
		private static Typed <>f__mg$cache2;

		// Token: 0x04002742 RID: 10050
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x04002743 RID: 10051
		[CompilerGenerated]
		private static TempSteamworksMatchmaking.AttemptUpdated <>f__mg$cache4;

		// Token: 0x04002744 RID: 10052
		[CompilerGenerated]
		private static TempSteamworksMatchmaking.TimedOut <>f__mg$cache5;

		// Token: 0x04002745 RID: 10053
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache6;
	}
}
