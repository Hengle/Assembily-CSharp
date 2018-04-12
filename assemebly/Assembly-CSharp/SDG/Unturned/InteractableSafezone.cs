using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004E4 RID: 1252
	public class InteractableSafezone : InteractablePower
	{
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x000B813B File Offset: 0x000B653B
		public bool isPowered
		{
			get
			{
				return this._isPowered;
			}
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000B8143 File Offset: 0x000B6543
		protected override void updateWired()
		{
			if (this.engine != null)
			{
				this.engine.gameObject.SetActive(base.isWired && this.isPowered);
			}
			this.updateBubble();
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x000B8180 File Offset: 0x000B6580
		public void updatePowered(bool newPowered)
		{
			this._isPowered = newPowered;
			if (this.engine != null)
			{
				this.engine.gameObject.SetActive(base.isWired && this.isPowered);
			}
			this.updateBubble();
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x000B81CF File Offset: 0x000B65CF
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

		// Token: 0x060021C7 RID: 8647 RVA: 0x000B81F8 File Offset: 0x000B65F8
		public override void updateState(Asset asset, byte[] state)
		{
			base.updateState(asset, state);
			this._isPowered = (state[0] == 1);
			if (!Dedicator.isDedicated)
			{
				this.engine = base.transform.FindChild("Engine");
			}
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x000B822E File Offset: 0x000B662E
		public override void use()
		{
			BarricadeManager.toggleSafezone(base.transform);
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x000B823B File Offset: 0x000B663B
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

		// Token: 0x060021CA RID: 8650 RVA: 0x000B8268 File Offset: 0x000B6668
		private void registerBubble()
		{
			if (!Provider.isServer)
			{
				return;
			}
			if (this.bubble != null)
			{
				return;
			}
			if (base.isPlant)
			{
				return;
			}
			this.bubble = SafezoneManager.registerBubble(base.transform.position, 24f);
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x000B82A8 File Offset: 0x000B66A8
		private void deregisterBubble()
		{
			if (!Provider.isServer)
			{
				return;
			}
			if (this.bubble == null)
			{
				return;
			}
			SafezoneManager.deregisterBubble(this.bubble);
			this.bubble = null;
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x000B82D3 File Offset: 0x000B66D3
		private void OnDestroy()
		{
			this.deregisterBubble();
		}

		// Token: 0x0400140C RID: 5132
		private bool _isPowered;

		// Token: 0x0400140D RID: 5133
		private Transform engine;

		// Token: 0x0400140E RID: 5134
		private SafezoneBubble bubble;
	}
}
