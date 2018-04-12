using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003BE RID: 958
	public interface ISkinableAsset
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001A21 RID: 6689
		Texture albedoBase { get; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001A22 RID: 6690
		Texture metallicBase { get; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001A23 RID: 6691
		Texture emissionBase { get; }
	}
}
