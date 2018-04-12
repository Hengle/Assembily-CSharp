using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000788 RID: 1928
	public class MenuWorkshopEditorUI
	{
		// Token: 0x0600377D RID: 14205 RVA: 0x00184C9C File Offset: 0x0018309C
		public MenuWorkshopEditorUI()
		{
			if (MenuWorkshopEditorUI.icons != null)
			{
				MenuWorkshopEditorUI.icons.unload();
				MenuWorkshopEditorUI.icons = null;
			}
			Local local = Localization.read("/Menu/Workshop/MenuWorkshopEditor.dat");
			MenuWorkshopEditorUI.icons = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Workshop/MenuWorkshopEditor/MenuWorkshopEditor.unity3d");
			MenuWorkshopEditorUI.container = new Sleek();
			MenuWorkshopEditorUI.container.positionOffset_X = 10;
			MenuWorkshopEditorUI.container.positionOffset_Y = 10;
			MenuWorkshopEditorUI.container.positionScale_Y = 1f;
			MenuWorkshopEditorUI.container.sizeOffset_X = -20;
			MenuWorkshopEditorUI.container.sizeOffset_Y = -20;
			MenuWorkshopEditorUI.container.sizeScale_X = 1f;
			MenuWorkshopEditorUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuWorkshopEditorUI.container);
			MenuWorkshopEditorUI.active = false;
			MenuWorkshopEditorUI.previewBox = new SleekBox();
			MenuWorkshopEditorUI.previewBox.positionOffset_X = -305;
			MenuWorkshopEditorUI.previewBox.positionOffset_Y = 100;
			MenuWorkshopEditorUI.previewBox.positionScale_X = 0.5f;
			MenuWorkshopEditorUI.previewBox.sizeOffset_X = 340;
			MenuWorkshopEditorUI.previewBox.sizeOffset_Y = 180;
			MenuWorkshopEditorUI.container.add(MenuWorkshopEditorUI.previewBox);
			MenuWorkshopEditorUI.previewImage = new SleekImageTexture();
			MenuWorkshopEditorUI.previewImage.positionOffset_X = 10;
			MenuWorkshopEditorUI.previewImage.positionOffset_Y = 10;
			MenuWorkshopEditorUI.previewImage.sizeOffset_X = -20;
			MenuWorkshopEditorUI.previewImage.sizeOffset_Y = -20;
			MenuWorkshopEditorUI.previewImage.sizeScale_X = 1f;
			MenuWorkshopEditorUI.previewImage.sizeScale_Y = 1f;
			MenuWorkshopEditorUI.previewImage.shouldDestroyTexture = true;
			MenuWorkshopEditorUI.previewBox.add(MenuWorkshopEditorUI.previewImage);
			MenuWorkshopEditorUI.levelScrollBox = new SleekScrollBox();
			MenuWorkshopEditorUI.levelScrollBox.positionOffset_X = -95;
			MenuWorkshopEditorUI.levelScrollBox.positionOffset_Y = 290;
			MenuWorkshopEditorUI.levelScrollBox.positionScale_X = 0.5f;
			MenuWorkshopEditorUI.levelScrollBox.sizeOffset_X = 430;
			MenuWorkshopEditorUI.levelScrollBox.sizeOffset_Y = -390;
			MenuWorkshopEditorUI.levelScrollBox.sizeScale_Y = 1f;
			MenuWorkshopEditorUI.levelScrollBox.area = new Rect(0f, 0f, 5f, 0f);
			MenuWorkshopEditorUI.container.add(MenuWorkshopEditorUI.levelScrollBox);
			MenuWorkshopEditorUI.selectedBox = new SleekBox();
			MenuWorkshopEditorUI.selectedBox.positionOffset_X = 45;
			MenuWorkshopEditorUI.selectedBox.positionOffset_Y = 100;
			MenuWorkshopEditorUI.selectedBox.positionScale_X = 0.5f;
			MenuWorkshopEditorUI.selectedBox.sizeOffset_X = 260;
			MenuWorkshopEditorUI.selectedBox.sizeOffset_Y = 30;
			MenuWorkshopEditorUI.container.add(MenuWorkshopEditorUI.selectedBox);
			MenuWorkshopEditorUI.descriptionBox = new SleekBox();
			MenuWorkshopEditorUI.descriptionBox.positionOffset_X = 45;
			MenuWorkshopEditorUI.descriptionBox.positionOffset_Y = 140;
			MenuWorkshopEditorUI.descriptionBox.positionScale_X = 0.5f;
			MenuWorkshopEditorUI.descriptionBox.sizeOffset_X = 260;
			MenuWorkshopEditorUI.descriptionBox.sizeOffset_Y = 140;
			MenuWorkshopEditorUI.descriptionBox.fontAlignment = TextAnchor.UpperCenter;
			MenuWorkshopEditorUI.container.add(MenuWorkshopEditorUI.descriptionBox);
			MenuWorkshopEditorUI.mapNameField = new SleekField();
			MenuWorkshopEditorUI.mapNameField.positionOffset_X = -305;
			MenuWorkshopEditorUI.mapNameField.positionOffset_Y = 370;
			MenuWorkshopEditorUI.mapNameField.positionScale_X = 0.5f;
			MenuWorkshopEditorUI.mapNameField.sizeOffset_X = 200;
			MenuWorkshopEditorUI.mapNameField.sizeOffset_Y = 30;
			MenuWorkshopEditorUI.mapNameField.maxLength = 24;
			MenuWorkshopEditorUI.mapNameField.addLabel(local.format("Name_Field_Label"), ESleekSide.LEFT);
			MenuWorkshopEditorUI.container.add(MenuWorkshopEditorUI.mapNameField);
			MenuWorkshopEditorUI.mapSizeState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuPlaySingleplayerUI.localization.format("Small")),
				new GUIContent(MenuPlaySingleplayerUI.localization.format("Medium")),
				new GUIContent(MenuPlaySingleplayerUI.localization.format("Large"))
			});
			MenuWorkshopEditorUI.mapSizeState.positionOffset_X = -305;
			MenuWorkshopEditorUI.mapSizeState.positionOffset_Y = 410;
			MenuWorkshopEditorUI.mapSizeState.positionScale_X = 0.5f;
			MenuWorkshopEditorUI.mapSizeState.sizeOffset_X = 200;
			MenuWorkshopEditorUI.mapSizeState.sizeOffset_Y = 30;
			MenuWorkshopEditorUI.container.add(MenuWorkshopEditorUI.mapSizeState);
			MenuWorkshopEditorUI.mapTypeState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuPlaySingleplayerUI.localization.format("Survival")),
				new GUIContent(MenuPlaySingleplayerUI.localization.format("Arena"))
			});
			MenuWorkshopEditorUI.mapTypeState.positionOffset_X = -305;
			MenuWorkshopEditorUI.mapTypeState.positionOffset_Y = 450;
			MenuWorkshopEditorUI.mapTypeState.positionScale_X = 0.5f;
			MenuWorkshopEditorUI.mapTypeState.sizeOffset_X = 200;
			MenuWorkshopEditorUI.mapTypeState.sizeOffset_Y = 30;
			MenuWorkshopEditorUI.container.add(MenuWorkshopEditorUI.mapTypeState);
			MenuWorkshopEditorUI.addButton = new SleekButtonIcon((Texture2D)MenuWorkshopEditorUI.icons.load("Add"));
			MenuWorkshopEditorUI.addButton.positionOffset_X = -305;
			MenuWorkshopEditorUI.addButton.positionOffset_Y = 490;
			MenuWorkshopEditorUI.addButton.positionScale_X = 0.5f;
			MenuWorkshopEditorUI.addButton.sizeOffset_X = 200;
			MenuWorkshopEditorUI.addButton.sizeOffset_Y = 30;
			MenuWorkshopEditorUI.addButton.text = local.format("Add_Button");
			MenuWorkshopEditorUI.addButton.tooltip = local.format("Add_Button_Tooltip");
			SleekButton sleekButton = MenuWorkshopEditorUI.addButton;
			if (MenuWorkshopEditorUI.<>f__mg$cache1 == null)
			{
				MenuWorkshopEditorUI.<>f__mg$cache1 = new ClickedButton(MenuWorkshopEditorUI.onClickedAddButton);
			}
			sleekButton.onClickedButton = MenuWorkshopEditorUI.<>f__mg$cache1;
			MenuWorkshopEditorUI.container.add(MenuWorkshopEditorUI.addButton);
			MenuWorkshopEditorUI.removeButton = new SleekButtonIconConfirm((Texture2D)MenuWorkshopEditorUI.icons.load("Remove"), local.format("Remove_Button_Confirm"), local.format("Remove_Button_Confirm_Tooltip"), local.format("Remove_Button_Deny"), local.format("Remove_Button_Deny_Tooltip"));
			MenuWorkshopEditorUI.removeButton.positionOffset_X = -305;
			MenuWorkshopEditorUI.removeButton.positionOffset_Y = 530;
			MenuWorkshopEditorUI.removeButton.positionScale_X = 0.5f;
			MenuWorkshopEditorUI.removeButton.sizeOffset_X = 200;
			MenuWorkshopEditorUI.removeButton.sizeOffset_Y = 30;
			MenuWorkshopEditorUI.removeButton.text = local.format("Remove_Button");
			MenuWorkshopEditorUI.removeButton.tooltip = local.format("Remove_Button_Tooltip");
			SleekButtonIconConfirm sleekButtonIconConfirm = MenuWorkshopEditorUI.removeButton;
			if (MenuWorkshopEditorUI.<>f__mg$cache2 == null)
			{
				MenuWorkshopEditorUI.<>f__mg$cache2 = new Confirm(MenuWorkshopEditorUI.onClickedRemoveButton);
			}
			sleekButtonIconConfirm.onConfirmed = MenuWorkshopEditorUI.<>f__mg$cache2;
			MenuWorkshopEditorUI.container.add(MenuWorkshopEditorUI.removeButton);
			MenuWorkshopEditorUI.editButton = new SleekButtonIcon((Texture2D)MenuWorkshopEditorUI.icons.load("Edit"));
			MenuWorkshopEditorUI.editButton.positionOffset_X = -305;
			MenuWorkshopEditorUI.editButton.positionOffset_Y = 290;
			MenuWorkshopEditorUI.editButton.positionScale_X = 0.5f;
			MenuWorkshopEditorUI.editButton.sizeOffset_X = 200;
			MenuWorkshopEditorUI.editButton.sizeOffset_Y = 30;
			MenuWorkshopEditorUI.editButton.text = local.format("Edit_Button");
			MenuWorkshopEditorUI.editButton.tooltip = local.format("Edit_Button_Tooltip");
			MenuWorkshopEditorUI.editButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton2 = MenuWorkshopEditorUI.editButton;
			if (MenuWorkshopEditorUI.<>f__mg$cache3 == null)
			{
				MenuWorkshopEditorUI.<>f__mg$cache3 = new ClickedButton(MenuWorkshopEditorUI.onClickedEditButton);
			}
			sleekButton2.onClickedButton = MenuWorkshopEditorUI.<>f__mg$cache3;
			MenuWorkshopEditorUI.container.add(MenuWorkshopEditorUI.editButton);
			MenuWorkshopEditorUI.edit2Button = new SleekButtonIcon((Texture2D)MenuWorkshopEditorUI.icons.load("Edit"));
			MenuWorkshopEditorUI.edit2Button.positionOffset_X = -305;
			MenuWorkshopEditorUI.edit2Button.positionOffset_Y = 330;
			MenuWorkshopEditorUI.edit2Button.positionScale_X = 0.5f;
			MenuWorkshopEditorUI.edit2Button.sizeOffset_X = 200;
			MenuWorkshopEditorUI.edit2Button.sizeOffset_Y = 30;
			MenuWorkshopEditorUI.edit2Button.text = local.format("Edit2_Button");
			MenuWorkshopEditorUI.edit2Button.tooltip = local.format("Edit2_Button_Tooltip");
			MenuWorkshopEditorUI.edit2Button.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton3 = MenuWorkshopEditorUI.edit2Button;
			if (MenuWorkshopEditorUI.<>f__mg$cache4 == null)
			{
				MenuWorkshopEditorUI.<>f__mg$cache4 = new ClickedButton(MenuWorkshopEditorUI.onClickedEdit2Button);
			}
			sleekButton3.onClickedButton = MenuWorkshopEditorUI.<>f__mg$cache4;
			MenuWorkshopEditorUI.container.add(MenuWorkshopEditorUI.edit2Button);
			MenuWorkshopEditorUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuWorkshopEditorUI.backButton.positionOffset_Y = -50;
			MenuWorkshopEditorUI.backButton.positionScale_Y = 1f;
			MenuWorkshopEditorUI.backButton.sizeOffset_X = 200;
			MenuWorkshopEditorUI.backButton.sizeOffset_Y = 50;
			MenuWorkshopEditorUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuWorkshopEditorUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton4 = MenuWorkshopEditorUI.backButton;
			if (MenuWorkshopEditorUI.<>f__mg$cache5 == null)
			{
				MenuWorkshopEditorUI.<>f__mg$cache5 = new ClickedButton(MenuWorkshopEditorUI.onClickedBackButton);
			}
			sleekButton4.onClickedButton = MenuWorkshopEditorUI.<>f__mg$cache5;
			MenuWorkshopEditorUI.backButton.fontSize = 14;
			MenuWorkshopEditorUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuWorkshopEditorUI.container.add(MenuWorkshopEditorUI.backButton);
			MenuWorkshopEditorUI.onLevelsRefreshed();
			Delegate onLevelsRefreshed = Level.onLevelsRefreshed;
			if (MenuWorkshopEditorUI.<>f__mg$cache6 == null)
			{
				MenuWorkshopEditorUI.<>f__mg$cache6 = new LevelsRefreshed(MenuWorkshopEditorUI.onLevelsRefreshed);
			}
			Level.onLevelsRefreshed = (LevelsRefreshed)Delegate.Combine(onLevelsRefreshed, MenuWorkshopEditorUI.<>f__mg$cache6);
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x001855C2 File Offset: 0x001839C2
		public static void open()
		{
			if (MenuWorkshopEditorUI.active)
			{
				return;
			}
			MenuWorkshopEditorUI.active = true;
			MenuWorkshopEditorUI.removeButton.reset();
			MenuWorkshopEditorUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x001855F9 File Offset: 0x001839F9
		public static void close()
		{
			if (!MenuWorkshopEditorUI.active)
			{
				return;
			}
			MenuWorkshopEditorUI.active = false;
			MenuWorkshopEditorUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x00185628 File Offset: 0x00183A28
		private static void updateSelection()
		{
			if (PlaySettings.editorMap == null || PlaySettings.editorMap.Length == 0)
			{
				return;
			}
			LevelInfo level = Level.getLevel(PlaySettings.editorMap);
			if (level == null)
			{
				return;
			}
			Local local = Localization.tryRead(level.path, false);
			if (local != null)
			{
				MenuWorkshopEditorUI.descriptionBox.text = local.format("Description");
			}
			if (local != null && local.has("Name"))
			{
				MenuWorkshopEditorUI.selectedBox.text = local.format("Name");
			}
			else
			{
				MenuWorkshopEditorUI.selectedBox.text = PlaySettings.editorMap;
			}
			if (MenuWorkshopEditorUI.previewImage.texture != null && MenuWorkshopEditorUI.previewImage.shouldDestroyTexture)
			{
				UnityEngine.Object.Destroy(MenuWorkshopEditorUI.previewImage.texture);
				MenuWorkshopEditorUI.previewImage.texture = null;
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
				texture2D.name = "Preview_" + PlaySettings.editorMap + "_Selected_Icon";
				texture2D.filterMode = FilterMode.Trilinear;
				texture2D.hideFlags = HideFlags.HideAndDontSave;
				texture2D.LoadImage(data);
				MenuWorkshopEditorUI.previewImage.texture = texture2D;
			}
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x001857A0 File Offset: 0x00183BA0
		private static void onClickedLevel(SleekLevel level, byte index)
		{
			if ((int)index < MenuWorkshopEditorUI.levels.Length && MenuWorkshopEditorUI.levels[(int)index] != null && MenuWorkshopEditorUI.levels[(int)index].isEditable)
			{
				PlaySettings.editorMap = MenuWorkshopEditorUI.levels[(int)index].name;
				MenuWorkshopEditorUI.updateSelection();
			}
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x001857F0 File Offset: 0x00183BF0
		private static void onClickedAddButton(SleekButton button)
		{
			if (MenuWorkshopEditorUI.mapNameField.text != string.Empty)
			{
				Level.add(MenuWorkshopEditorUI.mapNameField.text, MenuWorkshopEditorUI.mapSizeState.state + ELevelSize.SMALL, (MenuWorkshopEditorUI.mapTypeState.state != 0) ? ELevelType.ARENA : ELevelType.SURVIVAL);
				MenuWorkshopEditorUI.mapNameField.text = string.Empty;
			}
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x00185858 File Offset: 0x00183C58
		private static void onClickedRemoveButton(SleekButton button)
		{
			if (PlaySettings.editorMap == null || PlaySettings.editorMap.Length == 0)
			{
				return;
			}
			for (int i = 0; i < MenuWorkshopEditorUI.levels.Length; i++)
			{
				if (MenuWorkshopEditorUI.levels[i] != null && MenuWorkshopEditorUI.levels[i].name == PlaySettings.editorMap && MenuWorkshopEditorUI.levels[i].isEditable)
				{
					Level.remove(MenuWorkshopEditorUI.levels[i].name);
				}
			}
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x001858E0 File Offset: 0x00183CE0
		private static void onClickedEditButton(SleekButton button)
		{
			if (PlaySettings.editorMap == null || PlaySettings.editorMap.Length == 0)
			{
				return;
			}
			for (int i = 0; i < MenuWorkshopEditorUI.levels.Length; i++)
			{
				if (MenuWorkshopEditorUI.levels[i] != null && MenuWorkshopEditorUI.levels[i].name == PlaySettings.editorMap && MenuWorkshopEditorUI.levels[i].isEditable)
				{
					MenuSettings.save();
					Level.edit(MenuWorkshopEditorUI.levels[i], false);
				}
			}
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x0018596C File Offset: 0x00183D6C
		private static void onClickedEdit2Button(SleekButton button)
		{
			if (PlaySettings.editorMap == null || PlaySettings.editorMap.Length == 0)
			{
				return;
			}
			for (int i = 0; i < MenuWorkshopEditorUI.levels.Length; i++)
			{
				if (MenuWorkshopEditorUI.levels[i] != null && MenuWorkshopEditorUI.levels[i].name == PlaySettings.editorMap && MenuWorkshopEditorUI.levels[i].isEditable)
				{
					MenuSettings.save();
					Level.edit(MenuWorkshopEditorUI.levels[i], true);
				}
			}
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x001859F8 File Offset: 0x00183DF8
		private static void onLevelsRefreshed()
		{
			MenuWorkshopEditorUI.levelScrollBox.remove();
			MenuWorkshopEditorUI.levels = Level.getLevels(ESingleplayerMapCategory.EDITABLE);
			bool flag = false;
			MenuWorkshopEditorUI.levelButtons = new SleekLevel[MenuWorkshopEditorUI.levels.Length];
			for (int i = 0; i < MenuWorkshopEditorUI.levels.Length; i++)
			{
				if (MenuWorkshopEditorUI.levels[i] != null)
				{
					SleekLevel sleekLevel = new SleekLevel(MenuWorkshopEditorUI.levels[i], true);
					sleekLevel.positionOffset_Y = i * 110;
					SleekLevel sleekLevel2 = sleekLevel;
					if (MenuWorkshopEditorUI.<>f__mg$cache0 == null)
					{
						MenuWorkshopEditorUI.<>f__mg$cache0 = new ClickedLevel(MenuWorkshopEditorUI.onClickedLevel);
					}
					sleekLevel2.onClickedLevel = MenuWorkshopEditorUI.<>f__mg$cache0;
					MenuWorkshopEditorUI.levelScrollBox.add(sleekLevel);
					MenuWorkshopEditorUI.levelButtons[i] = sleekLevel;
					if (!flag && MenuWorkshopEditorUI.levels[i].name == PlaySettings.editorMap)
					{
						flag = true;
					}
				}
			}
			if (MenuWorkshopEditorUI.levels.Length == 0)
			{
				PlaySettings.editorMap = string.Empty;
			}
			else if (!flag || PlaySettings.editorMap == null || PlaySettings.editorMap.Length == 0)
			{
				PlaySettings.editorMap = MenuWorkshopEditorUI.levels[0].name;
			}
			MenuWorkshopEditorUI.updateSelection();
			MenuWorkshopEditorUI.levelScrollBox.area = new Rect(0f, 0f, 5f, (float)(MenuWorkshopEditorUI.levels.Length * 110 - 10));
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x00185B3E File Offset: 0x00183F3E
		private static void onClickedBackButton(SleekButton button)
		{
			MenuWorkshopUI.open();
			MenuWorkshopEditorUI.close();
		}

		// Token: 0x040028FD RID: 10493
		public static Bundle icons;

		// Token: 0x040028FE RID: 10494
		private static Sleek container;

		// Token: 0x040028FF RID: 10495
		public static bool active;

		// Token: 0x04002900 RID: 10496
		private static SleekButtonIcon backButton;

		// Token: 0x04002901 RID: 10497
		private static LevelInfo[] levels;

		// Token: 0x04002902 RID: 10498
		private static SleekBox previewBox;

		// Token: 0x04002903 RID: 10499
		private static SleekImageTexture previewImage;

		// Token: 0x04002904 RID: 10500
		private static SleekScrollBox levelScrollBox;

		// Token: 0x04002905 RID: 10501
		private static SleekLevel[] levelButtons;

		// Token: 0x04002906 RID: 10502
		private static SleekField mapNameField;

		// Token: 0x04002907 RID: 10503
		private static SleekButtonState mapSizeState;

		// Token: 0x04002908 RID: 10504
		private static SleekButtonState mapTypeState;

		// Token: 0x04002909 RID: 10505
		private static SleekButtonIcon addButton;

		// Token: 0x0400290A RID: 10506
		private static SleekButtonIconConfirm removeButton;

		// Token: 0x0400290B RID: 10507
		private static SleekButtonIcon editButton;

		// Token: 0x0400290C RID: 10508
		private static SleekButtonIcon edit2Button;

		// Token: 0x0400290D RID: 10509
		private static SleekBox selectedBox;

		// Token: 0x0400290E RID: 10510
		private static SleekBox descriptionBox;

		// Token: 0x0400290F RID: 10511
		[CompilerGenerated]
		private static ClickedLevel <>f__mg$cache0;

		// Token: 0x04002910 RID: 10512
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x04002911 RID: 10513
		[CompilerGenerated]
		private static Confirm <>f__mg$cache2;

		// Token: 0x04002912 RID: 10514
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x04002913 RID: 10515
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;

		// Token: 0x04002914 RID: 10516
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;

		// Token: 0x04002915 RID: 10517
		[CompilerGenerated]
		private static LevelsRefreshed <>f__mg$cache6;
	}
}
