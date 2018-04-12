using System;

namespace SDG.Unturned
{
	// Token: 0x020003F1 RID: 1009
	public class ItemVehicleRepairToolAsset : ItemToolAsset
	{
		// Token: 0x06001B47 RID: 6983 RVA: 0x00097356 File Offset: 0x00095756
		public ItemVehicleRepairToolAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			bundle.unload();
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x00097369 File Offset: 0x00095769
		public override bool isDangerous
		{
			get
			{
				return false;
			}
		}
	}
}
