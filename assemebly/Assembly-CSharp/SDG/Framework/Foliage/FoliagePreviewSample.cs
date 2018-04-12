using System;
using UnityEngine;

namespace SDG.Framework.Foliage
{
	// Token: 0x020001A3 RID: 419
	public struct FoliagePreviewSample
	{
		// Token: 0x06000C5A RID: 3162 RVA: 0x0005D01F File Offset: 0x0005B41F
		public FoliagePreviewSample(Vector3 newPosition, Color newColor)
		{
			this.position = newPosition;
			this.color = newColor;
		}

		// Token: 0x0400089C RID: 2204
		public Vector3 position;

		// Token: 0x0400089D RID: 2205
		public Color color;
	}
}
