using System;

namespace SDG.Unturned
{
	// Token: 0x02000418 RID: 1048
	public class NPCZombieKillsCondition : INPCCondition
	{
		// Token: 0x06001C38 RID: 7224 RVA: 0x0009988C File Offset: 0x00097C8C
		public NPCZombieKillsCondition(ushort newID, short newValue, EZombieSpeciality newZombie, bool newSpawn, byte newNav, string newText, bool newShouldReset) : base(newText, newShouldReset)
		{
			this.id = newID;
			this.value = newValue;
			this.zombie = newZombie;
			this.spawn = newSpawn;
			this.nav = newNav;
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x000998BD File Offset: 0x00097CBD
		// (set) Token: 0x06001C3A RID: 7226 RVA: 0x000998C5 File Offset: 0x00097CC5
		public ushort id { get; protected set; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001C3B RID: 7227 RVA: 0x000998CE File Offset: 0x00097CCE
		// (set) Token: 0x06001C3C RID: 7228 RVA: 0x000998D6 File Offset: 0x00097CD6
		public short value { get; protected set; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x000998DF File Offset: 0x00097CDF
		// (set) Token: 0x06001C3E RID: 7230 RVA: 0x000998E7 File Offset: 0x00097CE7
		public EZombieSpeciality zombie { get; protected set; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x000998F0 File Offset: 0x00097CF0
		// (set) Token: 0x06001C40 RID: 7232 RVA: 0x000998F8 File Offset: 0x00097CF8
		public bool spawn { get; protected set; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x00099901 File Offset: 0x00097D01
		// (set) Token: 0x06001C42 RID: 7234 RVA: 0x00099909 File Offset: 0x00097D09
		public byte nav { get; protected set; }

		// Token: 0x06001C43 RID: 7235 RVA: 0x00099914 File Offset: 0x00097D14
		public override bool isConditionMet(Player player)
		{
			short num;
			return player.quests.getFlag(this.id, out num) && num >= this.value;
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x00099947 File Offset: 0x00097D47
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

		// Token: 0x06001C45 RID: 7237 RVA: 0x00099984 File Offset: 0x00097D84
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
