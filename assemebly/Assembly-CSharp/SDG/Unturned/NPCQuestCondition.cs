using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200040C RID: 1036
	public class NPCQuestCondition : NPCLogicCondition
	{
		// Token: 0x06001BEF RID: 7151 RVA: 0x00098E4B File Offset: 0x0009724B
		public NPCQuestCondition(ushort newID, ENPCQuestStatus newStatus, bool newIgnoreNPC, ENPCLogicType newLogicType, string newText, bool newShouldReset) : base(newLogicType, newText, newShouldReset)
		{
			this.id = newID;
			this.status = newStatus;
			this.ignoreNPC = newIgnoreNPC;
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x00098E6E File Offset: 0x0009726E
		// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x00098E76 File Offset: 0x00097276
		public ushort id { get; protected set; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x00098E7F File Offset: 0x0009727F
		// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x00098E87 File Offset: 0x00097287
		public ENPCQuestStatus status { get; protected set; }

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x00098E90 File Offset: 0x00097290
		// (set) Token: 0x06001BF5 RID: 7157 RVA: 0x00098E98 File Offset: 0x00097298
		public bool ignoreNPC { get; protected set; }

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00098EA1 File Offset: 0x000972A1
		public override bool isConditionMet(Player player)
		{
			return base.doesLogicPass<ENPCQuestStatus>(player.quests.getQuestStatus(this.id), this.status);
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x00098EC0 File Offset: 0x000972C0
		public override void applyCondition(Player player, bool shouldSend)
		{
			if (shouldSend)
			{
				Debug.LogError("Send quest complete not supported!");
				return;
			}
			if (!this.shouldReset)
			{
				return;
			}
			switch (this.status)
			{
			case ENPCQuestStatus.NONE:
				Debug.LogError("Reset none quest status? How should this work?");
				return;
			case ENPCQuestStatus.ACTIVE:
				player.quests.abandonQuest(this.id);
				return;
			case ENPCQuestStatus.READY:
				player.quests.completeQuest(this.id, this.ignoreNPC);
				return;
			case ENPCQuestStatus.COMPLETED:
				player.quests.removeFlag(this.id);
				return;
			default:
				return;
			}
		}
	}
}
