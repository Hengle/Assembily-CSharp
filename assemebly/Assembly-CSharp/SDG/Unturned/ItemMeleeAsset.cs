using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003DD RID: 989
	public class ItemMeleeAsset : ItemWeaponAsset
	{
		// Token: 0x06001AEA RID: 6890 RVA: 0x00096848 File Offset: 0x00094C48
		public ItemMeleeAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._use = (AudioClip)bundle.load("Use");
			this._strength = data.readSingle("Strength");
			this._weak = data.readSingle("Weak");
			if ((double)this.weak < 0.01)
			{
				this._weak = 0.5f;
			}
			this._strong = data.readSingle("Strong");
			if ((double)this.strong < 0.01)
			{
				this._strong = 0.33f;
			}
			this._stamina = data.readByte("Stamina");
			this._isRepair = data.has("Repair");
			this._isRepeated = data.has("Repeated");
			this._isLight = data.has("Light");
			if (data.has("Alert_Radius"))
			{
				this.alertRadius = data.readSingle("Alert_Radius");
			}
			else
			{
				this.alertRadius = 8f;
			}
			bundle.unload();
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x00096964 File Offset: 0x00094D64
		public AudioClip use
		{
			get
			{
				return this._use;
			}
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x0009696C File Offset: 0x00094D6C
		public override byte[] getState(EItemOrigin origin)
		{
			if (this.isLight)
			{
				return new byte[]
				{
					1
				};
			}
			return new byte[0];
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x0009698A File Offset: 0x00094D8A
		public float strength
		{
			get
			{
				return this._strength;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x00096992 File Offset: 0x00094D92
		public float weak
		{
			get
			{
				return this._weak;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x0009699A File Offset: 0x00094D9A
		public float strong
		{
			get
			{
				return this._strong;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x000969A2 File Offset: 0x00094DA2
		public byte stamina
		{
			get
			{
				return this._stamina;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x000969AA File Offset: 0x00094DAA
		public bool isRepair
		{
			get
			{
				return this._isRepair;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x000969B2 File Offset: 0x00094DB2
		public bool isRepeated
		{
			get
			{
				return this._isRepeated;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x000969BA File Offset: 0x00094DBA
		public bool isLight
		{
			get
			{
				return this._isLight;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x000969C2 File Offset: 0x00094DC2
		public override bool showQuality
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x000969C5 File Offset: 0x00094DC5
		public override bool isDangerous
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x000969C8 File Offset: 0x00094DC8
		// (set) Token: 0x06001AF7 RID: 6903 RVA: 0x000969D0 File Offset: 0x00094DD0
		public float alertRadius { get; protected set; }

		// Token: 0x04000FC9 RID: 4041
		protected AudioClip _use;

		// Token: 0x04000FCA RID: 4042
		private float _strength;

		// Token: 0x04000FCB RID: 4043
		private float _weak;

		// Token: 0x04000FCC RID: 4044
		private float _strong;

		// Token: 0x04000FCD RID: 4045
		private byte _stamina;

		// Token: 0x04000FCE RID: 4046
		private bool _isRepair;

		// Token: 0x04000FCF RID: 4047
		private bool _isRepeated;

		// Token: 0x04000FD0 RID: 4048
		private bool _isLight;
	}
}
