using System;

namespace SDG.Unturned
{
	// Token: 0x02000413 RID: 1043
	public class NPCShortFlagReward : INPCReward
	{
		// Token: 0x06001C1B RID: 7195 RVA: 0x0009906F File Offset: 0x0009746F
		public NPCShortFlagReward(ushort newID, short newValue, ENPCModificationType newModificationType, string newText) : base(newText)
		{
			this.id = newID;
			this.value = newValue;
			this.modificationType = newModificationType;
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x0009908E File Offset: 0x0009748E
		// (set) Token: 0x06001C1D RID: 7197 RVA: 0x00099096 File Offset: 0x00097496
		public ushort id { get; protected set; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001C1E RID: 7198 RVA: 0x0009909F File Offset: 0x0009749F
		// (set) Token: 0x06001C1F RID: 7199 RVA: 0x000990A7 File Offset: 0x000974A7
		public virtual short value { get; protected set; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x000990B0 File Offset: 0x000974B0
		// (set) Token: 0x06001C21 RID: 7201 RVA: 0x000990B8 File Offset: 0x000974B8
		public ENPCModificationType modificationType { get; protected set; }

		// Token: 0x06001C22 RID: 7202 RVA: 0x000990C4 File Offset: 0x000974C4
		public override void grantReward(Player player, bool shouldSend)
		{
			if (this.modificationType == ENPCModificationType.ASSIGN)
			{
				if (shouldSend)
				{
					player.quests.sendSetFlag(this.id, this.value);
				}
				else
				{
					player.quests.setFlag(this.id, this.value);
				}
			}
			else
			{
				short num;
				player.quests.getFlag(this.id, out num);
				if (this.modificationType == ENPCModificationType.INCREMENT)
				{
					num += this.value;
				}
				else if (this.modificationType == ENPCModificationType.DECREMENT)
				{
					num -= this.value;
				}
				if (shouldSend)
				{
					player.quests.sendSetFlag(this.id, num);
				}
				else
				{
					player.quests.setFlag(this.id, num);
				}
			}
		}
	}
}
