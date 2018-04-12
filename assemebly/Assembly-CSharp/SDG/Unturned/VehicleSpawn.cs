using System;

namespace SDG.Unturned
{
	// Token: 0x02000573 RID: 1395
	public class VehicleSpawn
	{
		// Token: 0x06002685 RID: 9861 RVA: 0x000E4172 File Offset: 0x000E2572
		public VehicleSpawn(ushort newVehicle)
		{
			this._vehicle = newVehicle;
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x000E4181 File Offset: 0x000E2581
		public ushort vehicle
		{
			get
			{
				return this._vehicle;
			}
		}

		// Token: 0x0400181D RID: 6173
		private ushort _vehicle;
	}
}
