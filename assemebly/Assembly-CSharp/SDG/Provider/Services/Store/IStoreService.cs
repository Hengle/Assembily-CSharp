using System;
using SDG.Provider.Services.Economy;

namespace SDG.Provider.Services.Store
{
	// Token: 0x02000344 RID: 836
	public interface IStoreService : IService
	{
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001724 RID: 5924
		bool canOpenStore { get; }

		// Token: 0x06001725 RID: 5925
		void open(IStorePackageID packageID);

		// Token: 0x06001726 RID: 5926
		void open(IEconomyItemDefinition itemDefinitionID);

		// Token: 0x06001727 RID: 5927
		void open();
	}
}
