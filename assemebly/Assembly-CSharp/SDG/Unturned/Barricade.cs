using System;

namespace SDG.Unturned
{
	// Token: 0x02000503 RID: 1283
	public class Barricade
	{
		// Token: 0x0600230B RID: 8971 RVA: 0x000C3B10 File Offset: 0x000C1F10
		public Barricade(ushort newID)
		{
			this._id = newID;
			this.asset = (ItemBarricadeAsset)Assets.find(EAssetType.ITEM, this.id);
			if (this.asset == null)
			{
				this.health = 0;
				this.state = new byte[0];
				return;
			}
			this.health = this.asset.health;
			this.state = this.asset.getState();
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x000C3B82 File Offset: 0x000C1F82
		public Barricade(ushort newID, ushort newHealth, byte[] newState, ItemBarricadeAsset newAsset)
		{
			this._id = newID;
			this.health = newHealth;
			this.state = newState;
			this.asset = newAsset;
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x000C3BA7 File Offset: 0x000C1FA7
		public bool isDead
		{
			get
			{
				return this.health == 0;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x000C3BB2 File Offset: 0x000C1FB2
		public bool isRepaired
		{
			get
			{
				return this.health == this.asset.health;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x000C3BC7 File Offset: 0x000C1FC7
		public ushort id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x000C3BCF File Offset: 0x000C1FCF
		// (set) Token: 0x06002311 RID: 8977 RVA: 0x000C3BD7 File Offset: 0x000C1FD7
		public ItemBarricadeAsset asset { get; private set; }

		// Token: 0x06002312 RID: 8978 RVA: 0x000C3BE0 File Offset: 0x000C1FE0
		public void askDamage(ushort amount)
		{
			if (amount == 0 || this.isDead)
			{
				return;
			}
			if (amount >= this.health)
			{
				this.health = 0;
			}
			else
			{
				this.health -= amount;
			}
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000C3C1C File Offset: 0x000C201C
		public void askRepair(ushort amount)
		{
			if (amount == 0 || this.isDead)
			{
				return;
			}
			if (amount >= this.asset.health - this.health)
			{
				this.health = this.asset.health;
			}
			else
			{
				this.health += amount;
			}
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x000C3C78 File Offset: 0x000C2078
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.id,
				" ",
				this.health,
				" ",
				this.state.Length
			});
		}

		// Token: 0x04001512 RID: 5394
		private ushort _id;

		// Token: 0x04001513 RID: 5395
		public ushort health;

		// Token: 0x04001514 RID: 5396
		public byte[] state;
	}
}
