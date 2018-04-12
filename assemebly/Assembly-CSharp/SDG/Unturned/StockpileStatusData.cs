using System;

namespace SDG.Unturned
{
	// Token: 0x020006B7 RID: 1719
	public class StockpileStatusData
	{
		// Token: 0x060031D8 RID: 12760 RVA: 0x00144247 File Offset: 0x00142647
		public StockpileStatusData()
		{
			this.Has_New_Items = false;
			this.Featured_Item = 0;
		}

		// Token: 0x040021D9 RID: 8665
		public bool Has_New_Items;

		// Token: 0x040021DA RID: 8666
		public int Featured_Item;
	}
}
