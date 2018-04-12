using System;

namespace SDG.Unturned
{
	// Token: 0x020006B3 RID: 1715
	public class AlertStatusData
	{
		// Token: 0x060031D5 RID: 12757 RVA: 0x001441D0 File Offset: 0x001425D0
		public AlertStatusData()
		{
			this.Header = string.Empty;
			this.Body = string.Empty;
			this.Color = string.Empty;
			this.Link = string.Empty;
		}

		// Token: 0x040021CA RID: 8650
		public string Header;

		// Token: 0x040021CB RID: 8651
		public string Body;

		// Token: 0x040021CC RID: 8652
		public string Color;

		// Token: 0x040021CD RID: 8653
		public string Link;
	}
}
