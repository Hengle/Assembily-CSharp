using System;

namespace SDG.Unturned
{
	// Token: 0x02000409 RID: 1033
	public class NPCPlayerLifeHealthCondition : NPCLogicCondition
	{
		// Token: 0x06001BE3 RID: 7139 RVA: 0x00098D97 File Offset: 0x00097197
		public NPCPlayerLifeHealthCondition(int newHealth, ENPCLogicType newLogicType, string newText) : base(newLogicType, newText, false)
		{
			this.health = newHealth;
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x00098DA9 File Offset: 0x000971A9
		// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x00098DB1 File Offset: 0x000971B1
		public int health { get; protected set; }

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00098DBA File Offset: 0x000971BA
		public override bool isConditionMet(Player player)
		{
			return base.doesLogicPass<int>((int)player.life.health, this.health);
		}
	}
}
