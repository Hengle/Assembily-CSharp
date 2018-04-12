using System;

namespace SDG.Unturned
{
	// Token: 0x020003FA RID: 1018
	public class NPCAnimalKillsCondition : INPCCondition
	{
		// Token: 0x06001B6B RID: 7019 RVA: 0x00097A72 File Offset: 0x00095E72
		public NPCAnimalKillsCondition(ushort newID, short newValue, ushort newAnimal, string newText, bool newShouldReset) : base(newText, newShouldReset)
		{
			this.id = newID;
			this.value = newValue;
			this.animal = newAnimal;
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x00097A93 File Offset: 0x00095E93
		// (set) Token: 0x06001B6D RID: 7021 RVA: 0x00097A9B File Offset: 0x00095E9B
		public ushort id { get; protected set; }

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x00097AA4 File Offset: 0x00095EA4
		// (set) Token: 0x06001B6F RID: 7023 RVA: 0x00097AAC File Offset: 0x00095EAC
		public short value { get; protected set; }

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x00097AB5 File Offset: 0x00095EB5
		// (set) Token: 0x06001B71 RID: 7025 RVA: 0x00097ABD File Offset: 0x00095EBD
		public ushort animal { get; protected set; }

		// Token: 0x06001B72 RID: 7026 RVA: 0x00097AC8 File Offset: 0x00095EC8
		public override bool isConditionMet(Player player)
		{
			short num;
			return player.quests.getFlag(this.id, out num) && num >= this.value;
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x00097AFB File Offset: 0x00095EFB
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

		// Token: 0x06001B74 RID: 7028 RVA: 0x00097B38 File Offset: 0x00095F38
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
