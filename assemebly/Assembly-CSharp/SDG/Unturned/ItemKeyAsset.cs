using System;

namespace SDG.Unturned
{
	// Token: 0x020003D8 RID: 984
	public class ItemKeyAsset : ItemAsset
	{
		// Token: 0x06001AD4 RID: 6868 RVA: 0x00096541 File Offset: 0x00094941
		public ItemKeyAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this.exchangeWithTargetItem = data.has("Exchange_With_Target_Item");
			bundle.unload();
		}

		// Token: 0x04000FB1 RID: 4017
		public bool exchangeWithTargetItem;
	}
}
