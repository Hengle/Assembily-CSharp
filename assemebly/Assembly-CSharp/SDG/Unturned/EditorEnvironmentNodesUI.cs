using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000751 RID: 1873
	public class EditorEnvironmentNodesUI
	{
		// Token: 0x060034BF RID: 13503 RVA: 0x0015B4FC File Offset: 0x001598FC
		public EditorEnvironmentNodesUI()
		{
			Local local = Localization.read("/Editor/EditorEnvironmentNodes.dat");
			EditorEnvironmentNodesUI.container = new Sleek();
			EditorEnvironmentNodesUI.container.positionOffset_X = 10;
			EditorEnvironmentNodesUI.container.positionOffset_Y = 10;
			EditorEnvironmentNodesUI.container.positionScale_X = 1f;
			EditorEnvironmentNodesUI.container.sizeOffset_X = -20;
			EditorEnvironmentNodesUI.container.sizeOffset_Y = -20;
			EditorEnvironmentNodesUI.container.sizeScale_X = 1f;
			EditorEnvironmentNodesUI.container.sizeScale_Y = 1f;
			EditorUI.window.add(EditorEnvironmentNodesUI.container);
			EditorEnvironmentNodesUI.active = false;
			EditorEnvironmentNodesUI.nameField = new SleekField();
			EditorEnvironmentNodesUI.nameField.positionOffset_X = -200;
			EditorEnvironmentNodesUI.nameField.positionOffset_Y = 80;
			EditorEnvironmentNodesUI.nameField.positionScale_X = 1f;
			EditorEnvironmentNodesUI.nameField.sizeOffset_X = 200;
			EditorEnvironmentNodesUI.nameField.sizeOffset_Y = 30;
			EditorEnvironmentNodesUI.nameField.addLabel(local.format("Name_Label"), ESleekSide.LEFT);
			SleekField sleekField = EditorEnvironmentNodesUI.nameField;
			if (EditorEnvironmentNodesUI.<>f__mg$cache0 == null)
			{
				EditorEnvironmentNodesUI.<>f__mg$cache0 = new Typed(EditorEnvironmentNodesUI.onTypedNameField);
			}
			sleekField.onTyped = EditorEnvironmentNodesUI.<>f__mg$cache0;
			EditorEnvironmentNodesUI.nameField.maxLength = 32;
			EditorEnvironmentNodesUI.container.add(EditorEnvironmentNodesUI.nameField);
			EditorEnvironmentNodesUI.nameField.isVisible = false;
			EditorEnvironmentNodesUI.radiusSlider = new SleekSlider();
			EditorEnvironmentNodesUI.radiusSlider.positionOffset_X = -200;
			EditorEnvironmentNodesUI.radiusSlider.positionOffset_Y = 80;
			EditorEnvironmentNodesUI.radiusSlider.positionScale_X = 1f;
			EditorEnvironmentNodesUI.radiusSlider.sizeOffset_X = 200;
			EditorEnvironmentNodesUI.radiusSlider.sizeOffset_Y = 20;
			EditorEnvironmentNodesUI.radiusSlider.addLabel(local.format("Radius_Label"), ESleekSide.LEFT);
			EditorEnvironmentNodesUI.radiusSlider.orientation = ESleekOrientation.HORIZONTAL;
			SleekSlider sleekSlider = EditorEnvironmentNodesUI.radiusSlider;
			if (EditorEnvironmentNodesUI.<>f__mg$cache1 == null)
			{
				EditorEnvironmentNodesUI.<>f__mg$cache1 = new Dragged(EditorEnvironmentNodesUI.onDraggedRadiusSlider);
			}
			sleekSlider.onDragged = EditorEnvironmentNodesUI.<>f__mg$cache1;
			EditorEnvironmentNodesUI.container.add(EditorEnvironmentNodesUI.radiusSlider);
			EditorEnvironmentNodesUI.radiusSlider.isVisible = false;
			EditorEnvironmentNodesUI.widthField = new SleekSingleField();
			EditorEnvironmentNodesUI.widthField.positionOffset_X = -200;
			EditorEnvironmentNodesUI.widthField.positionOffset_Y = 110;
			EditorEnvironmentNodesUI.widthField.positionScale_X = 1f;
			EditorEnvironmentNodesUI.widthField.sizeOffset_X = 200;
			EditorEnvironmentNodesUI.widthField.sizeOffset_Y = 30;
			EditorEnvironmentNodesUI.widthField.addLabel(local.format("Width_Label"), ESleekSide.LEFT);
			SleekSingleField sleekSingleField = EditorEnvironmentNodesUI.widthField;
			if (EditorEnvironmentNodesUI.<>f__mg$cache2 == null)
			{
				EditorEnvironmentNodesUI.<>f__mg$cache2 = new TypedSingle(EditorEnvironmentNodesUI.onTypedWidthField);
			}
			sleekSingleField.onTypedSingle = EditorEnvironmentNodesUI.<>f__mg$cache2;
			EditorEnvironmentNodesUI.container.add(EditorEnvironmentNodesUI.widthField);
			EditorEnvironmentNodesUI.widthField.isVisible = false;
			EditorEnvironmentNodesUI.heightField = new SleekSingleField();
			EditorEnvironmentNodesUI.heightField.positionOffset_X = -200;
			EditorEnvironmentNodesUI.heightField.positionOffset_Y = 150;
			EditorEnvironmentNodesUI.heightField.positionScale_X = 1f;
			EditorEnvironmentNodesUI.heightField.sizeOffset_X = 200;
			EditorEnvironmentNodesUI.heightField.sizeOffset_Y = 30;
			EditorEnvironmentNodesUI.heightField.addLabel(local.format("Height_Label"), ESleekSide.LEFT);
			SleekSingleField sleekSingleField2 = EditorEnvironmentNodesUI.heightField;
			if (EditorEnvironmentNodesUI.<>f__mg$cache3 == null)
			{
				EditorEnvironmentNodesUI.<>f__mg$cache3 = new TypedSingle(EditorEnvironmentNodesUI.onTypedHeightField);
			}
			sleekSingleField2.onTypedSingle = EditorEnvironmentNodesUI.<>f__mg$cache3;
			EditorEnvironmentNodesUI.container.add(EditorEnvironmentNodesUI.heightField);
			EditorEnvironmentNodesUI.heightField.isVisible = false;
			EditorEnvironmentNodesUI.lengthField = new SleekSingleField();
			EditorEnvironmentNodesUI.lengthField.positionOffset_X = -200;
			EditorEnvironmentNodesUI.lengthField.positionOffset_Y = 190;
			EditorEnvironmentNodesUI.lengthField.positionScale_X = 1f;
			EditorEnvironmentNodesUI.lengthField.sizeOffset_X = 200;
			EditorEnvironmentNodesUI.lengthField.sizeOffset_Y = 30;
			EditorEnvironmentNodesUI.lengthField.addLabel(local.format("Length_Label"), ESleekSide.LEFT);
			SleekSingleField sleekSingleField3 = EditorEnvironmentNodesUI.lengthField;
			if (EditorEnvironmentNodesUI.<>f__mg$cache4 == null)
			{
				EditorEnvironmentNodesUI.<>f__mg$cache4 = new TypedSingle(EditorEnvironmentNodesUI.onTypedLengthField);
			}
			sleekSingleField3.onTypedSingle = EditorEnvironmentNodesUI.<>f__mg$cache4;
			EditorEnvironmentNodesUI.container.add(EditorEnvironmentNodesUI.lengthField);
			EditorEnvironmentNodesUI.lengthField.isVisible = false;
			EditorEnvironmentNodesUI.shapeButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(local.format("Sphere")),
				new GUIContent(local.format("Box"))
			});
			EditorEnvironmentNodesUI.shapeButton.positionOffset_X = -200;
			EditorEnvironmentNodesUI.shapeButton.positionOffset_Y = 230;
			EditorEnvironmentNodesUI.shapeButton.positionScale_X = 1f;
			EditorEnvironmentNodesUI.shapeButton.sizeOffset_X = 200;
			EditorEnvironmentNodesUI.shapeButton.sizeOffset_Y = 30;
			EditorEnvironmentNodesUI.shapeButton.tooltip = local.format("Shape_Tooltip");
			SleekButtonState sleekButtonState = EditorEnvironmentNodesUI.shapeButton;
			if (EditorEnvironmentNodesUI.<>f__mg$cache5 == null)
			{
				EditorEnvironmentNodesUI.<>f__mg$cache5 = new SwappedState(EditorEnvironmentNodesUI.onSwappedShape);
			}
			sleekButtonState.onSwappedState = EditorEnvironmentNodesUI.<>f__mg$cache5;
			EditorEnvironmentNodesUI.container.add(EditorEnvironmentNodesUI.shapeButton);
			EditorEnvironmentNodesUI.shapeButton.isVisible = false;
			EditorEnvironmentNodesUI.itemIDField = new SleekUInt16Field();
			EditorEnvironmentNodesUI.itemIDField.positionOffset_X = -200;
			EditorEnvironmentNodesUI.itemIDField.positionOffset_Y = 110;
			EditorEnvironmentNodesUI.itemIDField.positionScale_X = 1f;
			EditorEnvironmentNodesUI.itemIDField.sizeOffset_X = 200;
			EditorEnvironmentNodesUI.itemIDField.sizeOffset_Y = 30;
			EditorEnvironmentNodesUI.itemIDField.addLabel(local.format("Item_ID_Label"), ESleekSide.LEFT);
			SleekUInt16Field sleekUInt16Field = EditorEnvironmentNodesUI.itemIDField;
			if (EditorEnvironmentNodesUI.<>f__mg$cache6 == null)
			{
				EditorEnvironmentNodesUI.<>f__mg$cache6 = new TypedUInt16(EditorEnvironmentNodesUI.onTypedItemIDField);
			}
			sleekUInt16Field.onTypedUInt16 = EditorEnvironmentNodesUI.<>f__mg$cache6;
			EditorEnvironmentNodesUI.container.add(EditorEnvironmentNodesUI.itemIDField);
			EditorEnvironmentNodesUI.itemIDField.isVisible = false;
			EditorEnvironmentNodesUI.costField = new SleekUInt32Field();
			EditorEnvironmentNodesUI.costField.positionOffset_X = -200;
			EditorEnvironmentNodesUI.costField.positionOffset_Y = 150;
			EditorEnvironmentNodesUI.costField.positionScale_X = 1f;
			EditorEnvironmentNodesUI.costField.sizeOffset_X = 200;
			EditorEnvironmentNodesUI.costField.sizeOffset_Y = 30;
			EditorEnvironmentNodesUI.costField.addLabel(local.format("Cost_Label"), ESleekSide.LEFT);
			SleekUInt32Field sleekUInt32Field = EditorEnvironmentNodesUI.costField;
			if (EditorEnvironmentNodesUI.<>f__mg$cache7 == null)
			{
				EditorEnvironmentNodesUI.<>f__mg$cache7 = new TypedUInt32(EditorEnvironmentNodesUI.onTypedCostField);
			}
			sleekUInt32Field.onTypedUInt32 = EditorEnvironmentNodesUI.<>f__mg$cache7;
			EditorEnvironmentNodesUI.container.add(EditorEnvironmentNodesUI.costField);
			EditorEnvironmentNodesUI.costField.isVisible = false;
			EditorEnvironmentNodesUI.heightToggle = new SleekToggle();
			EditorEnvironmentNodesUI.heightToggle.positionOffset_X = -40;
			EditorEnvironmentNodesUI.heightToggle.positionOffset_Y = 110;
			EditorEnvironmentNodesUI.heightToggle.positionScale_X = 1f;
			EditorEnvironmentNodesUI.heightToggle.sizeOffset_X = 40;
			EditorEnvironmentNodesUI.heightToggle.sizeOffset_Y = 40;
			EditorEnvironmentNodesUI.heightToggle.addLabel(local.format("Height_Label"), ESleekSide.LEFT);
			SleekToggle sleekToggle = EditorEnvironmentNodesUI.heightToggle;
			if (EditorEnvironmentNodesUI.<>f__mg$cache8 == null)
			{
				EditorEnvironmentNodesUI.<>f__mg$cache8 = new Toggled(EditorEnvironmentNodesUI.onToggledHeightToggle);
			}
			sleekToggle.onToggled = EditorEnvironmentNodesUI.<>f__mg$cache8;
			EditorEnvironmentNodesUI.container.add(EditorEnvironmentNodesUI.heightToggle);
			EditorEnvironmentNodesUI.heightToggle.isVisible = false;
			EditorEnvironmentNodesUI.noWeaponsToggle = new SleekToggle();
			EditorEnvironmentNodesUI.noWeaponsToggle.positionOffset_X = -40;
			EditorEnvironmentNodesUI.noWeaponsToggle.positionOffset_Y = 160;
			EditorEnvironmentNodesUI.noWeaponsToggle.positionScale_X = 1f;
			EditorEnvironmentNodesUI.noWeaponsToggle.sizeOffset_X = 40;
			EditorEnvironmentNodesUI.noWeaponsToggle.sizeOffset_Y = 40;
			EditorEnvironmentNodesUI.noWeaponsToggle.addLabel(local.format("No_Weapons_Label"), ESleekSide.LEFT);
			SleekToggle sleekToggle2 = EditorEnvironmentNodesUI.noWeaponsToggle;
			if (EditorEnvironmentNodesUI.<>f__mg$cache9 == null)
			{
				EditorEnvironmentNodesUI.<>f__mg$cache9 = new Toggled(EditorEnvironmentNodesUI.onToggledNoWeaponsToggle);
			}
			sleekToggle2.onToggled = EditorEnvironmentNodesUI.<>f__mg$cache9;
			EditorEnvironmentNodesUI.container.add(EditorEnvironmentNodesUI.noWeaponsToggle);
			EditorEnvironmentNodesUI.noWeaponsToggle.isVisible = false;
			EditorEnvironmentNodesUI.noBuildablesToggle = new SleekToggle();
			EditorEnvironmentNodesUI.noBuildablesToggle.positionOffset_X = -40;
			EditorEnvironmentNodesUI.noBuildablesToggle.positionOffset_Y = 210;
			EditorEnvironmentNodesUI.noBuildablesToggle.positionScale_X = 1f;
			EditorEnvironmentNodesUI.noBuildablesToggle.sizeOffset_X = 40;
			EditorEnvironmentNodesUI.noBuildablesToggle.sizeOffset_Y = 40;
			EditorEnvironmentNodesUI.noBuildablesToggle.addLabel(local.format("No_Buildables_Label"), ESleekSide.LEFT);
			SleekToggle sleekToggle3 = EditorEnvironmentNodesUI.noBuildablesToggle;
			if (EditorEnvironmentNodesUI.<>f__mg$cacheA == null)
			{
				EditorEnvironmentNodesUI.<>f__mg$cacheA = new Toggled(EditorEnvironmentNodesUI.onToggledNoBuildablesToggle);
			}
			sleekToggle3.onToggled = EditorEnvironmentNodesUI.<>f__mg$cacheA;
			EditorEnvironmentNodesUI.container.add(EditorEnvironmentNodesUI.noBuildablesToggle);
			EditorEnvironmentNodesUI.noBuildablesToggle.isVisible = false;
			EditorEnvironmentNodesUI.spawnIDField = new SleekUInt16Field();
			EditorEnvironmentNodesUI.spawnIDField.positionOffset_X = -200;
			EditorEnvironmentNodesUI.spawnIDField.positionOffset_Y = 80;
			EditorEnvironmentNodesUI.spawnIDField.positionScale_X = 1f;
			EditorEnvironmentNodesUI.spawnIDField.sizeOffset_X = 200;
			EditorEnvironmentNodesUI.spawnIDField.sizeOffset_Y = 30;
			EditorEnvironmentNodesUI.spawnIDField.addLabel(local.format("Spawn_ID_Label"), ESleekSide.LEFT);
			SleekUInt16Field sleekUInt16Field2 = EditorEnvironmentNodesUI.spawnIDField;
			if (EditorEnvironmentNodesUI.<>f__mg$cacheB == null)
			{
				EditorEnvironmentNodesUI.<>f__mg$cacheB = new TypedUInt16(EditorEnvironmentNodesUI.onTypedSpawnIDField);
			}
			sleekUInt16Field2.onTypedUInt16 = EditorEnvironmentNodesUI.<>f__mg$cacheB;
			EditorEnvironmentNodesUI.container.add(EditorEnvironmentNodesUI.spawnIDField);
			EditorEnvironmentNodesUI.spawnIDField.isVisible = false;
			EditorEnvironmentNodesUI.effectIDField = new SleekUInt16Field();
			EditorEnvironmentNodesUI.effectIDField.positionOffset_X = -200;
			EditorEnvironmentNodesUI.effectIDField.positionOffset_Y = 270;
			EditorEnvironmentNodesUI.effectIDField.positionScale_X = 1f;
			EditorEnvironmentNodesUI.effectIDField.sizeOffset_X = 200;
			EditorEnvironmentNodesUI.effectIDField.sizeOffset_Y = 30;
			EditorEnvironmentNodesUI.effectIDField.addLabel(local.format("Effect_ID_Label"), ESleekSide.LEFT);
			SleekUInt16Field sleekUInt16Field3 = EditorEnvironmentNodesUI.effectIDField;
			if (EditorEnvironmentNodesUI.<>f__mg$cacheC == null)
			{
				EditorEnvironmentNodesUI.<>f__mg$cacheC = new TypedUInt16(EditorEnvironmentNodesUI.onTypedEffectIDField);
			}
			sleekUInt16Field3.onTypedUInt16 = EditorEnvironmentNodesUI.<>f__mg$cacheC;
			EditorEnvironmentNodesUI.container.add(EditorEnvironmentNodesUI.effectIDField);
			EditorEnvironmentNodesUI.effectIDField.isVisible = false;
			EditorEnvironmentNodesUI.noWaterToggle = new SleekToggle();
			EditorEnvironmentNodesUI.noWaterToggle.positionOffset_X = -40;
			EditorEnvironmentNodesUI.noWaterToggle.positionOffset_Y = 310;
			EditorEnvironmentNodesUI.noWaterToggle.positionScale_X = 1f;
			EditorEnvironmentNodesUI.noWaterToggle.sizeOffset_X = 40;
			EditorEnvironmentNodesUI.noWaterToggle.sizeOffset_Y = 40;
			EditorEnvironmentNodesUI.noWaterToggle.addLabel(local.format("No_Water_Label"), ESleekSide.LEFT);
			SleekToggle sleekToggle4 = EditorEnvironmentNodesUI.noWaterToggle;
			if (EditorEnvironmentNodesUI.<>f__mg$cacheD == null)
			{
				EditorEnvironmentNodesUI.<>f__mg$cacheD = new Toggled(EditorEnvironmentNodesUI.onToggledNoWaterToggle);
			}
			sleekToggle4.onToggled = EditorEnvironmentNodesUI.<>f__mg$cacheD;
			EditorEnvironmentNodesUI.container.add(EditorEnvironmentNodesUI.noWaterToggle);
			EditorEnvironmentNodesUI.noWaterToggle.isVisible = false;
			EditorEnvironmentNodesUI.noLightingToggle = new SleekToggle();
			EditorEnvironmentNodesUI.noLightingToggle.positionOffset_X = -40;
			EditorEnvironmentNodesUI.noLightingToggle.positionOffset_Y = 360;
			EditorEnvironmentNodesUI.noLightingToggle.positionScale_X = 1f;
			EditorEnvironmentNodesUI.noLightingToggle.sizeOffset_X = 40;
			EditorEnvironmentNodesUI.noLightingToggle.sizeOffset_Y = 40;
			EditorEnvironmentNodesUI.noLightingToggle.addLabel(local.format("No_Lighting_Label"), ESleekSide.LEFT);
			SleekToggle sleekToggle5 = EditorEnvironmentNodesUI.noLightingToggle;
			if (EditorEnvironmentNodesUI.<>f__mg$cacheE == null)
			{
				EditorEnvironmentNodesUI.<>f__mg$cacheE = new Toggled(EditorEnvironmentNodesUI.onToggledNoLightingToggle);
			}
			sleekToggle5.onToggled = EditorEnvironmentNodesUI.<>f__mg$cacheE;
			EditorEnvironmentNodesUI.container.add(EditorEnvironmentNodesUI.noLightingToggle);
			EditorEnvironmentNodesUI.noLightingToggle.isVisible = false;
			EditorEnvironmentNodesUI.typeButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(local.format("Location")),
				new GUIContent(local.format("Safezone")),
				new GUIContent(local.format("Purchase")),
				new GUIContent(local.format("Arena")),
				new GUIContent(local.format("Deadzone")),
				new GUIContent(local.format("Airdrop")),
				new GUIContent(local.format("Effect"))
			});
			EditorEnvironmentNodesUI.typeButton.positionOffset_Y = -30;
			EditorEnvironmentNodesUI.typeButton.positionScale_Y = 1f;
			EditorEnvironmentNodesUI.typeButton.sizeOffset_X = 200;
			EditorEnvironmentNodesUI.typeButton.sizeOffset_Y = 30;
			EditorEnvironmentNodesUI.typeButton.tooltip = local.format("Type_Tooltip");
			SleekButtonState sleekButtonState2 = EditorEnvironmentNodesUI.typeButton;
			if (EditorEnvironmentNodesUI.<>f__mg$cacheF == null)
			{
				EditorEnvironmentNodesUI.<>f__mg$cacheF = new SwappedState(EditorEnvironmentNodesUI.onSwappedType);
			}
			sleekButtonState2.onSwappedState = EditorEnvironmentNodesUI.<>f__mg$cacheF;
			EditorEnvironmentNodesUI.container.add(EditorEnvironmentNodesUI.typeButton);
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x0015C0A5 File Offset: 0x0015A4A5
		public static void open()
		{
			if (EditorEnvironmentNodesUI.active)
			{
				return;
			}
			EditorEnvironmentNodesUI.active = true;
			EditorNodes.isNoding = true;
			EditorUI.message(EEditorMessage.NODES);
			EditorEnvironmentNodesUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x0015C0DE File Offset: 0x0015A4DE
		public static void close()
		{
			if (!EditorEnvironmentNodesUI.active)
			{
				return;
			}
			EditorEnvironmentNodesUI.active = false;
			EditorNodes.isNoding = false;
			EditorEnvironmentNodesUI.container.lerpPositionScale(1f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x0015C114 File Offset: 0x0015A514
		public static void updateSelection(Node node)
		{
			if (node != null)
			{
				if (node.type == ENodeType.LOCATION)
				{
					EditorEnvironmentNodesUI.nameField.text = ((LocationNode)node).name;
				}
				else if (node.type == ENodeType.SAFEZONE)
				{
					EditorEnvironmentNodesUI.radiusSlider.state = ((SafezoneNode)node).radius;
					EditorEnvironmentNodesUI.heightToggle.state = ((SafezoneNode)node).isHeight;
					EditorEnvironmentNodesUI.noWeaponsToggle.state = ((SafezoneNode)node).noWeapons;
					EditorEnvironmentNodesUI.noBuildablesToggle.state = ((SafezoneNode)node).noBuildables;
				}
				else if (node.type == ENodeType.PURCHASE)
				{
					EditorEnvironmentNodesUI.radiusSlider.state = ((PurchaseNode)node).radius;
					EditorEnvironmentNodesUI.itemIDField.state = ((PurchaseNode)node).id;
					EditorEnvironmentNodesUI.costField.state = ((PurchaseNode)node).cost;
				}
				else if (node.type == ENodeType.ARENA)
				{
					EditorEnvironmentNodesUI.radiusSlider.state = ((ArenaNode)node).radius;
				}
				else if (node.type == ENodeType.DEADZONE)
				{
					EditorEnvironmentNodesUI.radiusSlider.state = ((DeadzoneNode)node).radius;
				}
				else if (node.type == ENodeType.AIRDROP)
				{
					EditorEnvironmentNodesUI.spawnIDField.state = ((AirdropNode)node).id;
				}
				else if (node.type == ENodeType.EFFECT)
				{
					EditorEnvironmentNodesUI.shapeButton.state = (int)((EffectNode)node).shape;
					EditorEnvironmentNodesUI.radiusSlider.state = ((EffectNode)node).radius;
					EditorEnvironmentNodesUI.widthField.state = ((EffectNode)node).bounds.x;
					EditorEnvironmentNodesUI.heightField.state = ((EffectNode)node).bounds.y;
					EditorEnvironmentNodesUI.lengthField.state = ((EffectNode)node).bounds.z;
					EditorEnvironmentNodesUI.effectIDField.state = ((EffectNode)node).id;
					EditorEnvironmentNodesUI.noWaterToggle.state = ((EffectNode)node).noWater;
					EditorEnvironmentNodesUI.noLightingToggle.state = ((EffectNode)node).noLighting;
				}
			}
			EditorEnvironmentNodesUI.nameField.isVisible = (node != null && node.type == ENodeType.LOCATION);
			EditorEnvironmentNodesUI.shapeButton.isVisible = (node != null && node.type == ENodeType.EFFECT);
			EditorEnvironmentNodesUI.radiusSlider.isVisible = (node != null && (node.type == ENodeType.SAFEZONE || node.type == ENodeType.PURCHASE || node.type == ENodeType.ARENA || node.type == ENodeType.DEADZONE || node.type == ENodeType.EFFECT));
			EditorEnvironmentNodesUI.widthField.isVisible = (node != null && node.type == ENodeType.EFFECT);
			EditorEnvironmentNodesUI.heightField.isVisible = (node != null && node.type == ENodeType.EFFECT);
			EditorEnvironmentNodesUI.lengthField.isVisible = (node != null && node.type == ENodeType.EFFECT);
			EditorEnvironmentNodesUI.itemIDField.isVisible = (node != null && node.type == ENodeType.PURCHASE);
			EditorEnvironmentNodesUI.costField.isVisible = (node != null && node.type == ENodeType.PURCHASE);
			EditorEnvironmentNodesUI.heightToggle.isVisible = (node != null && node.type == ENodeType.SAFEZONE);
			EditorEnvironmentNodesUI.spawnIDField.isVisible = (node != null && node.type == ENodeType.AIRDROP);
			EditorEnvironmentNodesUI.effectIDField.isVisible = (node != null && node.type == ENodeType.EFFECT);
			EditorEnvironmentNodesUI.noWaterToggle.isVisible = (node != null && node.type == ENodeType.EFFECT);
			EditorEnvironmentNodesUI.noLightingToggle.isVisible = (node != null && node.type == ENodeType.EFFECT);
			EditorEnvironmentNodesUI.noWeaponsToggle.isVisible = (node != null && node.type == ENodeType.SAFEZONE);
			EditorEnvironmentNodesUI.noBuildablesToggle.isVisible = (node != null && node.type == ENodeType.SAFEZONE);
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x0015C516 File Offset: 0x0015A916
		private static void onTypedNameField(SleekField field, string state)
		{
			if (EditorNodes.node != null && EditorNodes.node.type == ENodeType.LOCATION)
			{
				((LocationNode)EditorNodes.node).name = state;
			}
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x0015C544 File Offset: 0x0015A944
		private static void onDraggedRadiusSlider(SleekSlider slider, float state)
		{
			if (EditorNodes.node != null)
			{
				if (EditorNodes.node.type == ENodeType.SAFEZONE)
				{
					((SafezoneNode)EditorNodes.node).radius = state;
				}
				else if (EditorNodes.node.type == ENodeType.PURCHASE)
				{
					((PurchaseNode)EditorNodes.node).radius = state;
				}
				else if (EditorNodes.node.type == ENodeType.ARENA)
				{
					((ArenaNode)EditorNodes.node).radius = state;
				}
				else if (EditorNodes.node.type == ENodeType.DEADZONE)
				{
					((DeadzoneNode)EditorNodes.node).radius = state;
				}
				else if (EditorNodes.node.type == ENodeType.EFFECT)
				{
					((EffectNode)EditorNodes.node).radius = state;
				}
			}
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x0015C610 File Offset: 0x0015AA10
		private static void onTypedWidthField(SleekSingleField field, float state)
		{
			if (EditorNodes.node != null && EditorNodes.node.type == ENodeType.EFFECT)
			{
				Vector3 bounds = ((EffectNode)EditorNodes.node).bounds;
				bounds.x = state;
				((EffectNode)EditorNodes.node).bounds = bounds;
			}
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x0015C660 File Offset: 0x0015AA60
		private static void onTypedHeightField(SleekSingleField field, float state)
		{
			if (EditorNodes.node != null && EditorNodes.node.type == ENodeType.EFFECT)
			{
				Vector3 bounds = ((EffectNode)EditorNodes.node).bounds;
				bounds.y = state;
				((EffectNode)EditorNodes.node).bounds = bounds;
			}
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x0015C6B0 File Offset: 0x0015AAB0
		private static void onTypedLengthField(SleekSingleField field, float state)
		{
			if (EditorNodes.node != null && EditorNodes.node.type == ENodeType.EFFECT)
			{
				Vector3 bounds = ((EffectNode)EditorNodes.node).bounds;
				bounds.z = state;
				((EffectNode)EditorNodes.node).bounds = bounds;
			}
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x0015C6FF File Offset: 0x0015AAFF
		private static void onSwappedShape(SleekButtonState button, int state)
		{
			if (EditorNodes.node != null && EditorNodes.node.type == ENodeType.EFFECT)
			{
				((EffectNode)EditorNodes.node).shape = (ENodeShape)state;
			}
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x0015C72B File Offset: 0x0015AB2B
		private static void onTypedItemIDField(SleekUInt16Field field, ushort state)
		{
			if (EditorNodes.node != null && EditorNodes.node.type == ENodeType.PURCHASE)
			{
				((PurchaseNode)EditorNodes.node).id = state;
			}
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x0015C757 File Offset: 0x0015AB57
		private static void onTypedCostField(SleekUInt32Field field, uint state)
		{
			if (EditorNodes.node != null && EditorNodes.node.type == ENodeType.PURCHASE)
			{
				((PurchaseNode)EditorNodes.node).cost = state;
			}
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x0015C783 File Offset: 0x0015AB83
		private static void onToggledHeightToggle(SleekToggle toggle, bool state)
		{
			if (EditorNodes.node != null && EditorNodes.node.type == ENodeType.SAFEZONE)
			{
				((SafezoneNode)EditorNodes.node).isHeight = state;
			}
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x0015C7AF File Offset: 0x0015ABAF
		private static void onTypedSpawnIDField(SleekUInt16Field field, ushort state)
		{
			if (EditorNodes.node != null && EditorNodes.node.type == ENodeType.AIRDROP)
			{
				((AirdropNode)EditorNodes.node).id = state;
			}
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x0015C7DB File Offset: 0x0015ABDB
		private static void onTypedEffectIDField(SleekUInt16Field field, ushort state)
		{
			if (EditorNodes.node != null && EditorNodes.node.type == ENodeType.EFFECT)
			{
				((EffectNode)EditorNodes.node).id = state;
			}
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x0015C807 File Offset: 0x0015AC07
		private static void onToggledNoWaterToggle(SleekToggle toggle, bool state)
		{
			if (EditorNodes.node != null && EditorNodes.node.type == ENodeType.EFFECT)
			{
				((EffectNode)EditorNodes.node).noWater = state;
			}
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x0015C833 File Offset: 0x0015AC33
		private static void onToggledNoLightingToggle(SleekToggle toggle, bool state)
		{
			if (EditorNodes.node != null && EditorNodes.node.type == ENodeType.EFFECT)
			{
				((EffectNode)EditorNodes.node).noLighting = state;
			}
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x0015C85F File Offset: 0x0015AC5F
		private static void onToggledNoWeaponsToggle(SleekToggle toggle, bool state)
		{
			if (EditorNodes.node != null && EditorNodes.node.type == ENodeType.SAFEZONE)
			{
				((SafezoneNode)EditorNodes.node).noWeapons = state;
			}
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x0015C88B File Offset: 0x0015AC8B
		private static void onToggledNoBuildablesToggle(SleekToggle toggle, bool state)
		{
			if (EditorNodes.node != null && EditorNodes.node.type == ENodeType.SAFEZONE)
			{
				((SafezoneNode)EditorNodes.node).noBuildables = state;
			}
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x0015C8B7 File Offset: 0x0015ACB7
		private static void onSwappedType(SleekButtonState button, int state)
		{
			EditorNodes.nodeType = (ENodeType)state;
		}

		// Token: 0x04002400 RID: 9216
		private static Sleek container;

		// Token: 0x04002401 RID: 9217
		public static bool active;

		// Token: 0x04002402 RID: 9218
		private static SleekField nameField;

		// Token: 0x04002403 RID: 9219
		private static SleekSlider radiusSlider;

		// Token: 0x04002404 RID: 9220
		private static SleekSingleField widthField;

		// Token: 0x04002405 RID: 9221
		private static SleekSingleField heightField;

		// Token: 0x04002406 RID: 9222
		private static SleekSingleField lengthField;

		// Token: 0x04002407 RID: 9223
		private static SleekButtonState shapeButton;

		// Token: 0x04002408 RID: 9224
		private static SleekUInt16Field itemIDField;

		// Token: 0x04002409 RID: 9225
		private static SleekUInt32Field costField;

		// Token: 0x0400240A RID: 9226
		private static SleekToggle heightToggle;

		// Token: 0x0400240B RID: 9227
		private static SleekUInt16Field spawnIDField;

		// Token: 0x0400240C RID: 9228
		private static SleekUInt16Field effectIDField;

		// Token: 0x0400240D RID: 9229
		private static SleekToggle noWaterToggle;

		// Token: 0x0400240E RID: 9230
		private static SleekToggle noLightingToggle;

		// Token: 0x0400240F RID: 9231
		private static SleekToggle noWeaponsToggle;

		// Token: 0x04002410 RID: 9232
		private static SleekToggle noBuildablesToggle;

		// Token: 0x04002411 RID: 9233
		private static SleekButtonState typeButton;

		// Token: 0x04002412 RID: 9234
		[CompilerGenerated]
		private static Typed <>f__mg$cache0;

		// Token: 0x04002413 RID: 9235
		[CompilerGenerated]
		private static Dragged <>f__mg$cache1;

		// Token: 0x04002414 RID: 9236
		[CompilerGenerated]
		private static TypedSingle <>f__mg$cache2;

		// Token: 0x04002415 RID: 9237
		[CompilerGenerated]
		private static TypedSingle <>f__mg$cache3;

		// Token: 0x04002416 RID: 9238
		[CompilerGenerated]
		private static TypedSingle <>f__mg$cache4;

		// Token: 0x04002417 RID: 9239
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache5;

		// Token: 0x04002418 RID: 9240
		[CompilerGenerated]
		private static TypedUInt16 <>f__mg$cache6;

		// Token: 0x04002419 RID: 9241
		[CompilerGenerated]
		private static TypedUInt32 <>f__mg$cache7;

		// Token: 0x0400241A RID: 9242
		[CompilerGenerated]
		private static Toggled <>f__mg$cache8;

		// Token: 0x0400241B RID: 9243
		[CompilerGenerated]
		private static Toggled <>f__mg$cache9;

		// Token: 0x0400241C RID: 9244
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheA;

		// Token: 0x0400241D RID: 9245
		[CompilerGenerated]
		private static TypedUInt16 <>f__mg$cacheB;

		// Token: 0x0400241E RID: 9246
		[CompilerGenerated]
		private static TypedUInt16 <>f__mg$cacheC;

		// Token: 0x0400241F RID: 9247
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheD;

		// Token: 0x04002420 RID: 9248
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheE;

		// Token: 0x04002421 RID: 9249
		[CompilerGenerated]
		private static SwappedState <>f__mg$cacheF;
	}
}
