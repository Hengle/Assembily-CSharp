using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x02000515 RID: 1301
	public class Items
	{
		// Token: 0x06002357 RID: 9047 RVA: 0x000C434E File Offset: 0x000C274E
		public Items(byte newPage)
		{
			this._page = newPage;
			this.items = new List<ItemJar>();
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002358 RID: 9048 RVA: 0x000C4368 File Offset: 0x000C2768
		public byte page
		{
			get
			{
				return this._page;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002359 RID: 9049 RVA: 0x000C4370 File Offset: 0x000C2770
		public byte width
		{
			get
			{
				return this._width;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x000C4378 File Offset: 0x000C2778
		public byte height
		{
			get
			{
				return this._height;
			}
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000C4380 File Offset: 0x000C2780
		public void updateAmount(byte index, byte newAmount)
		{
			if (index < 0 || (int)index >= this.items.Count)
			{
				return;
			}
			this.items[(int)index].item.amount = newAmount;
			if (this.onItemUpdated != null)
			{
				this.onItemUpdated(this.page, index, this.items[(int)index]);
			}
			if (this.onStateUpdated != null)
			{
				this.onStateUpdated();
			}
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000C43FC File Offset: 0x000C27FC
		public void updateQuality(byte index, byte newQuality)
		{
			if (index < 0 || (int)index >= this.items.Count)
			{
				return;
			}
			this.items[(int)index].item.quality = newQuality;
			if (this.onItemUpdated != null)
			{
				this.onItemUpdated(this.page, index, this.items[(int)index]);
			}
			if (this.onStateUpdated != null)
			{
				this.onStateUpdated();
			}
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000C4478 File Offset: 0x000C2878
		public void updateState(byte index, byte[] newState)
		{
			if (index < 0 || (int)index >= this.items.Count)
			{
				return;
			}
			this.items[(int)index].item.state = newState;
			if (this.onItemUpdated != null)
			{
				this.onItemUpdated(this.page, index, this.items[(int)index]);
			}
			if (this.onStateUpdated != null)
			{
				this.onStateUpdated();
			}
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000C44F4 File Offset: 0x000C28F4
		public byte getItemCount()
		{
			return (byte)this.items.Count;
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x000C4502 File Offset: 0x000C2902
		public ItemJar getItem(byte index)
		{
			if (index < 0 || (int)index >= this.items.Count)
			{
				return null;
			}
			return this.items[(int)index];
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000C452C File Offset: 0x000C292C
		public byte getIndex(byte x, byte y)
		{
			if (this.page < PlayerInventory.SLOTS)
			{
				return 0;
			}
			if (x < 0 || y < 0 || x >= this.width || y >= this.height)
			{
				return byte.MaxValue;
			}
			byte b = 0;
			while ((int)b < this.items.Count)
			{
				if (this.items[(int)b].x == x && this.items[(int)b].y == y)
				{
					return b;
				}
				b += 1;
			}
			return byte.MaxValue;
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000C45CC File Offset: 0x000C29CC
		public byte findIndex(byte x, byte y, out byte find_x, out byte find_y)
		{
			find_x = byte.MaxValue;
			find_y = byte.MaxValue;
			if (x < 0 || y < 0 || x >= this.width || y >= this.height)
			{
				return byte.MaxValue;
			}
			byte b = 0;
			while ((int)b < this.items.Count)
			{
				if (this.items[(int)b].x <= x && this.items[(int)b].y <= y)
				{
					byte b2 = this.items[(int)b].size_x;
					byte b3 = this.items[(int)b].size_y;
					if (this.items[(int)b].rot % 2 == 1)
					{
						b2 = this.items[(int)b].size_y;
						b3 = this.items[(int)b].size_x;
					}
					if (this.items[(int)b].x + b2 > x && this.items[(int)b].y + b3 > y)
					{
						find_x = this.items[(int)b].x;
						find_y = this.items[(int)b].y;
						return b;
					}
				}
				b += 1;
			}
			return byte.MaxValue;
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000C4720 File Offset: 0x000C2B20
		public List<InventorySearch> search(List<InventorySearch> search, EItemType type)
		{
			byte b = 0;
			while ((int)b < this.items.Count)
			{
				ItemJar itemJar = this.items[(int)b];
				if (itemJar.item.amount > 0)
				{
					ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, itemJar.item.id);
					if (itemAsset != null && itemAsset.type == type)
					{
						search.Add(new InventorySearch(this.page, itemJar));
					}
				}
				b += 1;
			}
			return search;
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000C47A4 File Offset: 0x000C2BA4
		public List<InventorySearch> search(List<InventorySearch> search, EItemType type, ushort caliber)
		{
			byte b = 0;
			while ((int)b < this.items.Count)
			{
				ItemJar itemJar = this.items[(int)b];
				if (itemJar.item.amount > 0)
				{
					bool flag = false;
					for (int i = 0; i < search.Count; i++)
					{
						if (search[i].page == this.page && search[i].jar.x == itemJar.x && search[i].jar.y == itemJar.y)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, itemJar.item.id);
						if (itemAsset != null && itemAsset.type == type)
						{
							if (((ItemCaliberAsset)itemAsset).calibers.Length == 0)
							{
								search.Add(new InventorySearch(this.page, itemJar));
							}
							else
							{
								byte b2 = 0;
								while ((int)b2 < ((ItemCaliberAsset)itemAsset).calibers.Length)
								{
									if (((ItemCaliberAsset)itemAsset).calibers[(int)b2] == caliber)
									{
										search.Add(new InventorySearch(this.page, itemJar));
										break;
									}
									b2 += 1;
								}
							}
						}
					}
				}
				b += 1;
			}
			return search;
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000C4908 File Offset: 0x000C2D08
		public List<InventorySearch> search(List<InventorySearch> search, ushort id, bool findEmpty, bool findHealthy)
		{
			byte b = 0;
			while ((int)b < this.items.Count)
			{
				ItemJar itemJar = this.items[(int)b];
				if ((findEmpty || itemJar.item.amount > 0) && (findHealthy || itemJar.item.quality < 100) && itemJar.item.id == id)
				{
					search.Add(new InventorySearch(this.page, itemJar));
				}
				b += 1;
			}
			return search;
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000C4994 File Offset: 0x000C2D94
		public InventorySearch has(ushort id)
		{
			byte b = 0;
			while ((int)b < this.items.Count)
			{
				ItemJar itemJar = this.items[(int)b];
				if (itemJar.item.amount > 0 && itemJar.item.id == id)
				{
					return new InventorySearch(this.page, itemJar);
				}
				b += 1;
			}
			return null;
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000C49FC File Offset: 0x000C2DFC
		public void loadItem(byte x, byte y, byte rot, Item item)
		{
			ItemJar itemJar = new ItemJar(x, y, rot, item);
			this.fillSlot(itemJar, true);
			this.items.Add(itemJar);
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000C4A28 File Offset: 0x000C2E28
		public void addItem(byte x, byte y, byte rot, Item item)
		{
			ItemJar itemJar = new ItemJar(x, y, rot, item);
			this.fillSlot(itemJar, true);
			this.items.Add(itemJar);
			if (this.onItemAdded != null)
			{
				this.onItemAdded(this.page, (byte)(this.items.Count - 1), itemJar);
			}
			if (this.onStateUpdated != null)
			{
				this.onStateUpdated();
			}
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000C4A95 File Offset: 0x000C2E95
		public bool tryAddItem(Item item)
		{
			return this.tryAddItem(item, true);
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000C4AA0 File Offset: 0x000C2EA0
		public bool tryAddItem(Item item, bool isStateUpdatable)
		{
			if (this.getItemCount() >= 200)
			{
				return false;
			}
			ItemJar itemJar = new ItemJar(item);
			byte x;
			byte y;
			byte rot;
			if (!this.tryFindSpace(itemJar.size_x, itemJar.size_y, out x, out y, out rot))
			{
				return false;
			}
			itemJar.x = x;
			itemJar.y = y;
			itemJar.rot = rot;
			this.fillSlot(itemJar, true);
			this.items.Add(itemJar);
			if (this.onItemAdded != null)
			{
				this.onItemAdded(this.page, (byte)(this.items.Count - 1), itemJar);
			}
			if (isStateUpdatable && this.onStateUpdated != null)
			{
				this.onStateUpdated();
			}
			return true;
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x000C4B58 File Offset: 0x000C2F58
		public void removeItem(byte index)
		{
			if (index < 0 || (int)index >= this.items.Count)
			{
				return;
			}
			this.fillSlot(this.items[(int)index], false);
			if (this.onItemRemoved != null)
			{
				this.onItemRemoved(this.page, index, this.items[(int)index]);
			}
			this.items.RemoveAt((int)index);
			if (this.onStateUpdated != null)
			{
				this.onStateUpdated();
			}
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000C4BDC File Offset: 0x000C2FDC
		public void clear()
		{
			this.items.Clear();
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x000C4BEC File Offset: 0x000C2FEC
		public void loadSize(byte newWidth, byte newHeight)
		{
			this._width = newWidth;
			this._height = newHeight;
			this.slots = new bool[(int)this.width, (int)this.height];
			for (byte b = 0; b < this.width; b += 1)
			{
				for (byte b2 = 0; b2 < this.height; b2 += 1)
				{
					this.slots[(int)b, (int)b2] = false;
				}
			}
			List<ItemJar> list = new List<ItemJar>();
			if (this.items != null)
			{
				byte b3 = 0;
				while ((int)b3 < this.items.Count)
				{
					ItemJar itemJar = this.items[(int)b3];
					byte b4 = itemJar.size_x;
					byte b5 = itemJar.size_y;
					if (itemJar.rot % 2 == 1)
					{
						b4 = itemJar.size_y;
						b5 = itemJar.size_x;
					}
					if (this.width == 0 || this.height == 0 || (this.page >= PlayerInventory.SLOTS && (itemJar.x + b4 > this.width || itemJar.y + b5 > this.height)))
					{
						if (this.onItemDiscarded != null)
						{
							this.onItemDiscarded(this.page, b3, itemJar);
						}
						if (this.onStateUpdated != null)
						{
							this.onStateUpdated();
						}
					}
					else
					{
						this.fillSlot(itemJar, true);
						list.Add(itemJar);
					}
					b3 += 1;
				}
			}
			this.items = list;
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x000C4D6D File Offset: 0x000C316D
		public void resize(byte newWidth, byte newHeight)
		{
			this.loadSize(newWidth, newHeight);
			if (this.onItemsResized != null)
			{
				this.onItemsResized(this.page, newWidth, newHeight);
			}
			if (this.onStateUpdated != null)
			{
				this.onStateUpdated();
			}
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x000C4DAC File Offset: 0x000C31AC
		public bool checkSpaceEmpty(byte pos_x, byte pos_y, byte size_x, byte size_y, byte rot)
		{
			if (this.page < PlayerInventory.SLOTS)
			{
				return this.items.Count == 0;
			}
			if (rot % 2 == 1)
			{
				byte b = size_x;
				size_x = size_y;
				size_y = b;
			}
			for (byte b2 = pos_x; b2 < pos_x + size_x; b2 += 1)
			{
				for (byte b3 = pos_y; b3 < pos_y + size_y; b3 += 1)
				{
					if (b2 >= this.width || b3 >= this.height)
					{
						return false;
					}
					if (this.slots[(int)b2, (int)b3])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000C4E48 File Offset: 0x000C3248
		public bool checkSpaceDrag(byte old_x, byte old_y, byte oldRot, byte new_x, byte new_y, byte newRot, byte size_x, byte size_y, bool checkSame)
		{
			if (this.page < PlayerInventory.SLOTS)
			{
				return this.items.Count == 0 || checkSame;
			}
			byte b = size_x;
			byte b2 = size_y;
			if (oldRot % 2 == 1)
			{
				b = size_y;
				b2 = size_x;
			}
			byte b3 = size_x;
			byte b4 = size_y;
			if (newRot % 2 == 1)
			{
				b3 = size_y;
				b4 = size_x;
			}
			for (byte b5 = new_x; b5 < new_x + b3; b5 += 1)
			{
				for (byte b6 = new_y; b6 < new_y + b4; b6 += 1)
				{
					if (b5 >= this.width || b6 >= this.height)
					{
						return false;
					}
					if (this.slots[(int)b5, (int)b6])
					{
						int num = (int)(b5 - old_x);
						int num2 = (int)(b6 - old_y);
						if (!checkSame || num < 0 || num2 < 0 || num >= (int)b || num2 >= (int)b2)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000C4F44 File Offset: 0x000C3344
		public bool checkSpaceSwap(byte x, byte y, byte oldSize_X, byte oldSize_Y, byte oldRot, byte newSize_X, byte newSize_Y, byte newRot)
		{
			if (this.page < PlayerInventory.SLOTS)
			{
				return true;
			}
			if (oldRot % 2 == 1)
			{
				byte b = oldSize_X;
				oldSize_X = oldSize_Y;
				oldSize_Y = b;
			}
			if (newRot % 2 == 1)
			{
				byte b2 = newSize_X;
				newSize_X = newSize_Y;
				newSize_Y = b2;
			}
			for (byte b3 = x; b3 < x + newSize_X; b3 += 1)
			{
				for (byte b4 = y; b4 < y + newSize_Y; b4 += 1)
				{
					if (b3 >= this.width || b4 >= this.height)
					{
						return false;
					}
					if (this.slots[(int)b3, (int)b4])
					{
						int num = (int)(b3 - x);
						int num2 = (int)(b4 - y);
						if (num < 0 || num2 < 0 || num >= (int)oldSize_X || num2 >= (int)oldSize_Y)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x000C5010 File Offset: 0x000C3410
		public bool tryFindSpace(byte size_x, byte size_y, out byte x, out byte y, out byte rot)
		{
			x = byte.MaxValue;
			y = byte.MaxValue;
			rot = 0;
			if (this.page < PlayerInventory.SLOTS)
			{
				x = 0;
				y = 0;
				rot = 0;
				return this.items.Count == 0;
			}
			for (byte b = 0; b < this.height - size_y + 1; b += 1)
			{
				for (byte b2 = 0; b2 < this.width - size_x + 1; b2 += 1)
				{
					bool flag = false;
					for (byte b3 = 0; b3 < size_y; b3 += 1)
					{
						if (flag)
						{
							break;
						}
						for (byte b4 = 0; b4 < size_x; b4 += 1)
						{
							if (this.slots[(int)(b2 + b4), (int)(b + b3)])
							{
								flag = true;
								break;
							}
							if (b4 == size_x - 1 && b3 == size_y - 1)
							{
								x = b2;
								y = b;
								rot = 0;
								return true;
							}
						}
					}
				}
			}
			for (byte b5 = 0; b5 < this.height - size_x + 1; b5 += 1)
			{
				for (byte b6 = 0; b6 < this.width - size_y + 1; b6 += 1)
				{
					bool flag2 = false;
					for (byte b7 = 0; b7 < size_x; b7 += 1)
					{
						if (flag2)
						{
							break;
						}
						for (byte b8 = 0; b8 < size_y; b8 += 1)
						{
							if (this.slots[(int)(b6 + b8), (int)(b5 + b7)])
							{
								flag2 = true;
								break;
							}
							if (b8 == size_y - 1 && b7 == size_x - 1)
							{
								x = b6;
								y = b5;
								rot = 1;
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000C51CC File Offset: 0x000C35CC
		private void fillSlot(ItemJar jar, bool isOccupied)
		{
			byte b = jar.size_x;
			byte b2 = jar.size_y;
			if (jar.rot % 2 == 1)
			{
				b = jar.size_y;
				b2 = jar.size_x;
			}
			for (byte b3 = 0; b3 < b; b3 += 1)
			{
				for (byte b4 = 0; b4 < b2; b4 += 1)
				{
					if (jar.x + b3 < this.width && jar.y + b4 < this.height)
					{
						this.slots[(int)(jar.x + b3), (int)(jar.y + b4)] = isOccupied;
					}
				}
			}
		}

		// Token: 0x0400156D RID: 5485
		public ItemsResized onItemsResized;

		// Token: 0x0400156E RID: 5486
		public ItemUpdated onItemUpdated;

		// Token: 0x0400156F RID: 5487
		public ItemAdded onItemAdded;

		// Token: 0x04001570 RID: 5488
		public ItemRemoved onItemRemoved;

		// Token: 0x04001571 RID: 5489
		public ItemDiscarded onItemDiscarded;

		// Token: 0x04001572 RID: 5490
		public StateUpdated onStateUpdated;

		// Token: 0x04001573 RID: 5491
		private byte _page;

		// Token: 0x04001574 RID: 5492
		private byte _width;

		// Token: 0x04001575 RID: 5493
		private byte _height;

		// Token: 0x04001576 RID: 5494
		private bool[,] slots;

		// Token: 0x04001577 RID: 5495
		private List<ItemJar> items;
	}
}
