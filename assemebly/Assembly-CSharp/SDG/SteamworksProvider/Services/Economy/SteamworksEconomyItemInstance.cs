using System;
using SDG.Framework.IO.Streams;
using SDG.Provider.Services.Economy;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Economy
{
	// Token: 0x0200036C RID: 876
	public class SteamworksEconomyItemInstance : IEconomyItemInstance, INetworkStreamable
	{
		// Token: 0x060017F5 RID: 6133 RVA: 0x000888A5 File Offset: 0x00086CA5
		public SteamworksEconomyItemInstance(SteamItemInstanceID_t newSteamItemInstanceID)
		{
			this.steamItemInstanceID = newSteamItemInstanceID;
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x000888B4 File Offset: 0x00086CB4
		// (set) Token: 0x060017F7 RID: 6135 RVA: 0x000888BC File Offset: 0x00086CBC
		public SteamItemInstanceID_t steamItemInstanceID { get; protected set; }

		// Token: 0x060017F8 RID: 6136 RVA: 0x000888C5 File Offset: 0x00086CC5
		public void readFromStream(NetworkStream networkStream)
		{
			this.steamItemInstanceID = (SteamItemInstanceID_t)networkStream.readUInt64();
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x000888D8 File Offset: 0x00086CD8
		public void writeToStream(NetworkStream networkStream)
		{
			networkStream.writeUInt64((ulong)this.steamItemInstanceID);
		}
	}
}
