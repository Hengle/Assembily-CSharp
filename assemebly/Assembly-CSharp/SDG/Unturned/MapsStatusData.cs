using System;

namespace SDG.Unturned
{
	// Token: 0x020006B6 RID: 1718
	public class MapsStatusData
	{
		// Token: 0x060031D7 RID: 12759 RVA: 0x0014422A File Offset: 0x0014262A
		public MapsStatusData()
		{
			this.Official = EMapStatus.NONE;
			this.Curated = EMapStatus.NONE;
			this.Misc = EMapStatus.NONE;
		}

		// Token: 0x040021D6 RID: 8662
		public EMapStatus Official;

		// Token: 0x040021D7 RID: 8663
		public EMapStatus Curated;

		// Token: 0x040021D8 RID: 8664
		public EMapStatus Misc;
	}
}
