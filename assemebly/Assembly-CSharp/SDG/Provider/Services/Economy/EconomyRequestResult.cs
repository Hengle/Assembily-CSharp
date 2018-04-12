using System;

namespace SDG.Provider.Services.Economy
{
	// Token: 0x02000321 RID: 801
	public class EconomyRequestResult : IEconomyRequestResult
	{
		// Token: 0x060016B9 RID: 5817 RVA: 0x00085783 File Offset: 0x00083B83
		public EconomyRequestResult(EEconomyRequestState newEconomyRequestState, IEconomyItem[] newItems)
		{
			this.economyRequestState = newEconomyRequestState;
			this.items = newItems;
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x00085799 File Offset: 0x00083B99
		// (set) Token: 0x060016BB RID: 5819 RVA: 0x000857A1 File Offset: 0x00083BA1
		public EEconomyRequestState economyRequestState { get; protected set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x000857AA File Offset: 0x00083BAA
		// (set) Token: 0x060016BD RID: 5821 RVA: 0x000857B2 File Offset: 0x00083BB2
		public IEconomyItem[] items { get; protected set; }
	}
}
