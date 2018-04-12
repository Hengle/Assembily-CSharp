using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200008C RID: 140
	public class LevelGridNode : GraphNode
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x00028156 File Offset: 0x00026556
		public LevelGridNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0002815F File Offset: 0x0002655F
		public static LayerGridGraph GetGridGraph(uint graphIndex)
		{
			return LevelGridNode._gridGraphs[(int)graphIndex];
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00028168 File Offset: 0x00026568
		public static void SetGridGraph(int graphIndex, LayerGridGraph graph)
		{
			if (LevelGridNode._gridGraphs.Length <= graphIndex)
			{
				LayerGridGraph[] array = new LayerGridGraph[graphIndex + 1];
				for (int i = 0; i < LevelGridNode._gridGraphs.Length; i++)
				{
					array[i] = LevelGridNode._gridGraphs[i];
				}
				LevelGridNode._gridGraphs = array;
			}
			LevelGridNode._gridGraphs[graphIndex] = graph;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000281BB File Offset: 0x000265BB
		public void ResetAllGridConnections()
		{
			this.gridConnections = uint.MaxValue;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000281C4 File Offset: 0x000265C4
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x000281D8 File Offset: 0x000265D8
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

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x000281FF File Offset: 0x000265FF
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x00028213 File Offset: 0x00026613
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

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0002823A File Offset: 0x0002663A
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x00028242 File Offset: 0x00026642
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

		// Token: 0x06000491 RID: 1169 RVA: 0x0002824B File Offset: 0x0002664B
		public bool HasAnyGridConnections()
		{
			return this.gridConnections != uint.MaxValue;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00028259 File Offset: 0x00026659
		public void SetPosition(Int3 position)
		{
			this.position = position;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00028264 File Offset: 0x00026664
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse)
			{
				LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
				int[] neighbourOffsets = gridGraph.neighbourOffsets;
				LevelGridNode[] nodes = gridGraph.nodes;
				for (int i = 0; i < 4; i++)
				{
					int connectionValue = this.GetConnectionValue(i);
					if (connectionValue != 255)
					{
						LevelGridNode levelGridNode = nodes[this.NodeInGridIndex + neighbourOffsets[i] + gridGraph.width * gridGraph.depth * connectionValue];
						if (levelGridNode != null)
						{
							levelGridNode.SetConnectionValue((i >= 4) ? 7 : ((i + 2) % 4), 255);
						}
					}
				}
			}
			this.ResetAllGridConnections();
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00028304 File Offset: 0x00026704
		public override void GetConnections(GraphNodeDelegate del)
		{
			int num = this.NodeInGridIndex;
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			LevelGridNode[] nodes = gridGraph.nodes;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					LevelGridNode levelGridNode = nodes[num + neighbourOffsets[i] + gridGraph.width * gridGraph.depth * connectionValue];
					if (levelGridNode != null)
					{
						del(levelGridNode);
					}
				}
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00028388 File Offset: 0x00026788
		public override void FloodFill(Stack<GraphNode> stack, uint region)
		{
			int num = this.NodeInGridIndex;
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			LevelGridNode[] nodes = gridGraph.nodes;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					LevelGridNode levelGridNode = nodes[num + neighbourOffsets[i] + gridGraph.width * gridGraph.depth * connectionValue];
					if (levelGridNode != null && levelGridNode.Area != region)
					{
						levelGridNode.Area = region;
						stack.Push(levelGridNode);
					}
				}
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00028421 File Offset: 0x00026821
		public override void AddConnection(GraphNode node, uint cost)
		{
			throw new NotImplementedException("Layered Grid Nodes do not have support for adding manual connections");
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0002842D File Offset: 0x0002682D
		public override void RemoveConnection(GraphNode node)
		{
			throw new NotImplementedException("Layered Grid Nodes do not have support for adding manual connections");
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00028439 File Offset: 0x00026839
		public bool GetConnection(int i)
		{
			return (this.gridConnections >> i * 8 & 255u) != 255u;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00028458 File Offset: 0x00026858
		public void SetConnectionValue(int dir, int value)
		{
			this.gridConnections = ((this.gridConnections & ~(255u << dir * 8)) | (uint)((uint)value << dir * 8));
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0002847D File Offset: 0x0002687D
		public int GetConnectionValue(int dir)
		{
			return (int)(this.gridConnections >> dir * 8 & 255u);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00028494 File Offset: 0x00026894
		public override bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			if (backwards)
			{
				return true;
			}
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			LevelGridNode[] nodes = gridGraph.nodes;
			int num = this.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255 && other == nodes[num + neighbourOffsets[i] + gridGraph.width * gridGraph.depth * connectionValue])
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
			return false;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000285A0 File Offset: 0x000269A0
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			handler.PushNode(pathNode);
			base.UpdateG(path, pathNode);
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			LevelGridNode[] nodes = gridGraph.nodes;
			int num = this.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					LevelGridNode levelGridNode = nodes[num + neighbourOffsets[i] + gridGraph.width * gridGraph.depth * connectionValue];
					PathNode pathNode2 = handler.GetPathNode(levelGridNode);
					if (pathNode2 != null && pathNode2.parent == pathNode && pathNode2.pathID == handler.PathID)
					{
						levelGridNode.UpdateRecursiveG(path, pathNode2, handler);
					}
				}
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00028660 File Offset: 0x00026A60
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			uint[] neighbourCosts = gridGraph.neighbourCosts;
			LevelGridNode[] nodes = gridGraph.nodes;
			int num = this.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					GraphNode graphNode = nodes[num + neighbourOffsets[i] + gridGraph.width * gridGraph.depth * connectionValue];
					if (path.CanTraverse(graphNode))
					{
						PathNode pathNode2 = handler.GetPathNode(graphNode);
						if (pathNode2.pathID != handler.PathID)
						{
							pathNode2.parent = pathNode;
							pathNode2.pathID = handler.PathID;
							pathNode2.cost = neighbourCosts[i];
							pathNode2.H = path.CalculateHScore(graphNode);
							graphNode.UpdateG(path, pathNode2);
							handler.PushNode(pathNode2);
						}
						else
						{
							uint num2 = neighbourCosts[i];
							if (pathNode.G + num2 + path.GetTraversalCost(graphNode) < pathNode2.G)
							{
								pathNode2.cost = num2;
								pathNode2.parent = pathNode;
								graphNode.UpdateRecursiveG(path, pathNode2, handler);
							}
							else if (pathNode2.G + num2 + path.GetTraversalCost(this) < pathNode.G)
							{
								pathNode.parent = pathNode2;
								pathNode.cost = num2;
								this.UpdateRecursiveG(path, pathNode, handler);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000287D0 File Offset: 0x00026BD0
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.writer.Write(this.position.x);
			ctx.writer.Write(this.position.y);
			ctx.writer.Write(this.position.z);
			ctx.writer.Write(this.gridFlags);
			ctx.writer.Write(this.gridConnections);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00028848 File Offset: 0x00026C48
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = new Int3(ctx.reader.ReadInt32(), ctx.reader.ReadInt32(), ctx.reader.ReadInt32());
			this.gridFlags = ctx.reader.ReadUInt16();
			this.gridConnections = ctx.reader.ReadUInt32();
		}

		// Token: 0x040003FB RID: 1019
		private const int GridFlagsWalkableErosionOffset = 8;

		// Token: 0x040003FC RID: 1020
		private const int GridFlagsWalkableErosionMask = 256;

		// Token: 0x040003FD RID: 1021
		private const int GridFlagsWalkableTmpOffset = 9;

		// Token: 0x040003FE RID: 1022
		private const int GridFlagsWalkableTmpMask = 512;

		// Token: 0x040003FF RID: 1023
		private static LayerGridGraph[] _gridGraphs = new LayerGridGraph[0];

		// Token: 0x04000400 RID: 1024
		protected ushort gridFlags;

		// Token: 0x04000401 RID: 1025
		protected int nodeInGridIndex;

		// Token: 0x04000402 RID: 1026
		protected uint gridConnections;

		// Token: 0x04000403 RID: 1027
		protected static LayerGridGraph[] gridGraphs;

		// Token: 0x04000404 RID: 1028
		public const int NoConnection = 255;

		// Token: 0x04000405 RID: 1029
		public const int ConnectionMask = 255;

		// Token: 0x04000406 RID: 1030
		private const int ConnectionStride = 8;

		// Token: 0x04000407 RID: 1031
		public const int MaxLayerCount = 255;
	}
}
