using System;
using System.Runtime.CompilerServices;
using SDG.Provider;
using SDG.SteamworksProvider.Services.Economy;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200077E RID: 1918
	public class MenuSurvivorsClothingBoxUI
	{
		// Token: 0x0600372B RID: 14123 RVA: 0x0017F678 File Offset: 0x0017DA78
		public MenuSurvivorsClothingBoxUI()
		{
			MenuSurvivorsClothingBoxUI.localization = Localization.read("/Menu/Survivors/MenuSurvivorsClothingBox.dat");
			if (MenuSurvivorsClothingBoxUI.icons != null)
			{
				MenuSurvivorsClothingBoxUI.icons.unload();
			}
			MenuSurvivorsClothingBoxUI.icons = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Survivors/MenuSurvivorsClothingBox/MenuSurvivorsClothingBox.unity3d");
			MenuSurvivorsClothingBoxUI.container = new Sleek();
			MenuSurvivorsClothingBoxUI.container.positionOffset_X = 10;
			MenuSurvivorsClothingBoxUI.container.positionOffset_Y = 10;
			MenuSurvivorsClothingBoxUI.container.positionScale_Y = 1f;
			MenuSurvivorsClothingBoxUI.container.sizeOffset_X = -20;
			MenuSurvivorsClothingBoxUI.container.sizeOffset_Y = -20;
			MenuSurvivorsClothingBoxUI.container.sizeScale_X = 1f;
			MenuSurvivorsClothingBoxUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuSurvivorsClothingBoxUI.container);
			MenuSurvivorsClothingBoxUI.active = false;
			MenuSurvivorsClothingBoxUI.inventory = new Sleek();
			MenuSurvivorsClothingBoxUI.inventory.positionScale_X = 0.5f;
			MenuSurvivorsClothingBoxUI.inventory.positionOffset_Y = 10;
			MenuSurvivorsClothingBoxUI.inventory.sizeScale_X = 0.5f;
			MenuSurvivorsClothingBoxUI.inventory.sizeScale_Y = 1f;
			MenuSurvivorsClothingBoxUI.inventory.sizeOffset_Y = -20;
			MenuSurvivorsClothingBoxUI.inventory.constraint = ESleekConstraint.XY;
			MenuSurvivorsClothingBoxUI.container.add(MenuSurvivorsClothingBoxUI.inventory);
			MenuSurvivorsClothingBoxUI.finalBox = new SleekBox();
			MenuSurvivorsClothingBoxUI.finalBox.positionOffset_X = -10;
			MenuSurvivorsClothingBoxUI.finalBox.positionOffset_Y = -10;
			MenuSurvivorsClothingBoxUI.finalBox.sizeOffset_X = 20;
			MenuSurvivorsClothingBoxUI.finalBox.sizeOffset_Y = 20;
			MenuSurvivorsClothingBoxUI.inventory.add(MenuSurvivorsClothingBoxUI.finalBox);
			MenuSurvivorsClothingBoxUI.boxButton = new SleekInventory();
			MenuSurvivorsClothingBoxUI.boxButton.positionOffset_Y = -30;
			MenuSurvivorsClothingBoxUI.boxButton.positionScale_X = 0.3f;
			MenuSurvivorsClothingBoxUI.boxButton.positionScale_Y = 0.3f;
			MenuSurvivorsClothingBoxUI.boxButton.sizeScale_X = 0.4f;
			MenuSurvivorsClothingBoxUI.boxButton.sizeScale_Y = 0.4f;
			MenuSurvivorsClothingBoxUI.inventory.add(MenuSurvivorsClothingBoxUI.boxButton);
			MenuSurvivorsClothingBoxUI.keyButton = new SleekButtonIcon(null, 40);
			MenuSurvivorsClothingBoxUI.keyButton.positionOffset_Y = -20;
			MenuSurvivorsClothingBoxUI.keyButton.positionScale_X = 0.3f;
			MenuSurvivorsClothingBoxUI.keyButton.positionScale_Y = 0.7f;
			MenuSurvivorsClothingBoxUI.keyButton.sizeOffset_X = -5;
			MenuSurvivorsClothingBoxUI.keyButton.sizeOffset_Y = 50;
			MenuSurvivorsClothingBoxUI.keyButton.sizeScale_X = 0.2f;
			MenuSurvivorsClothingBoxUI.keyButton.text = MenuSurvivorsClothingBoxUI.localization.format("Key_Text");
			MenuSurvivorsClothingBoxUI.keyButton.tooltip = MenuSurvivorsClothingBoxUI.localization.format("Key_Tooltip");
			SleekButton sleekButton = MenuSurvivorsClothingBoxUI.keyButton;
			if (MenuSurvivorsClothingBoxUI.<>f__mg$cache0 == null)
			{
				MenuSurvivorsClothingBoxUI.<>f__mg$cache0 = new ClickedButton(MenuSurvivorsClothingBoxUI.onClickedKeyButton);
			}
			sleekButton.onClickedButton = MenuSurvivorsClothingBoxUI.<>f__mg$cache0;
			MenuSurvivorsClothingBoxUI.keyButton.fontSize = 14;
			MenuSurvivorsClothingBoxUI.inventory.add(MenuSurvivorsClothingBoxUI.keyButton);
			MenuSurvivorsClothingBoxUI.keyButton.isVisible = false;
			MenuSurvivorsClothingBoxUI.unboxButton = new SleekButtonIcon(null);
			MenuSurvivorsClothingBoxUI.unboxButton.positionOffset_X = 5;
			MenuSurvivorsClothingBoxUI.unboxButton.positionOffset_Y = -20;
			MenuSurvivorsClothingBoxUI.unboxButton.positionScale_X = 0.5f;
			MenuSurvivorsClothingBoxUI.unboxButton.positionScale_Y = 0.7f;
			MenuSurvivorsClothingBoxUI.unboxButton.sizeOffset_X = -5;
			MenuSurvivorsClothingBoxUI.unboxButton.sizeOffset_Y = 50;
			MenuSurvivorsClothingBoxUI.unboxButton.sizeScale_X = 0.2f;
			MenuSurvivorsClothingBoxUI.unboxButton.text = MenuSurvivorsClothingBoxUI.localization.format("Unbox_Text");
			MenuSurvivorsClothingBoxUI.unboxButton.tooltip = MenuSurvivorsClothingBoxUI.localization.format("Unbox_Tooltip");
			SleekButton sleekButton2 = MenuSurvivorsClothingBoxUI.unboxButton;
			if (MenuSurvivorsClothingBoxUI.<>f__mg$cache1 == null)
			{
				MenuSurvivorsClothingBoxUI.<>f__mg$cache1 = new ClickedButton(MenuSurvivorsClothingBoxUI.onClickedUnboxButton);
			}
			sleekButton2.onClickedButton = MenuSurvivorsClothingBoxUI.<>f__mg$cache1;
			MenuSurvivorsClothingBoxUI.unboxButton.fontSize = 14;
			MenuSurvivorsClothingBoxUI.inventory.add(MenuSurvivorsClothingBoxUI.unboxButton);
			MenuSurvivorsClothingBoxUI.unboxButton.isVisible = false;
			MenuSurvivorsClothingBoxUI.rareLabel = new SleekLabel();
			MenuSurvivorsClothingBoxUI.rareLabel.positionOffset_X = 50;
			MenuSurvivorsClothingBoxUI.rareLabel.positionOffset_Y = 50;
			MenuSurvivorsClothingBoxUI.rareLabel.sizeOffset_X = 200;
			MenuSurvivorsClothingBoxUI.rareLabel.sizeOffset_Y = 30;
			MenuSurvivorsClothingBoxUI.rareLabel.text = MenuSurvivorsClothingBoxUI.localization.format("Rarity_Rare", new object[]
			{
				"75.0"
			});
			MenuSurvivorsClothingBoxUI.rareLabel.foregroundTint = ESleekTint.NONE;
			MenuSurvivorsClothingBoxUI.rareLabel.foregroundColor = ItemTool.getRarityColorUI(EItemRarity.RARE);
			MenuSurvivorsClothingBoxUI.rareLabel.fontAlignment = TextAnchor.MiddleLeft;
			MenuSurvivorsClothingBoxUI.container.add(MenuSurvivorsClothingBoxUI.rareLabel);
			MenuSurvivorsClothingBoxUI.epicLabel = new SleekLabel();
			MenuSurvivorsClothingBoxUI.epicLabel.positionOffset_X = 50;
			MenuSurvivorsClothingBoxUI.epicLabel.positionOffset_Y = 70;
			MenuSurvivorsClothingBoxUI.epicLabel.sizeOffset_X = 200;
			MenuSurvivorsClothingBoxUI.epicLabel.sizeOffset_Y = 30;
			MenuSurvivorsClothingBoxUI.epicLabel.text = MenuSurvivorsClothingBoxUI.localization.format("Rarity_Epic", new object[]
			{
				"20.0"
			});
			MenuSurvivorsClothingBoxUI.epicLabel.foregroundTint = ESleekTint.NONE;
			MenuSurvivorsClothingBoxUI.epicLabel.foregroundColor = ItemTool.getRarityColorUI(EItemRarity.EPIC);
			MenuSurvivorsClothingBoxUI.epicLabel.fontAlignment = TextAnchor.MiddleLeft;
			MenuSurvivorsClothingBoxUI.container.add(MenuSurvivorsClothingBoxUI.epicLabel);
			MenuSurvivorsClothingBoxUI.legendaryLabel = new SleekLabel();
			MenuSurvivorsClothingBoxUI.legendaryLabel.positionOffset_X = 50;
			MenuSurvivorsClothingBoxUI.legendaryLabel.positionOffset_Y = 90;
			MenuSurvivorsClothingBoxUI.legendaryLabel.sizeOffset_X = 200;
			MenuSurvivorsClothingBoxUI.legendaryLabel.sizeOffset_Y = 30;
			MenuSurvivorsClothingBoxUI.legendaryLabel.text = MenuSurvivorsClothingBoxUI.localization.format("Rarity_Legendary", new object[]
			{
				"5.0"
			});
			MenuSurvivorsClothingBoxUI.legendaryLabel.foregroundTint = ESleekTint.NONE;
			MenuSurvivorsClothingBoxUI.legendaryLabel.foregroundColor = ItemTool.getRarityColorUI(EItemRarity.LEGENDARY);
			MenuSurvivorsClothingBoxUI.legendaryLabel.fontAlignment = TextAnchor.MiddleLeft;
			MenuSurvivorsClothingBoxUI.container.add(MenuSurvivorsClothingBoxUI.legendaryLabel);
			MenuSurvivorsClothingBoxUI.mythicalLabel = new SleekLabel();
			MenuSurvivorsClothingBoxUI.mythicalLabel.positionOffset_X = 50;
			MenuSurvivorsClothingBoxUI.mythicalLabel.positionOffset_Y = 110;
			MenuSurvivorsClothingBoxUI.mythicalLabel.sizeOffset_X = 200;
			MenuSurvivorsClothingBoxUI.mythicalLabel.sizeOffset_Y = 30;
			MenuSurvivorsClothingBoxUI.mythicalLabel.text = MenuSurvivorsClothingBoxUI.localization.format("Rarity_Mythical", new object[]
			{
				"3.0"
			});
			MenuSurvivorsClothingBoxUI.mythicalLabel.foregroundTint = ESleekTint.NONE;
			MenuSurvivorsClothingBoxUI.mythicalLabel.foregroundColor = ItemTool.getRarityColorUI(EItemRarity.MYTHICAL);
			MenuSurvivorsClothingBoxUI.mythicalLabel.fontAlignment = TextAnchor.MiddleLeft;
			MenuSurvivorsClothingBoxUI.container.add(MenuSurvivorsClothingBoxUI.mythicalLabel);
			if (!MenuSurvivorsClothingBoxUI.hasLoaded)
			{
				TempSteamworksEconomy economyService = Provider.provider.economyService;
				Delegate onInventoryExchanged = economyService.onInventoryExchanged;
				if (MenuSurvivorsClothingBoxUI.<>f__mg$cache2 == null)
				{
					MenuSurvivorsClothingBoxUI.<>f__mg$cache2 = new TempSteamworksEconomy.InventoryExchanged(MenuSurvivorsClothingBoxUI.onInventoryExchanged);
				}
				economyService.onInventoryExchanged = (TempSteamworksEconomy.InventoryExchanged)Delegate.Combine(onInventoryExchanged, MenuSurvivorsClothingBoxUI.<>f__mg$cache2);
			}
			MenuSurvivorsClothingBoxUI.hasLoaded = true;
			MenuSurvivorsClothingBoxUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuSurvivorsClothingBoxUI.backButton.positionOffset_Y = -50;
			MenuSurvivorsClothingBoxUI.backButton.positionScale_Y = 1f;
			MenuSurvivorsClothingBoxUI.backButton.sizeOffset_X = 200;
			MenuSurvivorsClothingBoxUI.backButton.sizeOffset_Y = 50;
			MenuSurvivorsClothingBoxUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuSurvivorsClothingBoxUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton3 = MenuSurvivorsClothingBoxUI.backButton;
			if (MenuSurvivorsClothingBoxUI.<>f__mg$cache3 == null)
			{
				MenuSurvivorsClothingBoxUI.<>f__mg$cache3 = new ClickedButton(MenuSurvivorsClothingBoxUI.onClickedBackButton);
			}
			sleekButton3.onClickedButton = MenuSurvivorsClothingBoxUI.<>f__mg$cache3;
			MenuSurvivorsClothingBoxUI.backButton.fontSize = 14;
			MenuSurvivorsClothingBoxUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuSurvivorsClothingBoxUI.container.add(MenuSurvivorsClothingBoxUI.backButton);
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x0017FD97 File Offset: 0x0017E197
		public static void open()
		{
			if (MenuSurvivorsClothingBoxUI.active)
			{
				return;
			}
			MenuSurvivorsClothingBoxUI.active = true;
			MenuSurvivorsClothingBoxUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x0017FDC4 File Offset: 0x0017E1C4
		public static void close()
		{
			if (!MenuSurvivorsClothingBoxUI.active)
			{
				return;
			}
			MenuSurvivorsClothingBoxUI.active = false;
			MenuSurvivorsClothingBoxUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x0017FDF4 File Offset: 0x0017E1F4
		public static void viewItem(int newItem, ushort newQuantity, ulong newInstance)
		{
			MenuSurvivorsClothingBoxUI.item = newItem;
			MenuSurvivorsClothingBoxUI.instance = newInstance;
			MenuSurvivorsClothingBoxUI.drop = -1;
			MenuSurvivorsClothingBoxUI.isMythical = false;
			MenuSurvivorsClothingBoxUI.angle = 0f;
			MenuSurvivorsClothingBoxUI.lastRotation = 0;
			MenuSurvivorsClothingBoxUI.rotation = 0;
			MenuSurvivorsClothingBoxUI.target = -1;
			MenuSurvivorsClothingBoxUI.keyButton.isVisible = true;
			MenuSurvivorsClothingBoxUI.unboxButton.isVisible = true;
			MenuSurvivorsClothingBoxUI.boxButton.updateInventory(MenuSurvivorsClothingBoxUI.instance, MenuSurvivorsClothingBoxUI.item, newQuantity, false, true);
			MenuSurvivorsClothingBoxUI.boxAsset = (ItemBoxAsset)Assets.find(EAssetType.ITEM, Provider.provider.economyService.getInventoryItemID(MenuSurvivorsClothingBoxUI.item));
			if (MenuSurvivorsClothingBoxUI.boxAsset != null)
			{
				if (MenuSurvivorsClothingBoxUI.boxAsset.destroy == 0)
				{
					MenuSurvivorsClothingBoxUI.keyButton.isVisible = false;
					MenuSurvivorsClothingBoxUI.unboxButton.icon = null;
					MenuSurvivorsClothingBoxUI.unboxButton.positionOffset_X = 0;
					MenuSurvivorsClothingBoxUI.unboxButton.positionScale_X = 0.3f;
					MenuSurvivorsClothingBoxUI.unboxButton.sizeOffset_X = 0;
					MenuSurvivorsClothingBoxUI.unboxButton.sizeScale_X = 0.4f;
					MenuSurvivorsClothingBoxUI.unboxButton.text = MenuSurvivorsClothingBoxUI.localization.format("Unwrap_Text");
					MenuSurvivorsClothingBoxUI.unboxButton.tooltip = MenuSurvivorsClothingBoxUI.localization.format("Unwrap_Tooltip");
					MenuSurvivorsClothingBoxUI.unboxButton.isVisible = true;
					MenuSurvivorsClothingBoxUI.keyAsset = null;
				}
				else
				{
					MenuSurvivorsClothingBoxUI.keyButton.isVisible = true;
					MenuSurvivorsClothingBoxUI.unboxButton.icon = (Texture2D)MenuSurvivorsClothingBoxUI.icons.load("Unbox");
					MenuSurvivorsClothingBoxUI.unboxButton.positionOffset_X = 5;
					MenuSurvivorsClothingBoxUI.unboxButton.positionScale_X = 0.5f;
					MenuSurvivorsClothingBoxUI.unboxButton.sizeOffset_X = -5;
					MenuSurvivorsClothingBoxUI.unboxButton.sizeScale_X = 0.2f;
					MenuSurvivorsClothingBoxUI.unboxButton.text = MenuSurvivorsClothingBoxUI.localization.format("Unbox_Text");
					MenuSurvivorsClothingBoxUI.unboxButton.tooltip = MenuSurvivorsClothingBoxUI.localization.format("Unbox_Tooltip");
					MenuSurvivorsClothingBoxUI.unboxButton.isVisible = true;
					MenuSurvivorsClothingBoxUI.keyAsset = (ItemKeyAsset)Assets.find(EAssetType.ITEM, Provider.provider.economyService.getInventoryItemID(MenuSurvivorsClothingBoxUI.boxAsset.destroy));
					if (MenuSurvivorsClothingBoxUI.keyAsset != null)
					{
						MenuSurvivorsClothingBoxUI.keyButton.icon = (Texture2D)Resources.Load("Economy" + MenuSurvivorsClothingBoxUI.keyAsset.proPath + "/Icon_Small");
					}
				}
				MenuSurvivorsClothingBoxUI.size = 6.28318548f / (float)MenuSurvivorsClothingBoxUI.boxAsset.drops.Length / 2.75f;
				MenuSurvivorsClothingBoxUI.finalBox.positionScale_Y = 0.5f - MenuSurvivorsClothingBoxUI.size / 2f;
				MenuSurvivorsClothingBoxUI.finalBox.sizeScale_X = MenuSurvivorsClothingBoxUI.size;
				MenuSurvivorsClothingBoxUI.finalBox.sizeScale_Y = MenuSurvivorsClothingBoxUI.size;
				if (MenuSurvivorsClothingBoxUI.dropButtons != null)
				{
					for (int i = 0; i < MenuSurvivorsClothingBoxUI.dropButtons.Length; i++)
					{
						MenuSurvivorsClothingBoxUI.inventory.remove(MenuSurvivorsClothingBoxUI.dropButtons[i]);
					}
				}
				MenuSurvivorsClothingBoxUI.dropButtons = new SleekInventory[MenuSurvivorsClothingBoxUI.boxAsset.drops.Length];
				for (int j = 0; j < MenuSurvivorsClothingBoxUI.boxAsset.drops.Length; j++)
				{
					float num = 6.28318548f * (float)j / (float)MenuSurvivorsClothingBoxUI.boxAsset.drops.Length + 3.14159274f;
					SleekInventory sleekInventory = new SleekInventory();
					sleekInventory.positionScale_X = 0.5f + Mathf.Cos(-num) * (0.5f - MenuSurvivorsClothingBoxUI.size / 2f) - MenuSurvivorsClothingBoxUI.size / 2f;
					sleekInventory.positionScale_Y = 0.5f + Mathf.Sin(-num) * (0.5f - MenuSurvivorsClothingBoxUI.size / 2f) - MenuSurvivorsClothingBoxUI.size / 2f;
					sleekInventory.sizeScale_X = MenuSurvivorsClothingBoxUI.size;
					sleekInventory.sizeScale_Y = MenuSurvivorsClothingBoxUI.size;
					sleekInventory.updateInventory(0UL, MenuSurvivorsClothingBoxUI.boxAsset.drops[j], 1, false, false);
					MenuSurvivorsClothingBoxUI.inventory.add(sleekInventory);
					MenuSurvivorsClothingBoxUI.dropButtons[j] = sleekInventory;
				}
			}
			MenuSurvivorsClothingBoxUI.keyButton.backgroundColor = Provider.provider.economyService.getInventoryColor(MenuSurvivorsClothingBoxUI.item);
			MenuSurvivorsClothingBoxUI.keyButton.foregroundColor = MenuSurvivorsClothingBoxUI.keyButton.backgroundColor;
			MenuSurvivorsClothingBoxUI.unboxButton.backgroundColor = MenuSurvivorsClothingBoxUI.keyButton.backgroundColor;
			MenuSurvivorsClothingBoxUI.unboxButton.foregroundColor = MenuSurvivorsClothingBoxUI.keyButton.backgroundColor;
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x00180208 File Offset: 0x0017E608
		private static void onClickedKeyButton(SleekButton button)
		{
			if (!Provider.provider.storeService.canOpenStore)
			{
				MenuUI.alert(MenuSurvivorsClothingBoxUI.localization.format("Overlay"));
				return;
			}
			Provider.provider.storeService.open(new SteamworksEconomyItemDefinition((SteamItemDef_t)MenuSurvivorsClothingBoxUI.boxAsset.destroy));
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x00180264 File Offset: 0x0017E664
		private static void onClickedUnboxButton(SleekButton button)
		{
			if (MenuSurvivorsClothingBoxUI.boxAsset.destroy == 0)
			{
				Provider.provider.economyService.exchangeInventory(MenuSurvivorsClothingBoxUI.boxAsset.generate, new ulong[]
				{
					MenuSurvivorsClothingBoxUI.instance
				});
			}
			else
			{
				ulong inventoryPackage = Provider.provider.economyService.getInventoryPackage(MenuSurvivorsClothingBoxUI.boxAsset.destroy);
				if (inventoryPackage == 0UL)
				{
					return;
				}
				Provider.provider.economyService.exchangeInventory(MenuSurvivorsClothingBoxUI.boxAsset.generate, new ulong[]
				{
					MenuSurvivorsClothingBoxUI.instance,
					inventoryPackage
				});
			}
			MenuSurvivorsClothingBoxUI.isUnboxing = true;
			MenuSurvivorsClothingBoxUI.backButton.isVisible = false;
			MenuSurvivorsClothingBoxUI.lastUnbox = Time.realtimeSinceStartup;
			MenuSurvivorsClothingBoxUI.lastAngle = Time.realtimeSinceStartup;
			MenuSurvivorsClothingBoxUI.keyButton.isVisible = false;
			MenuSurvivorsClothingBoxUI.unboxButton.isVisible = false;
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x00180338 File Offset: 0x0017E738
		private static void onInventoryExchanged(int newItem, ushort newQuantity, ulong newInstance)
		{
			if (!MenuSurvivorsClothingBoxUI.isUnboxing)
			{
				return;
			}
			MenuSurvivorsClothingBoxUI.drop = newItem;
			MenuSurvivorsClothingBoxUI.got = newInstance;
			ushort inventoryItemID = Provider.provider.economyService.getInventoryItemID(MenuSurvivorsClothingBoxUI.drop);
			if ((ItemAsset)Assets.find(EAssetType.ITEM, inventoryItemID) == null)
			{
				MenuSurvivorsClothingBoxUI.isUnboxing = false;
				MenuSurvivorsClothingBoxUI.backButton.isVisible = true;
				MenuUI.alert(MenuSurvivorsClothingBoxUI.localization.format("Exchange_Unknown"));
				MenuSurvivorsClothingUI.open();
				MenuSurvivorsClothingBoxUI.close();
				return;
			}
			MenuSurvivorsClothingUI.updatePage();
			int num = 0;
			MenuSurvivorsClothingBoxUI.isMythical = true;
			for (int i = 1; i < MenuSurvivorsClothingBoxUI.boxAsset.drops.Length; i++)
			{
				if (MenuSurvivorsClothingBoxUI.drop == MenuSurvivorsClothingBoxUI.boxAsset.drops[i])
				{
					num = i;
					MenuSurvivorsClothingBoxUI.isMythical = false;
					break;
				}
			}
			if (MenuSurvivorsClothingBoxUI.isMythical && Provider.provider.economyService.getInventoryMythicID(MenuSurvivorsClothingBoxUI.drop) == 0)
			{
				MenuSurvivorsClothingBoxUI.isUnboxing = false;
				MenuSurvivorsClothingBoxUI.backButton.isVisible = true;
				MenuUI.alert(MenuSurvivorsClothingBoxUI.localization.format("Exchange_Mythic"));
				MenuSurvivorsClothingUI.open();
				MenuSurvivorsClothingBoxUI.close();
				return;
			}
			if (MenuSurvivorsClothingBoxUI.rotation < MenuSurvivorsClothingBoxUI.boxAsset.drops.Length * 2)
			{
				MenuSurvivorsClothingBoxUI.target = MenuSurvivorsClothingBoxUI.boxAsset.drops.Length * 3 + num;
			}
			else
			{
				MenuSurvivorsClothingBoxUI.target = ((int)((float)MenuSurvivorsClothingBoxUI.rotation / (float)MenuSurvivorsClothingBoxUI.boxAsset.drops.Length) + 2) * MenuSurvivorsClothingBoxUI.boxAsset.drops.Length + num;
			}
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x001804B8 File Offset: 0x0017E8B8
		public static void update()
		{
			if (!MenuSurvivorsClothingBoxUI.isUnboxing)
			{
				return;
			}
			if (Time.realtimeSinceStartup - MenuSurvivorsClothingBoxUI.lastUnbox > (float)Provider.CLIENT_TIMEOUT)
			{
				MenuSurvivorsClothingBoxUI.isUnboxing = false;
				MenuSurvivorsClothingBoxUI.backButton.isVisible = true;
				MenuUI.alert(MenuSurvivorsClothingBoxUI.localization.format("Exchange_Timed_Out"));
				MenuSurvivorsClothingUI.open();
				MenuSurvivorsClothingBoxUI.close();
				return;
			}
			if (MenuSurvivorsClothingBoxUI.rotation == MenuSurvivorsClothingBoxUI.target)
			{
				if (Time.realtimeSinceStartup - MenuSurvivorsClothingBoxUI.lastAngle > 0.5f)
				{
					MenuSurvivorsClothingBoxUI.isUnboxing = false;
					MenuSurvivorsClothingBoxUI.backButton.isVisible = true;
					if (MenuSurvivorsClothingBoxUI.boxAsset.destroy == 0)
					{
						MenuUI.alert(MenuSurvivorsClothingBoxUI.localization.format("Origin_Unwrap"), MenuSurvivorsClothingBoxUI.got, MenuSurvivorsClothingBoxUI.drop, 1);
					}
					else
					{
						MenuUI.alert(MenuSurvivorsClothingBoxUI.localization.format("Origin_Unbox"), MenuSurvivorsClothingBoxUI.got, MenuSurvivorsClothingBoxUI.drop, 1);
					}
					MenuSurvivorsClothingItemUI.viewItem(MenuSurvivorsClothingBoxUI.drop, 1, MenuSurvivorsClothingBoxUI.got);
					MenuSurvivorsClothingItemUI.open();
					MenuSurvivorsClothingBoxUI.close();
					if (MenuSurvivorsClothingBoxUI.isMythical)
					{
						MainCamera.instance.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Economy/Sounds/Mythical"), 0.66f);
					}
					else
					{
						MainCamera.instance.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Economy/Sounds/Unbox"), 0.66f);
					}
				}
			}
			else
			{
				if (MenuSurvivorsClothingBoxUI.rotation < MenuSurvivorsClothingBoxUI.target - MenuSurvivorsClothingBoxUI.boxAsset.drops.Length || MenuSurvivorsClothingBoxUI.target == -1)
				{
					if (MenuSurvivorsClothingBoxUI.angle < 12.566371f)
					{
						MenuSurvivorsClothingBoxUI.angle += (Time.realtimeSinceStartup - MenuSurvivorsClothingBoxUI.lastAngle) * MenuSurvivorsClothingBoxUI.size * Mathf.Lerp(80f, 20f, MenuSurvivorsClothingBoxUI.angle / 12.566371f);
					}
					else
					{
						MenuSurvivorsClothingBoxUI.angle += (Time.realtimeSinceStartup - MenuSurvivorsClothingBoxUI.lastAngle) * MenuSurvivorsClothingBoxUI.size * 20f;
					}
				}
				else
				{
					MenuSurvivorsClothingBoxUI.angle += (Time.realtimeSinceStartup - MenuSurvivorsClothingBoxUI.lastAngle) * Mathf.Max(((float)MenuSurvivorsClothingBoxUI.target - MenuSurvivorsClothingBoxUI.angle / (6.28318548f / (float)MenuSurvivorsClothingBoxUI.boxAsset.drops.Length)) / (float)MenuSurvivorsClothingBoxUI.boxAsset.drops.Length, 0.05f) * MenuSurvivorsClothingBoxUI.size * 20f;
				}
				MenuSurvivorsClothingBoxUI.lastAngle = Time.realtimeSinceStartup;
				MenuSurvivorsClothingBoxUI.rotation = (int)(MenuSurvivorsClothingBoxUI.angle / (6.28318548f / (float)MenuSurvivorsClothingBoxUI.boxAsset.drops.Length));
				if (MenuSurvivorsClothingBoxUI.rotation == MenuSurvivorsClothingBoxUI.target)
				{
					MenuSurvivorsClothingBoxUI.angle = (float)MenuSurvivorsClothingBoxUI.rotation * (6.28318548f / (float)MenuSurvivorsClothingBoxUI.boxAsset.drops.Length);
				}
				for (int i = 0; i < MenuSurvivorsClothingBoxUI.boxAsset.drops.Length; i++)
				{
					float num = 6.28318548f * (float)i / (float)MenuSurvivorsClothingBoxUI.boxAsset.drops.Length + 3.14159274f;
					MenuSurvivorsClothingBoxUI.dropButtons[i].positionScale_X = 0.5f + Mathf.Cos(MenuSurvivorsClothingBoxUI.angle - num) * (0.5f - MenuSurvivorsClothingBoxUI.size / 2f) - MenuSurvivorsClothingBoxUI.size / 2f;
					MenuSurvivorsClothingBoxUI.dropButtons[i].positionScale_Y = 0.5f + Mathf.Sin(MenuSurvivorsClothingBoxUI.angle - num) * (0.5f - MenuSurvivorsClothingBoxUI.size / 2f) - MenuSurvivorsClothingBoxUI.size / 2f;
				}
				if (MenuSurvivorsClothingBoxUI.rotation != MenuSurvivorsClothingBoxUI.lastRotation)
				{
					MenuSurvivorsClothingBoxUI.lastRotation = MenuSurvivorsClothingBoxUI.rotation;
					MenuSurvivorsClothingBoxUI.boxButton.positionScale_Y = 0.25f;
					MenuSurvivorsClothingBoxUI.boxButton.lerpPositionScale(0.3f, 0.3f, ESleekLerp.EXPONENTIAL, 20f);
					MenuSurvivorsClothingBoxUI.finalBox.positionOffset_X = -20;
					MenuSurvivorsClothingBoxUI.finalBox.positionOffset_Y = -20;
					MenuSurvivorsClothingBoxUI.finalBox.sizeOffset_X = 40;
					MenuSurvivorsClothingBoxUI.finalBox.sizeOffset_Y = 40;
					MenuSurvivorsClothingBoxUI.finalBox.lerpPositionOffset(-10, -10, ESleekLerp.EXPONENTIAL, 1f);
					MenuSurvivorsClothingBoxUI.finalBox.lerpSizeOffset(20, 20, ESleekLerp.EXPONENTIAL, 1f);
					MenuSurvivorsClothingBoxUI.boxButton.updateInventory(0UL, MenuSurvivorsClothingBoxUI.boxAsset.drops[MenuSurvivorsClothingBoxUI.rotation % MenuSurvivorsClothingBoxUI.boxAsset.drops.Length], 1, false, true);
					if (MenuSurvivorsClothingBoxUI.rotation == MenuSurvivorsClothingBoxUI.target)
					{
						MainCamera.instance.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Economy/Sounds/Drop"), 0.33f);
					}
					else
					{
						MainCamera.instance.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Economy/Sounds/Tick"), 0.33f);
					}
				}
			}
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x0018092E File Offset: 0x0017ED2E
		private static void onClickedBackButton(SleekButton button)
		{
			MenuSurvivorsClothingItemUI.open();
			MenuSurvivorsClothingBoxUI.close();
		}

		// Token: 0x0400284F RID: 10319
		private static Bundle icons;

		// Token: 0x04002850 RID: 10320
		private static Local localization;

		// Token: 0x04002851 RID: 10321
		private static Sleek container;

		// Token: 0x04002852 RID: 10322
		public static bool active;

		// Token: 0x04002853 RID: 10323
		private static SleekButtonIcon backButton;

		// Token: 0x04002854 RID: 10324
		public static bool isUnboxing;

		// Token: 0x04002855 RID: 10325
		private static float lastUnbox;

		// Token: 0x04002856 RID: 10326
		private static float lastAngle;

		// Token: 0x04002857 RID: 10327
		private static float angle;

		// Token: 0x04002858 RID: 10328
		private static int lastRotation;

		// Token: 0x04002859 RID: 10329
		private static int rotation;

		// Token: 0x0400285A RID: 10330
		private static int target;

		// Token: 0x0400285B RID: 10331
		private static bool hasLoaded;

		// Token: 0x0400285C RID: 10332
		private static int item;

		// Token: 0x0400285D RID: 10333
		private static ulong instance;

		// Token: 0x0400285E RID: 10334
		private static int drop;

		// Token: 0x0400285F RID: 10335
		private static bool isMythical;

		// Token: 0x04002860 RID: 10336
		private static ulong got;

		// Token: 0x04002861 RID: 10337
		private static ItemBoxAsset boxAsset;

		// Token: 0x04002862 RID: 10338
		private static ItemKeyAsset keyAsset;

		// Token: 0x04002863 RID: 10339
		private static float size;

		// Token: 0x04002864 RID: 10340
		private static Sleek inventory;

		// Token: 0x04002865 RID: 10341
		private static SleekBox finalBox;

		// Token: 0x04002866 RID: 10342
		private static SleekInventory boxButton;

		// Token: 0x04002867 RID: 10343
		private static SleekButtonIcon keyButton;

		// Token: 0x04002868 RID: 10344
		private static SleekButtonIcon unboxButton;

		// Token: 0x04002869 RID: 10345
		private static SleekLabel rareLabel;

		// Token: 0x0400286A RID: 10346
		private static SleekLabel epicLabel;

		// Token: 0x0400286B RID: 10347
		private static SleekLabel legendaryLabel;

		// Token: 0x0400286C RID: 10348
		private static SleekLabel mythicalLabel;

		// Token: 0x0400286D RID: 10349
		private static SleekInventory[] dropButtons;

		// Token: 0x0400286E RID: 10350
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x0400286F RID: 10351
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x04002870 RID: 10352
		[CompilerGenerated]
		private static TempSteamworksEconomy.InventoryExchanged <>f__mg$cache2;

		// Token: 0x04002871 RID: 10353
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;
	}
}
