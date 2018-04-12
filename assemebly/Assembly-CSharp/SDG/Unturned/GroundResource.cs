using System;

namespace SDG.Unturned
{
	// Token: 0x02000536 RID: 1334
	public class GroundResource
	{
		// Token: 0x060023E9 RID: 9193 RVA: 0x000C75BC File Offset: 0x000C59BC
		public GroundResource(ushort newID)
		{
			this._id = newID;
			this.density = 0f;
			this.chance = 0f;
			this.isTree_0 = true;
			this.isTree_1 = false;
			this.isFlower_0 = false;
			this.isFlower_1 = false;
			this.isRock = false;
			this.isRoad = false;
			this.isSnow = false;
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060023EA RID: 9194 RVA: 0x000C761D File Offset: 0x000C5A1D
		public ushort id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x0400160E RID: 5646
		private ushort _id;

		// Token: 0x0400160F RID: 5647
		public float density;

		// Token: 0x04001610 RID: 5648
		public float chance;

		// Token: 0x04001611 RID: 5649
		public bool isTree_0;

		// Token: 0x04001612 RID: 5650
		public bool isTree_1;

		// Token: 0x04001613 RID: 5651
		public bool isFlower_0;

		// Token: 0x04001614 RID: 5652
		public bool isFlower_1;

		// Token: 0x04001615 RID: 5653
		public bool isRock;

		// Token: 0x04001616 RID: 5654
		public bool isRoad;

		// Token: 0x04001617 RID: 5655
		public bool isSnow;
	}
}
