﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using SDG.Provider;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200078D RID: 1933
	public class MenuWorkshopSubmitUI
	{
		// Token: 0x060037AF RID: 14255 RVA: 0x00188898 File Offset: 0x00186C98
		public MenuWorkshopSubmitUI()
		{
			MenuWorkshopSubmitUI.localization = Localization.read("/Menu/Workshop/MenuWorkshopSubmit.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Workshop/MenuWorkshopSubmit/MenuWorkshopSubmit.unity3d");
			MenuWorkshopSubmitUI.publishedButtons = new List<SleekButton>();
			TempSteamworksWorkshop workshopService = Provider.provider.workshopService;
			Delegate onPublishedAdded = workshopService.onPublishedAdded;
			if (MenuWorkshopSubmitUI.<>f__mg$cache1 == null)
			{
				MenuWorkshopSubmitUI.<>f__mg$cache1 = new TempSteamworksWorkshop.PublishedAdded(MenuWorkshopSubmitUI.onPublishedAdded);
			}
			workshopService.onPublishedAdded = (TempSteamworksWorkshop.PublishedAdded)Delegate.Combine(onPublishedAdded, MenuWorkshopSubmitUI.<>f__mg$cache1);
			TempSteamworksWorkshop workshopService2 = Provider.provider.workshopService;
			Delegate onPublishedRemoved = workshopService2.onPublishedRemoved;
			if (MenuWorkshopSubmitUI.<>f__mg$cache2 == null)
			{
				MenuWorkshopSubmitUI.<>f__mg$cache2 = new TempSteamworksWorkshop.PublishedRemoved(MenuWorkshopSubmitUI.onPublishedRemoved);
			}
			workshopService2.onPublishedRemoved = (TempSteamworksWorkshop.PublishedRemoved)Delegate.Combine(onPublishedRemoved, MenuWorkshopSubmitUI.<>f__mg$cache2);
			MenuWorkshopSubmitUI.container = new Sleek();
			MenuWorkshopSubmitUI.container.positionOffset_X = 10;
			MenuWorkshopSubmitUI.container.positionOffset_Y = 10;
			MenuWorkshopSubmitUI.container.positionScale_Y = 1f;
			MenuWorkshopSubmitUI.container.sizeOffset_X = -20;
			MenuWorkshopSubmitUI.container.sizeOffset_Y = -20;
			MenuWorkshopSubmitUI.container.sizeScale_X = 1f;
			MenuWorkshopSubmitUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuWorkshopSubmitUI.container);
			MenuWorkshopSubmitUI.active = false;
			MenuWorkshopSubmitUI.nameField = new SleekField();
			MenuWorkshopSubmitUI.nameField.positionOffset_X = -200;
			MenuWorkshopSubmitUI.nameField.positionOffset_Y = 100;
			MenuWorkshopSubmitUI.nameField.positionScale_X = 0.5f;
			MenuWorkshopSubmitUI.nameField.sizeOffset_X = 200;
			MenuWorkshopSubmitUI.nameField.sizeOffset_Y = 30;
			MenuWorkshopSubmitUI.nameField.maxLength = 24;
			MenuWorkshopSubmitUI.nameField.addLabel(MenuWorkshopSubmitUI.localization.format("Name_Field_Label"), ESleekSide.RIGHT);
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.nameField);
			MenuWorkshopSubmitUI.descriptionField = new SleekField();
			MenuWorkshopSubmitUI.descriptionField.positionOffset_X = -200;
			MenuWorkshopSubmitUI.descriptionField.positionOffset_Y = 140;
			MenuWorkshopSubmitUI.descriptionField.positionScale_X = 0.5f;
			MenuWorkshopSubmitUI.descriptionField.sizeOffset_X = 400;
			MenuWorkshopSubmitUI.descriptionField.sizeOffset_Y = 30;
			MenuWorkshopSubmitUI.descriptionField.maxLength = 128;
			MenuWorkshopSubmitUI.descriptionField.addLabel(MenuWorkshopSubmitUI.localization.format("Description_Field_Label"), ESleekSide.RIGHT);
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.descriptionField);
			MenuWorkshopSubmitUI.pathField = new SleekField();
			MenuWorkshopSubmitUI.pathField.positionOffset_X = -200;
			MenuWorkshopSubmitUI.pathField.positionOffset_Y = 180;
			MenuWorkshopSubmitUI.pathField.positionScale_X = 0.5f;
			MenuWorkshopSubmitUI.pathField.sizeOffset_X = 400;
			MenuWorkshopSubmitUI.pathField.sizeOffset_Y = 30;
			MenuWorkshopSubmitUI.pathField.maxLength = 128;
			MenuWorkshopSubmitUI.pathField.addLabel(MenuWorkshopSubmitUI.localization.format("Path_Field_Label"), ESleekSide.RIGHT);
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.pathField);
			MenuWorkshopSubmitUI.previewField = new SleekField();
			MenuWorkshopSubmitUI.previewField.positionOffset_X = -200;
			MenuWorkshopSubmitUI.previewField.positionOffset_Y = 220;
			MenuWorkshopSubmitUI.previewField.positionScale_X = 0.5f;
			MenuWorkshopSubmitUI.previewField.sizeOffset_X = 400;
			MenuWorkshopSubmitUI.previewField.sizeOffset_Y = 30;
			MenuWorkshopSubmitUI.previewField.maxLength = 128;
			MenuWorkshopSubmitUI.previewField.addLabel(MenuWorkshopSubmitUI.localization.format("Preview_Field_Label"), ESleekSide.RIGHT);
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.previewField);
			MenuWorkshopSubmitUI.changeField = new SleekField();
			MenuWorkshopSubmitUI.changeField.positionOffset_X = -200;
			MenuWorkshopSubmitUI.changeField.positionOffset_Y = 260;
			MenuWorkshopSubmitUI.changeField.positionScale_X = 0.5f;
			MenuWorkshopSubmitUI.changeField.sizeOffset_X = 400;
			MenuWorkshopSubmitUI.changeField.sizeOffset_Y = 30;
			MenuWorkshopSubmitUI.changeField.maxLength = 128;
			MenuWorkshopSubmitUI.changeField.addLabel(MenuWorkshopSubmitUI.localization.format("Change_Field_Label"), ESleekSide.RIGHT);
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.changeField);
			MenuWorkshopSubmitUI.typeState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Map")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Localization")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Object")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Vehicle")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin"))
			});
			MenuWorkshopSubmitUI.typeState.positionOffset_X = -200;
			MenuWorkshopSubmitUI.typeState.positionOffset_Y = 300;
			MenuWorkshopSubmitUI.typeState.positionScale_X = 0.5f;
			MenuWorkshopSubmitUI.typeState.sizeOffset_X = 195;
			MenuWorkshopSubmitUI.typeState.sizeOffset_Y = 30;
			SleekButtonState sleekButtonState = MenuWorkshopSubmitUI.typeState;
			if (MenuWorkshopSubmitUI.<>f__mg$cache3 == null)
			{
				MenuWorkshopSubmitUI.<>f__mg$cache3 = new SwappedState(MenuWorkshopSubmitUI.onSwappedTypeState);
			}
			sleekButtonState.onSwappedState = MenuWorkshopSubmitUI.<>f__mg$cache3;
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.typeState);
			MenuWorkshopSubmitUI.mapTypeState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Map_Type_Survival")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Map_Type_Horde")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Map_Type_Arena"))
			});
			MenuWorkshopSubmitUI.mapTypeState.positionOffset_X = 5;
			MenuWorkshopSubmitUI.mapTypeState.positionOffset_Y = 300;
			MenuWorkshopSubmitUI.mapTypeState.positionScale_X = 0.5f;
			MenuWorkshopSubmitUI.mapTypeState.sizeOffset_X = 195;
			MenuWorkshopSubmitUI.mapTypeState.sizeOffset_Y = 30;
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.mapTypeState);
			MenuWorkshopSubmitUI.mapTypeState.isVisible = true;
			MenuWorkshopSubmitUI.itemTypeState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Backpack")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Barrel")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Barricade")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Fisher")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Food")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Fuel")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Glasses")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Grip")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Grower")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Gun")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Hat")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Magazine")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Mask")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Medical")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Melee")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Optic")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Shirt")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Sight")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Structure")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Supply")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Tactical")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Throwable")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Tool")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Vest")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Item_Type_Water"))
			});
			MenuWorkshopSubmitUI.itemTypeState.positionOffset_X = 5;
			MenuWorkshopSubmitUI.itemTypeState.positionOffset_Y = 300;
			MenuWorkshopSubmitUI.itemTypeState.positionScale_X = 0.5f;
			MenuWorkshopSubmitUI.itemTypeState.sizeOffset_X = 195;
			MenuWorkshopSubmitUI.itemTypeState.sizeOffset_Y = 30;
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.itemTypeState);
			MenuWorkshopSubmitUI.itemTypeState.isVisible = false;
			MenuWorkshopSubmitUI.vehicleTypeState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Vehicle_Type_Wheels_2")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Vehicle_Type_Wheels_4")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Vehicle_Type_Plane")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Vehicle_Type_Helicopter")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Vehicle_Type_Boat")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Vehicle_Type_Train"))
			});
			MenuWorkshopSubmitUI.vehicleTypeState.positionOffset_X = 5;
			MenuWorkshopSubmitUI.vehicleTypeState.positionOffset_Y = 300;
			MenuWorkshopSubmitUI.vehicleTypeState.positionScale_X = 0.5f;
			MenuWorkshopSubmitUI.vehicleTypeState.sizeOffset_X = 195;
			MenuWorkshopSubmitUI.vehicleTypeState.sizeOffset_Y = 30;
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.vehicleTypeState);
			MenuWorkshopSubmitUI.vehicleTypeState.isVisible = false;
			MenuWorkshopSubmitUI.skinTypeState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Generic_Pattern")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Ace")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Augewehr")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Avenger")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Bluntforce")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Bulldog")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Butterfly_Knife")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Calling_Card")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Cobra")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Colt")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Compound_Bow")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Crossbow")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Desert_Falcon")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Dragonfang")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Eaglefire")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Ekho")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Fusilaut")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Grizzly")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Hawkhound")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Heartbreaker")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Hell_Fury")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Honeybadger")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Katana")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Kryzkarek")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Machete")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Maplestrike")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Maschinengewehr")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Masterkey")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Matamorez")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Military_Knife")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Nightraider")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Nykorev")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Peacemaker")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Rocket_Launcher")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Sabertooth")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Scalar")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Schofield")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Shadowstalker")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Snayperskya")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Sportshot")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Teklowvka")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Timberwolf")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Viper")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Vonya")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Yuri")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Skin_Type_Zubeknakov"))
			});
			MenuWorkshopSubmitUI.skinTypeState.positionOffset_X = 5;
			MenuWorkshopSubmitUI.skinTypeState.positionOffset_Y = 300;
			MenuWorkshopSubmitUI.skinTypeState.positionScale_X = 0.5f;
			MenuWorkshopSubmitUI.skinTypeState.sizeOffset_X = 195;
			MenuWorkshopSubmitUI.skinTypeState.sizeOffset_Y = 30;
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.skinTypeState);
			MenuWorkshopSubmitUI.skinTypeState.isVisible = false;
			MenuWorkshopSubmitUI.objectTypeState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Object_Type_Model")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Object_Type_Resource")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Object_Type_Effect")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Object_Type_Animal"))
			});
			MenuWorkshopSubmitUI.objectTypeState.positionOffset_X = 5;
			MenuWorkshopSubmitUI.objectTypeState.positionOffset_Y = 300;
			MenuWorkshopSubmitUI.objectTypeState.positionScale_X = 0.5f;
			MenuWorkshopSubmitUI.objectTypeState.sizeOffset_X = 195;
			MenuWorkshopSubmitUI.objectTypeState.sizeOffset_Y = 30;
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.objectTypeState);
			MenuWorkshopSubmitUI.objectTypeState.isVisible = false;
			MenuWorkshopSubmitUI.visibilityState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Public")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Friends_Only")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Private"))
			});
			MenuWorkshopSubmitUI.visibilityState.positionOffset_X = -200;
			MenuWorkshopSubmitUI.visibilityState.positionOffset_Y = 340;
			MenuWorkshopSubmitUI.visibilityState.positionScale_X = 0.5f;
			MenuWorkshopSubmitUI.visibilityState.sizeOffset_X = 200;
			MenuWorkshopSubmitUI.visibilityState.sizeOffset_Y = 30;
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.visibilityState);
			MenuWorkshopSubmitUI.forState = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Community")),
				new GUIContent(MenuWorkshopSubmitUI.localization.format("Review"))
			});
			MenuWorkshopSubmitUI.forState.positionOffset_X = -200;
			MenuWorkshopSubmitUI.forState.positionOffset_Y = 380;
			MenuWorkshopSubmitUI.forState.positionScale_X = 0.5f;
			MenuWorkshopSubmitUI.forState.sizeOffset_X = 200;
			MenuWorkshopSubmitUI.forState.sizeOffset_Y = 30;
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.forState);
			MenuWorkshopSubmitUI.createButton = new SleekButtonIcon((Texture2D)bundle.load("Create"));
			MenuWorkshopSubmitUI.createButton.positionOffset_X = -200;
			MenuWorkshopSubmitUI.createButton.positionOffset_Y = 420;
			MenuWorkshopSubmitUI.createButton.positionScale_X = 0.5f;
			MenuWorkshopSubmitUI.createButton.sizeOffset_X = 195;
			MenuWorkshopSubmitUI.createButton.sizeOffset_Y = 30;
			MenuWorkshopSubmitUI.createButton.text = MenuWorkshopSubmitUI.localization.format("Create_Button");
			MenuWorkshopSubmitUI.createButton.tooltip = MenuWorkshopSubmitUI.localization.format("Create_Button_Tooltip");
			SleekButton sleekButton = MenuWorkshopSubmitUI.createButton;
			if (MenuWorkshopSubmitUI.<>f__mg$cache4 == null)
			{
				MenuWorkshopSubmitUI.<>f__mg$cache4 = new ClickedButton(MenuWorkshopSubmitUI.onClickedCreateButton);
			}
			sleekButton.onClickedButton = MenuWorkshopSubmitUI.<>f__mg$cache4;
			MenuWorkshopSubmitUI.createButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.createButton);
			MenuWorkshopSubmitUI.legalButton = new SleekButton();
			MenuWorkshopSubmitUI.legalButton.positionOffset_X = 5;
			MenuWorkshopSubmitUI.legalButton.positionOffset_Y = 420;
			MenuWorkshopSubmitUI.legalButton.positionScale_X = 0.5f;
			MenuWorkshopSubmitUI.legalButton.sizeOffset_X = 195;
			MenuWorkshopSubmitUI.legalButton.sizeOffset_Y = 30;
			MenuWorkshopSubmitUI.legalButton.fontSize = 10;
			MenuWorkshopSubmitUI.legalButton.text = MenuWorkshopSubmitUI.localization.format("Legal_Button");
			MenuWorkshopSubmitUI.legalButton.tooltip = MenuWorkshopSubmitUI.localization.format("Legal_Button_Tooltip");
			SleekButton sleekButton2 = MenuWorkshopSubmitUI.legalButton;
			if (MenuWorkshopSubmitUI.<>f__mg$cache5 == null)
			{
				MenuWorkshopSubmitUI.<>f__mg$cache5 = new ClickedButton(MenuWorkshopSubmitUI.onClickedLegalButton);
			}
			sleekButton2.onClickedButton = MenuWorkshopSubmitUI.<>f__mg$cache5;
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.legalButton);
			MenuWorkshopSubmitUI.publishedBox = new SleekScrollBox();
			MenuWorkshopSubmitUI.publishedBox.positionOffset_X = -200;
			MenuWorkshopSubmitUI.publishedBox.positionOffset_Y = 460;
			MenuWorkshopSubmitUI.publishedBox.positionScale_X = 0.5f;
			MenuWorkshopSubmitUI.publishedBox.sizeOffset_X = 430;
			MenuWorkshopSubmitUI.publishedBox.sizeOffset_Y = -460;
			MenuWorkshopSubmitUI.publishedBox.sizeScale_Y = 1f;
			MenuWorkshopSubmitUI.publishedBox.area = new Rect(0f, 0f, 5f, 0f);
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.publishedBox);
			MenuWorkshopSubmitUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuWorkshopSubmitUI.backButton.positionOffset_Y = -50;
			MenuWorkshopSubmitUI.backButton.positionScale_Y = 1f;
			MenuWorkshopSubmitUI.backButton.sizeOffset_X = 200;
			MenuWorkshopSubmitUI.backButton.sizeOffset_Y = 50;
			MenuWorkshopSubmitUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuWorkshopSubmitUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton3 = MenuWorkshopSubmitUI.backButton;
			if (MenuWorkshopSubmitUI.<>f__mg$cache6 == null)
			{
				MenuWorkshopSubmitUI.<>f__mg$cache6 = new ClickedButton(MenuWorkshopSubmitUI.onClickedBackButton);
			}
			sleekButton3.onClickedButton = MenuWorkshopSubmitUI.<>f__mg$cache6;
			MenuWorkshopSubmitUI.backButton.fontSize = 14;
			MenuWorkshopSubmitUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuWorkshopSubmitUI.container.add(MenuWorkshopSubmitUI.backButton);
			MenuWorkshopSubmitUI.onPublishedAdded();
			bundle.unload();
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x00189BC6 File Offset: 0x00187FC6
		public static void open()
		{
			if (MenuWorkshopSubmitUI.active)
			{
				return;
			}
			MenuWorkshopSubmitUI.active = true;
			MenuWorkshopSubmitUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x00189BF3 File Offset: 0x00187FF3
		public static void close()
		{
			if (!MenuWorkshopSubmitUI.active)
			{
				return;
			}
			MenuWorkshopSubmitUI.active = false;
			MenuWorkshopSubmitUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060037B2 RID: 14258 RVA: 0x00189C20 File Offset: 0x00188020
		private static string tag
		{
			get
			{
				switch (MenuWorkshopSubmitUI.typeState.state)
				{
				case 0:
					return MenuWorkshopSubmitUI.mapTypeState.states[MenuWorkshopSubmitUI.mapTypeState.state].text;
				case 2:
					return MenuWorkshopSubmitUI.objectTypeState.states[MenuWorkshopSubmitUI.objectTypeState.state].text;
				case 3:
					return MenuWorkshopSubmitUI.itemTypeState.states[MenuWorkshopSubmitUI.itemTypeState.state].text;
				case 4:
					return MenuWorkshopSubmitUI.vehicleTypeState.states[MenuWorkshopSubmitUI.vehicleTypeState.state].text;
				case 5:
					return MenuWorkshopSubmitUI.skinTypeState.states[MenuWorkshopSubmitUI.skinTypeState.state].text;
				}
				return string.Empty;
			}
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x00189CE8 File Offset: 0x001880E8
		private static void onClickedCreateButton(SleekButton button)
		{
			if (MenuWorkshopSubmitUI.checkEntered() && MenuWorkshopSubmitUI.checkValid())
			{
				Provider.provider.workshopService.prepareUGC(MenuWorkshopSubmitUI.nameField.text, MenuWorkshopSubmitUI.descriptionField.text, MenuWorkshopSubmitUI.pathField.text, MenuWorkshopSubmitUI.previewField.text, MenuWorkshopSubmitUI.changeField.text, (ESteamUGCType)MenuWorkshopSubmitUI.typeState.state, MenuWorkshopSubmitUI.tag, (ESteamUGCVisibility)MenuWorkshopSubmitUI.visibilityState.state);
				Provider.provider.workshopService.createUGC(MenuWorkshopSubmitUI.forState.state == 1);
				MenuWorkshopSubmitUI.resetFields();
			}
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x00189D84 File Offset: 0x00188184
		private static void onClickedLegalButton(SleekButton button)
		{
			if (!Provider.provider.browserService.canOpenBrowser)
			{
				MenuUI.alert(MenuWorkshopSubmitUI.localization.format("Overlay"));
				return;
			}
			Provider.provider.browserService.open("http://steamcommunity.com/sharedfiles/workshoplegalagreement/?appid=304930");
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x00189DC4 File Offset: 0x001881C4
		private static void onClickedPublished(SleekButton button)
		{
			int index = button.positionOffset_Y / 40;
			if (MenuWorkshopSubmitUI.checkValid())
			{
				Provider.provider.workshopService.prepareUGC(MenuWorkshopSubmitUI.nameField.text, MenuWorkshopSubmitUI.descriptionField.text, MenuWorkshopSubmitUI.pathField.text, MenuWorkshopSubmitUI.previewField.text, MenuWorkshopSubmitUI.changeField.text, (ESteamUGCType)MenuWorkshopSubmitUI.typeState.state, MenuWorkshopSubmitUI.tag, (ESteamUGCVisibility)MenuWorkshopSubmitUI.visibilityState.state);
				Provider.provider.workshopService.prepareUGC(Provider.provider.workshopService.published[index].id);
				Provider.provider.workshopService.updateUGC();
				MenuWorkshopSubmitUI.resetFields();
			}
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x00189E7C File Offset: 0x0018827C
		private static void onPublishedAdded()
		{
			for (int i = 0; i < Provider.provider.workshopService.published.Count; i++)
			{
				SteamPublished steamPublished = Provider.provider.workshopService.published[i];
				SleekButton sleekButton = new SleekButton();
				sleekButton.positionOffset_Y = i * 40;
				sleekButton.sizeOffset_X = -30;
				sleekButton.sizeOffset_Y = 30;
				sleekButton.sizeScale_X = 1f;
				sleekButton.text = steamPublished.name;
				SleekButton sleekButton2 = sleekButton;
				if (MenuWorkshopSubmitUI.<>f__mg$cache0 == null)
				{
					MenuWorkshopSubmitUI.<>f__mg$cache0 = new ClickedButton(MenuWorkshopSubmitUI.onClickedPublished);
				}
				sleekButton2.onClickedButton = MenuWorkshopSubmitUI.<>f__mg$cache0;
				MenuWorkshopSubmitUI.publishedBox.add(sleekButton);
				MenuWorkshopSubmitUI.publishedButtons.Add(sleekButton);
				MenuWorkshopSubmitUI.publishedBox.area = new Rect(0f, 0f, 5f, (float)(MenuWorkshopSubmitUI.publishedButtons.Count * 40 - 10));
			}
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x00189F63 File Offset: 0x00188363
		private static void onPublishedRemoved()
		{
			MenuWorkshopSubmitUI.publishedBox.remove();
			MenuWorkshopSubmitUI.publishedButtons.Clear();
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x00189F7C File Offset: 0x0018837C
		private static bool checkEntered()
		{
			if (MenuWorkshopSubmitUI.nameField.text.Length == 0)
			{
				MenuUI.alert(MenuWorkshopSubmitUI.localization.format("Alert_Name"));
				return false;
			}
			if (MenuWorkshopSubmitUI.previewField.text.Length == 0 || !ReadWrite.fileExists(MenuWorkshopSubmitUI.previewField.text, false, false) || new FileInfo(MenuWorkshopSubmitUI.previewField.text).Length > 1000000L)
			{
				MenuUI.alert(MenuWorkshopSubmitUI.localization.format("Alert_Preview"));
				return false;
			}
			return true;
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x0018A014 File Offset: 0x00188414
		private static bool checkValid()
		{
			if (MenuWorkshopSubmitUI.pathField.text.Length == 0 || !ReadWrite.folderExists(MenuWorkshopSubmitUI.pathField.text, false))
			{
				MenuUI.alert(MenuWorkshopSubmitUI.localization.format("Alert_Path"));
				return false;
			}
			ESteamUGCType state = (ESteamUGCType)MenuWorkshopSubmitUI.typeState.state;
			bool flag = MenuWorkshopSubmitUI.forState.state == 1;
			if (flag)
			{
				if (state != ESteamUGCType.ITEM && state != ESteamUGCType.SKIN)
				{
					MenuUI.alert(MenuWorkshopSubmitUI.localization.format("Alert_Curated"));
					return false;
				}
			}
			else if (state == ESteamUGCType.SKIN)
			{
				MenuUI.alert(MenuWorkshopSubmitUI.localization.format("Alert_Curated"));
				return false;
			}
			bool flag2 = false;
			if (state == ESteamUGCType.MAP)
			{
				flag2 = WorkshopTool.checkMapValid(MenuWorkshopSubmitUI.pathField.text, false);
				if (!flag2)
				{
					MenuUI.alert(MenuWorkshopSubmitUI.localization.format("Alert_Map"));
				}
			}
			else if (state == ESteamUGCType.LOCALIZATION)
			{
				flag2 = WorkshopTool.checkLocalizationValid(MenuWorkshopSubmitUI.pathField.text, false);
				if (!flag2)
				{
					MenuUI.alert(MenuWorkshopSubmitUI.localization.format("Alert_Localization"));
				}
			}
			else if (state == ESteamUGCType.OBJECT || state == ESteamUGCType.ITEM || state == ESteamUGCType.VEHICLE || state == ESteamUGCType.SKIN)
			{
				flag2 = WorkshopTool.checkBundleValid(MenuWorkshopSubmitUI.pathField.text, false);
				if (!flag2)
				{
					MenuUI.alert(MenuWorkshopSubmitUI.localization.format("Alert_Object"));
				}
			}
			return flag2;
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x0018A17C File Offset: 0x0018857C
		private static void resetFields()
		{
			MenuWorkshopSubmitUI.nameField.text = string.Empty;
			MenuWorkshopSubmitUI.descriptionField.text = string.Empty;
			MenuWorkshopSubmitUI.pathField.text = string.Empty;
			MenuWorkshopSubmitUI.previewField.text = string.Empty;
			MenuWorkshopSubmitUI.changeField.text = string.Empty;
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x0018A1D4 File Offset: 0x001885D4
		private static void onSwappedTypeState(SleekButtonState button, int state)
		{
			MenuWorkshopSubmitUI.mapTypeState.isVisible = (state == 0);
			MenuWorkshopSubmitUI.itemTypeState.isVisible = (state == 3);
			MenuWorkshopSubmitUI.vehicleTypeState.isVisible = (state == 4);
			MenuWorkshopSubmitUI.skinTypeState.isVisible = (state == 5);
			MenuWorkshopSubmitUI.objectTypeState.isVisible = (state == 2);
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x0018A229 File Offset: 0x00188629
		private static void onClickedBackButton(SleekButton button)
		{
			MenuWorkshopUI.open();
			MenuWorkshopSubmitUI.close();
		}

		// Token: 0x04002957 RID: 10583
		private static Local localization;

		// Token: 0x04002958 RID: 10584
		private static Sleek container;

		// Token: 0x04002959 RID: 10585
		public static bool active;

		// Token: 0x0400295A RID: 10586
		private static SleekButtonIcon backButton;

		// Token: 0x0400295B RID: 10587
		private static SleekField nameField;

		// Token: 0x0400295C RID: 10588
		private static SleekField descriptionField;

		// Token: 0x0400295D RID: 10589
		private static SleekField pathField;

		// Token: 0x0400295E RID: 10590
		private static SleekField previewField;

		// Token: 0x0400295F RID: 10591
		private static SleekField changeField;

		// Token: 0x04002960 RID: 10592
		private static SleekButtonState typeState;

		// Token: 0x04002961 RID: 10593
		private static SleekButtonState mapTypeState;

		// Token: 0x04002962 RID: 10594
		private static SleekButtonState itemTypeState;

		// Token: 0x04002963 RID: 10595
		private static SleekButtonState vehicleTypeState;

		// Token: 0x04002964 RID: 10596
		private static SleekButtonState skinTypeState;

		// Token: 0x04002965 RID: 10597
		private static SleekButtonState objectTypeState;

		// Token: 0x04002966 RID: 10598
		private static SleekButtonState visibilityState;

		// Token: 0x04002967 RID: 10599
		private static SleekButtonState forState;

		// Token: 0x04002968 RID: 10600
		private static SleekButtonIcon createButton;

		// Token: 0x04002969 RID: 10601
		private static SleekButton legalButton;

		// Token: 0x0400296A RID: 10602
		private static SleekScrollBox publishedBox;

		// Token: 0x0400296B RID: 10603
		private static List<SleekButton> publishedButtons;

		// Token: 0x0400296C RID: 10604
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x0400296D RID: 10605
		[CompilerGenerated]
		private static TempSteamworksWorkshop.PublishedAdded <>f__mg$cache1;

		// Token: 0x0400296E RID: 10606
		[CompilerGenerated]
		private static TempSteamworksWorkshop.PublishedRemoved <>f__mg$cache2;

		// Token: 0x0400296F RID: 10607
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache3;

		// Token: 0x04002970 RID: 10608
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;

		// Token: 0x04002971 RID: 10609
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;

		// Token: 0x04002972 RID: 10610
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache6;
	}
}
