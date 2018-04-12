using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000535 RID: 1333
	public class GroundMaterial
	{
		// Token: 0x060023E7 RID: 9191 RVA: 0x000C750C File Offset: 0x000C590C
		public GroundMaterial(SplatPrototype newPrototype)
		{
			this._prototype = newPrototype;
			this.overgrowth = 0f;
			this.chance = 0f;
			this.steepness = 0f;
			this.height = 1f;
			this.transition = 0f;
			this.isGrassy_0 = false;
			this.isGrassy_1 = false;
			this.isFlowery_0 = false;
			this.isFlowery_1 = false;
			this.isRocky = true;
			this.isSnowy = false;
			this.isRoad = false;
			this.isFoundation = false;
			this.isManual = false;
			this.ignoreSteepness = false;
			this.ignoreHeight = false;
			this.ignoreFootprint = false;
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060023E8 RID: 9192 RVA: 0x000C75B1 File Offset: 0x000C59B1
		public SplatPrototype prototype
		{
			get
			{
				return this._prototype;
			}
		}

		// Token: 0x040015FC RID: 5628
		private SplatPrototype _prototype;

		// Token: 0x040015FD RID: 5629
		public float overgrowth;

		// Token: 0x040015FE RID: 5630
		public float chance;

		// Token: 0x040015FF RID: 5631
		public float steepness;

		// Token: 0x04001600 RID: 5632
		public float height;

		// Token: 0x04001601 RID: 5633
		public float transition;

		// Token: 0x04001602 RID: 5634
		public bool isGrassy_0;

		// Token: 0x04001603 RID: 5635
		public bool isGrassy_1;

		// Token: 0x04001604 RID: 5636
		public bool isFlowery_0;

		// Token: 0x04001605 RID: 5637
		public bool isFlowery_1;

		// Token: 0x04001606 RID: 5638
		public bool isRocky;

		// Token: 0x04001607 RID: 5639
		public bool isSnowy;

		// Token: 0x04001608 RID: 5640
		public bool isRoad;

		// Token: 0x04001609 RID: 5641
		public bool isFoundation;

		// Token: 0x0400160A RID: 5642
		public bool isManual;

		// Token: 0x0400160B RID: 5643
		public bool ignoreSteepness;

		// Token: 0x0400160C RID: 5644
		public bool ignoreHeight;

		// Token: 0x0400160D RID: 5645
		public bool ignoreFootprint;
	}
}
