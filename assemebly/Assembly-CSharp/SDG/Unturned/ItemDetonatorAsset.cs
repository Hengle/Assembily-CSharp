using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003CA RID: 970
	public class ItemDetonatorAsset : ItemAsset
	{
		// Token: 0x06001A8E RID: 6798 RVA: 0x00094C99 File Offset: 0x00093099
		public ItemDetonatorAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._use = (AudioClip)bundle.load("Use");
			bundle.unload();
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x00094CC2 File Offset: 0x000930C2
		public AudioClip use
		{
			get
			{
				return this._use;
			}
		}

		// Token: 0x04000F59 RID: 3929
		protected AudioClip _use;
	}
}
