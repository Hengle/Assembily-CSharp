using System;
using SDG.Framework.IO.Streams;

namespace SDG.Provider.Services.Economy
{
	// Token: 0x02000322 RID: 802
	public interface IEconomyItem : INetworkStreamable
	{
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060016BE RID: 5822
		IEconomyItemDefinition itemDefinitionID { get; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060016BF RID: 5823
		IEconomyItemInstance itemInstanceID { get; }
	}
}
