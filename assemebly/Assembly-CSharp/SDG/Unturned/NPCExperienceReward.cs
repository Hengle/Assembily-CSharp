using System;

namespace SDG.Unturned
{
	// Token: 0x02000400 RID: 1024
	public class NPCExperienceReward : INPCReward
	{
		// Token: 0x06001B95 RID: 7061 RVA: 0x00097F9C File Offset: 0x0009639C
		public NPCExperienceReward(uint newValue, string newText) : base(newText)
		{
			this.value = newValue;
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x00097FAC File Offset: 0x000963AC
		// (set) Token: 0x06001B97 RID: 7063 RVA: 0x00097FB4 File Offset: 0x000963B4
		public uint value { get; protected set; }

		// Token: 0x06001B98 RID: 7064 RVA: 0x00097FBD File Offset: 0x000963BD
		public override void grantReward(Player player, bool shouldSend)
		{
			if (shouldSend)
			{
				player.skills.askAward(this.value);
			}
			else
			{
				player.skills.modXp(this.value);
			}
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x00097FEC File Offset: 0x000963EC
		public override string formatReward(Player player)
		{
			if (string.IsNullOrEmpty(this.text))
			{
				this.text = PlayerNPCQuestUI.localization.read("Reward_Experience");
			}
			string arg = "+" + this.value;
			return string.Format(this.text, arg);
		}
	}
}
