using System;

namespace SDG.Unturned
{
	// Token: 0x020003CB RID: 971
	public class ItemFarmAsset : ItemBarricadeAsset
	{
		// Token: 0x06001A90 RID: 6800 RVA: 0x00094CCA File Offset: 0x000930CA
		public ItemFarmAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._growth = data.readUInt32("Growth");
			this._grow = data.readUInt16("Grow");
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x00094CF9 File Offset: 0x000930F9
		public uint growth
		{
			get
			{
				return this._growth;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x00094D01 File Offset: 0x00093101
		public ushort grow
		{
			get
			{
				return this._grow;
			}
		}

		// Token: 0x04000F5A RID: 3930
		protected uint _growth;

		// Token: 0x04000F5B RID: 3931
		protected ushort _grow;
	}
}
