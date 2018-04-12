using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005BB RID: 1467
	public class SafezoneBubble
	{
		// Token: 0x06002910 RID: 10512 RVA: 0x000FB155 File Offset: 0x000F9555
		public SafezoneBubble(Vector3 newOrigin, float newSqrRadius)
		{
			this.origin = newOrigin;
			this.sqrRadius = newSqrRadius;
		}

		// Token: 0x0400199B RID: 6555
		public Vector3 origin;

		// Token: 0x0400199C RID: 6556
		public float sqrRadius;
	}
}
