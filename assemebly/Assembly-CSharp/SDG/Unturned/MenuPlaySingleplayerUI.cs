using System;
using System.Runtime.CompilerServices;
using SDG.SteamworksProvider.Services.Economy;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200077A RID: 1914
	public class MenuPlaySingleplayerUI
	{
		// Token: 0x060036F5 RID: 14069 RVA: 0x0017B8D8 File Offset: 0x00179CD8
		public MenuPlaySingleplayerUI()
		{
			MenuPlaySingleplayerUI.localization = Localization.read("/Menu/Play/MenuPlaySingleplayer.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Play/MenuPlaySingleplayer/MenuPlaySingleplayer.unity3d");
			MenuPlaySingleplayerUI.container = new Sleek();
			MenuPlaySingleplayerUI.container.positionOffset_X = 10;
			MenuPlaySingleplayerUI.container.positionOffset_Y = 10;
			MenuPlaySingleplayerUI.container.positionScale_Y = 1f;
			MenuPlaySingleplayerUI.container.sizeOffset_X = -20;
			MenuPlaySingleplayerUI.container.sizeOffset_Y = -20;
			MenuPlaySingleplayerUI.container.sizeScale_X = 1f;
			MenuPlaySingleplayerUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuPlaySingleplayerUI.container);
			MenuPlaySingleplayerUI.active = false;
			MenuPlaySingleplayerUI.previewBox = new SleekBox();
			MenuPlaySingleplayerUI.previewBox.positionOffset_X = -450;
			MenuPlaySingleplayerUI.previewBox.positionOffset_Y = 100;
			MenuPlaySingleplayerUI.previewBox.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.previewBox.sizeOffset_X = 355;
			MenuPlaySingleplayerUI.previewBox.sizeOffset_Y = 180;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.previewBox);
			MenuPlaySingleplayerUI.previewImage = new SleekImageTexture();
			MenuPlaySingleplayerUI.previewImage.positionOffset_X = 10;
			MenuPlaySingleplayerUI.previewImage.positionOffset_Y = 10;
			MenuPlaySingleplayerUI.previewImage.sizeOffset_X = -20;
			MenuPlaySingleplayerUI.previewImage.sizeOffset_Y = -20;
			MenuPlaySingleplayerUI.previewImage.sizeScale_X = 1f;
			MenuPlaySingleplayerUI.previewImage.sizeScale_Y = 1f;
			MenuPlaySingleplayerUI.previewImage.shouldDestroyTexture = true;
			MenuPlaySingleplayerUI.previewBox.add(MenuPlaySingleplayerUI.previewImage);
			MenuPlaySingleplayerUI.levelScrollBox = new SleekScrollBox();
			MenuPlaySingleplayerUI.levelScrollBox.positionOffset_X = -240;
			MenuPlaySingleplayerUI.levelScrollBox.positionOffset_Y = 340;
			MenuPlaySingleplayerUI.levelScrollBox.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.levelScrollBox.sizeOffset_X = 430;
			MenuPlaySingleplayerUI.levelScrollBox.sizeOffset_Y = -440;
			MenuPlaySingleplayerUI.levelScrollBox.sizeScale_Y = 1f;
			MenuPlaySingleplayerUI.levelScrollBox.area = new Rect(0f, 0f, 5f, 0f);
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.levelScrollBox);
			MenuPlaySingleplayerUI.officalMapsButton = new SleekButton();
			MenuPlaySingleplayerUI.officalMapsButton.positionOffset_X = -240;
			MenuPlaySingleplayerUI.officalMapsButton.positionOffset_Y = 290;
			MenuPlaySingleplayerUI.officalMapsButton.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.officalMapsButton.sizeOffset_X = 100;
			MenuPlaySingleplayerUI.officalMapsButton.sizeOffset_Y = 50;
			MenuPlaySingleplayerUI.officalMapsButton.text = MenuPlaySingleplayerUI.localization.format("Maps_Official");
			MenuPlaySingleplayerUI.officalMapsButton.tooltip = MenuPlaySingleplayerUI.localization.format("Maps_Official_Tooltip");
			SleekButton sleekButton = MenuPlaySingleplayerUI.officalMapsButton;
			if (MenuPlaySingleplayerUI.<>f__mg$cache1 == null)
			{
				MenuPlaySingleplayerUI.<>f__mg$cache1 = new ClickedButton(MenuPlaySingleplayerUI.onClickedOfficialMapsButton);
			}
			sleekButton.onClickedButton = MenuPlaySingleplayerUI.<>f__mg$cache1;
			MenuPlaySingleplayerUI.officalMapsButton.fontSize = 14;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.officalMapsButton);
			if (Provider.statusData.Maps.Official != EMapStatus.NONE)
			{
				SleekNew sleek = new SleekNew(Provider.statusData.Maps.Official == EMapStatus.UPDATED);
				MenuPlaySingleplayerUI.officalMapsButton.add(sleek);
			}
			MenuPlaySingleplayerUI.curatedMapsButton = new SleekButton();
			MenuPlaySingleplayerUI.curatedMapsButton.positionOffset_X = -140;
			MenuPlaySingleplayerUI.curatedMapsButton.positionOffset_Y = 290;
			MenuPlaySingleplayerUI.curatedMapsButton.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.curatedMapsButton.sizeOffset_X = 100;
			MenuPlaySingleplayerUI.curatedMapsButton.sizeOffset_Y = 50;
			MenuPlaySingleplayerUI.curatedMapsButton.text = MenuPlaySingleplayerUI.localization.format("Maps_Curated");
			MenuPlaySingleplayerUI.curatedMapsButton.tooltip = MenuPlaySingleplayerUI.localization.format("Maps_Curated_Tooltip");
			SleekButton sleekButton2 = MenuPlaySingleplayerUI.curatedMapsButton;
			if (MenuPlaySingleplayerUI.<>f__mg$cache2 == null)
			{
				MenuPlaySingleplayerUI.<>f__mg$cache2 = new ClickedButton(MenuPlaySingleplayerUI.onClickedCuratedMapsButton);
			}
			sleekButton2.onClickedButton = MenuPlaySingleplayerUI.<>f__mg$cache2;
			MenuPlaySingleplayerUI.curatedMapsButton.fontSize = 14;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.curatedMapsButton);
			if (Provider.statusData.Maps.Curated != EMapStatus.NONE)
			{
				SleekNew sleek2 = new SleekNew(Provider.statusData.Maps.Curated == EMapStatus.UPDATED);
				MenuPlaySingleplayerUI.curatedMapsButton.add(sleek2);
			}
			MenuPlaySingleplayerUI.workshopMapsButton = new SleekButton();
			MenuPlaySingleplayerUI.workshopMapsButton.positionOffset_X = -40;
			MenuPlaySingleplayerUI.workshopMapsButton.positionOffset_Y = 290;
			MenuPlaySingleplayerUI.workshopMapsButton.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.workshopMapsButton.sizeOffset_X = 100;
			MenuPlaySingleplayerUI.workshopMapsButton.sizeOffset_Y = 50;
			MenuPlaySingleplayerUI.workshopMapsButton.text = MenuPlaySingleplayerUI.localization.format("Maps_Workshop");
			MenuPlaySingleplayerUI.workshopMapsButton.tooltip = MenuPlaySingleplayerUI.localization.format("Maps_Workshop_Tooltip");
			SleekButton sleekButton3 = MenuPlaySingleplayerUI.workshopMapsButton;
			if (MenuPlaySingleplayerUI.<>f__mg$cache3 == null)
			{
				MenuPlaySingleplayerUI.<>f__mg$cache3 = new ClickedButton(MenuPlaySingleplayerUI.onClickedWorkshopMapsButton);
			}
			sleekButton3.onClickedButton = MenuPlaySingleplayerUI.<>f__mg$cache3;
			MenuPlaySingleplayerUI.workshopMapsButton.fontSize = 14;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.workshopMapsButton);
			MenuPlaySingleplayerUI.miscMapsButton = new SleekButton();
			MenuPlaySingleplayerUI.miscMapsButton.positionOffset_X = 60;
			MenuPlaySingleplayerUI.miscMapsButton.positionOffset_Y = 290;
			MenuPlaySingleplayerUI.miscMapsButton.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.miscMapsButton.sizeOffset_X = 100;
			MenuPlaySingleplayerUI.miscMapsButton.sizeOffset_Y = 50;
			MenuPlaySingleplayerUI.miscMapsButton.text = MenuPlaySingleplayerUI.localization.format("Maps_Misc");
			MenuPlaySingleplayerUI.miscMapsButton.tooltip = MenuPlaySingleplayerUI.localization.format("Maps_Misc_Tooltip");
			SleekButton sleekButton4 = MenuPlaySingleplayerUI.miscMapsButton;
			if (MenuPlaySingleplayerUI.<>f__mg$cache4 == null)
			{
				MenuPlaySingleplayerUI.<>f__mg$cache4 = new ClickedButton(MenuPlaySingleplayerUI.onClickedMiscMapsButton);
			}
			sleekButton4.onClickedButton = MenuPlaySingleplayerUI.<>f__mg$cache4;
			MenuPlaySingleplayerUI.miscMapsButton.fontSize = 14;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.miscMapsButton);
			if (Provider.statusData.Maps.Misc != EMapStatus.NONE)
			{
				SleekNew sleek3 = new SleekNew(Provider.statusData.Maps.Misc == EMapStatus.UPDATED);
				MenuPlaySingleplayerUI.miscMapsButton.add(sleek3);
			}
			MenuPlaySingleplayerUI.selectedBox = new SleekBox();
			MenuPlaySingleplayerUI.selectedBox.positionOffset_X = -85;
			MenuPlaySingleplayerUI.selectedBox.positionOffset_Y = 100;
			MenuPlaySingleplayerUI.selectedBox.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.selectedBox.sizeOffset_X = 275;
			MenuPlaySingleplayerUI.selectedBox.sizeOffset_Y = 30;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.selectedBox);
			MenuPlaySingleplayerUI.descriptionBox = new SleekBox();
			MenuPlaySingleplayerUI.descriptionBox.positionOffset_X = -85;
			MenuPlaySingleplayerUI.descriptionBox.positionOffset_Y = 140;
			MenuPlaySingleplayerUI.descriptionBox.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.descriptionBox.sizeOffset_X = 275;
			MenuPlaySingleplayerUI.descriptionBox.sizeOffset_Y = 140;
			MenuPlaySingleplayerUI.descriptionBox.fontAlignment = TextAnchor.UpperCenter;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.descriptionBox);
			MenuPlaySingleplayerUI.creditsBox = new SleekBox();
			MenuPlaySingleplayerUI.creditsBox.positionOffset_X = 200;
			MenuPlaySingleplayerUI.creditsBox.positionOffset_Y = 100;
			MenuPlaySingleplayerUI.creditsBox.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.creditsBox.sizeOffset_X = 250;
			MenuPlaySingleplayerUI.creditsBox.foregroundTint = ESleekTint.NONE;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.creditsBox);
			MenuPlaySingleplayerUI.creditsBox.isVisible = false;
			MenuPlaySingleplayerUI.timedBox = new SleekBox();
			MenuPlaySingleplayerUI.timedBox.isRich = true;
			MenuPlaySingleplayerUI.timedBox.positionOffset_X = 200;
			MenuPlaySingleplayerUI.timedBox.positionOffset_Y = 100;
			MenuPlaySingleplayerUI.timedBox.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.timedBox.sizeOffset_X = 250;
			MenuPlaySingleplayerUI.timedBox.sizeOffset_Y = 70;
			MenuPlaySingleplayerUI.timedBox.foregroundTint = ESleekTint.NONE;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.timedBox);
			MenuPlaySingleplayerUI.timedBox.isVisible = false;
			MenuPlaySingleplayerUI.itemButton = new SleekButton();
			MenuPlaySingleplayerUI.itemButton.isRich = true;
			MenuPlaySingleplayerUI.itemButton.positionOffset_X = 200;
			MenuPlaySingleplayerUI.itemButton.positionOffset_Y = 100;
			MenuPlaySingleplayerUI.itemButton.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.itemButton.sizeOffset_X = 250;
			MenuPlaySingleplayerUI.itemButton.sizeOffset_Y = 100;
			MenuPlaySingleplayerUI.itemButton.foregroundTint = ESleekTint.NONE;
			SleekButton sleekButton5 = MenuPlaySingleplayerUI.itemButton;
			if (MenuPlaySingleplayerUI.<>f__mg$cache5 == null)
			{
				MenuPlaySingleplayerUI.<>f__mg$cache5 = new ClickedButton(MenuPlaySingleplayerUI.onClickedCreditsButton);
			}
			sleekButton5.onClickedButton = MenuPlaySingleplayerUI.<>f__mg$cache5;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.itemButton);
			MenuPlaySingleplayerUI.itemButton.isVisible = false;
			MenuPlaySingleplayerUI.feedbackButton = new SleekButton();
			MenuPlaySingleplayerUI.feedbackButton.positionOffset_X = 200;
			MenuPlaySingleplayerUI.feedbackButton.positionOffset_Y = 100;
			MenuPlaySingleplayerUI.feedbackButton.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.feedbackButton.sizeOffset_X = 250;
			MenuPlaySingleplayerUI.feedbackButton.sizeOffset_Y = 30;
			MenuPlaySingleplayerUI.feedbackButton.text = MenuPlaySingleplayerUI.localization.format("Feedback_Button");
			MenuPlaySingleplayerUI.feedbackButton.tooltip = MenuPlaySingleplayerUI.localization.format("Feedback_Button_Tooltip");
			SleekButton sleekButton6 = MenuPlaySingleplayerUI.feedbackButton;
			if (MenuPlaySingleplayerUI.<>f__mg$cache6 == null)
			{
				MenuPlaySingleplayerUI.<>f__mg$cache6 = new ClickedButton(MenuPlaySingleplayerUI.onClickedFeedbackButton);
			}
			sleekButton6.onClickedButton = MenuPlaySingleplayerUI.<>f__mg$cache6;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.feedbackButton);
			MenuPlaySingleplayerUI.feedbackButton.isVisible = false;
			MenuPlaySingleplayerUI.playButton = new SleekButtonIcon((Texture2D)bundle.load("Play"));
			MenuPlaySingleplayerUI.playButton.positionOffset_X = -450;
			MenuPlaySingleplayerUI.playButton.positionOffset_Y = 290;
			MenuPlaySingleplayerUI.playButton.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.playButton.sizeOffset_X = 200;
			MenuPlaySingleplayerUI.playButton.sizeOffset_Y = 30;
			MenuPlaySingleplayerUI.playButton.text = MenuPlaySingleplayerUI.localization.format("Play_Button");
			MenuPlaySingleplayerUI.playButton.tooltip = MenuPlaySingleplayerUI.localization.format("Play_Button_Tooltip");
			MenuPlaySingleplayerUI.playButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton7 = MenuPlaySingleplayerUI.playButton;
			if (MenuPlaySingleplayerUI.<>f__mg$cache7 == null)
			{
				MenuPlaySingleplayerUI.<>f__mg$cache7 = new ClickedButton(MenuPlaySingleplayerUI.onClickedPlayButton);
			}
			sleekButton7.onClickedButton = MenuPlaySingleplayerUI.<>f__mg$cache7;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.playButton);
			MenuPlaySingleplayerUI.modeButtonState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuPlaySingleplayerUI.localization.format("Easy_Button"), (Texture)bundle.load("Easy")),
				new GUIContent(MenuPlaySingleplayerUI.localization.format("Normal_Button"), (Texture)bundle.load("Normal")),
				new GUIContent(MenuPlaySingleplayerUI.localization.format("Hard_Button"), (Texture)bundle.load("Hard"))
			});
			MenuPlaySingleplayerUI.modeButtonState.positionOffset_X = -450;
			MenuPlaySingleplayerUI.modeButtonState.positionOffset_Y = 330;
			MenuPlaySingleplayerUI.modeButtonState.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.modeButtonState.sizeOffset_X = 200;
			MenuPlaySingleplayerUI.modeButtonState.sizeOffset_Y = 30;
			MenuPlaySingleplayerUI.modeButtonState.state = (int)PlaySettings.singleplayerMode;
			SleekButtonState sleekButtonState = MenuPlaySingleplayerUI.modeButtonState;
			if (MenuPlaySingleplayerUI.<>f__mg$cache8 == null)
			{
				MenuPlaySingleplayerUI.<>f__mg$cache8 = new SwappedState(MenuPlaySingleplayerUI.onSwappedModeState);
			}
			sleekButtonState.onSwappedState = MenuPlaySingleplayerUI.<>f__mg$cache8;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.modeButtonState);
			MenuPlaySingleplayerUI.configButton = new SleekButton();
			MenuPlaySingleplayerUI.configButton.positionOffset_X = -450;
			MenuPlaySingleplayerUI.configButton.positionOffset_Y = 370;
			MenuPlaySingleplayerUI.configButton.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.configButton.sizeOffset_X = 200;
			MenuPlaySingleplayerUI.configButton.sizeOffset_Y = 30;
			MenuPlaySingleplayerUI.configButton.text = MenuPlaySingleplayerUI.localization.format("Config_Button");
			MenuPlaySingleplayerUI.configButton.tooltip = MenuPlaySingleplayerUI.localization.format("Config_Button_Tooltip");
			SleekButton sleekButton8 = MenuPlaySingleplayerUI.configButton;
			if (MenuPlaySingleplayerUI.<>f__mg$cache9 == null)
			{
				MenuPlaySingleplayerUI.<>f__mg$cache9 = new ClickedButton(MenuPlaySingleplayerUI.onClickedConfigButton);
			}
			sleekButton8.onClickedButton = MenuPlaySingleplayerUI.<>f__mg$cache9;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.configButton);
			MenuPlaySingleplayerUI.cheatsToggle = new SleekToggle();
			MenuPlaySingleplayerUI.cheatsToggle.positionOffset_X = -450;
			MenuPlaySingleplayerUI.cheatsToggle.positionOffset_Y = 410;
			MenuPlaySingleplayerUI.cheatsToggle.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.cheatsToggle.sizeOffset_X = 40;
			MenuPlaySingleplayerUI.cheatsToggle.sizeOffset_Y = 40;
			MenuPlaySingleplayerUI.cheatsToggle.addLabel(MenuPlaySingleplayerUI.localization.format("Cheats_Label"), ESleekSide.RIGHT);
			MenuPlaySingleplayerUI.cheatsToggle.state = PlaySettings.singleplayerCheats;
			SleekToggle sleekToggle = MenuPlaySingleplayerUI.cheatsToggle;
			if (MenuPlaySingleplayerUI.<>f__mg$cacheA == null)
			{
				MenuPlaySingleplayerUI.<>f__mg$cacheA = new Toggled(MenuPlaySingleplayerUI.onToggledCheatsToggle);
			}
			sleekToggle.onToggled = MenuPlaySingleplayerUI.<>f__mg$cacheA;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.cheatsToggle);
			MenuPlaySingleplayerUI.resetButton = new SleekButtonIconConfirm(null, MenuPlaySingleplayerUI.localization.format("Reset_Button_Confirm"), MenuPlaySingleplayerUI.localization.format("Reset_Button_Confirm_Tooltip"), MenuPlaySingleplayerUI.localization.format("Reset_Button_Deny"), MenuPlaySingleplayerUI.localization.format("Reset_Button_Deny_Tooltip"));
			MenuPlaySingleplayerUI.resetButton.positionOffset_X = -450;
			MenuPlaySingleplayerUI.resetButton.positionOffset_Y = 470;
			MenuPlaySingleplayerUI.resetButton.positionScale_X = 0.5f;
			MenuPlaySingleplayerUI.resetButton.sizeOffset_X = 200;
			MenuPlaySingleplayerUI.resetButton.sizeOffset_Y = 30;
			MenuPlaySingleplayerUI.resetButton.text = MenuPlaySingleplayerUI.localization.format("Reset_Button");
			MenuPlaySingleplayerUI.resetButton.tooltip = MenuPlaySingleplayerUI.localization.format("Reset_Button_Tooltip");
			SleekButtonIconConfirm sleekButtonIconConfirm = MenuPlaySingleplayerUI.resetButton;
			if (MenuPlaySingleplayerUI.<>f__mg$cacheB == null)
			{
				MenuPlaySingleplayerUI.<>f__mg$cacheB = new Confirm(MenuPlaySingleplayerUI.onClickedResetButton);
			}
			sleekButtonIconConfirm.onConfirmed = MenuPlaySingleplayerUI.<>f__mg$cacheB;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.resetButton);
			bundle.unload();
			MenuPlaySingleplayerUI.refreshLevels();
			Delegate onLevelsRefreshed = Level.onLevelsRefreshed;
			if (MenuPlaySingleplayerUI.<>f__mg$cacheC == null)
			{
				MenuPlaySingleplayerUI.<>f__mg$cacheC = new LevelsRefreshed(MenuPlaySingleplayerUI.onLevelsRefreshed);
			}
			Level.onLevelsRefreshed = (LevelsRefreshed)Delegate.Combine(onLevelsRefreshed, MenuPlaySingleplayerUI.<>f__mg$cacheC);
			MenuPlaySingleplayerUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuPlaySingleplayerUI.backButton.positionOffset_Y = -50;
			MenuPlaySingleplayerUI.backButton.positionScale_Y = 1f;
			MenuPlaySingleplayerUI.backButton.sizeOffset_X = 200;
			MenuPlaySingleplayerUI.backButton.sizeOffset_Y = 50;
			MenuPlaySingleplayerUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuPlaySingleplayerUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton9 = MenuPlaySingleplayerUI.backButton;
			if (MenuPlaySingleplayerUI.<>f__mg$cacheD == null)
			{
				MenuPlaySingleplayerUI.<>f__mg$cacheD = new ClickedButton(MenuPlaySingleplayerUI.onClickedBackButton);
			}
			sleekButton9.onClickedButton = MenuPlaySingleplayerUI.<>f__mg$cacheD;
			MenuPlaySingleplayerUI.backButton.fontSize = 14;
			MenuPlaySingleplayerUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuPlaySingleplayerUI.container.add(MenuPlaySingleplayerUI.backButton);
			new MenuPlayConfigUI();
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x0017C710 File Offset: 0x0017AB10
		public static void open()
		{
			if (MenuPlaySingleplayerUI.active)
			{
				return;
			}
			MenuPlaySingleplayerUI.active = true;
			MenuPlaySingleplayerUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x0017C73D File Offset: 0x0017AB3D
		public static void close()
		{
			if (!MenuPlaySingleplayerUI.active)
			{
				return;
			}
			MenuPlaySingleplayerUI.active = false;
			MenuSettings.save();
			MenuPlaySingleplayerUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x0017C770 File Offset: 0x0017AB70
		private static void updateSelection()
		{
			if (PlaySettings.singleplayerMap == null || PlaySettings.singleplayerMap.Length == 0)
			{
				return;
			}
			LevelInfo level = Level.getLevel(PlaySettings.singleplayerMap);
			if (level == null)
			{
				return;
			}
			Local local = Localization.tryRead(level.path, false);
			if (local != null)
			{
				MenuPlaySingleplayerUI.descriptionBox.text = local.format("Description");
			}
			if (local != null && local.has("Name"))
			{
				MenuPlaySingleplayerUI.selectedBox.text = local.format("Name");
			}
			else
			{
				MenuPlaySingleplayerUI.selectedBox.text = PlaySettings.singleplayerMap;
			}
			if (MenuPlaySingleplayerUI.previewImage.texture != null && MenuPlaySingleplayerUI.previewImage.shouldDestroyTexture)
			{
				UnityEngine.Object.Destroy(MenuPlaySingleplayerUI.previewImage.texture);
				MenuPlaySingleplayerUI.previewImage.texture = null;
			}
			string path = level.path + "/Preview.png";
			if (!ReadWrite.fileExists(path, false, false))
			{
				path = level.path + "/Level.png";
			}
			if (ReadWrite.fileExists(path, false, false))
			{
				byte[] data = ReadWrite.readBytes(path, false, false);
				Texture2D texture2D = new Texture2D(320, 180, TextureFormat.ARGB32, false, true);
				texture2D.name = "Preview_" + PlaySettings.singleplayerMap + "_Selected_Icon";
				texture2D.filterMode = FilterMode.Trilinear;
				texture2D.hideFlags = HideFlags.HideAndDontSave;
				texture2D.LoadImage(data);
				MenuPlaySingleplayerUI.previewImage.texture = texture2D;
			}
			int num = MenuPlaySingleplayerUI.creditsBox.positionOffset_Y;
			if (level.configData.Creators.Length > 0 || level.configData.Collaborators.Length > 0 || level.configData.Thanks.Length > 0)
			{
				int num2 = 0;
				string text = string.Empty;
				if (level.configData.Creators.Length > 0)
				{
					text += MenuPlaySingleplayerUI.localization.format("Creators");
					num2 += 15;
					for (int i = 0; i < level.configData.Creators.Length; i++)
					{
						text = text + "\n" + level.configData.Creators[i];
						num2 += 15;
					}
				}
				if (level.configData.Collaborators.Length > 0)
				{
					if (text.Length > 0)
					{
						text += "\n\n";
						num2 += 30;
					}
					text += MenuPlaySingleplayerUI.localization.format("Collaborators");
					num2 += 15;
					for (int j = 0; j < level.configData.Collaborators.Length; j++)
					{
						text = text + "\n" + level.configData.Collaborators[j];
						num2 += 15;
					}
				}
				if (level.configData.Thanks.Length > 0)
				{
					if (text.Length > 0)
					{
						text += "\n\n";
						num2 += 30;
					}
					text += MenuPlaySingleplayerUI.localization.format("Thanks");
					num2 += 15;
					for (int k = 0; k < level.configData.Thanks.Length; k++)
					{
						text = text + "\n" + level.configData.Thanks[k];
						num2 += 15;
					}
				}
				num2 = Mathf.Max(num2, 40);
				MenuPlaySingleplayerUI.creditsBox.sizeOffset_Y = num2;
				MenuPlaySingleplayerUI.creditsBox.text = text;
				MenuPlaySingleplayerUI.creditsBox.isVisible = true;
				num += num2 + 10;
			}
			else
			{
				MenuPlaySingleplayerUI.creditsBox.isVisible = false;
			}
			if (level.configData.Item != 0)
			{
				MenuPlaySingleplayerUI.itemButton.positionOffset_Y = num;
				MenuPlaySingleplayerUI.itemButton.text = MenuPlaySingleplayerUI.localization.format("Credits_Text", new object[]
				{
					MenuPlaySingleplayerUI.selectedBox.text,
					string.Concat(new string[]
					{
						"<color=",
						Palette.hex(Provider.provider.economyService.getInventoryColor(level.configData.Item)),
						">",
						Provider.provider.economyService.getInventoryName(level.configData.Item),
						"</color>"
					})
				});
				MenuPlaySingleplayerUI.itemButton.tooltip = MenuPlaySingleplayerUI.localization.format("Credits_Tooltip");
				MenuPlaySingleplayerUI.itemButton.isVisible = true;
				num += MenuPlaySingleplayerUI.itemButton.sizeOffset_Y + 10;
			}
			else
			{
				MenuPlaySingleplayerUI.itemButton.isVisible = false;
			}
			if (!string.IsNullOrEmpty(level.configData.Feedback))
			{
				MenuPlaySingleplayerUI.feedbackButton.positionOffset_Y = num;
				MenuPlaySingleplayerUI.feedbackButton.isVisible = true;
				num += MenuPlaySingleplayerUI.feedbackButton.sizeOffset_Y + 10;
			}
			else
			{
				MenuPlaySingleplayerUI.feedbackButton.isVisible = false;
			}
			if (level.configData.Category == ESingleplayerMapCategory.CURATED && level.configData.CuratedMapMode == ECuratedMapMode.TIMED)
			{
				MenuPlaySingleplayerUI.timedBox.positionOffset_Y = num;
				MenuPlaySingleplayerUI.timedBox.text = MenuPlaySingleplayerUI.localization.format("Timed_Text", new object[]
				{
					MenuPlaySingleplayerUI.localization.format("Curated_Map_Timestamp", new object[]
					{
						level.configData.getCuratedMapTimestamp().ToString(MenuPlaySingleplayerUI.localization.format("Curated_Map_Timestamp_Format"))
					})
				});
				MenuPlaySingleplayerUI.timedBox.isVisible = true;
				num += MenuPlaySingleplayerUI.timedBox.sizeOffset_Y + 10;
			}
			else
			{
				MenuPlaySingleplayerUI.timedBox.isVisible = false;
			}
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x0017CD26 File Offset: 0x0017B126
		private static void onClickedLevel(SleekLevel level, byte index)
		{
			if ((int)index < MenuPlaySingleplayerUI.levels.Length && MenuPlaySingleplayerUI.levels[(int)index] != null)
			{
				PlaySettings.singleplayerMap = MenuPlaySingleplayerUI.levels[(int)index].name;
				MenuPlaySingleplayerUI.updateSelection();
			}
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x0017CD57 File Offset: 0x0017B157
		private static void onClickedPlayButton(SleekButton button)
		{
			if (PlaySettings.singleplayerMap == null || PlaySettings.singleplayerMap.Length == 0)
			{
				return;
			}
			Provider.map = PlaySettings.singleplayerMap;
			MenuSettings.save();
			Provider.singleplayer(PlaySettings.singleplayerMode, PlaySettings.singleplayerCheats);
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x0017CD91 File Offset: 0x0017B191
		private static void onClickedOfficialMapsButton(SleekButton button)
		{
			PlaySettings.singleplayerCategory = ESingleplayerMapCategory.OFFICIAL;
			MenuPlaySingleplayerUI.refreshLevels();
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x0017CD9E File Offset: 0x0017B19E
		private static void onClickedCuratedMapsButton(SleekButton button)
		{
			PlaySettings.singleplayerCategory = ESingleplayerMapCategory.CURATED;
			MenuPlaySingleplayerUI.refreshLevels();
		}

		// Token: 0x060036FD RID: 14077 RVA: 0x0017CDAB File Offset: 0x0017B1AB
		private static void onClickedWorkshopMapsButton(SleekButton button)
		{
			PlaySettings.singleplayerCategory = ESingleplayerMapCategory.WORKSHOP;
			MenuPlaySingleplayerUI.refreshLevels();
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x0017CDB8 File Offset: 0x0017B1B8
		private static void onClickedMiscMapsButton(SleekButton button)
		{
			PlaySettings.singleplayerCategory = ESingleplayerMapCategory.MISC;
			MenuPlaySingleplayerUI.refreshLevels();
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x0017CDC5 File Offset: 0x0017B1C5
		private static void onSwappedModeState(SleekButtonState button, int index)
		{
			PlaySettings.singleplayerMode = (EGameMode)index;
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x0017CDCD File Offset: 0x0017B1CD
		private static void onClickedConfigButton(SleekButton button)
		{
			if (PlaySettings.singleplayerMap == null || PlaySettings.singleplayerMap.Length == 0)
			{
				return;
			}
			MenuPlayConfigUI.open();
			MenuPlaySingleplayerUI.close();
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x0017CDF4 File Offset: 0x0017B1F4
		private static void onClickedResetButton(SleekButton button)
		{
			if (PlaySettings.singleplayerMap == null || PlaySettings.singleplayerMap.Length == 0)
			{
				return;
			}
			if (ReadWrite.folderExists(string.Concat(new object[]
			{
				"/Worlds/Singleplayer_",
				Characters.selected,
				"/Level/",
				PlaySettings.singleplayerMap
			})))
			{
				ReadWrite.deleteFolder(string.Concat(new object[]
				{
					"/Worlds/Singleplayer_",
					Characters.selected,
					"/Level/",
					PlaySettings.singleplayerMap
				}));
			}
			if (ReadWrite.folderExists(string.Concat(new object[]
			{
				"/Worlds/Singleplayer_",
				Characters.selected,
				"/Players/",
				Provider.user,
				"_",
				Characters.selected,
				"/",
				PlaySettings.singleplayerMap
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
					"/",
					PlaySettings.singleplayerMap
				}));
			}
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x0017CF4D File Offset: 0x0017B34D
		private static void onToggledCheatsToggle(SleekToggle toggle, bool state)
		{
			PlaySettings.singleplayerCheats = state;
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x0017CF58 File Offset: 0x0017B358
		private static void refreshLevels()
		{
			MenuPlaySingleplayerUI.levelScrollBox.remove();
			MenuPlaySingleplayerUI.levels = Level.getLevels(PlaySettings.singleplayerCategory);
			bool flag = false;
			MenuPlaySingleplayerUI.levelButtons = new SleekLevel[MenuPlaySingleplayerUI.levels.Length];
			for (int i = 0; i < MenuPlaySingleplayerUI.levels.Length; i++)
			{
				if (MenuPlaySingleplayerUI.levels[i] != null)
				{
					SleekLevel sleekLevel = new SleekLevel(MenuPlaySingleplayerUI.levels[i], false);
					sleekLevel.positionOffset_Y = i * 110;
					SleekLevel sleekLevel2 = sleekLevel;
					if (MenuPlaySingleplayerUI.<>f__mg$cache0 == null)
					{
						MenuPlaySingleplayerUI.<>f__mg$cache0 = new ClickedLevel(MenuPlaySingleplayerUI.onClickedLevel);
					}
					sleekLevel2.onClickedLevel = MenuPlaySingleplayerUI.<>f__mg$cache0;
					MenuPlaySingleplayerUI.levelScrollBox.add(sleekLevel);
					MenuPlaySingleplayerUI.levelButtons[i] = sleekLevel;
					if (!flag && MenuPlaySingleplayerUI.levels[i].name == PlaySettings.singleplayerMap)
					{
						flag = true;
					}
				}
			}
			if (MenuPlaySingleplayerUI.levels.Length == 0)
			{
				PlaySettings.singleplayerMap = string.Empty;
			}
			else if (!flag || PlaySettings.singleplayerMap == null || PlaySettings.singleplayerMap.Length == 0)
			{
				PlaySettings.singleplayerMap = MenuPlaySingleplayerUI.levels[0].name;
			}
			MenuPlaySingleplayerUI.updateSelection();
			MenuPlaySingleplayerUI.levelScrollBox.area = new Rect(0f, 0f, 5f, (float)(MenuPlaySingleplayerUI.levels.Length * 110 - 10));
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x0017D0A2 File Offset: 0x0017B4A2
		private static void onLevelsRefreshed()
		{
			MenuPlaySingleplayerUI.refreshLevels();
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x0017D0AC File Offset: 0x0017B4AC
		private static void onClickedCreditsButton(SleekButton button)
		{
			if (PlaySettings.singleplayerMap == null || PlaySettings.singleplayerMap.Length == 0)
			{
				return;
			}
			LevelInfo level = Level.getLevel(PlaySettings.singleplayerMap);
			if (level == null)
			{
				return;
			}
			if (level.configData.Item == 0)
			{
				return;
			}
			if (!Provider.provider.storeService.canOpenStore)
			{
				MenuUI.alert(MenuPlaySingleplayerUI.localization.format("Overlay"));
				return;
			}
			Provider.provider.storeService.open(new SteamworksEconomyItemDefinition((SteamItemDef_t)level.configData.Item));
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x0017D144 File Offset: 0x0017B544
		private static void onClickedFeedbackButton(SleekButton button)
		{
			if (PlaySettings.singleplayerMap == null || PlaySettings.singleplayerMap.Length == 0)
			{
				return;
			}
			LevelInfo level = Level.getLevel(PlaySettings.singleplayerMap);
			if (level == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(level.configData.Feedback))
			{
				return;
			}
			if (!Provider.provider.browserService.canOpenBrowser)
			{
				MenuUI.alert(MenuPlaySingleplayerUI.localization.format("Overlay"));
				return;
			}
			Provider.provider.browserService.open(level.configData.Feedback);
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x0017D1D6 File Offset: 0x0017B5D6
		private static void onClickedBackButton(SleekButton button)
		{
			MenuPlayUI.open();
			MenuPlaySingleplayerUI.close();
		}

		// Token: 0x040027E7 RID: 10215
		public static Local localization;

		// Token: 0x040027E8 RID: 10216
		private static Sleek container;

		// Token: 0x040027E9 RID: 10217
		public static bool active;

		// Token: 0x040027EA RID: 10218
		private static SleekButtonIcon backButton;

		// Token: 0x040027EB RID: 10219
		private static LevelInfo[] levels;

		// Token: 0x040027EC RID: 10220
		private static SleekBox previewBox;

		// Token: 0x040027ED RID: 10221
		private static SleekImageTexture previewImage;

		// Token: 0x040027EE RID: 10222
		private static SleekScrollBox levelScrollBox;

		// Token: 0x040027EF RID: 10223
		private static SleekLevel[] levelButtons;

		// Token: 0x040027F0 RID: 10224
		private static SleekButtonIcon playButton;

		// Token: 0x040027F1 RID: 10225
		private static SleekButtonState modeButtonState;

		// Token: 0x040027F2 RID: 10226
		private static SleekButton configButton;

		// Token: 0x040027F3 RID: 10227
		private static SleekButtonIconConfirm resetButton;

		// Token: 0x040027F4 RID: 10228
		private static SleekBox selectedBox;

		// Token: 0x040027F5 RID: 10229
		private static SleekBox descriptionBox;

		// Token: 0x040027F6 RID: 10230
		private static SleekToggle cheatsToggle;

		// Token: 0x040027F7 RID: 10231
		private static SleekBox creditsBox;

		// Token: 0x040027F8 RID: 10232
		private static SleekBox timedBox;

		// Token: 0x040027F9 RID: 10233
		private static SleekButton itemButton;

		// Token: 0x040027FA RID: 10234
		private static SleekButton feedbackButton;

		// Token: 0x040027FB RID: 10235
		private static SleekButton officalMapsButton;

		// Token: 0x040027FC RID: 10236
		private static SleekButton curatedMapsButton;

		// Token: 0x040027FD RID: 10237
		private static SleekButton workshopMapsButton;

		// Token: 0x040027FE RID: 10238
		private static SleekButton miscMapsButton;

		// Token: 0x040027FF RID: 10239
		[CompilerGenerated]
		private static ClickedLevel <>f__mg$cache0;

		// Token: 0x04002800 RID: 10240
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x04002801 RID: 10241
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x04002802 RID: 10242
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x04002803 RID: 10243
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;

		// Token: 0x04002804 RID: 10244
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;

		// Token: 0x04002805 RID: 10245
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache6;

		// Token: 0x04002806 RID: 10246
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache7;

		// Token: 0x04002807 RID: 10247
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache8;

		// Token: 0x04002808 RID: 10248
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache9;

		// Token: 0x04002809 RID: 10249
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheA;

		// Token: 0x0400280A RID: 10250
		[CompilerGenerated]
		private static Confirm <>f__mg$cacheB;

		// Token: 0x0400280B RID: 10251
		[CompilerGenerated]
		private static LevelsRefreshed <>f__mg$cacheC;

		// Token: 0x0400280C RID: 10252
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheD;
	}
}
