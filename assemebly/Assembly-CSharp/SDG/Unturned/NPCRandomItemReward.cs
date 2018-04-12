using System;

namespace SDG.Unturned
{
	// Token: 0x0200040E RID: 1038
	public class NPCRandomItemReward : INPCReward
	{
		// Token: 0x06001BFC RID: 7164 RVA: 0x00098FA3 File Offset: 0x000973A3
		public NPCRandomItemReward(ushort newID, byte newAmount, string newText) : base(newText)
		{
			this.id = newID;
			this.amount = newAmount;
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x00098FBA File Offset: 0x000973BA
		// (set) Token: 0x06001BFE RID: 7166 RVA: 0x00098FC2 File Offset: 0x000973C2
		public ushort id { get; protected set; }

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x00098FCB File Offset: 0x000973CB
		// (set) Token: 0x06001C00 RID: 7168 RVA: 0x00098FD3 File Offset: 0x000973D3
		public byte amount { get; protected set; }

		// Token: 0x06001C01 RID: 7169 RVA: 0x00098FDC File Offset: 0x000973DC
		public override void grantReward(Player player, bool shouldSend)
		{
			if (!Provider.isServer)
			{
				return;
			}
			for (byte b = 0; b < this.amount; b += 1)
			{
				ushort num = SpawnTableTool.resolve(this.id);
				if (num != 0)
				{
					player.inventory.forceAddItem(new Item(num, EItemOrigin.CRAFT), false, false);
				}
			}
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x00099032 File Offset: 0x00097432
		public override string formatReward(Player player)
		{
			if (string.IsNullOrEmpty(this.text))
			{
				this.text = PlayerNPCQuestUI.localization.read("Reward_Item_Random");
			}
			return string.Format(this.text, this.amount);
		}
	}
}
