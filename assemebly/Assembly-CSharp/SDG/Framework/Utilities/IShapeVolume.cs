using System;
using UnityEngine;

namespace SDG.Framework.Utilities
{
	// Token: 0x02000301 RID: 769
	public interface IShapeVolume
	{
		// Token: 0x0600160C RID: 5644
		bool containsPoint(Vector3 point);

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600160D RID: 5645
		Bounds worldBounds { get; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600160E RID: 5646
		float internalVolume { get; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600160F RID: 5647
		float surfaceArea { get; }
	}
}
