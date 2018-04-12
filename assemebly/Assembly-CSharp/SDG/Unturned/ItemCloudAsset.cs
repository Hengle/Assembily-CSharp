using System;

namespace SDG.Unturned
{
	// Token: 0x020003C8 RID: 968
	public class ItemCloudAsset : ItemAsset
	{
		// Token: 0x06001A79 RID: 6777 RVA: 0x000944FB File Offset: 0x000928FB
		public ItemCloudAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._gravity = data.readSingle("Gravity");
			bundle.unload();
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x0009451F File Offset: 0x0009291F
		public float gravity
		{
			get
			{
				return this._gravity;
			}
		}

		// Token: 0x04000F4A RID: 3914
		private float _gravity;
	}
}
