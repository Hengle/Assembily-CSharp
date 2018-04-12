using System;

namespace Pathfinding
{
	// Token: 0x020000DD RID: 221
	public class EndingConditionDistance : PathEndingCondition
	{
		// Token: 0x06000755 RID: 1877 RVA: 0x00047ADE File Offset: 0x00045EDE
		public EndingConditionDistance(Path p, int maxGScore) : base(p)
		{
			this.maxGScore = maxGScore;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00047AF6 File Offset: 0x00045EF6
		public override bool TargetFound(PathNode node)
		{
			return (ulong)node.G >= (ulong)((long)this.maxGScore);
		}

		// Token: 0x0400062D RID: 1581
		public int maxGScore = 100;
	}
}
