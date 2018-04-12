using System;

namespace SDG.Unturned
{
	// Token: 0x020006B4 RID: 1716
	public class NewsStatusData
	{
		// Token: 0x060031D6 RID: 12758 RVA: 0x00144204 File Offset: 0x00142604
		public NewsStatusData()
		{
			this.Featured_Workshop = 0UL;
			this.Popular_Workshop_Trend_Days = 30u;
			this.Popular_Workshop_Carousel_Items = 3;
			this.Announcements_Count = 3;
		}

		// Token: 0x040021CE RID: 8654
		public ulong Featured_Workshop;

		// Token: 0x040021CF RID: 8655
		public uint Popular_Workshop_Trend_Days;

		// Token: 0x040021D0 RID: 8656
		public int Popular_Workshop_Carousel_Items;

		// Token: 0x040021D1 RID: 8657
		public int Announcements_Count;
	}
}
