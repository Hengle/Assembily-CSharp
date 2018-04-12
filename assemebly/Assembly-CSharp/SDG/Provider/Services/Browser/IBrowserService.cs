using System;

namespace SDG.Provider.Services.Browser
{
	// Token: 0x0200031B RID: 795
	public interface IBrowserService : IService
	{
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060016A7 RID: 5799
		bool canOpenBrowser { get; }

		// Token: 0x060016A8 RID: 5800
		void open(string url);
	}
}
