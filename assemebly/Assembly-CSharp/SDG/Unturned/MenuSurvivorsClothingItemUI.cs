using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000782 RID: 1922
	public class MenuSurvivorsClothingItemUI
	{
		// Token: 0x06003743 RID: 14147 RVA: 0x001818F8 File Offset: 0x0017FCF8
		public MenuSurvivorsClothingItemUI()
		{
			MenuSurvivorsClothingItemUI.localization = Localization.read("/Menu/Survivors/MenuSurvivorsClothingItem.dat");
			MenuSurvivorsClothingItemUI.container = new Sleek();
			MenuSurvivorsClothingItemUI.container.positionOffset_X = 10;
			MenuSurvivorsClothingItemUI.container.positionOffset_Y = 10;
			MenuSurvivorsClothingItemUI.container.positionScale_Y = 1f;
			MenuSurvivorsClothingItemUI.container.sizeOffset_X = -20;
			MenuSurvivorsClothingItemUI.container.sizeOffset_Y = -20;
			MenuSurvivorsClothingItemUI.container.sizeScale_X = 1f;
			MenuSurvivorsClothingItemUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuSurvivorsClothingItemUI.container);
			MenuSurvivorsClothingItemUI.active = false;
			MenuSurvivorsClothingItemUI.inventory = new Sleek();
			MenuSurvivorsClothingItemUI.inventory.positionScale_X = 0.5f;
			MenuSurvivorsClothingItemUI.inventory.positionOffset_Y = 10;
			MenuSurvivorsClothingItemUI.inventory.sizeScale_X = 0.5f;
			MenuSurvivorsClothingItemUI.inventory.sizeScale_Y = 1f;
			MenuSurvivorsClothingItemUI.inventory.sizeOffset_Y = -20;
			MenuSurvivorsClothingItemUI.inventory.constraint = ESleekConstraint.XY;
			MenuSurvivorsClothingItemUI.container.add(MenuSurvivorsClothingItemUI.inventory);
			MenuSurvivorsClothingItemUI.packageBox = new SleekInventory();
			MenuSurvivorsClothingItemUI.packageBox.sizeScale_X = 1f;
			MenuSurvivorsClothingItemUI.packageBox.sizeScale_Y = 0.5f;
			MenuSurvivorsClothingItemUI.packageBox.sizeOffset_Y = -5;
			MenuSurvivorsClothingItemUI.packageBox.constraint = ESleekConstraint.XY;
			MenuSurvivorsClothingItemUI.inventory.add(MenuSurvivorsClothingItemUI.packageBox);
			MenuSurvivorsClothingItemUI.descriptionBox = new SleekBox();
			MenuSurvivorsClothingItemUI.descriptionBox.positionOffset_Y = 10;
			MenuSurvivorsClothingItemUI.descriptionBox.positionScale_Y = 1f;
			MenuSurvivorsClothingItemUI.descriptionBox.sizeScale_X = 1f;
			MenuSurvivorsClothingItemUI.descriptionBox.sizeScale_Y = 1f;
			MenuSurvivorsClothingItemUI.descriptionBox.foregroundTint = ESleekTint.NONE;
			MenuSurvivorsClothingItemUI.packageBox.add(MenuSurvivorsClothingItemUI.descriptionBox);
			MenuSurvivorsClothingItemUI.infoLabel = new SleekLabel();
			MenuSurvivorsClothingItemUI.infoLabel.isRich = true;
			MenuSurvivorsClothingItemUI.infoLabel.positionOffset_X = 5;
			MenuSurvivorsClothingItemUI.infoLabel.positionOffset_Y = 5;
			MenuSurvivorsClothingItemUI.infoLabel.sizeScale_X = 1f;
			MenuSurvivorsClothingItemUI.infoLabel.sizeScale_Y = 1f;
			MenuSurvivorsClothingItemUI.infoLabel.sizeOffset_X = -10;
			MenuSurvivorsClothingItemUI.infoLabel.sizeOffset_Y = -10;
			MenuSurvivorsClothingItemUI.infoLabel.fontAlignment = TextAnchor.UpperLeft;
			MenuSurvivorsClothingItemUI.infoLabel.foregroundTint = ESleekTint.NONE;
			MenuSurvivorsClothingItemUI.descriptionBox.add(MenuSurvivorsClothingItemUI.infoLabel);
			MenuSurvivorsClothingItemUI.useButton = new SleekButton();
			MenuSurvivorsClothingItemUI.useButton.positionScale_Y = 1f;
			MenuSurvivorsClothingItemUI.useButton.sizeOffset_X = -5;
			MenuSurvivorsClothingItemUI.useButton.sizeOffset_Y = 50;
			MenuSurvivorsClothingItemUI.useButton.sizeScale_X = 0.5f;
			SleekButton sleekButton = MenuSurvivorsClothingItemUI.useButton;
			if (MenuSurvivorsClothingItemUI.<>f__mg$cache0 == null)
			{
				MenuSurvivorsClothingItemUI.<>f__mg$cache0 = new ClickedButton(MenuSurvivorsClothingItemUI.onClickedUseButton);
			}
			sleekButton.onClickedButton = MenuSurvivorsClothingItemUI.<>f__mg$cache0;
			MenuSurvivorsClothingItemUI.descriptionBox.add(MenuSurvivorsClothingItemUI.useButton);
			MenuSurvivorsClothingItemUI.useButton.fontSize = 14;
			MenuSurvivorsClothingItemUI.useButton.isVisible = false;
			MenuSurvivorsClothingItemUI.inspectButton = new SleekButton();
			MenuSurvivorsClothingItemUI.inspectButton.positionOffset_X = 5;
			MenuSurvivorsClothingItemUI.inspectButton.positionScale_X = 0.5f;
			MenuSurvivorsClothingItemUI.inspectButton.positionScale_Y = 1f;
			MenuSurvivorsClothingItemUI.inspectButton.sizeOffset_X = -5;
			MenuSurvivorsClothingItemUI.inspectButton.sizeOffset_Y = 50;
			MenuSurvivorsClothingItemUI.inspectButton.sizeScale_X = 0.5f;
			MenuSurvivorsClothingItemUI.inspectButton.text = MenuSurvivorsClothingItemUI.localization.format("Inspect_Text");
			MenuSurvivorsClothingItemUI.inspectButton.tooltip = MenuSurvivorsClothingItemUI.localization.format("Inspect_Tooltip");
			SleekButton sleekButton2 = MenuSurvivorsClothingItemUI.inspectButton;
			if (MenuSurvivorsClothingItemUI.<>f__mg$cache1 == null)
			{
				MenuSurvivorsClothingItemUI.<>f__mg$cache1 = new ClickedButton(MenuSurvivorsClothingItemUI.onClickedInspectButton);
			}
			sleekButton2.onClickedButton = MenuSurvivorsClothingItemUI.<>f__mg$cache1;
			MenuSurvivorsClothingItemUI.descriptionBox.add(MenuSurvivorsClothingItemUI.inspectButton);
			MenuSurvivorsClothingItemUI.inspectButton.fontSize = 14;
			MenuSurvivorsClothingItemUI.inspectButton.isVisible = false;
			MenuSurvivorsClothingItemUI.marketButton = new SleekButton();
			MenuSurvivorsClothingItemUI.marketButton.positionScale_Y = 1f;
			MenuSurvivorsClothingItemUI.marketButton.sizeOffset_X = -5;
			MenuSurvivorsClothingItemUI.marketButton.sizeOffset_Y = 50;
			MenuSurvivorsClothingItemUI.marketButton.sizeScale_X = 0.5f;
			MenuSurvivorsClothingItemUI.marketButton.text = MenuSurvivorsClothingItemUI.localization.format("Market_Text");
			MenuSurvivorsClothingItemUI.marketButton.tooltip = MenuSurvivorsClothingItemUI.localization.format("Market_Tooltip");
			SleekButton sleekButton3 = MenuSurvivorsClothingItemUI.marketButton;
			if (MenuSurvivorsClothingItemUI.<>f__mg$cache2 == null)
			{
				MenuSurvivorsClothingItemUI.<>f__mg$cache2 = new ClickedButton(MenuSurvivorsClothingItemUI.onClickedMarketButton);
			}
			sleekButton3.onClickedButton = MenuSurvivorsClothingItemUI.<>f__mg$cache2;
			MenuSurvivorsClothingItemUI.descriptionBox.add(MenuSurvivorsClothingItemUI.marketButton);
			MenuSurvivorsClothingItemUI.marketButton.fontSize = 14;
			MenuSurvivorsClothingItemUI.marketButton.isVisible = false;
			MenuSurvivorsClothingItemUI.deleteButton = new SleekButton();
			MenuSurvivorsClothingItemUI.deleteButton.positionOffset_X = 5;
			MenuSurvivorsClothingItemUI.deleteButton.positionScale_X = 0.5f;
			MenuSurvivorsClothingItemUI.deleteButton.positionScale_Y = 1f;
			MenuSurvivorsClothingItemUI.deleteButton.sizeOffset_Y = 50;
			MenuSurvivorsClothingItemUI.deleteButton.sizeScale_X = 0.5f;
			MenuSurvivorsClothingItemUI.deleteButton.text = MenuSurvivorsClothingItemUI.localization.format("Delete_Text");
			MenuSurvivorsClothingItemUI.deleteButton.tooltip = MenuSurvivorsClothingItemUI.localization.format("Delete_Tooltip");
			SleekButton sleekButton4 = MenuSurvivorsClothingItemUI.deleteButton;
			if (MenuSurvivorsClothingItemUI.<>f__mg$cache3 == null)
			{
				MenuSurvivorsClothingItemUI.<>f__mg$cache3 = new ClickedButton(MenuSurvivorsClothingItemUI.onClickedDeleteButton);
			}
			sleekButton4.onClickedButton = MenuSurvivorsClothingItemUI.<>f__mg$cache3;
			MenuSurvivorsClothingItemUI.descriptionBox.add(MenuSurvivorsClothingItemUI.deleteButton);
			MenuSurvivorsClothingItemUI.deleteButton.fontSize = 14;
			MenuSurvivorsClothingItemUI.scrapButton = new SleekButton();
			MenuSurvivorsClothingItemUI.scrapButton.positionOffset_X = 5;
			MenuSurvivorsClothingItemUI.scrapButton.positionScale_X = 0.75f;
			MenuSurvivorsClothingItemUI.scrapButton.positionScale_Y = 1f;
			MenuSurvivorsClothingItemUI.scrapButton.sizeOffset_Y = 50;
			MenuSurvivorsClothingItemUI.scrapButton.sizeScale_X = 0.25f;
			MenuSurvivorsClothingItemUI.scrapButton.text = MenuSurvivorsClothingItemUI.localization.format("Scrap_Text");
			MenuSurvivorsClothingItemUI.scrapButton.tooltip = MenuSurvivorsClothingItemUI.localization.format("Scrap_Tooltip");
			SleekButton sleekButton5 = MenuSurvivorsClothingItemUI.scrapButton;
			if (MenuSurvivorsClothingItemUI.<>f__mg$cache4 == null)
			{
				MenuSurvivorsClothingItemUI.<>f__mg$cache4 = new ClickedButton(MenuSurvivorsClothingItemUI.onClickedScrapButton);
			}
			sleekButton5.onClickedButton = MenuSurvivorsClothingItemUI.<>f__mg$cache4;
			MenuSurvivorsClothingItemUI.descriptionBox.add(MenuSurvivorsClothingItemUI.scrapButton);
			MenuSurvivorsClothingItemUI.scrapButton.fontSize = 14;
			MenuSurvivorsClothingItemUI.scrapButton.isVisible = false;
			MenuSurvivorsClothingItemUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuSurvivorsClothingItemUI.backButton.positionOffset_Y = -50;
			MenuSurvivorsClothingItemUI.backButton.positionScale_Y = 1f;
			MenuSurvivorsClothingItemUI.backButton.sizeOffset_X = 200;
			MenuSurvivorsClothingItemUI.backButton.sizeOffset_Y = 50;
			MenuSurvivorsClothingItemUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuSurvivorsClothingItemUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton6 = MenuSurvivorsClothingItemUI.backButton;
			if (MenuSurvivorsClothingItemUI.<>f__mg$cache5 == null)
			{
				MenuSurvivorsClothingItemUI.<>f__mg$cache5 = new ClickedButton(MenuSurvivorsClothingItemUI.onClickedBackButton);
			}
			sleekButton6.onClickedButton = MenuSurvivorsClothingItemUI.<>f__mg$cache5;
			MenuSurvivorsClothingItemUI.backButton.fontSize = 14;
			MenuSurvivorsClothingItemUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuSurvivorsClothingItemUI.container.add(MenuSurvivorsClothingItemUI.backButton);
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x00181FBC File Offset: 0x001803BC
		public static void open()
		{
			if (MenuSurvivorsClothingItemUI.active)
			{
				return;
			}
			MenuSurvivorsClothingItemUI.active = true;
			MenuSurvivorsClothingItemUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x00181FE9 File Offset: 0x001803E9
		public static void close()
		{
			if (!MenuSurvivorsClothingItemUI.active)
			{
				return;
			}
			MenuSurvivorsClothingItemUI.active = false;
			MenuSurvivorsClothingItemUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x00182016 File Offset: 0x00180416
		public static void viewItem()
		{
			MenuSurvivorsClothingItemUI.viewItem(MenuSurvivorsClothingItemUI.item, MenuSurvivorsClothingItemUI.quantity, MenuSurvivorsClothingItemUI.instance);
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x0018202C File Offset: 0x0018042C
		public static void viewItem(int newItem, ushort newQuantity, ulong newInstance)
		{
			Debug.Log(string.Concat(new object[]
			{
				"View: ",
				newItem,
				" x",
				newQuantity,
				" [",
				newInstance,
				"]"
			}));
			MenuSurvivorsClothingItemUI.item = newItem;
			MenuSurvivorsClothingItemUI.quantity = newQuantity;
			MenuSurvivorsClothingItemUI.instance = newInstance;
			MenuSurvivorsClothingItemUI.packageBox.updateInventory(MenuSurvivorsClothingItemUI.instance, MenuSurvivorsClothingItemUI.item, newQuantity, false, true);
			if (MenuSurvivorsClothingItemUI.packageBox.itemAsset == null && MenuSurvivorsClothingItemUI.packageBox.vehicleAsset == null)
			{
				MenuSurvivorsClothingItemUI.useButton.isVisible = false;
				MenuSurvivorsClothingItemUI.inspectButton.isVisible = false;
				MenuSurvivorsClothingItemUI.marketButton.isVisible = false;
				MenuSurvivorsClothingItemUI.scrapButton.isVisible = false;
				MenuSurvivorsClothingItemUI.deleteButton.isVisible = true;
				MenuSurvivorsClothingItemUI.descriptionBox.sizeOffset_Y = -60;
				MenuSurvivorsClothingItemUI.deleteButton.positionOffset_Y = -MenuSurvivorsClothingItemUI.descriptionBox.sizeOffset_Y - 50;
				MenuSurvivorsClothingItemUI.deleteButton.sizeScale_X = 0.5f;
				MenuSurvivorsClothingItemUI.infoLabel.text = MenuSurvivorsClothingItemUI.localization.format("Unknown");
			}
			else
			{
				if (MenuSurvivorsClothingItemUI.packageBox.itemAsset != null && MenuSurvivorsClothingItemUI.packageBox.itemAsset.type == EItemType.KEY)
				{
					if ((MenuSurvivorsClothingItemUI.packageBox.itemAsset as ItemKeyAsset).exchangeWithTargetItem)
					{
						MenuSurvivorsClothingItemUI.useButton.isVisible = true;
						MenuSurvivorsClothingItemUI.useButton.text = MenuSurvivorsClothingItemUI.localization.format("Target_Item_Text");
						MenuSurvivorsClothingItemUI.useButton.tooltip = MenuSurvivorsClothingItemUI.localization.format("Target_Item_Tooltip");
					}
					else
					{
						MenuSurvivorsClothingItemUI.useButton.isVisible = false;
					}
					MenuSurvivorsClothingItemUI.inspectButton.isVisible = false;
				}
				else if (MenuSurvivorsClothingItemUI.packageBox.itemAsset != null && MenuSurvivorsClothingItemUI.packageBox.itemAsset.type == EItemType.BOX)
				{
					MenuSurvivorsClothingItemUI.useButton.isVisible = true;
					MenuSurvivorsClothingItemUI.inspectButton.isVisible = false;
					MenuSurvivorsClothingItemUI.useButton.text = MenuSurvivorsClothingItemUI.localization.format("Contents_Text");
					MenuSurvivorsClothingItemUI.useButton.tooltip = MenuSurvivorsClothingItemUI.localization.format("Contents_Tooltip");
				}
				else
				{
					MenuSurvivorsClothingItemUI.useButton.isVisible = true;
					MenuSurvivorsClothingItemUI.inspectButton.isVisible = true;
					bool flag;
					if (MenuSurvivorsClothingItemUI.packageBox.itemAsset == null || MenuSurvivorsClothingItemUI.packageBox.itemAsset.proPath == null || MenuSurvivorsClothingItemUI.packageBox.itemAsset.proPath.Length == 0)
					{
						flag = Characters.isSkinEquipped(MenuSurvivorsClothingItemUI.instance);
					}
					else
					{
						flag = Characters.isCosmeticEquipped(MenuSurvivorsClothingItemUI.instance);
					}
					MenuSurvivorsClothingItemUI.useButton.text = MenuSurvivorsClothingItemUI.localization.format((!flag) ? "Equip_Text" : "Dequip_Text");
					MenuSurvivorsClothingItemUI.useButton.tooltip = MenuSurvivorsClothingItemUI.localization.format((!flag) ? "Equip_Tooltip" : "Dequip_Tooltip");
				}
				MenuSurvivorsClothingItemUI.marketButton.isVisible = Provider.provider.economyService.getInventoryMarketable(MenuSurvivorsClothingItemUI.item);
				MenuSurvivorsClothingItemUI.scrapButton.isVisible = Provider.provider.economyService.getInventoryScrapable(MenuSurvivorsClothingItemUI.item);
				MenuSurvivorsClothingItemUI.descriptionBox.sizeOffset_Y = 0;
				if (MenuSurvivorsClothingItemUI.useButton.isVisible || MenuSurvivorsClothingItemUI.inspectButton.isVisible)
				{
					MenuSurvivorsClothingItemUI.descriptionBox.sizeOffset_Y -= 60;
					MenuSurvivorsClothingItemUI.useButton.positionOffset_Y = -MenuSurvivorsClothingItemUI.descriptionBox.sizeOffset_Y - 50;
					MenuSurvivorsClothingItemUI.inspectButton.positionOffset_Y = -MenuSurvivorsClothingItemUI.descriptionBox.sizeOffset_Y - 50;
				}
				if (MenuSurvivorsClothingItemUI.scrapButton.isVisible)
				{
					MenuSurvivorsClothingItemUI.deleteButton.sizeScale_X = 0.25f;
				}
				else
				{
					MenuSurvivorsClothingItemUI.deleteButton.sizeScale_X = 0.5f;
				}
				if (MenuSurvivorsClothingItemUI.marketButton.isVisible || MenuSurvivorsClothingItemUI.deleteButton.isVisible || MenuSurvivorsClothingItemUI.scrapButton.isVisible)
				{
					MenuSurvivorsClothingItemUI.descriptionBox.sizeOffset_Y -= 60;
					MenuSurvivorsClothingItemUI.marketButton.positionOffset_Y = -MenuSurvivorsClothingItemUI.descriptionBox.sizeOffset_Y - 50;
					MenuSurvivorsClothingItemUI.deleteButton.positionOffset_Y = -MenuSurvivorsClothingItemUI.descriptionBox.sizeOffset_Y - 50;
					MenuSurvivorsClothingItemUI.scrapButton.positionOffset_Y = -MenuSurvivorsClothingItemUI.descriptionBox.sizeOffset_Y - 50;
				}
				MenuSurvivorsClothingItemUI.infoLabel.text = string.Concat(new string[]
				{
					"<color=",
					Palette.hex(Provider.provider.economyService.getInventoryColor(MenuSurvivorsClothingItemUI.item)),
					">",
					Provider.provider.economyService.getInventoryType(MenuSurvivorsClothingItemUI.item),
					"</color>\n\n",
					Provider.provider.economyService.getInventoryDescription(MenuSurvivorsClothingItemUI.item)
				});
			}
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x00182500 File Offset: 0x00180900
		private static void onClickedUseButton(SleekButton button)
		{
			if (MenuSurvivorsClothingItemUI.packageBox.itemAsset != null && MenuSurvivorsClothingItemUI.packageBox.itemAsset.type == EItemType.KEY)
			{
				MenuSurvivorsClothingUI.setFilter(EEconFilterMode.STAT_TRACKER, MenuSurvivorsClothingItemUI.instance);
				MenuSurvivorsClothingUI.open();
				MenuSurvivorsClothingItemUI.close();
			}
			else if (MenuSurvivorsClothingItemUI.packageBox.itemAsset != null && MenuSurvivorsClothingItemUI.packageBox.itemAsset.type == EItemType.BOX)
			{
				MenuSurvivorsClothingBoxUI.viewItem(MenuSurvivorsClothingItemUI.item, MenuSurvivorsClothingItemUI.quantity, MenuSurvivorsClothingItemUI.instance);
				MenuSurvivorsClothingBoxUI.open();
				MenuSurvivorsClothingItemUI.close();
			}
			else
			{
				Characters.package(MenuSurvivorsClothingItemUI.instance);
				MenuSurvivorsClothingItemUI.viewItem();
			}
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x001825A3 File Offset: 0x001809A3
		private static void onClickedInspectButton(SleekButton button)
		{
			MenuSurvivorsClothingInspectUI.viewItem(MenuSurvivorsClothingItemUI.item, MenuSurvivorsClothingItemUI.instance);
			MenuSurvivorsClothingInspectUI.open();
			MenuSurvivorsClothingItemUI.close();
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x001825BE File Offset: 0x001809BE
		private static void onClickedMarketButton(SleekButton button)
		{
			if (!Provider.provider.economyService.canOpenInventory)
			{
				MenuUI.alert(MenuSurvivorsClothingItemUI.localization.format("Overlay"));
				return;
			}
			Provider.provider.economyService.open(MenuSurvivorsClothingItemUI.instance);
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x001825FD File Offset: 0x001809FD
		private static void onClickedDeleteButton(SleekButton button)
		{
			MenuSurvivorsClothingDeleteUI.viewItem(MenuSurvivorsClothingItemUI.item, MenuSurvivorsClothingItemUI.instance, EDeleteMode.DELETE, 0UL);
			MenuSurvivorsClothingDeleteUI.open();
			MenuSurvivorsClothingItemUI.close();
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x0018261C File Offset: 0x00180A1C
		private static void onClickedScrapButton(SleekButton button)
		{
			if (Provider.provider.economyService.getInventoryMythicID(MenuSurvivorsClothingItemUI.item) != 0)
			{
				MenuSurvivorsClothingDeleteUI.viewItem(MenuSurvivorsClothingItemUI.item, MenuSurvivorsClothingItemUI.instance, EDeleteMode.SALVAGE, 0UL);
				MenuSurvivorsClothingDeleteUI.open();
				MenuSurvivorsClothingItemUI.close();
			}
			else
			{
				MenuSurvivorsClothingDeleteUI.salvageItem(MenuSurvivorsClothingItemUI.item, MenuSurvivorsClothingItemUI.instance);
				MenuSurvivorsClothingItemUI.onClickedBackButton(MenuSurvivorsClothingItemUI.backButton);
			}
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x0018267C File Offset: 0x00180A7C
		private static void onClickedBackButton(SleekButton button)
		{
			MenuSurvivorsClothingUI.open();
			MenuSurvivorsClothingItemUI.close();
		}

		// Token: 0x04002897 RID: 10391
		private static Local localization;

		// Token: 0x04002898 RID: 10392
		private static Sleek container;

		// Token: 0x04002899 RID: 10393
		public static bool active;

		// Token: 0x0400289A RID: 10394
		private static SleekButtonIcon backButton;

		// Token: 0x0400289B RID: 10395
		private static int item;

		// Token: 0x0400289C RID: 10396
		private static ushort quantity;

		// Token: 0x0400289D RID: 10397
		private static ulong instance;

		// Token: 0x0400289E RID: 10398
		private static Sleek inventory;

		// Token: 0x0400289F RID: 10399
		private static SleekInventory packageBox;

		// Token: 0x040028A0 RID: 10400
		private static SleekBox descriptionBox;

		// Token: 0x040028A1 RID: 10401
		private static SleekLabel infoLabel;

		// Token: 0x040028A2 RID: 10402
		private static SleekButton useButton;

		// Token: 0x040028A3 RID: 10403
		private static SleekButton inspectButton;

		// Token: 0x040028A4 RID: 10404
		private static SleekButton marketButton;

		// Token: 0x040028A5 RID: 10405
		private static SleekButton deleteButton;

		// Token: 0x040028A6 RID: 10406
		private static SleekButton scrapButton;

		// Token: 0x040028A7 RID: 10407
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x040028A8 RID: 10408
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x040028A9 RID: 10409
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x040028AA RID: 10410
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x040028AB RID: 10411
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;

		// Token: 0x040028AC RID: 10412
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;
	}
}
