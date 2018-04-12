using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004CF RID: 1231
	public class InteractableForage : Interactable
	{
		// Token: 0x060020F3 RID: 8435 RVA: 0x000B4341 File Offset: 0x000B2741
		public override void use()
		{
			ResourceManager.forage(base.transform.parent);
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x000B4353 File Offset: 0x000B2753
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			message = EPlayerMessage.FORAGE;
			text = string.Empty;
			color = Color.white;
			return true;
		}
	}
}
