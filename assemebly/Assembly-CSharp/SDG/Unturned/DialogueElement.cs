using System;

namespace SDG.Unturned
{
	// Token: 0x02000397 RID: 919
	public class DialogueElement
	{
		// Token: 0x060019C3 RID: 6595 RVA: 0x000914D6 File Offset: 0x0008F8D6
		public DialogueElement(byte newIndex, INPCCondition[] newConditions, INPCReward[] newRewards)
		{
			this.index = newIndex;
			this.conditions = newConditions;
			this.rewards = newRewards;
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x000914F3 File Offset: 0x0008F8F3
		// (set) Token: 0x060019C5 RID: 6597 RVA: 0x000914FB File Offset: 0x0008F8FB
		public byte index { get; protected set; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x00091504 File Offset: 0x0008F904
		// (set) Token: 0x060019C7 RID: 6599 RVA: 0x0009150C File Offset: 0x0008F90C
		public INPCCondition[] conditions { get; protected set; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x00091515 File Offset: 0x0008F915
		// (set) Token: 0x060019C9 RID: 6601 RVA: 0x0009151D File Offset: 0x0008F91D
		public INPCReward[] rewards { get; protected set; }

		// Token: 0x060019CA RID: 6602 RVA: 0x00091528 File Offset: 0x0008F928
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

		// Token: 0x060019CB RID: 6603 RVA: 0x00091570 File Offset: 0x0008F970
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

		// Token: 0x060019CC RID: 6604 RVA: 0x000915B0 File Offset: 0x0008F9B0
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
