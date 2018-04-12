using System;
using System.Runtime.CompilerServices;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007A3 RID: 1955
	public class PlayerPauseUI
	{
		// Token: 0x060038E9 RID: 14569 RVA: 0x001A1804 File Offset: 0x0019FC04
		public PlayerPauseUI()
		{
			PlayerPauseUI.localization = Localization.read("/Player/PlayerPause.dat");
			if (PlayerPauseUI.icons != null)
			{
				PlayerPauseUI.icons.unload();
				PlayerPauseUI.icons = null;
			}
			PlayerPauseUI.icons = Bundles.getBundle("/Bundles/Textures/Player/Icons/PlayerPause/PlayerPause.unity3d");
			PlayerPauseUI.container = new Sleek();
			PlayerPauseUI.container.positionScale_Y = 1f;
			PlayerPauseUI.container.positionOffset_X = 10;
			PlayerPauseUI.container.positionOffset_Y = 10;
			PlayerPauseUI.container.sizeOffset_X = -20;
			PlayerPauseUI.container.sizeOffset_Y = -20;
			PlayerPauseUI.container.sizeScale_X = 1f;
			PlayerPauseUI.container.sizeScale_Y = 1f;
			PlayerUI.container.add(PlayerPauseUI.container);
			PlayerPauseUI.active = false;
			PlayerPauseUI.returnButton = new SleekButtonIcon((Texture2D)PlayerPauseUI.icons.load("Return"));
			PlayerPauseUI.returnButton.positionOffset_X = -100;
			PlayerPauseUI.returnButton.positionOffset_Y = -205;
			PlayerPauseUI.returnButton.positionScale_X = 0.5f;
			PlayerPauseUI.returnButton.positionScale_Y = 0.5f;
			PlayerPauseUI.returnButton.sizeOffset_X = 200;
			PlayerPauseUI.returnButton.sizeOffset_Y = 50;
			PlayerPauseUI.returnButton.text = PlayerPauseUI.localization.format("Return_Button_Text");
			PlayerPauseUI.returnButton.tooltip = PlayerPauseUI.localization.format("Return_Button_Tooltip");
			PlayerPauseUI.returnButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton = PlayerPauseUI.returnButton;
			if (PlayerPauseUI.<>f__mg$cache0 == null)
			{
				PlayerPauseUI.<>f__mg$cache0 = new ClickedButton(PlayerPauseUI.onClickedReturnButton);
			}
			sleekButton.onClickedButton = PlayerPauseUI.<>f__mg$cache0;
			PlayerPauseUI.returnButton.fontSize = 14;
			PlayerPauseUI.container.add(PlayerPauseUI.returnButton);
			PlayerPauseUI.optionsButton = new SleekButtonIcon((Texture2D)PlayerPauseUI.icons.load("Options"));
			PlayerPauseUI.optionsButton.positionOffset_X = -100;
			PlayerPauseUI.optionsButton.positionOffset_Y = -145;
			PlayerPauseUI.optionsButton.positionScale_X = 0.5f;
			PlayerPauseUI.optionsButton.positionScale_Y = 0.5f;
			PlayerPauseUI.optionsButton.sizeOffset_X = 200;
			PlayerPauseUI.optionsButton.sizeOffset_Y = 50;
			PlayerPauseUI.optionsButton.text = PlayerPauseUI.localization.format("Options_Button_Text");
			PlayerPauseUI.optionsButton.tooltip = PlayerPauseUI.localization.format("Options_Button_Tooltip");
			SleekButton sleekButton2 = PlayerPauseUI.optionsButton;
			if (PlayerPauseUI.<>f__mg$cache1 == null)
			{
				PlayerPauseUI.<>f__mg$cache1 = new ClickedButton(PlayerPauseUI.onClickedOptionsButton);
			}
			sleekButton2.onClickedButton = PlayerPauseUI.<>f__mg$cache1;
			PlayerPauseUI.optionsButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			PlayerPauseUI.optionsButton.fontSize = 14;
			PlayerPauseUI.container.add(PlayerPauseUI.optionsButton);
			PlayerPauseUI.displayButton = new SleekButtonIcon((Texture2D)PlayerPauseUI.icons.load("Display"));
			PlayerPauseUI.displayButton.positionOffset_X = -100;
			PlayerPauseUI.displayButton.positionOffset_Y = -85;
			PlayerPauseUI.displayButton.positionScale_X = 0.5f;
			PlayerPauseUI.displayButton.positionScale_Y = 0.5f;
			PlayerPauseUI.displayButton.sizeOffset_X = 200;
			PlayerPauseUI.displayButton.sizeOffset_Y = 50;
			PlayerPauseUI.displayButton.text = PlayerPauseUI.localization.format("Display_Button_Text");
			PlayerPauseUI.displayButton.tooltip = PlayerPauseUI.localization.format("Display_Button_Tooltip");
			PlayerPauseUI.displayButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton3 = PlayerPauseUI.displayButton;
			if (PlayerPauseUI.<>f__mg$cache2 == null)
			{
				PlayerPauseUI.<>f__mg$cache2 = new ClickedButton(PlayerPauseUI.onClickedDisplayButton);
			}
			sleekButton3.onClickedButton = PlayerPauseUI.<>f__mg$cache2;
			PlayerPauseUI.displayButton.fontSize = 14;
			PlayerPauseUI.container.add(PlayerPauseUI.displayButton);
			PlayerPauseUI.graphicsButton = new SleekButtonIcon((Texture2D)PlayerPauseUI.icons.load("Graphics"));
			PlayerPauseUI.graphicsButton.positionOffset_X = -100;
			PlayerPauseUI.graphicsButton.positionOffset_Y = -25;
			PlayerPauseUI.graphicsButton.positionScale_X = 0.5f;
			PlayerPauseUI.graphicsButton.positionScale_Y = 0.5f;
			PlayerPauseUI.graphicsButton.sizeOffset_X = 200;
			PlayerPauseUI.graphicsButton.sizeOffset_Y = 50;
			PlayerPauseUI.graphicsButton.text = PlayerPauseUI.localization.format("Graphics_Button_Text");
			PlayerPauseUI.graphicsButton.tooltip = PlayerPauseUI.localization.format("Graphics_Button_Tooltip");
			PlayerPauseUI.graphicsButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton4 = PlayerPauseUI.graphicsButton;
			if (PlayerPauseUI.<>f__mg$cache3 == null)
			{
				PlayerPauseUI.<>f__mg$cache3 = new ClickedButton(PlayerPauseUI.onClickedGraphicsButton);
			}
			sleekButton4.onClickedButton = PlayerPauseUI.<>f__mg$cache3;
			PlayerPauseUI.graphicsButton.fontSize = 14;
			PlayerPauseUI.container.add(PlayerPauseUI.graphicsButton);
			PlayerPauseUI.controlsButton = new SleekButtonIcon((Texture2D)PlayerPauseUI.icons.load("Controls"));
			PlayerPauseUI.controlsButton.positionOffset_X = -100;
			PlayerPauseUI.controlsButton.positionOffset_Y = 35;
			PlayerPauseUI.controlsButton.positionScale_X = 0.5f;
			PlayerPauseUI.controlsButton.positionScale_Y = 0.5f;
			PlayerPauseUI.controlsButton.sizeOffset_X = 200;
			PlayerPauseUI.controlsButton.sizeOffset_Y = 50;
			PlayerPauseUI.controlsButton.text = PlayerPauseUI.localization.format("Controls_Button_Text");
			PlayerPauseUI.controlsButton.tooltip = PlayerPauseUI.localization.format("Controls_Button_Tooltip");
			PlayerPauseUI.controlsButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton5 = PlayerPauseUI.controlsButton;
			if (PlayerPauseUI.<>f__mg$cache4 == null)
			{
				PlayerPauseUI.<>f__mg$cache4 = new ClickedButton(PlayerPauseUI.onClickedControlsButton);
			}
			sleekButton5.onClickedButton = PlayerPauseUI.<>f__mg$cache4;
			PlayerPauseUI.controlsButton.fontSize = 14;
			PlayerPauseUI.container.add(PlayerPauseUI.controlsButton);
			PlayerPauseUI.exitButton = new SleekButtonIcon((Texture2D)PlayerPauseUI.icons.load("Exit"));
			PlayerPauseUI.exitButton.positionOffset_X = -100;
			PlayerPauseUI.exitButton.positionOffset_Y = 155;
			PlayerPauseUI.exitButton.positionScale_X = 0.5f;
			PlayerPauseUI.exitButton.positionScale_Y = 0.5f;
			PlayerPauseUI.exitButton.sizeOffset_X = 200;
			PlayerPauseUI.exitButton.sizeOffset_Y = 50;
			PlayerPauseUI.exitButton.text = PlayerPauseUI.localization.format("Exit_Button_Text");
			PlayerPauseUI.exitButton.tooltip = PlayerPauseUI.localization.format("Exit_Button_Tooltip");
			PlayerPauseUI.exitButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton6 = PlayerPauseUI.exitButton;
			if (PlayerPauseUI.<>f__mg$cache5 == null)
			{
				PlayerPauseUI.<>f__mg$cache5 = new ClickedButton(PlayerPauseUI.onClickedExitButton);
			}
			sleekButton6.onClickedButton = PlayerPauseUI.<>f__mg$cache5;
			PlayerPauseUI.exitButton.fontSize = 14;
			PlayerPauseUI.container.add(PlayerPauseUI.exitButton);
			PlayerPauseUI.suicideButton = new SleekButtonIconConfirm((Texture2D)PlayerPauseUI.icons.load("Suicide"), PlayerPauseUI.localization.format("Suicide_Button_Confirm"), PlayerPauseUI.localization.format("Suicide_Button_Confirm_Tooltip"), PlayerPauseUI.localization.format("Suicide_Button_Deny"), PlayerPauseUI.localization.format("Suicide_Button_Deny_Tooltip"));
			PlayerPauseUI.suicideButton.positionOffset_X = -100;
			PlayerPauseUI.suicideButton.positionOffset_Y = 95;
			PlayerPauseUI.suicideButton.positionScale_X = 0.5f;
			PlayerPauseUI.suicideButton.positionScale_Y = 0.5f;
			PlayerPauseUI.suicideButton.sizeOffset_X = 200;
			PlayerPauseUI.suicideButton.sizeOffset_Y = 50;
			PlayerPauseUI.suicideButton.text = PlayerPauseUI.localization.format("Suicide_Button_Text");
			PlayerPauseUI.suicideButton.tooltip = PlayerPauseUI.localization.format("Suicide_Button_Tooltip");
			PlayerPauseUI.suicideButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButtonIconConfirm sleekButtonIconConfirm = PlayerPauseUI.suicideButton;
			if (PlayerPauseUI.<>f__mg$cache6 == null)
			{
				PlayerPauseUI.<>f__mg$cache6 = new Confirm(PlayerPauseUI.onClickedSuicideButton);
			}
			sleekButtonIconConfirm.onConfirmed = PlayerPauseUI.<>f__mg$cache6;
			PlayerPauseUI.suicideButton.fontSize = 14;
			PlayerPauseUI.container.add(PlayerPauseUI.suicideButton);
			if (!Provider.modeConfigData.Gameplay.Can_Suicide)
			{
				PlayerPauseUI.suicideButton.isInputable = false;
				SleekLabel sleekLabel = new SleekLabel();
				sleekLabel.positionOffset_X = -100;
				sleekLabel.positionOffset_Y = 95;
				sleekLabel.positionScale_X = 0.5f;
				sleekLabel.positionScale_Y = 0.5f;
				sleekLabel.sizeOffset_X = 200;
				sleekLabel.sizeOffset_Y = 50;
				sleekLabel.text = PlayerPauseUI.localization.format("Suicide_Disabled");
				sleekLabel.foregroundColor = Palette.COLOR_R;
				sleekLabel.foregroundTint = ESleekTint.NONE;
				sleekLabel.fontSize = 32;
				PlayerPauseUI.container.add(sleekLabel);
			}
			PlayerPauseUI.spyBox = new SleekBox();
			PlayerPauseUI.spyBox.positionOffset_Y = -310;
			PlayerPauseUI.spyBox.positionScale_X = 0.5f;
			PlayerPauseUI.spyBox.positionScale_Y = 0.5f;
			PlayerPauseUI.spyBox.sizeOffset_X = 660;
			PlayerPauseUI.spyBox.sizeOffset_Y = 500;
			PlayerPauseUI.container.add(PlayerPauseUI.spyBox);
			PlayerPauseUI.spyBox.isVisible = false;
			PlayerPauseUI.spyImage = new SleekImageTexture();
			PlayerPauseUI.spyImage.positionOffset_X = 10;
			PlayerPauseUI.spyImage.positionOffset_Y = 10;
			PlayerPauseUI.spyImage.sizeOffset_X = 640;
			PlayerPauseUI.spyImage.sizeOffset_Y = 480;
			PlayerPauseUI.spyBox.add(PlayerPauseUI.spyImage);
			PlayerPauseUI.spyRefreshButton = new SleekButton();
			PlayerPauseUI.spyRefreshButton.positionOffset_X = -205;
			PlayerPauseUI.spyRefreshButton.positionOffset_Y = 10;
			PlayerPauseUI.spyRefreshButton.positionScale_X = 0.5f;
			PlayerPauseUI.spyRefreshButton.positionScale_Y = 1f;
			PlayerPauseUI.spyRefreshButton.sizeOffset_X = 200;
			PlayerPauseUI.spyRefreshButton.sizeOffset_Y = 50;
			PlayerPauseUI.spyRefreshButton.text = PlayerPauseUI.localization.format("Spy_Refresh_Button_Text");
			PlayerPauseUI.spyRefreshButton.tooltip = PlayerPauseUI.localization.format("Spy_Refresh_Button_Tooltip");
			SleekButton sleekButton7 = PlayerPauseUI.spyRefreshButton;
			if (PlayerPauseUI.<>f__mg$cache7 == null)
			{
				PlayerPauseUI.<>f__mg$cache7 = new ClickedButton(PlayerPauseUI.onClickedSpyRefreshButton);
			}
			sleekButton7.onClickedButton = PlayerPauseUI.<>f__mg$cache7;
			PlayerPauseUI.spyRefreshButton.fontSize = 14;
			PlayerPauseUI.spyBox.add(PlayerPauseUI.spyRefreshButton);
			PlayerPauseUI.spySlayButton = new SleekButton();
			PlayerPauseUI.spySlayButton.positionOffset_X = 5;
			PlayerPauseUI.spySlayButton.positionOffset_Y = 10;
			PlayerPauseUI.spySlayButton.positionScale_X = 0.5f;
			PlayerPauseUI.spySlayButton.positionScale_Y = 1f;
			PlayerPauseUI.spySlayButton.sizeOffset_X = 200;
			PlayerPauseUI.spySlayButton.sizeOffset_Y = 50;
			PlayerPauseUI.spySlayButton.text = PlayerPauseUI.localization.format("Spy_Slay_Button_Text");
			PlayerPauseUI.spySlayButton.tooltip = PlayerPauseUI.localization.format("Spy_Slay_Button_Tooltip");
			SleekButton sleekButton8 = PlayerPauseUI.spySlayButton;
			if (PlayerPauseUI.<>f__mg$cache8 == null)
			{
				PlayerPauseUI.<>f__mg$cache8 = new ClickedButton(PlayerPauseUI.onClickedSpySlayButton);
			}
			sleekButton8.onClickedButton = PlayerPauseUI.<>f__mg$cache8;
			PlayerPauseUI.spySlayButton.fontSize = 14;
			PlayerPauseUI.spyBox.add(PlayerPauseUI.spySlayButton);
			PlayerPauseUI.serverBox = new SleekBox();
			PlayerPauseUI.serverBox.positionOffset_Y = -50;
			PlayerPauseUI.serverBox.positionScale_Y = 1f;
			PlayerPauseUI.serverBox.sizeOffset_X = -5;
			PlayerPauseUI.serverBox.sizeOffset_Y = 50;
			PlayerPauseUI.serverBox.sizeScale_X = 0.75f;
			PlayerPauseUI.serverBox.fontSize = 14;
			PlayerPauseUI.container.add(PlayerPauseUI.serverBox);
			PlayerPauseUI.favoriteButton = new SleekButtonIcon(null);
			PlayerPauseUI.favoriteButton.positionScale_X = 0.75f;
			PlayerPauseUI.favoriteButton.positionOffset_Y = -50;
			PlayerPauseUI.favoriteButton.positionOffset_X = 5;
			PlayerPauseUI.favoriteButton.positionScale_Y = 1f;
			PlayerPauseUI.favoriteButton.sizeOffset_X = -5;
			PlayerPauseUI.favoriteButton.sizeOffset_Y = 50;
			PlayerPauseUI.favoriteButton.sizeScale_X = 0.25f;
			PlayerPauseUI.favoriteButton.tooltip = PlayerPauseUI.localization.format("Favorite_Button_Tooltip");
			PlayerPauseUI.favoriteButton.fontSize = 14;
			SleekButton sleekButton9 = PlayerPauseUI.favoriteButton;
			if (PlayerPauseUI.<>f__mg$cache9 == null)
			{
				PlayerPauseUI.<>f__mg$cache9 = new ClickedButton(PlayerPauseUI.onClickedFavoriteButton);
			}
			sleekButton9.onClickedButton = PlayerPauseUI.<>f__mg$cache9;
			PlayerPauseUI.container.add(PlayerPauseUI.favoriteButton);
			if (Provider.isServer)
			{
				PlayerPauseUI.favoriteButton.isVisible = false;
				PlayerPauseUI.serverBox.sizeScale_X = 1f;
			}
			new MenuConfigurationOptionsUI();
			new MenuConfigurationDisplayUI();
			new MenuConfigurationGraphicsUI();
			new MenuConfigurationControlsUI();
			PlayerPauseUI.updateFavorite();
			if (PlayerPauseUI.<>f__mg$cacheA == null)
			{
				PlayerPauseUI.<>f__mg$cacheA = new PlayerSpyReady(PlayerPauseUI.onSpyReady);
			}
			Player.onSpyReady = PlayerPauseUI.<>f__mg$cacheA;
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x001A2438 File Offset: 0x001A0838
		public static void open()
		{
			if (PlayerPauseUI.active)
			{
				return;
			}
			PlayerPauseUI.active = true;
			PlayerPauseUI.lastLeave = Time.realtimeSinceStartup;
			if (Provider.currentServerInfo != null && Level.info != null)
			{
				Local local = Localization.tryRead(Level.info.path, false);
				string text;
				if (local != null && local.has("Name"))
				{
					text = local.format("Name");
				}
				else
				{
					text = Level.info.name;
				}
				string text2;
				if (Provider.currentServerInfo.IsVACSecure)
				{
					text2 = PlayerPauseUI.localization.format("VAC_Secure");
				}
				else
				{
					text2 = PlayerPauseUI.localization.format("VAC_Insecure");
				}
				if (Provider.currentServerInfo.IsBattlEyeSecure)
				{
					text2 = text2 + " + " + PlayerPauseUI.localization.format("BattlEye_Secure");
				}
				else
				{
					text2 = text2 + " + " + PlayerPauseUI.localization.format("BattlEye_Insecure");
				}
				PlayerPauseUI.serverBox.text = PlayerPauseUI.localization.format("Server", new object[]
				{
					text,
					(!OptionsSettings.streamer) ? Provider.currentServerInfo.name : PlayerPauseUI.localization.format("Streamer"),
					text2
				});
			}
			PlayerPauseUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x001A25A1 File Offset: 0x001A09A1
		public static void close()
		{
			if (!PlayerPauseUI.active)
			{
				return;
			}
			PlayerPauseUI.active = false;
			PlayerPauseUI.suicideButton.reset();
			MenuSettings.save();
			PlayerPauseUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x001A25DD File Offset: 0x001A09DD
		private static void onClickedReturnButton(SleekButton button)
		{
			PlayerPauseUI.close();
			PlayerLifeUI.open();
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x001A25E9 File Offset: 0x001A09E9
		private static void onClickedOptionsButton(SleekButton button)
		{
			PlayerPauseUI.close();
			MenuConfigurationOptionsUI.open();
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x001A25F5 File Offset: 0x001A09F5
		private static void onClickedDisplayButton(SleekButton button)
		{
			PlayerPauseUI.close();
			MenuConfigurationDisplayUI.open();
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x001A2601 File Offset: 0x001A0A01
		private static void onClickedGraphicsButton(SleekButton button)
		{
			PlayerPauseUI.close();
			MenuConfigurationGraphicsUI.open();
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x001A260D File Offset: 0x001A0A0D
		private static void onClickedControlsButton(SleekButton button)
		{
			PlayerPauseUI.close();
			MenuConfigurationControlsUI.open();
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x001A2619 File Offset: 0x001A0A19
		private static void onClickedSpyRefreshButton(SleekButton button)
		{
			ChatManager.sendChat(EChatMode.GLOBAL, "/spy " + PlayerPauseUI.spySteamID);
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x001A2635 File Offset: 0x001A0A35
		private static void onClickedSpySlayButton(SleekButton button)
		{
			ChatManager.sendChat(EChatMode.GLOBAL, "/slay " + PlayerPauseUI.spySteamID + "/Screenshot Evidence");
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x001A2658 File Offset: 0x001A0A58
		private static void onClickedExitButton(SleekButton button)
		{
			if (!Provider.isServer && Provider.isPvP && Provider.clients.Count > 1 && (!Player.player.movement.isSafe || !Player.player.movement.isSafeInfo.noWeapons) && !Player.player.life.isDead && Time.realtimeSinceStartup - PlayerPauseUI.lastLeave < Provider.modeConfigData.Gameplay.Timer_Exit)
			{
				return;
			}
			Provider.disconnect();
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x001A26F4 File Offset: 0x001A0AF4
		private static void onClickedSuicideButton(SleekButton button)
		{
			if (((Level.info != null && Level.info.type == ELevelType.SURVIVAL) || !Player.player.movement.isSafe || !Player.player.movement.isSafeInfo.noWeapons) && Provider.modeConfigData.Gameplay.Can_Suicide)
			{
				PlayerPauseUI.close();
				Player.player.life.sendSuicide();
			}
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x001A276F File Offset: 0x001A0B6F
		private static void onClickedFavoriteButton(SleekButton button)
		{
			Provider.toggleFavorite();
			PlayerPauseUI.updateFavorite();
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x001A277C File Offset: 0x001A0B7C
		private static void updateFavorite()
		{
			if (Provider.isFavorited)
			{
				PlayerPauseUI.favoriteButton.text = PlayerPauseUI.localization.format("Favorite_Off_Button_Text");
				PlayerPauseUI.favoriteButton.icon = (Texture2D)PlayerPauseUI.icons.load("Favorite_Off");
			}
			else
			{
				PlayerPauseUI.favoriteButton.text = PlayerPauseUI.localization.format("Favorite_On_Button_Text");
				PlayerPauseUI.favoriteButton.icon = (Texture2D)PlayerPauseUI.icons.load("Favorite_On");
			}
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x001A2808 File Offset: 0x001A0C08
		private static void onSpyReady(CSteamID steamID, byte[] data)
		{
			PlayerPauseUI.spySteamID = steamID;
			Texture2D texture2D = new Texture2D(640, 480, TextureFormat.RGB24, false, true);
			texture2D.name = "Spy";
			texture2D.filterMode = FilterMode.Trilinear;
			texture2D.LoadImage(data);
			PlayerPauseUI.spyImage.texture = texture2D;
			PlayerPauseUI.returnButton.positionOffset_X = -435;
			PlayerPauseUI.optionsButton.positionOffset_X = -435;
			PlayerPauseUI.displayButton.positionOffset_X = -435;
			PlayerPauseUI.graphicsButton.positionOffset_X = -435;
			PlayerPauseUI.controlsButton.positionOffset_X = -435;
			PlayerPauseUI.exitButton.positionOffset_X = -435;
			PlayerPauseUI.suicideButton.positionOffset_X = -435;
			PlayerPauseUI.spyBox.positionOffset_X = -225;
			PlayerPauseUI.spyBox.isVisible = true;
		}

		// Token: 0x04002BAF RID: 11183
		private static Sleek container;

		// Token: 0x04002BB0 RID: 11184
		public static Local localization;

		// Token: 0x04002BB1 RID: 11185
		private static Bundle icons;

		// Token: 0x04002BB2 RID: 11186
		public static bool active;

		// Token: 0x04002BB3 RID: 11187
		private static SleekButtonIcon returnButton;

		// Token: 0x04002BB4 RID: 11188
		private static SleekButtonIcon optionsButton;

		// Token: 0x04002BB5 RID: 11189
		private static SleekButtonIcon displayButton;

		// Token: 0x04002BB6 RID: 11190
		private static SleekButtonIcon graphicsButton;

		// Token: 0x04002BB7 RID: 11191
		private static SleekButtonIcon controlsButton;

		// Token: 0x04002BB8 RID: 11192
		public static SleekButtonIcon exitButton;

		// Token: 0x04002BB9 RID: 11193
		private static SleekButtonIconConfirm suicideButton;

		// Token: 0x04002BBA RID: 11194
		private static SleekBox spyBox;

		// Token: 0x04002BBB RID: 11195
		private static SleekImageTexture spyImage;

		// Token: 0x04002BBC RID: 11196
		private static SleekButton spyRefreshButton;

		// Token: 0x04002BBD RID: 11197
		private static SleekButton spySlayButton;

		// Token: 0x04002BBE RID: 11198
		private static SleekBox serverBox;

		// Token: 0x04002BBF RID: 11199
		private static SleekButtonIcon favoriteButton;

		// Token: 0x04002BC0 RID: 11200
		private static CSteamID spySteamID;

		// Token: 0x04002BC1 RID: 11201
		public static float lastLeave;

		// Token: 0x04002BC2 RID: 11202
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x04002BC3 RID: 11203
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x04002BC4 RID: 11204
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x04002BC5 RID: 11205
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x04002BC6 RID: 11206
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;

		// Token: 0x04002BC7 RID: 11207
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;

		// Token: 0x04002BC8 RID: 11208
		[CompilerGenerated]
		private static Confirm <>f__mg$cache6;

		// Token: 0x04002BC9 RID: 11209
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache7;

		// Token: 0x04002BCA RID: 11210
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache8;

		// Token: 0x04002BCB RID: 11211
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache9;

		// Token: 0x04002BCC RID: 11212
		[CompilerGenerated]
		private static PlayerSpyReady <>f__mg$cacheA;
	}
}
