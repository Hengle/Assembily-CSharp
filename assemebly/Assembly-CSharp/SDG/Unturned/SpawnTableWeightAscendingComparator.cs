using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x02000420 RID: 1056
	public class SpawnTableWeightAscendingComparator : IComparer<SpawnTable>
	{
		// Token: 0x06001CAF RID: 7343 RVA: 0x0009BD36 File Offset: 0x0009A136
		public int Compare(SpawnTable a, SpawnTable b)
		{
			return b.weight - a.weight;
		}
	}
}
