using System;

namespace Pathfinding
{
	// Token: 0x020000C9 RID: 201
	public interface IPathModifier
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060006B4 RID: 1716
		// (set) Token: 0x060006B5 RID: 1717
		int Priority { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060006B6 RID: 1718
		ModifierData input { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060006B7 RID: 1719
		ModifierData output { get; }

		// Token: 0x060006B8 RID: 1720
		void ApplyOriginal(Path p);

		// Token: 0x060006B9 RID: 1721
		void Apply(Path p, ModifierData source);

		// Token: 0x060006BA RID: 1722
		void PreProcess(Path p);
	}
}
