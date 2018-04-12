using System;

namespace SDG.Unturned
{
	// Token: 0x0200069F RID: 1695
	public class ConfigData
	{
		// Token: 0x060031BE RID: 12734 RVA: 0x001431D4 File Offset: 0x001415D4
		public ConfigData()
		{
			this.Browser = new BrowserConfigData();
			this.Server = new ServerConfigData();
			this.Easy = new ModeConfigData(EGameMode.EASY);
			this.Normal = new ModeConfigData(EGameMode.NORMAL);
			this.Hard = new ModeConfigData(EGameMode.HARD);
		}

		// Token: 0x04002109 RID: 8457
		public BrowserConfigData Browser;

		// Token: 0x0400210A RID: 8458
		public ServerConfigData Server;

		// Token: 0x0400210B RID: 8459
		public ModeConfigData Easy;

		// Token: 0x0400210C RID: 8460
		public ModeConfigData Normal;

		// Token: 0x0400210D RID: 8461
		public ModeConfigData Hard;
	}
}
