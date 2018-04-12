using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005B6 RID: 1462
	public class OxygenBubble
	{
		// Token: 0x060028F0 RID: 10480 RVA: 0x000F9BC0 File Offset: 0x000F7FC0
		public OxygenBubble(Transform newOrigin, float newSqrRadius)
		{
			this.origin = newOrigin;
			this.sqrRadius = newSqrRadius;
		}

		// Token: 0x0400198E RID: 6542
		public Transform origin;

		// Token: 0x0400198F RID: 6543
		public float sqrRadius;
	}
}
