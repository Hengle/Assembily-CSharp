﻿using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200075A RID: 1882
	public class EditorSpawnsItemsUI
	{
		// Token: 0x06003538 RID: 13624 RVA: 0x00161D68 File Offset: 0x00160168
		public EditorSpawnsItemsUI()
		{
			Local local = Localization.read("/Editor/EditorSpawnsItems.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Edit/Icons/EditorSpawnsItems/EditorSpawnsItems.unity3d");
			EditorSpawnsItemsUI.container = new Sleek();
			EditorSpawnsItemsUI.container.positionOffset_X = 10;
			EditorSpawnsItemsUI.container.positionOffset_Y = 10;
			EditorSpawnsItemsUI.container.positionScale_X = 1f;
			EditorSpawnsItemsUI.container.sizeOffset_X = -20;
			EditorSpawnsItemsUI.container.sizeOffset_Y = -20;
			EditorSpawnsItemsUI.container.sizeScale_X = 1f;
			EditorSpawnsItemsUI.container.sizeScale_Y = 1f;
			EditorUI.window.add(EditorSpawnsItemsUI.container);
			EditorSpawnsItemsUI.active = false;
			EditorSpawnsItemsUI.tableScrollBox = new SleekScrollBox();
			EditorSpawnsItemsUI.tableScrollBox.positionOffset_X = -470;
			EditorSpawnsItemsUI.tableScrollBox.positionOffset_Y = 120;
			EditorSpawnsItemsUI.tableScrollBox.positionScale_X = 1f;
			EditorSpawnsItemsUI.tableScrollBox.sizeOffset_X = 470;
			EditorSpawnsItemsUI.tableScrollBox.sizeOffset_Y = 200;
			EditorSpawnsItemsUI.container.add(EditorSpawnsItemsUI.tableScrollBox);
			EditorSpawnsItemsUI.tableNameField = new SleekField();
			EditorSpawnsItemsUI.tableNameField.positionOffset_X = -230;
			EditorSpawnsItemsUI.tableNameField.positionOffset_Y = 330;
			EditorSpawnsItemsUI.tableNameField.positionScale_X = 1f;
			EditorSpawnsItemsUI.tableNameField.sizeOffset_X = 230;
			EditorSpawnsItemsUI.tableNameField.sizeOffset_Y = 30;
			EditorSpawnsItemsUI.tableNameField.maxLength = 64;
			EditorSpawnsItemsUI.tableNameField.addLabel(local.format("TableNameFieldLabelText"), ESleekSide.LEFT);
			SleekField sleekField = EditorSpawnsItemsUI.tableNameField;
			Delegate onTyped = sleekField.onTyped;
			if (EditorSpawnsItemsUI.<>f__mg$cache4 == null)
			{
				EditorSpawnsItemsUI.<>f__mg$cache4 = new Typed(EditorSpawnsItemsUI.onTypedTableNameField);
			}
			sleekField.onTyped = (Typed)Delegate.Combine(onTyped, EditorSpawnsItemsUI.<>f__mg$cache4);
			EditorSpawnsItemsUI.container.add(EditorSpawnsItemsUI.tableNameField);
			EditorSpawnsItemsUI.addTableButton = new SleekButtonIcon((Texture2D)bundle.load("Add"));
			EditorSpawnsItemsUI.addTableButton.positionOffset_X = -230;
			EditorSpawnsItemsUI.addTableButton.positionOffset_Y = 370;
			EditorSpawnsItemsUI.addTableButton.positionScale_X = 1f;
			EditorSpawnsItemsUI.addTableButton.sizeOffset_X = 110;
			EditorSpawnsItemsUI.addTableButton.sizeOffset_Y = 30;
			EditorSpawnsItemsUI.addTableButton.text = local.format("AddTableButtonText");
			EditorSpawnsItemsUI.addTableButton.tooltip = local.format("AddTableButtonTooltip");
			SleekButton sleekButton = EditorSpawnsItemsUI.addTableButton;
			if (EditorSpawnsItemsUI.<>f__mg$cache5 == null)
			{
				EditorSpawnsItemsUI.<>f__mg$cache5 = new ClickedButton(EditorSpawnsItemsUI.onClickedAddTableButton);
			}
			sleekButton.onClickedButton = EditorSpawnsItemsUI.<>f__mg$cache5;
			EditorSpawnsItemsUI.container.add(EditorSpawnsItemsUI.addTableButton);
			EditorSpawnsItemsUI.removeTableButton = new SleekButtonIcon((Texture2D)bundle.load("Remove"));
			EditorSpawnsItemsUI.removeTableButton.positionOffset_X = -110;
			EditorSpawnsItemsUI.removeTableButton.positionOffset_Y = 370;
			EditorSpawnsItemsUI.removeTableButton.positionScale_X = 1f;
			EditorSpawnsItemsUI.removeTableButton.sizeOffset_X = 110;
			EditorSpawnsItemsUI.removeTableButton.sizeOffset_Y = 30;
			EditorSpawnsItemsUI.removeTableButton.text = local.format("RemoveTableButtonText");
			EditorSpawnsItemsUI.removeTableButton.tooltip = local.format("RemoveTableButtonTooltip");
			SleekButton sleekButton2 = EditorSpawnsItemsUI.removeTableButton;
			if (EditorSpawnsItemsUI.<>f__mg$cache6 == null)
			{
				EditorSpawnsItemsUI.<>f__mg$cache6 = new ClickedButton(EditorSpawnsItemsUI.onClickedRemoveTableButton);
			}
			sleekButton2.onClickedButton = EditorSpawnsItemsUI.<>f__mg$cache6;
			EditorSpawnsItemsUI.container.add(EditorSpawnsItemsUI.removeTableButton);
			EditorSpawnsItemsUI.updateTables();
			EditorSpawnsItemsUI.spawnsScrollBox = new SleekScrollBox();
			EditorSpawnsItemsUI.spawnsScrollBox.positionOffset_X = -470;
			EditorSpawnsItemsUI.spawnsScrollBox.positionOffset_Y = 410;
			EditorSpawnsItemsUI.spawnsScrollBox.positionScale_X = 1f;
			EditorSpawnsItemsUI.spawnsScrollBox.sizeOffset_X = 470;
			EditorSpawnsItemsUI.spawnsScrollBox.sizeOffset_Y = -410;
			EditorSpawnsItemsUI.spawnsScrollBox.sizeScale_Y = 1f;
			EditorSpawnsItemsUI.spawnsScrollBox.area = new Rect(0f, 0f, 5f, 1000f);
			EditorSpawnsItemsUI.container.add(EditorSpawnsItemsUI.spawnsScrollBox);
			EditorSpawnsItemsUI.tableColorPicker = new SleekColorPicker();
			EditorSpawnsItemsUI.tableColorPicker.positionOffset_X = 200;
			SleekColorPicker sleekColorPicker = EditorSpawnsItemsUI.tableColorPicker;
			if (EditorSpawnsItemsUI.<>f__mg$cache7 == null)
			{
				EditorSpawnsItemsUI.<>f__mg$cache7 = new ColorPicked(EditorSpawnsItemsUI.onItemColorPicked);
			}
			sleekColorPicker.onColorPicked = EditorSpawnsItemsUI.<>f__mg$cache7;
			EditorSpawnsItemsUI.spawnsScrollBox.add(EditorSpawnsItemsUI.tableColorPicker);
			EditorSpawnsItemsUI.tableIDField = new SleekUInt16Field();
			EditorSpawnsItemsUI.tableIDField.positionOffset_X = 240;
			EditorSpawnsItemsUI.tableIDField.positionOffset_Y = 130;
			EditorSpawnsItemsUI.tableIDField.sizeOffset_X = 200;
			EditorSpawnsItemsUI.tableIDField.sizeOffset_Y = 30;
			SleekUInt16Field sleekUInt16Field = EditorSpawnsItemsUI.tableIDField;
			if (EditorSpawnsItemsUI.<>f__mg$cache8 == null)
			{
				EditorSpawnsItemsUI.<>f__mg$cache8 = new TypedUInt16(EditorSpawnsItemsUI.onTableIDFieldTyped);
			}
			sleekUInt16Field.onTypedUInt16 = EditorSpawnsItemsUI.<>f__mg$cache8;
			EditorSpawnsItemsUI.tableIDField.addLabel(local.format("TableIDFieldLabelText"), ESleekSide.LEFT);
			EditorSpawnsItemsUI.spawnsScrollBox.add(EditorSpawnsItemsUI.tableIDField);
			EditorSpawnsItemsUI.tierNameField = new SleekField();
			EditorSpawnsItemsUI.tierNameField.positionOffset_X = 240;
			EditorSpawnsItemsUI.tierNameField.sizeOffset_X = 200;
			EditorSpawnsItemsUI.tierNameField.sizeOffset_Y = 30;
			EditorSpawnsItemsUI.tierNameField.maxLength = 64;
			EditorSpawnsItemsUI.tierNameField.addLabel(local.format("TierNameFieldLabelText"), ESleekSide.LEFT);
			SleekField sleekField2 = EditorSpawnsItemsUI.tierNameField;
			if (EditorSpawnsItemsUI.<>f__mg$cache9 == null)
			{
				EditorSpawnsItemsUI.<>f__mg$cache9 = new Typed(EditorSpawnsItemsUI.onTypedTierNameField);
			}
			sleekField2.onTyped = EditorSpawnsItemsUI.<>f__mg$cache9;
			EditorSpawnsItemsUI.spawnsScrollBox.add(EditorSpawnsItemsUI.tierNameField);
			EditorSpawnsItemsUI.addTierButton = new SleekButtonIcon((Texture2D)bundle.load("Add"));
			EditorSpawnsItemsUI.addTierButton.positionOffset_X = 240;
			EditorSpawnsItemsUI.addTierButton.sizeOffset_X = 95;
			EditorSpawnsItemsUI.addTierButton.sizeOffset_Y = 30;
			EditorSpawnsItemsUI.addTierButton.text = local.format("AddTierButtonText");
			EditorSpawnsItemsUI.addTierButton.tooltip = local.format("AddTierButtonTooltip");
			SleekButton sleekButton3 = EditorSpawnsItemsUI.addTierButton;
			if (EditorSpawnsItemsUI.<>f__mg$cacheA == null)
			{
				EditorSpawnsItemsUI.<>f__mg$cacheA = new ClickedButton(EditorSpawnsItemsUI.onClickedAddTierButton);
			}
			sleekButton3.onClickedButton = EditorSpawnsItemsUI.<>f__mg$cacheA;
			EditorSpawnsItemsUI.spawnsScrollBox.add(EditorSpawnsItemsUI.addTierButton);
			EditorSpawnsItemsUI.removeTierButton = new SleekButtonIcon((Texture2D)bundle.load("Remove"));
			EditorSpawnsItemsUI.removeTierButton.positionOffset_X = 345;
			EditorSpawnsItemsUI.removeTierButton.sizeOffset_X = 95;
			EditorSpawnsItemsUI.removeTierButton.sizeOffset_Y = 30;
			EditorSpawnsItemsUI.removeTierButton.text = local.format("RemoveTierButtonText");
			EditorSpawnsItemsUI.removeTierButton.tooltip = local.format("RemoveTierButtonTooltip");
			SleekButton sleekButton4 = EditorSpawnsItemsUI.removeTierButton;
			if (EditorSpawnsItemsUI.<>f__mg$cacheB == null)
			{
				EditorSpawnsItemsUI.<>f__mg$cacheB = new ClickedButton(EditorSpawnsItemsUI.onClickedRemoveTierButton);
			}
			sleekButton4.onClickedButton = EditorSpawnsItemsUI.<>f__mg$cacheB;
			EditorSpawnsItemsUI.spawnsScrollBox.add(EditorSpawnsItemsUI.removeTierButton);
			EditorSpawnsItemsUI.itemIDField = new SleekUInt16Field();
			EditorSpawnsItemsUI.itemIDField.positionOffset_X = 240;
			EditorSpawnsItemsUI.itemIDField.sizeOffset_X = 200;
			EditorSpawnsItemsUI.itemIDField.sizeOffset_Y = 30;
			EditorSpawnsItemsUI.itemIDField.addLabel(local.format("ItemIDFieldLabelText"), ESleekSide.LEFT);
			EditorSpawnsItemsUI.spawnsScrollBox.add(EditorSpawnsItemsUI.itemIDField);
			EditorSpawnsItemsUI.addItemButton = new SleekButtonIcon((Texture2D)bundle.load("Add"));
			EditorSpawnsItemsUI.addItemButton.positionOffset_X = 240;
			EditorSpawnsItemsUI.addItemButton.sizeOffset_X = 95;
			EditorSpawnsItemsUI.addItemButton.sizeOffset_Y = 30;
			EditorSpawnsItemsUI.addItemButton.text = local.format("AddItemButtonText");
			EditorSpawnsItemsUI.addItemButton.tooltip = local.format("AddItemButtonTooltip");
			SleekButton sleekButton5 = EditorSpawnsItemsUI.addItemButton;
			if (EditorSpawnsItemsUI.<>f__mg$cacheC == null)
			{
				EditorSpawnsItemsUI.<>f__mg$cacheC = new ClickedButton(EditorSpawnsItemsUI.onClickedAddItemButton);
			}
			sleekButton5.onClickedButton = EditorSpawnsItemsUI.<>f__mg$cacheC;
			EditorSpawnsItemsUI.spawnsScrollBox.add(EditorSpawnsItemsUI.addItemButton);
			EditorSpawnsItemsUI.removeItemButton = new SleekButtonIcon((Texture2D)bundle.load("Remove"));
			EditorSpawnsItemsUI.removeItemButton.positionOffset_X = 345;
			EditorSpawnsItemsUI.removeItemButton.sizeOffset_X = 95;
			EditorSpawnsItemsUI.removeItemButton.sizeOffset_Y = 30;
			EditorSpawnsItemsUI.removeItemButton.text = local.format("RemoveItemButtonText");
			EditorSpawnsItemsUI.removeItemButton.tooltip = local.format("RemoveItemButtonTooltip");
			SleekButton sleekButton6 = EditorSpawnsItemsUI.removeItemButton;
			if (EditorSpawnsItemsUI.<>f__mg$cacheD == null)
			{
				EditorSpawnsItemsUI.<>f__mg$cacheD = new ClickedButton(EditorSpawnsItemsUI.onClickedRemoveItemButton);
			}
			sleekButton6.onClickedButton = EditorSpawnsItemsUI.<>f__mg$cacheD;
			EditorSpawnsItemsUI.spawnsScrollBox.add(EditorSpawnsItemsUI.removeItemButton);
			EditorSpawnsItemsUI.selectedBox = new SleekBox();
			EditorSpawnsItemsUI.selectedBox.positionOffset_X = -230;
			EditorSpawnsItemsUI.selectedBox.positionOffset_Y = 80;
			EditorSpawnsItemsUI.selectedBox.positionScale_X = 1f;
			EditorSpawnsItemsUI.selectedBox.sizeOffset_X = 230;
			EditorSpawnsItemsUI.selectedBox.sizeOffset_Y = 30;
			EditorSpawnsItemsUI.selectedBox.addLabel(local.format("SelectionBoxLabelText"), ESleekSide.LEFT);
			EditorSpawnsItemsUI.container.add(EditorSpawnsItemsUI.selectedBox);
			EditorSpawnsItemsUI.updateSelection();
			EditorSpawnsItemsUI.radiusSlider = new SleekSlider();
			EditorSpawnsItemsUI.radiusSlider.positionOffset_Y = -100;
			EditorSpawnsItemsUI.radiusSlider.positionScale_Y = 1f;
			EditorSpawnsItemsUI.radiusSlider.sizeOffset_X = 200;
			EditorSpawnsItemsUI.radiusSlider.sizeOffset_Y = 20;
			EditorSpawnsItemsUI.radiusSlider.state = (float)(EditorSpawns.radius - EditorSpawns.MIN_REMOVE_SIZE) / (float)EditorSpawns.MAX_REMOVE_SIZE;
			EditorSpawnsItemsUI.radiusSlider.orientation = ESleekOrientation.HORIZONTAL;
			EditorSpawnsItemsUI.radiusSlider.addLabel(local.format("RadiusSliderLabelText"), ESleekSide.RIGHT);
			SleekSlider sleekSlider = EditorSpawnsItemsUI.radiusSlider;
			if (EditorSpawnsItemsUI.<>f__mg$cacheE == null)
			{
				EditorSpawnsItemsUI.<>f__mg$cacheE = new Dragged(EditorSpawnsItemsUI.onDraggedRadiusSlider);
			}
			sleekSlider.onDragged = EditorSpawnsItemsUI.<>f__mg$cacheE;
			EditorSpawnsItemsUI.container.add(EditorSpawnsItemsUI.radiusSlider);
			EditorSpawnsItemsUI.addButton = new SleekButtonIcon((Texture2D)bundle.load("Add"));
			EditorSpawnsItemsUI.addButton.positionOffset_Y = -70;
			EditorSpawnsItemsUI.addButton.positionScale_Y = 1f;
			EditorSpawnsItemsUI.addButton.sizeOffset_X = 200;
			EditorSpawnsItemsUI.addButton.sizeOffset_Y = 30;
			EditorSpawnsItemsUI.addButton.text = local.format("AddButtonText", new object[]
			{
				ControlsSettings.tool_0
			});
			EditorSpawnsItemsUI.addButton.tooltip = local.format("AddButtonTooltip");
			SleekButton sleekButton7 = EditorSpawnsItemsUI.addButton;
			if (EditorSpawnsItemsUI.<>f__mg$cacheF == null)
			{
				EditorSpawnsItemsUI.<>f__mg$cacheF = new ClickedButton(EditorSpawnsItemsUI.onClickedAddButton);
			}
			sleekButton7.onClickedButton = EditorSpawnsItemsUI.<>f__mg$cacheF;
			EditorSpawnsItemsUI.container.add(EditorSpawnsItemsUI.addButton);
			EditorSpawnsItemsUI.removeButton = new SleekButtonIcon((Texture2D)bundle.load("Remove"));
			EditorSpawnsItemsUI.removeButton.positionOffset_Y = -30;
			EditorSpawnsItemsUI.removeButton.positionScale_Y = 1f;
			EditorSpawnsItemsUI.removeButton.sizeOffset_X = 200;
			EditorSpawnsItemsUI.removeButton.sizeOffset_Y = 30;
			EditorSpawnsItemsUI.removeButton.text = local.format("RemoveButtonText", new object[]
			{
				ControlsSettings.tool_1
			});
			EditorSpawnsItemsUI.removeButton.tooltip = local.format("RemoveButtonTooltip");
			SleekButton sleekButton8 = EditorSpawnsItemsUI.removeButton;
			if (EditorSpawnsItemsUI.<>f__mg$cache10 == null)
			{
				EditorSpawnsItemsUI.<>f__mg$cache10 = new ClickedButton(EditorSpawnsItemsUI.onClickedRemoveButton);
			}
			sleekButton8.onClickedButton = EditorSpawnsItemsUI.<>f__mg$cache10;
			EditorSpawnsItemsUI.container.add(EditorSpawnsItemsUI.removeButton);
			bundle.unload();
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x0016283E File Offset: 0x00160C3E
		public static void open()
		{
			if (EditorSpawnsItemsUI.active)
			{
				return;
			}
			EditorSpawnsItemsUI.active = true;
			EditorSpawns.isSpawning = true;
			EditorSpawns.spawnMode = ESpawnMode.ADD_ITEM;
			EditorSpawnsItemsUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x00162877 File Offset: 0x00160C77
		public static void close()
		{
			if (!EditorSpawnsItemsUI.active)
			{
				return;
			}
			EditorSpawnsItemsUI.active = false;
			EditorSpawns.isSpawning = false;
			EditorSpawnsItemsUI.container.lerpPositionScale(1f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x001628AC File Offset: 0x00160CAC
		public static void updateTables()
		{
			if (EditorSpawnsItemsUI.tableButtons != null)
			{
				for (int i = 0; i < EditorSpawnsItemsUI.tableButtons.Length; i++)
				{
					EditorSpawnsItemsUI.tableScrollBox.remove(EditorSpawnsItemsUI.tableButtons[i]);
				}
			}
			EditorSpawnsItemsUI.tableButtons = new SleekButton[LevelItems.tables.Count];
			EditorSpawnsItemsUI.tableScrollBox.area = new Rect(0f, 0f, 5f, (float)(EditorSpawnsItemsUI.tableButtons.Length * 40 - 10));
			for (int j = 0; j < EditorSpawnsItemsUI.tableButtons.Length; j++)
			{
				SleekButton sleekButton = new SleekButton();
				sleekButton.positionOffset_X = 240;
				sleekButton.positionOffset_Y = j * 40;
				sleekButton.sizeOffset_X = 200;
				sleekButton.sizeOffset_Y = 30;
				sleekButton.text = j + " " + LevelItems.tables[j].name;
				SleekButton sleekButton2 = sleekButton;
				if (EditorSpawnsItemsUI.<>f__mg$cache0 == null)
				{
					EditorSpawnsItemsUI.<>f__mg$cache0 = new ClickedButton(EditorSpawnsItemsUI.onClickedTableButton);
				}
				sleekButton2.onClickedButton = EditorSpawnsItemsUI.<>f__mg$cache0;
				EditorSpawnsItemsUI.tableScrollBox.add(sleekButton);
				EditorSpawnsItemsUI.tableButtons[j] = sleekButton;
			}
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x001629D0 File Offset: 0x00160DD0
		public static void updateSelection()
		{
			if ((int)EditorSpawns.selectedItem < LevelItems.tables.Count)
			{
				ItemTable itemTable = LevelItems.tables[(int)EditorSpawns.selectedItem];
				EditorSpawnsItemsUI.selectedBox.text = itemTable.name;
				EditorSpawnsItemsUI.tableNameField.text = itemTable.name;
				EditorSpawnsItemsUI.tableIDField.state = itemTable.tableID;
				EditorSpawnsItemsUI.tableColorPicker.state = itemTable.color;
				if (EditorSpawnsItemsUI.tierButtons != null)
				{
					for (int i = 0; i < EditorSpawnsItemsUI.tierButtons.Length; i++)
					{
						EditorSpawnsItemsUI.spawnsScrollBox.remove(EditorSpawnsItemsUI.tierButtons[i]);
					}
				}
				EditorSpawnsItemsUI.tierButtons = new SleekButton[itemTable.tiers.Count];
				for (int j = 0; j < EditorSpawnsItemsUI.tierButtons.Length; j++)
				{
					ItemTier itemTier = itemTable.tiers[j];
					SleekButton sleekButton = new SleekButton();
					sleekButton.positionOffset_X = 240;
					sleekButton.positionOffset_Y = 170 + j * 70;
					sleekButton.sizeOffset_X = 200;
					sleekButton.sizeOffset_Y = 30;
					sleekButton.text = itemTier.name;
					SleekButton sleekButton2 = sleekButton;
					if (EditorSpawnsItemsUI.<>f__mg$cache1 == null)
					{
						EditorSpawnsItemsUI.<>f__mg$cache1 = new ClickedButton(EditorSpawnsItemsUI.onClickedTierButton);
					}
					sleekButton2.onClickedButton = EditorSpawnsItemsUI.<>f__mg$cache1;
					EditorSpawnsItemsUI.spawnsScrollBox.add(sleekButton);
					SleekSlider sleekSlider = new SleekSlider();
					sleekSlider.positionOffset_Y = 40;
					sleekSlider.sizeOffset_X = 200;
					sleekSlider.sizeOffset_Y = 20;
					sleekSlider.orientation = ESleekOrientation.HORIZONTAL;
					sleekSlider.state = itemTier.chance;
					sleekSlider.addLabel(Mathf.RoundToInt(itemTier.chance * 100f) + "%", ESleekSide.LEFT);
					SleekSlider sleekSlider2 = sleekSlider;
					if (EditorSpawnsItemsUI.<>f__mg$cache2 == null)
					{
						EditorSpawnsItemsUI.<>f__mg$cache2 = new Dragged(EditorSpawnsItemsUI.onDraggedChanceSlider);
					}
					sleekSlider2.onDragged = EditorSpawnsItemsUI.<>f__mg$cache2;
					sleekButton.add(sleekSlider);
					EditorSpawnsItemsUI.tierButtons[j] = sleekButton;
				}
				EditorSpawnsItemsUI.tierNameField.positionOffset_Y = 170 + EditorSpawnsItemsUI.tierButtons.Length * 70;
				EditorSpawnsItemsUI.addTierButton.positionOffset_Y = 170 + EditorSpawnsItemsUI.tierButtons.Length * 70 + 40;
				EditorSpawnsItemsUI.removeTierButton.positionOffset_Y = 170 + EditorSpawnsItemsUI.tierButtons.Length * 70 + 40;
				if (EditorSpawnsItemsUI.itemButtons != null)
				{
					for (int k = 0; k < EditorSpawnsItemsUI.itemButtons.Length; k++)
					{
						EditorSpawnsItemsUI.spawnsScrollBox.remove(EditorSpawnsItemsUI.itemButtons[k]);
					}
				}
				if ((int)EditorSpawnsItemsUI.selectedTier < itemTable.tiers.Count)
				{
					EditorSpawnsItemsUI.tierNameField.text = itemTable.tiers[(int)EditorSpawnsItemsUI.selectedTier].name;
					EditorSpawnsItemsUI.itemButtons = new SleekButton[itemTable.tiers[(int)EditorSpawnsItemsUI.selectedTier].table.Count];
					for (int l = 0; l < EditorSpawnsItemsUI.itemButtons.Length; l++)
					{
						SleekButton sleekButton3 = new SleekButton();
						sleekButton3.positionOffset_X = 240;
						sleekButton3.positionOffset_Y = 170 + EditorSpawnsItemsUI.tierButtons.Length * 70 + 80 + l * 40;
						sleekButton3.sizeOffset_X = 200;
						sleekButton3.sizeOffset_Y = 30;
						ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, itemTable.tiers[(int)EditorSpawnsItemsUI.selectedTier].table[l].item);
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
						sleekButton3.text = itemTable.tiers[(int)EditorSpawnsItemsUI.selectedTier].table[l].item.ToString() + " " + str;
						SleekButton sleekButton4 = sleekButton3;
						if (EditorSpawnsItemsUI.<>f__mg$cache3 == null)
						{
							EditorSpawnsItemsUI.<>f__mg$cache3 = new ClickedButton(EditorSpawnsItemsUI.onClickItemButton);
						}
						sleekButton4.onClickedButton = EditorSpawnsItemsUI.<>f__mg$cache3;
						EditorSpawnsItemsUI.spawnsScrollBox.add(sleekButton3);
						EditorSpawnsItemsUI.itemButtons[l] = sleekButton3;
					}
				}
				else
				{
					EditorSpawnsItemsUI.tierNameField.text = string.Empty;
					EditorSpawnsItemsUI.itemButtons = new SleekButton[0];
				}
				EditorSpawnsItemsUI.itemIDField.positionOffset_Y = 170 + EditorSpawnsItemsUI.tierButtons.Length * 70 + 80 + EditorSpawnsItemsUI.itemButtons.Length * 40;
				EditorSpawnsItemsUI.addItemButton.positionOffset_Y = 170 + EditorSpawnsItemsUI.tierButtons.Length * 70 + 80 + EditorSpawnsItemsUI.itemButtons.Length * 40 + 40;
				EditorSpawnsItemsUI.removeItemButton.positionOffset_Y = 170 + EditorSpawnsItemsUI.tierButtons.Length * 70 + 80 + EditorSpawnsItemsUI.itemButtons.Length * 40 + 40;
				EditorSpawnsItemsUI.spawnsScrollBox.area = new Rect(0f, 0f, 5f, (float)(170 + EditorSpawnsItemsUI.tierButtons.Length * 70 + 80 + EditorSpawnsItemsUI.itemButtons.Length * 40 + 70));
			}
			else
			{
				EditorSpawnsItemsUI.selectedBox.text = string.Empty;
				EditorSpawnsItemsUI.tableNameField.text = string.Empty;
				EditorSpawnsItemsUI.tableIDField.state = 0;
				EditorSpawnsItemsUI.tableColorPicker.state = Color.white;
				if (EditorSpawnsItemsUI.tierButtons != null)
				{
					for (int m = 0; m < EditorSpawnsItemsUI.tierButtons.Length; m++)
					{
						EditorSpawnsItemsUI.spawnsScrollBox.remove(EditorSpawnsItemsUI.tierButtons[m]);
					}
				}
				EditorSpawnsItemsUI.tierButtons = null;
				EditorSpawnsItemsUI.tierNameField.text = string.Empty;
				EditorSpawnsItemsUI.tierNameField.positionOffset_Y = 170;
				EditorSpawnsItemsUI.addTierButton.positionOffset_Y = 210;
				EditorSpawnsItemsUI.removeTierButton.positionOffset_Y = 210;
				if (EditorSpawnsItemsUI.itemButtons != null)
				{
					for (int n = 0; n < EditorSpawnsItemsUI.itemButtons.Length; n++)
					{
						EditorSpawnsItemsUI.spawnsScrollBox.remove(EditorSpawnsItemsUI.itemButtons[n]);
					}
				}
				EditorSpawnsItemsUI.itemButtons = null;
				EditorSpawnsItemsUI.itemIDField.positionOffset_Y = 250;
				EditorSpawnsItemsUI.addItemButton.positionOffset_Y = 290;
				EditorSpawnsItemsUI.removeItemButton.positionOffset_Y = 290;
				EditorSpawnsItemsUI.spawnsScrollBox.area = new Rect(0f, 0f, 5f, 320f);
			}
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x00163008 File Offset: 0x00161408
		private static void onClickedTableButton(SleekButton button)
		{
			if (EditorSpawns.selectedItem != (byte)(button.positionOffset_Y / 40))
			{
				EditorSpawns.selectedItem = (byte)(button.positionOffset_Y / 40);
				EditorSpawns.itemSpawn.GetComponent<Renderer>().material.color = LevelItems.tables[(int)EditorSpawns.selectedItem].color;
			}
			else
			{
				EditorSpawns.selectedItem = byte.MaxValue;
				EditorSpawns.itemSpawn.GetComponent<Renderer>().material.color = Color.white;
			}
			EditorSpawnsItemsUI.updateSelection();
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x0016308D File Offset: 0x0016148D
		private static void onItemColorPicked(SleekColorPicker picker, Color color)
		{
			if ((int)EditorSpawns.selectedItem < LevelItems.tables.Count)
			{
				LevelItems.tables[(int)EditorSpawns.selectedItem].color = color;
			}
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x001630B8 File Offset: 0x001614B8
		private static void onTableIDFieldTyped(SleekUInt16Field field, ushort state)
		{
			if ((int)EditorSpawns.selectedItem < LevelItems.tables.Count)
			{
				LevelItems.tables[(int)EditorSpawns.selectedItem].tableID = state;
			}
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x001630E4 File Offset: 0x001614E4
		private static void onClickedTierButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedItem < LevelItems.tables.Count)
			{
				if (EditorSpawnsItemsUI.selectedTier != (byte)((button.positionOffset_Y - 170) / 70))
				{
					EditorSpawnsItemsUI.selectedTier = (byte)((button.positionOffset_Y - 170) / 70);
				}
				else
				{
					EditorSpawnsItemsUI.selectedTier = byte.MaxValue;
				}
				EditorSpawnsItemsUI.updateSelection();
			}
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x00163148 File Offset: 0x00161548
		private static void onClickItemButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedItem < LevelItems.tables.Count)
			{
				EditorSpawnsItemsUI.selectItem = (byte)((button.positionOffset_Y - 170 - EditorSpawnsItemsUI.tierButtons.Length * 70 - 80) / 40);
				EditorSpawnsItemsUI.updateSelection();
			}
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x00163188 File Offset: 0x00161588
		private static void onDraggedChanceSlider(SleekSlider slider, float state)
		{
			if ((int)EditorSpawns.selectedItem < LevelItems.tables.Count)
			{
				int num = (slider.parent.positionOffset_Y - 170) / 70;
				LevelItems.tables[(int)EditorSpawns.selectedItem].updateChance(num, state);
				for (int i = 0; i < LevelItems.tables[(int)EditorSpawns.selectedItem].tiers.Count; i++)
				{
					ItemTier itemTier = LevelItems.tables[(int)EditorSpawns.selectedItem].tiers[i];
					SleekSlider sleekSlider = (SleekSlider)EditorSpawnsItemsUI.tierButtons[i].children[0];
					if (i != num)
					{
						sleekSlider.state = itemTier.chance;
					}
					sleekSlider.updateLabel(Mathf.RoundToInt(itemTier.chance * 100f) + "%");
				}
			}
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x0016326C File Offset: 0x0016166C
		private static void onTypedTableNameField(SleekField field, string state)
		{
			if ((int)EditorSpawns.selectedItem < LevelItems.tables.Count)
			{
				EditorSpawnsItemsUI.selectedBox.text = state;
				LevelItems.tables[(int)EditorSpawns.selectedItem].name = state;
				EditorSpawnsItemsUI.tableButtons[(int)EditorSpawns.selectedItem].text = EditorSpawns.selectedItem + " " + state;
			}
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x001632D4 File Offset: 0x001616D4
		private static void onClickedAddTableButton(SleekButton button)
		{
			if (EditorSpawnsItemsUI.tableNameField.text != string.Empty)
			{
				LevelItems.addTable(EditorSpawnsItemsUI.tableNameField.text);
				EditorSpawnsItemsUI.tableNameField.text = string.Empty;
				EditorSpawnsItemsUI.updateTables();
				EditorSpawnsItemsUI.tableScrollBox.state = new Vector2(0f, float.MaxValue);
			}
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x00163336 File Offset: 0x00161736
		private static void onClickedRemoveTableButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedItem < LevelItems.tables.Count)
			{
				LevelItems.removeTable();
				EditorSpawnsItemsUI.updateTables();
				EditorSpawnsItemsUI.updateSelection();
				EditorSpawnsItemsUI.tableScrollBox.state = new Vector2(0f, float.MaxValue);
			}
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x00163374 File Offset: 0x00161774
		private static void onTypedTierNameField(SleekField field, string state)
		{
			if ((int)EditorSpawns.selectedItem < LevelItems.tables.Count && (int)EditorSpawnsItemsUI.selectedTier < LevelItems.tables[(int)EditorSpawns.selectedItem].tiers.Count)
			{
				LevelItems.tables[(int)EditorSpawns.selectedItem].tiers[(int)EditorSpawnsItemsUI.selectedTier].name = state;
				EditorSpawnsItemsUI.tierButtons[(int)EditorSpawnsItemsUI.selectedTier].text = state;
			}
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x001633F0 File Offset: 0x001617F0
		private static void onClickedAddTierButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedItem < LevelItems.tables.Count && EditorSpawnsItemsUI.tierNameField.text != string.Empty)
			{
				LevelItems.tables[(int)EditorSpawns.selectedItem].addTier(EditorSpawnsItemsUI.tierNameField.text);
				EditorSpawnsItemsUI.tierNameField.text = string.Empty;
				EditorSpawnsItemsUI.updateSelection();
			}
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x0016345C File Offset: 0x0016185C
		private static void onClickedRemoveTierButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedItem < LevelItems.tables.Count && (int)EditorSpawnsItemsUI.selectedTier < LevelItems.tables[(int)EditorSpawns.selectedItem].tiers.Count)
			{
				LevelItems.tables[(int)EditorSpawns.selectedItem].removeTier((int)EditorSpawnsItemsUI.selectedTier);
				EditorSpawnsItemsUI.updateSelection();
			}
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x001634C0 File Offset: 0x001618C0
		private static void onClickedAddItemButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedItem < LevelItems.tables.Count && (int)EditorSpawnsItemsUI.selectedTier < LevelItems.tables[(int)EditorSpawns.selectedItem].tiers.Count)
			{
				ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, EditorSpawnsItemsUI.itemIDField.state);
				if (itemAsset != null && !itemAsset.isPro)
				{
					LevelItems.tables[(int)EditorSpawns.selectedItem].addItem(EditorSpawnsItemsUI.selectedTier, EditorSpawnsItemsUI.itemIDField.state);
					EditorSpawnsItemsUI.updateSelection();
					EditorSpawnsItemsUI.spawnsScrollBox.state = new Vector2(0f, float.MaxValue);
				}
				EditorSpawnsItemsUI.itemIDField.state = 0;
			}
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x00163578 File Offset: 0x00161978
		private static void onClickedRemoveItemButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedItem < LevelItems.tables.Count && (int)EditorSpawnsItemsUI.selectedTier < LevelItems.tables[(int)EditorSpawns.selectedItem].tiers.Count && (int)EditorSpawnsItemsUI.selectItem < LevelItems.tables[(int)EditorSpawns.selectedItem].tiers[(int)EditorSpawnsItemsUI.selectedTier].table.Count)
			{
				LevelItems.tables[(int)EditorSpawns.selectedItem].removeItem(EditorSpawnsItemsUI.selectedTier, EditorSpawnsItemsUI.selectItem);
				EditorSpawnsItemsUI.updateSelection();
				EditorSpawnsItemsUI.spawnsScrollBox.state = new Vector2(0f, float.MaxValue);
			}
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x0016362A File Offset: 0x00161A2A
		private static void onDraggedRadiusSlider(SleekSlider slider, float state)
		{
			EditorSpawns.radius = (byte)((float)EditorSpawns.MIN_REMOVE_SIZE + state * (float)EditorSpawns.MAX_REMOVE_SIZE);
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x00163641 File Offset: 0x00161A41
		private static void onClickedAddButton(SleekButton button)
		{
			EditorSpawns.spawnMode = ESpawnMode.ADD_ITEM;
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x00163649 File Offset: 0x00161A49
		private static void onClickedRemoveButton(SleekButton button)
		{
			EditorSpawns.spawnMode = ESpawnMode.REMOVE_ITEM;
		}

		// Token: 0x040024C7 RID: 9415
		private static Sleek container;

		// Token: 0x040024C8 RID: 9416
		public static bool active;

		// Token: 0x040024C9 RID: 9417
		private static SleekScrollBox tableScrollBox;

		// Token: 0x040024CA RID: 9418
		private static SleekScrollBox spawnsScrollBox;

		// Token: 0x040024CB RID: 9419
		private static SleekButton[] tableButtons;

		// Token: 0x040024CC RID: 9420
		private static SleekButton[] tierButtons;

		// Token: 0x040024CD RID: 9421
		private static SleekButton[] itemButtons;

		// Token: 0x040024CE RID: 9422
		private static SleekColorPicker tableColorPicker;

		// Token: 0x040024CF RID: 9423
		private static SleekUInt16Field tableIDField;

		// Token: 0x040024D0 RID: 9424
		private static SleekField tierNameField;

		// Token: 0x040024D1 RID: 9425
		private static SleekButtonIcon addTierButton;

		// Token: 0x040024D2 RID: 9426
		private static SleekButtonIcon removeTierButton;

		// Token: 0x040024D3 RID: 9427
		private static SleekUInt16Field itemIDField;

		// Token: 0x040024D4 RID: 9428
		private static SleekButtonIcon addItemButton;

		// Token: 0x040024D5 RID: 9429
		private static SleekButtonIcon removeItemButton;

		// Token: 0x040024D6 RID: 9430
		private static SleekBox selectedBox;

		// Token: 0x040024D7 RID: 9431
		private static SleekField tableNameField;

		// Token: 0x040024D8 RID: 9432
		private static SleekButtonIcon addTableButton;

		// Token: 0x040024D9 RID: 9433
		private static SleekButtonIcon removeTableButton;

		// Token: 0x040024DA RID: 9434
		private static SleekSlider radiusSlider;

		// Token: 0x040024DB RID: 9435
		private static SleekButtonIcon addButton;

		// Token: 0x040024DC RID: 9436
		private static SleekButtonIcon removeButton;

		// Token: 0x040024DD RID: 9437
		private static byte selectedTier;

		// Token: 0x040024DE RID: 9438
		private static byte selectItem;

		// Token: 0x040024DF RID: 9439
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x040024E0 RID: 9440
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x040024E1 RID: 9441
		[CompilerGenerated]
		private static Dragged <>f__mg$cache2;

		// Token: 0x040024E2 RID: 9442
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x040024E3 RID: 9443
		[CompilerGenerated]
		private static Typed <>f__mg$cache4;

		// Token: 0x040024E4 RID: 9444
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;

		// Token: 0x040024E5 RID: 9445
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache6;

		// Token: 0x040024E6 RID: 9446
		[CompilerGenerated]
		private static ColorPicked <>f__mg$cache7;

		// Token: 0x040024E7 RID: 9447
		[CompilerGenerated]
		private static TypedUInt16 <>f__mg$cache8;

		// Token: 0x040024E8 RID: 9448
		[CompilerGenerated]
		private static Typed <>f__mg$cache9;

		// Token: 0x040024E9 RID: 9449
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheA;

		// Token: 0x040024EA RID: 9450
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheB;

		// Token: 0x040024EB RID: 9451
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheC;

		// Token: 0x040024EC RID: 9452
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheD;

		// Token: 0x040024ED RID: 9453
		[CompilerGenerated]
		private static Dragged <>f__mg$cacheE;

		// Token: 0x040024EE RID: 9454
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheF;

		// Token: 0x040024EF RID: 9455
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache10;
	}
}
