using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000789 RID: 1929
	public class MenuWorkshopErrorUI
	{
		// Token: 0x06003788 RID: 14216 RVA: 0x00185B4C File Offset: 0x00183F4C
		public MenuWorkshopErrorUI()
		{
			MenuWorkshopErrorUI.localization = Localization.read("/Menu/Workshop/MenuWorkshopError.dat");
			MenuWorkshopErrorUI.container = new Sleek();
			MenuWorkshopErrorUI.container.positionOffset_X = 10;
			MenuWorkshopErrorUI.container.positionOffset_Y = 10;
			MenuWorkshopErrorUI.container.positionScale_Y = 1f;
			MenuWorkshopErrorUI.container.sizeOffset_X = -20;
			MenuWorkshopErrorUI.container.sizeOffset_Y = -20;
			MenuWorkshopErrorUI.container.sizeScale_X = 1f;
			MenuWorkshopErrorUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuWorkshopErrorUI.container);
			MenuWorkshopErrorUI.active = false;
			MenuWorkshopErrorUI.headerBox = new SleekBox();
			MenuWorkshopErrorUI.headerBox.sizeOffset_Y = 50;
			MenuWorkshopErrorUI.headerBox.sizeScale_X = 1f;
			MenuWorkshopErrorUI.headerBox.fontSize = 14;
			MenuWorkshopErrorUI.headerBox.text = MenuWorkshopErrorUI.localization.format("Header");
			MenuWorkshopErrorUI.container.add(MenuWorkshopErrorUI.headerBox);
			MenuWorkshopErrorUI.infoBox = new SleekBox();
			MenuWorkshopErrorUI.infoBox.positionOffset_Y = 60;
			MenuWorkshopErrorUI.infoBox.sizeOffset_X = -30;
			MenuWorkshopErrorUI.infoBox.sizeOffset_Y = 50;
			MenuWorkshopErrorUI.infoBox.sizeScale_X = 1f;
			MenuWorkshopErrorUI.infoBox.fontSize = 14;
			MenuWorkshopErrorUI.infoBox.text = MenuWorkshopErrorUI.localization.format("No_Errors");
			MenuWorkshopErrorUI.container.add(MenuWorkshopErrorUI.infoBox);
			MenuWorkshopErrorUI.infoBox.isVisible = false;
			MenuWorkshopErrorUI.errorBox = new SleekScrollBox();
			MenuWorkshopErrorUI.errorBox.positionOffset_Y = 60;
			MenuWorkshopErrorUI.errorBox.sizeOffset_Y = -120;
			MenuWorkshopErrorUI.errorBox.sizeScale_X = 1f;
			MenuWorkshopErrorUI.errorBox.sizeScale_Y = 1f;
			MenuWorkshopErrorUI.errorBox.area = new Rect(0f, 0f, 5f, 0f);
			MenuWorkshopErrorUI.container.add(MenuWorkshopErrorUI.errorBox);
			MenuWorkshopErrorUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuWorkshopErrorUI.backButton.positionOffset_Y = -50;
			MenuWorkshopErrorUI.backButton.positionScale_Y = 1f;
			MenuWorkshopErrorUI.backButton.sizeOffset_X = 200;
			MenuWorkshopErrorUI.backButton.sizeOffset_Y = 50;
			MenuWorkshopErrorUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuWorkshopErrorUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton = MenuWorkshopErrorUI.backButton;
			if (MenuWorkshopErrorUI.<>f__mg$cache0 == null)
			{
				MenuWorkshopErrorUI.<>f__mg$cache0 = new ClickedButton(MenuWorkshopErrorUI.onClickedBackButton);
			}
			sleekButton.onClickedButton = MenuWorkshopErrorUI.<>f__mg$cache0;
			MenuWorkshopErrorUI.backButton.fontSize = 14;
			MenuWorkshopErrorUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuWorkshopErrorUI.container.add(MenuWorkshopErrorUI.backButton);
			MenuWorkshopErrorUI.refreshButton = new SleekButton();
			MenuWorkshopErrorUI.refreshButton.positionOffset_X = -200;
			MenuWorkshopErrorUI.refreshButton.positionOffset_Y = -50;
			MenuWorkshopErrorUI.refreshButton.positionScale_X = 1f;
			MenuWorkshopErrorUI.refreshButton.positionScale_Y = 1f;
			MenuWorkshopErrorUI.refreshButton.sizeOffset_X = 200;
			MenuWorkshopErrorUI.refreshButton.sizeOffset_Y = 50;
			MenuWorkshopErrorUI.refreshButton.text = MenuWorkshopErrorUI.localization.format("Refresh");
			MenuWorkshopErrorUI.refreshButton.tooltip = MenuWorkshopErrorUI.localization.format("Refresh_Tooltip");
			SleekButton sleekButton2 = MenuWorkshopErrorUI.refreshButton;
			if (MenuWorkshopErrorUI.<>f__mg$cache1 == null)
			{
				MenuWorkshopErrorUI.<>f__mg$cache1 = new ClickedButton(MenuWorkshopErrorUI.onClickedRefreshButton);
			}
			sleekButton2.onClickedButton = MenuWorkshopErrorUI.<>f__mg$cache1;
			MenuWorkshopErrorUI.refreshButton.fontSize = 14;
			MenuWorkshopErrorUI.container.add(MenuWorkshopErrorUI.refreshButton);
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x00185EDB File Offset: 0x001842DB
		public static void open()
		{
			if (MenuWorkshopErrorUI.active)
			{
				return;
			}
			MenuWorkshopErrorUI.active = true;
			MenuWorkshopErrorUI.refresh();
			MenuWorkshopErrorUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x00185F0D File Offset: 0x0018430D
		public static void close()
		{
			if (!MenuWorkshopErrorUI.active)
			{
				return;
			}
			MenuWorkshopErrorUI.active = false;
			MenuWorkshopErrorUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x00185F3C File Offset: 0x0018433C
		private static void refresh()
		{
			MenuWorkshopErrorUI.errorBox.remove();
			for (int i = 0; i < Assets.errors.Count; i++)
			{
				SleekBox sleekBox = new SleekBox();
				sleekBox.positionOffset_Y = i * 60;
				sleekBox.sizeOffset_X = -30;
				sleekBox.sizeOffset_Y = 50;
				sleekBox.sizeScale_X = 1f;
				sleekBox.text = Assets.errors[i];
				MenuWorkshopErrorUI.errorBox.add(sleekBox);
			}
			MenuWorkshopErrorUI.errorBox.area = new Rect(0f, 0f, 5f, (float)(Assets.errors.Count * 60 - 10));
			MenuWorkshopErrorUI.infoBox.isVisible = (Assets.errors.Count == 0);
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x00185FFB File Offset: 0x001843FB
		private static void onClickedBackButton(SleekButton button)
		{
			MenuWorkshopUI.open();
			MenuWorkshopErrorUI.close();
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x00186007 File Offset: 0x00184407
		private static void onClickedRefreshButton(SleekButton button)
		{
			MenuWorkshopErrorUI.refresh();
		}

		// Token: 0x04002916 RID: 10518
		private static Local localization;

		// Token: 0x04002917 RID: 10519
		private static Sleek container;

		// Token: 0x04002918 RID: 10520
		public static bool active;

		// Token: 0x04002919 RID: 10521
		private static SleekButtonIcon backButton;

		// Token: 0x0400291A RID: 10522
		private static SleekButton refreshButton;

		// Token: 0x0400291B RID: 10523
		private static SleekBox headerBox;

		// Token: 0x0400291C RID: 10524
		private static SleekBox infoBox;

		// Token: 0x0400291D RID: 10525
		private static SleekScrollBox errorBox;

		// Token: 0x0400291E RID: 10526
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x0400291F RID: 10527
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;
	}
}
