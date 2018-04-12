using System;

namespace SDG.Unturned
{
	// Token: 0x0200052C RID: 1324
	public interface IAmbianceNode
	{
		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060023A7 RID: 9127
		ushort id { get; }

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060023A8 RID: 9128
		bool noWater { get; }

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060023A9 RID: 9129
		bool noLighting { get; }
	}
}
