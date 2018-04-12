using System;

namespace SDG.SteamworksProvider
{
	// Token: 0x0200037F RID: 895
	public class SteamworksAppInfo
	{
		// Token: 0x06001885 RID: 6277 RVA: 0x00089A1B File Offset: 0x00087E1B
		public SteamworksAppInfo(uint newID, string newName, string newVersion, bool newIsDedicated)
		{
			this.id = newID;
			this.name = newName;
			this.version = newVersion;
			this.isDedicated = newIsDedicated;
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x00089A40 File Offset: 0x00087E40
		// (set) Token: 0x06001887 RID: 6279 RVA: 0x00089A48 File Offset: 0x00087E48
		public uint id { get; protected set; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x00089A51 File Offset: 0x00087E51
		// (set) Token: 0x06001889 RID: 6281 RVA: 0x00089A59 File Offset: 0x00087E59
		public string name { get; protected set; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x00089A62 File Offset: 0x00087E62
		// (set) Token: 0x0600188B RID: 6283 RVA: 0x00089A6A File Offset: 0x00087E6A
		public string version { get; protected set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x00089A73 File Offset: 0x00087E73
		// (set) Token: 0x0600188D RID: 6285 RVA: 0x00089A7B File Offset: 0x00087E7B
		public bool isDedicated { get; protected set; }
	}
}
