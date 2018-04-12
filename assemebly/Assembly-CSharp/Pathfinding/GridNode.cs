using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000090 RID: 144
	public class GridNode : GraphNode
	{
		// Token: 0x060004D1 RID: 1233 RVA: 0x0002A9F1 File Offset: 0x00028DF1
		public GridNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0002A9FA File Offset: 0x00028DFA
		public static GridGraph GetGridGraph(uint graphIndex)
		{
			return GridNode._gridGraphs[(int)graphIndex];
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0002AA04 File Offset: 0x00028E04
		public static void SetGridGraph(int graphIndex, GridGraph graph)
		{
			if (GridNode._gridGraphs.Length <= graphIndex)
			{
				GridGraph[] array = new GridGraph[graphIndex + 1];
				for (int i = 0; i < GridNode._gridGraphs.Length; i++)
				{
					array[i] = GridNode._gridGraphs[i];
				}
				GridNode._gridGraphs = array;
			}
			GridNode._gridGraphs[graphIndex] = graph;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0002AA57 File Offset: 0x00028E57
		[Obsolete("This method has been deprecated. Please use NodeInGridIndex instead.", true)]
		public int GetIndex()
		{
			return 0;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0002AA5A File Offset: 0x00028E5A
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x0002AA62 File Offset: 0x00028E62
		internal ushort InternalGridFlags
		{
			get
			{
				return this.gridFlags;
			}
			set
			{
				this.gridFlags = value;
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0002AA6B File Offset: 0x00028E6B
		public bool GetConnectionInternal(int dir)
		{
			return (this.gridFlags >> dir & 1) != 0;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0002AA80 File Offset: 0x00028E80
		public void SetConnectionInternal(int dir, bool value)
		{
			this.gridFlags = (ushort)(((int)this.gridFlags & ~(1 << dir)) | ((!value) ? 0 : 1) << 0 << (dir & 31));
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0002AAAC File Offset: 0x00028EAC
		public void ResetConnectionsInternal()
		{
			this.gridFlags = (ushort)((int)this.gridFlags & -256);
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0002AAC1 File Offset: 0x00028EC1
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x0002AAD5 File Offset: 0x00028ED5
		public bool EdgeNode
		{
			get
			{
				return (this.gridFlags & 1024) != 0;
			}
			set
			{
				this.gridFlags = (ushort)(((int)this.gridFlags & -1025) | ((!value) ? 0 : 1024));
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0002AAFC File Offset: 0x00028EFC
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x0002AB10 File Offset: 0x00028F10
		public bool WalkableErosion
		{
			get
			{
				return (this.gridFlags & 256) != 0;
			}
			set
			{
				this.gridFlags = (ushort)(((int)this.gridFlags & -257) | ((!value) ? 0 : 256));
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0002AB37 File Offset: 0x00028F37
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x0002AB4B File Offset: 0x00028F4B
		public bool TmpWalkable
		{
			get
			{
				return (this.gridFlags & 512) != 0;
			}
			set
			{
				this.gridFlags = (ushort)(((int)this.gridFlags & -513) | ((!value) ? 0 : 512));
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0002AB72 File Offset: 0x00028F72
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x0002AB7A File Offset: 0x00028F7A
		public int NodeInGridIndex
		{
			get
			{
				return this.nodeInGridIndex;
			}
			set
			{
				this.nodeInGridIndex = value;
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0002AB84 File Offset: 0x00028F84
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse)
			{
				GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
				for (int i = 0; i < 8; i++)
				{
					GridNode nodeConnection = gridGraph.GetNodeConnection(this, i);
					if (nodeConnection != null)
					{
						nodeConnection.SetConnectionInternal((i >= 4) ? 7 : ((i + 2) % 4), false);
					}
				}
			}
			this.ResetConnectionsInternal();
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0002ABE4 File Offset: 0x00028FE4
		public override void GetConnections(GraphNodeDelegate del)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNode[] nodes = gridGraph.nodes;
			for (int i = 0; i < 8; i++)
			{
				if (this.GetConnectionInternal(i))
				{
					GridNode gridNode = nodes[this.nodeInGridIndex + neighbourOffsets[i]];
					if (gridNode != null)
					{
						del(gridNode);
					}
				}
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0002AC48 File Offset: 0x00029048
		public override bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			if (backwards)
			{
				return true;
			}
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNode[] nodes = gridGraph.nodes;
			for (int i = 0; i < 4; i++)
			{
				if (this.GetConnectionInternal(i) && other == nodes[this.nodeInGridIndex + neighbourOffsets[i]])
				{
					Vector3 a = (Vector3)(this.position + other.position) * 0.5f;
					Vector3 vector = Vector3.Cross(gridGraph.collision.up, (Vector3)(other.position - this.position));
					vector.Normalize();
					vector *= gridGraph.nodeSize * 0.5f;
					left.Add(a - vector);
					right.Add(a + vector);
					return true;
				}
			}
			for (int j = 4; j < 8; j++)
			{
				if (this.GetConnectionInternal(j) && other == nodes[this.nodeInGridIndex + neighbourOffsets[j]])
				{
					bool flag = false;
					bool flag2 = false;
					if (this.GetConnectionInternal(j - 4))
					{
						GridNode gridNode = nodes[this.nodeInGridIndex + neighbourOffsets[j - 4]];
						if (gridNode.Walkable && gridNode.GetConnectionInternal((j - 4 + 1) % 4))
						{
							flag = true;
						}
					}
					if (this.GetConnectionInternal((j - 4 + 1) % 4))
					{
						GridNode gridNode2 = nodes[this.nodeInGridIndex + neighbourOffsets[(j - 4 + 1) % 4]];
						if (gridNode2.Walkable && gridNode2.GetConnectionInternal(j - 4))
						{
							flag2 = true;
						}
					}
					Vector3 a2 = (Vector3)(this.position + other.position) * 0.5f;
					Vector3 vector2 = Vector3.Cross(gridGraph.collision.up, (Vector3)(other.position - this.position));
					vector2.Normalize();
					vector2 *= gridGraph.nodeSize * 1.4142f;
					left.Add(a2 - ((!flag2) ? Vector3.zero : vector2));
					right.Add(a2 + ((!flag) ? Vector3.zero : vector2));
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0002AEA0 File Offset: 0x000292A0
		public override void FloodFill(Stack<GraphNode> stack, uint region)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNode[] nodes = gridGraph.nodes;
			for (int i = 0; i < 8; i++)
			{
				if (this.GetConnectionInternal(i))
				{
					GridNode gridNode = nodes[this.nodeInGridIndex + neighbourOffsets[i]];
					if (gridNode != null && gridNode.Area != region)
					{
						gridNode.Area = region;
						stack.Push(gridNode);
					}
				}
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0002AF17 File Offset: 0x00029317
		public override void AddConnection(GraphNode node, uint cost)
		{
			throw new NotImplementedException("GridNodes do not have support for adding manual connections");
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0002AF23 File Offset: 0x00029323
		public override void RemoveConnection(GraphNode node)
		{
			throw new NotImplementedException("GridNodes do not have support for adding manual connections");
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0002AF30 File Offset: 0x00029330
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNode[] nodes = gridGraph.nodes;
			base.UpdateG(path, pathNode);
			handler.PushNode(pathNode);
			ushort pathID = handler.PathID;
			for (int i = 0; i < 8; i++)
			{
				if (this.GetConnectionInternal(i))
				{
					GridNode gridNode = nodes[this.nodeInGridIndex + neighbourOffsets[i]];
					PathNode pathNode2 = handler.GetPathNode(gridNode);
					if (pathNode2.parent == pathNode && pathNode2.pathID == pathID)
					{
						gridNode.UpdateRecursiveG(path, pathNode2, handler);
					}
				}
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0002AFD0 File Offset: 0x000293D0
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			ushort pathID = handler.PathID;
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			uint[] neighbourCosts = gridGraph.neighbourCosts;
			GridNode[] nodes = gridGraph.nodes;
			for (int i = 0; i < 8; i++)
			{
				if (this.GetConnectionInternal(i))
				{
					GridNode gridNode = nodes[this.nodeInGridIndex + neighbourOffsets[i]];
					if (path.CanTraverse(gridNode))
					{
						PathNode pathNode2 = handler.GetPathNode(gridNode);
						uint num = neighbourCosts[i];
						if (pathNode2.pathID != pathID)
						{
							pathNode2.parent = pathNode;
							pathNode2.pathID = pathID;
							pathNode2.cost = num;
							pathNode2.H = path.CalculateHScore(gridNode);
							gridNode.UpdateG(path, pathNode2);
							handler.PushNode(pathNode2);
						}
						else if (pathNode.G + num + path.GetTraversalCost(gridNode) < pathNode2.G)
						{
							pathNode2.cost = num;
							pathNode2.parent = pathNode;
							gridNode.UpdateRecursiveG(path, pathNode2, handler);
						}
						else if (pathNode2.G + num + path.GetTraversalCost(this) < pathNode.G)
						{
							pathNode.parent = pathNode2;
							pathNode.cost = num;
							this.UpdateRecursiveG(path, pathNode, handler);
						}
					}
				}
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0002B11C File Offset: 0x0002951C
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.writer.Write(this.position.x);
			ctx.writer.Write(this.position.y);
			ctx.writer.Write(this.position.z);
			ctx.writer.Write(this.gridFlags);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0002B184 File Offset: 0x00029584
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = new Int3(ctx.reader.ReadInt32(), ctx.reader.ReadInt32(), ctx.reader.ReadInt32());
			this.gridFlags = ctx.reader.ReadUInt16();
		}

		// Token: 0x04000414 RID: 1044
		private static GridGraph[] _gridGraphs = new GridGraph[0];

		// Token: 0x04000415 RID: 1045
		protected int nodeInGridIndex;

		// Token: 0x04000416 RID: 1046
		protected ushort gridFlags;

		// Token: 0x04000417 RID: 1047
		private const int GridFlagsConnectionOffset = 0;

		// Token: 0x04000418 RID: 1048
		private const int GridFlagsConnectionBit0 = 1;

		// Token: 0x04000419 RID: 1049
		private const int GridFlagsConnectionMask = 255;

		// Token: 0x0400041A RID: 1050
		private const int GridFlagsWalkableErosionOffset = 8;

		// Token: 0x0400041B RID: 1051
		private const int GridFlagsWalkableErosionMask = 256;

		// Token: 0x0400041C RID: 1052
		private const int GridFlagsWalkableTmpOffset = 9;

		// Token: 0x0400041D RID: 1053
		private const int GridFlagsWalkableTmpMask = 512;

		// Token: 0x0400041E RID: 1054
		private const int GridFlagsEdgeNodeOffset = 10;

		// Token: 0x0400041F RID: 1055
		private const int GridFlagsEdgeNodeMask = 1024;
	}
}
