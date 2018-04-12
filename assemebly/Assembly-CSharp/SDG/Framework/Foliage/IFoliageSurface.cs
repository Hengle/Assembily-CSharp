using System;
using UnityEngine;

namespace SDG.Framework.Foliage
{
	// Token: 0x020001B3 RID: 435
	public interface IFoliageSurface
	{
		// Token: 0x06000D01 RID: 3329
		FoliageBounds getFoliageSurfaceBounds();

		// Token: 0x06000D02 RID: 3330
		bool getFoliageSurfaceInfo(Vector3 position, out Vector3 surfacePosition, out Vector3 surfaceNormal);

		// Token: 0x06000D03 RID: 3331
		void bakeFoliageSurface(FoliageBakeSettings bakeSettings, FoliageTile foliageTile);
	}
}
