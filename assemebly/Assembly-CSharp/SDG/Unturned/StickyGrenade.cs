using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000723 RID: 1827
	public class StickyGrenade : TriggerGrenadeBase
	{
		// Token: 0x060033B7 RID: 13239 RVA: 0x0014F5F4 File Offset: 0x0014D9F4
		protected override void GrenadeTriggered()
		{
			base.GrenadeTriggered();
			Rigidbody component = base.GetComponent<Rigidbody>();
			component.useGravity = false;
			component.isKinematic = true;
		}
	}
}
