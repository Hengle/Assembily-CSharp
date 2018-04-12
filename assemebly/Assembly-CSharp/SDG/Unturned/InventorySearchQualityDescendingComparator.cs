using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x020005F2 RID: 1522
	public class InventorySearchQualityDescendingComparator : IComparer<InventorySearch>
	{
		// Token: 0x06002A61 RID: 10849 RVA: 0x00107F9D File Offset: 0x0010639D
		public int Compare(InventorySearch a, InventorySearch b)
		{
			return (int)(b.jar.item.quality - a.jar.item.quality);
		}
	}
}
