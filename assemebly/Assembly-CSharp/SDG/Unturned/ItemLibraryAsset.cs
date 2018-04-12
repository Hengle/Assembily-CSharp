using System;

namespace SDG.Unturned
{
	// Token: 0x020003D9 RID: 985
	public class ItemLibraryAsset : ItemBarricadeAsset
	{
		// Token: 0x06001AD5 RID: 6869 RVA: 0x00096565 File Offset: 0x00094965
		public ItemLibraryAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._capacity = data.readUInt32("Capacity");
			this._tax = data.readByte("Tax");
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x00096594 File Offset: 0x00094994
		public uint capacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x0009659C File Offset: 0x0009499C
		public byte tax
		{
			get
			{
				return this._tax;
			}
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x000965A4 File Offset: 0x000949A4
		public override byte[] getState(EItemOrigin origin)
		{
			return new byte[20];
		}

		// Token: 0x04000FB2 RID: 4018
		protected uint _capacity;

		// Token: 0x04000FB3 RID: 4019
		protected byte _tax;
	}
}
