using System;
using Steamworks;

namespace SDG.Provider.Services.Workshop
{
	// Token: 0x02000364 RID: 868
	public interface IWorkshopService : IService
	{
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060017CA RID: 6090
		bool canOpenWorkshop { get; }

		// Token: 0x060017CB RID: 6091
		void open(PublishedFileId_t id);
	}
}
