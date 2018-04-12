using System;
using System.Runtime.CompilerServices;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000774 RID: 1908
	public class MenuPlayLobbiesUI
	{
		// Token: 0x060036A5 RID: 13989 RVA: 0x00176478 File Offset: 0x00174878
		public MenuPlayLobbiesUI()
		{
			MenuPlayLobbiesUI.localization = Localization.read("/Menu/Play/MenuPlayLobbies.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Play/MenuPlayLobbies/MenuPlayLobbies.unity3d");
			MenuPlayLobbiesUI.container = new Sleek();
			MenuPlayLobbiesUI.container.positionOffset_X = 10;
			MenuPlayLobbiesUI.container.positionOffset_Y = 10;
			MenuPlayLobbiesUI.container.positionScale_Y = 1f;
			MenuPlayLobbiesUI.container.sizeOffset_X = -20;
			MenuPlayLobbiesUI.container.sizeOffset_Y = -20;
			MenuPlayLobbiesUI.container.sizeScale_X = 1f;
			MenuPlayLobbiesUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuPlayLobbiesUI.container);
			MenuPlayLobbiesUI.active = false;
			bundle.unload();
			MenuPlayLobbiesUI.membersLabel = new SleekLabel();
			MenuPlayLobbiesUI.membersLabel.positionOffset_X = -200;
			MenuPlayLobbiesUI.membersLabel.positionOffset_Y = 100;
			MenuPlayLobbiesUI.membersLabel.positionScale_X = 0.5f;
			MenuPlayLobbiesUI.membersLabel.sizeOffset_X = 400;
			MenuPlayLobbiesUI.membersLabel.sizeOffset_Y = 50;
			MenuPlayLobbiesUI.membersLabel.text = MenuPlayLobbiesUI.localization.format("Members");
			MenuPlayLobbiesUI.membersLabel.fontSize = 14;
			MenuPlayLobbiesUI.container.add(MenuPlayLobbiesUI.membersLabel);
			MenuPlayLobbiesUI.membersBox = new SleekScrollBox();
			MenuPlayLobbiesUI.membersBox.positionOffset_X = -200;
			MenuPlayLobbiesUI.membersBox.positionOffset_Y = 150;
			MenuPlayLobbiesUI.membersBox.positionScale_X = 0.5f;
			MenuPlayLobbiesUI.membersBox.sizeOffset_X = 430;
			MenuPlayLobbiesUI.membersBox.sizeOffset_Y = -300;
			MenuPlayLobbiesUI.membersBox.sizeScale_Y = 1f;
			MenuPlayLobbiesUI.container.add(MenuPlayLobbiesUI.membersBox);
			MenuPlayLobbiesUI.inviteButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Invite"));
			MenuPlayLobbiesUI.inviteButton.positionOffset_X = -200;
			MenuPlayLobbiesUI.inviteButton.positionOffset_Y = -150;
			MenuPlayLobbiesUI.inviteButton.positionScale_X = 0.5f;
			MenuPlayLobbiesUI.inviteButton.positionScale_Y = 1f;
			MenuPlayLobbiesUI.inviteButton.sizeOffset_X = 400;
			MenuPlayLobbiesUI.inviteButton.sizeOffset_Y = 50;
			MenuPlayLobbiesUI.inviteButton.text = MenuPlayLobbiesUI.localization.format("Invite_Button");
			MenuPlayLobbiesUI.inviteButton.tooltip = MenuPlayLobbiesUI.localization.format("Invite_Button_Tooltip");
			SleekButton sleekButton = MenuPlayLobbiesUI.inviteButton;
			if (MenuPlayLobbiesUI.<>f__mg$cache0 == null)
			{
				MenuPlayLobbiesUI.<>f__mg$cache0 = new ClickedButton(MenuPlayLobbiesUI.onClickedInviteButton);
			}
			sleekButton.onClickedButton = MenuPlayLobbiesUI.<>f__mg$cache0;
			MenuPlayLobbiesUI.inviteButton.fontSize = 14;
			MenuPlayLobbiesUI.inviteButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuPlayLobbiesUI.container.add(MenuPlayLobbiesUI.inviteButton);
			MenuPlayLobbiesUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuPlayLobbiesUI.backButton.positionOffset_Y = -50;
			MenuPlayLobbiesUI.backButton.positionScale_Y = 1f;
			MenuPlayLobbiesUI.backButton.sizeOffset_X = 200;
			MenuPlayLobbiesUI.backButton.sizeOffset_Y = 50;
			MenuPlayLobbiesUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuPlayLobbiesUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton2 = MenuPlayLobbiesUI.backButton;
			if (MenuPlayLobbiesUI.<>f__mg$cache1 == null)
			{
				MenuPlayLobbiesUI.<>f__mg$cache1 = new ClickedButton(MenuPlayLobbiesUI.onClickedBackButton);
			}
			sleekButton2.onClickedButton = MenuPlayLobbiesUI.<>f__mg$cache1;
			MenuPlayLobbiesUI.backButton.fontSize = 14;
			MenuPlayLobbiesUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuPlayLobbiesUI.container.add(MenuPlayLobbiesUI.backButton);
			if (MenuPlayLobbiesUI.<>f__mg$cache2 == null)
			{
				MenuPlayLobbiesUI.<>f__mg$cache2 = new LobbiesRefreshedHandler(MenuPlayLobbiesUI.handleLobbiesRefreshed);
			}
			Lobbies.lobbiesRefreshed = MenuPlayLobbiesUI.<>f__mg$cache2;
			if (MenuPlayLobbiesUI.<>f__mg$cache3 == null)
			{
				MenuPlayLobbiesUI.<>f__mg$cache3 = new LobbiesEnteredHandler(MenuPlayLobbiesUI.handleLobbiesEntered);
			}
			Lobbies.lobbiesEntered = MenuPlayLobbiesUI.<>f__mg$cache3;
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x00176834 File Offset: 0x00174C34
		public static void open()
		{
			if (MenuPlayLobbiesUI.active)
			{
				return;
			}
			MenuPlayLobbiesUI.active = true;
			if (Lobbies.inLobby)
			{
				MenuPlayLobbiesUI.refresh();
			}
			else
			{
				Lobbies.createLobby();
			}
			MenuPlayLobbiesUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x00176885 File Offset: 0x00174C85
		public static void close()
		{
			if (!MenuPlayLobbiesUI.active)
			{
				return;
			}
			MenuPlayLobbiesUI.active = false;
			MenuSettings.save();
			MenuPlayLobbiesUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x001768B8 File Offset: 0x00174CB8
		private static void refresh()
		{
			MenuPlayLobbiesUI.membersBox.remove();
			int lobbyMemberCount = Lobbies.getLobbyMemberCount();
			for (int i = 0; i < lobbyMemberCount; i++)
			{
				CSteamID lobbyMemberByIndex = Lobbies.getLobbyMemberByIndex(i);
				MenuPlayLobbiesUI.SleekLobbyPlayerButton sleekLobbyPlayerButton = new MenuPlayLobbiesUI.SleekLobbyPlayerButton(lobbyMemberByIndex);
				sleekLobbyPlayerButton.positionOffset_Y = i * 50;
				sleekLobbyPlayerButton.sizeOffset_X = -30;
				sleekLobbyPlayerButton.sizeOffset_Y = 50;
				sleekLobbyPlayerButton.sizeScale_X = 1f;
				MenuPlayLobbiesUI.membersBox.add(sleekLobbyPlayerButton);
			}
			MenuPlayLobbiesUI.membersBox.area = new Rect(0f, 0f, 5f, (float)(lobbyMemberCount * 50));
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x00176948 File Offset: 0x00174D48
		private static void handleLobbiesRefreshed()
		{
			if (!MenuPlayLobbiesUI.active)
			{
				return;
			}
			MenuPlayLobbiesUI.refresh();
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x0017695A File Offset: 0x00174D5A
		private static void handleLobbiesEntered()
		{
			if (MenuPlayLobbiesUI.active)
			{
				return;
			}
			MenuUI.closeAll();
			MenuPlayLobbiesUI.open();
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x00176971 File Offset: 0x00174D71
		private static void onClickedInviteButton(SleekButton button)
		{
			if (!Lobbies.canOpenInvitations)
			{
				MenuUI.alert(MenuPlayLobbiesUI.localization.format("Overlay"));
				return;
			}
			Lobbies.openInvitations();
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x00176997 File Offset: 0x00174D97
		private static void onClickedBackButton(SleekButton button)
		{
			MenuPlayUI.open();
			MenuPlayLobbiesUI.close();
		}

		// Token: 0x04002746 RID: 10054
		public static Local localization;

		// Token: 0x04002747 RID: 10055
		private static Sleek container;

		// Token: 0x04002748 RID: 10056
		public static bool active;

		// Token: 0x04002749 RID: 10057
		private static SleekLabel membersLabel;

		// Token: 0x0400274A RID: 10058
		private static SleekScrollBox membersBox;

		// Token: 0x0400274B RID: 10059
		private static SleekButtonIcon inviteButton;

		// Token: 0x0400274C RID: 10060
		private static SleekButtonIcon backButton;

		// Token: 0x0400274D RID: 10061
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x0400274E RID: 10062
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x0400274F RID: 10063
		[CompilerGenerated]
		private static LobbiesRefreshedHandler <>f__mg$cache2;

		// Token: 0x04002750 RID: 10064
		[CompilerGenerated]
		private static LobbiesEnteredHandler <>f__mg$cache3;

		// Token: 0x02000775 RID: 1909
		public class SleekLobbyPlayerButton : Sleek
		{
			// Token: 0x060036AD RID: 13997 RVA: 0x001769A4 File Offset: 0x00174DA4
			public SleekLobbyPlayerButton(CSteamID newSteamID)
			{
				this.steamID = newSteamID;
				base.init();
				this.button = new SleekButton();
				this.button.sizeScale_X = 1f;
				this.button.sizeScale_Y = 1f;
				this.button.onClickedButton = new ClickedButton(this.onClickedPlayerButton);
				base.add(this.button);
				this.avatarImage = new SleekImageTexture();
				this.avatarImage.positionOffset_X = 9;
				this.avatarImage.positionOffset_Y = 9;
				this.avatarImage.sizeOffset_X = 32;
				this.avatarImage.sizeOffset_Y = 32;
				this.avatarImage.texture = Provider.provider.communityService.getIcon(this.steamID);
				this.avatarImage.shouldDestroyTexture = true;
				this.button.add(this.avatarImage);
				this.nameLabel = new SleekLabel();
				this.nameLabel.positionOffset_X = 40;
				this.nameLabel.sizeOffset_X = -40;
				this.nameLabel.sizeScale_X = 1f;
				this.nameLabel.sizeScale_Y = 1f;
				this.nameLabel.text = SteamFriends.GetFriendPersonaName(this.steamID);
				this.nameLabel.fontSize = 14;
				this.button.add(this.nameLabel);
			}

			// Token: 0x060036AE RID: 13998 RVA: 0x00176B07 File Offset: 0x00174F07
			private void onClickedPlayerButton(SleekButton button)
			{
				Provider.provider.browserService.open("http://steamcommunity.com/profiles/" + this.steamID);
			}

			// Token: 0x04002751 RID: 10065
			private CSteamID steamID;

			// Token: 0x04002752 RID: 10066
			private SleekButton button;

			// Token: 0x04002753 RID: 10067
			private SleekImageTexture avatarImage;

			// Token: 0x04002754 RID: 10068
			private SleekLabel nameLabel;
		}
	}
}
