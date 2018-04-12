using System;

namespace Pathfinding
{
	// Token: 0x020000E6 RID: 230
	public class ABPathEndingCondition : PathEndingCondition
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x00049B83 File Offset: 0x00047F83
		public ABPathEndingCondition(ABPath p)
		{
			if (p == null)
			{
				throw new ArgumentNullException("Please supply a non-null path");
			}
			this.abPath = p;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00049BA3 File Offset: 0x00047FA3
		public override bool TargetFound(PathNode node)
		{
			return node.node == this.abPath.endNode;
		}

		// Token: 0x04000655 RID: 1621
		protected ABPath abPath;
	}
}
