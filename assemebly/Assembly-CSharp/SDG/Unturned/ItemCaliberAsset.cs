using System;

namespace SDG.Unturned
{
	// Token: 0x020003C5 RID: 965
	public class ItemCaliberAsset : ItemAsset
	{
		// Token: 0x06001A67 RID: 6759 RVA: 0x00093D44 File Offset: 0x00092144
		public ItemCaliberAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._calibers = new ushort[(int)data.readByte("Calibers")];
			byte b = 0;
			while ((int)b < this.calibers.Length)
			{
				this._calibers[(int)b] = data.readUInt16("Caliber_" + b);
				b += 1;
			}
			this._recoil_x = data.readSingle("Recoil_X");
			if ((double)this.recoil_x < 0.01)
			{
				this._recoil_x = 1f;
			}
			this._recoil_y = data.readSingle("Recoil_Y");
			if ((double)this.recoil_y < 0.01)
			{
				this._recoil_y = 1f;
			}
			this._spread = data.readSingle("Spread");
			if ((double)this.spread < 0.01)
			{
				this._spread = 1f;
			}
			this._sway = data.readSingle("Sway");
			if ((double)this.sway < 0.01)
			{
				this._sway = 1f;
			}
			this._shake = data.readSingle("Shake");
			if ((double)this.shake < 0.01)
			{
				this._shake = 1f;
			}
			this._damage = data.readSingle("Damage");
			if ((double)this.damage < 0.01)
			{
				this._damage = 1f;
			}
			this._firerate = data.readByte("Firerate");
			this._isPaintable = data.has("Paintable");
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001A68 RID: 6760 RVA: 0x00093EF2 File Offset: 0x000922F2
		public ushort[] calibers
		{
			get
			{
				return this._calibers;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00093EFA File Offset: 0x000922FA
		public float recoil_x
		{
			get
			{
				return this._recoil_x;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x00093F02 File Offset: 0x00092302
		public float recoil_y
		{
			get
			{
				return this._recoil_y;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x00093F0A File Offset: 0x0009230A
		public float spread
		{
			get
			{
				return this._spread;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x00093F12 File Offset: 0x00092312
		public float sway
		{
			get
			{
				return this._sway;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x00093F1A File Offset: 0x0009231A
		public float shake
		{
			get
			{
				return this._shake;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x00093F22 File Offset: 0x00092322
		public float damage
		{
			get
			{
				return this._damage;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x00093F2A File Offset: 0x0009232A
		public byte firerate
		{
			get
			{
				return this._firerate;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x00093F32 File Offset: 0x00092332
		public bool isPaintable
		{
			get
			{
				return this._isPaintable;
			}
		}

		// Token: 0x04000F34 RID: 3892
		private ushort[] _calibers;

		// Token: 0x04000F35 RID: 3893
		private float _recoil_x;

		// Token: 0x04000F36 RID: 3894
		private float _recoil_y;

		// Token: 0x04000F37 RID: 3895
		private float _spread;

		// Token: 0x04000F38 RID: 3896
		private float _sway;

		// Token: 0x04000F39 RID: 3897
		private float _shake;

		// Token: 0x04000F3A RID: 3898
		private float _damage;

		// Token: 0x04000F3B RID: 3899
		private byte _firerate;

		// Token: 0x04000F3C RID: 3900
		protected bool _isPaintable;
	}
}
