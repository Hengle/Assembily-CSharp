using System;

namespace SDG.Unturned
{
	// Token: 0x020003C1 RID: 961
	public class ItemBagAsset : ItemClothingAsset
	{
		// Token: 0x06001A42 RID: 6722 RVA: 0x00093CBD File Offset: 0x000920BD
		public ItemBagAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (!this.isPro)
			{
				this._width = data.readByte("Width");
				this._height = data.readByte("Height");
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x00093CF7 File Offset: 0x000920F7
		public byte width
		{
			get
			{
				return this._width;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x00093CFF File Offset: 0x000920FF
		public byte height
		{
			get
			{
				return this._height;
			}
		}

		// Token: 0x04000F16 RID: 3862
		private byte _width;

		// Token: 0x04000F17 RID: 3863
		private byte _height;
	}
}
