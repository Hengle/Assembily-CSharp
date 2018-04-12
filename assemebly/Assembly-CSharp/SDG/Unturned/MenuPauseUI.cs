using System;
using System.Runtime.CompilerServices;
using Steamworks;
using UnityEngine;
using UnityEngine.Analytics;

namespace SDG.Unturned
{
	// Token: 0x0200076D RID: 1901
	public class MenuPauseUI
	{
		// Token: 0x0600366B RID: 13931 RVA: 0x00173964 File Offset: 0x00171D64
		public MenuPauseUI()
		{
			MenuPauseUI.localization = Localization.read("/Menu/MenuPause.dat");
			if (MenuPauseUI.icons != null)
			{
				MenuPauseUI.icons.unload();
				MenuPauseUI.icons = null;
			}
			MenuPauseUI.icons = Bundles.getBundle("/Bundles/Textures/Menu/Icons/MenuPause/MenuPause.unity3d");
			MenuPauseUI.container = new Sleek();
			MenuPauseUI.container.positionOffset_X = 10;
			MenuPauseUI.container.positionOffset_Y = 10;
			MenuPauseUI.container.positionScale_Y = -1f;
			MenuPauseUI.container.sizeOffset_X = -20;
			MenuPauseUI.container.sizeOffset_Y = -20;
			MenuPauseUI.container.sizeScale_X = 1f;
			MenuPauseUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuPauseUI.container);
			MenuPauseUI.active = false;
			MenuPauseUI.exitButton = new SleekButtonIcon((Texture2D)MenuPauseUI.icons.load("Exit"));
			MenuPauseUI.exitButton.positionOffset_X = -100;
			MenuPauseUI.exitButton.positionOffset_Y = -265;
			MenuPauseUI.exitButton.positionScale_X = 0.5f;
			MenuPauseUI.exitButton.positionScale_Y = 0.5f;
			MenuPauseUI.exitButton.sizeOffset_X = 200;
			MenuPauseUI.exitButton.sizeOffset_Y = 50;
			MenuPauseUI.exitButton.text = MenuPauseUI.localization.format("Exit_Button");
			MenuPauseUI.exitButton.tooltip = MenuPauseUI.localization.format("Exit_Button_Tooltip");
			SleekButton sleekButton = MenuPauseUI.exitButton;
			if (MenuPauseUI.<>f__mg$cache0 == null)
			{
				MenuPauseUI.<>f__mg$cache0 = new ClickedButton(MenuPauseUI.onClickedExitButton);
			}
			sleekButton.onClickedButton = MenuPauseUI.<>f__mg$cache0;
			MenuPauseUI.exitButton.fontSize = 14;
			MenuPauseUI.exitButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuPauseUI.container.add(MenuPauseUI.exitButton);
			MenuPauseUI.returnButton = new SleekButtonIcon((Texture2D)MenuPauseUI.icons.load("Return"));
			MenuPauseUI.returnButton.positionOffset_X = -100;
			MenuPauseUI.returnButton.positionOffset_Y = -205;
			MenuPauseUI.returnButton.positionScale_X = 0.5f;
			MenuPauseUI.returnButton.positionScale_Y = 0.5f;
			MenuPauseUI.returnButton.sizeOffset_X = 200;
			MenuPauseUI.returnButton.sizeOffset_Y = 50;
			MenuPauseUI.returnButton.text = MenuPauseUI.localization.format("Return_Button");
			MenuPauseUI.returnButton.tooltip = MenuPauseUI.localization.format("Return_Button_Tooltip");
			SleekButton sleekButton2 = MenuPauseUI.returnButton;
			if (MenuPauseUI.<>f__mg$cache1 == null)
			{
				MenuPauseUI.<>f__mg$cache1 = new ClickedButton(MenuPauseUI.onClickedReturnButton);
			}
			sleekButton2.onClickedButton = MenuPauseUI.<>f__mg$cache1;
			MenuPauseUI.returnButton.fontSize = 14;
			MenuPauseUI.returnButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuPauseUI.container.add(MenuPauseUI.returnButton);
			MenuPauseUI.reportButton = new SleekButtonIcon((Texture2D)MenuPauseUI.icons.load("Report"));
			MenuPauseUI.reportButton.positionOffset_X = -100;
			MenuPauseUI.reportButton.positionOffset_Y = -145;
			MenuPauseUI.reportButton.positionScale_X = 0.5f;
			MenuPauseUI.reportButton.positionScale_Y = 0.5f;
			MenuPauseUI.reportButton.sizeOffset_X = 200;
			MenuPauseUI.reportButton.sizeOffset_Y = 50;
			MenuPauseUI.reportButton.text = MenuPauseUI.localization.format("Report_Button");
			MenuPauseUI.reportButton.tooltip = MenuPauseUI.localization.format("Report_Button_Tooltip");
			SleekButton sleekButton3 = MenuPauseUI.reportButton;
			if (MenuPauseUI.<>f__mg$cache2 == null)
			{
				MenuPauseUI.<>f__mg$cache2 = new ClickedButton(MenuPauseUI.onClickedReportButton);
			}
			sleekButton3.onClickedButton = MenuPauseUI.<>f__mg$cache2;
			MenuPauseUI.reportButton.fontSize = 14;
			MenuPauseUI.reportButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuPauseUI.container.add(MenuPauseUI.reportButton);
			MenuPauseUI.twitterButton = new SleekButtonIcon((Texture2D)MenuPauseUI.icons.load("Twitter"));
			MenuPauseUI.twitterButton.positionOffset_X = -100;
			MenuPauseUI.twitterButton.positionOffset_Y = -85;
			MenuPauseUI.twitterButton.positionScale_X = 0.5f;
			MenuPauseUI.twitterButton.positionScale_Y = 0.5f;
			MenuPauseUI.twitterButton.sizeOffset_X = 200;
			MenuPauseUI.twitterButton.sizeOffset_Y = 50;
			MenuPauseUI.twitterButton.text = MenuPauseUI.localization.format("Twitter_Button");
			MenuPauseUI.twitterButton.tooltip = MenuPauseUI.localization.format("Twitter_Button_Tooltip");
			SleekButton sleekButton4 = MenuPauseUI.twitterButton;
			if (MenuPauseUI.<>f__mg$cache3 == null)
			{
				MenuPauseUI.<>f__mg$cache3 = new ClickedButton(MenuPauseUI.onClickedTwitterButton);
			}
			sleekButton4.onClickedButton = MenuPauseUI.<>f__mg$cache3;
			MenuPauseUI.twitterButton.fontSize = 14;
			MenuPauseUI.container.add(MenuPauseUI.twitterButton);
			MenuPauseUI.steamButton = new SleekButtonIcon((Texture2D)MenuPauseUI.icons.load("Steam"));
			MenuPauseUI.steamButton.positionOffset_X = -100;
			MenuPauseUI.steamButton.positionOffset_Y = -25;
			MenuPauseUI.steamButton.positionScale_X = 0.5f;
			MenuPauseUI.steamButton.positionScale_Y = 0.5f;
			MenuPauseUI.steamButton.sizeOffset_X = 200;
			MenuPauseUI.steamButton.sizeOffset_Y = 50;
			MenuPauseUI.steamButton.text = MenuPauseUI.localization.format("Steam_Button");
			MenuPauseUI.steamButton.tooltip = MenuPauseUI.localization.format("Steam_Button_Tooltip");
			SleekButton sleekButton5 = MenuPauseUI.steamButton;
			if (MenuPauseUI.<>f__mg$cache4 == null)
			{
				MenuPauseUI.<>f__mg$cache4 = new ClickedButton(MenuPauseUI.onClickedSteamButton);
			}
			sleekButton5.onClickedButton = MenuPauseUI.<>f__mg$cache4;
			MenuPauseUI.steamButton.fontSize = 14;
			MenuPauseUI.container.add(MenuPauseUI.steamButton);
			MenuPauseUI.forumButton = new SleekButtonIcon((Texture2D)MenuPauseUI.icons.load("Forum"));
			MenuPauseUI.forumButton.positionOffset_X = -100;
			MenuPauseUI.forumButton.positionOffset_Y = 35;
			MenuPauseUI.forumButton.positionScale_X = 0.5f;
			MenuPauseUI.forumButton.positionScale_Y = 0.5f;
			MenuPauseUI.forumButton.sizeOffset_X = 200;
			MenuPauseUI.forumButton.sizeOffset_Y = 50;
			MenuPauseUI.forumButton.text = MenuPauseUI.localization.format("Forum_Button");
			MenuPauseUI.forumButton.tooltip = MenuPauseUI.localization.format("Forum_Button_Tooltip");
			SleekButton sleekButton6 = MenuPauseUI.forumButton;
			if (MenuPauseUI.<>f__mg$cache5 == null)
			{
				MenuPauseUI.<>f__mg$cache5 = new ClickedButton(MenuPauseUI.onClickedForumButton);
			}
			sleekButton6.onClickedButton = MenuPauseUI.<>f__mg$cache5;
			MenuPauseUI.forumButton.fontSize = 14;
			MenuPauseUI.container.add(MenuPauseUI.forumButton);
			MenuPauseUI.blogButton = new SleekButtonIcon((Texture2D)MenuPauseUI.icons.load("Blog"));
			MenuPauseUI.blogButton.positionOffset_X = -100;
			MenuPauseUI.blogButton.positionOffset_Y = 95;
			MenuPauseUI.blogButton.positionScale_X = 0.5f;
			MenuPauseUI.blogButton.positionScale_Y = 0.5f;
			MenuPauseUI.blogButton.sizeOffset_X = 200;
			MenuPauseUI.blogButton.sizeOffset_Y = 50;
			MenuPauseUI.blogButton.text = MenuPauseUI.localization.format("Blog_Button");
			MenuPauseUI.blogButton.tooltip = MenuPauseUI.localization.format("Blog_Button_Tooltip");
			SleekButton sleekButton7 = MenuPauseUI.blogButton;
			if (MenuPauseUI.<>f__mg$cache6 == null)
			{
				MenuPauseUI.<>f__mg$cache6 = new ClickedButton(MenuPauseUI.onClickedBlogButton);
			}
			sleekButton7.onClickedButton = MenuPauseUI.<>f__mg$cache6;
			MenuPauseUI.blogButton.fontSize = 14;
			MenuPauseUI.container.add(MenuPauseUI.blogButton);
			MenuPauseUI.wikiButton = new SleekButtonIcon((Texture2D)MenuPauseUI.icons.load("Wiki"));
			MenuPauseUI.wikiButton.positionOffset_X = -100;
			MenuPauseUI.wikiButton.positionOffset_Y = 155;
			MenuPauseUI.wikiButton.positionScale_X = 0.5f;
			MenuPauseUI.wikiButton.positionScale_Y = 0.5f;
			MenuPauseUI.wikiButton.sizeOffset_X = 200;
			MenuPauseUI.wikiButton.sizeOffset_Y = 50;
			MenuPauseUI.wikiButton.text = MenuPauseUI.localization.format("Wiki_Button");
			MenuPauseUI.wikiButton.tooltip = MenuPauseUI.localization.format("Wiki_Button_Tooltip");
			SleekButton sleekButton8 = MenuPauseUI.wikiButton;
			if (MenuPauseUI.<>f__mg$cache7 == null)
			{
				MenuPauseUI.<>f__mg$cache7 = new ClickedButton(MenuPauseUI.onClickedWikiButton);
			}
			sleekButton8.onClickedButton = MenuPauseUI.<>f__mg$cache7;
			MenuPauseUI.wikiButton.fontSize = 14;
			MenuPauseUI.container.add(MenuPauseUI.wikiButton);
			MenuPauseUI.creditsButton = new SleekButtonIcon((Texture2D)MenuPauseUI.icons.load("Credits"));
			MenuPauseUI.creditsButton.positionOffset_X = -100;
			MenuPauseUI.creditsButton.positionOffset_Y = 215;
			MenuPauseUI.creditsButton.positionScale_X = 0.5f;
			MenuPauseUI.creditsButton.positionScale_Y = 0.5f;
			MenuPauseUI.creditsButton.sizeOffset_X = 200;
			MenuPauseUI.creditsButton.sizeOffset_Y = 50;
			MenuPauseUI.creditsButton.text = MenuPauseUI.localization.format("Credits_Button");
			MenuPauseUI.creditsButton.tooltip = MenuPauseUI.localization.format("Credits_Button_Tooltip");
			SleekButton sleekButton9 = MenuPauseUI.creditsButton;
			if (MenuPauseUI.<>f__mg$cache8 == null)
			{
				MenuPauseUI.<>f__mg$cache8 = new ClickedButton(MenuPauseUI.onClickedCreditsButton);
			}
			sleekButton9.onClickedButton = MenuPauseUI.<>f__mg$cache8;
			MenuPauseUI.creditsButton.fontSize = 14;
			MenuPauseUI.container.add(MenuPauseUI.creditsButton);
			MenuPauseUI.icons.unload();
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x0017426F File Offset: 0x0017266F
		public static void open()
		{
			if (MenuPauseUI.active)
			{
				return;
			}
			MenuPauseUI.active = true;
			MenuPauseUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x0017429C File Offset: 0x0017269C
		public static void close()
		{
			if (!MenuPauseUI.active)
			{
				return;
			}
			MenuPauseUI.active = false;
			MenuPauseUI.container.lerpPositionScale(0f, -1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x001742C9 File Offset: 0x001726C9
		private static void onClickedReturnButton(SleekButton button)
		{
			MenuPauseUI.close();
			MenuDashboardUI.open();
			MenuTitleUI.open();
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x001742DA File Offset: 0x001726DA
		private static void onClickedExitButton(SleekButton button)
		{
			Application.Quit();
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x001742E4 File Offset: 0x001726E4
		private static void onClickedReportButton(SleekButton button)
		{
			if (!Provider.provider.browserService.canOpenBrowser)
			{
				MenuUI.alert(MenuPauseUI.localization.format("Overlay"));
				return;
			}
			Provider.provider.browserService.open("http://steamcommunity.com/app/" + SteamUtils.GetAppID() + "/discussions/9/613936673439628788/");
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x00174344 File Offset: 0x00172744
		private static void onClickedTrelloButton(SleekButton button)
		{
			if (!Provider.provider.browserService.canOpenBrowser)
			{
				MenuUI.alert(MenuPauseUI.localization.format("Overlay"));
				return;
			}
			Provider.provider.browserService.open("https://trello.com/b/ezUtMJif");
			Analytics.CustomEvent("Link_Trello", null);
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x0017439C File Offset: 0x0017279C
		private static void onClickedTwitterButton(SleekButton button)
		{
			if (!Provider.provider.browserService.canOpenBrowser)
			{
				MenuUI.alert(MenuPauseUI.localization.format("Overlay"));
				return;
			}
			Provider.provider.browserService.open("https://twitter.com/SDGNelson");
			Analytics.CustomEvent("Link_Twitter", null);
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x001743F4 File Offset: 0x001727F4
		private static void onClickedSteamButton(SleekButton button)
		{
			if (!Provider.provider.browserService.canOpenBrowser)
			{
				MenuUI.alert(MenuPauseUI.localization.format("Overlay"));
				return;
			}
			Provider.provider.browserService.open("http://steamcommunity.com/app/304930/announcements/");
			Analytics.CustomEvent("Link_News", null);
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x0017444A File Offset: 0x0017284A
		private static void onClickedCreditsButton(SleekButton button)
		{
			MenuPauseUI.close();
			MenuCreditsUI.open();
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x00174458 File Offset: 0x00172858
		private static void onClickedForumButton(SleekButton button)
		{
			if (!Provider.provider.browserService.canOpenBrowser)
			{
				MenuUI.alert(MenuPauseUI.localization.format("Overlay"));
				return;
			}
			Provider.provider.browserService.open("https://forum.smartlydressedgames.com/");
			Analytics.CustomEvent("Link_Forum", null);
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x001744B0 File Offset: 0x001728B0
		private static void onClickedBlogButton(SleekButton button)
		{
			if (!Provider.provider.browserService.canOpenBrowser)
			{
				MenuUI.alert(MenuPauseUI.localization.format("Overlay"));
				return;
			}
			Provider.provider.browserService.open("https://blog.smartlydressedgames.com/");
			Analytics.CustomEvent("Link_Blog", null);
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x00174508 File Offset: 0x00172908
		private static void onClickedWikiButton(SleekButton button)
		{
			if (!Provider.provider.browserService.canOpenBrowser)
			{
				MenuUI.alert(MenuPauseUI.localization.format("Overlay"));
				return;
			}
			Provider.provider.browserService.open("https://unturned.wikia.com");
			Analytics.CustomEvent("Link_Wiki", null);
		}

		// Token: 0x040026FE RID: 9982
		public static Local localization;

		// Token: 0x040026FF RID: 9983
		public static Bundle icons;

		// Token: 0x04002700 RID: 9984
		private static Sleek container;

		// Token: 0x04002701 RID: 9985
		public static bool active;

		// Token: 0x04002702 RID: 9986
		private static SleekButtonIcon returnButton;

		// Token: 0x04002703 RID: 9987
		private static SleekButtonIcon exitButton;

		// Token: 0x04002704 RID: 9988
		private static SleekButtonIcon reportButton;

		// Token: 0x04002705 RID: 9989
		private static SleekButtonIcon trelloButton;

		// Token: 0x04002706 RID: 9990
		private static SleekButtonIcon twitterButton;

		// Token: 0x04002707 RID: 9991
		private static SleekButtonIcon steamButton;

		// Token: 0x04002708 RID: 9992
		private static SleekButtonIcon creditsButton;

		// Token: 0x04002709 RID: 9993
		private static SleekButtonIcon forumButton;

		// Token: 0x0400270A RID: 9994
		private static SleekButtonIcon blogButton;

		// Token: 0x0400270B RID: 9995
		private static SleekButtonIcon wikiButton;

		// Token: 0x0400270C RID: 9996
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x0400270D RID: 9997
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x0400270E RID: 9998
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x0400270F RID: 9999
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x04002710 RID: 10000
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;

		// Token: 0x04002711 RID: 10001
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;

		// Token: 0x04002712 RID: 10002
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache6;

		// Token: 0x04002713 RID: 10003
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache7;

		// Token: 0x04002714 RID: 10004
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache8;
	}
}
