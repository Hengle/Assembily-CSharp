using System;

namespace SDG.Provider.Services.Matchmaking
{
	// Token: 0x0200033C RID: 828
	public class MatchmakingFilter : IMatchmakingFilter
	{
		// Token: 0x06001705 RID: 5893 RVA: 0x000857BB File Offset: 0x00083BBB
		public MatchmakingFilter(string newKey, string newValue)
		{
			this.key = newKey;
			this.value = newValue;
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x000857D1 File Offset: 0x00083BD1
		// (set) Token: 0x06001707 RID: 5895 RVA: 0x000857D9 File Offset: 0x00083BD9
		public string key { get; protected set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x000857E2 File Offset: 0x00083BE2
		// (set) Token: 0x06001709 RID: 5897 RVA: 0x000857EA File Offset: 0x00083BEA
		public string value { get; protected set; }
	}
}
