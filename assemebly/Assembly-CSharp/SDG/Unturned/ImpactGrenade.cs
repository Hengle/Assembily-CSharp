using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000721 RID: 1825
	public class ImpactGrenade : TriggerGrenadeBase
	{
		// Token: 0x060033B1 RID: 13233 RVA: 0x0014F499 File Offset: 0x0014D899
		protected override void GrenadeTriggered()
		{
			base.GrenadeTriggered();
			if (this.explodable == null)
			{
				Debug.LogWarning("Missing explodable", this);
				return;
			}
			this.explodable.Explode();
		}

		// Token: 0x0400231A RID: 8986
		public IExplodableThrowable explodable;
	}
}
