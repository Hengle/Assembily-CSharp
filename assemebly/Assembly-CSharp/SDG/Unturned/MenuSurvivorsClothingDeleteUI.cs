using System;
using System.Runtime.CompilerServices;

namespace SDG.Unturned
{
	// Token: 0x02000780 RID: 1920
	public class MenuSurvivorsClothingDeleteUI
	{
		// Token: 0x06003734 RID: 14132 RVA: 0x0018093C File Offset: 0x0017ED3C
		public MenuSurvivorsClothingDeleteUI()
		{
			MenuSurvivorsClothingDeleteUI.localization = Localization.read("/Menu/Survivors/MenuSurvivorsClothingDelete.dat");
			MenuSurvivorsClothingDeleteUI.container = new Sleek();
			MenuSurvivorsClothingDeleteUI.container.positionOffset_X = 10;
			MenuSurvivorsClothingDeleteUI.container.positionOffset_Y = 10;
			MenuSurvivorsClothingDeleteUI.container.positionScale_Y = 1f;
			MenuSurvivorsClothingDeleteUI.container.sizeOffset_X = -20;
			MenuSurvivorsClothingDeleteUI.container.sizeOffset_Y = -20;
			MenuSurvivorsClothingDeleteUI.container.sizeScale_X = 1f;
			MenuSurvivorsClothingDeleteUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuSurvivorsClothingDeleteUI.container);
			MenuSurvivorsClothingDeleteUI.active = false;
			MenuSurvivorsClothingDeleteUI.inventory = new Sleek();
			MenuSurvivorsClothingDeleteUI.inventory.positionScale_X = 0.5f;
			MenuSurvivorsClothingDeleteUI.inventory.positionOffset_Y = 10;
			MenuSurvivorsClothingDeleteUI.inventory.sizeScale_X = 0.5f;
			MenuSurvivorsClothingDeleteUI.inventory.sizeScale_Y = 1f;
			MenuSurvivorsClothingDeleteUI.inventory.sizeOffset_Y = -20;
			MenuSurvivorsClothingDeleteUI.inventory.constraint = ESleekConstraint.XY;
			MenuSurvivorsClothingDeleteUI.container.add(MenuSurvivorsClothingDeleteUI.inventory);
			MenuSurvivorsClothingDeleteUI.deleteBox = new SleekBox();
			MenuSurvivorsClothingDeleteUI.deleteBox.positionOffset_Y = -65;
			MenuSurvivorsClothingDeleteUI.deleteBox.positionScale_Y = 0.5f;
			MenuSurvivorsClothingDeleteUI.deleteBox.sizeOffset_Y = 130;
			MenuSurvivorsClothingDeleteUI.deleteBox.sizeScale_X = 1f;
			MenuSurvivorsClothingDeleteUI.inventory.add(MenuSurvivorsClothingDeleteUI.deleteBox);
			MenuSurvivorsClothingDeleteUI.intentLabel = new SleekLabel();
			MenuSurvivorsClothingDeleteUI.intentLabel.isRich = true;
			MenuSurvivorsClothingDeleteUI.intentLabel.positionOffset_X = 5;
			MenuSurvivorsClothingDeleteUI.intentLabel.positionOffset_Y = 5;
			MenuSurvivorsClothingDeleteUI.intentLabel.sizeOffset_X = -10;
			MenuSurvivorsClothingDeleteUI.intentLabel.sizeOffset_Y = 20;
			MenuSurvivorsClothingDeleteUI.intentLabel.sizeScale_X = 1f;
			MenuSurvivorsClothingDeleteUI.intentLabel.foregroundTint = ESleekTint.NONE;
			MenuSurvivorsClothingDeleteUI.deleteBox.add(MenuSurvivorsClothingDeleteUI.intentLabel);
			MenuSurvivorsClothingDeleteUI.warningLabel = new SleekLabel();
			MenuSurvivorsClothingDeleteUI.warningLabel.positionOffset_X = 5;
			MenuSurvivorsClothingDeleteUI.warningLabel.positionOffset_Y = 25;
			MenuSurvivorsClothingDeleteUI.warningLabel.sizeOffset_X = -10;
			MenuSurvivorsClothingDeleteUI.warningLabel.sizeOffset_Y = 20;
			MenuSurvivorsClothingDeleteUI.warningLabel.sizeScale_X = 1f;
			MenuSurvivorsClothingDeleteUI.warningLabel.text = MenuSurvivorsClothingDeleteUI.localization.format("Warning");
			MenuSurvivorsClothingDeleteUI.warningLabel.foregroundTint = ESleekTint.NONE;
			MenuSurvivorsClothingDeleteUI.warningLabel.foregroundColor = Palette.COLOR_O;
			MenuSurvivorsClothingDeleteUI.deleteBox.add(MenuSurvivorsClothingDeleteUI.warningLabel);
			MenuSurvivorsClothingDeleteUI.confirmLabel = new SleekLabel();
			MenuSurvivorsClothingDeleteUI.confirmLabel.positionOffset_X = 5;
			MenuSurvivorsClothingDeleteUI.confirmLabel.positionOffset_Y = 45;
			MenuSurvivorsClothingDeleteUI.confirmLabel.sizeOffset_X = -10;
			MenuSurvivorsClothingDeleteUI.confirmLabel.sizeOffset_Y = 20;
			MenuSurvivorsClothingDeleteUI.confirmLabel.sizeScale_X = 1f;
			MenuSurvivorsClothingDeleteUI.confirmLabel.foregroundTint = ESleekTint.NONE;
			MenuSurvivorsClothingDeleteUI.deleteBox.add(MenuSurvivorsClothingDeleteUI.confirmLabel);
			MenuSurvivorsClothingDeleteUI.confirmField = new SleekField();
			MenuSurvivorsClothingDeleteUI.confirmField.positionOffset_X = 5;
			MenuSurvivorsClothingDeleteUI.confirmField.positionOffset_Y = 75;
			MenuSurvivorsClothingDeleteUI.confirmField.sizeOffset_X = -150;
			MenuSurvivorsClothingDeleteUI.confirmField.sizeOffset_Y = 50;
			MenuSurvivorsClothingDeleteUI.confirmField.sizeScale_X = 1f;
			MenuSurvivorsClothingDeleteUI.confirmField.fontSize = 14;
			MenuSurvivorsClothingDeleteUI.confirmField.backgroundTint = ESleekTint.NONE;
			MenuSurvivorsClothingDeleteUI.confirmField.foregroundTint = ESleekTint.NONE;
			MenuSurvivorsClothingDeleteUI.deleteBox.add(MenuSurvivorsClothingDeleteUI.confirmField);
			MenuSurvivorsClothingDeleteUI.yesButton = new SleekButton();
			MenuSurvivorsClothingDeleteUI.yesButton.positionOffset_X = -135;
			MenuSurvivorsClothingDeleteUI.yesButton.positionOffset_Y = 75;
			MenuSurvivorsClothingDeleteUI.yesButton.positionScale_X = 1f;
			MenuSurvivorsClothingDeleteUI.yesButton.sizeOffset_X = 60;
			MenuSurvivorsClothingDeleteUI.yesButton.sizeOffset_Y = 50;
			MenuSurvivorsClothingDeleteUI.yesButton.fontSize = 14;
			MenuSurvivorsClothingDeleteUI.yesButton.backgroundTint = ESleekTint.NONE;
			MenuSurvivorsClothingDeleteUI.yesButton.foregroundTint = ESleekTint.NONE;
			MenuSurvivorsClothingDeleteUI.yesButton.text = MenuSurvivorsClothingDeleteUI.localization.format("Yes");
			SleekButton sleekButton = MenuSurvivorsClothingDeleteUI.yesButton;
			if (MenuSurvivorsClothingDeleteUI.<>f__mg$cache0 == null)
			{
				MenuSurvivorsClothingDeleteUI.<>f__mg$cache0 = new ClickedButton(MenuSurvivorsClothingDeleteUI.onClickedYesButton);
			}
			sleekButton.onClickedButton = MenuSurvivorsClothingDeleteUI.<>f__mg$cache0;
			MenuSurvivorsClothingDeleteUI.deleteBox.add(MenuSurvivorsClothingDeleteUI.yesButton);
			MenuSurvivorsClothingDeleteUI.noButton = new SleekButton();
			MenuSurvivorsClothingDeleteUI.noButton.positionOffset_X = -65;
			MenuSurvivorsClothingDeleteUI.noButton.positionOffset_Y = 75;
			MenuSurvivorsClothingDeleteUI.noButton.positionScale_X = 1f;
			MenuSurvivorsClothingDeleteUI.noButton.sizeOffset_X = 60;
			MenuSurvivorsClothingDeleteUI.noButton.sizeOffset_Y = 50;
			MenuSurvivorsClothingDeleteUI.noButton.fontSize = 14;
			MenuSurvivorsClothingDeleteUI.noButton.backgroundTint = ESleekTint.NONE;
			MenuSurvivorsClothingDeleteUI.noButton.foregroundTint = ESleekTint.NONE;
			MenuSurvivorsClothingDeleteUI.noButton.text = MenuSurvivorsClothingDeleteUI.localization.format("No");
			MenuSurvivorsClothingDeleteUI.noButton.tooltip = MenuSurvivorsClothingDeleteUI.localization.format("No_Tooltip");
			SleekButton sleekButton2 = MenuSurvivorsClothingDeleteUI.noButton;
			if (MenuSurvivorsClothingDeleteUI.<>f__mg$cache1 == null)
			{
				MenuSurvivorsClothingDeleteUI.<>f__mg$cache1 = new ClickedButton(MenuSurvivorsClothingDeleteUI.onClickedNoButton);
			}
			sleekButton2.onClickedButton = MenuSurvivorsClothingDeleteUI.<>f__mg$cache1;
			MenuSurvivorsClothingDeleteUI.deleteBox.add(MenuSurvivorsClothingDeleteUI.noButton);
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x00180DF9 File Offset: 0x0017F1F9
		public static void open()
		{
			if (MenuSurvivorsClothingDeleteUI.active)
			{
				return;
			}
			MenuSurvivorsClothingDeleteUI.active = true;
			MenuSurvivorsClothingDeleteUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x00180E26 File Offset: 0x0017F226
		public static void close()
		{
			if (!MenuSurvivorsClothingDeleteUI.active)
			{
				return;
			}
			MenuSurvivorsClothingDeleteUI.active = false;
			MenuSurvivorsClothingDeleteUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x00180E53 File Offset: 0x0017F253
		public static void salvageItem(int itemID, ulong instanceID)
		{
			Provider.provider.economyService.exchangeInventory(itemID + 1000000, new ulong[]
			{
				instanceID
			});
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x00180E75 File Offset: 0x0017F275
		public static void applyTagTool(int itemID, ulong targetID, ulong toolID)
		{
			Provider.provider.economyService.exchangeInventory(itemID, new ulong[]
			{
				targetID,
				toolID
			});
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x00180E98 File Offset: 0x0017F298
		public static void viewItem(int newItem, ulong newInstance, EDeleteMode newMode, ulong newInstigator)
		{
			MenuSurvivorsClothingDeleteUI.item = newItem;
			MenuSurvivorsClothingDeleteUI.instance = newInstance;
			MenuSurvivorsClothingDeleteUI.mode = newMode;
			MenuSurvivorsClothingDeleteUI.instigator = newInstigator;
			MenuSurvivorsClothingDeleteUI.yesButton.tooltip = MenuSurvivorsClothingDeleteUI.localization.format((MenuSurvivorsClothingDeleteUI.mode != EDeleteMode.SALVAGE) ? ((MenuSurvivorsClothingDeleteUI.mode != EDeleteMode.TAG_TOOL) ? "Yes_Delete_Tooltip" : "Yes_Tag_Tool_Tooltip") : "Yes_Salvage_Tooltip");
			if (MenuSurvivorsClothingDeleteUI.mode == EDeleteMode.TAG_TOOL)
			{
				int inventoryItem = Provider.provider.economyService.getInventoryItem(MenuSurvivorsClothingDeleteUI.instigator);
				MenuSurvivorsClothingDeleteUI.intentLabel.text = MenuSurvivorsClothingDeleteUI.localization.format("Intent_Tag_Tool", new object[]
				{
					string.Concat(new string[]
					{
						"<color=",
						Palette.hex(Provider.provider.economyService.getInventoryColor(inventoryItem)),
						">",
						Provider.provider.economyService.getInventoryName(inventoryItem),
						"</color>"
					}),
					string.Concat(new string[]
					{
						"<color=",
						Palette.hex(Provider.provider.economyService.getInventoryColor(MenuSurvivorsClothingDeleteUI.item)),
						">",
						Provider.provider.economyService.getInventoryName(MenuSurvivorsClothingDeleteUI.item),
						"</color>"
					})
				});
			}
			else
			{
				MenuSurvivorsClothingDeleteUI.intentLabel.text = MenuSurvivorsClothingDeleteUI.localization.format((MenuSurvivorsClothingDeleteUI.mode != EDeleteMode.SALVAGE) ? "Intent_Delete" : "Intent_Salvage", new object[]
				{
					string.Concat(new string[]
					{
						"<color=",
						Palette.hex(Provider.provider.economyService.getInventoryColor(MenuSurvivorsClothingDeleteUI.item)),
						">",
						Provider.provider.economyService.getInventoryName(MenuSurvivorsClothingDeleteUI.item),
						"</color>"
					})
				});
			}
			MenuSurvivorsClothingDeleteUI.confirmLabel.text = MenuSurvivorsClothingDeleteUI.localization.format("Confirm", new object[]
			{
				MenuSurvivorsClothingDeleteUI.localization.format((MenuSurvivorsClothingDeleteUI.mode != EDeleteMode.SALVAGE) ? "Delete" : "Salvage")
			});
			MenuSurvivorsClothingDeleteUI.confirmLabel.isVisible = (MenuSurvivorsClothingDeleteUI.mode != EDeleteMode.TAG_TOOL);
			MenuSurvivorsClothingDeleteUI.confirmField.hint = MenuSurvivorsClothingDeleteUI.localization.format((MenuSurvivorsClothingDeleteUI.mode != EDeleteMode.SALVAGE) ? "Delete" : "Salvage");
			MenuSurvivorsClothingDeleteUI.confirmField.text = string.Empty;
			MenuSurvivorsClothingDeleteUI.confirmField.isVisible = (MenuSurvivorsClothingDeleteUI.mode != EDeleteMode.TAG_TOOL);
			if (MenuSurvivorsClothingDeleteUI.mode == EDeleteMode.TAG_TOOL)
			{
				MenuSurvivorsClothingDeleteUI.yesButton.positionOffset_X = -65;
				MenuSurvivorsClothingDeleteUI.yesButton.positionScale_X = 0.5f;
				MenuSurvivorsClothingDeleteUI.noButton.positionOffset_X = 5;
				MenuSurvivorsClothingDeleteUI.noButton.positionScale_X = 0.5f;
			}
			else
			{
				MenuSurvivorsClothingDeleteUI.yesButton.positionOffset_X = -135;
				MenuSurvivorsClothingDeleteUI.yesButton.positionScale_X = 1f;
				MenuSurvivorsClothingDeleteUI.noButton.positionOffset_X = -65;
				MenuSurvivorsClothingDeleteUI.noButton.positionScale_X = 1f;
			}
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x001811B8 File Offset: 0x0017F5B8
		private static void onClickedYesButton(SleekButton button)
		{
			if (MenuSurvivorsClothingDeleteUI.mode == EDeleteMode.SALVAGE)
			{
				if (MenuSurvivorsClothingDeleteUI.confirmField.text != MenuSurvivorsClothingDeleteUI.localization.format("Salvage"))
				{
					return;
				}
				MenuSurvivorsClothingDeleteUI.salvageItem(MenuSurvivorsClothingDeleteUI.item, MenuSurvivorsClothingDeleteUI.instance);
			}
			else if (MenuSurvivorsClothingDeleteUI.mode == EDeleteMode.DELETE)
			{
				if (MenuSurvivorsClothingDeleteUI.confirmField.text != MenuSurvivorsClothingDeleteUI.localization.format("Delete"))
				{
					return;
				}
				Provider.provider.economyService.consumeItem(MenuSurvivorsClothingDeleteUI.instance);
				Provider.provider.economyService.refreshInventory();
			}
			MenuSurvivorsClothingUI.open();
			MenuSurvivorsClothingDeleteUI.close();
			if (MenuSurvivorsClothingDeleteUI.mode == EDeleteMode.TAG_TOOL)
			{
				MenuSurvivorsClothingUI.prepareForCraftResult();
				MenuSurvivorsClothingDeleteUI.applyTagTool(MenuSurvivorsClothingDeleteUI.item, MenuSurvivorsClothingDeleteUI.instance, MenuSurvivorsClothingDeleteUI.instigator);
			}
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x00181287 File Offset: 0x0017F687
		private static void onClickedNoButton(SleekButton button)
		{
			MenuSurvivorsClothingItemUI.open();
			MenuSurvivorsClothingDeleteUI.close();
		}

		// Token: 0x04002876 RID: 10358
		private static Local localization;

		// Token: 0x04002877 RID: 10359
		private static Sleek container;

		// Token: 0x04002878 RID: 10360
		public static bool active;

		// Token: 0x04002879 RID: 10361
		private static int item;

		// Token: 0x0400287A RID: 10362
		private static ulong instance;

		// Token: 0x0400287B RID: 10363
		private static EDeleteMode mode;

		// Token: 0x0400287C RID: 10364
		private static ulong instigator;

		// Token: 0x0400287D RID: 10365
		private static Sleek inventory;

		// Token: 0x0400287E RID: 10366
		private static SleekBox deleteBox;

		// Token: 0x0400287F RID: 10367
		private static SleekLabel intentLabel;

		// Token: 0x04002880 RID: 10368
		private static SleekLabel warningLabel;

		// Token: 0x04002881 RID: 10369
		private static SleekLabel confirmLabel;

		// Token: 0x04002882 RID: 10370
		private static SleekField confirmField;

		// Token: 0x04002883 RID: 10371
		private static SleekButton yesButton;

		// Token: 0x04002884 RID: 10372
		private static SleekButton noButton;

		// Token: 0x04002885 RID: 10373
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x04002886 RID: 10374
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;
	}
}
