using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200077D RID: 1917
	public class MenuSurvivorsCharacterUI
	{
		// Token: 0x06003722 RID: 14114 RVA: 0x0017EF24 File Offset: 0x0017D324
		public MenuSurvivorsCharacterUI()
		{
			if (MenuSurvivorsCharacterUI.icons != null)
			{
				MenuSurvivorsCharacterUI.icons.unload();
			}
			MenuSurvivorsCharacterUI.localization = Localization.read("/Menu/Survivors/MenuSurvivorsCharacter.dat");
			MenuSurvivorsCharacterUI.icons = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Survivors/MenuSurvivorsCharacter/MenuSurvivorsCharacter.unity3d");
			MenuSurvivorsCharacterUI.container = new Sleek();
			MenuSurvivorsCharacterUI.container.positionOffset_X = 10;
			MenuSurvivorsCharacterUI.container.positionOffset_Y = 10;
			MenuSurvivorsCharacterUI.container.positionScale_Y = 1f;
			MenuSurvivorsCharacterUI.container.sizeOffset_X = -20;
			MenuSurvivorsCharacterUI.container.sizeOffset_Y = -20;
			MenuSurvivorsCharacterUI.container.sizeScale_X = 1f;
			MenuSurvivorsCharacterUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuSurvivorsCharacterUI.container);
			MenuSurvivorsCharacterUI.active = false;
			MenuSurvivorsCharacterUI.characterBox = new SleekScrollBox();
			MenuSurvivorsCharacterUI.characterBox.positionOffset_X = -100;
			MenuSurvivorsCharacterUI.characterBox.positionOffset_Y = 45;
			MenuSurvivorsCharacterUI.characterBox.positionScale_X = 0.75f;
			MenuSurvivorsCharacterUI.characterBox.positionScale_Y = 0.5f;
			MenuSurvivorsCharacterUI.characterBox.sizeOffset_X = 230;
			MenuSurvivorsCharacterUI.characterBox.sizeOffset_Y = -145;
			MenuSurvivorsCharacterUI.characterBox.sizeScale_Y = 0.5f;
			MenuSurvivorsCharacterUI.characterBox.area = new Rect(0f, 0f, 5f, (float)((Customization.FREE_CHARACTERS + Customization.PRO_CHARACTERS) * 80 - 10));
			MenuSurvivorsCharacterUI.container.add(MenuSurvivorsCharacterUI.characterBox);
			MenuSurvivorsCharacterUI.characterButtons = new SleekCharacter[(int)(Customization.FREE_CHARACTERS + Customization.PRO_CHARACTERS)];
			byte b = 0;
			while ((int)b < MenuSurvivorsCharacterUI.characterButtons.Length)
			{
				SleekCharacter sleekCharacter = new SleekCharacter(b);
				sleekCharacter.positionOffset_Y = (int)(b * 80);
				sleekCharacter.sizeOffset_X = 200;
				sleekCharacter.sizeOffset_Y = 70;
				SleekCharacter sleekCharacter2 = sleekCharacter;
				if (MenuSurvivorsCharacterUI.<>f__mg$cache0 == null)
				{
					MenuSurvivorsCharacterUI.<>f__mg$cache0 = new ClickedCharacter(MenuSurvivorsCharacterUI.onClickedCharacter);
				}
				sleekCharacter2.onClickedCharacter = MenuSurvivorsCharacterUI.<>f__mg$cache0;
				MenuSurvivorsCharacterUI.characterBox.add(sleekCharacter);
				MenuSurvivorsCharacterUI.characterButtons[(int)b] = sleekCharacter;
				b += 1;
			}
			MenuSurvivorsCharacterUI.nameField = new SleekField();
			MenuSurvivorsCharacterUI.nameField.positionOffset_X = -100;
			MenuSurvivorsCharacterUI.nameField.positionOffset_Y = 100;
			MenuSurvivorsCharacterUI.nameField.positionScale_X = 0.75f;
			MenuSurvivorsCharacterUI.nameField.sizeOffset_X = 200;
			MenuSurvivorsCharacterUI.nameField.sizeOffset_Y = 30;
			MenuSurvivorsCharacterUI.nameField.maxLength = 32;
			MenuSurvivorsCharacterUI.nameField.addLabel(MenuSurvivorsCharacterUI.localization.format("Name_Field_Label"), ESleekSide.LEFT);
			SleekField sleekField = MenuSurvivorsCharacterUI.nameField;
			if (MenuSurvivorsCharacterUI.<>f__mg$cache1 == null)
			{
				MenuSurvivorsCharacterUI.<>f__mg$cache1 = new Typed(MenuSurvivorsCharacterUI.onTypedNameField);
			}
			sleekField.onTyped = MenuSurvivorsCharacterUI.<>f__mg$cache1;
			MenuSurvivorsCharacterUI.container.add(MenuSurvivorsCharacterUI.nameField);
			MenuSurvivorsCharacterUI.nickField = new SleekField();
			MenuSurvivorsCharacterUI.nickField.positionOffset_X = -100;
			MenuSurvivorsCharacterUI.nickField.positionOffset_Y = 140;
			MenuSurvivorsCharacterUI.nickField.positionScale_X = 0.75f;
			MenuSurvivorsCharacterUI.nickField.sizeOffset_X = 200;
			MenuSurvivorsCharacterUI.nickField.sizeOffset_Y = 30;
			MenuSurvivorsCharacterUI.nickField.maxLength = 32;
			MenuSurvivorsCharacterUI.nickField.addLabel(MenuSurvivorsCharacterUI.localization.format("Nick_Field_Label"), ESleekSide.LEFT);
			SleekField sleekField2 = MenuSurvivorsCharacterUI.nickField;
			if (MenuSurvivorsCharacterUI.<>f__mg$cache2 == null)
			{
				MenuSurvivorsCharacterUI.<>f__mg$cache2 = new Typed(MenuSurvivorsCharacterUI.onTypedNickField);
			}
			sleekField2.onTyped = MenuSurvivorsCharacterUI.<>f__mg$cache2;
			MenuSurvivorsCharacterUI.container.add(MenuSurvivorsCharacterUI.nickField);
			MenuSurvivorsCharacterUI.skillsetBox = new SleekBoxIcon(null);
			MenuSurvivorsCharacterUI.skillsetBox.positionOffset_X = -100;
			MenuSurvivorsCharacterUI.skillsetBox.positionOffset_Y = 180;
			MenuSurvivorsCharacterUI.skillsetBox.positionScale_X = 0.75f;
			MenuSurvivorsCharacterUI.skillsetBox.sizeOffset_X = 200;
			MenuSurvivorsCharacterUI.skillsetBox.sizeOffset_Y = 30;
			MenuSurvivorsCharacterUI.skillsetBox.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuSurvivorsCharacterUI.skillsetBox.addLabel(MenuSurvivorsCharacterUI.localization.format("Skillset_Box_Label"), ESleekSide.LEFT);
			MenuSurvivorsCharacterUI.container.add(MenuSurvivorsCharacterUI.skillsetBox);
			MenuSurvivorsCharacterUI.skillsetsBox = new SleekScrollBox();
			MenuSurvivorsCharacterUI.skillsetsBox.positionOffset_X = -100;
			MenuSurvivorsCharacterUI.skillsetsBox.positionOffset_Y = 220;
			MenuSurvivorsCharacterUI.skillsetsBox.positionScale_X = 0.75f;
			MenuSurvivorsCharacterUI.skillsetsBox.sizeOffset_X = 230;
			MenuSurvivorsCharacterUI.skillsetsBox.sizeOffset_Y = -185;
			MenuSurvivorsCharacterUI.skillsetsBox.sizeScale_Y = 0.5f;
			MenuSurvivorsCharacterUI.skillsetsBox.area = new Rect(0f, 0f, 5f, (float)(Customization.SKILLSETS * 40 - 10));
			MenuSurvivorsCharacterUI.container.add(MenuSurvivorsCharacterUI.skillsetsBox);
			for (int i = 0; i < (int)Customization.SKILLSETS; i++)
			{
				SleekButtonIcon sleekButtonIcon = new SleekButtonIcon((Texture2D)MenuSurvivorsCharacterUI.icons.load("Skillset_" + i));
				sleekButtonIcon.positionOffset_Y = i * 40;
				sleekButtonIcon.sizeOffset_X = 200;
				sleekButtonIcon.sizeOffset_Y = 30;
				sleekButtonIcon.text = MenuSurvivorsCharacterUI.localization.format("Skillset_" + i);
				sleekButtonIcon.iconImage.backgroundTint = ESleekTint.FOREGROUND;
				SleekButton sleekButton = sleekButtonIcon;
				if (MenuSurvivorsCharacterUI.<>f__mg$cache3 == null)
				{
					MenuSurvivorsCharacterUI.<>f__mg$cache3 = new ClickedButton(MenuSurvivorsCharacterUI.onClickedSkillset);
				}
				sleekButton.onClickedButton = MenuSurvivorsCharacterUI.<>f__mg$cache3;
				MenuSurvivorsCharacterUI.skillsetsBox.add(sleekButtonIcon);
			}
			Delegate onCharacterUpdated = Characters.onCharacterUpdated;
			if (MenuSurvivorsCharacterUI.<>f__mg$cache4 == null)
			{
				MenuSurvivorsCharacterUI.<>f__mg$cache4 = new CharacterUpdated(MenuSurvivorsCharacterUI.onCharacterUpdated);
			}
			Characters.onCharacterUpdated = (CharacterUpdated)Delegate.Combine(onCharacterUpdated, MenuSurvivorsCharacterUI.<>f__mg$cache4);
			MenuSurvivorsCharacterUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuSurvivorsCharacterUI.backButton.positionOffset_Y = -50;
			MenuSurvivorsCharacterUI.backButton.positionScale_Y = 1f;
			MenuSurvivorsCharacterUI.backButton.sizeOffset_X = 200;
			MenuSurvivorsCharacterUI.backButton.sizeOffset_Y = 50;
			MenuSurvivorsCharacterUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuSurvivorsCharacterUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton2 = MenuSurvivorsCharacterUI.backButton;
			if (MenuSurvivorsCharacterUI.<>f__mg$cache5 == null)
			{
				MenuSurvivorsCharacterUI.<>f__mg$cache5 = new ClickedButton(MenuSurvivorsCharacterUI.onClickedBackButton);
			}
			sleekButton2.onClickedButton = MenuSurvivorsCharacterUI.<>f__mg$cache5;
			MenuSurvivorsCharacterUI.backButton.fontSize = 14;
			MenuSurvivorsCharacterUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuSurvivorsCharacterUI.container.add(MenuSurvivorsCharacterUI.backButton);
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x0017F54A File Offset: 0x0017D94A
		public static void open()
		{
			if (MenuSurvivorsCharacterUI.active)
			{
				return;
			}
			MenuSurvivorsCharacterUI.active = true;
			MenuSurvivorsCharacterUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x0017F577 File Offset: 0x0017D977
		public static void close()
		{
			if (!MenuSurvivorsCharacterUI.active)
			{
				return;
			}
			MenuSurvivorsCharacterUI.active = false;
			MenuSurvivorsCharacterUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x0017F5A4 File Offset: 0x0017D9A4
		private static void onCharacterUpdated(byte index, Character character)
		{
			if (index == Characters.selected)
			{
				MenuSurvivorsCharacterUI.nameField.text = character.name;
				MenuSurvivorsCharacterUI.nickField.text = character.nick;
				MenuSurvivorsCharacterUI.skillsetBox.icon = (Texture2D)MenuSurvivorsCharacterUI.icons.load("Skillset_" + (int)character.skillset);
				MenuSurvivorsCharacterUI.skillsetBox.text = MenuSurvivorsCharacterUI.localization.format("Skillset_" + (byte)character.skillset);
			}
			MenuSurvivorsCharacterUI.characterButtons[(int)index].updateCharacter(character);
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x0017F641 File Offset: 0x0017DA41
		private static void onTypedNameField(SleekField field, string text)
		{
			Characters.rename(text);
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x0017F649 File Offset: 0x0017DA49
		private static void onTypedNickField(SleekField field, string text)
		{
			Characters.renick(text);
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x0017F651 File Offset: 0x0017DA51
		private static void onClickedCharacter(SleekCharacter character, byte index)
		{
			Characters.selected = index;
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x0017F659 File Offset: 0x0017DA59
		private static void onClickedSkillset(SleekButton button)
		{
			Characters.skillify((EPlayerSkillset)(button.positionOffset_Y / 40));
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x0017F669 File Offset: 0x0017DA69
		private static void onClickedBackButton(SleekButton button)
		{
			MenuSurvivorsUI.open();
			MenuSurvivorsCharacterUI.close();
		}

		// Token: 0x0400283E RID: 10302
		public static Local localization;

		// Token: 0x0400283F RID: 10303
		public static Bundle icons;

		// Token: 0x04002840 RID: 10304
		private static Sleek container;

		// Token: 0x04002841 RID: 10305
		public static bool active;

		// Token: 0x04002842 RID: 10306
		private static SleekButtonIcon backButton;

		// Token: 0x04002843 RID: 10307
		private static SleekScrollBox characterBox;

		// Token: 0x04002844 RID: 10308
		private static SleekCharacter[] characterButtons;

		// Token: 0x04002845 RID: 10309
		private static SleekField nameField;

		// Token: 0x04002846 RID: 10310
		private static SleekField nickField;

		// Token: 0x04002847 RID: 10311
		private static SleekBoxIcon skillsetBox;

		// Token: 0x04002848 RID: 10312
		private static SleekScrollBox skillsetsBox;

		// Token: 0x04002849 RID: 10313
		[CompilerGenerated]
		private static ClickedCharacter <>f__mg$cache0;

		// Token: 0x0400284A RID: 10314
		[CompilerGenerated]
		private static Typed <>f__mg$cache1;

		// Token: 0x0400284B RID: 10315
		[CompilerGenerated]
		private static Typed <>f__mg$cache2;

		// Token: 0x0400284C RID: 10316
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x0400284D RID: 10317
		[CompilerGenerated]
		private static CharacterUpdated <>f__mg$cache4;

		// Token: 0x0400284E RID: 10318
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;
	}
}
