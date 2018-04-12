﻿using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200075E RID: 1886
	public class EditorSpawnsZombiesUI
	{
		// Token: 0x06003574 RID: 13684 RVA: 0x001659B4 File Offset: 0x00163DB4
		public EditorSpawnsZombiesUI()
		{
			EditorSpawnsZombiesUI.localization = Localization.read("/Editor/EditorSpawnsZombies.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Edit/Icons/EditorSpawnsZombies/EditorSpawnsZombies.unity3d");
			EditorSpawnsZombiesUI.container = new Sleek();
			EditorSpawnsZombiesUI.container.positionOffset_X = 10;
			EditorSpawnsZombiesUI.container.positionOffset_Y = 10;
			EditorSpawnsZombiesUI.container.positionScale_X = 1f;
			EditorSpawnsZombiesUI.container.sizeOffset_X = -20;
			EditorSpawnsZombiesUI.container.sizeOffset_Y = -20;
			EditorSpawnsZombiesUI.container.sizeScale_X = 1f;
			EditorSpawnsZombiesUI.container.sizeScale_Y = 1f;
			EditorUI.window.add(EditorSpawnsZombiesUI.container);
			EditorSpawnsZombiesUI.active = false;
			EditorSpawnsZombiesUI.tableScrollBox = new SleekScrollBox();
			EditorSpawnsZombiesUI.tableScrollBox.positionOffset_X = -470;
			EditorSpawnsZombiesUI.tableScrollBox.positionOffset_Y = 120;
			EditorSpawnsZombiesUI.tableScrollBox.positionScale_X = 1f;
			EditorSpawnsZombiesUI.tableScrollBox.sizeOffset_X = 470;
			EditorSpawnsZombiesUI.tableScrollBox.sizeOffset_Y = 200;
			EditorSpawnsZombiesUI.container.add(EditorSpawnsZombiesUI.tableScrollBox);
			EditorSpawnsZombiesUI.tableNameField = new SleekField();
			EditorSpawnsZombiesUI.tableNameField.positionOffset_X = -230;
			EditorSpawnsZombiesUI.tableNameField.positionOffset_Y = 330;
			EditorSpawnsZombiesUI.tableNameField.positionScale_X = 1f;
			EditorSpawnsZombiesUI.tableNameField.sizeOffset_X = 230;
			EditorSpawnsZombiesUI.tableNameField.sizeOffset_Y = 30;
			EditorSpawnsZombiesUI.tableNameField.maxLength = 64;
			EditorSpawnsZombiesUI.tableNameField.addLabel(EditorSpawnsZombiesUI.localization.format("TableNameFieldLabelText"), ESleekSide.LEFT);
			SleekField sleekField = EditorSpawnsZombiesUI.tableNameField;
			Delegate onTyped = sleekField.onTyped;
			if (EditorSpawnsZombiesUI.<>f__mg$cache4 == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cache4 = new Typed(EditorSpawnsZombiesUI.onTypedNameField);
			}
			sleekField.onTyped = (Typed)Delegate.Combine(onTyped, EditorSpawnsZombiesUI.<>f__mg$cache4);
			EditorSpawnsZombiesUI.container.add(EditorSpawnsZombiesUI.tableNameField);
			EditorSpawnsZombiesUI.addTableButton = new SleekButtonIcon((Texture2D)bundle.load("Add"));
			EditorSpawnsZombiesUI.addTableButton.positionOffset_X = -230;
			EditorSpawnsZombiesUI.addTableButton.positionOffset_Y = 370;
			EditorSpawnsZombiesUI.addTableButton.positionScale_X = 1f;
			EditorSpawnsZombiesUI.addTableButton.sizeOffset_X = 110;
			EditorSpawnsZombiesUI.addTableButton.sizeOffset_Y = 30;
			EditorSpawnsZombiesUI.addTableButton.text = EditorSpawnsZombiesUI.localization.format("AddTableButtonText");
			EditorSpawnsZombiesUI.addTableButton.tooltip = EditorSpawnsZombiesUI.localization.format("AddTableButtonTooltip");
			SleekButton sleekButton = EditorSpawnsZombiesUI.addTableButton;
			if (EditorSpawnsZombiesUI.<>f__mg$cache5 == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cache5 = new ClickedButton(EditorSpawnsZombiesUI.onClickedAddTableButton);
			}
			sleekButton.onClickedButton = EditorSpawnsZombiesUI.<>f__mg$cache5;
			EditorSpawnsZombiesUI.container.add(EditorSpawnsZombiesUI.addTableButton);
			EditorSpawnsZombiesUI.removeTableButton = new SleekButtonIcon((Texture2D)bundle.load("Remove"));
			EditorSpawnsZombiesUI.removeTableButton.positionOffset_X = -110;
			EditorSpawnsZombiesUI.removeTableButton.positionOffset_Y = 370;
			EditorSpawnsZombiesUI.removeTableButton.positionScale_X = 1f;
			EditorSpawnsZombiesUI.removeTableButton.sizeOffset_X = 110;
			EditorSpawnsZombiesUI.removeTableButton.sizeOffset_Y = 30;
			EditorSpawnsZombiesUI.removeTableButton.text = EditorSpawnsZombiesUI.localization.format("RemoveTableButtonText");
			EditorSpawnsZombiesUI.removeTableButton.tooltip = EditorSpawnsZombiesUI.localization.format("RemoveTableButtonTooltip");
			SleekButton sleekButton2 = EditorSpawnsZombiesUI.removeTableButton;
			if (EditorSpawnsZombiesUI.<>f__mg$cache6 == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cache6 = new ClickedButton(EditorSpawnsZombiesUI.onClickedRemoveTableButton);
			}
			sleekButton2.onClickedButton = EditorSpawnsZombiesUI.<>f__mg$cache6;
			EditorSpawnsZombiesUI.container.add(EditorSpawnsZombiesUI.removeTableButton);
			EditorSpawnsZombiesUI.updateTables();
			EditorSpawnsZombiesUI.spawnsScrollBox = new SleekScrollBox();
			EditorSpawnsZombiesUI.spawnsScrollBox.positionOffset_X = -470;
			EditorSpawnsZombiesUI.spawnsScrollBox.positionOffset_Y = 410;
			EditorSpawnsZombiesUI.spawnsScrollBox.positionScale_X = 1f;
			EditorSpawnsZombiesUI.spawnsScrollBox.sizeOffset_X = 470;
			EditorSpawnsZombiesUI.spawnsScrollBox.sizeOffset_Y = -410;
			EditorSpawnsZombiesUI.spawnsScrollBox.sizeScale_Y = 1f;
			EditorSpawnsZombiesUI.spawnsScrollBox.area = new Rect(0f, 0f, 5f, 1000f);
			EditorSpawnsZombiesUI.container.add(EditorSpawnsZombiesUI.spawnsScrollBox);
			EditorSpawnsZombiesUI.tableColorPicker = new SleekColorPicker();
			EditorSpawnsZombiesUI.tableColorPicker.positionOffset_X = 200;
			SleekColorPicker sleekColorPicker = EditorSpawnsZombiesUI.tableColorPicker;
			if (EditorSpawnsZombiesUI.<>f__mg$cache7 == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cache7 = new ColorPicked(EditorSpawnsZombiesUI.onZombieColorPicked);
			}
			sleekColorPicker.onColorPicked = EditorSpawnsZombiesUI.<>f__mg$cache7;
			EditorSpawnsZombiesUI.spawnsScrollBox.add(EditorSpawnsZombiesUI.tableColorPicker);
			EditorSpawnsZombiesUI.megaToggle = new SleekToggle();
			EditorSpawnsZombiesUI.megaToggle.positionOffset_X = 240;
			EditorSpawnsZombiesUI.megaToggle.positionOffset_Y = 130;
			EditorSpawnsZombiesUI.megaToggle.sizeOffset_X = 40;
			EditorSpawnsZombiesUI.megaToggle.sizeOffset_Y = 40;
			SleekToggle sleekToggle = EditorSpawnsZombiesUI.megaToggle;
			if (EditorSpawnsZombiesUI.<>f__mg$cache8 == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cache8 = new Toggled(EditorSpawnsZombiesUI.onToggledMegaToggle);
			}
			sleekToggle.onToggled = EditorSpawnsZombiesUI.<>f__mg$cache8;
			EditorSpawnsZombiesUI.megaToggle.addLabel(EditorSpawnsZombiesUI.localization.format("MegaToggleLabelText"), ESleekSide.LEFT);
			EditorSpawnsZombiesUI.spawnsScrollBox.add(EditorSpawnsZombiesUI.megaToggle);
			EditorSpawnsZombiesUI.healthField = new SleekUInt16Field();
			EditorSpawnsZombiesUI.healthField.positionOffset_X = 240;
			EditorSpawnsZombiesUI.healthField.positionOffset_Y = 180;
			EditorSpawnsZombiesUI.healthField.sizeOffset_X = 200;
			EditorSpawnsZombiesUI.healthField.sizeOffset_Y = 30;
			SleekUInt16Field sleekUInt16Field = EditorSpawnsZombiesUI.healthField;
			if (EditorSpawnsZombiesUI.<>f__mg$cache9 == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cache9 = new TypedUInt16(EditorSpawnsZombiesUI.onHealthFieldTyped);
			}
			sleekUInt16Field.onTypedUInt16 = EditorSpawnsZombiesUI.<>f__mg$cache9;
			EditorSpawnsZombiesUI.healthField.addLabel(EditorSpawnsZombiesUI.localization.format("HealthFieldLabelText"), ESleekSide.LEFT);
			EditorSpawnsZombiesUI.spawnsScrollBox.add(EditorSpawnsZombiesUI.healthField);
			EditorSpawnsZombiesUI.damageField = new SleekByteField();
			EditorSpawnsZombiesUI.damageField.positionOffset_X = 240;
			EditorSpawnsZombiesUI.damageField.positionOffset_Y = 220;
			EditorSpawnsZombiesUI.damageField.sizeOffset_X = 200;
			EditorSpawnsZombiesUI.damageField.sizeOffset_Y = 30;
			SleekByteField sleekByteField = EditorSpawnsZombiesUI.damageField;
			if (EditorSpawnsZombiesUI.<>f__mg$cacheA == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cacheA = new TypedByte(EditorSpawnsZombiesUI.onDamageFieldTyped);
			}
			sleekByteField.onTypedByte = EditorSpawnsZombiesUI.<>f__mg$cacheA;
			EditorSpawnsZombiesUI.damageField.addLabel(EditorSpawnsZombiesUI.localization.format("DamageFieldLabelText"), ESleekSide.LEFT);
			EditorSpawnsZombiesUI.spawnsScrollBox.add(EditorSpawnsZombiesUI.damageField);
			EditorSpawnsZombiesUI.lootIndexField = new SleekByteField();
			EditorSpawnsZombiesUI.lootIndexField.positionOffset_X = 240;
			EditorSpawnsZombiesUI.lootIndexField.positionOffset_Y = 260;
			EditorSpawnsZombiesUI.lootIndexField.sizeOffset_X = 200;
			EditorSpawnsZombiesUI.lootIndexField.sizeOffset_Y = 30;
			SleekByteField sleekByteField2 = EditorSpawnsZombiesUI.lootIndexField;
			if (EditorSpawnsZombiesUI.<>f__mg$cacheB == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cacheB = new TypedByte(EditorSpawnsZombiesUI.onLootIndexFieldTyped);
			}
			sleekByteField2.onTypedByte = EditorSpawnsZombiesUI.<>f__mg$cacheB;
			EditorSpawnsZombiesUI.lootIndexField.addLabel(EditorSpawnsZombiesUI.localization.format("LootIndexFieldLabelText"), ESleekSide.LEFT);
			EditorSpawnsZombiesUI.spawnsScrollBox.add(EditorSpawnsZombiesUI.lootIndexField);
			EditorSpawnsZombiesUI.lootIDField = new SleekUInt16Field();
			EditorSpawnsZombiesUI.lootIDField.positionOffset_X = 240;
			EditorSpawnsZombiesUI.lootIDField.positionOffset_Y = 300;
			EditorSpawnsZombiesUI.lootIDField.sizeOffset_X = 200;
			EditorSpawnsZombiesUI.lootIDField.sizeOffset_Y = 30;
			SleekUInt16Field sleekUInt16Field2 = EditorSpawnsZombiesUI.lootIDField;
			if (EditorSpawnsZombiesUI.<>f__mg$cacheC == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cacheC = new TypedUInt16(EditorSpawnsZombiesUI.onLootIDFieldTyped);
			}
			sleekUInt16Field2.onTypedUInt16 = EditorSpawnsZombiesUI.<>f__mg$cacheC;
			EditorSpawnsZombiesUI.lootIDField.addLabel(EditorSpawnsZombiesUI.localization.format("LootIDFieldLabelText"), ESleekSide.LEFT);
			EditorSpawnsZombiesUI.spawnsScrollBox.add(EditorSpawnsZombiesUI.lootIDField);
			EditorSpawnsZombiesUI.xpField = new SleekUInt32Field();
			EditorSpawnsZombiesUI.xpField.positionOffset_X = 240;
			EditorSpawnsZombiesUI.xpField.positionOffset_Y = 340;
			EditorSpawnsZombiesUI.xpField.sizeOffset_X = 200;
			EditorSpawnsZombiesUI.xpField.sizeOffset_Y = 30;
			SleekUInt32Field sleekUInt32Field = EditorSpawnsZombiesUI.xpField;
			if (EditorSpawnsZombiesUI.<>f__mg$cacheD == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cacheD = new TypedUInt32(EditorSpawnsZombiesUI.onXPFieldTyped);
			}
			sleekUInt32Field.onTypedUInt32 = EditorSpawnsZombiesUI.<>f__mg$cacheD;
			EditorSpawnsZombiesUI.xpField.addLabel(EditorSpawnsZombiesUI.localization.format("XPFieldLabelText"), ESleekSide.LEFT);
			EditorSpawnsZombiesUI.spawnsScrollBox.add(EditorSpawnsZombiesUI.xpField);
			EditorSpawnsZombiesUI.regenField = new SleekSingleField();
			EditorSpawnsZombiesUI.regenField.positionOffset_X = 240;
			EditorSpawnsZombiesUI.regenField.positionOffset_Y = 380;
			EditorSpawnsZombiesUI.regenField.sizeOffset_X = 200;
			EditorSpawnsZombiesUI.regenField.sizeOffset_Y = 30;
			SleekSingleField sleekSingleField = EditorSpawnsZombiesUI.regenField;
			if (EditorSpawnsZombiesUI.<>f__mg$cacheE == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cacheE = new TypedSingle(EditorSpawnsZombiesUI.onRegenFieldTyped);
			}
			sleekSingleField.onTypedSingle = EditorSpawnsZombiesUI.<>f__mg$cacheE;
			EditorSpawnsZombiesUI.regenField.addLabel(EditorSpawnsZombiesUI.localization.format("RegenFieldLabelText"), ESleekSide.LEFT);
			EditorSpawnsZombiesUI.spawnsScrollBox.add(EditorSpawnsZombiesUI.regenField);
			EditorSpawnsZombiesUI.difficultyGUIDField = new SleekField();
			EditorSpawnsZombiesUI.difficultyGUIDField.positionOffset_X = 240;
			EditorSpawnsZombiesUI.difficultyGUIDField.positionOffset_Y = 420;
			EditorSpawnsZombiesUI.difficultyGUIDField.sizeOffset_X = 200;
			EditorSpawnsZombiesUI.difficultyGUIDField.sizeOffset_Y = 30;
			EditorSpawnsZombiesUI.difficultyGUIDField.maxLength = 32;
			SleekField sleekField2 = EditorSpawnsZombiesUI.difficultyGUIDField;
			if (EditorSpawnsZombiesUI.<>f__mg$cacheF == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cacheF = new Typed(EditorSpawnsZombiesUI.onDifficultyGUIDFieldTyped);
			}
			sleekField2.onTyped = EditorSpawnsZombiesUI.<>f__mg$cacheF;
			EditorSpawnsZombiesUI.difficultyGUIDField.addLabel(EditorSpawnsZombiesUI.localization.format("DifficultyGUIDFieldLabelText"), ESleekSide.LEFT);
			EditorSpawnsZombiesUI.spawnsScrollBox.add(EditorSpawnsZombiesUI.difficultyGUIDField);
			EditorSpawnsZombiesUI.itemIDField = new SleekUInt16Field();
			EditorSpawnsZombiesUI.itemIDField.positionOffset_X = 240;
			EditorSpawnsZombiesUI.itemIDField.sizeOffset_X = 200;
			EditorSpawnsZombiesUI.itemIDField.sizeOffset_Y = 30;
			EditorSpawnsZombiesUI.itemIDField.addLabel(EditorSpawnsZombiesUI.localization.format("ItemIDFieldLabelText"), ESleekSide.LEFT);
			EditorSpawnsZombiesUI.spawnsScrollBox.add(EditorSpawnsZombiesUI.itemIDField);
			EditorSpawnsZombiesUI.addItemButton = new SleekButtonIcon((Texture2D)bundle.load("Add"));
			EditorSpawnsZombiesUI.addItemButton.positionOffset_X = 240;
			EditorSpawnsZombiesUI.addItemButton.sizeOffset_X = 95;
			EditorSpawnsZombiesUI.addItemButton.sizeOffset_Y = 30;
			EditorSpawnsZombiesUI.addItemButton.text = EditorSpawnsZombiesUI.localization.format("AddItemButtonText");
			EditorSpawnsZombiesUI.addItemButton.tooltip = EditorSpawnsZombiesUI.localization.format("AddItemButtonTooltip");
			SleekButton sleekButton3 = EditorSpawnsZombiesUI.addItemButton;
			if (EditorSpawnsZombiesUI.<>f__mg$cache10 == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cache10 = new ClickedButton(EditorSpawnsZombiesUI.onClickedAddItemButton);
			}
			sleekButton3.onClickedButton = EditorSpawnsZombiesUI.<>f__mg$cache10;
			EditorSpawnsZombiesUI.spawnsScrollBox.add(EditorSpawnsZombiesUI.addItemButton);
			EditorSpawnsZombiesUI.removeItemButton = new SleekButtonIcon((Texture2D)bundle.load("Remove"));
			EditorSpawnsZombiesUI.removeItemButton.positionOffset_X = 345;
			EditorSpawnsZombiesUI.removeItemButton.sizeOffset_X = 95;
			EditorSpawnsZombiesUI.removeItemButton.sizeOffset_Y = 30;
			EditorSpawnsZombiesUI.removeItemButton.text = EditorSpawnsZombiesUI.localization.format("RemoveItemButtonText");
			EditorSpawnsZombiesUI.removeItemButton.tooltip = EditorSpawnsZombiesUI.localization.format("RemoveItemButtonTooltip");
			SleekButton sleekButton4 = EditorSpawnsZombiesUI.removeItemButton;
			if (EditorSpawnsZombiesUI.<>f__mg$cache11 == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cache11 = new ClickedButton(EditorSpawnsZombiesUI.onClickedRemoveItemButton);
			}
			sleekButton4.onClickedButton = EditorSpawnsZombiesUI.<>f__mg$cache11;
			EditorSpawnsZombiesUI.spawnsScrollBox.add(EditorSpawnsZombiesUI.removeItemButton);
			EditorSpawnsZombiesUI.selectedBox = new SleekBox();
			EditorSpawnsZombiesUI.selectedBox.positionOffset_X = -230;
			EditorSpawnsZombiesUI.selectedBox.positionOffset_Y = 80;
			EditorSpawnsZombiesUI.selectedBox.positionScale_X = 1f;
			EditorSpawnsZombiesUI.selectedBox.sizeOffset_X = 230;
			EditorSpawnsZombiesUI.selectedBox.sizeOffset_Y = 30;
			EditorSpawnsZombiesUI.selectedBox.addLabel(EditorSpawnsZombiesUI.localization.format("SelectionBoxLabelText"), ESleekSide.LEFT);
			EditorSpawnsZombiesUI.container.add(EditorSpawnsZombiesUI.selectedBox);
			EditorSpawnsZombiesUI.updateSelection();
			EditorSpawnsZombiesUI.radiusSlider = new SleekSlider();
			EditorSpawnsZombiesUI.radiusSlider.positionOffset_Y = -100;
			EditorSpawnsZombiesUI.radiusSlider.positionScale_Y = 1f;
			EditorSpawnsZombiesUI.radiusSlider.sizeOffset_X = 200;
			EditorSpawnsZombiesUI.radiusSlider.sizeOffset_Y = 20;
			EditorSpawnsZombiesUI.radiusSlider.state = (float)(EditorSpawns.radius - EditorSpawns.MIN_REMOVE_SIZE) / (float)EditorSpawns.MAX_REMOVE_SIZE;
			EditorSpawnsZombiesUI.radiusSlider.orientation = ESleekOrientation.HORIZONTAL;
			EditorSpawnsZombiesUI.radiusSlider.addLabel(EditorSpawnsZombiesUI.localization.format("RadiusSliderLabelText"), ESleekSide.RIGHT);
			SleekSlider sleekSlider = EditorSpawnsZombiesUI.radiusSlider;
			if (EditorSpawnsZombiesUI.<>f__mg$cache12 == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cache12 = new Dragged(EditorSpawnsZombiesUI.onDraggedRadiusSlider);
			}
			sleekSlider.onDragged = EditorSpawnsZombiesUI.<>f__mg$cache12;
			EditorSpawnsZombiesUI.container.add(EditorSpawnsZombiesUI.radiusSlider);
			EditorSpawnsZombiesUI.addButton = new SleekButtonIcon((Texture2D)bundle.load("Add"));
			EditorSpawnsZombiesUI.addButton.positionOffset_Y = -70;
			EditorSpawnsZombiesUI.addButton.positionScale_Y = 1f;
			EditorSpawnsZombiesUI.addButton.sizeOffset_X = 200;
			EditorSpawnsZombiesUI.addButton.sizeOffset_Y = 30;
			EditorSpawnsZombiesUI.addButton.text = EditorSpawnsZombiesUI.localization.format("AddButtonText", new object[]
			{
				ControlsSettings.tool_0
			});
			EditorSpawnsZombiesUI.addButton.tooltip = EditorSpawnsZombiesUI.localization.format("AddButtonTooltip");
			SleekButton sleekButton5 = EditorSpawnsZombiesUI.addButton;
			if (EditorSpawnsZombiesUI.<>f__mg$cache13 == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cache13 = new ClickedButton(EditorSpawnsZombiesUI.onClickedAddButton);
			}
			sleekButton5.onClickedButton = EditorSpawnsZombiesUI.<>f__mg$cache13;
			EditorSpawnsZombiesUI.container.add(EditorSpawnsZombiesUI.addButton);
			EditorSpawnsZombiesUI.removeButton = new SleekButtonIcon((Texture2D)bundle.load("Remove"));
			EditorSpawnsZombiesUI.removeButton.positionOffset_Y = -30;
			EditorSpawnsZombiesUI.removeButton.positionScale_Y = 1f;
			EditorSpawnsZombiesUI.removeButton.sizeOffset_X = 200;
			EditorSpawnsZombiesUI.removeButton.sizeOffset_Y = 30;
			EditorSpawnsZombiesUI.removeButton.text = EditorSpawnsZombiesUI.localization.format("RemoveButtonText", new object[]
			{
				ControlsSettings.tool_1
			});
			EditorSpawnsZombiesUI.removeButton.tooltip = EditorSpawnsZombiesUI.localization.format("RemoveButtonTooltip");
			SleekButton sleekButton6 = EditorSpawnsZombiesUI.removeButton;
			if (EditorSpawnsZombiesUI.<>f__mg$cache14 == null)
			{
				EditorSpawnsZombiesUI.<>f__mg$cache14 = new ClickedButton(EditorSpawnsZombiesUI.onClickedRemoveButton);
			}
			sleekButton6.onClickedButton = EditorSpawnsZombiesUI.<>f__mg$cache14;
			EditorSpawnsZombiesUI.container.add(EditorSpawnsZombiesUI.removeButton);
			bundle.unload();
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x00166712 File Offset: 0x00164B12
		public static void open()
		{
			if (EditorSpawnsZombiesUI.active)
			{
				return;
			}
			EditorSpawnsZombiesUI.active = true;
			EditorSpawns.isSpawning = true;
			EditorSpawns.spawnMode = ESpawnMode.ADD_ZOMBIE;
			EditorSpawnsZombiesUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x0016674B File Offset: 0x00164B4B
		public static void close()
		{
			if (!EditorSpawnsZombiesUI.active)
			{
				return;
			}
			EditorSpawnsZombiesUI.active = false;
			EditorSpawns.isSpawning = false;
			EditorSpawnsZombiesUI.container.lerpPositionScale(1f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x00166780 File Offset: 0x00164B80
		public static void updateTables()
		{
			if (EditorSpawnsZombiesUI.tableButtons != null)
			{
				for (int i = 0; i < EditorSpawnsZombiesUI.tableButtons.Length; i++)
				{
					EditorSpawnsZombiesUI.tableScrollBox.remove(EditorSpawnsZombiesUI.tableButtons[i]);
				}
			}
			EditorSpawnsZombiesUI.tableButtons = new SleekButton[LevelZombies.tables.Count];
			EditorSpawnsZombiesUI.tableScrollBox.area = new Rect(0f, 0f, 5f, (float)(EditorSpawnsZombiesUI.tableButtons.Length * 40 - 10));
			for (int j = 0; j < EditorSpawnsZombiesUI.tableButtons.Length; j++)
			{
				SleekButton sleekButton = new SleekButton();
				sleekButton.positionOffset_X = 240;
				sleekButton.positionOffset_Y = j * 40;
				sleekButton.sizeOffset_X = 200;
				sleekButton.sizeOffset_Y = 30;
				sleekButton.text = j + " " + LevelZombies.tables[j].name;
				SleekButton sleekButton2 = sleekButton;
				if (EditorSpawnsZombiesUI.<>f__mg$cache0 == null)
				{
					EditorSpawnsZombiesUI.<>f__mg$cache0 = new ClickedButton(EditorSpawnsZombiesUI.onClickedTableButton);
				}
				sleekButton2.onClickedButton = EditorSpawnsZombiesUI.<>f__mg$cache0;
				EditorSpawnsZombiesUI.tableScrollBox.add(sleekButton);
				EditorSpawnsZombiesUI.tableButtons[j] = sleekButton;
			}
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x001668A4 File Offset: 0x00164CA4
		public static void updateSelection()
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
			{
				ZombieTable zombieTable = LevelZombies.tables[(int)EditorSpawns.selectedZombie];
				EditorSpawnsZombiesUI.selectedBox.text = zombieTable.name;
				EditorSpawnsZombiesUI.tableNameField.text = zombieTable.name;
				EditorSpawnsZombiesUI.tableColorPicker.state = zombieTable.color;
				EditorSpawnsZombiesUI.megaToggle.state = zombieTable.isMega;
				EditorSpawnsZombiesUI.healthField.state = zombieTable.health;
				EditorSpawnsZombiesUI.damageField.state = zombieTable.damage;
				EditorSpawnsZombiesUI.lootIndexField.state = zombieTable.lootIndex;
				EditorSpawnsZombiesUI.lootIDField.state = zombieTable.lootID;
				EditorSpawnsZombiesUI.xpField.state = zombieTable.xp;
				EditorSpawnsZombiesUI.regenField.state = zombieTable.regen;
				EditorSpawnsZombiesUI.difficultyGUIDField.text = zombieTable.difficultyGUID;
				if (EditorSpawnsZombiesUI.slotButtons != null)
				{
					for (int i = 0; i < EditorSpawnsZombiesUI.slotButtons.Length; i++)
					{
						EditorSpawnsZombiesUI.spawnsScrollBox.remove(EditorSpawnsZombiesUI.slotButtons[i]);
					}
				}
				EditorSpawnsZombiesUI.slotButtons = new SleekButton[zombieTable.slots.Length];
				for (int j = 0; j < EditorSpawnsZombiesUI.slotButtons.Length; j++)
				{
					ZombieSlot zombieSlot = zombieTable.slots[j];
					SleekButton sleekButton = new SleekButton();
					sleekButton.positionOffset_X = 240;
					sleekButton.positionOffset_Y = 460 + j * 70;
					sleekButton.sizeOffset_X = 200;
					sleekButton.sizeOffset_Y = 30;
					sleekButton.text = EditorSpawnsZombiesUI.localization.format("Slot_" + j);
					SleekButton sleekButton2 = sleekButton;
					if (EditorSpawnsZombiesUI.<>f__mg$cache1 == null)
					{
						EditorSpawnsZombiesUI.<>f__mg$cache1 = new ClickedButton(EditorSpawnsZombiesUI.onClickedSlotButton);
					}
					sleekButton2.onClickedButton = EditorSpawnsZombiesUI.<>f__mg$cache1;
					EditorSpawnsZombiesUI.spawnsScrollBox.add(sleekButton);
					SleekSlider sleekSlider = new SleekSlider();
					sleekSlider.positionOffset_Y = 40;
					sleekSlider.sizeOffset_X = 200;
					sleekSlider.sizeOffset_Y = 20;
					sleekSlider.orientation = ESleekOrientation.HORIZONTAL;
					sleekSlider.state = zombieSlot.chance;
					sleekSlider.addLabel(Mathf.RoundToInt(zombieSlot.chance * 100f) + "%", ESleekSide.LEFT);
					SleekSlider sleekSlider2 = sleekSlider;
					if (EditorSpawnsZombiesUI.<>f__mg$cache2 == null)
					{
						EditorSpawnsZombiesUI.<>f__mg$cache2 = new Dragged(EditorSpawnsZombiesUI.onDraggedChanceSlider);
					}
					sleekSlider2.onDragged = EditorSpawnsZombiesUI.<>f__mg$cache2;
					sleekButton.add(sleekSlider);
					EditorSpawnsZombiesUI.slotButtons[j] = sleekButton;
				}
				if (EditorSpawnsZombiesUI.clothButtons != null)
				{
					for (int k = 0; k < EditorSpawnsZombiesUI.clothButtons.Length; k++)
					{
						EditorSpawnsZombiesUI.spawnsScrollBox.remove(EditorSpawnsZombiesUI.clothButtons[k]);
					}
				}
				if ((int)EditorSpawnsZombiesUI.selectedSlot < zombieTable.slots.Length)
				{
					EditorSpawnsZombiesUI.clothButtons = new SleekButton[zombieTable.slots[(int)EditorSpawnsZombiesUI.selectedSlot].table.Count];
					for (int l = 0; l < EditorSpawnsZombiesUI.clothButtons.Length; l++)
					{
						SleekButton sleekButton3 = new SleekButton();
						sleekButton3.positionOffset_X = 240;
						sleekButton3.positionOffset_Y = 460 + EditorSpawnsZombiesUI.slotButtons.Length * 70 + l * 40;
						sleekButton3.sizeOffset_X = 200;
						sleekButton3.sizeOffset_Y = 30;
						ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, zombieTable.slots[(int)EditorSpawnsZombiesUI.selectedSlot].table[l].item);
						string str = "?";
						if (itemAsset != null)
						{
							if (string.IsNullOrEmpty(itemAsset.itemName))
							{
								str = itemAsset.name;
							}
							else
							{
								str = itemAsset.itemName;
							}
						}
						sleekButton3.text = zombieTable.slots[(int)EditorSpawnsZombiesUI.selectedSlot].table[l].item.ToString() + " " + str;
						SleekButton sleekButton4 = sleekButton3;
						if (EditorSpawnsZombiesUI.<>f__mg$cache3 == null)
						{
							EditorSpawnsZombiesUI.<>f__mg$cache3 = new ClickedButton(EditorSpawnsZombiesUI.onClickItemButton);
						}
						sleekButton4.onClickedButton = EditorSpawnsZombiesUI.<>f__mg$cache3;
						EditorSpawnsZombiesUI.spawnsScrollBox.add(sleekButton3);
						EditorSpawnsZombiesUI.clothButtons[l] = sleekButton3;
					}
				}
				else
				{
					EditorSpawnsZombiesUI.clothButtons = new SleekButton[0];
				}
				EditorSpawnsZombiesUI.itemIDField.positionOffset_Y = 460 + EditorSpawnsZombiesUI.slotButtons.Length * 70 + EditorSpawnsZombiesUI.clothButtons.Length * 40;
				EditorSpawnsZombiesUI.addItemButton.positionOffset_Y = 460 + EditorSpawnsZombiesUI.slotButtons.Length * 70 + EditorSpawnsZombiesUI.clothButtons.Length * 40 + 40;
				EditorSpawnsZombiesUI.removeItemButton.positionOffset_Y = 460 + EditorSpawnsZombiesUI.slotButtons.Length * 70 + EditorSpawnsZombiesUI.clothButtons.Length * 40 + 40;
				EditorSpawnsZombiesUI.spawnsScrollBox.area = new Rect(0f, 0f, 5f, (float)(460 + EditorSpawnsZombiesUI.slotButtons.Length * 70 + EditorSpawnsZombiesUI.clothButtons.Length * 40 + 70));
			}
			else
			{
				EditorSpawnsZombiesUI.selectedBox.text = string.Empty;
				EditorSpawnsZombiesUI.tableNameField.text = string.Empty;
				EditorSpawnsZombiesUI.tableColorPicker.state = Color.white;
				EditorSpawnsZombiesUI.megaToggle.state = false;
				EditorSpawnsZombiesUI.healthField.state = 0;
				EditorSpawnsZombiesUI.damageField.state = 0;
				EditorSpawnsZombiesUI.lootIndexField.state = 0;
				EditorSpawnsZombiesUI.lootIDField.state = 0;
				EditorSpawnsZombiesUI.xpField.state = 0u;
				EditorSpawnsZombiesUI.regenField.state = 0f;
				EditorSpawnsZombiesUI.difficultyGUIDField.text = string.Empty;
				if (EditorSpawnsZombiesUI.slotButtons != null)
				{
					for (int m = 0; m < EditorSpawnsZombiesUI.slotButtons.Length; m++)
					{
						EditorSpawnsZombiesUI.spawnsScrollBox.remove(EditorSpawnsZombiesUI.slotButtons[m]);
					}
				}
				EditorSpawnsZombiesUI.slotButtons = null;
				if (EditorSpawnsZombiesUI.clothButtons != null)
				{
					for (int n = 0; n < EditorSpawnsZombiesUI.clothButtons.Length; n++)
					{
						EditorSpawnsZombiesUI.spawnsScrollBox.remove(EditorSpawnsZombiesUI.clothButtons[n]);
					}
				}
				EditorSpawnsZombiesUI.clothButtons = null;
				EditorSpawnsZombiesUI.itemIDField.positionOffset_Y = 460;
				EditorSpawnsZombiesUI.addItemButton.positionOffset_Y = 500;
				EditorSpawnsZombiesUI.removeItemButton.positionOffset_Y = 500;
				EditorSpawnsZombiesUI.spawnsScrollBox.area = new Rect(0f, 0f, 5f, 530f);
			}
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x00166ED4 File Offset: 0x001652D4
		private static void onClickedTableButton(SleekButton button)
		{
			if (EditorSpawns.selectedZombie != (byte)(button.positionOffset_Y / 40))
			{
				EditorSpawns.selectedZombie = (byte)(button.positionOffset_Y / 40);
				EditorSpawns.zombieSpawn.GetComponent<Renderer>().material.color = LevelZombies.tables[(int)EditorSpawns.selectedZombie].color;
			}
			else
			{
				EditorSpawns.selectedZombie = byte.MaxValue;
				EditorSpawns.zombieSpawn.GetComponent<Renderer>().material.color = Color.white;
			}
			EditorSpawnsZombiesUI.updateSelection();
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x00166F59 File Offset: 0x00165359
		private static void onZombieColorPicked(SleekColorPicker picker, Color color)
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
			{
				LevelZombies.tables[(int)EditorSpawns.selectedZombie].color = color;
			}
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x00166F84 File Offset: 0x00165384
		private static void onToggledMegaToggle(SleekToggle toggle, bool state)
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
			{
				LevelZombies.tables[(int)EditorSpawns.selectedZombie].isMega = state;
			}
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x00166FAF File Offset: 0x001653AF
		private static void onHealthFieldTyped(SleekUInt16Field field, ushort state)
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
			{
				LevelZombies.tables[(int)EditorSpawns.selectedZombie].health = state;
			}
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x00166FDA File Offset: 0x001653DA
		private static void onDamageFieldTyped(SleekByteField field, byte state)
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
			{
				LevelZombies.tables[(int)EditorSpawns.selectedZombie].damage = state;
			}
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x00167005 File Offset: 0x00165405
		private static void onLootIndexFieldTyped(SleekByteField field, byte state)
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count && (int)state < LevelItems.tables.Count)
			{
				LevelZombies.tables[(int)EditorSpawns.selectedZombie].lootIndex = state;
			}
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x00167040 File Offset: 0x00165440
		private static void onLootIDFieldTyped(SleekUInt16Field field, ushort state)
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
			{
				LevelZombies.tables[(int)EditorSpawns.selectedZombie].lootID = state;
			}
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x0016706B File Offset: 0x0016546B
		private static void onXPFieldTyped(SleekUInt32Field field, uint state)
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
			{
				LevelZombies.tables[(int)EditorSpawns.selectedZombie].xp = state;
			}
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x00167096 File Offset: 0x00165496
		private static void onRegenFieldTyped(SleekSingleField field, float state)
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
			{
				LevelZombies.tables[(int)EditorSpawns.selectedZombie].regen = state;
			}
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x001670C1 File Offset: 0x001654C1
		private static void onDifficultyGUIDFieldTyped(SleekField field, string state)
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
			{
				LevelZombies.tables[(int)EditorSpawns.selectedZombie].difficultyGUID = state;
			}
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x001670EC File Offset: 0x001654EC
		private static void onClickedSlotButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
			{
				EditorSpawnsZombiesUI.selectedSlot = (byte)((button.positionOffset_Y - 460) / 70);
				EditorSpawnsZombiesUI.updateSelection();
			}
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x0016711C File Offset: 0x0016551C
		private static void onClickItemButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
			{
				EditorSpawnsZombiesUI.selectItem = (byte)((button.positionOffset_Y - 460 - EditorSpawnsZombiesUI.slotButtons.Length * 70) / 40);
				EditorSpawnsZombiesUI.updateSelection();
			}
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x00167158 File Offset: 0x00165558
		private static void onDraggedChanceSlider(SleekSlider slider, float state)
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
			{
				int num = (slider.parent.positionOffset_Y - 460) / 70;
				LevelZombies.tables[(int)EditorSpawns.selectedZombie].slots[num].chance = state;
				SleekSlider sleekSlider = (SleekSlider)EditorSpawnsZombiesUI.slotButtons[num].children[0];
				sleekSlider.updateLabel(Mathf.RoundToInt(state * 100f) + "%");
			}
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x001671E4 File Offset: 0x001655E4
		private static void onTypedNameField(SleekField field, string state)
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
			{
				EditorSpawnsZombiesUI.selectedBox.text = state;
				LevelZombies.tables[(int)EditorSpawns.selectedZombie].name = state;
				EditorSpawnsZombiesUI.tableButtons[(int)EditorSpawns.selectedZombie].text = EditorSpawns.selectedZombie + " " + state;
			}
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x0016724C File Offset: 0x0016564C
		private static void onClickedAddTableButton(SleekButton button)
		{
			if (EditorSpawnsZombiesUI.tableNameField.text != string.Empty)
			{
				LevelZombies.addTable(EditorSpawnsZombiesUI.tableNameField.text);
				EditorSpawnsZombiesUI.tableNameField.text = string.Empty;
				EditorSpawnsZombiesUI.updateTables();
				EditorSpawnsZombiesUI.tableScrollBox.state = new Vector2(0f, float.MaxValue);
			}
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x001672AE File Offset: 0x001656AE
		private static void onClickedRemoveTableButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
			{
				LevelZombies.removeTable();
				EditorSpawnsZombiesUI.updateTables();
				EditorSpawnsZombiesUI.updateSelection();
				EditorSpawnsZombiesUI.tableScrollBox.state = new Vector2(0f, float.MaxValue);
			}
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x001672EC File Offset: 0x001656EC
		private static void onClickedAddItemButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
			{
				ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, EditorSpawnsZombiesUI.itemIDField.state);
				if (itemAsset != null)
				{
					if (EditorSpawnsZombiesUI.selectedSlot == 0 && itemAsset.type != EItemType.SHIRT)
					{
						return;
					}
					if (EditorSpawnsZombiesUI.selectedSlot == 1 && itemAsset.type != EItemType.PANTS)
					{
						return;
					}
					if ((EditorSpawnsZombiesUI.selectedSlot == 2 || EditorSpawnsZombiesUI.selectedSlot == 3) && itemAsset.type != EItemType.HAT && itemAsset.type != EItemType.BACKPACK && itemAsset.type != EItemType.VEST && itemAsset.type != EItemType.MASK && itemAsset.type != EItemType.GLASSES)
					{
						return;
					}
					LevelZombies.tables[(int)EditorSpawns.selectedZombie].addCloth(EditorSpawnsZombiesUI.selectedSlot, EditorSpawnsZombiesUI.itemIDField.state);
					EditorSpawnsZombiesUI.updateSelection();
					EditorSpawnsZombiesUI.spawnsScrollBox.state = new Vector2(0f, float.MaxValue);
				}
				EditorSpawnsZombiesUI.itemIDField.state = 0;
			}
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x001673F8 File Offset: 0x001657F8
		private static void onClickedRemoveItemButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count && (int)EditorSpawnsZombiesUI.selectItem < LevelZombies.tables[(int)EditorSpawns.selectedZombie].slots[(int)EditorSpawnsZombiesUI.selectedSlot].table.Count)
			{
				LevelZombies.tables[(int)EditorSpawns.selectedZombie].removeCloth(EditorSpawnsZombiesUI.selectedSlot, EditorSpawnsZombiesUI.selectItem);
				EditorSpawnsZombiesUI.updateSelection();
				EditorSpawnsZombiesUI.spawnsScrollBox.state = new Vector2(0f, float.MaxValue);
			}
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x00167483 File Offset: 0x00165883
		private static void onDraggedRadiusSlider(SleekSlider slider, float state)
		{
			EditorSpawns.radius = (byte)((float)EditorSpawns.MIN_REMOVE_SIZE + state * (float)EditorSpawns.MAX_REMOVE_SIZE);
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x0016749A File Offset: 0x0016589A
		private static void onClickedAddButton(SleekButton button)
		{
			EditorSpawns.spawnMode = ESpawnMode.ADD_ZOMBIE;
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x001674A2 File Offset: 0x001658A2
		private static void onClickedRemoveButton(SleekButton button)
		{
			EditorSpawns.spawnMode = ESpawnMode.REMOVE_ZOMBIE;
		}

		// Token: 0x04002531 RID: 9521
		private static Sleek container;

		// Token: 0x04002532 RID: 9522
		private static Local localization;

		// Token: 0x04002533 RID: 9523
		public static bool active;

		// Token: 0x04002534 RID: 9524
		private static SleekScrollBox tableScrollBox;

		// Token: 0x04002535 RID: 9525
		private static SleekScrollBox spawnsScrollBox;

		// Token: 0x04002536 RID: 9526
		private static SleekButton[] tableButtons;

		// Token: 0x04002537 RID: 9527
		private static SleekButton[] slotButtons;

		// Token: 0x04002538 RID: 9528
		private static SleekButton[] clothButtons;

		// Token: 0x04002539 RID: 9529
		private static SleekColorPicker tableColorPicker;

		// Token: 0x0400253A RID: 9530
		private static SleekToggle megaToggle;

		// Token: 0x0400253B RID: 9531
		private static SleekUInt16Field healthField;

		// Token: 0x0400253C RID: 9532
		private static SleekByteField damageField;

		// Token: 0x0400253D RID: 9533
		private static SleekByteField lootIndexField;

		// Token: 0x0400253E RID: 9534
		private static SleekUInt16Field lootIDField;

		// Token: 0x0400253F RID: 9535
		private static SleekUInt32Field xpField;

		// Token: 0x04002540 RID: 9536
		private static SleekSingleField regenField;

		// Token: 0x04002541 RID: 9537
		private static SleekField difficultyGUIDField;

		// Token: 0x04002542 RID: 9538
		private static SleekUInt16Field itemIDField;

		// Token: 0x04002543 RID: 9539
		private static SleekButtonIcon addItemButton;

		// Token: 0x04002544 RID: 9540
		private static SleekButtonIcon removeItemButton;

		// Token: 0x04002545 RID: 9541
		private static SleekBox selectedBox;

		// Token: 0x04002546 RID: 9542
		private static SleekField tableNameField;

		// Token: 0x04002547 RID: 9543
		private static SleekButtonIcon addTableButton;

		// Token: 0x04002548 RID: 9544
		private static SleekButtonIcon removeTableButton;

		// Token: 0x04002549 RID: 9545
		private static SleekSlider radiusSlider;

		// Token: 0x0400254A RID: 9546
		private static SleekButtonIcon addButton;

		// Token: 0x0400254B RID: 9547
		private static SleekButtonIcon removeButton;

		// Token: 0x0400254C RID: 9548
		private static byte selectedSlot;

		// Token: 0x0400254D RID: 9549
		private static byte selectItem;

		// Token: 0x0400254E RID: 9550
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x0400254F RID: 9551
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x04002550 RID: 9552
		[CompilerGenerated]
		private static Dragged <>f__mg$cache2;

		// Token: 0x04002551 RID: 9553
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x04002552 RID: 9554
		[CompilerGenerated]
		private static Typed <>f__mg$cache4;

		// Token: 0x04002553 RID: 9555
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;

		// Token: 0x04002554 RID: 9556
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache6;

		// Token: 0x04002555 RID: 9557
		[CompilerGenerated]
		private static ColorPicked <>f__mg$cache7;

		// Token: 0x04002556 RID: 9558
		[CompilerGenerated]
		private static Toggled <>f__mg$cache8;

		// Token: 0x04002557 RID: 9559
		[CompilerGenerated]
		private static TypedUInt16 <>f__mg$cache9;

		// Token: 0x04002558 RID: 9560
		[CompilerGenerated]
		private static TypedByte <>f__mg$cacheA;

		// Token: 0x04002559 RID: 9561
		[CompilerGenerated]
		private static TypedByte <>f__mg$cacheB;

		// Token: 0x0400255A RID: 9562
		[CompilerGenerated]
		private static TypedUInt16 <>f__mg$cacheC;

		// Token: 0x0400255B RID: 9563
		[CompilerGenerated]
		private static TypedUInt32 <>f__mg$cacheD;

		// Token: 0x0400255C RID: 9564
		[CompilerGenerated]
		private static TypedSingle <>f__mg$cacheE;

		// Token: 0x0400255D RID: 9565
		[CompilerGenerated]
		private static Typed <>f__mg$cacheF;

		// Token: 0x0400255E RID: 9566
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache10;

		// Token: 0x0400255F RID: 9567
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache11;

		// Token: 0x04002560 RID: 9568
		[CompilerGenerated]
		private static Dragged <>f__mg$cache12;

		// Token: 0x04002561 RID: 9569
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache13;

		// Token: 0x04002562 RID: 9570
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache14;
	}
}
