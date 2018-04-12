using System;

namespace Pathfinding.RVO
{
	// Token: 0x0200003C RID: 60
	[Flags]
	public enum RVOLayer
	{
		// Token: 0x04000211 RID: 529
		DefaultAgent = 1,
		// Token: 0x04000212 RID: 530
		DefaultObstacle = 2,
		// Token: 0x04000213 RID: 531
		Layer2 = 4,
		// Token: 0x04000214 RID: 532
		Layer3 = 8,
		// Token: 0x04000215 RID: 533
		Layer4 = 16,
		// Token: 0x04000216 RID: 534
		Layer5 = 32,
		// Token: 0x04000217 RID: 535
		Layer6 = 64,
		// Token: 0x04000218 RID: 536
		Layer7 = 128,
		// Token: 0x04000219 RID: 537
		Layer8 = 256,
		// Token: 0x0400021A RID: 538
		Layer9 = 512,
		// Token: 0x0400021B RID: 539
		Layer10 = 1024,
		// Token: 0x0400021C RID: 540
		Layer11 = 2048,
		// Token: 0x0400021D RID: 541
		Layer12 = 4096,
		// Token: 0x0400021E RID: 542
		Layer13 = 8192,
		// Token: 0x0400021F RID: 543
		Layer14 = 16384,
		// Token: 0x04000220 RID: 544
		Layer15 = 32768,
		// Token: 0x04000221 RID: 545
		Layer16 = 65536,
		// Token: 0x04000222 RID: 546
		Layer17 = 131072,
		// Token: 0x04000223 RID: 547
		Layer18 = 262144,
		// Token: 0x04000224 RID: 548
		Layer19 = 524288,
		// Token: 0x04000225 RID: 549
		Layer20 = 1048576,
		// Token: 0x04000226 RID: 550
		Layer21 = 2097152,
		// Token: 0x04000227 RID: 551
		Layer22 = 4194304,
		// Token: 0x04000228 RID: 552
		Layer23 = 8388608,
		// Token: 0x04000229 RID: 553
		Layer24 = 16777216,
		// Token: 0x0400022A RID: 554
		Layer25 = 33554432,
		// Token: 0x0400022B RID: 555
		Layer26 = 67108864,
		// Token: 0x0400022C RID: 556
		Layer27 = 134217728,
		// Token: 0x0400022D RID: 557
		Layer28 = 268435456,
		// Token: 0x0400022E RID: 558
		Layer29 = 536870912,
		// Token: 0x0400022F RID: 559
		Layer30 = 1073741824
	}
}
