using System;

namespace SDG.Unturned
{
	// Token: 0x020003DC RID: 988
	public class ItemMedicalAsset : ItemConsumeableAsset
	{
		// Token: 0x06001AE9 RID: 6889 RVA: 0x00096833 File Offset: 0x00094C33
		public ItemMedicalAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			bundle.unload();
		}
	}
}
