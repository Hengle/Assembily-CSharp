using System;

namespace SDG.Unturned
{
	// Token: 0x0200050D RID: 1293
	public class Item
	{
		// Token: 0x06002330 RID: 9008 RVA: 0x000C401A File Offset: 0x000C241A
		public Item(ushort newID, bool full) : this(newID, (!full) ? EItemOrigin.WORLD : EItemOrigin.ADMIN)
		{
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000C4030 File Offset: 0x000C2430
		public Item(ushort newID, EItemOrigin origin)
		{
			this._id = newID;
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, this.id);
			if (itemAsset == null)
			{
				this.state = new byte[0];
				return;
			}
			if (origin == EItemOrigin.WORLD && !Provider.modeConfigData.Items.Has_Durability)
			{
				origin = EItemOrigin.CRAFT;
			}
			if (origin != EItemOrigin.WORLD)
			{
				this.amount = itemAsset.amount;
				this.quality = 100;
			}
			else
			{
				this.amount = itemAsset.count;
				this.quality = itemAsset.quality;
			}
			this.state = itemAsset.getState(origin);
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x000C40D0 File Offset: 0x000C24D0
		public Item(ushort newID, bool full, byte newQuality) : this(newID, (!full) ? EItemOrigin.WORLD : EItemOrigin.ADMIN, newQuality)
		{
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x000C40E8 File Offset: 0x000C24E8
		public Item(ushort newID, EItemOrigin origin, byte newQuality)
		{
			this._id = newID;
			this.quality = newQuality;
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, this.id);
			if (itemAsset == null)
			{
				this.state = new byte[0];
				return;
			}
			if (origin == EItemOrigin.WORLD && !Provider.modeConfigData.Items.Has_Durability)
			{
				origin = EItemOrigin.CRAFT;
			}
			if (origin != EItemOrigin.WORLD)
			{
				this.amount = itemAsset.amount;
			}
			else
			{
				this.amount = itemAsset.count;
			}
			this.state = itemAsset.getState(origin);
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000C417C File Offset: 0x000C257C
		public Item(ushort newID, byte newAmount, byte newQuality)
		{
			this._id = newID;
			this.amount = newAmount;
			this.quality = newQuality;
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, this.id);
			if (itemAsset == null)
			{
				this.state = new byte[0];
				return;
			}
			this.state = itemAsset.getState();
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x000C41D5 File Offset: 0x000C25D5
		public Item(ushort newID, byte newAmount, byte newQuality, byte[] newState)
		{
			this._id = newID;
			this.amount = newAmount;
			this.quality = newQuality;
			this.state = newState;
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002336 RID: 9014 RVA: 0x000C41FA File Offset: 0x000C25FA
		public ushort id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x000C4202 File Offset: 0x000C2602
		// (set) Token: 0x06002338 RID: 9016 RVA: 0x000C420A File Offset: 0x000C260A
		public byte durability
		{
			get
			{
				return this.quality;
			}
			set
			{
				this.quality = value;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x000C4213 File Offset: 0x000C2613
		// (set) Token: 0x0600233A RID: 9018 RVA: 0x000C421B File Offset: 0x000C261B
		public byte[] metadata
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000C4224 File Offset: 0x000C2624
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.id,
				" ",
				this.amount,
				" ",
				this.quality,
				" ",
				this.state.Length
			});
		}

		// Token: 0x04001562 RID: 5474
		private ushort _id;

		// Token: 0x04001563 RID: 5475
		public byte amount;

		// Token: 0x04001564 RID: 5476
		public byte quality;

		// Token: 0x04001565 RID: 5477
		public byte[] state;
	}
}
