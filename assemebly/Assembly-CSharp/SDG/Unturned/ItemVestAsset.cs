using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003F2 RID: 1010
	public class ItemVestAsset : ItemBagAsset
	{
		// Token: 0x06001B49 RID: 6985 RVA: 0x000974E6 File Offset: 0x000958E6
		public ItemVestAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (!Dedicator.isDedicated)
			{
				this._vest = (GameObject)bundle.load("Vest");
			}
			bundle.unload();
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x00097519 File Offset: 0x00095919
		public GameObject vest
		{
			get
			{
				return this._vest;
			}
		}

		// Token: 0x04001020 RID: 4128
		protected GameObject _vest;
	}
}
