using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000033 RID: 51
	public abstract class GraphNode
	{
		// Token: 0x0600020E RID: 526 RVA: 0x00011535 File Offset: 0x0000F935
		public GraphNode(AstarPath astar)
		{
			if (astar != null)
			{
				this.nodeIndex = astar.GetNewNodeIndex();
				astar.InitializeNode(this);
				return;
			}
			throw new Exception("No active AstarPath object to bind to");
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0001156C File Offset: 0x0000F96C
		[Obsolete("This attribute is deprecated. Please use .position (not a capital P)")]
		public Int3 Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00011574 File Offset: 0x0000F974
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0001157C File Offset: 0x0000F97C
		[Obsolete("This attribute is deprecated. Please use .Walkable (with a capital W)")]
		public bool walkable
		{
			get
			{
				return this.Walkable;
			}
			set
			{
				this.Walkable = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00011585 File Offset: 0x0000F985
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0001158D File Offset: 0x0000F98D
		[Obsolete("This attribute is deprecated. Please use .Tag (with a capital T)")]
		public uint tags
		{
			get
			{
				return this.Tag;
			}
			set
			{
				this.Tag = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00011596 File Offset: 0x0000F996
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0001159E File Offset: 0x0000F99E
		[Obsolete("This attribute is deprecated. Please use .GraphIndex (with a capital G)")]
		public uint graphIndex
		{
			get
			{
				return this.GraphIndex;
			}
			set
			{
				this.GraphIndex = value;
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000115A7 File Offset: 0x0000F9A7
		public void Destroy()
		{
			if (this.nodeIndex == -1)
			{
				return;
			}
			this.ClearConnections(true);
			if (AstarPath.active != null)
			{
				AstarPath.active.DestroyNode(this);
			}
			this.nodeIndex = -1;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000217 RID: 535 RVA: 0x000115DF File Offset: 0x0000F9DF
		public bool Destroyed
		{
			get
			{
				return this.nodeIndex == -1;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000115EA File Offset: 0x0000F9EA
		public int NodeIndex
		{
			get
			{
				return this.nodeIndex;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000115F2 File Offset: 0x0000F9F2
		// (set) Token: 0x0600021A RID: 538 RVA: 0x000115FA File Offset: 0x0000F9FA
		public uint Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00011603 File Offset: 0x0000FA03
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0001160B File Offset: 0x0000FA0B
		public uint Penalty
		{
			get
			{
				return this.penalty;
			}
			set
			{
				if (value > 16777215u)
				{
					Debug.LogWarning("Very high penalty applied. Are you sure negative values haven't underflowed?\nPenalty values this high could with long paths cause overflows and in some cases infinity loops because of that.\nPenalty value applied: " + value);
				}
				this.penalty = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00011634 File Offset: 0x0000FA34
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00011644 File Offset: 0x0000FA44
		public bool Walkable
		{
			get
			{
				return (this.flags & 1u) != 0u;
			}
			set
			{
				this.flags = ((this.flags & 4294967294u) | ((!value) ? 0u : 1u) << 0);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00011665 File Offset: 0x0000FA65
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00011675 File Offset: 0x0000FA75
		public uint Area
		{
			get
			{
				return (this.flags & 262142u) >> 1;
			}
			set
			{
				this.flags = ((this.flags & 4294705153u) | value << 1);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0001168D File Offset: 0x0000FA8D
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0001169E File Offset: 0x0000FA9E
		public uint GraphIndex
		{
			get
			{
				return (this.flags & 4278190080u) >> 24;
			}
			set
			{
				this.flags = ((this.flags & 16777215u) | value << 24);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000116B7 File Offset: 0x0000FAB7
		// (set) Token: 0x06000224 RID: 548 RVA: 0x000116C8 File Offset: 0x0000FAC8
		public uint Tag
		{
			get
			{
				return (this.flags & 16252928u) >> 19;
			}
			set
			{
				this.flags = ((this.flags & 4278714367u) | value << 19);
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000116E1 File Offset: 0x0000FAE1
		public void UpdateG(Path path, PathNode pathNode)
		{
			pathNode.G = pathNode.parent.G + pathNode.cost + path.GetTraversalCost(this);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00011704 File Offset: 0x0000FB04
		public virtual void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			this.UpdateG(path, pathNode);
			handler.PushNode(pathNode);
			this.GetConnections(delegate(GraphNode other)
			{
				PathNode pathNode2 = handler.GetPathNode(other);
				if (pathNode2.parent == pathNode && pathNode2.pathID == handler.PathID)
				{
					other.UpdateRecursiveG(path, pathNode2, handler);
				}
			});
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00011764 File Offset: 0x0000FB64
		public virtual void FloodFill(Stack<GraphNode> stack, uint region)
		{
			this.GetConnections(delegate(GraphNode other)
			{
				if (other.Area != region)
				{
					other.Area = region;
					stack.Push(other);
				}
			});
		}

		// Token: 0x06000228 RID: 552
		public abstract void GetConnections(GraphNodeDelegate del);

		// Token: 0x06000229 RID: 553
		public abstract void AddConnection(GraphNode node, uint cost);

		// Token: 0x0600022A RID: 554
		public abstract void RemoveConnection(GraphNode node);

		// Token: 0x0600022B RID: 555
		public abstract void ClearConnections(bool alsoReverse);

		// Token: 0x0600022C RID: 556 RVA: 0x00011798 File Offset: 0x0000FB98
		public virtual bool ContainsConnection(GraphNode node)
		{
			bool contains = false;
			this.GetConnections(delegate(GraphNode n)
			{
				if (n == node)
				{
					contains = true;
				}
			});
			return contains;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000117D1 File Offset: 0x0000FBD1
		public virtual void RecalculateConnectionCosts()
		{
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000117D3 File Offset: 0x0000FBD3
		public virtual bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			return false;
		}

		// Token: 0x0600022F RID: 559
		public abstract void Open(Path path, PathNode pathNode, PathHandler handler);

		// Token: 0x06000230 RID: 560 RVA: 0x000117D6 File Offset: 0x0000FBD6
		public virtual void SerializeNode(GraphSerializationContext ctx)
		{
			ctx.writer.Write(this.Penalty);
			ctx.writer.Write(this.Flags);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000117FA File Offset: 0x0000FBFA
		public virtual void DeserializeNode(GraphSerializationContext ctx)
		{
			this.Penalty = ctx.reader.ReadUInt32();
			this.Flags = ctx.reader.ReadUInt32();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0001181E File Offset: 0x0000FC1E
		public virtual void SerializeReferences(GraphSerializationContext ctx)
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00011820 File Offset: 0x0000FC20
		public virtual void DeserializeReferences(GraphSerializationContext ctx)
		{
		}

		// Token: 0x04000181 RID: 385
		private int nodeIndex;

		// Token: 0x04000182 RID: 386
		protected uint flags;

		// Token: 0x04000183 RID: 387
		private uint penalty;

		// Token: 0x04000184 RID: 388
		public Int3 position;

		// Token: 0x04000185 RID: 389
		private const int FlagsWalkableOffset = 0;

		// Token: 0x04000186 RID: 390
		private const uint FlagsWalkableMask = 1u;

		// Token: 0x04000187 RID: 391
		private const int FlagsAreaOffset = 1;

		// Token: 0x04000188 RID: 392
		private const uint FlagsAreaMask = 262142u;

		// Token: 0x04000189 RID: 393
		private const int FlagsGraphOffset = 24;

		// Token: 0x0400018A RID: 394
		private const uint FlagsGraphMask = 4278190080u;

		// Token: 0x0400018B RID: 395
		public const uint MaxAreaIndex = 131071u;

		// Token: 0x0400018C RID: 396
		public const uint MaxGraphIndex = 255u;

		// Token: 0x0400018D RID: 397
		private const int FlagsTagOffset = 19;

		// Token: 0x0400018E RID: 398
		private const uint FlagsTagMask = 16252928u;
	}
}
