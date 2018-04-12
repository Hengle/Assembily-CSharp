using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000759 RID: 1881
	public class EditorSpawnsAnimalsUI
	{
		// Token: 0x06003522 RID: 13602 RVA: 0x00160488 File Offset: 0x0015E888
		public EditorSpawnsAnimalsUI()
		{
			Local local = Localization.read("/Editor/EditorSpawnsAnimals.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Edit/Icons/EditorSpawnsAnimals/EditorSpawnsAnimals.unity3d");
			EditorSpawnsAnimalsUI.container = new Sleek();
			EditorSpawnsAnimalsUI.container.positionOffset_X = 10;
			EditorSpawnsAnimalsUI.container.positionOffset_Y = 10;
			EditorSpawnsAnimalsUI.container.positionScale_X = 1f;
			EditorSpawnsAnimalsUI.container.sizeOffset_X = -20;
			EditorSpawnsAnimalsUI.container.sizeOffset_Y = -20;
			EditorSpawnsAnimalsUI.container.sizeScale_X = 1f;
			EditorSpawnsAnimalsUI.container.sizeScale_Y = 1f;
			EditorUI.window.add(EditorSpawnsAnimalsUI.container);
			EditorSpawnsAnimalsUI.active = false;
			EditorSpawnsAnimalsUI.tableScrollBox = new SleekScrollBox();
			EditorSpawnsAnimalsUI.tableScrollBox.positionOffset_X = -470;
			EditorSpawnsAnimalsUI.tableScrollBox.positionOffset_Y = 120;
			EditorSpawnsAnimalsUI.tableScrollBox.positionScale_X = 1f;
			EditorSpawnsAnimalsUI.tableScrollBox.sizeOffset_X = 470;
			EditorSpawnsAnimalsUI.tableScrollBox.sizeOffset_Y = 200;
			EditorSpawnsAnimalsUI.container.add(EditorSpawnsAnimalsUI.tableScrollBox);
			EditorSpawnsAnimalsUI.tableNameField = new SleekField();
			EditorSpawnsAnimalsUI.tableNameField.positionOffset_X = -230;
			EditorSpawnsAnimalsUI.tableNameField.positionOffset_Y = 330;
			EditorSpawnsAnimalsUI.tableNameField.positionScale_X = 1f;
			EditorSpawnsAnimalsUI.tableNameField.sizeOffset_X = 230;
			EditorSpawnsAnimalsUI.tableNameField.sizeOffset_Y = 30;
			EditorSpawnsAnimalsUI.tableNameField.maxLength = 64;
			EditorSpawnsAnimalsUI.tableNameField.addLabel(local.format("TableNameFieldLabelText"), ESleekSide.LEFT);
			SleekField sleekField = EditorSpawnsAnimalsUI.tableNameField;
			Delegate onTyped = sleekField.onTyped;
			if (EditorSpawnsAnimalsUI.<>f__mg$cache4 == null)
			{
				EditorSpawnsAnimalsUI.<>f__mg$cache4 = new Typed(EditorSpawnsAnimalsUI.onTypedNameField);
			}
			sleekField.onTyped = (Typed)Delegate.Combine(onTyped, EditorSpawnsAnimalsUI.<>f__mg$cache4);
			EditorSpawnsAnimalsUI.container.add(EditorSpawnsAnimalsUI.tableNameField);
			EditorSpawnsAnimalsUI.addTableButton = new SleekButtonIcon((Texture2D)bundle.load("Add"));
			EditorSpawnsAnimalsUI.addTableButton.positionOffset_X = -230;
			EditorSpawnsAnimalsUI.addTableButton.positionOffset_Y = 370;
			EditorSpawnsAnimalsUI.addTableButton.positionScale_X = 1f;
			EditorSpawnsAnimalsUI.addTableButton.sizeOffset_X = 110;
			EditorSpawnsAnimalsUI.addTableButton.sizeOffset_Y = 30;
			EditorSpawnsAnimalsUI.addTableButton.text = local.format("AddTableButtonText");
			EditorSpawnsAnimalsUI.addTableButton.tooltip = local.format("AddTableButtonTooltip");
			SleekButton sleekButton = EditorSpawnsAnimalsUI.addTableButton;
			if (EditorSpawnsAnimalsUI.<>f__mg$cache5 == null)
			{
				EditorSpawnsAnimalsUI.<>f__mg$cache5 = new ClickedButton(EditorSpawnsAnimalsUI.onClickedAddTableButton);
			}
			sleekButton.onClickedButton = EditorSpawnsAnimalsUI.<>f__mg$cache5;
			EditorSpawnsAnimalsUI.container.add(EditorSpawnsAnimalsUI.addTableButton);
			EditorSpawnsAnimalsUI.removeTableButton = new SleekButtonIcon((Texture2D)bundle.load("Remove"));
			EditorSpawnsAnimalsUI.removeTableButton.positionOffset_X = -110;
			EditorSpawnsAnimalsUI.removeTableButton.positionOffset_Y = 370;
			EditorSpawnsAnimalsUI.removeTableButton.positionScale_X = 1f;
			EditorSpawnsAnimalsUI.removeTableButton.sizeOffset_X = 110;
			EditorSpawnsAnimalsUI.removeTableButton.sizeOffset_Y = 30;
			EditorSpawnsAnimalsUI.removeTableButton.text = local.format("RemoveTableButtonText");
			EditorSpawnsAnimalsUI.removeTableButton.tooltip = local.format("RemoveTableButtonTooltip");
			SleekButton sleekButton2 = EditorSpawnsAnimalsUI.removeTableButton;
			if (EditorSpawnsAnimalsUI.<>f__mg$cache6 == null)
			{
				EditorSpawnsAnimalsUI.<>f__mg$cache6 = new ClickedButton(EditorSpawnsAnimalsUI.onClickedRemoveTableButton);
			}
			sleekButton2.onClickedButton = EditorSpawnsAnimalsUI.<>f__mg$cache6;
			EditorSpawnsAnimalsUI.container.add(EditorSpawnsAnimalsUI.removeTableButton);
			EditorSpawnsAnimalsUI.updateTables();
			EditorSpawnsAnimalsUI.spawnsScrollBox = new SleekScrollBox();
			EditorSpawnsAnimalsUI.spawnsScrollBox.positionOffset_X = -470;
			EditorSpawnsAnimalsUI.spawnsScrollBox.positionOffset_Y = 410;
			EditorSpawnsAnimalsUI.spawnsScrollBox.positionScale_X = 1f;
			EditorSpawnsAnimalsUI.spawnsScrollBox.sizeOffset_X = 470;
			EditorSpawnsAnimalsUI.spawnsScrollBox.sizeOffset_Y = -410;
			EditorSpawnsAnimalsUI.spawnsScrollBox.sizeScale_Y = 1f;
			EditorSpawnsAnimalsUI.spawnsScrollBox.area = new Rect(0f, 0f, 5f, 1000f);
			EditorSpawnsAnimalsUI.container.add(EditorSpawnsAnimalsUI.spawnsScrollBox);
			EditorSpawnsAnimalsUI.tableColorPicker = new SleekColorPicker();
			EditorSpawnsAnimalsUI.tableColorPicker.positionOffset_X = 200;
			SleekColorPicker sleekColorPicker = EditorSpawnsAnimalsUI.tableColorPicker;
			if (EditorSpawnsAnimalsUI.<>f__mg$cache7 == null)
			{
				EditorSpawnsAnimalsUI.<>f__mg$cache7 = new ColorPicked(EditorSpawnsAnimalsUI.onAnimalColorPicked);
			}
			sleekColorPicker.onColorPicked = EditorSpawnsAnimalsUI.<>f__mg$cache7;
			EditorSpawnsAnimalsUI.spawnsScrollBox.add(EditorSpawnsAnimalsUI.tableColorPicker);
			EditorSpawnsAnimalsUI.tableIDField = new SleekUInt16Field();
			EditorSpawnsAnimalsUI.tableIDField.positionOffset_X = 240;
			EditorSpawnsAnimalsUI.tableIDField.positionOffset_Y = 130;
			EditorSpawnsAnimalsUI.tableIDField.sizeOffset_X = 200;
			EditorSpawnsAnimalsUI.tableIDField.sizeOffset_Y = 30;
			SleekUInt16Field sleekUInt16Field = EditorSpawnsAnimalsUI.tableIDField;
			if (EditorSpawnsAnimalsUI.<>f__mg$cache8 == null)
			{
				EditorSpawnsAnimalsUI.<>f__mg$cache8 = new TypedUInt16(EditorSpawnsAnimalsUI.onTableIDFieldTyped);
			}
			sleekUInt16Field.onTypedUInt16 = EditorSpawnsAnimalsUI.<>f__mg$cache8;
			EditorSpawnsAnimalsUI.tableIDField.addLabel(local.format("TableIDFieldLabelText"), ESleekSide.LEFT);
			EditorSpawnsAnimalsUI.spawnsScrollBox.add(EditorSpawnsAnimalsUI.tableIDField);
			EditorSpawnsAnimalsUI.tierNameField = new SleekField();
			EditorSpawnsAnimalsUI.tierNameField.positionOffset_X = 240;
			EditorSpawnsAnimalsUI.tierNameField.sizeOffset_X = 200;
			EditorSpawnsAnimalsUI.tierNameField.sizeOffset_Y = 30;
			EditorSpawnsAnimalsUI.tierNameField.maxLength = 64;
			EditorSpawnsAnimalsUI.tierNameField.addLabel(local.format("TierNameFieldLabelText"), ESleekSide.LEFT);
			SleekField sleekField2 = EditorSpawnsAnimalsUI.tierNameField;
			if (EditorSpawnsAnimalsUI.<>f__mg$cache9 == null)
			{
				EditorSpawnsAnimalsUI.<>f__mg$cache9 = new Typed(EditorSpawnsAnimalsUI.onTypedTierNameField);
			}
			sleekField2.onTyped = EditorSpawnsAnimalsUI.<>f__mg$cache9;
			EditorSpawnsAnimalsUI.spawnsScrollBox.add(EditorSpawnsAnimalsUI.tierNameField);
			EditorSpawnsAnimalsUI.addTierButton = new SleekButtonIcon((Texture2D)bundle.load("Add"));
			EditorSpawnsAnimalsUI.addTierButton.positionOffset_X = 240;
			EditorSpawnsAnimalsUI.addTierButton.sizeOffset_X = 95;
			EditorSpawnsAnimalsUI.addTierButton.sizeOffset_Y = 30;
			EditorSpawnsAnimalsUI.addTierButton.text = local.format("AddTierButtonText");
			EditorSpawnsAnimalsUI.addTierButton.tooltip = local.format("AddTierButtonTooltip");
			SleekButton sleekButton3 = EditorSpawnsAnimalsUI.addTierButton;
			if (EditorSpawnsAnimalsUI.<>f__mg$cacheA == null)
			{
				EditorSpawnsAnimalsUI.<>f__mg$cacheA = new ClickedButton(EditorSpawnsAnimalsUI.onClickedAddTierButton);
			}
			sleekButton3.onClickedButton = EditorSpawnsAnimalsUI.<>f__mg$cacheA;
			EditorSpawnsAnimalsUI.spawnsScrollBox.add(EditorSpawnsAnimalsUI.addTierButton);
			EditorSpawnsAnimalsUI.removeTierButton = new SleekButtonIcon((Texture2D)bundle.load("Remove"));
			EditorSpawnsAnimalsUI.removeTierButton.positionOffset_X = 345;
			EditorSpawnsAnimalsUI.removeTierButton.sizeOffset_X = 95;
			EditorSpawnsAnimalsUI.removeTierButton.sizeOffset_Y = 30;
			EditorSpawnsAnimalsUI.removeTierButton.text = local.format("RemoveTierButtonText");
			EditorSpawnsAnimalsUI.removeTierButton.tooltip = local.format("RemoveTierButtonTooltip");
			SleekButton sleekButton4 = EditorSpawnsAnimalsUI.removeTierButton;
			if (EditorSpawnsAnimalsUI.<>f__mg$cacheB == null)
			{
				EditorSpawnsAnimalsUI.<>f__mg$cacheB = new ClickedButton(EditorSpawnsAnimalsUI.onClickedRemoveTierButton);
			}
			sleekButton4.onClickedButton = EditorSpawnsAnimalsUI.<>f__mg$cacheB;
			EditorSpawnsAnimalsUI.spawnsScrollBox.add(EditorSpawnsAnimalsUI.removeTierButton);
			EditorSpawnsAnimalsUI.animalIDField = new SleekUInt16Field();
			EditorSpawnsAnimalsUI.animalIDField.positionOffset_X = 240;
			EditorSpawnsAnimalsUI.animalIDField.sizeOffset_X = 200;
			EditorSpawnsAnimalsUI.animalIDField.sizeOffset_Y = 30;
			EditorSpawnsAnimalsUI.animalIDField.addLabel(local.format("AnimalIDFieldLabelText"), ESleekSide.LEFT);
			EditorSpawnsAnimalsUI.spawnsScrollBox.add(EditorSpawnsAnimalsUI.animalIDField);
			EditorSpawnsAnimalsUI.addAnimalButton = new SleekButtonIcon((Texture2D)bundle.load("Add"));
			EditorSpawnsAnimalsUI.addAnimalButton.positionOffset_X = 240;
			EditorSpawnsAnimalsUI.addAnimalButton.sizeOffset_X = 95;
			EditorSpawnsAnimalsUI.addAnimalButton.sizeOffset_Y = 30;
			EditorSpawnsAnimalsUI.addAnimalButton.text = local.format("AddAnimalButtonText");
			EditorSpawnsAnimalsUI.addAnimalButton.tooltip = local.format("AddAnimalButtonTooltip");
			SleekButton sleekButton5 = EditorSpawnsAnimalsUI.addAnimalButton;
			if (EditorSpawnsAnimalsUI.<>f__mg$cacheC == null)
			{
				EditorSpawnsAnimalsUI.<>f__mg$cacheC = new ClickedButton(EditorSpawnsAnimalsUI.onClickedAddAnimalButton);
			}
			sleekButton5.onClickedButton = EditorSpawnsAnimalsUI.<>f__mg$cacheC;
			EditorSpawnsAnimalsUI.spawnsScrollBox.add(EditorSpawnsAnimalsUI.addAnimalButton);
			EditorSpawnsAnimalsUI.removeAnimalButton = new SleekButtonIcon((Texture2D)bundle.load("Remove"));
			EditorSpawnsAnimalsUI.removeAnimalButton.positionOffset_X = 345;
			EditorSpawnsAnimalsUI.removeAnimalButton.sizeOffset_X = 95;
			EditorSpawnsAnimalsUI.removeAnimalButton.sizeOffset_Y = 30;
			EditorSpawnsAnimalsUI.removeAnimalButton.text = local.format("RemoveAnimalButtonText");
			EditorSpawnsAnimalsUI.removeAnimalButton.tooltip = local.format("RemoveAnimalButtonTooltip");
			SleekButton sleekButton6 = EditorSpawnsAnimalsUI.removeAnimalButton;
			if (EditorSpawnsAnimalsUI.<>f__mg$cacheD == null)
			{
				EditorSpawnsAnimalsUI.<>f__mg$cacheD = new ClickedButton(EditorSpawnsAnimalsUI.onClickedRemoveAnimalButton);
			}
			sleekButton6.onClickedButton = EditorSpawnsAnimalsUI.<>f__mg$cacheD;
			EditorSpawnsAnimalsUI.spawnsScrollBox.add(EditorSpawnsAnimalsUI.removeAnimalButton);
			EditorSpawnsAnimalsUI.selectedBox = new SleekBox();
			EditorSpawnsAnimalsUI.selectedBox.positionOffset_X = -230;
			EditorSpawnsAnimalsUI.selectedBox.positionOffset_Y = 80;
			EditorSpawnsAnimalsUI.selectedBox.positionScale_X = 1f;
			EditorSpawnsAnimalsUI.selectedBox.sizeOffset_X = 230;
			EditorSpawnsAnimalsUI.selectedBox.sizeOffset_Y = 30;
			EditorSpawnsAnimalsUI.selectedBox.addLabel(local.format("SelectionBoxLabelText"), ESleekSide.LEFT);
			EditorSpawnsAnimalsUI.container.add(EditorSpawnsAnimalsUI.selectedBox);
			EditorSpawnsAnimalsUI.updateSelection();
			EditorSpawnsAnimalsUI.radiusSlider = new SleekSlider();
			EditorSpawnsAnimalsUI.radiusSlider.positionOffset_Y = -100;
			EditorSpawnsAnimalsUI.radiusSlider.positionScale_Y = 1f;
			EditorSpawnsAnimalsUI.radiusSlider.sizeOffset_X = 200;
			EditorSpawnsAnimalsUI.radiusSlider.sizeOffset_Y = 20;
			EditorSpawnsAnimalsUI.radiusSlider.state = (float)(EditorSpawns.radius - EditorSpawns.MIN_REMOVE_SIZE) / (float)EditorSpawns.MAX_REMOVE_SIZE;
			EditorSpawnsAnimalsUI.radiusSlider.orientation = ESleekOrientation.HORIZONTAL;
			EditorSpawnsAnimalsUI.radiusSlider.addLabel(local.format("RadiusSliderLabelText"), ESleekSide.RIGHT);
			SleekSlider sleekSlider = EditorSpawnsAnimalsUI.radiusSlider;
			if (EditorSpawnsAnimalsUI.<>f__mg$cacheE == null)
			{
				EditorSpawnsAnimalsUI.<>f__mg$cacheE = new Dragged(EditorSpawnsAnimalsUI.onDraggedRadiusSlider);
			}
			sleekSlider.onDragged = EditorSpawnsAnimalsUI.<>f__mg$cacheE;
			EditorSpawnsAnimalsUI.container.add(EditorSpawnsAnimalsUI.radiusSlider);
			EditorSpawnsAnimalsUI.addButton = new SleekButtonIcon((Texture2D)bundle.load("Add"));
			EditorSpawnsAnimalsUI.addButton.positionOffset_Y = -70;
			EditorSpawnsAnimalsUI.addButton.positionScale_Y = 1f;
			EditorSpawnsAnimalsUI.addButton.sizeOffset_X = 200;
			EditorSpawnsAnimalsUI.addButton.sizeOffset_Y = 30;
			EditorSpawnsAnimalsUI.addButton.text = local.format("AddButtonText", new object[]
			{
				ControlsSettings.tool_0
			});
			EditorSpawnsAnimalsUI.addButton.tooltip = local.format("AddButtonTooltip");
			SleekButton sleekButton7 = EditorSpawnsAnimalsUI.addButton;
			if (EditorSpawnsAnimalsUI.<>f__mg$cacheF == null)
			{
				EditorSpawnsAnimalsUI.<>f__mg$cacheF = new ClickedButton(EditorSpawnsAnimalsUI.onClickedAddButton);
			}
			sleekButton7.onClickedButton = EditorSpawnsAnimalsUI.<>f__mg$cacheF;
			EditorSpawnsAnimalsUI.container.add(EditorSpawnsAnimalsUI.addButton);
			EditorSpawnsAnimalsUI.removeButton = new SleekButtonIcon((Texture2D)bundle.load("Remove"));
			EditorSpawnsAnimalsUI.removeButton.positionOffset_Y = -30;
			EditorSpawnsAnimalsUI.removeButton.positionScale_Y = 1f;
			EditorSpawnsAnimalsUI.removeButton.sizeOffset_X = 200;
			EditorSpawnsAnimalsUI.removeButton.sizeOffset_Y = 30;
			EditorSpawnsAnimalsUI.removeButton.text = local.format("RemoveButtonText", new object[]
			{
				ControlsSettings.tool_1
			});
			EditorSpawnsAnimalsUI.removeButton.tooltip = local.format("RemoveButtonTooltip");
			SleekButton sleekButton8 = EditorSpawnsAnimalsUI.removeButton;
			if (EditorSpawnsAnimalsUI.<>f__mg$cache10 == null)
			{
				EditorSpawnsAnimalsUI.<>f__mg$cache10 = new ClickedButton(EditorSpawnsAnimalsUI.onClickedRemoveButton);
			}
			sleekButton8.onClickedButton = EditorSpawnsAnimalsUI.<>f__mg$cache10;
			EditorSpawnsAnimalsUI.container.add(EditorSpawnsAnimalsUI.removeButton);
			bundle.unload();
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x00160F5E File Offset: 0x0015F35E
		public static void open()
		{
			if (EditorSpawnsAnimalsUI.active)
			{
				return;
			}
			EditorSpawnsAnimalsUI.active = true;
			EditorSpawns.isSpawning = true;
			EditorSpawns.spawnMode = ESpawnMode.ADD_ANIMAL;
			EditorSpawnsAnimalsUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x00160F98 File Offset: 0x0015F398
		public static void close()
		{
			if (!EditorSpawnsAnimalsUI.active)
			{
				return;
			}
			EditorSpawnsAnimalsUI.active = false;
			EditorSpawns.isSpawning = false;
			EditorSpawnsAnimalsUI.container.lerpPositionScale(1f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x00160FCC File Offset: 0x0015F3CC
		public static void updateTables()
		{
			if (EditorSpawnsAnimalsUI.tableButtons != null)
			{
				for (int i = 0; i < EditorSpawnsAnimalsUI.tableButtons.Length; i++)
				{
					EditorSpawnsAnimalsUI.tableScrollBox.remove(EditorSpawnsAnimalsUI.tableButtons[i]);
				}
			}
			EditorSpawnsAnimalsUI.tableButtons = new SleekButton[LevelAnimals.tables.Count];
			EditorSpawnsAnimalsUI.tableScrollBox.area = new Rect(0f, 0f, 5f, (float)(EditorSpawnsAnimalsUI.tableButtons.Length * 40 - 10));
			for (int j = 0; j < EditorSpawnsAnimalsUI.tableButtons.Length; j++)
			{
				SleekButton sleekButton = new SleekButton();
				sleekButton.positionOffset_X = 240;
				sleekButton.positionOffset_Y = j * 40;
				sleekButton.sizeOffset_X = 200;
				sleekButton.sizeOffset_Y = 30;
				sleekButton.text = j + " " + LevelAnimals.tables[j].name;
				SleekButton sleekButton2 = sleekButton;
				if (EditorSpawnsAnimalsUI.<>f__mg$cache0 == null)
				{
					EditorSpawnsAnimalsUI.<>f__mg$cache0 = new ClickedButton(EditorSpawnsAnimalsUI.onClickedTableButton);
				}
				sleekButton2.onClickedButton = EditorSpawnsAnimalsUI.<>f__mg$cache0;
				EditorSpawnsAnimalsUI.tableScrollBox.add(sleekButton);
				EditorSpawnsAnimalsUI.tableButtons[j] = sleekButton;
			}
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x001610F0 File Offset: 0x0015F4F0
		public static void updateSelection()
		{
			if ((int)EditorSpawns.selectedAnimal < LevelAnimals.tables.Count)
			{
				AnimalTable animalTable = LevelAnimals.tables[(int)EditorSpawns.selectedAnimal];
				EditorSpawnsAnimalsUI.selectedBox.text = animalTable.name;
				EditorSpawnsAnimalsUI.tableNameField.text = animalTable.name;
				EditorSpawnsAnimalsUI.tableIDField.state = animalTable.tableID;
				EditorSpawnsAnimalsUI.tableColorPicker.state = animalTable.color;
				if (EditorSpawnsAnimalsUI.tierButtons != null)
				{
					for (int i = 0; i < EditorSpawnsAnimalsUI.tierButtons.Length; i++)
					{
						EditorSpawnsAnimalsUI.spawnsScrollBox.remove(EditorSpawnsAnimalsUI.tierButtons[i]);
					}
				}
				EditorSpawnsAnimalsUI.tierButtons = new SleekButton[animalTable.tiers.Count];
				for (int j = 0; j < EditorSpawnsAnimalsUI.tierButtons.Length; j++)
				{
					AnimalTier animalTier = animalTable.tiers[j];
					SleekButton sleekButton = new SleekButton();
					sleekButton.positionOffset_X = 240;
					sleekButton.positionOffset_Y = 170 + j * 70;
					sleekButton.sizeOffset_X = 200;
					sleekButton.sizeOffset_Y = 30;
					sleekButton.text = animalTier.name;
					SleekButton sleekButton2 = sleekButton;
					if (EditorSpawnsAnimalsUI.<>f__mg$cache1 == null)
					{
						EditorSpawnsAnimalsUI.<>f__mg$cache1 = new ClickedButton(EditorSpawnsAnimalsUI.onClickedTierButton);
					}
					sleekButton2.onClickedButton = EditorSpawnsAnimalsUI.<>f__mg$cache1;
					EditorSpawnsAnimalsUI.spawnsScrollBox.add(sleekButton);
					SleekSlider sleekSlider = new SleekSlider();
					sleekSlider.positionOffset_Y = 40;
					sleekSlider.sizeOffset_X = 200;
					sleekSlider.sizeOffset_Y = 20;
					sleekSlider.orientation = ESleekOrientation.HORIZONTAL;
					sleekSlider.state = animalTier.chance;
					sleekSlider.addLabel(Mathf.RoundToInt(animalTier.chance * 100f) + "%", ESleekSide.LEFT);
					SleekSlider sleekSlider2 = sleekSlider;
					if (EditorSpawnsAnimalsUI.<>f__mg$cache2 == null)
					{
						EditorSpawnsAnimalsUI.<>f__mg$cache2 = new Dragged(EditorSpawnsAnimalsUI.onDraggedChanceSlider);
					}
					sleekSlider2.onDragged = EditorSpawnsAnimalsUI.<>f__mg$cache2;
					sleekButton.add(sleekSlider);
					EditorSpawnsAnimalsUI.tierButtons[j] = sleekButton;
				}
				EditorSpawnsAnimalsUI.tierNameField.positionOffset_Y = 170 + EditorSpawnsAnimalsUI.tierButtons.Length * 70;
				EditorSpawnsAnimalsUI.addTierButton.positionOffset_Y = 170 + EditorSpawnsAnimalsUI.tierButtons.Length * 70 + 40;
				EditorSpawnsAnimalsUI.removeTierButton.positionOffset_Y = 170 + EditorSpawnsAnimalsUI.tierButtons.Length * 70 + 40;
				if (EditorSpawnsAnimalsUI.animalButtons != null)
				{
					for (int k = 0; k < EditorSpawnsAnimalsUI.animalButtons.Length; k++)
					{
						EditorSpawnsAnimalsUI.spawnsScrollBox.remove(EditorSpawnsAnimalsUI.animalButtons[k]);
					}
				}
				if ((int)EditorSpawnsAnimalsUI.selectedTier < animalTable.tiers.Count)
				{
					EditorSpawnsAnimalsUI.tierNameField.text = animalTable.tiers[(int)EditorSpawnsAnimalsUI.selectedTier].name;
					EditorSpawnsAnimalsUI.animalButtons = new SleekButton[animalTable.tiers[(int)EditorSpawnsAnimalsUI.selectedTier].table.Count];
					for (int l = 0; l < EditorSpawnsAnimalsUI.animalButtons.Length; l++)
					{
						SleekButton sleekButton3 = new SleekButton();
						sleekButton3.positionOffset_X = 240;
						sleekButton3.positionOffset_Y = 170 + EditorSpawnsAnimalsUI.tierButtons.Length * 70 + 80 + l * 40;
						sleekButton3.sizeOffset_X = 200;
						sleekButton3.sizeOffset_Y = 30;
						AnimalAsset animalAsset = (AnimalAsset)Assets.find(EAssetType.ANIMAL, animalTable.tiers[(int)EditorSpawnsAnimalsUI.selectedTier].table[l].animal);
						string str = "?";
						if (animalAsset != null)
						{
							if (string.IsNullOrEmpty(animalAsset.animalName))
							{
								str = animalAsset.name;
							}
							else
							{
								str = animalAsset.animalName;
							}
						}
						sleekButton3.text = animalTable.tiers[(int)EditorSpawnsAnimalsUI.selectedTier].table[l].animal.ToString() + " " + str;
						SleekButton sleekButton4 = sleekButton3;
						if (EditorSpawnsAnimalsUI.<>f__mg$cache3 == null)
						{
							EditorSpawnsAnimalsUI.<>f__mg$cache3 = new ClickedButton(EditorSpawnsAnimalsUI.onClickAnimalButton);
						}
						sleekButton4.onClickedButton = EditorSpawnsAnimalsUI.<>f__mg$cache3;
						EditorSpawnsAnimalsUI.spawnsScrollBox.add(sleekButton3);
						EditorSpawnsAnimalsUI.animalButtons[l] = sleekButton3;
					}
				}
				else
				{
					EditorSpawnsAnimalsUI.tierNameField.text = string.Empty;
					EditorSpawnsAnimalsUI.animalButtons = new SleekButton[0];
				}
				EditorSpawnsAnimalsUI.animalIDField.positionOffset_Y = 170 + EditorSpawnsAnimalsUI.tierButtons.Length * 70 + 80 + EditorSpawnsAnimalsUI.animalButtons.Length * 40;
				EditorSpawnsAnimalsUI.addAnimalButton.positionOffset_Y = 170 + EditorSpawnsAnimalsUI.tierButtons.Length * 70 + 80 + EditorSpawnsAnimalsUI.animalButtons.Length * 40 + 40;
				EditorSpawnsAnimalsUI.removeAnimalButton.positionOffset_Y = 170 + EditorSpawnsAnimalsUI.tierButtons.Length * 70 + 80 + EditorSpawnsAnimalsUI.animalButtons.Length * 40 + 40;
				EditorSpawnsAnimalsUI.spawnsScrollBox.area = new Rect(0f, 0f, 5f, (float)(170 + EditorSpawnsAnimalsUI.tierButtons.Length * 70 + 80 + EditorSpawnsAnimalsUI.animalButtons.Length * 40 + 70));
			}
			else
			{
				EditorSpawnsAnimalsUI.selectedBox.text = string.Empty;
				EditorSpawnsAnimalsUI.tableNameField.text = string.Empty;
				EditorSpawnsAnimalsUI.tableIDField.state = 0;
				EditorSpawnsAnimalsUI.tableColorPicker.state = Color.white;
				if (EditorSpawnsAnimalsUI.tierButtons != null)
				{
					for (int m = 0; m < EditorSpawnsAnimalsUI.tierButtons.Length; m++)
					{
						EditorSpawnsAnimalsUI.spawnsScrollBox.remove(EditorSpawnsAnimalsUI.tierButtons[m]);
					}
				}
				EditorSpawnsAnimalsUI.tierButtons = null;
				EditorSpawnsAnimalsUI.tierNameField.text = string.Empty;
				EditorSpawnsAnimalsUI.tierNameField.positionOffset_Y = 170;
				EditorSpawnsAnimalsUI.addTierButton.positionOffset_Y = 210;
				EditorSpawnsAnimalsUI.removeTierButton.positionOffset_Y = 210;
				if (EditorSpawnsAnimalsUI.animalButtons != null)
				{
					for (int n = 0; n < EditorSpawnsAnimalsUI.animalButtons.Length; n++)
					{
						EditorSpawnsAnimalsUI.spawnsScrollBox.remove(EditorSpawnsAnimalsUI.animalButtons[n]);
					}
				}
				EditorSpawnsAnimalsUI.animalButtons = null;
				EditorSpawnsAnimalsUI.animalIDField.positionOffset_Y = 250;
				EditorSpawnsAnimalsUI.addAnimalButton.positionOffset_Y = 290;
				EditorSpawnsAnimalsUI.removeAnimalButton.positionOffset_Y = 290;
				EditorSpawnsAnimalsUI.spawnsScrollBox.area = new Rect(0f, 0f, 5f, 320f);
			}
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x00161728 File Offset: 0x0015FB28
		private static void onClickedTableButton(SleekButton button)
		{
			if (EditorSpawns.selectedAnimal != (byte)(button.positionOffset_Y / 40))
			{
				EditorSpawns.selectedAnimal = (byte)(button.positionOffset_Y / 40);
				EditorSpawns.animalSpawn.GetComponent<Renderer>().material.color = LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].color;
			}
			else
			{
				EditorSpawns.selectedAnimal = byte.MaxValue;
				EditorSpawns.animalSpawn.GetComponent<Renderer>().material.color = Color.white;
			}
			EditorSpawnsAnimalsUI.updateSelection();
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x001617AD File Offset: 0x0015FBAD
		private static void onAnimalColorPicked(SleekColorPicker picker, Color color)
		{
			if ((int)EditorSpawns.selectedAnimal < LevelAnimals.tables.Count)
			{
				LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].color = color;
			}
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x001617D8 File Offset: 0x0015FBD8
		private static void onTableIDFieldTyped(SleekUInt16Field field, ushort state)
		{
			if ((int)EditorSpawns.selectedAnimal < LevelAnimals.tables.Count)
			{
				LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].tableID = state;
			}
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x00161804 File Offset: 0x0015FC04
		private static void onClickedTierButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedAnimal < LevelAnimals.tables.Count)
			{
				if (EditorSpawnsAnimalsUI.selectedTier != (byte)((button.positionOffset_Y - 170) / 70))
				{
					EditorSpawnsAnimalsUI.selectedTier = (byte)((button.positionOffset_Y - 170) / 70);
				}
				else
				{
					EditorSpawnsAnimalsUI.selectedTier = byte.MaxValue;
				}
				EditorSpawnsAnimalsUI.updateSelection();
			}
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x00161868 File Offset: 0x0015FC68
		private static void onClickAnimalButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedAnimal < LevelAnimals.tables.Count)
			{
				EditorSpawnsAnimalsUI.selectAnimal = (byte)((button.positionOffset_Y - 170 - EditorSpawnsAnimalsUI.tierButtons.Length * 70 - 80) / 40);
				EditorSpawnsAnimalsUI.updateSelection();
			}
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x001618A8 File Offset: 0x0015FCA8
		private static void onDraggedChanceSlider(SleekSlider slider, float state)
		{
			if ((int)EditorSpawns.selectedAnimal < LevelAnimals.tables.Count)
			{
				int num = (slider.parent.positionOffset_Y - 170) / 70;
				LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].updateChance(num, state);
				for (int i = 0; i < LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].tiers.Count; i++)
				{
					AnimalTier animalTier = LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].tiers[i];
					SleekSlider sleekSlider = (SleekSlider)EditorSpawnsAnimalsUI.tierButtons[i].children[0];
					if (i != num)
					{
						sleekSlider.state = animalTier.chance;
					}
					sleekSlider.updateLabel(Mathf.RoundToInt(animalTier.chance * 100f) + "%");
				}
			}
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x0016198C File Offset: 0x0015FD8C
		private static void onTypedNameField(SleekField field, string state)
		{
			if ((int)EditorSpawns.selectedAnimal < LevelAnimals.tables.Count)
			{
				EditorSpawnsAnimalsUI.selectedBox.text = state;
				LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].name = state;
				EditorSpawnsAnimalsUI.tableButtons[(int)EditorSpawns.selectedAnimal].text = EditorSpawns.selectedAnimal + " " + state;
			}
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x001619F4 File Offset: 0x0015FDF4
		private static void onClickedAddTableButton(SleekButton button)
		{
			if (EditorSpawnsAnimalsUI.tableNameField.text != string.Empty)
			{
				LevelAnimals.addTable(EditorSpawnsAnimalsUI.tableNameField.text);
				EditorSpawnsAnimalsUI.tableNameField.text = string.Empty;
				EditorSpawnsAnimalsUI.updateTables();
				EditorSpawnsAnimalsUI.tableScrollBox.state = new Vector2(0f, float.MaxValue);
			}
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x00161A56 File Offset: 0x0015FE56
		private static void onClickedRemoveTableButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedAnimal < LevelAnimals.tables.Count)
			{
				LevelAnimals.removeTable();
				EditorSpawnsAnimalsUI.updateTables();
				EditorSpawnsAnimalsUI.updateSelection();
				EditorSpawnsAnimalsUI.tableScrollBox.state = new Vector2(0f, float.MaxValue);
			}
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x00161A94 File Offset: 0x0015FE94
		private static void onTypedTierNameField(SleekField field, string state)
		{
			if ((int)EditorSpawns.selectedAnimal < LevelAnimals.tables.Count && (int)EditorSpawnsAnimalsUI.selectedTier < LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].tiers.Count)
			{
				LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].tiers[(int)EditorSpawnsAnimalsUI.selectedTier].name = state;
				EditorSpawnsAnimalsUI.tierButtons[(int)EditorSpawnsAnimalsUI.selectedTier].text = state;
			}
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x00161B10 File Offset: 0x0015FF10
		private static void onClickedAddTierButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedAnimal < LevelAnimals.tables.Count && EditorSpawnsAnimalsUI.tierNameField.text != string.Empty)
			{
				LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].addTier(EditorSpawnsAnimalsUI.tierNameField.text);
				EditorSpawnsAnimalsUI.tierNameField.text = string.Empty;
				EditorSpawnsAnimalsUI.updateSelection();
			}
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x00161B7C File Offset: 0x0015FF7C
		private static void onClickedRemoveTierButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedAnimal < LevelAnimals.tables.Count && (int)EditorSpawnsAnimalsUI.selectedTier < LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].tiers.Count)
			{
				LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].removeTier((int)EditorSpawnsAnimalsUI.selectedTier);
				EditorSpawnsAnimalsUI.updateSelection();
			}
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x00161BE0 File Offset: 0x0015FFE0
		private static void onClickedAddAnimalButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedAnimal < LevelAnimals.tables.Count && (int)EditorSpawnsAnimalsUI.selectedTier < LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].tiers.Count)
			{
				AnimalAsset animalAsset = (AnimalAsset)Assets.find(EAssetType.ANIMAL, EditorSpawnsAnimalsUI.animalIDField.state);
				if (animalAsset != null)
				{
					LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].addAnimal(EditorSpawnsAnimalsUI.selectedTier, EditorSpawnsAnimalsUI.animalIDField.state);
					EditorSpawnsAnimalsUI.updateSelection();
					EditorSpawnsAnimalsUI.spawnsScrollBox.state = new Vector2(0f, float.MaxValue);
				}
				EditorSpawnsAnimalsUI.animalIDField.state = 0;
			}
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x00161C8C File Offset: 0x0016008C
		private static void onClickedRemoveAnimalButton(SleekButton button)
		{
			if ((int)EditorSpawns.selectedAnimal < LevelAnimals.tables.Count && (int)EditorSpawnsAnimalsUI.selectedTier < LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].tiers.Count && (int)EditorSpawnsAnimalsUI.selectAnimal < LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].tiers[(int)EditorSpawnsAnimalsUI.selectedTier].table.Count)
			{
				LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].removeAnimal(EditorSpawnsAnimalsUI.selectedTier, EditorSpawnsAnimalsUI.selectAnimal);
				EditorSpawnsAnimalsUI.updateSelection();
				EditorSpawnsAnimalsUI.spawnsScrollBox.state = new Vector2(0f, float.MaxValue);
			}
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x00161D3E File Offset: 0x0016013E
		private static void onDraggedRadiusSlider(SleekSlider slider, float state)
		{
			EditorSpawns.radius = (byte)((float)EditorSpawns.MIN_REMOVE_SIZE + state * (float)EditorSpawns.MAX_REMOVE_SIZE);
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x00161D55 File Offset: 0x00160155
		private static void onClickedAddButton(SleekButton button)
		{
			EditorSpawns.spawnMode = ESpawnMode.ADD_ANIMAL;
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x00161D5E File Offset: 0x0016015E
		private static void onClickedRemoveButton(SleekButton button)
		{
			EditorSpawns.spawnMode = ESpawnMode.REMOVE_ANIMAL;
		}

		// Token: 0x0400249E RID: 9374
		private static Sleek container;

		// Token: 0x0400249F RID: 9375
		public static bool active;

		// Token: 0x040024A0 RID: 9376
		private static SleekScrollBox tableScrollBox;

		// Token: 0x040024A1 RID: 9377
		private static SleekScrollBox spawnsScrollBox;

		// Token: 0x040024A2 RID: 9378
		private static SleekButton[] tableButtons;

		// Token: 0x040024A3 RID: 9379
		private static SleekButton[] tierButtons;

		// Token: 0x040024A4 RID: 9380
		private static SleekButton[] animalButtons;

		// Token: 0x040024A5 RID: 9381
		private static SleekColorPicker tableColorPicker;

		// Token: 0x040024A6 RID: 9382
		private static SleekUInt16Field tableIDField;

		// Token: 0x040024A7 RID: 9383
		private static SleekField tierNameField;

		// Token: 0x040024A8 RID: 9384
		private static SleekButtonIcon addTierButton;

		// Token: 0x040024A9 RID: 9385
		private static SleekButtonIcon removeTierButton;

		// Token: 0x040024AA RID: 9386
		private static SleekUInt16Field animalIDField;

		// Token: 0x040024AB RID: 9387
		private static SleekButtonIcon addAnimalButton;

		// Token: 0x040024AC RID: 9388
		private static SleekButtonIcon removeAnimalButton;

		// Token: 0x040024AD RID: 9389
		private static SleekBox selectedBox;

		// Token: 0x040024AE RID: 9390
		private static SleekField tableNameField;

		// Token: 0x040024AF RID: 9391
		private static SleekButtonIcon addTableButton;

		// Token: 0x040024B0 RID: 9392
		private static SleekButtonIcon removeTableButton;

		// Token: 0x040024B1 RID: 9393
		private static SleekSlider radiusSlider;

		// Token: 0x040024B2 RID: 9394
		private static SleekButtonIcon addButton;

		// Token: 0x040024B3 RID: 9395
		private static SleekButtonIcon removeButton;

		// Token: 0x040024B4 RID: 9396
		private static byte selectedTier;

		// Token: 0x040024B5 RID: 9397
		private static byte selectAnimal;

		// Token: 0x040024B6 RID: 9398
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x040024B7 RID: 9399
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x040024B8 RID: 9400
		[CompilerGenerated]
		private static Dragged <>f__mg$cache2;

		// Token: 0x040024B9 RID: 9401
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x040024BA RID: 9402
		[CompilerGenerated]
		private static Typed <>f__mg$cache4;

		// Token: 0x040024BB RID: 9403
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;

		// Token: 0x040024BC RID: 9404
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache6;

		// Token: 0x040024BD RID: 9405
		[CompilerGenerated]
		private static ColorPicked <>f__mg$cache7;

		// Token: 0x040024BE RID: 9406
		[CompilerGenerated]
		private static TypedUInt16 <>f__mg$cache8;

		// Token: 0x040024BF RID: 9407
		[CompilerGenerated]
		private static Typed <>f__mg$cache9;

		// Token: 0x040024C0 RID: 9408
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheA;

		// Token: 0x040024C1 RID: 9409
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheB;

		// Token: 0x040024C2 RID: 9410
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheC;

		// Token: 0x040024C3 RID: 9411
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheD;

		// Token: 0x040024C4 RID: 9412
		[CompilerGenerated]
		private static Dragged <>f__mg$cacheE;

		// Token: 0x040024C5 RID: 9413
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheF;

		// Token: 0x040024C6 RID: 9414
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache10;
	}
}
