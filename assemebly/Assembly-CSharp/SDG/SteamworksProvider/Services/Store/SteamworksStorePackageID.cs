using System;
using SDG.Provider.Services.Store;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Store
{
	// Token: 0x02000379 RID: 889
	public class SteamworksStorePackageID : IStorePackageID
	{
		// Token: 0x06001868 RID: 6248 RVA: 0x00089694 File Offset: 0x00087A94
		public SteamworksStorePackageID(uint appID)
		{
			this.appID = new AppId_t(appID);
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x000896A8 File Offset: 0x00087AA8
		// (set) Token: 0x0600186A RID: 6250 RVA: 0x000896B0 File Offset: 0x00087AB0
		public AppId_t appID { get; protected set; }
	}
}
