using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001D0 RID: 464
	public interface ILandscaleHoleVolumeInteractionHandler
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000DE1 RID: 3553
		bool landscapeHoleAutoIgnoreTerrainCollision { get; }

		// Token: 0x06000DE2 RID: 3554
		void landscapeHoleBeginCollision(LandscapeHoleVolume volume, List<TerrainCollider> terrainColliders);

		// Token: 0x06000DE3 RID: 3555
		void landscapeHoleEndCollision(LandscapeHoleVolume volume, List<TerrainCollider> terrainColliders);
	}
}
