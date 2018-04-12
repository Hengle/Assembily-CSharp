using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000404 RID: 1028
	public class NPCItemCondition : INPCCondition
	{
		// Token: 0x06001BB1 RID: 7089 RVA: 0x00098269 File Offset: 0x00096669
		public NPCItemCondition(ushort newID, ushort newAmount, string newText, bool newShouldReset) : base(newText, newShouldReset)
		{
			this.id = newID;
			this.amount = newAmount;
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x00098282 File Offset: 0x00096682
		// (set) Token: 0x06001BB3 RID: 7091 RVA: 0x0009828A File Offset: 0x0009668A
		public ushort id { get; protected set; }

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x00098293 File Offset: 0x00096693
		// (set) Token: 0x06001BB5 RID: 7093 RVA: 0x0009829B File Offset: 0x0009669B
		public ushort amount { get; protected set; }

		// Token: 0x06001BB6 RID: 7094 RVA: 0x000982A4 File Offset: 0x000966A4
		public override bool isConditionMet(Player player)
		{
			NPCItemCondition.search.Clear();
			player.inventory.search(NPCItemCondition.search, this.id, false, true);
			ushort num = 0;
			byte b = 0;
			while ((int)b < NPCItemCondition.search.Count)
			{
				num += (ushort)NPCItemCondition.search[(int)b].jar.item.amount;
				b += 1;
			}
			return num >= this.amount;
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0009831C File Offset: 0x0009671C
		public override void applyCondition(Player player, bool shouldSend)
		{
			if (!Provider.isServer)
			{
				return;
			}
			if (!this.shouldReset)
			{
				return;
			}
			NPCItemCondition.search.Clear();
			player.inventory.search(NPCItemCondition.search, this.id, false, true);
			NPCItemCondition.search.Sort(NPCItemCondition.qualityAscendingComparator);
			ushort num = this.amount;
			byte b = 0;
			while ((int)b < NPCItemCondition.search.Count)
			{
				InventorySearch inventorySearch = NPCItemCondition.search[(int)b];
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
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x000984B0 File Offset: 0x000968B0
		public override string formatCondition(Player player)
		{
			if (string.IsNullOrEmpty(this.text))
			{
				this.text = PlayerNPCQuestUI.localization.format("Condition_Item");
			}
			ItemAsset itemAsset = Assets.find(EAssetType.ITEM, this.id) as ItemAsset;
			string arg;
			if (itemAsset != null)
			{
				arg = string.Concat(new string[]
				{
					"<color=",
					Palette.hex(ItemTool.getRarityColorUI(itemAsset.rarity)),
					">",
					itemAsset.itemName,
					"</color>"
				});
			}
			else
			{
				arg = "?";
			}
			NPCItemCondition.search.Clear();
			player.inventory.search(NPCItemCondition.search, this.id, false, true);
			return string.Format(this.text, NPCItemCondition.search.Count, this.amount, arg);
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x00098594 File Offset: 0x00096994
		public override Sleek createUI(Player player, Texture2D icon)
		{
			string text = this.formatCondition(player);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, this.id);
			if (itemAsset == null)
			{
				return null;
			}
			SleekBox sleekBox = new SleekBox();
			if (itemAsset.size_y == 1)
			{
				sleekBox.sizeOffset_Y = (int)(itemAsset.size_y * 50 + 10);
			}
			else
			{
				sleekBox.sizeOffset_Y = (int)(itemAsset.size_y * 25 + 10);
			}
			sleekBox.sizeScale_X = 1f;
			if (icon != null)
			{
				sleekBox.add(new SleekImageTexture(icon)
				{
					positionOffset_X = 5,
					positionOffset_Y = -10,
					positionScale_Y = 0.5f,
					sizeOffset_X = 20,
					sizeOffset_Y = 20
				});
			}
			SleekImageTexture sleekImageTexture = new SleekImageTexture();
			if (icon != null)
			{
				sleekImageTexture.positionOffset_X = 30;
			}
			else
			{
				sleekImageTexture.positionOffset_X = 5;
			}
			sleekImageTexture.positionOffset_Y = 5;
			if (itemAsset.size_y == 1)
			{
				sleekImageTexture.sizeOffset_X = (int)(itemAsset.size_x * 50);
				sleekImageTexture.sizeOffset_Y = (int)(itemAsset.size_y * 50);
			}
			else
			{
				sleekImageTexture.sizeOffset_X = (int)(itemAsset.size_x * 25);
				sleekImageTexture.sizeOffset_Y = (int)(itemAsset.size_y * 25);
			}
			sleekBox.add(sleekImageTexture);
			ItemTool.getIcon(this.id, 100, itemAsset.getState(false), itemAsset, sleekImageTexture.sizeOffset_X, sleekImageTexture.sizeOffset_Y, new ItemIconReady(sleekImageTexture.updateTexture));
			SleekLabel sleekLabel = new SleekLabel();
			if (icon != null)
			{
				sleekLabel.positionOffset_X = 35 + sleekImageTexture.sizeOffset_X;
				sleekLabel.sizeOffset_X = -40 - sleekImageTexture.sizeOffset_X;
			}
			else
			{
				sleekLabel.positionOffset_X = 10 + sleekImageTexture.sizeOffset_X;
				sleekLabel.sizeOffset_X = -15 - sleekImageTexture.sizeOffset_X;
			}
			sleekLabel.sizeScale_X = 1f;
			sleekLabel.sizeScale_Y = 1f;
			sleekLabel.fontAlignment = TextAnchor.MiddleLeft;
			sleekLabel.foregroundTint = ESleekTint.NONE;
			sleekLabel.isRich = true;
			sleekLabel.text = text;
			sleekBox.add(sleekLabel);
			return sleekBox;
		}

		// Token: 0x04001058 RID: 4184
		private static InventorySearchQualityAscendingComparator qualityAscendingComparator = new InventorySearchQualityAscendingComparator();

		// Token: 0x04001059 RID: 4185
		private static List<InventorySearch> search = new List<InventorySearch>();
	}
}
