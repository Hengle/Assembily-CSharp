using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000094 RID: 148
	public class ConvexMeshNode : MeshNode
	{
		// Token: 0x06000510 RID: 1296 RVA: 0x0002BE88 File Offset: 0x0002A288
		public ConvexMeshNode(AstarPath astar) : base(astar)
		{
			this.indices = new int[0];
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0002BEAA File Offset: 0x0002A2AA
		protected static INavmeshHolder GetNavmeshHolder(uint graphIndex)
		{
			return ConvexMeshNode.navmeshHolders[(int)graphIndex];
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0002BEB3 File Offset: 0x0002A2B3
		public void SetPosition(Int3 p)
		{
			this.position = p;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0002BEBC File Offset: 0x0002A2BC
		public int GetVertexIndex(int i)
		{
			return this.indices[i];
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0002BEC6 File Offset: 0x0002A2C6
		public override Int3 GetVertex(int i)
		{
			return ConvexMeshNode.GetNavmeshHolder(base.GraphIndex).GetVertex(this.GetVertexIndex(i));
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0002BEDF File Offset: 0x0002A2DF
		public override int GetVertexCount()
		{
			return this.indices.Length;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0002BEE9 File Offset: 0x0002A2E9
		public override Vector3 ClosestPointOnNode(Vector3 p)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0002BEF0 File Offset: 0x0002A2F0
		public override Vector3 ClosestPointOnNodeXZ(Vector3 p)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0002BEF8 File Offset: 0x0002A2F8
		public override void GetConnections(GraphNodeDelegate del)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				del(this.connections[i]);
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0002BF38 File Offset: 0x0002A338
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				GraphNode graphNode = this.connections[i];
				if (path.CanTraverse(graphNode))
				{
					PathNode pathNode2 = handler.GetPathNode(graphNode);
					if (pathNode2.pathID != handler.PathID)
					{
						pathNode2.parent = pathNode;
						pathNode2.pathID = handler.PathID;
						pathNode2.cost = this.connectionCosts[i];
						pathNode2.H = path.CalculateHScore(graphNode);
						graphNode.UpdateG(path, pathNode2);
						handler.PushNode(pathNode2);
					}
					else
					{
						uint num = this.connectionCosts[i];
						if (pathNode.G + num + path.GetTraversalCost(graphNode) < pathNode2.G)
						{
							pathNode2.cost = num;
							pathNode2.parent = pathNode;
							graphNode.UpdateRecursiveG(path, pathNode2, handler);
						}
						else if (pathNode2.G + num + path.GetTraversalCost(this) < pathNode.G && graphNode.ContainsConnection(this))
						{
							pathNode.parent = pathNode2;
							pathNode.cost = num;
							this.UpdateRecursiveG(path, pathNode, handler);
						}
					}
				}
			}
		}

		// Token: 0x04000428 RID: 1064
		private int[] indices;

		// Token: 0x04000429 RID: 1065
		protected static INavmeshHolder[] navmeshHolders = new INavmeshHolder[0];
	}
}
