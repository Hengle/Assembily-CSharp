using System;
using System.Collections.Generic;
using Pathfinding.Util;

namespace Pathfinding
{
	// Token: 0x020000F2 RID: 242
	public static class GraphUpdateUtilities
	{
		// Token: 0x060007E2 RID: 2018 RVA: 0x0004BAA0 File Offset: 0x00049EA0
		[Obsolete("This function has been moved to Pathfinding.Util.PathUtilities. Please use the version in that class")]
		public static bool IsPathPossible(GraphNode n1, GraphNode n2)
		{
			return n1.Walkable && n2.Walkable && n1.Area == n2.Area;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0004BACC File Offset: 0x00049ECC
		[Obsolete("This function has been moved to Pathfinding.Util.PathUtilities. Please use the version in that class")]
		public static bool IsPathPossible(List<GraphNode> nodes)
		{
			uint area = nodes[0].Area;
			for (int i = 0; i < nodes.Count; i++)
			{
				if (!nodes[i].Walkable || nodes[i].Area != area)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0004BB24 File Offset: 0x00049F24
		public static bool UpdateGraphsNoBlock(GraphUpdateObject guo, GraphNode node1, GraphNode node2, bool alwaysRevert = false)
		{
			List<GraphNode> list = ListPool<GraphNode>.Claim();
			list.Add(node1);
			list.Add(node2);
			bool result = GraphUpdateUtilities.UpdateGraphsNoBlock(guo, list, alwaysRevert);
			ListPool<GraphNode>.Release(list);
			return result;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0004BB58 File Offset: 0x00049F58
		public static bool UpdateGraphsNoBlock(GraphUpdateObject guo, List<GraphNode> nodes, bool alwaysRevert = false)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				if (!nodes[i].Walkable)
				{
					return false;
				}
			}
			guo.trackChangedNodes = true;
			bool worked = true;
			AstarPath.RegisterSafeUpdate(delegate
			{
				AstarPath.active.UpdateGraphs(guo);
				AstarPath.active.QueueGraphUpdates();
				AstarPath.active.FlushGraphUpdates();
				worked = (worked && PathUtilities.IsPathPossible(nodes));
				if (!worked || alwaysRevert)
				{
					guo.RevertFromBackup();
					AstarPath.active.FloodFill();
				}
			});
			AstarPath.active.FlushThreadSafeCallbacks();
			guo.trackChangedNodes = false;
			return worked;
		}
	}
}
