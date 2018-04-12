using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000534 RID: 1332
	public class GroundDetail
	{
		// Token: 0x060023E5 RID: 9189 RVA: 0x000C74A0 File Offset: 0x000C58A0
		public GroundDetail(DetailPrototype newPrototype)
		{
			this._prototype = newPrototype;
			this.density = 0f;
			this.chance = 0f;
			this.isGrass_0 = true;
			this.isGrass_1 = true;
			this.isFlower_0 = false;
			this.isFlower_1 = false;
			this.isRock = false;
			this.isRoad = false;
			this.isSnow = false;
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x000C7501 File Offset: 0x000C5901
		public DetailPrototype prototype
		{
			get
			{
				return this._prototype;
			}
		}

		// Token: 0x040015F2 RID: 5618
		private DetailPrototype _prototype;

		// Token: 0x040015F3 RID: 5619
		public float density;

		// Token: 0x040015F4 RID: 5620
		public float chance;

		// Token: 0x040015F5 RID: 5621
		public bool isGrass_0;

		// Token: 0x040015F6 RID: 5622
		public bool isGrass_1;

		// Token: 0x040015F7 RID: 5623
		public bool isFlower_0;

		// Token: 0x040015F8 RID: 5624
		public bool isFlower_1;

		// Token: 0x040015F9 RID: 5625
		public bool isRock;

		// Token: 0x040015FA RID: 5626
		public bool isRoad;

		// Token: 0x040015FB RID: 5627
		public bool isSnow;
	}
}
