using System;

namespace SDG.Unturned
{
	// Token: 0x02000410 RID: 1040
	public class NPCReputationCondition : NPCLogicCondition
	{
		// Token: 0x06001C0B RID: 7179 RVA: 0x00099209 File Offset: 0x00097609
		public NPCReputationCondition(int newReputation, ENPCLogicType newLogicType, string newText) : base(newLogicType, newText, false)
		{
			this.reputation = newReputation;
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x0009921B File Offset: 0x0009761B
		// (set) Token: 0x06001C0D RID: 7181 RVA: 0x00099223 File Offset: 0x00097623
		public int reputation { get; protected set; }

		// Token: 0x06001C0E RID: 7182 RVA: 0x0009922C File Offset: 0x0009762C
		public override bool isConditionMet(Player player)
		{
			return base.doesLogicPass<int>(player.skills.reputation, this.reputation);
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00099248 File Offset: 0x00097648
		public override string formatCondition(Player player)
		{
			if (string.IsNullOrEmpty(this.text))
			{
				this.text = PlayerNPCQuestUI.localization.read("Condition_Reputation");
			}
			string text = player.skills.reputation.ToString();
			if (player.skills.reputation > 0)
			{
				text = "+" + text;
			}
			string text2 = this.reputation.ToString();
			if (this.reputation > 0)
			{
				text2 = "+" + text2;
			}
			return string.Format(this.text, text, text2);
		}
	}
}
