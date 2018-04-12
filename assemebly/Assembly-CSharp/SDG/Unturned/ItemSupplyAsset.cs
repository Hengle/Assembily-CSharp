using System;

namespace SDG.Unturned
{
	// Token: 0x020003E9 RID: 1001
	public class ItemSupplyAsset : ItemAsset
	{
		// Token: 0x06001B28 RID: 6952 RVA: 0x0009708B File Offset: 0x0009548B
		public ItemSupplyAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			bundle.unload();
		}
	}
}
