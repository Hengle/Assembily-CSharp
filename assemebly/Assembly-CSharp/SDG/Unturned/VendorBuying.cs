using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x0200042C RID: 1068
	public class VendorBuying : VendorElement
	{
		// Token: 0x06001D38 RID: 7480 RVA: 0x0009D644 File Offset: 0x0009BA44
		public VendorBuying(byte newIndex, ushort newID, uint newCost, INPCCondition[] newConditions) : base(newIndex, newID, newCost, newConditions)
		{
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0009D654 File Offset: 0x0009BA54
		public bool canSell(Player player)
		{
			ItemAsset itemAsset = Assets.find(EAssetType.ITEM, base.id) as ItemAsset;
			VendorBuying.search.Clear();
			player.inventory.search(VendorBuying.search, base.id, false, true);
			ushort num = 0;
			byte b = 0;
			while ((int)b < VendorBuying.search.Count)
			{
				num += (ushort)VendorBuying.search[(int)b].jar.item.amount;
				b += 1;
			}
			return num >= (ushort)itemAsset.amount;
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x0009D6E0 File Offset: 0x0009BAE0
		public void sell(Player player)
		{
			ItemAsset itemAsset = Assets.find(EAssetType.ITEM, base.id) as ItemAsset;
			VendorBuying.search.Clear();
			player.inventory.search(VendorBuying.search, base.id, false, true);
			VendorBuying.search.Sort(VendorBuying.qualityAscendingComparator);
			ushort num = (ushort)itemAsset.amount;
			byte b = 0;
			while ((int)b < VendorBuying.search.Count)
			{
				InventorySearch inventorySearch = VendorBuying.search[(int)b];
				if (player.equipment.checkSelection(inventorySearch.page, inventorySearch.jar.x, inventorySearch.jar.y))
				{
					player.equipment.dequip();
				}
				if ((ushort)inventorySearch.jar.item.amount > num)
				{
					player.inventory.sendUpdateAmount(inventorySearch.page, inventorySearch.jar.x, inventorySearch.jar.y, (byte)((ushort)inventorySearch.jar.item.amount - num));
					break;
				}
				num -= (ushort)inventorySearch.jar.item.amount;
				player.inventory.sendUpdateAmount(inventorySearch.page, inventorySearch.jar.x, inventorySearch.jar.y, 0);
				player.crafting.removeItem(inventorySearch.page, inventorySearch.jar);
				if (inventorySearch.page < PlayerInventory.SLOTS)
				{
					player.equipment.sendSlot(inventorySearch.page);
				}
				if (num == 0)
				{
					break;
				}
				b += 1;
			}
			player.skills.askAward(base.cost);
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x0009D880 File Offset: 0x0009BC80
		public void format(Player player, out ushort total, out byte amount)
		{
			ItemAsset itemAsset = Assets.find(EAssetType.ITEM, base.id) as ItemAsset;
			VendorBuying.search.Clear();
			player.inventory.search(VendorBuying.search, base.id, false, true);
			total = 0;
			byte b = 0;
			while ((int)b < VendorBuying.search.Count)
			{
				total += (ushort)VendorBuying.search[(int)b].jar.item.amount;
				b += 1;
			}
			amount = itemAsset.amount;
		}

		// Token: 0x04001156 RID: 4438
		private static InventorySearchQualityAscendingComparator qualityAscendingComparator = new InventorySearchQualityAscendingComparator();

		// Token: 0x04001157 RID: 4439
		private static List<InventorySearch> search = new List<InventorySearch>();
	}
}
