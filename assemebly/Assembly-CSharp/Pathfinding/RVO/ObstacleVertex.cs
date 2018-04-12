using System;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x0200003A RID: 58
	public class ObstacleVertex
	{
		// Token: 0x04000207 RID: 519
		public bool ignore;

		// Token: 0x04000208 RID: 520
		public Vector3 position;

		// Token: 0x04000209 RID: 521
		public Vector2 dir;

		// Token: 0x0400020A RID: 522
		public float height;

		// Token: 0x0400020B RID: 523
		public RVOLayer layer;

		// Token: 0x0400020C RID: 524
		public bool convex;

		// Token: 0x0400020D RID: 525
		public bool split;

		// Token: 0x0400020E RID: 526
		public ObstacleVertex next;

		// Token: 0x0400020F RID: 527
		public ObstacleVertex prev;
	}
}
