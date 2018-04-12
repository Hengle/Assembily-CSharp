using System;

namespace SDG.Framework.Utilities
{
	// Token: 0x02000300 RID: 768
	public interface IPoolable
	{
		// Token: 0x0600160A RID: 5642
		void poolClaim();

		// Token: 0x0600160B RID: 5643
		void poolRelease();
	}
}
