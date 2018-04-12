using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000E0 RID: 224
	public class FloodPathConstraint : NNConstraint
	{
		// Token: 0x0600076A RID: 1898 RVA: 0x000484A0 File Offset: 0x000468A0
		public FloodPathConstraint(FloodPath path)
		{
			if (path == null)
			{
				Debug.LogWarning("FloodPathConstraint should not be used with a NULL path");
			}
			this.path = path;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x000484BF File Offset: 0x000468BF
		public override bool Suitable(GraphNode node)
		{
			return base.Suitable(node) && this.path.HasPathTo(node);
		}

		// Token: 0x04000633 RID: 1587
		private FloodPath path;
	}
}
