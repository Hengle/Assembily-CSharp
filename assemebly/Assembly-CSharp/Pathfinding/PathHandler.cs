using System;
using System.Collections.Generic;
using System.Text;

namespace Pathfinding
{
	// Token: 0x02000037 RID: 55
	public class PathHandler
	{
		// Token: 0x06000277 RID: 631 RVA: 0x00014868 File Offset: 0x00012C68
		public PathHandler(int threadID, int totalThreadCount)
		{
			this.threadID = threadID;
			this.totalThreadCount = totalThreadCount;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000148D3 File Offset: 0x00012CD3
		public ushort PathID
		{
			get
			{
				return this.pathID;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000148DB File Offset: 0x00012CDB
		public void PushNode(PathNode node)
		{
			this.heap.Add(node);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000148E9 File Offset: 0x00012CE9
		public PathNode PopNode()
		{
			return this.heap.Remove();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000148F6 File Offset: 0x00012CF6
		public BinaryHeapM GetHeap()
		{
			return this.heap;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000148FE File Offset: 0x00012CFE
		public void RebuildHeap()
		{
			this.heap.Rebuild();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0001490B File Offset: 0x00012D0B
		public bool HeapEmpty()
		{
			return this.heap.numberOfItems <= 0;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0001491E File Offset: 0x00012D1E
		public void InitializeForPath(Path p)
		{
			this.pathID = p.pathID;
			this.heap.Clear();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00014938 File Offset: 0x00012D38
		public void DestroyNode(GraphNode node)
		{
			PathNode pathNode = this.GetPathNode(node);
			pathNode.node = null;
			pathNode.parent = null;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0001495C File Offset: 0x00012D5C
		public void InitializeNode(GraphNode node)
		{
			int nodeIndex = node.NodeIndex;
			int num = nodeIndex >> 10;
			int num2 = nodeIndex & 1023;
			if (num >= this.nodes.Length)
			{
				PathNode[][] array = new PathNode[Math.Max(Math.Max(this.nodes.Length * 3 / 2, num + 1), this.nodes.Length + 2)][];
				for (int i = 0; i < this.nodes.Length; i++)
				{
					array[i] = this.nodes[i];
				}
				bool[] array2 = new bool[array.Length];
				for (int j = 0; j < this.nodes.Length; j++)
				{
					array2[j] = this.bucketNew[j];
				}
				bool[] array3 = new bool[array.Length];
				for (int k = 0; k < this.nodes.Length; k++)
				{
					array3[k] = this.bucketCreated[k];
				}
				this.nodes = array;
				this.bucketNew = array2;
				this.bucketCreated = array3;
			}
			if (this.nodes[num] == null)
			{
				PathNode[] array4;
				if (this.bucketCache.Count > 0)
				{
					array4 = this.bucketCache.Pop();
				}
				else
				{
					array4 = new PathNode[1024];
					for (int l = 0; l < 1024; l++)
					{
						array4[l] = new PathNode();
					}
				}
				this.nodes[num] = array4;
				if (!this.bucketCreated[num])
				{
					this.bucketNew[num] = true;
					this.bucketCreated[num] = true;
				}
				this.filledBuckets++;
			}
			PathNode pathNode = this.nodes[num][num2];
			pathNode.node = node;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00014B0A File Offset: 0x00012F0A
		public PathNode GetPathNode(int nodeIndex)
		{
			return this.nodes[nodeIndex >> 10][nodeIndex & 1023];
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00014B20 File Offset: 0x00012F20
		public PathNode GetPathNode(GraphNode node)
		{
			int nodeIndex = node.NodeIndex;
			return this.nodes[nodeIndex >> 10][nodeIndex & 1023];
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00014B48 File Offset: 0x00012F48
		public void ClearPathIDs()
		{
			for (int i = 0; i < this.nodes.Length; i++)
			{
				PathNode[] array = this.nodes[i];
				if (this.nodes[i] != null)
				{
					for (int j = 0; j < 1024; j++)
					{
						array[j].pathID = 0;
					}
				}
			}
		}

		// Token: 0x040001C1 RID: 449
		private ushort pathID;

		// Token: 0x040001C2 RID: 450
		public readonly int threadID;

		// Token: 0x040001C3 RID: 451
		public readonly int totalThreadCount;

		// Token: 0x040001C4 RID: 452
		private BinaryHeapM heap = new BinaryHeapM(128);

		// Token: 0x040001C5 RID: 453
		private const int BucketSizeLog2 = 10;

		// Token: 0x040001C6 RID: 454
		private const int BucketSize = 1024;

		// Token: 0x040001C7 RID: 455
		private const int BucketIndexMask = 1023;

		// Token: 0x040001C8 RID: 456
		public PathNode[][] nodes = new PathNode[0][];

		// Token: 0x040001C9 RID: 457
		private bool[] bucketNew = new bool[0];

		// Token: 0x040001CA RID: 458
		private bool[] bucketCreated = new bool[0];

		// Token: 0x040001CB RID: 459
		private Stack<PathNode[]> bucketCache = new Stack<PathNode[]>();

		// Token: 0x040001CC RID: 460
		private int filledBuckets;

		// Token: 0x040001CD RID: 461
		public readonly StringBuilder DebugStringBuilder = new StringBuilder();
	}
}
