using System;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000123 RID: 291
	public class AmbianceUtility
	{
		// Token: 0x060008D7 RID: 2263 RVA: 0x0004DD34 File Offset: 0x0004C134
		public static bool isPointInsideVolume(Vector3 point, out AmbianceVolume volume)
		{
			for (int i = 0; i < AmbianceSystem.volumes.Count; i++)
			{
				volume = AmbianceSystem.volumes[i];
				if (AmbianceUtility.isPointInsideVolume(volume, point))
				{
					return true;
				}
			}
			volume = null;
			return false;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0004DD7C File Offset: 0x0004C17C
		public static bool isPointInsideVolume(AmbianceVolume volume, Vector3 point)
		{
			Vector3 vector = volume.transform.InverseTransformPoint(point);
			return Mathf.Abs(vector.x) < 0.5f && Mathf.Abs(vector.y) < 0.5f && Mathf.Abs(vector.z) < 0.5f;
		}
	}
}
