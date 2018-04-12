using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005C3 RID: 1475
	public class TemperatureBubble
	{
		// Token: 0x06002951 RID: 10577 RVA: 0x000FD8C6 File Offset: 0x000FBCC6
		public TemperatureBubble(Transform newOrigin, float newSqrRadius, EPlayerTemperature newTemperature)
		{
			this.origin = newOrigin;
			this.sqrRadius = newSqrRadius;
			this.temperature = newTemperature;
		}

		// Token: 0x040019B8 RID: 6584
		public Transform origin;

		// Token: 0x040019B9 RID: 6585
		public float sqrRadius;

		// Token: 0x040019BA RID: 6586
		public EPlayerTemperature temperature;
	}
}
