using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000739 RID: 1849
	public class LightLODTool
	{
		// Token: 0x06003430 RID: 13360 RVA: 0x001549B0 File Offset: 0x00152DB0
		public static void applyLightLOD(Transform transform)
		{
			if (transform == null)
			{
				return;
			}
			LightLODTool.lightsInChildren.Clear();
			transform.GetComponentsInChildren<Light>(true, LightLODTool.lightsInChildren);
			for (int i = 0; i < LightLODTool.lightsInChildren.Count; i++)
			{
				Light light = LightLODTool.lightsInChildren[i];
				if (light.type != LightType.Area && light.type != LightType.Directional)
				{
					LightLOD lightLOD = light.gameObject.AddComponent<LightLOD>();
					lightLOD.targetLight = light;
				}
			}
		}

		// Token: 0x0400239C RID: 9116
		private static List<Light> lightsInChildren = new List<Light>();
	}
}
