using System;

namespace SDG.Unturned
{
	// Token: 0x0200040D RID: 1037
	public class NPCQuestReward : INPCReward
	{
		// Token: 0x06001BF8 RID: 7160 RVA: 0x00098F53 File Offset: 0x00097353
		public NPCQuestReward(ushort newID, string newText) : base(newText)
		{
			this.id = newID;
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00098F63 File Offset: 0x00097363
		// (set) Token: 0x06001BFA RID: 7162 RVA: 0x00098F6B File Offset: 0x0009736B
		public ushort id { get; protected set; }

		// Token: 0x06001BFB RID: 7163 RVA: 0x00098F74 File Offset: 0x00097374
		public override void grantReward(Player player, bool shouldSend)
		{
			if (shouldSend)
			{
				player.quests.sendAddQuest(this.id);
			}
			else
			{
				player.quests.addQuest(this.id);
			}
		}
	}
}
