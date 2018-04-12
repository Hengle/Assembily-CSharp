﻿using System;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Water
{
	// Token: 0x02000314 RID: 788
	public class WaterUtility
	{
		// Token: 0x06001669 RID: 5737 RVA: 0x00084D6C File Offset: 0x0008316C
		public static bool isPointInsideVolume(Vector3 point)
		{
			WaterVolume waterVolume;
			return WaterUtility.isPointInsideVolume(point, out waterVolume);
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00084D84 File Offset: 0x00083184
		public static bool isPointInsideVolume(Vector3 point, out WaterVolume volume)
		{
			for (int i = 0; i < WaterSystem.volumes.Count; i++)
			{
				volume = WaterSystem.volumes[i];
				if (WaterUtility.isPointInsideVolume(volume, point))
				{
					return true;
				}
			}
			volume = null;
			return false;
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00084DCC File Offset: 0x000831CC
		public static float getWaterSurfaceElevation(WaterVolume volume, Vector3 point)
		{
			point.y += 1024f;
			Ray ray = new Ray(point, new Vector3(0f, -1f, 0f));
			RaycastHit raycastHit;
			if (volume.box.Raycast(ray, out raycastHit, 2048f))
			{
				return raycastHit.point.y;
			}
			return 0f;
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00084E38 File Offset: 0x00083238
		public static bool isPointInsideVolume(WaterVolume volume, Vector3 point)
		{
			Vector3 vector = volume.transform.InverseTransformPoint(point);
			return Mathf.Abs(vector.x) < 0.5f && Mathf.Abs(vector.y) < 0.5f && Mathf.Abs(vector.z) < 0.5f;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00084E97 File Offset: 0x00083297
		public static bool isPointUnderwater(Vector3 point)
		{
			return (Level.info != null && Level.info.configData.Use_Legacy_Water && LevelLighting.isPositionUnderwater(point)) || WaterUtility.isPointInsideVolume(point);
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00084ECA File Offset: 0x000832CA
		public static bool isPointUnderwater(Vector3 point, out WaterVolume volume)
		{
			if (Level.info != null && Level.info.configData.Use_Legacy_Water && LevelLighting.isPositionUnderwater(point))
			{
				volume = null;
				return true;
			}
			return WaterUtility.isPointInsideVolume(point, out volume);
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00084F04 File Offset: 0x00083304
		public static float getWaterSurfaceElevation(Vector3 point)
		{
			bool flag = false;
			float num = -1024f;
			foreach (WaterVolume waterVolume in WaterSystem.volumes)
			{
				if (WaterUtility.isPointInsideVolume(waterVolume, point))
				{
					return WaterUtility.getWaterSurfaceElevation(waterVolume, point);
				}
				Ray ray = new Ray(point, new Vector3(0f, -1f, 0f));
				RaycastHit raycastHit;
				if (waterVolume.box.Raycast(ray, out raycastHit, 2048f) && raycastHit.point.y > num)
				{
					num = raycastHit.point.y;
					flag = true;
				}
			}
			if (flag)
			{
				return num;
			}
			if (Level.info != null && Level.info.configData.Use_Legacy_Water)
			{
				return LevelLighting.getWaterSurfaceElevation();
			}
			return -1024f;
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0008500C File Offset: 0x0008340C
		public static void getUnderwaterInfo(Vector3 point, out bool isUnderwater, out float surfaceElevation)
		{
			if (Level.info != null && Level.info.configData.Use_Legacy_Water)
			{
				isUnderwater = LevelLighting.isPositionUnderwater(point);
				surfaceElevation = LevelLighting.getWaterSurfaceElevation();
			}
			else
			{
				isUnderwater = false;
				surfaceElevation = -1024f;
			}
			WaterVolume volume;
			if (!isUnderwater && WaterUtility.isPointInsideVolume(point, out volume))
			{
				isUnderwater = true;
				surfaceElevation = WaterUtility.getWaterSurfaceElevation(volume, point);
			}
		}
	}
}
