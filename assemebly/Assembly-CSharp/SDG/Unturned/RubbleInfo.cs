using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004DD RID: 1245
	public class RubbleInfo
	{
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x000B7169 File Offset: 0x000B5569
		public bool isDead
		{
			get
			{
				return this.health == 0;
			}
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x000B7174 File Offset: 0x000B5574
		public void askDamage(ushort amount)
		{
			if (amount == 0 || this.isDead)
			{
				return;
			}
			if (amount >= this.health)
			{
				this.health = 0;
			}
			else
			{
				this.health -= amount;
			}
		}

		// Token: 0x040013F4 RID: 5108
		public float lastDead;

		// Token: 0x040013F5 RID: 5109
		public ushort health;

		// Token: 0x040013F6 RID: 5110
		public Transform section;

		// Token: 0x040013F7 RID: 5111
		public GameObject aliveGameObject;

		// Token: 0x040013F8 RID: 5112
		public GameObject deadGameObject;

		// Token: 0x040013F9 RID: 5113
		public RubbleRagdollInfo[] ragdolls;

		// Token: 0x040013FA RID: 5114
		public Transform effectTransform;
	}
}
