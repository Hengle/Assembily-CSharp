using System;

namespace SDG.Unturned
{
	// Token: 0x020003FD RID: 1021
	public class NPCCompareFlagsCondition : NPCLogicCondition
	{
		// Token: 0x06001B81 RID: 7041 RVA: 0x00097D5C File Offset: 0x0009615C
		public NPCCompareFlagsCondition(ushort newFlag_A_ID, ushort newFlag_B_ID, bool newAllowFlag_A_Unset, bool newAllowFlag_B_Unset, ENPCLogicType newLogicType, string newText, bool newShouldReset) : base(newLogicType, newText, newShouldReset)
		{
			this.flag_A_ID = newFlag_A_ID;
			this.allowFlag_A_Unset = newAllowFlag_A_Unset;
			this.flag_B_ID = newFlag_B_ID;
			this.allowFlag_B_Unset = newAllowFlag_B_Unset;
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x00097D87 File Offset: 0x00096187
		// (set) Token: 0x06001B83 RID: 7043 RVA: 0x00097D8F File Offset: 0x0009618F
		public ushort flag_A_ID { get; protected set; }

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x00097D98 File Offset: 0x00096198
		// (set) Token: 0x06001B85 RID: 7045 RVA: 0x00097DA0 File Offset: 0x000961A0
		public bool allowFlag_A_Unset { get; protected set; }

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x00097DA9 File Offset: 0x000961A9
		// (set) Token: 0x06001B87 RID: 7047 RVA: 0x00097DB1 File Offset: 0x000961B1
		public bool allowFlag_B_Unset { get; protected set; }

		// Token: 0x06001B88 RID: 7048 RVA: 0x00097DBC File Offset: 0x000961BC
		public override bool isConditionMet(Player player)
		{
			short a;
			short b;
			return (player.quests.getFlag(this.flag_A_ID, out a) || this.allowFlag_A_Unset) && (player.quests.getFlag(this.flag_B_ID, out b) || this.allowFlag_B_Unset) && base.doesLogicPass<short>(a, b);
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00097E1C File Offset: 0x0009621C
		public override void applyCondition(Player player, bool shouldSend)
		{
			if (!this.shouldReset)
			{
				return;
			}
			if (shouldSend)
			{
				player.quests.sendRemoveFlag(this.flag_A_ID);
				player.quests.sendRemoveFlag(this.flag_B_ID);
			}
			else
			{
				player.quests.removeFlag(this.flag_A_ID);
				player.quests.removeFlag(this.flag_B_ID);
			}
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x00097E84 File Offset: 0x00096284
		public override string formatCondition(Player player)
		{
			if (string.IsNullOrEmpty(this.text))
			{
				return null;
			}
			return this.text;
		}

		// Token: 0x0400104B RID: 4171
		public ushort flag_B_ID;
	}
}
