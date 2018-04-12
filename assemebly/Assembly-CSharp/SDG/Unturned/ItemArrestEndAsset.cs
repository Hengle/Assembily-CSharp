using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003BC RID: 956
	public class ItemArrestEndAsset : ItemAsset
	{
		// Token: 0x06001A1A RID: 6682 RVA: 0x00093B83 File Offset: 0x00091F83
		public ItemArrestEndAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._use = (AudioClip)bundle.load("Use");
			this._recover = data.readUInt16("Recover");
			bundle.unload();
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x00093BBD File Offset: 0x00091FBD
		public AudioClip use
		{
			get
			{
				return this._use;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00093BC5 File Offset: 0x00091FC5
		public ushort recover
		{
			get
			{
				return this._recover;
			}
		}

		// Token: 0x04000EF4 RID: 3828
		protected AudioClip _use;

		// Token: 0x04000EF5 RID: 3829
		protected ushort _recover;
	}
}
