using System;

namespace SDG.Unturned
{
	// Token: 0x020003F9 RID: 1017
	public class NPCAchievementReward : INPCReward
	{
		// Token: 0x06001B67 RID: 7015 RVA: 0x000979F9 File Offset: 0x00095DF9
		public NPCAchievementReward(string newID, string newText) : base(newText)
		{
			this.id = newID;
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x00097A09 File Offset: 0x00095E09
		// (set) Token: 0x06001B69 RID: 7017 RVA: 0x00097A11 File Offset: 0x00095E11
		public string id { get; protected set; }

		// Token: 0x06001B6A RID: 7018 RVA: 0x00097A1C File Offset: 0x00095E1C
		public override void grantReward(Player player, bool shouldSend)
		{
			if (!player.channel.isOwner)
			{
				return;
			}
			bool flag;
			if (Provider.provider.achievementsService.getAchievement(this.id, out flag) && !flag)
			{
				Provider.provider.achievementsService.setAchievement(this.id);
			}
		}
	}
}
