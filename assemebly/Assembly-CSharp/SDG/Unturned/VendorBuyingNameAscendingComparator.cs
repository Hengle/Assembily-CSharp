using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x0200042B RID: 1067
	public class VendorBuyingNameAscendingComparator : IComparer<VendorBuying>
	{
		// Token: 0x06001D37 RID: 7479 RVA: 0x0009D500 File Offset: 0x0009B900
		public int Compare(VendorBuying a, VendorBuying b)
		{
			ItemAsset itemAsset = Assets.find(EAssetType.ITEM, a.id) as ItemAsset;
			ItemAsset itemAsset2 = Assets.find(EAssetType.ITEM, b.id) as ItemAsset;
			if (itemAsset == null || itemAsset2 == null)
			{
				return 0;
			}
			return itemAsset.itemName.CompareTo(itemAsset2.itemName);
		}
	}
}
