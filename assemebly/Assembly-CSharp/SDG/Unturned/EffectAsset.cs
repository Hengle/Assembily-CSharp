using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003B5 RID: 949
	public class EffectAsset : Asset
	{
		// Token: 0x060019E2 RID: 6626 RVA: 0x000916F0 File Offset: 0x0008FAF0
		public EffectAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (id < 200 && !bundle.hasResource && !data.has("Bypass_ID_Limit"))
			{
				throw new NotSupportedException("ID < 200");
			}
			this._effect = (GameObject)bundle.load("Effect");
			if (this.effect == null)
			{
				throw new NotSupportedException("Missing effect gameobject");
			}
			this._gore = data.has("Gore");
			this._splatters = new GameObject[(int)data.readByte("Splatter")];
			for (int i = 0; i < this.splatters.Length; i++)
			{
				this.splatters[i] = (GameObject)bundle.load("Splatter_" + i);
			}
			this._splatter = data.readByte("Splatters");
			this._splatterLiquid = data.has("Splatter_Liquid");
			if (data.has("Splatter_Temperature"))
			{
				this._splatterTemperature = (EPlayerTemperature)Enum.Parse(typeof(EPlayerTemperature), data.readString("Splatter_Temperature"), true);
			}
			else
			{
				this._splatterTemperature = EPlayerTemperature.NONE;
			}
			this._splatterLifetime = data.readSingle("Splatter_Lifetime");
			if (data.has("Splatter_Lifetime_Spread"))
			{
				this._splatterLifetimeSpread = data.readSingle("Splatter_Lifetime_Spread");
			}
			else
			{
				this._splatterLifetimeSpread = 1f;
			}
			this._lifetime = data.readSingle("Lifetime");
			if (data.has("Lifetime_Spread"))
			{
				this._lifetimeSpread = data.readSingle("Lifetime_Spread");
			}
			else
			{
				this._lifetimeSpread = 4f;
			}
			this._isStatic = data.has("Static");
			if (data.has("Preload"))
			{
				this._preload = data.readByte("Preload");
			}
			else
			{
				this._preload = 1;
			}
			if (data.has("Splatter_Preload"))
			{
				this._splatterPreload = data.readByte("Splatter_Preload");
			}
			else
			{
				this._splatterPreload = (byte)(Mathf.CeilToInt((float)this.splatter / (float)this.splatters.Length) * (int)this.preload);
			}
			this._blast = data.readUInt16("Blast");
			this.spawnOnDedicatedServer = data.has("Spawn_On_Dedicated_Server");
			if (data.has("Randomize_Rotation"))
			{
				this.randomizeRotation = data.readBoolean("Randomize_Rotation");
			}
			else
			{
				this.randomizeRotation = true;
			}
			bundle.unload();
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x00091994 File Offset: 0x0008FD94
		public GameObject effect
		{
			get
			{
				return this._effect;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x0009199C File Offset: 0x0008FD9C
		public GameObject[] splatters
		{
			get
			{
				return this._splatters;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x000919A4 File Offset: 0x0008FDA4
		public bool gore
		{
			get
			{
				return this._gore;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x000919AC File Offset: 0x0008FDAC
		public byte splatter
		{
			get
			{
				return this._splatter;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x000919B4 File Offset: 0x0008FDB4
		public float splatterLifetime
		{
			get
			{
				return this._splatterLifetime;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x000919BC File Offset: 0x0008FDBC
		public float splatterLifetimeSpread
		{
			get
			{
				return this._splatterLifetimeSpread;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x000919C4 File Offset: 0x0008FDC4
		public bool splatterLiquid
		{
			get
			{
				return this._splatterLiquid;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x000919CC File Offset: 0x0008FDCC
		public EPlayerTemperature splatterTemperature
		{
			get
			{
				return this._splatterTemperature;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x000919D4 File Offset: 0x0008FDD4
		public byte splatterPreload
		{
			get
			{
				return this._splatterPreload;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x000919DC File Offset: 0x0008FDDC
		public float lifetime
		{
			get
			{
				return this._lifetime;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x000919E4 File Offset: 0x0008FDE4
		public float lifetimeSpread
		{
			get
			{
				return this._lifetimeSpread;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x000919EC File Offset: 0x0008FDEC
		public bool isStatic
		{
			get
			{
				return this._isStatic;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x000919F4 File Offset: 0x0008FDF4
		public byte preload
		{
			get
			{
				return this._preload;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x000919FC File Offset: 0x0008FDFC
		public ushort blast
		{
			get
			{
				return this._blast;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00091A04 File Offset: 0x0008FE04
		// (set) Token: 0x060019F2 RID: 6642 RVA: 0x00091A0C File Offset: 0x0008FE0C
		public bool spawnOnDedicatedServer { get; protected set; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x00091A15 File Offset: 0x0008FE15
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x00091A1D File Offset: 0x0008FE1D
		public bool randomizeRotation { get; protected set; }

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x00091A26 File Offset: 0x0008FE26
		public override EAssetType assetCategory
		{
			get
			{
				return EAssetType.EFFECT;
			}
		}

		// Token: 0x04000ED9 RID: 3801
		protected GameObject _effect;

		// Token: 0x04000EDA RID: 3802
		protected GameObject[] _splatters;

		// Token: 0x04000EDB RID: 3803
		private bool _gore;

		// Token: 0x04000EDC RID: 3804
		private byte _splatter;

		// Token: 0x04000EDD RID: 3805
		private float _splatterLifetime;

		// Token: 0x04000EDE RID: 3806
		private float _splatterLifetimeSpread;

		// Token: 0x04000EDF RID: 3807
		private bool _splatterLiquid;

		// Token: 0x04000EE0 RID: 3808
		private EPlayerTemperature _splatterTemperature;

		// Token: 0x04000EE1 RID: 3809
		private byte _splatterPreload;

		// Token: 0x04000EE2 RID: 3810
		private float _lifetime;

		// Token: 0x04000EE3 RID: 3811
		private float _lifetimeSpread;

		// Token: 0x04000EE4 RID: 3812
		private bool _isStatic;

		// Token: 0x04000EE5 RID: 3813
		private byte _preload;

		// Token: 0x04000EE6 RID: 3814
		private ushort _blast;
	}
}
