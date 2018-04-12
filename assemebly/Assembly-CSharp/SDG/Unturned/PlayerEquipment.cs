using System;
using System.Collections.Generic;
using SDG.Provider;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200060D RID: 1549
	public class PlayerEquipment : PlayerCaller
	{
		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x001109C3 File Offset: 0x0010EDC3
		public ushort itemID
		{
			get
			{
				return this._itemID;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x001109D4 File Offset: 0x0010EDD4
		// (set) Token: 0x06002B90 RID: 11152 RVA: 0x001109CB File Offset: 0x0010EDCB
		public byte[] state
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002B92 RID: 11154 RVA: 0x001109DC File Offset: 0x0010EDDC
		// (set) Token: 0x06002B93 RID: 11155 RVA: 0x001109E4 File Offset: 0x0010EDE4
		public byte quality
		{
			get
			{
				return this._quality;
			}
			set
			{
				if (this.isTurret)
				{
					return;
				}
				this._quality = value;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002B94 RID: 11156 RVA: 0x001109F9 File Offset: 0x0010EDF9
		public Transform firstModel
		{
			get
			{
				return this._firstModel;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002B95 RID: 11157 RVA: 0x00110A01 File Offset: 0x0010EE01
		public Transform thirdModel
		{
			get
			{
				return this._thirdModel;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002B96 RID: 11158 RVA: 0x00110A09 File Offset: 0x0010EE09
		public Transform characterModel
		{
			get
			{
				return this._characterModel;
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002B97 RID: 11159 RVA: 0x00110A11 File Offset: 0x0010EE11
		public ItemAsset asset
		{
			get
			{
				return this._asset;
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x00110A19 File Offset: 0x0010EE19
		public Useable useable
		{
			get
			{
				return this._useable;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06002B99 RID: 11161 RVA: 0x00110A21 File Offset: 0x0010EE21
		public Transform thirdPrimaryMeleeSlot
		{
			get
			{
				return this._thirdPrimaryMeleeSlot;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002B9A RID: 11162 RVA: 0x00110A29 File Offset: 0x0010EE29
		public Transform thirdPrimaryLargeGunSlot
		{
			get
			{
				return this._thirdPrimaryLargeGunSlot;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x00110A31 File Offset: 0x0010EE31
		public Transform thirdPrimarySmallGunSlot
		{
			get
			{
				return this._thirdPrimarySmallGunSlot;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002B9C RID: 11164 RVA: 0x00110A39 File Offset: 0x0010EE39
		public Transform thirdSecondaryMeleeSlot
		{
			get
			{
				return this._thirdSecondaryMeleeSlot;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06002B9D RID: 11165 RVA: 0x00110A41 File Offset: 0x0010EE41
		public Transform thirdSecondaryGunSlot
		{
			get
			{
				return this._thirdSecondaryGunSlot;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06002B9E RID: 11166 RVA: 0x00110A49 File Offset: 0x0010EE49
		public Transform characterPrimaryMeleeSlot
		{
			get
			{
				return this._characterPrimaryMeleeSlot;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002B9F RID: 11167 RVA: 0x00110A51 File Offset: 0x0010EE51
		public Transform characterPrimaryLargeGunSlot
		{
			get
			{
				return this._characterPrimaryLargeGunSlot;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x00110A59 File Offset: 0x0010EE59
		public Transform characterPrimarySmallGunSlot
		{
			get
			{
				return this._characterPrimarySmallGunSlot;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002BA1 RID: 11169 RVA: 0x00110A61 File Offset: 0x0010EE61
		public Transform characterSecondaryMeleeSlot
		{
			get
			{
				return this._characterSecondaryMeleeSlot;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x00110A69 File Offset: 0x0010EE69
		public Transform characterSecondaryGunSlot
		{
			get
			{
				return this._characterSecondaryGunSlot;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x00110A71 File Offset: 0x0010EE71
		public Transform firstLeftHook
		{
			get
			{
				return this._firstLeftHook;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x00110A79 File Offset: 0x0010EE79
		public Transform firstRightHook
		{
			get
			{
				return this._firstRightHook;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x00110A81 File Offset: 0x0010EE81
		public Transform thirdLeftHook
		{
			get
			{
				return this._thirdLeftHook;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002BA6 RID: 11174 RVA: 0x00110A89 File Offset: 0x0010EE89
		public Transform thirdRightHook
		{
			get
			{
				return this._thirdRightHook;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002BA7 RID: 11175 RVA: 0x00110A91 File Offset: 0x0010EE91
		public Transform characterLeftHook
		{
			get
			{
				return this._characterLeftHook;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x00110A99 File Offset: 0x0010EE99
		public Transform characterRightHook
		{
			get
			{
				return this._characterRightHook;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x00110AA1 File Offset: 0x0010EEA1
		public HotkeyInfo[] hotkeys
		{
			get
			{
				return this._hotkeys;
			}
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x00110AAC File Offset: 0x0010EEAC
		public bool isItemHotkeyed(byte page, byte index, ItemJar jar, out byte button)
		{
			if (page < PlayerInventory.SLOTS)
			{
				button = page;
				return true;
			}
			byte b = 0;
			while ((int)b < this.hotkeys.Length)
			{
				HotkeyInfo hotkeyInfo = this.hotkeys[(int)b];
				if (hotkeyInfo.page == page && hotkeyInfo.x == jar.x && hotkeyInfo.y == jar.y && hotkeyInfo.id == jar.item.id)
				{
					button = b + 2;
					return true;
				}
				b += 1;
			}
			button = 0;
			return false;
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06002BAB RID: 11179 RVA: 0x00110B3F File Offset: 0x0010EF3F
		public bool isSelected
		{
			get
			{
				return this.thirdModel != null && this.useable != null;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002BAC RID: 11180 RVA: 0x00110B61 File Offset: 0x0010EF61
		public bool isEquipped
		{
			get
			{
				return Time.realtimeSinceStartup - this.lastEquipped > this.equippedTime;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002BAD RID: 11181 RVA: 0x00110B77 File Offset: 0x0010EF77
		// (set) Token: 0x06002BAE RID: 11182 RVA: 0x00110B7F File Offset: 0x0010EF7F
		public bool isTurret { get; private set; }

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x00110B88 File Offset: 0x0010EF88
		public byte equippedPage
		{
			get
			{
				return this._equippedPage;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002BB0 RID: 11184 RVA: 0x00110B90 File Offset: 0x0010EF90
		public byte equipped_x
		{
			get
			{
				return this._equipped_x;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002BB1 RID: 11185 RVA: 0x00110B98 File Offset: 0x0010EF98
		public byte equipped_y
		{
			get
			{
				return this._equipped_y;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002BB2 RID: 11186 RVA: 0x00110BA0 File Offset: 0x0010EFA0
		public bool primary
		{
			get
			{
				return this._primary;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002BB3 RID: 11187 RVA: 0x00110BA8 File Offset: 0x0010EFA8
		public bool secondary
		{
			get
			{
				return this._secondary;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06002BB4 RID: 11188 RVA: 0x00110BB0 File Offset: 0x0010EFB0
		// (set) Token: 0x06002BB5 RID: 11189 RVA: 0x00110BB8 File Offset: 0x0010EFB8
		public float lastPunching { get; private set; }

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x00110BC1 File Offset: 0x0010EFC1
		public bool isInspecting
		{
			get
			{
				return Time.realtimeSinceStartup - PlayerEquipment.lastInspect < PlayerEquipment.inspectTime;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002BB7 RID: 11191 RVA: 0x00110BD8 File Offset: 0x0010EFD8
		public bool canInspect
		{
			get
			{
				return this.isSelected && this.isEquipped && !this.isBusy && base.player.animator.checkExists("Inspect") && !this.isInspecting && this.useable.canInspect;
			}
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x00110C39 File Offset: 0x0010F039
		public bool getUseableStatTrackerValue(out EStatTrackerType type, out int kills)
		{
			return base.channel.owner.getStatTrackerValue(this.itemID, out type, out kills);
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x00110C54 File Offset: 0x0010F054
		protected bool getSlot0StatTrackerValue(out EStatTrackerType type, out int kills)
		{
			ItemJar item = base.player.inventory.getItem(0, 0);
			if (item != null)
			{
				return base.channel.owner.getStatTrackerValue(item.item.id, out type, out kills);
			}
			type = EStatTrackerType.NONE;
			kills = -1;
			return false;
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x00110CA0 File Offset: 0x0010F0A0
		protected bool getSlot1StatTrackerValue(out EStatTrackerType type, out int kills)
		{
			ItemJar item = base.player.inventory.getItem(1, 0);
			if (item != null)
			{
				return base.channel.owner.getStatTrackerValue(item.item.id, out type, out kills);
			}
			type = EStatTrackerType.NONE;
			kills = -1;
			return false;
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x00110CEC File Offset: 0x0010F0EC
		protected void fixStatTrackerHookScale(Transform itemModelTransform)
		{
			if (!base.channel.owner.hand)
			{
				return;
			}
			Transform transform = itemModelTransform.FindChild("Stat_Tracker");
			if (!transform)
			{
				return;
			}
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x00110D60 File Offset: 0x0010F160
		public void inspect()
		{
			base.player.animator.setAnimationSpeed("Inspect", 1f);
			PlayerEquipment.lastInspect = Time.realtimeSinceStartup;
			PlayerEquipment.inspectTime = base.player.animator.getAnimationLength("Inspect");
			base.player.animator.play("Inspect", false);
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x00110DC1 File Offset: 0x0010F1C1
		public void uninspect()
		{
			base.player.animator.setAnimationSpeed("Inspect", float.MaxValue);
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x00110DDD File Offset: 0x0010F1DD
		public bool checkSelection(byte page)
		{
			return page == this.equippedPage;
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x00110DE8 File Offset: 0x0010F1E8
		public bool checkSelection(byte page, byte x, byte y)
		{
			return page == this.equippedPage && x == this.equipped_x && y == this.equipped_y;
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x00110E10 File Offset: 0x0010F210
		public void applySkinVisual()
		{
			if (this.firstModel != null && this.firstSkinned != base.player.clothing.isSkinned)
			{
				this.firstSkinned = base.player.clothing.isSkinned;
				if (this.tempFirstMaterial != null)
				{
					Attachments component = this.firstModel.GetComponent<Attachments>();
					if (component != null)
					{
						component.isSkinned = this.firstSkinned;
						component.applyVisual();
					}
					if (this.tempFirstMesh.Count > 0)
					{
						HighlighterTool.remesh(this.firstModel, this.tempFirstMesh, this.tempFirstMesh, false);
					}
					HighlighterTool.rematerialize(this.firstModel, this.tempFirstMaterial, out this.tempFirstMaterial);
				}
			}
			if (this.thirdModel != null && this.thirdSkinned != base.player.clothing.isSkinned)
			{
				this.thirdSkinned = base.player.clothing.isSkinned;
				if (this.tempThirdMaterial != null)
				{
					Attachments component2 = this.thirdModel.GetComponent<Attachments>();
					if (component2 != null)
					{
						component2.isSkinned = this.thirdSkinned;
						component2.applyVisual();
					}
					if (this.tempThirdMesh.Count > 0)
					{
						HighlighterTool.remesh(this.thirdModel, this.tempThirdMesh, this.tempThirdMesh, false);
					}
					HighlighterTool.rematerialize(this.thirdModel, this.tempThirdMaterial, out this.tempThirdMaterial);
				}
			}
			if (this.characterModel != null && this.characterSkinned != base.player.clothing.isSkinned)
			{
				this.characterSkinned = base.player.clothing.isSkinned;
				if (this.tempCharacterMaterial != null)
				{
					Attachments component3 = this.characterModel.GetComponent<Attachments>();
					if (component3 != null)
					{
						component3.isSkinned = this.characterSkinned;
						component3.applyVisual();
					}
					if (this.tempCharacterMesh.Count > 0)
					{
						HighlighterTool.remesh(this.characterModel, this.tempCharacterMesh, this.tempCharacterMesh, false);
					}
					HighlighterTool.rematerialize(this.characterModel, this.tempCharacterMaterial, out this.tempCharacterMaterial);
				}
			}
			if (this.thirdSlots != null)
			{
				byte b = 0;
				while ((int)b < this.thirdSlots.Length)
				{
					if (this.thirdSlots[(int)b] != null && this.thirdSkinneds[(int)b] != base.player.clothing.isSkinned)
					{
						this.thirdSkinneds[(int)b] = base.player.clothing.isSkinned;
						if (this.tempThirdMaterials[(int)b] != null)
						{
							Attachments component4 = this.thirdSlots[(int)b].GetComponent<Attachments>();
							if (component4 != null)
							{
								component4.isSkinned = this.thirdSkinneds[(int)b];
								component4.applyVisual();
							}
							if (this.tempThirdMeshes[(int)b].Count > 0)
							{
								HighlighterTool.remesh(this.thirdSlots[(int)b], this.tempThirdMeshes[(int)b], this.tempThirdMeshes[(int)b], false);
							}
							HighlighterTool.rematerialize(this.thirdSlots[(int)b], this.tempThirdMaterials[(int)b], out this.tempThirdMaterials[(int)b]);
						}
					}
					if (this.characterSlots != null && this.characterSlots[(int)b] != null && this.characterSkinneds[(int)b] != base.player.clothing.isSkinned)
					{
						this.characterSkinneds[(int)b] = base.player.clothing.isSkinned;
						if (this.tempCharacterMaterials[(int)b] != null)
						{
							Attachments component5 = this.characterSlots[(int)b].GetComponent<Attachments>();
							if (component5 != null)
							{
								component5.isSkinned = this.characterSkinneds[(int)b];
								component5.applyVisual();
							}
							if (this.tempCharacterMeshes[(int)b].Count > 0)
							{
								HighlighterTool.remesh(this.characterSlots[(int)b], this.tempCharacterMeshes[(int)b], this.tempCharacterMeshes[(int)b], false);
							}
							HighlighterTool.rematerialize(this.characterSlots[(int)b], this.tempCharacterMaterials[(int)b], out this.tempCharacterMaterials[(int)b]);
						}
					}
					b += 1;
				}
			}
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x00111244 File Offset: 0x0010F644
		public void applyMythicVisual()
		{
			if (this.firstMythic != null)
			{
				this.firstMythic.isMythic = (base.player.clothing.isSkinned && base.player.clothing.isMythic);
			}
			if (this.thirdMythic != null)
			{
				this.thirdMythic.isMythic = (base.player.clothing.isSkinned && base.player.clothing.isMythic);
			}
			if (this.characterMythic != null)
			{
				this.characterMythic.isMythic = (base.player.clothing.isSkinned && base.player.clothing.isMythic);
			}
			if (this.thirdSlots != null)
			{
				byte b = 0;
				while ((int)b < this.thirdSlots.Length)
				{
					if (this.thirdMythics[(int)b] != null)
					{
						this.thirdMythics[(int)b].isMythic = (base.player.clothing.isSkinned && base.player.clothing.isMythic);
					}
					if (this.characterSlots != null && this.characterMythics[(int)b] != null)
					{
						this.characterMythics[(int)b].isMythic = (base.player.clothing.isSkinned && base.player.clothing.isMythic);
					}
					b += 1;
				}
			}
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x001113E0 File Offset: 0x0010F7E0
		private void updateSlot(byte slot, ushort id, byte[] state)
		{
			if (!Dedicator.isDedicated)
			{
				if (this.thirdSlots == null)
				{
					return;
				}
				if (this.thirdSlots[(int)slot] != null)
				{
					UnityEngine.Object.Destroy(this.thirdSlots[(int)slot].gameObject);
					this.thirdSkinneds[(int)slot] = false;
					this.tempThirdMaterials[(int)slot] = null;
					this.thirdMythics[(int)slot] = null;
				}
				if (this.characterSlots != null && this.characterSlots[(int)slot] != null)
				{
					UnityEngine.Object.Destroy(this.characterSlots[(int)slot].gameObject);
					this.characterSkinneds[(int)slot] = false;
					this.tempCharacterMaterials[(int)slot] = null;
					this.characterMythics[(int)slot] = null;
				}
				if (base.channel.isOwner)
				{
					if (slot == 0)
					{
						Characters.active.primaryItem = id;
						Characters.active.primaryState = state;
					}
					else if (slot == 1)
					{
						Characters.active.secondaryItem = id;
						Characters.active.secondaryState = state;
					}
				}
				if (id == 0)
				{
					return;
				}
				ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, id);
				if (itemAsset != null)
				{
					int item = 0;
					ushort skin = 0;
					ushort num = 0;
					if (base.channel.owner.skinItems != null && base.channel.owner.itemSkins != null && base.channel.owner.itemSkins.TryGetValue(id, out item))
					{
						skin = Provider.provider.economyService.getInventorySkinID(item);
						num = Provider.provider.economyService.getInventoryMythicID(item);
					}
					GetStatTrackerValueHandler statTrackerCallback = null;
					if (slot == 0)
					{
						statTrackerCallback = new GetStatTrackerValueHandler(this.getSlot0StatTrackerValue);
					}
					else if (slot == 1)
					{
						statTrackerCallback = new GetStatTrackerValueHandler(this.getSlot1StatTrackerValue);
					}
					Transform item2 = ItemTool.getItem(id, skin, 100, state, false, itemAsset, this.tempThirdMeshes[(int)slot], out this.tempThirdMaterials[(int)slot], statTrackerCallback);
					this.fixStatTrackerHookScale(item2);
					if (slot == 0)
					{
						if (itemAsset.type == EItemType.MELEE)
						{
							item2.transform.parent = this.thirdPrimaryMeleeSlot;
						}
						else if (itemAsset.slot == ESlotType.PRIMARY)
						{
							item2.transform.parent = this.thirdPrimaryLargeGunSlot;
						}
						else
						{
							item2.transform.parent = this.thirdPrimarySmallGunSlot;
						}
					}
					else if (slot == 1)
					{
						if (itemAsset.type == EItemType.MELEE)
						{
							item2.transform.parent = this.thirdSecondaryMeleeSlot;
						}
						else
						{
							item2.transform.parent = this.thirdSecondaryGunSlot;
						}
					}
					item2.localPosition = Vector3.zero;
					item2.localRotation = Quaternion.Euler(0f, 0f, 90f);
					item2.localScale = Vector3.one;
					item2.gameObject.SetActive(false);
					item2.gameObject.SetActive(true);
					UnityEngine.Object.Destroy(item2.GetComponent<Collider>());
					Layerer.enemy(item2);
					if (num != 0)
					{
						Transform transform = ItemTool.applyEffect(item2, num, EEffectType.THIRD);
						if (transform != null)
						{
							this.thirdMythics[(int)slot] = transform.GetComponent<MythicLockee>();
						}
					}
					this.thirdSlots[(int)slot] = item2;
					this.thirdSkinneds[(int)slot] = true;
					this.applySkinVisual();
					if (this.thirdMythics[(int)slot] != null)
					{
						this.thirdMythics[(int)slot].isMythic = (base.player.clothing.isSkinned && base.player.clothing.isMythic);
					}
					if (this.characterSlots != null)
					{
						item2 = ItemTool.getItem(id, skin, 100, state, false, itemAsset, this.tempCharacterMeshes[(int)slot], out this.tempCharacterMaterials[(int)slot], statTrackerCallback);
						this.fixStatTrackerHookScale(item2);
						if (slot == 0)
						{
							if (itemAsset.type == EItemType.MELEE)
							{
								item2.transform.parent = this.characterPrimaryMeleeSlot;
							}
							else if (itemAsset.slot == ESlotType.PRIMARY)
							{
								item2.transform.parent = this.characterPrimaryLargeGunSlot;
							}
							else
							{
								item2.transform.parent = this.characterPrimarySmallGunSlot;
							}
						}
						else if (slot == 1)
						{
							if (itemAsset.type == EItemType.MELEE)
							{
								item2.transform.parent = this.characterSecondaryMeleeSlot;
							}
							else
							{
								item2.transform.parent = this.characterSecondaryGunSlot;
							}
						}
						item2.localPosition = Vector3.zero;
						item2.localRotation = Quaternion.Euler(0f, 0f, 90f);
						item2.localScale = Vector3.one;
						item2.gameObject.SetActive(false);
						item2.gameObject.SetActive(true);
						UnityEngine.Object.Destroy(item2.GetComponent<Collider>());
						Layerer.enemy(item2);
						if (num != 0)
						{
							Transform transform2 = ItemTool.applyEffect(item2, num, EEffectType.THIRD);
							if (transform2 != null)
							{
								this.characterMythics[(int)slot] = transform2.GetComponent<MythicLockee>();
							}
						}
						this.characterSlots[(int)slot] = item2;
						this.characterSkinneds[(int)slot] = true;
						this.applySkinVisual();
						if (this.characterMythics[(int)slot] != null)
						{
							this.characterMythics[(int)slot].isMythic = (base.player.clothing.isSkinned && base.player.clothing.isMythic);
						}
					}
				}
			}
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x00111918 File Offset: 0x0010FD18
		[SteamCall]
		public void askToggleVision(CSteamID steamID)
		{
			if (base.channel.checkOwner(steamID))
			{
				if (!base.player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if (!this.hasVision)
				{
					return;
				}
				if (base.player.clothing.glassesState.Length != 1)
				{
					return;
				}
				base.channel.send("tellToggleVision", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
				if (base.player.clothing.glassesAsset != null)
				{
					if (base.player.clothing.glassesAsset.vision == ELightingVision.HEADLAMP)
					{
						EffectManager.sendEffect(8, EffectManager.SMALL, base.transform.position);
					}
					else if (base.player.clothing.glassesAsset.vision == ELightingVision.CIVILIAN || base.player.clothing.glassesAsset.vision == ELightingVision.MILITARY)
					{
						EffectManager.sendEffect(56, EffectManager.SMALL, base.transform.position);
					}
				}
			}
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x00111A20 File Offset: 0x0010FE20
		[SteamCall]
		public void tellToggleVision(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID))
			{
				if (!this.hasVision)
				{
					return;
				}
				if (base.player.clothing.glassesState.Length != 1)
				{
					return;
				}
				base.player.clothing.glassesState[0] = ((base.player.clothing.glassesState[0] != 0) ? 0 : 1);
				this.updateVision();
			}
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x00111A9A File Offset: 0x0010FE9A
		[SteamCall]
		public void tellSlot(CSteamID steamID, byte slot, ushort id, byte[] state)
		{
			if (base.channel.checkServer(steamID))
			{
				this.updateSlot(slot, id, state);
			}
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x00111AB7 File Offset: 0x0010FEB7
		[SteamCall]
		public void tellUpdateStateTemp(CSteamID steamID, byte[] newState)
		{
			if (base.channel.checkServer(steamID))
			{
				this._state = newState;
				if (this.useable != null)
				{
					this.useable.updateState(this.state);
				}
			}
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x00111AF4 File Offset: 0x0010FEF4
		[SteamCall]
		public void tellUpdateState(CSteamID steamID, byte page, byte index, byte[] newState)
		{
			if (base.channel.checkServer(steamID))
			{
				if (this.thirdSlots == null)
				{
					return;
				}
				this._state = newState;
				if (this.slot != 255 && (int)this.slot < this.thirdSlots.Length && this.thirdSlots[(int)this.slot] != null)
				{
					this.updateSlot(this.slot, this.itemID, newState);
					this.thirdSlots[(int)this.slot].gameObject.SetActive(false);
					if (this.characterSlots != null)
					{
						this.characterSlots[(int)this.slot].gameObject.SetActive(false);
					}
				}
				if (base.channel.isOwner || Provider.isServer)
				{
					base.player.inventory.updateState(page, index, this.state);
				}
				if (this.useable != null)
				{
					this.useable.updateState(this.state);
				}
				if (this.characterModel != null)
				{
					UnityEngine.Object.Destroy(this.characterModel.gameObject);
					ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, this.itemID);
					if (itemAsset != null)
					{
						int item = 0;
						ushort skin = 0;
						ushort num = 0;
						if (base.channel.owner.skinItems != null && base.channel.owner.itemSkins != null && base.channel.owner.itemSkins.TryGetValue(this.itemID, out item))
						{
							skin = Provider.provider.economyService.getInventorySkinID(item);
							num = Provider.provider.economyService.getInventoryMythicID(item);
						}
						GetStatTrackerValueHandler statTrackerCallback = null;
						if (this.slot == 0)
						{
							statTrackerCallback = new GetStatTrackerValueHandler(this.getSlot0StatTrackerValue);
						}
						else if (this.slot == 1)
						{
							statTrackerCallback = new GetStatTrackerValueHandler(this.getSlot1StatTrackerValue);
						}
						this._characterModel = ItemTool.getItem(this.itemID, skin, 100, this.state, false, itemAsset, this.tempCharacterMesh, out this.tempCharacterMaterial, statTrackerCallback);
						this.fixStatTrackerHookScale(this._characterModel);
						if (itemAsset.isBackward)
						{
							this.characterModel.transform.parent = this._characterLeftHook;
						}
						else
						{
							this.characterModel.transform.parent = this._characterRightHook;
						}
						this.characterModel.localPosition = Vector3.zero;
						this.characterModel.localRotation = Quaternion.Euler(0f, 0f, 90f);
						this.characterModel.localScale = Vector3.one;
						this.characterModel.gameObject.AddComponent<Rigidbody>();
						this.characterModel.GetComponent<Rigidbody>().useGravity = false;
						this.characterModel.GetComponent<Rigidbody>().isKinematic = true;
						if (num != 0)
						{
							Transform transform = ItemTool.applyEffect(this.characterModel, num, EEffectType.THIRD);
							if (transform != null)
							{
								this.characterMythic = transform.GetComponent<MythicLockee>();
							}
						}
						this.characterSkinned = true;
						this.applySkinVisual();
						if (this.characterMythic != null)
						{
							this.characterMythic.isMythic = (base.player.clothing.isSkinned && base.player.clothing.isMythic);
						}
					}
				}
			}
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x00111E50 File Offset: 0x00110250
		[SteamCall]
		public void tellAskEquip(CSteamID steamID, byte page, byte x, byte y)
		{
			if (!base.channel.checkServer(steamID))
			{
				return;
			}
			this.wasTryingToSelect = true;
			byte index = base.player.inventory.getIndex(page, x, y);
			if (index == 255)
			{
				return;
			}
			ItemJar item = base.player.inventory.getItem(page, index);
			if (item == null)
			{
				return;
			}
			this.equip(page, x, y, item.item.id);
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x00111EC8 File Offset: 0x001102C8
		[SteamCall]
		public void tellEquip(CSteamID steamID, byte page, byte x, byte y, ushort id, byte newQuality, byte[] newState)
		{
			if (base.channel.checkServer(steamID))
			{
				if (this.thirdSlots == null)
				{
					return;
				}
				if (this.slot != 255 && (int)this.slot < this.thirdSlots.Length && this.thirdSlots[(int)this.slot] != null)
				{
					this.thirdSlots[(int)this.slot].gameObject.SetActive(true);
					if (this.characterSlots != null)
					{
						this.characterSlots[(int)this.slot].gameObject.SetActive(true);
					}
				}
				this.slot = page;
				if (this.slot != 255 && (int)this.slot < this.thirdSlots.Length && this.thirdSlots[(int)this.slot] != null)
				{
					this.thirdSlots[(int)this.slot].gameObject.SetActive(false);
					if (this.characterSlots != null)
					{
						this.characterSlots[(int)this.slot].gameObject.SetActive(false);
					}
				}
				if (this.isSelected)
				{
					this.useable.dequip();
					if (base.player.life.isDead)
					{
						UnityEngine.Object.Destroy(this.useable);
					}
					else
					{
						UnityEngine.Object.DestroyImmediate(this.useable);
					}
					this._useable = null;
					this._itemID = 0;
					if (this.firstModel != null)
					{
						UnityEngine.Object.Destroy(this.firstModel.gameObject);
					}
					this.firstSkinned = false;
					this.tempFirstMaterial = null;
					this.firstMythic = null;
					if (this.thirdModel != null)
					{
						UnityEngine.Object.Destroy(this.thirdModel.gameObject);
					}
					this.thirdSkinned = false;
					this.tempThirdMaterial = null;
					this.thirdMythic = null;
					if (this.characterModel != null)
					{
						UnityEngine.Object.Destroy(this.characterModel.gameObject);
					}
					this.characterSkinned = false;
					this.tempCharacterMaterial = null;
					this.characterMythic = null;
					for (int i = 0; i < this.asset.animations.Length; i++)
					{
						base.player.animator.removeAnimation(this.asset.animations[i]);
					}
					base.channel.build();
				}
				this.isBusy = false;
				if (id == 0)
				{
					this._equippedPage = byte.MaxValue;
					this._equipped_x = byte.MaxValue;
					this._equipped_y = byte.MaxValue;
					this._itemID = 0;
					this._asset = null;
					return;
				}
				this._equippedPage = page;
				this._equipped_x = x;
				this._equipped_y = y;
				this._asset = (ItemAsset)Assets.find(EAssetType.ITEM, id);
				Type type = Assets.useableTypes.getType(this.asset.useable);
				if (this.asset != null && type != null && typeof(Useable).IsAssignableFrom(type))
				{
					this._itemID = id;
					this.quality = newQuality;
					this._state = newState;
					int item = 0;
					ushort skin = 0;
					ushort num = 0;
					if (base.channel.owner.skinItems != null && base.channel.owner.itemSkins != null && base.channel.owner.itemSkins.TryGetValue(id, out item))
					{
						skin = Provider.provider.economyService.getInventorySkinID(item);
						num = Provider.provider.economyService.getInventoryMythicID(item);
					}
					if (base.channel.isOwner)
					{
						this._firstModel = ItemTool.getItem(id, skin, this.quality, this.state, true, this.asset, this.tempFirstMesh, out this.tempFirstMaterial, new GetStatTrackerValueHandler(this.getUseableStatTrackerValue));
						this.fixStatTrackerHookScale(this._firstModel);
						if (this.asset.isBackward)
						{
							this.firstModel.transform.parent = this.firstLeftHook;
						}
						else
						{
							this.firstModel.transform.parent = this.firstRightHook;
						}
						this.firstModel.localPosition = Vector3.zero;
						this.firstModel.localRotation = Quaternion.Euler(0f, 0f, 90f);
						this.firstModel.localScale = Vector3.one;
						this.firstModel.gameObject.SetActive(false);
						this.firstModel.gameObject.SetActive(true);
						this.firstModel.gameObject.AddComponent<Rigidbody>();
						this.firstModel.GetComponent<Rigidbody>().useGravity = false;
						this.firstModel.GetComponent<Rigidbody>().isKinematic = true;
						UnityEngine.Object.Destroy(this.firstModel.GetComponent<Collider>());
						Layerer.viewmodel(this.firstModel);
						if (num != 0)
						{
							Transform transform = ItemTool.applyEffect(this.firstModel, num, EEffectType.FIRST);
							if (transform != null)
							{
								this.firstMythic = transform.GetComponent<MythicLockee>();
							}
						}
						this.firstSkinned = true;
						this.applySkinVisual();
						if (this.firstMythic != null)
						{
							this.firstMythic.isMythic = (base.player.clothing.isSkinned && base.player.clothing.isMythic);
						}
						this._characterModel = ItemTool.getItem(id, skin, this.quality, this.state, false, this.asset, this.tempCharacterMesh, out this.tempCharacterMaterial, new GetStatTrackerValueHandler(this.getUseableStatTrackerValue));
						this.fixStatTrackerHookScale(this._characterModel);
						if (this.asset.isBackward)
						{
							this.characterModel.transform.parent = this.characterLeftHook;
						}
						else
						{
							this.characterModel.transform.parent = this.characterRightHook;
						}
						this.characterModel.localPosition = Vector3.zero;
						this.characterModel.localRotation = Quaternion.Euler(0f, 0f, 90f);
						this.characterModel.localScale = Vector3.one;
						this.characterModel.gameObject.AddComponent<Rigidbody>();
						this.characterModel.GetComponent<Rigidbody>().useGravity = false;
						this.characterModel.GetComponent<Rigidbody>().isKinematic = true;
						if (num != 0)
						{
							Transform transform2 = ItemTool.applyEffect(this.characterModel, num, EEffectType.THIRD);
							if (transform2 != null)
							{
								this.characterMythic = transform2.GetComponent<MythicLockee>();
							}
						}
						this.characterSkinned = true;
						this.applySkinVisual();
						if (this.characterMythic != null)
						{
							this.characterMythic.isMythic = (base.player.clothing.isSkinned && base.player.clothing.isMythic);
						}
					}
					this._thirdModel = ItemTool.getItem(id, skin, this.quality, this.state, false, this.asset, this.tempThirdMesh, out this.tempThirdMaterial, new GetStatTrackerValueHandler(this.getUseableStatTrackerValue));
					this.fixStatTrackerHookScale(this._thirdModel);
					if (this.asset.isBackward)
					{
						this.thirdModel.transform.parent = this.thirdLeftHook;
					}
					else
					{
						this.thirdModel.transform.parent = this.thirdRightHook;
					}
					this.thirdModel.localPosition = Vector3.zero;
					this.thirdModel.localRotation = Quaternion.Euler(0f, 0f, 90f);
					this.thirdModel.localScale = Vector3.one;
					this.thirdModel.gameObject.SetActive(false);
					this.thirdModel.gameObject.SetActive(true);
					this.thirdModel.gameObject.AddComponent<Rigidbody>();
					this.thirdModel.GetComponent<Rigidbody>().useGravity = false;
					this.thirdModel.GetComponent<Rigidbody>().isKinematic = true;
					UnityEngine.Object.Destroy(this.thirdModel.GetComponent<Collider>());
					Layerer.enemy(this.thirdModel);
					if (num != 0)
					{
						Transform transform3 = ItemTool.applyEffect(this.thirdModel, num, EEffectType.THIRD);
						if (transform3 != null)
						{
							this.thirdMythic = transform3.GetComponent<MythicLockee>();
						}
					}
					this.thirdSkinned = true;
					this.applySkinVisual();
					if (this.thirdMythic != null)
					{
						this.thirdMythic.isMythic = (base.player.clothing.isSkinned && base.player.clothing.isMythic);
					}
					for (int j = 0; j < this.asset.animations.Length; j++)
					{
						base.player.animator.addAnimation(this.asset.animations[j]);
					}
					this._useable = (base.gameObject.AddComponent(type) as Useable);
					base.channel.build();
					this.useable.equip();
					this.lastEquipped = Time.realtimeSinceStartup;
					this.equippedTime = base.player.animator.getAnimationLength("Equip");
					if (!Dedicator.isDedicated && this.asset.equip != null)
					{
						base.player.playSound(this.asset.equip, 1f, 0.05f);
					}
				}
			}
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x00112819 File Offset: 0x00110C19
		public void tryEquip(byte page, byte x, byte y)
		{
			base.channel.send("tellAskEquip", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				page,
				x,
				y
			});
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x00112850 File Offset: 0x00110C50
		public void tryEquip(byte page, byte x, byte y, byte[] hash)
		{
			if (this.isBusy || !this.canEquip || base.player.life.isDead || base.player.stance.stance == EPlayerStance.CLIMB || base.player.stance.stance == EPlayerStance.DRIVING)
			{
				return;
			}
			if (this.isSelected && !this.isEquipped)
			{
				return;
			}
			if (this.isTurret)
			{
				return;
			}
			if ((page == this.equippedPage && x == this.equipped_x && y == this.equipped_y) || page == 255)
			{
				this.dequip();
			}
			else
			{
				if (page < 0 || page >= PlayerInventory.PAGES - 2)
				{
					return;
				}
				byte index = base.player.inventory.getIndex(page, x, y);
				if (index == 255)
				{
					return;
				}
				ItemJar item = base.player.inventory.getItem(page, index);
				if (item == null)
				{
					return;
				}
				if (ItemTool.checkUseable(page, item.item.id))
				{
					ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item.item.id);
					if (itemAsset == null)
					{
						return;
					}
					if (base.player.stance.stance == EPlayerStance.SWIM && itemAsset.slot == ESlotType.PRIMARY)
					{
						return;
					}
					if (base.player.animator.gesture == EPlayerGesture.ARREST_START)
					{
						return;
					}
					if (itemAsset.shouldVerifyHash && !Hash.verifyHash(hash, itemAsset.hash))
					{
						return;
					}
					if (item.item.state != null)
					{
						base.channel.send("tellEquip", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							page,
							x,
							y,
							item.item.id,
							item.item.quality,
							item.item.state
						});
					}
					else
					{
						base.channel.send("tellEquip", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							page,
							x,
							y,
							item.item.id,
							item.item.quality,
							new byte[0]
						});
					}
				}
			}
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x00112ACD File Offset: 0x00110ECD
		public void turretEquipClient()
		{
			this.isTurret = true;
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x00112AD8 File Offset: 0x00110ED8
		public void turretEquipServer(ushort id, byte[] state)
		{
			base.channel.send("tellEquip", ESteamCall.OTHERS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				254,
				254,
				254,
				id,
				100,
				state
			});
			this.tellEquip(Provider.server, 254, 254, 254, id, 100, state);
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x00112B5A File Offset: 0x00110F5A
		public void turretDequipClient()
		{
			this.isTurret = false;
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x00112B64 File Offset: 0x00110F64
		public void turretDequipServer()
		{
			base.channel.send("tellEquip", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				0,
				0,
				new byte[0]
			});
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x00112BCC File Offset: 0x00110FCC
		[SteamCall]
		public void askEquip(CSteamID steamID, byte page, byte x, byte y, byte[] hash)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				this.tryEquip(page, x, y, hash);
			}
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x00112BF8 File Offset: 0x00110FF8
		[SteamCall]
		public void askEquipment(CSteamID steamID)
		{
			if (Provider.isServer)
			{
				for (byte b = 0; b < PlayerInventory.SLOTS; b += 1)
				{
					ItemJar item = base.player.inventory.getItem(b, 0);
					if (item != null)
					{
						if (item.item.state != null)
						{
							base.channel.send("tellSlot", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								b,
								item.item.id,
								item.item.state
							});
						}
						else
						{
							base.channel.send("tellSlot", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								b,
								item.item.id,
								new byte[0]
							});
						}
					}
					else
					{
						base.channel.send("tellSlot", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							b,
							0,
							new byte[0]
						});
					}
				}
				if (this.isSelected)
				{
					if (this.state != null)
					{
						base.channel.send("tellEquip", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							this.equippedPage,
							this.equipped_x,
							this.equipped_y,
							this.itemID,
							this.quality,
							this.state
						});
					}
					else
					{
						base.channel.send("tellEquip", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							this.equippedPage,
							this.equipped_x,
							this.equipped_y,
							this.itemID,
							this.quality,
							new byte[0]
						});
					}
				}
			}
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x00112DFC File Offset: 0x001111FC
		public void updateState()
		{
			if (this.isTurret)
			{
				return;
			}
			base.player.inventory.updateState(this.equippedPage, base.player.inventory.getIndex(this.equippedPage, this.equipped_x, this.equipped_y), this.state);
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x00112E54 File Offset: 0x00111254
		public void updateQuality()
		{
			if (this.isTurret)
			{
				return;
			}
			base.player.inventory.updateQuality(this.equippedPage, base.player.inventory.getIndex(this.equippedPage, this.equipped_x, this.equipped_y), this.quality);
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x00112EAC File Offset: 0x001112AC
		public void sendUpdateState()
		{
			if (this.isTurret)
			{
				base.channel.send("tellUpdateStateTemp", ESteamCall.OTHERS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					this.state
				});
				this.tellUpdateStateTemp(Provider.server, this.state);
			}
			else
			{
				byte index = base.player.inventory.getIndex(this.equippedPage, this.equipped_x, this.equipped_y);
				base.channel.send("tellUpdateState", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					this.equippedPage,
					index,
					this.state
				});
			}
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x00112F58 File Offset: 0x00111358
		public void sendUpdateQuality()
		{
			if (this.isTurret)
			{
				return;
			}
			base.player.inventory.sendUpdateQuality(this.equippedPage, this.equipped_x, this.equipped_y, this.quality);
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x00112F90 File Offset: 0x00111390
		public void sendSlot(byte slot)
		{
			ItemJar item = base.player.inventory.getItem(slot, 0);
			if (item != null)
			{
				if (item.item.state != null)
				{
					base.channel.send("tellSlot", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						slot,
						item.item.id,
						item.item.state
					});
				}
				else
				{
					base.channel.send("tellSlot", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						slot,
						item.item.id,
						new byte[0]
					});
				}
			}
			else
			{
				base.channel.send("tellSlot", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					slot,
					0,
					new byte[0]
				});
			}
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x00113088 File Offset: 0x00111488
		public void equip(byte page, byte x, byte y, ushort id)
		{
			if (page < 0 || page >= PlayerInventory.PAGES - 2)
			{
				return;
			}
			if (this.isBusy || !this.canEquip || base.player.life.isDead || base.player.stance.stance == EPlayerStance.CLIMB || base.player.stance.stance == EPlayerStance.DRIVING)
			{
				return;
			}
			if (this.isSelected && !this.isEquipped)
			{
				return;
			}
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, id);
			if (itemAsset == null)
			{
				return;
			}
			if (base.player.stance.stance == EPlayerStance.SWIM && itemAsset.slot == ESlotType.PRIMARY)
			{
				return;
			}
			if (base.player.animator.gesture == EPlayerGesture.ARREST_START)
			{
				return;
			}
			this.lastEquip = Time.realtimeSinceStartup;
			base.channel.send("askEquip", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				page,
				x,
				y,
				itemAsset.hash
			});
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x001131B0 File Offset: 0x001115B0
		public void dequip()
		{
			if (this.isTurret)
			{
				return;
			}
			if (this.ignoreDequip_A)
			{
				return;
			}
			if (Provider.isServer)
			{
				base.channel.send("tellEquip", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					byte.MaxValue,
					byte.MaxValue,
					byte.MaxValue,
					0,
					0,
					new byte[0]
				});
			}
			else
			{
				if (this.isBusy)
				{
					return;
				}
				base.channel.send("askEquip", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					byte.MaxValue,
					byte.MaxValue,
					byte.MaxValue
				});
			}
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x0011328C File Offset: 0x0011168C
		public void use()
		{
			if (this.isSelected)
			{
				ushort itemID = this.itemID;
				byte index = base.player.inventory.getIndex(this.equippedPage, this.equipped_x, this.equipped_y);
				ItemJar item = base.player.inventory.getItem(this.equippedPage, index);
				byte equippedPage = this.equippedPage;
				byte equipped_x = this.equipped_x;
				byte equipped_y = this.equipped_y;
				byte rot = item.rot;
				base.player.inventory.removeItem(this.equippedPage, index);
				this.dequip();
				InventorySearch inventorySearch = base.player.inventory.has(itemID);
				if (inventorySearch != null)
				{
					base.player.inventory.askDragItem(base.channel.owner.playerID.steamID, inventorySearch.page, inventorySearch.jar.x, inventorySearch.jar.y, equippedPage, equipped_x, equipped_y, rot);
					this.tryEquip(equippedPage, equipped_x, equipped_y);
				}
			}
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x00113394 File Offset: 0x00111794
		public void useStepA()
		{
			if (this.isSelected)
			{
				byte index = base.player.inventory.getIndex(this.equippedPage, this.equipped_x, this.equipped_y);
				ItemJar item = base.player.inventory.getItem(this.equippedPage, index);
				this.page_A = this.equippedPage;
				this.x_A = this.equipped_x;
				this.y_A = this.equipped_y;
				this.rot_A = item.rot;
				this.ignoreDequip_A = true;
				base.player.inventory.removeItem(this.equippedPage, index);
				this.ignoreDequip_A = false;
			}
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x0011343C File Offset: 0x0011183C
		public void useStepB()
		{
			if (this.isSelected)
			{
				ushort itemID = this.itemID;
				this.dequip();
				InventorySearch inventorySearch = base.player.inventory.has(itemID);
				if (inventorySearch != null)
				{
					base.player.inventory.askDragItem(base.channel.owner.playerID.steamID, inventorySearch.page, inventorySearch.jar.x, inventorySearch.jar.y, this.page_A, this.x_A, this.y_A, this.rot_A);
					this.tryEquip(this.page_A, this.x_A, this.y_A);
				}
			}
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x001134EC File Offset: 0x001118EC
		private void punch(EPlayerPunch mode)
		{
			if (base.channel.isOwner)
			{
				base.player.playSound((AudioClip)Resources.Load("Sounds/General/Punch"));
				Ray ray = new Ray(base.player.look.aim.position, base.player.look.aim.forward);
				RaycastInfo raycastInfo = DamageTool.raycast(ray, 1.75f, RayMasks.DAMAGE_CLIENT);
				if (raycastInfo.player != null && PlayerEquipment.DAMAGE_PLAYER_MULTIPLIER.damage > 1f && !base.player.quests.isMemberOfSameGroupAs(raycastInfo.player) && Provider.isPvP)
				{
					PlayerUI.hitmark(0, raycastInfo.point, false, (raycastInfo.limb != ELimb.SKULL) ? EPlayerHit.ENTITIY : EPlayerHit.CRITICAL);
				}
				else if ((raycastInfo.zombie != null && PlayerEquipment.DAMAGE_ZOMBIE_MULTIPLIER.damage > 1f) || (raycastInfo.animal != null && PlayerEquipment.DAMAGE_ANIMAL_MULTIPLIER.damage > 1f))
				{
					PlayerUI.hitmark(0, raycastInfo.point, false, (raycastInfo.limb != ELimb.SKULL) ? EPlayerHit.ENTITIY : EPlayerHit.CRITICAL);
				}
				else if (raycastInfo.transform != null && raycastInfo.transform.CompareTag("Barricade") && PlayerEquipment.DAMAGE_BARRICADE > 1f)
				{
					InteractableDoorHinge component = raycastInfo.transform.GetComponent<InteractableDoorHinge>();
					if (component != null)
					{
						raycastInfo.transform = component.transform.parent.parent;
					}
					ushort id;
					if (ushort.TryParse(raycastInfo.transform.name, out id))
					{
						ItemBarricadeAsset itemBarricadeAsset = (ItemBarricadeAsset)Assets.find(EAssetType.ITEM, id);
						if (itemBarricadeAsset != null && itemBarricadeAsset.isVulnerable)
						{
							PlayerUI.hitmark(0, raycastInfo.point, false, EPlayerHit.BUILD);
						}
					}
				}
				else if (raycastInfo.transform != null && raycastInfo.transform.CompareTag("Structure") && PlayerEquipment.DAMAGE_STRUCTURE > 1f)
				{
					ushort id2;
					if (ushort.TryParse(raycastInfo.transform.name, out id2))
					{
						ItemStructureAsset itemStructureAsset = (ItemStructureAsset)Assets.find(EAssetType.ITEM, id2);
						if (itemStructureAsset != null && itemStructureAsset.isVulnerable)
						{
							PlayerUI.hitmark(0, raycastInfo.point, false, EPlayerHit.BUILD);
						}
					}
				}
				else if (raycastInfo.vehicle != null && !raycastInfo.vehicle.isDead && PlayerEquipment.DAMAGE_VEHICLE > 1f)
				{
					if (raycastInfo.vehicle.asset != null && raycastInfo.vehicle.canBeDamaged && raycastInfo.vehicle.asset.isVulnerable)
					{
						PlayerUI.hitmark(0, raycastInfo.point, false, EPlayerHit.BUILD);
					}
				}
				else if (raycastInfo.transform != null && raycastInfo.transform.CompareTag("Resource") && PlayerEquipment.DAMAGE_RESOURCE > 1f)
				{
					byte x;
					byte y;
					ushort index;
					if (ResourceManager.tryGetRegion(raycastInfo.transform, out x, out y, out index))
					{
						ResourceSpawnpoint resourceSpawnpoint = ResourceManager.getResourceSpawnpoint(x, y, index);
						if (resourceSpawnpoint != null && !resourceSpawnpoint.isDead)
						{
							PlayerUI.hitmark(0, raycastInfo.point, false, EPlayerHit.BUILD);
						}
					}
				}
				else if (raycastInfo.transform != null && PlayerEquipment.DAMAGE_OBJECT > 1f)
				{
					InteractableObjectRubble component2 = raycastInfo.transform.GetComponent<InteractableObjectRubble>();
					if (component2 != null)
					{
						raycastInfo.section = component2.getSection(raycastInfo.collider.transform);
						if (!component2.isSectionDead(raycastInfo.section) && component2.asset.rubbleIsVulnerable)
						{
							PlayerUI.hitmark(0, raycastInfo.point, false, EPlayerHit.BUILD);
						}
					}
				}
				base.player.input.sendRaycast(raycastInfo);
			}
			if (mode == EPlayerPunch.LEFT)
			{
				base.player.animator.play("Punch_Left", false);
				if (Provider.isServer)
				{
					base.player.animator.sendGesture(EPlayerGesture.PUNCH_LEFT, false);
				}
			}
			else if (mode == EPlayerPunch.RIGHT)
			{
				base.player.animator.play("Punch_Right", false);
				if (Provider.isServer)
				{
					base.player.animator.sendGesture(EPlayerGesture.PUNCH_RIGHT, false);
				}
			}
			if (Provider.isServer)
			{
				if (!base.player.input.hasInputs())
				{
					return;
				}
				InputInfo input = base.player.input.getInput(true);
				if (input == null)
				{
					return;
				}
				if ((input.point - base.player.look.aim.position).sqrMagnitude > 36f)
				{
					return;
				}
				DamageTool.impact(input.point, input.normal, input.material, input.type != ERaycastInfoType.NONE && input.type != ERaycastInfoType.OBJECT);
				EPlayerKill eplayerKill = EPlayerKill.NONE;
				uint num = 0u;
				float num2 = 1f;
				num2 *= 1f + base.channel.owner.player.skills.mastery(0, 0) * 0.5f;
				if (input.type == ERaycastInfoType.PLAYER)
				{
					this.lastPunching = Time.realtimeSinceStartup;
					if (input.player != null && !base.player.quests.isMemberOfSameGroupAs(input.player) && Provider.isPvP)
					{
						DamageTool.damage(input.player, EDeathCause.PUNCH, input.limb, base.channel.owner.playerID.steamID, input.direction, PlayerEquipment.DAMAGE_PLAYER_MULTIPLIER, num2, true, out eplayerKill);
					}
				}
				else if (input.type == ERaycastInfoType.ZOMBIE)
				{
					if (input.zombie != null)
					{
						DamageTool.damage(input.zombie, input.limb, input.direction, PlayerEquipment.DAMAGE_ZOMBIE_MULTIPLIER, num2, true, out eplayerKill, out num);
						if (base.player.movement.nav != 255)
						{
							input.zombie.alert(base.transform.position, true);
						}
					}
				}
				else if (input.type == ERaycastInfoType.ANIMAL)
				{
					this.lastPunching = Time.realtimeSinceStartup;
					if (input.animal != null)
					{
						DamageTool.damage(input.animal, input.limb, input.direction, PlayerEquipment.DAMAGE_ANIMAL_MULTIPLIER, num2, out eplayerKill, out num);
						input.animal.alertPoint(base.transform.position, true);
					}
				}
				else if (input.type == ERaycastInfoType.VEHICLE)
				{
					this.lastPunching = Time.realtimeSinceStartup;
					if (input.vehicle != null && input.vehicle.asset != null && input.vehicle.canBeDamaged && input.vehicle.asset.isVulnerable)
					{
						DamageTool.damage(input.vehicle, false, Vector3.zero, false, PlayerEquipment.DAMAGE_VEHICLE, num2, true, out eplayerKill);
					}
				}
				else if (input.type == ERaycastInfoType.BARRICADE)
				{
					this.lastPunching = Time.realtimeSinceStartup;
					ushort id3;
					if (input.transform != null && input.transform.CompareTag("Barricade") && ushort.TryParse(input.transform.name, out id3))
					{
						ItemBarricadeAsset itemBarricadeAsset2 = (ItemBarricadeAsset)Assets.find(EAssetType.ITEM, id3);
						if (itemBarricadeAsset2 != null && itemBarricadeAsset2.isVulnerable)
						{
							DamageTool.damage(input.transform, false, PlayerEquipment.DAMAGE_BARRICADE, num2, out eplayerKill);
						}
					}
				}
				else if (input.type == ERaycastInfoType.STRUCTURE)
				{
					this.lastPunching = Time.realtimeSinceStartup;
					ushort id4;
					if (input.transform != null && input.transform.CompareTag("Structure") && ushort.TryParse(input.transform.name, out id4))
					{
						ItemStructureAsset itemStructureAsset2 = (ItemStructureAsset)Assets.find(EAssetType.ITEM, id4);
						if (itemStructureAsset2 != null && itemStructureAsset2.isVulnerable)
						{
							DamageTool.damage(input.transform, false, input.direction, PlayerEquipment.DAMAGE_STRUCTURE, num2, out eplayerKill);
						}
					}
				}
				else if (input.type == ERaycastInfoType.RESOURCE)
				{
					this.lastPunching = Time.realtimeSinceStartup;
					byte x2;
					byte y2;
					ushort index2;
					if (input.transform != null && input.transform.CompareTag("Resource") && ResourceManager.tryGetRegion(input.transform, out x2, out y2, out index2))
					{
						ResourceSpawnpoint resourceSpawnpoint2 = ResourceManager.getResourceSpawnpoint(x2, y2, index2);
						if (resourceSpawnpoint2 != null && !resourceSpawnpoint2.isDead)
						{
							DamageTool.damage(input.transform, input.direction, PlayerEquipment.DAMAGE_RESOURCE, num2, 1f, out eplayerKill, out num);
						}
					}
				}
				else if (input.type == ERaycastInfoType.OBJECT && input.transform != null && input.section < 255)
				{
					InteractableObjectRubble component3 = input.transform.GetComponent<InteractableObjectRubble>();
					if (component3 != null && !component3.isSectionDead(input.section) && component3.asset.rubbleIsVulnerable)
					{
						DamageTool.damage(input.transform, input.direction, input.section, PlayerEquipment.DAMAGE_OBJECT, num2, out eplayerKill, out num);
					}
				}
				if (input.type != ERaycastInfoType.PLAYER && input.type != ERaycastInfoType.ZOMBIE && input.type != ERaycastInfoType.ANIMAL && !base.player.life.isAggressor)
				{
					float num3 = 2f + Provider.modeConfigData.Players.Ray_Aggressor_Distance;
					num3 *= num3;
					float num4 = Provider.modeConfigData.Players.Ray_Aggressor_Distance;
					num4 *= num4;
					Vector3 forward = base.player.look.aim.forward;
					for (int i = 0; i < Provider.clients.Count; i++)
					{
						if (Provider.clients[i] != base.channel.owner)
						{
							Player player = Provider.clients[i].player;
							if (!(player == null))
							{
								Vector3 vector = player.look.aim.position - base.player.look.aim.position;
								Vector3 a = Vector3.Project(vector, forward);
								if (a.sqrMagnitude < num3 && (a - vector).sqrMagnitude < num4)
								{
									base.player.life.markAggressive(false, true);
								}
							}
						}
					}
				}
				if (Level.info.type == ELevelType.HORDE)
				{
					if (input.zombie != null)
					{
						if (input.limb == ELimb.SKULL)
						{
							base.player.skills.askPay(10u);
						}
						else
						{
							base.player.skills.askPay(5u);
						}
					}
					if (eplayerKill == EPlayerKill.ZOMBIE)
					{
						if (input.limb == ELimb.SKULL)
						{
							base.player.skills.askPay(50u);
						}
						else
						{
							base.player.skills.askPay(25u);
						}
					}
				}
				else
				{
					if (eplayerKill == EPlayerKill.PLAYER && Level.info.type == ELevelType.ARENA)
					{
						base.player.skills.askPay(100u);
					}
					base.player.sendStat(eplayerKill);
					if (num > 0u)
					{
						base.player.skills.askPay(num);
					}
				}
			}
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x00114104 File Offset: 0x00112504
		public void simulate(uint simulation, bool inputPrimary, bool inputSecondary, bool inputSteady)
		{
			if (base.player.stance.stance == EPlayerStance.CLIMB || (base.player.stance.stance == EPlayerStance.DRIVING && !this.isTurret) || (base.player.stance.stance == EPlayerStance.SWIM && this.asset != null && this.asset.slot == ESlotType.PRIMARY))
			{
				if (this.isSelected && Provider.isServer)
				{
					this.dequip();
				}
				return;
			}
			if (Time.realtimeSinceStartup - this.lastEquip < 0.1f || base.player.life.isDead)
			{
				return;
			}
			if (base.player.movement.isSafe)
			{
				if (this.asset == null)
				{
					if (base.player.movement.isSafeInfo.noWeapons)
					{
						return;
					}
				}
				else if (this.asset.isDangerous)
				{
					if (this.asset is ItemBarricadeAsset || this.asset is ItemStructureAsset)
					{
						if (base.player.movement.isSafeInfo.noBuildables)
						{
							inputPrimary = false;
							inputSecondary = false;
						}
					}
					else if (base.player.movement.isSafeInfo.noWeapons)
					{
						inputPrimary = false;
						inputSecondary = false;
					}
				}
			}
			if (Level.info != null && Level.info.type != ELevelType.SURVIVAL && this.asset == null)
			{
				return;
			}
			if (base.player.stance.stance == EPlayerStance.SWIM && this.asset == null)
			{
				return;
			}
			if (base.player.animator.gesture == EPlayerGesture.ARREST_START)
			{
				return;
			}
			if (this.isTurret && (base.player.movement.getVehicle() == null || !base.player.movement.getVehicle().canUseTurret))
			{
				return;
			}
			if (this.isSelected)
			{
				if (inputPrimary != this.lastPrimary)
				{
					this.lastPrimary = inputPrimary;
					if (this.isEquipped)
					{
						if (inputPrimary)
						{
							this.useable.startPrimary();
						}
						else
						{
							this.useable.stopPrimary();
						}
					}
				}
				if (inputSecondary != this.lastSecondary)
				{
					this.lastSecondary = inputSecondary;
					if (this.isEquipped)
					{
						if (inputSecondary)
						{
							this.useable.startSecondary();
						}
						else
						{
							this.useable.stopSecondary();
						}
					}
				}
				if (this.isSelected && this.isEquipped)
				{
					this.useable.simulate(simulation, inputSteady);
				}
			}
			else
			{
				if (inputPrimary != this.lastPrimary)
				{
					this.lastPrimary = inputPrimary;
					if (!this.isBusy && base.player.stance.stance != EPlayerStance.PRONE && inputPrimary && simulation - this.lastPunch > 5u)
					{
						this.lastPunch = simulation;
						this.punch(EPlayerPunch.LEFT);
					}
				}
				if (inputSecondary != this.lastSecondary)
				{
					this.lastSecondary = inputSecondary;
					if (!this.isBusy && base.player.stance.stance != EPlayerStance.PRONE && inputSecondary && simulation - this.lastPunch > 5u)
					{
						this.lastPunch = simulation;
						this.punch(EPlayerPunch.RIGHT);
					}
				}
			}
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x00114474 File Offset: 0x00112874
		public void tock(uint clock)
		{
			if (this.isSelected && this.isEquipped)
			{
				this.useable.tock(clock);
			}
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x00114498 File Offset: 0x00112898
		private void updateVision()
		{
			if (this.hasVision && base.player.clothing.glassesState[0] != 0)
			{
				if (base.player.clothing.glassesAsset.vision == ELightingVision.HEADLAMP)
				{
					base.player.updateHeadlamp(true);
					if (base.channel.isOwner)
					{
						LevelLighting.vision = ELightingVision.NONE;
						LevelLighting.updateLighting();
						PlayerLifeUI.updateGrayscale();
					}
				}
				else
				{
					base.player.updateHeadlamp(false);
					if (base.channel.isOwner)
					{
						LevelLighting.vision = ((base.player.look.perspective != EPlayerPerspective.FIRST) ? ELightingVision.NONE : base.player.clothing.glassesAsset.vision);
						LevelLighting.updateLighting();
						PlayerLifeUI.updateGrayscale();
					}
				}
				base.player.updateGlassesLights(true);
			}
			else
			{
				base.player.updateHeadlamp(false);
				if (base.channel.isOwner)
				{
					LevelLighting.vision = ELightingVision.NONE;
					LevelLighting.updateLighting();
					PlayerLifeUI.updateGrayscale();
				}
				base.player.updateGlassesLights(false);
			}
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x001145B7 File Offset: 0x001129B7
		private void onVisionUpdated(bool isViewing)
		{
			if (isViewing)
			{
				this.warp = (UnityEngine.Random.value < 0.25f);
			}
			else
			{
				this.warp = false;
			}
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x001145DD File Offset: 0x001129DD
		private void onPerspectiveUpdated(EPlayerPerspective perspective)
		{
			if (this.hasVision)
			{
				this.updateVision();
			}
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x001145F0 File Offset: 0x001129F0
		private void onGlassesUpdated(ushort id, byte quality, byte[] state)
		{
			this.hasVision = (id != 0 && base.player.clothing.glassesAsset != null && base.player.clothing.glassesAsset.vision != ELightingVision.NONE);
			this.updateVision();
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x00114642 File Offset: 0x00112A42
		private void OnVisualToggleChanged(PlayerClothing sender)
		{
			if (this.hasVision)
			{
				this.updateVision();
			}
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x00114658 File Offset: 0x00112A58
		private void onLifeUpdated(bool isDead)
		{
			if (isDead)
			{
				for (byte b = 0; b < PlayerInventory.SLOTS; b += 1)
				{
					this.updateSlot(b, 0, new byte[0]);
				}
				if (Provider.isServer)
				{
					this.dequip();
				}
				this.isBusy = false;
				this.canEquip = true;
				this._equippedPage = byte.MaxValue;
				this._equipped_x = byte.MaxValue;
				this._equipped_y = byte.MaxValue;
			}
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x001146D0 File Offset: 0x00112AD0
		private void hotkey(byte button)
		{
			byte b = button - 2;
			if (!PlayerUI.window.showCursor)
			{
				if (!this.isBusy)
				{
					if (button < PlayerInventory.SLOTS)
					{
						ItemJar item = base.player.inventory.getItem(button, 0);
						if (item != null)
						{
							this.equip(button, item.x, item.y, item.item.id);
						}
						else if (this.isSelected && this.isEquipped)
						{
							this.dequip();
						}
					}
					else
					{
						HotkeyInfo hotkeyInfo = this.hotkeys[(int)b];
						if (hotkeyInfo.id != 0)
						{
							this.equip(hotkeyInfo.page, hotkeyInfo.x, hotkeyInfo.y, hotkeyInfo.id);
						}
						else if (this.isSelected && this.isEquipped)
						{
							this.dequip();
						}
					}
				}
			}
			else if (button >= PlayerInventory.SLOTS && PlayerDashboardInventoryUI.active)
			{
				if (PlayerDashboardInventoryUI.selectedPage >= PlayerInventory.SLOTS && PlayerDashboardInventoryUI.selectedPage < PlayerInventory.STORAGE)
				{
					if (ItemTool.checkUseable(PlayerDashboardInventoryUI.selectedPage, PlayerDashboardInventoryUI.selectedJar.item.id))
					{
						HotkeyInfo hotkeyInfo2 = this.hotkeys[(int)b];
						hotkeyInfo2.id = PlayerDashboardInventoryUI.selectedJar.item.id;
						hotkeyInfo2.page = PlayerDashboardInventoryUI.selectedPage;
						hotkeyInfo2.x = PlayerDashboardInventoryUI.selected_x;
						hotkeyInfo2.y = PlayerDashboardInventoryUI.selected_y;
						PlayerDashboardInventoryUI.closeSelection();
						for (int i = 0; i < this.hotkeys.Length; i++)
						{
							if (i != (int)b)
							{
								HotkeyInfo hotkeyInfo3 = this.hotkeys[i];
								if (hotkeyInfo3.page == hotkeyInfo2.page && hotkeyInfo3.x == hotkeyInfo2.x && hotkeyInfo3.y == hotkeyInfo2.y)
								{
									hotkeyInfo3.id = 0;
									hotkeyInfo3.page = byte.MaxValue;
									hotkeyInfo3.x = byte.MaxValue;
									hotkeyInfo3.y = byte.MaxValue;
								}
							}
						}
						if (this.onHotkeysUpdated != null)
						{
							this.onHotkeysUpdated();
						}
					}
				}
				else if (PlayerDashboardInventoryUI.selectedPage == 255)
				{
					HotkeyInfo hotkeyInfo4 = this.hotkeys[(int)b];
					hotkeyInfo4.id = 0;
					hotkeyInfo4.page = byte.MaxValue;
					hotkeyInfo4.x = byte.MaxValue;
					hotkeyInfo4.y = byte.MaxValue;
					if (this.onHotkeysUpdated != null)
					{
						this.onHotkeysUpdated();
					}
				}
			}
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x0011495E File Offset: 0x00112D5E
		public void init()
		{
			base.channel.send("askEquipment", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x0011497C File Offset: 0x00112D7C
		private void Update()
		{
			if (base.channel.isOwner)
			{
				if (!PlayerUI.window.showCursor && !base.player.workzone.isBuilding && (base.player.movement.getVehicle() == null || base.player.look.perspective == EPlayerPerspective.FIRST))
				{
					this._primary = (this.prim || Input.GetKey(ControlsSettings.primary));
					if (ControlsSettings.aiming == EControlMode.TOGGLE && this.asset != null && (this.asset.type == EItemType.GUN || this.asset.type == EItemType.OPTIC))
					{
						if ((this.sec || Input.GetKey(ControlsSettings.secondary)) != this.flipSecondary)
						{
							this.flipSecondary = (this.sec || Input.GetKey(ControlsSettings.secondary));
							if (this.flipSecondary)
							{
								this._secondary = !this.secondary;
							}
						}
					}
					else
					{
						this._secondary = (this.sec || Input.GetKey(ControlsSettings.secondary));
						this.flipSecondary = this.secondary;
					}
					this.prim = false;
					this.sec = false;
					if (this.warp)
					{
						bool primary = this.primary;
						this._primary = this.secondary;
						this._secondary = primary;
					}
				}
				else
				{
					this._primary = false;
					this._secondary = false;
				}
			}
			this.wasTryingToSelect = false;
			if (base.channel.isOwner)
			{
				if (!PlayerUI.window.showCursor && !base.player.workzone.isBuilding)
				{
					if (Input.GetKeyDown(ControlsSettings.vision) && this.hasVision && !PlayerLifeUI.scopeOverlay.isVisible)
					{
						base.channel.send("askToggleVision", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
					}
					if (Input.GetKey(ControlsSettings.primary))
					{
						this.prim = true;
					}
					if (Input.GetKey(ControlsSettings.secondary))
					{
						this.sec = true;
					}
					if (Input.GetKeyDown(ControlsSettings.dequip) && this.isSelected && !this.isBusy && this.isEquipped)
					{
						this.dequip();
					}
				}
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					this.hotkey(0);
				}
				if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					this.hotkey(1);
				}
				if (Input.GetKeyDown(KeyCode.Alpha3))
				{
					this.hotkey(2);
				}
				if (Input.GetKeyDown(KeyCode.Alpha4))
				{
					this.hotkey(3);
				}
				if (Input.GetKeyDown(KeyCode.Alpha5))
				{
					this.hotkey(4);
				}
				if (Input.GetKeyDown(KeyCode.Alpha6))
				{
					this.hotkey(5);
				}
				if (Input.GetKeyDown(KeyCode.Alpha7))
				{
					this.hotkey(6);
				}
				if (Input.GetKeyDown(KeyCode.Alpha8))
				{
					this.hotkey(7);
				}
				if (Input.GetKeyDown(KeyCode.Alpha9))
				{
					this.hotkey(8);
				}
				if (Input.GetKeyDown(KeyCode.Alpha0))
				{
					this.hotkey(9);
				}
			}
			if (this.isSelected)
			{
				this.useable.tick();
			}
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x00114CC8 File Offset: 0x001130C8
		private void Start()
		{
			this.hasVision = (base.player.clothing.glasses != 0 && base.player.clothing.glassesAsset != null && base.player.clothing.glassesAsset.vision != ELightingVision.NONE);
			this.updateVision();
			this.thirdSlots = new Transform[(int)PlayerInventory.SLOTS];
			this.thirdSkinneds = new bool[(int)PlayerInventory.SLOTS];
			this.tempThirdMeshes = new List<Mesh>[(int)PlayerInventory.SLOTS];
			for (int i = 0; i < this.tempThirdMeshes.Length; i++)
			{
				this.tempThirdMeshes[i] = new List<Mesh>(4);
			}
			this.tempThirdMaterials = new Material[(int)PlayerInventory.SLOTS];
			this.thirdMythics = new MythicLockee[(int)PlayerInventory.SLOTS];
			this.tempThirdMesh = new List<Mesh>(4);
			if (base.channel.isOwner && base.player.character != null)
			{
				this.tempFirstMesh = new List<Mesh>(4);
				this.tempCharacterMesh = new List<Mesh>(4);
				this.characterSlots = new Transform[(int)PlayerInventory.SLOTS];
				this.characterSkinneds = new bool[(int)PlayerInventory.SLOTS];
				this.tempCharacterMeshes = new List<Mesh>[(int)PlayerInventory.SLOTS];
				for (int j = 0; j < this.tempCharacterMeshes.Length; j++)
				{
					this.tempCharacterMeshes[j] = new List<Mesh>(4);
				}
				this.tempCharacterMaterials = new Material[(int)PlayerInventory.SLOTS];
				this.characterMythics = new MythicLockee[(int)PlayerInventory.SLOTS];
			}
			this.warp = false;
			this._equippedPage = byte.MaxValue;
			this._equipped_x = byte.MaxValue;
			this._equipped_y = byte.MaxValue;
			this.isBusy = false;
			this.canEquip = true;
			if (base.player.third != null)
			{
				this._thirdPrimaryMeleeSlot = base.player.animator.thirdSkeleton.FindChild("Spine").FindChild("Primary_Melee");
				this._thirdPrimaryLargeGunSlot = base.player.animator.thirdSkeleton.FindChild("Spine").FindChild("Primary_Large_Gun");
				this._thirdPrimarySmallGunSlot = base.player.animator.thirdSkeleton.FindChild("Spine").FindChild("Primary_Small_Gun");
				this._thirdSecondaryMeleeSlot = base.player.animator.thirdSkeleton.FindChild("Right_Hip").FindChild("Right_Leg").FindChild("Secondary_Melee");
				this._thirdSecondaryGunSlot = base.player.animator.thirdSkeleton.FindChild("Right_Hip").FindChild("Right_Leg").FindChild("Secondary_Gun");
			}
			if (base.channel.isOwner)
			{
				this._characterPrimaryMeleeSlot = base.player.character.FindChild("Skeleton").FindChild("Spine").FindChild("Primary_Melee");
				this._characterPrimaryLargeGunSlot = base.player.character.FindChild("Skeleton").FindChild("Spine").FindChild("Primary_Large_Gun");
				this._characterPrimarySmallGunSlot = base.player.character.FindChild("Skeleton").FindChild("Spine").FindChild("Primary_Small_Gun");
				this._characterSecondaryMeleeSlot = base.player.character.FindChild("Skeleton").FindChild("Right_Hip").FindChild("Right_Leg").FindChild("Secondary_Melee");
				this._characterSecondaryGunSlot = base.player.character.FindChild("Skeleton").FindChild("Right_Hip").FindChild("Right_Leg").FindChild("Secondary_Gun");
			}
			if (base.player.first != null)
			{
				this._firstLeftHook = base.player.animator.firstSkeleton.FindChild("Spine").FindChild("Left_Shoulder").FindChild("Left_Arm").FindChild("Left_Hand").FindChild("Left_Hook");
				this._firstRightHook = base.player.animator.firstSkeleton.FindChild("Spine").FindChild("Right_Shoulder").FindChild("Right_Arm").FindChild("Right_Hand").FindChild("Right_Hook");
			}
			if (base.player.third != null)
			{
				this._thirdLeftHook = base.player.animator.thirdSkeleton.FindChild("Spine").FindChild("Left_Shoulder").FindChild("Left_Arm").FindChild("Left_Hand").FindChild("Left_Hook");
				this._thirdRightHook = base.player.animator.thirdSkeleton.FindChild("Spine").FindChild("Right_Shoulder").FindChild("Right_Arm").FindChild("Right_Hand").FindChild("Right_Hook");
			}
			if (base.channel.isOwner && base.player.character != null)
			{
				this._characterLeftHook = base.player.character.transform.FindChild("Skeleton").FindChild("Spine").FindChild("Left_Shoulder").FindChild("Left_Arm").FindChild("Left_Hand").FindChild("Left_Hook");
				this._characterRightHook = base.player.character.transform.FindChild("Skeleton").FindChild("Spine").FindChild("Right_Shoulder").FindChild("Right_Arm").FindChild("Right_Hand").FindChild("Right_Hook");
			}
			if (base.channel.isOwner || Provider.isServer)
			{
				PlayerLife life = base.player.life;
				life.onVisionUpdated = (VisionUpdated)Delegate.Combine(life.onVisionUpdated, new VisionUpdated(this.onVisionUpdated));
			}
			PlayerClothing clothing = base.player.clothing;
			clothing.onGlassesUpdated = (GlassesUpdated)Delegate.Combine(clothing.onGlassesUpdated, new GlassesUpdated(this.onGlassesUpdated));
			base.player.clothing.VisualToggleChanged += this.OnVisualToggleChanged;
			if (base.channel.isOwner)
			{
				this._hotkeys = new HotkeyInfo[8];
				byte b = 0;
				while ((int)b < this.hotkeys.Length)
				{
					this.hotkeys[(int)b] = new HotkeyInfo();
					b += 1;
				}
				this.load();
				PlayerLook look = base.player.look;
				look.onPerspectiveUpdated = (PerspectiveUpdated)Delegate.Combine(look.onPerspectiveUpdated, new PerspectiveUpdated(this.onPerspectiveUpdated));
			}
			PlayerLife life2 = base.player.life;
			life2.onLifeUpdated = (LifeUpdated)Delegate.Combine(life2.onLifeUpdated, new LifeUpdated(this.onLifeUpdated));
			base.Invoke("init", 0.1f);
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x001153F7 File Offset: 0x001137F7
		private void OnDestroy()
		{
			if (this.useable != null)
			{
				this.useable.dequip();
			}
			if (base.channel.isOwner)
			{
				this.save();
			}
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x0011542C File Offset: 0x0011382C
		private void load()
		{
			if (ReadWrite.fileExists(string.Concat(new object[]
			{
				"/Worlds/Hotkeys/Equip_",
				Provider.currentServerInfo.ip,
				"_",
				Provider.currentServerInfo.port,
				"_",
				Characters.selected,
				".dat"
			}), false))
			{
				Block block = ReadWrite.readBlock(string.Concat(new object[]
				{
					"/Worlds/Hotkeys/Equip_",
					Provider.currentServerInfo.ip,
					"_",
					Provider.currentServerInfo.port,
					"_",
					Characters.selected,
					".dat"
				}), false, 0);
				block.readByte();
				byte b = 0;
				while ((int)b < this.hotkeys.Length)
				{
					HotkeyInfo hotkeyInfo = this.hotkeys[(int)b];
					hotkeyInfo.id = block.readUInt16();
					hotkeyInfo.page = block.readByte();
					hotkeyInfo.x = block.readByte();
					hotkeyInfo.y = block.readByte();
					b += 1;
				}
			}
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x00115560 File Offset: 0x00113960
		private void save()
		{
			if (this.hotkeys == null)
			{
				return;
			}
			bool flag = false;
			byte b = 0;
			while ((int)b < this.hotkeys.Length)
			{
				HotkeyInfo hotkeyInfo = this.hotkeys[(int)b];
				if (hotkeyInfo.id != 0 || (hotkeyInfo.page != 255 && hotkeyInfo.x != 255 && hotkeyInfo.y != 255))
				{
					flag = true;
					break;
				}
				b += 1;
			}
			if (!flag)
			{
				if (ReadWrite.fileExists(string.Concat(new object[]
				{
					"/Worlds/Hotkeys/Equip_",
					Provider.currentServerInfo.ip,
					"_",
					Provider.currentServerInfo.port,
					"_",
					Characters.selected,
					".dat"
				}), false))
				{
					ReadWrite.deleteFile(string.Concat(new object[]
					{
						"/Worlds/Hotkeys/Equip_",
						Provider.currentServerInfo.ip,
						"_",
						Provider.currentServerInfo.port,
						"_",
						Characters.selected,
						".dat"
					}), false);
				}
				return;
			}
			Block block = new Block();
			block.writeByte(PlayerEquipment.SAVEDATA_VERSION);
			byte b2 = 0;
			while ((int)b2 < this.hotkeys.Length)
			{
				HotkeyInfo hotkeyInfo2 = this.hotkeys[(int)b2];
				block.writeUInt16(hotkeyInfo2.id);
				block.writeByte(hotkeyInfo2.page);
				block.writeByte(hotkeyInfo2.x);
				block.writeByte(hotkeyInfo2.y);
				b2 += 1;
			}
			ReadWrite.writeBlock(string.Concat(new object[]
			{
				"/Worlds/Hotkeys/Equip_",
				Provider.currentServerInfo.ip,
				"_",
				Provider.currentServerInfo.port,
				"_",
				Characters.selected,
				".dat"
			}), false, block);
		}

		// Token: 0x04001BF2 RID: 7154
		public static readonly byte SAVEDATA_VERSION = 1;

		// Token: 0x04001BF3 RID: 7155
		private static readonly float DAMAGE_BARRICADE = 2f;

		// Token: 0x04001BF4 RID: 7156
		private static readonly float DAMAGE_STRUCTURE = 2f;

		// Token: 0x04001BF5 RID: 7157
		private static readonly float DAMAGE_VEHICLE = 0f;

		// Token: 0x04001BF6 RID: 7158
		private static readonly float DAMAGE_RESOURCE = 0f;

		// Token: 0x04001BF7 RID: 7159
		private static readonly float DAMAGE_OBJECT = 5f;

		// Token: 0x04001BF8 RID: 7160
		private static readonly PlayerDamageMultiplier DAMAGE_PLAYER_MULTIPLIER = new PlayerDamageMultiplier(15f, 0.6f, 0.6f, 0.8f, 1.1f);

		// Token: 0x04001BF9 RID: 7161
		private static readonly ZombieDamageMultiplier DAMAGE_ZOMBIE_MULTIPLIER = new ZombieDamageMultiplier(15f, 0.3f, 0.3f, 0.6f, 1.1f);

		// Token: 0x04001BFA RID: 7162
		private static readonly AnimalDamageMultiplier DAMAGE_ANIMAL_MULTIPLIER = new AnimalDamageMultiplier(15f, 0.3f, 0.6f, 1.1f);

		// Token: 0x04001BFB RID: 7163
		private ushort _itemID;

		// Token: 0x04001BFC RID: 7164
		private byte[] _state;

		// Token: 0x04001BFD RID: 7165
		private byte _quality;

		// Token: 0x04001BFE RID: 7166
		private Transform[] thirdSlots;

		// Token: 0x04001BFF RID: 7167
		private bool[] thirdSkinneds;

		// Token: 0x04001C00 RID: 7168
		private List<Mesh>[] tempThirdMeshes;

		// Token: 0x04001C01 RID: 7169
		private Material[] tempThirdMaterials;

		// Token: 0x04001C02 RID: 7170
		private MythicLockee[] thirdMythics;

		// Token: 0x04001C03 RID: 7171
		private Transform[] characterSlots;

		// Token: 0x04001C04 RID: 7172
		private bool[] characterSkinneds;

		// Token: 0x04001C05 RID: 7173
		private List<Mesh>[] tempCharacterMeshes;

		// Token: 0x04001C06 RID: 7174
		private Material[] tempCharacterMaterials;

		// Token: 0x04001C07 RID: 7175
		private MythicLockee[] characterMythics;

		// Token: 0x04001C08 RID: 7176
		private Transform _firstModel;

		// Token: 0x04001C09 RID: 7177
		private bool firstSkinned;

		// Token: 0x04001C0A RID: 7178
		private List<Mesh> tempFirstMesh;

		// Token: 0x04001C0B RID: 7179
		private Material tempFirstMaterial;

		// Token: 0x04001C0C RID: 7180
		private MythicLockee firstMythic;

		// Token: 0x04001C0D RID: 7181
		private Transform _thirdModel;

		// Token: 0x04001C0E RID: 7182
		private bool thirdSkinned;

		// Token: 0x04001C0F RID: 7183
		private List<Mesh> tempThirdMesh;

		// Token: 0x04001C10 RID: 7184
		private Material tempThirdMaterial;

		// Token: 0x04001C11 RID: 7185
		private MythicLockee thirdMythic;

		// Token: 0x04001C12 RID: 7186
		private Transform _characterModel;

		// Token: 0x04001C13 RID: 7187
		private bool characterSkinned;

		// Token: 0x04001C14 RID: 7188
		private List<Mesh> tempCharacterMesh;

		// Token: 0x04001C15 RID: 7189
		private Material tempCharacterMaterial;

		// Token: 0x04001C16 RID: 7190
		private MythicLockee characterMythic;

		// Token: 0x04001C17 RID: 7191
		private ItemAsset _asset;

		// Token: 0x04001C18 RID: 7192
		private Useable _useable;

		// Token: 0x04001C19 RID: 7193
		private Transform _thirdPrimaryMeleeSlot;

		// Token: 0x04001C1A RID: 7194
		private Transform _thirdPrimaryLargeGunSlot;

		// Token: 0x04001C1B RID: 7195
		private Transform _thirdPrimarySmallGunSlot;

		// Token: 0x04001C1C RID: 7196
		private Transform _thirdSecondaryMeleeSlot;

		// Token: 0x04001C1D RID: 7197
		private Transform _thirdSecondaryGunSlot;

		// Token: 0x04001C1E RID: 7198
		private Transform _characterPrimaryMeleeSlot;

		// Token: 0x04001C1F RID: 7199
		private Transform _characterPrimaryLargeGunSlot;

		// Token: 0x04001C20 RID: 7200
		private Transform _characterPrimarySmallGunSlot;

		// Token: 0x04001C21 RID: 7201
		private Transform _characterSecondaryMeleeSlot;

		// Token: 0x04001C22 RID: 7202
		private Transform _characterSecondaryGunSlot;

		// Token: 0x04001C23 RID: 7203
		private Transform _firstLeftHook;

		// Token: 0x04001C24 RID: 7204
		private Transform _firstRightHook;

		// Token: 0x04001C25 RID: 7205
		private Transform _thirdLeftHook;

		// Token: 0x04001C26 RID: 7206
		private Transform _thirdRightHook;

		// Token: 0x04001C27 RID: 7207
		private Transform _characterLeftHook;

		// Token: 0x04001C28 RID: 7208
		private Transform _characterRightHook;

		// Token: 0x04001C29 RID: 7209
		private HotkeyInfo[] _hotkeys;

		// Token: 0x04001C2A RID: 7210
		public HotkeysUpdated onHotkeysUpdated;

		// Token: 0x04001C2B RID: 7211
		public bool wasTryingToSelect;

		// Token: 0x04001C2D RID: 7213
		public bool isBusy;

		// Token: 0x04001C2E RID: 7214
		public bool canEquip;

		// Token: 0x04001C2F RID: 7215
		private byte slot = byte.MaxValue;

		// Token: 0x04001C30 RID: 7216
		private bool warp;

		// Token: 0x04001C31 RID: 7217
		private byte _equippedPage;

		// Token: 0x04001C32 RID: 7218
		private byte _equipped_x;

		// Token: 0x04001C33 RID: 7219
		private byte _equipped_y;

		// Token: 0x04001C34 RID: 7220
		private bool prim;

		// Token: 0x04001C35 RID: 7221
		private bool lastPrimary;

		// Token: 0x04001C36 RID: 7222
		private bool _primary;

		// Token: 0x04001C37 RID: 7223
		private bool sec;

		// Token: 0x04001C38 RID: 7224
		private bool flipSecondary;

		// Token: 0x04001C39 RID: 7225
		private bool lastSecondary;

		// Token: 0x04001C3A RID: 7226
		private bool _secondary;

		// Token: 0x04001C3B RID: 7227
		private bool hasVision;

		// Token: 0x04001C3D RID: 7229
		private float lastEquip;

		// Token: 0x04001C3E RID: 7230
		private float lastEquipped;

		// Token: 0x04001C3F RID: 7231
		private float equippedTime;

		// Token: 0x04001C40 RID: 7232
		private uint lastPunch;

		// Token: 0x04001C41 RID: 7233
		private static float lastInspect;

		// Token: 0x04001C42 RID: 7234
		private static float inspectTime;

		// Token: 0x04001C43 RID: 7235
		protected byte page_A;

		// Token: 0x04001C44 RID: 7236
		protected byte x_A;

		// Token: 0x04001C45 RID: 7237
		protected byte y_A;

		// Token: 0x04001C46 RID: 7238
		protected byte rot_A;

		// Token: 0x04001C47 RID: 7239
		protected bool ignoreDequip_A;
	}
}
