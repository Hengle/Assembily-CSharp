using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200077B RID: 1915
	public class MenuPlayUI
	{
		// Token: 0x06003708 RID: 14088 RVA: 0x0017D1E4 File Offset: 0x0017B5E4
		public MenuPlayUI()
		{
			Local local = Localization.read("/Menu/Play/MenuPlay.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Play/MenuPlay/MenuPlay.unity3d");
			MenuPlayUI.container = new Sleek();
			MenuPlayUI.container.positionOffset_X = 10;
			MenuPlayUI.container.positionOffset_Y = 10;
			MenuPlayUI.container.positionScale_Y = -1f;
			MenuPlayUI.container.sizeOffset_X = -20;
			MenuPlayUI.container.sizeOffset_Y = -20;
			MenuPlayUI.container.sizeScale_X = 1f;
			MenuPlayUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuPlayUI.container);
			MenuPlayUI.active = false;
			MenuPlayUI.connectButton = new SleekButtonIcon((Texture2D)bundle.load("Connect"));
			MenuPlayUI.connectButton.positionOffset_X = -100;
			MenuPlayUI.connectButton.positionOffset_Y = 95;
			MenuPlayUI.connectButton.positionScale_X = 0.5f;
			MenuPlayUI.connectButton.positionScale_Y = 0.5f;
			MenuPlayUI.connectButton.sizeOffset_X = 200;
			MenuPlayUI.connectButton.sizeOffset_Y = 50;
			MenuPlayUI.connectButton.text = local.format("ConnectButtonText");
			MenuPlayUI.connectButton.tooltip = local.format("ConnectButtonTooltip");
			MenuPlayUI.connectButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton = MenuPlayUI.connectButton;
			if (MenuPlayUI.<>f__mg$cache0 == null)
			{
				MenuPlayUI.<>f__mg$cache0 = new ClickedButton(MenuPlayUI.onClickedConnectButton);
			}
			sleekButton.onClickedButton = MenuPlayUI.<>f__mg$cache0;
			MenuPlayUI.connectButton.fontSize = 14;
			MenuPlayUI.container.add(MenuPlayUI.connectButton);
			MenuPlayUI.serversButton = new SleekButtonIcon((Texture2D)bundle.load("Servers"));
			MenuPlayUI.serversButton.positionOffset_X = -100;
			MenuPlayUI.serversButton.positionOffset_Y = 35;
			MenuPlayUI.serversButton.positionScale_X = 0.5f;
			MenuPlayUI.serversButton.positionScale_Y = 0.5f;
			MenuPlayUI.serversButton.sizeOffset_X = 200;
			MenuPlayUI.serversButton.sizeOffset_Y = 50;
			MenuPlayUI.serversButton.text = local.format("ServersButtonText");
			MenuPlayUI.serversButton.tooltip = local.format("ServersButtonTooltip");
			MenuPlayUI.serversButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton2 = MenuPlayUI.serversButton;
			if (MenuPlayUI.<>f__mg$cache1 == null)
			{
				MenuPlayUI.<>f__mg$cache1 = new ClickedButton(MenuPlayUI.onClickedServersButton);
			}
			sleekButton2.onClickedButton = MenuPlayUI.<>f__mg$cache1;
			MenuPlayUI.serversButton.fontSize = 14;
			MenuPlayUI.container.add(MenuPlayUI.serversButton);
			MenuPlayUI.singleplayerButton = new SleekButtonIcon((Texture2D)bundle.load("Singleplayer"));
			MenuPlayUI.singleplayerButton.positionOffset_X = -100;
			MenuPlayUI.singleplayerButton.positionOffset_Y = -145;
			MenuPlayUI.singleplayerButton.positionScale_X = 0.5f;
			MenuPlayUI.singleplayerButton.positionScale_Y = 0.5f;
			MenuPlayUI.singleplayerButton.sizeOffset_X = 200;
			MenuPlayUI.singleplayerButton.sizeOffset_Y = 50;
			MenuPlayUI.singleplayerButton.text = local.format("SingleplayerButtonText");
			MenuPlayUI.singleplayerButton.tooltip = local.format("SingleplayerButtonTooltip");
			SleekButton sleekButton3 = MenuPlayUI.singleplayerButton;
			if (MenuPlayUI.<>f__mg$cache2 == null)
			{
				MenuPlayUI.<>f__mg$cache2 = new ClickedButton(MenuPlayUI.onClickedSingleplayerButton);
			}
			sleekButton3.onClickedButton = MenuPlayUI.<>f__mg$cache2;
			MenuPlayUI.singleplayerButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuPlayUI.singleplayerButton.fontSize = 14;
			MenuPlayUI.container.add(MenuPlayUI.singleplayerButton);
			MenuPlayUI.matchmakingButton = new SleekButtonIcon((Texture2D)bundle.load("Matchmaking"));
			MenuPlayUI.matchmakingButton.positionOffset_X = -100;
			MenuPlayUI.matchmakingButton.positionOffset_Y = -85;
			MenuPlayUI.matchmakingButton.positionScale_X = 0.5f;
			MenuPlayUI.matchmakingButton.positionScale_Y = 0.5f;
			MenuPlayUI.matchmakingButton.sizeOffset_X = 200;
			MenuPlayUI.matchmakingButton.sizeOffset_Y = 50;
			MenuPlayUI.matchmakingButton.text = local.format("MatchmakingButtonText");
			MenuPlayUI.matchmakingButton.tooltip = local.format("MatchmakingButtonTooltip");
			SleekButton sleekButton4 = MenuPlayUI.matchmakingButton;
			if (MenuPlayUI.<>f__mg$cache3 == null)
			{
				MenuPlayUI.<>f__mg$cache3 = new ClickedButton(MenuPlayUI.onClickedMatchmakingButton);
			}
			sleekButton4.onClickedButton = MenuPlayUI.<>f__mg$cache3;
			MenuPlayUI.matchmakingButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuPlayUI.matchmakingButton.fontSize = 14;
			MenuPlayUI.container.add(MenuPlayUI.matchmakingButton);
			MenuPlayUI.lobbiesButton = new SleekButtonIcon((Texture2D)bundle.load("Lobbies"));
			MenuPlayUI.lobbiesButton.positionOffset_X = -100;
			MenuPlayUI.lobbiesButton.positionOffset_Y = -25;
			MenuPlayUI.lobbiesButton.positionScale_X = 0.5f;
			MenuPlayUI.lobbiesButton.positionScale_Y = 0.5f;
			MenuPlayUI.lobbiesButton.sizeOffset_X = 200;
			MenuPlayUI.lobbiesButton.sizeOffset_Y = 50;
			MenuPlayUI.lobbiesButton.text = local.format("LobbiesButtonText");
			MenuPlayUI.lobbiesButton.tooltip = local.format("LobbiesButtonTooltip");
			SleekButton sleekButton5 = MenuPlayUI.lobbiesButton;
			if (MenuPlayUI.<>f__mg$cache4 == null)
			{
				MenuPlayUI.<>f__mg$cache4 = new ClickedButton(MenuPlayUI.onClickedLobbiesButton);
			}
			sleekButton5.onClickedButton = MenuPlayUI.<>f__mg$cache4;
			MenuPlayUI.lobbiesButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuPlayUI.lobbiesButton.fontSize = 14;
			MenuPlayUI.container.add(MenuPlayUI.lobbiesButton);
			MenuPlayUI.tutorialButton = new SleekButtonIcon((Texture2D)bundle.load("Tutorial"));
			MenuPlayUI.tutorialButton.positionOffset_X = -100;
			MenuPlayUI.tutorialButton.positionOffset_Y = -205;
			MenuPlayUI.tutorialButton.positionScale_X = 0.5f;
			MenuPlayUI.tutorialButton.positionScale_Y = 0.5f;
			MenuPlayUI.tutorialButton.sizeOffset_X = 200;
			MenuPlayUI.tutorialButton.sizeOffset_Y = 50;
			MenuPlayUI.tutorialButton.text = local.format("TutorialButtonText");
			MenuPlayUI.tutorialButton.tooltip = local.format("TutorialButtonTooltip");
			SleekButton sleekButton6 = MenuPlayUI.tutorialButton;
			if (MenuPlayUI.<>f__mg$cache5 == null)
			{
				MenuPlayUI.<>f__mg$cache5 = new ClickedButton(MenuPlayUI.onClickedTutorialButton);
			}
			sleekButton6.onClickedButton = MenuPlayUI.<>f__mg$cache5;
			MenuPlayUI.tutorialButton.fontSize = 14;
			MenuPlayUI.tutorialButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuPlayUI.container.add(MenuPlayUI.tutorialButton);
			MenuPlayUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuPlayUI.backButton.positionOffset_X = -100;
			MenuPlayUI.backButton.positionOffset_Y = 155;
			MenuPlayUI.backButton.positionScale_X = 0.5f;
			MenuPlayUI.backButton.positionScale_Y = 0.5f;
			MenuPlayUI.backButton.sizeOffset_X = 200;
			MenuPlayUI.backButton.sizeOffset_Y = 50;
			MenuPlayUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuPlayUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton7 = MenuPlayUI.backButton;
			if (MenuPlayUI.<>f__mg$cache6 == null)
			{
				MenuPlayUI.<>f__mg$cache6 = new ClickedButton(MenuPlayUI.onClickedBackButton);
			}
			sleekButton7.onClickedButton = MenuPlayUI.<>f__mg$cache6;
			MenuPlayUI.backButton.fontSize = 14;
			MenuPlayUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuPlayUI.container.add(MenuPlayUI.backButton);
			bundle.unload();
			new MenuPlayConnectUI();
			new MenuPlayServersUI();
			new MenuPlayServerInfoUI();
			new MenuPlaySingleplayerUI();
			new MenuPlayMatchmakingUI();
			new MenuPlayLobbiesUI();
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x0017D919 File Offset: 0x0017BD19
		public static void open()
		{
			if (MenuPlayUI.active)
			{
				return;
			}
			MenuPlayUI.active = true;
			MenuPlayUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x0017D946 File Offset: 0x0017BD46
		public static void close()
		{
			if (!MenuPlayUI.active)
			{
				return;
			}
			MenuPlayUI.active = false;
			MenuPlayUI.container.lerpPositionScale(0f, -1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x0017D973 File Offset: 0x0017BD73
		private static void onClickedConnectButton(SleekButton button)
		{
			MenuPlayConnectUI.open();
			MenuPlayUI.close();
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x0017D97F File Offset: 0x0017BD7F
		private static void onClickedServersButton(SleekButton button)
		{
			MenuPlayServersUI.open();
			MenuPlayUI.close();
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x0017D98B File Offset: 0x0017BD8B
		private static void onClickedSingleplayerButton(SleekButton button)
		{
			MenuPlaySingleplayerUI.open();
			MenuPlayUI.close();
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x0017D997 File Offset: 0x0017BD97
		private static void onClickedMatchmakingButton(SleekButton button)
		{
			MenuPlayMatchmakingUI.open();
			MenuPlayUI.close();
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x0017D9A3 File Offset: 0x0017BDA3
		private static void onClickedLobbiesButton(SleekButton button)
		{
			MenuPlayLobbiesUI.open();
			MenuPlayUI.close();
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x0017D9B0 File Offset: 0x0017BDB0
		private static void onClickedTutorialButton(SleekButton button)
		{
			if (ReadWrite.folderExists("/Worlds/Singleplayer_" + Characters.selected + "/Level/Tutorial"))
			{
				ReadWrite.deleteFolder("/Worlds/Singleplayer_" + Characters.selected + "/Level/Tutorial");
			}
			if (ReadWrite.folderExists(string.Concat(new object[]
			{
				"/Worlds/Singleplayer_",
				Characters.selected,
				"/Players/",
				Provider.user,
				"_",
				Characters.selected,
				"/Tutorial"
			})))
			{
				ReadWrite.deleteFolder(string.Concat(new object[]
				{
					"/Worlds/Singleplayer_",
					Characters.selected,
					"/Players/",
					Provider.user,
					"_",
					Characters.selected,
					"/Tutorial"
				}));
			}
			Provider.map = "Tutorial";
			Provider.singleplayer(EGameMode.TUTORIAL, false);
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x0017DAC2 File Offset: 0x0017BEC2
		private static void onClickedBackButton(SleekButton button)
		{
			MenuDashboardUI.open();
			MenuTitleUI.open();
			MenuPlayUI.close();
		}

		// Token: 0x0400280D RID: 10253
		private static Sleek container;

		// Token: 0x0400280E RID: 10254
		public static bool active;

		// Token: 0x0400280F RID: 10255
		private static SleekButtonIcon connectButton;

		// Token: 0x04002810 RID: 10256
		private static SleekButtonIcon serversButton;

		// Token: 0x04002811 RID: 10257
		private static SleekButtonIcon singleplayerButton;

		// Token: 0x04002812 RID: 10258
		private static SleekButtonIcon matchmakingButton;

		// Token: 0x04002813 RID: 10259
		private static SleekButtonIcon lobbiesButton;

		// Token: 0x04002814 RID: 10260
		private static SleekButtonIcon tutorialButton;

		// Token: 0x04002815 RID: 10261
		private static SleekButtonIcon backButton;

		// Token: 0x04002816 RID: 10262
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x04002817 RID: 10263
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x04002818 RID: 10264
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x04002819 RID: 10265
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x0400281A RID: 10266
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;

		// Token: 0x0400281B RID: 10267
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;

		// Token: 0x0400281C RID: 10268
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache6;
	}
}
