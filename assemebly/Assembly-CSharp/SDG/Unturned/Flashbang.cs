using System;
using SDG.Framework.Utilities;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200071E RID: 1822
	public class Flashbang : MonoBehaviour, IExplodableThrowable
	{
		// Token: 0x060033AA RID: 13226 RVA: 0x0014F208 File Offset: 0x0014D608
		public void Explode()
		{
			base.GetComponent<AudioSource>().Play();
			if (MainCamera.instance != null)
			{
				Vector3 a = base.transform.position - MainCamera.instance.transform.position;
				if (a.sqrMagnitude < 1024f)
				{
					float num = Vector3.Dot(a.normalized, MainCamera.instance.transform.forward);
					if (num > -0.25f)
					{
						float magnitude = a.magnitude;
						RaycastHit raycastHit;
						if (magnitude < 0.5f || !PhysicsUtility.raycast(new Ray(MainCamera.instance.transform.position, a / magnitude), out raycastHit, magnitude - 0.5f, RayMasks.DAMAGE_SERVER, QueryTriggerInteraction.Ignore))
						{
							float num2;
							if (num > 0.5f)
							{
								num2 = 1f;
							}
							else
							{
								num2 = (num + 0.25f) / 0.75f;
							}
							float num3;
							if (magnitude > 8f)
							{
								num3 = 1f - (magnitude - 8f) / 24f;
							}
							else
							{
								num3 = 1f;
							}
							PlayerUI.stun(num2 * num3);
						}
					}
				}
			}
			AlertTool.alert(base.transform.position, 32f);
			UnityEngine.Object.Destroy(base.gameObject, 2.5f);
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x0014F350 File Offset: 0x0014D750
		private void Start()
		{
			base.Invoke("Explode", this.fuseLength);
		}

		// Token: 0x0400230D RID: 8973
		public float fuseLength = 2.5f;
	}
}
