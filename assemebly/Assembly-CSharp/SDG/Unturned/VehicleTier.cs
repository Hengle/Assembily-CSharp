using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x02000576 RID: 1398
	public class VehicleTier
	{
		// Token: 0x06002698 RID: 9880 RVA: 0x000E499F File Offset: 0x000E2D9F
		public VehicleTier(List<VehicleSpawn> newTable, string newName, float newChance)
		{
			this._table = newTable;
			this.name = newName;
			this.chance = newChance;
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002699 RID: 9881 RVA: 0x000E49BC File Offset: 0x000E2DBC
		public List<VehicleSpawn> table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000E49C4 File Offset: 0x000E2DC4
		public void addVehicle(ushort id)
		{
			if (this.table.Count == 255)
			{
				return;
			}
			byte b = 0;
			while ((int)b < this.table.Count)
			{
				if (this.table[(int)b].vehicle == id)
				{
					return;
				}
				b += 1;
			}
			this.table.Add(new VehicleSpawn(id));
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x000E4A2D File Offset: 0x000E2E2D
		public void removeVehicle(byte index)
		{
			this.table.RemoveAt((int)index);
		}

		// Token: 0x04001826 RID: 6182
		private List<VehicleSpawn> _table;

		// Token: 0x04001827 RID: 6183
		public string name;

		// Token: 0x04001828 RID: 6184
		public float chance;
	}
}
