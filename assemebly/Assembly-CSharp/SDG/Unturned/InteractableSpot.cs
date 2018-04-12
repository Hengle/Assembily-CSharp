using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004E7 RID: 1255
	public class InteractableSpot : InteractablePower
	{
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x000BB3D8 File Offset: 0x000B97D8
		public bool isPowered
		{
			get
			{
				return this._isPowered;
			}
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x000BB3E0 File Offset: 0x000B97E0
		private void updateLights()
		{
			bool flag = base.isWired && this.isPowered;
			if (this.material != null)
			{
				this.material.SetColor("_EmissionColor", (!flag) ? Color.black : Color.white);
			}
			if (this.spot != null)
			{
				this.spot.gameObject.SetActive(flag);
			}
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x000BB45A File Offset: 0x000B985A
		protected override void updateWired()
		{
			this.updateLights();
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x000BB462 File Offset: 0x000B9862
		public void updatePowered(bool newPowered)
		{
			this._isPowered = newPowered;
			this.updateLights();
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x000BB474 File Offset: 0x000B9874
		public override void updateState(Asset asset, byte[] state)
		{
			base.updateState(asset, state);
			this._isPowered = (state[0] == 1);
			if (!Dedicator.isDedicated)
			{
				this.material = HighlighterTool.getMaterialInstance(base.transform);
				this.spot = base.transform.FindChild("Spots");
				LightLODTool.applyLightLOD(this.spot);
			}
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x000BB4D1 File Offset: 0x000B98D1
		public override void use()
		{
			BarricadeManager.toggleSpot(base.transform);
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x000BB4DE File Offset: 0x000B98DE
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

		// Token: 0x060021E9 RID: 8681 RVA: 0x000BB50B File Offset: 0x000B990B
		private void OnDestroy()
		{
			if (this.material != null)
			{
				UnityEngine.Object.DestroyImmediate(this.material);
			}
		}

		// Token: 0x0400143A RID: 5178
		private bool _isPowered;

		// Token: 0x0400143B RID: 5179
		private Material material;

		// Token: 0x0400143C RID: 5180
		private Transform spot;
	}
}
