using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003C0 RID: 960
	public class ItemBackpackAsset : ItemBagAsset
	{
		// Token: 0x06001A40 RID: 6720 RVA: 0x00093D07 File Offset: 0x00092107
		public ItemBackpackAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (!Dedicator.isDedicated)
			{
				this._backpack = (GameObject)bundle.load("Backpack");
			}
			bundle.unload();
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x00093D3A File Offset: 0x0009213A
		public GameObject backpack
		{
			get
			{
				return this._backpack;
			}
		}

		// Token: 0x04000F15 RID: 3861
		protected GameObject _backpack;
	}
}
