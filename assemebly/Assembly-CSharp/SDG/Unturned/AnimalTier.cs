using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x0200051D RID: 1309
	public class AnimalTier
	{
		// Token: 0x06002395 RID: 9109 RVA: 0x000C5CE7 File Offset: 0x000C40E7
		public AnimalTier(List<AnimalSpawn> newTable, string newName, float newChance)
		{
			this._table = newTable;
			this.name = newName;
			this.chance = newChance;
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x000C5D04 File Offset: 0x000C4104
		public List<AnimalSpawn> table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000C5D0C File Offset: 0x000C410C
		public void addAnimal(ushort id)
		{
			if (this.table.Count == 255)
			{
				return;
			}
			byte b = 0;
			while ((int)b < this.table.Count)
			{
				if (this.table[(int)b].animal == id)
				{
					return;
				}
				b += 1;
			}
			this.table.Add(new AnimalSpawn(id));
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000C5D75 File Offset: 0x000C4175
		public void removeAnimal(byte index)
		{
			this.table.RemoveAt((int)index);
		}

		// Token: 0x04001587 RID: 5511
		private List<AnimalSpawn> _table;

		// Token: 0x04001588 RID: 5512
		public string name;

		// Token: 0x04001589 RID: 5513
		public float chance;
	}
}
