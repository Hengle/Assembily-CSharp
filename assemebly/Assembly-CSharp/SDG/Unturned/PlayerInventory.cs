﻿using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200061A RID: 1562
	public class PlayerInventory : PlayerCaller
	{
		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002C29 RID: 11305 RVA: 0x001188CF File Offset: 0x00116CCF
		// (set) Token: 0x06002C2A RID: 11306 RVA: 0x001188D7 File Offset: 0x00116CD7
		public Items[] items { get; private set; }

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002C2B RID: 11307 RVA: 0x001188E0 File Offset: 0x00116CE0
		public bool shouldInventoryStopGestureCloseStorage
		{
			get
			{
				return !this.isStorageTrunk;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002C2C RID: 11308 RVA: 0x001188EB File Offset: 0x00116CEB
		public bool shouldInteractCloseStorage
		{
			get
			{
				return !this.isStorageTrunk;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002C2D RID: 11309 RVA: 0x001188F6 File Offset: 0x00116CF6
		public bool shouldStorageOpenDashboard
		{
			get
			{
				return !this.isStorageTrunk;
			}
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x00118901 File Offset: 0x00116D01
		public byte getWidth(byte page)
		{
			return this.items[(int)page].width;
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x00118910 File Offset: 0x00116D10
		public byte getHeight(byte page)
		{
			return this.items[(int)page].height;
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x0011891F File Offset: 0x00116D1F
		public byte getItemCount(byte page)
		{
			return this.items[(int)page].getItemCount();
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x0011892E File Offset: 0x00116D2E
		public ItemJar getItem(byte page, byte index)
		{
			return this.items[(int)page].getItem(index);
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x0011893E File Offset: 0x00116D3E
		public byte getIndex(byte page, byte x, byte y)
		{
			return this.items[(int)page].getIndex(x, y);
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x0011894F File Offset: 0x00116D4F
		public byte findIndex(byte page, byte x, byte y, out byte find_x, out byte find_y)
		{
			find_x = byte.MaxValue;
			find_y = byte.MaxValue;
			return this.items[(int)page].findIndex(x, y, out find_x, out find_y);
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x00118974 File Offset: 0x00116D74
		public void updateAmount(byte page, byte index, byte newAmount)
		{
			if (page >= PlayerInventory.PAGES || this.items == null || this.items[(int)page] == null)
			{
				return;
			}
			this.items[(int)page].updateAmount(index, newAmount);
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x001189AC File Offset: 0x00116DAC
		public void updateQuality(byte page, byte index, byte newQuality)
		{
			if (page >= PlayerInventory.PAGES || this.items == null || this.items[(int)page] == null)
			{
				return;
			}
			this.items[(int)page].updateQuality(index, newQuality);
			ItemJar item = this.items[(int)page].getItem(index);
			if (item != null && base.player.equipment.checkSelection(page, item.x, item.y))
			{
				base.player.equipment.quality = newQuality;
			}
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x00118A34 File Offset: 0x00116E34
		public void updateState(byte page, byte index, byte[] newState)
		{
			if (page >= PlayerInventory.PAGES || this.items == null || this.items[(int)page] == null)
			{
				return;
			}
			this.items[(int)page].updateState(index, newState);
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x00118A6C File Offset: 0x00116E6C
		public List<InventorySearch> search(EItemType type)
		{
			List<InventorySearch> list = new List<InventorySearch>();
			this.search(list, type);
			return list;
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x00118A88 File Offset: 0x00116E88
		public void search(List<InventorySearch> search, EItemType type)
		{
			for (byte b = PlayerInventory.SLOTS; b < PlayerInventory.PAGES - 2; b += 1)
			{
				this.items[(int)b].search(search, type);
			}
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x00118AC4 File Offset: 0x00116EC4
		public List<InventorySearch> search(EItemType type, ushort[] calibers)
		{
			List<InventorySearch> list = new List<InventorySearch>();
			foreach (ushort caliber in calibers)
			{
				this.search(list, type, caliber);
			}
			return list;
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x00118AFC File Offset: 0x00116EFC
		public void search(List<InventorySearch> search, EItemType type, ushort caliber)
		{
			for (byte b = PlayerInventory.SLOTS; b < PlayerInventory.PAGES - 2; b += 1)
			{
				this.items[(int)b].search(search, type, caliber);
			}
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x00118B38 File Offset: 0x00116F38
		public List<InventorySearch> search(ushort id, bool findEmpty, bool findHealthy)
		{
			List<InventorySearch> list = new List<InventorySearch>();
			this.search(list, id, findEmpty, findHealthy);
			return list;
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x00118B58 File Offset: 0x00116F58
		public void search(List<InventorySearch> search, ushort id, bool findEmpty, bool findHealthy)
		{
			for (byte b = PlayerInventory.SLOTS; b < PlayerInventory.PAGES - 2; b += 1)
			{
				this.items[(int)b].search(search, id, findEmpty, findHealthy);
			}
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x00118B98 File Offset: 0x00116F98
		public List<InventorySearch> search(List<InventorySearch> search)
		{
			List<InventorySearch> list = new List<InventorySearch>();
			for (int i = 0; i < search.Count; i++)
			{
				InventorySearch inventorySearch = search[i];
				bool flag = true;
				for (int j = 0; j < list.Count; j++)
				{
					InventorySearch inventorySearch2 = list[j];
					if (inventorySearch2.jar.item.id == inventorySearch.jar.item.id && inventorySearch2.jar.item.amount == inventorySearch.jar.item.amount && inventorySearch2.jar.item.quality == inventorySearch.jar.item.quality)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					list.Add(inventorySearch);
				}
			}
			return list;
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x00118C78 File Offset: 0x00117078
		public InventorySearch has(ushort id)
		{
			for (byte b = 0; b < PlayerInventory.PAGES - 1; b += 1)
			{
				InventorySearch inventorySearch = this.items[(int)b].has(id);
				if (inventorySearch != null)
				{
					return inventorySearch;
				}
			}
			return null;
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x00118CB8 File Offset: 0x001170B8
		public bool tryAddItem(Item item, byte x, byte y, byte page, byte rot)
		{
			if (page >= PlayerInventory.PAGES - 1)
			{
				return false;
			}
			if (item == null)
			{
				return false;
			}
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item.id);
			if (itemAsset == null || itemAsset.isPro)
			{
				return false;
			}
			if (itemAsset.slot == ESlotType.NONE && page < PlayerInventory.SLOTS)
			{
				return false;
			}
			if (x == 255 && y == 255)
			{
				if (!this.items[(int)page].tryAddItem(item))
				{
					return false;
				}
			}
			else
			{
				if (!this.items[(int)page].checkSpaceEmpty(x, y, itemAsset.size_x, itemAsset.size_y, rot))
				{
					return false;
				}
				this.items[(int)page].addItem(x, y, rot, item);
			}
			if (page < PlayerInventory.SLOTS)
			{
				base.player.equipment.sendSlot(page);
			}
			return true;
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x00118DA3 File Offset: 0x001171A3
		public bool tryAddItem(Item item, bool auto)
		{
			return this.tryAddItem(item, auto, true);
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x00118DAE File Offset: 0x001171AE
		public bool tryAddItem(Item item, bool auto, bool playEffect)
		{
			return this.tryAddItemAuto(item, auto, auto, auto, playEffect);
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x00118DBC File Offset: 0x001171BC
		public bool tryAddItemAuto(Item item, bool autoEquipWeapon, bool autoEquipUseable, bool autoEquipClothing, bool playEffect)
		{
			if (item == null)
			{
				return false;
			}
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item.id);
			if (itemAsset == null || itemAsset.isPro)
			{
				return false;
			}
			if (autoEquipWeapon)
			{
				if (itemAsset.slot == ESlotType.PRIMARY)
				{
					if (this.items[0].tryAddItem(item))
					{
						base.player.equipment.sendSlot(0);
						if (!base.player.equipment.isSelected)
						{
							base.player.equipment.tryEquip(0, 0, 0);
						}
						return true;
					}
				}
				else if (itemAsset.slot == ESlotType.SECONDARY)
				{
					if (this.items[1].tryAddItem(item))
					{
						base.player.equipment.sendSlot(1);
						if (!base.player.equipment.isSelected)
						{
							base.player.equipment.tryEquip(1, 0, 0);
						}
						return true;
					}
					if (this.items[0].tryAddItem(item))
					{
						base.player.equipment.sendSlot(0);
						if (!base.player.equipment.isSelected)
						{
							base.player.equipment.tryEquip(0, 0, 0);
						}
						return true;
					}
				}
			}
			if (autoEquipClothing)
			{
				if (base.player.clothing.hat == 0 && itemAsset.type == EItemType.HAT)
				{
					base.player.clothing.askWearHat(item.id, item.quality, item.state, playEffect);
					return true;
				}
				if (base.player.clothing.shirt == 0 && itemAsset.type == EItemType.SHIRT)
				{
					base.player.clothing.askWearShirt(item.id, item.quality, item.state, playEffect);
					return true;
				}
				if (base.player.clothing.pants == 0 && itemAsset.type == EItemType.PANTS)
				{
					base.player.clothing.askWearPants(item.id, item.quality, item.state, playEffect);
					return true;
				}
				if (base.player.clothing.backpack == 0 && itemAsset.type == EItemType.BACKPACK)
				{
					base.player.clothing.askWearBackpack(item.id, item.quality, item.state, playEffect);
					return true;
				}
				if (base.player.clothing.vest == 0 && itemAsset.type == EItemType.VEST)
				{
					base.player.clothing.askWearVest(item.id, item.quality, item.state, playEffect);
					return true;
				}
				if (base.player.clothing.mask == 0 && itemAsset.type == EItemType.MASK)
				{
					base.player.clothing.askWearMask(item.id, item.quality, item.state, playEffect);
					return true;
				}
				if (base.player.clothing.glasses == 0 && itemAsset.type == EItemType.GLASSES)
				{
					base.player.clothing.askWearGlasses(item.id, item.quality, item.state, playEffect);
					return true;
				}
			}
			for (byte b = PlayerInventory.SLOTS; b < PlayerInventory.PAGES - 2; b += 1)
			{
				if (this.items[(int)b].tryAddItem(item))
				{
					if (autoEquipUseable && !base.player.equipment.isSelected && itemAsset.slot == ESlotType.NONE && itemAsset.isUseable)
					{
						ItemJar item2 = this.items[(int)b].getItem(this.items[(int)b].getItemCount() - 1);
						base.player.equipment.tryEquip(b, item2.x, item2.y);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x00119199 File Offset: 0x00117599
		public void forceAddItem(Item item, bool auto)
		{
			this.forceAddItemAuto(item, auto, auto, auto);
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x001191A5 File Offset: 0x001175A5
		public void forceAddItemAuto(Item item, bool autoEquipWeapon, bool autoEquipUseable, bool autoEquipClothing)
		{
			this.forceAddItemAuto(item, autoEquipWeapon, autoEquipUseable, autoEquipClothing, true);
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x001191B3 File Offset: 0x001175B3
		public void forceAddItem(Item item, bool auto, bool playEffect)
		{
			if (!this.tryAddItemAuto(item, auto, auto, auto, playEffect))
			{
				ItemManager.dropItem(item, base.transform.position, false, true, true);
			}
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x001191D9 File Offset: 0x001175D9
		public void forceAddItemAuto(Item item, bool autoEquipWeapon, bool autoEquipUseable, bool autoEquipClothing, bool playEffect)
		{
			if (!this.tryAddItemAuto(item, autoEquipWeapon, autoEquipUseable, autoEquipClothing, playEffect))
			{
				ItemManager.dropItem(item, base.transform.position, false, true, true);
			}
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x00119201 File Offset: 0x00117601
		public void replaceItems(byte page, Items replacement)
		{
			this.items[(int)page] = replacement;
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x0011920C File Offset: 0x0011760C
		public void removeItem(byte page, byte index)
		{
			this.items[(int)page].removeItem(index);
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x0011921C File Offset: 0x0011761C
		public bool checkSpaceEmpty(byte page, byte x, byte y, byte size_x, byte size_y, byte rot)
		{
			return page >= 0 && page < PlayerInventory.PAGES && this.items[(int)page].checkSpaceEmpty(x, y, size_x, size_y, rot);
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x00119248 File Offset: 0x00117648
		public bool checkSpaceDrag(byte page, byte old_x, byte old_y, byte oldRot, byte new_x, byte new_y, byte newRot, byte size_x, byte size_y, bool checkSame)
		{
			return page >= 0 && page < PlayerInventory.PAGES && this.items[(int)page].checkSpaceDrag(old_x, old_y, oldRot, new_x, new_y, newRot, size_x, size_y, checkSame);
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x00119288 File Offset: 0x00117688
		public bool checkSpaceSwap(byte page, byte x, byte y, byte oldSize_X, byte oldSize_Y, byte oldRot, byte newSize_X, byte newSize_Y, byte newRot)
		{
			return page >= 0 && page < PlayerInventory.PAGES && this.items[(int)page].checkSpaceSwap(x, y, oldSize_X, oldSize_Y, oldRot, newSize_X, newSize_Y, newRot);
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x001192C4 File Offset: 0x001176C4
		public bool tryFindSpace(byte page, byte size_x, byte size_y, out byte x, out byte y, out byte rot)
		{
			x = 0;
			y = 0;
			rot = 0;
			return page >= 0 && page < PlayerInventory.PAGES && this.items[(int)page].tryFindSpace(size_x, size_y, out x, out y, out rot);
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x001192FC File Offset: 0x001176FC
		public bool tryFindSpace(byte size_x, byte size_y, out byte page, out byte x, out byte y, out byte rot)
		{
			x = 0;
			y = 0;
			rot = 0;
			for (page = PlayerInventory.SLOTS; page < PlayerInventory.PAGES - 1; page += 1)
			{
				if (this.items[(int)page].tryFindSpace(size_x, size_y, out x, out y, out rot))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x00119354 File Offset: 0x00117754
		[SteamCall]
		public void askDragItem(CSteamID steamID, byte page_0, byte x_0, byte y_0, byte page_1, byte x_1, byte y_1, byte rot_1)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (!base.player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if (base.player.equipment.checkSelection(page_0, x_0, y_0))
				{
					if (base.player.equipment.isBusy)
					{
						return;
					}
					base.player.equipment.dequip();
				}
				else if (base.player.equipment.checkSelection(page_1, x_1, y_1))
				{
					if (base.player.equipment.isBusy)
					{
						return;
					}
					base.player.equipment.dequip();
				}
				if (page_0 < 0 || page_0 >= PlayerInventory.PAGES - 1)
				{
					return;
				}
				if (this.items[(int)page_0] == null)
				{
					return;
				}
				byte index = this.items[(int)page_0].getIndex(x_0, y_0);
				if (index == 255)
				{
					return;
				}
				if (page_1 < 0 || page_1 >= PlayerInventory.PAGES - 1)
				{
					return;
				}
				if (this.items[(int)page_1] == null)
				{
					return;
				}
				if (this.getItemCount(page_1) >= 200)
				{
					return;
				}
				ItemJar item = this.items[(int)page_0].getItem(index);
				if (item == null)
				{
					return;
				}
				if (!this.checkSpaceDrag(page_1, x_0, y_0, item.rot, x_1, y_1, rot_1, item.size_x, item.size_y, page_0 == page_1))
				{
					return;
				}
				ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item.item.id);
				if (itemAsset == null)
				{
					return;
				}
				if (page_1 < PlayerInventory.SLOTS && (itemAsset.slot == ESlotType.NONE || (page_1 == 1 && itemAsset.slot != ESlotType.SECONDARY)))
				{
					return;
				}
				if (page_1 < PlayerInventory.SLOTS)
				{
					rot_1 = 0;
				}
				this.removeItem(page_0, index);
				this.items[(int)page_1].addItem(x_1, y_1, rot_1, item.item);
				if (page_0 < PlayerInventory.SLOTS)
				{
					base.player.equipment.sendSlot(page_0);
				}
				if (page_1 < PlayerInventory.SLOTS)
				{
					base.player.equipment.sendSlot(page_1);
				}
			}
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x00119584 File Offset: 0x00117984
		[SteamCall]
		public void askSwapItem(CSteamID steamID, byte page_0, byte x_0, byte y_0, byte page_1, byte x_1, byte y_1)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (!base.player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if (base.player.equipment.checkSelection(page_0, x_0, y_0))
				{
					if (base.player.equipment.isBusy)
					{
						return;
					}
					base.player.equipment.dequip();
				}
				else if (base.player.equipment.checkSelection(page_1, x_1, y_1))
				{
					if (base.player.equipment.isBusy)
					{
						return;
					}
					base.player.equipment.dequip();
				}
				if (page_0 == page_1 && x_0 == x_1 && y_0 == y_1)
				{
					return;
				}
				if (page_0 < 0 || page_0 >= PlayerInventory.PAGES - 1)
				{
					return;
				}
				if (this.items[(int)page_0] == null)
				{
					return;
				}
				byte index = this.items[(int)page_0].getIndex(x_0, y_0);
				if (index == 255)
				{
					return;
				}
				if (page_1 < 0 || page_1 >= PlayerInventory.PAGES - 1)
				{
					return;
				}
				if (this.items[(int)page_1] == null)
				{
					return;
				}
				byte b = this.items[(int)page_1].getIndex(x_1, y_1);
				if (b == 255)
				{
					return;
				}
				ItemJar item = this.items[(int)page_0].getItem(index);
				if (item == null)
				{
					return;
				}
				ItemJar item2 = this.items[(int)page_1].getItem(b);
				if (item2 == null)
				{
					return;
				}
				if (!this.checkSpaceSwap(page_0, x_0, y_0, item.size_x, item.size_y, item.rot, item2.size_x, item2.size_y, item.rot))
				{
					return;
				}
				if (!this.checkSpaceSwap(page_1, x_1, y_1, item2.size_x, item2.size_y, item2.rot, item.size_x, item.size_y, item2.rot))
				{
					return;
				}
				ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item.item.id);
				if (itemAsset == null)
				{
					return;
				}
				if (page_1 < PlayerInventory.SLOTS && (itemAsset.slot == ESlotType.NONE || (page_1 == 1 && itemAsset.slot != ESlotType.SECONDARY)))
				{
					return;
				}
				ItemAsset itemAsset2 = (ItemAsset)Assets.find(EAssetType.ITEM, item2.item.id);
				if (itemAsset2 == null)
				{
					return;
				}
				if (page_0 < PlayerInventory.SLOTS && (itemAsset2.slot == ESlotType.NONE || (page_0 == 1 && itemAsset2.slot != ESlotType.SECONDARY)))
				{
					return;
				}
				this.removeItem(page_0, index);
				if (page_0 == page_1 && b > index)
				{
					b -= 1;
				}
				this.removeItem(page_1, b);
				this.items[(int)page_0].addItem(x_0, y_0, item.rot, item2.item);
				this.items[(int)page_1].addItem(x_1, y_1, item2.rot, item.item);
				if (page_0 < PlayerInventory.SLOTS)
				{
					base.player.equipment.sendSlot(page_0);
				}
				if (page_1 < PlayerInventory.SLOTS)
				{
					base.player.equipment.sendSlot(page_1);
				}
			}
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x001198AC File Offset: 0x00117CAC
		public void sendDragItem(byte page_0, byte x_0, byte y_0, byte page_1, byte x_1, byte y_1, byte rot_1)
		{
			base.channel.send("askDragItem", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				page_0,
				x_0,
				y_0,
				page_1,
				x_1,
				y_1,
				rot_1
			});
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x00119918 File Offset: 0x00117D18
		public void sendSwapItem(byte page_0, byte x_0, byte y_0, byte page_1, byte x_1, byte y_1)
		{
			base.channel.send("askSwapItem", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				page_0,
				x_0,
				y_0,
				page_1,
				x_1,
				y_1
			});
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x00119978 File Offset: 0x00117D78
		[SteamCall]
		public void askDropItem(CSteamID steamID, byte page, byte x, byte y)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (!base.player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if (base.player.equipment.checkSelection(page, x, y))
				{
					if (base.player.equipment.isBusy)
					{
						return;
					}
					base.player.equipment.dequip();
				}
				if (page < 0 || page >= PlayerInventory.PAGES - 1)
				{
					return;
				}
				if (this.items[(int)page] == null)
				{
					return;
				}
				if (this.items == null)
				{
					return;
				}
				byte index = this.items[(int)page].getIndex(x, y);
				if (index == 255)
				{
					return;
				}
				ItemJar item = this.items[(int)page].getItem(index);
				if (item == null)
				{
					return;
				}
				ItemManager.dropItem(item.item, base.transform.position + base.transform.forward * 0.5f, true, true, false);
				this.removeItem(page, index);
				if (page < PlayerInventory.SLOTS)
				{
					base.player.equipment.sendSlot(page);
				}
			}
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x00119AA9 File Offset: 0x00117EA9
		public void sendDropItem(byte page, byte x, byte y)
		{
			base.channel.send("askDropItem", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				page,
				x,
				y
			});
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x00119ADF File Offset: 0x00117EDF
		[SteamCall]
		public void tellUpdateAmount(CSteamID steamID, byte page, byte index, byte amount)
		{
			if (base.channel.checkServer(steamID))
			{
				this.updateAmount(page, index, amount);
			}
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x00119AFC File Offset: 0x00117EFC
		[SteamCall]
		public void tellUpdateQuality(CSteamID steamID, byte page, byte index, byte quality)
		{
			if (base.channel.checkServer(steamID))
			{
				this.updateQuality(page, index, quality);
			}
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x00119B19 File Offset: 0x00117F19
		[SteamCall]
		public void tellUpdateInvState(CSteamID steamID, byte page, byte index, byte[] state)
		{
			if (base.channel.checkServer(steamID))
			{
				this.updateState(page, index, state);
			}
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x00119B38 File Offset: 0x00117F38
		[SteamCall]
		public void tellItemAdd(CSteamID steamID, byte page, byte x, byte y, byte rot, ushort id, byte amount, byte quality, byte[] state)
		{
			if (base.channel.checkServer(steamID))
			{
				if (page >= PlayerInventory.PAGES || this.items == null || this.items[(int)page] == null)
				{
					return;
				}
				this.items[(int)page].addItem(x, y, rot, new Item(id, amount, quality, state));
			}
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x00119B9C File Offset: 0x00117F9C
		[SteamCall]
		public void tellItemRemove(CSteamID steamID, byte page, byte x, byte y)
		{
			if (base.channel.checkServer(steamID))
			{
				if (page >= PlayerInventory.PAGES || this.items == null || this.items[(int)page] == null)
				{
					return;
				}
				byte index = this.items[(int)page].getIndex(x, y);
				if (index == 255)
				{
					return;
				}
				this.items[(int)page].removeItem(index);
			}
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x00119C0C File Offset: 0x0011800C
		[SteamCall]
		public void tellSize(CSteamID steamID, byte page, byte newWidth, byte newHeight)
		{
			if (base.channel.checkServer(steamID))
			{
				if (page >= PlayerInventory.PAGES || this.items == null || this.items[(int)page] == null)
				{
					return;
				}
				this.items[(int)page].resize(newWidth, newHeight);
			}
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x00119C60 File Offset: 0x00118060
		[SteamCall]
		public void tellStoraging(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID))
			{
				this.isStorageTrunk = (bool)base.channel.read(Types.BOOLEAN_TYPE);
				object[] array = base.channel.read(Types.BYTE_TYPE, Types.BYTE_TYPE, Types.BYTE_TYPE);
				this.items[(int)PlayerInventory.STORAGE].resize((byte)array[0], (byte)array[1]);
				byte b = (byte)array[2];
				for (byte b2 = 0; b2 < b; b2 += 1)
				{
					object[] array2 = base.channel.read(Types.BYTE_TYPE, Types.BYTE_TYPE, Types.BYTE_TYPE, Types.UINT16_TYPE, Types.BYTE_TYPE, Types.BYTE_TYPE, Types.BYTE_ARRAY_TYPE);
					this.items[(int)PlayerInventory.STORAGE].addItem((byte)array2[0], (byte)array2[1], (byte)array2[2], new Item((ushort)array2[3], (byte)array2[4], (byte)array2[5], (byte[])array2[6]));
				}
				this.isStoring = (this.items[(int)PlayerInventory.STORAGE].height > 0);
				if (this.isStoring && this.onInventoryStored != null)
				{
					this.onInventoryStored();
				}
			}
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x00119DAC File Offset: 0x001181AC
		[SteamCall]
		public void tellInventory(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID))
			{
				Player.isLoadingInventory = false;
				for (byte b = 0; b < PlayerInventory.PAGES - 2; b += 1)
				{
					object[] array = base.channel.read(Types.BYTE_TYPE, Types.BYTE_TYPE, Types.BYTE_TYPE);
					this.items[(int)b].resize((byte)array[0], (byte)array[1]);
					byte b2 = (byte)array[2];
					for (byte b3 = 0; b3 < b2; b3 += 1)
					{
						object[] array2 = base.channel.read(Types.BYTE_TYPE, Types.BYTE_TYPE, Types.BYTE_TYPE, Types.UINT16_TYPE, Types.BYTE_TYPE, Types.BYTE_TYPE, Types.BYTE_ARRAY_TYPE);
						this.items[(int)b].addItem((byte)array2[0], (byte)array2[1], (byte)array2[2], new Item((ushort)array2[3], (byte)array2[4], (byte)array2[5], (byte[])array2[6]));
					}
				}
			}
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x00119EC0 File Offset: 0x001182C0
		[SteamCall]
		public void askInventory(CSteamID steamID)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (!base.channel.isOwner)
				{
					base.channel.openWrite();
				}
				for (byte b = 0; b < PlayerInventory.PAGES - 2; b += 1)
				{
					if (base.channel.isOwner)
					{
						this.onInventoryResized(b, this.items[(int)b].width, this.items[(int)b].height);
					}
					else
					{
						base.channel.write(this.items[(int)b].width, this.items[(int)b].height, this.items[(int)b].getItemCount());
					}
					for (byte b2 = 0; b2 < this.items[(int)b].getItemCount(); b2 += 1)
					{
						ItemJar item = this.items[(int)b].getItem(b2);
						if (base.channel.isOwner)
						{
							this.onItemAdded(b, b2, item);
						}
						else
						{
							base.channel.write(item.x, item.y, item.rot, item.item.id, item.item.amount, item.item.quality, item.item.state);
						}
					}
				}
				if (base.channel.isOwner)
				{
					Player.isLoadingInventory = false;
				}
				else
				{
					base.channel.closeWrite("tellInventory", steamID, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
				}
			}
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x0011A07C File Offset: 0x0011847C
		public void sendStorage()
		{
			if (!base.channel.isOwner)
			{
				base.channel.openWrite();
				base.channel.write(this.isStorageTrunk);
			}
			if (base.channel.isOwner)
			{
				this.onInventoryResized(PlayerInventory.STORAGE, this.items[(int)PlayerInventory.STORAGE].width, this.items[(int)PlayerInventory.STORAGE].height);
				if (this.items[(int)PlayerInventory.STORAGE].height > 0 && this.onInventoryStored != null)
				{
					this.onInventoryStored();
				}
			}
			else
			{
				base.channel.write(this.items[(int)PlayerInventory.STORAGE].width, this.items[(int)PlayerInventory.STORAGE].height, this.items[(int)PlayerInventory.STORAGE].getItemCount());
			}
			for (byte b = 0; b < this.items[(int)PlayerInventory.STORAGE].getItemCount(); b += 1)
			{
				ItemJar item = this.items[(int)PlayerInventory.STORAGE].getItem(b);
				if (base.channel.isOwner)
				{
					this.onItemAdded(PlayerInventory.STORAGE, b, item);
				}
				else
				{
					base.channel.write(item.x, item.y, item.rot, item.item.id, item.item.amount, item.item.quality, item.item.state);
				}
			}
			if (!base.channel.isOwner)
			{
				base.channel.closeWrite("tellStoraging", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
			}
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x0011A260 File Offset: 0x00118660
		public void updateItems(byte page, Items newItems)
		{
			if (this.items[(int)page] != null)
			{
				Items items = this.items[(int)page];
				items.onItemsResized = (ItemsResized)Delegate.Remove(items.onItemsResized, new ItemsResized(this.onItemsResized));
				Items items2 = this.items[(int)page];
				items2.onItemUpdated = (ItemUpdated)Delegate.Remove(items2.onItemUpdated, new ItemUpdated(this.onItemUpdated));
				Items items3 = this.items[(int)page];
				items3.onItemAdded = (ItemAdded)Delegate.Remove(items3.onItemAdded, new ItemAdded(this.onItemAdded));
				Items items4 = this.items[(int)page];
				items4.onItemRemoved = (ItemRemoved)Delegate.Remove(items4.onItemRemoved, new ItemRemoved(this.onItemRemoved));
				Items items5 = this.items[(int)page];
				items5.onStateUpdated = (StateUpdated)Delegate.Remove(items5.onStateUpdated, new StateUpdated(this.onItemStateUpdated));
			}
			if (newItems != null)
			{
				this.items[(int)page] = newItems;
				Items items6 = this.items[(int)page];
				items6.onItemsResized = (ItemsResized)Delegate.Combine(items6.onItemsResized, new ItemsResized(this.onItemsResized));
				Items items7 = this.items[(int)page];
				items7.onItemUpdated = (ItemUpdated)Delegate.Combine(items7.onItemUpdated, new ItemUpdated(this.onItemUpdated));
				Items items8 = this.items[(int)page];
				items8.onItemAdded = (ItemAdded)Delegate.Combine(items8.onItemAdded, new ItemAdded(this.onItemAdded));
				Items items9 = this.items[(int)page];
				items9.onItemRemoved = (ItemRemoved)Delegate.Combine(items9.onItemRemoved, new ItemRemoved(this.onItemRemoved));
				Items items10 = this.items[(int)page];
				items10.onStateUpdated = (StateUpdated)Delegate.Combine(items10.onStateUpdated, new StateUpdated(this.onItemStateUpdated));
			}
			else
			{
				this.items[(int)page] = new Items(PlayerInventory.STORAGE);
				Items items11 = this.items[(int)page];
				items11.onItemsResized = (ItemsResized)Delegate.Combine(items11.onItemsResized, new ItemsResized(this.onItemsResized));
				Items items12 = this.items[(int)page];
				items12.onItemUpdated = (ItemUpdated)Delegate.Combine(items12.onItemUpdated, new ItemUpdated(this.onItemUpdated));
				Items items13 = this.items[(int)page];
				items13.onItemAdded = (ItemAdded)Delegate.Combine(items13.onItemAdded, new ItemAdded(this.onItemAdded));
				Items items14 = this.items[(int)page];
				items14.onItemRemoved = (ItemRemoved)Delegate.Combine(items14.onItemRemoved, new ItemRemoved(this.onItemRemoved));
				Items items15 = this.items[(int)page];
				items15.onStateUpdated = (StateUpdated)Delegate.Combine(items15.onStateUpdated, new StateUpdated(this.onItemStateUpdated));
				if (this.onInventoryResized != null)
				{
					this.onInventoryResized(page, 0, 0);
				}
			}
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x0011A520 File Offset: 0x00118920
		public void sendUpdateAmount(byte page, byte x, byte y, byte amount)
		{
			byte index = this.getIndex(page, x, y);
			this.updateAmount(page, index, amount);
			if (!base.channel.isOwner)
			{
				base.channel.send("tellUpdateAmount", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					page,
					index,
					amount
				});
			}
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x0011A588 File Offset: 0x00118988
		public void sendUpdateQuality(byte page, byte x, byte y, byte quality)
		{
			byte index = this.getIndex(page, x, y);
			this.updateQuality(page, index, quality);
			if (!base.channel.isOwner)
			{
				base.channel.send("tellUpdateQuality", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					page,
					index,
					quality
				});
			}
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x0011A5F0 File Offset: 0x001189F0
		public void sendUpdateInvState(byte page, byte x, byte y, byte[] state)
		{
			byte index = this.getIndex(page, x, y);
			this.updateState(page, index, state);
			if (!base.channel.isOwner)
			{
				base.channel.send("tellUpdateInvState", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					page,
					index,
					state
				});
			}
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x0011A654 File Offset: 0x00118A54
		private void sendItemAdd(byte page, ItemJar jar)
		{
			base.channel.send("tellItemAdd", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				page,
				jar.x,
				jar.y,
				jar.rot,
				jar.item.id,
				jar.item.amount,
				jar.item.quality,
				jar.item.state
			});
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x0011A6F4 File Offset: 0x00118AF4
		private void sendItemRemove(byte page, ItemJar jar)
		{
			base.channel.send("tellItemRemove", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				page,
				jar.x,
				jar.y
			});
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x0011A734 File Offset: 0x00118B34
		private void bestowLoadout()
		{
			if (PlayerInventory.loadout != null && PlayerInventory.loadout.Length > 0)
			{
				for (int i = 0; i < PlayerInventory.loadout.Length; i++)
				{
					this.tryAddItem(new Item(PlayerInventory.loadout[i], EItemOrigin.ADMIN), true, false);
				}
			}
			else if (Level.info != null)
			{
				if (PlayerInventory.skillsets != null && PlayerInventory.skillsets[(int)((byte)base.channel.owner.skillset)] != null && PlayerInventory.skillsets[(int)((byte)base.channel.owner.skillset)].Length > 0)
				{
					for (int j = 0; j < PlayerInventory.skillsets[(int)((byte)base.channel.owner.skillset)].Length; j++)
					{
						this.tryAddItem(new Item(PlayerInventory.skillsets[(int)((byte)base.channel.owner.skillset)][j], EItemOrigin.WORLD), true, false);
					}
				}
				else if (Level.info.type == ELevelType.HORDE)
				{
					for (int k = 0; k < PlayerInventory.HORDE.Length; k++)
					{
						this.tryAddItem(new Item(PlayerInventory.HORDE[k], EItemOrigin.ADMIN), true, false);
					}
				}
			}
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x0011A870 File Offset: 0x00118C70
		private void onShirtUpdated(ushort id, byte quality, byte[] state)
		{
			if (id != 0)
			{
				ItemBagAsset itemBagAsset = (ItemBagAsset)Assets.find(EAssetType.ITEM, id);
				if (itemBagAsset != null)
				{
					this.items[(int)PlayerInventory.SHIRT].resize(itemBagAsset.width, itemBagAsset.height);
				}
			}
			else
			{
				this.items[(int)PlayerInventory.SHIRT].resize(0, 0);
			}
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x0011A8CC File Offset: 0x00118CCC
		private void onPantsUpdated(ushort id, byte quality, byte[] state)
		{
			if (id != 0)
			{
				ItemBagAsset itemBagAsset = (ItemBagAsset)Assets.find(EAssetType.ITEM, id);
				if (itemBagAsset != null)
				{
					this.items[(int)PlayerInventory.PANTS].resize(itemBagAsset.width, itemBagAsset.height);
				}
			}
			else
			{
				this.items[(int)PlayerInventory.PANTS].resize(0, 0);
			}
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x0011A928 File Offset: 0x00118D28
		private void onBackpackUpdated(ushort id, byte quality, byte[] state)
		{
			if (id != 0)
			{
				ItemBagAsset itemBagAsset = (ItemBagAsset)Assets.find(EAssetType.ITEM, id);
				if (itemBagAsset != null)
				{
					this.items[(int)PlayerInventory.BACKPACK].resize(itemBagAsset.width, itemBagAsset.height);
				}
			}
			else
			{
				this.items[(int)PlayerInventory.BACKPACK].resize(0, 0);
			}
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x0011A984 File Offset: 0x00118D84
		private void onVestUpdated(ushort id, byte quality, byte[] state)
		{
			if (id != 0)
			{
				ItemBagAsset itemBagAsset = (ItemBagAsset)Assets.find(EAssetType.ITEM, id);
				if (itemBagAsset != null)
				{
					this.items[(int)PlayerInventory.VEST].resize(itemBagAsset.width, itemBagAsset.height);
				}
			}
			else
			{
				this.items[(int)PlayerInventory.VEST].resize(0, 0);
			}
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x0011A9E0 File Offset: 0x00118DE0
		private void onLifeUpdated(bool isDead)
		{
			if ((Provider.isServer || base.channel.isOwner) && isDead && this.isStoring)
			{
				this.isStoring = false;
				this.isStorageTrunk = false;
				if (this.storage != null)
				{
					if (Provider.isServer)
					{
						this.storage.isOpen = false;
						this.storage.opener = null;
					}
					this.storage = null;
				}
				this.updateItems(PlayerInventory.STORAGE, null);
			}
			if (Provider.isServer)
			{
				bool flag = (!base.player.life.wasPvPDeath) ? Provider.modeConfigData.Players.Lose_Clothes_PvE : Provider.modeConfigData.Players.Lose_Clothes_PvP;
				if (flag)
				{
					if (isDead)
					{
						for (byte b = 0; b < PlayerInventory.PAGES - 2; b += 1)
						{
							this.items[(int)b].resize(0, 0);
						}
					}
					else
					{
						this.items[0].resize(1, 1);
						this.items[1].resize(1, 1);
						this.items[2].resize(5, 3);
						this.bestowLoadout();
					}
				}
				else if (isDead)
				{
					this.items[0].resize(0, 0);
					this.items[1].resize(0, 0);
					float num = (!base.player.life.wasPvPDeath) ? Provider.modeConfigData.Players.Lose_Items_PvE : Provider.modeConfigData.Players.Lose_Items_PvP;
					for (byte b2 = PlayerInventory.SLOTS; b2 < PlayerInventory.PAGES - 2; b2 += 1)
					{
						if (this.items[(int)b2].getItemCount() > 0)
						{
							for (int i = (int)(this.items[(int)b2].getItemCount() - 1); i >= 0; i--)
							{
								if (UnityEngine.Random.value < num)
								{
									ItemJar item = this.items[(int)b2].getItem((byte)i);
									ItemManager.dropItem(item.item, base.transform.position, false, true, true);
									this.items[(int)b2].removeItem((byte)i);
								}
							}
						}
					}
				}
				else
				{
					this.items[0].resize(1, 1);
					this.items[1].resize(1, 1);
				}
			}
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x0011AC3C File Offset: 0x0011903C
		private void onItemsResized(byte page, byte newWidth, byte newHeight)
		{
			if (!base.channel.isOwner && Provider.isServer)
			{
				base.channel.send("tellSize", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					page,
					newWidth,
					newHeight
				});
			}
			if (this.onInventoryResized != null)
			{
				this.onInventoryResized(page, newWidth, newHeight);
			}
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x0011ACB0 File Offset: 0x001190B0
		private void onItemUpdated(byte page, byte index, ItemJar jar)
		{
			if (this.onInventoryUpdated != null)
			{
				this.onInventoryUpdated(page, index, jar);
			}
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x0011ACCB File Offset: 0x001190CB
		private void onItemAdded(byte page, byte index, ItemJar jar)
		{
			if (!base.channel.isOwner && Provider.isServer)
			{
				this.sendItemAdd(page, jar);
			}
			if (this.onInventoryAdded != null)
			{
				this.onInventoryAdded(page, index, jar);
			}
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x0011AD08 File Offset: 0x00119108
		private void onItemRemoved(byte page, byte index, ItemJar jar)
		{
			if (Provider.isServer)
			{
				if (!base.channel.isOwner)
				{
					this.sendItemRemove(page, jar);
				}
				if (base.player.equipment.checkSelection(page, jar.x, jar.y))
				{
					base.player.equipment.dequip();
				}
			}
			if (this.onInventoryRemoved != null)
			{
				this.onInventoryRemoved(page, index, jar);
			}
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x0011AD84 File Offset: 0x00119184
		private void onItemDiscarded(byte page, byte index, ItemJar jar)
		{
			if (Provider.isServer)
			{
				if (!base.channel.isOwner)
				{
					this.sendItemRemove(page, jar);
				}
				if (base.player.equipment.checkSelection(page, jar.x, jar.y))
				{
					base.player.equipment.dequip();
				}
				if (this.onInventoryRemoved != null)
				{
					this.onInventoryRemoved(page, index, jar);
				}
				ItemManager.dropItem(jar.item, base.transform.position, false, true, true);
			}
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x0011AE17 File Offset: 0x00119217
		private void onItemStateUpdated()
		{
			if (this.onInventoryStateUpdated != null)
			{
				this.onInventoryStateUpdated();
			}
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x0011AE2F File Offset: 0x0011922F
		public void init()
		{
			base.channel.send("askInventory", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x0011AE4C File Offset: 0x0011924C
		private void OnDestroy()
		{
			if (this.isStoring)
			{
				this.isStoring = false;
				this.isStorageTrunk = false;
				if (this.storage != null)
				{
					this.storage.isOpen = false;
					this.storage.opener = null;
					this.storage = null;
				}
				this.updateItems(PlayerInventory.STORAGE, null);
			}
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x0011AEB0 File Offset: 0x001192B0
		private void Start()
		{
			this.items = new Items[(int)PlayerInventory.PAGES];
			for (byte b = 0; b < PlayerInventory.PAGES - 1; b += 1)
			{
				this.items[(int)b] = new Items(b);
				Items items = this.items[(int)b];
				items.onItemsResized = (ItemsResized)Delegate.Combine(items.onItemsResized, new ItemsResized(this.onItemsResized));
				Items items2 = this.items[(int)b];
				items2.onItemUpdated = (ItemUpdated)Delegate.Combine(items2.onItemUpdated, new ItemUpdated(this.onItemUpdated));
				Items items3 = this.items[(int)b];
				items3.onItemAdded = (ItemAdded)Delegate.Combine(items3.onItemAdded, new ItemAdded(this.onItemAdded));
				Items items4 = this.items[(int)b];
				items4.onItemRemoved = (ItemRemoved)Delegate.Combine(items4.onItemRemoved, new ItemRemoved(this.onItemRemoved));
				Items items5 = this.items[(int)b];
				items5.onStateUpdated = (StateUpdated)Delegate.Combine(items5.onStateUpdated, new StateUpdated(this.onItemStateUpdated));
			}
			if (base.channel.isOwner || Provider.isServer)
			{
				PlayerLife life = base.player.life;
				life.onLifeUpdated = (LifeUpdated)Delegate.Combine(life.onLifeUpdated, new LifeUpdated(this.onLifeUpdated));
			}
			if (Provider.isServer)
			{
				PlayerClothing clothing = base.player.clothing;
				clothing.onShirtUpdated = (ShirtUpdated)Delegate.Combine(clothing.onShirtUpdated, new ShirtUpdated(this.onShirtUpdated));
				PlayerClothing clothing2 = base.player.clothing;
				clothing2.onPantsUpdated = (PantsUpdated)Delegate.Combine(clothing2.onPantsUpdated, new PantsUpdated(this.onPantsUpdated));
				PlayerClothing clothing3 = base.player.clothing;
				clothing3.onBackpackUpdated = (BackpackUpdated)Delegate.Combine(clothing3.onBackpackUpdated, new BackpackUpdated(this.onBackpackUpdated));
				PlayerClothing clothing4 = base.player.clothing;
				clothing4.onVestUpdated = (VestUpdated)Delegate.Combine(clothing4.onVestUpdated, new VestUpdated(this.onVestUpdated));
				for (byte b2 = 0; b2 < PlayerInventory.PAGES - 1; b2 += 1)
				{
					this.items[(int)b2].onItemDiscarded = new ItemDiscarded(this.onItemDiscarded);
				}
				this.load();
			}
			if (base.channel.isOwner)
			{
				base.Invoke("init", 0.1f);
			}
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x0011B11C File Offset: 0x0011951C
		public void load()
		{
			if (PlayerSavedata.fileExists(base.channel.owner.playerID, "/Player/Inventory.dat") && Level.info.type == ELevelType.SURVIVAL)
			{
				Block block = PlayerSavedata.readBlock(base.channel.owner.playerID, "/Player/Inventory.dat", 0);
				byte b = block.readByte();
				if (b > 3)
				{
					for (byte b2 = 0; b2 < PlayerInventory.PAGES - 2; b2 += 1)
					{
						this.items[(int)b2].loadSize(block.readByte(), block.readByte());
						byte b3 = block.readByte();
						for (byte b4 = 0; b4 < b3; b4 += 1)
						{
							byte x = block.readByte();
							byte y = block.readByte();
							byte rot = 0;
							if (b > 4)
							{
								rot = block.readByte();
							}
							ushort num = block.readUInt16();
							byte newAmount = block.readByte();
							byte newQuality = block.readByte();
							byte[] newState = block.readByteArray();
							ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, num);
							if (itemAsset != null)
							{
								this.items[(int)b2].loadItem(x, y, rot, new Item(num, newAmount, newQuality, newState));
							}
						}
					}
				}
				else
				{
					this.items[0].loadSize(1, 1);
					this.items[1].loadSize(1, 1);
					this.items[2].loadSize(5, 3);
					this.items[(int)PlayerInventory.BACKPACK].loadSize(0, 0);
					this.items[(int)PlayerInventory.VEST].loadSize(0, 0);
					this.items[(int)PlayerInventory.SHIRT].loadSize(0, 0);
					this.items[(int)PlayerInventory.PANTS].loadSize(0, 0);
					this.items[(int)PlayerInventory.STORAGE].loadSize(0, 0);
					this.bestowLoadout();
				}
			}
			else
			{
				this.items[0].loadSize(1, 1);
				this.items[1].loadSize(1, 1);
				this.items[2].loadSize(5, 3);
				this.items[(int)PlayerInventory.BACKPACK].loadSize(0, 0);
				this.items[(int)PlayerInventory.VEST].loadSize(0, 0);
				this.items[(int)PlayerInventory.SHIRT].loadSize(0, 0);
				this.items[(int)PlayerInventory.PANTS].loadSize(0, 0);
				this.items[(int)PlayerInventory.STORAGE].loadSize(0, 0);
				this.bestowLoadout();
			}
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x0011B378 File Offset: 0x00119778
		public void save()
		{
			bool flag = (!base.player.life.wasPvPDeath) ? Provider.modeConfigData.Players.Lose_Clothes_PvE : Provider.modeConfigData.Players.Lose_Clothes_PvP;
			if (base.player.life.isDead && flag)
			{
				if (PlayerSavedata.fileExists(base.channel.owner.playerID, "/Player/Inventory.dat"))
				{
					PlayerSavedata.deleteFile(base.channel.owner.playerID, "/Player/Inventory.dat");
				}
			}
			else
			{
				Block block = new Block();
				block.writeByte(PlayerInventory.SAVEDATA_VERSION);
				for (byte b = 0; b < PlayerInventory.PAGES - 2; b += 1)
				{
					block.writeByte(this.items[(int)b].width);
					block.writeByte(this.items[(int)b].height);
					block.writeByte(this.items[(int)b].getItemCount());
					for (byte b2 = 0; b2 < this.items[(int)b].getItemCount(); b2 += 1)
					{
						ItemJar item = this.items[(int)b].getItem(b2);
						block.writeByte(item.x);
						block.writeByte(item.y);
						block.writeByte(item.rot);
						block.writeUInt16(item.item.id);
						block.writeByte(item.item.amount);
						block.writeByte(item.item.quality);
						block.writeByteArray(item.item.state);
					}
				}
				PlayerSavedata.writeBlock(base.channel.owner.playerID, "/Player/Inventory.dat", block);
			}
		}

		// Token: 0x04001C84 RID: 7300
		public static readonly ushort[] LOADOUT = new ushort[0];

		// Token: 0x04001C85 RID: 7301
		public static readonly ushort[] HORDE = new ushort[]
		{
			97,
			98,
			98,
			98
		};

		// Token: 0x04001C86 RID: 7302
		public static readonly ushort[][] SKILLSETS_SERVER = new ushort[][]
		{
			new ushort[0],
			new ushort[0],
			new ushort[0],
			new ushort[0],
			new ushort[0],
			new ushort[0],
			new ushort[0],
			new ushort[0],
			new ushort[0],
			new ushort[0],
			new ushort[0]
		};

		// Token: 0x04001C87 RID: 7303
		public static readonly ushort[][] SKILLSETS_CLIENT = new ushort[][]
		{
			new ushort[]
			{
				180,
				214
			},
			new ushort[]
			{
				233,
				234,
				241
			},
			new ushort[]
			{
				223,
				224,
				225
			},
			new ushort[]
			{
				1171,
				1172
			},
			new ushort[]
			{
				242,
				243,
				244
			},
			new ushort[]
			{
				510,
				511,
				509
			},
			new ushort[]
			{
				211,
				213
			},
			new ushort[]
			{
				232,
				2,
				240
			},
			new ushort[]
			{
				230,
				231,
				239
			},
			new ushort[]
			{
				1156,
				1157
			},
			new ushort[]
			{
				311,
				312
			}
		};

		// Token: 0x04001C88 RID: 7304
		public static readonly ushort[][] SKILLSETS_HERO = new ushort[][]
		{
			new ushort[]
			{
				180,
				214
			},
			new ushort[]
			{
				233,
				234,
				241,
				104
			},
			new ushort[]
			{
				223,
				224,
				225,
				10,
				112,
				99
			},
			new ushort[]
			{
				1171,
				1172,
				1169,
				334,
				297,
				1027
			},
			new ushort[]
			{
				242,
				243,
				244,
				101,
				1034
			},
			new ushort[]
			{
				510,
				511,
				509
			},
			new ushort[]
			{
				211,
				213,
				16
			},
			new ushort[]
			{
				232,
				2,
				240,
				138
			},
			new ushort[]
			{
				230,
				231,
				239,
				137
			},
			new ushort[]
			{
				1156,
				1157,
				434,
				122,
				1036
			},
			new ushort[]
			{
				311,
				312
			}
		};

		// Token: 0x04001C89 RID: 7305
		public static readonly byte SAVEDATA_VERSION = 5;

		// Token: 0x04001C8A RID: 7306
		public static readonly byte SLOTS = 2;

		// Token: 0x04001C8B RID: 7307
		public static readonly byte PAGES = 9;

		// Token: 0x04001C8C RID: 7308
		public static readonly byte BACKPACK = 3;

		// Token: 0x04001C8D RID: 7309
		public static readonly byte VEST = 4;

		// Token: 0x04001C8E RID: 7310
		public static readonly byte SHIRT = 5;

		// Token: 0x04001C8F RID: 7311
		public static readonly byte PANTS = 6;

		// Token: 0x04001C90 RID: 7312
		public static readonly byte STORAGE = 7;

		// Token: 0x04001C91 RID: 7313
		public static readonly byte AREA = 8;

		// Token: 0x04001C92 RID: 7314
		public static ushort[] loadout = PlayerInventory.LOADOUT;

		// Token: 0x04001C93 RID: 7315
		public static ushort[][] skillsets = PlayerInventory.SKILLSETS_SERVER;

		// Token: 0x04001C95 RID: 7317
		public bool isStoring;

		// Token: 0x04001C96 RID: 7318
		public bool isStorageTrunk;

		// Token: 0x04001C97 RID: 7319
		public InteractableStorage storage;

		// Token: 0x04001C98 RID: 7320
		public InventoryResized onInventoryResized;

		// Token: 0x04001C99 RID: 7321
		public InventoryUpdated onInventoryUpdated;

		// Token: 0x04001C9A RID: 7322
		public InventoryAdded onInventoryAdded;

		// Token: 0x04001C9B RID: 7323
		public InventoryRemoved onInventoryRemoved;

		// Token: 0x04001C9C RID: 7324
		public InventoryStored onInventoryStored;

		// Token: 0x04001C9D RID: 7325
		public InventoryStateUpdated onInventoryStateUpdated;
	}
}
