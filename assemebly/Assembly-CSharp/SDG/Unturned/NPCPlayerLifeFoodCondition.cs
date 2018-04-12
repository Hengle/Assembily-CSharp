using System;

namespace SDG.Unturned
{
	// Token: 0x02000408 RID: 1032
	public class NPCPlayerLifeFoodCondition : NPCLogicCondition
	{
		// Token: 0x06001BDF RID: 7135 RVA: 0x00098D5B File Offset: 0x0009715B
		public NPCPlayerLifeFoodCondition(int newFood, ENPCLogicType newLogicType, string newText) : base(newLogicType, newText, false)
		{
			this.food = newFood;
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x00098D6D File Offset: 0x0009716D
		// (set) Token: 0x06001BE1 RID: 7137 RVA: 0x00098D75 File Offset: 0x00097175
		public int food { get; protected set; }

		// Token: 0x06001BE2 RID: 7138 RVA: 0x00098D7E File Offset: 0x0009717E
		public override bool isConditionMet(Player player)
		{
			return base.doesLogicPass<int>((int)player.life.food, this.food);
		}
	}
}
