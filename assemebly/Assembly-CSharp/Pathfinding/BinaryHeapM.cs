using System;

namespace Pathfinding
{
	// Token: 0x0200001D RID: 29
	public class BinaryHeapM
	{
		// Token: 0x06000154 RID: 340 RVA: 0x0000FD9B File Offset: 0x0000E19B
		public BinaryHeapM(int numberOfElements)
		{
			this.binaryHeap = new BinaryHeapM.Tuple[numberOfElements];
			this.numberOfItems = 0;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000FDC1 File Offset: 0x0000E1C1
		public void Clear()
		{
			this.numberOfItems = 0;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000FDCA File Offset: 0x0000E1CA
		internal PathNode GetNode(int i)
		{
			return this.binaryHeap[i].node;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000FDDD File Offset: 0x0000E1DD
		internal void SetF(int i, uint F)
		{
			this.binaryHeap[i].F = F;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000FDF4 File Offset: 0x0000E1F4
		public void Add(PathNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("Sending null node to BinaryHeap");
			}
			if (this.numberOfItems == this.binaryHeap.Length)
			{
				int num = Math.Max(this.binaryHeap.Length + 4, (int)Math.Round((double)((float)this.binaryHeap.Length * this.growthFactor)));
				if (num > 262144)
				{
					throw new Exception("Binary Heap Size really large (2^18). A heap size this large is probably the cause of pathfinding running in an infinite loop. \nRemove this check (in BinaryHeap.cs) if you are sure that it is not caused by a bug");
				}
				BinaryHeapM.Tuple[] array = new BinaryHeapM.Tuple[num];
				for (int i = 0; i < this.binaryHeap.Length; i++)
				{
					array[i] = this.binaryHeap[i];
				}
				this.binaryHeap = array;
			}
			BinaryHeapM.Tuple tuple = new BinaryHeapM.Tuple(node.F, node);
			this.binaryHeap[this.numberOfItems] = tuple;
			int num2 = this.numberOfItems;
			uint f = node.F;
			uint g = node.G;
			while (num2 != 0)
			{
				int num3 = (num2 - 1) / 4;
				if (f >= this.binaryHeap[num3].F && (f != this.binaryHeap[num3].F || g <= this.binaryHeap[num3].node.G))
				{
					break;
				}
				this.binaryHeap[num2] = this.binaryHeap[num3];
				this.binaryHeap[num3] = tuple;
				num2 = num3;
			}
			this.numberOfItems++;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000FF9C File Offset: 0x0000E39C
		public PathNode Remove()
		{
			this.numberOfItems--;
			PathNode node = this.binaryHeap[0].node;
			this.binaryHeap[0] = this.binaryHeap[this.numberOfItems];
			int num = 0;
			for (;;)
			{
				int num2 = num;
				uint f = this.binaryHeap[num].F;
				int num3 = num2 * 4 + 1;
				if (num3 <= this.numberOfItems && (this.binaryHeap[num3].F < f || (this.binaryHeap[num3].F == f && this.binaryHeap[num3].node.G < this.binaryHeap[num].node.G)))
				{
					f = this.binaryHeap[num3].F;
					num = num3;
				}
				if (num3 + 1 <= this.numberOfItems && (this.binaryHeap[num3 + 1].F < f || (this.binaryHeap[num3 + 1].F == f && this.binaryHeap[num3 + 1].node.G < this.binaryHeap[num].node.G)))
				{
					f = this.binaryHeap[num3 + 1].F;
					num = num3 + 1;
				}
				if (num3 + 2 <= this.numberOfItems && (this.binaryHeap[num3 + 2].F < f || (this.binaryHeap[num3 + 2].F == f && this.binaryHeap[num3 + 2].node.G < this.binaryHeap[num].node.G)))
				{
					f = this.binaryHeap[num3 + 2].F;
					num = num3 + 2;
				}
				if (num3 + 3 <= this.numberOfItems && (this.binaryHeap[num3 + 3].F < f || (this.binaryHeap[num3 + 3].F == f && this.binaryHeap[num3 + 3].node.G < this.binaryHeap[num].node.G)))
				{
					f = this.binaryHeap[num3 + 3].F;
					num = num3 + 3;
				}
				if (num2 == num)
				{
					break;
				}
				BinaryHeapM.Tuple tuple = this.binaryHeap[num2];
				this.binaryHeap[num2] = this.binaryHeap[num];
				this.binaryHeap[num] = tuple;
			}
			return node;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000102A8 File Offset: 0x0000E6A8
		private void Validate()
		{
			for (int i = 1; i < this.numberOfItems; i++)
			{
				int num = (i - 1) / 4;
				if (this.binaryHeap[num].F > this.binaryHeap[i].F)
				{
					throw new Exception(string.Concat(new object[]
					{
						"Invalid state at ",
						i,
						":",
						num,
						" ( ",
						this.binaryHeap[num].F,
						" > ",
						this.binaryHeap[i].F,
						" ) "
					}));
				}
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00010378 File Offset: 0x0000E778
		public void Rebuild()
		{
			for (int i = 2; i < this.numberOfItems; i++)
			{
				int num = i;
				BinaryHeapM.Tuple tuple = this.binaryHeap[i];
				uint f = tuple.F;
				while (num != 1)
				{
					int num2 = num / 4;
					if (f >= this.binaryHeap[num2].F)
					{
						break;
					}
					this.binaryHeap[num] = this.binaryHeap[num2];
					this.binaryHeap[num2] = tuple;
					num = num2;
				}
			}
		}

		// Token: 0x04000135 RID: 309
		public int numberOfItems;

		// Token: 0x04000136 RID: 310
		public float growthFactor = 2f;

		// Token: 0x04000137 RID: 311
		public const int D = 4;

		// Token: 0x04000138 RID: 312
		private const bool SortGScores = true;

		// Token: 0x04000139 RID: 313
		private BinaryHeapM.Tuple[] binaryHeap;

		// Token: 0x0200001E RID: 30
		private struct Tuple
		{
			// Token: 0x0600015C RID: 348 RVA: 0x00010424 File Offset: 0x0000E824
			public Tuple(uint F, PathNode node)
			{
				this.F = F;
				this.node = node;
			}

			// Token: 0x0400013A RID: 314
			public uint F;

			// Token: 0x0400013B RID: 315
			public PathNode node;
		}
	}
}
