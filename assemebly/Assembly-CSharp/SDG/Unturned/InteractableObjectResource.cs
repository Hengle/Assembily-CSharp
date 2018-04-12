using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004DB RID: 1243
	public class InteractableObjectResource : InteractableObject
	{
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x000B6F10 File Offset: 0x000B5310
		public ushort amount
		{
			get
			{
				return this._amount;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06002179 RID: 8569 RVA: 0x000B6F18 File Offset: 0x000B5318
		public ushort capacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x000B6F20 File Offset: 0x000B5320
		public bool isRefillable
		{
			get
			{
				return this.amount < this.capacity;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x000B6F30 File Offset: 0x000B5330
		public bool isSiphonable
		{
			get
			{
				return this.amount > 0;
			}
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x000B6F3C File Offset: 0x000B533C
		public bool checkCanReset(float multiplier)
		{
			if (this.amount == this.capacity)
			{
				return false;
			}
			if (base.objectAsset.interactabilityReset < 1f)
			{
				return false;
			}
			if (base.objectAsset.interactability == EObjectInteractability.WATER)
			{
				return Time.realtimeSinceStartup - this.lastUsed > base.objectAsset.interactabilityReset * multiplier;
			}
			return base.objectAsset.interactability == EObjectInteractability.FUEL && Time.realtimeSinceStartup - this.lastUsed > base.objectAsset.interactabilityReset * multiplier;
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x000B6FCE File Offset: 0x000B53CE
		public void updateAmount(ushort newAmount)
		{
			this._amount = newAmount;
			this.lastUsed = Time.realtimeSinceStartup;
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x000B6FE4 File Offset: 0x000B53E4
		public override void updateState(Asset asset, byte[] state)
		{
			base.updateState(asset, state);
			this._amount = BitConverter.ToUInt16(state, 0);
			this._capacity = ((ObjectAsset)asset).interactabilityResource;
			this.lastUsed = Time.realtimeSinceStartup;
			if (base.objectAsset.interactability == EObjectInteractability.WATER)
			{
				if (this.isListeningForRain)
				{
					return;
				}
				this.isListeningForRain = true;
				LightingManager.onRainUpdated = (RainUpdated)Delegate.Combine(LightingManager.onRainUpdated, new RainUpdated(this.onRainUpdated));
			}
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x000B7066 File Offset: 0x000B5466
		public override bool checkUseable()
		{
			return this.amount > 0;
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x000B7074 File Offset: 0x000B5474
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			if (base.objectAsset.interactability == EObjectInteractability.WATER)
			{
				message = EPlayerMessage.VOLUME_WATER;
				text = this.amount + "/" + this.capacity;
			}
			else
			{
				message = EPlayerMessage.VOLUME_FUEL;
				text = string.Empty;
			}
			color = Color.white;
			return true;
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x000B70D4 File Offset: 0x000B54D4
		private void onRainUpdated(ELightingRain rain)
		{
			if (rain != ELightingRain.POST_DRIZZLE)
			{
				return;
			}
			this._amount = this.capacity;
			if (Provider.isServer)
			{
				ObjectManager.updateObjectResource(base.transform, this.amount, false);
			}
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x000B7108 File Offset: 0x000B5508
		private void OnDestroy()
		{
			if (base.objectAsset.interactability == EObjectInteractability.WATER)
			{
				if (!this.isListeningForRain)
				{
					return;
				}
				this.isListeningForRain = false;
				LightingManager.onRainUpdated = (RainUpdated)Delegate.Remove(LightingManager.onRainUpdated, new RainUpdated(this.onRainUpdated));
			}
		}

		// Token: 0x040013EE RID: 5102
		private ushort _amount;

		// Token: 0x040013EF RID: 5103
		private ushort _capacity;

		// Token: 0x040013F0 RID: 5104
		private bool isListeningForRain;

		// Token: 0x040013F1 RID: 5105
		private float lastUsed = -9999f;
	}
}
