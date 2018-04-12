using System;

namespace SDG.Unturned
{
	// Token: 0x020003DF RID: 991
	public class ItemOpticAsset : ItemAsset
	{
		// Token: 0x06001AFC RID: 6908 RVA: 0x00096A81 File Offset: 0x00094E81
		public ItemOpticAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._zoom = 90f / (float)data.readByte("Zoom");
			bundle.unload();
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x00096AAC File Offset: 0x00094EAC
		public float zoom
		{
			get
			{
				return this._zoom;
			}
		}

		// Token: 0x04000FD5 RID: 4053
		private float _zoom;
	}
}
