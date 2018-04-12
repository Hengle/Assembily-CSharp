using System;

namespace SDG.Unturned
{
	// Token: 0x020006B2 RID: 1714
	public class StatusData
	{
		// Token: 0x060031D4 RID: 12756 RVA: 0x0014419C File Offset: 0x0014259C
		public StatusData()
		{
			this.Alert = new AlertStatusData();
			this.News = new NewsStatusData();
			this.Maps = new MapsStatusData();
			this.Stockpile = new StockpileStatusData();
		}

		// Token: 0x040021C6 RID: 8646
		public AlertStatusData Alert;

		// Token: 0x040021C7 RID: 8647
		public NewsStatusData News;

		// Token: 0x040021C8 RID: 8648
		public MapsStatusData Maps;

		// Token: 0x040021C9 RID: 8649
		public StockpileStatusData Stockpile;
	}
}
