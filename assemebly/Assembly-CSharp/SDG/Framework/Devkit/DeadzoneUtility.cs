using System;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000126 RID: 294
	public class DeadzoneUtility
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x0004E3A4 File Offset: 0x0004C7A4
		public static bool isPointInsideVolume(Vector3 point, out DeadzoneVolume volume)
		{
			for (int i = 0; i < DeadzoneSystem.volumes.Count; i++)
			{
				volume = DeadzoneSystem.volumes[i];
				if (DeadzoneUtility.isPointInsideVolume(volume, point))
				{
					return true;
				}
			}
			volume = null;
			return false;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0004E3EC File Offset: 0x0004C7EC
		public static bool isPointInsideVolume(DeadzoneVolume volume, Vector3 point)
		{
			Vector3 vector = volume.transform.InverseTransformPoint(point);
			return Mathf.Abs(vector.x) < 0.5f && Mathf.Abs(vector.y) < 0.5f && Mathf.Abs(vector.z) < 0.5f;
		}
	}
}
