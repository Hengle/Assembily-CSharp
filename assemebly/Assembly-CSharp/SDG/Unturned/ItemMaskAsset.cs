using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003DB RID: 987
	public class ItemMaskAsset : ItemGearAsset
	{
		// Token: 0x06001AE5 RID: 6885 RVA: 0x000967B0 File Offset: 0x00094BB0
		public ItemMaskAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (!Dedicator.isDedicated)
			{
				this._mask = (GameObject)bundle.load("Mask");
			}
			if (!this.isPro)
			{
				this._proofRadiation = data.has("Proof_Radiation");
				this._isEarpiece = data.has("Earpiece");
			}
			bundle.unload();
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x0009681B File Offset: 0x00094C1B
		public GameObject mask
		{
			get
			{
				return this._mask;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x00096823 File Offset: 0x00094C23
		public bool proofRadiation
		{
			get
			{
				return this._proofRadiation;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x0009682B File Offset: 0x00094C2B
		public bool isEarpiece
		{
			get
			{
				return this._isEarpiece;
			}
		}

		// Token: 0x04000FC6 RID: 4038
		protected GameObject _mask;

		// Token: 0x04000FC7 RID: 4039
		private bool _proofRadiation;

		// Token: 0x04000FC8 RID: 4040
		private bool _isEarpiece;
	}
}
