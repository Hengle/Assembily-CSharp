using System;

namespace SDG.Unturned
{
	// Token: 0x02000636 RID: 1590
	public class PlayerQuest
	{
		// Token: 0x06002D72 RID: 11634 RVA: 0x00125926 File Offset: 0x00123D26
		public PlayerQuest(ushort newID)
		{
			this.id = newID;
			this.asset = (Assets.find(EAssetType.NPC, this.id) as QuestAsset);
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002D73 RID: 11635 RVA: 0x0012594D File Offset: 0x00123D4D
		// (set) Token: 0x06002D74 RID: 11636 RVA: 0x00125955 File Offset: 0x00123D55
		public ushort id { get; private set; }

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002D75 RID: 11637 RVA: 0x0012595E File Offset: 0x00123D5E
		// (set) Token: 0x06002D76 RID: 11638 RVA: 0x00125966 File Offset: 0x00123D66
		public QuestAsset asset { get; protected set; }
	}
}
