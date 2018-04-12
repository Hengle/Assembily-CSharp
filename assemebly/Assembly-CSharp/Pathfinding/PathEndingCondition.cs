using System;

namespace Pathfinding
{
	// Token: 0x020000E5 RID: 229
	public class PathEndingCondition
	{
		// Token: 0x06000791 RID: 1937 RVA: 0x00047AB3 File Offset: 0x00045EB3
		protected PathEndingCondition()
		{
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00047ABB File Offset: 0x00045EBB
		public PathEndingCondition(Path p)
		{
			if (p == null)
			{
				throw new ArgumentNullException("Please supply a non-null path");
			}
			this.p = p;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00047ADB File Offset: 0x00045EDB
		public virtual bool TargetFound(PathNode node)
		{
			return true;
		}

		// Token: 0x04000654 RID: 1620
		protected Path p;
	}
}
