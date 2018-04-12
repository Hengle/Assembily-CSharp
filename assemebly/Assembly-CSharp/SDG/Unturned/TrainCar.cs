using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004F9 RID: 1273
	public class TrainCar
	{
		// Token: 0x04001472 RID: 5234
		public float trackPositionOffset;

		// Token: 0x04001473 RID: 5235
		public Vector3 currentFrontPosition;

		// Token: 0x04001474 RID: 5236
		public Vector3 currentFrontNormal;

		// Token: 0x04001475 RID: 5237
		public Vector3 currentFrontDirection;

		// Token: 0x04001476 RID: 5238
		public Vector3 currentBackPosition;

		// Token: 0x04001477 RID: 5239
		public Vector3 currentBackNormal;

		// Token: 0x04001478 RID: 5240
		public Vector3 currentBackDirection;

		// Token: 0x04001479 RID: 5241
		public Transform root;

		// Token: 0x0400147A RID: 5242
		public Transform trackFront;

		// Token: 0x0400147B RID: 5243
		public Transform trackBack;
	}
}
