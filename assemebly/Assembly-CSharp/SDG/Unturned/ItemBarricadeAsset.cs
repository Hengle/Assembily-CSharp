using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003C3 RID: 963
	public class ItemBarricadeAsset : ItemAsset
	{
		// Token: 0x06001A4E RID: 6734 RVA: 0x00094040 File Offset: 0x00092440
		public ItemBarricadeAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (!Dedicator.isDedicated)
			{
				this._barricade = (GameObject)bundle.load("Barricade");
			}
			this._clip = (GameObject)bundle.load("Clip");
			this._nav = (GameObject)bundle.load("Nav");
			this._use = (AudioClip)bundle.load("Use");
			this._build = (EBuild)Enum.Parse(typeof(EBuild), data.readString("Build"), true);
			this._health = data.readUInt16("Health");
			this._range = data.readSingle("Range");
			this._radius = data.readSingle("Radius");
			this._offset = data.readSingle("Offset");
			this._explosion = data.readUInt16("Explosion");
			this._isLocked = data.has("Locked");
			this._isVulnerable = data.has("Vulnerable");
			this._bypassClaim = data.has("Bypass_Claim");
			this._isRepairable = !data.has("Unrepairable");
			this._proofExplosion = data.has("Proof_Explosion");
			this._isUnpickupable = data.has("Unpickupable");
			this._isSalvageable = !data.has("Unsalvageable");
			this._isSaveable = !data.has("Unsaveable");
			bundle.unload();
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x000941CC File Offset: 0x000925CC
		public GameObject barricade
		{
			get
			{
				return this._barricade;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x000941D4 File Offset: 0x000925D4
		public GameObject clip
		{
			get
			{
				return this._clip;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x000941DC File Offset: 0x000925DC
		public GameObject nav
		{
			get
			{
				return this._nav;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x000941E4 File Offset: 0x000925E4
		public AudioClip use
		{
			get
			{
				return this._use;
			}
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x000941EC File Offset: 0x000925EC
		public override byte[] getState(EItemOrigin origin)
		{
			if (this.build == EBuild.DOOR || this.build == EBuild.GATE || this.build == EBuild.SHUTTER || this.build == EBuild.HATCH)
			{
				return new byte[17];
			}
			if (this.build == EBuild.BED)
			{
				return new byte[8];
			}
			if (this.build == EBuild.FARM)
			{
				return new byte[4];
			}
			if (this.build == EBuild.TORCH || this.build == EBuild.CAMPFIRE || this.build == EBuild.OVEN || this.build == EBuild.SPOT || this.build == EBuild.SAFEZONE || this.build == EBuild.OXYGENATOR || this.build == EBuild.BARREL_RAIN || this.build == EBuild.CAGE)
			{
				return new byte[1];
			}
			if (this.build == EBuild.OIL)
			{
				return new byte[2];
			}
			if (this.build == EBuild.SIGN || this.build == EBuild.SIGN_WALL || this.build == EBuild.NOTE)
			{
				return new byte[17];
			}
			if (this.build == EBuild.STEREO)
			{
				return new byte[17];
			}
			if (this.build == EBuild.MANNEQUIN)
			{
				return new byte[73];
			}
			return new byte[0];
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x0009433A File Offset: 0x0009273A
		public EBuild build
		{
			get
			{
				return this._build;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x00094342 File Offset: 0x00092742
		public ushort health
		{
			get
			{
				return this._health;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x0009434A File Offset: 0x0009274A
		public float range
		{
			get
			{
				return this._range;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x00094352 File Offset: 0x00092752
		public float radius
		{
			get
			{
				return this._radius;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x0009435A File Offset: 0x0009275A
		public float offset
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x00094362 File Offset: 0x00092762
		public ushort explosion
		{
			get
			{
				return this._explosion;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x0009436A File Offset: 0x0009276A
		public bool isLocked
		{
			get
			{
				return this._isLocked;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x00094372 File Offset: 0x00092772
		public bool isVulnerable
		{
			get
			{
				return this._isVulnerable;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x0009437A File Offset: 0x0009277A
		public bool bypassClaim
		{
			get
			{
				return this._bypassClaim;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x00094382 File Offset: 0x00092782
		public bool isRepairable
		{
			get
			{
				return this._isRepairable;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x0009438A File Offset: 0x0009278A
		public bool proofExplosion
		{
			get
			{
				return this._proofExplosion;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001A5F RID: 6751 RVA: 0x00094392 File Offset: 0x00092792
		public bool isUnpickupable
		{
			get
			{
				return this._isUnpickupable;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x0009439A File Offset: 0x0009279A
		public bool isSalvageable
		{
			get
			{
				return this._isSalvageable;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x000943A2 File Offset: 0x000927A2
		public bool isSaveable
		{
			get
			{
				return this._isSaveable;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x000943AA File Offset: 0x000927AA
		public override bool isDangerous
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000F1F RID: 3871
		protected GameObject _barricade;

		// Token: 0x04000F20 RID: 3872
		protected GameObject _clip;

		// Token: 0x04000F21 RID: 3873
		protected GameObject _nav;

		// Token: 0x04000F22 RID: 3874
		protected AudioClip _use;

		// Token: 0x04000F23 RID: 3875
		protected EBuild _build;

		// Token: 0x04000F24 RID: 3876
		protected ushort _health;

		// Token: 0x04000F25 RID: 3877
		protected float _range;

		// Token: 0x04000F26 RID: 3878
		protected float _radius;

		// Token: 0x04000F27 RID: 3879
		protected float _offset;

		// Token: 0x04000F28 RID: 3880
		protected ushort _explosion;

		// Token: 0x04000F29 RID: 3881
		protected bool _isLocked;

		// Token: 0x04000F2A RID: 3882
		protected bool _isVulnerable;

		// Token: 0x04000F2B RID: 3883
		protected bool _bypassClaim;

		// Token: 0x04000F2C RID: 3884
		protected bool _isRepairable;

		// Token: 0x04000F2D RID: 3885
		protected bool _proofExplosion;

		// Token: 0x04000F2E RID: 3886
		protected bool _isUnpickupable;

		// Token: 0x04000F2F RID: 3887
		protected bool _isSalvageable;

		// Token: 0x04000F30 RID: 3888
		protected bool _isSaveable;
	}
}
