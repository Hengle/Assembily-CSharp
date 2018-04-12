using System;

namespace SDG.Provider.Services.Economy
{
	// Token: 0x02000327 RID: 807
	public interface IEconomyService : IService
	{
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060016C3 RID: 5827
		bool canOpenInventory { get; }

		// Token: 0x060016C4 RID: 5828
		IEconomyRequestHandle requestInventory(EconomyRequestReadyCallback economyRequestReadyCallback);

		// Token: 0x060016C5 RID: 5829
		IEconomyRequestHandle requestPromo(EconomyRequestReadyCallback economyRequestReadyCallback);

		// Token: 0x060016C6 RID: 5830
		IEconomyRequestHandle exchangeItems(IEconomyItemInstance[] inputItemInstanceIDs, uint[] inputItemQuantities, IEconomyItemDefinition[] outputItemDefinitionIDs, uint[] outputItemQuantities, EconomyRequestReadyCallback economyRequestReadyCallback);

		// Token: 0x060016C7 RID: 5831
		void open(ulong id);
	}
}
