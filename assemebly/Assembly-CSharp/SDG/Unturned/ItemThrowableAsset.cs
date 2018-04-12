using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003EC RID: 1004
	public class ItemThrowableAsset : ItemWeaponAsset
	{
		// Token: 0x06001B33 RID: 6963 RVA: 0x000971E4 File Offset: 0x000955E4
		public ItemThrowableAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._use = (AudioClip)bundle.load("Use");
			this._throwable = (GameObject)bundle.load("Throwable");
			this._explosion = data.readUInt16("Explosion");
			this._isExplosive = data.has("Explosive");
			this._isFlash = data.has("Flash");
			this._isSticky = data.has("Sticky");
			this._explodeOnImpact = data.has("Explode_On_Impact");
			if (data.has("Fuse_Length"))
			{
				this._fuseLength = data.readSingle("Fuse_Length");
			}
			else if (this.isExplosive || this.isFlash)
			{
				this._fuseLength = 2.5f;
			}
			else
			{
				this._fuseLength = 180f;
			}
			bundle.unload();
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x000972DA File Offset: 0x000956DA
		public AudioClip use
		{
			get
			{
				return this._use;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x000972E2 File Offset: 0x000956E2
		public GameObject throwable
		{
			get
			{
				return this._throwable;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x000972EA File Offset: 0x000956EA
		public ushort explosion
		{
			get
			{
				return this._explosion;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x000972F2 File Offset: 0x000956F2
		public bool isExplosive
		{
			get
			{
				return this._isExplosive;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x000972FA File Offset: 0x000956FA
		public bool isFlash
		{
			get
			{
				return this._isFlash;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x00097302 File Offset: 0x00095702
		public bool isSticky
		{
			get
			{
				return this._isSticky;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0009730A File Offset: 0x0009570A
		public bool explodeOnImpact
		{
			get
			{
				return this._explodeOnImpact;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x00097312 File Offset: 0x00095712
		public float fuseLength
		{
			get
			{
				return this._fuseLength;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x0009731A File Offset: 0x0009571A
		public override bool isDangerous
		{
			get
			{
				return this.isExplosive;
			}
		}

		// Token: 0x04001006 RID: 4102
		protected AudioClip _use;

		// Token: 0x04001007 RID: 4103
		protected GameObject _throwable;

		// Token: 0x04001008 RID: 4104
		private ushort _explosion;

		// Token: 0x04001009 RID: 4105
		private bool _isExplosive;

		// Token: 0x0400100A RID: 4106
		private bool _isFlash;

		// Token: 0x0400100B RID: 4107
		private bool _isSticky;

		// Token: 0x0400100C RID: 4108
		private bool _explodeOnImpact;

		// Token: 0x0400100D RID: 4109
		private float _fuseLength;
	}
}
