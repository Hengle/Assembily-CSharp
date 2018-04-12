using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x0200042F RID: 1071
	public class VendorSelling : VendorElement
	{
		// Token: 0x06001D4A RID: 7498 RVA: 0x0009D978 File Offset: 0x0009BD78
		public VendorSelling(byte newIndex, ushort newID, uint newCost, INPCCondition[] newConditions) : base(newIndex, newID, newCost, newConditions)
		{
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0009D985 File Offset: 0x0009BD85
		public bool canBuy(Player player)
		{
			return player.skills.experience >= base.cost;
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x0009D99D File Offset: 0x0009BD9D
		public void buy(Player player)
		{
			player.inventory.forceAddItem(new Item(base.id, EItemOrigin.ADMIN), false, false);
			player.skills.askSpend(base.cost);
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x0009D9CC File Offset: 0x0009BDCC
		public void format(Player player, out ushort total)
		{
			VendorSelling.search.Clear();
			player.inventory.search(VendorSelling.search, base.id, false, true);
			total = 0;
			byte b = 0;
			while ((int)b < VendorSelling.search.Count)
			{
				total += (ushort)VendorSelling.search[(int)b].jar.item.amount;
				b += 1;
			}
		}

		// Token: 0x0400115C RID: 4444
		private static List<InventorySearch> search = new List<InventorySearch>();
	}
}
