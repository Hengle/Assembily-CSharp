using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003CF RID: 975
	public class ItemFuelAsset : ItemAsset
	{
		// Token: 0x06001A9B RID: 6811 RVA: 0x00094DE0 File Offset: 0x000931E0
		public ItemFuelAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._use = (AudioClip)bundle.load("Use");
			this._fuel = data.readUInt16("Fuel");
			this.fuelState = BitConverter.GetBytes(this.fuel);
			bundle.unload();
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x00094E36 File Offset: 0x00093236
		public AudioClip use
		{
			get
			{
				return this._use;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x00094E3E File Offset: 0x0009323E
		public ushort fuel
		{
			get
			{
				return this._fuel;
			}
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x00094E48 File Offset: 0x00093248
		public override byte[] getState(EItemOrigin origin)
		{
			byte[] array = new byte[2];
			if (origin == EItemOrigin.ADMIN)
			{
				array[0] = this.fuelState[0];
				array[1] = this.fuelState[1];
			}
			return array;
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00094E7C File Offset: 0x0009327C
		public override string getContext(string desc, byte[] state)
		{
			ushort num = BitConverter.ToUInt16(state, 0);
			desc += PlayerDashboardInventoryUI.localization.format("Fuel", new object[]
			{
				((int)((float)num / (float)this.fuel * 100f)).ToString()
			});
			desc += "\n\n";
			return desc;
		}

		// Token: 0x04000F61 RID: 3937
		protected AudioClip _use;

		// Token: 0x04000F62 RID: 3938
		protected ushort _fuel;

		// Token: 0x04000F63 RID: 3939
		private byte[] fuelState;
	}
}
