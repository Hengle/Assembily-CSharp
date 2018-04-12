using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Provider;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200079E RID: 1950
	public class PlayerLifeUI
	{
		// Token: 0x0600388B RID: 14475 RVA: 0x00199C48 File Offset: 0x00198048
		public PlayerLifeUI()
		{
			if (PlayerLifeUI.icons != null)
			{
				PlayerLifeUI.icons.unload();
			}
			PlayerLifeUI.localization = Localization.read("/Player/PlayerLife.dat");
			PlayerLifeUI.icons = Bundles.getBundle("/Bundles/Textures/Player/Icons/PlayerLife/PlayerLife.unity3d");
			PlayerLifeUI._container = new Sleek();
			PlayerLifeUI.container.positionOffset_X = 10;
			PlayerLifeUI.container.positionOffset_Y = 10;
			PlayerLifeUI.container.sizeOffset_X = -20;
			PlayerLifeUI.container.sizeOffset_Y = -20;
			PlayerLifeUI.container.sizeScale_X = 1f;
			PlayerLifeUI.container.sizeScale_Y = 1f;
			PlayerUI.container.add(PlayerLifeUI.container);
			PlayerLifeUI.active = true;
			PlayerLifeUI.chatting = false;
			PlayerLifeUI.chatBox = new SleekScrollBox();
			PlayerLifeUI.chatBox.sizeOffset_X = 504;
			PlayerLifeUI.chatBox.sizeOffset_Y = 160;
			PlayerLifeUI.chatBox.area = new Rect(0f, 0f, 5f, (float)(ChatManager.chat.Length * 40));
			PlayerLifeUI.chatBox.state = new Vector3(0f, float.MaxValue);
			PlayerLifeUI.container.add(PlayerLifeUI.chatBox);
			PlayerLifeUI.chatBox.isVisible = false;
			PlayerLifeUI.chatLabel = new SleekChat[ChatManager.chat.Length];
			for (int i = 0; i < PlayerLifeUI.chatLabel.Length; i++)
			{
				SleekChat sleekChat = new SleekChat();
				sleekChat.positionOffset_Y = PlayerLifeUI.chatLabel.Length * 40 - 40 - i * 40;
				sleekChat.sizeOffset_X = 474;
				sleekChat.sizeOffset_Y = 40;
				PlayerLifeUI.chatBox.add(sleekChat);
				PlayerLifeUI.chatLabel[i] = sleekChat;
			}
			PlayerLifeUI.gameLabel = new SleekChat[4];
			for (int j = 0; j < PlayerLifeUI.gameLabel.Length; j++)
			{
				SleekChat sleekChat2 = new SleekChat();
				sleekChat2.positionOffset_Y = 120 - j * 40;
				sleekChat2.sizeOffset_X = 474;
				sleekChat2.sizeOffset_Y = 40;
				PlayerLifeUI.container.add(sleekChat2);
				PlayerLifeUI.gameLabel[j] = sleekChat2;
			}
			PlayerLifeUI.chatField = new SleekField();
			PlayerLifeUI.chatField.positionOffset_X = -424;
			PlayerLifeUI.chatField.positionOffset_Y = 170;
			PlayerLifeUI.chatField.sizeOffset_X = 404;
			PlayerLifeUI.chatField.sizeOffset_Y = 30;
			PlayerLifeUI.chatField.fontAlignment = TextAnchor.MiddleLeft;
			PlayerLifeUI.chatField.maxLength = ChatManager.LENGTH;
			PlayerLifeUI.chatField.foregroundTint = ESleekTint.NONE;
			PlayerLifeUI.container.add(PlayerLifeUI.chatField);
			PlayerLifeUI.modeBox = new SleekBox();
			PlayerLifeUI.modeBox.positionOffset_X = -100;
			PlayerLifeUI.modeBox.sizeOffset_X = 90;
			PlayerLifeUI.modeBox.sizeOffset_Y = 30;
			PlayerLifeUI.modeBox.fontAlignment = TextAnchor.MiddleCenter;
			PlayerLifeUI.modeBox.foregroundTint = ESleekTint.NONE;
			PlayerLifeUI.chatField.add(PlayerLifeUI.modeBox);
			PlayerLifeUI.voteBox = new SleekBox();
			PlayerLifeUI.voteBox.positionOffset_X = -430;
			PlayerLifeUI.voteBox.positionScale_X = 1f;
			PlayerLifeUI.voteBox.sizeOffset_X = 430;
			PlayerLifeUI.voteBox.sizeOffset_Y = 90;
			PlayerLifeUI.container.add(PlayerLifeUI.voteBox);
			PlayerLifeUI.voteBox.isVisible = false;
			PlayerLifeUI.voteInfoLabel = new SleekLabel();
			PlayerLifeUI.voteInfoLabel.sizeOffset_Y = 30;
			PlayerLifeUI.voteInfoLabel.sizeScale_X = 1f;
			PlayerLifeUI.voteBox.add(PlayerLifeUI.voteInfoLabel);
			PlayerLifeUI.votesNeededLabel = new SleekLabel();
			PlayerLifeUI.votesNeededLabel.positionOffset_Y = 30;
			PlayerLifeUI.votesNeededLabel.sizeOffset_Y = 30;
			PlayerLifeUI.votesNeededLabel.sizeScale_X = 1f;
			PlayerLifeUI.voteBox.add(PlayerLifeUI.votesNeededLabel);
			PlayerLifeUI.voteYesLabel = new SleekLabel();
			PlayerLifeUI.voteYesLabel.positionOffset_Y = 60;
			PlayerLifeUI.voteYesLabel.sizeOffset_Y = 30;
			PlayerLifeUI.voteYesLabel.sizeScale_X = 0.5f;
			PlayerLifeUI.voteBox.add(PlayerLifeUI.voteYesLabel);
			PlayerLifeUI.voteNoLabel = new SleekLabel();
			PlayerLifeUI.voteNoLabel.positionOffset_Y = 60;
			PlayerLifeUI.voteNoLabel.positionScale_X = 0.5f;
			PlayerLifeUI.voteNoLabel.sizeOffset_Y = 30;
			PlayerLifeUI.voteNoLabel.sizeScale_X = 0.5f;
			PlayerLifeUI.voteBox.add(PlayerLifeUI.voteNoLabel);
			PlayerLifeUI.voiceBox = new SleekBoxIcon((Texture2D)PlayerLifeUI.icons.load("Voice"));
			PlayerLifeUI.voiceBox.positionOffset_Y = 210;
			PlayerLifeUI.voiceBox.sizeOffset_X = 50;
			PlayerLifeUI.voiceBox.sizeOffset_Y = 50;
			PlayerLifeUI.voiceBox.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			PlayerLifeUI.container.add(PlayerLifeUI.voiceBox);
			PlayerLifeUI.voiceBox.isVisible = false;
			PlayerLifeUI.trackedQuestTitle = new SleekLabel();
			PlayerLifeUI.trackedQuestTitle.positionOffset_X = -500;
			PlayerLifeUI.trackedQuestTitle.positionOffset_Y = 215;
			PlayerLifeUI.trackedQuestTitle.positionScale_X = 1f;
			PlayerLifeUI.trackedQuestTitle.sizeOffset_X = 500;
			PlayerLifeUI.trackedQuestTitle.sizeOffset_Y = 25;
			PlayerLifeUI.trackedQuestTitle.isRich = true;
			PlayerLifeUI.trackedQuestTitle.fontSize = 14;
			PlayerLifeUI.trackedQuestTitle.fontAlignment = TextAnchor.MiddleRight;
			PlayerLifeUI.container.add(PlayerLifeUI.trackedQuestTitle);
			PlayerLifeUI.trackedQuestBar = new SleekImageTexture();
			PlayerLifeUI.trackedQuestBar.positionOffset_X = -200;
			PlayerLifeUI.trackedQuestBar.positionOffset_Y = 240;
			PlayerLifeUI.trackedQuestBar.positionScale_X = 1f;
			PlayerLifeUI.trackedQuestBar.sizeOffset_X = 200;
			PlayerLifeUI.trackedQuestBar.sizeOffset_Y = 3;
			PlayerLifeUI.trackedQuestBar.texture = (Texture2D)Resources.Load("Materials/Pixel");
			PlayerLifeUI.trackedQuestBar.backgroundTint = ESleekTint.FOREGROUND;
			PlayerLifeUI.container.add(PlayerLifeUI.trackedQuestBar);
			PlayerLifeUI.levelTextBox = new SleekBox();
			PlayerLifeUI.levelTextBox.positionOffset_X = -180;
			PlayerLifeUI.levelTextBox.positionScale_X = 0.5f;
			PlayerLifeUI.levelTextBox.sizeOffset_X = 300;
			PlayerLifeUI.levelTextBox.sizeOffset_Y = 50;
			PlayerLifeUI.levelTextBox.fontSize = 14;
			PlayerLifeUI.container.add(PlayerLifeUI.levelTextBox);
			PlayerLifeUI.levelTextBox.isVisible = false;
			PlayerLifeUI.levelNumberBox = new SleekBox();
			PlayerLifeUI.levelNumberBox.positionOffset_X = 130;
			PlayerLifeUI.levelNumberBox.positionScale_X = 0.5f;
			PlayerLifeUI.levelNumberBox.sizeOffset_X = 50;
			PlayerLifeUI.levelNumberBox.sizeOffset_Y = 50;
			PlayerLifeUI.levelNumberBox.fontSize = 14;
			PlayerLifeUI.container.add(PlayerLifeUI.levelNumberBox);
			PlayerLifeUI.levelNumberBox.isVisible = false;
			PlayerLifeUI.compassBox = new SleekBox();
			PlayerLifeUI.compassBox.positionOffset_X = -180;
			PlayerLifeUI.compassBox.positionScale_X = 0.5f;
			PlayerLifeUI.compassBox.sizeOffset_X = 360;
			PlayerLifeUI.compassBox.sizeOffset_Y = 50;
			PlayerLifeUI.compassBox.fontSize = 14;
			PlayerLifeUI.container.add(PlayerLifeUI.compassBox);
			PlayerLifeUI.compassBox.isVisible = false;
			PlayerLifeUI.compassLabelsContainer = new Sleek();
			PlayerLifeUI.compassLabelsContainer.positionOffset_X = 10;
			PlayerLifeUI.compassLabelsContainer.positionOffset_Y = 10;
			PlayerLifeUI.compassLabelsContainer.sizeOffset_X = -20;
			PlayerLifeUI.compassLabelsContainer.sizeOffset_Y = -20;
			PlayerLifeUI.compassLabelsContainer.sizeScale_X = 1f;
			PlayerLifeUI.compassLabelsContainer.sizeScale_Y = 1f;
			PlayerLifeUI.compassBox.add(PlayerLifeUI.compassLabelsContainer);
			PlayerLifeUI.compassMarkersContainer = new Sleek();
			PlayerLifeUI.compassMarkersContainer.positionOffset_X = 10;
			PlayerLifeUI.compassMarkersContainer.positionOffset_Y = 10;
			PlayerLifeUI.compassMarkersContainer.sizeOffset_X = -20;
			PlayerLifeUI.compassMarkersContainer.sizeOffset_Y = -20;
			PlayerLifeUI.compassMarkersContainer.sizeScale_X = 1f;
			PlayerLifeUI.compassMarkersContainer.sizeScale_Y = 1f;
			PlayerLifeUI.compassBox.add(PlayerLifeUI.compassMarkersContainer);
			PlayerLifeUI.compassLabels = new SleekLabel[72];
			for (int k = 0; k < PlayerLifeUI.compassLabels.Length; k++)
			{
				SleekLabel sleekLabel = new SleekLabel();
				sleekLabel.positionOffset_X = -25;
				sleekLabel.sizeOffset_X = 50;
				sleekLabel.sizeOffset_Y = 30;
				sleekLabel.text = (k * 5).ToString();
				sleekLabel.foregroundTint = ESleekTint.NONE;
				sleekLabel.foregroundColor = new Color(0.75f, 0.75f, 0.75f);
				PlayerLifeUI.compassLabelsContainer.add(sleekLabel);
				PlayerLifeUI.compassLabels[k] = sleekLabel;
			}
			PlayerLifeUI.compassLabels[0].fontSize = 16;
			PlayerLifeUI.compassLabels[0].text = "N";
			PlayerLifeUI.compassLabels[0].foregroundColor = Palette.COLOR_R;
			PlayerLifeUI.compassLabels[8].fontSize = 14;
			PlayerLifeUI.compassLabels[8].text = "NE";
			PlayerLifeUI.compassLabels[8].foregroundColor = new Color(1f, 1f, 1f);
			PlayerLifeUI.compassLabels[17].fontSize = 16;
			PlayerLifeUI.compassLabels[17].text = "E";
			PlayerLifeUI.compassLabels[17].foregroundColor = new Color(1f, 1f, 1f);
			PlayerLifeUI.compassLabels[26].fontSize = 14;
			PlayerLifeUI.compassLabels[26].text = "SE";
			PlayerLifeUI.compassLabels[26].foregroundColor = new Color(1f, 1f, 1f);
			PlayerLifeUI.compassLabels[35].fontSize = 16;
			PlayerLifeUI.compassLabels[35].text = "S";
			PlayerLifeUI.compassLabels[35].foregroundColor = new Color(1f, 1f, 1f);
			PlayerLifeUI.compassLabels[44].fontSize = 14;
			PlayerLifeUI.compassLabels[44].text = "SW";
			PlayerLifeUI.compassLabels[44].foregroundColor = new Color(1f, 1f, 1f);
			PlayerLifeUI.compassLabels[53].fontSize = 16;
			PlayerLifeUI.compassLabels[53].text = "W";
			PlayerLifeUI.compassLabels[53].foregroundColor = new Color(1f, 1f, 1f);
			PlayerLifeUI.compassLabels[62].fontSize = 14;
			PlayerLifeUI.compassLabels[62].text = "NW";
			PlayerLifeUI.compassLabels[62].foregroundColor = new Color(1f, 1f, 1f);
			PlayerLifeUI.hotbarContainer = new Sleek();
			PlayerLifeUI.hotbarContainer.positionScale_X = 0.5f;
			PlayerLifeUI.hotbarContainer.positionScale_Y = 1f;
			PlayerLifeUI.hotbarContainer.positionOffset_Y = -200;
			PlayerLifeUI.container.add(PlayerLifeUI.hotbarContainer);
			PlayerLifeUI.hotbarContainer.isVisible = false;
			PlayerLifeUI.hotbarIDs = new ushort[10];
			PlayerLifeUI.hotbarImages = new SleekImageTexture[PlayerLifeUI.hotbarIDs.Length];
			for (int l = 0; l < PlayerLifeUI.hotbarImages.Length; l++)
			{
				SleekImageTexture sleekImageTexture = new SleekImageTexture();
				sleekImageTexture.backgroundColor = new Color(1f, 1f, 1f, 0.5f);
				PlayerLifeUI.hotbarContainer.add(sleekImageTexture);
				sleekImageTexture.isVisible = false;
				PlayerLifeUI.hotbarImages[l] = sleekImageTexture;
			}
			PlayerLifeUI.hotbarLabels = new SleekLabel[PlayerLifeUI.hotbarIDs.Length];
			for (int m = 0; m < PlayerLifeUI.hotbarLabels.Length; m++)
			{
				SleekLabel sleekLabel2 = new SleekLabel();
				sleekLabel2.positionOffset_Y = 5;
				sleekLabel2.sizeOffset_X = 50;
				sleekLabel2.sizeOffset_Y = 30;
				sleekLabel2.text = (m + 1).ToString();
				sleekLabel2.fontAlignment = TextAnchor.UpperRight;
				sleekLabel2.foregroundTint = ESleekTint.NONE;
				sleekLabel2.foregroundColor = new Color(1f, 1f, 1f, 0.75f);
				PlayerLifeUI.hotbarContainer.add(sleekLabel2);
				sleekLabel2.isVisible = false;
				PlayerLifeUI.hotbarLabels[m] = sleekLabel2;
			}
			PlayerLifeUI.statTrackerLabel = new SleekLabel();
			PlayerLifeUI.statTrackerLabel.positionOffset_X = -100;
			PlayerLifeUI.statTrackerLabel.positionOffset_Y = -30;
			PlayerLifeUI.statTrackerLabel.positionScale_X = 0.5f;
			PlayerLifeUI.statTrackerLabel.positionScale_Y = 1f;
			PlayerLifeUI.statTrackerLabel.sizeOffset_X = 200;
			PlayerLifeUI.statTrackerLabel.sizeOffset_Y = 30;
			PlayerLifeUI.statTrackerLabel.foregroundTint = ESleekTint.NONE;
			PlayerLifeUI.statTrackerLabel.fontAlignment = TextAnchor.LowerCenter;
			PlayerLifeUI.statTrackerLabel.fontStyle = FontStyle.Italic;
			PlayerLifeUI.statTrackerLabel.fontSize = 12;
			PlayerLifeUI.container.add(PlayerLifeUI.statTrackerLabel);
			PlayerLifeUI.statTrackerLabel.isVisible = false;
			PlayerLifeUI.painImage = new SleekImageTexture();
			PlayerLifeUI.painImage.sizeScale_X = 1f;
			PlayerLifeUI.painImage.sizeScale_Y = 1f;
			Color color_R = Palette.COLOR_R;
			color_R.a = 0f;
			PlayerLifeUI.painImage.backgroundColor = color_R;
			PlayerLifeUI.painImage.texture = (Texture2D)Resources.Load("Materials/Pixel");
			PlayerUI.window.add(PlayerLifeUI.painImage);
			PlayerLifeUI.scopeOverlay = new SleekImageTexture((Texture2D)Resources.Load("Overlay/Scope"));
			PlayerLifeUI.scopeOverlay.positionScale_X = 0.1f;
			PlayerLifeUI.scopeOverlay.positionScale_Y = 0.1f;
			PlayerLifeUI.scopeOverlay.sizeScale_X = 0.8f;
			PlayerLifeUI.scopeOverlay.sizeScale_Y = 0.8f;
			PlayerLifeUI.scopeOverlay.constraint = ESleekConstraint.X;
			PlayerUI.window.add(PlayerLifeUI.scopeOverlay);
			PlayerLifeUI.scopeOverlay.isVisible = false;
			PlayerLifeUI.scopeLeftOverlay = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
			PlayerLifeUI.scopeLeftOverlay.positionScale_X = -10f;
			PlayerLifeUI.scopeLeftOverlay.sizeScale_X = 10f;
			PlayerLifeUI.scopeLeftOverlay.sizeScale_Y = 1f;
			PlayerLifeUI.scopeLeftOverlay.backgroundColor = Color.black;
			PlayerLifeUI.scopeOverlay.add(PlayerLifeUI.scopeLeftOverlay);
			PlayerLifeUI.scopeRightOverlay = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
			PlayerLifeUI.scopeRightOverlay.positionScale_X = 1f;
			PlayerLifeUI.scopeRightOverlay.sizeScale_X = 10f;
			PlayerLifeUI.scopeRightOverlay.sizeScale_Y = 1f;
			PlayerLifeUI.scopeRightOverlay.backgroundColor = Color.black;
			PlayerLifeUI.scopeOverlay.add(PlayerLifeUI.scopeRightOverlay);
			PlayerLifeUI.scopeUpOverlay = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
			PlayerLifeUI.scopeUpOverlay.positionScale_X = -10f;
			PlayerLifeUI.scopeUpOverlay.positionScale_Y = -10f;
			PlayerLifeUI.scopeUpOverlay.sizeScale_X = 21f;
			PlayerLifeUI.scopeUpOverlay.sizeScale_Y = 10f;
			PlayerLifeUI.scopeUpOverlay.backgroundColor = Color.black;
			PlayerLifeUI.scopeOverlay.add(PlayerLifeUI.scopeUpOverlay);
			PlayerLifeUI.scopeDownOverlay = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
			PlayerLifeUI.scopeDownOverlay.positionScale_X = -10f;
			PlayerLifeUI.scopeDownOverlay.positionScale_Y = 1f;
			PlayerLifeUI.scopeDownOverlay.sizeScale_X = 21f;
			PlayerLifeUI.scopeDownOverlay.sizeScale_Y = 10f;
			PlayerLifeUI.scopeDownOverlay.backgroundColor = Color.black;
			PlayerLifeUI.scopeOverlay.add(PlayerLifeUI.scopeDownOverlay);
			PlayerLifeUI.scopeImage = new SleekImageTexture();
			PlayerLifeUI.scopeImage.sizeScale_X = 1f;
			PlayerLifeUI.scopeImage.sizeScale_Y = 1f;
			PlayerLifeUI.scopeOverlay.add(PlayerLifeUI.scopeImage);
			PlayerLifeUI.binocularsOverlay = new SleekImageTexture((Texture2D)Resources.Load("Overlay/Binoculars"));
			PlayerLifeUI.binocularsOverlay.sizeScale_X = 1f;
			PlayerLifeUI.binocularsOverlay.sizeScale_Y = 1f;
			PlayerUI.window.add(PlayerLifeUI.binocularsOverlay);
			PlayerLifeUI.binocularsOverlay.isVisible = false;
			PlayerLifeUI.faceButtons = new SleekButton[(int)(Customization.FACES_FREE + Customization.FACES_PRO)];
			for (int n = 0; n < PlayerLifeUI.faceButtons.Length; n++)
			{
				float num = 12.566371f * ((float)n / (float)PlayerLifeUI.faceButtons.Length);
				float num2 = 185f;
				if (n >= PlayerLifeUI.faceButtons.Length / 2)
				{
					num += 3.14159274f / (float)(PlayerLifeUI.faceButtons.Length / 2);
					num2 += 30f;
				}
				SleekButton sleekButton = new SleekButton();
				sleekButton.positionOffset_X = (int)(Mathf.Cos(num) * num2) - 20;
				sleekButton.positionOffset_Y = (int)(Mathf.Sin(num) * num2) - 20;
				sleekButton.positionScale_X = 0.5f;
				sleekButton.positionScale_Y = 0.5f;
				sleekButton.sizeOffset_X = 40;
				sleekButton.sizeOffset_Y = 40;
				PlayerLifeUI.container.add(sleekButton);
				sleekButton.isVisible = false;
				SleekImageTexture sleekImageTexture2 = new SleekImageTexture();
				sleekImageTexture2.positionOffset_X = 10;
				sleekImageTexture2.positionOffset_Y = 10;
				sleekImageTexture2.sizeOffset_X = 20;
				sleekImageTexture2.sizeOffset_Y = 20;
				sleekImageTexture2.texture = (Texture2D)Resources.Load("Materials/Pixel");
				sleekImageTexture2.backgroundColor = Characters.active.skin;
				sleekButton.add(sleekImageTexture2);
				sleekImageTexture2.add(new SleekImageTexture
				{
					positionOffset_X = 2,
					positionOffset_Y = 2,
					sizeOffset_X = 16,
					sizeOffset_Y = 16,
					texture = (Texture2D)Resources.Load("Faces/" + n + "/Texture")
				});
				if (n >= (int)Customization.FACES_FREE)
				{
					if (Provider.isPro)
					{
						SleekButton sleekButton2 = sleekButton;
						if (PlayerLifeUI.<>f__mg$cache0 == null)
						{
							PlayerLifeUI.<>f__mg$cache0 = new ClickedButton(PlayerLifeUI.onClickedFaceButton);
						}
						sleekButton2.onClickedButton = PlayerLifeUI.<>f__mg$cache0;
					}
					else
					{
						sleekButton.backgroundColor = Palette.PRO;
						Bundle bundle = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Pro/Pro.unity3d");
						sleekButton.add(new SleekImageTexture
						{
							positionOffset_X = -10,
							positionOffset_Y = -10,
							positionScale_X = 0.5f,
							positionScale_Y = 0.5f,
							sizeOffset_X = 20,
							sizeOffset_Y = 20,
							texture = (Texture2D)bundle.load("Lock_Small")
						});
						bundle.unload();
					}
				}
				else
				{
					SleekButton sleekButton3 = sleekButton;
					if (PlayerLifeUI.<>f__mg$cache1 == null)
					{
						PlayerLifeUI.<>f__mg$cache1 = new ClickedButton(PlayerLifeUI.onClickedFaceButton);
					}
					sleekButton3.onClickedButton = PlayerLifeUI.<>f__mg$cache1;
				}
				PlayerLifeUI.faceButtons[n] = sleekButton;
			}
			PlayerLifeUI.surrenderButton = new SleekButton();
			PlayerLifeUI.surrenderButton.positionOffset_X = -110;
			PlayerLifeUI.surrenderButton.positionOffset_Y = -15;
			PlayerLifeUI.surrenderButton.positionScale_X = 0.5f;
			PlayerLifeUI.surrenderButton.positionScale_Y = 0.5f;
			PlayerLifeUI.surrenderButton.sizeOffset_X = 100;
			PlayerLifeUI.surrenderButton.sizeOffset_Y = 30;
			PlayerLifeUI.surrenderButton.text = PlayerLifeUI.localization.format("Surrender");
			SleekButton sleekButton4 = PlayerLifeUI.surrenderButton;
			if (PlayerLifeUI.<>f__mg$cache2 == null)
			{
				PlayerLifeUI.<>f__mg$cache2 = new ClickedButton(PlayerLifeUI.onClickedSurrenderButton);
			}
			sleekButton4.onClickedButton = PlayerLifeUI.<>f__mg$cache2;
			PlayerLifeUI.container.add(PlayerLifeUI.surrenderButton);
			PlayerLifeUI.surrenderButton.isVisible = false;
			PlayerLifeUI.pointButton = new SleekButton();
			PlayerLifeUI.pointButton.positionOffset_X = 10;
			PlayerLifeUI.pointButton.positionOffset_Y = -15;
			PlayerLifeUI.pointButton.positionScale_X = 0.5f;
			PlayerLifeUI.pointButton.positionScale_Y = 0.5f;
			PlayerLifeUI.pointButton.sizeOffset_X = 100;
			PlayerLifeUI.pointButton.sizeOffset_Y = 30;
			PlayerLifeUI.pointButton.text = PlayerLifeUI.localization.format("Point");
			SleekButton sleekButton5 = PlayerLifeUI.pointButton;
			if (PlayerLifeUI.<>f__mg$cache3 == null)
			{
				PlayerLifeUI.<>f__mg$cache3 = new ClickedButton(PlayerLifeUI.onClickedPointButton);
			}
			sleekButton5.onClickedButton = PlayerLifeUI.<>f__mg$cache3;
			PlayerLifeUI.container.add(PlayerLifeUI.pointButton);
			PlayerLifeUI.pointButton.isVisible = false;
			PlayerLifeUI.waveButton = new SleekButton();
			PlayerLifeUI.waveButton.positionOffset_X = -50;
			PlayerLifeUI.waveButton.positionOffset_Y = -55;
			PlayerLifeUI.waveButton.positionScale_X = 0.5f;
			PlayerLifeUI.waveButton.positionScale_Y = 0.5f;
			PlayerLifeUI.waveButton.sizeOffset_X = 100;
			PlayerLifeUI.waveButton.sizeOffset_Y = 30;
			PlayerLifeUI.waveButton.text = PlayerLifeUI.localization.format("Wave");
			SleekButton sleekButton6 = PlayerLifeUI.waveButton;
			if (PlayerLifeUI.<>f__mg$cache4 == null)
			{
				PlayerLifeUI.<>f__mg$cache4 = new ClickedButton(PlayerLifeUI.onClickedWaveButton);
			}
			sleekButton6.onClickedButton = PlayerLifeUI.<>f__mg$cache4;
			PlayerLifeUI.container.add(PlayerLifeUI.waveButton);
			PlayerLifeUI.waveButton.isVisible = false;
			PlayerLifeUI.saluteButton = new SleekButton();
			PlayerLifeUI.saluteButton.positionOffset_X = -50;
			PlayerLifeUI.saluteButton.positionOffset_Y = 25;
			PlayerLifeUI.saluteButton.positionScale_X = 0.5f;
			PlayerLifeUI.saluteButton.positionScale_Y = 0.5f;
			PlayerLifeUI.saluteButton.sizeOffset_X = 100;
			PlayerLifeUI.saluteButton.sizeOffset_Y = 30;
			PlayerLifeUI.saluteButton.text = PlayerLifeUI.localization.format("Salute");
			SleekButton sleekButton7 = PlayerLifeUI.saluteButton;
			if (PlayerLifeUI.<>f__mg$cache5 == null)
			{
				PlayerLifeUI.<>f__mg$cache5 = new ClickedButton(PlayerLifeUI.onClickedSaluteButton);
			}
			sleekButton7.onClickedButton = PlayerLifeUI.<>f__mg$cache5;
			PlayerLifeUI.container.add(PlayerLifeUI.saluteButton);
			PlayerLifeUI.saluteButton.isVisible = false;
			PlayerLifeUI.restButton = new SleekButton();
			PlayerLifeUI.restButton.positionOffset_X = -110;
			PlayerLifeUI.restButton.positionOffset_Y = 65;
			PlayerLifeUI.restButton.positionScale_X = 0.5f;
			PlayerLifeUI.restButton.positionScale_Y = 0.5f;
			PlayerLifeUI.restButton.sizeOffset_X = 100;
			PlayerLifeUI.restButton.sizeOffset_Y = 30;
			PlayerLifeUI.restButton.text = PlayerLifeUI.localization.format("Rest");
			SleekButton sleekButton8 = PlayerLifeUI.restButton;
			if (PlayerLifeUI.<>f__mg$cache6 == null)
			{
				PlayerLifeUI.<>f__mg$cache6 = new ClickedButton(PlayerLifeUI.onClickedRestButton);
			}
			sleekButton8.onClickedButton = PlayerLifeUI.<>f__mg$cache6;
			PlayerLifeUI.container.add(PlayerLifeUI.restButton);
			PlayerLifeUI.restButton.isVisible = false;
			PlayerLifeUI.facepalmButton = new SleekButton();
			PlayerLifeUI.facepalmButton.positionOffset_X = 10;
			PlayerLifeUI.facepalmButton.positionOffset_Y = -95;
			PlayerLifeUI.facepalmButton.positionScale_X = 0.5f;
			PlayerLifeUI.facepalmButton.positionScale_Y = 0.5f;
			PlayerLifeUI.facepalmButton.sizeOffset_X = 100;
			PlayerLifeUI.facepalmButton.sizeOffset_Y = 30;
			PlayerLifeUI.facepalmButton.text = PlayerLifeUI.localization.format("Facepalm");
			SleekButton sleekButton9 = PlayerLifeUI.facepalmButton;
			if (PlayerLifeUI.<>f__mg$cache7 == null)
			{
				PlayerLifeUI.<>f__mg$cache7 = new ClickedButton(PlayerLifeUI.onClickedFacepalmButton);
			}
			sleekButton9.onClickedButton = PlayerLifeUI.<>f__mg$cache7;
			PlayerLifeUI.container.add(PlayerLifeUI.facepalmButton);
			PlayerLifeUI.facepalmButton.isVisible = false;
			PlayerLifeUI.hitmarkers = new HitmarkerInfo[16];
			for (int num3 = 0; num3 < PlayerLifeUI.hitmarkers.Length; num3++)
			{
				SleekImageTexture sleekImageTexture3 = new SleekImageTexture();
				sleekImageTexture3.positionOffset_X = -16;
				sleekImageTexture3.positionOffset_Y = -16;
				sleekImageTexture3.sizeOffset_X = 32;
				sleekImageTexture3.sizeOffset_Y = 32;
				sleekImageTexture3.texture = (Texture)PlayerLifeUI.icons.load("Hit_Entity");
				sleekImageTexture3.backgroundColor = OptionsSettings.hitmarkerColor;
				PlayerUI.window.add(sleekImageTexture3);
				sleekImageTexture3.isVisible = false;
				SleekImageTexture sleekImageTexture4 = new SleekImageTexture();
				sleekImageTexture4.positionOffset_X = -16;
				sleekImageTexture4.positionOffset_Y = -16;
				sleekImageTexture4.sizeOffset_X = 32;
				sleekImageTexture4.sizeOffset_Y = 32;
				sleekImageTexture4.texture = (Texture)PlayerLifeUI.icons.load("Hit_Critical");
				sleekImageTexture4.backgroundColor = OptionsSettings.criticalHitmarkerColor;
				PlayerUI.window.add(sleekImageTexture4);
				sleekImageTexture4.isVisible = false;
				SleekImageTexture sleekImageTexture5 = new SleekImageTexture();
				sleekImageTexture5.positionOffset_X = -16;
				sleekImageTexture5.positionOffset_Y = -16;
				sleekImageTexture5.sizeOffset_X = 32;
				sleekImageTexture5.sizeOffset_Y = 32;
				sleekImageTexture5.texture = (Texture)PlayerLifeUI.icons.load("Hit_Build");
				sleekImageTexture5.backgroundColor = OptionsSettings.crosshairColor;
				PlayerUI.window.add(sleekImageTexture5);
				sleekImageTexture5.isVisible = false;
				HitmarkerInfo hitmarkerInfo = new HitmarkerInfo();
				hitmarkerInfo.hit = EPlayerHit.NONE;
				hitmarkerInfo.hitEntitiyImage = sleekImageTexture3;
				hitmarkerInfo.hitCriticalImage = sleekImageTexture4;
				hitmarkerInfo.hitBuildImage = sleekImageTexture5;
				PlayerLifeUI.hitmarkers[num3] = hitmarkerInfo;
			}
			PlayerLifeUI.dotImage = new SleekImageTexture();
			PlayerLifeUI.dotImage.positionOffset_X = -4;
			PlayerLifeUI.dotImage.positionOffset_Y = -4;
			PlayerLifeUI.dotImage.positionScale_X = 0.5f;
			PlayerLifeUI.dotImage.positionScale_Y = 0.5f;
			PlayerLifeUI.dotImage.sizeOffset_X = 8;
			PlayerLifeUI.dotImage.sizeOffset_Y = 8;
			PlayerLifeUI.dotImage.texture = (Texture)PlayerLifeUI.icons.load("Dot");
			PlayerLifeUI.dotImage.backgroundColor = OptionsSettings.crosshairColor;
			PlayerLifeUI.container.add(PlayerLifeUI.dotImage);
			PlayerLifeUI.crosshairLeftImage = new SleekImageTexture();
			PlayerLifeUI.crosshairLeftImage.positionOffset_X = -4;
			PlayerLifeUI.crosshairLeftImage.positionOffset_Y = -4;
			PlayerLifeUI.crosshairLeftImage.positionScale_X = 0.5f;
			PlayerLifeUI.crosshairLeftImage.positionScale_Y = 0.5f;
			PlayerLifeUI.crosshairLeftImage.sizeOffset_X = 8;
			PlayerLifeUI.crosshairLeftImage.sizeOffset_Y = 8;
			PlayerLifeUI.crosshairLeftImage.texture = (Texture)PlayerLifeUI.icons.load("Crosshair_Right");
			PlayerLifeUI.crosshairLeftImage.backgroundColor = OptionsSettings.crosshairColor;
			PlayerLifeUI.container.add(PlayerLifeUI.crosshairLeftImage);
			PlayerLifeUI.crosshairLeftImage.isVisible = false;
			PlayerLifeUI.crosshairRightImage = new SleekImageTexture();
			PlayerLifeUI.crosshairRightImage.positionOffset_X = -4;
			PlayerLifeUI.crosshairRightImage.positionOffset_Y = -4;
			PlayerLifeUI.crosshairRightImage.positionScale_X = 0.5f;
			PlayerLifeUI.crosshairRightImage.positionScale_Y = 0.5f;
			PlayerLifeUI.crosshairRightImage.sizeOffset_X = 8;
			PlayerLifeUI.crosshairRightImage.sizeOffset_Y = 8;
			PlayerLifeUI.crosshairRightImage.texture = (Texture)PlayerLifeUI.icons.load("Crosshair_Left");
			PlayerLifeUI.crosshairRightImage.backgroundColor = OptionsSettings.crosshairColor;
			PlayerLifeUI.container.add(PlayerLifeUI.crosshairRightImage);
			PlayerLifeUI.crosshairRightImage.isVisible = false;
			PlayerLifeUI.crosshairDownImage = new SleekImageTexture();
			PlayerLifeUI.crosshairDownImage.positionOffset_X = -4;
			PlayerLifeUI.crosshairDownImage.positionOffset_Y = -4;
			PlayerLifeUI.crosshairDownImage.positionScale_X = 0.5f;
			PlayerLifeUI.crosshairDownImage.positionScale_Y = 0.5f;
			PlayerLifeUI.crosshairDownImage.sizeOffset_X = 8;
			PlayerLifeUI.crosshairDownImage.sizeOffset_Y = 8;
			PlayerLifeUI.crosshairDownImage.texture = (Texture)PlayerLifeUI.icons.load("Crosshair_Up");
			PlayerLifeUI.crosshairDownImage.backgroundColor = OptionsSettings.crosshairColor;
			PlayerLifeUI.container.add(PlayerLifeUI.crosshairDownImage);
			PlayerLifeUI.crosshairDownImage.isVisible = false;
			PlayerLifeUI.crosshairUpImage = new SleekImageTexture();
			PlayerLifeUI.crosshairUpImage.positionOffset_X = -4;
			PlayerLifeUI.crosshairUpImage.positionOffset_Y = -4;
			PlayerLifeUI.crosshairUpImage.positionScale_X = 0.5f;
			PlayerLifeUI.crosshairUpImage.positionScale_Y = 0.5f;
			PlayerLifeUI.crosshairUpImage.sizeOffset_X = 8;
			PlayerLifeUI.crosshairUpImage.sizeOffset_Y = 8;
			PlayerLifeUI.crosshairUpImage.texture = (Texture)PlayerLifeUI.icons.load("Crosshair_Down");
			PlayerLifeUI.crosshairUpImage.backgroundColor = OptionsSettings.crosshairColor;
			PlayerLifeUI.container.add(PlayerLifeUI.crosshairUpImage);
			PlayerLifeUI.crosshairUpImage.isVisible = false;
			PlayerLifeUI.lifeBox = new SleekBox();
			PlayerLifeUI.lifeBox.positionOffset_Y = -180;
			PlayerLifeUI.lifeBox.positionScale_Y = 1f;
			PlayerLifeUI.lifeBox.sizeOffset_Y = 180;
			PlayerLifeUI.lifeBox.sizeScale_X = 0.2f;
			PlayerLifeUI.container.add(PlayerLifeUI.lifeBox);
			PlayerLifeUI.healthIcon = new SleekImageTexture();
			PlayerLifeUI.healthIcon.positionOffset_X = 5;
			PlayerLifeUI.healthIcon.positionOffset_Y = 5;
			PlayerLifeUI.healthIcon.sizeOffset_X = 20;
			PlayerLifeUI.healthIcon.sizeOffset_Y = 20;
			PlayerLifeUI.healthIcon.texture = (Texture2D)PlayerLifeUI.icons.load("Health");
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.healthIcon);
			PlayerLifeUI.healthProgress = new SleekProgress(string.Empty);
			PlayerLifeUI.healthProgress.positionOffset_X = 30;
			PlayerLifeUI.healthProgress.positionOffset_Y = 10;
			PlayerLifeUI.healthProgress.sizeOffset_X = -40;
			PlayerLifeUI.healthProgress.sizeOffset_Y = 10;
			PlayerLifeUI.healthProgress.sizeScale_X = 1f;
			PlayerLifeUI.healthProgress.color = Palette.COLOR_R;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.healthProgress);
			PlayerLifeUI.foodIcon = new SleekImageTexture();
			PlayerLifeUI.foodIcon.positionOffset_X = 5;
			PlayerLifeUI.foodIcon.positionOffset_Y = 35;
			PlayerLifeUI.foodIcon.sizeOffset_X = 20;
			PlayerLifeUI.foodIcon.sizeOffset_Y = 20;
			PlayerLifeUI.foodIcon.texture = (Texture2D)PlayerLifeUI.icons.load("Food");
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.foodIcon);
			PlayerLifeUI.foodProgress = new SleekProgress(string.Empty);
			PlayerLifeUI.foodProgress.positionOffset_X = 30;
			PlayerLifeUI.foodProgress.positionOffset_Y = 40;
			PlayerLifeUI.foodProgress.sizeOffset_X = -40;
			PlayerLifeUI.foodProgress.sizeOffset_Y = 10;
			PlayerLifeUI.foodProgress.sizeScale_X = 1f;
			PlayerLifeUI.foodProgress.color = Palette.COLOR_O;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.foodProgress);
			PlayerLifeUI.waterIcon = new SleekImageTexture();
			PlayerLifeUI.waterIcon.positionOffset_X = 5;
			PlayerLifeUI.waterIcon.positionOffset_Y = 65;
			PlayerLifeUI.waterIcon.sizeOffset_X = 20;
			PlayerLifeUI.waterIcon.sizeOffset_Y = 20;
			PlayerLifeUI.waterIcon.texture = (Texture2D)PlayerLifeUI.icons.load("Water");
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.waterIcon);
			PlayerLifeUI.waterProgress = new SleekProgress(string.Empty);
			PlayerLifeUI.waterProgress.positionOffset_X = 30;
			PlayerLifeUI.waterProgress.positionOffset_Y = 70;
			PlayerLifeUI.waterProgress.sizeOffset_X = -40;
			PlayerLifeUI.waterProgress.sizeOffset_Y = 10;
			PlayerLifeUI.waterProgress.sizeScale_X = 1f;
			PlayerLifeUI.waterProgress.color = Palette.COLOR_B;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.waterProgress);
			PlayerLifeUI.virusIcon = new SleekImageTexture();
			PlayerLifeUI.virusIcon.positionOffset_X = 5;
			PlayerLifeUI.virusIcon.positionOffset_Y = 95;
			PlayerLifeUI.virusIcon.sizeOffset_X = 20;
			PlayerLifeUI.virusIcon.sizeOffset_Y = 20;
			PlayerLifeUI.virusIcon.texture = (Texture2D)PlayerLifeUI.icons.load("Virus");
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.virusIcon);
			PlayerLifeUI.virusProgress = new SleekProgress(string.Empty);
			PlayerLifeUI.virusProgress.positionOffset_X = 30;
			PlayerLifeUI.virusProgress.positionOffset_Y = 100;
			PlayerLifeUI.virusProgress.sizeOffset_X = -40;
			PlayerLifeUI.virusProgress.sizeOffset_Y = 10;
			PlayerLifeUI.virusProgress.sizeScale_X = 1f;
			PlayerLifeUI.virusProgress.color = Palette.COLOR_G;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.virusProgress);
			PlayerLifeUI.staminaIcon = new SleekImageTexture();
			PlayerLifeUI.staminaIcon.positionOffset_X = 5;
			PlayerLifeUI.staminaIcon.positionOffset_Y = 125;
			PlayerLifeUI.staminaIcon.sizeOffset_X = 20;
			PlayerLifeUI.staminaIcon.sizeOffset_Y = 20;
			PlayerLifeUI.staminaIcon.texture = (Texture2D)PlayerLifeUI.icons.load("Stamina");
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.staminaIcon);
			PlayerLifeUI.staminaProgress = new SleekProgress(string.Empty);
			PlayerLifeUI.staminaProgress.positionOffset_X = 30;
			PlayerLifeUI.staminaProgress.positionOffset_Y = 130;
			PlayerLifeUI.staminaProgress.sizeOffset_X = -40;
			PlayerLifeUI.staminaProgress.sizeOffset_Y = 10;
			PlayerLifeUI.staminaProgress.sizeScale_X = 1f;
			PlayerLifeUI.staminaProgress.color = Palette.COLOR_Y;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.staminaProgress);
			PlayerLifeUI.waveLabel = new SleekLabel();
			PlayerLifeUI.waveLabel.positionOffset_Y = 60;
			PlayerLifeUI.waveLabel.sizeOffset_Y = 30;
			PlayerLifeUI.waveLabel.sizeScale_X = 0.5f;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.waveLabel);
			PlayerLifeUI.waveLabel.isVisible = false;
			PlayerLifeUI.scoreLabel = new SleekLabel();
			PlayerLifeUI.scoreLabel.positionOffset_Y = 60;
			PlayerLifeUI.scoreLabel.positionScale_X = 0.5f;
			PlayerLifeUI.scoreLabel.sizeOffset_Y = 30;
			PlayerLifeUI.scoreLabel.sizeScale_X = 0.5f;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.scoreLabel);
			PlayerLifeUI.scoreLabel.isVisible = false;
			PlayerLifeUI.oxygenIcon = new SleekImageTexture();
			PlayerLifeUI.oxygenIcon.positionOffset_X = 5;
			PlayerLifeUI.oxygenIcon.positionOffset_Y = 155;
			PlayerLifeUI.oxygenIcon.sizeOffset_X = 20;
			PlayerLifeUI.oxygenIcon.sizeOffset_Y = 20;
			PlayerLifeUI.oxygenIcon.texture = (Texture2D)PlayerLifeUI.icons.load("Oxygen");
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.oxygenIcon);
			PlayerLifeUI.oxygenProgress = new SleekProgress(string.Empty);
			PlayerLifeUI.oxygenProgress.positionOffset_X = 30;
			PlayerLifeUI.oxygenProgress.positionOffset_Y = 160;
			PlayerLifeUI.oxygenProgress.sizeOffset_X = -40;
			PlayerLifeUI.oxygenProgress.sizeOffset_Y = 10;
			PlayerLifeUI.oxygenProgress.sizeScale_X = 1f;
			PlayerLifeUI.oxygenProgress.color = Color.white;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.oxygenProgress);
			PlayerLifeUI.vehicleBox = new SleekBox();
			PlayerLifeUI.vehicleBox.positionOffset_Y = -120;
			PlayerLifeUI.vehicleBox.positionScale_X = 0.8f;
			PlayerLifeUI.vehicleBox.positionScale_Y = 1f;
			PlayerLifeUI.vehicleBox.sizeOffset_Y = 120;
			PlayerLifeUI.vehicleBox.sizeScale_X = 0.2f;
			PlayerLifeUI.container.add(PlayerLifeUI.vehicleBox);
			PlayerLifeUI.vehicleBox.isVisible = false;
			PlayerLifeUI.fuelIcon = new SleekImageTexture();
			PlayerLifeUI.fuelIcon.positionOffset_X = 5;
			PlayerLifeUI.fuelIcon.positionOffset_Y = 5;
			PlayerLifeUI.fuelIcon.sizeOffset_X = 20;
			PlayerLifeUI.fuelIcon.sizeOffset_Y = 20;
			PlayerLifeUI.fuelIcon.texture = (Texture2D)PlayerLifeUI.icons.load("Fuel");
			PlayerLifeUI.vehicleBox.add(PlayerLifeUI.fuelIcon);
			PlayerLifeUI.fuelProgress = new SleekProgress(string.Empty);
			PlayerLifeUI.fuelProgress.positionOffset_X = 30;
			PlayerLifeUI.fuelProgress.positionOffset_Y = 10;
			PlayerLifeUI.fuelProgress.sizeOffset_X = -40;
			PlayerLifeUI.fuelProgress.sizeOffset_Y = 10;
			PlayerLifeUI.fuelProgress.sizeScale_X = 1f;
			PlayerLifeUI.fuelProgress.color = Palette.COLOR_Y;
			PlayerLifeUI.vehicleBox.add(PlayerLifeUI.fuelProgress);
			PlayerLifeUI.speedIcon = new SleekImageTexture();
			PlayerLifeUI.speedIcon.positionOffset_X = 5;
			PlayerLifeUI.speedIcon.positionOffset_Y = 35;
			PlayerLifeUI.speedIcon.sizeOffset_X = 20;
			PlayerLifeUI.speedIcon.sizeOffset_Y = 20;
			PlayerLifeUI.speedIcon.texture = (Texture2D)PlayerLifeUI.icons.load("Speed");
			PlayerLifeUI.vehicleBox.add(PlayerLifeUI.speedIcon);
			PlayerLifeUI.speedProgress = new SleekProgress((!OptionsSettings.metric) ? " mph" : " kph");
			PlayerLifeUI.speedProgress.positionOffset_X = 30;
			PlayerLifeUI.speedProgress.positionOffset_Y = 40;
			PlayerLifeUI.speedProgress.sizeOffset_X = -40;
			PlayerLifeUI.speedProgress.sizeOffset_Y = 10;
			PlayerLifeUI.speedProgress.sizeScale_X = 1f;
			PlayerLifeUI.speedProgress.color = Palette.COLOR_P;
			PlayerLifeUI.vehicleBox.add(PlayerLifeUI.speedProgress);
			PlayerLifeUI.hpIcon = new SleekImageTexture();
			PlayerLifeUI.hpIcon.positionOffset_X = 5;
			PlayerLifeUI.hpIcon.positionOffset_Y = 65;
			PlayerLifeUI.hpIcon.sizeOffset_X = 20;
			PlayerLifeUI.hpIcon.sizeOffset_Y = 20;
			PlayerLifeUI.hpIcon.texture = (Texture2D)PlayerLifeUI.icons.load("Health");
			PlayerLifeUI.vehicleBox.add(PlayerLifeUI.hpIcon);
			PlayerLifeUI.hpProgress = new SleekProgress(string.Empty);
			PlayerLifeUI.hpProgress.positionOffset_X = 30;
			PlayerLifeUI.hpProgress.positionOffset_Y = 70;
			PlayerLifeUI.hpProgress.sizeOffset_X = -40;
			PlayerLifeUI.hpProgress.sizeOffset_Y = 10;
			PlayerLifeUI.hpProgress.sizeScale_X = 1f;
			PlayerLifeUI.hpProgress.color = Palette.COLOR_R;
			PlayerLifeUI.vehicleBox.add(PlayerLifeUI.hpProgress);
			PlayerLifeUI.batteryChargeIcon = new SleekImageTexture();
			PlayerLifeUI.batteryChargeIcon.positionOffset_X = 5;
			PlayerLifeUI.batteryChargeIcon.positionOffset_Y = 95;
			PlayerLifeUI.batteryChargeIcon.sizeOffset_X = 20;
			PlayerLifeUI.batteryChargeIcon.sizeOffset_Y = 20;
			PlayerLifeUI.batteryChargeIcon.texture = (Texture2D)PlayerLifeUI.icons.load("Stamina");
			PlayerLifeUI.vehicleBox.add(PlayerLifeUI.batteryChargeIcon);
			PlayerLifeUI.batteryChargeProgress = new SleekProgress(string.Empty);
			PlayerLifeUI.batteryChargeProgress.positionOffset_X = 30;
			PlayerLifeUI.batteryChargeProgress.positionOffset_Y = 100;
			PlayerLifeUI.batteryChargeProgress.sizeOffset_X = -40;
			PlayerLifeUI.batteryChargeProgress.sizeOffset_Y = 10;
			PlayerLifeUI.batteryChargeProgress.sizeScale_X = 1f;
			PlayerLifeUI.batteryChargeProgress.color = Palette.COLOR_Y;
			PlayerLifeUI.vehicleBox.add(PlayerLifeUI.batteryChargeProgress);
			PlayerLifeUI.gasmaskBox = new SleekBox();
			PlayerLifeUI.gasmaskBox.positionOffset_X = -200;
			PlayerLifeUI.gasmaskBox.positionOffset_Y = -60;
			PlayerLifeUI.gasmaskBox.positionScale_X = 0.5f;
			PlayerLifeUI.gasmaskBox.positionScale_Y = 1f;
			PlayerLifeUI.gasmaskBox.sizeOffset_X = 400;
			PlayerLifeUI.gasmaskBox.sizeOffset_Y = 60;
			PlayerLifeUI.container.add(PlayerLifeUI.gasmaskBox);
			PlayerLifeUI.gasmaskBox.isVisible = false;
			PlayerLifeUI.gasmaskIcon = new SleekImageTexture();
			PlayerLifeUI.gasmaskIcon.positionOffset_X = 5;
			PlayerLifeUI.gasmaskIcon.positionOffset_Y = 5;
			PlayerLifeUI.gasmaskIcon.sizeOffset_X = 50;
			PlayerLifeUI.gasmaskIcon.sizeOffset_Y = 50;
			PlayerLifeUI.gasmaskBox.add(PlayerLifeUI.gasmaskIcon);
			PlayerLifeUI.gasmaskProgress = new SleekProgress(string.Empty);
			PlayerLifeUI.gasmaskProgress.positionOffset_X = 60;
			PlayerLifeUI.gasmaskProgress.positionOffset_Y = 10;
			PlayerLifeUI.gasmaskProgress.sizeOffset_X = -70;
			PlayerLifeUI.gasmaskProgress.sizeOffset_Y = 40;
			PlayerLifeUI.gasmaskProgress.sizeScale_X = 1f;
			PlayerLifeUI.gasmaskBox.add(PlayerLifeUI.gasmaskProgress);
			PlayerLifeUI.bleedingBox = new SleekBoxIcon((Texture2D)PlayerLifeUI.icons.load("Bleeding"));
			PlayerLifeUI.bleedingBox.positionOffset_Y = -60;
			PlayerLifeUI.bleedingBox.sizeOffset_X = 50;
			PlayerLifeUI.bleedingBox.sizeOffset_Y = 50;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.bleedingBox);
			PlayerLifeUI.bleedingBox.isVisible = false;
			PlayerLifeUI.brokenBox = new SleekBoxIcon((Texture2D)PlayerLifeUI.icons.load("Broken"));
			PlayerLifeUI.brokenBox.positionOffset_Y = -60;
			PlayerLifeUI.brokenBox.sizeOffset_X = 50;
			PlayerLifeUI.brokenBox.sizeOffset_Y = 50;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.brokenBox);
			PlayerLifeUI.brokenBox.isVisible = false;
			PlayerLifeUI.temperatureBox = new SleekBoxIcon(null);
			PlayerLifeUI.temperatureBox.positionOffset_Y = -60;
			PlayerLifeUI.temperatureBox.sizeOffset_X = 50;
			PlayerLifeUI.temperatureBox.sizeOffset_Y = 50;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.temperatureBox);
			PlayerLifeUI.temperatureBox.isVisible = false;
			PlayerLifeUI.starvedBox = new SleekBoxIcon((Texture2D)PlayerLifeUI.icons.load("Starved"));
			PlayerLifeUI.starvedBox.positionOffset_Y = -60;
			PlayerLifeUI.starvedBox.sizeOffset_X = 50;
			PlayerLifeUI.starvedBox.sizeOffset_Y = 50;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.starvedBox);
			PlayerLifeUI.starvedBox.isVisible = false;
			PlayerLifeUI.dehydratedBox = new SleekBoxIcon((Texture2D)PlayerLifeUI.icons.load("Dehydrated"));
			PlayerLifeUI.dehydratedBox.positionOffset_Y = -60;
			PlayerLifeUI.dehydratedBox.sizeOffset_X = 50;
			PlayerLifeUI.dehydratedBox.sizeOffset_Y = 50;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.dehydratedBox);
			PlayerLifeUI.dehydratedBox.isVisible = false;
			PlayerLifeUI.infectedBox = new SleekBoxIcon((Texture2D)PlayerLifeUI.icons.load("Infected"));
			PlayerLifeUI.infectedBox.positionOffset_Y = -60;
			PlayerLifeUI.infectedBox.sizeOffset_X = 50;
			PlayerLifeUI.infectedBox.sizeOffset_Y = 50;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.infectedBox);
			PlayerLifeUI.infectedBox.isVisible = false;
			PlayerLifeUI.drownedBox = new SleekBoxIcon((Texture2D)PlayerLifeUI.icons.load("Drowned"));
			PlayerLifeUI.drownedBox.positionOffset_Y = -60;
			PlayerLifeUI.drownedBox.sizeOffset_X = 50;
			PlayerLifeUI.drownedBox.sizeOffset_Y = 50;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.drownedBox);
			PlayerLifeUI.drownedBox.isVisible = false;
			PlayerLifeUI.moonBox = new SleekBoxIcon((Texture2D)PlayerLifeUI.icons.load("Moon"));
			PlayerLifeUI.moonBox.positionOffset_Y = -60;
			PlayerLifeUI.moonBox.sizeOffset_X = 50;
			PlayerLifeUI.moonBox.sizeOffset_Y = 50;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.moonBox);
			PlayerLifeUI.moonBox.isVisible = false;
			PlayerLifeUI.radiationBox = new SleekBoxIcon((Texture2D)PlayerLifeUI.icons.load("Deadzone"));
			PlayerLifeUI.radiationBox.positionOffset_Y = -60;
			PlayerLifeUI.radiationBox.sizeOffset_X = 50;
			PlayerLifeUI.radiationBox.sizeOffset_Y = 50;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.radiationBox);
			PlayerLifeUI.radiationBox.isVisible = false;
			PlayerLifeUI.safeBox = new SleekBoxIcon((Texture2D)PlayerLifeUI.icons.load("Safe"));
			PlayerLifeUI.safeBox.positionOffset_Y = -60;
			PlayerLifeUI.safeBox.sizeOffset_X = 50;
			PlayerLifeUI.safeBox.sizeOffset_Y = 50;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.safeBox);
			PlayerLifeUI.safeBox.isVisible = false;
			PlayerLifeUI.arrestBox = new SleekBoxIcon((Texture2D)PlayerLifeUI.icons.load("Arrest"));
			PlayerLifeUI.arrestBox.positionOffset_Y = -60;
			PlayerLifeUI.arrestBox.sizeOffset_X = 50;
			PlayerLifeUI.arrestBox.sizeOffset_Y = 50;
			PlayerLifeUI.lifeBox.add(PlayerLifeUI.arrestBox);
			PlayerLifeUI.arrestBox.isVisible = false;
			if (Level.info != null)
			{
				if (Level.info.type == ELevelType.ARENA)
				{
					PlayerLifeUI.levelTextBox.isVisible = true;
					PlayerLifeUI.levelNumberBox.isVisible = true;
					PlayerLifeUI.compassBox.positionOffset_Y = 60;
				}
				if (Level.info.type != ELevelType.SURVIVAL)
				{
					PlayerLifeUI.foodIcon.isVisible = false;
					PlayerLifeUI.foodProgress.isVisible = false;
					PlayerLifeUI.waterIcon.isVisible = false;
					PlayerLifeUI.waterProgress.isVisible = false;
					PlayerLifeUI.virusIcon.isVisible = false;
					PlayerLifeUI.virusProgress.isVisible = false;
					if (Level.info.type == ELevelType.HORDE)
					{
						PlayerLifeUI.oxygenIcon.isVisible = false;
						PlayerLifeUI.oxygenProgress.isVisible = false;
						PlayerLifeUI.waveLabel.isVisible = true;
						PlayerLifeUI.scoreLabel.isVisible = true;
						PlayerLifeUI.staminaIcon.positionOffset_Y = 35;
						PlayerLifeUI.staminaProgress.positionOffset_Y = 40;
						PlayerLifeUI.lifeBox.positionOffset_Y = -90;
						PlayerLifeUI.lifeBox.sizeOffset_Y = 90;
					}
					else
					{
						PlayerLifeUI.staminaIcon.positionOffset_Y = 35;
						PlayerLifeUI.staminaProgress.positionOffset_Y = 40;
						PlayerLifeUI.oxygenIcon.positionOffset_Y = 65;
						PlayerLifeUI.oxygenProgress.positionOffset_Y = 70;
						PlayerLifeUI.lifeBox.positionOffset_Y = -90;
						PlayerLifeUI.lifeBox.sizeOffset_Y = 90;
					}
				}
				else
				{
					PlayerLifeUI.moonBox.isVisible = LightingManager.isFullMoon;
					PlayerLifeUI.updateIcons();
				}
			}
			PlayerLife life = Player.player.life;
			Delegate onDamaged = life.onDamaged;
			if (PlayerLifeUI.<>f__mg$cache8 == null)
			{
				PlayerLifeUI.<>f__mg$cache8 = new Damaged(PlayerLifeUI.onDamaged);
			}
			life.onDamaged = (Damaged)Delegate.Combine(onDamaged, PlayerLifeUI.<>f__mg$cache8);
			PlayerLife life2 = Player.player.life;
			if (PlayerLifeUI.<>f__mg$cache9 == null)
			{
				PlayerLifeUI.<>f__mg$cache9 = new HealthUpdated(PlayerLifeUI.onHealthUpdated);
			}
			life2.onHealthUpdated = PlayerLifeUI.<>f__mg$cache9;
			PlayerLife life3 = Player.player.life;
			if (PlayerLifeUI.<>f__mg$cacheA == null)
			{
				PlayerLifeUI.<>f__mg$cacheA = new FoodUpdated(PlayerLifeUI.onFoodUpdated);
			}
			life3.onFoodUpdated = PlayerLifeUI.<>f__mg$cacheA;
			PlayerLife life4 = Player.player.life;
			if (PlayerLifeUI.<>f__mg$cacheB == null)
			{
				PlayerLifeUI.<>f__mg$cacheB = new WaterUpdated(PlayerLifeUI.onWaterUpdated);
			}
			life4.onWaterUpdated = PlayerLifeUI.<>f__mg$cacheB;
			PlayerLife life5 = Player.player.life;
			if (PlayerLifeUI.<>f__mg$cacheC == null)
			{
				PlayerLifeUI.<>f__mg$cacheC = new VirusUpdated(PlayerLifeUI.onVirusUpdated);
			}
			life5.onVirusUpdated = PlayerLifeUI.<>f__mg$cacheC;
			PlayerLife life6 = Player.player.life;
			if (PlayerLifeUI.<>f__mg$cacheD == null)
			{
				PlayerLifeUI.<>f__mg$cacheD = new StaminaUpdated(PlayerLifeUI.onStaminaUpdated);
			}
			life6.onStaminaUpdated = PlayerLifeUI.<>f__mg$cacheD;
			PlayerLife life7 = Player.player.life;
			if (PlayerLifeUI.<>f__mg$cacheE == null)
			{
				PlayerLifeUI.<>f__mg$cacheE = new OxygenUpdated(PlayerLifeUI.onOxygenUpdated);
			}
			life7.onOxygenUpdated = PlayerLifeUI.<>f__mg$cacheE;
			PlayerLife life8 = Player.player.life;
			if (PlayerLifeUI.<>f__mg$cacheF == null)
			{
				PlayerLifeUI.<>f__mg$cacheF = new BleedingUpdated(PlayerLifeUI.onBleedingUpdated);
			}
			life8.onBleedingUpdated = PlayerLifeUI.<>f__mg$cacheF;
			PlayerLife life9 = Player.player.life;
			if (PlayerLifeUI.<>f__mg$cache10 == null)
			{
				PlayerLifeUI.<>f__mg$cache10 = new BrokenUpdated(PlayerLifeUI.onBrokenUpdated);
			}
			life9.onBrokenUpdated = PlayerLifeUI.<>f__mg$cache10;
			PlayerLife life10 = Player.player.life;
			if (PlayerLifeUI.<>f__mg$cache11 == null)
			{
				PlayerLifeUI.<>f__mg$cache11 = new TemperatureUpdated(PlayerLifeUI.onTemperatureUpdated);
			}
			life10.onTemperatureUpdated = PlayerLifeUI.<>f__mg$cache11;
			PlayerLook look = Player.player.look;
			Delegate onPerspectiveUpdated = look.onPerspectiveUpdated;
			if (PlayerLifeUI.<>f__mg$cache12 == null)
			{
				PlayerLifeUI.<>f__mg$cache12 = new PerspectiveUpdated(PlayerLifeUI.onPerspectiveUpdated);
			}
			look.onPerspectiveUpdated = (PerspectiveUpdated)Delegate.Combine(onPerspectiveUpdated, PlayerLifeUI.<>f__mg$cache12);
			PlayerMovement movement = Player.player.movement;
			Delegate onSeated = movement.onSeated;
			if (PlayerLifeUI.<>f__mg$cache13 == null)
			{
				PlayerLifeUI.<>f__mg$cache13 = new Seated(PlayerLifeUI.onSeated);
			}
			movement.onSeated = (Seated)Delegate.Combine(onSeated, PlayerLifeUI.<>f__mg$cache13);
			PlayerMovement movement2 = Player.player.movement;
			Delegate onVehicleUpdated = movement2.onVehicleUpdated;
			if (PlayerLifeUI.<>f__mg$cache14 == null)
			{
				PlayerLifeUI.<>f__mg$cache14 = new VehicleUpdated(PlayerLifeUI.onVehicleUpdated);
			}
			movement2.onVehicleUpdated = (VehicleUpdated)Delegate.Combine(onVehicleUpdated, PlayerLifeUI.<>f__mg$cache14);
			PlayerMovement movement3 = Player.player.movement;
			Delegate onSafetyUpdated = movement3.onSafetyUpdated;
			if (PlayerLifeUI.<>f__mg$cache15 == null)
			{
				PlayerLifeUI.<>f__mg$cache15 = new SafetyUpdated(PlayerLifeUI.onSafetyUpdated);
			}
			movement3.onSafetyUpdated = (SafetyUpdated)Delegate.Combine(onSafetyUpdated, PlayerLifeUI.<>f__mg$cache15);
			PlayerMovement movement4 = Player.player.movement;
			Delegate onRadiationUpdated = movement4.onRadiationUpdated;
			if (PlayerLifeUI.<>f__mg$cache16 == null)
			{
				PlayerLifeUI.<>f__mg$cache16 = new RadiationUpdated(PlayerLifeUI.onRadiationUpdated);
			}
			movement4.onRadiationUpdated = (RadiationUpdated)Delegate.Combine(onRadiationUpdated, PlayerLifeUI.<>f__mg$cache16);
			PlayerAnimator animator = Player.player.animator;
			Delegate onGestureUpdated = animator.onGestureUpdated;
			if (PlayerLifeUI.<>f__mg$cache17 == null)
			{
				PlayerLifeUI.<>f__mg$cache17 = new GestureUpdated(PlayerLifeUI.onGestureUpdated);
			}
			animator.onGestureUpdated = (GestureUpdated)Delegate.Combine(onGestureUpdated, PlayerLifeUI.<>f__mg$cache17);
			PlayerInventory inventory = Player.player.inventory;
			Delegate onInventoryUpdated = inventory.onInventoryUpdated;
			if (PlayerLifeUI.<>f__mg$cache18 == null)
			{
				PlayerLifeUI.<>f__mg$cache18 = new InventoryUpdated(PlayerLifeUI.onInventoryUpdated);
			}
			inventory.onInventoryUpdated = (InventoryUpdated)Delegate.Combine(onInventoryUpdated, PlayerLifeUI.<>f__mg$cache18);
			PlayerVoice voice = Player.player.voice;
			Delegate onTalked = voice.onTalked;
			if (PlayerLifeUI.<>f__mg$cache19 == null)
			{
				PlayerLifeUI.<>f__mg$cache19 = new Talked(PlayerLifeUI.onTalked);
			}
			voice.onTalked = (Talked)Delegate.Combine(onTalked, PlayerLifeUI.<>f__mg$cache19);
			PlayerQuests quests = Player.player.quests;
			if (PlayerLifeUI.<>f__mg$cache1A == null)
			{
				PlayerLifeUI.<>f__mg$cache1A = new TrackedQuestUpdated(PlayerLifeUI.OnTrackedQuestUpdated);
			}
			quests.TrackedQuestUpdated += PlayerLifeUI.<>f__mg$cache1A;
			PlayerSkills skills = Player.player.skills;
			Delegate onExperienceUpdated = skills.onExperienceUpdated;
			if (PlayerLifeUI.<>f__mg$cache1B == null)
			{
				PlayerLifeUI.<>f__mg$cache1B = new ExperienceUpdated(PlayerLifeUI.onExperienceUpdated);
			}
			skills.onExperienceUpdated = (ExperienceUpdated)Delegate.Combine(onExperienceUpdated, PlayerLifeUI.<>f__mg$cache1B);
			Delegate onMoonUpdated = LightingManager.onMoonUpdated;
			if (PlayerLifeUI.<>f__mg$cache1C == null)
			{
				PlayerLifeUI.<>f__mg$cache1C = new MoonUpdated(PlayerLifeUI.onMoonUpdated);
			}
			LightingManager.onMoonUpdated = (MoonUpdated)Delegate.Combine(onMoonUpdated, PlayerLifeUI.<>f__mg$cache1C);
			Delegate onWaveUpdated = ZombieManager.onWaveUpdated;
			if (PlayerLifeUI.<>f__mg$cache1D == null)
			{
				PlayerLifeUI.<>f__mg$cache1D = new WaveUpdated(PlayerLifeUI.onWaveUpdated);
			}
			ZombieManager.onWaveUpdated = (WaveUpdated)Delegate.Combine(onWaveUpdated, PlayerLifeUI.<>f__mg$cache1D);
			PlayerClothing clothing = Player.player.clothing;
			Delegate onMaskUpdated = clothing.onMaskUpdated;
			if (PlayerLifeUI.<>f__mg$cache1E == null)
			{
				PlayerLifeUI.<>f__mg$cache1E = new MaskUpdated(PlayerLifeUI.onMaskUpdated);
			}
			clothing.onMaskUpdated = (MaskUpdated)Delegate.Combine(onMaskUpdated, PlayerLifeUI.<>f__mg$cache1E);
			PlayerLifeUI.onListed();
			Delegate onListed = ChatManager.onListed;
			if (PlayerLifeUI.<>f__mg$cache1F == null)
			{
				PlayerLifeUI.<>f__mg$cache1F = new Listed(PlayerLifeUI.onListed);
			}
			ChatManager.onListed = (Listed)Delegate.Combine(onListed, PlayerLifeUI.<>f__mg$cache1F);
			Delegate onVotingStart = ChatManager.onVotingStart;
			if (PlayerLifeUI.<>f__mg$cache20 == null)
			{
				PlayerLifeUI.<>f__mg$cache20 = new VotingStart(PlayerLifeUI.onVotingStart);
			}
			ChatManager.onVotingStart = (VotingStart)Delegate.Combine(onVotingStart, PlayerLifeUI.<>f__mg$cache20);
			Delegate onVotingUpdate = ChatManager.onVotingUpdate;
			if (PlayerLifeUI.<>f__mg$cache21 == null)
			{
				PlayerLifeUI.<>f__mg$cache21 = new VotingUpdate(PlayerLifeUI.onVotingUpdate);
			}
			ChatManager.onVotingUpdate = (VotingUpdate)Delegate.Combine(onVotingUpdate, PlayerLifeUI.<>f__mg$cache21);
			Delegate onVotingStop = ChatManager.onVotingStop;
			if (PlayerLifeUI.<>f__mg$cache22 == null)
			{
				PlayerLifeUI.<>f__mg$cache22 = new VotingStop(PlayerLifeUI.onVotingStop);
			}
			ChatManager.onVotingStop = (VotingStop)Delegate.Combine(onVotingStop, PlayerLifeUI.<>f__mg$cache22);
			Delegate onVotingMessage = ChatManager.onVotingMessage;
			if (PlayerLifeUI.<>f__mg$cache23 == null)
			{
				PlayerLifeUI.<>f__mg$cache23 = new VotingMessage(PlayerLifeUI.onVotingMessage);
			}
			ChatManager.onVotingMessage = (VotingMessage)Delegate.Combine(onVotingMessage, PlayerLifeUI.<>f__mg$cache23);
			Delegate onArenaMessageUpdated = LevelManager.onArenaMessageUpdated;
			if (PlayerLifeUI.<>f__mg$cache24 == null)
			{
				PlayerLifeUI.<>f__mg$cache24 = new ArenaMessageUpdated(PlayerLifeUI.onArenaMessageUpdated);
			}
			LevelManager.onArenaMessageUpdated = (ArenaMessageUpdated)Delegate.Combine(onArenaMessageUpdated, PlayerLifeUI.<>f__mg$cache24);
			Delegate onArenaPlayerUpdated = LevelManager.onArenaPlayerUpdated;
			if (PlayerLifeUI.<>f__mg$cache25 == null)
			{
				PlayerLifeUI.<>f__mg$cache25 = new ArenaPlayerUpdated(PlayerLifeUI.onArenaPlayerUpdated);
			}
			LevelManager.onArenaPlayerUpdated = (ArenaPlayerUpdated)Delegate.Combine(onArenaPlayerUpdated, PlayerLifeUI.<>f__mg$cache25);
			Delegate onLevelNumberUpdated = LevelManager.onLevelNumberUpdated;
			if (PlayerLifeUI.<>f__mg$cache26 == null)
			{
				PlayerLifeUI.<>f__mg$cache26 = new LevelNumberUpdated(PlayerLifeUI.onLevelNumberUpdated);
			}
			LevelManager.onLevelNumberUpdated = (LevelNumberUpdated)Delegate.Combine(onLevelNumberUpdated, PlayerLifeUI.<>f__mg$cache26);
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x0600388C RID: 14476 RVA: 0x0019CD72 File Offset: 0x0019B172
		public static Sleek container
		{
			get
			{
				return PlayerLifeUI._container;
			}
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x0019CD7C File Offset: 0x0019B17C
		public static void open()
		{
			if (PlayerLifeUI.active)
			{
				return;
			}
			PlayerLifeUI.active = true;
			if (PlayerLifeUI.npc != null)
			{
				PlayerLifeUI.npc.isLookingAtPlayer = false;
				PlayerLifeUI.npc = null;
			}
			PlayerLifeUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x0019CDD5 File Offset: 0x0019B1D5
		public static void close()
		{
			if (!PlayerLifeUI.active)
			{
				return;
			}
			PlayerLifeUI.active = false;
			PlayerLifeUI.closeChat();
			PlayerLifeUI.closeGestures();
			PlayerLifeUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x0019CE0C File Offset: 0x0019B20C
		public static void openChat()
		{
			if (PlayerLifeUI.chatting)
			{
				return;
			}
			PlayerLifeUI.chatting = true;
			PlayerLifeUI.chatField.text = string.Empty;
			PlayerLifeUI.chatField.lerpPositionOffset(100, 170, ESleekLerp.EXPONENTIAL, 20f);
			if (PlayerUI.chat == EChatMode.GLOBAL)
			{
				PlayerLifeUI.modeBox.text = PlayerLifeUI.localization.format("Mode_Global");
			}
			else if (PlayerUI.chat == EChatMode.LOCAL)
			{
				PlayerLifeUI.modeBox.text = PlayerLifeUI.localization.format("Mode_Local");
			}
			else if (PlayerUI.chat == EChatMode.GROUP)
			{
				PlayerLifeUI.modeBox.text = PlayerLifeUI.localization.format("Mode_Group");
			}
			else
			{
				PlayerLifeUI.modeBox.text = "?";
			}
			if (Player.player.channel.owner.isAdmin && !Provider.isServer)
			{
				PlayerLifeUI.modeBox.backgroundColor = Palette.ADMIN;
				PlayerLifeUI.modeBox.foregroundColor = Palette.ADMIN;
				PlayerLifeUI.chatField.backgroundColor = Palette.ADMIN;
				PlayerLifeUI.chatField.foregroundColor = Palette.ADMIN;
			}
			else if (Provider.isPro)
			{
				PlayerLifeUI.modeBox.backgroundColor = Palette.PRO;
				PlayerLifeUI.modeBox.foregroundColor = Palette.PRO;
				PlayerLifeUI.chatField.backgroundColor = Palette.PRO;
				PlayerLifeUI.chatField.foregroundColor = Palette.PRO;
			}
			else
			{
				PlayerLifeUI.modeBox.backgroundColor = Color.white;
				PlayerLifeUI.modeBox.foregroundColor = Color.white;
				PlayerLifeUI.chatField.backgroundColor = Color.white;
				PlayerLifeUI.chatField.foregroundColor = Color.white;
			}
			PlayerLifeUI.chatBox.state = new Vector2(0f, float.MaxValue);
			PlayerLifeUI.chatBox.isVisible = true;
			for (int i = 0; i < 4; i++)
			{
				PlayerLifeUI.gameLabel[i].isVisible = false;
			}
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x0019D008 File Offset: 0x0019B408
		public static void closeChat()
		{
			if (!PlayerLifeUI.chatting)
			{
				return;
			}
			PlayerLifeUI.chatting = false;
			PlayerLifeUI.chatField.lerpPositionOffset(-424, 170, ESleekLerp.EXPONENTIAL, 20f);
			PlayerLifeUI.chatBox.isVisible = false;
			for (int i = 0; i < 4; i++)
			{
				PlayerLifeUI.gameLabel[i].isVisible = true;
			}
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x0019D06C File Offset: 0x0019B46C
		public static void openGestures()
		{
			if (PlayerLifeUI.gesturing)
			{
				return;
			}
			PlayerLifeUI.gesturing = true;
			for (int i = 0; i < PlayerLifeUI.faceButtons.Length; i++)
			{
				PlayerLifeUI.faceButtons[i].isVisible = true;
			}
			bool isVisible = !Player.player.equipment.isSelected && Player.player.stance.stance != EPlayerStance.PRONE && Player.player.stance.stance != EPlayerStance.DRIVING && Player.player.stance.stance != EPlayerStance.SITTING;
			PlayerLifeUI.surrenderButton.isVisible = isVisible;
			PlayerLifeUI.pointButton.isVisible = isVisible;
			PlayerLifeUI.waveButton.isVisible = isVisible;
			PlayerLifeUI.saluteButton.isVisible = isVisible;
			PlayerLifeUI.restButton.isVisible = isVisible;
			PlayerLifeUI.facepalmButton.isVisible = isVisible;
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x0019D148 File Offset: 0x0019B548
		public static void closeGestures()
		{
			if (!PlayerLifeUI.gesturing)
			{
				return;
			}
			PlayerLifeUI.gesturing = false;
			for (int i = 0; i < PlayerLifeUI.faceButtons.Length; i++)
			{
				PlayerLifeUI.faceButtons[i].isVisible = false;
			}
			PlayerLifeUI.surrenderButton.isVisible = false;
			PlayerLifeUI.pointButton.isVisible = false;
			PlayerLifeUI.waveButton.isVisible = false;
			PlayerLifeUI.saluteButton.isVisible = false;
			PlayerLifeUI.restButton.isVisible = false;
			PlayerLifeUI.facepalmButton.isVisible = false;
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x0019D1CD File Offset: 0x0019B5CD
		private static void onDamaged(byte damage)
		{
			if (damage > 5)
			{
				PlayerUI.pain(Mathf.Clamp((float)damage / 40f, 0f, 1f));
			}
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x0019D1F4 File Offset: 0x0019B5F4
		private static void updateHotbarItem(ref int offset, ItemJar jar, byte index)
		{
			ushort num = 0;
			if (jar != null && jar.item != null)
			{
				num = jar.item.id;
			}
			SleekImageTexture sleekImageTexture = PlayerLifeUI.hotbarImages[(int)index];
			SleekLabel sleekLabel = PlayerLifeUI.hotbarLabels[(int)index];
			sleekImageTexture.isVisible = (num != 0);
			sleekLabel.isVisible = (num != 0);
			if (num != 0 && PlayerLifeUI.hotbarIDs[(int)index] != num)
			{
				ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, num);
				sleekImageTexture.sizeOffset_X = (int)(itemAsset.size_x * 25);
				sleekImageTexture.sizeOffset_Y = (int)(itemAsset.size_y * 25);
				ItemTool.getIcon(jar.item.id, jar.item.quality, jar.item.state, new ItemIconReady(sleekImageTexture.updateTexture));
			}
			PlayerLifeUI.hotbarIDs[(int)index] = num;
			sleekImageTexture.positionOffset_X = offset;
			sleekLabel.positionOffset_X = offset + sleekImageTexture.sizeOffset_X - 55;
			if (sleekImageTexture.isVisible)
			{
				offset += sleekImageTexture.sizeOffset_X;
				PlayerLifeUI.hotbarContainer.sizeOffset_X = offset;
				offset += 5;
				PlayerLifeUI.hotbarContainer.sizeOffset_Y = Mathf.Max(PlayerLifeUI.hotbarContainer.sizeOffset_Y, sleekImageTexture.sizeOffset_Y);
			}
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x0019D324 File Offset: 0x0019B724
		public static void updateHotbar()
		{
			PlayerLifeUI.hotbarContainer.isVisible = (!PlayerUI.messageBox.isVisible && !PlayerUI.messageBox2.isVisible && OptionsSettings.showHotbar);
			if (!PlayerLifeUI.hotbarContainer.isVisible)
			{
				return;
			}
			int num = 0;
			PlayerLifeUI.updateHotbarItem(ref num, Player.player.inventory.getItem(0, 0), 0);
			PlayerLifeUI.updateHotbarItem(ref num, Player.player.inventory.getItem(1, 0), 1);
			byte b = 0;
			while ((int)b < Player.player.equipment.hotkeys.Length)
			{
				HotkeyInfo hotkeyInfo = Player.player.equipment.hotkeys[(int)b];
				ItemJar itemJar = null;
				if (hotkeyInfo.id != 0)
				{
					byte index = Player.player.inventory.getIndex(hotkeyInfo.page, hotkeyInfo.x, hotkeyInfo.y);
					itemJar = Player.player.inventory.getItem(hotkeyInfo.page, index);
					if (itemJar != null && itemJar.item.id != hotkeyInfo.id)
					{
						itemJar = null;
					}
				}
				PlayerLifeUI.updateHotbarItem(ref num, itemJar, b + 2);
				b += 1;
			}
			PlayerLifeUI.hotbarContainer.positionOffset_X = PlayerLifeUI.hotbarContainer.sizeOffset_X / -2;
			PlayerLifeUI.hotbarContainer.positionOffset_Y = -80 - PlayerLifeUI.hotbarContainer.sizeOffset_Y;
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x0019D47C File Offset: 0x0019B87C
		public static void updateStatTracker()
		{
			EStatTrackerType estatTrackerType;
			int num;
			PlayerLifeUI.statTrackerLabel.isVisible = Player.player.equipment.getUseableStatTrackerValue(out estatTrackerType, out num);
			if (PlayerLifeUI.statTrackerLabel.isVisible)
			{
				PlayerLifeUI.statTrackerLabel.foregroundColor = Provider.provider.economyService.getStatTrackerColor(estatTrackerType);
				PlayerLifeUI.statTrackerLabel.text = PlayerLifeUI.localization.format((estatTrackerType != EStatTrackerType.TOTAL) ? "Stat_Tracker_Player_Kills" : "Stat_Tracker_Total_Kills", new object[]
				{
					num.ToString("D7")
				});
			}
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x0019D510 File Offset: 0x0019B910
		private static void onInventoryUpdated(byte page, byte index, ItemJar jar)
		{
			byte b;
			if (Player.player.equipment.isItemHotkeyed(page, index, jar, out b))
			{
				PlayerLifeUI.hotbarIDs[(int)b] = 0;
				PlayerLifeUI.updateHotbar();
			}
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x0019D544 File Offset: 0x0019B944
		public static void updateGrayscale()
		{
			GrayscaleEffect component = Player.player.animator.view.GetComponent<GrayscaleEffect>();
			GrayscaleEffect component2 = MainCamera.instance.GetComponent<GrayscaleEffect>();
			GrayscaleEffect component3 = Player.player.look.characterCamera.GetComponent<GrayscaleEffect>();
			if (Player.player.look.perspective == EPlayerPerspective.FIRST)
			{
				component.enabled = true;
				component2.enabled = false;
			}
			else
			{
				component.enabled = false;
				component2.enabled = true;
			}
			if (LevelLighting.vision == ELightingVision.CIVILIAN)
			{
				component.blend = 1f;
			}
			else if (Player.player.life.health < 50)
			{
				component.blend = (1f - (float)Player.player.life.health / 50f) * (1f - Player.player.skills.mastery(1, 3) * 0.75f);
			}
			else
			{
				component.blend = 0f;
			}
			component2.blend = component.blend;
			component3.blend = component.blend;
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x0019D654 File Offset: 0x0019BA54
		private static void onPerspectiveUpdated(EPlayerPerspective newPerspective)
		{
			PlayerLifeUI.updateGrayscale();
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x0019D65B File Offset: 0x0019BA5B
		private static void onHealthUpdated(byte newHealth)
		{
			PlayerLifeUI.healthProgress.state = (float)newHealth / 100f;
			PlayerLifeUI.onPerspectiveUpdated(Player.player.look.perspective);
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x0019D683 File Offset: 0x0019BA83
		private static void onFoodUpdated(byte newFood)
		{
			if (newFood == 0 != PlayerLifeUI.starvedBox.isVisible)
			{
				PlayerLifeUI.starvedBox.isVisible = (newFood == 0);
				PlayerLifeUI.updateIcons();
			}
			PlayerLifeUI.foodProgress.state = (float)newFood / 100f;
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x0019D6BD File Offset: 0x0019BABD
		private static void onWaterUpdated(byte newWater)
		{
			if (newWater == 0 != PlayerLifeUI.dehydratedBox.isVisible)
			{
				PlayerLifeUI.dehydratedBox.isVisible = (newWater == 0);
				PlayerLifeUI.updateIcons();
			}
			PlayerLifeUI.waterProgress.state = (float)newWater / 100f;
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x0019D6F7 File Offset: 0x0019BAF7
		private static void onVirusUpdated(byte newVirus)
		{
			if (newVirus == 0 != PlayerLifeUI.infectedBox.isVisible)
			{
				PlayerLifeUI.infectedBox.isVisible = (newVirus == 0);
				PlayerLifeUI.updateIcons();
			}
			PlayerLifeUI.virusProgress.state = (float)newVirus / 100f;
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x0019D731 File Offset: 0x0019BB31
		private static void onStaminaUpdated(byte newStamina)
		{
			PlayerLifeUI.staminaProgress.state = (float)newStamina / 100f;
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x0019D745 File Offset: 0x0019BB45
		private static void onOxygenUpdated(byte newOxygen)
		{
			if (newOxygen == 0 != PlayerLifeUI.drownedBox.isVisible)
			{
				PlayerLifeUI.drownedBox.isVisible = (newOxygen == 0);
				PlayerLifeUI.updateIcons();
			}
			PlayerLifeUI.oxygenProgress.state = (float)newOxygen / 100f;
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x0019D780 File Offset: 0x0019BB80
		private static void updateCompassElement(Sleek element, Color color, float viewAngle, float elementAngle)
		{
			float num = Mathf.DeltaAngle(viewAngle, elementAngle) / 22.5f;
			element.positionScale_X = num / 2f + 0.5f;
			color.a = 1f - Mathf.Abs(num);
			element.backgroundColor = color;
			element.foregroundColor = color;
			element.isVisible = (Mathf.Abs(num) < 1f);
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x0019D7E4 File Offset: 0x0019BBE4
		public static void updateCompass()
		{
			if (Provider.modeConfigData.Gameplay.Compass || (Level.info != null && Level.info.type == ELevelType.ARENA))
			{
				PlayerLifeUI.compassBox.isVisible = true;
			}
			else
			{
				InventorySearch inventorySearch = Player.player.inventory.has(1508);
				bool isVisible = inventorySearch != null;
				PlayerLifeUI.compassBox.isVisible = isVisible;
			}
			if (!PlayerLifeUI.compassBox.isVisible)
			{
				return;
			}
			float y = MainCamera.instance.transform.rotation.eulerAngles.y;
			for (int i = 0; i < PlayerLifeUI.compassLabels.Length; i++)
			{
				float elementAngle = (float)(i * 5);
				SleekLabel sleekLabel = PlayerLifeUI.compassLabels[i];
				PlayerLifeUI.updateCompassElement(sleekLabel, sleekLabel.foregroundColor, y, elementAngle);
			}
			PlayerLifeUI.compassMarkersContainer.remove();
			foreach (SteamPlayer steamPlayer in Provider.clients)
			{
				if (!(steamPlayer.model == null))
				{
					PlayerQuests quests = steamPlayer.player.quests;
					if (!(steamPlayer.playerID.steamID != Provider.client) || quests.isMemberOfSameGroupAs(Player.player))
					{
						if (quests.isMarkerPlaced)
						{
							SleekImageTexture sleekImageTexture = new SleekImageTexture((Texture2D)PlayerLifeUI.icons.load("Marker"));
							sleekImageTexture.positionOffset_X = -10;
							sleekImageTexture.positionOffset_Y = -5;
							sleekImageTexture.sizeOffset_X = 20;
							sleekImageTexture.sizeOffset_Y = 20;
							sleekImageTexture.backgroundColor = steamPlayer.markerColor;
							PlayerLifeUI.compassMarkersContainer.add(sleekImageTexture);
							float num = Mathf.Atan2(quests.markerPosition.x - MainCamera.instance.transform.position.x, quests.markerPosition.z - MainCamera.instance.transform.position.z);
							num *= 57.29578f;
							PlayerLifeUI.updateCompassElement(sleekImageTexture, sleekImageTexture.backgroundColor, y, num);
						}
					}
				}
			}
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x0019DA5C File Offset: 0x0019BE5C
		private static void updateIcons()
		{
			int num = 0;
			if (PlayerLifeUI.bleedingBox.isVisible)
			{
				num += 60;
			}
			PlayerLifeUI.brokenBox.positionOffset_X = num;
			if (PlayerLifeUI.brokenBox.isVisible)
			{
				num += 60;
			}
			PlayerLifeUI.temperatureBox.positionOffset_X = num;
			if (PlayerLifeUI.temperatureBox.isVisible)
			{
				num += 60;
			}
			PlayerLifeUI.starvedBox.positionOffset_X = num;
			if (PlayerLifeUI.starvedBox.isVisible)
			{
				num += 60;
			}
			PlayerLifeUI.dehydratedBox.positionOffset_X = num;
			if (PlayerLifeUI.dehydratedBox.isVisible)
			{
				num += 60;
			}
			PlayerLifeUI.infectedBox.positionOffset_X = num;
			if (PlayerLifeUI.infectedBox.isVisible)
			{
				num += 60;
			}
			PlayerLifeUI.drownedBox.positionOffset_X = num;
			if (PlayerLifeUI.drownedBox.isVisible)
			{
				num += 60;
			}
			PlayerLifeUI.moonBox.positionOffset_X = num;
			if (PlayerLifeUI.moonBox.isVisible)
			{
				num += 60;
			}
			PlayerLifeUI.radiationBox.positionOffset_X = num;
			if (PlayerLifeUI.radiationBox.isVisible)
			{
				num += 60;
			}
			PlayerLifeUI.safeBox.positionOffset_X = num;
			if (PlayerLifeUI.safeBox.isVisible)
			{
				num += 60;
			}
			PlayerLifeUI.arrestBox.positionOffset_X = num;
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x0019DBA1 File Offset: 0x0019BFA1
		private static void onBleedingUpdated(bool newBleeding)
		{
			PlayerLifeUI.bleedingBox.isVisible = newBleeding;
			PlayerLifeUI.updateIcons();
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x0019DBB3 File Offset: 0x0019BFB3
		private static void onBrokenUpdated(bool newBroken)
		{
			PlayerLifeUI.brokenBox.isVisible = newBroken;
			PlayerLifeUI.updateIcons();
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x0019DBC8 File Offset: 0x0019BFC8
		private static void onTemperatureUpdated(EPlayerTemperature newTemperature)
		{
			PlayerLifeUI.temperatureBox.isVisible = (newTemperature != EPlayerTemperature.NONE);
			switch (newTemperature)
			{
			case EPlayerTemperature.FREEZING:
				PlayerLifeUI.temperatureBox.icon = (Texture2D)PlayerLifeUI.icons.load("Freezing");
				goto IL_11A;
			case EPlayerTemperature.COLD:
				PlayerLifeUI.temperatureBox.icon = (Texture2D)PlayerLifeUI.icons.load("Cold");
				goto IL_11A;
			case EPlayerTemperature.WARM:
				PlayerLifeUI.temperatureBox.icon = (Texture2D)PlayerLifeUI.icons.load("Warm");
				goto IL_11A;
			case EPlayerTemperature.BURNING:
				PlayerLifeUI.temperatureBox.icon = (Texture2D)PlayerLifeUI.icons.load("Burning");
				goto IL_11A;
			case EPlayerTemperature.COVERED:
				PlayerLifeUI.temperatureBox.icon = (Texture2D)PlayerLifeUI.icons.load("Covered");
				goto IL_11A;
			case EPlayerTemperature.ACID:
				PlayerLifeUI.temperatureBox.icon = (Texture2D)PlayerLifeUI.icons.load("Acid");
				goto IL_11A;
			}
			PlayerLifeUI.temperatureBox.icon = null;
			IL_11A:
			PlayerLifeUI.updateIcons();
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x0019DCF4 File Offset: 0x0019C0F4
		private static void onMoonUpdated(bool isFullMoon)
		{
			PlayerLifeUI.moonBox.isVisible = isFullMoon;
			PlayerLifeUI.updateIcons();
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x0019DD06 File Offset: 0x0019C106
		private static void onExperienceUpdated(uint newExperience)
		{
			PlayerLifeUI.scoreLabel.text = PlayerLifeUI.localization.format("Score", new object[]
			{
				newExperience.ToString()
			});
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x0019DD38 File Offset: 0x0019C138
		private static void onWaveUpdated(bool newWaveReady, int newWaveIndex)
		{
			PlayerLifeUI.waveLabel.text = PlayerLifeUI.localization.format("Round", new object[]
			{
				newWaveIndex
			});
			if (newWaveReady)
			{
				PlayerUI.message(EPlayerMessage.WAVE_ON, string.Empty);
			}
			else
			{
				PlayerUI.message(EPlayerMessage.WAVE_OFF, string.Empty);
			}
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x0019DD90 File Offset: 0x0019C190
		private static void onSeated(bool isDriver, bool inVehicle, bool wasVehicle, InteractableVehicle oldVehicle, InteractableVehicle newVehicle)
		{
			if (isDriver && inVehicle)
			{
				int num = 5;
				PlayerLifeUI.fuelIcon.positionOffset_Y = num;
				PlayerLifeUI.fuelProgress.positionOffset_Y = num + 5;
				num += 30;
				PlayerLifeUI.speedIcon.positionOffset_Y = num;
				PlayerLifeUI.speedProgress.positionOffset_Y = num + 5;
				num += 30;
				PlayerLifeUI.hpIcon.isVisible = newVehicle.usesHealth;
				PlayerLifeUI.hpProgress.isVisible = newVehicle.usesHealth;
				if (newVehicle.usesHealth)
				{
					PlayerLifeUI.hpIcon.positionOffset_Y = num;
					PlayerLifeUI.hpProgress.positionOffset_Y = num + 5;
					num += 30;
				}
				PlayerLifeUI.batteryChargeIcon.isVisible = newVehicle.usesBattery;
				PlayerLifeUI.batteryChargeProgress.isVisible = newVehicle.usesBattery;
				if (newVehicle.usesBattery)
				{
					PlayerLifeUI.batteryChargeIcon.positionOffset_Y = num;
					PlayerLifeUI.batteryChargeProgress.positionOffset_Y = num + 5;
					num += 30;
				}
				PlayerLifeUI.vehicleBox.sizeOffset_Y = num - 5;
				PlayerLifeUI.vehicleBox.positionOffset_Y = -PlayerLifeUI.vehicleBox.sizeOffset_Y;
				if (newVehicle.passengers[(int)Player.player.movement.getSeat()].turret != null)
				{
					PlayerLifeUI.vehicleBox.positionOffset_Y -= 80;
				}
				PlayerLifeUI.vehicleBox.isVisible = true;
			}
			else
			{
				PlayerLifeUI.vehicleBox.isVisible = false;
			}
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x0019DEEC File Offset: 0x0019C2EC
		private static void onVehicleUpdated(bool isDriveable, ushort newFuel, ushort maxFuel, float newSpeed, float minSpeed, float maxSpeed, ushort newHealth, ushort maxHealth, ushort newBatteryCharge)
		{
			if (isDriveable)
			{
				PlayerLifeUI.fuelProgress.state = (float)newFuel / (float)maxFuel;
				float num = Mathf.Clamp(newSpeed, minSpeed, maxSpeed);
				if (num > 0f)
				{
					num /= maxSpeed;
				}
				else
				{
					num /= minSpeed;
				}
				PlayerLifeUI.speedProgress.state = num;
				if (OptionsSettings.metric)
				{
					PlayerLifeUI.speedProgress.measure = (int)MeasurementTool.speedToKPH(Mathf.Abs(newSpeed));
				}
				else
				{
					PlayerLifeUI.speedProgress.measure = (int)MeasurementTool.KPHToMPH(MeasurementTool.speedToKPH(Mathf.Abs(newSpeed)));
				}
				PlayerLifeUI.batteryChargeProgress.state = (float)newBatteryCharge / 10000f;
				PlayerLifeUI.hpProgress.state = (float)newHealth / (float)maxHealth;
			}
			PlayerLifeUI.vehicleBox.isVisible = isDriveable;
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x0019DFB0 File Offset: 0x0019C3B0
		private static void updateGasmask()
		{
			if (Player.player.movement.isRadiated)
			{
				ItemMaskAsset itemMaskAsset = (ItemMaskAsset)Assets.find(EAssetType.ITEM, Player.player.clothing.mask);
				if (itemMaskAsset != null && itemMaskAsset.proofRadiation)
				{
					ItemTool.getIcon(Player.player.clothing.mask, Player.player.clothing.maskQuality, Player.player.clothing.maskState, new ItemIconReady(PlayerLifeUI.gasmaskIcon.updateTexture));
					PlayerLifeUI.gasmaskProgress.state = (float)Player.player.clothing.maskQuality / 100f;
					PlayerLifeUI.gasmaskProgress.color = ItemTool.getQualityColor((float)Player.player.clothing.maskQuality / 100f);
					PlayerLifeUI.gasmaskBox.isVisible = true;
				}
				else
				{
					PlayerLifeUI.gasmaskBox.isVisible = false;
				}
			}
			else
			{
				PlayerLifeUI.gasmaskBox.isVisible = false;
			}
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x0019E0AF File Offset: 0x0019C4AF
		private static void onMaskUpdated(ushort newMask, byte newMaskQuality, byte[] newMaskState)
		{
			PlayerLifeUI.updateGasmask();
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x0019E0B6 File Offset: 0x0019C4B6
		private static void onSafetyUpdated(bool isSafe)
		{
			PlayerLifeUI.safeBox.isVisible = isSafe;
			PlayerLifeUI.updateIcons();
			if (isSafe)
			{
				PlayerUI.message(EPlayerMessage.SAFEZONE_ON, string.Empty);
			}
			else
			{
				PlayerUI.message(EPlayerMessage.SAFEZONE_OFF, string.Empty);
			}
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x0019E0EB File Offset: 0x0019C4EB
		private static void onRadiationUpdated(bool isRadiated)
		{
			PlayerLifeUI.radiationBox.isVisible = isRadiated;
			PlayerLifeUI.updateIcons();
			if (isRadiated)
			{
				PlayerUI.message(EPlayerMessage.DEADZONE_ON, string.Empty);
			}
			else
			{
				PlayerUI.message(EPlayerMessage.DEADZONE_OFF, string.Empty);
			}
			PlayerLifeUI.updateGasmask();
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x0019E125 File Offset: 0x0019C525
		private static void onGestureUpdated(EPlayerGesture gesture)
		{
			PlayerLifeUI.arrestBox.isVisible = (gesture == EPlayerGesture.ARREST_START);
			PlayerLifeUI.updateIcons();
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x0019E13B File Offset: 0x0019C53B
		private static void onTalked(bool isTalking)
		{
			PlayerLifeUI.voiceBox.isVisible = isTalking;
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x0019E148 File Offset: 0x0019C548
		private static void UpdateTrackedQuest()
		{
			QuestAsset questAsset = Assets.find(EAssetType.NPC, Player.player.quests.TrackedQuestID) as QuestAsset;
			if (questAsset == null)
			{
				PlayerLifeUI.trackedQuestTitle.isVisible = false;
				PlayerLifeUI.trackedQuestBar.isVisible = false;
				return;
			}
			PlayerLifeUI.trackedQuestTitle.text = questAsset.questName;
			bool flag = true;
			if (questAsset.conditions != null)
			{
				PlayerLifeUI.trackedQuestBar.remove();
				int num = 5;
				for (int i = 0; i < questAsset.conditions.Length; i++)
				{
					INPCCondition inpccondition = questAsset.conditions[i];
					if (inpccondition != null && !inpccondition.isConditionMet(Player.player))
					{
						string text = inpccondition.formatCondition(Player.player);
						if (!string.IsNullOrEmpty(text))
						{
							SleekLabel sleekLabel = new SleekLabel();
							sleekLabel.positionOffset_X = -300;
							sleekLabel.positionOffset_Y = num;
							sleekLabel.sizeOffset_X = 500;
							sleekLabel.sizeOffset_Y = 20;
							sleekLabel.isRich = true;
							sleekLabel.fontAlignment = TextAnchor.MiddleRight;
							sleekLabel.text = text;
							PlayerLifeUI.trackedQuestBar.add(sleekLabel);
							num += 20;
							flag = false;
						}
					}
				}
			}
			PlayerLifeUI.trackedQuestTitle.isVisible = !flag;
			PlayerLifeUI.trackedQuestBar.isVisible = PlayerLifeUI.trackedQuestTitle.isVisible;
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x0019E294 File Offset: 0x0019C694
		private static void OnTrackedQuestUpdated(PlayerQuests quests)
		{
			PlayerLifeUI.UpdateTrackedQuest();
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x0019E29C File Offset: 0x0019C69C
		private static void onListed()
		{
			for (int i = ChatManager.chat.Length - 1; i >= 0; i--)
			{
				Chat chat = ChatManager.chat[i];
				if (chat != null)
				{
					if (i == 0)
					{
						if (chat.player != null)
						{
							Texture2D texture;
							if (OptionsSettings.streamer)
							{
								texture = null;
							}
							else
							{
								texture = Provider.provider.communityService.getIcon(chat.player.playerID.steamID);
							}
							PlayerLifeUI.chatLabel[i].avatarImage.texture = texture;
							if (chat.player.player != null && chat.player.player.skills != null)
							{
								PlayerLifeUI.chatLabel[i].repImage.texture = PlayerTool.getRepTexture(chat.player.player.skills.reputation);
								PlayerLifeUI.chatLabel[i].repImage.backgroundColor = PlayerTool.getRepColor(chat.player.player.skills.reputation);
							}
							else
							{
								PlayerLifeUI.chatLabel[i].repImage.texture = null;
							}
						}
						else
						{
							PlayerLifeUI.chatLabel[i].avatarImage.texture = null;
							PlayerLifeUI.chatLabel[i].repImage.texture = null;
						}
					}
					else
					{
						if (i == ChatManager.chat.Length - 1 && PlayerLifeUI.chatLabel[i].avatarImage.texture != null)
						{
							UnityEngine.Object.DestroyImmediate(PlayerLifeUI.chatLabel[i].avatarImage.texture);
						}
						PlayerLifeUI.chatLabel[i].avatarImage.texture = PlayerLifeUI.chatLabel[i - 1].avatarImage.texture;
						PlayerLifeUI.chatLabel[i].repImage.texture = PlayerLifeUI.chatLabel[i - 1].repImage.texture;
						PlayerLifeUI.chatLabel[i].repImage.backgroundColor = PlayerLifeUI.chatLabel[i - 1].repImage.backgroundColor;
					}
					PlayerLifeUI.chatLabel[i].msg.foregroundColor = chat.color;
					PlayerLifeUI.chatLabel[i].msg.richOverrideColor = chat.color;
					PlayerLifeUI.chatLabel[i].msg.isRich = chat.isRich;
					if (chat.mode == EChatMode.GLOBAL)
					{
						PlayerLifeUI.chatLabel[i].msg.text = PlayerLifeUI.localization.format("Chat_Global", new object[]
						{
							chat.speaker,
							chat.text
						});
					}
					else if (chat.mode == EChatMode.LOCAL)
					{
						PlayerLifeUI.chatLabel[i].msg.text = PlayerLifeUI.localization.format("Chat_Local", new object[]
						{
							chat.speaker,
							chat.text
						});
					}
					else if (chat.mode == EChatMode.GROUP)
					{
						PlayerLifeUI.chatLabel[i].msg.text = PlayerLifeUI.localization.format("Chat_Group", new object[]
						{
							chat.speaker,
							chat.text
						});
					}
					else if (chat.mode == EChatMode.WELCOME)
					{
						PlayerLifeUI.chatLabel[i].msg.text = PlayerLifeUI.localization.format("Chat_Welcome", new object[]
						{
							chat.speaker,
							chat.text
						});
					}
					else if (chat.mode == EChatMode.SAY)
					{
						PlayerLifeUI.chatLabel[i].msg.text = PlayerLifeUI.localization.format("Chat_Say", new object[]
						{
							chat.speaker,
							chat.text
						});
					}
					else
					{
						PlayerLifeUI.chatLabel[i].msg.text = "?";
					}
				}
			}
			for (int j = 0; j < 4; j++)
			{
				PlayerLifeUI.gameLabel[j].avatarImage.texture = PlayerLifeUI.chatLabel[j].avatarImage.texture;
				PlayerLifeUI.gameLabel[j].repImage.texture = PlayerLifeUI.chatLabel[j].repImage.texture;
				PlayerLifeUI.gameLabel[j].repImage.backgroundColor = PlayerLifeUI.chatLabel[j].repImage.backgroundColor;
				PlayerLifeUI.gameLabel[j].msg.foregroundColor = PlayerLifeUI.chatLabel[j].msg.foregroundColor;
				PlayerLifeUI.gameLabel[j].msg.richOverrideColor = PlayerLifeUI.chatLabel[j].msg.richOverrideColor;
				PlayerLifeUI.gameLabel[j].msg.isRich = PlayerLifeUI.chatLabel[j].msg.isRich;
				PlayerLifeUI.gameLabel[j].msg.text = PlayerLifeUI.chatLabel[j].msg.text;
			}
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x0019E768 File Offset: 0x0019CB68
		private static void onVotingStart(SteamPlayer origin, SteamPlayer target, byte votesNeeded)
		{
			PlayerLifeUI.isVoteMessaged = false;
			PlayerLifeUI.voteBox.text = string.Empty;
			PlayerLifeUI.voteBox.isVisible = true;
			PlayerLifeUI.voteInfoLabel.isVisible = true;
			PlayerLifeUI.votesNeededLabel.isVisible = true;
			PlayerLifeUI.voteYesLabel.isVisible = true;
			PlayerLifeUI.voteNoLabel.isVisible = true;
			PlayerLifeUI.voteInfoLabel.text = PlayerLifeUI.localization.format("Vote_Kick", new object[]
			{
				origin.playerID.characterName,
				origin.playerID.playerName,
				target.playerID.characterName,
				target.playerID.playerName
			});
			PlayerLifeUI.votesNeededLabel.text = PlayerLifeUI.localization.format("Votes_Needed", new object[]
			{
				votesNeeded
			});
			PlayerLifeUI.voteYesLabel.text = PlayerLifeUI.localization.format("Vote_Yes", new object[]
			{
				KeyCode.F1,
				0
			});
			PlayerLifeUI.voteNoLabel.text = PlayerLifeUI.localization.format("Vote_No", new object[]
			{
				KeyCode.F2,
				0
			});
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x0019E8AC File Offset: 0x0019CCAC
		private static void onVotingUpdate(byte voteYes, byte voteNo)
		{
			PlayerLifeUI.voteYesLabel.text = PlayerLifeUI.localization.format("Vote_Yes", new object[]
			{
				KeyCode.F1,
				voteYes
			});
			PlayerLifeUI.voteNoLabel.text = PlayerLifeUI.localization.format("Vote_No", new object[]
			{
				KeyCode.F2,
				voteNo
			});
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x0019E924 File Offset: 0x0019CD24
		private static void onVotingStop(EVotingMessage message)
		{
			PlayerLifeUI.voteInfoLabel.isVisible = false;
			PlayerLifeUI.votesNeededLabel.isVisible = false;
			PlayerLifeUI.voteYesLabel.isVisible = false;
			PlayerLifeUI.voteNoLabel.isVisible = false;
			if (message == EVotingMessage.PASS)
			{
				PlayerLifeUI.voteBox.text = PlayerLifeUI.localization.format("Vote_Pass");
			}
			else if (message == EVotingMessage.FAIL)
			{
				PlayerLifeUI.voteBox.text = PlayerLifeUI.localization.format("Vote_Fail");
			}
			PlayerLifeUI.isVoteMessaged = true;
			PlayerLifeUI.lastVoteMessage = Time.realtimeSinceStartup;
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x0019E9B4 File Offset: 0x0019CDB4
		private static void onVotingMessage(EVotingMessage message)
		{
			PlayerLifeUI.voteBox.isVisible = true;
			PlayerLifeUI.voteInfoLabel.isVisible = false;
			PlayerLifeUI.votesNeededLabel.isVisible = false;
			PlayerLifeUI.voteYesLabel.isVisible = false;
			PlayerLifeUI.voteNoLabel.isVisible = false;
			if (message == EVotingMessage.OFF)
			{
				PlayerLifeUI.voteBox.text = PlayerLifeUI.localization.format("Vote_Off");
			}
			else if (message == EVotingMessage.DELAY)
			{
				PlayerLifeUI.voteBox.text = PlayerLifeUI.localization.format("Vote_Delay");
			}
			else if (message == EVotingMessage.PLAYERS)
			{
				PlayerLifeUI.voteBox.text = PlayerLifeUI.localization.format("Vote_Players");
			}
			PlayerLifeUI.isVoteMessaged = true;
			PlayerLifeUI.lastVoteMessage = Time.realtimeSinceStartup;
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x0019EA74 File Offset: 0x0019CE74
		private static void onArenaMessageUpdated(EArenaMessage newArenaMessage)
		{
			switch (newArenaMessage)
			{
			case EArenaMessage.LOBBY:
				PlayerLifeUI.levelTextBox.text = PlayerLifeUI.localization.format("Arena_Lobby");
				return;
			case EArenaMessage.WARMUP:
				PlayerLifeUI.levelTextBox.text = PlayerLifeUI.localization.format("Arena_Warm_Up");
				return;
			case EArenaMessage.PLAY:
				PlayerLifeUI.levelTextBox.text = PlayerLifeUI.localization.format("Arena_Play");
				return;
			case EArenaMessage.LOSE:
				PlayerLifeUI.levelTextBox.text = PlayerLifeUI.localization.format("Arena_Lose");
				return;
			case EArenaMessage.INTERMISSION:
				PlayerLifeUI.levelTextBox.text = PlayerLifeUI.localization.format("Arena_Intermission");
				return;
			}
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x0019EB30 File Offset: 0x0019CF30
		private static void onArenaPlayerUpdated(ulong[] playerIDs, EArenaMessage newArenaMessage)
		{
			List<SteamPlayer> list = new List<SteamPlayer>();
			for (int i = 0; i < playerIDs.Length; i++)
			{
				SteamPlayer steamPlayer = PlayerTool.getSteamPlayer(playerIDs[i]);
				if (steamPlayer != null)
				{
					list.Add(steamPlayer);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			string text = string.Empty;
			for (int j = 0; j < list.Count; j++)
			{
				SteamPlayer steamPlayer2 = list[j];
				if (j == 0)
				{
					text += steamPlayer2.playerID.characterName;
				}
				else if (j == list.Count - 1)
				{
					text = text + PlayerLifeUI.localization.format("List_Joint_1") + steamPlayer2.playerID.characterName;
				}
				else
				{
					text = text + PlayerLifeUI.localization.format("List_Joint_0") + steamPlayer2.playerID.characterName;
				}
			}
			if (newArenaMessage == EArenaMessage.DIED)
			{
				PlayerLifeUI.levelTextBox.text = PlayerLifeUI.localization.format("Arena_Died", new object[]
				{
					text
				});
				return;
			}
			if (newArenaMessage == EArenaMessage.ABANDONED)
			{
				PlayerLifeUI.levelTextBox.text = PlayerLifeUI.localization.format("Arena_Abandoned", new object[]
				{
					text
				});
				return;
			}
			if (newArenaMessage != EArenaMessage.WIN)
			{
				return;
			}
			PlayerLifeUI.levelTextBox.text = PlayerLifeUI.localization.format("Arena_Win", new object[]
			{
				text
			});
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x0019ECA6 File Offset: 0x0019D0A6
		private static void onLevelNumberUpdated(int newLevelNumber)
		{
			PlayerLifeUI.levelNumberBox.text = newLevelNumber.ToString();
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x0019ECC0 File Offset: 0x0019D0C0
		private static void onClickedFaceButton(SleekButton button)
		{
			byte b = 0;
			while ((int)b < PlayerLifeUI.faceButtons.Length)
			{
				if (PlayerLifeUI.faceButtons[(int)b] == button)
				{
					break;
				}
				b += 1;
			}
			Player.player.clothing.sendSwapFace(b);
			PlayerLifeUI.closeGestures();
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x0019ED10 File Offset: 0x0019D110
		private static void onClickedSurrenderButton(SleekButton button)
		{
			if (Player.player.animator.gesture == EPlayerGesture.SURRENDER_START)
			{
				Player.player.animator.sendGesture(EPlayerGesture.SURRENDER_STOP, true);
			}
			else
			{
				Player.player.animator.sendGesture(EPlayerGesture.SURRENDER_START, true);
			}
			PlayerLifeUI.closeGestures();
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x0019ED5E File Offset: 0x0019D15E
		private static void onClickedPointButton(SleekButton button)
		{
			Player.player.animator.sendGesture(EPlayerGesture.POINT, true);
			PlayerLifeUI.closeGestures();
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x0019ED76 File Offset: 0x0019D176
		private static void onClickedWaveButton(SleekButton button)
		{
			Player.player.animator.sendGesture(EPlayerGesture.WAVE, true);
			PlayerLifeUI.closeGestures();
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x0019ED8F File Offset: 0x0019D18F
		private static void onClickedSaluteButton(SleekButton button)
		{
			Player.player.animator.sendGesture(EPlayerGesture.SALUTE, true);
			PlayerLifeUI.closeGestures();
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x0019EDA8 File Offset: 0x0019D1A8
		private static void onClickedRestButton(SleekButton button)
		{
			if (Player.player.animator.gesture == EPlayerGesture.REST_START)
			{
				Player.player.animator.sendGesture(EPlayerGesture.REST_STOP, true);
			}
			else
			{
				Player.player.animator.sendGesture(EPlayerGesture.REST_START, true);
			}
			PlayerLifeUI.closeGestures();
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x0019EDF9 File Offset: 0x0019D1F9
		private static void onClickedFacepalmButton(SleekButton button)
		{
			Player.player.animator.sendGesture(EPlayerGesture.FACEPALM, true);
			PlayerLifeUI.closeGestures();
		}

		// Token: 0x04002AD3 RID: 10963
		public static Local localization;

		// Token: 0x04002AD4 RID: 10964
		public static Bundle icons;

		// Token: 0x04002AD5 RID: 10965
		private static Sleek _container;

		// Token: 0x04002AD6 RID: 10966
		public static bool active;

		// Token: 0x04002AD7 RID: 10967
		public static bool chatting;

		// Token: 0x04002AD8 RID: 10968
		public static bool gesturing;

		// Token: 0x04002AD9 RID: 10969
		public static InteractableObjectNPC npc;

		// Token: 0x04002ADA RID: 10970
		public static bool isVoteMessaged;

		// Token: 0x04002ADB RID: 10971
		public static float lastVoteMessage;

		// Token: 0x04002ADC RID: 10972
		private static SleekScrollBox chatBox;

		// Token: 0x04002ADD RID: 10973
		private static SleekChat[] chatLabel;

		// Token: 0x04002ADE RID: 10974
		private static SleekChat[] gameLabel;

		// Token: 0x04002ADF RID: 10975
		public static SleekField chatField;

		// Token: 0x04002AE0 RID: 10976
		private static SleekBox modeBox;

		// Token: 0x04002AE1 RID: 10977
		public static SleekBox voteBox;

		// Token: 0x04002AE2 RID: 10978
		private static SleekLabel voteInfoLabel;

		// Token: 0x04002AE3 RID: 10979
		private static SleekLabel votesNeededLabel;

		// Token: 0x04002AE4 RID: 10980
		private static SleekLabel voteYesLabel;

		// Token: 0x04002AE5 RID: 10981
		private static SleekLabel voteNoLabel;

		// Token: 0x04002AE6 RID: 10982
		private static SleekBoxIcon voiceBox;

		// Token: 0x04002AE7 RID: 10983
		private static SleekLabel trackedQuestTitle;

		// Token: 0x04002AE8 RID: 10984
		private static SleekImageTexture trackedQuestBar;

		// Token: 0x04002AE9 RID: 10985
		private static SleekBox levelTextBox;

		// Token: 0x04002AEA RID: 10986
		private static SleekBox levelNumberBox;

		// Token: 0x04002AEB RID: 10987
		public static SleekBox compassBox;

		// Token: 0x04002AEC RID: 10988
		private static Sleek compassLabelsContainer;

		// Token: 0x04002AED RID: 10989
		private static Sleek compassMarkersContainer;

		// Token: 0x04002AEE RID: 10990
		private static SleekLabel[] compassLabels;

		// Token: 0x04002AEF RID: 10991
		private static Sleek hotbarContainer;

		// Token: 0x04002AF0 RID: 10992
		private static SleekImageTexture[] hotbarImages;

		// Token: 0x04002AF1 RID: 10993
		private static SleekLabel[] hotbarLabels;

		// Token: 0x04002AF2 RID: 10994
		private static ushort[] hotbarIDs;

		// Token: 0x04002AF3 RID: 10995
		public static SleekLabel statTrackerLabel;

		// Token: 0x04002AF4 RID: 10996
		private static SleekButton[] faceButtons;

		// Token: 0x04002AF5 RID: 10997
		private static SleekButton surrenderButton;

		// Token: 0x04002AF6 RID: 10998
		private static SleekButton pointButton;

		// Token: 0x04002AF7 RID: 10999
		private static SleekButton waveButton;

		// Token: 0x04002AF8 RID: 11000
		private static SleekButton saluteButton;

		// Token: 0x04002AF9 RID: 11001
		private static SleekButton restButton;

		// Token: 0x04002AFA RID: 11002
		private static SleekButton facepalmButton;

		// Token: 0x04002AFB RID: 11003
		public static SleekImageTexture painImage;

		// Token: 0x04002AFC RID: 11004
		public static SleekImageTexture scopeImage;

		// Token: 0x04002AFD RID: 11005
		public static SleekImageTexture scopeOverlay;

		// Token: 0x04002AFE RID: 11006
		public static SleekImageTexture scopeLeftOverlay;

		// Token: 0x04002AFF RID: 11007
		public static SleekImageTexture scopeRightOverlay;

		// Token: 0x04002B00 RID: 11008
		public static SleekImageTexture scopeUpOverlay;

		// Token: 0x04002B01 RID: 11009
		public static SleekImageTexture scopeDownOverlay;

		// Token: 0x04002B02 RID: 11010
		public static SleekImageTexture binocularsOverlay;

		// Token: 0x04002B03 RID: 11011
		public static HitmarkerInfo[] hitmarkers;

		// Token: 0x04002B04 RID: 11012
		public static SleekImageTexture crosshairLeftImage;

		// Token: 0x04002B05 RID: 11013
		public static SleekImageTexture crosshairRightImage;

		// Token: 0x04002B06 RID: 11014
		public static SleekImageTexture crosshairDownImage;

		// Token: 0x04002B07 RID: 11015
		public static SleekImageTexture crosshairUpImage;

		// Token: 0x04002B08 RID: 11016
		public static SleekImageTexture dotImage;

		// Token: 0x04002B09 RID: 11017
		private static SleekBox lifeBox;

		// Token: 0x04002B0A RID: 11018
		private static SleekImageTexture healthIcon;

		// Token: 0x04002B0B RID: 11019
		private static SleekProgress healthProgress;

		// Token: 0x04002B0C RID: 11020
		private static SleekImageTexture foodIcon;

		// Token: 0x04002B0D RID: 11021
		private static SleekProgress foodProgress;

		// Token: 0x04002B0E RID: 11022
		private static SleekImageTexture waterIcon;

		// Token: 0x04002B0F RID: 11023
		private static SleekProgress waterProgress;

		// Token: 0x04002B10 RID: 11024
		private static SleekImageTexture virusIcon;

		// Token: 0x04002B11 RID: 11025
		private static SleekProgress virusProgress;

		// Token: 0x04002B12 RID: 11026
		private static SleekImageTexture staminaIcon;

		// Token: 0x04002B13 RID: 11027
		private static SleekProgress staminaProgress;

		// Token: 0x04002B14 RID: 11028
		private static SleekLabel waveLabel;

		// Token: 0x04002B15 RID: 11029
		private static SleekLabel scoreLabel;

		// Token: 0x04002B16 RID: 11030
		private static SleekImageTexture oxygenIcon;

		// Token: 0x04002B17 RID: 11031
		private static SleekProgress oxygenProgress;

		// Token: 0x04002B18 RID: 11032
		private static SleekBox vehicleBox;

		// Token: 0x04002B19 RID: 11033
		private static SleekImageTexture fuelIcon;

		// Token: 0x04002B1A RID: 11034
		private static SleekProgress fuelProgress;

		// Token: 0x04002B1B RID: 11035
		private static SleekBox gasmaskBox;

		// Token: 0x04002B1C RID: 11036
		private static SleekImageTexture gasmaskIcon;

		// Token: 0x04002B1D RID: 11037
		private static SleekProgress gasmaskProgress;

		// Token: 0x04002B1E RID: 11038
		private static SleekImageTexture speedIcon;

		// Token: 0x04002B1F RID: 11039
		private static SleekProgress speedProgress;

		// Token: 0x04002B20 RID: 11040
		private static SleekImageTexture batteryChargeIcon;

		// Token: 0x04002B21 RID: 11041
		private static SleekProgress batteryChargeProgress;

		// Token: 0x04002B22 RID: 11042
		private static SleekImageTexture hpIcon;

		// Token: 0x04002B23 RID: 11043
		private static SleekProgress hpProgress;

		// Token: 0x04002B24 RID: 11044
		private static SleekBoxIcon bleedingBox;

		// Token: 0x04002B25 RID: 11045
		private static SleekBoxIcon brokenBox;

		// Token: 0x04002B26 RID: 11046
		private static SleekBoxIcon temperatureBox;

		// Token: 0x04002B27 RID: 11047
		private static SleekBoxIcon starvedBox;

		// Token: 0x04002B28 RID: 11048
		private static SleekBoxIcon dehydratedBox;

		// Token: 0x04002B29 RID: 11049
		private static SleekBoxIcon infectedBox;

		// Token: 0x04002B2A RID: 11050
		private static SleekBoxIcon drownedBox;

		// Token: 0x04002B2B RID: 11051
		private static SleekBoxIcon moonBox;

		// Token: 0x04002B2C RID: 11052
		private static SleekBoxIcon radiationBox;

		// Token: 0x04002B2D RID: 11053
		private static SleekBoxIcon safeBox;

		// Token: 0x04002B2E RID: 11054
		private static SleekBoxIcon arrestBox;

		// Token: 0x04002B2F RID: 11055
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x04002B30 RID: 11056
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x04002B31 RID: 11057
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x04002B32 RID: 11058
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x04002B33 RID: 11059
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;

		// Token: 0x04002B34 RID: 11060
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;

		// Token: 0x04002B35 RID: 11061
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache6;

		// Token: 0x04002B36 RID: 11062
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache7;

		// Token: 0x04002B37 RID: 11063
		[CompilerGenerated]
		private static Damaged <>f__mg$cache8;

		// Token: 0x04002B38 RID: 11064
		[CompilerGenerated]
		private static HealthUpdated <>f__mg$cache9;

		// Token: 0x04002B39 RID: 11065
		[CompilerGenerated]
		private static FoodUpdated <>f__mg$cacheA;

		// Token: 0x04002B3A RID: 11066
		[CompilerGenerated]
		private static WaterUpdated <>f__mg$cacheB;

		// Token: 0x04002B3B RID: 11067
		[CompilerGenerated]
		private static VirusUpdated <>f__mg$cacheC;

		// Token: 0x04002B3C RID: 11068
		[CompilerGenerated]
		private static StaminaUpdated <>f__mg$cacheD;

		// Token: 0x04002B3D RID: 11069
		[CompilerGenerated]
		private static OxygenUpdated <>f__mg$cacheE;

		// Token: 0x04002B3E RID: 11070
		[CompilerGenerated]
		private static BleedingUpdated <>f__mg$cacheF;

		// Token: 0x04002B3F RID: 11071
		[CompilerGenerated]
		private static BrokenUpdated <>f__mg$cache10;

		// Token: 0x04002B40 RID: 11072
		[CompilerGenerated]
		private static TemperatureUpdated <>f__mg$cache11;

		// Token: 0x04002B41 RID: 11073
		[CompilerGenerated]
		private static PerspectiveUpdated <>f__mg$cache12;

		// Token: 0x04002B42 RID: 11074
		[CompilerGenerated]
		private static Seated <>f__mg$cache13;

		// Token: 0x04002B43 RID: 11075
		[CompilerGenerated]
		private static VehicleUpdated <>f__mg$cache14;

		// Token: 0x04002B44 RID: 11076
		[CompilerGenerated]
		private static SafetyUpdated <>f__mg$cache15;

		// Token: 0x04002B45 RID: 11077
		[CompilerGenerated]
		private static RadiationUpdated <>f__mg$cache16;

		// Token: 0x04002B46 RID: 11078
		[CompilerGenerated]
		private static GestureUpdated <>f__mg$cache17;

		// Token: 0x04002B47 RID: 11079
		[CompilerGenerated]
		private static InventoryUpdated <>f__mg$cache18;

		// Token: 0x04002B48 RID: 11080
		[CompilerGenerated]
		private static Talked <>f__mg$cache19;

		// Token: 0x04002B49 RID: 11081
		[CompilerGenerated]
		private static TrackedQuestUpdated <>f__mg$cache1A;

		// Token: 0x04002B4A RID: 11082
		[CompilerGenerated]
		private static ExperienceUpdated <>f__mg$cache1B;

		// Token: 0x04002B4B RID: 11083
		[CompilerGenerated]
		private static MoonUpdated <>f__mg$cache1C;

		// Token: 0x04002B4C RID: 11084
		[CompilerGenerated]
		private static WaveUpdated <>f__mg$cache1D;

		// Token: 0x04002B4D RID: 11085
		[CompilerGenerated]
		private static MaskUpdated <>f__mg$cache1E;

		// Token: 0x04002B4E RID: 11086
		[CompilerGenerated]
		private static Listed <>f__mg$cache1F;

		// Token: 0x04002B4F RID: 11087
		[CompilerGenerated]
		private static VotingStart <>f__mg$cache20;

		// Token: 0x04002B50 RID: 11088
		[CompilerGenerated]
		private static VotingUpdate <>f__mg$cache21;

		// Token: 0x04002B51 RID: 11089
		[CompilerGenerated]
		private static VotingStop <>f__mg$cache22;

		// Token: 0x04002B52 RID: 11090
		[CompilerGenerated]
		private static VotingMessage <>f__mg$cache23;

		// Token: 0x04002B53 RID: 11091
		[CompilerGenerated]
		private static ArenaMessageUpdated <>f__mg$cache24;

		// Token: 0x04002B54 RID: 11092
		[CompilerGenerated]
		private static ArenaPlayerUpdated <>f__mg$cache25;

		// Token: 0x04002B55 RID: 11093
		[CompilerGenerated]
		private static LevelNumberUpdated <>f__mg$cache26;
	}
}
