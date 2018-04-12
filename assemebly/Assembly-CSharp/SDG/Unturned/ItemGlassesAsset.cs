using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003D2 RID: 978
	public class ItemGlassesAsset : ItemGearAsset
	{
		// Token: 0x06001AA8 RID: 6824 RVA: 0x00094F80 File Offset: 0x00093380
		public ItemGlassesAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (!Dedicator.isDedicated)
			{
				this._glasses = (GameObject)bundle.load("Glasses");
			}
			if (data.has("Vision"))
			{
				this._vision = (ELightingVision)Enum.Parse(typeof(ELightingVision), data.readString("Vision"), true);
			}
			else
			{
				this._vision = ELightingVision.NONE;
			}
			this.isBlindfold = data.has("Blindfold");
			bundle.unload();
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x00095011 File Offset: 0x00093411
		public GameObject glasses
		{
			get
			{
				return this._glasses;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x00095019 File Offset: 0x00093419
		public ELightingVision vision
		{
			get
			{
				return this._vision;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x00095021 File Offset: 0x00093421
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x00095029 File Offset: 0x00093429
		public bool isBlindfold { get; protected set; }

		// Token: 0x06001AAD RID: 6829 RVA: 0x00095032 File Offset: 0x00093432
		public override byte[] getState(EItemOrigin origin)
		{
			if (this.vision != ELightingVision.NONE)
			{
				return new byte[]
				{
					1
				};
			}
			return new byte[0];
		}

		// Token: 0x04000F69 RID: 3945
		protected GameObject _glasses;

		// Token: 0x04000F6A RID: 3946
		private ELightingVision _vision;
	}
}
