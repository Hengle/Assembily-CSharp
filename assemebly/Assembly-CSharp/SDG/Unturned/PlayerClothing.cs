using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000608 RID: 1544
	public class PlayerClothing : PlayerCaller
	{
		// Token: 0x14000080 RID: 128
		// (add) Token: 0x06002B1B RID: 11035 RVA: 0x0010C860 File Offset: 0x0010AC60
		// (remove) Token: 0x06002B1C RID: 11036 RVA: 0x0010C898 File Offset: 0x0010AC98
		public event VisualToggleChanged VisualToggleChanged;

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002B1D RID: 11037 RVA: 0x0010C8CE File Offset: 0x0010ACCE
		// (set) Token: 0x06002B1E RID: 11038 RVA: 0x0010C8D6 File Offset: 0x0010ACD6
		public HumanClothes firstClothes { get; private set; }

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x0010C8DF File Offset: 0x0010ACDF
		// (set) Token: 0x06002B20 RID: 11040 RVA: 0x0010C8E7 File Offset: 0x0010ACE7
		public HumanClothes thirdClothes { get; private set; }

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002B21 RID: 11041 RVA: 0x0010C8F0 File Offset: 0x0010ACF0
		// (set) Token: 0x06002B22 RID: 11042 RVA: 0x0010C8F8 File Offset: 0x0010ACF8
		public HumanClothes characterClothes { get; private set; }

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x0010C901 File Offset: 0x0010AD01
		public bool isVisual
		{
			get
			{
				return this.thirdClothes.isVisual;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x0010C90E File Offset: 0x0010AD0E
		// (set) Token: 0x06002B25 RID: 11045 RVA: 0x0010C916 File Offset: 0x0010AD16
		public bool isSkinned { get; private set; }

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x0010C91F File Offset: 0x0010AD1F
		public bool isMythic
		{
			get
			{
				return this.thirdClothes.isMythic;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002B27 RID: 11047 RVA: 0x0010C92C File Offset: 0x0010AD2C
		public ItemShirtAsset shirtAsset
		{
			get
			{
				return this.thirdClothes.shirtAsset;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x0010C939 File Offset: 0x0010AD39
		public ItemPantsAsset pantsAsset
		{
			get
			{
				return this.thirdClothes.pantsAsset;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06002B29 RID: 11049 RVA: 0x0010C946 File Offset: 0x0010AD46
		public ItemHatAsset hatAsset
		{
			get
			{
				return this.thirdClothes.hatAsset;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06002B2A RID: 11050 RVA: 0x0010C953 File Offset: 0x0010AD53
		public ItemBackpackAsset backpackAsset
		{
			get
			{
				return this.thirdClothes.backpackAsset;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x0010C960 File Offset: 0x0010AD60
		public ItemVestAsset vestAsset
		{
			get
			{
				return this.thirdClothes.vestAsset;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002B2C RID: 11052 RVA: 0x0010C96D File Offset: 0x0010AD6D
		public ItemMaskAsset maskAsset
		{
			get
			{
				return this.thirdClothes.maskAsset;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002B2D RID: 11053 RVA: 0x0010C97A File Offset: 0x0010AD7A
		public ItemGlassesAsset glassesAsset
		{
			get
			{
				return this.thirdClothes.glassesAsset;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002B2E RID: 11054 RVA: 0x0010C987 File Offset: 0x0010AD87
		public int visualShirt
		{
			get
			{
				return this.thirdClothes.visualShirt;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x0010C994 File Offset: 0x0010AD94
		public int visualPants
		{
			get
			{
				return this.thirdClothes.visualPants;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002B30 RID: 11056 RVA: 0x0010C9A1 File Offset: 0x0010ADA1
		public int visualHat
		{
			get
			{
				return this.thirdClothes.visualHat;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06002B31 RID: 11057 RVA: 0x0010C9AE File Offset: 0x0010ADAE
		public int visualBackpack
		{
			get
			{
				return this.thirdClothes.visualBackpack;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002B32 RID: 11058 RVA: 0x0010C9BB File Offset: 0x0010ADBB
		public int visualVest
		{
			get
			{
				return this.thirdClothes.visualVest;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x0010C9C8 File Offset: 0x0010ADC8
		public int visualMask
		{
			get
			{
				return this.thirdClothes.visualMask;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002B34 RID: 11060 RVA: 0x0010C9D5 File Offset: 0x0010ADD5
		public int visualGlasses
		{
			get
			{
				return this.thirdClothes.visualGlasses;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002B35 RID: 11061 RVA: 0x0010C9E2 File Offset: 0x0010ADE2
		public ushort shirt
		{
			get
			{
				return this.thirdClothes.shirt;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x0010C9EF File Offset: 0x0010ADEF
		public ushort pants
		{
			get
			{
				return this.thirdClothes.pants;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002B37 RID: 11063 RVA: 0x0010C9FC File Offset: 0x0010ADFC
		public ushort hat
		{
			get
			{
				return this.thirdClothes.hat;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x0010CA09 File Offset: 0x0010AE09
		public ushort backpack
		{
			get
			{
				return this.thirdClothes.backpack;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x0010CA16 File Offset: 0x0010AE16
		public ushort vest
		{
			get
			{
				return this.thirdClothes.vest;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x0010CA23 File Offset: 0x0010AE23
		public ushort mask
		{
			get
			{
				return this.thirdClothes.mask;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x0010CA30 File Offset: 0x0010AE30
		public ushort glasses
		{
			get
			{
				return this.thirdClothes.glasses;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x0010CA3D File Offset: 0x0010AE3D
		public byte face
		{
			get
			{
				return this.thirdClothes.face;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x0010CA4A File Offset: 0x0010AE4A
		public byte hair
		{
			get
			{
				return this.thirdClothes.hair;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x0010CA57 File Offset: 0x0010AE57
		public byte beard
		{
			get
			{
				return this.thirdClothes.beard;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002B3F RID: 11071 RVA: 0x0010CA64 File Offset: 0x0010AE64
		public Color skin
		{
			get
			{
				return this.thirdClothes.skin;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x0010CA71 File Offset: 0x0010AE71
		public Color color
		{
			get
			{
				return this.thirdClothes.color;
			}
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x0010CA80 File Offset: 0x0010AE80
		[SteamCall]
		public void tellUpdateShirtQuality(CSteamID steamID, byte quality)
		{
			if (base.channel.checkServer(steamID))
			{
				this.shirtQuality = quality;
				if (this.onShirtUpdated != null)
				{
					this.onShirtUpdated(this.shirt, this.shirtQuality, this.shirtState);
				}
			}
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x0010CACD File Offset: 0x0010AECD
		public void sendUpdateShirtQuality()
		{
			base.channel.send("tellUpdateShirtQuality", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				this.shirtQuality
			});
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x0010CAF8 File Offset: 0x0010AEF8
		[SteamCall]
		public void tellUpdatePantsQuality(CSteamID steamID, byte quality)
		{
			if (base.channel.checkServer(steamID))
			{
				this.pantsQuality = quality;
				if (this.onPantsUpdated != null)
				{
					this.onPantsUpdated(this.pants, this.pantsQuality, this.pantsState);
				}
			}
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x0010CB45 File Offset: 0x0010AF45
		public void sendUpdatePantsQuality()
		{
			base.channel.send("tellUpdatePantsQuality", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				this.pantsQuality
			});
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x0010CB70 File Offset: 0x0010AF70
		[SteamCall]
		public void tellUpdateHatQuality(CSteamID steamID, byte quality)
		{
			if (base.channel.checkServer(steamID))
			{
				this.hatQuality = quality;
				if (this.onHatUpdated != null)
				{
					this.onHatUpdated(this.hat, this.hatQuality, this.hatState);
				}
			}
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x0010CBBD File Offset: 0x0010AFBD
		public void sendUpdateHatQuality()
		{
			base.channel.send("tellUpdateHatQuality", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				this.hatQuality
			});
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x0010CBE8 File Offset: 0x0010AFE8
		[SteamCall]
		public void tellUpdateBackpackQuality(CSteamID steamID, byte quality)
		{
			if (base.channel.checkServer(steamID))
			{
				this.backpackQuality = quality;
				if (this.onBackpackUpdated != null)
				{
					this.onBackpackUpdated(this.backpack, this.backpackQuality, this.backpackState);
				}
			}
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x0010CC35 File Offset: 0x0010B035
		public void sendUpdateBackpackQuality()
		{
			base.channel.send("tellUpdateBackpackQuality", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				this.backpackQuality
			});
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x0010CC60 File Offset: 0x0010B060
		[SteamCall]
		public void tellUpdateVestQuality(CSteamID steamID, byte quality)
		{
			if (base.channel.checkServer(steamID))
			{
				this.vestQuality = quality;
				if (this.onVestUpdated != null)
				{
					this.onVestUpdated(this.vest, this.vestQuality, this.vestState);
				}
			}
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x0010CCAD File Offset: 0x0010B0AD
		public void sendUpdateVestQuality()
		{
			base.channel.send("tellUpdateVestQuality", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				this.vestQuality
			});
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x0010CCD8 File Offset: 0x0010B0D8
		[SteamCall]
		public void tellUpdateMaskQuality(CSteamID steamID, byte quality)
		{
			if (base.channel.checkServer(steamID))
			{
				this.maskQuality = quality;
				if (this.onMaskUpdated != null)
				{
					this.onMaskUpdated(this.mask, this.maskQuality, this.maskState);
				}
			}
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x0010CD25 File Offset: 0x0010B125
		public void sendUpdateMaskQuality()
		{
			base.channel.send("tellUpdateMaskQuality", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				this.maskQuality
			});
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x0010CD4E File Offset: 0x0010B14E
		public void updateMaskQuality()
		{
			if (this.onMaskUpdated != null)
			{
				this.onMaskUpdated(this.mask, this.maskQuality, this.maskState);
			}
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x0010CD78 File Offset: 0x0010B178
		[SteamCall]
		public void tellUpdateGlassesQuality(CSteamID steamID, byte quality)
		{
			if (base.channel.checkServer(steamID))
			{
				this.glassesQuality = quality;
				if (this.onGlassesUpdated != null)
				{
					this.onGlassesUpdated(this.glasses, this.glassesQuality, this.glassesState);
				}
			}
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x0010CDC5 File Offset: 0x0010B1C5
		public void sendUpdateGlassesQuality()
		{
			base.channel.send("tellUpdateGlassesQuality", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				this.glassesQuality
			});
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x0010CDF0 File Offset: 0x0010B1F0
		[SteamCall]
		public void tellWearShirt(CSteamID steamID, ushort id, byte quality, byte[] state)
		{
			if (base.channel.checkServer(steamID))
			{
				if (this.thirdClothes == null)
				{
					return;
				}
				this.thirdClothes.shirt = id;
				this.shirtQuality = quality;
				this.shirtState = state;
				this.thirdClothes.apply();
				if (this.firstClothes != null)
				{
					this.firstClothes.shirt = id;
					this.firstClothes.apply();
				}
				if (this.characterClothes != null)
				{
					this.characterClothes.shirt = id;
					this.characterClothes.apply();
					Characters.active.shirt = id;
				}
				if (this.onShirtUpdated != null)
				{
					this.onShirtUpdated(id, quality, state);
				}
			}
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x0010CEBC File Offset: 0x0010B2BC
		[SteamCall]
		public void askSwapShirt(CSteamID steamID, byte page, byte x, byte y)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (base.player.equipment.checkSelection(PlayerInventory.SHIRT))
				{
					if (base.player.equipment.isBusy)
					{
						return;
					}
					base.player.equipment.dequip();
				}
				if (page == 255)
				{
					if (this.shirt == 0)
					{
						return;
					}
					this.askWearShirt(0, 0, new byte[0], true);
				}
				else
				{
					byte index = base.player.inventory.getIndex(page, x, y);
					if (index == 255)
					{
						return;
					}
					ItemJar item = base.player.inventory.getItem(page, index);
					ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item.item.id);
					if (itemAsset != null && itemAsset.type == EItemType.SHIRT)
					{
						base.player.inventory.removeItem(page, index);
						this.askWearShirt(item.item.id, item.item.quality, item.item.state, true);
					}
				}
			}
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x0010CFE8 File Offset: 0x0010B3E8
		public void askWearShirt(ushort id, byte quality, byte[] state, bool playEffect)
		{
			ushort shirt = this.shirt;
			byte newQuality = this.shirtQuality;
			byte[] newState = this.shirtState;
			if (id != 0 && playEffect)
			{
				EffectManager.sendEffect(9, EffectManager.SMALL, base.transform.position);
			}
			base.channel.send("tellWearShirt", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				quality,
				state
			});
			if (shirt != 0)
			{
				base.player.inventory.forceAddItem(new Item(shirt, 1, newQuality, newState), false);
			}
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x0010D080 File Offset: 0x0010B480
		public void sendSwapShirt(byte page, byte x, byte y)
		{
			if (page == 255 && this.shirt == 0)
			{
				return;
			}
			base.channel.send("askSwapShirt", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				page,
				x,
				y
			});
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x0010D0D8 File Offset: 0x0010B4D8
		[SteamCall]
		public void tellWearPants(CSteamID steamID, ushort id, byte quality, byte[] state)
		{
			if (base.channel.checkServer(steamID))
			{
				if (this.thirdClothes == null)
				{
					return;
				}
				this.thirdClothes.pants = id;
				this.pantsQuality = quality;
				this.pantsState = state;
				this.thirdClothes.apply();
				if (this.characterClothes != null)
				{
					this.characterClothes.pants = id;
					this.characterClothes.apply();
					Characters.active.pants = id;
				}
				if (this.onPantsUpdated != null)
				{
					this.onPantsUpdated(id, quality, state);
				}
			}
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x0010D17C File Offset: 0x0010B57C
		[SteamCall]
		public void askSwapPants(CSteamID steamID, byte page, byte x, byte y)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (base.player.equipment.checkSelection(PlayerInventory.PANTS))
				{
					if (base.player.equipment.isBusy)
					{
						return;
					}
					base.player.equipment.dequip();
				}
				if (page == 255)
				{
					if (this.pants == 0)
					{
						return;
					}
					this.askWearPants(0, 0, new byte[0], true);
				}
				else
				{
					byte index = base.player.inventory.getIndex(page, x, y);
					if (index == 255)
					{
						return;
					}
					ItemJar item = base.player.inventory.getItem(page, index);
					ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item.item.id);
					if (itemAsset != null && itemAsset.type == EItemType.PANTS)
					{
						base.player.inventory.removeItem(page, index);
						this.askWearPants(item.item.id, item.item.quality, item.item.state, true);
					}
				}
			}
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x0010D2A8 File Offset: 0x0010B6A8
		public void askWearPants(ushort id, byte quality, byte[] state, bool playEffect)
		{
			ushort pants = this.pants;
			byte newQuality = this.pantsQuality;
			byte[] newState = this.pantsState;
			if (id != 0 && playEffect)
			{
				EffectManager.sendEffect(9, EffectManager.SMALL, base.transform.position);
			}
			base.channel.send("tellWearPants", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				quality,
				state
			});
			if (pants != 0)
			{
				base.player.inventory.forceAddItem(new Item(pants, 1, newQuality, newState), false);
			}
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x0010D340 File Offset: 0x0010B740
		public void sendSwapPants(byte page, byte x, byte y)
		{
			if (page == 255 && this.pants == 0)
			{
				return;
			}
			base.channel.send("askSwapPants", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				page,
				x,
				y
			});
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x0010D398 File Offset: 0x0010B798
		[SteamCall]
		public void tellWearHat(CSteamID steamID, ushort id, byte quality, byte[] state)
		{
			if (base.channel.checkServer(steamID))
			{
				if (this.thirdClothes == null)
				{
					return;
				}
				this.thirdClothes.hat = id;
				this.hatQuality = quality;
				this.hatState = state;
				this.thirdClothes.apply();
				if (this.characterClothes != null)
				{
					this.characterClothes.hat = id;
					this.characterClothes.apply();
					Characters.active.hat = id;
				}
				if (this.onHatUpdated != null)
				{
					this.onHatUpdated(id, quality, state);
				}
			}
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x0010D43C File Offset: 0x0010B83C
		[SteamCall]
		public void askSwapHat(CSteamID steamID, byte page, byte x, byte y)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (page == 255)
				{
					if (this.hat == 0)
					{
						return;
					}
					this.askWearHat(0, 0, new byte[0], true);
				}
				else
				{
					byte index = base.player.inventory.getIndex(page, x, y);
					if (index == 255)
					{
						return;
					}
					ItemJar item = base.player.inventory.getItem(page, index);
					ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item.item.id);
					if (itemAsset != null && itemAsset.type == EItemType.HAT)
					{
						base.player.inventory.removeItem(page, index);
						this.askWearHat(item.item.id, item.item.quality, item.item.state, true);
					}
				}
			}
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x0010D528 File Offset: 0x0010B928
		public void askWearHat(ushort id, byte quality, byte[] state, bool playEffect)
		{
			ushort hat = this.hat;
			byte newQuality = this.hatQuality;
			byte[] newState = this.hatState;
			if (id != 0 && playEffect)
			{
				EffectManager.sendEffect(9, EffectManager.SMALL, base.transform.position);
			}
			base.channel.send("tellWearHat", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				quality,
				state
			});
			if (hat != 0)
			{
				base.player.inventory.forceAddItem(new Item(hat, 1, newQuality, newState), false);
			}
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x0010D5C0 File Offset: 0x0010B9C0
		public void sendSwapHat(byte page, byte x, byte y)
		{
			if (page == 255 && this.hat == 0)
			{
				return;
			}
			base.channel.send("askSwapHat", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				page,
				x,
				y
			});
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x0010D618 File Offset: 0x0010BA18
		[SteamCall]
		public void tellWearBackpack(CSteamID steamID, ushort id, byte quality, byte[] state)
		{
			if (base.channel.checkServer(steamID))
			{
				if (this.thirdClothes == null)
				{
					return;
				}
				this.thirdClothes.backpack = id;
				this.backpackQuality = quality;
				this.backpackState = state;
				this.thirdClothes.apply();
				if (this.characterClothes != null)
				{
					this.characterClothes.backpack = id;
					this.characterClothes.apply();
					Characters.active.backpack = id;
				}
				if (this.onBackpackUpdated != null)
				{
					this.onBackpackUpdated(id, quality, state);
				}
			}
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x0010D6BC File Offset: 0x0010BABC
		[SteamCall]
		public void askSwapBackpack(CSteamID steamID, byte page, byte x, byte y)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (base.player.equipment.checkSelection(PlayerInventory.BACKPACK))
				{
					if (base.player.equipment.isBusy)
					{
						return;
					}
					base.player.equipment.dequip();
				}
				if (page == 255)
				{
					if (this.backpack == 0)
					{
						return;
					}
					this.askWearBackpack(0, 0, new byte[0], true);
				}
				else
				{
					byte index = base.player.inventory.getIndex(page, x, y);
					if (index == 255)
					{
						return;
					}
					ItemJar item = base.player.inventory.getItem(page, index);
					ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item.item.id);
					if (itemAsset != null && itemAsset.type == EItemType.BACKPACK)
					{
						base.player.inventory.removeItem(page, index);
						this.askWearBackpack(item.item.id, item.item.quality, item.item.state, true);
					}
				}
			}
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x0010D7E8 File Offset: 0x0010BBE8
		public void askWearBackpack(ushort id, byte quality, byte[] state, bool playEffect)
		{
			ushort backpack = this.backpack;
			byte newQuality = this.backpackQuality;
			byte[] newState = this.backpackState;
			if (id != 0 && playEffect)
			{
				EffectManager.sendEffect(10, EffectManager.SMALL, base.transform.position);
			}
			base.channel.send("tellWearBackpack", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				quality,
				state
			});
			if (backpack != 0)
			{
				base.player.inventory.forceAddItem(new Item(backpack, 1, newQuality, newState), false);
			}
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x0010D880 File Offset: 0x0010BC80
		public void sendSwapBackpack(byte page, byte x, byte y)
		{
			if (page == 255 && this.backpack == 0)
			{
				return;
			}
			base.channel.send("askSwapBackpack", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				page,
				x,
				y
			});
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x0010D8D8 File Offset: 0x0010BCD8
		[SteamCall]
		public void tellVisualToggle(CSteamID steamID, byte index, bool toggle)
		{
			if (base.channel.checkServer(steamID))
			{
				if (index != 0)
				{
					if (index != 1)
					{
						if (index == 2)
						{
							this.thirdClothes.isMythic = toggle;
							this.thirdClothes.apply();
							if (this.firstClothes != null)
							{
								this.firstClothes.isMythic = toggle;
								this.firstClothes.apply();
							}
							if (this.characterClothes != null)
							{
								this.characterClothes.isMythic = toggle;
								this.characterClothes.apply();
							}
							base.player.equipment.applyMythicVisual();
						}
					}
					else
					{
						this.isSkinned = toggle;
						base.player.equipment.applySkinVisual();
						base.player.equipment.applyMythicVisual();
					}
				}
				else
				{
					this.thirdClothes.isVisual = toggle;
					this.thirdClothes.apply();
					if (this.firstClothes != null)
					{
						this.firstClothes.isVisual = toggle;
						this.firstClothes.apply();
					}
					if (this.characterClothes != null)
					{
						this.characterClothes.isVisual = toggle;
						this.characterClothes.apply();
					}
				}
				if (this.VisualToggleChanged != null)
				{
					this.VisualToggleChanged(this);
				}
			}
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x0010DA3C File Offset: 0x0010BE3C
		[SteamCall]
		public void askVisualToggle(CSteamID steamID, byte index)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (Time.realtimeSinceStartup - this.lastVisualToggle < 0.5f)
				{
					return;
				}
				this.lastVisualToggle = Time.realtimeSinceStartup;
				if (index != 0)
				{
					if (index != 1)
					{
						if (index == 2)
						{
							base.channel.send("tellVisualToggle", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								index,
								!this.isMythic
							});
						}
					}
					else
					{
						base.channel.send("tellVisualToggle", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							index,
							!this.isSkinned
						});
					}
				}
				else
				{
					base.channel.send("tellVisualToggle", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						index,
						!this.isVisual
					});
				}
			}
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x0010DB49 File Offset: 0x0010BF49
		public void sendVisualToggle(EVisualToggleType type)
		{
			base.channel.send("askVisualToggle", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				(byte)type
			});
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x0010DB70 File Offset: 0x0010BF70
		[SteamCall]
		public void tellWearVest(CSteamID steamID, ushort id, byte quality, byte[] state)
		{
			if (base.channel.checkServer(steamID))
			{
				if (this.thirdClothes == null)
				{
					return;
				}
				this.thirdClothes.vest = id;
				this.vestQuality = quality;
				this.vestState = state;
				this.thirdClothes.apply();
				if (this.characterClothes != null)
				{
					this.characterClothes.vest = id;
					this.characterClothes.apply();
					Characters.active.vest = id;
				}
				if (this.onVestUpdated != null)
				{
					this.onVestUpdated(id, quality, state);
				}
			}
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x0010DC14 File Offset: 0x0010C014
		[SteamCall]
		public void askSwapVest(CSteamID steamID, byte page, byte x, byte y)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (base.player.equipment.checkSelection(PlayerInventory.VEST))
				{
					if (base.player.equipment.isBusy)
					{
						return;
					}
					base.player.equipment.dequip();
				}
				if (page == 255)
				{
					if (this.vest == 0)
					{
						return;
					}
					this.askWearVest(0, 0, new byte[0], true);
				}
				else
				{
					byte index = base.player.inventory.getIndex(page, x, y);
					if (index == 255)
					{
						return;
					}
					ItemJar item = base.player.inventory.getItem(page, index);
					ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item.item.id);
					if (itemAsset != null && itemAsset.type == EItemType.VEST)
					{
						base.player.inventory.removeItem(page, index);
						this.askWearVest(item.item.id, item.item.quality, item.item.state, true);
					}
				}
			}
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x0010DD40 File Offset: 0x0010C140
		public void askWearVest(ushort id, byte quality, byte[] state, bool playEffect)
		{
			ushort vest = this.vest;
			byte newQuality = this.vestQuality;
			byte[] newState = this.vestState;
			if (id != 0 && playEffect)
			{
				EffectManager.sendEffect(10, EffectManager.SMALL, base.transform.position);
			}
			base.channel.send("tellWearVest", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				quality,
				state
			});
			if (vest != 0)
			{
				base.player.inventory.forceAddItem(new Item(vest, 1, newQuality, newState), false);
			}
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x0010DDD8 File Offset: 0x0010C1D8
		public void sendSwapVest(byte page, byte x, byte y)
		{
			if (page == 255 && this.vest == 0)
			{
				return;
			}
			base.channel.send("askSwapVest", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				page,
				x,
				y
			});
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x0010DE30 File Offset: 0x0010C230
		[SteamCall]
		public void tellWearMask(CSteamID steamID, ushort id, byte quality, byte[] state)
		{
			if (base.channel.checkServer(steamID))
			{
				if (this.thirdClothes == null)
				{
					return;
				}
				this.thirdClothes.mask = id;
				this.maskQuality = quality;
				this.maskState = state;
				this.thirdClothes.apply();
				if (this.characterClothes != null)
				{
					this.characterClothes.mask = id;
					this.characterClothes.apply();
					Characters.active.mask = id;
				}
				if (this.onMaskUpdated != null)
				{
					this.onMaskUpdated(id, quality, state);
				}
			}
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x0010DED4 File Offset: 0x0010C2D4
		[SteamCall]
		public void askSwapMask(CSteamID steamID, byte page, byte x, byte y)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (page == 255)
				{
					if (this.mask == 0)
					{
						return;
					}
					this.askWearMask(0, 0, new byte[0], true);
				}
				else
				{
					byte index = base.player.inventory.getIndex(page, x, y);
					if (index == 255)
					{
						return;
					}
					ItemJar item = base.player.inventory.getItem(page, index);
					ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item.item.id);
					if (itemAsset != null && itemAsset.type == EItemType.MASK)
					{
						base.player.inventory.removeItem(page, index);
						this.askWearMask(item.item.id, item.item.quality, item.item.state, true);
					}
				}
			}
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x0010DFC0 File Offset: 0x0010C3C0
		public void askWearMask(ushort id, byte quality, byte[] state, bool playEffect)
		{
			ushort mask = this.mask;
			byte newQuality = this.maskQuality;
			byte[] newState = this.maskState;
			if (id != 0 && playEffect)
			{
				EffectManager.sendEffect(9, EffectManager.SMALL, base.transform.position);
			}
			base.channel.send("tellWearMask", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				quality,
				state
			});
			if (mask != 0)
			{
				base.player.inventory.forceAddItem(new Item(mask, 1, newQuality, newState), false);
			}
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x0010E058 File Offset: 0x0010C458
		public void sendSwapMask(byte page, byte x, byte y)
		{
			if (page == 255 && this.mask == 0)
			{
				return;
			}
			base.channel.send("askSwapMask", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				page,
				x,
				y
			});
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x0010E0B0 File Offset: 0x0010C4B0
		[SteamCall]
		public void tellWearGlasses(CSteamID steamID, ushort id, byte quality, byte[] state)
		{
			if (base.channel.checkServer(steamID))
			{
				if (this.thirdClothes == null)
				{
					return;
				}
				this.thirdClothes.glasses = id;
				this.glassesQuality = quality;
				this.glassesState = state;
				this.thirdClothes.apply();
				if (this.characterClothes != null)
				{
					this.characterClothes.glasses = id;
					this.characterClothes.apply();
					Characters.active.glasses = id;
				}
				if (this.onGlassesUpdated != null)
				{
					this.onGlassesUpdated(id, quality, state);
				}
			}
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x0010E154 File Offset: 0x0010C554
		[SteamCall]
		public void askSwapGlasses(CSteamID steamID, byte page, byte x, byte y)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (page == 255)
				{
					if (this.glasses == 0)
					{
						return;
					}
					this.askWearGlasses(0, 0, new byte[0], true);
				}
				else
				{
					byte index = base.player.inventory.getIndex(page, x, y);
					if (index == 255)
					{
						return;
					}
					ItemJar item = base.player.inventory.getItem(page, index);
					ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item.item.id);
					if (itemAsset != null && itemAsset.type == EItemType.GLASSES)
					{
						base.player.inventory.removeItem(page, index);
						this.askWearGlasses(item.item.id, item.item.quality, item.item.state, true);
					}
				}
			}
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x0010E240 File Offset: 0x0010C640
		public void askWearGlasses(ushort id, byte quality, byte[] state, bool playEffect)
		{
			ushort glasses = this.glasses;
			byte newQuality = this.glassesQuality;
			byte[] newState = this.glassesState;
			if (id != 0 && playEffect)
			{
				EffectManager.sendEffect(9, EffectManager.SMALL, base.transform.position);
			}
			base.channel.send("tellWearGlasses", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				quality,
				state
			});
			if (glasses != 0)
			{
				base.player.inventory.forceAddItem(new Item(glasses, 1, newQuality, newState), false);
			}
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x0010E2D8 File Offset: 0x0010C6D8
		public void sendSwapGlasses(byte page, byte x, byte y)
		{
			if (page == 255 && this.glasses == 0)
			{
				return;
			}
			base.channel.send("askSwapGlasses", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				page,
				x,
				y
			});
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x0010E330 File Offset: 0x0010C730
		[SteamCall]
		public void tellClothing(CSteamID steamID, ushort newShirt, byte newShirtQuality, byte[] newShirtState, ushort newPants, byte newPantsQuality, byte[] newPantsState, ushort newHat, byte newHatQuality, byte[] newHatState, ushort newBackpack, byte newBackpackQuality, byte[] newBackpackState, ushort newVest, byte newVestQuality, byte[] newVestState, ushort newMask, byte newMaskQuality, byte[] newMaskState, ushort newGlasses, byte newGlassesQuality, byte[] newGlassesState, bool newVisual, bool newSkinned, bool newMythic)
		{
			if (base.channel.checkServer(steamID))
			{
				base.player.animator.unlock();
				if (base.channel.isOwner)
				{
					Player.isLoadingClothing = false;
				}
				if (this.thirdClothes != null)
				{
					this.thirdClothes.face = base.channel.owner.face;
					this.thirdClothes.hair = base.channel.owner.hair;
					this.thirdClothes.beard = base.channel.owner.beard;
					this.thirdClothes.skin = base.channel.owner.skin;
					this.thirdClothes.color = base.channel.owner.color;
					this.thirdClothes.shirt = newShirt;
					this.shirtQuality = newShirtQuality;
					this.shirtState = newShirtState;
					this.thirdClothes.pants = newPants;
					this.pantsQuality = newPantsQuality;
					this.pantsState = newPantsState;
					this.thirdClothes.hat = newHat;
					this.hatQuality = newHatQuality;
					this.hatState = newHatState;
					this.thirdClothes.backpack = newBackpack;
					this.backpackQuality = newBackpackQuality;
					this.backpackState = newBackpackState;
					this.thirdClothes.vest = newVest;
					this.vestQuality = newVestQuality;
					this.vestState = newVestState;
					this.thirdClothes.mask = newMask;
					this.maskQuality = newMaskQuality;
					this.maskState = newMaskState;
					this.thirdClothes.glasses = newGlasses;
					this.glassesQuality = newGlassesQuality;
					this.glassesState = newGlassesState;
					this.thirdClothes.isVisual = newVisual;
					this.thirdClothes.isMythic = newMythic;
					this.thirdClothes.apply();
				}
				if (this.firstClothes != null)
				{
					this.firstClothes.skin = base.channel.owner.skin;
					this.firstClothes.shirt = newShirt;
					this.firstClothes.isVisual = newVisual;
					this.firstClothes.isMythic = newMythic;
					this.firstClothes.apply();
				}
				if (this.characterClothes != null)
				{
					this.characterClothes.face = base.channel.owner.face;
					this.characterClothes.hair = base.channel.owner.hair;
					this.characterClothes.beard = base.channel.owner.beard;
					this.characterClothes.skin = base.channel.owner.skin;
					this.characterClothes.color = base.channel.owner.color;
					this.characterClothes.shirt = newShirt;
					this.characterClothes.pants = newPants;
					this.characterClothes.hat = newHat;
					this.characterClothes.backpack = newBackpack;
					this.characterClothes.vest = newVest;
					this.characterClothes.mask = newMask;
					this.characterClothes.glasses = newGlasses;
					this.characterClothes.isVisual = newVisual;
					this.characterClothes.isMythic = newMythic;
					this.characterClothes.apply();
					Characters.active.shirt = newShirt;
					Characters.active.pants = newPants;
					Characters.active.hat = newHat;
					Characters.active.backpack = newBackpack;
					Characters.active.vest = newVest;
					Characters.active.mask = newMask;
					Characters.active.glasses = newGlasses;
					Characters.hasPlayed = true;
				}
				this.isSkinned = newSkinned;
				base.player.equipment.applySkinVisual();
				base.player.equipment.applyMythicVisual();
				if (this.onShirtUpdated != null)
				{
					this.onShirtUpdated(newShirt, newShirtQuality, newShirtState);
				}
				if (this.onPantsUpdated != null)
				{
					this.onPantsUpdated(newPants, newPantsQuality, newPantsState);
				}
				if (this.onHatUpdated != null)
				{
					this.onHatUpdated(newHat, newHatQuality, newHatState);
				}
				if (this.onBackpackUpdated != null)
				{
					this.onBackpackUpdated(newBackpack, newBackpackQuality, newBackpackState);
				}
				if (this.onVestUpdated != null)
				{
					this.onVestUpdated(newVest, newVestQuality, newVestState);
				}
				if (this.onMaskUpdated != null)
				{
					this.onMaskUpdated(newMask, newMaskQuality, newMaskState);
				}
				if (this.onGlassesUpdated != null)
				{
					this.onGlassesUpdated(newGlasses, newGlassesQuality, newGlassesState);
				}
			}
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x0010E7B4 File Offset: 0x0010CBB4
		public void updateClothes(ushort newShirt, byte newShirtQuality, byte[] newShirtState, ushort newPants, byte newPantsQuality, byte[] newPantsState, ushort newHat, byte newHatQuality, byte[] newHatState, ushort newBackpack, byte newBackpackQuality, byte[] newBackpackState, ushort newVest, byte newVestQuality, byte[] newVestState, ushort newMask, byte newMaskQuality, byte[] newMaskState, ushort newGlasses, byte newGlassesQuality, byte[] newGlassesState)
		{
			base.channel.send("tellClothing", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				newShirt,
				newShirtQuality,
				newShirtState,
				newPants,
				newPantsQuality,
				newPantsState,
				newHat,
				newHatQuality,
				newHatState,
				newBackpack,
				newBackpackQuality,
				newBackpackState,
				newVest,
				newVestQuality,
				newVestState,
				newMask,
				newMaskQuality,
				newMaskState,
				newGlasses,
				newGlassesQuality,
				newGlassesState,
				this.isVisual,
				this.isSkinned,
				this.isMythic
			});
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x0010E8C0 File Offset: 0x0010CCC0
		[SteamCall]
		public void askClothing(CSteamID steamID)
		{
			if (Provider.isServer)
			{
				base.channel.send("tellClothing", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					this.shirt,
					this.shirtQuality,
					this.shirtState,
					this.pants,
					this.pantsQuality,
					this.pantsState,
					this.hat,
					this.hatQuality,
					this.hatState,
					this.backpack,
					this.backpackQuality,
					this.backpackState,
					this.vest,
					this.vestQuality,
					this.vestState,
					this.mask,
					this.maskQuality,
					this.maskState,
					this.glasses,
					this.glassesQuality,
					this.glassesState,
					this.isVisual,
					this.isSkinned,
					this.isMythic
				});
			}
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x0010EA30 File Offset: 0x0010CE30
		[SteamCall]
		public void tellSwapFace(CSteamID steamID, byte index)
		{
			if (base.channel.checkServer(steamID))
			{
				base.channel.owner.face = index;
				if (this.thirdClothes != null)
				{
					this.thirdClothes.face = base.channel.owner.face;
					this.thirdClothes.apply();
				}
				if (this.characterClothes != null)
				{
					this.characterClothes.face = base.channel.owner.face;
					this.characterClothes.apply();
				}
			}
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x0010EAD0 File Offset: 0x0010CED0
		[SteamCall]
		public void askSwapFace(CSteamID steamID, byte index)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (Time.realtimeSinceStartup - this.lastFaceSwap < 0.5f)
				{
					return;
				}
				this.lastFaceSwap = Time.realtimeSinceStartup;
				if (index >= Customization.FACES_FREE + Customization.FACES_PRO)
				{
					return;
				}
				if (!base.channel.owner.isPro && index >= Customization.FACES_FREE)
				{
					return;
				}
				base.channel.send("tellSwapFace", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					index
				});
			}
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x0010EB6F File Offset: 0x0010CF6F
		public void sendSwapFace(byte index)
		{
			base.channel.send("askSwapFace", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				index
			});
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x0010EB94 File Offset: 0x0010CF94
		private void onStanceUpdated()
		{
			if (this.thirdClothes == null)
			{
				return;
			}
			if (base.player.movement.getVehicle() != null)
			{
				this.thirdClothes.hasBackpack = (base.player.movement.getVehicle().passengers[(int)base.player.movement.getSeat()].obj == null);
			}
			else
			{
				this.thirdClothes.hasBackpack = true;
			}
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x0010EC1C File Offset: 0x0010D01C
		private void onLifeUpdated(bool isDead)
		{
			if (isDead && Provider.isServer)
			{
				bool flag = (!base.player.life.wasPvPDeath) ? Provider.modeConfigData.Players.Lose_Clothes_PvE : Provider.modeConfigData.Players.Lose_Clothes_PvP;
				if (flag)
				{
					if (this.shirt != 0)
					{
						ItemManager.dropItem(new Item(this.shirt, 1, this.shirtQuality, this.shirtState), base.transform.position, false, true, true);
					}
					if (this.pants != 0)
					{
						ItemManager.dropItem(new Item(this.pants, 1, this.pantsQuality, this.pantsState), base.transform.position, false, true, true);
					}
					if (this.hat != 0)
					{
						ItemManager.dropItem(new Item(this.hat, 1, this.hatQuality, this.hatState), base.transform.position, false, true, true);
					}
					if (this.backpack != 0)
					{
						ItemManager.dropItem(new Item(this.backpack, 1, this.backpackQuality, this.backpackState), base.transform.position, false, true, true);
					}
					if (this.vest != 0)
					{
						ItemManager.dropItem(new Item(this.vest, 1, this.vestQuality, this.vestState), base.transform.position, false, true, true);
					}
					if (this.mask != 0)
					{
						ItemManager.dropItem(new Item(this.mask, 1, this.maskQuality, this.maskState), base.transform.position, false, true, true);
					}
					if (this.glasses != 0)
					{
						ItemManager.dropItem(new Item(this.glasses, 1, this.glassesQuality, this.glassesState), base.transform.position, false, true, true);
					}
					this.thirdClothes.shirt = 0;
					this.shirtQuality = 0;
					this.thirdClothes.pants = 0;
					this.pantsQuality = 0;
					this.thirdClothes.hat = 0;
					this.hatQuality = 0;
					this.thirdClothes.backpack = 0;
					this.backpackQuality = 0;
					this.thirdClothes.vest = 0;
					this.vestQuality = 0;
					this.thirdClothes.mask = 0;
					this.maskQuality = 0;
					this.thirdClothes.glasses = 0;
					this.glassesQuality = 0;
					this.shirtState = new byte[0];
					this.pantsState = new byte[0];
					this.hatState = new byte[0];
					this.backpackState = new byte[0];
					this.vestState = new byte[0];
					this.maskState = new byte[0];
					this.glassesState = new byte[0];
					base.channel.send("tellClothing", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						this.shirt,
						this.shirtQuality,
						this.shirtState,
						this.pants,
						this.pantsQuality,
						this.pantsState,
						this.hat,
						this.hatQuality,
						this.hatState,
						this.backpack,
						this.backpackQuality,
						this.backpackState,
						this.vest,
						this.vestQuality,
						this.vestState,
						this.mask,
						this.maskQuality,
						this.maskState,
						this.glasses,
						this.glassesQuality,
						this.glassesState,
						this.isVisual,
						this.isSkinned,
						this.isMythic
					});
				}
			}
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x0010F021 File Offset: 0x0010D421
		public void init()
		{
			base.channel.send("askClothing", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x0010F03C File Offset: 0x0010D43C
		private void Start()
		{
			if (!Dedicator.isDedicated)
			{
				PlayerStance stance = base.player.stance;
				stance.onStanceUpdated = (StanceUpdated)Delegate.Combine(stance.onStanceUpdated, new StanceUpdated(this.onStanceUpdated));
			}
			if (base.channel.isOwner)
			{
				if (base.player.first != null)
				{
					this.firstClothes = base.player.first.FindChild("Camera").FindChild("Viewmodel").GetComponent<HumanClothes>();
					this.firstClothes.isMine = true;
				}
				if (base.player.third != null)
				{
					this.thirdClothes = base.player.third.GetComponent<HumanClothes>();
				}
				if (base.player.character != null)
				{
					this.characterClothes = base.player.character.GetComponent<HumanClothes>();
				}
			}
			else if (base.player.third != null)
			{
				this.thirdClothes = base.player.third.GetComponent<HumanClothes>();
			}
			if (this.firstClothes != null)
			{
				this.firstClothes.visualShirt = base.channel.owner.shirtItem;
				this.firstClothes.hand = base.channel.owner.hand;
			}
			if (this.thirdClothes != null)
			{
				this.thirdClothes.visualShirt = base.channel.owner.shirtItem;
				this.thirdClothes.visualPants = base.channel.owner.pantsItem;
				this.thirdClothes.visualHat = base.channel.owner.hatItem;
				this.thirdClothes.visualBackpack = base.channel.owner.backpackItem;
				this.thirdClothes.visualVest = base.channel.owner.vestItem;
				this.thirdClothes.visualMask = base.channel.owner.maskItem;
				this.thirdClothes.visualGlasses = base.channel.owner.glassesItem;
				this.thirdClothes.hand = base.channel.owner.hand;
			}
			if (this.characterClothes != null)
			{
				this.characterClothes.visualShirt = base.channel.owner.shirtItem;
				this.characterClothes.visualPants = base.channel.owner.pantsItem;
				this.characterClothes.visualHat = base.channel.owner.hatItem;
				this.characterClothes.visualBackpack = base.channel.owner.backpackItem;
				this.characterClothes.visualVest = base.channel.owner.vestItem;
				this.characterClothes.visualMask = base.channel.owner.maskItem;
				this.characterClothes.visualGlasses = base.channel.owner.glassesItem;
				this.characterClothes.hand = base.channel.owner.hand;
			}
			this.isSkinned = true;
			if (Provider.isServer)
			{
				this.load();
				PlayerLife life = base.player.life;
				life.onLifeUpdated = (LifeUpdated)Delegate.Combine(life.onLifeUpdated, new LifeUpdated(this.onLifeUpdated));
			}
			if (Provider.isClient)
			{
				base.Invoke("init", 0.1f);
			}
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x0010F3DC File Offset: 0x0010D7DC
		public void load()
		{
			this.thirdClothes.visualShirt = base.channel.owner.shirtItem;
			this.thirdClothes.visualPants = base.channel.owner.pantsItem;
			this.thirdClothes.visualHat = base.channel.owner.hatItem;
			this.thirdClothes.visualBackpack = base.channel.owner.backpackItem;
			this.thirdClothes.visualVest = base.channel.owner.vestItem;
			this.thirdClothes.visualMask = base.channel.owner.maskItem;
			this.thirdClothes.visualGlasses = base.channel.owner.glassesItem;
			if (PlayerSavedata.fileExists(base.channel.owner.playerID, "/Player/Clothing.dat") && Level.info.type == ELevelType.SURVIVAL)
			{
				Block block = PlayerSavedata.readBlock(base.channel.owner.playerID, "/Player/Clothing.dat", 0);
				byte b = block.readByte();
				if (b > 1)
				{
					this.thirdClothes.shirt = block.readUInt16();
					this.shirtQuality = block.readByte();
					this.thirdClothes.pants = block.readUInt16();
					this.pantsQuality = block.readByte();
					this.thirdClothes.hat = block.readUInt16();
					this.hatQuality = block.readByte();
					this.thirdClothes.backpack = block.readUInt16();
					this.backpackQuality = block.readByte();
					this.thirdClothes.vest = block.readUInt16();
					this.vestQuality = block.readByte();
					this.thirdClothes.mask = block.readUInt16();
					this.maskQuality = block.readByte();
					this.thirdClothes.glasses = block.readUInt16();
					this.glassesQuality = block.readByte();
					if (b > 2)
					{
						this.thirdClothes.isVisual = block.readBoolean();
					}
					if (b > 5)
					{
						this.isSkinned = block.readBoolean();
						this.thirdClothes.isMythic = block.readBoolean();
					}
					else
					{
						this.isSkinned = true;
						this.thirdClothes.isMythic = true;
					}
					if (b > 4)
					{
						this.shirtState = block.readByteArray();
						this.pantsState = block.readByteArray();
						this.hatState = block.readByteArray();
						this.backpackState = block.readByteArray();
						this.vestState = block.readByteArray();
						this.maskState = block.readByteArray();
						this.glassesState = block.readByteArray();
					}
					else
					{
						this.shirtState = new byte[0];
						this.pantsState = new byte[0];
						this.hatState = new byte[0];
						this.backpackState = new byte[0];
						this.vestState = new byte[0];
						this.maskState = new byte[0];
						this.glassesState = new byte[0];
						if (this.glasses == 334)
						{
							this.glassesState = new byte[1];
						}
					}
					this.thirdClothes.apply();
					return;
				}
			}
			this.thirdClothes.shirt = 0;
			this.shirtQuality = 0;
			this.thirdClothes.pants = 0;
			this.pantsQuality = 0;
			this.thirdClothes.hat = 0;
			this.hatQuality = 0;
			this.thirdClothes.backpack = 0;
			this.backpackQuality = 0;
			this.thirdClothes.vest = 0;
			this.vestQuality = 0;
			this.thirdClothes.mask = 0;
			this.maskQuality = 0;
			this.thirdClothes.glasses = 0;
			this.glassesQuality = 0;
			this.shirtState = new byte[0];
			this.pantsState = new byte[0];
			this.hatState = new byte[0];
			this.backpackState = new byte[0];
			this.vestState = new byte[0];
			this.maskState = new byte[0];
			this.maskState = new byte[0];
			this.thirdClothes.apply();
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x0010F7E0 File Offset: 0x0010DBE0
		public void save()
		{
			bool flag = (!base.player.life.wasPvPDeath) ? Provider.modeConfigData.Players.Lose_Clothes_PvE : Provider.modeConfigData.Players.Lose_Clothes_PvP;
			if ((base.player.life.isDead && flag) || this.thirdClothes == null)
			{
				if (PlayerSavedata.fileExists(base.channel.owner.playerID, "/Player/Clothing.dat"))
				{
					PlayerSavedata.deleteFile(base.channel.owner.playerID, "/Player/Clothing.dat");
				}
			}
			else
			{
				Block block = new Block();
				block.writeByte(PlayerClothing.SAVEDATA_VERSION);
				block.writeUInt16(this.thirdClothes.shirt);
				block.writeByte(this.shirtQuality);
				block.writeUInt16(this.thirdClothes.pants);
				block.writeByte(this.pantsQuality);
				block.writeUInt16(this.thirdClothes.hat);
				block.writeByte(this.hatQuality);
				block.writeUInt16(this.thirdClothes.backpack);
				block.writeByte(this.backpackQuality);
				block.writeUInt16(this.thirdClothes.vest);
				block.writeByte(this.vestQuality);
				block.writeUInt16(this.thirdClothes.mask);
				block.writeByte(this.maskQuality);
				block.writeUInt16(this.thirdClothes.glasses);
				block.writeByte(this.glassesQuality);
				block.writeBoolean(this.isVisual);
				block.writeBoolean(this.isSkinned);
				block.writeBoolean(this.isMythic);
				block.writeByteArray(this.shirtState);
				block.writeByteArray(this.pantsState);
				block.writeByteArray(this.hatState);
				block.writeByteArray(this.backpackState);
				block.writeByteArray(this.vestState);
				block.writeByteArray(this.maskState);
				block.writeByteArray(this.glassesState);
				PlayerSavedata.writeBlock(base.channel.owner.playerID, "/Player/Clothing.dat", block);
			}
		}

		// Token: 0x04001BCE RID: 7118
		public static readonly byte SAVEDATA_VERSION = 6;

		// Token: 0x04001BCF RID: 7119
		public ShirtUpdated onShirtUpdated;

		// Token: 0x04001BD0 RID: 7120
		public PantsUpdated onPantsUpdated;

		// Token: 0x04001BD1 RID: 7121
		public HatUpdated onHatUpdated;

		// Token: 0x04001BD2 RID: 7122
		public BackpackUpdated onBackpackUpdated;

		// Token: 0x04001BD3 RID: 7123
		public VestUpdated onVestUpdated;

		// Token: 0x04001BD4 RID: 7124
		public MaskUpdated onMaskUpdated;

		// Token: 0x04001BD5 RID: 7125
		public GlassesUpdated onGlassesUpdated;

		// Token: 0x04001BDA RID: 7130
		private float lastFaceSwap;

		// Token: 0x04001BDB RID: 7131
		private float lastVisualToggle;

		// Token: 0x04001BDD RID: 7133
		public byte shirtQuality;

		// Token: 0x04001BDE RID: 7134
		public byte pantsQuality;

		// Token: 0x04001BDF RID: 7135
		public byte hatQuality;

		// Token: 0x04001BE0 RID: 7136
		public byte backpackQuality;

		// Token: 0x04001BE1 RID: 7137
		public byte vestQuality;

		// Token: 0x04001BE2 RID: 7138
		public byte maskQuality;

		// Token: 0x04001BE3 RID: 7139
		public byte glassesQuality;

		// Token: 0x04001BE4 RID: 7140
		public byte[] shirtState;

		// Token: 0x04001BE5 RID: 7141
		public byte[] pantsState;

		// Token: 0x04001BE6 RID: 7142
		public byte[] hatState;

		// Token: 0x04001BE7 RID: 7143
		public byte[] backpackState;

		// Token: 0x04001BE8 RID: 7144
		public byte[] vestState;

		// Token: 0x04001BE9 RID: 7145
		public byte[] maskState;

		// Token: 0x04001BEA RID: 7146
		public byte[] glassesState;
	}
}
