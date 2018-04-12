using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003C2 RID: 962
	public class ItemBarrelAsset : ItemCaliberAsset
	{
		// Token: 0x06001A45 RID: 6725 RVA: 0x00093F3C File Offset: 0x0009233C
		public ItemBarrelAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._shoot = (AudioClip)bundle.load("Shoot");
			this._barrel = (GameObject)bundle.load("Barrel");
			this._isBraked = data.has("Braked");
			this._isSilenced = data.has("Silenced");
			this._volume = data.readSingle("Volume");
			this._durability = data.readByte("Durability");
			if (data.has("Ballistic_Drop"))
			{
				this._ballisticDrop = data.readSingle("Ballistic_Drop");
			}
			else
			{
				this._ballisticDrop = 1f;
			}
			bundle.unload();
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x00093FFB File Offset: 0x000923FB
		public AudioClip shoot
		{
			get
			{
				return this._shoot;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x00094003 File Offset: 0x00092403
		public GameObject barrel
		{
			get
			{
				return this._barrel;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x0009400B File Offset: 0x0009240B
		public bool isBraked
		{
			get
			{
				return this._isBraked;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x00094013 File Offset: 0x00092413
		public bool isSilenced
		{
			get
			{
				return this._isSilenced;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x0009401B File Offset: 0x0009241B
		public float volume
		{
			get
			{
				return this._volume;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x00094023 File Offset: 0x00092423
		public byte durability
		{
			get
			{
				return this._durability;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x0009402B File Offset: 0x0009242B
		public override bool showQuality
		{
			get
			{
				return this.durability > 0;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x00094036 File Offset: 0x00092436
		public float ballisticDrop
		{
			get
			{
				return this._ballisticDrop;
			}
		}

		// Token: 0x04000F18 RID: 3864
		protected AudioClip _shoot;

		// Token: 0x04000F19 RID: 3865
		protected GameObject _barrel;

		// Token: 0x04000F1A RID: 3866
		private bool _isBraked;

		// Token: 0x04000F1B RID: 3867
		private bool _isSilenced;

		// Token: 0x04000F1C RID: 3868
		private float _volume;

		// Token: 0x04000F1D RID: 3869
		private byte _durability;

		// Token: 0x04000F1E RID: 3870
		private float _ballisticDrop;
	}
}
