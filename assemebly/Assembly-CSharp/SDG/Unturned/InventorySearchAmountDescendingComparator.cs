using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x020005F4 RID: 1524
	public class InventorySearchAmountDescendingComparator : IComparer<InventorySearch>
	{
		// Token: 0x06002A65 RID: 10853 RVA: 0x00107FF3 File Offset: 0x001063F3
		public int Compare(InventorySearch a, InventorySearch b)
		{
			return (int)(b.jar.item.amount - a.jar.item.amount);
		}
	}
}
