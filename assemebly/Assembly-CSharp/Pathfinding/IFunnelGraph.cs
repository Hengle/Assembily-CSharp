using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000C7 RID: 199
	public interface IFunnelGraph
	{
		// Token: 0x060006B2 RID: 1714
		void BuildFunnelCorridor(List<GraphNode> path, int sIndex, int eIndex, List<Vector3> left, List<Vector3> right);

		// Token: 0x060006B3 RID: 1715
		void AddPortal(GraphNode n1, GraphNode n2, List<Vector3> left, List<Vector3> right);
	}
}
