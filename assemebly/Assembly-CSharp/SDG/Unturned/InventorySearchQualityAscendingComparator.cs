using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x020005F1 RID: 1521
	public class InventorySearchQualityAscendingComparator : IComparer<InventorySearch>
	{
		// Token: 0x06002A5F RID: 10847 RVA: 0x00107F72 File Offset: 0x00106372
		public int Compare(InventorySearch a, InventorySearch b)
		{
			return (int)(a.jar.item.quality - b.jar.item.quality);
		}
	}
}
