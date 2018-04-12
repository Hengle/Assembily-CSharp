using System;

namespace SDG.Unturned
{
	// Token: 0x020003FF RID: 1023
	public class NPCExperienceCondition : NPCLogicCondition
	{
		// Token: 0x06001B8F RID: 7055 RVA: 0x00097ECC File Offset: 0x000962CC
		public NPCExperienceCondition(uint newExperience, ENPCLogicType newLogicType, string newText, bool newShouldReset) : base(newLogicType, newText, newShouldReset)
		{
			this.experience = newExperience;
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x00097EDF File Offset: 0x000962DF
		// (set) Token: 0x06001B91 RID: 7057 RVA: 0x00097EE7 File Offset: 0x000962E7
		public uint experience { get; protected set; }

		// Token: 0x06001B92 RID: 7058 RVA: 0x00097EF0 File Offset: 0x000962F0
		public override bool isConditionMet(Player player)
		{
			return base.doesLogicPass<uint>(player.skills.experience, this.experience);
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x00097F09 File Offset: 0x00096309
		public override void applyCondition(Player player, bool shouldSend)
		{
			if (!this.shouldReset)
			{
				return;
			}
			if (shouldSend)
			{
				player.skills.askSpend(this.experience);
			}
			else
			{
				player.skills.modXp2(this.experience);
			}
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x00097F44 File Offset: 0x00096344
		public override string formatCondition(Player player)
		{
			if (string.IsNullOrEmpty(this.text))
			{
				this.text = PlayerNPCQuestUI.localization.read("Condition_Experience");
			}
			return string.Format(this.text, player.skills.experience, this.experience);
		}
	}
}
