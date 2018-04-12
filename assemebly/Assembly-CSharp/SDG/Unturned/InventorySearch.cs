using System;

namespace SDG.Unturned
{
	// Token: 0x020005F5 RID: 1525
	public class InventorySearch
	{
		// Token: 0x06002A66 RID: 10854 RVA: 0x00108016 File Offset: 0x00106416
		public InventorySearch(byte newPage, ItemJar newJar)
		{
			this._page = newPage;
			this._jar = newJar;
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002A67 RID: 10855 RVA: 0x0010802C File Offset: 0x0010642C
		public byte page
		{
			get
			{
				return this._page;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002A68 RID: 10856 RVA: 0x00108034 File Offset: 0x00106434
		public ItemJar jar
		{
			get
			{
				return this._jar;
			}
		}

		// Token: 0x04001B5B RID: 7003
		private byte _page;

		// Token: 0x04001B5C RID: 7004
		private ItemJar _jar;
	}
}
