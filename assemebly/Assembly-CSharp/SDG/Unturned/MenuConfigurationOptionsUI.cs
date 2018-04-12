using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000767 RID: 1895
	public class MenuConfigurationOptionsUI
	{
		// Token: 0x06003626 RID: 13862 RVA: 0x0016EC84 File Offset: 0x0016D084
		public MenuConfigurationOptionsUI()
		{
			MenuConfigurationOptionsUI.localization = Localization.read("/Menu/Configuration/MenuConfigurationOptions.dat");
			MenuConfigurationOptionsUI.container = new Sleek();
			MenuConfigurationOptionsUI.container.positionOffset_X = 10;
			MenuConfigurationOptionsUI.container.positionOffset_Y = 10;
			MenuConfigurationOptionsUI.container.positionScale_Y = 1f;
			MenuConfigurationOptionsUI.container.sizeOffset_X = -20;
			MenuConfigurationOptionsUI.container.sizeOffset_Y = -20;
			MenuConfigurationOptionsUI.container.sizeScale_X = 1f;
			MenuConfigurationOptionsUI.container.sizeScale_Y = 1f;
			if (Provider.isConnected)
			{
				PlayerUI.container.add(MenuConfigurationOptionsUI.container);
			}
			else
			{
				MenuUI.container.add(MenuConfigurationOptionsUI.container);
			}
			MenuConfigurationOptionsUI.active = false;
			MenuConfigurationOptionsUI.optionsBox = new SleekScrollBox();
			MenuConfigurationOptionsUI.optionsBox.positionOffset_X = -200;
			MenuConfigurationOptionsUI.optionsBox.positionOffset_Y = 100;
			MenuConfigurationOptionsUI.optionsBox.positionScale_X = 0.5f;
			MenuConfigurationOptionsUI.optionsBox.sizeOffset_X = 430;
			MenuConfigurationOptionsUI.optionsBox.sizeOffset_Y = -200;
			MenuConfigurationOptionsUI.optionsBox.sizeScale_Y = 1f;
			MenuConfigurationOptionsUI.optionsBox.area = new Rect(0f, 0f, 5f, 2260f);
			MenuConfigurationOptionsUI.container.add(MenuConfigurationOptionsUI.optionsBox);
			MenuConfigurationOptionsUI.fovSlider = new SleekSlider();
			MenuConfigurationOptionsUI.fovSlider.positionOffset_Y = 830;
			MenuConfigurationOptionsUI.fovSlider.sizeOffset_X = 200;
			MenuConfigurationOptionsUI.fovSlider.sizeOffset_Y = 20;
			MenuConfigurationOptionsUI.fovSlider.orientation = ESleekOrientation.HORIZONTAL;
			MenuConfigurationOptionsUI.fovSlider.addLabel(MenuConfigurationOptionsUI.localization.format("FOV_Slider_Label", new object[]
			{
				(int)OptionsSettings.view
			}), ESleekSide.RIGHT);
			SleekSlider sleekSlider = MenuConfigurationOptionsUI.fovSlider;
			if (MenuConfigurationOptionsUI.<>f__mg$cache0 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache0 = new Dragged(MenuConfigurationOptionsUI.onDraggedFOVSlider);
			}
			sleekSlider.onDragged = MenuConfigurationOptionsUI.<>f__mg$cache0;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.fovSlider);
			MenuConfigurationOptionsUI.volumeSlider = new SleekSlider();
			MenuConfigurationOptionsUI.volumeSlider.positionOffset_Y = 860;
			MenuConfigurationOptionsUI.volumeSlider.sizeOffset_X = 200;
			MenuConfigurationOptionsUI.volumeSlider.sizeOffset_Y = 20;
			MenuConfigurationOptionsUI.volumeSlider.orientation = ESleekOrientation.HORIZONTAL;
			MenuConfigurationOptionsUI.volumeSlider.addLabel(MenuConfigurationOptionsUI.localization.format("Volume_Slider_Label", new object[]
			{
				(int)(OptionsSettings.volume * 100f)
			}), ESleekSide.RIGHT);
			SleekSlider sleekSlider2 = MenuConfigurationOptionsUI.volumeSlider;
			if (MenuConfigurationOptionsUI.<>f__mg$cache1 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache1 = new Dragged(MenuConfigurationOptionsUI.onDraggedVolumeSlider);
			}
			sleekSlider2.onDragged = MenuConfigurationOptionsUI.<>f__mg$cache1;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.volumeSlider);
			MenuConfigurationOptionsUI.voiceSlider = new SleekSlider();
			MenuConfigurationOptionsUI.voiceSlider.positionOffset_Y = 890;
			MenuConfigurationOptionsUI.voiceSlider.sizeOffset_X = 200;
			MenuConfigurationOptionsUI.voiceSlider.sizeOffset_Y = 20;
			MenuConfigurationOptionsUI.voiceSlider.orientation = ESleekOrientation.HORIZONTAL;
			MenuConfigurationOptionsUI.voiceSlider.addLabel(MenuConfigurationOptionsUI.localization.format("Voice_Slider_Label", new object[]
			{
				(int)(OptionsSettings.voice * 100f)
			}), ESleekSide.RIGHT);
			SleekSlider sleekSlider3 = MenuConfigurationOptionsUI.voiceSlider;
			if (MenuConfigurationOptionsUI.<>f__mg$cache2 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache2 = new Dragged(MenuConfigurationOptionsUI.onDraggedVoiceSlider);
			}
			sleekSlider3.onDragged = MenuConfigurationOptionsUI.<>f__mg$cache2;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.voiceSlider);
			MenuConfigurationOptionsUI.debugToggle = new SleekToggle();
			MenuConfigurationOptionsUI.debugToggle.sizeOffset_X = 40;
			MenuConfigurationOptionsUI.debugToggle.sizeOffset_Y = 40;
			MenuConfigurationOptionsUI.debugToggle.addLabel(MenuConfigurationOptionsUI.localization.format("Debug_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle = MenuConfigurationOptionsUI.debugToggle;
			if (MenuConfigurationOptionsUI.<>f__mg$cache3 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache3 = new Toggled(MenuConfigurationOptionsUI.onToggledDebugToggle);
			}
			sleekToggle.onToggled = MenuConfigurationOptionsUI.<>f__mg$cache3;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.debugToggle);
			MenuConfigurationOptionsUI.musicToggle = new SleekToggle();
			MenuConfigurationOptionsUI.musicToggle.positionOffset_Y = 50;
			MenuConfigurationOptionsUI.musicToggle.sizeOffset_X = 40;
			MenuConfigurationOptionsUI.musicToggle.sizeOffset_Y = 40;
			MenuConfigurationOptionsUI.musicToggle.addLabel(MenuConfigurationOptionsUI.localization.format("Music_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle2 = MenuConfigurationOptionsUI.musicToggle;
			if (MenuConfigurationOptionsUI.<>f__mg$cache4 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache4 = new Toggled(MenuConfigurationOptionsUI.onToggledMusicToggle);
			}
			sleekToggle2.onToggled = MenuConfigurationOptionsUI.<>f__mg$cache4;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.musicToggle);
			MenuConfigurationOptionsUI.splashscreenToggle = new SleekToggle();
			MenuConfigurationOptionsUI.splashscreenToggle.positionOffset_Y = 100;
			MenuConfigurationOptionsUI.splashscreenToggle.sizeOffset_X = 40;
			MenuConfigurationOptionsUI.splashscreenToggle.sizeOffset_Y = 40;
			MenuConfigurationOptionsUI.splashscreenToggle.addLabel(MenuConfigurationOptionsUI.localization.format("Splashscreen_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle3 = MenuConfigurationOptionsUI.splashscreenToggle;
			if (MenuConfigurationOptionsUI.<>f__mg$cache5 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache5 = new Toggled(MenuConfigurationOptionsUI.onToggledSplashscreenToggle);
			}
			sleekToggle3.onToggled = MenuConfigurationOptionsUI.<>f__mg$cache5;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.splashscreenToggle);
			MenuConfigurationOptionsUI.timerToggle = new SleekToggle();
			MenuConfigurationOptionsUI.timerToggle.positionOffset_Y = 150;
			MenuConfigurationOptionsUI.timerToggle.sizeOffset_X = 40;
			MenuConfigurationOptionsUI.timerToggle.sizeOffset_Y = 40;
			MenuConfigurationOptionsUI.timerToggle.addLabel(MenuConfigurationOptionsUI.localization.format("Timer_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle4 = MenuConfigurationOptionsUI.timerToggle;
			if (MenuConfigurationOptionsUI.<>f__mg$cache6 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache6 = new Toggled(MenuConfigurationOptionsUI.onToggledTimerToggle);
			}
			sleekToggle4.onToggled = MenuConfigurationOptionsUI.<>f__mg$cache6;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.timerToggle);
			MenuConfigurationOptionsUI.goreToggle = new SleekToggle();
			MenuConfigurationOptionsUI.goreToggle.positionOffset_Y = 200;
			MenuConfigurationOptionsUI.goreToggle.sizeOffset_X = 40;
			MenuConfigurationOptionsUI.goreToggle.sizeOffset_Y = 40;
			MenuConfigurationOptionsUI.goreToggle.addLabel(MenuConfigurationOptionsUI.localization.format("Gore_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle5 = MenuConfigurationOptionsUI.goreToggle;
			if (MenuConfigurationOptionsUI.<>f__mg$cache7 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache7 = new Toggled(MenuConfigurationOptionsUI.onToggledGoreToggle);
			}
			sleekToggle5.onToggled = MenuConfigurationOptionsUI.<>f__mg$cache7;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.goreToggle);
			MenuConfigurationOptionsUI.filterToggle = new SleekToggle();
			MenuConfigurationOptionsUI.filterToggle.positionOffset_Y = 250;
			MenuConfigurationOptionsUI.filterToggle.sizeOffset_X = 40;
			MenuConfigurationOptionsUI.filterToggle.sizeOffset_Y = 40;
			MenuConfigurationOptionsUI.filterToggle.addLabel(MenuConfigurationOptionsUI.localization.format("Filter_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle6 = MenuConfigurationOptionsUI.filterToggle;
			if (MenuConfigurationOptionsUI.<>f__mg$cache8 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache8 = new Toggled(MenuConfigurationOptionsUI.onToggledFilterToggle);
			}
			sleekToggle6.onToggled = MenuConfigurationOptionsUI.<>f__mg$cache8;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.filterToggle);
			MenuConfigurationOptionsUI.chatTextToggle = new SleekToggle();
			MenuConfigurationOptionsUI.chatTextToggle.positionOffset_Y = 300;
			MenuConfigurationOptionsUI.chatTextToggle.sizeOffset_X = 40;
			MenuConfigurationOptionsUI.chatTextToggle.sizeOffset_Y = 40;
			MenuConfigurationOptionsUI.chatTextToggle.addLabel(MenuConfigurationOptionsUI.localization.format("Chat_Text_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle7 = MenuConfigurationOptionsUI.chatTextToggle;
			if (MenuConfigurationOptionsUI.<>f__mg$cache9 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache9 = new Toggled(MenuConfigurationOptionsUI.onToggledChatTextToggle);
			}
			sleekToggle7.onToggled = MenuConfigurationOptionsUI.<>f__mg$cache9;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.chatTextToggle);
			MenuConfigurationOptionsUI.chatVoiceInToggle = new SleekToggle();
			MenuConfigurationOptionsUI.chatVoiceInToggle.positionOffset_Y = 350;
			MenuConfigurationOptionsUI.chatVoiceInToggle.sizeOffset_X = 40;
			MenuConfigurationOptionsUI.chatVoiceInToggle.sizeOffset_Y = 40;
			MenuConfigurationOptionsUI.chatVoiceInToggle.addLabel(MenuConfigurationOptionsUI.localization.format("Chat_Voice_In_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle8 = MenuConfigurationOptionsUI.chatVoiceInToggle;
			if (MenuConfigurationOptionsUI.<>f__mg$cacheA == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cacheA = new Toggled(MenuConfigurationOptionsUI.onToggledChatVoiceInToggle);
			}
			sleekToggle8.onToggled = MenuConfigurationOptionsUI.<>f__mg$cacheA;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.chatVoiceInToggle);
			MenuConfigurationOptionsUI.chatVoiceOutToggle = new SleekToggle();
			MenuConfigurationOptionsUI.chatVoiceOutToggle.positionOffset_Y = 400;
			MenuConfigurationOptionsUI.chatVoiceOutToggle.sizeOffset_X = 40;
			MenuConfigurationOptionsUI.chatVoiceOutToggle.sizeOffset_Y = 40;
			MenuConfigurationOptionsUI.chatVoiceOutToggle.addLabel(MenuConfigurationOptionsUI.localization.format("Chat_Voice_Out_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle9 = MenuConfigurationOptionsUI.chatVoiceOutToggle;
			if (MenuConfigurationOptionsUI.<>f__mg$cacheB == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cacheB = new Toggled(MenuConfigurationOptionsUI.onToggledChatVoiceOutToggle);
			}
			sleekToggle9.onToggled = MenuConfigurationOptionsUI.<>f__mg$cacheB;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.chatVoiceOutToggle);
			MenuConfigurationOptionsUI.hintsToggle = new SleekToggle();
			MenuConfigurationOptionsUI.hintsToggle.positionOffset_Y = 450;
			MenuConfigurationOptionsUI.hintsToggle.sizeOffset_X = 40;
			MenuConfigurationOptionsUI.hintsToggle.sizeOffset_Y = 40;
			MenuConfigurationOptionsUI.hintsToggle.addLabel(MenuConfigurationOptionsUI.localization.format("Hints_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle10 = MenuConfigurationOptionsUI.hintsToggle;
			if (MenuConfigurationOptionsUI.<>f__mg$cacheC == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cacheC = new Toggled(MenuConfigurationOptionsUI.onToggledHintsToggle);
			}
			sleekToggle10.onToggled = MenuConfigurationOptionsUI.<>f__mg$cacheC;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.hintsToggle);
			MenuConfigurationOptionsUI.ambienceToggle = new SleekToggle();
			MenuConfigurationOptionsUI.ambienceToggle.positionOffset_Y = 500;
			MenuConfigurationOptionsUI.ambienceToggle.sizeOffset_X = 40;
			MenuConfigurationOptionsUI.ambienceToggle.sizeOffset_Y = 40;
			MenuConfigurationOptionsUI.ambienceToggle.addLabel(MenuConfigurationOptionsUI.localization.format("Ambience_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle11 = MenuConfigurationOptionsUI.ambienceToggle;
			if (MenuConfigurationOptionsUI.<>f__mg$cacheD == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cacheD = new Toggled(MenuConfigurationOptionsUI.onToggledAmbienceToggle);
			}
			sleekToggle11.onToggled = MenuConfigurationOptionsUI.<>f__mg$cacheD;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.ambienceToggle);
			MenuConfigurationOptionsUI.streamerToggle = new SleekToggle();
			MenuConfigurationOptionsUI.streamerToggle.positionOffset_Y = 550;
			MenuConfigurationOptionsUI.streamerToggle.sizeOffset_X = 40;
			MenuConfigurationOptionsUI.streamerToggle.sizeOffset_Y = 40;
			MenuConfigurationOptionsUI.streamerToggle.addLabel(MenuConfigurationOptionsUI.localization.format("Streamer_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle12 = MenuConfigurationOptionsUI.streamerToggle;
			if (MenuConfigurationOptionsUI.<>f__mg$cacheE == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cacheE = new Toggled(MenuConfigurationOptionsUI.onToggledStreamerToggle);
			}
			sleekToggle12.onToggled = MenuConfigurationOptionsUI.<>f__mg$cacheE;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.streamerToggle);
			MenuConfigurationOptionsUI.featuredWorkshopToggle = new SleekToggle();
			MenuConfigurationOptionsUI.featuredWorkshopToggle.positionOffset_Y = 600;
			MenuConfigurationOptionsUI.featuredWorkshopToggle.sizeOffset_X = 40;
			MenuConfigurationOptionsUI.featuredWorkshopToggle.sizeOffset_Y = 40;
			MenuConfigurationOptionsUI.featuredWorkshopToggle.addLabel(MenuConfigurationOptionsUI.localization.format("Featured_Workshop_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle13 = MenuConfigurationOptionsUI.featuredWorkshopToggle;
			if (MenuConfigurationOptionsUI.<>f__mg$cacheF == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cacheF = new Toggled(MenuConfigurationOptionsUI.onToggledFeaturedWorkshopToggle);
			}
			sleekToggle13.onToggled = MenuConfigurationOptionsUI.<>f__mg$cacheF;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.featuredWorkshopToggle);
			MenuConfigurationOptionsUI.showHotbarToggle = new SleekToggle();
			MenuConfigurationOptionsUI.showHotbarToggle.positionOffset_Y = 650;
			MenuConfigurationOptionsUI.showHotbarToggle.sizeOffset_X = 40;
			MenuConfigurationOptionsUI.showHotbarToggle.sizeOffset_Y = 40;
			MenuConfigurationOptionsUI.showHotbarToggle.addLabel(MenuConfigurationOptionsUI.localization.format("Show_Hotbar_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle14 = MenuConfigurationOptionsUI.showHotbarToggle;
			if (MenuConfigurationOptionsUI.<>f__mg$cache10 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache10 = new Toggled(MenuConfigurationOptionsUI.onToggledShowHotbarToggle);
			}
			sleekToggle14.onToggled = MenuConfigurationOptionsUI.<>f__mg$cache10;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.showHotbarToggle);
			MenuConfigurationOptionsUI.matchmakingShowAllMapsToggle = new SleekToggle();
			MenuConfigurationOptionsUI.matchmakingShowAllMapsToggle.positionOffset_Y = 700;
			MenuConfigurationOptionsUI.matchmakingShowAllMapsToggle.sizeOffset_X = 40;
			MenuConfigurationOptionsUI.matchmakingShowAllMapsToggle.sizeOffset_Y = 40;
			MenuConfigurationOptionsUI.matchmakingShowAllMapsToggle.addLabel(MenuConfigurationOptionsUI.localization.format("Matchmaking_Show_All_Maps_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle15 = MenuConfigurationOptionsUI.matchmakingShowAllMapsToggle;
			if (MenuConfigurationOptionsUI.<>f__mg$cache11 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache11 = new Toggled(MenuConfigurationOptionsUI.onToggledMatchmakingShowAllMapsToggle);
			}
			sleekToggle15.onToggled = MenuConfigurationOptionsUI.<>f__mg$cache11;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.matchmakingShowAllMapsToggle);
			MenuConfigurationOptionsUI.minMatchmakingPlayersField = new SleekInt32Field();
			MenuConfigurationOptionsUI.minMatchmakingPlayersField.positionOffset_Y = 750;
			MenuConfigurationOptionsUI.minMatchmakingPlayersField.sizeOffset_X = 200;
			MenuConfigurationOptionsUI.minMatchmakingPlayersField.sizeOffset_Y = 30;
			MenuConfigurationOptionsUI.minMatchmakingPlayersField.addLabel(MenuConfigurationOptionsUI.localization.format("Min_Matchmaking_Players_Field_Label"), ESleekSide.RIGHT);
			SleekInt32Field sleekInt32Field = MenuConfigurationOptionsUI.minMatchmakingPlayersField;
			if (MenuConfigurationOptionsUI.<>f__mg$cache12 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache12 = new TypedInt32(MenuConfigurationOptionsUI.onTypedMinMatchmakingPlayersField);
			}
			sleekInt32Field.onTypedInt = MenuConfigurationOptionsUI.<>f__mg$cache12;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.minMatchmakingPlayersField);
			MenuConfigurationOptionsUI.maxMatchmakingPingField = new SleekInt32Field();
			MenuConfigurationOptionsUI.maxMatchmakingPingField.positionOffset_Y = 790;
			MenuConfigurationOptionsUI.maxMatchmakingPingField.sizeOffset_X = 200;
			MenuConfigurationOptionsUI.maxMatchmakingPingField.sizeOffset_Y = 30;
			MenuConfigurationOptionsUI.maxMatchmakingPingField.addLabel(MenuConfigurationOptionsUI.localization.format("Max_Matchmaking_Ping_Field_Label"), ESleekSide.RIGHT);
			SleekInt32Field sleekInt32Field2 = MenuConfigurationOptionsUI.maxMatchmakingPingField;
			if (MenuConfigurationOptionsUI.<>f__mg$cache13 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache13 = new TypedInt32(MenuConfigurationOptionsUI.onTypedMaxMatchmakingPingField);
			}
			sleekInt32Field2.onTypedInt = MenuConfigurationOptionsUI.<>f__mg$cache13;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.maxMatchmakingPingField);
			MenuConfigurationOptionsUI.talkButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationOptionsUI.localization.format("Talk_Off")),
				new GUIContent(MenuConfigurationOptionsUI.localization.format("Talk_On"))
			});
			MenuConfigurationOptionsUI.talkButton.positionOffset_Y = 920;
			MenuConfigurationOptionsUI.talkButton.sizeOffset_X = 200;
			MenuConfigurationOptionsUI.talkButton.sizeOffset_Y = 30;
			MenuConfigurationOptionsUI.talkButton.state = ((!OptionsSettings.talk) ? 0 : 1);
			MenuConfigurationOptionsUI.talkButton.tooltip = MenuConfigurationOptionsUI.localization.format("Talk_Tooltip");
			SleekButtonState sleekButtonState = MenuConfigurationOptionsUI.talkButton;
			if (MenuConfigurationOptionsUI.<>f__mg$cache14 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache14 = new SwappedState(MenuConfigurationOptionsUI.onSwappedTalkState);
			}
			sleekButtonState.onSwappedState = MenuConfigurationOptionsUI.<>f__mg$cache14;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.talkButton);
			MenuConfigurationOptionsUI.metricButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationOptionsUI.localization.format("Metric_Off")),
				new GUIContent(MenuConfigurationOptionsUI.localization.format("Metric_On"))
			});
			MenuConfigurationOptionsUI.metricButton.positionOffset_Y = 960;
			MenuConfigurationOptionsUI.metricButton.sizeOffset_X = 200;
			MenuConfigurationOptionsUI.metricButton.sizeOffset_Y = 30;
			MenuConfigurationOptionsUI.metricButton.state = ((!OptionsSettings.metric) ? 0 : 1);
			MenuConfigurationOptionsUI.metricButton.tooltip = MenuConfigurationOptionsUI.localization.format("Metric_Tooltip");
			SleekButtonState sleekButtonState2 = MenuConfigurationOptionsUI.metricButton;
			if (MenuConfigurationOptionsUI.<>f__mg$cache15 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache15 = new SwappedState(MenuConfigurationOptionsUI.onSwappedMetricState);
			}
			sleekButtonState2.onSwappedState = MenuConfigurationOptionsUI.<>f__mg$cache15;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.metricButton);
			MenuConfigurationOptionsUI.uiButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationOptionsUI.localization.format("UI_Free")),
				new GUIContent(MenuConfigurationOptionsUI.localization.format("UI_Pro"))
			});
			MenuConfigurationOptionsUI.uiButton.positionOffset_Y = 1000;
			MenuConfigurationOptionsUI.uiButton.sizeOffset_X = 200;
			MenuConfigurationOptionsUI.uiButton.sizeOffset_Y = 30;
			MenuConfigurationOptionsUI.uiButton.tooltip = MenuConfigurationOptionsUI.localization.format("UI_Tooltip");
			SleekButtonState sleekButtonState3 = MenuConfigurationOptionsUI.uiButton;
			if (MenuConfigurationOptionsUI.<>f__mg$cache16 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache16 = new SwappedState(MenuConfigurationOptionsUI.onSwappedUIState);
			}
			sleekButtonState3.onSwappedState = MenuConfigurationOptionsUI.<>f__mg$cache16;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.uiButton);
			MenuConfigurationOptionsUI.hitmarkerButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationOptionsUI.localization.format("Hitmarker_Static")),
				new GUIContent(MenuConfigurationOptionsUI.localization.format("Hitmarker_Dynamic"))
			});
			MenuConfigurationOptionsUI.hitmarkerButton.positionOffset_Y = 1040;
			MenuConfigurationOptionsUI.hitmarkerButton.sizeOffset_X = 200;
			MenuConfigurationOptionsUI.hitmarkerButton.sizeOffset_Y = 30;
			MenuConfigurationOptionsUI.hitmarkerButton.tooltip = MenuConfigurationOptionsUI.localization.format("Hitmarker_Tooltip");
			SleekButtonState sleekButtonState4 = MenuConfigurationOptionsUI.hitmarkerButton;
			if (MenuConfigurationOptionsUI.<>f__mg$cache17 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache17 = new SwappedState(MenuConfigurationOptionsUI.onSwappedHitmarkerState);
			}
			sleekButtonState4.onSwappedState = MenuConfigurationOptionsUI.<>f__mg$cache17;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.hitmarkerButton);
			MenuConfigurationOptionsUI.crosshairBox = new SleekBox();
			MenuConfigurationOptionsUI.crosshairBox.positionOffset_Y = 1080;
			MenuConfigurationOptionsUI.crosshairBox.sizeOffset_X = 240;
			MenuConfigurationOptionsUI.crosshairBox.sizeOffset_Y = 30;
			MenuConfigurationOptionsUI.crosshairBox.text = MenuConfigurationOptionsUI.localization.format("Crosshair_Box");
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.crosshairBox);
			MenuConfigurationOptionsUI.crosshairColorPicker = new SleekColorPicker();
			MenuConfigurationOptionsUI.crosshairColorPicker.positionOffset_Y = 1120;
			SleekColorPicker sleekColorPicker = MenuConfigurationOptionsUI.crosshairColorPicker;
			if (MenuConfigurationOptionsUI.<>f__mg$cache18 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache18 = new ColorPicked(MenuConfigurationOptionsUI.onCrosshairColorPicked);
			}
			sleekColorPicker.onColorPicked = MenuConfigurationOptionsUI.<>f__mg$cache18;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.crosshairColorPicker);
			MenuConfigurationOptionsUI.hitmarkerBox = new SleekBox();
			MenuConfigurationOptionsUI.hitmarkerBox.positionOffset_Y = 1250;
			MenuConfigurationOptionsUI.hitmarkerBox.sizeOffset_X = 240;
			MenuConfigurationOptionsUI.hitmarkerBox.sizeOffset_Y = 30;
			MenuConfigurationOptionsUI.hitmarkerBox.text = MenuConfigurationOptionsUI.localization.format("Hitmarker_Box");
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.hitmarkerBox);
			MenuConfigurationOptionsUI.hitmarkerColorPicker = new SleekColorPicker();
			MenuConfigurationOptionsUI.hitmarkerColorPicker.positionOffset_Y = 1290;
			SleekColorPicker sleekColorPicker2 = MenuConfigurationOptionsUI.hitmarkerColorPicker;
			if (MenuConfigurationOptionsUI.<>f__mg$cache19 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache19 = new ColorPicked(MenuConfigurationOptionsUI.onHitmarkerColorPicked);
			}
			sleekColorPicker2.onColorPicked = MenuConfigurationOptionsUI.<>f__mg$cache19;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.hitmarkerColorPicker);
			MenuConfigurationOptionsUI.criticalHitmarkerBox = new SleekBox();
			MenuConfigurationOptionsUI.criticalHitmarkerBox.positionOffset_Y = 1420;
			MenuConfigurationOptionsUI.criticalHitmarkerBox.sizeOffset_X = 240;
			MenuConfigurationOptionsUI.criticalHitmarkerBox.sizeOffset_Y = 30;
			MenuConfigurationOptionsUI.criticalHitmarkerBox.text = MenuConfigurationOptionsUI.localization.format("Critical_Hitmarker_Box");
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.criticalHitmarkerBox);
			MenuConfigurationOptionsUI.criticalHitmarkerColorPicker = new SleekColorPicker();
			MenuConfigurationOptionsUI.criticalHitmarkerColorPicker.positionOffset_Y = 1460;
			SleekColorPicker sleekColorPicker3 = MenuConfigurationOptionsUI.criticalHitmarkerColorPicker;
			if (MenuConfigurationOptionsUI.<>f__mg$cache1A == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache1A = new ColorPicked(MenuConfigurationOptionsUI.onCriticalHitmarkerColorPicked);
			}
			sleekColorPicker3.onColorPicked = MenuConfigurationOptionsUI.<>f__mg$cache1A;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.criticalHitmarkerColorPicker);
			MenuConfigurationOptionsUI.cursorBox = new SleekBox();
			MenuConfigurationOptionsUI.cursorBox.positionOffset_Y = 1590;
			MenuConfigurationOptionsUI.cursorBox.sizeOffset_X = 240;
			MenuConfigurationOptionsUI.cursorBox.sizeOffset_Y = 30;
			MenuConfigurationOptionsUI.cursorBox.text = MenuConfigurationOptionsUI.localization.format("Cursor_Box");
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.cursorBox);
			MenuConfigurationOptionsUI.cursorColorPicker = new SleekColorPicker();
			MenuConfigurationOptionsUI.cursorColorPicker.positionOffset_Y = 1630;
			SleekColorPicker sleekColorPicker4 = MenuConfigurationOptionsUI.cursorColorPicker;
			if (MenuConfigurationOptionsUI.<>f__mg$cache1B == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache1B = new ColorPicked(MenuConfigurationOptionsUI.onCursorColorPicked);
			}
			sleekColorPicker4.onColorPicked = MenuConfigurationOptionsUI.<>f__mg$cache1B;
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.cursorColorPicker);
			MenuConfigurationOptionsUI.backgroundBox = new SleekBox();
			MenuConfigurationOptionsUI.backgroundBox.positionOffset_Y = 1760;
			MenuConfigurationOptionsUI.backgroundBox.sizeOffset_X = 240;
			MenuConfigurationOptionsUI.backgroundBox.sizeOffset_Y = 30;
			MenuConfigurationOptionsUI.backgroundBox.text = MenuConfigurationOptionsUI.localization.format("Background_Box");
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.backgroundBox);
			MenuConfigurationOptionsUI.backgroundColorPicker = new SleekColorPicker();
			MenuConfigurationOptionsUI.backgroundColorPicker.positionOffset_Y = 1800;
			if (Provider.isPro)
			{
				SleekColorPicker sleekColorPicker5 = MenuConfigurationOptionsUI.backgroundColorPicker;
				if (MenuConfigurationOptionsUI.<>f__mg$cache1C == null)
				{
					MenuConfigurationOptionsUI.<>f__mg$cache1C = new ColorPicked(MenuConfigurationOptionsUI.onBackgroundColorPicked);
				}
				sleekColorPicker5.onColorPicked = MenuConfigurationOptionsUI.<>f__mg$cache1C;
			}
			else
			{
				Bundle bundle = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Pro/Pro.unity3d");
				SleekImageTexture sleekImageTexture = new SleekImageTexture();
				sleekImageTexture.positionOffset_X = -40;
				sleekImageTexture.positionOffset_Y = -40;
				sleekImageTexture.positionScale_X = 0.5f;
				sleekImageTexture.positionScale_Y = 0.5f;
				sleekImageTexture.sizeOffset_X = 80;
				sleekImageTexture.sizeOffset_Y = 80;
				sleekImageTexture.texture = (Texture2D)bundle.load("Lock_Large");
				MenuConfigurationOptionsUI.backgroundColorPicker.add(sleekImageTexture);
				bundle.unload();
			}
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.backgroundColorPicker);
			MenuConfigurationOptionsUI.foregroundBox = new SleekBox();
			MenuConfigurationOptionsUI.foregroundBox.positionOffset_Y = 1930;
			MenuConfigurationOptionsUI.foregroundBox.sizeOffset_X = 240;
			MenuConfigurationOptionsUI.foregroundBox.sizeOffset_Y = 30;
			MenuConfigurationOptionsUI.foregroundBox.text = MenuConfigurationOptionsUI.localization.format("Foreground_Box");
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.foregroundBox);
			MenuConfigurationOptionsUI.foregroundColorPicker = new SleekColorPicker();
			MenuConfigurationOptionsUI.foregroundColorPicker.positionOffset_Y = 1970;
			if (Provider.isPro)
			{
				SleekColorPicker sleekColorPicker6 = MenuConfigurationOptionsUI.foregroundColorPicker;
				if (MenuConfigurationOptionsUI.<>f__mg$cache1D == null)
				{
					MenuConfigurationOptionsUI.<>f__mg$cache1D = new ColorPicked(MenuConfigurationOptionsUI.onForegroundColorPicked);
				}
				sleekColorPicker6.onColorPicked = MenuConfigurationOptionsUI.<>f__mg$cache1D;
			}
			else
			{
				Bundle bundle2 = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Pro/Pro.unity3d");
				SleekImageTexture sleekImageTexture2 = new SleekImageTexture();
				sleekImageTexture2.positionOffset_X = -40;
				sleekImageTexture2.positionOffset_Y = -40;
				sleekImageTexture2.positionScale_X = 0.5f;
				sleekImageTexture2.positionScale_Y = 0.5f;
				sleekImageTexture2.sizeOffset_X = 80;
				sleekImageTexture2.sizeOffset_Y = 80;
				sleekImageTexture2.texture = (Texture2D)bundle2.load("Lock_Large");
				MenuConfigurationOptionsUI.foregroundColorPicker.add(sleekImageTexture2);
				bundle2.unload();
			}
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.foregroundColorPicker);
			MenuConfigurationOptionsUI.fontBox = new SleekBox();
			MenuConfigurationOptionsUI.fontBox.positionOffset_Y = 2100;
			MenuConfigurationOptionsUI.fontBox.sizeOffset_X = 240;
			MenuConfigurationOptionsUI.fontBox.sizeOffset_Y = 30;
			MenuConfigurationOptionsUI.fontBox.text = MenuConfigurationOptionsUI.localization.format("Font_Box");
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.fontBox);
			MenuConfigurationOptionsUI.fontColorPicker = new SleekColorPicker();
			MenuConfigurationOptionsUI.fontColorPicker.positionOffset_Y = 2140;
			if (Provider.isPro)
			{
				SleekColorPicker sleekColorPicker7 = MenuConfigurationOptionsUI.fontColorPicker;
				if (MenuConfigurationOptionsUI.<>f__mg$cache1E == null)
				{
					MenuConfigurationOptionsUI.<>f__mg$cache1E = new ColorPicked(MenuConfigurationOptionsUI.onFontColorPicked);
				}
				sleekColorPicker7.onColorPicked = MenuConfigurationOptionsUI.<>f__mg$cache1E;
			}
			else
			{
				Bundle bundle3 = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Pro/Pro.unity3d");
				SleekImageTexture sleekImageTexture3 = new SleekImageTexture();
				sleekImageTexture3.positionOffset_X = -40;
				sleekImageTexture3.positionOffset_Y = -40;
				sleekImageTexture3.positionScale_X = 0.5f;
				sleekImageTexture3.positionScale_Y = 0.5f;
				sleekImageTexture3.sizeOffset_X = 80;
				sleekImageTexture3.sizeOffset_Y = 80;
				sleekImageTexture3.texture = (Texture2D)bundle3.load("Lock_Large");
				MenuConfigurationOptionsUI.fontColorPicker.add(sleekImageTexture3);
				bundle3.unload();
			}
			MenuConfigurationOptionsUI.optionsBox.add(MenuConfigurationOptionsUI.fontColorPicker);
			MenuConfigurationOptionsUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuConfigurationOptionsUI.backButton.positionOffset_Y = -50;
			MenuConfigurationOptionsUI.backButton.positionScale_Y = 1f;
			MenuConfigurationOptionsUI.backButton.sizeOffset_X = 200;
			MenuConfigurationOptionsUI.backButton.sizeOffset_Y = 50;
			MenuConfigurationOptionsUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuConfigurationOptionsUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton = MenuConfigurationOptionsUI.backButton;
			if (MenuConfigurationOptionsUI.<>f__mg$cache1F == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache1F = new ClickedButton(MenuConfigurationOptionsUI.onClickedBackButton);
			}
			sleekButton.onClickedButton = MenuConfigurationOptionsUI.<>f__mg$cache1F;
			MenuConfigurationOptionsUI.backButton.fontSize = 14;
			MenuConfigurationOptionsUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuConfigurationOptionsUI.container.add(MenuConfigurationOptionsUI.backButton);
			MenuConfigurationOptionsUI.defaultButton = new SleekButton();
			MenuConfigurationOptionsUI.defaultButton.positionOffset_X = -200;
			MenuConfigurationOptionsUI.defaultButton.positionOffset_Y = -50;
			MenuConfigurationOptionsUI.defaultButton.positionScale_X = 1f;
			MenuConfigurationOptionsUI.defaultButton.positionScale_Y = 1f;
			MenuConfigurationOptionsUI.defaultButton.sizeOffset_X = 200;
			MenuConfigurationOptionsUI.defaultButton.sizeOffset_Y = 50;
			MenuConfigurationOptionsUI.defaultButton.text = MenuPlayConfigUI.localization.format("Default");
			MenuConfigurationOptionsUI.defaultButton.tooltip = MenuPlayConfigUI.localization.format("Default_Tooltip");
			SleekButton sleekButton2 = MenuConfigurationOptionsUI.defaultButton;
			if (MenuConfigurationOptionsUI.<>f__mg$cache20 == null)
			{
				MenuConfigurationOptionsUI.<>f__mg$cache20 = new ClickedButton(MenuConfigurationOptionsUI.onClickedDefaultButton);
			}
			sleekButton2.onClickedButton = MenuConfigurationOptionsUI.<>f__mg$cache20;
			MenuConfigurationOptionsUI.defaultButton.fontSize = 14;
			MenuConfigurationOptionsUI.container.add(MenuConfigurationOptionsUI.defaultButton);
			MenuConfigurationOptionsUI.updateAll();
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x0017033B File Offset: 0x0016E73B
		public static void open()
		{
			if (MenuConfigurationOptionsUI.active)
			{
				return;
			}
			MenuConfigurationOptionsUI.active = true;
			MenuConfigurationOptionsUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x00170368 File Offset: 0x0016E768
		public static void close()
		{
			if (!MenuConfigurationOptionsUI.active)
			{
				return;
			}
			MenuConfigurationOptionsUI.active = false;
			MenuConfigurationOptionsUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x00170395 File Offset: 0x0016E795
		private static void onDraggedFOVSlider(SleekSlider slider, float state)
		{
			OptionsSettings.fov = state;
			OptionsSettings.apply();
			MenuConfigurationOptionsUI.fovSlider.updateLabel(MenuConfigurationOptionsUI.localization.format("FOV_Slider_Label", new object[]
			{
				(int)OptionsSettings.view
			}));
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x001703CF File Offset: 0x0016E7CF
		private static void onDraggedVolumeSlider(SleekSlider slider, float state)
		{
			OptionsSettings.volume = state;
			OptionsSettings.apply();
			MenuConfigurationOptionsUI.volumeSlider.updateLabel(MenuConfigurationOptionsUI.localization.format("Volume_Slider_Label", new object[]
			{
				(int)(OptionsSettings.volume * 100f)
			}));
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x00170410 File Offset: 0x0016E810
		private static void onDraggedVoiceSlider(SleekSlider slider, float state)
		{
			OptionsSettings.voice = state * 4f;
			MenuConfigurationOptionsUI.voiceSlider.updateLabel(MenuConfigurationOptionsUI.localization.format("Voice_Slider_Label", new object[]
			{
				(int)(OptionsSettings.voice * 100f)
			}));
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x0017045C File Offset: 0x0016E85C
		private static void onToggledDebugToggle(SleekToggle toggle, bool state)
		{
			OptionsSettings.debug = state;
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x00170464 File Offset: 0x0016E864
		private static void onToggledMusicToggle(SleekToggle toggle, bool state)
		{
			OptionsSettings.music = state;
			OptionsSettings.apply();
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x00170471 File Offset: 0x0016E871
		private static void onToggledSplashscreenToggle(SleekToggle toggle, bool state)
		{
			OptionsSettings.splashscreen = state;
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x00170479 File Offset: 0x0016E879
		private static void onToggledTimerToggle(SleekToggle toggle, bool state)
		{
			OptionsSettings.timer = state;
			OptionsSettings.apply();
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x00170486 File Offset: 0x0016E886
		private static void onToggledGoreToggle(SleekToggle toggle, bool state)
		{
			OptionsSettings.gore = state;
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x0017048E File Offset: 0x0016E88E
		private static void onToggledFilterToggle(SleekToggle toggle, bool state)
		{
			OptionsSettings.filter = state;
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x00170496 File Offset: 0x0016E896
		private static void onToggledChatTextToggle(SleekToggle toggle, bool state)
		{
			OptionsSettings.chatText = state;
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x0017049E File Offset: 0x0016E89E
		private static void onToggledChatVoiceInToggle(SleekToggle toggle, bool state)
		{
			OptionsSettings.chatVoiceIn = state;
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x001704A6 File Offset: 0x0016E8A6
		private static void onToggledChatVoiceOutToggle(SleekToggle toggle, bool state)
		{
			OptionsSettings.chatVoiceOut = state;
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x001704AE File Offset: 0x0016E8AE
		private static void onToggledHintsToggle(SleekToggle toggle, bool state)
		{
			OptionsSettings.hints = state;
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x001704B6 File Offset: 0x0016E8B6
		private static void onToggledAmbienceToggle(SleekToggle toggle, bool state)
		{
			OptionsSettings.ambience = state;
			OptionsSettings.apply();
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x001704C3 File Offset: 0x0016E8C3
		private static void onToggledStreamerToggle(SleekToggle toggle, bool state)
		{
			OptionsSettings.streamer = state;
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x001704CB File Offset: 0x0016E8CB
		private static void onToggledFeaturedWorkshopToggle(SleekToggle toggle, bool state)
		{
			OptionsSettings.featuredWorkshop = state;
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x001704D3 File Offset: 0x0016E8D3
		private static void onToggledMatchmakingShowAllMapsToggle(SleekToggle toggle, bool state)
		{
			OptionsSettings.matchmakingShowAllMaps = state;
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x001704DB File Offset: 0x0016E8DB
		private static void onToggledShowHotbarToggle(SleekToggle toggle, bool state)
		{
			OptionsSettings.showHotbar = state;
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x001704E3 File Offset: 0x0016E8E3
		private static void onTypedMinMatchmakingPlayersField(SleekInt32Field field, int state)
		{
			OptionsSettings.minMatchmakingPlayers = state;
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x001704EB File Offset: 0x0016E8EB
		private static void onTypedMaxMatchmakingPingField(SleekInt32Field field, int state)
		{
			OptionsSettings.maxMatchmakingPing = state;
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x001704F3 File Offset: 0x0016E8F3
		private static void onSwappedMetricState(SleekButtonState button, int index)
		{
			OptionsSettings.metric = (index == 1);
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x001704FE File Offset: 0x0016E8FE
		private static void onSwappedTalkState(SleekButtonState button, int index)
		{
			OptionsSettings.talk = (index == 1);
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x00170509 File Offset: 0x0016E909
		private static void onSwappedUIState(SleekButtonState button, int index)
		{
			OptionsSettings.proUI = (index == 1);
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x00170514 File Offset: 0x0016E914
		private static void onSwappedHitmarkerState(SleekButtonState button, int index)
		{
			OptionsSettings.hitmarker = (index == 1);
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x00170520 File Offset: 0x0016E920
		private static void onCrosshairColorPicked(SleekColorPicker picker, Color color)
		{
			OptionsSettings.crosshairColor = color;
			if (PlayerLifeUI.dotImage != null)
			{
				PlayerLifeUI.crosshairLeftImage.backgroundColor = color;
				PlayerLifeUI.crosshairRightImage.backgroundColor = color;
				PlayerLifeUI.crosshairDownImage.backgroundColor = color;
				PlayerLifeUI.crosshairUpImage.backgroundColor = color;
				PlayerLifeUI.dotImage.backgroundColor = color;
			}
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x00170574 File Offset: 0x0016E974
		private static void onHitmarkerColorPicked(SleekColorPicker picker, Color color)
		{
			OptionsSettings.hitmarkerColor = color;
			if (PlayerLifeUI.hitmarkers != null)
			{
				for (int i = 0; i < PlayerLifeUI.hitmarkers.Length; i++)
				{
					HitmarkerInfo hitmarkerInfo = PlayerLifeUI.hitmarkers[i];
					hitmarkerInfo.hitEntitiyImage.backgroundColor = color;
					hitmarkerInfo.hitBuildImage.backgroundColor = color;
				}
			}
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x001705CC File Offset: 0x0016E9CC
		private static void onCriticalHitmarkerColorPicked(SleekColorPicker picker, Color color)
		{
			OptionsSettings.criticalHitmarkerColor = color;
			if (PlayerLifeUI.hitmarkers != null)
			{
				for (int i = 0; i < PlayerLifeUI.hitmarkers.Length; i++)
				{
					HitmarkerInfo hitmarkerInfo = PlayerLifeUI.hitmarkers[i];
					hitmarkerInfo.hitCriticalImage.backgroundColor = color;
					hitmarkerInfo.hitCriticalImage.backgroundColor = color;
				}
			}
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x00170621 File Offset: 0x0016EA21
		private static void onCursorColorPicked(SleekColorPicker picker, Color color)
		{
			OptionsSettings.cursorColor = color;
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x00170629 File Offset: 0x0016EA29
		private static void onBackgroundColorPicked(SleekColorPicker picker, Color color)
		{
			OptionsSettings.backgroundColor = color;
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x00170631 File Offset: 0x0016EA31
		private static void onForegroundColorPicked(SleekColorPicker picker, Color color)
		{
			OptionsSettings.foregroundColor = color;
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x00170639 File Offset: 0x0016EA39
		private static void onFontColorPicked(SleekColorPicker picker, Color color)
		{
			OptionsSettings.fontColor = color;
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x00170641 File Offset: 0x0016EA41
		private static void onClickedBackButton(SleekButton button)
		{
			if (Player.player != null)
			{
				PlayerPauseUI.open();
			}
			else
			{
				MenuConfigurationUI.open();
			}
			MenuConfigurationOptionsUI.close();
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x00170667 File Offset: 0x0016EA67
		private static void onClickedDefaultButton(SleekButton button)
		{
			OptionsSettings.restoreDefaults();
			MenuConfigurationOptionsUI.updateAll();
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x00170674 File Offset: 0x0016EA74
		private static void updateAll()
		{
			MenuConfigurationOptionsUI.fovSlider.state = OptionsSettings.fov;
			MenuConfigurationOptionsUI.fovSlider.updateLabel(MenuConfigurationOptionsUI.localization.format("FOV_Slider_Label", new object[]
			{
				(int)OptionsSettings.view
			}));
			MenuConfigurationOptionsUI.volumeSlider.state = OptionsSettings.volume;
			MenuConfigurationOptionsUI.volumeSlider.updateLabel(MenuConfigurationOptionsUI.localization.format("Volume_Slider_Label", new object[]
			{
				(int)(OptionsSettings.volume * 100f)
			}));
			MenuConfigurationOptionsUI.voiceSlider.state = OptionsSettings.voice / 4f;
			MenuConfigurationOptionsUI.voiceSlider.updateLabel(MenuConfigurationOptionsUI.localization.format("Voice_Slider_Label", new object[]
			{
				(int)(OptionsSettings.voice * 100f)
			}));
			MenuConfigurationOptionsUI.debugToggle.state = OptionsSettings.debug;
			MenuConfigurationOptionsUI.musicToggle.state = OptionsSettings.music;
			MenuConfigurationOptionsUI.splashscreenToggle.state = OptionsSettings.splashscreen;
			MenuConfigurationOptionsUI.timerToggle.state = OptionsSettings.timer;
			MenuConfigurationOptionsUI.goreToggle.state = OptionsSettings.gore;
			MenuConfigurationOptionsUI.filterToggle.state = OptionsSettings.filter;
			MenuConfigurationOptionsUI.chatTextToggle.state = OptionsSettings.chatText;
			MenuConfigurationOptionsUI.chatVoiceInToggle.state = OptionsSettings.chatVoiceIn;
			MenuConfigurationOptionsUI.chatVoiceOutToggle.state = OptionsSettings.chatVoiceOut;
			MenuConfigurationOptionsUI.hintsToggle.state = OptionsSettings.hints;
			MenuConfigurationOptionsUI.ambienceToggle.state = OptionsSettings.ambience;
			MenuConfigurationOptionsUI.streamerToggle.state = OptionsSettings.streamer;
			MenuConfigurationOptionsUI.featuredWorkshopToggle.state = OptionsSettings.featuredWorkshop;
			MenuConfigurationOptionsUI.matchmakingShowAllMapsToggle.state = OptionsSettings.matchmakingShowAllMaps;
			MenuConfigurationOptionsUI.showHotbarToggle.state = OptionsSettings.showHotbar;
			MenuConfigurationOptionsUI.minMatchmakingPlayersField.state = OptionsSettings.minMatchmakingPlayers;
			MenuConfigurationOptionsUI.maxMatchmakingPingField.state = OptionsSettings.maxMatchmakingPing;
			MenuConfigurationOptionsUI.metricButton.state = ((!OptionsSettings.metric) ? 0 : 1);
			MenuConfigurationOptionsUI.talkButton.state = ((!OptionsSettings.talk) ? 0 : 1);
			MenuConfigurationOptionsUI.uiButton.state = ((!OptionsSettings.proUI) ? 0 : 1);
			MenuConfigurationOptionsUI.hitmarkerButton.state = ((!OptionsSettings.hitmarker) ? 0 : 1);
			MenuConfigurationOptionsUI.crosshairColorPicker.state = OptionsSettings.crosshairColor;
			MenuConfigurationOptionsUI.hitmarkerColorPicker.state = OptionsSettings.hitmarkerColor;
			MenuConfigurationOptionsUI.criticalHitmarkerColorPicker.state = OptionsSettings.criticalHitmarkerColor;
			MenuConfigurationOptionsUI.cursorColorPicker.state = OptionsSettings.cursorColor;
			MenuConfigurationOptionsUI.backgroundColorPicker.state = OptionsSettings.backgroundColor;
			MenuConfigurationOptionsUI.foregroundColorPicker.state = OptionsSettings.foregroundColor;
			MenuConfigurationOptionsUI.fontColorPicker.state = OptionsSettings.fontColor;
		}

		// Token: 0x0400266B RID: 9835
		private static Local localization;

		// Token: 0x0400266C RID: 9836
		private static Sleek container;

		// Token: 0x0400266D RID: 9837
		public static bool active;

		// Token: 0x0400266E RID: 9838
		private static SleekButtonIcon backButton;

		// Token: 0x0400266F RID: 9839
		private static SleekButton defaultButton;

		// Token: 0x04002670 RID: 9840
		private static SleekScrollBox optionsBox;

		// Token: 0x04002671 RID: 9841
		private static SleekSlider fovSlider;

		// Token: 0x04002672 RID: 9842
		private static SleekSlider volumeSlider;

		// Token: 0x04002673 RID: 9843
		private static SleekSlider voiceSlider;

		// Token: 0x04002674 RID: 9844
		private static SleekToggle debugToggle;

		// Token: 0x04002675 RID: 9845
		private static SleekToggle musicToggle;

		// Token: 0x04002676 RID: 9846
		private static SleekToggle splashscreenToggle;

		// Token: 0x04002677 RID: 9847
		private static SleekToggle timerToggle;

		// Token: 0x04002678 RID: 9848
		private static SleekToggle goreToggle;

		// Token: 0x04002679 RID: 9849
		private static SleekToggle filterToggle;

		// Token: 0x0400267A RID: 9850
		private static SleekToggle chatTextToggle;

		// Token: 0x0400267B RID: 9851
		private static SleekToggle chatVoiceInToggle;

		// Token: 0x0400267C RID: 9852
		private static SleekToggle chatVoiceOutToggle;

		// Token: 0x0400267D RID: 9853
		private static SleekToggle hintsToggle;

		// Token: 0x0400267E RID: 9854
		private static SleekToggle ambienceToggle;

		// Token: 0x0400267F RID: 9855
		private static SleekToggle streamerToggle;

		// Token: 0x04002680 RID: 9856
		private static SleekToggle featuredWorkshopToggle;

		// Token: 0x04002681 RID: 9857
		private static SleekToggle matchmakingShowAllMapsToggle;

		// Token: 0x04002682 RID: 9858
		private static SleekToggle showHotbarToggle;

		// Token: 0x04002683 RID: 9859
		private static SleekInt32Field minMatchmakingPlayersField;

		// Token: 0x04002684 RID: 9860
		private static SleekInt32Field maxMatchmakingPingField;

		// Token: 0x04002685 RID: 9861
		private static SleekButtonState metricButton;

		// Token: 0x04002686 RID: 9862
		private static SleekButtonState talkButton;

		// Token: 0x04002687 RID: 9863
		private static SleekButtonState uiButton;

		// Token: 0x04002688 RID: 9864
		private static SleekButtonState hitmarkerButton;

		// Token: 0x04002689 RID: 9865
		private static SleekBox crosshairBox;

		// Token: 0x0400268A RID: 9866
		private static SleekColorPicker crosshairColorPicker;

		// Token: 0x0400268B RID: 9867
		private static SleekBox hitmarkerBox;

		// Token: 0x0400268C RID: 9868
		private static SleekColorPicker hitmarkerColorPicker;

		// Token: 0x0400268D RID: 9869
		private static SleekBox criticalHitmarkerBox;

		// Token: 0x0400268E RID: 9870
		private static SleekColorPicker criticalHitmarkerColorPicker;

		// Token: 0x0400268F RID: 9871
		private static SleekBox cursorBox;

		// Token: 0x04002690 RID: 9872
		private static SleekColorPicker cursorColorPicker;

		// Token: 0x04002691 RID: 9873
		private static SleekBox backgroundBox;

		// Token: 0x04002692 RID: 9874
		private static SleekColorPicker backgroundColorPicker;

		// Token: 0x04002693 RID: 9875
		private static SleekBox foregroundBox;

		// Token: 0x04002694 RID: 9876
		private static SleekColorPicker foregroundColorPicker;

		// Token: 0x04002695 RID: 9877
		private static SleekBox fontBox;

		// Token: 0x04002696 RID: 9878
		private static SleekColorPicker fontColorPicker;

		// Token: 0x04002697 RID: 9879
		[CompilerGenerated]
		private static Dragged <>f__mg$cache0;

		// Token: 0x04002698 RID: 9880
		[CompilerGenerated]
		private static Dragged <>f__mg$cache1;

		// Token: 0x04002699 RID: 9881
		[CompilerGenerated]
		private static Dragged <>f__mg$cache2;

		// Token: 0x0400269A RID: 9882
		[CompilerGenerated]
		private static Toggled <>f__mg$cache3;

		// Token: 0x0400269B RID: 9883
		[CompilerGenerated]
		private static Toggled <>f__mg$cache4;

		// Token: 0x0400269C RID: 9884
		[CompilerGenerated]
		private static Toggled <>f__mg$cache5;

		// Token: 0x0400269D RID: 9885
		[CompilerGenerated]
		private static Toggled <>f__mg$cache6;

		// Token: 0x0400269E RID: 9886
		[CompilerGenerated]
		private static Toggled <>f__mg$cache7;

		// Token: 0x0400269F RID: 9887
		[CompilerGenerated]
		private static Toggled <>f__mg$cache8;

		// Token: 0x040026A0 RID: 9888
		[CompilerGenerated]
		private static Toggled <>f__mg$cache9;

		// Token: 0x040026A1 RID: 9889
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheA;

		// Token: 0x040026A2 RID: 9890
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheB;

		// Token: 0x040026A3 RID: 9891
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheC;

		// Token: 0x040026A4 RID: 9892
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheD;

		// Token: 0x040026A5 RID: 9893
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheE;

		// Token: 0x040026A6 RID: 9894
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheF;

		// Token: 0x040026A7 RID: 9895
		[CompilerGenerated]
		private static Toggled <>f__mg$cache10;

		// Token: 0x040026A8 RID: 9896
		[CompilerGenerated]
		private static Toggled <>f__mg$cache11;

		// Token: 0x040026A9 RID: 9897
		[CompilerGenerated]
		private static TypedInt32 <>f__mg$cache12;

		// Token: 0x040026AA RID: 9898
		[CompilerGenerated]
		private static TypedInt32 <>f__mg$cache13;

		// Token: 0x040026AB RID: 9899
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache14;

		// Token: 0x040026AC RID: 9900
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache15;

		// Token: 0x040026AD RID: 9901
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache16;

		// Token: 0x040026AE RID: 9902
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache17;

		// Token: 0x040026AF RID: 9903
		[CompilerGenerated]
		private static ColorPicked <>f__mg$cache18;

		// Token: 0x040026B0 RID: 9904
		[CompilerGenerated]
		private static ColorPicked <>f__mg$cache19;

		// Token: 0x040026B1 RID: 9905
		[CompilerGenerated]
		private static ColorPicked <>f__mg$cache1A;

		// Token: 0x040026B2 RID: 9906
		[CompilerGenerated]
		private static ColorPicked <>f__mg$cache1B;

		// Token: 0x040026B3 RID: 9907
		[CompilerGenerated]
		private static ColorPicked <>f__mg$cache1C;

		// Token: 0x040026B4 RID: 9908
		[CompilerGenerated]
		private static ColorPicked <>f__mg$cache1D;

		// Token: 0x040026B5 RID: 9909
		[CompilerGenerated]
		private static ColorPicked <>f__mg$cache1E;

		// Token: 0x040026B6 RID: 9910
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1F;

		// Token: 0x040026B7 RID: 9911
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache20;
	}
}
