using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004E1 RID: 1249
	public class InteractableOxygenator : InteractablePower
	{
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x000B7ED3 File Offset: 0x000B62D3
		public bool isPowered
		{
			get
			{
				return this._isPowered;
			}
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x000B7EDB File Offset: 0x000B62DB
		protected override void updateWired()
		{
			if (this.engine != null)
			{
				this.engine.gameObject.SetActive(base.isWired && this.isPowered);
			}
			this.updateBubble();
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x000B7F18 File Offset: 0x000B6318
		public void updatePowered(bool newPowered)
		{
			this._isPowered = newPowered;
			if (this.engine != null)
			{
				this.engine.gameObject.SetActive(base.isWired && this.isPowered);
			}
			this.updateBubble();
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x000B7F67 File Offset: 0x000B6367
		private void updateBubble()
		{
			if (base.isWired && this.isPowered)
			{
				this.registerBubble();
			}
			else
			{
				this.deregisterBubble();
			}
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x000B7F90 File Offset: 0x000B6390
		public override void updateState(Asset asset, byte[] state)
		{
			base.updateState(asset, state);
			this._isPowered = (state[0] == 1);
			if (!Dedicator.isDedicated)
			{
				this.engine = base.transform.FindChild("Engine");
			}
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x000B7FC6 File Offset: 0x000B63C6
		public override void use()
		{
			BarricadeManager.toggleOxygenator(base.transform);
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x000B7FD3 File Offset: 0x000B63D3
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			if (this.isPowered)
			{
				message = EPlayerMessage.SPOT_OFF;
			}
			else
			{
				message = EPlayerMessage.SPOT_ON;
			}
			text = string.Empty;
			color = Color.white;
			return true;
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x000B8000 File Offset: 0x000B6400
		private void registerBubble()
		{
			if (this.bubble != null)
			{
				return;
			}
			this.bubble = OxygenManager.registerBubble(base.transform, 24f);
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x000B8024 File Offset: 0x000B6424
		private void deregisterBubble()
		{
			OxygenManager.deregisterBubble(this.bubble);
			this.bubble = null;
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x000B8038 File Offset: 0x000B6438
		private void OnDestroy()
		{
			this.deregisterBubble();
		}

		// Token: 0x04001407 RID: 5127
		private bool _isPowered;

		// Token: 0x04001408 RID: 5128
		private Transform engine;

		// Token: 0x04001409 RID: 5129
		private OxygenBubble bubble;
	}
}
