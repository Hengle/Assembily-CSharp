using System;

namespace Pathfinding
{
	// Token: 0x02000056 RID: 86
	public interface IUpdatableGraph
	{
		// Token: 0x06000369 RID: 873
		void UpdateArea(GraphUpdateObject o);

		// Token: 0x0600036A RID: 874
		void UpdateAreaInit(GraphUpdateObject o);

		// Token: 0x0600036B RID: 875
		GraphUpdateThreading CanUpdateAsync(GraphUpdateObject o);
	}
}
