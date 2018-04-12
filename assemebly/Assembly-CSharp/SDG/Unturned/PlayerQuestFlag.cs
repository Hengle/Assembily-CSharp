using System;

namespace SDG.Unturned
{
	// Token: 0x02000638 RID: 1592
	public class PlayerQuestFlag
	{
		// Token: 0x06002D79 RID: 11641 RVA: 0x00125986 File Offset: 0x00123D86
		public PlayerQuestFlag(ushort newID, short newValue)
		{
			this.id = newID;
			this.value = newValue;
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x0012599C File Offset: 0x00123D9C
		// (set) Token: 0x06002D7B RID: 11643 RVA: 0x001259A4 File Offset: 0x00123DA4
		public ushort id { get; private set; }

		// Token: 0x04001D82 RID: 7554
		public short value;
	}
}
