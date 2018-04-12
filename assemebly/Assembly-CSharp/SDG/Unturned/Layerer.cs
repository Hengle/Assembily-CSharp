using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace SDG.Unturned
{
	// Token: 0x020007C8 RID: 1992
	public class Layerer
	{
		// Token: 0x06003A3B RID: 14907 RVA: 0x001BE148 File Offset: 0x001BC548
		public static void relayer(Transform target, int layer)
		{
			if (target == null)
			{
				return;
			}
			target.gameObject.layer = layer;
			for (int i = 0; i < target.childCount; i++)
			{
				Layerer.relayer(target.GetChild(i), layer);
			}
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x001BE194 File Offset: 0x001BC594
		public static void viewmodel(Transform target)
		{
			if (target.GetComponent<Renderer>() != null)
			{
				target.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.Off;
				target.GetComponent<Renderer>().receiveShadows = false;
				target.tag = "Viewmodel";
				target.gameObject.layer = LayerMasks.VIEWMODEL;
			}
			else if (target.GetComponent<LODGroup>() != null)
			{
				for (int i = 0; i < 4; i++)
				{
					Transform transform = target.FindChild("Model_" + i);
					if (transform == null)
					{
						break;
					}
					if (transform.GetComponent<Renderer>() != null)
					{
						transform.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.Off;
						transform.GetComponent<Renderer>().receiveShadows = false;
					}
					transform.tag = "Viewmodel";
					transform.gameObject.layer = LayerMasks.VIEWMODEL;
				}
			}
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x001BE27C File Offset: 0x001BC67C
		public static void enemy(Transform target)
		{
			if (target.GetComponent<Renderer>() != null)
			{
				target.tag = "Enemy";
				target.gameObject.layer = LayerMasks.ENEMY;
			}
			else if (target.GetComponent<LODGroup>() != null)
			{
				for (int i = 0; i < 4; i++)
				{
					Transform transform = target.FindChild("Model_" + i);
					if (!(transform == null))
					{
						if (transform.GetComponent<Renderer>() != null)
						{
							transform.tag = "Enemy";
							transform.gameObject.layer = LayerMasks.ENEMY;
						}
					}
				}
			}
		}
	}
}
