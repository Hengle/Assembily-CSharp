using System;

namespace SDG.Unturned
{
	// Token: 0x0200076F RID: 1903
	public class AppNews
	{
		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x0600367E RID: 13950 RVA: 0x0017531C File Offset: 0x0017371C
		// (set) Token: 0x0600367F RID: 13951 RVA: 0x00175324 File Offset: 0x00173724
		public uint AppID { get; set; }

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06003680 RID: 13952 RVA: 0x0017532D File Offset: 0x0017372D
		// (set) Token: 0x06003681 RID: 13953 RVA: 0x00175335 File Offset: 0x00173735
		public NewsItem[] NewsItems { get; set; }
	}
}
