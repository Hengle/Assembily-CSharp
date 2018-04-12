using System;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001DE RID: 478
	public class LandscapeHoleUtility
	{
		// Token: 0x06000E59 RID: 3673 RVA: 0x0006373C File Offset: 0x00061B3C
		public static bool isPointInsideHoleVolume(Vector3 point)
		{
			LandscapeHoleVolume landscapeHoleVolume;
			return LandscapeHoleUtility.isPointInsideHoleVolume(point, out landscapeHoleVolume);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00063754 File Offset: 0x00061B54
		public static bool isPointInsideHoleVolume(Vector3 point, out LandscapeHoleVolume volume)
		{
			for (int i = 0; i < LandscapeHoleSystem.volumes.Count; i++)
			{
				volume = LandscapeHoleSystem.volumes[i];
				if (LandscapeHoleUtility.isPointInsideHoleVolume(volume, point))
				{
					return true;
				}
			}
			volume = null;
			return false;
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0006379C File Offset: 0x00061B9C
		public static bool isPointInsideHoleVolume(LandscapeHoleVolume volume, Vector3 point)
		{
			Vector3 vector = volume.transform.InverseTransformPoint(point);
			return Mathf.Abs(vector.x) < 0.5f && Mathf.Abs(vector.y) < 0.5f && Mathf.Abs(vector.z) < 0.5f;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x000637FC File Offset: 0x00061BFC
		public static bool doesRayIntersectHoleVolume(Ray ray, out RaycastHit hit, out LandscapeHoleVolume volume, float maxDistance)
		{
			for (int i = 0; i < LandscapeHoleSystem.volumes.Count; i++)
			{
				volume = LandscapeHoleSystem.volumes[i];
				if (LandscapeHoleUtility.doesRayIntersectHoleVolume(volume, ray, out hit, maxDistance))
				{
					return true;
				}
			}
			hit = default(RaycastHit);
			volume = null;
			return false;
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0006384D File Offset: 0x00061C4D
		public static bool doesRayIntersectHoleVolume(LandscapeHoleVolume volume, Ray ray, out RaycastHit hit, float maxDistance)
		{
			return volume.box.Raycast(ray, out hit, maxDistance);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00063860 File Offset: 0x00061C60
		public static bool shouldRaycastIgnoreLandscape(Ray ray, float maxDistance)
		{
			RaycastHit raycastHit;
			LandscapeHoleVolume volume;
			RaycastHit raycastHit2;
			RaycastHit raycastHit3;
			return (LandscapeHoleUtility.doesRayIntersectHoleVolume(ray, out raycastHit, out volume, maxDistance) && Physics.Raycast(ray, out raycastHit2, maxDistance, RayMasks.GROUND) && LandscapeHoleUtility.isPointInsideHoleVolume(volume, raycastHit2.point)) || (LandscapeHoleUtility.isPointInsideHoleVolume(ray.origin, out volume) && Physics.Raycast(ray, out raycastHit3, maxDistance, RayMasks.GROUND) && LandscapeHoleUtility.isPointInsideHoleVolume(volume, raycastHit3.point));
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x000638DF File Offset: 0x00061CDF
		public static void raycastIgnoreLandscapeIfNecessary(Ray ray, float maxDistance, ref int layerMask)
		{
			if (LandscapeHoleUtility.shouldRaycastIgnoreLandscape(ray, maxDistance))
			{
				layerMask &= ~RayMasks.GROUND;
			}
		}
	}
}
