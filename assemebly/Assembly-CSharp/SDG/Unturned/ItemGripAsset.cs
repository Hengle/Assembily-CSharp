using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003D3 RID: 979
	public class ItemGripAsset : ItemCaliberAsset
	{
		// Token: 0x06001AAE RID: 6830 RVA: 0x00095050 File Offset: 0x00093450
		public ItemGripAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._grip = (GameObject)bundle.load("Grip");
			this._isBipod = data.has("Bipod");
			bundle.unload();
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0009508A File Offset: 0x0009348A
		public GameObject grip
		{
			get
			{
				return this._grip;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x00095092 File Offset: 0x00093492
		public bool isBipod
		{
			get
			{
				return this._isBipod;
			}
		}

		// Token: 0x04000F6C RID: 3948
		protected GameObject _grip;

		// Token: 0x04000F6D RID: 3949
		private bool _isBipod;
	}
}
