using System;

namespace SDG.Unturned
{
	// Token: 0x02000517 RID: 1303
	public class Structure
	{
		// Token: 0x06002375 RID: 9077 RVA: 0x000C529D File Offset: 0x000C369D
		public Structure(ushort newID)
		{
			this._id = newID;
			this.asset = (ItemStructureAsset)Assets.find(EAssetType.ITEM, this.id);
			this.health = this.asset.health;
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000C52D4 File Offset: 0x000C36D4
		public Structure(ushort newID, ushort newHealth, ItemStructureAsset newAsset)
		{
			this._id = newID;
			this.health = newHealth;
			this.asset = newAsset;
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06002377 RID: 9079 RVA: 0x000C52F1 File Offset: 0x000C36F1
		public bool isDead
		{
			get
			{
				return this.health == 0;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06002378 RID: 9080 RVA: 0x000C52FC File Offset: 0x000C36FC
		public bool isRepaired
		{
			get
			{
				return this.health == this.asset.health;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06002379 RID: 9081 RVA: 0x000C5311 File Offset: 0x000C3711
		public ushort id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x0600237A RID: 9082 RVA: 0x000C5319 File Offset: 0x000C3719
		// (set) Token: 0x0600237B RID: 9083 RVA: 0x000C5321 File Offset: 0x000C3721
		public ItemStructureAsset asset { get; private set; }

		// Token: 0x0600237C RID: 9084 RVA: 0x000C532A File Offset: 0x000C372A
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

		// Token: 0x0600237D RID: 9085 RVA: 0x000C5368 File Offset: 0x000C3768
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

		// Token: 0x0600237E RID: 9086 RVA: 0x000C53C4 File Offset: 0x000C37C4
		public override string ToString()
		{
			return this.id + " " + this.health;
		}

		// Token: 0x0400157B RID: 5499
		private ushort _id;

		// Token: 0x0400157C RID: 5500
		public ushort health;
	}
}
