using System;
using SDG.Framework.IO.Streams;

namespace SDG.Provider.Services.Economy
{
	// Token: 0x02000323 RID: 803
	public interface IEconomyItemDefinition : INetworkStreamable
	{
		// Token: 0x060016C0 RID: 5824
		string getPropertyValue(string key);
	}
}
