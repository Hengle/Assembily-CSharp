using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004FC RID: 1276
	public class TemperatureTrigger : MonoBehaviour
	{
		// Token: 0x060022E5 RID: 8933 RVA: 0x000C3020 File Offset: 0x000C1420
		private void OnEnable()
		{
			if (this.bubble != null)
			{
				return;
			}
			this.bubble = TemperatureManager.registerBubble(base.transform, base.transform.localScale.x, this.temperature);
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x000C3063 File Offset: 0x000C1463
		private void OnDisable()
		{
			if (this.bubble == null)
			{
				return;
			}
			TemperatureManager.deregisterBubble(this.bubble);
			this.bubble = null;
		}

		// Token: 0x040014F5 RID: 5365
		public EPlayerTemperature temperature;

		// Token: 0x040014F6 RID: 5366
		private TemperatureBubble bubble;
	}
}
