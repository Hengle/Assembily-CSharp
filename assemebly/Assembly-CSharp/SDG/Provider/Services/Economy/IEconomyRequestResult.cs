using System;

namespace SDG.Provider.Services.Economy
{
	// Token: 0x02000326 RID: 806
	public interface IEconomyRequestResult
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060016C1 RID: 5825
		EEconomyRequestState economyRequestState { get; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060016C2 RID: 5826
		IEconomyItem[] items { get; }
	}
}
