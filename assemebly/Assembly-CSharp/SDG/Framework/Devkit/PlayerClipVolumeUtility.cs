using System;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000159 RID: 345
	public class PlayerClipVolumeUtility
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x000514D8 File Offset: 0x0004F8D8
		public static bool isPointInsideVolume(Vector3 point)
		{
			PlayerClipVolume playerClipVolume;
			return PlayerClipVolumeUtility.isPointInsideVolume(point, out playerClipVolume);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x000514F0 File Offset: 0x0004F8F0
		public static bool isPointInsideVolume(Vector3 point, out PlayerClipVolume volume)
		{
			for (int i = 0; i < PlayerClipVolumeSystem.volumes.Count; i++)
			{
				volume = (PlayerClipVolumeSystem.volumes[i] as PlayerClipVolume);
				if (!(volume == null))
				{
					if (PlayerClipVolumeUtility.isPointInsideVolume(volume, point))
					{
						return true;
					}
				}
			}
			volume = null;
			return false;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00051550 File Offset: 0x0004F950
		public static bool isPointInsideVolume(PlayerClipVolume volume, Vector3 point)
		{
			Vector3 vector = volume.transform.InverseTransformPoint(point);
			return Mathf.Abs(vector.x) < 0.5f && Mathf.Abs(vector.y) < 0.5f && Mathf.Abs(vector.z) < 0.5f;
		}
	}
}
