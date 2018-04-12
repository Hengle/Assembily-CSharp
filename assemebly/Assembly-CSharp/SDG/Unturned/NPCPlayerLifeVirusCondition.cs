using System;

namespace SDG.Unturned
{
	// Token: 0x0200040A RID: 1034
	public class NPCPlayerLifeVirusCondition : NPCLogicCondition
	{
		// Token: 0x06001BE7 RID: 7143 RVA: 0x00098DD3 File Offset: 0x000971D3
		public NPCPlayerLifeVirusCondition(int newVirus, ENPCLogicType newLogicType, string newText) : base(newLogicType, newText, false)
		{
			this.virus = newVirus;
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x00098DE5 File Offset: 0x000971E5
		// (set) Token: 0x06001BE9 RID: 7145 RVA: 0x00098DED File Offset: 0x000971ED
		public int virus { get; protected set; }

		// Token: 0x06001BEA RID: 7146 RVA: 0x00098DF6 File Offset: 0x000971F6
		public override bool isConditionMet(Player player)
		{
			return base.doesLogicPass<int>((int)player.life.virus, this.virus);
		}
	}
}
