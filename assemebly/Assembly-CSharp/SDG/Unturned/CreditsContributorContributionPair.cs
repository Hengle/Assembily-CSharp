using System;

namespace SDG.Unturned
{
	// Token: 0x02000769 RID: 1897
	public struct CreditsContributorContributionPair
	{
		// Token: 0x06003653 RID: 13907 RVA: 0x00170F11 File Offset: 0x0016F311
		public CreditsContributorContributionPair(string newContributor, string newContribution)
		{
			this.contributor = newContributor;
			this.contribution = newContribution;
		}

		// Token: 0x040026C4 RID: 9924
		public string contributor;

		// Token: 0x040026C5 RID: 9925
		public string contribution;
	}
}
