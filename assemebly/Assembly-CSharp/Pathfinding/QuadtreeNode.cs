using System;

namespace Pathfinding
{
	// Token: 0x02000097 RID: 151
	public class QuadtreeNode : GraphNode
	{
		// Token: 0x06000541 RID: 1345 RVA: 0x0002E420 File Offset: 0x0002C820
		public QuadtreeNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0002E429 File Offset: 0x0002C829
		public void SetPosition(Int3 value)
		{
			this.position = value;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0002E434 File Offset: 0x0002C834
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

		// Token: 0x06000544 RID: 1348 RVA: 0x0002E474 File Offset: 0x0002C874
		public override void AddConnection(GraphNode node, uint cost)
		{
			throw new NotImplementedException("QuadTree Nodes do not have support for adding manual connections");
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0002E480 File Offset: 0x0002C880
		public override void RemoveConnection(GraphNode node)
		{
			throw new NotImplementedException("QuadTree Nodes do not have support for adding manual connections");
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0002E48C File Offset: 0x0002C88C
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					this.connections[i].RemoveConnection(this);
				}
			}
			this.connections = null;
			this.connectionCosts = null;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0002E4D4 File Offset: 0x0002C8D4
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
						pathNode2.node = graphNode;
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

		// Token: 0x04000448 RID: 1096
		public GraphNode[] connections;

		// Token: 0x04000449 RID: 1097
		public uint[] connectionCosts;
	}
}
