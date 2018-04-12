using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004E3 RID: 1251
	public class InteractableRainBarrel : Interactable
	{
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060021BA RID: 8634 RVA: 0x000B8048 File Offset: 0x000B6448
		public bool isFull
		{
			get
			{
				return this._isFull;
			}
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x000B8050 File Offset: 0x000B6450
		public void updateFull(bool newFull)
		{
			this._isFull = newFull;
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x000B8059 File Offset: 0x000B6459
		public override void updateState(Asset asset, byte[] state)
		{
			this._isFull = (state[0] == 1);
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x000B8067 File Offset: 0x000B6467
		public override bool checkUseable()
		{
			return this.isFull;
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000B806F File Offset: 0x000B646F
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			message = EPlayerMessage.VOLUME_WATER;
			text = string.Empty;
			color = Color.white;
			return true;
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x000B8088 File Offset: 0x000B6488
		private void onRainUpdated(ELightingRain rain)
		{
			if (rain != ELightingRain.POST_DRIZZLE)
			{
				return;
			}
			if (Physics.Raycast(base.transform.position + Vector3.up, Vector3.up, 32f, RayMasks.BLOCK_WIND))
			{
				return;
			}
			this._isFull = true;
			if (Provider.isServer)
			{
				BarricadeManager.updateRainBarrel(base.transform, this.isFull, false);
			}
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x000B80EF File Offset: 0x000B64EF
		private void OnEnable()
		{
			LightingManager.onRainUpdated = (RainUpdated)Delegate.Combine(LightingManager.onRainUpdated, new RainUpdated(this.onRainUpdated));
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x000B8111 File Offset: 0x000B6511
		private void OnDisable()
		{
			LightingManager.onRainUpdated = (RainUpdated)Delegate.Remove(LightingManager.onRainUpdated, new RainUpdated(this.onRainUpdated));
		}

		// Token: 0x0400140B RID: 5131
		private bool _isFull;
	}
}
