using System;
using SDG.Framework.Debug;

namespace SDG.Unturned
{
	// Token: 0x02000388 RID: 904
	public interface IAssetReference
	{
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001930 RID: 6448
		// (set) Token: 0x06001931 RID: 6449
		[Inspectable("#SDG::Asset.AssetReference.GUID.Name", null)]
		Guid GUID { get; set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001932 RID: 6450
		bool isValid { get; }
	}
}
