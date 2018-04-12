using System;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200004F RID: 79
	public class UserConnection
	{
		// Token: 0x0400027D RID: 637
		public Vector3 p1;

		// Token: 0x0400027E RID: 638
		public Vector3 p2;

		// Token: 0x0400027F RID: 639
		public ConnectionType type;

		// Token: 0x04000280 RID: 640
		[JsonName("doOverCost")]
		public bool doOverrideCost;

		// Token: 0x04000281 RID: 641
		[JsonName("overCost")]
		public int overrideCost;

		// Token: 0x04000282 RID: 642
		public bool oneWay;

		// Token: 0x04000283 RID: 643
		public bool enable = true;

		// Token: 0x04000284 RID: 644
		public float width;

		// Token: 0x04000285 RID: 645
		[JsonName("doOverWalkable")]
		public bool doOverrideWalkability = true;

		// Token: 0x04000286 RID: 646
		[JsonName("doOverCost")]
		public bool doOverridePenalty;

		// Token: 0x04000287 RID: 647
		[JsonName("overPenalty")]
		public uint overridePenalty;
	}
}
