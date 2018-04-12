using System;
using UnityEngine;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001E0 RID: 480
	public struct LandscapePreviewSample
	{
		// Token: 0x06000E6E RID: 3694 RVA: 0x00063C3A File Offset: 0x0006203A
		public LandscapePreviewSample(Vector3 newPosition, float newWeight)
		{
			this.position = newPosition;
			this.weight = newWeight;
		}

		// Token: 0x0400092B RID: 2347
		public Vector3 position;

		// Token: 0x0400092C RID: 2348
		public float weight;
	}
}
