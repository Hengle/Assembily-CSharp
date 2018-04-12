using System;

namespace SDG.Unturned
{
	// Token: 0x020003CE RID: 974
	public class ItemFoodAsset : ItemConsumeableAsset
	{
		// Token: 0x06001A9A RID: 6810 RVA: 0x00094DCD File Offset: 0x000931CD
		public ItemFoodAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			bundle.unload();
		}
	}
}
