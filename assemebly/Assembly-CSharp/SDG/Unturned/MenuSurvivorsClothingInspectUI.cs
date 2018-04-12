using System;
using System.Runtime.CompilerServices;
using SDG.Provider;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000781 RID: 1921
	public class MenuSurvivorsClothingInspectUI
	{
		// Token: 0x0600373C RID: 14140 RVA: 0x00181294 File Offset: 0x0017F694
		public MenuSurvivorsClothingInspectUI()
		{
			MenuSurvivorsClothingInspectUI.container = new Sleek();
			MenuSurvivorsClothingInspectUI.container.positionOffset_X = 10;
			MenuSurvivorsClothingInspectUI.container.positionOffset_Y = 10;
			MenuSurvivorsClothingInspectUI.container.positionScale_Y = 1f;
			MenuSurvivorsClothingInspectUI.container.sizeOffset_X = -20;
			MenuSurvivorsClothingInspectUI.container.sizeOffset_Y = -20;
			MenuSurvivorsClothingInspectUI.container.sizeScale_X = 1f;
			MenuSurvivorsClothingInspectUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuSurvivorsClothingInspectUI.container);
			MenuSurvivorsClothingInspectUI.active = false;
			MenuSurvivorsClothingInspectUI.inventory = new Sleek();
			MenuSurvivorsClothingInspectUI.inventory.positionScale_X = 0.5f;
			MenuSurvivorsClothingInspectUI.inventory.positionOffset_Y = 10;
			MenuSurvivorsClothingInspectUI.inventory.sizeScale_X = 0.5f;
			MenuSurvivorsClothingInspectUI.inventory.sizeScale_Y = 1f;
			MenuSurvivorsClothingInspectUI.inventory.sizeOffset_Y = -20;
			MenuSurvivorsClothingInspectUI.inventory.constraint = ESleekConstraint.XY;
			MenuSurvivorsClothingInspectUI.container.add(MenuSurvivorsClothingInspectUI.inventory);
			MenuSurvivorsClothingInspectUI.image = new SleekInspect("RenderTextures/Item");
			MenuSurvivorsClothingInspectUI.image.positionScale_Y = 0.125f;
			MenuSurvivorsClothingInspectUI.image.sizeScale_X = 1f;
			MenuSurvivorsClothingInspectUI.image.sizeScale_Y = 0.75f;
			MenuSurvivorsClothingInspectUI.image.constraint = ESleekConstraint.XY;
			MenuSurvivorsClothingInspectUI.inventory.add(MenuSurvivorsClothingInspectUI.image);
			MenuSurvivorsClothingInspectUI.slider = new SleekSlider();
			MenuSurvivorsClothingInspectUI.slider.positionOffset_Y = 10;
			MenuSurvivorsClothingInspectUI.slider.positionScale_Y = 1f;
			MenuSurvivorsClothingInspectUI.slider.sizeOffset_Y = 20;
			MenuSurvivorsClothingInspectUI.slider.sizeScale_X = 1f;
			MenuSurvivorsClothingInspectUI.slider.orientation = ESleekOrientation.HORIZONTAL;
			SleekSlider sleekSlider = MenuSurvivorsClothingInspectUI.slider;
			if (MenuSurvivorsClothingInspectUI.<>f__mg$cache2 == null)
			{
				MenuSurvivorsClothingInspectUI.<>f__mg$cache2 = new Dragged(MenuSurvivorsClothingInspectUI.onDraggedSlider);
			}
			sleekSlider.onDragged = MenuSurvivorsClothingInspectUI.<>f__mg$cache2;
			MenuSurvivorsClothingInspectUI.image.add(MenuSurvivorsClothingInspectUI.slider);
			MenuSurvivorsClothingInspectUI.inspect = GameObject.Find("Inspect").transform;
			MenuSurvivorsClothingInspectUI.look = MenuSurvivorsClothingInspectUI.inspect.GetComponent<ItemLook>();
			MenuSurvivorsClothingInspectUI.camera = MenuSurvivorsClothingInspectUI.look.inspectCamera;
			MenuSurvivorsClothingInspectUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuSurvivorsClothingInspectUI.backButton.positionOffset_Y = -50;
			MenuSurvivorsClothingInspectUI.backButton.positionScale_Y = 1f;
			MenuSurvivorsClothingInspectUI.backButton.sizeOffset_X = 200;
			MenuSurvivorsClothingInspectUI.backButton.sizeOffset_Y = 50;
			MenuSurvivorsClothingInspectUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuSurvivorsClothingInspectUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton = MenuSurvivorsClothingInspectUI.backButton;
			if (MenuSurvivorsClothingInspectUI.<>f__mg$cache3 == null)
			{
				MenuSurvivorsClothingInspectUI.<>f__mg$cache3 = new ClickedButton(MenuSurvivorsClothingInspectUI.onClickedBackButton);
			}
			sleekButton.onClickedButton = MenuSurvivorsClothingInspectUI.<>f__mg$cache3;
			MenuSurvivorsClothingInspectUI.backButton.fontSize = 14;
			MenuSurvivorsClothingInspectUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuSurvivorsClothingInspectUI.container.add(MenuSurvivorsClothingInspectUI.backButton);
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x00181570 File Offset: 0x0017F970
		public static void open()
		{
			if (MenuSurvivorsClothingInspectUI.active)
			{
				return;
			}
			MenuSurvivorsClothingInspectUI.active = true;
			MenuSurvivorsClothingInspectUI.camera.gameObject.SetActive(true);
			MenuSurvivorsClothingInspectUI.look._yaw = 0f;
			MenuSurvivorsClothingInspectUI.look.yaw = 0f;
			MenuSurvivorsClothingInspectUI.slider.state = 0f;
			MenuSurvivorsClothingInspectUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x001815E5 File Offset: 0x0017F9E5
		public static void close()
		{
			if (!MenuSurvivorsClothingInspectUI.active)
			{
				return;
			}
			MenuSurvivorsClothingInspectUI.active = false;
			MenuSurvivorsClothingInspectUI.camera.gameObject.SetActive(false);
			MenuSurvivorsClothingInspectUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x00181622 File Offset: 0x0017FA22
		private static bool getInspectedItemStatTrackerValue(out EStatTrackerType type, out int kills)
		{
			return Provider.provider.economyService.getInventoryStatTrackerValue(MenuSurvivorsClothingInspectUI.instance, out type, out kills);
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x0018163C File Offset: 0x0017FA3C
		public static void viewItem(int newItem, ulong newInstance)
		{
			MenuSurvivorsClothingInspectUI.item = newItem;
			MenuSurvivorsClothingInspectUI.instance = newInstance;
			if (MenuSurvivorsClothingInspectUI.model != null)
			{
				UnityEngine.Object.Destroy(MenuSurvivorsClothingInspectUI.model.gameObject);
			}
			ushort id;
			ushort id2;
			Provider.provider.economyService.getInventoryTargetID(MenuSurvivorsClothingInspectUI.item, out id, out id2);
			ushort inventorySkinID = Provider.provider.economyService.getInventorySkinID(MenuSurvivorsClothingInspectUI.item);
			ushort inventoryMythicID = Provider.provider.economyService.getInventoryMythicID(MenuSurvivorsClothingInspectUI.item);
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, id);
			VehicleAsset vehicleAsset = (VehicleAsset)Assets.find(EAssetType.VEHICLE, id2);
			if (itemAsset == null && vehicleAsset == null)
			{
				return;
			}
			if (inventorySkinID != 0)
			{
				SkinAsset skinAsset = (SkinAsset)Assets.find(EAssetType.SKIN, inventorySkinID);
				if (vehicleAsset != null)
				{
					MenuSurvivorsClothingInspectUI.model = VehicleTool.getVehicle(vehicleAsset.id, skinAsset.id, inventoryMythicID, vehicleAsset, skinAsset);
				}
				else
				{
					ushort id3 = itemAsset.id;
					ushort skin = inventorySkinID;
					byte quality = 100;
					byte[] state = itemAsset.getState();
					bool viewmodel = false;
					ItemAsset itemAsset2 = itemAsset;
					SkinAsset skinAsset2 = skinAsset;
					if (MenuSurvivorsClothingInspectUI.<>f__mg$cache0 == null)
					{
						MenuSurvivorsClothingInspectUI.<>f__mg$cache0 = new GetStatTrackerValueHandler(MenuSurvivorsClothingInspectUI.getInspectedItemStatTrackerValue);
					}
					MenuSurvivorsClothingInspectUI.model = ItemTool.getItem(id3, skin, quality, state, viewmodel, itemAsset2, skinAsset2, MenuSurvivorsClothingInspectUI.<>f__mg$cache0);
					if (inventoryMythicID != 0)
					{
						ItemTool.applyEffect(MenuSurvivorsClothingInspectUI.model, inventoryMythicID, EEffectType.THIRD);
					}
				}
			}
			else
			{
				ushort id4 = itemAsset.id;
				ushort skin2 = 0;
				byte quality2 = 100;
				byte[] state2 = itemAsset.getState();
				bool viewmodel2 = false;
				ItemAsset itemAsset3 = itemAsset;
				if (MenuSurvivorsClothingInspectUI.<>f__mg$cache1 == null)
				{
					MenuSurvivorsClothingInspectUI.<>f__mg$cache1 = new GetStatTrackerValueHandler(MenuSurvivorsClothingInspectUI.getInspectedItemStatTrackerValue);
				}
				MenuSurvivorsClothingInspectUI.model = ItemTool.getItem(id4, skin2, quality2, state2, viewmodel2, itemAsset3, MenuSurvivorsClothingInspectUI.<>f__mg$cache1);
				if (inventoryMythicID != 0)
				{
					ItemTool.applyEffect(MenuSurvivorsClothingInspectUI.model, inventoryMythicID, EEffectType.HOOK);
				}
			}
			MenuSurvivorsClothingInspectUI.model.parent = MenuSurvivorsClothingInspectUI.inspect;
			MenuSurvivorsClothingInspectUI.model.localPosition = Vector3.zero;
			if (vehicleAsset != null)
			{
				MenuSurvivorsClothingInspectUI.model.localRotation = Quaternion.identity;
			}
			else if (itemAsset != null && itemAsset.type == EItemType.MELEE)
			{
				MenuSurvivorsClothingInspectUI.model.localRotation = Quaternion.Euler(0f, -90f, 90f);
			}
			else
			{
				MenuSurvivorsClothingInspectUI.model.localRotation = Quaternion.Euler(-90f, 0f, 0f);
			}
			Bounds bounds = new Bounds(MenuSurvivorsClothingInspectUI.model.position, Vector3.zero);
			Collider[] components = MenuSurvivorsClothingInspectUI.model.GetComponents<Collider>();
			foreach (Collider collider in components)
			{
				Bounds bounds2 = collider.bounds;
				bounds.Encapsulate(bounds2);
			}
			MenuSurvivorsClothingInspectUI.look.pos = bounds.center;
			MenuSurvivorsClothingInspectUI.look.dist = bounds.extents.magnitude * 2.25f;
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x001818D9 File Offset: 0x0017FCD9
		private static void onDraggedSlider(SleekSlider slider, float state)
		{
			MenuSurvivorsClothingInspectUI.look.yaw = state * 360f;
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x001818EC File Offset: 0x0017FCEC
		private static void onClickedBackButton(SleekButton button)
		{
			MenuSurvivorsClothingItemUI.open();
			MenuSurvivorsClothingInspectUI.close();
		}

		// Token: 0x04002887 RID: 10375
		private static Sleek container;

		// Token: 0x04002888 RID: 10376
		public static bool active;

		// Token: 0x04002889 RID: 10377
		private static SleekButtonIcon backButton;

		// Token: 0x0400288A RID: 10378
		private static Sleek inventory;

		// Token: 0x0400288B RID: 10379
		private static SleekInspect image;

		// Token: 0x0400288C RID: 10380
		private static SleekSlider slider;

		// Token: 0x0400288D RID: 10381
		private static int item;

		// Token: 0x0400288E RID: 10382
		private static ulong instance;

		// Token: 0x0400288F RID: 10383
		private static Transform inspect;

		// Token: 0x04002890 RID: 10384
		private static Transform model;

		// Token: 0x04002891 RID: 10385
		private static ItemLook look;

		// Token: 0x04002892 RID: 10386
		private static Camera camera;

		// Token: 0x04002893 RID: 10387
		[CompilerGenerated]
		private static GetStatTrackerValueHandler <>f__mg$cache0;

		// Token: 0x04002894 RID: 10388
		[CompilerGenerated]
		private static GetStatTrackerValueHandler <>f__mg$cache1;

		// Token: 0x04002895 RID: 10389
		[CompilerGenerated]
		private static Dragged <>f__mg$cache2;

		// Token: 0x04002896 RID: 10390
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;
	}
}
