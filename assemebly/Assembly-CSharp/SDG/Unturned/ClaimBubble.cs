using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000597 RID: 1431
	public class ClaimBubble
	{
		// Token: 0x060027DD RID: 10205 RVA: 0x000F1D2B File Offset: 0x000F012B
		public ClaimBubble(Vector3 newOrigin, float newSqrRadius, ulong newOwner, ulong newGroup)
		{
			this.origin = newOrigin;
			this.sqrRadius = newSqrRadius;
			this.owner = newOwner;
			this.group = newGroup;
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x000F1D50 File Offset: 0x000F0150
		public bool hasOwnership
		{
			get
			{
				return OwnershipTool.checkToggle(this.owner, this.group);
			}
		}

		// Token: 0x040018F7 RID: 6391
		public Vector3 origin;

		// Token: 0x040018F8 RID: 6392
		public float sqrRadius;

		// Token: 0x040018F9 RID: 6393
		public ulong owner;

		// Token: 0x040018FA RID: 6394
		public ulong group;
	}
}
