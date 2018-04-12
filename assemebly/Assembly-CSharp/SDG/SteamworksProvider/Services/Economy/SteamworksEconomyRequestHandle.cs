using System;
using SDG.Provider.Services.Economy;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Economy
{
	// Token: 0x0200036D RID: 877
	public class SteamworksEconomyRequestHandle : IEconomyRequestHandle
	{
		// Token: 0x060017FA RID: 6138 RVA: 0x000888EB File Offset: 0x00086CEB
		public SteamworksEconomyRequestHandle(SteamInventoryResult_t newSteamInventoryResult, EconomyRequestReadyCallback newEconomyRequestReadyCallback)
		{
			this.steamInventoryResult = newSteamInventoryResult;
			this.economyRequestReadyCallback = newEconomyRequestReadyCallback;
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x00088901 File Offset: 0x00086D01
		// (set) Token: 0x060017FC RID: 6140 RVA: 0x00088909 File Offset: 0x00086D09
		public SteamInventoryResult_t steamInventoryResult { get; protected set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x00088912 File Offset: 0x00086D12
		// (set) Token: 0x060017FE RID: 6142 RVA: 0x0008891A File Offset: 0x00086D1A
		private EconomyRequestReadyCallback economyRequestReadyCallback { get; set; }

		// Token: 0x060017FF RID: 6143 RVA: 0x00088923 File Offset: 0x00086D23
		public void triggerInventoryRequestReadyCallback(IEconomyRequestResult inventoryRequestResult)
		{
			if (this.economyRequestReadyCallback == null)
			{
				return;
			}
			this.economyRequestReadyCallback(this, inventoryRequestResult);
		}
	}
}
