using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000382 RID: 898
	public class Attachments : MonoBehaviour
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0008BA34 File Offset: 0x00089E34
		public ItemGunAsset gunAsset
		{
			get
			{
				return this._gunAsset;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x0008BA3C File Offset: 0x00089E3C
		public SkinAsset skinAsset
		{
			get
			{
				return this._skinAsset;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0008BA44 File Offset: 0x00089E44
		public ushort sightID
		{
			get
			{
				return this._sightID;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x0008BA4C File Offset: 0x00089E4C
		public ushort tacticalID
		{
			get
			{
				return this._tacticalID;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0008BA54 File Offset: 0x00089E54
		public ushort gripID
		{
			get
			{
				return this._gripID;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x0008BA5C File Offset: 0x00089E5C
		public ushort barrelID
		{
			get
			{
				return this._barrelID;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x0008BA64 File Offset: 0x00089E64
		public ushort magazineID
		{
			get
			{
				return this._magazineID;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x0008BA6C File Offset: 0x00089E6C
		public ItemSightAsset sightAsset
		{
			get
			{
				return this._sightAsset;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x0008BA74 File Offset: 0x00089E74
		public ItemTacticalAsset tacticalAsset
		{
			get
			{
				return this._tacticalAsset;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x0008BA7C File Offset: 0x00089E7C
		public ItemGripAsset gripAsset
		{
			get
			{
				return this._gripAsset;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x0008BA84 File Offset: 0x00089E84
		public ItemBarrelAsset barrelAsset
		{
			get
			{
				return this._barrelAsset;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0008BA8C File Offset: 0x00089E8C
		public ItemMagazineAsset magazineAsset
		{
			get
			{
				return this._magazineAsset;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x0008BA94 File Offset: 0x00089E94
		public Transform sightModel
		{
			get
			{
				return this._sightModel;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x0008BA9C File Offset: 0x00089E9C
		public Transform tacticalModel
		{
			get
			{
				return this._tacticalModel;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x0008BAA4 File Offset: 0x00089EA4
		public Transform gripModel
		{
			get
			{
				return this._gripModel;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x0008BAAC File Offset: 0x00089EAC
		public Transform barrelModel
		{
			get
			{
				return this._barrelModel;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x0008BAB4 File Offset: 0x00089EB4
		public Transform magazineModel
		{
			get
			{
				return this._magazineModel;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x0008BABC File Offset: 0x00089EBC
		public Transform sightHook
		{
			get
			{
				return this._sightHook;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x0008BAC4 File Offset: 0x00089EC4
		public Transform viewHook
		{
			get
			{
				return this._viewHook;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x0008BACC File Offset: 0x00089ECC
		public Transform tacticalHook
		{
			get
			{
				return this._tacticalHook;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x0008BAD4 File Offset: 0x00089ED4
		public Transform gripHook
		{
			get
			{
				return this._gripHook;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x0008BADC File Offset: 0x00089EDC
		public Transform barrelHook
		{
			get
			{
				return this._barrelHook;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x0008BAE4 File Offset: 0x00089EE4
		public Transform magazineHook
		{
			get
			{
				return this._magazineHook;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x0008BAEC File Offset: 0x00089EEC
		public Transform ejectHook
		{
			get
			{
				return this._ejectHook;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x0008BAF4 File Offset: 0x00089EF4
		public Transform lightHook
		{
			get
			{
				return this._lightHook;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x0008BAFC File Offset: 0x00089EFC
		public Transform light2Hook
		{
			get
			{
				return this._light2Hook;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x0008BB04 File Offset: 0x00089F04
		public Transform aimHook
		{
			get
			{
				return this._aimHook;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x0008BB0C File Offset: 0x00089F0C
		public Transform scopeHook
		{
			get
			{
				return this._scopeHook;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x0008BB14 File Offset: 0x00089F14
		public Transform reticuleHook
		{
			get
			{
				return this._reticuleHook;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x0008BB1C File Offset: 0x00089F1C
		public Transform leftHook
		{
			get
			{
				return this._leftHook;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x0008BB24 File Offset: 0x00089F24
		public Transform rightHook
		{
			get
			{
				return this._rightHook;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x0008BB2C File Offset: 0x00089F2C
		public Transform nockHook
		{
			get
			{
				return this._nockHook;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x0008BB34 File Offset: 0x00089F34
		public Transform restHook
		{
			get
			{
				return this._restHook;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x0008BB3C File Offset: 0x00089F3C
		public LineRenderer rope
		{
			get
			{
				return this._rope;
			}
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0008BB44 File Offset: 0x00089F44
		public void applyVisual()
		{
			if (this.isSkinned != this.wasSkinned)
			{
				this.wasSkinned = this.isSkinned;
				if (this.tempSightMaterial != null)
				{
					HighlighterTool.rematerialize(this.sightModel, this.tempSightMaterial, out this.tempSightMaterial);
				}
				if (this.tempTacticalMaterial != null)
				{
					HighlighterTool.rematerialize(this.tacticalModel, this.tempTacticalMaterial, out this.tempTacticalMaterial);
				}
				if (this.tempGripMaterial != null)
				{
					HighlighterTool.rematerialize(this.gripModel, this.tempGripMaterial, out this.tempGripMaterial);
				}
				if (this.tempBarrelMaterial != null)
				{
					HighlighterTool.rematerialize(this.barrelModel, this.tempBarrelMaterial, out this.tempBarrelMaterial);
				}
				if (this.tempMagazineMaterial != null)
				{
					HighlighterTool.rematerialize(this.magazineModel, this.tempMagazineMaterial, out this.tempMagazineMaterial);
				}
			}
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0008BC36 File Offset: 0x0008A036
		public void updateGun(ItemGunAsset newGunAsset, SkinAsset newSkinAsset)
		{
			this._gunAsset = newGunAsset;
			this._skinAsset = newSkinAsset;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0008BC48 File Offset: 0x0008A048
		public void updateAttachments(byte[] state, bool viewmodel)
		{
			if (state == null || state.Length != 18)
			{
				return;
			}
			base.transform.localScale = Vector3.one;
			this._sightID = BitConverter.ToUInt16(state, 0);
			this._tacticalID = BitConverter.ToUInt16(state, 2);
			this._gripID = BitConverter.ToUInt16(state, 4);
			this._barrelID = BitConverter.ToUInt16(state, 6);
			this._magazineID = BitConverter.ToUInt16(state, 8);
			if (this.sightModel != null)
			{
				UnityEngine.Object.Destroy(this.sightModel.gameObject);
				this._sightModel = null;
			}
			try
			{
				this._sightAsset = (ItemSightAsset)Assets.find(EAssetType.ITEM, this.sightID);
			}
			catch
			{
				this._sightAsset = null;
			}
			this.tempSightMaterial = null;
			if (this.sightAsset != null && this.sightHook != null)
			{
				this._sightModel = UnityEngine.Object.Instantiate<GameObject>(this.sightAsset.sight).transform;
				this.sightModel.name = "Sight";
				this.sightModel.transform.parent = this.sightHook;
				this.sightModel.transform.localPosition = Vector3.zero;
				this.sightModel.transform.localRotation = Quaternion.identity;
				this.sightModel.localScale = Vector3.one;
				if (viewmodel)
				{
					Layerer.viewmodel(this.sightModel);
				}
				if (this.gunAsset != null && this.skinAsset != null && this.skinAsset.secondarySkins != null)
				{
					Material material = null;
					if (!this.skinAsset.secondarySkins.TryGetValue(this.sightID, out material) && this.skinAsset.hasSight && this.sightAsset.isPaintable)
					{
						if (this.sightAsset.albedoBase != null && this.skinAsset.attachmentSkin != null)
						{
							material = UnityEngine.Object.Instantiate<Material>(this.skinAsset.attachmentSkin);
							material.SetTexture("_AlbedoBase", this.sightAsset.albedoBase);
							material.SetTexture("_MetallicBase", this.sightAsset.metallicBase);
							material.SetTexture("_EmissionBase", this.sightAsset.emissionBase);
						}
						else if (this.skinAsset.tertiarySkin != null)
						{
							material = this.skinAsset.tertiarySkin;
						}
					}
					if (material != null)
					{
						HighlighterTool.rematerialize(this.sightModel, material, out this.tempSightMaterial);
					}
				}
			}
			if (this.tacticalModel != null)
			{
				UnityEngine.Object.Destroy(this.tacticalModel.gameObject);
				this._tacticalModel = null;
			}
			try
			{
				this._tacticalAsset = (ItemTacticalAsset)Assets.find(EAssetType.ITEM, this.tacticalID);
			}
			catch
			{
				this._tacticalAsset = null;
			}
			this.tempTacticalMaterial = null;
			if (this.tacticalAsset != null && this.tacticalHook != null)
			{
				this._tacticalModel = UnityEngine.Object.Instantiate<GameObject>(this.tacticalAsset.tactical).transform;
				this.tacticalModel.name = "Tactical";
				this.tacticalModel.transform.parent = this.tacticalHook;
				this.tacticalModel.transform.localPosition = Vector3.zero;
				this.tacticalModel.transform.localRotation = Quaternion.identity;
				this.tacticalModel.localScale = Vector3.one;
				if (viewmodel)
				{
					Layerer.viewmodel(this.tacticalModel);
				}
				if (this.gunAsset != null && this.skinAsset != null && this.skinAsset.secondarySkins != null)
				{
					Material material2 = null;
					if (!this.skinAsset.secondarySkins.TryGetValue(this.tacticalID, out material2) && this.skinAsset.hasTactical && this.tacticalAsset.isPaintable)
					{
						if (this.tacticalAsset.albedoBase != null && this.skinAsset.attachmentSkin != null)
						{
							material2 = UnityEngine.Object.Instantiate<Material>(this.skinAsset.attachmentSkin);
							material2.SetTexture("_AlbedoBase", this.tacticalAsset.albedoBase);
							material2.SetTexture("_MetallicBase", this.tacticalAsset.metallicBase);
							material2.SetTexture("_EmissionBase", this.tacticalAsset.emissionBase);
						}
						else if (this.skinAsset.tertiarySkin != null)
						{
							material2 = this.skinAsset.tertiarySkin;
						}
					}
					if (material2 != null)
					{
						HighlighterTool.rematerialize(this.tacticalModel, material2, out this.tempTacticalMaterial);
					}
				}
			}
			if (this.gripModel != null)
			{
				UnityEngine.Object.Destroy(this.gripModel.gameObject);
				this._gripModel = null;
			}
			try
			{
				this._gripAsset = (ItemGripAsset)Assets.find(EAssetType.ITEM, this.gripID);
			}
			catch
			{
				this._gripAsset = null;
			}
			this.tempGripMaterial = null;
			if (this.gripAsset != null && this.gripHook != null)
			{
				this._gripModel = UnityEngine.Object.Instantiate<GameObject>(this.gripAsset.grip).transform;
				this.gripModel.name = "Grip";
				this.gripModel.transform.parent = this.gripHook;
				this.gripModel.transform.localPosition = Vector3.zero;
				this.gripModel.transform.localRotation = Quaternion.identity;
				this.gripModel.localScale = Vector3.one;
				if (viewmodel)
				{
					Layerer.viewmodel(this.gripModel);
				}
				if (this.gunAsset != null && this.skinAsset != null && this.skinAsset.secondarySkins != null)
				{
					Material material3 = null;
					if (!this.skinAsset.secondarySkins.TryGetValue(this.gripID, out material3) && this.skinAsset.hasGrip && this.gripAsset.isPaintable)
					{
						if (this.gripAsset.albedoBase != null && this.skinAsset.attachmentSkin != null)
						{
							material3 = UnityEngine.Object.Instantiate<Material>(this.skinAsset.attachmentSkin);
							material3.SetTexture("_AlbedoBase", this.gripAsset.albedoBase);
							material3.SetTexture("_MetallicBase", this.gripAsset.metallicBase);
							material3.SetTexture("_EmissionBase", this.gripAsset.emissionBase);
						}
						else if (this.skinAsset.tertiarySkin != null)
						{
							material3 = this.skinAsset.tertiarySkin;
						}
					}
					if (material3 != null)
					{
						HighlighterTool.rematerialize(this.gripModel, material3, out this.tempGripMaterial);
					}
				}
			}
			if (this.barrelModel != null)
			{
				UnityEngine.Object.Destroy(this.barrelModel.gameObject);
				this._barrelModel = null;
			}
			try
			{
				this._barrelAsset = (ItemBarrelAsset)Assets.find(EAssetType.ITEM, this.barrelID);
			}
			catch
			{
				this._barrelAsset = null;
			}
			this.tempBarrelMaterial = null;
			if (this.barrelAsset != null && this.barrelHook != null)
			{
				this._barrelModel = UnityEngine.Object.Instantiate<GameObject>(this.barrelAsset.barrel).transform;
				this.barrelModel.name = "Barrel";
				this.barrelModel.transform.parent = this.barrelHook;
				this.barrelModel.transform.localPosition = Vector3.zero;
				this.barrelModel.transform.localRotation = Quaternion.identity;
				this.barrelModel.localScale = Vector3.one;
				if (viewmodel)
				{
					Layerer.viewmodel(this.barrelModel);
				}
				if (this.gunAsset != null && this.skinAsset != null && this.skinAsset.secondarySkins != null)
				{
					Material material4 = null;
					if (!this.skinAsset.secondarySkins.TryGetValue(this.barrelID, out material4) && this.skinAsset.hasBarrel && this.barrelAsset.isPaintable)
					{
						if (this.barrelAsset.albedoBase != null && this.skinAsset.attachmentSkin != null)
						{
							material4 = UnityEngine.Object.Instantiate<Material>(this.skinAsset.attachmentSkin);
							material4.SetTexture("_AlbedoBase", this.barrelAsset.albedoBase);
							material4.SetTexture("_MetallicBase", this.barrelAsset.metallicBase);
							material4.SetTexture("_EmissionBase", this.barrelAsset.emissionBase);
						}
						else if (this.skinAsset.tertiarySkin != null)
						{
							material4 = this.skinAsset.tertiarySkin;
						}
					}
					if (material4 != null)
					{
						HighlighterTool.rematerialize(this.barrelModel, material4, out this.tempBarrelMaterial);
					}
				}
			}
			if (this.magazineModel != null)
			{
				UnityEngine.Object.Destroy(this.magazineModel.gameObject);
				this._magazineModel = null;
			}
			try
			{
				this._magazineAsset = (ItemMagazineAsset)Assets.find(EAssetType.ITEM, this.magazineID);
			}
			catch
			{
				this._magazineAsset = null;
			}
			this.tempMagazineMaterial = null;
			if (this.magazineAsset != null && this.magazineHook != null)
			{
				Transform transform = null;
				if (this.magazineAsset.calibers.Length > 0)
				{
					transform = this.magazineHook.FindChild("Caliber_" + this.magazineAsset.calibers[0]);
				}
				if (transform == null)
				{
					transform = this.magazineHook;
				}
				this._magazineModel = UnityEngine.Object.Instantiate<GameObject>(this.magazineAsset.magazine).transform;
				this.magazineModel.name = "Magazine";
				this.magazineModel.transform.parent = transform;
				this.magazineModel.transform.localPosition = Vector3.zero;
				this.magazineModel.transform.localRotation = Quaternion.identity;
				this.magazineModel.localScale = Vector3.one;
				if (viewmodel)
				{
					Layerer.viewmodel(this.magazineModel);
				}
				if (this.gunAsset != null && this.skinAsset != null && this.skinAsset.secondarySkins != null)
				{
					Material material5 = null;
					if (!this.skinAsset.secondarySkins.TryGetValue(this.magazineID, out material5) && this.skinAsset.hasMagazine && this.magazineAsset.isPaintable)
					{
						if (this.magazineAsset.albedoBase != null && this.skinAsset.attachmentSkin != null)
						{
							material5 = UnityEngine.Object.Instantiate<Material>(this.skinAsset.attachmentSkin);
							material5.SetTexture("_AlbedoBase", this.magazineAsset.albedoBase);
							material5.SetTexture("_MetallicBase", this.magazineAsset.metallicBase);
							material5.SetTexture("_EmissionBase", this.magazineAsset.emissionBase);
						}
						else if (this.skinAsset.tertiarySkin != null)
						{
							material5 = this.skinAsset.tertiarySkin;
						}
					}
					if (material5 != null)
					{
						HighlighterTool.rematerialize(this.magazineModel, material5, out this.tempMagazineMaterial);
					}
				}
			}
			if (this.tacticalModel != null && this.tacticalModel.childCount > 0)
			{
				this._lightHook = this.tacticalModel.transform.FindChild("Model_0").FindChild("Light");
				this._light2Hook = this.tacticalModel.transform.FindChild("Model_0").FindChild("Light2");
				if (viewmodel)
				{
					if (this.lightHook != null)
					{
						this.lightHook.tag = "Viewmodel";
						this.lightHook.gameObject.layer = LayerMasks.VIEWMODEL;
						Transform transform2 = this.lightHook.FindChild("Light");
						if (transform2 != null)
						{
							transform2.tag = "Viewmodel";
							transform2.gameObject.layer = LayerMasks.VIEWMODEL;
						}
					}
					if (this.light2Hook != null)
					{
						this.light2Hook.tag = "Viewmodel";
						this.light2Hook.gameObject.layer = LayerMasks.VIEWMODEL;
						Transform transform3 = this.light2Hook.FindChild("Light");
						if (transform3 != null)
						{
							transform3.tag = "Viewmodel";
							transform3.gameObject.layer = LayerMasks.VIEWMODEL;
						}
					}
				}
				else
				{
					LightLODTool.applyLightLOD(this.lightHook);
					LightLODTool.applyLightLOD(this.light2Hook);
				}
			}
			else
			{
				this._lightHook = null;
				this._light2Hook = null;
			}
			if (this.sightModel != null)
			{
				this._aimHook = this.sightModel.transform.FindChild("Model_0").FindChild("Aim");
				if (this.aimHook != null)
				{
					Transform transform4 = this.aimHook.parent.FindChild("Reticule");
					if (transform4 != null)
					{
						Renderer component = transform4.GetComponent<Renderer>();
						Material material6 = component.material;
						material6.SetColor("_Color", OptionsSettings.criticalHitmarkerColor);
						material6.SetColor("_EmissionColor", OptionsSettings.criticalHitmarkerColor);
					}
				}
				this._reticuleHook = this.sightModel.transform.FindChild("Model_0").FindChild("Reticule");
			}
			else
			{
				this._aimHook = null;
				this._reticuleHook = null;
			}
			if (this.aimHook != null)
			{
				this._scopeHook = this.aimHook.FindChild("Scope");
			}
			else
			{
				this._scopeHook = null;
			}
			if (this.rope != null && viewmodel)
			{
				this.rope.tag = "Viewmodel";
				this.rope.gameObject.layer = LayerMasks.VIEWMODEL;
			}
			this.wasSkinned = true;
			this.applyVisual();
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0008CAF4 File Offset: 0x0008AEF4
		private void Awake()
		{
			this._sightHook = base.transform.FindChild("Sight");
			this._viewHook = base.transform.FindChild("View");
			this._tacticalHook = base.transform.FindChild("Tactical");
			this._gripHook = base.transform.FindChild("Grip");
			this._barrelHook = base.transform.FindChild("Barrel");
			this._magazineHook = base.transform.FindChild("Magazine");
			this._ejectHook = base.transform.FindChild("Eject");
			this._leftHook = base.transform.FindChild("Left");
			this._rightHook = base.transform.FindChild("Right");
			this._nockHook = base.transform.FindChild("Nock");
			this._restHook = base.transform.FindChild("Rest");
			Transform transform = base.transform.FindChild("Rope");
			if (transform != null)
			{
				this._rope = (LineRenderer)transform.GetComponent<Renderer>();
			}
		}

		// Token: 0x04000D3E RID: 3390
		private ItemGunAsset _gunAsset;

		// Token: 0x04000D3F RID: 3391
		private SkinAsset _skinAsset;

		// Token: 0x04000D40 RID: 3392
		private ushort _sightID;

		// Token: 0x04000D41 RID: 3393
		private ushort _tacticalID;

		// Token: 0x04000D42 RID: 3394
		private ushort _gripID;

		// Token: 0x04000D43 RID: 3395
		private ushort _barrelID;

		// Token: 0x04000D44 RID: 3396
		private ushort _magazineID;

		// Token: 0x04000D45 RID: 3397
		private ItemSightAsset _sightAsset;

		// Token: 0x04000D46 RID: 3398
		private ItemTacticalAsset _tacticalAsset;

		// Token: 0x04000D47 RID: 3399
		private ItemGripAsset _gripAsset;

		// Token: 0x04000D48 RID: 3400
		private ItemBarrelAsset _barrelAsset;

		// Token: 0x04000D49 RID: 3401
		private ItemMagazineAsset _magazineAsset;

		// Token: 0x04000D4A RID: 3402
		private Transform _sightModel;

		// Token: 0x04000D4B RID: 3403
		private Transform _tacticalModel;

		// Token: 0x04000D4C RID: 3404
		private Transform _gripModel;

		// Token: 0x04000D4D RID: 3405
		private Transform _barrelModel;

		// Token: 0x04000D4E RID: 3406
		private Transform _magazineModel;

		// Token: 0x04000D4F RID: 3407
		private Transform _sightHook;

		// Token: 0x04000D50 RID: 3408
		private Transform _viewHook;

		// Token: 0x04000D51 RID: 3409
		private Transform _tacticalHook;

		// Token: 0x04000D52 RID: 3410
		private Transform _gripHook;

		// Token: 0x04000D53 RID: 3411
		private Transform _barrelHook;

		// Token: 0x04000D54 RID: 3412
		private Transform _magazineHook;

		// Token: 0x04000D55 RID: 3413
		private Transform _ejectHook;

		// Token: 0x04000D56 RID: 3414
		private Transform _lightHook;

		// Token: 0x04000D57 RID: 3415
		private Transform _light2Hook;

		// Token: 0x04000D58 RID: 3416
		private Transform _aimHook;

		// Token: 0x04000D59 RID: 3417
		private Transform _scopeHook;

		// Token: 0x04000D5A RID: 3418
		private Transform _reticuleHook;

		// Token: 0x04000D5B RID: 3419
		private Transform _leftHook;

		// Token: 0x04000D5C RID: 3420
		private Transform _rightHook;

		// Token: 0x04000D5D RID: 3421
		private Transform _nockHook;

		// Token: 0x04000D5E RID: 3422
		private Transform _restHook;

		// Token: 0x04000D5F RID: 3423
		private LineRenderer _rope;

		// Token: 0x04000D60 RID: 3424
		public bool isSkinned;

		// Token: 0x04000D61 RID: 3425
		private bool wasSkinned;

		// Token: 0x04000D62 RID: 3426
		private Material tempSightMaterial;

		// Token: 0x04000D63 RID: 3427
		private Material tempTacticalMaterial;

		// Token: 0x04000D64 RID: 3428
		private Material tempGripMaterial;

		// Token: 0x04000D65 RID: 3429
		private Material tempBarrelMaterial;

		// Token: 0x04000D66 RID: 3430
		private Material tempMagazineMaterial;
	}
}
