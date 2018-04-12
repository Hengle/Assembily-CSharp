using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003E2 RID: 994
	public class ItemRefillAsset : ItemAsset
	{
		// Token: 0x06001B02 RID: 6914 RVA: 0x00096B36 File Offset: 0x00094F36
		public ItemRefillAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._use = (AudioClip)bundle.load("Use");
			this._water = data.readByte("Water");
			bundle.unload();
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x00096B70 File Offset: 0x00094F70
		public AudioClip use
		{
			get
			{
				return this._use;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x00096B78 File Offset: 0x00094F78
		public byte water
		{
			get
			{
				return this._water;
			}
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00096B80 File Offset: 0x00094F80
		public override byte[] getState(EItemOrigin origin)
		{
			byte[] array = new byte[1];
			if (origin == EItemOrigin.ADMIN)
			{
				array[0] = 1;
			}
			else
			{
				array[0] = 0;
			}
			return array;
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x00096BAC File Offset: 0x00094FAC
		public override string getContext(string desc, byte[] state)
		{
			string key;
			switch (state[0])
			{
			case 0:
				key = "Empty";
				break;
			case 1:
				key = "Clean";
				break;
			case 2:
				key = "Salty";
				break;
			case 3:
				key = "Dirty";
				break;
			default:
				key = "Full";
				break;
			}
			desc += PlayerDashboardInventoryUI.localization.format("Refill", new object[]
			{
				PlayerDashboardInventoryUI.localization.format(key)
			});
			desc += "\n\n";
			return desc;
		}

		// Token: 0x04000FDE RID: 4062
		protected AudioClip _use;

		// Token: 0x04000FDF RID: 4063
		protected byte _water;
	}
}
