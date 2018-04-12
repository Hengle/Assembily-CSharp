using System;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Foliage
{
	// Token: 0x0200019F RID: 415
	public struct FoliageInstanceGroup
	{
		// Token: 0x06000C2B RID: 3115 RVA: 0x0005C539 File Offset: 0x0005A939
		public FoliageInstanceGroup(AssetReference<FoliageInstancedMeshInfoAsset> newAssetReference, Matrix4x4 newMatrix, bool newClearWhenBaked)
		{
			this.assetReference = newAssetReference;
			this.matrix = newMatrix;
			this.clearWhenBaked = newClearWhenBaked;
		}

		// Token: 0x04000889 RID: 2185
		public AssetReference<FoliageInstancedMeshInfoAsset> assetReference;

		// Token: 0x0400088A RID: 2186
		public Matrix4x4 matrix;

		// Token: 0x0400088B RID: 2187
		public bool clearWhenBaked;
	}
}
