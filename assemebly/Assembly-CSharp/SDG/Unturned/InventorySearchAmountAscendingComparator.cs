using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x020005F3 RID: 1523
	public class InventorySearchAmountAscendingComparator : IComparer<InventorySearch>
	{
		// Token: 0x06002A63 RID: 10851 RVA: 0x00107FC8 File Offset: 0x001063C8
		public int Compare(InventorySearch a, InventorySearch b)
		{
			return (int)(a.jar.item.amount - b.jar.item.amount);
		}
	}
}
