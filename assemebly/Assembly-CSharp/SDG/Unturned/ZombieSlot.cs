using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x0200057B RID: 1403
	public class ZombieSlot
	{
		// Token: 0x060026AD RID: 9901 RVA: 0x000E51AC File Offset: 0x000E35AC
		public ZombieSlot(float newChance, List<ZombieCloth> newTable)
		{
			this._table = newTable;
			this.chance = newChance;
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060026AE RID: 9902 RVA: 0x000E51C2 File Offset: 0x000E35C2
		public List<ZombieCloth> table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x000E51CC File Offset: 0x000E35CC
		public void addCloth(ushort id)
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
			this.table.Add(new ZombieCloth(id));
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000E5235 File Offset: 0x000E3635
		public void removeCloth(byte index)
		{
			this.table.RemoveAt((int)index);
		}

		// Token: 0x04001838 RID: 6200
		private List<ZombieCloth> _table;

		// Token: 0x04001839 RID: 6201
		public float chance;
	}
}
