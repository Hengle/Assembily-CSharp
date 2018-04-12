using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000724 RID: 1828
	public class TriggerGrenadeBase : MonoBehaviour
	{
		// Token: 0x060033B9 RID: 13241 RVA: 0x0014F41E File Offset: 0x0014D81E
		protected virtual void GrenadeTriggered()
		{
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x0014F420 File Offset: 0x0014D820
		private void OnTriggerEnter(Collider other)
		{
			if (this.isStuck)
			{
				return;
			}
			if (other.isTrigger)
			{
				return;
			}
			if (other.transform == this.ignoreTransform)
			{
				return;
			}
			this.isStuck = true;
			this.GrenadeTriggered();
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x0014F460 File Offset: 0x0014D860
		private void Awake()
		{
			BoxCollider component = base.GetComponent<BoxCollider>();
			component.isTrigger = true;
			component.size *= 2f;
		}

		// Token: 0x0400232A RID: 9002
		public Transform ignoreTransform;

		// Token: 0x0400232B RID: 9003
		private bool isStuck;
	}
}
