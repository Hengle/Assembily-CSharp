using System;

namespace SDG.Unturned
{
	// Token: 0x020003FC RID: 1020
	public class NPCBoolFlagReward : INPCReward
	{
		// Token: 0x06001B7B RID: 7035 RVA: 0x00097CC2 File Offset: 0x000960C2
		public NPCBoolFlagReward(ushort newID, bool newValue, string newText) : base(newText)
		{
			this.id = newID;
			this.value = newValue;
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001B7C RID: 7036 RVA: 0x00097CD9 File Offset: 0x000960D9
		// (set) Token: 0x06001B7D RID: 7037 RVA: 0x00097CE1 File Offset: 0x000960E1
		public ushort id { get; protected set; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x00097CEA File Offset: 0x000960EA
		// (set) Token: 0x06001B7F RID: 7039 RVA: 0x00097CF2 File Offset: 0x000960F2
		public bool value { get; protected set; }

		// Token: 0x06001B80 RID: 7040 RVA: 0x00097CFC File Offset: 0x000960FC
		public override void grantReward(Player player, bool shouldSend)
		{
			if (shouldSend)
			{
				player.quests.sendSetFlag(this.id, (!this.value) ? 0 : 1);
			}
			else
			{
				player.quests.setFlag(this.id, (!this.value) ? 0 : 1);
			}
		}
	}
}
