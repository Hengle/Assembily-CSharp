using System;

namespace SDG.Unturned
{
	// Token: 0x0200040B RID: 1035
	public class NPCPlayerLifeWaterCondition : NPCLogicCondition
	{
		// Token: 0x06001BEB RID: 7147 RVA: 0x00098E0F File Offset: 0x0009720F
		public NPCPlayerLifeWaterCondition(int newWater, ENPCLogicType newLogicType, string newText) : base(newLogicType, newText, false)
		{
			this.water = newWater;
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x00098E21 File Offset: 0x00097221
		// (set) Token: 0x06001BED RID: 7149 RVA: 0x00098E29 File Offset: 0x00097229
		public int water { get; protected set; }

		// Token: 0x06001BEE RID: 7150 RVA: 0x00098E32 File Offset: 0x00097232
		public override bool isConditionMet(Player player)
		{
			return base.doesLogicPass<int>((int)player.life.water, this.water);
		}
	}
}
