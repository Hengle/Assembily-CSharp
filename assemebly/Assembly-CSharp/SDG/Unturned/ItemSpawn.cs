using System;

namespace SDG.Unturned
{
	// Token: 0x02000538 RID: 1336
	public class ItemSpawn
	{
		// Token: 0x060023EF RID: 9199 RVA: 0x000C7803 File Offset: 0x000C5C03
		public ItemSpawn(ushort newItem)
		{
			this._item = newItem;
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x000C7812 File Offset: 0x000C5C12
		public ushort item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x0400161D RID: 5661
		private ushort _item;
	}
}
