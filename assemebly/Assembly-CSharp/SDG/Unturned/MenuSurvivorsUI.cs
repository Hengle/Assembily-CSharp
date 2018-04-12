using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000787 RID: 1927
	public class MenuSurvivorsUI
	{
		// Token: 0x06003775 RID: 14197 RVA: 0x001846A4 File Offset: 0x00182AA4
		public MenuSurvivorsUI()
		{
			Local local = Localization.read("/Menu/Survivors/MenuSurvivors.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Survivors/MenuSurvivors/MenuSurvivors.unity3d");
			MenuSurvivorsUI.container = new Sleek();
			MenuSurvivorsUI.container.positionOffset_X = 10;
			MenuSurvivorsUI.container.positionOffset_Y = 10;
			MenuSurvivorsUI.container.positionScale_Y = -1f;
			MenuSurvivorsUI.container.sizeOffset_X = -20;
			MenuSurvivorsUI.container.sizeOffset_Y = -20;
			MenuSurvivorsUI.container.sizeScale_X = 1f;
			MenuSurvivorsUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuSurvivorsUI.container);
			MenuSurvivorsUI.active = false;
			MenuSurvivorsUI.characterButton = new SleekButtonIcon((Texture2D)bundle.load("Character"));
			MenuSurvivorsUI.characterButton.positionOffset_X = -100;
			MenuSurvivorsUI.characterButton.positionOffset_Y = -145;
			MenuSurvivorsUI.characterButton.positionScale_X = 0.5f;
			MenuSurvivorsUI.characterButton.positionScale_Y = 0.5f;
			MenuSurvivorsUI.characterButton.sizeOffset_X = 200;
			MenuSurvivorsUI.characterButton.sizeOffset_Y = 50;
			MenuSurvivorsUI.characterButton.text = local.format("CharacterButtonText");
			MenuSurvivorsUI.characterButton.tooltip = local.format("CharacterButtonTooltip");
			SleekButton sleekButton = MenuSurvivorsUI.characterButton;
			if (MenuSurvivorsUI.<>f__mg$cache0 == null)
			{
				MenuSurvivorsUI.<>f__mg$cache0 = new ClickedButton(MenuSurvivorsUI.onClickedCharacterButton);
			}
			sleekButton.onClickedButton = MenuSurvivorsUI.<>f__mg$cache0;
			MenuSurvivorsUI.characterButton.fontSize = 14;
			MenuSurvivorsUI.characterButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuSurvivorsUI.container.add(MenuSurvivorsUI.characterButton);
			MenuSurvivorsUI.appearanceButton = new SleekButtonIcon((Texture2D)bundle.load("Appearance"));
			MenuSurvivorsUI.appearanceButton.positionOffset_X = -100;
			MenuSurvivorsUI.appearanceButton.positionOffset_Y = -85;
			MenuSurvivorsUI.appearanceButton.positionScale_X = 0.5f;
			MenuSurvivorsUI.appearanceButton.positionScale_Y = 0.5f;
			MenuSurvivorsUI.appearanceButton.sizeOffset_X = 200;
			MenuSurvivorsUI.appearanceButton.sizeOffset_Y = 50;
			MenuSurvivorsUI.appearanceButton.text = local.format("AppearanceButtonText");
			MenuSurvivorsUI.appearanceButton.tooltip = local.format("AppearanceButtonTooltip");
			SleekButton sleekButton2 = MenuSurvivorsUI.appearanceButton;
			if (MenuSurvivorsUI.<>f__mg$cache1 == null)
			{
				MenuSurvivorsUI.<>f__mg$cache1 = new ClickedButton(MenuSurvivorsUI.onClickedAppearanceButton);
			}
			sleekButton2.onClickedButton = MenuSurvivorsUI.<>f__mg$cache1;
			MenuSurvivorsUI.appearanceButton.fontSize = 14;
			MenuSurvivorsUI.appearanceButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuSurvivorsUI.container.add(MenuSurvivorsUI.appearanceButton);
			MenuSurvivorsUI.groupButton = new SleekButtonIcon((Texture2D)bundle.load("Group"));
			MenuSurvivorsUI.groupButton.positionOffset_X = -100;
			MenuSurvivorsUI.groupButton.positionOffset_Y = -25;
			MenuSurvivorsUI.groupButton.positionScale_X = 0.5f;
			MenuSurvivorsUI.groupButton.positionScale_Y = 0.5f;
			MenuSurvivorsUI.groupButton.sizeOffset_X = 200;
			MenuSurvivorsUI.groupButton.sizeOffset_Y = 50;
			MenuSurvivorsUI.groupButton.text = local.format("GroupButtonText");
			MenuSurvivorsUI.groupButton.tooltip = local.format("GroupButtonTooltip");
			SleekButton sleekButton3 = MenuSurvivorsUI.groupButton;
			if (MenuSurvivorsUI.<>f__mg$cache2 == null)
			{
				MenuSurvivorsUI.<>f__mg$cache2 = new ClickedButton(MenuSurvivorsUI.onClickedGroupButton);
			}
			sleekButton3.onClickedButton = MenuSurvivorsUI.<>f__mg$cache2;
			MenuSurvivorsUI.groupButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuSurvivorsUI.groupButton.fontSize = 14;
			MenuSurvivorsUI.container.add(MenuSurvivorsUI.groupButton);
			MenuSurvivorsUI.clothingButton = new SleekButtonIcon((Texture2D)bundle.load("Clothing"));
			MenuSurvivorsUI.clothingButton.positionOffset_X = -100;
			MenuSurvivorsUI.clothingButton.positionOffset_Y = 35;
			MenuSurvivorsUI.clothingButton.positionScale_X = 0.5f;
			MenuSurvivorsUI.clothingButton.positionScale_Y = 0.5f;
			MenuSurvivorsUI.clothingButton.sizeOffset_X = 200;
			MenuSurvivorsUI.clothingButton.sizeOffset_Y = 50;
			MenuSurvivorsUI.clothingButton.text = local.format("ClothingButtonText");
			MenuSurvivorsUI.clothingButton.tooltip = local.format("ClothingButtonTooltip");
			SleekButton sleekButton4 = MenuSurvivorsUI.clothingButton;
			if (MenuSurvivorsUI.<>f__mg$cache3 == null)
			{
				MenuSurvivorsUI.<>f__mg$cache3 = new ClickedButton(MenuSurvivorsUI.onClickedClothingButton);
			}
			sleekButton4.onClickedButton = MenuSurvivorsUI.<>f__mg$cache3;
			MenuSurvivorsUI.clothingButton.fontSize = 14;
			MenuSurvivorsUI.clothingButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuSurvivorsUI.container.add(MenuSurvivorsUI.clothingButton);
			MenuSurvivorsUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuSurvivorsUI.backButton.positionOffset_X = -100;
			MenuSurvivorsUI.backButton.positionOffset_Y = 95;
			MenuSurvivorsUI.backButton.positionScale_X = 0.5f;
			MenuSurvivorsUI.backButton.positionScale_Y = 0.5f;
			MenuSurvivorsUI.backButton.sizeOffset_X = 200;
			MenuSurvivorsUI.backButton.sizeOffset_Y = 50;
			MenuSurvivorsUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuSurvivorsUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton5 = MenuSurvivorsUI.backButton;
			if (MenuSurvivorsUI.<>f__mg$cache4 == null)
			{
				MenuSurvivorsUI.<>f__mg$cache4 = new ClickedButton(MenuSurvivorsUI.onClickedBackButton);
			}
			sleekButton5.onClickedButton = MenuSurvivorsUI.<>f__mg$cache4;
			MenuSurvivorsUI.backButton.fontSize = 14;
			MenuSurvivorsUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuSurvivorsUI.container.add(MenuSurvivorsUI.backButton);
			bundle.unload();
			new MenuSurvivorsCharacterUI();
			new MenuSurvivorsAppearanceUI();
			new MenuSurvivorsGroupUI();
			new MenuSurvivorsClothingUI();
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x00184BF9 File Offset: 0x00182FF9
		public static void open()
		{
			if (MenuSurvivorsUI.active)
			{
				return;
			}
			MenuSurvivorsUI.active = true;
			MenuSurvivorsUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x00184C26 File Offset: 0x00183026
		public static void close()
		{
			if (!MenuSurvivorsUI.active)
			{
				return;
			}
			MenuSurvivorsUI.active = false;
			Characters.save();
			MenuSurvivorsUI.container.lerpPositionScale(0f, -1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x00184C58 File Offset: 0x00183058
		private static void onClickedCharacterButton(SleekButton button)
		{
			MenuSurvivorsCharacterUI.open();
			MenuSurvivorsUI.close();
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x00184C64 File Offset: 0x00183064
		private static void onClickedAppearanceButton(SleekButton button)
		{
			MenuSurvivorsAppearanceUI.open();
			MenuSurvivorsUI.close();
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x00184C70 File Offset: 0x00183070
		private static void onClickedGroupButton(SleekButton button)
		{
			MenuSurvivorsGroupUI.open();
			MenuSurvivorsUI.close();
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x00184C7C File Offset: 0x0018307C
		private static void onClickedClothingButton(SleekButton button)
		{
			MenuSurvivorsClothingUI.open();
			MenuSurvivorsUI.close();
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x00184C88 File Offset: 0x00183088
		private static void onClickedBackButton(SleekButton button)
		{
			MenuDashboardUI.open();
			MenuTitleUI.open();
			MenuSurvivorsUI.close();
		}

		// Token: 0x040028F1 RID: 10481
		private static Sleek container;

		// Token: 0x040028F2 RID: 10482
		public static bool active;

		// Token: 0x040028F3 RID: 10483
		private static SleekButtonIcon characterButton;

		// Token: 0x040028F4 RID: 10484
		private static SleekButtonIcon appearanceButton;

		// Token: 0x040028F5 RID: 10485
		private static SleekButtonIcon groupButton;

		// Token: 0x040028F6 RID: 10486
		private static SleekButtonIcon clothingButton;

		// Token: 0x040028F7 RID: 10487
		private static SleekButtonIcon backButton;

		// Token: 0x040028F8 RID: 10488
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x040028F9 RID: 10489
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x040028FA RID: 10490
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x040028FB RID: 10491
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x040028FC RID: 10492
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;
	}
}
