using System;

namespace SDG.Unturned
{
	// Token: 0x0200041B RID: 1051
	public class QuestAsset : Asset
	{
		// Token: 0x06001C82 RID: 7298 RVA: 0x0009AEE8 File Offset: 0x000992E8
		public QuestAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (id < 2000 && !bundle.hasResource && !data.has("Bypass_ID_Limit"))
			{
				throw new NotSupportedException("ID < 2000");
			}
			this.questName = localization.format("Name");
			this.questName = ItemTool.filterRarityRichText(this.questName);
			this.questDescription = localization.format("Description");
			this.questDescription = ItemTool.filterRarityRichText(this.questDescription);
			this.conditions = new INPCCondition[(int)data.readByte("Conditions")];
			NPCTool.readConditions(data, localization, "Condition_", this.conditions, "quest " + id);
			this.rewards = new INPCReward[(int)data.readByte("Rewards")];
			NPCTool.readRewards(data, localization, "Reward_", this.rewards, "quest " + id);
			bundle.unload();
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x0009AFEE File Offset: 0x000993EE
		// (set) Token: 0x06001C84 RID: 7300 RVA: 0x0009AFF6 File Offset: 0x000993F6
		public string questName { get; protected set; }

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x0009AFFF File Offset: 0x000993FF
		// (set) Token: 0x06001C86 RID: 7302 RVA: 0x0009B007 File Offset: 0x00099407
		public string questDescription { get; protected set; }

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x0009B010 File Offset: 0x00099410
		// (set) Token: 0x06001C88 RID: 7304 RVA: 0x0009B018 File Offset: 0x00099418
		public INPCCondition[] conditions { get; protected set; }

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x0009B021 File Offset: 0x00099421
		// (set) Token: 0x06001C8A RID: 7306 RVA: 0x0009B029 File Offset: 0x00099429
		public INPCReward[] rewards { get; protected set; }

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x0009B032 File Offset: 0x00099432
		public override EAssetType assetCategory
		{
			get
			{
				return EAssetType.NPC;
			}
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x0009B038 File Offset: 0x00099438
		public bool areConditionsMet(Player player)
		{
			if (this.conditions != null)
			{
				for (int i = 0; i < this.conditions.Length; i++)
				{
					if (!this.conditions[i].isConditionMet(player))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0009B080 File Offset: 0x00099480
		public void applyConditions(Player player, bool shouldSend)
		{
			if (this.conditions != null)
			{
				for (int i = 0; i < this.conditions.Length; i++)
				{
					this.conditions[i].applyCondition(player, shouldSend);
				}
			}
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0009B0C0 File Offset: 0x000994C0
		public void grantRewards(Player player, bool shouldSend)
		{
			if (this.rewards != null)
			{
				for (int i = 0; i < this.rewards.Length; i++)
				{
					this.rewards[i].grantReward(player, shouldSend);
				}
			}
		}
	}
}
