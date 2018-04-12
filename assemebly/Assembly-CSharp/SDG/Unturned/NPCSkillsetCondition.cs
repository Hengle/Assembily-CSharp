using System;

namespace SDG.Unturned
{
	// Token: 0x02000414 RID: 1044
	public class NPCSkillsetCondition : NPCLogicCondition
	{
		// Token: 0x06001C23 RID: 7203 RVA: 0x000994B0 File Offset: 0x000978B0
		public NPCSkillsetCondition(EPlayerSkillset newSkillset, ENPCLogicType newLogicType, string newText) : base(newLogicType, newText, false)
		{
			this.skillset = newSkillset;
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x000994C2 File Offset: 0x000978C2
		// (set) Token: 0x06001C25 RID: 7205 RVA: 0x000994CA File Offset: 0x000978CA
		public EPlayerSkillset skillset { get; protected set; }

		// Token: 0x06001C26 RID: 7206 RVA: 0x000994D3 File Offset: 0x000978D3
		public override bool isConditionMet(Player player)
		{
			return base.doesLogicPass<EPlayerSkillset>(player.channel.owner.skillset, this.skillset);
		}
	}
}
