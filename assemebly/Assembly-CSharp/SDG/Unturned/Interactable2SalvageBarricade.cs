using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004C4 RID: 1220
	public class Interactable2SalvageBarricade : Interactable2
	{
		// Token: 0x06002094 RID: 8340 RVA: 0x000B2F68 File Offset: 0x000B1368
		public override bool checkHint(out EPlayerMessage message, out float data)
		{
			message = EPlayerMessage.SALVAGE;
			if (this.hp != null)
			{
				data = (float)this.hp.hp / 100f;
			}
			else
			{
				data = 0f;
			}
			return base.hasOwnership;
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000B2FB8 File Offset: 0x000B13B8
		public override void use()
		{
			BarricadeManager.salvageBarricade(this.root);
		}

		// Token: 0x0400136B RID: 4971
		public Transform root;

		// Token: 0x0400136C RID: 4972
		public Interactable2HP hp;
	}
}
