using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003E0 RID: 992
	public class ItemPantsAsset : ItemBagAsset
	{
		// Token: 0x06001AFE RID: 6910 RVA: 0x00096AB4 File Offset: 0x00094EB4
		public ItemPantsAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (!Dedicator.isDedicated)
			{
				this._pants = (Texture2D)bundle.load("Pants");
				this._emission = (Texture2D)bundle.load("Emission");
				this._metallic = (Texture2D)bundle.load("Metallic");
			}
			bundle.unload();
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x00096B1E File Offset: 0x00094F1E
		public Texture2D pants
		{
			get
			{
				return this._pants;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x00096B26 File Offset: 0x00094F26
		public Texture2D emission
		{
			get
			{
				return this._emission;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x00096B2E File Offset: 0x00094F2E
		public Texture2D metallic
		{
			get
			{
				return this._metallic;
			}
		}

		// Token: 0x04000FD6 RID: 4054
		protected Texture2D _pants;

		// Token: 0x04000FD7 RID: 4055
		protected Texture2D _emission;

		// Token: 0x04000FD8 RID: 4056
		protected Texture2D _metallic;
	}
}
