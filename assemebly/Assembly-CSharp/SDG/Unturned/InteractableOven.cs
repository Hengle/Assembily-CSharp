using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004E0 RID: 1248
	public class InteractableOven : InteractablePower
	{
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x000B7DE5 File Offset: 0x000B61E5
		public bool isLit
		{
			get
			{
				return this._isLit;
			}
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x000B7DED File Offset: 0x000B61ED
		protected override void updateWired()
		{
			if (this.fire != null)
			{
				this.fire.gameObject.SetActive(base.isWired && this.isLit);
			}
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x000B7E24 File Offset: 0x000B6224
		public void updateLit(bool newLit)
		{
			this._isLit = newLit;
			if (this.fire != null)
			{
				this.fire.gameObject.SetActive(base.isWired && this.isLit);
			}
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000B7E62 File Offset: 0x000B6262
		public override void updateState(Asset asset, byte[] state)
		{
			this._isLit = (state[0] == 1);
			this.fire = base.transform.FindChild("Fire");
			LightLODTool.applyLightLOD(this.fire);
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x000B7E91 File Offset: 0x000B6291
		public override void use()
		{
			BarricadeManager.toggleOven(base.transform);
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x000B7E9E File Offset: 0x000B629E
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			if (this.isLit)
			{
				message = EPlayerMessage.FIRE_OFF;
			}
			else
			{
				message = EPlayerMessage.FIRE_ON;
			}
			text = string.Empty;
			color = Color.white;
			return true;
		}

		// Token: 0x04001405 RID: 5125
		private bool _isLit;

		// Token: 0x04001406 RID: 5126
		private Transform fire;
	}
}
