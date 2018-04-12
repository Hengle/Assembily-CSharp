using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004CE RID: 1230
	public class InteractableFire : Interactable
	{
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060020EB RID: 8427 RVA: 0x000B4206 File Offset: 0x000B2606
		public bool isLit
		{
			get
			{
				return this._isLit;
			}
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000B4210 File Offset: 0x000B2610
		private void updateFire()
		{
			if (this.material != null)
			{
				this.material.SetColor("_EmissionColor", (!this.isLit) ? Color.black : Color.white);
			}
			if (this.fire != null)
			{
				this.fire.gameObject.SetActive(this.isLit);
			}
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000B427F File Offset: 0x000B267F
		public void updateLit(bool newLit)
		{
			this._isLit = newLit;
			this.updateFire();
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x000B4290 File Offset: 0x000B2690
		public override void updateState(Asset asset, byte[] state)
		{
			this._isLit = (state[0] == 1);
			this.material = HighlighterTool.getMaterialInstance(base.transform);
			this.fire = base.transform.FindChild("Fire");
			LightLODTool.applyLightLOD(this.fire);
			this.updateFire();
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x000B42E1 File Offset: 0x000B26E1
		public override void use()
		{
			BarricadeManager.toggleFire(base.transform);
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x000B42EE File Offset: 0x000B26EE
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

		// Token: 0x060020F1 RID: 8433 RVA: 0x000B431B File Offset: 0x000B271B
		private void OnDestroy()
		{
			if (this.material != null)
			{
				UnityEngine.Object.DestroyImmediate(this.material);
			}
		}

		// Token: 0x04001399 RID: 5017
		private bool _isLit;

		// Token: 0x0400139A RID: 5018
		private Material material;

		// Token: 0x0400139B RID: 5019
		private Transform fire;
	}
}
