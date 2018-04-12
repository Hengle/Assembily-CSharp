using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007A1 RID: 1953
	public class PlayerNPCQuestUI
	{
		// Token: 0x060038D4 RID: 14548 RVA: 0x0019FED4 File Offset: 0x0019E2D4
		public PlayerNPCQuestUI()
		{
			if (PlayerNPCQuestUI.icons != null)
			{
				PlayerNPCQuestUI.icons.unload();
			}
			PlayerNPCQuestUI.localization = Localization.read("/Player/PlayerNPCQuest.dat");
			PlayerNPCQuestUI.icons = Bundles.getBundle("/Bundles/Textures/Player/Icons/PlayerNPCQuest/PlayerNPCQuest.unity3d");
			PlayerNPCQuestUI.container = new Sleek();
			PlayerNPCQuestUI.container.positionScale_Y = 1f;
			PlayerNPCQuestUI.container.positionOffset_X = 10;
			PlayerNPCQuestUI.container.positionOffset_Y = 10;
			PlayerNPCQuestUI.container.sizeOffset_X = -20;
			PlayerNPCQuestUI.container.sizeOffset_Y = -20;
			PlayerNPCQuestUI.container.sizeScale_X = 1f;
			PlayerNPCQuestUI.container.sizeScale_Y = 1f;
			PlayerUI.container.add(PlayerNPCQuestUI.container);
			PlayerNPCQuestUI.active = false;
			PlayerNPCQuestUI.questBox = new SleekBox();
			PlayerNPCQuestUI.questBox.positionOffset_X = -250;
			PlayerNPCQuestUI.questBox.positionScale_X = 0.5f;
			PlayerNPCQuestUI.questBox.sizeOffset_X = 500;
			PlayerNPCQuestUI.container.add(PlayerNPCQuestUI.questBox);
			PlayerNPCQuestUI.nameLabel = new SleekLabel();
			PlayerNPCQuestUI.nameLabel.positionOffset_X = 5;
			PlayerNPCQuestUI.nameLabel.positionOffset_Y = 5;
			PlayerNPCQuestUI.nameLabel.sizeOffset_X = -10;
			PlayerNPCQuestUI.nameLabel.sizeOffset_Y = 30;
			PlayerNPCQuestUI.nameLabel.sizeScale_X = 1f;
			PlayerNPCQuestUI.nameLabel.fontAlignment = TextAnchor.UpperLeft;
			PlayerNPCQuestUI.nameLabel.foregroundTint = ESleekTint.NONE;
			PlayerNPCQuestUI.nameLabel.isRich = true;
			PlayerNPCQuestUI.nameLabel.fontSize = 14;
			PlayerNPCQuestUI.questBox.add(PlayerNPCQuestUI.nameLabel);
			PlayerNPCQuestUI.descriptionLabel = new SleekLabel();
			PlayerNPCQuestUI.descriptionLabel.positionOffset_X = 5;
			PlayerNPCQuestUI.descriptionLabel.positionOffset_Y = 30;
			PlayerNPCQuestUI.descriptionLabel.sizeOffset_X = -10;
			PlayerNPCQuestUI.descriptionLabel.sizeOffset_Y = -35;
			PlayerNPCQuestUI.descriptionLabel.sizeScale_X = 1f;
			PlayerNPCQuestUI.descriptionLabel.sizeScale_Y = 1f;
			PlayerNPCQuestUI.descriptionLabel.fontAlignment = TextAnchor.UpperLeft;
			PlayerNPCQuestUI.descriptionLabel.foregroundTint = ESleekTint.NONE;
			PlayerNPCQuestUI.descriptionLabel.isRich = true;
			PlayerNPCQuestUI.questBox.add(PlayerNPCQuestUI.descriptionLabel);
			PlayerNPCQuestUI.detailsBox = new SleekScrollBox();
			PlayerNPCQuestUI.detailsBox.positionOffset_X = 5;
			PlayerNPCQuestUI.detailsBox.positionScale_Y = 1f;
			PlayerNPCQuestUI.detailsBox.sizeScale_X = 1f;
			PlayerNPCQuestUI.questBox.add(PlayerNPCQuestUI.detailsBox);
			PlayerNPCQuestUI.conditionsLabel = new SleekLabel();
			PlayerNPCQuestUI.conditionsLabel.sizeOffset_X = -30;
			PlayerNPCQuestUI.conditionsLabel.sizeOffset_Y = 30;
			PlayerNPCQuestUI.conditionsLabel.sizeScale_X = 1f;
			PlayerNPCQuestUI.conditionsLabel.fontAlignment = TextAnchor.MiddleLeft;
			PlayerNPCQuestUI.conditionsLabel.text = PlayerNPCQuestUI.localization.format("Conditions");
			PlayerNPCQuestUI.conditionsLabel.fontSize = 14;
			PlayerNPCQuestUI.detailsBox.add(PlayerNPCQuestUI.conditionsLabel);
			PlayerNPCQuestUI.conditionsContainer = new Sleek();
			PlayerNPCQuestUI.conditionsContainer.positionOffset_Y = 30;
			PlayerNPCQuestUI.conditionsContainer.sizeOffset_X = -30;
			PlayerNPCQuestUI.conditionsContainer.sizeScale_X = 1f;
			PlayerNPCQuestUI.detailsBox.add(PlayerNPCQuestUI.conditionsContainer);
			PlayerNPCQuestUI.rewardsLabel = new SleekLabel();
			PlayerNPCQuestUI.rewardsLabel.sizeOffset_X = -30;
			PlayerNPCQuestUI.rewardsLabel.sizeOffset_Y = 30;
			PlayerNPCQuestUI.rewardsLabel.sizeScale_X = 1f;
			PlayerNPCQuestUI.rewardsLabel.fontAlignment = TextAnchor.MiddleLeft;
			PlayerNPCQuestUI.rewardsLabel.text = PlayerNPCQuestUI.localization.format("Rewards");
			PlayerNPCQuestUI.rewardsLabel.fontSize = 14;
			PlayerNPCQuestUI.detailsBox.add(PlayerNPCQuestUI.rewardsLabel);
			PlayerNPCQuestUI.rewardsContainer = new Sleek();
			PlayerNPCQuestUI.rewardsContainer.sizeOffset_X = -30;
			PlayerNPCQuestUI.rewardsContainer.sizeScale_X = 1f;
			PlayerNPCQuestUI.detailsBox.add(PlayerNPCQuestUI.rewardsContainer);
			PlayerNPCQuestUI.beginContainer = new Sleek();
			PlayerNPCQuestUI.beginContainer.positionOffset_Y = 10;
			PlayerNPCQuestUI.beginContainer.positionScale_Y = 1f;
			PlayerNPCQuestUI.beginContainer.sizeOffset_Y = 50;
			PlayerNPCQuestUI.beginContainer.sizeScale_X = 1f;
			PlayerNPCQuestUI.questBox.add(PlayerNPCQuestUI.beginContainer);
			PlayerNPCQuestUI.beginContainer.isVisible = false;
			PlayerNPCQuestUI.endContainer = new Sleek();
			PlayerNPCQuestUI.endContainer.positionOffset_Y = 10;
			PlayerNPCQuestUI.endContainer.positionScale_Y = 1f;
			PlayerNPCQuestUI.endContainer.sizeOffset_Y = 50;
			PlayerNPCQuestUI.endContainer.sizeScale_X = 1f;
			PlayerNPCQuestUI.questBox.add(PlayerNPCQuestUI.endContainer);
			PlayerNPCQuestUI.endContainer.isVisible = false;
			PlayerNPCQuestUI.detailsContainer = new Sleek();
			PlayerNPCQuestUI.detailsContainer.positionOffset_Y = 10;
			PlayerNPCQuestUI.detailsContainer.positionScale_Y = 1f;
			PlayerNPCQuestUI.detailsContainer.sizeOffset_Y = 50;
			PlayerNPCQuestUI.detailsContainer.sizeScale_X = 1f;
			PlayerNPCQuestUI.questBox.add(PlayerNPCQuestUI.detailsContainer);
			PlayerNPCQuestUI.detailsContainer.isVisible = false;
			PlayerNPCQuestUI.acceptButton = new SleekButton();
			PlayerNPCQuestUI.acceptButton.sizeOffset_X = -5;
			PlayerNPCQuestUI.acceptButton.sizeScale_X = 0.5f;
			PlayerNPCQuestUI.acceptButton.sizeScale_Y = 1f;
			PlayerNPCQuestUI.acceptButton.text = PlayerNPCQuestUI.localization.format("Accept");
			PlayerNPCQuestUI.acceptButton.tooltip = PlayerNPCQuestUI.localization.format("Accept_Tooltip");
			PlayerNPCQuestUI.acceptButton.fontSize = 14;
			SleekButton sleekButton = PlayerNPCQuestUI.acceptButton;
			if (PlayerNPCQuestUI.<>f__mg$cache0 == null)
			{
				PlayerNPCQuestUI.<>f__mg$cache0 = new ClickedButton(PlayerNPCQuestUI.onClickedAcceptButton);
			}
			sleekButton.onClickedButton = PlayerNPCQuestUI.<>f__mg$cache0;
			PlayerNPCQuestUI.beginContainer.add(PlayerNPCQuestUI.acceptButton);
			PlayerNPCQuestUI.declineButton = new SleekButton();
			PlayerNPCQuestUI.declineButton.positionOffset_X = 5;
			PlayerNPCQuestUI.declineButton.positionScale_X = 0.5f;
			PlayerNPCQuestUI.declineButton.sizeOffset_X = -5;
			PlayerNPCQuestUI.declineButton.sizeScale_X = 0.5f;
			PlayerNPCQuestUI.declineButton.sizeScale_Y = 1f;
			PlayerNPCQuestUI.declineButton.text = PlayerNPCQuestUI.localization.format("Decline");
			PlayerNPCQuestUI.declineButton.tooltip = PlayerNPCQuestUI.localization.format("Decline_Tooltip");
			PlayerNPCQuestUI.declineButton.fontSize = 14;
			SleekButton sleekButton2 = PlayerNPCQuestUI.declineButton;
			if (PlayerNPCQuestUI.<>f__mg$cache1 == null)
			{
				PlayerNPCQuestUI.<>f__mg$cache1 = new ClickedButton(PlayerNPCQuestUI.onClickedDeclineButton);
			}
			sleekButton2.onClickedButton = PlayerNPCQuestUI.<>f__mg$cache1;
			PlayerNPCQuestUI.beginContainer.add(PlayerNPCQuestUI.declineButton);
			PlayerNPCQuestUI.continueButton = new SleekButton();
			PlayerNPCQuestUI.continueButton.sizeScale_X = 1f;
			PlayerNPCQuestUI.continueButton.sizeScale_Y = 1f;
			PlayerNPCQuestUI.continueButton.text = PlayerNPCQuestUI.localization.format("Continue");
			PlayerNPCQuestUI.continueButton.tooltip = PlayerNPCQuestUI.localization.format("Continue_Tooltip");
			PlayerNPCQuestUI.continueButton.fontSize = 14;
			SleekButton sleekButton3 = PlayerNPCQuestUI.continueButton;
			if (PlayerNPCQuestUI.<>f__mg$cache2 == null)
			{
				PlayerNPCQuestUI.<>f__mg$cache2 = new ClickedButton(PlayerNPCQuestUI.onClickedContinueButton);
			}
			sleekButton3.onClickedButton = PlayerNPCQuestUI.<>f__mg$cache2;
			PlayerNPCQuestUI.endContainer.add(PlayerNPCQuestUI.continueButton);
			PlayerNPCQuestUI.trackButton = new SleekButton();
			PlayerNPCQuestUI.trackButton.sizeOffset_X = -5;
			PlayerNPCQuestUI.trackButton.sizeScale_X = 0.333f;
			PlayerNPCQuestUI.trackButton.sizeScale_Y = 1f;
			PlayerNPCQuestUI.trackButton.tooltip = PlayerNPCQuestUI.localization.format("Track_Tooltip");
			PlayerNPCQuestUI.trackButton.fontSize = 14;
			SleekButton sleekButton4 = PlayerNPCQuestUI.trackButton;
			if (PlayerNPCQuestUI.<>f__mg$cache3 == null)
			{
				PlayerNPCQuestUI.<>f__mg$cache3 = new ClickedButton(PlayerNPCQuestUI.onClickedTrackButton);
			}
			sleekButton4.onClickedButton = PlayerNPCQuestUI.<>f__mg$cache3;
			PlayerNPCQuestUI.detailsContainer.add(PlayerNPCQuestUI.trackButton);
			PlayerNPCQuestUI.abandonButton = new SleekButton();
			PlayerNPCQuestUI.abandonButton.positionOffset_X = 5;
			PlayerNPCQuestUI.abandonButton.positionScale_X = 0.333f;
			PlayerNPCQuestUI.abandonButton.sizeOffset_X = -10;
			PlayerNPCQuestUI.abandonButton.sizeScale_X = 0.333f;
			PlayerNPCQuestUI.abandonButton.sizeScale_Y = 1f;
			PlayerNPCQuestUI.abandonButton.text = PlayerNPCQuestUI.localization.format("Abandon");
			PlayerNPCQuestUI.abandonButton.tooltip = PlayerNPCQuestUI.localization.format("Abandon_Tooltip");
			PlayerNPCQuestUI.abandonButton.fontSize = 14;
			SleekButton sleekButton5 = PlayerNPCQuestUI.abandonButton;
			if (PlayerNPCQuestUI.<>f__mg$cache4 == null)
			{
				PlayerNPCQuestUI.<>f__mg$cache4 = new ClickedButton(PlayerNPCQuestUI.onClickedAbandonButton);
			}
			sleekButton5.onClickedButton = PlayerNPCQuestUI.<>f__mg$cache4;
			PlayerNPCQuestUI.detailsContainer.add(PlayerNPCQuestUI.abandonButton);
			PlayerNPCQuestUI.returnButton = new SleekButton();
			PlayerNPCQuestUI.returnButton.positionOffset_X = 5;
			PlayerNPCQuestUI.returnButton.positionScale_X = 0.667f;
			PlayerNPCQuestUI.returnButton.sizeOffset_X = -5;
			PlayerNPCQuestUI.returnButton.sizeScale_X = 0.333f;
			PlayerNPCQuestUI.returnButton.sizeScale_Y = 1f;
			PlayerNPCQuestUI.returnButton.text = PlayerNPCQuestUI.localization.format("Return");
			PlayerNPCQuestUI.returnButton.tooltip = PlayerNPCQuestUI.localization.format("Return_Tooltip");
			PlayerNPCQuestUI.returnButton.fontSize = 14;
			SleekButton sleekButton6 = PlayerNPCQuestUI.returnButton;
			if (PlayerNPCQuestUI.<>f__mg$cache5 == null)
			{
				PlayerNPCQuestUI.<>f__mg$cache5 = new ClickedButton(PlayerNPCQuestUI.onClickedReturnButton);
			}
			sleekButton6.onClickedButton = PlayerNPCQuestUI.<>f__mg$cache5;
			PlayerNPCQuestUI.detailsContainer.add(PlayerNPCQuestUI.returnButton);
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x001A07A2 File Offset: 0x0019EBA2
		public static void open(QuestAsset newQuest, DialogueResponse newResponse, DialogueAsset newAcceptDialogue, DialogueAsset newDeclineDialogue, EQuestViewMode newMode)
		{
			if (PlayerNPCQuestUI.active)
			{
				return;
			}
			PlayerNPCQuestUI.active = true;
			PlayerNPCQuestUI.updateQuest(newQuest, newResponse, newAcceptDialogue, newDeclineDialogue, newMode);
			PlayerNPCQuestUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x001A07DA File Offset: 0x0019EBDA
		public static void close()
		{
			if (!PlayerNPCQuestUI.active)
			{
				return;
			}
			PlayerNPCQuestUI.active = false;
			PlayerNPCQuestUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x001A0808 File Offset: 0x0019EC08
		public static void closeNicely()
		{
			PlayerNPCQuestUI.close();
			if (PlayerNPCQuestUI.mode == EQuestViewMode.BEGIN)
			{
				PlayerNPCDialogueUI.open(PlayerNPCQuestUI.declineDialogue, null);
			}
			else if (PlayerNPCQuestUI.mode == EQuestViewMode.END)
			{
				PlayerNPCDialogueUI.registerResponse(PlayerNPCQuestUI.declineDialogue, PlayerNPCQuestUI.response);
				PlayerNPCDialogueUI.open(PlayerNPCQuestUI.acceptDialogue, PlayerNPCQuestUI.declineDialogue);
			}
			else if (PlayerNPCQuestUI.mode == EQuestViewMode.DETAILS)
			{
				PlayerDashboardInventoryUI.active = false;
				PlayerDashboardCraftingUI.active = false;
				PlayerDashboardSkillsUI.active = false;
				PlayerDashboardInformationUI.active = true;
				PlayerDashboardUI.open();
			}
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x001A088C File Offset: 0x0019EC8C
		private static void updateQuest(QuestAsset newQuest, DialogueResponse newResponse, DialogueAsset newAcceptDialogue, DialogueAsset newDeclineDialogue, EQuestViewMode newMode)
		{
			PlayerNPCQuestUI.quest = newQuest;
			PlayerNPCQuestUI.response = newResponse;
			PlayerNPCQuestUI.acceptDialogue = newAcceptDialogue;
			PlayerNPCQuestUI.declineDialogue = newDeclineDialogue;
			PlayerNPCQuestUI.mode = newMode;
			if (PlayerNPCQuestUI.quest == null)
			{
				return;
			}
			PlayerNPCQuestUI.beginContainer.isVisible = (PlayerNPCQuestUI.mode == EQuestViewMode.BEGIN);
			PlayerNPCQuestUI.endContainer.isVisible = (PlayerNPCQuestUI.mode == EQuestViewMode.END);
			PlayerNPCQuestUI.detailsContainer.isVisible = (PlayerNPCQuestUI.mode == EQuestViewMode.DETAILS);
			if (PlayerNPCQuestUI.mode == EQuestViewMode.DETAILS)
			{
				if (Player.player.quests.TrackedQuestID == PlayerNPCQuestUI.quest.id)
				{
					PlayerNPCQuestUI.trackButton.text = PlayerNPCQuestUI.localization.format("Track_Off");
				}
				else
				{
					PlayerNPCQuestUI.trackButton.text = PlayerNPCQuestUI.localization.format("Track_On");
				}
			}
			PlayerNPCQuestUI.nameLabel.text = PlayerNPCQuestUI.quest.questName;
			string text = PlayerNPCQuestUI.quest.questDescription;
			text = text.Replace("<br>", "\n");
			PlayerNPCQuestUI.descriptionLabel.text = text;
			int num = Screen.height - 80;
			int num2 = 0;
			if (PlayerNPCQuestUI.quest.conditions != null && PlayerNPCQuestUI.quest.conditions.Length > 0)
			{
				PlayerNPCQuestUI.conditionsLabel.isVisible = true;
				PlayerNPCQuestUI.conditionsContainer.isVisible = true;
				PlayerNPCQuestUI.conditionsContainer.remove();
				int num3 = 0;
				for (int i = 0; i < PlayerNPCQuestUI.quest.conditions.Length; i++)
				{
					INPCCondition inpccondition = PlayerNPCQuestUI.quest.conditions[i];
					bool flag = inpccondition.isConditionMet(Player.player);
					Texture2D icon = null;
					if (PlayerNPCQuestUI.mode != EQuestViewMode.BEGIN)
					{
						if (flag)
						{
							icon = (Texture2D)PlayerNPCQuestUI.icons.load("Complete");
						}
						else
						{
							icon = (Texture2D)PlayerNPCQuestUI.icons.load("Incomplete");
						}
					}
					Sleek sleek = inpccondition.createUI(Player.player, icon);
					if (sleek != null)
					{
						sleek.positionOffset_Y = num3;
						PlayerNPCQuestUI.conditionsContainer.add(sleek);
						num3 += sleek.sizeOffset_Y;
					}
				}
				PlayerNPCQuestUI.conditionsContainer.sizeOffset_Y = num3;
				num2 += 30;
				num2 += num3;
			}
			else
			{
				PlayerNPCQuestUI.conditionsLabel.isVisible = false;
				PlayerNPCQuestUI.conditionsContainer.isVisible = false;
			}
			if (PlayerNPCQuestUI.quest.rewards != null && PlayerNPCQuestUI.quest.rewards.Length > 0)
			{
				PlayerNPCQuestUI.rewardsLabel.isVisible = true;
				PlayerNPCQuestUI.rewardsContainer.isVisible = true;
				PlayerNPCQuestUI.rewardsContainer.remove();
				int num4 = 0;
				for (int j = 0; j < PlayerNPCQuestUI.quest.rewards.Length; j++)
				{
					INPCReward inpcreward = PlayerNPCQuestUI.quest.rewards[j];
					Sleek sleek2 = inpcreward.createUI(Player.player);
					if (sleek2 != null)
					{
						sleek2.positionOffset_Y = num4;
						PlayerNPCQuestUI.rewardsContainer.add(sleek2);
						num4 += sleek2.sizeOffset_Y;
					}
				}
				PlayerNPCQuestUI.rewardsLabel.positionOffset_Y = num2;
				PlayerNPCQuestUI.rewardsContainer.positionOffset_Y = num2 + 30;
				PlayerNPCQuestUI.rewardsContainer.sizeOffset_Y = num4;
				num2 += 30;
				num2 += num4;
			}
			else
			{
				PlayerNPCQuestUI.rewardsLabel.isVisible = false;
				PlayerNPCQuestUI.rewardsContainer.isVisible = false;
			}
			PlayerNPCQuestUI.detailsBox.area = new Rect(0f, 0f, 5f, (float)num2);
			if (num2 + 105 > num)
			{
				PlayerNPCQuestUI.questBox.positionOffset_Y = 0;
				PlayerNPCQuestUI.questBox.positionScale_Y = 0f;
				PlayerNPCQuestUI.questBox.sizeOffset_Y = num;
				PlayerNPCQuestUI.detailsBox.positionOffset_Y = -num + 100;
				PlayerNPCQuestUI.detailsBox.sizeOffset_Y = num - 105;
				PlayerNPCQuestUI.detailsBox.sizeOffset_X = -10;
			}
			else
			{
				PlayerNPCQuestUI.questBox.positionOffset_Y = -num2 / 2 - 80;
				PlayerNPCQuestUI.questBox.positionScale_Y = 0.5f;
				PlayerNPCQuestUI.questBox.sizeOffset_Y = num2 + 100;
				PlayerNPCQuestUI.detailsBox.positionOffset_Y = -5 - num2;
				PlayerNPCQuestUI.detailsBox.sizeOffset_Y = num2;
				PlayerNPCQuestUI.detailsBox.sizeOffset_X = 20;
			}
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x001A0C97 File Offset: 0x0019F097
		private static void onClickedAcceptButton(SleekButton button)
		{
			PlayerNPCQuestUI.close();
			PlayerNPCDialogueUI.registerResponse(PlayerNPCQuestUI.declineDialogue, PlayerNPCQuestUI.response);
			PlayerNPCDialogueUI.open(PlayerNPCQuestUI.acceptDialogue, PlayerNPCQuestUI.declineDialogue);
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x001A0CBC File Offset: 0x0019F0BC
		private static void onClickedDeclineButton(SleekButton button)
		{
			PlayerNPCQuestUI.close();
			PlayerNPCDialogueUI.open(PlayerNPCQuestUI.declineDialogue, null);
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x001A0CCE File Offset: 0x0019F0CE
		private static void onClickedContinueButton(SleekButton button)
		{
			PlayerNPCQuestUI.close();
			PlayerNPCDialogueUI.registerResponse(PlayerNPCQuestUI.declineDialogue, PlayerNPCQuestUI.response);
			PlayerNPCDialogueUI.open(PlayerNPCQuestUI.acceptDialogue, PlayerNPCQuestUI.declineDialogue);
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x001A0CF4 File Offset: 0x0019F0F4
		private static void onClickedTrackButton(SleekButton button)
		{
			Player.player.quests.sendTrackQuest(PlayerNPCQuestUI.quest.id);
			if (!Provider.isServer)
			{
				Player.player.quests.trackQuest(PlayerNPCQuestUI.quest.id);
			}
			PlayerNPCQuestUI.closeNicely();
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x001A0D44 File Offset: 0x0019F144
		private static void onClickedAbandonButton(SleekButton button)
		{
			Player.player.quests.sendAbandonQuest(PlayerNPCQuestUI.quest.id);
			if (!Provider.isServer)
			{
				Player.player.quests.abandonQuest(PlayerNPCQuestUI.quest.id);
			}
			PlayerNPCQuestUI.closeNicely();
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x001A0D92 File Offset: 0x0019F192
		private static void onClickedReturnButton(SleekButton button)
		{
			PlayerNPCQuestUI.closeNicely();
		}

		// Token: 0x04002B76 RID: 11126
		private static Sleek container;

		// Token: 0x04002B77 RID: 11127
		public static Local localization;

		// Token: 0x04002B78 RID: 11128
		public static Bundle icons;

		// Token: 0x04002B79 RID: 11129
		public static bool active;

		// Token: 0x04002B7A RID: 11130
		private static QuestAsset quest;

		// Token: 0x04002B7B RID: 11131
		private static DialogueResponse response;

		// Token: 0x04002B7C RID: 11132
		private static DialogueAsset acceptDialogue;

		// Token: 0x04002B7D RID: 11133
		private static DialogueAsset declineDialogue;

		// Token: 0x04002B7E RID: 11134
		private static EQuestViewMode mode;

		// Token: 0x04002B7F RID: 11135
		private static SleekBox questBox;

		// Token: 0x04002B80 RID: 11136
		private static SleekLabel nameLabel;

		// Token: 0x04002B81 RID: 11137
		private static SleekLabel descriptionLabel;

		// Token: 0x04002B82 RID: 11138
		private static SleekScrollBox detailsBox;

		// Token: 0x04002B83 RID: 11139
		private static SleekLabel conditionsLabel;

		// Token: 0x04002B84 RID: 11140
		private static Sleek conditionsContainer;

		// Token: 0x04002B85 RID: 11141
		private static SleekLabel rewardsLabel;

		// Token: 0x04002B86 RID: 11142
		private static Sleek rewardsContainer;

		// Token: 0x04002B87 RID: 11143
		private static Sleek beginContainer;

		// Token: 0x04002B88 RID: 11144
		private static SleekButton acceptButton;

		// Token: 0x04002B89 RID: 11145
		private static SleekButton declineButton;

		// Token: 0x04002B8A RID: 11146
		private static Sleek endContainer;

		// Token: 0x04002B8B RID: 11147
		private static SleekButton continueButton;

		// Token: 0x04002B8C RID: 11148
		private static Sleek detailsContainer;

		// Token: 0x04002B8D RID: 11149
		private static SleekButton trackButton;

		// Token: 0x04002B8E RID: 11150
		private static SleekButton abandonButton;

		// Token: 0x04002B8F RID: 11151
		private static SleekButton returnButton;

		// Token: 0x04002B90 RID: 11152
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x04002B91 RID: 11153
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x04002B92 RID: 11154
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x04002B93 RID: 11155
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x04002B94 RID: 11156
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;

		// Token: 0x04002B95 RID: 11157
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;
	}
}
