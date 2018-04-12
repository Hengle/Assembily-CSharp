using System;

namespace SDG.Unturned
{
	// Token: 0x020003D0 RID: 976
	public class ItemGearAsset : ItemClothingAsset
	{
		// Token: 0x06001AA0 RID: 6816 RVA: 0x00094EDE File Offset: 0x000932DE
		public ItemGearAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._hasHair = data.has("Hair");
			this._hasBeard = data.has("Beard");
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x00094F0D File Offset: 0x0009330D
		public bool hasHair
		{
			get
			{
				return this._hasHair;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x00094F15 File Offset: 0x00093315
		public bool hasBeard
		{
			get
			{
				return this._hasBeard;
			}
		}

		// Token: 0x04000F64 RID: 3940
		protected bool _hasHair;

		// Token: 0x04000F65 RID: 3941
		protected bool _hasBeard;
	}
}
