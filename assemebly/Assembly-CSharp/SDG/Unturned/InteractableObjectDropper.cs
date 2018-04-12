using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004D7 RID: 1239
	public class InteractableObjectDropper : InteractableObject
	{
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x000B5B90 File Offset: 0x000B3F90
		public bool isUsable
		{
			get
			{
				return Time.realtimeSinceStartup - this.lastUsed > base.objectAsset.interactabilityDelay && (base.objectAsset.interactabilityPower == EObjectInteractabilityPower.NONE || base.isWired);
			}
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x000B5BCA File Offset: 0x000B3FCA
		private void initAudioSourceComponent()
		{
			this.audioSourceComponent = base.transform.GetComponent<AudioSource>();
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x000B5BDD File Offset: 0x000B3FDD
		private void updateAudioSourceComponent()
		{
			if (this.audioSourceComponent != null && !Dedicator.isDedicated)
			{
				this.audioSourceComponent.Play();
			}
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x000B5C05 File Offset: 0x000B4005
		private void initDropTransform()
		{
			this.dropTransform = base.transform.FindChild("Drop");
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x000B5C1D File Offset: 0x000B401D
		public override void updateState(Asset asset, byte[] state)
		{
			base.updateState(asset, state);
			this.interactabilityDrops = ((ObjectAsset)asset).interactabilityDrops;
			this.interactabilityRewardID = ((ObjectAsset)asset).interactabilityRewardID;
			this.initAudioSourceComponent();
			this.initDropTransform();
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000B5C58 File Offset: 0x000B4058
		public void drop()
		{
			this.lastUsed = Time.realtimeSinceStartup;
			if (this.dropTransform == null)
			{
				return;
			}
			if (this.interactabilityRewardID != 0)
			{
				ushort num = SpawnTableTool.resolve(this.interactabilityRewardID);
				if (num != 0)
				{
					ItemManager.dropItem(new Item(num, EItemOrigin.NATURE), this.dropTransform.position, false, true, false);
				}
			}
			else
			{
				ushort num2 = this.interactabilityDrops[UnityEngine.Random.Range(0, this.interactabilityDrops.Length)];
				if (num2 != 0)
				{
					ItemManager.dropItem(new Item(num2, EItemOrigin.NATURE), this.dropTransform.position, false, true, false);
				}
			}
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x000B5CF4 File Offset: 0x000B40F4
		public override void use()
		{
			this.updateAudioSourceComponent();
			ObjectManager.useObjectDropper(base.transform);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000B5D07 File Offset: 0x000B4107
		public override bool checkUseable()
		{
			return (base.objectAsset.interactabilityPower == EObjectInteractabilityPower.NONE || base.isWired) && base.objectAsset.areInteractabilityConditionsMet(Player.player);
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x000B5D38 File Offset: 0x000B4138
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			for (int i = 0; i < base.objectAsset.interactabilityConditions.Length; i++)
			{
				INPCCondition inpccondition = base.objectAsset.interactabilityConditions[i];
				if (!inpccondition.isConditionMet(Player.player))
				{
					message = EPlayerMessage.CONDITION;
					text = inpccondition.formatCondition(Player.player);
					color = Color.white;
					return true;
				}
			}
			if (base.objectAsset.interactabilityPower != EObjectInteractabilityPower.NONE && !base.isWired)
			{
				message = EPlayerMessage.POWER;
			}
			else
			{
				switch (base.objectAsset.interactabilityHint)
				{
				case EObjectInteractabilityHint.DOOR:
					message = EPlayerMessage.DOOR_OPEN;
					break;
				case EObjectInteractabilityHint.SWITCH:
					message = EPlayerMessage.SPOT_ON;
					break;
				case EObjectInteractabilityHint.FIRE:
					message = EPlayerMessage.FIRE_ON;
					break;
				case EObjectInteractabilityHint.GENERATOR:
					message = EPlayerMessage.GENERATOR_ON;
					break;
				case EObjectInteractabilityHint.USE:
					message = EPlayerMessage.USE;
					break;
				default:
					message = EPlayerMessage.NONE;
					break;
				}
			}
			text = string.Empty;
			color = Color.white;
			return true;
		}

		// Token: 0x040013D6 RID: 5078
		private float lastUsed = -9999f;

		// Token: 0x040013D7 RID: 5079
		private ushort[] interactabilityDrops;

		// Token: 0x040013D8 RID: 5080
		private ushort interactabilityRewardID;

		// Token: 0x040013D9 RID: 5081
		private AudioSource audioSourceComponent;

		// Token: 0x040013DA RID: 5082
		private Transform dropTransform;
	}
}
