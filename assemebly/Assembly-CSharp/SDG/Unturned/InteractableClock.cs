using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004CA RID: 1226
	public class InteractableClock : InteractablePower
	{
		// Token: 0x060020C8 RID: 8392 RVA: 0x000B39EA File Offset: 0x000B1DEA
		public override void updateState(Asset asset, byte[] state)
		{
			this.handHourTransform = base.transform.FindChild("Hand_Hour");
			this.handMinuteTransform = base.transform.FindChild("Hand_Minute");
			base.updateState(asset, state);
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x000B3A20 File Offset: 0x000B1E20
		public override bool checkUseable()
		{
			return base.isWired;
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x000B3A28 File Offset: 0x000B1E28
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			text = string.Empty;
			color = Color.white;
			if (!base.isWired)
			{
				message = EPlayerMessage.POWER;
				return true;
			}
			message = EPlayerMessage.NONE;
			return false;
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000B3A54 File Offset: 0x000B1E54
		private void Update()
		{
			if (!base.isWired)
			{
				return;
			}
			if (this.handHourTransform == null || this.handMinuteTransform == null)
			{
				return;
			}
			float num;
			if (LightingManager.day < LevelLighting.bias)
			{
				num = LightingManager.day / LevelLighting.bias;
			}
			else
			{
				num = (LightingManager.day - LevelLighting.bias) / (1f - LevelLighting.bias);
			}
			float num2 = num - 0.5f;
			float num3 = num * 12f;
			this.handHourTransform.localRotation = Quaternion.Euler(0f, num2 * -360f, 0f);
			this.handMinuteTransform.localRotation = Quaternion.Euler(0f, num3 * -360f, 0f);
		}

		// Token: 0x0400138A RID: 5002
		private Transform handHourTransform;

		// Token: 0x0400138B RID: 5003
		private Transform handMinuteTransform;
	}
}
