using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000434 RID: 1076
	public class HumanClothes : MonoBehaviour
	{
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x0009E1E1 File Offset: 0x0009C5E1
		// (set) Token: 0x06001D70 RID: 7536 RVA: 0x0009E1E9 File Offset: 0x0009C5E9
		public Transform hatModel { get; private set; }

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x0009E1F2 File Offset: 0x0009C5F2
		// (set) Token: 0x06001D72 RID: 7538 RVA: 0x0009E1FA File Offset: 0x0009C5FA
		public Transform backpackModel { get; private set; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001D73 RID: 7539 RVA: 0x0009E203 File Offset: 0x0009C603
		// (set) Token: 0x06001D74 RID: 7540 RVA: 0x0009E20B File Offset: 0x0009C60B
		public Transform vestModel { get; private set; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001D75 RID: 7541 RVA: 0x0009E214 File Offset: 0x0009C614
		// (set) Token: 0x06001D76 RID: 7542 RVA: 0x0009E21C File Offset: 0x0009C61C
		public Transform maskModel { get; private set; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001D77 RID: 7543 RVA: 0x0009E225 File Offset: 0x0009C625
		// (set) Token: 0x06001D78 RID: 7544 RVA: 0x0009E22D File Offset: 0x0009C62D
		public Transform glassesModel { get; private set; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x0009E236 File Offset: 0x0009C636
		// (set) Token: 0x06001D7A RID: 7546 RVA: 0x0009E23E File Offset: 0x0009C63E
		public Transform hairModel { get; private set; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001D7B RID: 7547 RVA: 0x0009E247 File Offset: 0x0009C647
		// (set) Token: 0x06001D7C RID: 7548 RVA: 0x0009E24F File Offset: 0x0009C64F
		public Transform beardModel { get; private set; }

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x0009E258 File Offset: 0x0009C658
		// (set) Token: 0x06001D7E RID: 7550 RVA: 0x0009E260 File Offset: 0x0009C660
		public bool isVisual
		{
			get
			{
				return this._isVisual;
			}
			set
			{
				if (this.isVisual != value)
				{
					this._isVisual = value;
					this.updateAll(true);
				}
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x0009E27C File Offset: 0x0009C67C
		// (set) Token: 0x06001D80 RID: 7552 RVA: 0x0009E284 File Offset: 0x0009C684
		public bool isMythic
		{
			get
			{
				return this._isMythic;
			}
			set
			{
				if (this.isMythic != value)
				{
					this._isMythic = value;
					this.updateAll(true);
				}
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x0009E2A0 File Offset: 0x0009C6A0
		// (set) Token: 0x06001D82 RID: 7554 RVA: 0x0009E2A8 File Offset: 0x0009C6A8
		public bool hasBackpack
		{
			get
			{
				return this._hasBackpack;
			}
			set
			{
				if (value != this._hasBackpack)
				{
					this._hasBackpack = value;
					if (this.backpackModel != null)
					{
						this.backpackModel.gameObject.SetActive(this.hasBackpack);
					}
				}
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x0009E2E4 File Offset: 0x0009C6E4
		// (set) Token: 0x06001D84 RID: 7556 RVA: 0x0009E2EC File Offset: 0x0009C6EC
		public int visualShirt
		{
			get
			{
				return this._visualShirt;
			}
			set
			{
				if (this.visualShirt != value)
				{
					this._visualShirt = value;
					if (!Dedicator.isDedicated)
					{
						if (this.visualShirt != 0)
						{
							try
							{
								this.visualShirtAsset = (ItemShirtAsset)Assets.find(EAssetType.ITEM, Provider.provider.economyService.getInventoryItemID(this.visualShirt));
							}
							catch
							{
								this.visualShirtAsset = null;
							}
							if (this.visualShirtAsset != null && !this.visualShirtAsset.isPro)
							{
								this._visualShirt = 0;
								this.visualShirtAsset = null;
							}
						}
						else
						{
							this.visualShirtAsset = null;
						}
						this.needsShirtUpdate = true;
					}
				}
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x0009E3A8 File Offset: 0x0009C7A8
		// (set) Token: 0x06001D86 RID: 7558 RVA: 0x0009E3B0 File Offset: 0x0009C7B0
		public int visualPants
		{
			get
			{
				return this._visualPants;
			}
			set
			{
				if (this.visualPants != value)
				{
					this._visualPants = value;
					if (!Dedicator.isDedicated)
					{
						if (this.visualPants != 0)
						{
							try
							{
								this.visualPantsAsset = (ItemPantsAsset)Assets.find(EAssetType.ITEM, Provider.provider.economyService.getInventoryItemID(this.visualPants));
							}
							catch
							{
								this.visualPantsAsset = null;
							}
							if (this.visualPantsAsset != null && !this.visualPantsAsset.isPro)
							{
								this._visualPants = 0;
								this.visualPantsAsset = null;
							}
						}
						else
						{
							this.visualPantsAsset = null;
						}
						this.needsPantsUpdate = true;
					}
				}
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x0009E46C File Offset: 0x0009C86C
		// (set) Token: 0x06001D88 RID: 7560 RVA: 0x0009E474 File Offset: 0x0009C874
		public int visualHat
		{
			get
			{
				return this._visualHat;
			}
			set
			{
				if (this.visualHat != value)
				{
					this._visualHat = value;
					if (!Dedicator.isDedicated)
					{
						if (this.visualHat != 0)
						{
							try
							{
								this.visualHatAsset = (ItemHatAsset)Assets.find(EAssetType.ITEM, Provider.provider.economyService.getInventoryItemID(this.visualHat));
							}
							catch
							{
								this.visualHatAsset = null;
							}
							if (this.visualHatAsset != null && !this.visualHatAsset.isPro)
							{
								this._visualHat = 0;
								this.visualHatAsset = null;
							}
						}
						else
						{
							this.visualHatAsset = null;
						}
						this.needsHatUpdate = true;
					}
				}
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x0009E530 File Offset: 0x0009C930
		// (set) Token: 0x06001D8A RID: 7562 RVA: 0x0009E538 File Offset: 0x0009C938
		public int visualBackpack
		{
			get
			{
				return this._visualBackpack;
			}
			set
			{
				if (this.visualBackpack != value)
				{
					this._visualBackpack = value;
					if (!Dedicator.isDedicated)
					{
						if (this.visualBackpack != 0)
						{
							try
							{
								this.visualBackpackAsset = (ItemBackpackAsset)Assets.find(EAssetType.ITEM, Provider.provider.economyService.getInventoryItemID(this.visualBackpack));
							}
							catch
							{
								this.visualBackpackAsset = null;
							}
							if (this.visualBackpackAsset != null && !this.visualBackpackAsset.isPro)
							{
								this._visualBackpack = 0;
								this.visualBackpackAsset = null;
							}
						}
						else
						{
							this.visualBackpackAsset = null;
						}
						this.needsBackpackUpdate = true;
					}
				}
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x0009E5F4 File Offset: 0x0009C9F4
		// (set) Token: 0x06001D8C RID: 7564 RVA: 0x0009E5FC File Offset: 0x0009C9FC
		public int visualVest
		{
			get
			{
				return this._visualVest;
			}
			set
			{
				if (this.visualVest != value)
				{
					this._visualVest = value;
					if (!Dedicator.isDedicated)
					{
						if (this.visualVest != 0)
						{
							try
							{
								this.visualVestAsset = (ItemVestAsset)Assets.find(EAssetType.ITEM, Provider.provider.economyService.getInventoryItemID(this.visualVest));
							}
							catch
							{
								this.visualVestAsset = null;
							}
							if (this.visualVestAsset != null && !this.visualVestAsset.isPro)
							{
								this._visualVest = 0;
								this.visualVestAsset = null;
							}
						}
						else
						{
							this.visualVestAsset = null;
						}
						this.needsVestUpdate = true;
					}
				}
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x0009E6B8 File Offset: 0x0009CAB8
		// (set) Token: 0x06001D8E RID: 7566 RVA: 0x0009E6C0 File Offset: 0x0009CAC0
		public int visualMask
		{
			get
			{
				return this._visualMask;
			}
			set
			{
				if (this.visualMask != value)
				{
					this._visualMask = value;
					if (!Dedicator.isDedicated)
					{
						if (this.visualMask != 0)
						{
							try
							{
								this.visualMaskAsset = (ItemMaskAsset)Assets.find(EAssetType.ITEM, Provider.provider.economyService.getInventoryItemID(this.visualMask));
							}
							catch
							{
								this.visualMaskAsset = null;
							}
							if (this.visualMaskAsset != null && !this.visualMaskAsset.isPro)
							{
								this._visualMask = 0;
								this.visualMaskAsset = null;
							}
						}
						else
						{
							this.visualMaskAsset = null;
						}
						this.needsMaskUpdate = true;
					}
				}
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001D8F RID: 7567 RVA: 0x0009E77C File Offset: 0x0009CB7C
		// (set) Token: 0x06001D90 RID: 7568 RVA: 0x0009E784 File Offset: 0x0009CB84
		public int visualGlasses
		{
			get
			{
				return this._visualGlasses;
			}
			set
			{
				if (this.visualGlasses != value)
				{
					this._visualGlasses = value;
					if (!Dedicator.isDedicated)
					{
						if (this.visualGlasses != 0)
						{
							try
							{
								this.visualGlassesAsset = (ItemGlassesAsset)Assets.find(EAssetType.ITEM, Provider.provider.economyService.getInventoryItemID(this.visualGlasses));
							}
							catch
							{
								this.visualGlassesAsset = null;
							}
							if (this.visualGlassesAsset != null && !this.visualGlassesAsset.isPro)
							{
								this._visualGlasses = 0;
								this.visualGlassesAsset = null;
							}
						}
						else
						{
							this.visualGlassesAsset = null;
						}
						this.needsGlassesUpdate = true;
					}
				}
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x0009E840 File Offset: 0x0009CC40
		public ItemShirtAsset shirtAsset
		{
			get
			{
				return this._shirtAsset;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001D92 RID: 7570 RVA: 0x0009E848 File Offset: 0x0009CC48
		public ItemPantsAsset pantsAsset
		{
			get
			{
				return this._pantsAsset;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x0009E850 File Offset: 0x0009CC50
		public ItemHatAsset hatAsset
		{
			get
			{
				return this._hatAsset;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x0009E858 File Offset: 0x0009CC58
		public ItemBackpackAsset backpackAsset
		{
			get
			{
				return this._backpackAsset;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x0009E860 File Offset: 0x0009CC60
		public ItemVestAsset vestAsset
		{
			get
			{
				return this._vestAsset;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x0009E868 File Offset: 0x0009CC68
		public ItemMaskAsset maskAsset
		{
			get
			{
				return this._maskAsset;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x0009E870 File Offset: 0x0009CC70
		public ItemGlassesAsset glassesAsset
		{
			get
			{
				return this._glassesAsset;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001D98 RID: 7576 RVA: 0x0009E878 File Offset: 0x0009CC78
		// (set) Token: 0x06001D99 RID: 7577 RVA: 0x0009E880 File Offset: 0x0009CC80
		public ushort shirt
		{
			get
			{
				return this._shirt;
			}
			set
			{
				if (this.shirt != value)
				{
					this._shirt = value;
					if (this.shirt != 0)
					{
						try
						{
							this._shirtAsset = (ItemShirtAsset)Assets.find(EAssetType.ITEM, this.shirt);
						}
						catch
						{
							this._shirtAsset = null;
						}
						if (this.shirtAsset != null && this.shirtAsset.isPro && !this.canWearPro)
						{
							this._shirt = 0;
							this._shirtAsset = null;
						}
					}
					else
					{
						this._shirtAsset = null;
					}
					this.needsShirtUpdate = true;
				}
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x0009E92C File Offset: 0x0009CD2C
		// (set) Token: 0x06001D9B RID: 7579 RVA: 0x0009E934 File Offset: 0x0009CD34
		public ushort pants
		{
			get
			{
				return this._pants;
			}
			set
			{
				if (this.pants != value)
				{
					this._pants = value;
					if (this.pants != 0)
					{
						try
						{
							this._pantsAsset = (ItemPantsAsset)Assets.find(EAssetType.ITEM, this.pants);
						}
						catch
						{
							this._pantsAsset = null;
						}
						if (this.pantsAsset != null && this.pantsAsset.isPro && !this.canWearPro)
						{
							this._pants = 0;
							this._pantsAsset = null;
						}
					}
					else
					{
						this._pantsAsset = null;
					}
					this.needsPantsUpdate = true;
				}
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x0009E9E0 File Offset: 0x0009CDE0
		// (set) Token: 0x06001D9D RID: 7581 RVA: 0x0009E9E8 File Offset: 0x0009CDE8
		public ushort hat
		{
			get
			{
				return this._hat;
			}
			set
			{
				if (this.hat != value)
				{
					this._hat = value;
					if (this.hat != 0)
					{
						try
						{
							this._hatAsset = (ItemHatAsset)Assets.find(EAssetType.ITEM, this.hat);
						}
						catch
						{
							this._hatAsset = null;
						}
						if (this.hatAsset != null && this.hatAsset.isPro && !this.canWearPro)
						{
							this._hat = 0;
							this._hatAsset = null;
						}
					}
					else
					{
						this._hatAsset = null;
					}
					this.needsHatUpdate = true;
				}
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x0009EA94 File Offset: 0x0009CE94
		// (set) Token: 0x06001D9F RID: 7583 RVA: 0x0009EA9C File Offset: 0x0009CE9C
		public ushort backpack
		{
			get
			{
				return this._backpack;
			}
			set
			{
				if (this.backpack != value)
				{
					this._backpack = value;
					if (this.backpack != 0)
					{
						try
						{
							this._backpackAsset = (ItemBackpackAsset)Assets.find(EAssetType.ITEM, this.backpack);
						}
						catch
						{
							this._backpackAsset = null;
						}
						if (this.backpackAsset != null && this.backpackAsset.isPro && !this.canWearPro)
						{
							this._backpack = 0;
							this._backpackAsset = null;
						}
					}
					else
					{
						this._backpackAsset = null;
					}
					this.needsBackpackUpdate = true;
				}
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x0009EB48 File Offset: 0x0009CF48
		// (set) Token: 0x06001DA1 RID: 7585 RVA: 0x0009EB50 File Offset: 0x0009CF50
		public ushort vest
		{
			get
			{
				return this._vest;
			}
			set
			{
				if (this.vest != value)
				{
					this._vest = value;
					if (this.vest != 0)
					{
						try
						{
							this._vestAsset = (ItemVestAsset)Assets.find(EAssetType.ITEM, this.vest);
						}
						catch
						{
							this._vestAsset = null;
						}
						if (this.vestAsset != null && this.vestAsset.isPro && !this.canWearPro)
						{
							this._vest = 0;
							this._vestAsset = null;
						}
					}
					else
					{
						this._vestAsset = null;
					}
					this.needsVestUpdate = true;
				}
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x0009EBFC File Offset: 0x0009CFFC
		// (set) Token: 0x06001DA3 RID: 7587 RVA: 0x0009EC04 File Offset: 0x0009D004
		public ushort mask
		{
			get
			{
				return this._mask;
			}
			set
			{
				if (this.mask != value)
				{
					this._mask = value;
					if (this.mask != 0)
					{
						try
						{
							this._maskAsset = (ItemMaskAsset)Assets.find(EAssetType.ITEM, this.mask);
						}
						catch
						{
							this._maskAsset = null;
						}
						if (this.maskAsset != null && this.maskAsset.isPro && !this.canWearPro)
						{
							this._mask = 0;
							this._maskAsset = null;
						}
					}
					else
					{
						this._maskAsset = null;
					}
					this.needsMaskUpdate = true;
				}
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x0009ECB0 File Offset: 0x0009D0B0
		// (set) Token: 0x06001DA5 RID: 7589 RVA: 0x0009ECB8 File Offset: 0x0009D0B8
		public ushort glasses
		{
			get
			{
				return this._glasses;
			}
			set
			{
				if (this.glasses != value)
				{
					this._glasses = value;
					if (this.glasses != 0)
					{
						try
						{
							this._glassesAsset = (ItemGlassesAsset)Assets.find(EAssetType.ITEM, this.glasses);
						}
						catch
						{
							this._glassesAsset = null;
						}
						if (this.glassesAsset != null && this.glassesAsset.isPro && !this.canWearPro)
						{
							this._glasses = 0;
							this._glassesAsset = null;
						}
					}
					else
					{
						this._glassesAsset = null;
					}
					this.needsGlassesUpdate = true;
				}
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x0009ED64 File Offset: 0x0009D164
		// (set) Token: 0x06001DA7 RID: 7591 RVA: 0x0009ED6C File Offset: 0x0009D16C
		public byte face
		{
			get
			{
				return this._face;
			}
			set
			{
				if (this.face != value)
				{
					this._face = value;
					if (!Dedicator.isDedicated)
					{
						this.clothing.face = (Texture2D)Resources.Load("Faces/" + this.face + "/Texture");
						this.clothing.faceEmission = (Texture2D)Resources.Load("Faces/" + this.face + "/Emission");
						this.clothing.faceMetallic = (Texture2D)Resources.Load("Faces/" + this.face + "/Metallic");
						this.needsClothesUpdate = true;
					}
				}
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x0009EE2A File Offset: 0x0009D22A
		// (set) Token: 0x06001DA9 RID: 7593 RVA: 0x0009EE32 File Offset: 0x0009D232
		public byte hair
		{
			get
			{
				return this._hair;
			}
			set
			{
				if (this.hair != value)
				{
					this._hair = value;
					this.needsHairUpdate = true;
				}
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x0009EE4E File Offset: 0x0009D24E
		// (set) Token: 0x06001DAB RID: 7595 RVA: 0x0009EE56 File Offset: 0x0009D256
		public byte beard
		{
			get
			{
				return this._beard;
			}
			set
			{
				if (this.beard != value)
				{
					this._beard = value;
					this.needsBeardUpdate = true;
				}
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x0009EE72 File Offset: 0x0009D272
		// (set) Token: 0x06001DAD RID: 7597 RVA: 0x0009EE7F File Offset: 0x0009D27F
		public Color skin
		{
			get
			{
				return this.clothing.skin;
			}
			set
			{
				this.clothing.skin = value;
				this.needsClothesUpdate = true;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x0009EE94 File Offset: 0x0009D294
		// (set) Token: 0x06001DAF RID: 7599 RVA: 0x0009EE9C File Offset: 0x0009D29C
		public Color color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x0009EEA8 File Offset: 0x0009D2A8
		private void updateAll(bool isNeeded)
		{
			this.needsHairUpdate = isNeeded;
			this.needsBeardUpdate = isNeeded;
			this.needsClothesUpdate = isNeeded;
			this.needsShirtUpdate = isNeeded;
			this.needsPantsUpdate = isNeeded;
			this.needsHatUpdate = isNeeded;
			this.needsBackpackUpdate = isNeeded;
			this.needsVestUpdate = isNeeded;
			this.needsMaskUpdate = isNeeded;
			this.needsGlassesUpdate = isNeeded;
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x0009EEFC File Offset: 0x0009D2FC
		public void apply()
		{
			if (Dedicator.isDedicated)
			{
				return;
			}
			ItemShirtAsset itemShirtAsset = (this.visualShirtAsset == null || !this.isVisual) ? this.shirtAsset : this.visualShirtAsset;
			ItemPantsAsset itemPantsAsset = (this.visualPantsAsset == null || !this.isVisual) ? this.pantsAsset : this.visualPantsAsset;
			if (this.needsClothesUpdate || this.needsShirtUpdate || this.needsPantsUpdate)
			{
				this.clothing.shirt = null;
				this.clothing.shirtEmission = null;
				this.clothing.shirtMetallic = null;
				this.clothing.flipShirt = false;
				if (itemShirtAsset != null)
				{
					this.clothing.shirt = itemShirtAsset.shirt;
					this.clothing.shirtEmission = itemShirtAsset.emission;
					this.clothing.shirtMetallic = itemShirtAsset.metallic;
					this.clothing.flipShirt = (this.hand && itemShirtAsset.ignoreHand);
				}
				this.clothing.pants = null;
				this.clothing.pantsEmission = null;
				this.clothing.pantsMetallic = null;
				if (itemPantsAsset != null)
				{
					this.clothing.pants = itemPantsAsset.pants;
					this.clothing.pantsEmission = itemPantsAsset.emission;
					this.clothing.pantsMetallic = itemPantsAsset.metallic;
				}
				this.clothing.apply();
				if (this.materialClothing != null)
				{
					this.materialClothing.mainTexture = this.clothing.texture;
					this.materialClothing.SetTexture("_EmissionMap", this.clothing.emission);
					this.materialClothing.SetTexture("_MetallicGlossMap", this.clothing.metallic);
				}
			}
			if (!this.isMine)
			{
				if (this.needsShirtUpdate)
				{
					if (this.isUpper && this.upperSystems != null)
					{
						for (int i = 0; i < this.upperSystems.Length; i++)
						{
							Transform transform = this.upperSystems[i];
							if (transform != null)
							{
								UnityEngine.Object.Destroy(transform.gameObject);
							}
						}
						this.isUpper = false;
					}
					if (this.isVisual && this.isMythic && this.visualShirt != 0)
					{
						ushort inventoryMythicID = Provider.provider.economyService.getInventoryMythicID(this.visualShirt);
						if (inventoryMythicID != 0)
						{
							ItemTool.applyEffect(this.upperBones, this.upperSystems, inventoryMythicID, EEffectType.AREA);
							this.isUpper = true;
						}
					}
				}
				if (this.needsPantsUpdate)
				{
					if (this.isLower && this.lowerSystems != null)
					{
						for (int j = 0; j < this.lowerSystems.Length; j++)
						{
							Transform transform2 = this.lowerSystems[j];
							if (transform2 != null)
							{
								UnityEngine.Object.Destroy(transform2.gameObject);
							}
						}
						this.isLower = false;
					}
					if (this.isVisual && this.isMythic && this.visualPants != 0)
					{
						ushort inventoryMythicID2 = Provider.provider.economyService.getInventoryMythicID(this.visualPants);
						if (inventoryMythicID2 != 0)
						{
							ItemTool.applyEffect(this.lowerBones, this.lowerSystems, inventoryMythicID2, EEffectType.AREA);
							this.isLower = true;
						}
					}
				}
				ItemHatAsset itemHatAsset = (this.visualHatAsset == null || !this.isVisual) ? this.hatAsset : this.visualHatAsset;
				ItemBackpackAsset itemBackpackAsset = (this.visualBackpackAsset == null || !this.isVisual) ? this.backpackAsset : this.visualBackpackAsset;
				ItemVestAsset itemVestAsset = (this.visualVestAsset == null || !this.isVisual) ? this.vestAsset : this.visualVestAsset;
				ItemMaskAsset itemMaskAsset = (this.visualMaskAsset == null || !this.isVisual) ? this.maskAsset : this.visualMaskAsset;
				ItemGlassesAsset itemGlassesAsset = (this.visualGlassesAsset == null || !this.isVisual || (this.glassesAsset != null && (this.glassesAsset.vision != ELightingVision.NONE || this.glassesAsset.isBlindfold))) ? this.glassesAsset : this.visualGlassesAsset;
				bool flag = true;
				bool flag2 = true;
				if (this.needsHatUpdate)
				{
					if (this.hatModel != null)
					{
						UnityEngine.Object.Destroy(this.hatModel.gameObject);
					}
					if (itemHatAsset != null && itemHatAsset.hat != null)
					{
						this.hatModel = UnityEngine.Object.Instantiate<GameObject>(itemHatAsset.hat).transform;
						this.hatModel.name = "Hat";
						this.hatModel.transform.parent = this.skull;
						this.hatModel.transform.localPosition = Vector3.zero;
						this.hatModel.transform.localRotation = Quaternion.identity;
						this.hatModel.transform.localScale = Vector3.one;
						if (!this.isView)
						{
							UnityEngine.Object.Destroy(this.hatModel.GetComponent<Collider>());
						}
						if (this.isVisual && this.isMythic && this.visualHat != 0)
						{
							ushort inventoryMythicID3 = Provider.provider.economyService.getInventoryMythicID(this.visualHat);
							if (inventoryMythicID3 != 0)
							{
								ItemTool.applyEffect(this.hatModel, inventoryMythicID3, EEffectType.HOOK);
							}
						}
					}
				}
				if (itemHatAsset != null && itemHatAsset.hat != null)
				{
					if (!itemHatAsset.hasHair)
					{
						flag = false;
					}
					if (!itemHatAsset.hasBeard)
					{
						flag2 = false;
					}
				}
				if (this.needsBackpackUpdate)
				{
					if (this.backpackModel != null)
					{
						UnityEngine.Object.Destroy(this.backpackModel.gameObject);
					}
					if (itemBackpackAsset != null && itemBackpackAsset.backpack != null)
					{
						this.backpackModel = UnityEngine.Object.Instantiate<GameObject>(itemBackpackAsset.backpack).transform;
						this.backpackModel.name = "Backpack";
						this.backpackModel.transform.parent = this.spine;
						this.backpackModel.transform.localPosition = Vector3.zero;
						this.backpackModel.transform.localRotation = Quaternion.identity;
						this.backpackModel.transform.localScale = Vector3.one;
						if (!this.isView)
						{
							UnityEngine.Object.Destroy(this.backpackModel.GetComponent<Collider>());
						}
						if (this.isVisual && this.isMythic && this.visualBackpack != 0)
						{
							ushort inventoryMythicID4 = Provider.provider.economyService.getInventoryMythicID(this.visualBackpack);
							if (inventoryMythicID4 != 0)
							{
								ItemTool.applyEffect(this.backpackModel, inventoryMythicID4, EEffectType.HOOK);
							}
						}
						this.backpackModel.gameObject.SetActive(this.hasBackpack);
					}
				}
				if (this.needsVestUpdate)
				{
					if (this.vestModel != null)
					{
						UnityEngine.Object.Destroy(this.vestModel.gameObject);
					}
					if (itemVestAsset != null && itemVestAsset.vest != null)
					{
						this.vestModel = UnityEngine.Object.Instantiate<GameObject>(itemVestAsset.vest).transform;
						this.vestModel.name = "Vest";
						this.vestModel.transform.parent = this.spine;
						this.vestModel.transform.localPosition = Vector3.zero;
						this.vestModel.transform.localRotation = Quaternion.identity;
						this.vestModel.transform.localScale = Vector3.one;
						if (!this.isView)
						{
							UnityEngine.Object.Destroy(this.vestModel.GetComponent<Collider>());
						}
						if (this.isVisual && this.isMythic && this.visualVest != 0)
						{
							ushort inventoryMythicID5 = Provider.provider.economyService.getInventoryMythicID(this.visualVest);
							if (inventoryMythicID5 != 0)
							{
								ItemTool.applyEffect(this.vestModel, inventoryMythicID5, EEffectType.HOOK);
							}
						}
					}
				}
				if (this.needsMaskUpdate)
				{
					if (this.maskModel != null)
					{
						UnityEngine.Object.Destroy(this.maskModel.gameObject);
					}
					if (itemMaskAsset != null && itemMaskAsset.mask != null)
					{
						this.maskModel = UnityEngine.Object.Instantiate<GameObject>(itemMaskAsset.mask).transform;
						this.maskModel.name = "Mask";
						this.maskModel.transform.parent = this.skull;
						this.maskModel.transform.localPosition = Vector3.zero;
						this.maskModel.transform.localRotation = Quaternion.identity;
						this.maskModel.transform.localScale = Vector3.one;
						if (!this.isView)
						{
							UnityEngine.Object.Destroy(this.maskModel.GetComponent<Collider>());
						}
						if (this.isVisual && this.isMythic && this.visualMask != 0)
						{
							ushort inventoryMythicID6 = Provider.provider.economyService.getInventoryMythicID(this.visualMask);
							if (inventoryMythicID6 != 0)
							{
								ItemTool.applyEffect(this.maskModel, inventoryMythicID6, EEffectType.HOOK);
							}
						}
					}
				}
				if (itemMaskAsset != null && itemMaskAsset.mask != null)
				{
					if (!itemMaskAsset.hasHair)
					{
						flag = false;
					}
					if (!itemMaskAsset.hasBeard)
					{
						flag2 = false;
					}
				}
				if (this.needsGlassesUpdate)
				{
					if (this.glassesModel != null)
					{
						UnityEngine.Object.Destroy(this.glassesModel.gameObject);
					}
					if (itemGlassesAsset != null && itemGlassesAsset.glasses != null)
					{
						this.glassesModel = UnityEngine.Object.Instantiate<GameObject>(itemGlassesAsset.glasses).transform;
						this.glassesModel.name = "Glasses";
						this.glassesModel.transform.parent = this.skull;
						this.glassesModel.transform.localPosition = Vector3.zero;
						this.glassesModel.transform.localRotation = Quaternion.identity;
						this.glassesModel.localScale = Vector3.one;
						if (!this.isView)
						{
							UnityEngine.Object.Destroy(this.glassesModel.GetComponent<Collider>());
						}
						if (this.isVisual && this.isMythic && this.visualGlasses != 0)
						{
							ushort inventoryMythicID7 = Provider.provider.economyService.getInventoryMythicID(this.visualGlasses);
							if (inventoryMythicID7 != 0)
							{
								ItemTool.applyEffect(this.glassesModel, inventoryMythicID7, EEffectType.HOOK);
							}
						}
					}
				}
				if (itemGlassesAsset != null && itemGlassesAsset.glasses != null)
				{
					if (!itemGlassesAsset.hasHair)
					{
						flag = false;
					}
					if (!itemGlassesAsset.hasBeard)
					{
						flag2 = false;
					}
				}
				if (this.materialHair != null)
				{
					this.materialHair.color = this.color;
				}
				if (this.hasHair != flag)
				{
					this.hasHair = flag;
					this.needsHairUpdate = true;
				}
				if (this.needsHairUpdate)
				{
					if (this.hairModel != null)
					{
						UnityEngine.Object.Destroy(this.hairModel.gameObject);
					}
					if (this.hasHair)
					{
						UnityEngine.Object @object = Resources.Load("Hairs/" + this.hair + "/Hair");
						if (@object != null)
						{
							this.hairModel = ((GameObject)UnityEngine.Object.Instantiate(@object)).transform;
							this.hairModel.name = "Hair";
							this.hairModel.transform.parent = this.skull;
							this.hairModel.transform.localPosition = Vector3.zero;
							this.hairModel.transform.localRotation = Quaternion.identity;
							this.hairModel.transform.localScale = Vector3.one;
							if (this.hairModel.FindChild("Model_0") != null)
							{
								this.hairModel.FindChild("Model_0").GetComponent<Renderer>().sharedMaterial = this.materialHair;
							}
						}
					}
				}
				if (this.hasBeard != flag2)
				{
					this.hasBeard = flag2;
					this.needsBeardUpdate = true;
				}
				if (this.needsBeardUpdate)
				{
					if (this.beardModel != null)
					{
						UnityEngine.Object.Destroy(this.beardModel.gameObject);
					}
					if (this.hasBeard)
					{
						UnityEngine.Object object2 = Resources.Load("Beards/" + this.beard + "/Beard");
						if (object2 != null)
						{
							this.beardModel = ((GameObject)UnityEngine.Object.Instantiate(object2)).transform;
							this.beardModel.name = "Beard";
							this.beardModel.transform.parent = this.skull;
							this.beardModel.transform.localPosition = Vector3.zero;
							this.beardModel.transform.localRotation = Quaternion.identity;
							this.beardModel.localScale = Vector3.one;
							if (this.beardModel.FindChild("Model_0") != null)
							{
								this.beardModel.FindChild("Model_0").GetComponent<Renderer>().sharedMaterial = this.materialHair;
							}
						}
					}
				}
			}
			this.updateAll(false);
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0009FC78 File Offset: 0x0009E078
		private void Awake()
		{
			this.spine = base.transform.FindChild("Skeleton").FindChild("Spine");
			this.skull = this.spine.FindChild("Skull");
			this.upperBones = new Transform[]
			{
				this.spine,
				this.spine.FindChild("Left_Shoulder/Left_Arm"),
				this.spine.FindChild("Left_Shoulder/Left_Arm/Left_Hand"),
				this.spine.FindChild("Right_Shoulder/Right_Arm"),
				this.spine.FindChild("Right_Shoulder/Right_Arm/Right_Hand")
			};
			this.upperSystems = new Transform[this.upperBones.Length];
			this.lowerBones = new Transform[]
			{
				this.spine.parent.FindChild("Left_Hip/Left_Leg"),
				this.spine.parent.FindChild("Left_Hip/Left_Leg/Left_Foot"),
				this.spine.parent.FindChild("Right_Hip/Right_Leg"),
				this.spine.parent.FindChild("Right_Hip/Right_Leg/Right_Foot")
			};
			this.lowerSystems = new Transform[this.lowerBones.Length];
			if (!Dedicator.isDedicated)
			{
				if (HumanClothes.humanTexture == null)
				{
					HumanClothes.humanTexture = (Texture2D)Resources.Load("Characters/Human");
				}
				if (HumanClothes.shader == null)
				{
					HumanClothes.shader = Shader.Find("Standard");
				}
				if (base.transform.FindChild("Model_0") != null)
				{
					this.materialClothing = base.transform.FindChild("Model_0").GetComponent<Renderer>().material;
				}
				else if (base.transform.FindChild("Model_1") != null)
				{
					this.materialClothing = base.transform.FindChild("Model_1").GetComponent<Renderer>().material;
				}
				this.materialClothing.name = "Human";
				this.materialClothing.hideFlags = HideFlags.HideAndDontSave;
				this.materialHair = new Material(HumanClothes.shader);
				this.materialHair.name = "Hair";
				this.materialHair.hideFlags = HideFlags.HideAndDontSave;
				this.materialHair.SetFloat("_Glossiness", 0f);
			}
			if (base.transform.FindChild("Model_0") != null)
			{
				base.transform.FindChild("Model_0").GetComponent<Renderer>().sharedMaterial = this.materialClothing;
			}
			if (base.transform.FindChild("Model_1") != null)
			{
				base.transform.FindChild("Model_1").GetComponent<Renderer>().sharedMaterial = this.materialClothing;
			}
			this.clothing = new HumanClothing();
			this.updateAll(true);
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x0009FF60 File Offset: 0x0009E360
		private void OnDestroy()
		{
			if (this.materialClothing != null)
			{
				UnityEngine.Object.DestroyImmediate(this.materialClothing);
				this.materialClothing = null;
			}
			if (this.materialHair != null)
			{
				UnityEngine.Object.DestroyImmediate(this.materialHair);
				this.materialHair = null;
			}
			if (this.clothing != null)
			{
				this.clothing.destroy();
				this.clothing = null;
			}
		}

		// Token: 0x04001176 RID: 4470
		private static Texture2D humanTexture;

		// Token: 0x04001177 RID: 4471
		private static Shader shader;

		// Token: 0x04001178 RID: 4472
		private Material materialClothing;

		// Token: 0x04001179 RID: 4473
		private Material materialHair;

		// Token: 0x0400117A RID: 4474
		private Transform spine;

		// Token: 0x0400117B RID: 4475
		private Transform skull;

		// Token: 0x0400117C RID: 4476
		private Transform[] upperBones;

		// Token: 0x0400117D RID: 4477
		private Transform[] upperSystems;

		// Token: 0x0400117E RID: 4478
		private Transform[] lowerBones;

		// Token: 0x0400117F RID: 4479
		private Transform[] lowerSystems;

		// Token: 0x04001187 RID: 4487
		public HumanClothing clothing;

		// Token: 0x04001188 RID: 4488
		public bool isMine;

		// Token: 0x04001189 RID: 4489
		public bool isView;

		// Token: 0x0400118A RID: 4490
		public bool canWearPro;

		// Token: 0x0400118B RID: 4491
		private bool _isVisual = true;

		// Token: 0x0400118C RID: 4492
		private bool _isMythic = true;

		// Token: 0x0400118D RID: 4493
		public bool hand;

		// Token: 0x0400118E RID: 4494
		private bool _hasBackpack = true;

		// Token: 0x0400118F RID: 4495
		private bool isUpper;

		// Token: 0x04001190 RID: 4496
		private bool isLower;

		// Token: 0x04001191 RID: 4497
		private ItemShirtAsset visualShirtAsset;

		// Token: 0x04001192 RID: 4498
		private ItemPantsAsset visualPantsAsset;

		// Token: 0x04001193 RID: 4499
		private ItemHatAsset visualHatAsset;

		// Token: 0x04001194 RID: 4500
		private ItemBackpackAsset visualBackpackAsset;

		// Token: 0x04001195 RID: 4501
		private ItemVestAsset visualVestAsset;

		// Token: 0x04001196 RID: 4502
		private ItemMaskAsset visualMaskAsset;

		// Token: 0x04001197 RID: 4503
		private ItemGlassesAsset visualGlassesAsset;

		// Token: 0x04001198 RID: 4504
		private int _visualShirt;

		// Token: 0x04001199 RID: 4505
		private int _visualPants;

		// Token: 0x0400119A RID: 4506
		private int _visualHat;

		// Token: 0x0400119B RID: 4507
		public int _visualBackpack;

		// Token: 0x0400119C RID: 4508
		public int _visualVest;

		// Token: 0x0400119D RID: 4509
		public int _visualMask;

		// Token: 0x0400119E RID: 4510
		public int _visualGlasses;

		// Token: 0x0400119F RID: 4511
		private ItemShirtAsset _shirtAsset;

		// Token: 0x040011A0 RID: 4512
		private ItemPantsAsset _pantsAsset;

		// Token: 0x040011A1 RID: 4513
		private ItemHatAsset _hatAsset;

		// Token: 0x040011A2 RID: 4514
		private ItemBackpackAsset _backpackAsset;

		// Token: 0x040011A3 RID: 4515
		private ItemVestAsset _vestAsset;

		// Token: 0x040011A4 RID: 4516
		private ItemMaskAsset _maskAsset;

		// Token: 0x040011A5 RID: 4517
		private ItemGlassesAsset _glassesAsset;

		// Token: 0x040011A6 RID: 4518
		private ushort _shirt;

		// Token: 0x040011A7 RID: 4519
		private ushort _pants;

		// Token: 0x040011A8 RID: 4520
		private ushort _hat;

		// Token: 0x040011A9 RID: 4521
		public ushort _backpack;

		// Token: 0x040011AA RID: 4522
		public ushort _vest;

		// Token: 0x040011AB RID: 4523
		public ushort _mask;

		// Token: 0x040011AC RID: 4524
		public ushort _glasses;

		// Token: 0x040011AD RID: 4525
		private byte _face = byte.MaxValue;

		// Token: 0x040011AE RID: 4526
		private byte _hair;

		// Token: 0x040011AF RID: 4527
		private byte _beard;

		// Token: 0x040011B0 RID: 4528
		private Color _color;

		// Token: 0x040011B1 RID: 4529
		private bool hasHair;

		// Token: 0x040011B2 RID: 4530
		private bool hasBeard;

		// Token: 0x040011B3 RID: 4531
		private bool needsHairUpdate;

		// Token: 0x040011B4 RID: 4532
		private bool needsBeardUpdate;

		// Token: 0x040011B5 RID: 4533
		private bool needsClothesUpdate;

		// Token: 0x040011B6 RID: 4534
		private bool needsShirtUpdate;

		// Token: 0x040011B7 RID: 4535
		private bool needsPantsUpdate;

		// Token: 0x040011B8 RID: 4536
		private bool needsHatUpdate;

		// Token: 0x040011B9 RID: 4537
		private bool needsBackpackUpdate;

		// Token: 0x040011BA RID: 4538
		private bool needsVestUpdate;

		// Token: 0x040011BB RID: 4539
		private bool needsMaskUpdate;

		// Token: 0x040011BC RID: 4540
		private bool needsGlassesUpdate;
	}
}
