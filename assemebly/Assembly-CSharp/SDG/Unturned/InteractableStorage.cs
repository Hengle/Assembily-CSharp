using System;
using SDG.Provider;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004EA RID: 1258
	public class InteractableStorage : Interactable, IManualOnDestroy
	{
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x000B82F9 File Offset: 0x000B66F9
		public CSteamID owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x000B8301 File Offset: 0x000B6701
		public CSteamID group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x000B8309 File Offset: 0x000B6709
		public Items items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060021F8 RID: 8696 RVA: 0x000B8311 File Offset: 0x000B6711
		public bool isDisplay
		{
			get
			{
				return this._isDisplay;
			}
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000B831C File Offset: 0x000B671C
		protected bool getDisplayStatTrackerValue(out EStatTrackerType type, out int kills)
		{
			DynamicEconDetails dynamicEconDetails = new DynamicEconDetails(this.displayTags, this.displayDynamicProps);
			return dynamicEconDetails.getStatTrackerValue(out type, out kills);
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x000B8348 File Offset: 0x000B6748
		private void onStateUpdated()
		{
			if (this.isDisplay)
			{
				this.updateDisplay();
				if (Dedicator.isDedicated)
				{
					BarricadeManager.sendStorageDisplay(base.transform, this.displayItem, this.displaySkin, this.displayMythic, this.displayTags, this.displayDynamicProps);
				}
				this.refreshDisplay();
			}
			this.rebuildState();
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000B83A8 File Offset: 0x000B67A8
		public void rebuildState()
		{
			if (this.items == null)
			{
				return;
			}
			SteamPacker.openWrite(0);
			SteamPacker.write(this.owner, this.group, this.items.getItemCount());
			for (byte b = 0; b < this.items.getItemCount(); b += 1)
			{
				ItemJar item = this.items.getItem(b);
				SteamPacker.write(item.x, item.y, item.rot, item.item.id, item.item.amount, item.item.quality, item.item.state);
			}
			if (this.isDisplay)
			{
				SteamPacker.write(this.displaySkin);
				SteamPacker.write(this.displayMythic);
				SteamPacker.write((!string.IsNullOrEmpty(this.displayTags)) ? this.displayTags : string.Empty);
				SteamPacker.write((!string.IsNullOrEmpty(this.displayDynamicProps)) ? this.displayDynamicProps : string.Empty);
				SteamPacker.write(this.rot_comp);
			}
			int size;
			byte[] state = SteamPacker.closeWrite(out size);
			BarricadeManager.updateState(base.transform, state, size);
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x000B8518 File Offset: 0x000B6918
		private void updateDisplay()
		{
			if (this.items != null && this.items.getItemCount() > 0)
			{
				if (this.displayItem == null || this.items.getItem(0).item != this.displayItem)
				{
					if (this.displayItem != null)
					{
						this.displaySkin = 0;
						this.displayMythic = 0;
						this.displayTags = string.Empty;
						this.displayDynamicProps = string.Empty;
					}
					this.displayItem = this.items.getItem(0).item;
					int item;
					if (this.opener != null && this.opener.channel.owner.getItemSkinItemDefID(this.displayItem.id, out item))
					{
						this.displaySkin = Provider.provider.economyService.getInventorySkinID(item);
						this.displayMythic = Provider.provider.economyService.getInventoryMythicID(item);
						this.opener.channel.owner.getTagsAndDynamicPropsForItem(item, out this.displayTags, out this.displayDynamicProps);
					}
				}
			}
			else
			{
				this.displayItem = null;
				this.displaySkin = 0;
				this.displayMythic = 0;
				this.displayTags = string.Empty;
				this.displayDynamicProps = string.Empty;
			}
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x000B8668 File Offset: 0x000B6A68
		public void setDisplay(ushort id, byte quality, byte[] state, ushort skin, ushort mythic, string tags, string dynamicProps)
		{
			if (id == 0)
			{
				this.displayItem = null;
			}
			else
			{
				this.displayItem = new Item(id, 0, quality, state);
			}
			this.displaySkin = skin;
			this.displayMythic = mythic;
			this.displayTags = tags;
			this.displayDynamicProps = dynamicProps;
			this.refreshDisplay();
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x000B86BC File Offset: 0x000B6ABC
		public byte getRotation(byte rot_x, byte rot_y, byte rot_z)
		{
			return (byte)((int)rot_x << 4 | (int)rot_y << 2 | (int)rot_z);
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x000B86D8 File Offset: 0x000B6AD8
		public void applyRotation(byte rotComp)
		{
			this.rot_comp = rotComp;
			this.rot_x = (byte)(rotComp >> 4 & 3);
			this.rot_y = (byte)(rotComp >> 2 & 3);
			this.rot_z = (rotComp & 3);
			this.displayRotation = Quaternion.Euler((float)(this.rot_x * 90), (float)(this.rot_y * 90), (float)(this.rot_z * 90));
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x000B8737 File Offset: 0x000B6B37
		public void setRotation(byte rotComp)
		{
			this.applyRotation(rotComp);
			this.refreshDisplay();
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x000B8748 File Offset: 0x000B6B48
		public virtual void refreshDisplay()
		{
			if (this.displayModel != null)
			{
				UnityEngine.Object.Destroy(this.displayModel.gameObject);
				this.displayModel = null;
				this.displayAsset = null;
			}
			if (this.displayItem == null)
			{
				return;
			}
			if (this.gunLargeTransform == null || this.gunSmallTransform == null || this.meleeTransform == null || this.itemTransform == null)
			{
				return;
			}
			this.displayAsset = (ItemAsset)Assets.find(EAssetType.ITEM, this.displayItem.id);
			if (this.displayAsset == null)
			{
				return;
			}
			if (this.displaySkin != 0)
			{
				if ((SkinAsset)Assets.find(EAssetType.SKIN, this.displaySkin) == null)
				{
					return;
				}
				this.displayModel = ItemTool.getItem(this.displayItem.id, this.displaySkin, this.displayItem.quality, this.displayItem.state, false, new GetStatTrackerValueHandler(this.getDisplayStatTrackerValue));
				if (this.displayMythic != 0)
				{
					ItemTool.applyEffect(this.displayModel, this.displayMythic, EEffectType.THIRD);
				}
			}
			else
			{
				this.displayModel = ItemTool.getItem(this.displayItem.id, 0, this.displayItem.quality, this.displayItem.state, false, new GetStatTrackerValueHandler(this.getDisplayStatTrackerValue));
				if (this.displayMythic != 0)
				{
					ItemTool.applyEffect(this.displayModel, this.displayMythic, EEffectType.HOOK);
				}
			}
			if (this.displayModel == null)
			{
				return;
			}
			if (this.displayAsset.type == EItemType.GUN)
			{
				if (this.displayAsset.slot == ESlotType.PRIMARY)
				{
					this.displayModel.parent = this.gunLargeTransform;
				}
				else
				{
					this.displayModel.parent = this.gunSmallTransform;
				}
			}
			else if (this.displayAsset.type == EItemType.MELEE)
			{
				this.displayModel.parent = this.meleeTransform;
			}
			else
			{
				this.displayModel.parent = this.itemTransform;
			}
			this.displayModel.localPosition = Vector3.zero;
			this.displayModel.localRotation = this.displayRotation;
			this.displayModel.localScale = Vector3.one;
			UnityEngine.Object.Destroy(this.displayModel.GetComponent<Collider>());
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x000B89B8 File Offset: 0x000B6DB8
		public bool checkRot(CSteamID enemyPlayer, CSteamID enemyGroup)
		{
			return (Provider.isServer && !Dedicator.isDedicated) || !this.isLocked || enemyPlayer == this.owner || (this.group != CSteamID.Nil && enemyGroup == this.group);
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000B8A20 File Offset: 0x000B6E20
		public bool checkStore(CSteamID enemyPlayer, CSteamID enemyGroup)
		{
			return (Provider.isServer && !Dedicator.isDedicated) || ((!this.isLocked || enemyPlayer == this.owner || (this.group != CSteamID.Nil && enemyGroup == this.group)) && !this.isOpen);
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000B8A94 File Offset: 0x000B6E94
		public override void updateState(Asset asset, byte[] state)
		{
			this.gunLargeTransform = base.transform.FindChildRecursive("Gun_Large");
			this.gunSmallTransform = base.transform.FindChildRecursive("Gun_Small");
			this.meleeTransform = base.transform.FindChildRecursive("Melee");
			this.itemTransform = base.transform.FindChildRecursive("Item");
			this.isLocked = ((ItemBarricadeAsset)asset).isLocked;
			this._isDisplay = ((ItemStorageAsset)asset).isDisplay;
			if (Provider.isServer)
			{
				SteamPacker.openRead(0, state);
				this._owner = (CSteamID)SteamPacker.read(Types.STEAM_ID_TYPE);
				this._group = (CSteamID)SteamPacker.read(Types.STEAM_ID_TYPE);
				this._items = new Items(PlayerInventory.STORAGE);
				this.items.resize(((ItemStorageAsset)asset).storage_x, ((ItemStorageAsset)asset).storage_y);
				byte b = (byte)SteamPacker.read(Types.BYTE_TYPE);
				for (byte b2 = 0; b2 < b; b2 += 1)
				{
					if (BarricadeManager.version > 7)
					{
						object[] array = SteamPacker.read(Types.BYTE_TYPE, Types.BYTE_TYPE, Types.BYTE_TYPE, Types.UINT16_TYPE, Types.BYTE_TYPE, Types.BYTE_TYPE, Types.BYTE_ARRAY_TYPE);
						ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, (ushort)array[3]);
						if (itemAsset != null)
						{
							this.items.loadItem((byte)array[0], (byte)array[1], (byte)array[2], new Item((ushort)array[3], (byte)array[4], (byte)array[5], (byte[])array[6]));
						}
					}
					else
					{
						object[] array2 = SteamPacker.read(Types.BYTE_TYPE, Types.BYTE_TYPE, Types.UINT16_TYPE, Types.BYTE_TYPE, Types.BYTE_TYPE, Types.BYTE_ARRAY_TYPE);
						ItemAsset itemAsset2 = (ItemAsset)Assets.find(EAssetType.ITEM, (ushort)array2[2]);
						if (itemAsset2 != null)
						{
							this.items.loadItem((byte)array2[0], (byte)array2[1], 0, new Item((ushort)array2[2], (byte)array2[3], (byte)array2[4], (byte[])array2[5]));
						}
					}
				}
				if (this.isDisplay)
				{
					this.displaySkin = (ushort)SteamPacker.read(Types.UINT16_TYPE);
					this.displayMythic = (ushort)SteamPacker.read(Types.UINT16_TYPE);
					if (BarricadeManager.version > 12)
					{
						this.displayTags = (string)SteamPacker.read(Types.STRING_TYPE);
						this.displayDynamicProps = (string)SteamPacker.read(Types.STRING_TYPE);
					}
					else
					{
						this.displayTags = string.Empty;
						this.displayDynamicProps = string.Empty;
					}
					if (BarricadeManager.version > 8)
					{
						this.applyRotation((byte)SteamPacker.read(Types.BYTE_TYPE));
					}
					else
					{
						this.applyRotation(0);
					}
				}
				this.items.onStateUpdated = new StateUpdated(this.onStateUpdated);
				SteamPacker.closeRead();
				if (this.isDisplay)
				{
					this.updateDisplay();
					this.refreshDisplay();
				}
			}
			else
			{
				Block block = new Block(state);
				this._owner = new CSteamID((ulong)block.read(Types.UINT64_TYPE));
				this._group = new CSteamID((ulong)block.read(Types.UINT64_TYPE));
				if (state.Length > 16)
				{
					object[] array3 = block.read(new Type[]
					{
						Types.UINT16_TYPE,
						Types.BYTE_TYPE,
						Types.BYTE_ARRAY_TYPE,
						Types.UINT16_TYPE,
						Types.UINT16_TYPE,
						Types.STRING_TYPE,
						Types.STRING_TYPE,
						Types.BYTE_TYPE
					});
					this.applyRotation((byte)array3[7]);
					this.setDisplay((ushort)array3[0], (byte)array3[1], (byte[])array3[2], (ushort)array3[3], (ushort)array3[4], (string)array3[5], (string)array3[6]);
				}
			}
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000B8EA5 File Offset: 0x000B72A5
		public override bool checkUseable()
		{
			return this.checkStore(Provider.client, Player.player.quests.groupID) && !PlayerUI.window.showCursor;
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000B8ED6 File Offset: 0x000B72D6
		public override void use()
		{
			BarricadeManager.storeStorage(base.transform, Input.GetKey(ControlsSettings.other));
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000B8EED File Offset: 0x000B72ED
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			text = string.Empty;
			color = Color.white;
			if (this.checkUseable())
			{
				message = EPlayerMessage.STORAGE;
			}
			else
			{
				message = EPlayerMessage.LOCKED;
			}
			return true;
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000B8F1A File Offset: 0x000B731A
		private void Start()
		{
			if (!Provider.isServer)
			{
				return;
			}
			if (BarricadeManager.version < 13)
			{
				this.onStateUpdated();
			}
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000B8F3C File Offset: 0x000B733C
		public void ManualOnDestroy()
		{
			if (this.isDisplay)
			{
				this.setDisplay(0, 0, null, 0, 0, string.Empty, string.Empty);
			}
			if (!Provider.isServer)
			{
				return;
			}
			this.items.onStateUpdated = null;
			if (!this.despawnWhenDestroyed)
			{
				for (byte b = 0; b < this.items.getItemCount(); b += 1)
				{
					ItemJar item = this.items.getItem(b);
					ItemManager.dropItem(item.item, base.transform.position, false, true, true);
				}
			}
			this.items.clear();
			this._items = null;
			if (this.isOpen)
			{
				if (this.opener != null)
				{
					if (this.opener.inventory.isStoring)
					{
						this.opener.inventory.isStoring = false;
						this.opener.inventory.isStorageTrunk = false;
						this.opener.inventory.storage = null;
						this.opener.inventory.updateItems(PlayerInventory.STORAGE, null);
						this.opener.inventory.sendStorage();
					}
					this.opener = null;
				}
				this.isOpen = false;
			}
		}

		// Token: 0x04001440 RID: 5184
		private CSteamID _owner;

		// Token: 0x04001441 RID: 5185
		private CSteamID _group;

		// Token: 0x04001442 RID: 5186
		private Items _items;

		// Token: 0x04001443 RID: 5187
		private Transform gunLargeTransform;

		// Token: 0x04001444 RID: 5188
		private Transform gunSmallTransform;

		// Token: 0x04001445 RID: 5189
		private Transform meleeTransform;

		// Token: 0x04001446 RID: 5190
		private Transform itemTransform;

		// Token: 0x04001447 RID: 5191
		protected Transform displayModel;

		// Token: 0x04001448 RID: 5192
		protected ItemAsset displayAsset;

		// Token: 0x04001449 RID: 5193
		public Item displayItem;

		// Token: 0x0400144A RID: 5194
		public ushort displaySkin;

		// Token: 0x0400144B RID: 5195
		public ushort displayMythic;

		// Token: 0x0400144C RID: 5196
		public string displayTags = string.Empty;

		// Token: 0x0400144D RID: 5197
		public string displayDynamicProps = string.Empty;

		// Token: 0x0400144E RID: 5198
		private Quaternion displayRotation;

		// Token: 0x0400144F RID: 5199
		public byte rot_comp;

		// Token: 0x04001450 RID: 5200
		public byte rot_x;

		// Token: 0x04001451 RID: 5201
		public byte rot_y;

		// Token: 0x04001452 RID: 5202
		public byte rot_z;

		// Token: 0x04001453 RID: 5203
		public bool isOpen;

		// Token: 0x04001454 RID: 5204
		public Player opener;

		// Token: 0x04001455 RID: 5205
		private bool isLocked;

		// Token: 0x04001456 RID: 5206
		private bool _isDisplay;

		// Token: 0x04001457 RID: 5207
		public bool despawnWhenDestroyed;
	}
}
