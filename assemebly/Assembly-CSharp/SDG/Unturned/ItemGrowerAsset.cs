using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003D4 RID: 980
	public class ItemGrowerAsset : ItemAsset
	{
		// Token: 0x06001AB1 RID: 6833 RVA: 0x0009509A File Offset: 0x0009349A
		public ItemGrowerAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._use = (AudioClip)bundle.load("Use");
			bundle.unload();
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x000950C3 File Offset: 0x000934C3
		public AudioClip use
		{
			get
			{
				return this._use;
			}
		}

		// Token: 0x04000F6E RID: 3950
		protected AudioClip _use;
	}
}
