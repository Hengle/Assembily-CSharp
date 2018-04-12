using System;

namespace Pathfinding
{
	// Token: 0x020000C8 RID: 200
	[Flags]
	public enum ModifierData
	{
		// Token: 0x040005A3 RID: 1443
		All = -1,
		// Token: 0x040005A4 RID: 1444
		StrictNodePath = 1,
		// Token: 0x040005A5 RID: 1445
		NodePath = 2,
		// Token: 0x040005A6 RID: 1446
		StrictVectorPath = 4,
		// Token: 0x040005A7 RID: 1447
		VectorPath = 8,
		// Token: 0x040005A8 RID: 1448
		Original = 16,
		// Token: 0x040005A9 RID: 1449
		None = 0,
		// Token: 0x040005AA RID: 1450
		Nodes = 3,
		// Token: 0x040005AB RID: 1451
		Vector = 12
	}
}
