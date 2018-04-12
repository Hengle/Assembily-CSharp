using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003EA RID: 1002
	public class ItemTacticalAsset : ItemCaliberAsset
	{
		// Token: 0x06001B29 RID: 6953 RVA: 0x000970A0 File Offset: 0x000954A0
		public ItemTacticalAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._tactical = (GameObject)bundle.load("Tactical");
			this._isLaser = data.has("Laser");
			this._isLight = data.has("Light");
			this._isRangefinder = data.has("Rangefinder");
			this._isMelee = data.has("Melee");
			bundle.unload();
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x00097118 File Offset: 0x00095518
		public GameObject tactical
		{
			get
			{
				return this._tactical;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x00097120 File Offset: 0x00095520
		public bool isLaser
		{
			get
			{
				return this._isLaser;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x00097128 File Offset: 0x00095528
		public bool isLight
		{
			get
			{
				return this._isLight;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x00097130 File Offset: 0x00095530
		public bool isRangefinder
		{
			get
			{
				return this._isRangefinder;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x00097138 File Offset: 0x00095538
		public bool isMelee
		{
			get
			{
				return this._isMelee;
			}
		}

		// Token: 0x04000FFE RID: 4094
		protected GameObject _tactical;

		// Token: 0x04000FFF RID: 4095
		private bool _isLaser;

		// Token: 0x04001000 RID: 4096
		private bool _isLight;

		// Token: 0x04001001 RID: 4097
		private bool _isRangefinder;

		// Token: 0x04001002 RID: 4098
		private bool _isMelee;
	}
}
