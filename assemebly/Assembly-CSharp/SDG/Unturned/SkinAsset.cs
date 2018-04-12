using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200041F RID: 1055
	public class SkinAsset : Asset
	{
		// Token: 0x06001CA1 RID: 7329 RVA: 0x0009BA98 File Offset: 0x00099E98
		public SkinAsset(bool isPattern, Material primarySkin, Dictionary<ushort, Material> secondarySkins, Material attachmentSkin, Material tertiarySkin)
		{
			this._isPattern = isPattern;
			this._hasSight = true;
			this._hasTactical = true;
			this._hasGrip = true;
			this._hasBarrel = true;
			this._hasMagazine = true;
			this._primarySkin = primarySkin;
			this._secondarySkins = secondarySkins;
			this._attachmentSkin = attachmentSkin;
			this._tertiarySkin = tertiarySkin;
			this.overrideMeshes = new List<Mesh>(0);
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0009BB00 File Offset: 0x00099F00
		public SkinAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (id < 2000 && !bundle.hasResource && !data.has("Bypass_ID_Limit"))
			{
				throw new NotSupportedException("ID < 2000");
			}
			this._isPattern = data.has("Pattern");
			this._hasSight = data.has("Sight");
			this._hasTactical = data.has("Tactical");
			this._hasGrip = data.has("Grip");
			this._hasBarrel = data.has("Barrel");
			this._hasMagazine = data.has("Magazine");
			if (!Dedicator.isDedicated)
			{
				this._primarySkin = (Material)bundle.load("Skin_Primary");
				this._secondarySkins = new Dictionary<ushort, Material>();
				ushort num = data.readUInt16("Secondary_Skins");
				for (ushort num2 = 0; num2 < num; num2 += 1)
				{
					ushort num3 = data.readUInt16("Secondary_" + num2);
					if (!this.secondarySkins.ContainsKey(num3))
					{
						this.secondarySkins.Add(num3, (Material)bundle.load("Skin_Secondary_" + num3));
					}
				}
				this._attachmentSkin = (Material)bundle.load("Skin_Attachment");
				this._tertiarySkin = (Material)bundle.load("Skin_Tertiary");
				ushort num4 = data.readUInt16("Override_Meshes");
				this.overrideMeshes = new List<Mesh>((int)num4);
				for (ushort num5 = 0; num5 < num4; num5 += 1)
				{
					this.overrideMeshes.Add(((GameObject)bundle.load("Override_Mesh_" + num5)).GetComponent<MeshFilter>().sharedMesh);
				}
			}
			bundle.unload();
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x0009BCDB File Offset: 0x0009A0DB
		public bool isPattern
		{
			get
			{
				return this._isPattern;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x0009BCE3 File Offset: 0x0009A0E3
		public bool hasSight
		{
			get
			{
				return this._hasSight;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x0009BCEB File Offset: 0x0009A0EB
		public bool hasTactical
		{
			get
			{
				return this._hasTactical;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x0009BCF3 File Offset: 0x0009A0F3
		public bool hasGrip
		{
			get
			{
				return this._hasGrip;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x0009BCFB File Offset: 0x0009A0FB
		public bool hasBarrel
		{
			get
			{
				return this._hasBarrel;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x0009BD03 File Offset: 0x0009A103
		public bool hasMagazine
		{
			get
			{
				return this._hasMagazine;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x0009BD0B File Offset: 0x0009A10B
		public Material primarySkin
		{
			get
			{
				return this._primarySkin;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x0009BD13 File Offset: 0x0009A113
		public Dictionary<ushort, Material> secondarySkins
		{
			get
			{
				return this._secondarySkins;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x0009BD1B File Offset: 0x0009A11B
		public Material attachmentSkin
		{
			get
			{
				return this._attachmentSkin;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x0009BD23 File Offset: 0x0009A123
		public Material tertiarySkin
		{
			get
			{
				return this._tertiarySkin;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001CAD RID: 7341 RVA: 0x0009BD2B File Offset: 0x0009A12B
		public override EAssetType assetCategory
		{
			get
			{
				return EAssetType.SKIN;
			}
		}

		// Token: 0x040010ED RID: 4333
		protected bool _isPattern;

		// Token: 0x040010EE RID: 4334
		protected bool _hasSight;

		// Token: 0x040010EF RID: 4335
		protected bool _hasTactical;

		// Token: 0x040010F0 RID: 4336
		protected bool _hasGrip;

		// Token: 0x040010F1 RID: 4337
		protected bool _hasBarrel;

		// Token: 0x040010F2 RID: 4338
		protected bool _hasMagazine;

		// Token: 0x040010F3 RID: 4339
		protected Material _primarySkin;

		// Token: 0x040010F4 RID: 4340
		protected Dictionary<ushort, Material> _secondarySkins;

		// Token: 0x040010F5 RID: 4341
		protected Material _attachmentSkin;

		// Token: 0x040010F6 RID: 4342
		protected Material _tertiarySkin;

		// Token: 0x040010F7 RID: 4343
		public List<Mesh> overrideMeshes;
	}
}
