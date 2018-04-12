using System;
using SDG.Framework.Landscapes;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Utilities
{
	// Token: 0x02000305 RID: 773
	public class PhysicsUtility
	{
		// Token: 0x0600161E RID: 5662 RVA: 0x000845FE File Offset: 0x000829FE
		public static bool raycast(Ray ray, out RaycastHit hit, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			if ((layerMask & RayMasks.GROUND) == RayMasks.GROUND)
			{
				LandscapeHoleUtility.raycastIgnoreLandscapeIfNecessary(ray, maxDistance, ref layerMask);
			}
			return Physics.Raycast(ray, out hit, maxDistance, layerMask, queryTriggerInteraction);
		}
	}
}
