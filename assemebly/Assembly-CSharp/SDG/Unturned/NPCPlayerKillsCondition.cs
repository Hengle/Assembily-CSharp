using System;

namespace SDG.Unturned
{
	// Token: 0x02000407 RID: 1031
	public class NPCPlayerKillsCondition : INPCCondition
	{
		// Token: 0x06001BD3 RID: 7123 RVA: 0x00098C3B File Offset: 0x0009703B
		public NPCPlayerKillsCondition(ushort newID, short newValue, short newTheirRep, ENPCLogicType newTheirRepLogic, string newText, bool newShouldReset) : base(newText, newShouldReset)
		{
			this.id = newID;
			this.value = newValue;
			this.theirRep = newTheirRep;
			this.theirRepLogic = newTheirRepLogic;
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x00098C64 File Offset: 0x00097064
		// (set) Token: 0x06001BD5 RID: 7125 RVA: 0x00098C6C File Offset: 0x0009706C
		public ushort id { get; protected set; }

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x00098C75 File Offset: 0x00097075
		// (set) Token: 0x06001BD7 RID: 7127 RVA: 0x00098C7D File Offset: 0x0009707D
		public short value { get; protected set; }

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x00098C86 File Offset: 0x00097086
		// (set) Token: 0x06001BD9 RID: 7129 RVA: 0x00098C8E File Offset: 0x0009708E
		public short theirRep { get; protected set; }

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x00098C97 File Offset: 0x00097097
		// (set) Token: 0x06001BDB RID: 7131 RVA: 0x00098C9F File Offset: 0x0009709F
		public ENPCLogicType theirRepLogic { get; protected set; }

		// Token: 0x06001BDC RID: 7132 RVA: 0x00098CA8 File Offset: 0x000970A8
		public override bool isConditionMet(Player player)
		{
			short num;
			return player.quests.getFlag(this.id, out num) && num >= this.value;
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x00098CDB File Offset: 0x000970DB
		public override void applyCondition(Player player, bool shouldSend)
		{
			if (!this.shouldReset)
			{
				return;
			}
			if (shouldSend)
			{
				player.quests.sendRemoveFlag(this.id);
			}
			else
			{
				player.quests.removeFlag(this.id);
			}
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x00098D18 File Offset: 0x00097118
		public override string formatCondition(Player player)
		{
			short num;
			if (!player.quests.getFlag(this.id, out num))
			{
				num = 0;
			}
			return string.Format(this.text, num, this.value);
		}
	}
}
