using System;

namespace SDG.Unturned
{
	// Token: 0x020003F3 RID: 1011
	public class ItemWaterAsset : ItemConsumeableAsset
	{
		// Token: 0x06001B4B RID: 6987 RVA: 0x00097521 File Offset: 0x00095921
		public ItemWaterAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			bundle.unload();
		}
	}
}
