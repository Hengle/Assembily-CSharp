using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004D9 RID: 1241
	public class InteractableObjectNote : InteractableObject
	{
		// Token: 0x06002170 RID: 8560 RVA: 0x000B6CB0 File Offset: 0x000B50B0
		public override void use()
		{
			PlayerBarricadeSignUI.open(base.objectAsset.interactabilityText);
			PlayerLifeUI.close();
			ObjectManager.useObjectQuest(base.transform);
			if (!Provider.isServer)
			{
				base.objectAsset.applyInteractabilityConditions(Player.player, false);
				base.objectAsset.grantInteractabilityRewards(Player.player, false);
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x000B6D09 File Offset: 0x000B5109
		public override bool checkUseable()
		{
			return !PlayerUI.window.showCursor;
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x000B6D18 File Offset: 0x000B5118
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			EObjectInteractabilityHint interactabilityHint = base.objectAsset.interactabilityHint;
			if (interactabilityHint != EObjectInteractabilityHint.USE)
			{
				message = EPlayerMessage.NONE;
			}
			else
			{
				message = EPlayerMessage.USE;
			}
			text = string.Empty;
			color = Color.white;
			return !PlayerUI.window.showCursor;
		}
	}
}
