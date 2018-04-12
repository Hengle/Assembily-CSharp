using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000795 RID: 1941
	public class PlayerDashboardInformationUI
	{
		// Token: 0x060037FD RID: 14333 RVA: 0x0018DD1C File Offset: 0x0018C11C
		public PlayerDashboardInformationUI()
		{
			if (PlayerDashboardInformationUI.icons != null)
			{
				PlayerDashboardInformationUI.icons.unload();
			}
			PlayerDashboardInformationUI.localization = Localization.read("/Player/PlayerDashboardInformation.dat");
			PlayerDashboardInformationUI.icons = Bundles.getBundle("/Bundles/Textures/Player/Icons/PlayerDashboardInformation/PlayerDashboardInformation.unity3d");
			PlayerDashboardInformationUI.container = new Sleek();
			PlayerDashboardInformationUI.container.positionScale_Y = 1f;
			PlayerDashboardInformationUI.container.positionOffset_X = 10;
			PlayerDashboardInformationUI.container.positionOffset_Y = 10;
			PlayerDashboardInformationUI.container.sizeOffset_X = -20;
			PlayerDashboardInformationUI.container.sizeOffset_Y = -20;
			PlayerDashboardInformationUI.container.sizeScale_X = 1f;
			PlayerDashboardInformationUI.container.sizeScale_Y = 1f;
			PlayerUI.container.add(PlayerDashboardInformationUI.container);
			PlayerDashboardInformationUI.active = false;
			PlayerDashboardInformationUI.zoom = 1;
			PlayerDashboardInformationUI.tab = PlayerDashboardInformationUI.EInfoTab.PLAYERS;
			PlayerDashboardInformationUI.isDragging = false;
			PlayerDashboardInformationUI.dragOrigin = Vector2.zero;
			PlayerDashboardInformationUI.dragOffset = Vector2.zero;
			PlayerDashboardInformationUI.backdropBox = new SleekBox();
			PlayerDashboardInformationUI.backdropBox.positionOffset_Y = 60;
			PlayerDashboardInformationUI.backdropBox.sizeOffset_Y = -60;
			PlayerDashboardInformationUI.backdropBox.sizeScale_X = 1f;
			PlayerDashboardInformationUI.backdropBox.sizeScale_Y = 1f;
			Color white = Color.white;
			white.a = 0.5f;
			PlayerDashboardInformationUI.backdropBox.backgroundColor = white;
			PlayerDashboardInformationUI.container.add(PlayerDashboardInformationUI.backdropBox);
			PlayerDashboardInformationUI.mapInspect = new Sleek();
			PlayerDashboardInformationUI.mapInspect.positionOffset_X = 10;
			PlayerDashboardInformationUI.mapInspect.positionOffset_Y = 10;
			PlayerDashboardInformationUI.mapInspect.sizeOffset_X = -15;
			PlayerDashboardInformationUI.mapInspect.sizeOffset_Y = -20;
			PlayerDashboardInformationUI.mapInspect.sizeScale_X = 0.6f;
			PlayerDashboardInformationUI.mapInspect.sizeScale_Y = 1f;
			PlayerDashboardInformationUI.backdropBox.add(PlayerDashboardInformationUI.mapInspect);
			PlayerDashboardInformationUI.mapBox = new SleekViewBox();
			PlayerDashboardInformationUI.mapBox.sizeOffset_Y = -40;
			PlayerDashboardInformationUI.mapBox.sizeScale_X = 1f;
			PlayerDashboardInformationUI.mapBox.sizeScale_Y = 1f;
			PlayerDashboardInformationUI.mapBox.constraint = ESleekConstraint.XY;
			PlayerDashboardInformationUI.mapInspect.add(PlayerDashboardInformationUI.mapBox);
			PlayerDashboardInformationUI.mapImage = new SleekImageTexture();
			PlayerDashboardInformationUI.mapBox.add(PlayerDashboardInformationUI.mapImage);
			PlayerDashboardInformationUI.mapStaticContainer = new Sleek();
			PlayerDashboardInformationUI.mapStaticContainer.sizeScale_X = 1f;
			PlayerDashboardInformationUI.mapStaticContainer.sizeScale_Y = 1f;
			PlayerDashboardInformationUI.mapImage.add(PlayerDashboardInformationUI.mapStaticContainer);
			PlayerDashboardInformationUI.mapDynamicContainer = new Sleek();
			PlayerDashboardInformationUI.mapDynamicContainer.sizeScale_X = 1f;
			PlayerDashboardInformationUI.mapDynamicContainer.sizeScale_Y = 1f;
			PlayerDashboardInformationUI.mapImage.add(PlayerDashboardInformationUI.mapDynamicContainer);
			PlayerDashboardInformationUI.noLabel = new SleekLabel();
			PlayerDashboardInformationUI.noLabel.sizeOffset_Y = -40;
			PlayerDashboardInformationUI.noLabel.sizeScale_X = 1f;
			PlayerDashboardInformationUI.noLabel.sizeScale_Y = 1f;
			PlayerDashboardInformationUI.noLabel.foregroundColor = Palette.COLOR_R;
			PlayerDashboardInformationUI.noLabel.foregroundTint = ESleekTint.NONE;
			PlayerDashboardInformationUI.noLabel.fontSize = 24;
			PlayerDashboardInformationUI.mapInspect.add(PlayerDashboardInformationUI.noLabel);
			PlayerDashboardInformationUI.noLabel.isVisible = false;
			PlayerDashboardInformationUI.updateZoom();
			PlayerDashboardInformationUI.zoomInButton = new SleekButtonIcon((Texture2D)PlayerDashboardInformationUI.icons.load("Zoom_In"));
			PlayerDashboardInformationUI.zoomInButton.positionOffset_Y = -30;
			PlayerDashboardInformationUI.zoomInButton.positionScale_Y = 1f;
			PlayerDashboardInformationUI.zoomInButton.sizeOffset_X = -5;
			PlayerDashboardInformationUI.zoomInButton.sizeOffset_Y = 30;
			PlayerDashboardInformationUI.zoomInButton.sizeScale_X = 0.25f;
			PlayerDashboardInformationUI.zoomInButton.text = PlayerDashboardInformationUI.localization.format("Zoom_In_Button");
			PlayerDashboardInformationUI.zoomInButton.tooltip = PlayerDashboardInformationUI.localization.format("Zoom_In_Button_Tooltip");
			PlayerDashboardInformationUI.zoomInButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton = PlayerDashboardInformationUI.zoomInButton;
			if (PlayerDashboardInformationUI.<>f__mg$cache8 == null)
			{
				PlayerDashboardInformationUI.<>f__mg$cache8 = new ClickedButton(PlayerDashboardInformationUI.onClickedZoomInButton);
			}
			sleekButton.onClickedButton = PlayerDashboardInformationUI.<>f__mg$cache8;
			PlayerDashboardInformationUI.mapInspect.add(PlayerDashboardInformationUI.zoomInButton);
			PlayerDashboardInformationUI.zoomOutButton = new SleekButtonIcon((Texture2D)PlayerDashboardInformationUI.icons.load("Zoom_Out"));
			PlayerDashboardInformationUI.zoomOutButton.positionOffset_X = 5;
			PlayerDashboardInformationUI.zoomOutButton.positionOffset_Y = -30;
			PlayerDashboardInformationUI.zoomOutButton.positionScale_X = 0.25f;
			PlayerDashboardInformationUI.zoomOutButton.positionScale_Y = 1f;
			PlayerDashboardInformationUI.zoomOutButton.sizeOffset_X = -10;
			PlayerDashboardInformationUI.zoomOutButton.sizeOffset_Y = 30;
			PlayerDashboardInformationUI.zoomOutButton.sizeScale_X = 0.25f;
			PlayerDashboardInformationUI.zoomOutButton.text = PlayerDashboardInformationUI.localization.format("Zoom_Out_Button");
			PlayerDashboardInformationUI.zoomOutButton.tooltip = PlayerDashboardInformationUI.localization.format("Zoom_Out_Button_Tooltip");
			PlayerDashboardInformationUI.zoomOutButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton2 = PlayerDashboardInformationUI.zoomOutButton;
			if (PlayerDashboardInformationUI.<>f__mg$cache9 == null)
			{
				PlayerDashboardInformationUI.<>f__mg$cache9 = new ClickedButton(PlayerDashboardInformationUI.onClickedZoomOutButton);
			}
			sleekButton2.onClickedButton = PlayerDashboardInformationUI.<>f__mg$cache9;
			PlayerDashboardInformationUI.mapInspect.add(PlayerDashboardInformationUI.zoomOutButton);
			PlayerDashboardInformationUI.centerButton = new SleekButtonIcon((Texture2D)PlayerDashboardInformationUI.icons.load("Center"));
			PlayerDashboardInformationUI.centerButton.positionOffset_X = 5;
			PlayerDashboardInformationUI.centerButton.positionOffset_Y = -30;
			PlayerDashboardInformationUI.centerButton.positionScale_X = 0.5f;
			PlayerDashboardInformationUI.centerButton.positionScale_Y = 1f;
			PlayerDashboardInformationUI.centerButton.sizeOffset_X = -10;
			PlayerDashboardInformationUI.centerButton.sizeOffset_Y = 30;
			PlayerDashboardInformationUI.centerButton.sizeScale_X = 0.25f;
			PlayerDashboardInformationUI.centerButton.text = PlayerDashboardInformationUI.localization.format("Center_Button");
			PlayerDashboardInformationUI.centerButton.tooltip = PlayerDashboardInformationUI.localization.format("Center_Button_Tooltip");
			PlayerDashboardInformationUI.centerButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton3 = PlayerDashboardInformationUI.centerButton;
			if (PlayerDashboardInformationUI.<>f__mg$cacheA == null)
			{
				PlayerDashboardInformationUI.<>f__mg$cacheA = new ClickedButton(PlayerDashboardInformationUI.onClickedCenterButton);
			}
			sleekButton3.onClickedButton = PlayerDashboardInformationUI.<>f__mg$cacheA;
			PlayerDashboardInformationUI.mapInspect.add(PlayerDashboardInformationUI.centerButton);
			PlayerDashboardInformationUI.mapButtonState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(PlayerDashboardInformationUI.localization.format("Chart")),
				new GUIContent(PlayerDashboardInformationUI.localization.format("Satellite"))
			});
			PlayerDashboardInformationUI.mapButtonState.positionOffset_X = 5;
			PlayerDashboardInformationUI.mapButtonState.positionOffset_Y = -30;
			PlayerDashboardInformationUI.mapButtonState.positionScale_X = 0.75f;
			PlayerDashboardInformationUI.mapButtonState.positionScale_Y = 1f;
			PlayerDashboardInformationUI.mapButtonState.sizeOffset_X = -5;
			PlayerDashboardInformationUI.mapButtonState.sizeOffset_Y = 30;
			PlayerDashboardInformationUI.mapButtonState.sizeScale_X = 0.25f;
			SleekButtonState sleekButtonState = PlayerDashboardInformationUI.mapButtonState;
			if (PlayerDashboardInformationUI.<>f__mg$cacheB == null)
			{
				PlayerDashboardInformationUI.<>f__mg$cacheB = new SwappedState(PlayerDashboardInformationUI.onSwappedMapState);
			}
			sleekButtonState.onSwappedState = PlayerDashboardInformationUI.<>f__mg$cacheB;
			PlayerDashboardInformationUI.mapInspect.add(PlayerDashboardInformationUI.mapButtonState);
			PlayerDashboardInformationUI.headerButtonsContainer = new Sleek();
			PlayerDashboardInformationUI.headerButtonsContainer.positionOffset_X = 5;
			PlayerDashboardInformationUI.headerButtonsContainer.positionOffset_Y = 10;
			PlayerDashboardInformationUI.headerButtonsContainer.positionScale_X = 0.6f;
			PlayerDashboardInformationUI.headerButtonsContainer.sizeOffset_X = -15;
			PlayerDashboardInformationUI.headerButtonsContainer.sizeOffset_Y = 50;
			PlayerDashboardInformationUI.headerButtonsContainer.sizeScale_X = 0.4f;
			PlayerDashboardInformationUI.backdropBox.add(PlayerDashboardInformationUI.headerButtonsContainer);
			PlayerDashboardInformationUI.questsButton = new SleekButtonIcon((Texture2D)PlayerDashboardInformationUI.icons.load("Quests"));
			PlayerDashboardInformationUI.questsButton.sizeOffset_X = -5;
			PlayerDashboardInformationUI.questsButton.sizeScale_X = 0.333f;
			PlayerDashboardInformationUI.questsButton.sizeScale_Y = 1f;
			PlayerDashboardInformationUI.questsButton.fontSize = 14;
			PlayerDashboardInformationUI.questsButton.tooltip = PlayerDashboardInformationUI.localization.format("Quests_Tooltip");
			SleekButton sleekButton4 = PlayerDashboardInformationUI.questsButton;
			if (PlayerDashboardInformationUI.<>f__mg$cacheC == null)
			{
				PlayerDashboardInformationUI.<>f__mg$cacheC = new ClickedButton(PlayerDashboardInformationUI.onClickedQuestsButton);
			}
			sleekButton4.onClickedButton = PlayerDashboardInformationUI.<>f__mg$cacheC;
			PlayerDashboardInformationUI.headerButtonsContainer.add(PlayerDashboardInformationUI.questsButton);
			PlayerDashboardInformationUI.groupsButton = new SleekButtonIcon((Texture2D)PlayerDashboardInformationUI.icons.load("Groups"));
			PlayerDashboardInformationUI.groupsButton.positionOffset_X = 5;
			PlayerDashboardInformationUI.groupsButton.positionScale_X = 0.333f;
			PlayerDashboardInformationUI.groupsButton.sizeOffset_X = -10;
			PlayerDashboardInformationUI.groupsButton.sizeScale_X = 0.334f;
			PlayerDashboardInformationUI.groupsButton.sizeScale_Y = 1f;
			PlayerDashboardInformationUI.groupsButton.fontSize = 14;
			PlayerDashboardInformationUI.groupsButton.text = PlayerDashboardInformationUI.localization.format("Groups");
			PlayerDashboardInformationUI.groupsButton.tooltip = PlayerDashboardInformationUI.localization.format("Groups_Tooltip");
			SleekButton sleekButton5 = PlayerDashboardInformationUI.groupsButton;
			if (PlayerDashboardInformationUI.<>f__mg$cacheD == null)
			{
				PlayerDashboardInformationUI.<>f__mg$cacheD = new ClickedButton(PlayerDashboardInformationUI.onClickedGroupsButton);
			}
			sleekButton5.onClickedButton = PlayerDashboardInformationUI.<>f__mg$cacheD;
			PlayerDashboardInformationUI.headerButtonsContainer.add(PlayerDashboardInformationUI.groupsButton);
			PlayerDashboardInformationUI.playersButton = new SleekButtonIcon((Texture2D)PlayerDashboardInformationUI.icons.load("Players"));
			PlayerDashboardInformationUI.playersButton.positionOffset_X = 5;
			PlayerDashboardInformationUI.playersButton.positionScale_X = 0.667f;
			PlayerDashboardInformationUI.playersButton.sizeOffset_X = -5;
			PlayerDashboardInformationUI.playersButton.sizeScale_X = 0.333f;
			PlayerDashboardInformationUI.playersButton.sizeScale_Y = 1f;
			PlayerDashboardInformationUI.playersButton.fontSize = 14;
			PlayerDashboardInformationUI.playersButton.tooltip = PlayerDashboardInformationUI.localization.format("Players_Tooltip");
			SleekButton sleekButton6 = PlayerDashboardInformationUI.playersButton;
			if (PlayerDashboardInformationUI.<>f__mg$cacheE == null)
			{
				PlayerDashboardInformationUI.<>f__mg$cacheE = new ClickedButton(PlayerDashboardInformationUI.onClickedPlayersButton);
			}
			sleekButton6.onClickedButton = PlayerDashboardInformationUI.<>f__mg$cacheE;
			PlayerDashboardInformationUI.headerButtonsContainer.add(PlayerDashboardInformationUI.playersButton);
			PlayerDashboardInformationUI.questsBox = new SleekScrollBox();
			PlayerDashboardInformationUI.questsBox.positionOffset_X = 5;
			PlayerDashboardInformationUI.questsBox.positionOffset_Y = 70;
			PlayerDashboardInformationUI.questsBox.positionScale_X = 0.6f;
			PlayerDashboardInformationUI.questsBox.sizeOffset_X = -15;
			PlayerDashboardInformationUI.questsBox.sizeOffset_Y = -80;
			PlayerDashboardInformationUI.questsBox.sizeScale_X = 0.4f;
			PlayerDashboardInformationUI.questsBox.sizeScale_Y = 1f;
			PlayerDashboardInformationUI.backdropBox.add(PlayerDashboardInformationUI.questsBox);
			PlayerDashboardInformationUI.questsBox.isVisible = false;
			PlayerDashboardInformationUI.groupsBox = new SleekScrollBox();
			PlayerDashboardInformationUI.groupsBox.positionOffset_X = 5;
			PlayerDashboardInformationUI.groupsBox.positionOffset_Y = 70;
			PlayerDashboardInformationUI.groupsBox.positionScale_X = 0.6f;
			PlayerDashboardInformationUI.groupsBox.sizeOffset_X = -15;
			PlayerDashboardInformationUI.groupsBox.sizeOffset_Y = -80;
			PlayerDashboardInformationUI.groupsBox.sizeScale_X = 0.4f;
			PlayerDashboardInformationUI.groupsBox.sizeScale_Y = 1f;
			PlayerDashboardInformationUI.backdropBox.add(PlayerDashboardInformationUI.groupsBox);
			PlayerDashboardInformationUI.groupsBox.isVisible = false;
			PlayerDashboardInformationUI.playersBox = new SleekScrollBox();
			PlayerDashboardInformationUI.playersBox.positionOffset_X = 5;
			PlayerDashboardInformationUI.playersBox.positionOffset_Y = 70;
			PlayerDashboardInformationUI.playersBox.positionScale_X = 0.6f;
			PlayerDashboardInformationUI.playersBox.sizeOffset_X = -15;
			PlayerDashboardInformationUI.playersBox.sizeOffset_Y = -80;
			PlayerDashboardInformationUI.playersBox.sizeScale_X = 0.4f;
			PlayerDashboardInformationUI.playersBox.sizeScale_Y = 1f;
			PlayerDashboardInformationUI.backdropBox.add(PlayerDashboardInformationUI.playersBox);
			PlayerDashboardInformationUI.playersBox.isVisible = true;
			SleekWindow window = PlayerUI.window;
			Delegate onClickedMouseStarted = window.onClickedMouseStarted;
			if (PlayerDashboardInformationUI.<>f__mg$cacheF == null)
			{
				PlayerDashboardInformationUI.<>f__mg$cacheF = new ClickedMouseStarted(PlayerDashboardInformationUI.onClickedMouseStarted);
			}
			window.onClickedMouseStarted = (ClickedMouseStarted)Delegate.Combine(onClickedMouseStarted, PlayerDashboardInformationUI.<>f__mg$cacheF);
			SleekWindow window2 = PlayerUI.window;
			Delegate onClickedMouseStopped = window2.onClickedMouseStopped;
			if (PlayerDashboardInformationUI.<>f__mg$cache10 == null)
			{
				PlayerDashboardInformationUI.<>f__mg$cache10 = new ClickedMouseStopped(PlayerDashboardInformationUI.onClickedMouseStopped);
			}
			window2.onClickedMouseStopped = (ClickedMouseStopped)Delegate.Combine(onClickedMouseStopped, PlayerDashboardInformationUI.<>f__mg$cache10);
			SleekWindow window3 = PlayerUI.window;
			Delegate onMovedMouse = window3.onMovedMouse;
			if (PlayerDashboardInformationUI.<>f__mg$cache11 == null)
			{
				PlayerDashboardInformationUI.<>f__mg$cache11 = new MovedMouse(PlayerDashboardInformationUI.onMovedMouse);
			}
			window3.onMovedMouse = (MovedMouse)Delegate.Combine(onMovedMouse, PlayerDashboardInformationUI.<>f__mg$cache11);
			if (PlayerDashboardInformationUI.<>f__mg$cache12 == null)
			{
				PlayerDashboardInformationUI.<>f__mg$cache12 = new IsBlindfoldedChangedHandler(PlayerDashboardInformationUI.handleIsBlindfoldedChanged);
			}
			PlayerUI.isBlindfoldedChanged += PlayerDashboardInformationUI.<>f__mg$cache12;
			Player player = Player.player;
			Delegate onPlayerTeleported = player.onPlayerTeleported;
			if (PlayerDashboardInformationUI.<>f__mg$cache13 == null)
			{
				PlayerDashboardInformationUI.<>f__mg$cache13 = new PlayerTeleported(PlayerDashboardInformationUI.onPlayerTeleported);
			}
			player.onPlayerTeleported = (PlayerTeleported)Delegate.Combine(onPlayerTeleported, PlayerDashboardInformationUI.<>f__mg$cache13);
			if (PlayerDashboardInformationUI.<>f__mg$cache14 == null)
			{
				PlayerDashboardInformationUI.<>f__mg$cache14 = new GroupUpdatedHandler(PlayerDashboardInformationUI.handleGroupUpdated);
			}
			PlayerQuests.groupUpdated = PlayerDashboardInformationUI.<>f__mg$cache14;
			if (PlayerDashboardInformationUI.<>f__mg$cache15 == null)
			{
				PlayerDashboardInformationUI.<>f__mg$cache15 = new GroupInfoReadyHandler(PlayerDashboardInformationUI.handleGroupInfoReady);
			}
			GroupManager.groupInfoReady += PlayerDashboardInformationUI.<>f__mg$cache15;
			PlayerDashboardInformationUI.onPlayerTeleported(Player.player, Player.player.transform.position);
			if (Level.info != null && ReadWrite.fileExists(Level.info.path + "/Chart.png", false, false))
			{
				byte[] data = ReadWrite.readBytes(Level.info.path + "/Chart.png", false, false);
				PlayerDashboardInformationUI.chartTexture = new Texture2D((int)Level.size, (int)Level.size, TextureFormat.ARGB32, false, true);
				PlayerDashboardInformationUI.chartTexture.name = "Chart_" + Level.info.name;
				PlayerDashboardInformationUI.chartTexture.filterMode = FilterMode.Trilinear;
				PlayerDashboardInformationUI.chartTexture.LoadImage(data);
			}
			else
			{
				PlayerDashboardInformationUI.chartTexture = null;
			}
			if (Level.info != null && ReadWrite.fileExists(Level.info.path + "/Map.png", false, false))
			{
				byte[] data2 = ReadWrite.readBytes(Level.info.path + "/Map.png", false, false);
				PlayerDashboardInformationUI.mapTexture = new Texture2D((int)Level.size, (int)Level.size, TextureFormat.ARGB32, false, true);
				PlayerDashboardInformationUI.mapTexture.name = "Satellite_" + Level.info.name;
				PlayerDashboardInformationUI.mapTexture.filterMode = FilterMode.Trilinear;
				PlayerDashboardInformationUI.mapTexture.LoadImage(data2);
			}
			else
			{
				PlayerDashboardInformationUI.mapTexture = null;
			}
			PlayerDashboardInformationUI.staticTexture = (Texture2D)Resources.Load("Level/Map");
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x0018EA7C File Offset: 0x0018CE7C
		private static void refreshStaticMap(int view)
		{
			PlayerDashboardInformationUI.mapStaticContainer.remove();
			if (PlayerDashboardInformationUI.mapImage.texture != null && PlayerDashboardInformationUI.mapImage.shouldDestroyTexture)
			{
				UnityEngine.Object.Destroy(PlayerDashboardInformationUI.mapImage.texture);
				PlayerDashboardInformationUI.mapImage.texture = null;
			}
			if (view == 0)
			{
				if (PlayerDashboardInformationUI.chartTexture != null && !PlayerUI.isBlindfolded && PlayerDashboardInformationUI.hasChart)
				{
					PlayerDashboardInformationUI.mapImage.texture = PlayerDashboardInformationUI.chartTexture;
					PlayerDashboardInformationUI.noLabel.isVisible = false;
				}
				else
				{
					PlayerDashboardInformationUI.mapImage.texture = PlayerDashboardInformationUI.staticTexture;
					PlayerDashboardInformationUI.noLabel.text = PlayerDashboardInformationUI.localization.format("No_Chart");
					PlayerDashboardInformationUI.noLabel.isVisible = true;
				}
			}
			else if (PlayerDashboardInformationUI.mapTexture != null && !PlayerUI.isBlindfolded && PlayerDashboardInformationUI.hasGPS)
			{
				PlayerDashboardInformationUI.mapImage.texture = PlayerDashboardInformationUI.mapTexture;
				PlayerDashboardInformationUI.noLabel.isVisible = false;
			}
			else
			{
				PlayerDashboardInformationUI.mapImage.texture = PlayerDashboardInformationUI.staticTexture;
				PlayerDashboardInformationUI.noLabel.text = PlayerDashboardInformationUI.localization.format("No_GPS");
				PlayerDashboardInformationUI.noLabel.isVisible = true;
			}
			if (!PlayerDashboardInformationUI.noLabel.isVisible)
			{
				for (int i = 0; i < LevelNodes.nodes.Count; i++)
				{
					Node node = LevelNodes.nodes[i];
					if (node.type == ENodeType.LOCATION)
					{
						SleekLabel sleekLabel = new SleekLabel();
						sleekLabel.positionOffset_X = -200;
						sleekLabel.positionOffset_Y = -30;
						sleekLabel.positionScale_X = node.point.x / (float)(Level.size - Level.border * 2) + 0.5f;
						sleekLabel.positionScale_Y = 0.5f - node.point.z / (float)(Level.size - Level.border * 2);
						sleekLabel.sizeOffset_X = 400;
						sleekLabel.sizeOffset_Y = 60;
						sleekLabel.text = ((LocationNode)node).name;
						sleekLabel.foregroundTint = ESleekTint.FONT;
						PlayerDashboardInformationUI.mapStaticContainer.add(sleekLabel);
					}
				}
			}
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x0018ECB0 File Offset: 0x0018D0B0
		public static void refreshDynamicMap()
		{
			PlayerDashboardInformationUI.mapDynamicContainer.remove();
			if (!PlayerDashboardInformationUI.active)
			{
				return;
			}
			if (!PlayerDashboardInformationUI.noLabel.isVisible && Provider.modeConfigData.Gameplay.Group_Map)
			{
				if (LevelManager.levelType == ELevelType.ARENA)
				{
					SleekImageTexture sleekImageTexture = new SleekImageTexture((Texture2D)PlayerDashboardInformationUI.icons.load("Arena_Area"));
					sleekImageTexture.positionScale_X = LevelManager.arenaTargetCenter.x / (float)(Level.size - Level.border * 2) + 0.5f - LevelManager.arenaTargetRadius / (float)(Level.size - Level.border * 2);
					sleekImageTexture.positionScale_Y = 0.5f - LevelManager.arenaTargetCenter.z / (float)(Level.size - Level.border * 2) - LevelManager.arenaTargetRadius / (float)(Level.size - Level.border * 2);
					sleekImageTexture.sizeScale_X = LevelManager.arenaTargetRadius * 2f / (float)(Level.size - Level.border * 2);
					sleekImageTexture.sizeScale_Y = LevelManager.arenaTargetRadius * 2f / (float)(Level.size - Level.border * 2);
					sleekImageTexture.backgroundColor = new Color(1f, 1f, 0f, 0.5f);
					PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture);
					SleekImageTexture sleekImageTexture2 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
					sleekImageTexture2.positionScale_Y = sleekImageTexture.positionScale_Y;
					sleekImageTexture2.sizeScale_X = sleekImageTexture.positionScale_X;
					sleekImageTexture2.sizeScale_Y = sleekImageTexture.sizeScale_Y;
					sleekImageTexture2.backgroundColor = new Color(1f, 1f, 0f, 0.5f);
					PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture2);
					SleekImageTexture sleekImageTexture3 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
					sleekImageTexture3.positionScale_X = sleekImageTexture.positionScale_X + sleekImageTexture.sizeScale_X;
					sleekImageTexture3.positionScale_Y = sleekImageTexture.positionScale_Y;
					sleekImageTexture3.sizeScale_X = 1f - sleekImageTexture.positionScale_X - sleekImageTexture.sizeScale_X;
					sleekImageTexture3.sizeScale_Y = sleekImageTexture.sizeScale_Y;
					sleekImageTexture3.backgroundColor = new Color(1f, 1f, 0f, 0.5f);
					PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture3);
					SleekImageTexture sleekImageTexture4 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
					sleekImageTexture4.sizeScale_X = 1f;
					sleekImageTexture4.sizeScale_Y = sleekImageTexture.positionScale_Y;
					sleekImageTexture4.backgroundColor = new Color(1f, 1f, 0f, 0.5f);
					PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture4);
					SleekImageTexture sleekImageTexture5 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
					sleekImageTexture5.positionScale_Y = sleekImageTexture.positionScale_Y + sleekImageTexture.sizeScale_Y;
					sleekImageTexture5.sizeScale_X = 1f;
					sleekImageTexture5.sizeScale_Y = 1f - sleekImageTexture.positionScale_Y - sleekImageTexture.sizeScale_Y;
					sleekImageTexture5.backgroundColor = new Color(1f, 1f, 0f, 0.5f);
					PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture5);
					SleekImageTexture sleekImageTexture6 = new SleekImageTexture((Texture2D)PlayerDashboardInformationUI.icons.load("Arena_Area"));
					sleekImageTexture6.positionScale_X = LevelManager.arenaCurrentCenter.x / (float)(Level.size - Level.border * 2) + 0.5f - LevelManager.arenaCurrentRadius / (float)(Level.size - Level.border * 2);
					sleekImageTexture6.positionScale_Y = 0.5f - LevelManager.arenaCurrentCenter.z / (float)(Level.size - Level.border * 2) - LevelManager.arenaCurrentRadius / (float)(Level.size - Level.border * 2);
					sleekImageTexture6.sizeScale_X = LevelManager.arenaCurrentRadius * 2f / (float)(Level.size - Level.border * 2);
					sleekImageTexture6.sizeScale_Y = LevelManager.arenaCurrentRadius * 2f / (float)(Level.size - Level.border * 2);
					PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture6);
					SleekImageTexture sleekImageTexture7 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
					sleekImageTexture7.positionScale_Y = sleekImageTexture6.positionScale_Y;
					sleekImageTexture7.sizeScale_X = sleekImageTexture6.positionScale_X;
					sleekImageTexture7.sizeScale_Y = sleekImageTexture6.sizeScale_Y;
					PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture7);
					SleekImageTexture sleekImageTexture8 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
					sleekImageTexture8.positionScale_X = sleekImageTexture6.positionScale_X + sleekImageTexture6.sizeScale_X;
					sleekImageTexture8.positionScale_Y = sleekImageTexture6.positionScale_Y;
					sleekImageTexture8.sizeScale_X = 1f - sleekImageTexture6.positionScale_X - sleekImageTexture6.sizeScale_X;
					sleekImageTexture8.sizeScale_Y = sleekImageTexture6.sizeScale_Y;
					PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture8);
					SleekImageTexture sleekImageTexture9 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
					sleekImageTexture9.sizeScale_X = 1f;
					sleekImageTexture9.sizeScale_Y = sleekImageTexture6.positionScale_Y;
					PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture9);
					SleekImageTexture sleekImageTexture10 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
					sleekImageTexture10.positionScale_Y = sleekImageTexture6.positionScale_Y + sleekImageTexture6.sizeScale_Y;
					sleekImageTexture10.sizeScale_X = 1f;
					sleekImageTexture10.sizeScale_Y = 1f - sleekImageTexture6.positionScale_Y - sleekImageTexture6.sizeScale_Y;
					PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture10);
				}
				foreach (SteamPlayer steamPlayer in Provider.clients)
				{
					if (!(steamPlayer.model == null))
					{
						PlayerQuests quests = steamPlayer.player.quests;
						if (!(steamPlayer.playerID.steamID != Provider.client) || quests.isMemberOfSameGroupAs(Player.player))
						{
							if (quests.isMarkerPlaced)
							{
								SleekImageTexture sleekImageTexture11 = new SleekImageTexture((Texture2D)PlayerDashboardInformationUI.icons.load("Marker"));
								sleekImageTexture11.positionScale_X = quests.markerPosition.x / (float)(Level.size - Level.border * 2) + 0.5f;
								sleekImageTexture11.positionScale_Y = 0.5f - quests.markerPosition.z / (float)(Level.size - Level.border * 2);
								sleekImageTexture11.positionOffset_X = -10;
								sleekImageTexture11.positionOffset_Y = -10;
								sleekImageTexture11.sizeOffset_X = 20;
								sleekImageTexture11.sizeOffset_Y = 20;
								sleekImageTexture11.backgroundColor = steamPlayer.markerColor;
								if (string.IsNullOrEmpty(steamPlayer.playerID.nickName))
								{
									sleekImageTexture11.addLabel(steamPlayer.playerID.characterName, ESleekSide.RIGHT);
								}
								else
								{
									sleekImageTexture11.addLabel(steamPlayer.playerID.nickName, ESleekSide.RIGHT);
								}
								PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture11);
							}
						}
					}
				}
				for (int i = 0; i < Provider.clients.Count; i++)
				{
					SteamPlayer steamPlayer2 = Provider.clients[i];
					if (steamPlayer2.playerID.steamID != Provider.client && steamPlayer2.model != null && steamPlayer2.player.quests.isMemberOfSameGroupAs(Player.player))
					{
						SleekImageTexture sleekImageTexture12 = new SleekImageTexture();
						sleekImageTexture12.positionOffset_X = -10;
						sleekImageTexture12.positionOffset_Y = -10;
						sleekImageTexture12.positionScale_X = steamPlayer2.player.transform.position.x / (float)(Level.size - Level.border * 2) + 0.5f;
						sleekImageTexture12.positionScale_Y = 0.5f - steamPlayer2.player.transform.position.z / (float)(Level.size - Level.border * 2);
						sleekImageTexture12.sizeOffset_X = 20;
						sleekImageTexture12.sizeOffset_Y = 20;
						if (!OptionsSettings.streamer)
						{
							sleekImageTexture12.texture = Provider.provider.communityService.getIcon(steamPlayer2.playerID.steamID);
						}
						if (string.IsNullOrEmpty(steamPlayer2.playerID.nickName))
						{
							sleekImageTexture12.addLabel(steamPlayer2.playerID.characterName, ESleekSide.RIGHT);
						}
						else
						{
							sleekImageTexture12.addLabel(steamPlayer2.playerID.nickName, ESleekSide.RIGHT);
						}
						sleekImageTexture12.shouldDestroyTexture = true;
						PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture12);
					}
				}
				if (Player.player != null)
				{
					SleekImageTexture sleekImageTexture13 = new SleekImageTexture();
					sleekImageTexture13.positionOffset_X = -10;
					sleekImageTexture13.positionOffset_Y = -10;
					sleekImageTexture13.positionScale_X = Player.player.transform.position.x / (float)(Level.size - Level.border * 2) + 0.5f;
					sleekImageTexture13.positionScale_Y = 0.5f - Player.player.transform.position.z / (float)(Level.size - Level.border * 2);
					sleekImageTexture13.sizeOffset_X = 20;
					sleekImageTexture13.sizeOffset_Y = 20;
					sleekImageTexture13.isAngled = true;
					sleekImageTexture13.angle = Player.player.transform.rotation.eulerAngles.y;
					sleekImageTexture13.texture = (Texture2D)PlayerDashboardInformationUI.icons.load("Player");
					sleekImageTexture13.backgroundTint = ESleekTint.FOREGROUND;
					if (string.IsNullOrEmpty(Characters.active.nick))
					{
						sleekImageTexture13.addLabel(Characters.active.name, ESleekSide.RIGHT);
					}
					else
					{
						sleekImageTexture13.addLabel(Characters.active.nick, ESleekSide.RIGHT);
					}
					PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture13);
				}
			}
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x0018F680 File Offset: 0x0018DA80
		public static void open()
		{
			if (PlayerDashboardInformationUI.active)
			{
				return;
			}
			PlayerDashboardInformationUI.active = true;
			PlayerDashboardInformationUI.hasChart = (Player.player.inventory.has(1175) != null || Provider.modeConfigData.Gameplay.Chart || Level.info.type != ELevelType.SURVIVAL);
			PlayerDashboardInformationUI.hasGPS = (Player.player.inventory.has(1176) != null || Provider.modeConfigData.Gameplay.Satellite || Level.info.type != ELevelType.SURVIVAL);
			if (PlayerDashboardInformationUI.hasChart && !PlayerDashboardInformationUI.hasGPS)
			{
				PlayerDashboardInformationUI.mapButtonState.state = 0;
			}
			if (PlayerDashboardInformationUI.hasGPS && !PlayerDashboardInformationUI.hasChart)
			{
				PlayerDashboardInformationUI.mapButtonState.state = 1;
			}
			PlayerDashboardInformationUI.refreshStaticMap(PlayerDashboardInformationUI.mapButtonState.state);
			PlayerDashboardInformationUI.refreshDynamicMap();
			PlayerDashboardInformationUI.questsButton.text = PlayerDashboardInformationUI.localization.format("Quests", new object[]
			{
				Player.player.quests.questsList.Count
			});
			if (OptionsSettings.streamer)
			{
				PlayerDashboardInformationUI.playersButton.text = PlayerDashboardInformationUI.localization.format("Streamer");
			}
			else
			{
				PlayerDashboardInformationUI.playersButton.text = PlayerDashboardInformationUI.localization.format("Players", new object[]
				{
					Provider.clients.Count,
					Provider.maxPlayers
				});
			}
			PlayerDashboardInformationUI.EInfoTab einfoTab = PlayerDashboardInformationUI.tab;
			if (einfoTab != PlayerDashboardInformationUI.EInfoTab.GROUPS)
			{
				if (einfoTab != PlayerDashboardInformationUI.EInfoTab.QUESTS)
				{
					if (einfoTab == PlayerDashboardInformationUI.EInfoTab.PLAYERS)
					{
						PlayerDashboardInformationUI.openPlayers();
					}
				}
				else
				{
					PlayerDashboardInformationUI.openQuests();
				}
			}
			else
			{
				PlayerDashboardInformationUI.openGroups();
			}
			PlayerDashboardInformationUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x0018F876 File Offset: 0x0018DC76
		public static void close()
		{
			if (!PlayerDashboardInformationUI.active)
			{
				return;
			}
			PlayerDashboardInformationUI.active = false;
			PlayerDashboardInformationUI.isDragging = false;
			PlayerDashboardInformationUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x0018F8AC File Offset: 0x0018DCAC
		public static void openQuests()
		{
			PlayerDashboardInformationUI.tab = PlayerDashboardInformationUI.EInfoTab.QUESTS;
			PlayerDashboardInformationUI.questsBox.remove();
			for (int i = 0; i < Player.player.quests.questsList.Count; i++)
			{
				PlayerQuest playerQuest = Player.player.quests.questsList[i];
				if (playerQuest != null && playerQuest.asset != null)
				{
					bool flag = playerQuest.asset.areConditionsMet(Player.player);
					SleekButton sleekButton = new SleekButton();
					sleekButton.positionOffset_Y = i * 60;
					sleekButton.sizeOffset_X = -30;
					sleekButton.sizeOffset_Y = 50;
					sleekButton.sizeScale_X = 1f;
					SleekButton sleekButton2 = sleekButton;
					if (PlayerDashboardInformationUI.<>f__mg$cache0 == null)
					{
						PlayerDashboardInformationUI.<>f__mg$cache0 = new ClickedButton(PlayerDashboardInformationUI.onClickedQuestButton);
					}
					sleekButton2.onClickedButton = PlayerDashboardInformationUI.<>f__mg$cache0;
					PlayerDashboardInformationUI.questsBox.add(sleekButton);
					sleekButton.add(new SleekImageTexture((Texture2D)PlayerDashboardInformationUI.icons.load((!flag) ? "Incomplete" : "Complete"))
					{
						positionOffset_X = 5,
						positionOffset_Y = 5,
						sizeOffset_X = 40,
						sizeOffset_Y = 40
					});
					sleekButton.add(new SleekLabel
					{
						positionOffset_X = 50,
						sizeOffset_X = -55,
						sizeScale_X = 1f,
						sizeScale_Y = 1f,
						fontAlignment = TextAnchor.MiddleLeft,
						foregroundTint = ESleekTint.NONE,
						isRich = true,
						fontSize = 14,
						text = playerQuest.asset.questName
					});
				}
			}
			PlayerDashboardInformationUI.questsBox.area = new Rect(0f, 0f, 5f, (float)(Player.player.quests.questsList.Count * 60 - 10));
			PlayerDashboardInformationUI.updateTabs();
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x0018FA84 File Offset: 0x0018DE84
		private static void onClickedTuneButton(SleekButton button)
		{
			uint num = (uint)(PlayerDashboardInformationUI.radioFrequencyField.state * 1000.0);
			if (num < 300000u)
			{
				num = 300000u;
			}
			else if (num > 900000u)
			{
				num = 900000u;
			}
			PlayerDashboardInformationUI.radioFrequencyField.state = num / 1000.0;
			Player.player.quests.sendSetRadioFrequency(num);
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x0018FAF5 File Offset: 0x0018DEF5
		private static void onClickedResetButton(SleekButton button)
		{
			PlayerDashboardInformationUI.radioFrequencyField.state = PlayerQuests.DEFAULT_RADIO_FREQUENCY / 1000.0;
			PlayerDashboardInformationUI.onClickedTuneButton(button);
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x0018FB18 File Offset: 0x0018DF18
		private static void onClickedRenameButton(SleekButton button)
		{
			Player.player.quests.sendRenameGroup(PlayerDashboardInformationUI.groupNameField.text);
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x0018FB33 File Offset: 0x0018DF33
		private static void onClickedMainGroupButton(SleekButton button)
		{
			Player.player.quests.sendJoinGroupInvite(Characters.active.group);
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x0018FB4E File Offset: 0x0018DF4E
		private static void onClickedLeaveGroupButton(SleekButton button)
		{
			Player.player.quests.sendLeaveGroup();
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x0018FB5F File Offset: 0x0018DF5F
		private static void onClickedDeleteGroupButton(SleekButton button)
		{
			Player.player.quests.sendDeleteGroup();
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x0018FB70 File Offset: 0x0018DF70
		private static void onClickedCreateGroupButton(SleekButton button)
		{
			Player.player.quests.sendCreateGroup();
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x0018FB84 File Offset: 0x0018DF84
		private static void refreshGroups()
		{
			if (!PlayerDashboardInformationUI.active)
			{
				return;
			}
			PlayerDashboardInformationUI.groupsBox.remove();
			int num = 0;
			PlayerDashboardInformationUI.radioFrequencyField = new SleekDoubleField();
			PlayerDashboardInformationUI.radioFrequencyField.positionOffset_X = 125;
			PlayerDashboardInformationUI.radioFrequencyField.sizeOffset_X = -255;
			PlayerDashboardInformationUI.radioFrequencyField.positionOffset_Y = num;
			PlayerDashboardInformationUI.radioFrequencyField.sizeOffset_Y = 30;
			PlayerDashboardInformationUI.radioFrequencyField.sizeScale_X = 1f;
			PlayerDashboardInformationUI.radioFrequencyField.state = Player.player.quests.radioFrequency / 1000.0;
			PlayerDashboardInformationUI.groupsBox.add(PlayerDashboardInformationUI.radioFrequencyField);
			num += 30;
			SleekBox sleekBox = new SleekBox();
			sleekBox.positionOffset_X = -125;
			sleekBox.sizeOffset_X = 125;
			sleekBox.sizeScale_Y = 1f;
			sleekBox.text = PlayerDashboardInformationUI.localization.format("Radio_Frequency_Label");
			PlayerDashboardInformationUI.radioFrequencyField.add(sleekBox);
			SleekButton sleekButton = new SleekButton();
			sleekButton.positionScale_X = 1f;
			sleekButton.sizeOffset_X = 50;
			sleekButton.sizeScale_Y = 1f;
			sleekButton.text = PlayerDashboardInformationUI.localization.format("Radio_Frequency_Tune");
			sleekButton.tooltip = PlayerDashboardInformationUI.localization.format("Radio_Frequency_Tune_Tooltip");
			SleekButton sleekButton2 = sleekButton;
			Delegate onClickedButton = sleekButton2.onClickedButton;
			if (PlayerDashboardInformationUI.<>f__mg$cache1 == null)
			{
				PlayerDashboardInformationUI.<>f__mg$cache1 = new ClickedButton(PlayerDashboardInformationUI.onClickedTuneButton);
			}
			sleekButton2.onClickedButton = (ClickedButton)Delegate.Combine(onClickedButton, PlayerDashboardInformationUI.<>f__mg$cache1);
			PlayerDashboardInformationUI.radioFrequencyField.add(sleekButton);
			SleekButton sleekButton3 = new SleekButton();
			sleekButton3.positionOffset_X = 50;
			sleekButton3.positionScale_X = 1f;
			sleekButton3.sizeOffset_X = 50;
			sleekButton3.sizeScale_Y = 1f;
			sleekButton3.text = PlayerDashboardInformationUI.localization.format("Radio_Frequency_Reset");
			sleekButton3.tooltip = PlayerDashboardInformationUI.localization.format("Radio_Frequency_Reset_Tooltip");
			SleekButton sleekButton4 = sleekButton3;
			Delegate onClickedButton2 = sleekButton4.onClickedButton;
			if (PlayerDashboardInformationUI.<>f__mg$cache2 == null)
			{
				PlayerDashboardInformationUI.<>f__mg$cache2 = new ClickedButton(PlayerDashboardInformationUI.onClickedResetButton);
			}
			sleekButton4.onClickedButton = (ClickedButton)Delegate.Combine(onClickedButton2, PlayerDashboardInformationUI.<>f__mg$cache2);
			PlayerDashboardInformationUI.radioFrequencyField.add(sleekButton3);
			PlayerQuests quests = Player.player.quests;
			if (quests.isMemberOfAGroup)
			{
				if (Characters.active.group == quests.groupID)
				{
					SteamGroup cachedGroup = Provider.provider.communityService.getCachedGroup(Characters.active.group);
					if (cachedGroup != null)
					{
						SleekBoxIcon sleekBoxIcon = new SleekBoxIcon(cachedGroup.icon, 40);
						sleekBoxIcon.positionOffset_Y = num;
						sleekBoxIcon.sizeOffset_X = -30;
						sleekBoxIcon.sizeOffset_Y = 50;
						sleekBoxIcon.sizeScale_X = 1f;
						sleekBoxIcon.text = cachedGroup.name;
						PlayerDashboardInformationUI.groupsBox.add(sleekBoxIcon);
						num += 50;
					}
				}
				else
				{
					GroupInfo groupInfo = GroupManager.getGroupInfo(quests.groupID);
					string text = (groupInfo == null) ? quests.groupID.ToString() : groupInfo.name;
					Sleek sleek;
					if (quests.groupRank == EPlayerGroupRank.OWNER)
					{
						PlayerDashboardInformationUI.groupNameField = new SleekField();
						PlayerDashboardInformationUI.groupNameField.maxLength = 32;
						PlayerDashboardInformationUI.groupNameField.text = text;
						sleek = PlayerDashboardInformationUI.groupNameField;
						sleek.sizeOffset_X = -130;
						SleekButton sleekButton5 = new SleekButton();
						sleekButton5.positionScale_X = 1f;
						sleekButton5.sizeOffset_X = 100;
						sleekButton5.sizeScale_Y = 1f;
						sleekButton5.text = PlayerDashboardInformationUI.localization.format("Group_Rename");
						sleekButton5.tooltip = PlayerDashboardInformationUI.localization.format("Group_Rename_Tooltip");
						SleekButton sleekButton6 = sleekButton5;
						Delegate onClickedButton3 = sleekButton6.onClickedButton;
						if (PlayerDashboardInformationUI.<>f__mg$cache3 == null)
						{
							PlayerDashboardInformationUI.<>f__mg$cache3 = new ClickedButton(PlayerDashboardInformationUI.onClickedRenameButton);
						}
						sleekButton6.onClickedButton = (ClickedButton)Delegate.Combine(onClickedButton3, PlayerDashboardInformationUI.<>f__mg$cache3);
						PlayerDashboardInformationUI.groupNameField.add(sleekButton5);
					}
					else
					{
						sleek = new SleekBox
						{
							text = text
						};
						sleek.sizeOffset_X = -30;
					}
					sleek.positionOffset_Y = num;
					sleek.sizeOffset_Y = 30;
					sleek.sizeScale_X = 1f;
					PlayerDashboardInformationUI.groupsBox.add(sleek);
					num += 30;
					if (quests.useMaxGroupMembersLimit)
					{
						SleekBox sleekBox2 = new SleekBox();
						sleekBox2.positionOffset_Y = num;
						sleekBox2.sizeOffset_X = -30;
						sleekBox2.sizeOffset_Y = 30;
						sleekBox2.sizeScale_X = 1f;
						sleekBox2.text = PlayerDashboardInformationUI.localization.format("Group_Members", new object[]
						{
							groupInfo.members,
							Provider.modeConfigData.Gameplay.Max_Group_Members
						});
						PlayerDashboardInformationUI.groupsBox.add(sleekBox2);
						num += 30;
					}
				}
				if (quests.hasPermissionToLeaveGroup)
				{
					SleekButtonIcon sleekButtonIcon = new SleekButtonIcon((Texture2D)MenuWorkshopEditorUI.icons.load("Remove"));
					sleekButtonIcon.positionOffset_Y = num;
					sleekButtonIcon.sizeOffset_X = -30;
					sleekButtonIcon.sizeOffset_Y = 30;
					sleekButtonIcon.sizeScale_X = 1f;
					sleekButtonIcon.text = PlayerDashboardInformationUI.localization.format("Group_Leave");
					sleekButtonIcon.tooltip = PlayerDashboardInformationUI.localization.format("Group_Leave_Tooltip");
					SleekButtonIcon sleekButtonIcon2 = sleekButtonIcon;
					Delegate onClickedButton4 = sleekButtonIcon2.onClickedButton;
					if (PlayerDashboardInformationUI.<>f__mg$cache4 == null)
					{
						PlayerDashboardInformationUI.<>f__mg$cache4 = new ClickedButton(PlayerDashboardInformationUI.onClickedLeaveGroupButton);
					}
					sleekButtonIcon2.onClickedButton = (ClickedButton)Delegate.Combine(onClickedButton4, PlayerDashboardInformationUI.<>f__mg$cache4);
					PlayerDashboardInformationUI.groupsBox.add(sleekButtonIcon);
					num += 30;
				}
				if (quests.hasPermissionToDeleteGroup)
				{
					SleekButtonIconConfirm sleekButtonIconConfirm = new SleekButtonIconConfirm((Texture2D)MenuWorkshopEditorUI.icons.load("Remove"), PlayerDashboardInformationUI.localization.format("Group_Delete_Confirm"), PlayerDashboardInformationUI.localization.format("Group_Delete_Confirm_Tooltip"), PlayerDashboardInformationUI.localization.format("Group_Delete_Deny"), PlayerDashboardInformationUI.localization.format("Group_Delete_Deny_Tooltip"));
					sleekButtonIconConfirm.positionOffset_Y = num;
					sleekButtonIconConfirm.sizeOffset_X = -30;
					sleekButtonIconConfirm.sizeOffset_Y = 30;
					sleekButtonIconConfirm.sizeScale_X = 1f;
					sleekButtonIconConfirm.text = PlayerDashboardInformationUI.localization.format("Group_Delete");
					sleekButtonIconConfirm.tooltip = PlayerDashboardInformationUI.localization.format("Group_Delete_Tooltip");
					SleekButtonIconConfirm sleekButtonIconConfirm2 = sleekButtonIconConfirm;
					Delegate onConfirmed = sleekButtonIconConfirm2.onConfirmed;
					if (PlayerDashboardInformationUI.<>f__mg$cache5 == null)
					{
						PlayerDashboardInformationUI.<>f__mg$cache5 = new Confirm(PlayerDashboardInformationUI.onClickedDeleteGroupButton);
					}
					sleekButtonIconConfirm2.onConfirmed = (Confirm)Delegate.Combine(onConfirmed, PlayerDashboardInformationUI.<>f__mg$cache5);
					PlayerDashboardInformationUI.groupsBox.add(sleekButtonIconConfirm);
					num += 30;
				}
				foreach (SteamPlayer steamPlayer in Provider.clients)
				{
					if (!(steamPlayer.player == null) && steamPlayer.player.quests.isMemberOfSameGroupAs(Player.player))
					{
						SleekPlayer sleekPlayer = new SleekPlayer(steamPlayer, true, SleekPlayer.ESleekPlayerDisplayContext.GROUP_ROSTER);
						sleekPlayer.positionOffset_Y = num;
						sleekPlayer.sizeOffset_X = -30;
						sleekPlayer.sizeOffset_Y = 50;
						sleekPlayer.sizeScale_X = 1f;
						PlayerDashboardInformationUI.groupsBox.add(sleekPlayer);
						num += 50;
					}
				}
			}
			else
			{
				if (Characters.active.group != CSteamID.Nil && Provider.modeConfigData.Gameplay.Allow_Static_Groups)
				{
					SteamGroup cachedGroup2 = Provider.provider.communityService.getCachedGroup(Characters.active.group);
					if (cachedGroup2 != null)
					{
						SleekButtonIcon sleekButtonIcon3 = new SleekButtonIcon(cachedGroup2.icon, 40);
						sleekButtonIcon3.positionOffset_Y = num;
						sleekButtonIcon3.sizeOffset_X = -30;
						sleekButtonIcon3.sizeOffset_Y = 50;
						sleekButtonIcon3.sizeScale_X = 1f;
						sleekButtonIcon3.text = cachedGroup2.name;
						SleekButton sleekButton7 = sleekButtonIcon3;
						if (PlayerDashboardInformationUI.<>f__mg$cache6 == null)
						{
							PlayerDashboardInformationUI.<>f__mg$cache6 = new ClickedButton(PlayerDashboardInformationUI.onClickedMainGroupButton);
						}
						sleekButton7.onClickedButton = PlayerDashboardInformationUI.<>f__mg$cache6;
						PlayerDashboardInformationUI.groupsBox.add(sleekButtonIcon3);
						num += 50;
					}
				}
				foreach (CSteamID newGroupID in quests.groupInvites)
				{
					PlayerDashboardInformationUI.SleekInviteButton sleekInviteButton = new PlayerDashboardInformationUI.SleekInviteButton(newGroupID);
					sleekInviteButton.positionOffset_Y = num;
					sleekInviteButton.sizeOffset_X = -30;
					sleekInviteButton.sizeOffset_Y = 30;
					sleekInviteButton.sizeScale_X = 1f;
					PlayerDashboardInformationUI.groupsBox.add(sleekInviteButton);
					num += 30;
				}
				if (Player.player.quests.hasPermissionToCreateGroup)
				{
					SleekButtonIcon sleekButtonIcon4 = new SleekButtonIcon((Texture2D)MenuWorkshopEditorUI.icons.load("Add"));
					sleekButtonIcon4.positionOffset_Y = num;
					sleekButtonIcon4.sizeOffset_X = -30;
					sleekButtonIcon4.sizeOffset_Y = 30;
					sleekButtonIcon4.sizeScale_X = 1f;
					sleekButtonIcon4.text = PlayerDashboardInformationUI.localization.format("Group_Create");
					sleekButtonIcon4.tooltip = PlayerDashboardInformationUI.localization.format("Group_Create_Tooltip");
					SleekButtonIcon sleekButtonIcon5 = sleekButtonIcon4;
					Delegate onClickedButton5 = sleekButtonIcon5.onClickedButton;
					if (PlayerDashboardInformationUI.<>f__mg$cache7 == null)
					{
						PlayerDashboardInformationUI.<>f__mg$cache7 = new ClickedButton(PlayerDashboardInformationUI.onClickedCreateGroupButton);
					}
					sleekButtonIcon5.onClickedButton = (ClickedButton)Delegate.Combine(onClickedButton5, PlayerDashboardInformationUI.<>f__mg$cache7);
					PlayerDashboardInformationUI.groupsBox.add(sleekButtonIcon4);
					num += 30;
				}
			}
			PlayerDashboardInformationUI.groupsBox.area = new Rect(0f, 0f, 5f, (float)num);
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x001904D8 File Offset: 0x0018E8D8
		private static void handleGroupUpdated(PlayerQuests sender)
		{
			PlayerDashboardInformationUI.refreshGroups();
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x001904DF File Offset: 0x0018E8DF
		private static void handleGroupInfoReady(GroupInfo group)
		{
			PlayerDashboardInformationUI.refreshGroups();
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x001904E6 File Offset: 0x0018E8E6
		public static void openGroups()
		{
			PlayerDashboardInformationUI.tab = PlayerDashboardInformationUI.EInfoTab.GROUPS;
			PlayerDashboardInformationUI.refreshGroups();
			PlayerDashboardInformationUI.updateTabs();
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x001904F8 File Offset: 0x0018E8F8
		public static void openPlayers()
		{
			PlayerDashboardInformationUI.tab = PlayerDashboardInformationUI.EInfoTab.PLAYERS;
			if (OptionsSettings.streamer)
			{
				PlayerDashboardInformationUI.playersBox.remove();
				PlayerDashboardInformationUI.playersBox.area = new Rect(0f, 0f, 5f, 0f);
			}
			else
			{
				PlayerDashboardInformationUI.playersBox.remove();
				PlayerDashboardInformationUI.SORTED_CLIENTS.Clear();
				for (int i = 0; i < Provider.clients.Count; i++)
				{
					int num = PlayerDashboardInformationUI.SORTED_CLIENTS.BinarySearch(Provider.clients[i], PlayerDashboardInformationUI.GROUP_COMPARATOR);
					if (num < 0)
					{
						num = ~num;
					}
					PlayerDashboardInformationUI.SORTED_CLIENTS.Insert(num, Provider.clients[i]);
				}
				if (PlayerDashboardInformationUI.SORTED_CLIENTS.Count > 1)
				{
					for (int j = 1; j < PlayerDashboardInformationUI.SORTED_CLIENTS.Count; j++)
					{
						if (PlayerDashboardInformationUI.SORTED_CLIENTS[j - 1].player.quests.isMemberOfSameGroupAs(PlayerDashboardInformationUI.SORTED_CLIENTS[j].player))
						{
							SleekImageTexture sleekImageTexture = new SleekImageTexture((Texture2D)PlayerDashboardInformationUI.icons.load("Group"));
							sleekImageTexture.positionOffset_X = 21;
							sleekImageTexture.positionOffset_Y = j * 60 - 13;
							sleekImageTexture.sizeOffset_X = 8;
							sleekImageTexture.sizeOffset_Y = 16;
							sleekImageTexture.backgroundTint = ESleekTint.FOREGROUND;
							PlayerDashboardInformationUI.playersBox.add(sleekImageTexture);
						}
					}
				}
				for (int k = 0; k < PlayerDashboardInformationUI.SORTED_CLIENTS.Count; k++)
				{
					SteamPlayer newPlayer = PlayerDashboardInformationUI.SORTED_CLIENTS[k];
					SleekPlayer sleekPlayer = new SleekPlayer(newPlayer, true, SleekPlayer.ESleekPlayerDisplayContext.PLAYER_LIST);
					sleekPlayer.positionOffset_Y = k * 60;
					sleekPlayer.sizeOffset_X = -30;
					sleekPlayer.sizeOffset_Y = 50;
					sleekPlayer.sizeScale_X = 1f;
					PlayerDashboardInformationUI.playersBox.add(sleekPlayer);
				}
				PlayerDashboardInformationUI.playersBox.area = new Rect(0f, 0f, 5f, (float)(PlayerDashboardInformationUI.SORTED_CLIENTS.Count * 60 - 10));
			}
			PlayerDashboardInformationUI.updateTabs();
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x001906FF File Offset: 0x0018EAFF
		private static void updateTabs()
		{
			PlayerDashboardInformationUI.questsBox.isVisible = (PlayerDashboardInformationUI.tab == PlayerDashboardInformationUI.EInfoTab.QUESTS);
			PlayerDashboardInformationUI.groupsBox.isVisible = (PlayerDashboardInformationUI.tab == PlayerDashboardInformationUI.EInfoTab.GROUPS);
			PlayerDashboardInformationUI.playersBox.isVisible = (PlayerDashboardInformationUI.tab == PlayerDashboardInformationUI.EInfoTab.PLAYERS);
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x00190738 File Offset: 0x0018EB38
		private static void updateZoom()
		{
			if (PlayerDashboardInformationUI.zoom == 0 || (float)(1024 * (int)PlayerDashboardInformationUI.zoom) < PlayerDashboardInformationUI.mapBox.frame.height)
			{
				PlayerDashboardInformationUI.mapBox.area = new Rect(0f, 0f, PlayerDashboardInformationUI.mapBox.frame.width, PlayerDashboardInformationUI.mapBox.frame.height);
				PlayerDashboardInformationUI.mapImage.sizeOffset_X = (int)PlayerDashboardInformationUI.mapBox.frame.width;
				PlayerDashboardInformationUI.mapImage.sizeOffset_Y = (int)PlayerDashboardInformationUI.mapBox.frame.height;
			}
			else
			{
				PlayerDashboardInformationUI.mapBox.area = new Rect(0f, 0f, (float)(1024 * (int)PlayerDashboardInformationUI.zoom), (float)(1024 * (int)PlayerDashboardInformationUI.zoom));
				PlayerDashboardInformationUI.mapImage.sizeOffset_X = 1024 * (int)PlayerDashboardInformationUI.zoom;
				PlayerDashboardInformationUI.mapImage.sizeOffset_Y = 1024 * (int)PlayerDashboardInformationUI.zoom;
			}
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x0019084C File Offset: 0x0018EC4C
		public static void focusPoint(Vector3 point)
		{
			PlayerDashboardInformationUI.mapBox.state = new Vector2((point.x / (float)(Level.size - Level.border * 2) + 0.5f) * PlayerDashboardInformationUI.mapBox.area.width, (0.5f - point.z / (float)(Level.size - Level.border * 2)) * PlayerDashboardInformationUI.mapBox.area.height);
			PlayerDashboardInformationUI.mapBox.state -= new Vector2(PlayerDashboardInformationUI.mapBox.frame.width / 2f, PlayerDashboardInformationUI.mapBox.frame.width / 2f);
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x0019090C File Offset: 0x0018ED0C
		private static void onClickedMouseStarted()
		{
			if (PlayerUI.window.mouse_x > PlayerDashboardInformationUI.mapBox.frame.xMin && PlayerUI.window.mouse_x < PlayerDashboardInformationUI.mapBox.frame.xMax && PlayerUI.window.mouse_y > PlayerDashboardInformationUI.mapBox.frame.yMin && PlayerUI.window.mouse_y < PlayerDashboardInformationUI.mapBox.frame.yMax)
			{
				if (Event.current.button == 0)
				{
					PlayerDashboardInformationUI.isDragging = true;
					PlayerDashboardInformationUI.dragOrigin.x = PlayerUI.window.mouse_x;
					PlayerDashboardInformationUI.dragOrigin.y = PlayerUI.window.mouse_y;
					PlayerDashboardInformationUI.dragOffset.x = PlayerDashboardInformationUI.mapBox.state.x;
					PlayerDashboardInformationUI.dragOffset.y = PlayerDashboardInformationUI.mapBox.state.y;
				}
				else
				{
					Vector3 newMarkerPosition = new Vector3(((PlayerUI.window.mouse_x - PlayerDashboardInformationUI.mapBox.frame.xMin + PlayerDashboardInformationUI.mapBox.state.x) / PlayerDashboardInformationUI.mapBox.area.width - 0.5f) * (float)(Level.size - Level.border * 2), 0f, (0.5f - (PlayerUI.window.mouse_y - PlayerDashboardInformationUI.mapBox.frame.yMin + PlayerDashboardInformationUI.mapBox.state.y) / PlayerDashboardInformationUI.mapBox.area.height) * (float)(Level.size - Level.border * 2));
					PlayerQuests quests = Player.player.quests;
					bool newIsMarkerPlaced;
					if (quests.isMarkerPlaced)
					{
						Vector2 a = new Vector2(newMarkerPosition.x / (float)(Level.size - Level.border * 2) + 0.5f, 0.5f - newMarkerPosition.z / (float)(Level.size - Level.border * 2));
						Vector2 b = new Vector2(quests.markerPosition.x / (float)(Level.size - Level.border * 2) + 0.5f, 0.5f - quests.markerPosition.z / (float)(Level.size - Level.border * 2));
						float num = Vector2.Distance(a, b);
						num *= PlayerDashboardInformationUI.mapBox.area.width;
						newIsMarkerPlaced = (num > 15f);
					}
					else
					{
						newIsMarkerPlaced = true;
					}
					quests.sendSetMarker(newIsMarkerPlaced, newMarkerPosition);
					PlayerDashboardInformationUI.refreshStaticMap(PlayerDashboardInformationUI.mapButtonState.state);
				}
			}
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x00190BB7 File Offset: 0x0018EFB7
		private static void onClickedMouseStopped()
		{
			if (PlayerDashboardInformationUI.isDragging)
			{
				PlayerDashboardInformationUI.isDragging = false;
				PlayerDashboardInformationUI.dragOrigin = Vector2.zero;
				PlayerDashboardInformationUI.dragOffset = Vector2.zero;
			}
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x00190BE0 File Offset: 0x0018EFE0
		private static void onMovedMouse(float x, float y)
		{
			if (PlayerDashboardInformationUI.isDragging)
			{
				PlayerDashboardInformationUI.mapBox.state.x = PlayerDashboardInformationUI.dragOffset.x - x + PlayerDashboardInformationUI.dragOrigin.x;
				PlayerDashboardInformationUI.mapBox.state.y = PlayerDashboardInformationUI.dragOffset.y - y + PlayerDashboardInformationUI.dragOrigin.y;
			}
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x00190C44 File Offset: 0x0018F044
		private static void onClickedZoomInButton(SleekButton button)
		{
			if ((ushort)PlayerDashboardInformationUI.zoom < Level.size / 1024)
			{
				PlayerDashboardInformationUI.zoom += 1;
				Vector2 vector = PlayerDashboardInformationUI.mapBox.state + new Vector2(PlayerDashboardInformationUI.mapBox.frame.width / 2f, PlayerDashboardInformationUI.mapBox.frame.width / 2f);
				Vector2 state = new Vector2(vector.x / PlayerDashboardInformationUI.mapBox.area.width, vector.y / PlayerDashboardInformationUI.mapBox.area.height);
				PlayerDashboardInformationUI.updateZoom();
				state = new Vector2(state.x * PlayerDashboardInformationUI.mapBox.area.width, state.y * PlayerDashboardInformationUI.mapBox.area.height);
				PlayerDashboardInformationUI.mapBox.state = state;
				PlayerDashboardInformationUI.mapBox.state -= new Vector2(PlayerDashboardInformationUI.mapBox.frame.width / 2f, PlayerDashboardInformationUI.mapBox.frame.width / 2f);
				PlayerDashboardInformationUI.isDragging = false;
				PlayerDashboardInformationUI.dragOrigin = Vector2.zero;
				PlayerDashboardInformationUI.dragOffset = Vector2.zero;
			}
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x00190D98 File Offset: 0x0018F198
		private static void onClickedZoomOutButton(SleekButton button)
		{
			if (PlayerDashboardInformationUI.zoom > 0)
			{
				PlayerDashboardInformationUI.zoom -= 1;
				Vector2 vector = PlayerDashboardInformationUI.mapBox.state + new Vector2(PlayerDashboardInformationUI.mapBox.frame.width / 2f, PlayerDashboardInformationUI.mapBox.frame.width / 2f);
				Vector2 state = new Vector2(vector.x / PlayerDashboardInformationUI.mapBox.area.width, vector.y / PlayerDashboardInformationUI.mapBox.area.height);
				PlayerDashboardInformationUI.updateZoom();
				state = new Vector2(state.x * PlayerDashboardInformationUI.mapBox.area.width, state.y * PlayerDashboardInformationUI.mapBox.area.height);
				PlayerDashboardInformationUI.mapBox.state = state;
				PlayerDashboardInformationUI.mapBox.state -= new Vector2(PlayerDashboardInformationUI.mapBox.frame.width / 2f, PlayerDashboardInformationUI.mapBox.frame.width / 2f);
				PlayerDashboardInformationUI.isDragging = false;
				PlayerDashboardInformationUI.dragOrigin = Vector2.zero;
				PlayerDashboardInformationUI.dragOffset = Vector2.zero;
			}
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x00190EE2 File Offset: 0x0018F2E2
		private static void onClickedCenterButton(SleekButton button)
		{
			PlayerDashboardInformationUI.focusPoint(Player.player.transform.position);
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x00190EF8 File Offset: 0x0018F2F8
		private static void onSwappedMapState(SleekButtonState button, int index)
		{
			PlayerDashboardInformationUI.refreshStaticMap(index);
			PlayerDashboardInformationUI.refreshDynamicMap();
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x00190F08 File Offset: 0x0018F308
		private static void onClickedQuestButton(SleekButton button)
		{
			int index = PlayerDashboardInformationUI.questsBox.search(button);
			PlayerQuest playerQuest = Player.player.quests.questsList[index];
			PlayerDashboardUI.close();
			PlayerNPCQuestUI.open(playerQuest.asset, null, null, null, EQuestViewMode.DETAILS);
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x00190F4B File Offset: 0x0018F34B
		private static void onClickedQuestsButton(SleekButton button)
		{
			PlayerDashboardInformationUI.openQuests();
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x00190F52 File Offset: 0x0018F352
		private static void onClickedGroupsButton(SleekButton button)
		{
			PlayerDashboardInformationUI.openGroups();
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x00190F59 File Offset: 0x0018F359
		private static void onClickedPlayersButton(SleekButton button)
		{
			PlayerDashboardInformationUI.openPlayers();
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x00190F60 File Offset: 0x0018F360
		private static void handleIsBlindfoldedChanged()
		{
			PlayerDashboardInformationUI.refreshStaticMap(PlayerDashboardInformationUI.mapButtonState.state);
			PlayerDashboardInformationUI.refreshDynamicMap();
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x00190F76 File Offset: 0x0018F376
		private static void onPlayerTeleported(Player player, Vector3 point)
		{
			PlayerDashboardInformationUI.focusPoint(point);
		}

		// Token: 0x040029E0 RID: 10720
		private static readonly List<SteamPlayer> SORTED_CLIENTS = new List<SteamPlayer>();

		// Token: 0x040029E1 RID: 10721
		private static readonly SteamPlayerGroupAscendingComparator GROUP_COMPARATOR = new SteamPlayerGroupAscendingComparator();

		// Token: 0x040029E2 RID: 10722
		public static Local localization;

		// Token: 0x040029E3 RID: 10723
		public static Bundle icons;

		// Token: 0x040029E4 RID: 10724
		private static Sleek container;

		// Token: 0x040029E5 RID: 10725
		public static bool active;

		// Token: 0x040029E6 RID: 10726
		private static byte zoom;

		// Token: 0x040029E7 RID: 10727
		private static SleekBox backdropBox;

		// Token: 0x040029E8 RID: 10728
		private static bool isDragging;

		// Token: 0x040029E9 RID: 10729
		private static Vector2 dragOrigin;

		// Token: 0x040029EA RID: 10730
		private static Vector2 dragOffset;

		// Token: 0x040029EB RID: 10731
		private static Sleek mapInspect;

		// Token: 0x040029EC RID: 10732
		private static SleekViewBox mapBox;

		// Token: 0x040029ED RID: 10733
		private static SleekImageTexture mapImage;

		// Token: 0x040029EE RID: 10734
		private static Sleek mapStaticContainer;

		// Token: 0x040029EF RID: 10735
		private static Sleek mapDynamicContainer;

		// Token: 0x040029F0 RID: 10736
		private static SleekButtonIcon zoomInButton;

		// Token: 0x040029F1 RID: 10737
		private static SleekButtonIcon zoomOutButton;

		// Token: 0x040029F2 RID: 10738
		private static SleekButtonIcon centerButton;

		// Token: 0x040029F3 RID: 10739
		private static SleekButtonState mapButtonState;

		// Token: 0x040029F4 RID: 10740
		public static SleekLabel noLabel;

		// Token: 0x040029F5 RID: 10741
		private static Sleek headerButtonsContainer;

		// Token: 0x040029F6 RID: 10742
		private static SleekButtonIcon questsButton;

		// Token: 0x040029F7 RID: 10743
		private static SleekButtonIcon groupsButton;

		// Token: 0x040029F8 RID: 10744
		private static SleekButtonIcon playersButton;

		// Token: 0x040029F9 RID: 10745
		private static SleekScrollBox questsBox;

		// Token: 0x040029FA RID: 10746
		private static SleekScrollBox groupsBox;

		// Token: 0x040029FB RID: 10747
		private static SleekScrollBox playersBox;

		// Token: 0x040029FC RID: 10748
		private static SleekDoubleField radioFrequencyField;

		// Token: 0x040029FD RID: 10749
		private static SleekField groupNameField;

		// Token: 0x040029FE RID: 10750
		private static bool hasChart;

		// Token: 0x040029FF RID: 10751
		private static bool hasGPS;

		// Token: 0x04002A00 RID: 10752
		private static PlayerDashboardInformationUI.EInfoTab tab;

		// Token: 0x04002A01 RID: 10753
		private static Texture2D mapTexture;

		// Token: 0x04002A02 RID: 10754
		private static Texture2D chartTexture;

		// Token: 0x04002A03 RID: 10755
		private static Texture2D staticTexture;

		// Token: 0x04002A04 RID: 10756
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x04002A05 RID: 10757
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x04002A06 RID: 10758
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x04002A07 RID: 10759
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x04002A08 RID: 10760
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;

		// Token: 0x04002A09 RID: 10761
		[CompilerGenerated]
		private static Confirm <>f__mg$cache5;

		// Token: 0x04002A0A RID: 10762
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache6;

		// Token: 0x04002A0B RID: 10763
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache7;

		// Token: 0x04002A0C RID: 10764
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache8;

		// Token: 0x04002A0D RID: 10765
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache9;

		// Token: 0x04002A0E RID: 10766
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheA;

		// Token: 0x04002A0F RID: 10767
		[CompilerGenerated]
		private static SwappedState <>f__mg$cacheB;

		// Token: 0x04002A10 RID: 10768
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheC;

		// Token: 0x04002A11 RID: 10769
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheD;

		// Token: 0x04002A12 RID: 10770
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheE;

		// Token: 0x04002A13 RID: 10771
		[CompilerGenerated]
		private static ClickedMouseStarted <>f__mg$cacheF;

		// Token: 0x04002A14 RID: 10772
		[CompilerGenerated]
		private static ClickedMouseStopped <>f__mg$cache10;

		// Token: 0x04002A15 RID: 10773
		[CompilerGenerated]
		private static MovedMouse <>f__mg$cache11;

		// Token: 0x04002A16 RID: 10774
		[CompilerGenerated]
		private static IsBlindfoldedChangedHandler <>f__mg$cache12;

		// Token: 0x04002A17 RID: 10775
		[CompilerGenerated]
		private static PlayerTeleported <>f__mg$cache13;

		// Token: 0x04002A18 RID: 10776
		[CompilerGenerated]
		private static GroupUpdatedHandler <>f__mg$cache14;

		// Token: 0x04002A19 RID: 10777
		[CompilerGenerated]
		private static GroupInfoReadyHandler <>f__mg$cache15;

		// Token: 0x02000796 RID: 1942
		private class SleekInviteButton : Sleek
		{
			// Token: 0x06003820 RID: 14368 RVA: 0x00190F94 File Offset: 0x0018F394
			public SleekInviteButton(CSteamID newGroupID)
			{
				this.groupID = newGroupID;
				GroupInfo groupInfo = GroupManager.getGroupInfo(this.groupID);
				string text = (groupInfo == null) ? this.groupID.ToString() : groupInfo.name;
				SleekBox sleekBox = new SleekBox();
				sleekBox.sizeOffset_X = -140;
				sleekBox.sizeScale_X = 1f;
				sleekBox.sizeScale_Y = 1f;
				sleekBox.text = text;
				base.add(sleekBox);
				sleekBox.add(new SleekButton
				{
					positionScale_X = 1f,
					sizeOffset_X = 60,
					sizeScale_Y = 1f,
					text = PlayerDashboardInformationUI.localization.format("Group_Join"),
					tooltip = PlayerDashboardInformationUI.localization.format("Group_Join_Tooltip"),
					onClickedButton = new ClickedButton(this.handleJoinButtonClicked)
				});
				sleekBox.add(new SleekButton
				{
					positionOffset_X = 60,
					positionScale_X = 1f,
					sizeOffset_X = 80,
					sizeScale_Y = 1f,
					text = PlayerDashboardInformationUI.localization.format("Group_Ignore"),
					tooltip = PlayerDashboardInformationUI.localization.format("Group_Ignore_Tooltip"),
					onClickedButton = new ClickedButton(this.handleIgnoreButtonClicked)
				});
			}

			// Token: 0x17000A4F RID: 2639
			// (get) Token: 0x06003821 RID: 14369 RVA: 0x001910FC File Offset: 0x0018F4FC
			private CSteamID groupID { get; }

			// Token: 0x06003822 RID: 14370 RVA: 0x00191104 File Offset: 0x0018F504
			private void handleJoinButtonClicked(SleekButton button)
			{
				Player.player.quests.sendJoinGroupInvite(this.groupID);
			}

			// Token: 0x06003823 RID: 14371 RVA: 0x0019111B File Offset: 0x0018F51B
			private void handleIgnoreButtonClicked(SleekButton button)
			{
				Player.player.quests.sendIgnoreGroupInvite(this.groupID);
			}
		}

		// Token: 0x02000797 RID: 1943
		private enum EInfoTab
		{
			// Token: 0x04002A1C RID: 10780
			QUESTS,
			// Token: 0x04002A1D RID: 10781
			GROUPS,
			// Token: 0x04002A1E RID: 10782
			PLAYERS
		}
	}
}
