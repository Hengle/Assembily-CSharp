using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Provider;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000779 RID: 1913
	public class MenuPlayServersUI
	{
		// Token: 0x060036D4 RID: 14036 RVA: 0x001798D0 File Offset: 0x00177CD0
		public MenuPlayServersUI()
		{
			if (MenuPlayServersUI.icons != null)
			{
				MenuPlayServersUI.icons.unload();
			}
			MenuPlayServersUI.localization = Localization.read("/Menu/Play/MenuPlayServers.dat");
			MenuPlayServersUI.icons = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Play/MenuPlayServers/MenuPlayServers.unity3d");
			MenuPlayServersUI.container = new Sleek();
			MenuPlayServersUI.container.positionOffset_X = 10;
			MenuPlayServersUI.container.positionOffset_Y = 10;
			MenuPlayServersUI.container.positionScale_Y = 1f;
			MenuPlayServersUI.container.sizeOffset_X = -20;
			MenuPlayServersUI.container.sizeOffset_Y = -20;
			MenuPlayServersUI.container.sizeScale_X = 1f;
			MenuPlayServersUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuPlayServersUI.container);
			MenuPlayServersUI.active = false;
			MenuPlayServersUI.list = new Sleek();
			MenuPlayServersUI.list.positionOffset_X = 210;
			MenuPlayServersUI.list.sizeOffset_X = -210;
			MenuPlayServersUI.list.sizeScale_X = 1f;
			MenuPlayServersUI.list.sizeScale_Y = 1f;
			MenuPlayServersUI.container.add(MenuPlayServersUI.list);
			MenuPlayServersUI.sortName = new SleekButton();
			MenuPlayServersUI.sortName.sizeOffset_X = -270;
			MenuPlayServersUI.sortName.sizeOffset_Y = 30;
			MenuPlayServersUI.sortName.sizeScale_X = 1f;
			MenuPlayServersUI.sortName.text = MenuPlayServersUI.localization.format("Sort_Name");
			MenuPlayServersUI.sortName.tooltip = MenuPlayServersUI.localization.format("Sort_Name_Tooltip");
			SleekButton sleekButton = MenuPlayServersUI.sortName;
			if (MenuPlayServersUI.<>f__mg$cache2 == null)
			{
				MenuPlayServersUI.<>f__mg$cache2 = new ClickedButton(MenuPlayServersUI.onClickedSortNameButton);
			}
			sleekButton.onClickedButton = MenuPlayServersUI.<>f__mg$cache2;
			MenuPlayServersUI.list.add(MenuPlayServersUI.sortName);
			MenuPlayServersUI.sortMap = new SleekButton();
			MenuPlayServersUI.sortMap.positionOffset_X = -260;
			MenuPlayServersUI.sortMap.positionScale_X = 1f;
			MenuPlayServersUI.sortMap.sizeOffset_X = 100;
			MenuPlayServersUI.sortMap.sizeOffset_Y = 30;
			MenuPlayServersUI.sortMap.text = MenuPlayServersUI.localization.format("Sort_Map");
			MenuPlayServersUI.sortMap.tooltip = MenuPlayServersUI.localization.format("Sort_Map_Tooltip");
			SleekButton sleekButton2 = MenuPlayServersUI.sortMap;
			if (MenuPlayServersUI.<>f__mg$cache3 == null)
			{
				MenuPlayServersUI.<>f__mg$cache3 = new ClickedButton(MenuPlayServersUI.onClickedSortMapButton);
			}
			sleekButton2.onClickedButton = MenuPlayServersUI.<>f__mg$cache3;
			MenuPlayServersUI.list.add(MenuPlayServersUI.sortMap);
			MenuPlayServersUI.sortPlayers = new SleekButton();
			MenuPlayServersUI.sortPlayers.positionOffset_X = -150;
			MenuPlayServersUI.sortPlayers.positionScale_X = 1f;
			MenuPlayServersUI.sortPlayers.sizeOffset_X = 60;
			MenuPlayServersUI.sortPlayers.sizeOffset_Y = 30;
			MenuPlayServersUI.sortPlayers.text = MenuPlayServersUI.localization.format("Sort_Players");
			MenuPlayServersUI.sortPlayers.tooltip = MenuPlayServersUI.localization.format("Sort_Players_Tooltip");
			SleekButton sleekButton3 = MenuPlayServersUI.sortPlayers;
			if (MenuPlayServersUI.<>f__mg$cache4 == null)
			{
				MenuPlayServersUI.<>f__mg$cache4 = new ClickedButton(MenuPlayServersUI.onClickedSortPlayersButton);
			}
			sleekButton3.onClickedButton = MenuPlayServersUI.<>f__mg$cache4;
			MenuPlayServersUI.list.add(MenuPlayServersUI.sortPlayers);
			MenuPlayServersUI.sortPing = new SleekButton();
			MenuPlayServersUI.sortPing.positionOffset_X = -80;
			MenuPlayServersUI.sortPing.positionScale_X = 1f;
			MenuPlayServersUI.sortPing.sizeOffset_X = 50;
			MenuPlayServersUI.sortPing.sizeOffset_Y = 30;
			MenuPlayServersUI.sortPing.text = MenuPlayServersUI.localization.format("Sort_Ping");
			MenuPlayServersUI.sortPing.tooltip = MenuPlayServersUI.localization.format("Sort_Ping_Tooltip");
			SleekButton sleekButton4 = MenuPlayServersUI.sortPing;
			if (MenuPlayServersUI.<>f__mg$cache5 == null)
			{
				MenuPlayServersUI.<>f__mg$cache5 = new ClickedButton(MenuPlayServersUI.onClickedSortPingButton);
			}
			sleekButton4.onClickedButton = MenuPlayServersUI.<>f__mg$cache5;
			MenuPlayServersUI.list.add(MenuPlayServersUI.sortPing);
			MenuPlayServersUI.infoBox = new SleekBox();
			MenuPlayServersUI.infoBox.positionOffset_Y = 40;
			MenuPlayServersUI.infoBox.sizeOffset_X = -30;
			MenuPlayServersUI.infoBox.sizeScale_X = 1f;
			MenuPlayServersUI.infoBox.sizeOffset_Y = 50;
			MenuPlayServersUI.infoBox.text = MenuPlayServersUI.localization.format("No_Servers", new object[]
			{
				Provider.APP_VERSION
			});
			MenuPlayServersUI.infoBox.fontSize = 14;
			MenuPlayServersUI.list.add(MenuPlayServersUI.infoBox);
			MenuPlayServersUI.infoBox.isVisible = false;
			MenuPlayServersUI.serverButtons = new List<SleekServer>();
			TempSteamworksMatchmaking matchmakingService = Provider.provider.matchmakingService;
			if (MenuPlayServersUI.<>f__mg$cache6 == null)
			{
				MenuPlayServersUI.<>f__mg$cache6 = new TempSteamworksMatchmaking.MasterServerAdded(MenuPlayServersUI.onMasterServerAdded);
			}
			matchmakingService.onMasterServerAdded = MenuPlayServersUI.<>f__mg$cache6;
			TempSteamworksMatchmaking matchmakingService2 = Provider.provider.matchmakingService;
			if (MenuPlayServersUI.<>f__mg$cache7 == null)
			{
				MenuPlayServersUI.<>f__mg$cache7 = new TempSteamworksMatchmaking.MasterServerRemoved(MenuPlayServersUI.onMasterServerRemoved);
			}
			matchmakingService2.onMasterServerRemoved = MenuPlayServersUI.<>f__mg$cache7;
			TempSteamworksMatchmaking matchmakingService3 = Provider.provider.matchmakingService;
			if (MenuPlayServersUI.<>f__mg$cache8 == null)
			{
				MenuPlayServersUI.<>f__mg$cache8 = new TempSteamworksMatchmaking.MasterServerResorted(MenuPlayServersUI.onMasterServerResorted);
			}
			matchmakingService3.onMasterServerResorted = MenuPlayServersUI.<>f__mg$cache8;
			TempSteamworksMatchmaking matchmakingService4 = Provider.provider.matchmakingService;
			if (MenuPlayServersUI.<>f__mg$cache9 == null)
			{
				MenuPlayServersUI.<>f__mg$cache9 = new TempSteamworksMatchmaking.MasterServerRefreshed(MenuPlayServersUI.onMasterServerRefreshed);
			}
			matchmakingService4.onMasterServerRefreshed = MenuPlayServersUI.<>f__mg$cache9;
			MenuPlayServersUI.nameField = new SleekField();
			MenuPlayServersUI.nameField.positionOffset_Y = -110;
			MenuPlayServersUI.nameField.positionScale_Y = 1f;
			MenuPlayServersUI.nameField.sizeOffset_X = -5;
			MenuPlayServersUI.nameField.sizeOffset_Y = 30;
			MenuPlayServersUI.nameField.sizeScale_X = 0.4f;
			MenuPlayServersUI.nameField.text = PlaySettings.serversName;
			MenuPlayServersUI.nameField.hint = MenuPlayServersUI.localization.format("Name_Field_Hint");
			SleekField sleekField = MenuPlayServersUI.nameField;
			if (MenuPlayServersUI.<>f__mg$cacheA == null)
			{
				MenuPlayServersUI.<>f__mg$cacheA = new Typed(MenuPlayServersUI.onTypedNameField);
			}
			sleekField.onTyped = MenuPlayServersUI.<>f__mg$cacheA;
			MenuPlayServersUI.list.add(MenuPlayServersUI.nameField);
			MenuPlayServersUI.passwordField = new SleekField();
			MenuPlayServersUI.passwordField.positionOffset_X = 5;
			MenuPlayServersUI.passwordField.positionOffset_Y = -110;
			MenuPlayServersUI.passwordField.positionScale_X = 0.4f;
			MenuPlayServersUI.passwordField.positionScale_Y = 1f;
			MenuPlayServersUI.passwordField.sizeOffset_X = -10;
			MenuPlayServersUI.passwordField.sizeOffset_Y = 30;
			MenuPlayServersUI.passwordField.sizeScale_X = 0.4f;
			MenuPlayServersUI.passwordField.replace = MenuPlayServersUI.localization.format("Password_Field_Replace").ToCharArray()[0];
			MenuPlayServersUI.passwordField.text = PlaySettings.serversPassword;
			MenuPlayServersUI.passwordField.hint = MenuPlayServersUI.localization.format("Password_Field_Hint");
			SleekField sleekField2 = MenuPlayServersUI.passwordField;
			if (MenuPlayServersUI.<>f__mg$cacheB == null)
			{
				MenuPlayServersUI.<>f__mg$cacheB = new Typed(MenuPlayServersUI.onTypedPasswordField);
			}
			sleekField2.onTyped = MenuPlayServersUI.<>f__mg$cacheB;
			MenuPlayServersUI.list.add(MenuPlayServersUI.passwordField);
			MenuPlayServersUI.refreshInternetButton = new SleekButton();
			MenuPlayServersUI.refreshInternetButton.sizeOffset_X = 200;
			MenuPlayServersUI.refreshInternetButton.sizeOffset_Y = 50;
			MenuPlayServersUI.refreshInternetButton.text = MenuPlayServersUI.localization.format("Refresh_Internet_Button");
			MenuPlayServersUI.refreshInternetButton.tooltip = MenuPlayServersUI.localization.format("Refresh_Internet_Button_Tooltip");
			SleekButton sleekButton5 = MenuPlayServersUI.refreshInternetButton;
			if (MenuPlayServersUI.<>f__mg$cacheC == null)
			{
				MenuPlayServersUI.<>f__mg$cacheC = new ClickedButton(MenuPlayServersUI.onClickedRefreshInternetButton);
			}
			sleekButton5.onClickedButton = MenuPlayServersUI.<>f__mg$cacheC;
			MenuPlayServersUI.refreshInternetButton.fontSize = 14;
			MenuPlayServersUI.container.add(MenuPlayServersUI.refreshInternetButton);
			MenuPlayServersUI.refreshLANButton = new SleekButton();
			MenuPlayServersUI.refreshLANButton.positionOffset_Y = 180;
			MenuPlayServersUI.refreshLANButton.sizeOffset_X = 200;
			MenuPlayServersUI.refreshLANButton.sizeOffset_Y = 50;
			MenuPlayServersUI.refreshLANButton.text = MenuPlayServersUI.localization.format("Refresh_LAN_Button");
			MenuPlayServersUI.refreshLANButton.tooltip = MenuPlayServersUI.localization.format("Refresh_LAN_Button_Tooltip");
			SleekButton sleekButton6 = MenuPlayServersUI.refreshLANButton;
			if (MenuPlayServersUI.<>f__mg$cacheD == null)
			{
				MenuPlayServersUI.<>f__mg$cacheD = new ClickedButton(MenuPlayServersUI.onClickedRefreshLANButton);
			}
			sleekButton6.onClickedButton = MenuPlayServersUI.<>f__mg$cacheD;
			MenuPlayServersUI.refreshLANButton.fontSize = 14;
			MenuPlayServersUI.container.add(MenuPlayServersUI.refreshLANButton);
			MenuPlayServersUI.refreshHistoryButton = new SleekButton();
			MenuPlayServersUI.refreshHistoryButton.positionOffset_Y = 120;
			MenuPlayServersUI.refreshHistoryButton.sizeOffset_X = 200;
			MenuPlayServersUI.refreshHistoryButton.sizeOffset_Y = 50;
			MenuPlayServersUI.refreshHistoryButton.text = MenuPlayServersUI.localization.format("Refresh_History_Button");
			MenuPlayServersUI.refreshHistoryButton.tooltip = MenuPlayServersUI.localization.format("Refresh_History_Button_Tooltip");
			SleekButton sleekButton7 = MenuPlayServersUI.refreshHistoryButton;
			if (MenuPlayServersUI.<>f__mg$cacheE == null)
			{
				MenuPlayServersUI.<>f__mg$cacheE = new ClickedButton(MenuPlayServersUI.onClickedRefreshHistoryButton);
			}
			sleekButton7.onClickedButton = MenuPlayServersUI.<>f__mg$cacheE;
			MenuPlayServersUI.refreshHistoryButton.fontSize = 14;
			MenuPlayServersUI.container.add(MenuPlayServersUI.refreshHistoryButton);
			MenuPlayServersUI.refreshFavoritesButton = new SleekButton();
			MenuPlayServersUI.refreshFavoritesButton.positionOffset_Y = 60;
			MenuPlayServersUI.refreshFavoritesButton.sizeOffset_X = 200;
			MenuPlayServersUI.refreshFavoritesButton.sizeOffset_Y = 50;
			MenuPlayServersUI.refreshFavoritesButton.text = MenuPlayServersUI.localization.format("Refresh_Favorites_Button");
			MenuPlayServersUI.refreshFavoritesButton.tooltip = MenuPlayServersUI.localization.format("Refresh_Favorites_Button_Tooltip");
			SleekButton sleekButton8 = MenuPlayServersUI.refreshFavoritesButton;
			if (MenuPlayServersUI.<>f__mg$cacheF == null)
			{
				MenuPlayServersUI.<>f__mg$cacheF = new ClickedButton(MenuPlayServersUI.onClickedRefreshFavoritesButton);
			}
			sleekButton8.onClickedButton = MenuPlayServersUI.<>f__mg$cacheF;
			MenuPlayServersUI.refreshFavoritesButton.fontSize = 14;
			MenuPlayServersUI.container.add(MenuPlayServersUI.refreshFavoritesButton);
			MenuPlayServersUI.refreshFriendsButton = new SleekButton();
			MenuPlayServersUI.refreshFriendsButton.positionOffset_Y = 240;
			MenuPlayServersUI.refreshFriendsButton.sizeOffset_X = 200;
			MenuPlayServersUI.refreshFriendsButton.sizeOffset_Y = 50;
			MenuPlayServersUI.refreshFriendsButton.text = MenuPlayServersUI.localization.format("Refresh_Friends_Button");
			MenuPlayServersUI.refreshFriendsButton.tooltip = MenuPlayServersUI.localization.format("Refresh_Friends_Button_Tooltip");
			SleekButton sleekButton9 = MenuPlayServersUI.refreshFriendsButton;
			if (MenuPlayServersUI.<>f__mg$cache10 == null)
			{
				MenuPlayServersUI.<>f__mg$cache10 = new ClickedButton(MenuPlayServersUI.onClickedRefreshFriendsButton);
			}
			sleekButton9.onClickedButton = MenuPlayServersUI.<>f__mg$cache10;
			MenuPlayServersUI.refreshFriendsButton.fontSize = 14;
			MenuPlayServersUI.container.add(MenuPlayServersUI.refreshFriendsButton);
			if (Provider.isPro)
			{
				MenuPlayServersUI.refreshLANButton.positionOffset_Y += 60;
				MenuPlayServersUI.refreshHistoryButton.positionOffset_Y += 60;
				MenuPlayServersUI.refreshFavoritesButton.positionOffset_Y += 60;
				MenuPlayServersUI.refreshFriendsButton.positionOffset_Y += 60;
				MenuPlayServersUI.refreshGoldButton = new SleekButton();
				MenuPlayServersUI.refreshGoldButton.positionOffset_Y = 60;
				MenuPlayServersUI.refreshGoldButton.sizeOffset_X = 200;
				MenuPlayServersUI.refreshGoldButton.sizeOffset_Y = 50;
				MenuPlayServersUI.refreshGoldButton.text = MenuPlayServersUI.localization.format("Refresh_Gold_Button");
				MenuPlayServersUI.refreshGoldButton.tooltip = MenuPlayServersUI.localization.format("Refresh_Gold_Button_Tooltip");
				SleekButton sleekButton10 = MenuPlayServersUI.refreshGoldButton;
				if (MenuPlayServersUI.<>f__mg$cache11 == null)
				{
					MenuPlayServersUI.<>f__mg$cache11 = new ClickedButton(MenuPlayServersUI.onClickedRefreshGoldButton);
				}
				sleekButton10.onClickedButton = MenuPlayServersUI.<>f__mg$cache11;
				MenuPlayServersUI.refreshGoldButton.fontSize = 14;
				MenuPlayServersUI.refreshGoldButton.backgroundTint = ESleekTint.NONE;
				MenuPlayServersUI.refreshGoldButton.foregroundTint = ESleekTint.NONE;
				MenuPlayServersUI.refreshGoldButton.backgroundColor = Palette.PRO;
				MenuPlayServersUI.refreshGoldButton.foregroundColor = Palette.PRO;
				MenuPlayServersUI.container.add(MenuPlayServersUI.refreshGoldButton);
			}
			MenuPlayServersUI.mapButtonState = new SleekButtonState(new GUIContent[0]);
			MenuPlayServersUI.mapButtonState.positionOffset_X = 5;
			MenuPlayServersUI.mapButtonState.positionOffset_Y = -70;
			MenuPlayServersUI.mapButtonState.positionScale_X = 0.2f;
			MenuPlayServersUI.mapButtonState.positionScale_Y = 1f;
			MenuPlayServersUI.mapButtonState.sizeOffset_X = -10;
			MenuPlayServersUI.mapButtonState.sizeOffset_Y = 30;
			MenuPlayServersUI.mapButtonState.sizeScale_X = 0.2f;
			SleekButtonState sleekButtonState = MenuPlayServersUI.mapButtonState;
			if (MenuPlayServersUI.<>f__mg$cache12 == null)
			{
				MenuPlayServersUI.<>f__mg$cache12 = new SwappedState(MenuPlayServersUI.onSwappedMapState);
			}
			sleekButtonState.onSwappedState = MenuPlayServersUI.<>f__mg$cache12;
			MenuPlayServersUI.list.add(MenuPlayServersUI.mapButtonState);
			MenuPlayServersUI.passwordButtonState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuPlayServersUI.localization.format("No_Password_Button")),
				new GUIContent(MenuPlayServersUI.localization.format("Yes_Password_Button")),
				new GUIContent(MenuPlayServersUI.localization.format("Any_Password_Button"))
			});
			MenuPlayServersUI.passwordButtonState.positionOffset_X = 5;
			MenuPlayServersUI.passwordButtonState.positionOffset_Y = -110;
			MenuPlayServersUI.passwordButtonState.positionScale_X = 0.8f;
			MenuPlayServersUI.passwordButtonState.positionScale_Y = 1f;
			MenuPlayServersUI.passwordButtonState.sizeOffset_X = -5;
			MenuPlayServersUI.passwordButtonState.sizeOffset_Y = 30;
			MenuPlayServersUI.passwordButtonState.sizeScale_X = 0.2f;
			MenuPlayServersUI.passwordButtonState.state = (int)FilterSettings.filterPassword;
			SleekButtonState sleekButtonState2 = MenuPlayServersUI.passwordButtonState;
			if (MenuPlayServersUI.<>f__mg$cache13 == null)
			{
				MenuPlayServersUI.<>f__mg$cache13 = new SwappedState(MenuPlayServersUI.onSwappedPasswordState);
			}
			sleekButtonState2.onSwappedState = MenuPlayServersUI.<>f__mg$cache13;
			MenuPlayServersUI.list.add(MenuPlayServersUI.passwordButtonState);
			MenuPlayServersUI.workshopButtonState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuPlayServersUI.localization.format("No_Workshop_Button")),
				new GUIContent(MenuPlayServersUI.localization.format("Yes_Workshop_Button")),
				new GUIContent(MenuPlayServersUI.localization.format("Any_Workshop_Button"))
			});
			MenuPlayServersUI.workshopButtonState.positionOffset_Y = -30;
			MenuPlayServersUI.workshopButtonState.positionScale_Y = 1f;
			MenuPlayServersUI.workshopButtonState.sizeOffset_X = -5;
			MenuPlayServersUI.workshopButtonState.sizeOffset_Y = 30;
			MenuPlayServersUI.workshopButtonState.sizeScale_X = 0.2f;
			MenuPlayServersUI.workshopButtonState.state = (int)FilterSettings.filterWorkshop;
			SleekButtonState sleekButtonState3 = MenuPlayServersUI.workshopButtonState;
			if (MenuPlayServersUI.<>f__mg$cache14 == null)
			{
				MenuPlayServersUI.<>f__mg$cache14 = new SwappedState(MenuPlayServersUI.onSwappedWorkshopState);
			}
			sleekButtonState3.onSwappedState = MenuPlayServersUI.<>f__mg$cache14;
			MenuPlayServersUI.list.add(MenuPlayServersUI.workshopButtonState);
			MenuPlayServersUI.pluginsButtonState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuPlayServersUI.localization.format("No_Plugins_Button")),
				new GUIContent(MenuPlayServersUI.localization.format("Yes_Plugins_Button")),
				new GUIContent(MenuPlayServersUI.localization.format("Any_Plugins_Button"))
			});
			MenuPlayServersUI.pluginsButtonState.positionOffset_X = 5;
			MenuPlayServersUI.pluginsButtonState.positionOffset_Y = -70;
			MenuPlayServersUI.pluginsButtonState.positionScale_X = 0.8f;
			MenuPlayServersUI.pluginsButtonState.positionScale_Y = 1f;
			MenuPlayServersUI.pluginsButtonState.sizeOffset_X = -5;
			MenuPlayServersUI.pluginsButtonState.sizeOffset_Y = 30;
			MenuPlayServersUI.pluginsButtonState.sizeScale_X = 0.2f;
			MenuPlayServersUI.pluginsButtonState.state = (int)FilterSettings.filterPlugins;
			SleekButtonState sleekButtonState4 = MenuPlayServersUI.pluginsButtonState;
			if (MenuPlayServersUI.<>f__mg$cache15 == null)
			{
				MenuPlayServersUI.<>f__mg$cache15 = new SwappedState(MenuPlayServersUI.onSwappedPluginsState);
			}
			sleekButtonState4.onSwappedState = MenuPlayServersUI.<>f__mg$cache15;
			MenuPlayServersUI.list.add(MenuPlayServersUI.pluginsButtonState);
			MenuPlayServersUI.cheatsButtonState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuPlayServersUI.localization.format("No_Cheats_Button")),
				new GUIContent(MenuPlayServersUI.localization.format("Yes_Cheats_Button")),
				new GUIContent(MenuPlayServersUI.localization.format("Any_Cheats_Button"))
			});
			MenuPlayServersUI.cheatsButtonState.positionOffset_X = 5;
			MenuPlayServersUI.cheatsButtonState.positionOffset_Y = -30;
			MenuPlayServersUI.cheatsButtonState.positionScale_X = 0.8f;
			MenuPlayServersUI.cheatsButtonState.positionScale_Y = 1f;
			MenuPlayServersUI.cheatsButtonState.sizeOffset_X = -5;
			MenuPlayServersUI.cheatsButtonState.sizeOffset_Y = 30;
			MenuPlayServersUI.cheatsButtonState.sizeScale_X = 0.2f;
			MenuPlayServersUI.cheatsButtonState.state = (int)FilterSettings.filterCheats;
			SleekButtonState sleekButtonState5 = MenuPlayServersUI.cheatsButtonState;
			if (MenuPlayServersUI.<>f__mg$cache16 == null)
			{
				MenuPlayServersUI.<>f__mg$cache16 = new SwappedState(MenuPlayServersUI.onSwappedCheatsState);
			}
			sleekButtonState5.onSwappedState = MenuPlayServersUI.<>f__mg$cache16;
			MenuPlayServersUI.list.add(MenuPlayServersUI.cheatsButtonState);
			MenuPlayServersUI.attendanceButtonState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuPlayServersUI.localization.format("Empty_Button"), (Texture)MenuPlayServersUI.icons.load("Empty")),
				new GUIContent(MenuPlayServersUI.localization.format("Space_Button"), (Texture)MenuPlayServersUI.icons.load("Space")),
				new GUIContent(MenuPlayServersUI.localization.format("Any_Attendance_Button"))
			});
			MenuPlayServersUI.attendanceButtonState.positionOffset_X = 5;
			MenuPlayServersUI.attendanceButtonState.positionOffset_Y = -30;
			MenuPlayServersUI.attendanceButtonState.positionScale_X = 0.4f;
			MenuPlayServersUI.attendanceButtonState.positionScale_Y = 1f;
			MenuPlayServersUI.attendanceButtonState.sizeOffset_X = -10;
			MenuPlayServersUI.attendanceButtonState.sizeOffset_Y = 30;
			MenuPlayServersUI.attendanceButtonState.sizeScale_X = 0.2f;
			MenuPlayServersUI.attendanceButtonState.state = (int)FilterSettings.filterAttendance;
			SleekButtonState sleekButtonState6 = MenuPlayServersUI.attendanceButtonState;
			if (MenuPlayServersUI.<>f__mg$cache17 == null)
			{
				MenuPlayServersUI.<>f__mg$cache17 = new SwappedState(MenuPlayServersUI.onSwappedAttendanceState);
			}
			sleekButtonState6.onSwappedState = MenuPlayServersUI.<>f__mg$cache17;
			MenuPlayServersUI.list.add(MenuPlayServersUI.attendanceButtonState);
			MenuPlayServersUI.VACProtectionButtonState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuPlayServersUI.localization.format("VAC_Secure_Button"), (Texture)MenuPlayServersUI.icons.load("VAC")),
				new GUIContent(MenuPlayServersUI.localization.format("VAC_Insecure_Button")),
				new GUIContent(MenuPlayServersUI.localization.format("VAC_Any_Button"))
			});
			MenuPlayServersUI.VACProtectionButtonState.positionOffset_X = 5;
			MenuPlayServersUI.VACProtectionButtonState.positionOffset_Y = -70;
			MenuPlayServersUI.VACProtectionButtonState.positionScale_X = 0.4f;
			MenuPlayServersUI.VACProtectionButtonState.positionScale_Y = 1f;
			MenuPlayServersUI.VACProtectionButtonState.sizeOffset_X = -10;
			MenuPlayServersUI.VACProtectionButtonState.sizeOffset_Y = 30;
			MenuPlayServersUI.VACProtectionButtonState.sizeScale_X = 0.2f;
			MenuPlayServersUI.VACProtectionButtonState.state = (int)FilterSettings.filterVACProtection;
			SleekButtonState vacprotectionButtonState = MenuPlayServersUI.VACProtectionButtonState;
			if (MenuPlayServersUI.<>f__mg$cache18 == null)
			{
				MenuPlayServersUI.<>f__mg$cache18 = new SwappedState(MenuPlayServersUI.onSwappedVACProtectionState);
			}
			vacprotectionButtonState.onSwappedState = MenuPlayServersUI.<>f__mg$cache18;
			MenuPlayServersUI.list.add(MenuPlayServersUI.VACProtectionButtonState);
			MenuPlayServersUI.battlEyeProtectionButtonState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuPlayServersUI.localization.format("BattlEye_Secure_Button"), (Texture)MenuPlayServersUI.icons.load("BattlEye")),
				new GUIContent(MenuPlayServersUI.localization.format("BattlEye_Insecure_Button")),
				new GUIContent(MenuPlayServersUI.localization.format("BattlEye_Any_Button"))
			});
			MenuPlayServersUI.battlEyeProtectionButtonState.positionOffset_X = 5;
			MenuPlayServersUI.battlEyeProtectionButtonState.positionOffset_Y = -70;
			MenuPlayServersUI.battlEyeProtectionButtonState.positionScale_X = 0.6f;
			MenuPlayServersUI.battlEyeProtectionButtonState.positionScale_Y = 1f;
			MenuPlayServersUI.battlEyeProtectionButtonState.sizeOffset_X = -10;
			MenuPlayServersUI.battlEyeProtectionButtonState.sizeOffset_Y = 30;
			MenuPlayServersUI.battlEyeProtectionButtonState.sizeScale_X = 0.2f;
			MenuPlayServersUI.battlEyeProtectionButtonState.state = (int)FilterSettings.filterBattlEyeProtection;
			SleekButtonState sleekButtonState7 = MenuPlayServersUI.battlEyeProtectionButtonState;
			if (MenuPlayServersUI.<>f__mg$cache19 == null)
			{
				MenuPlayServersUI.<>f__mg$cache19 = new SwappedState(MenuPlayServersUI.onSwappedBattlEyeProtectionState);
			}
			sleekButtonState7.onSwappedState = MenuPlayServersUI.<>f__mg$cache19;
			MenuPlayServersUI.list.add(MenuPlayServersUI.battlEyeProtectionButtonState);
			MenuPlayServersUI.combatButtonState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuPlayServersUI.localization.format("PvP_Button"), (Texture)MenuPlayServersUI.icons.load("PvP")),
				new GUIContent(MenuPlayServersUI.localization.format("PvE_Button"), (Texture)MenuPlayServersUI.icons.load("PvE")),
				new GUIContent(MenuPlayServersUI.localization.format("Any_Combat_Button"))
			});
			MenuPlayServersUI.combatButtonState.positionOffset_Y = -70;
			MenuPlayServersUI.combatButtonState.positionScale_Y = 1f;
			MenuPlayServersUI.combatButtonState.sizeOffset_X = -5;
			MenuPlayServersUI.combatButtonState.sizeOffset_Y = 30;
			MenuPlayServersUI.combatButtonState.sizeScale_X = 0.2f;
			MenuPlayServersUI.combatButtonState.state = (int)FilterSettings.filterCombat;
			SleekButtonState sleekButtonState8 = MenuPlayServersUI.combatButtonState;
			if (MenuPlayServersUI.<>f__mg$cache1A == null)
			{
				MenuPlayServersUI.<>f__mg$cache1A = new SwappedState(MenuPlayServersUI.onSwappedCombatState);
			}
			sleekButtonState8.onSwappedState = MenuPlayServersUI.<>f__mg$cache1A;
			MenuPlayServersUI.list.add(MenuPlayServersUI.combatButtonState);
			MenuPlayServersUI.modeButtonState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuPlayServersUI.localization.format("Easy_Button"), (Texture)MenuPlayServersUI.icons.load("Easy")),
				new GUIContent(MenuPlayServersUI.localization.format("Normal_Button"), (Texture)MenuPlayServersUI.icons.load("Normal")),
				new GUIContent(MenuPlayServersUI.localization.format("Hard_Button"), (Texture)MenuPlayServersUI.icons.load("Hard")),
				new GUIContent(MenuPlayServersUI.localization.format("Any_Mode_Button"))
			});
			MenuPlayServersUI.modeButtonState.positionOffset_X = 5;
			MenuPlayServersUI.modeButtonState.positionOffset_Y = -30;
			MenuPlayServersUI.modeButtonState.positionScale_X = 0.6f;
			MenuPlayServersUI.modeButtonState.positionScale_Y = 1f;
			MenuPlayServersUI.modeButtonState.sizeOffset_X = -10;
			MenuPlayServersUI.modeButtonState.sizeOffset_Y = 30;
			MenuPlayServersUI.modeButtonState.sizeScale_X = 0.2f;
			MenuPlayServersUI.modeButtonState.state = (int)FilterSettings.filterMode;
			SleekButtonState sleekButtonState9 = MenuPlayServersUI.modeButtonState;
			if (MenuPlayServersUI.<>f__mg$cache1B == null)
			{
				MenuPlayServersUI.<>f__mg$cache1B = new SwappedState(MenuPlayServersUI.onSwappedModeState);
			}
			sleekButtonState9.onSwappedState = MenuPlayServersUI.<>f__mg$cache1B;
			MenuPlayServersUI.list.add(MenuPlayServersUI.modeButtonState);
			MenuPlayServersUI.cameraButtonState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuPlayServersUI.localization.format("First_Button"), (Texture)MenuPlayServersUI.icons.load("First")),
				new GUIContent(MenuPlayServersUI.localization.format("Third_Button"), (Texture)MenuPlayServersUI.icons.load("Third")),
				new GUIContent(MenuPlayServersUI.localization.format("Both_Button"), (Texture)MenuPlayServersUI.icons.load("Both")),
				new GUIContent(MenuPlayServersUI.localization.format("Vehicle_Button"), (Texture)MenuPlayServersUI.icons.load("Vehicle")),
				new GUIContent(MenuPlayServersUI.localization.format("Any_Camera_Button"))
			});
			MenuPlayServersUI.cameraButtonState.positionOffset_X = 5;
			MenuPlayServersUI.cameraButtonState.positionOffset_Y = -30;
			MenuPlayServersUI.cameraButtonState.positionScale_X = 0.2f;
			MenuPlayServersUI.cameraButtonState.positionScale_Y = 1f;
			MenuPlayServersUI.cameraButtonState.sizeOffset_X = -10;
			MenuPlayServersUI.cameraButtonState.sizeOffset_Y = 30;
			MenuPlayServersUI.cameraButtonState.sizeScale_X = 0.2f;
			MenuPlayServersUI.cameraButtonState.state = (int)FilterSettings.filterCamera;
			SleekButtonState sleekButtonState10 = MenuPlayServersUI.cameraButtonState;
			if (MenuPlayServersUI.<>f__mg$cache1C == null)
			{
				MenuPlayServersUI.<>f__mg$cache1C = new SwappedState(MenuPlayServersUI.onSwappedCameraState);
			}
			sleekButtonState10.onSwappedState = MenuPlayServersUI.<>f__mg$cache1C;
			MenuPlayServersUI.list.add(MenuPlayServersUI.cameraButtonState);
			MenuPlayServersUI.serverBox = new SleekScrollBox();
			MenuPlayServersUI.serverBox.positionOffset_Y = 40;
			MenuPlayServersUI.serverBox.sizeOffset_Y = -160;
			MenuPlayServersUI.serverBox.sizeScale_X = 1f;
			MenuPlayServersUI.serverBox.sizeScale_Y = 1f;
			MenuPlayServersUI.serverBox.area = new Rect(0f, 0f, 5f, 0f);
			MenuPlayServersUI.list.add(MenuPlayServersUI.serverBox);
			MenuPlayServersUI.onLevelsRefreshed();
			Delegate onLevelsRefreshed = Level.onLevelsRefreshed;
			if (MenuPlayServersUI.<>f__mg$cache1D == null)
			{
				MenuPlayServersUI.<>f__mg$cache1D = new LevelsRefreshed(MenuPlayServersUI.onLevelsRefreshed);
			}
			Level.onLevelsRefreshed = (LevelsRefreshed)Delegate.Combine(onLevelsRefreshed, MenuPlayServersUI.<>f__mg$cache1D);
			MenuPlayServersUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuPlayServersUI.backButton.positionOffset_Y = -50;
			MenuPlayServersUI.backButton.positionScale_Y = 1f;
			MenuPlayServersUI.backButton.sizeOffset_X = 200;
			MenuPlayServersUI.backButton.sizeOffset_Y = 50;
			MenuPlayServersUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuPlayServersUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton11 = MenuPlayServersUI.backButton;
			if (MenuPlayServersUI.<>f__mg$cache1E == null)
			{
				MenuPlayServersUI.<>f__mg$cache1E = new ClickedButton(MenuPlayServersUI.onClickedBackButton);
			}
			sleekButton11.onClickedButton = MenuPlayServersUI.<>f__mg$cache1E;
			MenuPlayServersUI.backButton.fontSize = 14;
			MenuPlayServersUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuPlayServersUI.container.add(MenuPlayServersUI.backButton);
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x0017B081 File Offset: 0x00179481
		public static void open()
		{
			if (MenuPlayServersUI.active)
			{
				return;
			}
			MenuPlayServersUI.active = true;
			MenuPlayServersUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x0017B0AE File Offset: 0x001794AE
		public static void close()
		{
			if (!MenuPlayServersUI.active)
			{
				return;
			}
			MenuPlayServersUI.active = false;
			MenuSettings.save();
			MenuPlayServersUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x0017B0E0 File Offset: 0x001794E0
		private static void onClickedServer(SleekServer server, SteamServerInfo info)
		{
			if (info.isPro && !Provider.isPro)
			{
				return;
			}
			if (info.isPassworded && MenuPlayServersUI.passwordField.text == string.Empty)
			{
				return;
			}
			MenuSettings.save();
			MenuPlayServerInfoUI.open(info, MenuPlayServersUI.passwordField.text, MenuPlayServerInfoUI.EServerInfoOpenContext.SERVERS);
			MenuPlayServersUI.close();
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x0017B144 File Offset: 0x00179544
		private static void onMasterServerAdded(int insert, SteamServerInfo info)
		{
			if (insert > MenuPlayServersUI.serverButtons.Count)
			{
				return;
			}
			SleekServer sleekServer = new SleekServer(Provider.provider.matchmakingService.currentList, info);
			sleekServer.positionOffset_Y = insert * 40;
			sleekServer.sizeOffset_X = -30;
			sleekServer.sizeOffset_Y = 30;
			sleekServer.sizeScale_X = 1f;
			SleekServer sleekServer2 = sleekServer;
			if (MenuPlayServersUI.<>f__mg$cache0 == null)
			{
				MenuPlayServersUI.<>f__mg$cache0 = new ClickedServer(MenuPlayServersUI.onClickedServer);
			}
			sleekServer2.onClickedServer = MenuPlayServersUI.<>f__mg$cache0;
			MenuPlayServersUI.serverBox.add(sleekServer);
			for (int i = insert; i < MenuPlayServersUI.serverButtons.Count; i++)
			{
				MenuPlayServersUI.serverButtons[i].positionOffset_Y += 40;
			}
			MenuPlayServersUI.serverButtons.Insert(insert, sleekServer);
			MenuPlayServersUI.serverBox.area = new Rect(0f, 0f, 5f, (float)(MenuPlayServersUI.serverButtons.Count * 40 - 10));
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x0017B23A File Offset: 0x0017963A
		private static void onMasterServerRemoved()
		{
			MenuPlayServersUI.infoBox.isVisible = false;
			MenuPlayServersUI.serverBox.remove();
			MenuPlayServersUI.serverButtons.Clear();
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x0017B25C File Offset: 0x0017965C
		private static void onMasterServerResorted()
		{
			MenuPlayServersUI.infoBox.isVisible = false;
			MenuPlayServersUI.serverBox.remove();
			MenuPlayServersUI.serverButtons.Clear();
			for (int i = 0; i < Provider.provider.matchmakingService.serverList.Count; i++)
			{
				SteamServerInfo newInfo = Provider.provider.matchmakingService.serverList[i];
				SleekServer sleekServer = new SleekServer(Provider.provider.matchmakingService.currentList, newInfo);
				sleekServer.positionOffset_Y = i * 40;
				sleekServer.sizeOffset_X = -30;
				sleekServer.sizeOffset_Y = 30;
				sleekServer.sizeScale_X = 1f;
				SleekServer sleekServer2 = sleekServer;
				if (MenuPlayServersUI.<>f__mg$cache1 == null)
				{
					MenuPlayServersUI.<>f__mg$cache1 = new ClickedServer(MenuPlayServersUI.onClickedServer);
				}
				sleekServer2.onClickedServer = MenuPlayServersUI.<>f__mg$cache1;
				MenuPlayServersUI.serverBox.add(sleekServer);
				MenuPlayServersUI.serverButtons.Add(sleekServer);
			}
			MenuPlayServersUI.serverBox.area = new Rect(0f, 0f, 5f, (float)(Provider.provider.matchmakingService.serverList.Count * 40 - 10));
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x0017B370 File Offset: 0x00179770
		private static void onMasterServerRefreshed(EMatchMakingServerResponse response)
		{
			if (MenuPlayServersUI.serverButtons.Count == 0)
			{
				MenuPlayServersUI.infoBox.isVisible = true;
			}
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x0017B38C File Offset: 0x0017978C
		private static void onClickedSortNameButton(SleekButton button)
		{
			if (Provider.provider.matchmakingService.serverInfoComparer is SteamServerInfoNameAscendingComparator)
			{
				Provider.provider.matchmakingService.sortMasterServer(new SteamServerInfoNameDescendingComparator());
			}
			else
			{
				Provider.provider.matchmakingService.sortMasterServer(new SteamServerInfoNameAscendingComparator());
			}
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x0017B3E0 File Offset: 0x001797E0
		private static void onClickedSortMapButton(SleekButton button)
		{
			if (Provider.provider.matchmakingService.serverInfoComparer is SteamServerInfoMapAscendingComparator)
			{
				Provider.provider.matchmakingService.sortMasterServer(new SteamServerInfoMapDescendingComparator());
			}
			else
			{
				Provider.provider.matchmakingService.sortMasterServer(new SteamServerInfoMapAscendingComparator());
			}
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x0017B434 File Offset: 0x00179834
		private static void onClickedSortPlayersButton(SleekButton button)
		{
			if (Provider.provider.matchmakingService.serverInfoComparer is SteamServerInfoPlayersAscendingComparator)
			{
				Provider.provider.matchmakingService.sortMasterServer(new SteamServerInfoPlayersDescendingComparator());
			}
			else
			{
				Provider.provider.matchmakingService.sortMasterServer(new SteamServerInfoPlayersAscendingComparator());
			}
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x0017B488 File Offset: 0x00179888
		private static void onClickedSortPingButton(SleekButton button)
		{
			if (Provider.provider.matchmakingService.serverInfoComparer is SteamServerInfoPingAscendingComparator)
			{
				Provider.provider.matchmakingService.sortMasterServer(new SteamServerInfoPingDescendingComparator());
			}
			else
			{
				Provider.provider.matchmakingService.sortMasterServer(new SteamServerInfoPingAscendingComparator());
			}
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x0017B4DC File Offset: 0x001798DC
		private static void onClickedRefreshInternetButton(SleekButton button)
		{
			Provider.provider.matchmakingService.refreshMasterServer(ESteamServerList.INTERNET, FilterSettings.filterMap, FilterSettings.filterPassword, FilterSettings.filterWorkshop, FilterSettings.filterPlugins, FilterSettings.filterAttendance, FilterSettings.filterVACProtection, FilterSettings.filterBattlEyeProtection, false, FilterSettings.filterCombat, FilterSettings.filterCheats, FilterSettings.filterMode, FilterSettings.filterCamera);
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x0017B534 File Offset: 0x00179934
		private static void onClickedRefreshGoldButton(SleekButton button)
		{
			Provider.provider.matchmakingService.refreshMasterServer(ESteamServerList.INTERNET, FilterSettings.filterMap, FilterSettings.filterPassword, FilterSettings.filterWorkshop, FilterSettings.filterPlugins, FilterSettings.filterAttendance, FilterSettings.filterVACProtection, FilterSettings.filterBattlEyeProtection, true, FilterSettings.filterCombat, FilterSettings.filterCheats, FilterSettings.filterMode, FilterSettings.filterCamera);
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x0017B58C File Offset: 0x0017998C
		private static void onClickedRefreshLANButton(SleekButton button)
		{
			Provider.provider.matchmakingService.refreshMasterServer(ESteamServerList.LAN, FilterSettings.filterMap, FilterSettings.filterPassword, FilterSettings.filterWorkshop, FilterSettings.filterPlugins, FilterSettings.filterAttendance, FilterSettings.filterVACProtection, FilterSettings.filterBattlEyeProtection, false, FilterSettings.filterCombat, FilterSettings.filterCheats, FilterSettings.filterMode, FilterSettings.filterCamera);
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x0017B5E4 File Offset: 0x001799E4
		private static void onClickedRefreshHistoryButton(SleekButton button)
		{
			Provider.provider.matchmakingService.refreshMasterServer(ESteamServerList.HISTORY, FilterSettings.filterMap, FilterSettings.filterPassword, FilterSettings.filterWorkshop, FilterSettings.filterPlugins, FilterSettings.filterAttendance, FilterSettings.filterVACProtection, FilterSettings.filterBattlEyeProtection, false, FilterSettings.filterCombat, FilterSettings.filterCheats, FilterSettings.filterMode, FilterSettings.filterCamera);
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x0017B63C File Offset: 0x00179A3C
		private static void onClickedRefreshFavoritesButton(SleekButton button)
		{
			Provider.provider.matchmakingService.refreshMasterServer(ESteamServerList.FAVORITES, FilterSettings.filterMap, FilterSettings.filterPassword, FilterSettings.filterWorkshop, FilterSettings.filterPlugins, FilterSettings.filterAttendance, FilterSettings.filterVACProtection, FilterSettings.filterBattlEyeProtection, false, FilterSettings.filterCombat, FilterSettings.filterCheats, FilterSettings.filterMode, FilterSettings.filterCamera);
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x0017B694 File Offset: 0x00179A94
		private static void onClickedRefreshFriendsButton(SleekButton button)
		{
			Provider.provider.matchmakingService.refreshMasterServer(ESteamServerList.FRIENDS, FilterSettings.filterMap, FilterSettings.filterPassword, FilterSettings.filterWorkshop, FilterSettings.filterPlugins, FilterSettings.filterAttendance, FilterSettings.filterVACProtection, FilterSettings.filterBattlEyeProtection, false, FilterSettings.filterCombat, FilterSettings.filterCheats, FilterSettings.filterMode, FilterSettings.filterCamera);
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x0017B6E9 File Offset: 0x00179AE9
		private static void onTypedNameField(SleekField field, string text)
		{
			PlaySettings.serversName = text;
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x0017B6F1 File Offset: 0x00179AF1
		private static void onTypedPasswordField(SleekField field, string text)
		{
			PlaySettings.serversPassword = text;
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x0017B6F9 File Offset: 0x00179AF9
		private static void onSwappedMapState(SleekButtonState button, int index)
		{
			if (index > 0)
			{
				FilterSettings.filterMap = MenuPlayServersUI.levels[index - 1].name;
			}
			else
			{
				FilterSettings.filterMap = string.Empty;
			}
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x0017B724 File Offset: 0x00179B24
		private static void onSwappedPasswordState(SleekButtonState button, int index)
		{
			FilterSettings.filterPassword = (EPassword)index;
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x0017B72C File Offset: 0x00179B2C
		private static void onSwappedWorkshopState(SleekButtonState button, int index)
		{
			FilterSettings.filterWorkshop = (EWorkshop)index;
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x0017B734 File Offset: 0x00179B34
		private static void onSwappedPluginsState(SleekButtonState button, int index)
		{
			FilterSettings.filterPlugins = (EPlugins)index;
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x0017B73C File Offset: 0x00179B3C
		private static void onSwappedCheatsState(SleekButtonState button, int index)
		{
			FilterSettings.filterCheats = (ECheats)index;
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x0017B744 File Offset: 0x00179B44
		private static void onSwappedAttendanceState(SleekButtonState button, int index)
		{
			FilterSettings.filterAttendance = (EAttendance)index;
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x0017B74C File Offset: 0x00179B4C
		private static void onSwappedVACProtectionState(SleekButtonState button, int index)
		{
			FilterSettings.filterVACProtection = (EVACProtectionFilter)index;
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x0017B754 File Offset: 0x00179B54
		private static void onSwappedBattlEyeProtectionState(SleekButtonState button, int index)
		{
			FilterSettings.filterBattlEyeProtection = (EBattlEyeProtectionFilter)index;
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x0017B75C File Offset: 0x00179B5C
		private static void onSwappedCombatState(SleekButtonState button, int index)
		{
			FilterSettings.filterCombat = (ECombat)index;
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x0017B764 File Offset: 0x00179B64
		private static void onSwappedModeState(SleekButtonState button, int index)
		{
			FilterSettings.filterMode = (EGameMode)index;
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x0017B76C File Offset: 0x00179B6C
		private static void onSwappedCameraState(SleekButtonState button, int index)
		{
			FilterSettings.filterCamera = (ECameraMode)index;
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x0017B774 File Offset: 0x00179B74
		private static void onLevelsRefreshed()
		{
			MenuPlayServersUI.levels = Level.getLevels(ESingleplayerMapCategory.ALL);
			GUIContent[] array = new GUIContent[MenuPlayServersUI.levels.Length + 1];
			array[0] = new GUIContent(MenuPlayServersUI.localization.format("Any_Map"));
			for (int i = 0; i < MenuPlayServersUI.levels.Length; i++)
			{
				LevelInfo levelInfo = MenuPlayServersUI.levels[i];
				if (levelInfo != null)
				{
					Local local = Localization.tryRead(levelInfo.path, false);
					if (local != null && local.has("Name"))
					{
						array[i + 1] = new GUIContent(local.format("Name"));
					}
					else
					{
						array[i + 1] = new GUIContent(levelInfo.name);
					}
				}
			}
			int num = -1;
			for (int j = 0; j < MenuPlayServersUI.levels.Length; j++)
			{
				LevelInfo levelInfo2 = MenuPlayServersUI.levels[j];
				if (levelInfo2 != null && levelInfo2.name == FilterSettings.filterMap)
				{
					num = j;
					break;
				}
			}
			if (num != -1 && MenuPlayServersUI.levels[num] != null)
			{
				FilterSettings.filterMap = MenuPlayServersUI.levels[num].name;
				num++;
			}
			else
			{
				FilterSettings.filterMap = string.Empty;
				num = 0;
			}
			MenuPlayServersUI.mapButtonState.setContent(array);
			MenuPlayServersUI.mapButtonState.state = num;
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x0017B8CA File Offset: 0x00179CCA
		private static void onClickedBackButton(SleekButton button)
		{
			MenuPlayUI.open();
			MenuPlayServersUI.close();
		}

		// Token: 0x040027A7 RID: 10151
		public static Local localization;

		// Token: 0x040027A8 RID: 10152
		public static Bundle icons;

		// Token: 0x040027A9 RID: 10153
		private static Sleek container;

		// Token: 0x040027AA RID: 10154
		private static Sleek list;

		// Token: 0x040027AB RID: 10155
		public static bool active;

		// Token: 0x040027AC RID: 10156
		private static SleekButtonIcon backButton;

		// Token: 0x040027AD RID: 10157
		private static LevelInfo[] levels;

		// Token: 0x040027AE RID: 10158
		private static SleekScrollBox serverBox;

		// Token: 0x040027AF RID: 10159
		private static SleekBox infoBox;

		// Token: 0x040027B0 RID: 10160
		private static List<SleekServer> serverButtons;

		// Token: 0x040027B1 RID: 10161
		private static SleekButton sortName;

		// Token: 0x040027B2 RID: 10162
		private static SleekButton sortMap;

		// Token: 0x040027B3 RID: 10163
		private static SleekButton sortPlayers;

		// Token: 0x040027B4 RID: 10164
		private static SleekButton sortPing;

		// Token: 0x040027B5 RID: 10165
		private static SleekField nameField;

		// Token: 0x040027B6 RID: 10166
		private static SleekField passwordField;

		// Token: 0x040027B7 RID: 10167
		private static SleekButton refreshInternetButton;

		// Token: 0x040027B8 RID: 10168
		private static SleekButton refreshGoldButton;

		// Token: 0x040027B9 RID: 10169
		private static SleekButton refreshLANButton;

		// Token: 0x040027BA RID: 10170
		private static SleekButton refreshHistoryButton;

		// Token: 0x040027BB RID: 10171
		private static SleekButton refreshFavoritesButton;

		// Token: 0x040027BC RID: 10172
		private static SleekButton refreshFriendsButton;

		// Token: 0x040027BD RID: 10173
		private static SleekButtonState mapButtonState;

		// Token: 0x040027BE RID: 10174
		private static SleekButtonState passwordButtonState;

		// Token: 0x040027BF RID: 10175
		private static SleekButtonState workshopButtonState;

		// Token: 0x040027C0 RID: 10176
		private static SleekButtonState pluginsButtonState;

		// Token: 0x040027C1 RID: 10177
		private static SleekButtonState cheatsButtonState;

		// Token: 0x040027C2 RID: 10178
		private static SleekButtonState attendanceButtonState;

		// Token: 0x040027C3 RID: 10179
		private static SleekButtonState VACProtectionButtonState;

		// Token: 0x040027C4 RID: 10180
		private static SleekButtonState battlEyeProtectionButtonState;

		// Token: 0x040027C5 RID: 10181
		private static SleekButtonState combatButtonState;

		// Token: 0x040027C6 RID: 10182
		private static SleekButtonState modeButtonState;

		// Token: 0x040027C7 RID: 10183
		private static SleekButtonState cameraButtonState;

		// Token: 0x040027C8 RID: 10184
		[CompilerGenerated]
		private static ClickedServer <>f__mg$cache0;

		// Token: 0x040027C9 RID: 10185
		[CompilerGenerated]
		private static ClickedServer <>f__mg$cache1;

		// Token: 0x040027CA RID: 10186
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x040027CB RID: 10187
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x040027CC RID: 10188
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;

		// Token: 0x040027CD RID: 10189
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;

		// Token: 0x040027CE RID: 10190
		[CompilerGenerated]
		private static TempSteamworksMatchmaking.MasterServerAdded <>f__mg$cache6;

		// Token: 0x040027CF RID: 10191
		[CompilerGenerated]
		private static TempSteamworksMatchmaking.MasterServerRemoved <>f__mg$cache7;

		// Token: 0x040027D0 RID: 10192
		[CompilerGenerated]
		private static TempSteamworksMatchmaking.MasterServerResorted <>f__mg$cache8;

		// Token: 0x040027D1 RID: 10193
		[CompilerGenerated]
		private static TempSteamworksMatchmaking.MasterServerRefreshed <>f__mg$cache9;

		// Token: 0x040027D2 RID: 10194
		[CompilerGenerated]
		private static Typed <>f__mg$cacheA;

		// Token: 0x040027D3 RID: 10195
		[CompilerGenerated]
		private static Typed <>f__mg$cacheB;

		// Token: 0x040027D4 RID: 10196
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheC;

		// Token: 0x040027D5 RID: 10197
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheD;

		// Token: 0x040027D6 RID: 10198
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheE;

		// Token: 0x040027D7 RID: 10199
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheF;

		// Token: 0x040027D8 RID: 10200
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache10;

		// Token: 0x040027D9 RID: 10201
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache11;

		// Token: 0x040027DA RID: 10202
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache12;

		// Token: 0x040027DB RID: 10203
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache13;

		// Token: 0x040027DC RID: 10204
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache14;

		// Token: 0x040027DD RID: 10205
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache15;

		// Token: 0x040027DE RID: 10206
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache16;

		// Token: 0x040027DF RID: 10207
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache17;

		// Token: 0x040027E0 RID: 10208
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache18;

		// Token: 0x040027E1 RID: 10209
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache19;

		// Token: 0x040027E2 RID: 10210
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache1A;

		// Token: 0x040027E3 RID: 10211
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache1B;

		// Token: 0x040027E4 RID: 10212
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache1C;

		// Token: 0x040027E5 RID: 10213
		[CompilerGenerated]
		private static LevelsRefreshed <>f__mg$cache1D;

		// Token: 0x040027E6 RID: 10214
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1E;
	}
}
