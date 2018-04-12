using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000768 RID: 1896
	public class MenuConfigurationUI
	{
		// Token: 0x0600364B RID: 13899 RVA: 0x0017091C File Offset: 0x0016ED1C
		public MenuConfigurationUI()
		{
			Local local = Localization.read("/Menu/Configuration/MenuConfiguration.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Configuration/MenuConfiguration/MenuConfiguration.unity3d");
			MenuConfigurationUI.container = new Sleek();
			MenuConfigurationUI.container.positionOffset_X = 10;
			MenuConfigurationUI.container.positionOffset_Y = 10;
			MenuConfigurationUI.container.positionScale_Y = -1f;
			MenuConfigurationUI.container.sizeOffset_X = -20;
			MenuConfigurationUI.container.sizeOffset_Y = -20;
			MenuConfigurationUI.container.sizeScale_X = 1f;
			MenuConfigurationUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuConfigurationUI.container);
			MenuConfigurationUI.active = false;
			MenuConfigurationUI.optionsButton = new SleekButtonIcon((Texture2D)bundle.load("Options"));
			MenuConfigurationUI.optionsButton.positionOffset_X = -100;
			MenuConfigurationUI.optionsButton.positionOffset_Y = -145;
			MenuConfigurationUI.optionsButton.positionScale_X = 0.5f;
			MenuConfigurationUI.optionsButton.positionScale_Y = 0.5f;
			MenuConfigurationUI.optionsButton.sizeOffset_X = 200;
			MenuConfigurationUI.optionsButton.sizeOffset_Y = 50;
			MenuConfigurationUI.optionsButton.text = local.format("Options_Button_Text");
			MenuConfigurationUI.optionsButton.tooltip = local.format("Options_Button_Tooltip");
			SleekButton sleekButton = MenuConfigurationUI.optionsButton;
			if (MenuConfigurationUI.<>f__mg$cache0 == null)
			{
				MenuConfigurationUI.<>f__mg$cache0 = new ClickedButton(MenuConfigurationUI.onClickedOptionsButton);
			}
			sleekButton.onClickedButton = MenuConfigurationUI.<>f__mg$cache0;
			MenuConfigurationUI.optionsButton.fontSize = 14;
			MenuConfigurationUI.optionsButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuConfigurationUI.container.add(MenuConfigurationUI.optionsButton);
			MenuConfigurationUI.displayButton = new SleekButtonIcon((Texture2D)bundle.load("Display"));
			MenuConfigurationUI.displayButton.positionOffset_X = -100;
			MenuConfigurationUI.displayButton.positionOffset_Y = -85;
			MenuConfigurationUI.displayButton.positionScale_X = 0.5f;
			MenuConfigurationUI.displayButton.positionScale_Y = 0.5f;
			MenuConfigurationUI.displayButton.sizeOffset_X = 200;
			MenuConfigurationUI.displayButton.sizeOffset_Y = 50;
			MenuConfigurationUI.displayButton.text = local.format("Display_Button_Text");
			MenuConfigurationUI.displayButton.tooltip = local.format("Display_Button_Tooltip");
			SleekButton sleekButton2 = MenuConfigurationUI.displayButton;
			if (MenuConfigurationUI.<>f__mg$cache1 == null)
			{
				MenuConfigurationUI.<>f__mg$cache1 = new ClickedButton(MenuConfigurationUI.onClickedDisplayButton);
			}
			sleekButton2.onClickedButton = MenuConfigurationUI.<>f__mg$cache1;
			MenuConfigurationUI.displayButton.fontSize = 14;
			MenuConfigurationUI.displayButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuConfigurationUI.container.add(MenuConfigurationUI.displayButton);
			MenuConfigurationUI.graphicsButton = new SleekButtonIcon((Texture2D)bundle.load("Graphics"));
			MenuConfigurationUI.graphicsButton.positionOffset_X = -100;
			MenuConfigurationUI.graphicsButton.positionOffset_Y = -25;
			MenuConfigurationUI.graphicsButton.positionScale_X = 0.5f;
			MenuConfigurationUI.graphicsButton.positionScale_Y = 0.5f;
			MenuConfigurationUI.graphicsButton.sizeOffset_X = 200;
			MenuConfigurationUI.graphicsButton.sizeOffset_Y = 50;
			MenuConfigurationUI.graphicsButton.text = local.format("Graphics_Button_Text");
			MenuConfigurationUI.graphicsButton.tooltip = local.format("Graphics_Button_Tooltip");
			SleekButton sleekButton3 = MenuConfigurationUI.graphicsButton;
			if (MenuConfigurationUI.<>f__mg$cache2 == null)
			{
				MenuConfigurationUI.<>f__mg$cache2 = new ClickedButton(MenuConfigurationUI.onClickedGraphicsButton);
			}
			sleekButton3.onClickedButton = MenuConfigurationUI.<>f__mg$cache2;
			MenuConfigurationUI.graphicsButton.fontSize = 14;
			MenuConfigurationUI.graphicsButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuConfigurationUI.container.add(MenuConfigurationUI.graphicsButton);
			MenuConfigurationUI.controlsButton = new SleekButtonIcon((Texture2D)bundle.load("Controls"));
			MenuConfigurationUI.controlsButton.positionOffset_X = -100;
			MenuConfigurationUI.controlsButton.positionOffset_Y = 35;
			MenuConfigurationUI.controlsButton.positionScale_X = 0.5f;
			MenuConfigurationUI.controlsButton.positionScale_Y = 0.5f;
			MenuConfigurationUI.controlsButton.sizeOffset_X = 200;
			MenuConfigurationUI.controlsButton.sizeOffset_Y = 50;
			MenuConfigurationUI.controlsButton.text = local.format("Controls_Button_Text");
			MenuConfigurationUI.controlsButton.tooltip = local.format("Controls_Button_Tooltip");
			SleekButton sleekButton4 = MenuConfigurationUI.controlsButton;
			if (MenuConfigurationUI.<>f__mg$cache3 == null)
			{
				MenuConfigurationUI.<>f__mg$cache3 = new ClickedButton(MenuConfigurationUI.onClickedControlsButton);
			}
			sleekButton4.onClickedButton = MenuConfigurationUI.<>f__mg$cache3;
			MenuConfigurationUI.controlsButton.fontSize = 14;
			MenuConfigurationUI.controlsButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuConfigurationUI.container.add(MenuConfigurationUI.controlsButton);
			MenuConfigurationUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuConfigurationUI.backButton.positionOffset_X = -100;
			MenuConfigurationUI.backButton.positionOffset_Y = 95;
			MenuConfigurationUI.backButton.positionScale_X = 0.5f;
			MenuConfigurationUI.backButton.positionScale_Y = 0.5f;
			MenuConfigurationUI.backButton.sizeOffset_X = 200;
			MenuConfigurationUI.backButton.sizeOffset_Y = 50;
			MenuConfigurationUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuConfigurationUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton5 = MenuConfigurationUI.backButton;
			if (MenuConfigurationUI.<>f__mg$cache4 == null)
			{
				MenuConfigurationUI.<>f__mg$cache4 = new ClickedButton(MenuConfigurationUI.onClickedBackButton);
			}
			sleekButton5.onClickedButton = MenuConfigurationUI.<>f__mg$cache4;
			MenuConfigurationUI.backButton.fontSize = 14;
			MenuConfigurationUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuConfigurationUI.container.add(MenuConfigurationUI.backButton);
			bundle.unload();
			new MenuConfigurationOptionsUI();
			new MenuConfigurationDisplayUI();
			new MenuConfigurationGraphicsUI();
			new MenuConfigurationControlsUI();
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x00170E71 File Offset: 0x0016F271
		public static void open()
		{
			if (MenuConfigurationUI.active)
			{
				return;
			}
			MenuConfigurationUI.active = true;
			MenuConfigurationUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x00170E9E File Offset: 0x0016F29E
		public static void close()
		{
			if (!MenuConfigurationUI.active)
			{
				return;
			}
			MenuConfigurationUI.active = false;
			MenuSettings.save();
			MenuConfigurationUI.container.lerpPositionScale(0f, -1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x00170ED0 File Offset: 0x0016F2D0
		private static void onClickedOptionsButton(SleekButton button)
		{
			MenuConfigurationOptionsUI.open();
			MenuConfigurationUI.close();
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x00170EDC File Offset: 0x0016F2DC
		private static void onClickedDisplayButton(SleekButton button)
		{
			MenuConfigurationDisplayUI.open();
			MenuConfigurationUI.close();
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x00170EE8 File Offset: 0x0016F2E8
		private static void onClickedGraphicsButton(SleekButton button)
		{
			MenuConfigurationGraphicsUI.open();
			MenuConfigurationUI.close();
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x00170EF4 File Offset: 0x0016F2F4
		private static void onClickedControlsButton(SleekButton button)
		{
			MenuConfigurationControlsUI.open();
			MenuConfigurationUI.close();
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x00170F00 File Offset: 0x0016F300
		private static void onClickedBackButton(SleekButton button)
		{
			MenuDashboardUI.open();
			MenuTitleUI.open();
			MenuConfigurationUI.close();
		}

		// Token: 0x040026B8 RID: 9912
		private static Sleek container;

		// Token: 0x040026B9 RID: 9913
		public static bool active;

		// Token: 0x040026BA RID: 9914
		private static SleekButtonIcon optionsButton;

		// Token: 0x040026BB RID: 9915
		private static SleekButtonIcon displayButton;

		// Token: 0x040026BC RID: 9916
		private static SleekButtonIcon graphicsButton;

		// Token: 0x040026BD RID: 9917
		private static SleekButtonIcon controlsButton;

		// Token: 0x040026BE RID: 9918
		private static SleekButtonIcon backButton;

		// Token: 0x040026BF RID: 9919
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x040026C0 RID: 9920
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x040026C1 RID: 9921
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x040026C2 RID: 9922
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x040026C3 RID: 9923
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;
	}
}
