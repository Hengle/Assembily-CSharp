using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003BD RID: 957
	public class ItemArrestStartAsset : ItemAsset
	{
		// Token: 0x06001A1D RID: 6685 RVA: 0x00093BCD File Offset: 0x00091FCD
		public ItemArrestStartAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._use = (AudioClip)bundle.load("Use");
			this._strength = data.readUInt16("Strength");
			bundle.unload();
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x00093C07 File Offset: 0x00092007
		public AudioClip use
		{
			get
			{
				return this._use;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x00093C0F File Offset: 0x0009200F
		public ushort strength
		{
			get
			{
				return this._strength;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x00093C17 File Offset: 0x00092017
		public override bool isDangerous
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000EF6 RID: 3830
		protected AudioClip _use;

		// Token: 0x04000EF7 RID: 3831
		protected ushort _strength;
	}
}
