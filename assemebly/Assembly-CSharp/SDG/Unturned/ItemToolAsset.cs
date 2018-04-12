using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003EF RID: 1007
	public class ItemToolAsset : ItemAsset
	{
		// Token: 0x06001B3F RID: 6975 RVA: 0x00097322 File Offset: 0x00095722
		public ItemToolAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._use = (AudioClip)bundle.load("Use");
			bundle.unload();
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x0009734B File Offset: 0x0009574B
		public AudioClip use
		{
			get
			{
				return this._use;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x00097353 File Offset: 0x00095753
		public override bool isDangerous
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001012 RID: 4114
		protected AudioClip _use;
	}
}
