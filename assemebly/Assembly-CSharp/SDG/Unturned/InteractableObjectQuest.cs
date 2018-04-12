using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004DA RID: 1242
	public class InteractableObjectQuest : InteractableObject
	{
		// Token: 0x06002174 RID: 8564 RVA: 0x000B6D78 File Offset: 0x000B5178
		public override void use()
		{
			if (base.objectAsset.interactabilityEffect != 0 && Time.realtimeSinceStartup - this.lastEffect > 1f)
			{
				this.lastEffect = Time.realtimeSinceStartup;
				Transform transform = base.transform.FindChild("Effect");
				if (transform != null)
				{
					EffectManager.effect(base.objectAsset.interactabilityEffect, transform.position, transform.forward);
				}
				else
				{
					EffectManager.effect(base.objectAsset.interactabilityEffect, base.transform.position, base.transform.forward);
				}
			}
			ObjectManager.useObjectQuest(base.transform);
			if (!Provider.isServer)
			{
				base.objectAsset.applyInteractabilityConditions(Player.player, false);
				base.objectAsset.grantInteractabilityRewards(Player.player, false);
			}
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x000B6E53 File Offset: 0x000B5253
		public override bool checkUseable()
		{
			return base.objectAsset.areInteractabilityConditionsMet(Player.player);
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x000B6E68 File Offset: 0x000B5268
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			int i = 0;
			while (i < base.objectAsset.interactabilityConditions.Length)
			{
				INPCCondition inpccondition = base.objectAsset.interactabilityConditions[i];
				if (!inpccondition.isConditionMet(Player.player))
				{
					text = inpccondition.formatCondition(Player.player);
					color = Color.white;
					if (string.IsNullOrEmpty(text))
					{
						message = EPlayerMessage.NONE;
						return false;
					}
					message = EPlayerMessage.CONDITION;
					return true;
				}
				else
				{
					i++;
				}
			}
			message = EPlayerMessage.INTERACT;
			text = base.objectAsset.interactabilityText;
			color = Color.white;
			return true;
		}

		// Token: 0x040013ED RID: 5101
		private float lastEffect;
	}
}
