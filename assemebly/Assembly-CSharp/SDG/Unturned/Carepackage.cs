using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200071B RID: 1819
	public class Carepackage : MonoBehaviour
	{
		// Token: 0x060033A2 RID: 13218 RVA: 0x0014EFC8 File Offset: 0x0014D3C8
		private void OnCollisionEnter(Collision collision)
		{
			if (this.isExploded)
			{
				return;
			}
			if (collision.collider.isTrigger)
			{
				return;
			}
			this.isExploded = true;
			if (Provider.isServer)
			{
				Transform transform = BarricadeManager.dropBarricade(new Barricade(1374), null, base.transform.position, 0f, 0f, 0f, 0UL, 0UL);
				if (transform != null)
				{
					InteractableStorage component = transform.GetComponent<InteractableStorage>();
					component.despawnWhenDestroyed = true;
					if (component != null && component.items != null)
					{
						int i = 0;
						while (i < 8)
						{
							ushort num = SpawnTableTool.resolve(this.id);
							if (num == 0)
							{
								break;
							}
							if (!component.items.tryAddItem(new Item(num, EItemOrigin.ADMIN), false))
							{
								i++;
							}
						}
						component.items.onStateUpdated();
					}
				}
				transform.gameObject.AddComponent<CarepackageDestroy>();
				EffectManager.sendEffectReliable(120, EffectManager.INSANE, base.transform.position);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x0400230B RID: 8971
		public ushort id;

		// Token: 0x0400230C RID: 8972
		private bool isExploded;
	}
}
