using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using SDG.Provider;
using Steamworks;
using UnityEngine;
using UnityEngine.Analytics;

namespace SDG.Unturned
{
	// Token: 0x02000777 RID: 1911
	public class MenuPlayServerInfoUI
	{
		// Token: 0x060036BF RID: 14015 RVA: 0x0017776C File Offset: 0x00175B6C
		public MenuPlayServerInfoUI()
		{
			MenuPlayServerInfoUI.localization = Localization.read("/Menu/Play/MenuPlayServerInfo.dat");
			MenuPlayServerInfoUI.container = new Sleek();
			MenuPlayServerInfoUI.container.positionOffset_X = 10;
			MenuPlayServerInfoUI.container.positionOffset_Y = 10;
			MenuPlayServerInfoUI.container.positionScale_Y = 1f;
			MenuPlayServerInfoUI.container.sizeOffset_X = -20;
			MenuPlayServerInfoUI.container.sizeOffset_Y = -20;
			MenuPlayServerInfoUI.container.sizeScale_X = 1f;
			MenuPlayServerInfoUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuPlayServerInfoUI.container);
			MenuPlayServerInfoUI.active = false;
			MenuPlayServerInfoUI.infoContainer = new Sleek();
			MenuPlayServerInfoUI.infoContainer.positionOffset_Y = 94;
			MenuPlayServerInfoUI.infoContainer.sizeOffset_Y = -154;
			MenuPlayServerInfoUI.infoContainer.sizeScale_X = 1f;
			MenuPlayServerInfoUI.infoContainer.sizeScale_Y = 1f;
			MenuPlayServerInfoUI.container.add(MenuPlayServerInfoUI.infoContainer);
			MenuPlayServerInfoUI.buttonsContainer = new Sleek();
			MenuPlayServerInfoUI.buttonsContainer.positionOffset_Y = -50;
			MenuPlayServerInfoUI.buttonsContainer.positionScale_Y = 1f;
			MenuPlayServerInfoUI.buttonsContainer.sizeOffset_Y = 50;
			MenuPlayServerInfoUI.buttonsContainer.sizeScale_X = 1f;
			MenuPlayServerInfoUI.container.add(MenuPlayServerInfoUI.buttonsContainer);
			MenuPlayServerInfoUI.titleBox = new SleekBox();
			MenuPlayServerInfoUI.titleBox.sizeOffset_Y = 84;
			MenuPlayServerInfoUI.titleBox.sizeScale_X = 1f;
			MenuPlayServerInfoUI.container.add(MenuPlayServerInfoUI.titleBox);
			MenuPlayServerInfoUI.titleIconImage = new SleekImageTexture();
			MenuPlayServerInfoUI.titleIconImage.positionOffset_X = 10;
			MenuPlayServerInfoUI.titleIconImage.positionOffset_Y = 10;
			MenuPlayServerInfoUI.titleIconImage.sizeOffset_X = 64;
			MenuPlayServerInfoUI.titleIconImage.sizeOffset_Y = 64;
			MenuPlayServerInfoUI.titleBox.add(MenuPlayServerInfoUI.titleIconImage);
			MenuPlayServerInfoUI.titleNameLabel = new SleekLabel();
			MenuPlayServerInfoUI.titleNameLabel.positionOffset_X = 79;
			MenuPlayServerInfoUI.titleNameLabel.positionOffset_Y = 5;
			MenuPlayServerInfoUI.titleNameLabel.sizeOffset_X = -84;
			MenuPlayServerInfoUI.titleNameLabel.sizeOffset_Y = 40;
			MenuPlayServerInfoUI.titleNameLabel.sizeScale_X = 1f;
			MenuPlayServerInfoUI.titleNameLabel.fontSize = 24;
			MenuPlayServerInfoUI.titleNameLabel.foregroundTint = ESleekTint.NONE;
			MenuPlayServerInfoUI.titleBox.add(MenuPlayServerInfoUI.titleNameLabel);
			MenuPlayServerInfoUI.titleDescriptionLabel = new SleekLabel();
			MenuPlayServerInfoUI.titleDescriptionLabel.positionOffset_X = 79;
			MenuPlayServerInfoUI.titleDescriptionLabel.positionOffset_Y = 45;
			MenuPlayServerInfoUI.titleDescriptionLabel.sizeOffset_X = -84;
			MenuPlayServerInfoUI.titleDescriptionLabel.sizeOffset_Y = 34;
			MenuPlayServerInfoUI.titleDescriptionLabel.sizeScale_X = 1f;
			MenuPlayServerInfoUI.titleDescriptionLabel.isRich = true;
			MenuPlayServerInfoUI.titleBox.add(MenuPlayServerInfoUI.titleDescriptionLabel);
			MenuPlayServerInfoUI.playersContainer = new Sleek();
			MenuPlayServerInfoUI.playersContainer.sizeOffset_X = 280;
			MenuPlayServerInfoUI.playersContainer.sizeScale_Y = 1f;
			MenuPlayServerInfoUI.infoContainer.add(MenuPlayServerInfoUI.playersContainer);
			MenuPlayServerInfoUI.playerCountBox = new SleekBox();
			MenuPlayServerInfoUI.playerCountBox.sizeScale_X = 1f;
			MenuPlayServerInfoUI.playerCountBox.sizeOffset_Y = 30;
			MenuPlayServerInfoUI.playersContainer.add(MenuPlayServerInfoUI.playerCountBox);
			MenuPlayServerInfoUI.playersScrollBox = new SleekScrollBox();
			MenuPlayServerInfoUI.playersScrollBox.positionOffset_Y = 40;
			MenuPlayServerInfoUI.playersScrollBox.sizeScale_X = 1f;
			MenuPlayServerInfoUI.playersScrollBox.sizeOffset_Y = -40;
			MenuPlayServerInfoUI.playersScrollBox.sizeScale_Y = 1f;
			MenuPlayServerInfoUI.playersContainer.add(MenuPlayServerInfoUI.playersScrollBox);
			MenuPlayServerInfoUI.detailsContainer = new Sleek();
			MenuPlayServerInfoUI.detailsContainer.positionOffset_X = 290;
			MenuPlayServerInfoUI.detailsContainer.sizeOffset_X = -655;
			MenuPlayServerInfoUI.detailsContainer.sizeScale_X = 1f;
			MenuPlayServerInfoUI.detailsContainer.sizeScale_Y = 1f;
			MenuPlayServerInfoUI.infoContainer.add(MenuPlayServerInfoUI.detailsContainer);
			MenuPlayServerInfoUI.detailsBox = new SleekBox();
			MenuPlayServerInfoUI.detailsBox.sizeScale_X = 1f;
			MenuPlayServerInfoUI.detailsBox.sizeOffset_Y = 30;
			MenuPlayServerInfoUI.detailsBox.text = MenuPlayServerInfoUI.localization.format("Details");
			MenuPlayServerInfoUI.detailsContainer.add(MenuPlayServerInfoUI.detailsBox);
			MenuPlayServerInfoUI.detailsScrollBox = new SleekScrollBox();
			MenuPlayServerInfoUI.detailsScrollBox.positionOffset_Y = 40;
			MenuPlayServerInfoUI.detailsScrollBox.sizeScale_X = 1f;
			MenuPlayServerInfoUI.detailsScrollBox.sizeOffset_Y = -40;
			MenuPlayServerInfoUI.detailsScrollBox.sizeScale_Y = 1f;
			MenuPlayServerInfoUI.detailsContainer.add(MenuPlayServerInfoUI.detailsScrollBox);
			MenuPlayServerInfoUI.serverTitle = new SleekBox();
			MenuPlayServerInfoUI.serverTitle.sizeOffset_X = -30;
			MenuPlayServerInfoUI.serverTitle.sizeOffset_Y = 30;
			MenuPlayServerInfoUI.serverTitle.sizeScale_X = 1f;
			MenuPlayServerInfoUI.serverTitle.text = MenuPlayServerInfoUI.localization.format("Server");
			MenuPlayServerInfoUI.detailsScrollBox.add(MenuPlayServerInfoUI.serverTitle);
			MenuPlayServerInfoUI.serverBox = new SleekBox();
			MenuPlayServerInfoUI.serverBox.positionOffset_Y = 40;
			MenuPlayServerInfoUI.serverBox.sizeOffset_X = -30;
			MenuPlayServerInfoUI.serverBox.sizeScale_X = 1f;
			MenuPlayServerInfoUI.serverBox.sizeOffset_Y = 150;
			MenuPlayServerInfoUI.detailsScrollBox.add(MenuPlayServerInfoUI.serverBox);
			MenuPlayServerInfoUI.serverWorkshopLabel = new SleekLabel();
			MenuPlayServerInfoUI.serverWorkshopLabel.positionOffset_X = 5;
			MenuPlayServerInfoUI.serverWorkshopLabel.positionOffset_Y = 5;
			MenuPlayServerInfoUI.serverWorkshopLabel.sizeOffset_Y = 20;
			MenuPlayServerInfoUI.serverWorkshopLabel.sizeScale_X = 1f;
			MenuPlayServerInfoUI.serverWorkshopLabel.fontAlignment = TextAnchor.MiddleLeft;
			MenuPlayServerInfoUI.serverBox.add(MenuPlayServerInfoUI.serverWorkshopLabel);
			MenuPlayServerInfoUI.serverCombatLabel = new SleekLabel();
			MenuPlayServerInfoUI.serverCombatLabel.positionOffset_X = 5;
			MenuPlayServerInfoUI.serverCombatLabel.positionOffset_Y = 25;
			MenuPlayServerInfoUI.serverCombatLabel.sizeOffset_Y = 20;
			MenuPlayServerInfoUI.serverCombatLabel.sizeScale_X = 1f;
			MenuPlayServerInfoUI.serverCombatLabel.fontAlignment = TextAnchor.MiddleLeft;
			MenuPlayServerInfoUI.serverBox.add(MenuPlayServerInfoUI.serverCombatLabel);
			MenuPlayServerInfoUI.serverPerspectiveLabel = new SleekLabel();
			MenuPlayServerInfoUI.serverPerspectiveLabel.positionOffset_X = 5;
			MenuPlayServerInfoUI.serverPerspectiveLabel.positionOffset_Y = 45;
			MenuPlayServerInfoUI.serverPerspectiveLabel.sizeOffset_Y = 20;
			MenuPlayServerInfoUI.serverPerspectiveLabel.sizeScale_X = 1f;
			MenuPlayServerInfoUI.serverPerspectiveLabel.fontAlignment = TextAnchor.MiddleLeft;
			MenuPlayServerInfoUI.serverBox.add(MenuPlayServerInfoUI.serverPerspectiveLabel);
			MenuPlayServerInfoUI.serverSecurityLabel = new SleekLabel();
			MenuPlayServerInfoUI.serverSecurityLabel.positionOffset_X = 5;
			MenuPlayServerInfoUI.serverSecurityLabel.positionOffset_Y = 65;
			MenuPlayServerInfoUI.serverSecurityLabel.sizeOffset_Y = 20;
			MenuPlayServerInfoUI.serverSecurityLabel.sizeScale_X = 1f;
			MenuPlayServerInfoUI.serverSecurityLabel.fontAlignment = TextAnchor.MiddleLeft;
			MenuPlayServerInfoUI.serverBox.add(MenuPlayServerInfoUI.serverSecurityLabel);
			MenuPlayServerInfoUI.serverModeLabel = new SleekLabel();
			MenuPlayServerInfoUI.serverModeLabel.positionOffset_X = 5;
			MenuPlayServerInfoUI.serverModeLabel.positionOffset_Y = 85;
			MenuPlayServerInfoUI.serverModeLabel.sizeOffset_Y = 20;
			MenuPlayServerInfoUI.serverModeLabel.sizeScale_X = 1f;
			MenuPlayServerInfoUI.serverModeLabel.fontAlignment = TextAnchor.MiddleLeft;
			MenuPlayServerInfoUI.serverBox.add(MenuPlayServerInfoUI.serverModeLabel);
			MenuPlayServerInfoUI.serverCheatsLabel = new SleekLabel();
			MenuPlayServerInfoUI.serverCheatsLabel.positionOffset_X = 5;
			MenuPlayServerInfoUI.serverCheatsLabel.positionOffset_Y = 105;
			MenuPlayServerInfoUI.serverCheatsLabel.sizeOffset_Y = 20;
			MenuPlayServerInfoUI.serverCheatsLabel.sizeScale_X = 1f;
			MenuPlayServerInfoUI.serverCheatsLabel.fontAlignment = TextAnchor.MiddleLeft;
			MenuPlayServerInfoUI.serverBox.add(MenuPlayServerInfoUI.serverCheatsLabel);
			MenuPlayServerInfoUI.serverGameModeLabel = new SleekLabel();
			MenuPlayServerInfoUI.serverGameModeLabel.positionOffset_X = 5;
			MenuPlayServerInfoUI.serverGameModeLabel.positionOffset_Y = 125;
			MenuPlayServerInfoUI.serverGameModeLabel.sizeOffset_Y = 20;
			MenuPlayServerInfoUI.serverGameModeLabel.sizeScale_X = 1f;
			MenuPlayServerInfoUI.serverGameModeLabel.fontAlignment = TextAnchor.MiddleLeft;
			MenuPlayServerInfoUI.serverBox.add(MenuPlayServerInfoUI.serverGameModeLabel);
			MenuPlayServerInfoUI.ugcTitle = new SleekBox();
			MenuPlayServerInfoUI.ugcTitle.sizeOffset_X = -30;
			MenuPlayServerInfoUI.ugcTitle.sizeOffset_Y = 30;
			MenuPlayServerInfoUI.ugcTitle.sizeScale_X = 1f;
			MenuPlayServerInfoUI.ugcTitle.text = MenuPlayServerInfoUI.localization.format("UGC");
			MenuPlayServerInfoUI.detailsScrollBox.add(MenuPlayServerInfoUI.ugcTitle);
			MenuPlayServerInfoUI.ugcTitle.isVisible = false;
			MenuPlayServerInfoUI.ugcBox = new SleekBox();
			MenuPlayServerInfoUI.ugcBox.sizeOffset_X = -30;
			MenuPlayServerInfoUI.ugcBox.sizeScale_X = 1f;
			MenuPlayServerInfoUI.detailsScrollBox.add(MenuPlayServerInfoUI.ugcBox);
			MenuPlayServerInfoUI.ugcBox.isVisible = false;
			MenuPlayServerInfoUI.configTitle = new SleekBox();
			MenuPlayServerInfoUI.configTitle.sizeOffset_X = -30;
			MenuPlayServerInfoUI.configTitle.sizeOffset_Y = 30;
			MenuPlayServerInfoUI.configTitle.sizeScale_X = 1f;
			MenuPlayServerInfoUI.configTitle.text = MenuPlayServerInfoUI.localization.format("Config");
			MenuPlayServerInfoUI.detailsScrollBox.add(MenuPlayServerInfoUI.configTitle);
			MenuPlayServerInfoUI.configTitle.isVisible = false;
			MenuPlayServerInfoUI.configBox = new SleekBox();
			MenuPlayServerInfoUI.configBox.sizeOffset_X = -30;
			MenuPlayServerInfoUI.configBox.sizeScale_X = 1f;
			MenuPlayServerInfoUI.detailsScrollBox.add(MenuPlayServerInfoUI.configBox);
			MenuPlayServerInfoUI.configBox.isVisible = false;
			MenuPlayServerInfoUI.rocketTitle = new SleekBox();
			MenuPlayServerInfoUI.rocketTitle.sizeOffset_X = -30;
			MenuPlayServerInfoUI.rocketTitle.sizeOffset_Y = 30;
			MenuPlayServerInfoUI.rocketTitle.sizeScale_X = 1f;
			MenuPlayServerInfoUI.rocketTitle.text = MenuPlayServerInfoUI.localization.format("Rocket");
			MenuPlayServerInfoUI.detailsScrollBox.add(MenuPlayServerInfoUI.rocketTitle);
			MenuPlayServerInfoUI.rocketTitle.isVisible = false;
			MenuPlayServerInfoUI.rocketBox = new SleekBox();
			MenuPlayServerInfoUI.rocketBox.sizeOffset_X = -30;
			MenuPlayServerInfoUI.rocketBox.sizeScale_X = 1f;
			MenuPlayServerInfoUI.detailsScrollBox.add(MenuPlayServerInfoUI.rocketBox);
			MenuPlayServerInfoUI.rocketBox.isVisible = false;
			MenuPlayServerInfoUI.mapContainer = new Sleek();
			MenuPlayServerInfoUI.mapContainer.positionOffset_X = -355;
			MenuPlayServerInfoUI.mapContainer.positionScale_X = 1f;
			MenuPlayServerInfoUI.mapContainer.sizeOffset_X = 355;
			MenuPlayServerInfoUI.mapContainer.sizeScale_Y = 1f;
			MenuPlayServerInfoUI.infoContainer.add(MenuPlayServerInfoUI.mapContainer);
			MenuPlayServerInfoUI.mapNameBox = new SleekBox();
			MenuPlayServerInfoUI.mapNameBox.sizeOffset_X = 355;
			MenuPlayServerInfoUI.mapNameBox.sizeOffset_Y = 30;
			MenuPlayServerInfoUI.mapContainer.add(MenuPlayServerInfoUI.mapNameBox);
			MenuPlayServerInfoUI.mapPreviewBox = new SleekBox();
			MenuPlayServerInfoUI.mapPreviewBox.positionOffset_Y = 40;
			MenuPlayServerInfoUI.mapPreviewBox.sizeOffset_X = 355;
			MenuPlayServerInfoUI.mapPreviewBox.sizeOffset_Y = 180;
			MenuPlayServerInfoUI.mapContainer.add(MenuPlayServerInfoUI.mapPreviewBox);
			MenuPlayServerInfoUI.mapPreviewImage = new SleekImageTexture();
			MenuPlayServerInfoUI.mapPreviewImage.positionOffset_X = 10;
			MenuPlayServerInfoUI.mapPreviewImage.positionOffset_Y = 10;
			MenuPlayServerInfoUI.mapPreviewImage.sizeOffset_X = -20;
			MenuPlayServerInfoUI.mapPreviewImage.sizeOffset_Y = -20;
			MenuPlayServerInfoUI.mapPreviewImage.sizeScale_X = 1f;
			MenuPlayServerInfoUI.mapPreviewImage.sizeScale_Y = 1f;
			MenuPlayServerInfoUI.mapPreviewBox.add(MenuPlayServerInfoUI.mapPreviewImage);
			MenuPlayServerInfoUI.mapDescriptionBox = new SleekBox();
			MenuPlayServerInfoUI.mapDescriptionBox.positionOffset_Y = 230;
			MenuPlayServerInfoUI.mapDescriptionBox.sizeOffset_X = 355;
			MenuPlayServerInfoUI.mapDescriptionBox.sizeOffset_Y = 140;
			MenuPlayServerInfoUI.mapDescriptionBox.fontAlignment = TextAnchor.UpperCenter;
			MenuPlayServerInfoUI.mapContainer.add(MenuPlayServerInfoUI.mapDescriptionBox);
			MenuPlayServerInfoUI.serverDescriptionBox = new SleekBox();
			MenuPlayServerInfoUI.serverDescriptionBox.positionOffset_Y = 380;
			MenuPlayServerInfoUI.serverDescriptionBox.sizeOffset_X = 355;
			MenuPlayServerInfoUI.serverDescriptionBox.sizeOffset_Y = -380;
			MenuPlayServerInfoUI.serverDescriptionBox.sizeScale_Y = 1f;
			MenuPlayServerInfoUI.serverDescriptionBox.fontAlignment = TextAnchor.UpperCenter;
			MenuPlayServerInfoUI.serverDescriptionBox.isRich = true;
			MenuPlayServerInfoUI.mapContainer.add(MenuPlayServerInfoUI.serverDescriptionBox);
			MenuPlayServerInfoUI.joinButton = new SleekButton();
			MenuPlayServerInfoUI.joinButton.sizeOffset_X = -5;
			MenuPlayServerInfoUI.joinButton.sizeScale_X = 0.25f;
			MenuPlayServerInfoUI.joinButton.sizeScale_Y = 1f;
			MenuPlayServerInfoUI.joinButton.text = MenuPlayServerInfoUI.localization.format("Join_Button");
			MenuPlayServerInfoUI.joinButton.tooltip = MenuPlayServerInfoUI.localization.format("Join_Button_Tooltip");
			SleekButton sleekButton = MenuPlayServerInfoUI.joinButton;
			if (MenuPlayServerInfoUI.<>f__mg$cache0 == null)
			{
				MenuPlayServerInfoUI.<>f__mg$cache0 = new ClickedButton(MenuPlayServerInfoUI.onClickedJoinButton);
			}
			sleekButton.onClickedButton = MenuPlayServerInfoUI.<>f__mg$cache0;
			MenuPlayServerInfoUI.joinButton.fontSize = 14;
			MenuPlayServerInfoUI.buttonsContainer.add(MenuPlayServerInfoUI.joinButton);
			MenuPlayServerInfoUI.favoriteButton = new SleekButton();
			MenuPlayServerInfoUI.favoriteButton.positionOffset_X = 5;
			MenuPlayServerInfoUI.favoriteButton.positionScale_X = 0.25f;
			MenuPlayServerInfoUI.favoriteButton.sizeOffset_X = -10;
			MenuPlayServerInfoUI.favoriteButton.sizeScale_X = 0.25f;
			MenuPlayServerInfoUI.favoriteButton.sizeScale_Y = 1f;
			MenuPlayServerInfoUI.favoriteButton.tooltip = MenuPlayServerInfoUI.localization.format("Favorite_Button_Tooltip");
			SleekButton sleekButton2 = MenuPlayServerInfoUI.favoriteButton;
			if (MenuPlayServerInfoUI.<>f__mg$cache1 == null)
			{
				MenuPlayServerInfoUI.<>f__mg$cache1 = new ClickedButton(MenuPlayServerInfoUI.onClickedFavoriteButton);
			}
			sleekButton2.onClickedButton = MenuPlayServerInfoUI.<>f__mg$cache1;
			MenuPlayServerInfoUI.favoriteButton.fontSize = 14;
			MenuPlayServerInfoUI.buttonsContainer.add(MenuPlayServerInfoUI.favoriteButton);
			MenuPlayServerInfoUI.refreshButton = new SleekButton();
			MenuPlayServerInfoUI.refreshButton.positionOffset_X = 5;
			MenuPlayServerInfoUI.refreshButton.positionScale_X = 0.5f;
			MenuPlayServerInfoUI.refreshButton.sizeOffset_X = -10;
			MenuPlayServerInfoUI.refreshButton.sizeScale_X = 0.25f;
			MenuPlayServerInfoUI.refreshButton.sizeScale_Y = 1f;
			MenuPlayServerInfoUI.refreshButton.text = MenuPlayServerInfoUI.localization.format("Refresh_Button");
			MenuPlayServerInfoUI.refreshButton.tooltip = MenuPlayServerInfoUI.localization.format("Refresh_Button_Tooltip");
			SleekButton sleekButton3 = MenuPlayServerInfoUI.refreshButton;
			if (MenuPlayServerInfoUI.<>f__mg$cache2 == null)
			{
				MenuPlayServerInfoUI.<>f__mg$cache2 = new ClickedButton(MenuPlayServerInfoUI.onClickedRefreshButton);
			}
			sleekButton3.onClickedButton = MenuPlayServerInfoUI.<>f__mg$cache2;
			MenuPlayServerInfoUI.refreshButton.fontSize = 14;
			MenuPlayServerInfoUI.buttonsContainer.add(MenuPlayServerInfoUI.refreshButton);
			MenuPlayServerInfoUI.cancelButton = new SleekButton();
			MenuPlayServerInfoUI.cancelButton.positionOffset_X = 5;
			MenuPlayServerInfoUI.cancelButton.positionScale_X = 0.75f;
			MenuPlayServerInfoUI.cancelButton.sizeOffset_X = -5;
			MenuPlayServerInfoUI.cancelButton.sizeScale_X = 0.25f;
			MenuPlayServerInfoUI.cancelButton.sizeScale_Y = 1f;
			MenuPlayServerInfoUI.cancelButton.text = MenuPlayServerInfoUI.localization.format("Cancel_Button");
			MenuPlayServerInfoUI.cancelButton.tooltip = MenuPlayServerInfoUI.localization.format("Cancel_Button_Tooltip");
			SleekButton sleekButton4 = MenuPlayServerInfoUI.cancelButton;
			if (MenuPlayServerInfoUI.<>f__mg$cache3 == null)
			{
				MenuPlayServerInfoUI.<>f__mg$cache3 = new ClickedButton(MenuPlayServerInfoUI.onClickedCancelButton);
			}
			sleekButton4.onClickedButton = MenuPlayServerInfoUI.<>f__mg$cache3;
			MenuPlayServerInfoUI.cancelButton.fontSize = 14;
			MenuPlayServerInfoUI.buttonsContainer.add(MenuPlayServerInfoUI.cancelButton);
			TempSteamworksMatchmaking matchmakingService = Provider.provider.matchmakingService;
			if (MenuPlayServerInfoUI.<>f__mg$cache4 == null)
			{
				MenuPlayServerInfoUI.<>f__mg$cache4 = new TempSteamworksMatchmaking.MasterServerQueryRefreshed(MenuPlayServerInfoUI.onMasterServerQueryRefreshed);
			}
			matchmakingService.onMasterServerQueryRefreshed = MenuPlayServerInfoUI.<>f__mg$cache4;
			if (MenuPlayServerInfoUI.<>f__mg$cache5 == null)
			{
				MenuPlayServerInfoUI.<>f__mg$cache5 = new Provider.IconQueryRefreshed(MenuPlayServerInfoUI.onIconQueryRefreshed);
			}
			Provider.onIconQueryRefreshed = MenuPlayServerInfoUI.<>f__mg$cache5;
			TempSteamworksMatchmaking matchmakingService2 = Provider.provider.matchmakingService;
			if (MenuPlayServerInfoUI.<>f__mg$cache6 == null)
			{
				MenuPlayServerInfoUI.<>f__mg$cache6 = new TempSteamworksMatchmaking.PlayersQueryRefreshed(MenuPlayServerInfoUI.onPlayersQueryRefreshed);
			}
			matchmakingService2.onPlayersQueryRefreshed = MenuPlayServerInfoUI.<>f__mg$cache6;
			TempSteamworksMatchmaking matchmakingService3 = Provider.provider.matchmakingService;
			if (MenuPlayServerInfoUI.<>f__mg$cache7 == null)
			{
				MenuPlayServerInfoUI.<>f__mg$cache7 = new TempSteamworksMatchmaking.RulesQueryRefreshed(MenuPlayServerInfoUI.onRulesQueryRefreshed);
			}
			matchmakingService3.onRulesQueryRefreshed = MenuPlayServerInfoUI.<>f__mg$cache7;
			if (MenuPlayServerInfoUI.ugcQueryCompleted == null)
			{
				if (MenuPlayServerInfoUI.<>f__mg$cache8 == null)
				{
					MenuPlayServerInfoUI.<>f__mg$cache8 = new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(MenuPlayServerInfoUI.onUGCQueryCompleted);
				}
				MenuPlayServerInfoUI.ugcQueryCompleted = CallResult<SteamUGCQueryCompleted_t>.Create(MenuPlayServerInfoUI.<>f__mg$cache8);
			}
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x0017862C File Offset: 0x00176A2C
		private static void onUGCQueryCompleted(SteamUGCQueryCompleted_t callback, bool io)
		{
			if (callback.m_eResult != EResult.k_EResultOK || io)
			{
				return;
			}
			int num = 0;
			while ((long)num < (long)((ulong)callback.m_unNumResultsReturned))
			{
				SteamUGCDetails_t steamUGCDetails_t;
				if (SteamUGC.GetQueryUGCResult(MenuPlayServerInfoUI.detailsHandle, (uint)num, out steamUGCDetails_t))
				{
					SleekLabel sleekLabel = new SleekLabel();
					sleekLabel.positionOffset_X = 5;
					sleekLabel.positionOffset_Y = 5 + num * 20;
					sleekLabel.sizeOffset_Y = 20;
					sleekLabel.sizeScale_X = 1f;
					sleekLabel.fontAlignment = TextAnchor.MiddleLeft;
					sleekLabel.text = steamUGCDetails_t.m_rgchTitle;
					MenuPlayServerInfoUI.ugcBox.add(sleekLabel);
				}
				num++;
			}
			MenuPlayServerInfoUI.ugcBox.sizeOffset_Y = (int)(callback.m_unNumResultsReturned * 20u + 10u);
			MenuPlayServerInfoUI.ugcTitle.isVisible = true;
			MenuPlayServerInfoUI.ugcBox.isVisible = true;
			MenuPlayServerInfoUI.updateDetails();
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060036C1 RID: 14017 RVA: 0x001786F6 File Offset: 0x00176AF6
		// (set) Token: 0x060036C2 RID: 14018 RVA: 0x001786FD File Offset: 0x00176AFD
		public static MenuPlayServerInfoUI.EServerInfoOpenContext openContext { get; private set; }

		// Token: 0x060036C3 RID: 14019 RVA: 0x00178708 File Offset: 0x00176B08
		public static void open(SteamServerInfo newServerInfo, string newServerPassword, MenuPlayServerInfoUI.EServerInfoOpenContext newOpenContext)
		{
			if (MenuPlayServerInfoUI.active)
			{
				return;
			}
			MenuPlayServerInfoUI.active = true;
			MenuPlayServerInfoUI.openContext = newOpenContext;
			MenuPlayServerInfoUI.serverInfo = newServerInfo;
			MenuPlayServerInfoUI.serverPassword = newServerPassword;
			MenuPlayServerInfoUI.reset();
			MenuPlayServerInfoUI.serverFavorited = Provider.checkFavorite(MenuPlayServerInfoUI.serverInfo.ip, MenuPlayServerInfoUI.serverInfo.port);
			MenuPlayServerInfoUI.updateFavorite();
			MenuPlayServerInfoUI.updatePlayers();
			Provider.provider.matchmakingService.refreshPlayers(MenuPlayServerInfoUI.serverInfo.ip, MenuPlayServerInfoUI.serverInfo.port);
			Provider.provider.matchmakingService.refreshPlayers(MenuPlayServerInfoUI.serverInfo.ip, MenuPlayServerInfoUI.serverInfo.port);
			MenuPlayServerInfoUI.updateRules();
			Provider.provider.matchmakingService.refreshRules(MenuPlayServerInfoUI.serverInfo.ip, MenuPlayServerInfoUI.serverInfo.port);
			MenuPlayServerInfoUI.updateServerInfo();
			MenuPlayServerInfoUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x001787F2 File Offset: 0x00176BF2
		public static void close()
		{
			if (!MenuPlayServerInfoUI.active)
			{
				return;
			}
			MenuPlayServerInfoUI.active = false;
			MenuPlayServerInfoUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x00178820 File Offset: 0x00176C20
		private static void onClickedJoinButton(SleekButton button)
		{
			Dictionary<string, object> eventData = new Dictionary<string, object>
			{
				{
					"Camera",
					MenuPlayServerInfoUI.serverInfo.cameraMode
				},
				{
					"Cheats",
					MenuPlayServerInfoUI.serverInfo.hasCheats
				},
				{
					"Password",
					MenuPlayServerInfoUI.serverInfo.isPassworded
				},
				{
					"Pro",
					MenuPlayServerInfoUI.serverInfo.isPro
				},
				{
					"PvP",
					MenuPlayServerInfoUI.serverInfo.isPvP
				},
				{
					"Secure",
					MenuPlayServerInfoUI.serverInfo.IsVACSecure && MenuPlayServerInfoUI.serverInfo.IsBattlEyeSecure
				},
				{
					"Workshop",
					MenuPlayServerInfoUI.serverInfo.isWorkshop
				},
				{
					"Mode",
					MenuPlayServerInfoUI.serverInfo.mode
				},
				{
					"Ping",
					MenuPlayServerInfoUI.serverInfo.ping
				},
				{
					"Rocket",
					MenuPlayServerInfoUI.rocketTitle.isVisible
				}
			};
			Analytics.CustomEvent("Join", eventData);
			Provider.connect(MenuPlayServerInfoUI.serverInfo, MenuPlayServerInfoUI.serverPassword);
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x00178966 File Offset: 0x00176D66
		private static void onClickedFavoriteButton(SleekButton button)
		{
			MenuPlayServerInfoUI.serverFavorited = !MenuPlayServerInfoUI.serverFavorited;
			Provider.updateFavorite(MenuPlayServerInfoUI.serverInfo.ip, MenuPlayServerInfoUI.serverInfo.port, MenuPlayServerInfoUI.serverFavorited);
			MenuPlayServerInfoUI.updateFavorite();
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x00178998 File Offset: 0x00176D98
		private static void onClickedRefreshButton(SleekButton button)
		{
			MenuPlayServerInfoUI.updatePlayers();
			Provider.provider.matchmakingService.refreshPlayers(MenuPlayServerInfoUI.serverInfo.ip, MenuPlayServerInfoUI.serverInfo.port);
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x001789C4 File Offset: 0x00176DC4
		private static void onClickedCancelButton(SleekButton button)
		{
			MenuPlayServerInfoUI.EServerInfoOpenContext openContext = MenuPlayServerInfoUI.openContext;
			if (openContext != MenuPlayServerInfoUI.EServerInfoOpenContext.CONNECT)
			{
				if (openContext != MenuPlayServerInfoUI.EServerInfoOpenContext.SERVERS)
				{
					if (openContext == MenuPlayServerInfoUI.EServerInfoOpenContext.MATCHMAKING)
					{
						MenuPlayMatchmakingUI.open();
					}
				}
				else
				{
					MenuPlayServersUI.open();
				}
			}
			else
			{
				MenuPlayConnectUI.open();
			}
			MenuPlayServerInfoUI.close();
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x00178A13 File Offset: 0x00176E13
		private static void onMasterServerQueryRefreshed(SteamServerInfo server)
		{
			MenuPlayServerInfoUI.serverInfo = server;
			MenuPlayServerInfoUI.updateServerInfo();
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x00178A20 File Offset: 0x00176E20
		private static void reset()
		{
			MenuPlayServerInfoUI.titleDescriptionLabel.text = string.Empty;
			MenuPlayServerInfoUI.titleIconImage.texture = null;
			MenuPlayServerInfoUI.serverDescriptionBox.text = string.Empty;
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x00178A4C File Offset: 0x00176E4C
		private static void updateServerInfo()
		{
			MenuPlayServerInfoUI.titleNameLabel.foregroundColor = ((!MenuPlayServerInfoUI.serverInfo.isPro) ? Color.white : Palette.PRO);
			MenuPlayServerInfoUI.titleNameLabel.text = MenuPlayServerInfoUI.serverInfo.name;
			MenuPlayServerInfoUI.serverWorkshopLabel.text = MenuPlayServerInfoUI.localization.format("Workshop", new object[]
			{
				MenuPlayServerInfoUI.localization.format((!MenuPlayServerInfoUI.serverInfo.isWorkshop) ? "No" : "Yes")
			});
			MenuPlayServerInfoUI.serverCombatLabel.text = MenuPlayServerInfoUI.localization.format("Combat", new object[]
			{
				MenuPlayServerInfoUI.localization.format((!MenuPlayServerInfoUI.serverInfo.isPvP) ? "PvE" : "PvP")
			});
			string text;
			switch (MenuPlayServerInfoUI.serverInfo.cameraMode)
			{
			case ECameraMode.FIRST:
				text = MenuPlayServerInfoUI.localization.format("First");
				break;
			case ECameraMode.THIRD:
				text = MenuPlayServerInfoUI.localization.format("Third");
				break;
			case ECameraMode.BOTH:
				text = MenuPlayServerInfoUI.localization.format("Both");
				break;
			case ECameraMode.VEHICLE:
				text = MenuPlayServerInfoUI.localization.format("Vehicle");
				break;
			default:
				text = string.Empty;
				break;
			}
			MenuPlayServerInfoUI.serverPerspectiveLabel.text = MenuPlayServerInfoUI.localization.format("Perspective", new object[]
			{
				text
			});
			string text2;
			if (MenuPlayServerInfoUI.serverInfo.IsVACSecure)
			{
				text2 = MenuPlayServerInfoUI.localization.format("VAC_Secure");
			}
			else
			{
				text2 = MenuPlayServerInfoUI.localization.format("VAC_Insecure");
			}
			if (MenuPlayServerInfoUI.serverInfo.IsBattlEyeSecure)
			{
				text2 = text2 + " + " + MenuPlayServerInfoUI.localization.format("BattlEye_Secure");
			}
			else
			{
				text2 = text2 + " + " + MenuPlayServerInfoUI.localization.format("BattlEye_Insecure");
			}
			MenuPlayServerInfoUI.serverSecurityLabel.text = MenuPlayServerInfoUI.localization.format("Security", new object[]
			{
				text2
			});
			string text3;
			switch (MenuPlayServerInfoUI.serverInfo.mode)
			{
			case EGameMode.EASY:
				text3 = MenuPlayServerInfoUI.localization.format("Easy");
				break;
			case EGameMode.NORMAL:
				text3 = MenuPlayServerInfoUI.localization.format("Normal");
				break;
			case EGameMode.HARD:
				text3 = MenuPlayServerInfoUI.localization.format("Hard");
				break;
			default:
				text3 = string.Empty;
				break;
			}
			MenuPlayServerInfoUI.serverModeLabel.text = MenuPlayServerInfoUI.localization.format("Mode", new object[]
			{
				text3
			});
			MenuPlayServerInfoUI.serverCheatsLabel.text = MenuPlayServerInfoUI.localization.format("Cheats", new object[]
			{
				MenuPlayServerInfoUI.localization.format((!MenuPlayServerInfoUI.serverInfo.hasCheats) ? "No" : "Yes")
			});
			MenuPlayServerInfoUI.serverGameModeLabel.text = MenuPlayServerInfoUI.localization.format("GameMode", new object[]
			{
				MenuPlayServerInfoUI.serverInfo.gameMode
			});
			MenuPlayServerInfoUI.updateDetails();
			LevelInfo level = Level.getLevel(MenuPlayServerInfoUI.serverInfo.map);
			if (level == null)
			{
				return;
			}
			Local local = Localization.tryRead(level.path, false);
			if (local != null)
			{
				MenuPlayServerInfoUI.mapDescriptionBox.text = local.format("Description");
			}
			if (local != null && local.has("Name"))
			{
				MenuPlayServerInfoUI.mapNameBox.text = MenuPlayServerInfoUI.localization.format("Map", new object[]
				{
					local.format("Name")
				});
			}
			else
			{
				MenuPlayServerInfoUI.mapNameBox.text = MenuPlayServerInfoUI.localization.format("Map", new object[]
				{
					MenuPlayServerInfoUI.serverInfo.map
				});
			}
			if (ReadWrite.fileExists(level.path + "/Level.png", false, false))
			{
				byte[] data = ReadWrite.readBytes(level.path + "/Level.png", false, false);
				Texture2D texture2D = new Texture2D(320, 180, TextureFormat.ARGB32, false, true);
				texture2D.name = "Preview";
				texture2D.LoadImage(data);
				MenuPlayServerInfoUI.mapPreviewImage.texture = texture2D;
			}
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x00178EA8 File Offset: 0x001772A8
		private static void updateFavorite()
		{
			if (MenuPlayServerInfoUI.serverFavorited)
			{
				MenuPlayServerInfoUI.favoriteButton.text = MenuPlayServerInfoUI.localization.format("Favorite_Off_Button");
			}
			else
			{
				MenuPlayServerInfoUI.favoriteButton.text = MenuPlayServerInfoUI.localization.format("Favorite_On_Button");
			}
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x00178EF8 File Offset: 0x001772F8
		private static void updatePlayers()
		{
			MenuPlayServerInfoUI.playersScrollBox.remove();
			MenuPlayServerInfoUI.playersOffset = 0;
			MenuPlayServerInfoUI.playersScrollBox.area = new Rect(0f, 0f, 5f, 0f);
			MenuPlayServerInfoUI.playerCountBox.text = MenuPlayServerInfoUI.localization.format("Players", new object[]
			{
				0,
				MenuPlayServerInfoUI.serverInfo.maxPlayers
			});
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x00178F72 File Offset: 0x00177372
		private static void onIconQueryRefreshed(Texture2D icon)
		{
			MenuPlayServerInfoUI.titleIconImage.texture = icon;
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x00178F80 File Offset: 0x00177380
		private static void onPlayersQueryRefreshed(string name, int score, float time)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)time);
			string text = string.Empty;
			if (timeSpan.Days > 0)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					" ",
					timeSpan.Days,
					"d"
				});
			}
			if (timeSpan.Hours > 0)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					" ",
					timeSpan.Hours,
					"h"
				});
			}
			if (timeSpan.Minutes > 0)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					" ",
					timeSpan.Minutes,
					"m"
				});
			}
			if (timeSpan.Seconds > 0)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					" ",
					timeSpan.Seconds,
					"s"
				});
			}
			SleekBox sleekBox = new SleekBox();
			sleekBox.positionOffset_Y = MenuPlayServerInfoUI.playersOffset;
			sleekBox.sizeOffset_X = -30;
			sleekBox.sizeOffset_Y = 30;
			sleekBox.sizeScale_X = 1f;
			MenuPlayServerInfoUI.playersScrollBox.add(sleekBox);
			sleekBox.add(new SleekLabel
			{
				positionOffset_X = 5,
				sizeOffset_X = -10,
				sizeScale_X = 1f,
				sizeScale_Y = 1f,
				fontAlignment = TextAnchor.MiddleLeft,
				text = name
			});
			sleekBox.add(new SleekLabel
			{
				positionOffset_X = -5,
				sizeOffset_X = -10,
				sizeScale_X = 1f,
				sizeScale_Y = 1f,
				fontAlignment = TextAnchor.MiddleRight,
				text = text
			});
			MenuPlayServerInfoUI.playersOffset += 40;
			MenuPlayServerInfoUI.playersScrollBox.area = new Rect(0f, 0f, 5f, (float)(MenuPlayServerInfoUI.playersOffset - 10));
			MenuPlayServerInfoUI.playerCountBox.text = MenuPlayServerInfoUI.localization.format("Players", new object[]
			{
				MenuPlayServerInfoUI.playersScrollBox.children.Count,
				MenuPlayServerInfoUI.serverInfo.maxPlayers
			});
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x001791D8 File Offset: 0x001775D8
		private static void updateRules()
		{
			MenuPlayServerInfoUI.ugcTitle.isVisible = false;
			MenuPlayServerInfoUI.ugcBox.remove();
			MenuPlayServerInfoUI.ugcBox.isVisible = false;
			MenuPlayServerInfoUI.configTitle.isVisible = false;
			MenuPlayServerInfoUI.configBox.remove();
			MenuPlayServerInfoUI.configBox.isVisible = false;
			MenuPlayServerInfoUI.rocketTitle.isVisible = false;
			MenuPlayServerInfoUI.rocketBox.remove();
			MenuPlayServerInfoUI.rocketBox.isVisible = false;
			MenuPlayServerInfoUI.updateDetails();
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x0017924C File Offset: 0x0017764C
		private static void onRulesQueryRefreshed(Dictionary<string, string> rulesMap)
		{
			if (rulesMap == null)
			{
				return;
			}
			string text;
			if (rulesMap.TryGetValue("Browser_Icon", out text) && !string.IsNullOrEmpty(text))
			{
				Provider.refreshIcon(text);
			}
			string text2;
			if (rulesMap.TryGetValue("Browser_Desc_Hint", out text2) && !string.IsNullOrEmpty(text2))
			{
				if (OptionsSettings.filter)
				{
					text2 = ChatManager.filter(text2);
				}
				MenuPlayServerInfoUI.titleDescriptionLabel.text = text2;
			}
			string s;
			int num;
			if (rulesMap.TryGetValue("Browser_Desc_Full_Count", out s) && int.TryParse(s, out num) && num > 0)
			{
				string text3 = string.Empty;
				for (int i = 0; i < num; i++)
				{
					string str;
					if (rulesMap.TryGetValue("Browser_Desc_Full_Line_" + i, out str))
					{
						text3 += str;
					}
				}
				if (OptionsSettings.filter)
				{
					text3 = ChatManager.filter(text3);
				}
				text3 = text3.Replace("<br>", "\n");
				MenuPlayServerInfoUI.serverDescriptionBox.text = text3;
			}
			string text4;
			if (rulesMap.TryGetValue("rocketplugins", out text4) && !string.IsNullOrEmpty(text4))
			{
				string[] array = text4.Split(new char[]
				{
					','
				});
				MenuPlayServerInfoUI.rocketBox.sizeOffset_Y = array.Length * 20 + 10;
				for (int j = 0; j < array.Length; j++)
				{
					SleekLabel sleekLabel = new SleekLabel();
					sleekLabel.positionOffset_X = 5;
					sleekLabel.positionOffset_Y = 5 + j * 20;
					sleekLabel.sizeOffset_Y = 20;
					sleekLabel.sizeScale_X = 1f;
					sleekLabel.fontAlignment = TextAnchor.MiddleLeft;
					sleekLabel.text = array[j];
					MenuPlayServerInfoUI.rocketBox.add(sleekLabel);
				}
				MenuPlayServerInfoUI.rocketTitle.isVisible = true;
				MenuPlayServerInfoUI.rocketBox.isVisible = true;
			}
			string s2;
			int num2;
			if (rulesMap.TryGetValue("Browser_Workshop_Count", out s2) && int.TryParse(s2, out num2) && num2 > 0)
			{
				string text5 = string.Empty;
				for (int k = 0; k < num2; k++)
				{
					string str2;
					if (rulesMap.TryGetValue("Browser_Workshop_Line_" + k, out str2))
					{
						text5 += str2;
					}
				}
				string[] array2 = text5.Split(new char[]
				{
					','
				});
				PublishedFileId_t[] array3 = new PublishedFileId_t[array2.Length];
				for (int l = 0; l < array2.Length; l++)
				{
					ulong value;
					if (ulong.TryParse(array2[l], out value))
					{
						array3[l] = new PublishedFileId_t(value);
					}
				}
				MenuPlayServerInfoUI.detailsHandle = SteamUGC.CreateQueryUGCDetailsRequest(array3, (uint)array3.Length);
				SteamUGC.SetAllowCachedResponse(MenuPlayServerInfoUI.detailsHandle, 60u);
				SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(MenuPlayServerInfoUI.detailsHandle);
				MenuPlayServerInfoUI.ugcQueryCompleted.Set(hAPICall, null);
			}
			string s3;
			int num3;
			if (rulesMap.TryGetValue("Browser_Config_Count", out s3) && int.TryParse(s3, out num3) && num3 > 0)
			{
				int num4 = 0;
				string text6 = string.Empty;
				for (int m = 0; m < num3; m++)
				{
					string str3;
					if (rulesMap.TryGetValue("Browser_Config_Line_" + m, out str3))
					{
						text6 += str3;
					}
				}
				string[] array4 = text6.Split(new char[]
				{
					','
				});
				int num5 = 0;
				ModeConfigData modeConfigData = new ModeConfigData(MenuPlayServerInfoUI.serverInfo.mode);
				Type type = modeConfigData.GetType();
				foreach (FieldInfo fieldInfo in type.GetFields())
				{
					object value2 = fieldInfo.GetValue(modeConfigData);
					Type type2 = value2.GetType();
					foreach (FieldInfo fieldInfo2 in type2.GetFields())
					{
						object value3 = fieldInfo2.GetValue(value2);
						if (num5 < array4.Length)
						{
							string text7 = string.Empty;
							if (value3 is bool)
							{
								if ((bool)value3)
								{
									if (array4[num5] == "F")
									{
										text7 = MenuPlayServerInfoUI.localization.format("No");
									}
								}
								else if (array4[num5] == "T")
								{
									text7 = MenuPlayServerInfoUI.localization.format("Yes");
								}
							}
							else if (array4[num5] != value3.ToString())
							{
								text7 = array4[num5];
							}
							if (!string.IsNullOrEmpty(text7))
							{
								SleekLabel sleekLabel2 = new SleekLabel();
								sleekLabel2.positionOffset_X = 5;
								sleekLabel2.positionOffset_Y = 5 + num4;
								sleekLabel2.sizeOffset_Y = 20;
								sleekLabel2.sizeScale_X = 1f;
								sleekLabel2.fontAlignment = TextAnchor.MiddleLeft;
								sleekLabel2.text = MenuPlayServerInfoUI.localization.format("Rule", new object[]
								{
									MenuPlayConfigUI.localization.format(fieldInfo2.Name),
									text7
								});
								MenuPlayServerInfoUI.configBox.add(sleekLabel2);
								num4 += 20;
							}
							num5++;
						}
					}
				}
				MenuPlayServerInfoUI.configBox.sizeOffset_Y = num4 + 10;
				if (num4 > 0)
				{
					MenuPlayServerInfoUI.configTitle.isVisible = true;
					MenuPlayServerInfoUI.configBox.isVisible = true;
				}
			}
			MenuPlayServerInfoUI.updateDetails();
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x0017979C File Offset: 0x00177B9C
		private static void updateDetails()
		{
			int num = 200;
			if (MenuPlayServerInfoUI.ugcTitle.isVisible)
			{
				MenuPlayServerInfoUI.ugcTitle.positionOffset_Y = num;
				num += MenuPlayServerInfoUI.ugcTitle.sizeOffset_Y + 10;
			}
			if (MenuPlayServerInfoUI.ugcBox.isVisible)
			{
				MenuPlayServerInfoUI.ugcBox.positionOffset_Y = num;
				num += MenuPlayServerInfoUI.ugcBox.sizeOffset_Y + 10;
			}
			if (MenuPlayServerInfoUI.configTitle.isVisible)
			{
				MenuPlayServerInfoUI.configTitle.positionOffset_Y = num;
				num += MenuPlayServerInfoUI.configTitle.sizeOffset_Y + 10;
			}
			if (MenuPlayServerInfoUI.configBox.isVisible)
			{
				MenuPlayServerInfoUI.configBox.positionOffset_Y = num;
				num += MenuPlayServerInfoUI.configBox.sizeOffset_Y + 10;
			}
			if (MenuPlayServerInfoUI.rocketTitle.isVisible)
			{
				MenuPlayServerInfoUI.rocketTitle.positionOffset_Y = num;
				num += MenuPlayServerInfoUI.rocketTitle.sizeOffset_Y + 10;
			}
			if (MenuPlayServerInfoUI.rocketBox.isVisible)
			{
				MenuPlayServerInfoUI.rocketBox.positionOffset_Y = num;
				num += MenuPlayServerInfoUI.rocketBox.sizeOffset_Y + 10;
			}
			MenuPlayServerInfoUI.detailsScrollBox.area = new Rect(0f, 0f, 5f, (float)(num - 10));
		}

		// Token: 0x0400276B RID: 10091
		private static Local localization;

		// Token: 0x0400276C RID: 10092
		private static Sleek container;

		// Token: 0x0400276D RID: 10093
		public static bool active;

		// Token: 0x0400276E RID: 10094
		private static Sleek infoContainer;

		// Token: 0x0400276F RID: 10095
		private static Sleek playersContainer;

		// Token: 0x04002770 RID: 10096
		private static Sleek detailsContainer;

		// Token: 0x04002771 RID: 10097
		private static Sleek mapContainer;

		// Token: 0x04002772 RID: 10098
		private static Sleek buttonsContainer;

		// Token: 0x04002773 RID: 10099
		private static SleekBox titleBox;

		// Token: 0x04002774 RID: 10100
		private static SleekImageTexture titleIconImage;

		// Token: 0x04002775 RID: 10101
		private static SleekLabel titleNameLabel;

		// Token: 0x04002776 RID: 10102
		private static SleekLabel titleDescriptionLabel;

		// Token: 0x04002777 RID: 10103
		private static SleekBox playerCountBox;

		// Token: 0x04002778 RID: 10104
		private static SleekScrollBox playersScrollBox;

		// Token: 0x04002779 RID: 10105
		private static SleekBox detailsBox;

		// Token: 0x0400277A RID: 10106
		private static SleekScrollBox detailsScrollBox;

		// Token: 0x0400277B RID: 10107
		private static SleekBox serverTitle;

		// Token: 0x0400277C RID: 10108
		private static SleekBox serverBox;

		// Token: 0x0400277D RID: 10109
		private static SleekLabel serverWorkshopLabel;

		// Token: 0x0400277E RID: 10110
		private static SleekLabel serverCombatLabel;

		// Token: 0x0400277F RID: 10111
		private static SleekLabel serverPerspectiveLabel;

		// Token: 0x04002780 RID: 10112
		private static SleekLabel serverSecurityLabel;

		// Token: 0x04002781 RID: 10113
		private static SleekLabel serverModeLabel;

		// Token: 0x04002782 RID: 10114
		private static SleekLabel serverCheatsLabel;

		// Token: 0x04002783 RID: 10115
		private static SleekLabel serverGameModeLabel;

		// Token: 0x04002784 RID: 10116
		private static SleekBox ugcTitle;

		// Token: 0x04002785 RID: 10117
		private static SleekBox ugcBox;

		// Token: 0x04002786 RID: 10118
		private static SleekBox configTitle;

		// Token: 0x04002787 RID: 10119
		private static SleekBox configBox;

		// Token: 0x04002788 RID: 10120
		private static SleekBox rocketTitle;

		// Token: 0x04002789 RID: 10121
		private static SleekBox rocketBox;

		// Token: 0x0400278A RID: 10122
		private static SleekBox mapNameBox;

		// Token: 0x0400278B RID: 10123
		private static SleekBox mapPreviewBox;

		// Token: 0x0400278C RID: 10124
		private static SleekImageTexture mapPreviewImage;

		// Token: 0x0400278D RID: 10125
		private static SleekBox mapDescriptionBox;

		// Token: 0x0400278E RID: 10126
		private static SleekBox serverDescriptionBox;

		// Token: 0x0400278F RID: 10127
		private static SleekButton joinButton;

		// Token: 0x04002790 RID: 10128
		private static SleekButton favoriteButton;

		// Token: 0x04002791 RID: 10129
		private static SleekButton refreshButton;

		// Token: 0x04002792 RID: 10130
		private static SleekButton cancelButton;

		// Token: 0x04002793 RID: 10131
		private static SteamServerInfo serverInfo;

		// Token: 0x04002794 RID: 10132
		private static string serverPassword;

		// Token: 0x04002795 RID: 10133
		private static bool serverFavorited;

		// Token: 0x04002796 RID: 10134
		private static int playersOffset;

		// Token: 0x04002797 RID: 10135
		private static UGCQueryHandle_t detailsHandle;

		// Token: 0x04002798 RID: 10136
		private static CallResult<SteamUGCQueryCompleted_t> ugcQueryCompleted;

		// Token: 0x0400279A RID: 10138
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x0400279B RID: 10139
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x0400279C RID: 10140
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x0400279D RID: 10141
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x0400279E RID: 10142
		[CompilerGenerated]
		private static TempSteamworksMatchmaking.MasterServerQueryRefreshed <>f__mg$cache4;

		// Token: 0x0400279F RID: 10143
		[CompilerGenerated]
		private static Provider.IconQueryRefreshed <>f__mg$cache5;

		// Token: 0x040027A0 RID: 10144
		[CompilerGenerated]
		private static TempSteamworksMatchmaking.PlayersQueryRefreshed <>f__mg$cache6;

		// Token: 0x040027A1 RID: 10145
		[CompilerGenerated]
		private static TempSteamworksMatchmaking.RulesQueryRefreshed <>f__mg$cache7;

		// Token: 0x040027A2 RID: 10146
		[CompilerGenerated]
		private static CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate <>f__mg$cache8;

		// Token: 0x02000778 RID: 1912
		public enum EServerInfoOpenContext
		{
			// Token: 0x040027A4 RID: 10148
			CONNECT,
			// Token: 0x040027A5 RID: 10149
			SERVERS,
			// Token: 0x040027A6 RID: 10150
			MATCHMAKING
		}
	}
}
