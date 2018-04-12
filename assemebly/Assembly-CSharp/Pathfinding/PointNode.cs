﻿using System;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000091 RID: 145
	public class PointNode : GraphNode
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x000118D0 File Offset: 0x0000FCD0
		public PointNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000118D9 File Offset: 0x0000FCD9
		public void SetPosition(Int3 value)
		{
			this.position = value;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000118E4 File Offset: 0x0000FCE4
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

		// Token: 0x060004F0 RID: 1264 RVA: 0x00011924 File Offset: 0x0000FD24
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse && this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					this.connections[i].RemoveConnection(this);
				}
			}
			this.connections = null;
			this.connectionCosts = null;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00011978 File Offset: 0x0000FD78
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			base.UpdateG(path, pathNode);
			handler.PushNode(pathNode);
			for (int i = 0; i < this.connections.Length; i++)
			{
				GraphNode graphNode = this.connections[i];
				PathNode pathNode2 = handler.GetPathNode(graphNode);
				if (pathNode2.parent == pathNode && pathNode2.pathID == handler.PathID)
				{
					graphNode.UpdateRecursiveG(path, pathNode2, handler);
				}
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000119E4 File Offset: 0x0000FDE4
		public override bool ContainsConnection(GraphNode node)
		{
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i] == node)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00011A1C File Offset: 0x0000FE1C
		public override void AddConnection(GraphNode node, uint cost)
		{
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i] == node)
					{
						this.connectionCosts[i] = cost;
						return;
					}
				}
			}
			int num = (this.connections == null) ? 0 : this.connections.Length;
			GraphNode[] array = new GraphNode[num + 1];
			uint[] array2 = new uint[num + 1];
			for (int j = 0; j < num; j++)
			{
				array[j] = this.connections[j];
				array2[j] = this.connectionCosts[j];
			}
			array[num] = node;
			array2[num] = cost;
			this.connections = array;
			this.connectionCosts = array2;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00011AD8 File Offset: 0x0000FED8
		public override void RemoveConnection(GraphNode node)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i] == node)
				{
					int num = this.connections.Length;
					GraphNode[] array = new GraphNode[num - 1];
					uint[] array2 = new uint[num - 1];
					for (int j = 0; j < i; j++)
					{
						array[j] = this.connections[j];
						array2[j] = this.connectionCosts[j];
					}
					for (int k = i + 1; k < num; k++)
					{
						array[k - 1] = this.connections[k];
						array2[k - 1] = this.connectionCosts[k];
					}
					this.connections = array;
					this.connectionCosts = array2;
					return;
				}
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00011BA8 File Offset: 0x0000FFA8
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

		// Token: 0x060004F6 RID: 1270 RVA: 0x00011CC8 File Offset: 0x000100C8
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.writer.Write(this.position.x);
			ctx.writer.Write(this.position.y);
			ctx.writer.Write(this.position.z);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00011D1E File Offset: 0x0001011E
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = new Int3(ctx.reader.ReadInt32(), ctx.reader.ReadInt32(), ctx.reader.ReadInt32());
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00011D54 File Offset: 0x00010154
		public override void SerializeReferences(GraphSerializationContext ctx)
		{
			if (this.connections == null)
			{
				ctx.writer.Write(-1);
			}
			else
			{
				ctx.writer.Write(this.connections.Length);
				for (int i = 0; i < this.connections.Length; i++)
				{
					ctx.writer.Write(ctx.GetNodeIdentifier(this.connections[i]));
					ctx.writer.Write(this.connectionCosts[i]);
				}
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00011DD8 File Offset: 0x000101D8
		public override void DeserializeReferences(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			if (num == -1)
			{
				this.connections = null;
				this.connectionCosts = null;
			}
			else
			{
				this.connections = new GraphNode[num];
				this.connectionCosts = new uint[num];
				for (int i = 0; i < num; i++)
				{
					this.connections[i] = ctx.GetNodeFromIdentifier(ctx.reader.ReadInt32());
					this.connectionCosts[i] = ctx.reader.ReadUInt32();
				}
			}
		}

		// Token: 0x04000420 RID: 1056
		public GraphNode[] connections;

		// Token: 0x04000421 RID: 1057
		public uint[] connectionCosts;

		// Token: 0x04000422 RID: 1058
		public GameObject gameObject;

		// Token: 0x04000423 RID: 1059
		public PointNode next;
	}
}
