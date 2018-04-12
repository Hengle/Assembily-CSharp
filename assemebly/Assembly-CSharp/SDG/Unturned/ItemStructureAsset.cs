using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003E8 RID: 1000
	public class ItemStructureAsset : ItemAsset
	{
		// Token: 0x06001B18 RID: 6936 RVA: 0x00096ED0 File Offset: 0x000952D0
		public ItemStructureAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (!Dedicator.isDedicated)
			{
				this._structure = (GameObject)bundle.load("Structure");
			}
			this._clip = (GameObject)bundle.load("Clip");
			this._nav = (GameObject)bundle.load("Nav");
			this._use = (AudioClip)bundle.load("Use");
			this._construct = (EConstruct)Enum.Parse(typeof(EConstruct), data.readString("Construct"), true);
			this._health = data.readUInt16("Health");
			this._range = data.readSingle("Range");
			this._explosion = data.readUInt16("Explosion");
			this._isVulnerable = data.has("Vulnerable");
			this._isRepairable = !data.has("Unrepairable");
			this._proofExplosion = data.has("Proof_Explosion");
			this._isUnpickupable = data.has("Unpickupable");
			this._isSalvageable = !data.has("Unsalvageable");
			this._isSaveable = !data.has("Unsaveable");
			bundle.unload();
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x00097018 File Offset: 0x00095418
		public GameObject structure
		{
			get
			{
				return this._structure;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x00097020 File Offset: 0x00095420
		public GameObject clip
		{
			get
			{
				return this._clip;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x00097028 File Offset: 0x00095428
		public GameObject nav
		{
			get
			{
				return this._nav;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001B1C RID: 6940 RVA: 0x00097030 File Offset: 0x00095430
		public AudioClip use
		{
			get
			{
				return this._use;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x00097038 File Offset: 0x00095438
		public EConstruct construct
		{
			get
			{
				return this._construct;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001B1E RID: 6942 RVA: 0x00097040 File Offset: 0x00095440
		public ushort health
		{
			get
			{
				return this._health;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x00097048 File Offset: 0x00095448
		public float range
		{
			get
			{
				return this._range;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x00097050 File Offset: 0x00095450
		public ushort explosion
		{
			get
			{
				return this._explosion;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001B21 RID: 6945 RVA: 0x00097058 File Offset: 0x00095458
		public bool isVulnerable
		{
			get
			{
				return this._isVulnerable;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x00097060 File Offset: 0x00095460
		public bool isRepairable
		{
			get
			{
				return this._isRepairable;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x00097068 File Offset: 0x00095468
		public bool proofExplosion
		{
			get
			{
				return this._proofExplosion;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001B24 RID: 6948 RVA: 0x00097070 File Offset: 0x00095470
		public bool isUnpickupable
		{
			get
			{
				return this._isUnpickupable;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x00097078 File Offset: 0x00095478
		public bool isSalvageable
		{
			get
			{
				return this._isSalvageable;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x00097080 File Offset: 0x00095480
		public bool isSaveable
		{
			get
			{
				return this._isSaveable;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x00097088 File Offset: 0x00095488
		public override bool isDangerous
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000FF0 RID: 4080
		protected GameObject _structure;

		// Token: 0x04000FF1 RID: 4081
		protected GameObject _clip;

		// Token: 0x04000FF2 RID: 4082
		protected GameObject _nav;

		// Token: 0x04000FF3 RID: 4083
		protected AudioClip _use;

		// Token: 0x04000FF4 RID: 4084
		protected EConstruct _construct;

		// Token: 0x04000FF5 RID: 4085
		protected ushort _health;

		// Token: 0x04000FF6 RID: 4086
		protected float _range;

		// Token: 0x04000FF7 RID: 4087
		protected ushort _explosion;

		// Token: 0x04000FF8 RID: 4088
		protected bool _isVulnerable;

		// Token: 0x04000FF9 RID: 4089
		protected bool _isRepairable;

		// Token: 0x04000FFA RID: 4090
		protected bool _proofExplosion;

		// Token: 0x04000FFB RID: 4091
		protected bool _isUnpickupable;

		// Token: 0x04000FFC RID: 4092
		protected bool _isSalvageable;

		// Token: 0x04000FFD RID: 4093
		protected bool _isSaveable;
	}
}
