using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200078A RID: 1930
	public class MenuWorkshopLocalizationUI
	{
		// Token: 0x0600378E RID: 14222 RVA: 0x00186010 File Offset: 0x00184410
		public MenuWorkshopLocalizationUI()
		{
			MenuWorkshopLocalizationUI.localization = Localization.read("/Menu/Workshop/MenuWorkshopLocalization.dat");
			MenuWorkshopLocalizationUI.container = new Sleek();
			MenuWorkshopLocalizationUI.container.positionOffset_X = 10;
			MenuWorkshopLocalizationUI.container.positionOffset_Y = 10;
			MenuWorkshopLocalizationUI.container.positionScale_Y = 1f;
			MenuWorkshopLocalizationUI.container.sizeOffset_X = -20;
			MenuWorkshopLocalizationUI.container.sizeOffset_Y = -20;
			MenuWorkshopLocalizationUI.container.sizeScale_X = 1f;
			MenuWorkshopLocalizationUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuWorkshopLocalizationUI.container);
			MenuWorkshopLocalizationUI.active = false;
			MenuWorkshopLocalizationUI.headerBox = new SleekBox();
			MenuWorkshopLocalizationUI.headerBox.sizeOffset_Y = 50;
			MenuWorkshopLocalizationUI.headerBox.sizeScale_X = 1f;
			MenuWorkshopLocalizationUI.headerBox.fontSize = 14;
			MenuWorkshopLocalizationUI.headerBox.text = MenuWorkshopLocalizationUI.localization.format("Header", new object[]
			{
				Provider.language,
				"English"
			});
			MenuWorkshopLocalizationUI.container.add(MenuWorkshopLocalizationUI.headerBox);
			MenuWorkshopLocalizationUI.infoBox = new SleekBox();
			MenuWorkshopLocalizationUI.infoBox.positionOffset_Y = 60;
			MenuWorkshopLocalizationUI.infoBox.sizeOffset_X = -30;
			MenuWorkshopLocalizationUI.infoBox.sizeOffset_Y = 50;
			MenuWorkshopLocalizationUI.infoBox.sizeScale_X = 1f;
			MenuWorkshopLocalizationUI.infoBox.fontSize = 14;
			MenuWorkshopLocalizationUI.infoBox.text = MenuWorkshopLocalizationUI.localization.format("No_Differences");
			MenuWorkshopLocalizationUI.container.add(MenuWorkshopLocalizationUI.infoBox);
			MenuWorkshopLocalizationUI.infoBox.isVisible = false;
			MenuWorkshopLocalizationUI.messageBox = new SleekScrollBox();
			MenuWorkshopLocalizationUI.messageBox.positionOffset_Y = 60;
			MenuWorkshopLocalizationUI.messageBox.sizeOffset_Y = -120;
			MenuWorkshopLocalizationUI.messageBox.sizeScale_X = 1f;
			MenuWorkshopLocalizationUI.messageBox.sizeScale_Y = 1f;
			MenuWorkshopLocalizationUI.messageBox.area = new Rect(0f, 0f, 5f, 0f);
			MenuWorkshopLocalizationUI.container.add(MenuWorkshopLocalizationUI.messageBox);
			MenuWorkshopLocalizationUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuWorkshopLocalizationUI.backButton.positionOffset_Y = -50;
			MenuWorkshopLocalizationUI.backButton.positionScale_Y = 1f;
			MenuWorkshopLocalizationUI.backButton.sizeOffset_X = 200;
			MenuWorkshopLocalizationUI.backButton.sizeOffset_Y = 50;
			MenuWorkshopLocalizationUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuWorkshopLocalizationUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton = MenuWorkshopLocalizationUI.backButton;
			if (MenuWorkshopLocalizationUI.<>f__mg$cache0 == null)
			{
				MenuWorkshopLocalizationUI.<>f__mg$cache0 = new ClickedButton(MenuWorkshopLocalizationUI.onClickedBackButton);
			}
			sleekButton.onClickedButton = MenuWorkshopLocalizationUI.<>f__mg$cache0;
			MenuWorkshopLocalizationUI.backButton.fontSize = 14;
			MenuWorkshopLocalizationUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuWorkshopLocalizationUI.container.add(MenuWorkshopLocalizationUI.backButton);
			MenuWorkshopLocalizationUI.refreshButton = new SleekButton();
			MenuWorkshopLocalizationUI.refreshButton.positionOffset_X = -200;
			MenuWorkshopLocalizationUI.refreshButton.positionOffset_Y = -50;
			MenuWorkshopLocalizationUI.refreshButton.positionScale_X = 1f;
			MenuWorkshopLocalizationUI.refreshButton.positionScale_Y = 1f;
			MenuWorkshopLocalizationUI.refreshButton.sizeOffset_X = 200;
			MenuWorkshopLocalizationUI.refreshButton.sizeOffset_Y = 50;
			MenuWorkshopLocalizationUI.refreshButton.text = MenuWorkshopLocalizationUI.localization.format("Refresh");
			MenuWorkshopLocalizationUI.refreshButton.tooltip = MenuWorkshopLocalizationUI.localization.format("Refresh_Tooltip");
			SleekButton sleekButton2 = MenuWorkshopLocalizationUI.refreshButton;
			if (MenuWorkshopLocalizationUI.<>f__mg$cache1 == null)
			{
				MenuWorkshopLocalizationUI.<>f__mg$cache1 = new ClickedButton(MenuWorkshopLocalizationUI.onClickedRefreshButton);
			}
			sleekButton2.onClickedButton = MenuWorkshopLocalizationUI.<>f__mg$cache1;
			MenuWorkshopLocalizationUI.refreshButton.fontSize = 14;
			MenuWorkshopLocalizationUI.container.add(MenuWorkshopLocalizationUI.refreshButton);
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x001863B5 File Offset: 0x001847B5
		public static void open()
		{
			if (MenuWorkshopLocalizationUI.active)
			{
				return;
			}
			MenuWorkshopLocalizationUI.active = true;
			Localization.refresh();
			MenuWorkshopLocalizationUI.refresh();
			MenuWorkshopLocalizationUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x001863EC File Offset: 0x001847EC
		public static void close()
		{
			if (!MenuWorkshopLocalizationUI.active)
			{
				return;
			}
			MenuWorkshopLocalizationUI.active = false;
			MenuWorkshopLocalizationUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x0018641C File Offset: 0x0018481C
		private static void refresh()
		{
			MenuWorkshopLocalizationUI.messageBox.remove();
			for (int i = 0; i < Localization.messages.Count; i++)
			{
				SleekBox sleekBox = new SleekBox();
				sleekBox.positionOffset_Y = i * 60;
				sleekBox.sizeOffset_X = -30;
				sleekBox.sizeOffset_Y = 50;
				sleekBox.sizeScale_X = 1f;
				sleekBox.text = Localization.messages[i];
				MenuWorkshopLocalizationUI.messageBox.add(sleekBox);
			}
			MenuWorkshopLocalizationUI.messageBox.area = new Rect(0f, 0f, 5f, (float)(Localization.messages.Count * 60 - 10));
			MenuWorkshopLocalizationUI.infoBox.isVisible = (Localization.messages.Count == 0);
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x001864DB File Offset: 0x001848DB
		private static void onClickedBackButton(SleekButton button)
		{
			MenuWorkshopUI.open();
			MenuWorkshopLocalizationUI.close();
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x001864E7 File Offset: 0x001848E7
		private static void onClickedRefreshButton(SleekButton button)
		{
			Localization.refresh();
			MenuWorkshopLocalizationUI.refresh();
		}

		// Token: 0x04002920 RID: 10528
		private static Local localization;

		// Token: 0x04002921 RID: 10529
		private static Sleek container;

		// Token: 0x04002922 RID: 10530
		public static bool active;

		// Token: 0x04002923 RID: 10531
		private static SleekButtonIcon backButton;

		// Token: 0x04002924 RID: 10532
		private static SleekButton refreshButton;

		// Token: 0x04002925 RID: 10533
		private static SleekBox headerBox;

		// Token: 0x04002926 RID: 10534
		private static SleekBox infoBox;

		// Token: 0x04002927 RID: 10535
		private static SleekScrollBox messageBox;

		// Token: 0x04002928 RID: 10536
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x04002929 RID: 10537
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;
	}
}
