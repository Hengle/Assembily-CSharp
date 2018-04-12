using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x0200053B RID: 1339
	public class ItemTier
	{
		// Token: 0x06002401 RID: 9217 RVA: 0x000C7FBF File Offset: 0x000C63BF
		public ItemTier(List<ItemSpawn> newTable, string newName, float newChance)
		{
			this._table = newTable;
			this.name = newName;
			this.chance = newChance;
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x000C7FDC File Offset: 0x000C63DC
		public List<ItemSpawn> table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000C7FE4 File Offset: 0x000C63E4
		public void addItem(ushort id)
		{
			if (this.table.Count == 255)
			{
				return;
			}
			byte b = 0;
			while ((int)b < this.table.Count)
			{
				if (this.table[(int)b].item == id)
				{
					return;
				}
				b += 1;
			}
			this.table.Add(new ItemSpawn(id));
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000C804D File Offset: 0x000C644D
		public void removeItem(byte index)
		{
			this.table.RemoveAt((int)index);
		}

		// Token: 0x04001625 RID: 5669
		private List<ItemSpawn> _table;

		// Token: 0x04001626 RID: 5670
		public string name;

		// Token: 0x04001627 RID: 5671
		public float chance;
	}
}
