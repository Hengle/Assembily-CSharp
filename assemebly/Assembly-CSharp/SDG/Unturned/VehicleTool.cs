using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200074B RID: 1867
	public class VehicleTool : MonoBehaviour
	{
		// Token: 0x06003489 RID: 13449 RVA: 0x00159464 File Offset: 0x00157864
		public static Transform getVehicle(ushort id, ushort skin, ushort mythic, VehicleAsset vehicleAsset, SkinAsset skinAsset)
		{
			if (vehicleAsset != null && vehicleAsset.vehicle != null)
			{
				if (id != vehicleAsset.id)
				{
					Debug.LogError("ID and asset ID are not in sync!");
				}
				Transform transform = UnityEngine.Object.Instantiate<GameObject>(vehicleAsset.vehicle).transform;
				transform.name = id.ToString();
				if (skinAsset != null)
				{
					InteractableVehicle interactableVehicle = transform.gameObject.AddComponent<InteractableVehicle>();
					interactableVehicle.id = id;
					interactableVehicle.skinID = skin;
					interactableVehicle.mythicID = mythic;
					interactableVehicle.fuel = 10000;
					interactableVehicle.isExploded = false;
					interactableVehicle.health = 10000;
					interactableVehicle.batteryCharge = 10000;
					interactableVehicle.safeInit();
					interactableVehicle.updateFires();
					interactableVehicle.updateSkin();
				}
				return transform;
			}
			Transform transform2 = new GameObject().transform;
			transform2.name = id.ToString();
			transform2.tag = "Vehicle";
			transform2.gameObject.layer = LayerMasks.VEHICLE;
			return transform2;
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x00159560 File Offset: 0x00157960
		public static void getIcon(ushort id, ushort skin, VehicleAsset vehicleAsset, SkinAsset skinAsset, int x, int y, VehicleIconReady callback)
		{
			if (vehicleAsset != null && id != vehicleAsset.id)
			{
				Debug.LogError("ID and vehicle asset ID are not in sync!");
			}
			if (skinAsset != null && skin != skinAsset.id)
			{
				Debug.LogError("ID and skin asset ID are not in sync!");
			}
			VehicleIconInfo vehicleIconInfo = new VehicleIconInfo();
			vehicleIconInfo.id = id;
			vehicleIconInfo.skin = skin;
			vehicleIconInfo.vehicleAsset = vehicleAsset;
			vehicleIconInfo.skinAsset = skinAsset;
			vehicleIconInfo.x = x;
			vehicleIconInfo.y = y;
			vehicleIconInfo.callback = callback;
			VehicleTool.icons.Enqueue(vehicleIconInfo);
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x001595EC File Offset: 0x001579EC
		public static bool giveVehicle(Player player, ushort id)
		{
			VehicleAsset vehicleAsset = (VehicleAsset)Assets.find(EAssetType.VEHICLE, id);
			if (vehicleAsset != null)
			{
				Vector3 vector = player.transform.position + player.transform.forward * 6f;
				RaycastHit raycastHit;
				Physics.Raycast(vector + Vector3.up * 16f, Vector3.down, out raycastHit, 32f, RayMasks.BLOCK_VEHICLE);
				if (raycastHit.collider != null)
				{
					vector.y = raycastHit.point.y + 16f;
				}
				VehicleManager.spawnVehicle(id, vector, player.transform.rotation);
				return true;
			}
			return false;
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x001596A4 File Offset: 0x00157AA4
		private void Update()
		{
			if (VehicleTool.icons == null || VehicleTool.icons.Count == 0)
			{
				return;
			}
			VehicleIconInfo vehicleIconInfo = VehicleTool.icons.Dequeue();
			if (vehicleIconInfo == null)
			{
				return;
			}
			if (vehicleIconInfo.vehicleAsset == null)
			{
				return;
			}
			Transform vehicle = VehicleTool.getVehicle(vehicleIconInfo.id, vehicleIconInfo.skin, 0, vehicleIconInfo.vehicleAsset, vehicleIconInfo.skinAsset);
			vehicle.position = new Vector3(-256f, -256f, 0f);
			Transform transform = vehicle.FindChild("Icon2");
			if (transform == null)
			{
				UnityEngine.Object.Destroy(vehicle.gameObject);
				Assets.errors.Add("Failed to find a skin icon hook on " + vehicleIconInfo.id + ".");
				return;
			}
			float size2_z = vehicleIconInfo.vehicleAsset.size2_z;
			Texture2D texture = ItemTool.captureIcon(vehicleIconInfo.id, vehicleIconInfo.skin, vehicle, transform, vehicleIconInfo.x, vehicleIconInfo.y, size2_z);
			if (vehicleIconInfo.callback != null)
			{
				vehicleIconInfo.callback(texture);
			}
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x001597B0 File Offset: 0x00157BB0
		private void Start()
		{
			VehicleTool.icons = new Queue<VehicleIconInfo>();
		}

		// Token: 0x040023BD RID: 9149
		private static Queue<VehicleIconInfo> icons;
	}
}
