using System;

namespace SDG.Unturned
{
	// Token: 0x02000783 RID: 1923
	public class EconCraftOption
	{
		// Token: 0x0600374E RID: 14158 RVA: 0x00182688 File Offset: 0x00180A88
		public EconCraftOption(string newToken, int newGenerate, int newScrapsNeeded)
		{
			this.token = newToken;
			this.generate = newGenerate;
			this.scrapsNeeded = newScrapsNeeded;
		}

		// Token: 0x040028AD RID: 10413
		public string token;

		// Token: 0x040028AE RID: 10414
		public int generate;

		// Token: 0x040028AF RID: 10415
		public int scrapsNeeded;
	}
}
