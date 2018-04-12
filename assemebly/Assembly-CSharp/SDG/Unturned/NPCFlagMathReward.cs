using System;

namespace SDG.Unturned
{
	// Token: 0x02000402 RID: 1026
	public class NPCFlagMathReward : INPCReward
	{
		// Token: 0x06001B9F RID: 7071 RVA: 0x00098040 File Offset: 0x00096440
		public NPCFlagMathReward(ushort newFlag_A_ID, ushort newFlag_B_ID, ENPCOperationType newOperationType, string newText) : base(newText)
		{
			this.flag_A_ID = newFlag_A_ID;
			this.flag_B_ID = newFlag_B_ID;
			this.operationType = newOperationType;
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x0009805F File Offset: 0x0009645F
		// (set) Token: 0x06001BA1 RID: 7073 RVA: 0x00098067 File Offset: 0x00096467
		public ushort flag_A_ID { get; protected set; }

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x00098070 File Offset: 0x00096470
		// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x00098078 File Offset: 0x00096478
		public ushort flag_B_ID { get; protected set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x00098081 File Offset: 0x00096481
		// (set) Token: 0x06001BA5 RID: 7077 RVA: 0x00098089 File Offset: 0x00096489
		public ENPCOperationType operationType { get; protected set; }

		// Token: 0x06001BA6 RID: 7078 RVA: 0x00098094 File Offset: 0x00096494
		public override void grantReward(Player player, bool shouldSend)
		{
			short num;
			player.quests.getFlag(this.flag_A_ID, out num);
			short num2;
			player.quests.getFlag(this.flag_B_ID, out num2);
			switch (this.operationType)
			{
			case ENPCOperationType.ASSIGN:
				num = num2;
				break;
			case ENPCOperationType.ADDITION:
				num += num2;
				break;
			case ENPCOperationType.SUBTRACTION:
				num -= num2;
				break;
			case ENPCOperationType.MULTIPLICATION:
				num *= num2;
				break;
			case ENPCOperationType.DIVISION:
				num /= num2;
				break;
			default:
				return;
			}
			if (shouldSend)
			{
				player.quests.sendSetFlag(this.flag_A_ID, num);
			}
			else
			{
				player.quests.setFlag(this.flag_A_ID, num);
			}
		}
	}
}
