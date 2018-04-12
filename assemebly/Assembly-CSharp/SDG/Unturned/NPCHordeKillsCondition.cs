using System;

namespace SDG.Unturned
{
	// Token: 0x02000403 RID: 1027
	public class NPCHordeKillsCondition : INPCCondition
	{
		// Token: 0x06001BA7 RID: 7079 RVA: 0x00098150 File Offset: 0x00096550
		public NPCHordeKillsCondition(ushort newID, short newValue, byte newNav, string newText, bool newShouldReset) : base(newText, newShouldReset)
		{
			this.id = newID;
			this.value = newValue;
			this.nav = newNav;
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x00098171 File Offset: 0x00096571
		// (set) Token: 0x06001BA9 RID: 7081 RVA: 0x00098179 File Offset: 0x00096579
		public ushort id { get; protected set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x00098182 File Offset: 0x00096582
		// (set) Token: 0x06001BAB RID: 7083 RVA: 0x0009818A File Offset: 0x0009658A
		public short value { get; protected set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x00098193 File Offset: 0x00096593
		// (set) Token: 0x06001BAD RID: 7085 RVA: 0x0009819B File Offset: 0x0009659B
		public byte nav { get; protected set; }

		// Token: 0x06001BAE RID: 7086 RVA: 0x000981A4 File Offset: 0x000965A4
		public override bool isConditionMet(Player player)
		{
			short num;
			return player.quests.getFlag(this.id, out num) && num >= this.value;
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x000981D7 File Offset: 0x000965D7
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

		// Token: 0x06001BB0 RID: 7088 RVA: 0x00098214 File Offset: 0x00096614
		public override string formatCondition(Player player)
		{
			if (string.IsNullOrEmpty(this.text))
			{
				return null;
			}
			short num;
			if (!player.quests.getFlag(this.id, out num))
			{
				num = 0;
			}
			return string.Format(this.text, num, this.value);
		}
	}
}
