using System;

namespace SDG.Unturned
{
	// Token: 0x0200050E RID: 1294
	public class ItemJar
	{
		// Token: 0x0600233C RID: 9020 RVA: 0x000C4290 File Offset: 0x000C2690
		public ItemJar(Item newItem)
		{
			this._item = newItem;
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, this.item.id);
			if (itemAsset == null)
			{
				return;
			}
			this.size_x = itemAsset.size_x;
			this.size_y = itemAsset.size_y;
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x000C42E0 File Offset: 0x000C26E0
		public ItemJar(byte new_x, byte new_y, byte newRot, Item newItem)
		{
			this.x = new_x;
			this.y = new_y;
			this.rot = newRot;
			this._item = newItem;
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, this.item.id);
			if (itemAsset == null)
			{
				return;
			}
			this.size_x = itemAsset.size_x;
			this.size_y = itemAsset.size_y;
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x000C4346 File Offset: 0x000C2746
		public Item item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x04001566 RID: 5478
		public byte x;

		// Token: 0x04001567 RID: 5479
		public byte y;

		// Token: 0x04001568 RID: 5480
		public byte rot;

		// Token: 0x04001569 RID: 5481
		public byte size_x;

		// Token: 0x0400156A RID: 5482
		public byte size_y;

		// Token: 0x0400156B RID: 5483
		private Item _item;

		// Token: 0x0400156C RID: 5484
		public InteractableItem interactableItem;
	}
}
