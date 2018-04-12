using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003CC RID: 972
	public class ItemFilterAsset : ItemAsset
	{
		// Token: 0x06001A93 RID: 6803 RVA: 0x00094D09 File Offset: 0x00093109
		public ItemFilterAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._use = (AudioClip)bundle.load("Use");
			bundle.unload();
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x00094D32 File Offset: 0x00093132
		public AudioClip use
		{
			get
			{
				return this._use;
			}
		}

		// Token: 0x04000F5C RID: 3932
		protected AudioClip _use;
	}
}
