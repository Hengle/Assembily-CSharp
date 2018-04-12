using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000059 RID: 89
	public interface IRaycastableGraph
	{
		// Token: 0x06000375 RID: 885
		bool Linecast(Vector3 start, Vector3 end);

		// Token: 0x06000376 RID: 886
		bool Linecast(Vector3 start, Vector3 end, GraphNode hint);

		// Token: 0x06000377 RID: 887
		bool Linecast(Vector3 start, Vector3 end, GraphNode hint, out GraphHitInfo hit);

		// Token: 0x06000378 RID: 888
		bool Linecast(Vector3 start, Vector3 end, GraphNode hint, out GraphHitInfo hit, List<GraphNode> trace);
	}
}
