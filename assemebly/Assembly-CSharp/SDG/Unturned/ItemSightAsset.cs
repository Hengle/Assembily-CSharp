using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003E6 RID: 998
	public class ItemSightAsset : ItemCaliberAsset
	{
		// Token: 0x06001B0E RID: 6926 RVA: 0x00096DF0 File Offset: 0x000951F0
		public ItemSightAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._sight = (GameObject)bundle.load("Sight");
			if (data.has("Vision"))
			{
				this._vision = (ELightingVision)Enum.Parse(typeof(ELightingVision), data.readString("Vision"), true);
			}
			else
			{
				this._vision = ELightingVision.NONE;
			}
			if (data.has("Zoom"))
			{
				this._zoom = 90f / (float)data.readByte("Zoom");
			}
			else
			{
				this._zoom = 90f;
			}
			this._isHolographic = data.has("Holographic");
			bundle.unload();
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x00096EAF File Offset: 0x000952AF
		public GameObject sight
		{
			get
			{
				return this._sight;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x00096EB7 File Offset: 0x000952B7
		public ELightingVision vision
		{
			get
			{
				return this._vision;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x00096EBF File Offset: 0x000952BF
		public float zoom
		{
			get
			{
				return this._zoom;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x00096EC7 File Offset: 0x000952C7
		public bool isHolographic
		{
			get
			{
				return this._isHolographic;
			}
		}

		// Token: 0x04000FE9 RID: 4073
		protected GameObject _sight;

		// Token: 0x04000FEA RID: 4074
		private ELightingVision _vision;

		// Token: 0x04000FEB RID: 4075
		private float _zoom;

		// Token: 0x04000FEC RID: 4076
		private bool _isHolographic;
	}
}
