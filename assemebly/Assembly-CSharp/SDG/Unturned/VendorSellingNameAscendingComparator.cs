using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x0200042E RID: 1070
	public class VendorSellingNameAscendingComparator : IComparer<VendorSelling>
	{
		// Token: 0x06001D49 RID: 7497 RVA: 0x0009D928 File Offset: 0x0009BD28
		public int Compare(VendorSelling a, VendorSelling b)
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
