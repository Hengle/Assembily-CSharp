using System;

namespace SDG.Unturned
{
	// Token: 0x02000411 RID: 1041
	public class NPCReputationReward : INPCReward
	{
		// Token: 0x06001C10 RID: 7184 RVA: 0x000992EB File Offset: 0x000976EB
		public NPCReputationReward(int newValue, string newText) : base(newText)
		{
			this.value = newValue;
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x000992FB File Offset: 0x000976FB
		// (set) Token: 0x06001C12 RID: 7186 RVA: 0x00099303 File Offset: 0x00097703
		public int value { get; protected set; }

		// Token: 0x06001C13 RID: 7187 RVA: 0x0009930C File Offset: 0x0009770C
		public override void grantReward(Player player, bool shouldSend)
		{
			if (shouldSend)
			{
				player.skills.askRep(this.value);
			}
			else
			{
				player.skills.modRep(this.value);
			}
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x0009933C File Offset: 0x0009773C
		public override string formatReward(Player player)
		{
			if (string.IsNullOrEmpty(this.text))
			{
				this.text = PlayerNPCQuestUI.localization.read("Reward_Reputation");
			}
			string text = this.value.ToString();
			if (this.value > 0)
			{
				text = "+" + text;
			}
			return string.Format(this.text, text);
		}
	}
}
