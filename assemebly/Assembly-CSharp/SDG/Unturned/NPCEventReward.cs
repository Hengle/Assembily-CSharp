using System;

namespace SDG.Unturned
{
	// Token: 0x020003FE RID: 1022
	public class NPCEventReward : INPCReward
	{
		// Token: 0x06001B8B RID: 7051 RVA: 0x00097E9E File Offset: 0x0009629E
		public NPCEventReward(string newID, string newText) : base(newText)
		{
			this.id = newID;
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x00097EAE File Offset: 0x000962AE
		// (set) Token: 0x06001B8D RID: 7053 RVA: 0x00097EB6 File Offset: 0x000962B6
		public string id { get; protected set; }

		// Token: 0x06001B8E RID: 7054 RVA: 0x00097EBF File Offset: 0x000962BF
		public override void grantReward(Player player, bool shouldSend)
		{
			NPCEventManager.triggerEventTriggered(this.id);
		}
	}
}
