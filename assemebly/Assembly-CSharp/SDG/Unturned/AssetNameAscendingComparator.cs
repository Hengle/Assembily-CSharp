using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x02000754 RID: 1876
	public class AssetNameAscendingComparator : IComparer<Asset>
	{
		// Token: 0x060034EB RID: 13547 RVA: 0x0015D96C File Offset: 0x0015BD6C
		public int Compare(Asset a, Asset b)
		{
			return a.name.CompareTo(b.name);
		}
	}
}
