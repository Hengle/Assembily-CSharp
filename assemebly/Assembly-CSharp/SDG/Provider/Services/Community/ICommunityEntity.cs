using System;
using SDG.Framework.IO.Streams;

namespace SDG.Provider.Services.Community
{
	// Token: 0x0200031D RID: 797
	public interface ICommunityEntity : INetworkStreamable
	{
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060016AE RID: 5806
		bool isValid { get; }
	}
}
