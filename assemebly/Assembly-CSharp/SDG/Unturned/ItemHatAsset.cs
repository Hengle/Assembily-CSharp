using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003D7 RID: 983
	public class ItemHatAsset : ItemGearAsset
	{
		// Token: 0x06001AD2 RID: 6866 RVA: 0x00096506 File Offset: 0x00094906
		public ItemHatAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (!Dedicator.isDedicated)
			{
				this._hat = (GameObject)bundle.load("Hat");
			}
			bundle.unload();
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x00096539 File Offset: 0x00094939
		public GameObject hat
		{
			get
			{
				return this._hat;
			}
		}

		// Token: 0x04000FB0 RID: 4016
		protected GameObject _hat;
	}
}
