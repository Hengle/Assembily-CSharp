using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200079F RID: 1951
	public class PlayerNPCDialogueUI
	{
		// Token: 0x060038C2 RID: 14530 RVA: 0x0019EE14 File Offset: 0x0019D214
		public PlayerNPCDialogueUI()
		{
			if (PlayerNPCDialogueUI.icons != null)
			{
				PlayerNPCDialogueUI.icons.unload();
			}
			PlayerNPCDialogueUI.localization = Localization.read("/Player/PlayerNPCDialogue.dat");
			PlayerNPCDialogueUI.icons = Bundles.getBundle("/Bundles/Textures/Player/Icons/PlayerNPCDialogue/PlayerNPCDialogue.unity3d");
			PlayerNPCDialogueUI.container = new Sleek();
			PlayerNPCDialogueUI.container.positionScale_Y = 1f;
			PlayerNPCDialogueUI.container.positionOffset_X = 10;
			PlayerNPCDialogueUI.container.positionOffset_Y = 10;
			PlayerNPCDialogueUI.container.sizeOffset_X = -20;
			PlayerNPCDialogueUI.container.sizeOffset_Y = -20;
			PlayerNPCDialogueUI.container.sizeScale_X = 1f;
			PlayerNPCDialogueUI.container.sizeScale_Y = 1f;
			PlayerUI.container.add(PlayerNPCDialogueUI.container);
			PlayerNPCDialogueUI.active = false;
			PlayerNPCDialogueUI.dialogueBox = new SleekBox();
			PlayerNPCDialogueUI.dialogueBox.positionOffset_X = -250;
			PlayerNPCDialogueUI.dialogueBox.positionOffset_Y = -200;
			PlayerNPCDialogueUI.dialogueBox.positionScale_X = 0.5f;
			PlayerNPCDialogueUI.dialogueBox.positionScale_Y = 0.85f;
			PlayerNPCDialogueUI.dialogueBox.sizeOffset_X = 500;
			PlayerNPCDialogueUI.dialogueBox.sizeOffset_Y = 100;
			PlayerNPCDialogueUI.container.add(PlayerNPCDialogueUI.dialogueBox);
			PlayerNPCDialogueUI.characterLabel = new SleekLabel();
			PlayerNPCDialogueUI.characterLabel.positionOffset_X = 5;
			PlayerNPCDialogueUI.characterLabel.positionOffset_Y = 5;
			PlayerNPCDialogueUI.characterLabel.sizeOffset_X = -10;
			PlayerNPCDialogueUI.characterLabel.sizeOffset_Y = 30;
			PlayerNPCDialogueUI.characterLabel.sizeScale_X = 1f;
			PlayerNPCDialogueUI.characterLabel.fontAlignment = TextAnchor.UpperLeft;
			PlayerNPCDialogueUI.characterLabel.foregroundTint = ESleekTint.NONE;
			PlayerNPCDialogueUI.characterLabel.isRich = true;
			PlayerNPCDialogueUI.characterLabel.fontSize = 14;
			PlayerNPCDialogueUI.dialogueBox.add(PlayerNPCDialogueUI.characterLabel);
			PlayerNPCDialogueUI.messageLabel = new SleekLabel();
			PlayerNPCDialogueUI.messageLabel.positionOffset_X = 5;
			PlayerNPCDialogueUI.messageLabel.positionOffset_Y = 30;
			PlayerNPCDialogueUI.messageLabel.sizeOffset_X = -10;
			PlayerNPCDialogueUI.messageLabel.sizeOffset_Y = -35;
			PlayerNPCDialogueUI.messageLabel.sizeScale_X = 1f;
			PlayerNPCDialogueUI.messageLabel.sizeScale_Y = 1f;
			PlayerNPCDialogueUI.messageLabel.fontAlignment = TextAnchor.UpperLeft;
			PlayerNPCDialogueUI.messageLabel.foregroundTint = ESleekTint.NONE;
			PlayerNPCDialogueUI.messageLabel.isRich = true;
			PlayerNPCDialogueUI.dialogueBox.add(PlayerNPCDialogueUI.messageLabel);
			PlayerNPCDialogueUI.pageLabel = new SleekLabel();
			PlayerNPCDialogueUI.pageLabel.positionOffset_X = -30;
			PlayerNPCDialogueUI.pageLabel.positionOffset_Y = -30;
			PlayerNPCDialogueUI.pageLabel.positionScale_X = 1f;
			PlayerNPCDialogueUI.pageLabel.positionScale_Y = 1f;
			PlayerNPCDialogueUI.pageLabel.sizeOffset_X = 30;
			PlayerNPCDialogueUI.pageLabel.sizeOffset_Y = 30;
			PlayerNPCDialogueUI.pageLabel.fontAlignment = TextAnchor.LowerRight;
			PlayerNPCDialogueUI.dialogueBox.add(PlayerNPCDialogueUI.pageLabel);
			PlayerNPCDialogueUI.responseBox = new SleekScrollBox();
			PlayerNPCDialogueUI.responseBox.positionOffset_X = -250;
			PlayerNPCDialogueUI.responseBox.positionOffset_Y = -100;
			PlayerNPCDialogueUI.responseBox.positionScale_X = 0.5f;
			PlayerNPCDialogueUI.responseBox.positionScale_Y = 0.85f;
			PlayerNPCDialogueUI.responseBox.sizeOffset_X = 530;
			PlayerNPCDialogueUI.responseBox.sizeScale_Y = 0.15f;
			PlayerNPCDialogueUI.container.add(PlayerNPCDialogueUI.responseBox);
			PlayerNPCDialogueUI.responseBox.isVisible = false;
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060038C3 RID: 14531 RVA: 0x0019F13C File Offset: 0x0019D53C
		// (set) Token: 0x060038C4 RID: 14532 RVA: 0x0019F143 File Offset: 0x0019D543
		public static bool dialogueHasNextPage { get; private set; }

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060038C5 RID: 14533 RVA: 0x0019F14B File Offset: 0x0019D54B
		// (set) Token: 0x060038C6 RID: 14534 RVA: 0x0019F152 File Offset: 0x0019D552
		public static bool dialogueAnimating { get; private set; }

		// Token: 0x060038C7 RID: 14535 RVA: 0x0019F15C File Offset: 0x0019D55C
		public static void open(DialogueAsset newDialogue, DialogueAsset newPrevDialogue)
		{
			if (PlayerNPCDialogueUI.active)
			{
				return;
			}
			PlayerNPCDialogueUI.active = true;
			if (PlayerLifeUI.npc != null && PlayerLifeUI.npc.npcAsset != null)
			{
				PlayerNPCDialogueUI.characterLabel.text = PlayerLifeUI.npc.npcAsset.npcName;
			}
			else
			{
				PlayerNPCDialogueUI.characterLabel.text = "?";
			}
			PlayerNPCDialogueUI.updateDialogue(newDialogue, newPrevDialogue);
			PlayerNPCDialogueUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x0019F1E7 File Offset: 0x0019D5E7
		public static void close()
		{
			if (!PlayerNPCDialogueUI.active)
			{
				return;
			}
			PlayerNPCDialogueUI.active = false;
			PlayerNPCDialogueUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x0019F214 File Offset: 0x0019D614
		private static void updateDialogue(DialogueAsset newDialogue, DialogueAsset newPrevDialogue)
		{
			PlayerNPCDialogueUI.dialogue = newDialogue;
			PlayerNPCDialogueUI.prevDialogue = newPrevDialogue;
			if (PlayerNPCDialogueUI.dialogue == null)
			{
				return;
			}
			PlayerNPCDialogueUI.responseBox.isVisible = false;
			int availableMessage = PlayerNPCDialogueUI.dialogue.getAvailableMessage(Player.player);
			if (availableMessage == -1)
			{
				return;
			}
			PlayerNPCDialogueUI.message = PlayerNPCDialogueUI.dialogue.messages[availableMessage];
			if ((PlayerNPCDialogueUI.message.conditions != null && PlayerNPCDialogueUI.message.conditions.Length > 0) || (PlayerNPCDialogueUI.message.rewards != null && PlayerNPCDialogueUI.message.rewards.Length > 0))
			{
				Player.player.quests.sendRegisterMessage(PlayerNPCDialogueUI.dialogue.id);
				if (!Provider.isServer)
				{
					Player.player.quests.registerMessage(PlayerNPCDialogueUI.dialogue.id);
				}
			}
			PlayerNPCDialogueUI.responses.Clear();
			if (PlayerNPCDialogueUI.message.responses != null && PlayerNPCDialogueUI.message.responses.Length > 0)
			{
				for (int i = 0; i < PlayerNPCDialogueUI.message.responses.Length; i++)
				{
					DialogueResponse dialogueResponse = PlayerNPCDialogueUI.dialogue.responses[(int)PlayerNPCDialogueUI.message.responses[i]];
					if (dialogueResponse.areConditionsMet(Player.player))
					{
						PlayerNPCDialogueUI.responses.Add(dialogueResponse);
					}
				}
			}
			else
			{
				int j = 0;
				while (j < PlayerNPCDialogueUI.dialogue.responses.Length)
				{
					DialogueResponse dialogueResponse2 = PlayerNPCDialogueUI.dialogue.responses[j];
					if (dialogueResponse2.messages == null || dialogueResponse2.messages.Length <= 0)
					{
						goto IL_1BE;
					}
					bool flag = false;
					for (int k = 0; k < dialogueResponse2.messages.Length; k++)
					{
						if ((int)dialogueResponse2.messages[k] == availableMessage)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						goto IL_1BE;
					}
					IL_1DB:
					j++;
					continue;
					IL_1BE:
					if (dialogueResponse2.areConditionsMet(Player.player))
					{
						PlayerNPCDialogueUI.responses.Add(dialogueResponse2);
						goto IL_1DB;
					}
					goto IL_1DB;
				}
			}
			if (PlayerNPCDialogueUI.message.prev != 0)
			{
				PlayerNPCDialogueUI.prevDialogue = (Assets.find(EAssetType.NPC, PlayerNPCDialogueUI.message.prev) as DialogueAsset);
			}
			if (PlayerNPCDialogueUI.responses.Count == 0)
			{
				if (PlayerNPCDialogueUI.prevDialogue == null)
				{
					PlayerNPCDialogueUI.responses.Add(new DialogueResponse(0, null, 0, 0, 0, PlayerNPCDialogueUI.localization.format("Goodbye"), null, null));
				}
			}
			else
			{
				PlayerNPCDialogueUI.prevDialogue = null;
			}
			PlayerNPCDialogueUI.responseBox.remove();
			for (int l = 0; l < PlayerNPCDialogueUI.responses.Count; l++)
			{
				DialogueResponse dialogueResponse3 = PlayerNPCDialogueUI.responses[l];
				string text = dialogueResponse3.text;
				text = text.Replace("<name_npc>", (!(PlayerLifeUI.npc != null)) ? "?" : PlayerLifeUI.npc.npcAsset.npcName);
				text = text.Replace("<name_char>", Player.player.channel.owner.playerID.characterName);
				Texture2D newIcon = null;
				if (dialogueResponse3.quest != 0)
				{
					if (Player.player.quests.getQuestStatus(dialogueResponse3.quest) == ENPCQuestStatus.READY)
					{
						newIcon = (Texture2D)PlayerNPCDialogueUI.icons.load("Quest_End");
					}
					else
					{
						newIcon = (Texture2D)PlayerNPCDialogueUI.icons.load("Quest_Begin");
					}
				}
				else if (dialogueResponse3.vendor != 0)
				{
					newIcon = (Texture2D)PlayerNPCDialogueUI.icons.load("Vendor");
				}
				SleekButtonIcon sleekButtonIcon = new SleekButtonIcon(newIcon);
				sleekButtonIcon.positionOffset_Y = l * 30;
				sleekButtonIcon.sizeOffset_X = -30;
				sleekButtonIcon.sizeOffset_Y = 30;
				sleekButtonIcon.sizeScale_X = 1f;
				sleekButtonIcon.foregroundTint = ESleekTint.NONE;
				sleekButtonIcon.isRich = true;
				sleekButtonIcon.text = text;
				SleekButton sleekButton = sleekButtonIcon;
				if (PlayerNPCDialogueUI.<>f__mg$cache0 == null)
				{
					PlayerNPCDialogueUI.<>f__mg$cache0 = new ClickedButton(PlayerNPCDialogueUI.onClickedResponseButton);
				}
				sleekButton.onClickedButton = PlayerNPCDialogueUI.<>f__mg$cache0;
				PlayerNPCDialogueUI.responseBox.add(sleekButtonIcon);
				sleekButtonIcon.isVisible = false;
			}
			PlayerNPCDialogueUI.responseBox.area = new Rect(0f, 0f, 5f, 0f);
			PlayerNPCDialogueUI.dialoguePage = 0;
			PlayerNPCDialogueUI.updatePage();
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x0019F658 File Offset: 0x0019DA58
		private static void updatePage()
		{
			PlayerNPCDialogueUI.messageLabel.text = string.Empty;
			PlayerNPCDialogueUI.pageLabel.isVisible = false;
			PlayerNPCDialogueUI.dialogueTime = 0f;
			PlayerNPCDialogueUI.dialoguePause = 0f;
			PlayerNPCDialogueUI.dialogueBuilder.Length = 0;
			PlayerNPCDialogueUI.dialogueAppend = string.Empty;
			PlayerNPCDialogueUI.dialogueIndex = 0;
			PlayerNPCDialogueUI.dialogueOffset = 0;
			PlayerNPCDialogueUI.responseTime = 0f;
			PlayerNPCDialogueUI.responseIndex = 0;
			PlayerNPCDialogueUI.dialogueAnimating = true;
			PlayerNPCDialogueUI.dialogueHasNextPage = false;
			if (PlayerNPCDialogueUI.message != null && PlayerNPCDialogueUI.message.pages != null && (int)PlayerNPCDialogueUI.dialoguePage < PlayerNPCDialogueUI.message.pages.Length)
			{
				PlayerNPCDialogueUI.dialogueText = PlayerNPCDialogueUI.message.pages[(int)PlayerNPCDialogueUI.dialoguePage].text;
				PlayerNPCDialogueUI.dialogueText = PlayerNPCDialogueUI.dialogueText.Replace("<br>", "\n");
				PlayerNPCDialogueUI.dialogueText = PlayerNPCDialogueUI.dialogueText.Replace("<name_npc>", (!(PlayerLifeUI.npc != null)) ? "?" : PlayerLifeUI.npc.npcAsset.npcName);
				PlayerNPCDialogueUI.dialogueText = PlayerNPCDialogueUI.dialogueText.Replace("<name_char>", Player.player.channel.owner.playerID.characterName);
			}
			else
			{
				PlayerNPCDialogueUI.dialogueText = "?";
			}
			PlayerNPCDialogueUI.dialogueCharacters = PlayerNPCDialogueUI.dialogueText.ToCharArray();
			if (OptionsSettings.talk)
			{
				PlayerNPCDialogueUI.skipText();
			}
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x0019F7CC File Offset: 0x0019DBCC
		private static bool findKeyword(char[] characters, int index, char[] keyword)
		{
			if (index + keyword.Length > characters.Length)
			{
				return false;
			}
			for (int i = 0; i < keyword.Length; i++)
			{
				if (characters[index + i] != keyword[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x0019F80C File Offset: 0x0019DC0C
		private static bool findTags(char[] characters, int index, out int begin, out int end)
		{
			begin = 0;
			end = 0;
			while (index < characters.Length)
			{
				if (characters[index] == '<')
				{
					if (begin == 0)
					{
						begin = index;
					}
				}
				else if (characters[index] == '>' && (index == characters.Length - 1 || characters[index + 1] != '<'))
				{
					end = index;
					return true;
				}
				index++;
			}
			return false;
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x0019F872 File Offset: 0x0019DC72
		public static void nextPage()
		{
			if ((int)PlayerNPCDialogueUI.dialoguePage == PlayerNPCDialogueUI.message.pages.Length - 1)
			{
				PlayerNPCDialogueUI.updateDialogue(PlayerNPCDialogueUI.prevDialogue, null);
			}
			else
			{
				PlayerNPCDialogueUI.dialoguePage += 1;
				PlayerNPCDialogueUI.updatePage();
			}
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x0019F8B0 File Offset: 0x0019DCB0
		private static void finishPage()
		{
			PlayerNPCDialogueUI.dialogueAnimating = false;
			if (PlayerNPCDialogueUI.message != null && PlayerNPCDialogueUI.message.pages != null)
			{
				if ((int)PlayerNPCDialogueUI.dialoguePage < PlayerNPCDialogueUI.message.pages.Length - 1)
				{
					PlayerNPCDialogueUI.dialogueHasNextPage = true;
					PlayerNPCDialogueUI.pageLabel.text = PlayerNPCDialogueUI.localization.format("Page", new object[]
					{
						MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
					});
					PlayerNPCDialogueUI.pageLabel.isVisible = true;
				}
				else if ((int)PlayerNPCDialogueUI.dialoguePage == PlayerNPCDialogueUI.message.pages.Length - 1 && PlayerNPCDialogueUI.prevDialogue != null)
				{
					PlayerNPCDialogueUI.dialogueHasNextPage = true;
					PlayerNPCDialogueUI.pageLabel.text = PlayerNPCDialogueUI.localization.format("Page", new object[]
					{
						MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
					});
					PlayerNPCDialogueUI.pageLabel.isVisible = true;
					PlayerNPCDialogueUI.responseBox.isVisible = true;
				}
				else
				{
					PlayerNPCDialogueUI.responseBox.isVisible = true;
				}
			}
			else
			{
				PlayerNPCDialogueUI.responseBox.isVisible = true;
			}
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x0019F9C0 File Offset: 0x0019DDC0
		public static void skipText()
		{
			PlayerNPCDialogueUI.messageLabel.text = PlayerNPCDialogueUI.dialogueText.Replace("<pause>", string.Empty);
			PlayerNPCDialogueUI.responseIndex = PlayerNPCDialogueUI.responses.Count;
			for (int i = 0; i < PlayerNPCDialogueUI.responses.Count; i++)
			{
				PlayerNPCDialogueUI.responseBox.children[i].isVisible = true;
			}
			PlayerNPCDialogueUI.responseBox.area = new Rect(0f, 0f, 5f, (float)(PlayerNPCDialogueUI.responses.Count * 30));
			PlayerNPCDialogueUI.finishPage();
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x0019FA5C File Offset: 0x0019DE5C
		public static void updateText()
		{
			if (PlayerNPCDialogueUI.dialogue == null)
			{
				return;
			}
			if (PlayerNPCDialogueUI.dialogueAnimating)
			{
				if (PlayerNPCDialogueUI.dialoguePause > 0f)
				{
					PlayerNPCDialogueUI.dialoguePause -= Time.deltaTime;
				}
				else
				{
					PlayerNPCDialogueUI.dialogueTime += Time.deltaTime;
				}
				int num = Mathf.Min(PlayerNPCDialogueUI.dialogueCharacters.Length, Mathf.CeilToInt(PlayerNPCDialogueUI.dialogueTime * 30f) + PlayerNPCDialogueUI.dialogueOffset);
				if (PlayerNPCDialogueUI.dialogueIndex != num)
				{
					while (PlayerNPCDialogueUI.dialogueIndex < PlayerNPCDialogueUI.dialogueCharacters.Length && PlayerNPCDialogueUI.dialogueIndex < num)
					{
						char c = PlayerNPCDialogueUI.dialogueCharacters[PlayerNPCDialogueUI.dialogueIndex];
						if (c == '<')
						{
							int num2;
							int num3;
							if (PlayerNPCDialogueUI.dialogueAppend.Length > 0)
							{
								num += PlayerNPCDialogueUI.dialogueAppend.Length;
								PlayerNPCDialogueUI.dialogueIndex += PlayerNPCDialogueUI.dialogueAppend.Length;
								PlayerNPCDialogueUI.dialogueOffset += PlayerNPCDialogueUI.dialogueAppend.Length;
								PlayerNPCDialogueUI.dialogueBuilder.Append(PlayerNPCDialogueUI.dialogueAppend);
								PlayerNPCDialogueUI.dialogueAppend = string.Empty;
							}
							else if (PlayerNPCDialogueUI.findKeyword(PlayerNPCDialogueUI.dialogueCharacters, PlayerNPCDialogueUI.dialogueIndex, PlayerNPCDialogueUI.KEYWORD_PAUSE))
							{
								PlayerNPCDialogueUI.dialoguePause += 0.5f;
								num = PlayerNPCDialogueUI.dialogueIndex + PlayerNPCDialogueUI.KEYWORD_PAUSE.Length;
								PlayerNPCDialogueUI.dialogueIndex = num;
								PlayerNPCDialogueUI.dialogueOffset += PlayerNPCDialogueUI.KEYWORD_PAUSE.Length - 1;
							}
							else if (PlayerNPCDialogueUI.findTags(PlayerNPCDialogueUI.dialogueCharacters, PlayerNPCDialogueUI.dialogueIndex, out num2, out num3))
							{
								int num4 = num3 - num2 + 1;
								num += num4;
								PlayerNPCDialogueUI.dialogueIndex += num4;
								PlayerNPCDialogueUI.dialogueOffset += num4;
								PlayerNPCDialogueUI.dialogueBuilder.Append(PlayerNPCDialogueUI.dialogueText.Substring(num2, num4));
								if (PlayerNPCDialogueUI.findTags(PlayerNPCDialogueUI.dialogueCharacters, num3 + 1, out num2, out num3))
								{
									num4 = num3 - num2 + 1;
									PlayerNPCDialogueUI.dialogueAppend = PlayerNPCDialogueUI.dialogueText.Substring(num2, num4);
								}
							}
						}
						else
						{
							PlayerNPCDialogueUI.dialogueBuilder.Append(c);
							PlayerNPCDialogueUI.dialogueIndex++;
						}
					}
					PlayerNPCDialogueUI.messageLabel.text = PlayerNPCDialogueUI.dialogueBuilder.ToString() + PlayerNPCDialogueUI.dialogueAppend;
					if (PlayerNPCDialogueUI.dialogueIndex == PlayerNPCDialogueUI.dialogueCharacters.Length)
					{
						PlayerNPCDialogueUI.finishPage();
					}
				}
			}
			else
			{
				PlayerNPCDialogueUI.responseTime += Time.deltaTime;
				int num5 = Mathf.Min(PlayerNPCDialogueUI.responses.Count, Mathf.FloorToInt(PlayerNPCDialogueUI.responseTime * 10f));
				if (PlayerNPCDialogueUI.responseIndex != num5)
				{
					while (PlayerNPCDialogueUI.responseIndex < num5)
					{
						PlayerNPCDialogueUI.responseBox.children[PlayerNPCDialogueUI.responseIndex].isVisible = true;
						PlayerNPCDialogueUI.responseBox.area = new Rect(0f, 0f, 5f, (float)(num5 * 30));
						PlayerNPCDialogueUI.responseIndex++;
					}
				}
			}
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x0019FD44 File Offset: 0x0019E144
		public static void registerResponse(DialogueAsset dialogue, DialogueResponse response)
		{
			if ((response.conditions != null && response.conditions.Length > 0) || (response.rewards != null && response.rewards.Length > 0))
			{
				Player.player.quests.sendRegisterResponse(dialogue.id, response.index);
				if (!Provider.isServer)
				{
					Player.player.quests.registerResponse(dialogue.id, response.index);
				}
			}
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x0019FDC4 File Offset: 0x0019E1C4
		private static void onClickedResponseButton(SleekButton button)
		{
			byte index = (byte)PlayerNPCDialogueUI.responseBox.search(button);
			DialogueResponse dialogueResponse = PlayerNPCDialogueUI.responses[(int)index];
			DialogueAsset dialogueAsset = (DialogueAsset)Assets.find(EAssetType.NPC, dialogueResponse.dialogue);
			QuestAsset questAsset = (QuestAsset)Assets.find(EAssetType.NPC, dialogueResponse.quest);
			if (questAsset != null)
			{
				PlayerNPCDialogueUI.close();
				PlayerNPCQuestUI.open(questAsset, dialogueResponse, dialogueAsset, PlayerNPCDialogueUI.dialogue, (Player.player.quests.getQuestStatus(dialogueResponse.quest) != ENPCQuestStatus.READY) ? EQuestViewMode.BEGIN : EQuestViewMode.END);
			}
			else
			{
				VendorAsset vendorAsset = (VendorAsset)Assets.find(EAssetType.NPC, dialogueResponse.vendor);
				if (vendorAsset != null)
				{
					PlayerNPCDialogueUI.close();
					PlayerNPCVendorUI.open(vendorAsset, dialogueResponse, dialogueAsset, PlayerNPCDialogueUI.dialogue);
				}
				else
				{
					PlayerNPCDialogueUI.registerResponse(PlayerNPCDialogueUI.dialogue, dialogueResponse);
					if (dialogueAsset != null)
					{
						PlayerNPCDialogueUI.updateDialogue(dialogueAsset, PlayerNPCDialogueUI.dialogue);
					}
					else
					{
						PlayerNPCDialogueUI.close();
						PlayerLifeUI.open();
					}
				}
			}
		}

		// Token: 0x04002B56 RID: 11094
		private static readonly char[] KEYWORD_PAUSE = "<pause>".ToCharArray();

		// Token: 0x04002B57 RID: 11095
		private static Sleek container;

		// Token: 0x04002B58 RID: 11096
		private static Local localization;

		// Token: 0x04002B59 RID: 11097
		public static Bundle icons;

		// Token: 0x04002B5A RID: 11098
		public static bool active;

		// Token: 0x04002B5B RID: 11099
		private static DialogueAsset dialogue;

		// Token: 0x04002B5C RID: 11100
		private static DialogueMessage message;

		// Token: 0x04002B5D RID: 11101
		private static DialogueAsset prevDialogue;

		// Token: 0x04002B5E RID: 11102
		private static List<DialogueResponse> responses = new List<DialogueResponse>();

		// Token: 0x04002B5F RID: 11103
		private static SleekBox dialogueBox;

		// Token: 0x04002B60 RID: 11104
		private static SleekLabel characterLabel;

		// Token: 0x04002B61 RID: 11105
		private static SleekLabel messageLabel;

		// Token: 0x04002B62 RID: 11106
		private static SleekLabel pageLabel;

		// Token: 0x04002B63 RID: 11107
		private static SleekScrollBox responseBox;

		// Token: 0x04002B64 RID: 11108
		private static byte dialoguePage;

		// Token: 0x04002B65 RID: 11109
		private static string dialogueText;

		// Token: 0x04002B66 RID: 11110
		private static char[] dialogueCharacters;

		// Token: 0x04002B67 RID: 11111
		private static float dialogueTime;

		// Token: 0x04002B68 RID: 11112
		private static float dialoguePause;

		// Token: 0x04002B69 RID: 11113
		private static StringBuilder dialogueBuilder = new StringBuilder();

		// Token: 0x04002B6A RID: 11114
		private static string dialogueAppend;

		// Token: 0x04002B6B RID: 11115
		private static int dialogueIndex;

		// Token: 0x04002B6C RID: 11116
		private static int dialogueOffset;

		// Token: 0x04002B6D RID: 11117
		private static float responseTime;

		// Token: 0x04002B6E RID: 11118
		private static int responseIndex;

		// Token: 0x04002B71 RID: 11121
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;
	}
}
