using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003E5 RID: 997
	public class ItemShirtAsset : ItemBagAsset
	{
		// Token: 0x06001B09 RID: 6921 RVA: 0x00096D54 File Offset: 0x00095154
		public ItemShirtAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (!Dedicator.isDedicated)
			{
				this._shirt = (Texture2D)bundle.load("Shirt");
				this._emission = (Texture2D)bundle.load("Emission");
				this._metallic = (Texture2D)bundle.load("Metallic");
			}
			this._ignoreHand = data.has("Ignore_Hand");
			bundle.unload();
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x00096DCF File Offset: 0x000951CF
		public Texture2D shirt
		{
			get
			{
				return this._shirt;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x00096DD7 File Offset: 0x000951D7
		public Texture2D emission
		{
			get
			{
				return this._emission;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x00096DDF File Offset: 0x000951DF
		public Texture2D metallic
		{
			get
			{
				return this._metallic;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x00096DE7 File Offset: 0x000951E7
		public bool ignoreHand
		{
			get
			{
				return this._ignoreHand;
			}
		}

		// Token: 0x04000FE5 RID: 4069
		protected Texture2D _shirt;

		// Token: 0x04000FE6 RID: 4070
		protected Texture2D _emission;

		// Token: 0x04000FE7 RID: 4071
		protected Texture2D _metallic;

		// Token: 0x04000FE8 RID: 4072
		protected bool _ignoreHand;
	}
}
