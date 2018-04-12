using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Provider;
using SDG.SteamworksProvider.Services.Economy;
using Steamworks;
using UnityEngine;
using UnityEngine.Analytics;

namespace SDG.Unturned
{
	// Token: 0x02000785 RID: 1925
	public class MenuSurvivorsClothingUI
	{
		// Token: 0x0600374F RID: 14159 RVA: 0x001826A8 File Offset: 0x00180AA8
		public MenuSurvivorsClothingUI()
		{
			MenuSurvivorsClothingUI.localization = Localization.read("/Menu/Survivors/MenuSurvivorsClothing.dat");
			if (MenuSurvivorsClothingUI.icons != null)
			{
				MenuSurvivorsClothingUI.icons.unload();
				MenuSurvivorsClothingUI.icons = null;
			}
			MenuSurvivorsClothingUI.icons = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Survivors/MenuSurvivorsClothing/MenuSurvivorsClothing.unity3d");
			MenuSurvivorsClothingUI.container = new Sleek();
			MenuSurvivorsClothingUI.container.positionOffset_X = 10;
			MenuSurvivorsClothingUI.container.positionOffset_Y = 10;
			MenuSurvivorsClothingUI.container.positionScale_Y = 1f;
			MenuSurvivorsClothingUI.container.sizeOffset_X = -20;
			MenuSurvivorsClothingUI.container.sizeOffset_Y = -20;
			MenuSurvivorsClothingUI.container.sizeScale_X = 1f;
			MenuSurvivorsClothingUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuSurvivorsClothingUI.container);
			MenuSurvivorsClothingUI.active = false;
			MenuSurvivorsClothingUI.page = 0;
			MenuSurvivorsClothingUI.filterMode = EEconFilterMode.SEARCH;
			MenuSurvivorsClothingUI.inventory = new Sleek();
			MenuSurvivorsClothingUI.inventory.positionOffset_Y = 40;
			MenuSurvivorsClothingUI.inventory.positionScale_X = 0.5f;
			MenuSurvivorsClothingUI.inventory.sizeScale_X = 0.5f;
			MenuSurvivorsClothingUI.inventory.sizeScale_Y = 1f;
			MenuSurvivorsClothingUI.inventory.sizeOffset_Y = -80;
			MenuSurvivorsClothingUI.inventory.constraint = ESleekConstraint.XY;
			MenuSurvivorsClothingUI.container.add(MenuSurvivorsClothingUI.inventory);
			MenuSurvivorsClothingUI.crafting = new Sleek();
			MenuSurvivorsClothingUI.crafting.positionOffset_Y = 40;
			MenuSurvivorsClothingUI.crafting.positionScale_X = 0.5f;
			MenuSurvivorsClothingUI.crafting.sizeScale_X = 0.5f;
			MenuSurvivorsClothingUI.crafting.sizeScale_Y = 1f;
			MenuSurvivorsClothingUI.crafting.sizeOffset_Y = -80;
			MenuSurvivorsClothingUI.crafting.constraint = ESleekConstraint.XY;
			MenuSurvivorsClothingUI.container.add(MenuSurvivorsClothingUI.crafting);
			MenuSurvivorsClothingUI.crafting.isVisible = false;
			MenuSurvivorsClothingUI.packageButtons = new SleekInventory[25];
			for (int i = 0; i < MenuSurvivorsClothingUI.packageButtons.Length; i++)
			{
				SleekInventory sleekInventory = new SleekInventory();
				sleekInventory.positionOffset_X = 5;
				sleekInventory.positionOffset_Y = 5;
				sleekInventory.positionScale_X = (float)(i % 5) * 0.2f;
				sleekInventory.positionScale_Y = (float)Mathf.FloorToInt((float)i / 5f) * 0.2f;
				sleekInventory.sizeOffset_X = -10;
				sleekInventory.sizeOffset_Y = -10;
				sleekInventory.sizeScale_X = 0.2f;
				sleekInventory.sizeScale_Y = 0.2f;
				SleekInventory sleekInventory2 = sleekInventory;
				if (MenuSurvivorsClothingUI.<>f__mg$cache0 == null)
				{
					MenuSurvivorsClothingUI.<>f__mg$cache0 = new ClickedInventory(MenuSurvivorsClothingUI.onClickedInventory);
				}
				sleekInventory2.onClickedInventory = MenuSurvivorsClothingUI.<>f__mg$cache0;
				MenuSurvivorsClothingUI.inventory.add(sleekInventory);
				MenuSurvivorsClothingUI.packageButtons[i] = sleekInventory;
			}
			MenuSurvivorsClothingUI.searchField = new SleekField();
			MenuSurvivorsClothingUI.searchField.positionOffset_X = 5;
			MenuSurvivorsClothingUI.searchField.positionOffset_Y = -35;
			MenuSurvivorsClothingUI.searchField.sizeOffset_X = -120;
			MenuSurvivorsClothingUI.searchField.sizeOffset_Y = 30;
			MenuSurvivorsClothingUI.searchField.sizeScale_X = 1f;
			MenuSurvivorsClothingUI.searchField.hint = MenuSurvivorsClothingUI.localization.format("Search_Field_Hint");
			MenuSurvivorsClothingUI.searchField.control = "Search";
			SleekField sleekField = MenuSurvivorsClothingUI.searchField;
			Delegate onEntered = sleekField.onEntered;
			if (MenuSurvivorsClothingUI.<>f__mg$cache1 == null)
			{
				MenuSurvivorsClothingUI.<>f__mg$cache1 = new Entered(MenuSurvivorsClothingUI.onEnteredSearchField);
			}
			sleekField.onEntered = (Entered)Delegate.Combine(onEntered, MenuSurvivorsClothingUI.<>f__mg$cache1);
			MenuSurvivorsClothingUI.inventory.add(MenuSurvivorsClothingUI.searchField);
			MenuSurvivorsClothingUI.searchButton = new SleekButton();
			MenuSurvivorsClothingUI.searchButton.positionOffset_X = -105;
			MenuSurvivorsClothingUI.searchButton.positionOffset_Y = -35;
			MenuSurvivorsClothingUI.searchButton.positionScale_X = 1f;
			MenuSurvivorsClothingUI.searchButton.sizeOffset_X = 100;
			MenuSurvivorsClothingUI.searchButton.sizeOffset_Y = 30;
			MenuSurvivorsClothingUI.searchButton.text = MenuSurvivorsClothingUI.localization.format("Search");
			MenuSurvivorsClothingUI.searchButton.tooltip = MenuSurvivorsClothingUI.localization.format("Search_Tooltip");
			SleekButton sleekButton = MenuSurvivorsClothingUI.searchButton;
			if (MenuSurvivorsClothingUI.<>f__mg$cache2 == null)
			{
				MenuSurvivorsClothingUI.<>f__mg$cache2 = new ClickedButton(MenuSurvivorsClothingUI.onClickedSearchButton);
			}
			sleekButton.onClickedButton = MenuSurvivorsClothingUI.<>f__mg$cache2;
			MenuSurvivorsClothingUI.inventory.add(MenuSurvivorsClothingUI.searchButton);
			MenuSurvivorsClothingUI.filterBox = new SleekBox();
			MenuSurvivorsClothingUI.filterBox.positionOffset_X = 5;
			MenuSurvivorsClothingUI.filterBox.positionOffset_Y = -35;
			MenuSurvivorsClothingUI.filterBox.sizeOffset_X = -120;
			MenuSurvivorsClothingUI.filterBox.sizeOffset_Y = 30;
			MenuSurvivorsClothingUI.filterBox.sizeScale_X = 1f;
			MenuSurvivorsClothingUI.filterBox.isRich = true;
			MenuSurvivorsClothingUI.inventory.add(MenuSurvivorsClothingUI.filterBox);
			MenuSurvivorsClothingUI.filterBox.isVisible = false;
			MenuSurvivorsClothingUI.cancelFilterButton = new SleekButton();
			MenuSurvivorsClothingUI.cancelFilterButton.positionOffset_X = -105;
			MenuSurvivorsClothingUI.cancelFilterButton.positionOffset_Y = -35;
			MenuSurvivorsClothingUI.cancelFilterButton.positionScale_X = 1f;
			MenuSurvivorsClothingUI.cancelFilterButton.sizeOffset_X = 100;
			MenuSurvivorsClothingUI.cancelFilterButton.sizeOffset_Y = 30;
			MenuSurvivorsClothingUI.cancelFilterButton.text = MenuSurvivorsClothingUI.localization.format("Cancel_Filter");
			MenuSurvivorsClothingUI.cancelFilterButton.tooltip = MenuSurvivorsClothingUI.localization.format("Cancel_Filter_Tooltip");
			SleekButton sleekButton2 = MenuSurvivorsClothingUI.cancelFilterButton;
			if (MenuSurvivorsClothingUI.<>f__mg$cache3 == null)
			{
				MenuSurvivorsClothingUI.<>f__mg$cache3 = new ClickedButton(MenuSurvivorsClothingUI.onClickedCancelFilterButton);
			}
			sleekButton2.onClickedButton = MenuSurvivorsClothingUI.<>f__mg$cache3;
			MenuSurvivorsClothingUI.inventory.add(MenuSurvivorsClothingUI.cancelFilterButton);
			MenuSurvivorsClothingUI.cancelFilterButton.isVisible = false;
			MenuSurvivorsClothingUI.pageBox = new SleekBox();
			MenuSurvivorsClothingUI.pageBox.positionOffset_X = -145;
			MenuSurvivorsClothingUI.pageBox.positionOffset_Y = 5;
			MenuSurvivorsClothingUI.pageBox.positionScale_X = 1f;
			MenuSurvivorsClothingUI.pageBox.positionScale_Y = 1f;
			MenuSurvivorsClothingUI.pageBox.sizeOffset_X = 100;
			MenuSurvivorsClothingUI.pageBox.sizeOffset_Y = 30;
			MenuSurvivorsClothingUI.pageBox.fontSize = 14;
			MenuSurvivorsClothingUI.inventory.add(MenuSurvivorsClothingUI.pageBox);
			MenuSurvivorsClothingUI.infoBox = new SleekBox();
			MenuSurvivorsClothingUI.infoBox.positionOffset_X = 5;
			MenuSurvivorsClothingUI.infoBox.positionOffset_Y = -25;
			MenuSurvivorsClothingUI.infoBox.positionScale_Y = 0.5f;
			MenuSurvivorsClothingUI.infoBox.sizeScale_X = 1f;
			MenuSurvivorsClothingUI.infoBox.sizeOffset_X = -10;
			MenuSurvivorsClothingUI.infoBox.sizeOffset_Y = 50;
			MenuSurvivorsClothingUI.infoBox.text = MenuSurvivorsClothingUI.localization.format("No_Items");
			MenuSurvivorsClothingUI.infoBox.fontSize = 14;
			MenuSurvivorsClothingUI.inventory.add(MenuSurvivorsClothingUI.infoBox);
			MenuSurvivorsClothingUI.infoBox.isVisible = !Provider.provider.economyService.isInventoryAvailable;
			MenuSurvivorsClothingUI.leftButton = new SleekButtonIcon((Texture2D)MenuSurvivorsClothingUI.icons.load("Left"));
			MenuSurvivorsClothingUI.leftButton.positionOffset_X = -185;
			MenuSurvivorsClothingUI.leftButton.positionOffset_Y = 5;
			MenuSurvivorsClothingUI.leftButton.positionScale_X = 1f;
			MenuSurvivorsClothingUI.leftButton.positionScale_Y = 1f;
			MenuSurvivorsClothingUI.leftButton.sizeOffset_X = 30;
			MenuSurvivorsClothingUI.leftButton.sizeOffset_Y = 30;
			MenuSurvivorsClothingUI.leftButton.tooltip = MenuSurvivorsClothingUI.localization.format("Left_Tooltip");
			MenuSurvivorsClothingUI.leftButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton3 = MenuSurvivorsClothingUI.leftButton;
			if (MenuSurvivorsClothingUI.<>f__mg$cache4 == null)
			{
				MenuSurvivorsClothingUI.<>f__mg$cache4 = new ClickedButton(MenuSurvivorsClothingUI.onClickedLeftButton);
			}
			sleekButton3.onClickedButton = MenuSurvivorsClothingUI.<>f__mg$cache4;
			MenuSurvivorsClothingUI.inventory.add(MenuSurvivorsClothingUI.leftButton);
			MenuSurvivorsClothingUI.rightButton = new SleekButtonIcon((Texture2D)MenuSurvivorsClothingUI.icons.load("Right"));
			MenuSurvivorsClothingUI.rightButton.positionOffset_X = -35;
			MenuSurvivorsClothingUI.rightButton.positionOffset_Y = 5;
			MenuSurvivorsClothingUI.rightButton.positionScale_X = 1f;
			MenuSurvivorsClothingUI.rightButton.positionScale_Y = 1f;
			MenuSurvivorsClothingUI.rightButton.sizeOffset_X = 30;
			MenuSurvivorsClothingUI.rightButton.sizeOffset_Y = 30;
			MenuSurvivorsClothingUI.rightButton.tooltip = MenuSurvivorsClothingUI.localization.format("Right_Tooltip");
			MenuSurvivorsClothingUI.rightButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton4 = MenuSurvivorsClothingUI.rightButton;
			if (MenuSurvivorsClothingUI.<>f__mg$cache5 == null)
			{
				MenuSurvivorsClothingUI.<>f__mg$cache5 = new ClickedButton(MenuSurvivorsClothingUI.onClickedRightButton);
			}
			sleekButton4.onClickedButton = MenuSurvivorsClothingUI.<>f__mg$cache5;
			MenuSurvivorsClothingUI.inventory.add(MenuSurvivorsClothingUI.rightButton);
			MenuSurvivorsClothingUI.refreshButton = new SleekButtonIcon((Texture2D)MenuSurvivorsClothingUI.icons.load("Refresh"));
			MenuSurvivorsClothingUI.refreshButton.positionOffset_X = 5;
			MenuSurvivorsClothingUI.refreshButton.positionOffset_Y = 5;
			MenuSurvivorsClothingUI.refreshButton.positionScale_Y = 1f;
			MenuSurvivorsClothingUI.refreshButton.sizeOffset_X = 30;
			MenuSurvivorsClothingUI.refreshButton.sizeOffset_Y = 30;
			MenuSurvivorsClothingUI.refreshButton.tooltip = MenuSurvivorsClothingUI.localization.format("Refresh_Tooltip");
			MenuSurvivorsClothingUI.refreshButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton5 = MenuSurvivorsClothingUI.refreshButton;
			if (MenuSurvivorsClothingUI.<>f__mg$cache6 == null)
			{
				MenuSurvivorsClothingUI.<>f__mg$cache6 = new ClickedButton(MenuSurvivorsClothingUI.onClickedRefreshButton);
			}
			sleekButton5.onClickedButton = MenuSurvivorsClothingUI.<>f__mg$cache6;
			MenuSurvivorsClothingUI.inventory.add(MenuSurvivorsClothingUI.refreshButton);
			MenuSurvivorsClothingUI.characterSlider = new SleekSlider();
			MenuSurvivorsClothingUI.characterSlider.positionOffset_X = 45;
			MenuSurvivorsClothingUI.characterSlider.positionOffset_Y = 10;
			MenuSurvivorsClothingUI.characterSlider.positionScale_Y = 1f;
			MenuSurvivorsClothingUI.characterSlider.sizeOffset_X = -240;
			MenuSurvivorsClothingUI.characterSlider.sizeOffset_Y = 20;
			MenuSurvivorsClothingUI.characterSlider.sizeScale_X = 1f;
			MenuSurvivorsClothingUI.characterSlider.orientation = ESleekOrientation.HORIZONTAL;
			SleekSlider sleekSlider = MenuSurvivorsClothingUI.characterSlider;
			if (MenuSurvivorsClothingUI.<>f__mg$cache7 == null)
			{
				MenuSurvivorsClothingUI.<>f__mg$cache7 = new Dragged(MenuSurvivorsClothingUI.onDraggedCharacterSlider);
			}
			sleekSlider.onDragged = MenuSurvivorsClothingUI.<>f__mg$cache7;
			MenuSurvivorsClothingUI.inventory.add(MenuSurvivorsClothingUI.characterSlider);
			MenuSurvivorsClothingUI.availableBox = new SleekBox();
			MenuSurvivorsClothingUI.availableBox.sizeScale_X = 1f;
			MenuSurvivorsClothingUI.availableBox.sizeOffset_Y = 30;
			MenuSurvivorsClothingUI.availableBox.isRich = true;
			MenuSurvivorsClothingUI.crafting.add(MenuSurvivorsClothingUI.availableBox);
			MenuSurvivorsClothingUI.craftingScrollBox = new SleekScrollBox();
			MenuSurvivorsClothingUI.craftingScrollBox.positionOffset_Y = 40;
			MenuSurvivorsClothingUI.craftingScrollBox.sizeScale_X = 1f;
			MenuSurvivorsClothingUI.craftingScrollBox.sizeScale_Y = 1f;
			MenuSurvivorsClothingUI.craftingScrollBox.sizeOffset_Y = -40;
			MenuSurvivorsClothingUI.crafting.add(MenuSurvivorsClothingUI.craftingScrollBox);
			MenuSurvivorsClothingUI.craftingButtons = new SleekButton[MenuSurvivorsClothingUI.ECON_CRAFT_OPTIONS.Length];
			for (int j = 0; j < MenuSurvivorsClothingUI.craftingButtons.Length; j++)
			{
				EconCraftOption econCraftOption = MenuSurvivorsClothingUI.ECON_CRAFT_OPTIONS[j];
				SleekButton sleekButton6 = new SleekButton();
				sleekButton6.positionOffset_Y = j * 30;
				sleekButton6.sizeScale_X = 1f;
				sleekButton6.sizeOffset_X = -30;
				sleekButton6.sizeOffset_Y = 30;
				sleekButton6.isRich = true;
				sleekButton6.text = ItemTool.filterRarityRichText(MenuSurvivorsClothingUI.localization.format("Craft_Entry", new object[]
				{
					MenuSurvivorsClothingUI.localization.format(econCraftOption.token),
					econCraftOption.scrapsNeeded
				}));
				SleekButton sleekButton7 = sleekButton6;
				if (MenuSurvivorsClothingUI.<>f__mg$cache8 == null)
				{
					MenuSurvivorsClothingUI.<>f__mg$cache8 = new ClickedButton(MenuSurvivorsClothingUI.onClickedCraftButton);
				}
				sleekButton7.onClickedButton = MenuSurvivorsClothingUI.<>f__mg$cache8;
				MenuSurvivorsClothingUI.craftingScrollBox.add(sleekButton6);
				MenuSurvivorsClothingUI.craftingButtons[j] = sleekButton6;
			}
			MenuSurvivorsClothingUI.craftingScrollBox.area = new Rect(0f, 0f, 5f, (float)(MenuSurvivorsClothingUI.ECON_CRAFT_OPTIONS.Length * 30));
			MenuSurvivorsClothingUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuSurvivorsClothingUI.backButton.positionOffset_Y = -50;
			MenuSurvivorsClothingUI.backButton.positionScale_Y = 1f;
			MenuSurvivorsClothingUI.backButton.sizeOffset_X = 200;
			MenuSurvivorsClothingUI.backButton.sizeOffset_Y = 50;
			MenuSurvivorsClothingUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuSurvivorsClothingUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton8 = MenuSurvivorsClothingUI.backButton;
			if (MenuSurvivorsClothingUI.<>f__mg$cache9 == null)
			{
				MenuSurvivorsClothingUI.<>f__mg$cache9 = new ClickedButton(MenuSurvivorsClothingUI.onClickedBackButton);
			}
			sleekButton8.onClickedButton = MenuSurvivorsClothingUI.<>f__mg$cache9;
			MenuSurvivorsClothingUI.backButton.fontSize = 14;
			MenuSurvivorsClothingUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuSurvivorsClothingUI.container.add(MenuSurvivorsClothingUI.backButton);
			MenuSurvivorsClothingUI.itemstoreButton = new SleekButton();
			MenuSurvivorsClothingUI.itemstoreButton.positionOffset_Y = -170;
			MenuSurvivorsClothingUI.itemstoreButton.positionScale_Y = 1f;
			MenuSurvivorsClothingUI.itemstoreButton.sizeOffset_X = 200;
			MenuSurvivorsClothingUI.itemstoreButton.sizeOffset_Y = 50;
			MenuSurvivorsClothingUI.itemstoreButton.text = MenuSurvivorsClothingUI.localization.format("Itemstore");
			MenuSurvivorsClothingUI.itemstoreButton.tooltip = MenuSurvivorsClothingUI.localization.format("Itemstore_Tooltip");
			SleekButton sleekButton9 = MenuSurvivorsClothingUI.itemstoreButton;
			if (MenuSurvivorsClothingUI.<>f__mg$cacheA == null)
			{
				MenuSurvivorsClothingUI.<>f__mg$cacheA = new ClickedButton(MenuSurvivorsClothingUI.onClickedItemstoreButton);
			}
			sleekButton9.onClickedButton = MenuSurvivorsClothingUI.<>f__mg$cacheA;
			MenuSurvivorsClothingUI.itemstoreButton.fontSize = 14;
			MenuSurvivorsClothingUI.container.add(MenuSurvivorsClothingUI.itemstoreButton);
			MenuSurvivorsClothingUI.craftingButton = new SleekButtonIcon((Texture2D)MenuSurvivorsClothingUI.icons.load("Crafting"));
			MenuSurvivorsClothingUI.craftingButton.positionOffset_Y = -110;
			MenuSurvivorsClothingUI.craftingButton.positionScale_Y = 1f;
			MenuSurvivorsClothingUI.craftingButton.sizeOffset_X = 200;
			MenuSurvivorsClothingUI.craftingButton.sizeOffset_Y = 50;
			MenuSurvivorsClothingUI.craftingButton.text = MenuSurvivorsClothingUI.localization.format("Crafting");
			MenuSurvivorsClothingUI.craftingButton.tooltip = MenuSurvivorsClothingUI.localization.format("Crafting_Tooltip");
			SleekButton sleekButton10 = MenuSurvivorsClothingUI.craftingButton;
			if (MenuSurvivorsClothingUI.<>f__mg$cacheB == null)
			{
				MenuSurvivorsClothingUI.<>f__mg$cacheB = new ClickedButton(MenuSurvivorsClothingUI.onClickedCraftingButton);
			}
			sleekButton10.onClickedButton = MenuSurvivorsClothingUI.<>f__mg$cacheB;
			MenuSurvivorsClothingUI.craftingButton.fontSize = 14;
			MenuSurvivorsClothingUI.craftingButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuSurvivorsClothingUI.container.add(MenuSurvivorsClothingUI.craftingButton);
			if (Provider.statusData.Stockpile.Has_New_Items)
			{
				SleekNew sleek = new SleekNew(false);
				MenuSurvivorsClothingUI.itemstoreButton.add(sleek);
			}
			if (Provider.statusData.Stockpile.Featured_Item != 0)
			{
				MenuSurvivorsClothingUI.featuredButton = new SleekButton();
				MenuSurvivorsClothingUI.featuredButton.positionOffset_Y = -230;
				MenuSurvivorsClothingUI.featuredButton.positionScale_Y = 1f;
				MenuSurvivorsClothingUI.featuredButton.sizeOffset_X = 200;
				MenuSurvivorsClothingUI.featuredButton.sizeOffset_Y = 50;
				MenuSurvivorsClothingUI.featuredButton.text = Provider.provider.economyService.getInventoryName(Provider.statusData.Stockpile.Featured_Item);
				MenuSurvivorsClothingUI.featuredButton.tooltip = MenuSurvivorsClothingUI.localization.format("Featured_Tooltip");
				SleekButton sleekButton11 = MenuSurvivorsClothingUI.featuredButton;
				if (MenuSurvivorsClothingUI.<>f__mg$cacheC == null)
				{
					MenuSurvivorsClothingUI.<>f__mg$cacheC = new ClickedButton(MenuSurvivorsClothingUI.onClickedFeaturedButton);
				}
				sleekButton11.onClickedButton = MenuSurvivorsClothingUI.<>f__mg$cacheC;
				MenuSurvivorsClothingUI.featuredButton.foregroundTint = ESleekTint.NONE;
				MenuSurvivorsClothingUI.featuredButton.foregroundColor = Provider.provider.economyService.getInventoryColor(Provider.statusData.Stockpile.Featured_Item);
				MenuSurvivorsClothingUI.featuredButton.fontSize = 14;
				MenuSurvivorsClothingUI.container.add(MenuSurvivorsClothingUI.featuredButton);
				SleekNew sleek2 = new SleekNew(false);
				MenuSurvivorsClothingUI.featuredButton.add(sleek2);
			}
			if (!MenuSurvivorsClothingUI.hasLoaded)
			{
				TempSteamworksEconomy economyService = Provider.provider.economyService;
				Delegate onInventoryExchanged = economyService.onInventoryExchanged;
				if (MenuSurvivorsClothingUI.<>f__mg$cacheD == null)
				{
					MenuSurvivorsClothingUI.<>f__mg$cacheD = new TempSteamworksEconomy.InventoryExchanged(MenuSurvivorsClothingUI.onInventoryExchanged);
				}
				economyService.onInventoryExchanged = (TempSteamworksEconomy.InventoryExchanged)Delegate.Combine(onInventoryExchanged, MenuSurvivorsClothingUI.<>f__mg$cacheD);
				TempSteamworksEconomy economyService2 = Provider.provider.economyService;
				Delegate onInventoryRefreshed = economyService2.onInventoryRefreshed;
				if (MenuSurvivorsClothingUI.<>f__mg$cacheE == null)
				{
					MenuSurvivorsClothingUI.<>f__mg$cacheE = new TempSteamworksEconomy.InventoryRefreshed(MenuSurvivorsClothingUI.onInventoryRefreshed);
				}
				economyService2.onInventoryRefreshed = (TempSteamworksEconomy.InventoryRefreshed)Delegate.Combine(onInventoryRefreshed, MenuSurvivorsClothingUI.<>f__mg$cacheE);
				TempSteamworksEconomy economyService3 = Provider.provider.economyService;
				Delegate onInventoryDropped = economyService3.onInventoryDropped;
				if (MenuSurvivorsClothingUI.<>f__mg$cacheF == null)
				{
					MenuSurvivorsClothingUI.<>f__mg$cacheF = new TempSteamworksEconomy.InventoryDropped(MenuSurvivorsClothingUI.onInventoryDropped);
				}
				economyService3.onInventoryDropped = (TempSteamworksEconomy.InventoryDropped)Delegate.Combine(onInventoryDropped, MenuSurvivorsClothingUI.<>f__mg$cacheF);
			}
			Delegate onCharacterUpdated = Characters.onCharacterUpdated;
			if (MenuSurvivorsClothingUI.<>f__mg$cache10 == null)
			{
				MenuSurvivorsClothingUI.<>f__mg$cache10 = new CharacterUpdated(MenuSurvivorsClothingUI.onCharacterUpdated);
			}
			Characters.onCharacterUpdated = (CharacterUpdated)Delegate.Combine(onCharacterUpdated, MenuSurvivorsClothingUI.<>f__mg$cache10);
			MenuSurvivorsClothingUI.hasLoaded = true;
			MenuSurvivorsClothingUI.updateFilter();
			MenuSurvivorsClothingUI.updatePage();
			new MenuSurvivorsClothingItemUI();
			new MenuSurvivorsClothingInspectUI();
			new MenuSurvivorsClothingDeleteUI();
			new MenuSurvivorsClothingBoxUI();
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06003750 RID: 14160 RVA: 0x00183611 File Offset: 0x00181A11
		private static int pages
		{
			get
			{
				if (MenuSurvivorsClothingUI.filteredItems.Count == 0)
				{
					return 1;
				}
				return (int)Mathf.Ceil((float)MenuSurvivorsClothingUI.filteredItems.Count / 25f);
			}
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x0018363B File Offset: 0x00181A3B
		public static void open()
		{
			if (MenuSurvivorsClothingUI.active)
			{
				return;
			}
			MenuSurvivorsClothingUI.active = true;
			Characters.apply(false, true);
			MenuSurvivorsClothingUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x00183670 File Offset: 0x00181A70
		public static void close()
		{
			if (!MenuSurvivorsClothingUI.active)
			{
				return;
			}
			MenuSurvivorsClothingUI.active = false;
			if (!MenuSurvivorsClothingBoxUI.active && !MenuSurvivorsClothingInspectUI.active && !MenuSurvivorsClothingDeleteUI.active && !MenuSurvivorsClothingItemUI.active)
			{
				Characters.apply(true, true);
			}
			MenuSurvivorsClothingUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x001836D8 File Offset: 0x00181AD8
		public static void setFilter(EEconFilterMode newFilterMode, ulong newFilterInstigator)
		{
			MenuSurvivorsClothingUI.setCrafting(false);
			MenuSurvivorsClothingUI.filterMode = newFilterMode;
			MenuSurvivorsClothingUI.filterInstigator = newFilterInstigator;
			MenuSurvivorsClothingUI.searchField.isVisible = (MenuSurvivorsClothingUI.filterMode == EEconFilterMode.SEARCH);
			MenuSurvivorsClothingUI.searchButton.isVisible = (MenuSurvivorsClothingUI.filterMode == EEconFilterMode.SEARCH);
			MenuSurvivorsClothingUI.filterBox.isVisible = (MenuSurvivorsClothingUI.filterMode != EEconFilterMode.SEARCH);
			MenuSurvivorsClothingUI.cancelFilterButton.isVisible = (MenuSurvivorsClothingUI.filterMode != EEconFilterMode.SEARCH);
			if (MenuSurvivorsClothingUI.filterMode == EEconFilterMode.STAT_TRACKER)
			{
				int inventoryItem = Provider.provider.economyService.getInventoryItem(MenuSurvivorsClothingUI.filterInstigator);
				string inventoryName = Provider.provider.economyService.getInventoryName(inventoryItem);
				Color inventoryColor = Provider.provider.economyService.getInventoryColor(inventoryItem);
				string text = string.Concat(new string[]
				{
					"<color=",
					Palette.hex(inventoryColor),
					">",
					inventoryName,
					"</color>"
				});
				MenuSurvivorsClothingUI.filterBox.text = MenuSurvivorsClothingUI.localization.format("Filter_Item_Target", new object[]
				{
					text
				});
			}
			MenuSurvivorsClothingUI.updateFilterAndPage();
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x001837E5 File Offset: 0x00181BE5
		private static void updateFilterAndPage()
		{
			MenuSurvivorsClothingUI.updateFilter();
			if (MenuSurvivorsClothingUI.page >= MenuSurvivorsClothingUI.pages)
			{
				MenuSurvivorsClothingUI.page = MenuSurvivorsClothingUI.pages - 1;
			}
			MenuSurvivorsClothingUI.updatePage();
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x0018380C File Offset: 0x00181C0C
		public static void viewPage(int newPage)
		{
			MenuSurvivorsClothingUI.page = newPage;
			MenuSurvivorsClothingUI.updatePage();
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x0018381C File Offset: 0x00181C1C
		private static void onClickedInventory(SleekInventory button)
		{
			int num = MenuSurvivorsClothingUI.packageButtons.Length * MenuSurvivorsClothingUI.page;
			int num2 = MenuSurvivorsClothingUI.inventory.search(button);
			if (num + num2 < MenuSurvivorsClothingUI.filteredItems.Count)
			{
				if (MenuSurvivorsClothingUI.filterMode == EEconFilterMode.STAT_TRACKER)
				{
					MenuSurvivorsClothingDeleteUI.viewItem(MenuSurvivorsClothingUI.filteredItems[num + num2].m_iDefinition.m_SteamItemDef, MenuSurvivorsClothingUI.filteredItems[num + num2].m_itemId.m_SteamItemInstanceID, EDeleteMode.TAG_TOOL, MenuSurvivorsClothingUI.filterInstigator);
					MenuSurvivorsClothingDeleteUI.open();
					MenuSurvivorsClothingUI.setFilter(EEconFilterMode.SEARCH, 0UL);
					MenuSurvivorsClothingUI.close();
				}
				else if (Input.GetKey(ControlsSettings.other) && MenuSurvivorsClothingUI.packageButtons[num2].itemAsset != null)
				{
					if (MenuSurvivorsClothingUI.packageButtons[num2].itemAsset.type == EItemType.BOX)
					{
						MenuSurvivorsClothingItemUI.viewItem(MenuSurvivorsClothingUI.filteredItems[num + num2].m_iDefinition.m_SteamItemDef, MenuSurvivorsClothingUI.filteredItems[num + num2].m_unQuantity, MenuSurvivorsClothingUI.filteredItems[num + num2].m_itemId.m_SteamItemInstanceID);
						MenuSurvivorsClothingBoxUI.viewItem(MenuSurvivorsClothingUI.filteredItems[num + num2].m_iDefinition.m_SteamItemDef, MenuSurvivorsClothingUI.filteredItems[num + num2].m_unQuantity, MenuSurvivorsClothingUI.filteredItems[num + num2].m_itemId.m_SteamItemInstanceID);
						MenuSurvivorsClothingBoxUI.open();
						MenuSurvivorsClothingUI.close();
					}
					else
					{
						Characters.package(MenuSurvivorsClothingUI.filteredItems[num + num2].m_itemId.m_SteamItemInstanceID);
					}
				}
				else
				{
					MenuSurvivorsClothingItemUI.viewItem(MenuSurvivorsClothingUI.filteredItems[num + num2].m_iDefinition.m_SteamItemDef, MenuSurvivorsClothingUI.filteredItems[num + num2].m_unQuantity, MenuSurvivorsClothingUI.filteredItems[num + num2].m_itemId.m_SteamItemInstanceID);
					MenuSurvivorsClothingItemUI.open();
					MenuSurvivorsClothingUI.close();
				}
			}
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x00183A1F File Offset: 0x00181E1F
		private static void onEnteredSearchField(SleekField field)
		{
			MenuSurvivorsClothingUI.updateFilterAndPage();
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x00183A26 File Offset: 0x00181E26
		private static void onClickedSearchButton(SleekButton button)
		{
			MenuSurvivorsClothingUI.updateFilterAndPage();
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x00183A2D File Offset: 0x00181E2D
		private static void onClickedCancelFilterButton(SleekButton button)
		{
			MenuSurvivorsClothingUI.setFilter(EEconFilterMode.SEARCH, 0UL);
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x00183A37 File Offset: 0x00181E37
		private static void onClickedLeftButton(SleekButton button)
		{
			if (MenuSurvivorsClothingUI.page > 0)
			{
				MenuSurvivorsClothingUI.viewPage(MenuSurvivorsClothingUI.page - 1);
			}
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x00183A50 File Offset: 0x00181E50
		private static void onClickedRightButton(SleekButton button)
		{
			if (MenuSurvivorsClothingUI.page < MenuSurvivorsClothingUI.pages - 1)
			{
				MenuSurvivorsClothingUI.viewPage(MenuSurvivorsClothingUI.page + 1);
			}
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x00183A6F File Offset: 0x00181E6F
		private static void onClickedRefreshButton(SleekButton button)
		{
			Provider.provider.economyService.refreshInventory();
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x00183A80 File Offset: 0x00181E80
		public static void prepareForCraftResult()
		{
			MenuSurvivorsClothingUI.isCrafting = true;
			MenuUI.openAlert(MenuSurvivorsClothingUI.localization.format("Alert_Crafting"));
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x00183A9C File Offset: 0x00181E9C
		private static void onClickedCraftButton(SleekButton button)
		{
			if (MenuSurvivorsClothingUI.isCrafting)
			{
				return;
			}
			int num = MenuSurvivorsClothingUI.craftingScrollBox.search(button);
			if (num == -1)
			{
				return;
			}
			EconCraftOption econCraftOption = MenuSurvivorsClothingUI.ECON_CRAFT_OPTIONS[num];
			ulong[] array = new ulong[econCraftOption.scrapsNeeded];
			if (!Provider.provider.economyService.getInventoryPackages(19000, array))
			{
				return;
			}
			MenuSurvivorsClothingUI.prepareForCraftResult();
			Provider.provider.economyService.exchangeInventory(econCraftOption.generate, array);
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x00183B12 File Offset: 0x00181F12
		private static void onInventoryRefreshed()
		{
			MenuSurvivorsClothingUI.infoBox.isVisible = false;
			MenuSurvivorsClothingUI.updateFilter();
			if (MenuSurvivorsClothingUI.page >= MenuSurvivorsClothingUI.pages)
			{
				MenuSurvivorsClothingUI.page = MenuSurvivorsClothingUI.pages - 1;
			}
			MenuSurvivorsClothingUI.updatePage();
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x00183B44 File Offset: 0x00181F44
		public static void onInventoryDropped(int item, ushort quantity, ulong instance)
		{
			MenuUI.closeAll();
			MenuUI.alert(MenuSurvivorsClothingUI.localization.format("Origin_Drop"), instance, item, quantity);
			MenuSurvivorsClothingItemUI.viewItem(item, quantity, instance);
			MenuSurvivorsClothingItemUI.open();
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x00183B6F File Offset: 0x00181F6F
		private static void onCharacterUpdated(byte index, Character character)
		{
			MenuSurvivorsClothingUI.updatePage();
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x00183B78 File Offset: 0x00181F78
		private static void updateFilter()
		{
			string text = MenuSurvivorsClothingUI.searchField.text;
			if (MenuSurvivorsClothingUI.filterMode == EEconFilterMode.STAT_TRACKER)
			{
				MenuSurvivorsClothingUI.filteredItems = new List<SteamItemDetails_t>();
				for (int i = 0; i < Provider.provider.economyService.inventory.Length; i++)
				{
					SteamItemDetails_t item = Provider.provider.economyService.inventory[i];
					int inventoryItemID = (int)Provider.provider.economyService.getInventoryItemID(item.m_iDefinition.m_SteamItemDef);
					int inventorySkinID = (int)Provider.provider.economyService.getInventorySkinID(item.m_iDefinition.m_SteamItemDef);
					if (inventoryItemID != 0 && inventorySkinID != 0)
					{
						MenuSurvivorsClothingUI.filteredItems.Add(item);
					}
				}
			}
			else if (text == null || text.Length < 1)
			{
				MenuSurvivorsClothingUI.filteredItems = new List<SteamItemDetails_t>(Provider.provider.economyService.inventory);
			}
			else
			{
				MenuSurvivorsClothingUI.filteredItems = new List<SteamItemDetails_t>();
				for (int j = 0; j < Provider.provider.economyService.inventory.Length; j++)
				{
					SteamItemDetails_t item2 = Provider.provider.economyService.inventory[j];
					string inventoryName = Provider.provider.economyService.getInventoryName(item2.m_iDefinition.m_SteamItemDef);
					if (inventoryName.IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1)
					{
						MenuSurvivorsClothingUI.filteredItems.Add(item2);
					}
					else
					{
						string inventoryType = Provider.provider.economyService.getInventoryType(item2.m_iDefinition.m_SteamItemDef);
						if (inventoryType.IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1)
						{
							MenuSurvivorsClothingUI.filteredItems.Add(item2);
						}
					}
				}
			}
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x00183D2C File Offset: 0x0018212C
		public static void updatePage()
		{
			MenuSurvivorsClothingUI.availableBox.text = ItemTool.filterRarityRichText(MenuSurvivorsClothingUI.localization.format("Craft_Available", new object[]
			{
				Provider.provider.economyService.countInventoryPackages(19000)
			}));
			MenuSurvivorsClothingUI.pageBox.text = MenuSurvivorsClothingUI.localization.format("Page", new object[]
			{
				MenuSurvivorsClothingUI.page + 1,
				MenuSurvivorsClothingUI.pages
			});
			if (MenuSurvivorsClothingUI.packageButtons == null)
			{
				return;
			}
			int num = MenuSurvivorsClothingUI.packageButtons.Length * MenuSurvivorsClothingUI.page;
			for (int i = 0; i < MenuSurvivorsClothingUI.packageButtons.Length; i++)
			{
				if (num + i < MenuSurvivorsClothingUI.filteredItems.Count)
				{
					MenuSurvivorsClothingUI.packageButtons[i].updateInventory(MenuSurvivorsClothingUI.filteredItems[num + i].m_itemId.m_SteamItemInstanceID, MenuSurvivorsClothingUI.filteredItems[num + i].m_iDefinition.m_SteamItemDef, MenuSurvivorsClothingUI.filteredItems[num + i].m_unQuantity, true, false);
				}
				else
				{
					MenuSurvivorsClothingUI.packageButtons[i].updateInventory(0UL, 0, 0, false, false);
				}
			}
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x00183E66 File Offset: 0x00182266
		private static void onDraggedCharacterSlider(SleekSlider slider, float state)
		{
			Characters.characterYaw = state * 360f;
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x00183E74 File Offset: 0x00182274
		private static void onClickedBackButton(SleekButton button)
		{
			MenuSurvivorsUI.open();
			MenuSurvivorsClothingUI.close();
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x00183E80 File Offset: 0x00182280
		private static void onClickedItemstoreButton(SleekButton button)
		{
			if (!Provider.provider.storeService.canOpenStore)
			{
				MenuUI.alert(MenuSurvivorsClothingUI.localization.format("Overlay"));
				return;
			}
			Provider.provider.storeService.open();
			Analytics.CustomEvent("Link_Stockpile", null);
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x00183ED4 File Offset: 0x001822D4
		private static void setCrafting(bool isCrafting)
		{
			MenuSurvivorsClothingUI.inventory.isVisible = !isCrafting;
			MenuSurvivorsClothingUI.crafting.isVisible = isCrafting;
			MenuSurvivorsClothingUI.craftingButton.iconImage.texture = ((!MenuSurvivorsClothingUI.inventory.isVisible) ? ((Texture2D)MenuSurvivorsClothingUI.icons.load("Backpack")) : ((Texture2D)MenuSurvivorsClothingUI.icons.load("Crafting")));
			MenuSurvivorsClothingUI.craftingButton.text = MenuSurvivorsClothingUI.localization.format((!MenuSurvivorsClothingUI.inventory.isVisible) ? "Backpack" : "Crafting");
			MenuSurvivorsClothingUI.craftingButton.tooltip = MenuSurvivorsClothingUI.localization.format((!MenuSurvivorsClothingUI.inventory.isVisible) ? "Backpack_Tooltip" : "Crafting_Tooltip");
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x00183FA9 File Offset: 0x001823A9
		private static void onClickedCraftingButton(SleekButton button)
		{
			MenuSurvivorsClothingUI.setCrafting(!MenuSurvivorsClothingUI.crafting.isVisible);
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x00183FC0 File Offset: 0x001823C0
		private static void onClickedFeaturedButton(SleekButton button)
		{
			if (!Provider.provider.storeService.canOpenStore)
			{
				MenuUI.alert(MenuSurvivorsClothingUI.localization.format("Overlay"));
				return;
			}
			Provider.provider.storeService.open(new SteamworksEconomyItemDefinition((SteamItemDef_t)Provider.statusData.Stockpile.Featured_Item));
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x00184020 File Offset: 0x00182420
		private static void onInventoryExchanged(int newItem, ushort newQuantity, ulong newInstance)
		{
			if (!MenuSurvivorsClothingUI.isCrafting)
			{
				return;
			}
			MenuSurvivorsClothingUI.isCrafting = false;
			MenuUI.closeAlert();
			MenuUI.alert(MenuSurvivorsClothingUI.localization.format("Origin_Craft"), newInstance, newItem, newQuantity);
			MenuSurvivorsClothingItemUI.viewItem(newItem, newQuantity, newInstance);
			MenuSurvivorsClothingItemUI.open();
			MenuSurvivorsClothingUI.close();
		}

		// Token: 0x040028B3 RID: 10419
		public static EconCraftOption[] ECON_CRAFT_OPTIONS = new EconCraftOption[]
		{
			new EconCraftOption("Craft_Common_Cosmetic", 10003, 2),
			new EconCraftOption("Craft_Common_Skin", 10006, 2),
			new EconCraftOption("Craft_Uncommon_Cosmetic", 10004, 13),
			new EconCraftOption("Craft_Uncommon_Skin", 10007, 13),
			new EconCraftOption("Craft_Stat_Tracker_Total_Kills", 19001, 30),
			new EconCraftOption("Craft_Stat_Tracker_Player_Kills", 19002, 30)
		};

		// Token: 0x040028B4 RID: 10420
		public static Local localization;

		// Token: 0x040028B5 RID: 10421
		public static Bundle icons;

		// Token: 0x040028B6 RID: 10422
		private static Sleek container;

		// Token: 0x040028B7 RID: 10423
		public static bool active;

		// Token: 0x040028B8 RID: 10424
		public static bool isCrafting;

		// Token: 0x040028B9 RID: 10425
		private static SleekButtonIcon backButton;

		// Token: 0x040028BA RID: 10426
		private static SleekButton itemstoreButton;

		// Token: 0x040028BB RID: 10427
		private static SleekButtonIcon craftingButton;

		// Token: 0x040028BC RID: 10428
		private static SleekButton featuredButton;

		// Token: 0x040028BD RID: 10429
		private static List<SteamItemDetails_t> filteredItems;

		// Token: 0x040028BE RID: 10430
		private static Sleek inventory;

		// Token: 0x040028BF RID: 10431
		private static Sleek crafting;

		// Token: 0x040028C0 RID: 10432
		private static SleekInventory[] packageButtons;

		// Token: 0x040028C1 RID: 10433
		private static SleekBox availableBox;

		// Token: 0x040028C2 RID: 10434
		private static SleekScrollBox craftingScrollBox;

		// Token: 0x040028C3 RID: 10435
		private static SleekButton[] craftingButtons;

		// Token: 0x040028C4 RID: 10436
		private static SleekBox pageBox;

		// Token: 0x040028C5 RID: 10437
		private static SleekBox infoBox;

		// Token: 0x040028C6 RID: 10438
		private static SleekField searchField;

		// Token: 0x040028C7 RID: 10439
		private static SleekButton searchButton;

		// Token: 0x040028C8 RID: 10440
		private static SleekBox filterBox;

		// Token: 0x040028C9 RID: 10441
		private static SleekButton cancelFilterButton;

		// Token: 0x040028CA RID: 10442
		private static SleekButtonIcon leftButton;

		// Token: 0x040028CB RID: 10443
		private static SleekButtonIcon rightButton;

		// Token: 0x040028CC RID: 10444
		private static SleekButtonIcon refreshButton;

		// Token: 0x040028CD RID: 10445
		private static SleekSlider characterSlider;

		// Token: 0x040028CE RID: 10446
		private static int page;

		// Token: 0x040028CF RID: 10447
		private static EEconFilterMode filterMode;

		// Token: 0x040028D0 RID: 10448
		private static ulong filterInstigator;

		// Token: 0x040028D1 RID: 10449
		private static bool hasLoaded;

		// Token: 0x040028D2 RID: 10450
		[CompilerGenerated]
		private static ClickedInventory <>f__mg$cache0;

		// Token: 0x040028D3 RID: 10451
		[CompilerGenerated]
		private static Entered <>f__mg$cache1;

		// Token: 0x040028D4 RID: 10452
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x040028D5 RID: 10453
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x040028D6 RID: 10454
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;

		// Token: 0x040028D7 RID: 10455
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;

		// Token: 0x040028D8 RID: 10456
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache6;

		// Token: 0x040028D9 RID: 10457
		[CompilerGenerated]
		private static Dragged <>f__mg$cache7;

		// Token: 0x040028DA RID: 10458
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache8;

		// Token: 0x040028DB RID: 10459
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache9;

		// Token: 0x040028DC RID: 10460
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheA;

		// Token: 0x040028DD RID: 10461
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheB;

		// Token: 0x040028DE RID: 10462
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheC;

		// Token: 0x040028DF RID: 10463
		[CompilerGenerated]
		private static TempSteamworksEconomy.InventoryExchanged <>f__mg$cacheD;

		// Token: 0x040028E0 RID: 10464
		[CompilerGenerated]
		private static TempSteamworksEconomy.InventoryRefreshed <>f__mg$cacheE;

		// Token: 0x040028E1 RID: 10465
		[CompilerGenerated]
		private static TempSteamworksEconomy.InventoryDropped <>f__mg$cacheF;

		// Token: 0x040028E2 RID: 10466
		[CompilerGenerated]
		private static CharacterUpdated <>f__mg$cache10;
	}
}
