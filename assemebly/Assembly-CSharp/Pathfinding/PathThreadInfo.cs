using System;

namespace Pathfinding
{
	// Token: 0x0200005A RID: 90
	public struct PathThreadInfo
	{
		// Token: 0x06000379 RID: 889 RVA: 0x0001AEF5 File Offset: 0x000192F5
		public PathThreadInfo(int index, AstarPath astar, PathHandler runData)
		{
			this.threadIndex = index;
			this.astar = astar;
			this.runData = runData;
			this._lock = new object();
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0001AF17 File Offset: 0x00019317
		public object Lock
		{
			get
			{
				return this._lock;
			}
		}

		// Token: 0x040002BE RID: 702
		public int threadIndex;

		// Token: 0x040002BF RID: 703
		public AstarPath astar;

		// Token: 0x040002C0 RID: 704
		public PathHandler runData;

		// Token: 0x040002C1 RID: 705
		private object _lock;
	}
}
