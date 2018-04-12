using System;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020000AC RID: 172
	public class VoxelArea
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x00038FD0 File Offset: 0x000373D0
		public VoxelArea(int width, int depth)
		{
			this.width = width;
			this.depth = depth;
			int num = width * depth;
			this.compactCells = new CompactVoxelCell[num];
			this.linkedSpans = new LinkedVoxelSpan[(int)((float)num * 8f) + 15 & -16];
			this.ResetLinkedVoxelSpans();
			int[] array = new int[4];
			array[0] = -1;
			array[2] = 1;
			this.DirectionX = array;
			this.DirectionZ = new int[]
			{
				0,
				width,
				0,
				-width
			};
			this.VectorDirection = new Vector3[]
			{
				Vector3.left,
				Vector3.forward,
				Vector3.right,
				Vector3.back
			};
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000390AC File Offset: 0x000374AC
		public void Reset()
		{
			this.ResetLinkedVoxelSpans();
			for (int i = 0; i < this.compactCells.Length; i++)
			{
				this.compactCells[i].count = 0u;
				this.compactCells[i].index = 0u;
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000390FC File Offset: 0x000374FC
		private void ResetLinkedVoxelSpans()
		{
			int num = this.linkedSpans.Length;
			this.linkedSpanCount = this.width * this.depth;
			LinkedVoxelSpan linkedVoxelSpan = new LinkedVoxelSpan(uint.MaxValue, uint.MaxValue, -1, -1);
			for (int i = 0; i < num; i++)
			{
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
			}
			this.removedStackCount = 0;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000392A8 File Offset: 0x000376A8
		public int GetSpanCountAll()
		{
			int num = 0;
			int num2 = this.width * this.depth;
			for (int i = 0; i < num2; i++)
			{
				int num3 = i;
				while (num3 != -1 && this.linkedSpans[num3].bottom != 4294967295u)
				{
					num++;
					num3 = this.linkedSpans[num3].next;
				}
			}
			return num;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00039314 File Offset: 0x00037714
		public int GetSpanCount()
		{
			int num = 0;
			int num2 = this.width * this.depth;
			for (int i = 0; i < num2; i++)
			{
				int num3 = i;
				while (num3 != -1 && this.linkedSpans[num3].bottom != 4294967295u)
				{
					if (this.linkedSpans[num3].area != 0)
					{
						num++;
					}
					num3 = this.linkedSpans[num3].next;
				}
			}
			return num;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00039398 File Offset: 0x00037798
		public void AddLinkedSpan(int index, uint bottom, uint top, int area, int voxelWalkableClimb)
		{
			if (this.linkedSpans[index].bottom == 4294967295u)
			{
				this.linkedSpans[index] = new LinkedVoxelSpan(bottom, top, area);
				return;
			}
			int num = -1;
			int num2 = index;
			while (index != -1)
			{
				if (this.linkedSpans[index].bottom > top)
				{
					break;
				}
				if (this.linkedSpans[index].top < bottom)
				{
					num = index;
					index = this.linkedSpans[index].next;
				}
				else
				{
					if (this.linkedSpans[index].bottom < bottom)
					{
						bottom = this.linkedSpans[index].bottom;
					}
					if (this.linkedSpans[index].top > top)
					{
						top = this.linkedSpans[index].top;
					}
					if (AstarMath.Abs((int)(top - this.linkedSpans[index].top)) <= voxelWalkableClimb)
					{
						area = AstarMath.Max(area, this.linkedSpans[index].area);
					}
					int next = this.linkedSpans[index].next;
					if (num != -1)
					{
						this.linkedSpans[num].next = next;
						if (this.removedStackCount == this.removedStack.Length)
						{
							int[] dst = new int[this.removedStackCount * 4];
							Buffer.BlockCopy(this.removedStack, 0, dst, 0, this.removedStackCount * 4);
							this.removedStack = dst;
						}
						this.removedStack[this.removedStackCount] = index;
						this.removedStackCount++;
						index = next;
					}
					else
					{
						if (next == -1)
						{
							this.linkedSpans[num2] = new LinkedVoxelSpan(bottom, top, area);
							return;
						}
						this.linkedSpans[num2] = this.linkedSpans[next];
						if (this.removedStackCount == this.removedStack.Length)
						{
							int[] dst2 = new int[this.removedStackCount * 4];
							Buffer.BlockCopy(this.removedStack, 0, dst2, 0, this.removedStackCount * 4);
							this.removedStack = dst2;
						}
						this.removedStack[this.removedStackCount] = next;
						this.removedStackCount++;
						index = this.linkedSpans[num2].next;
					}
				}
			}
			if (this.linkedSpanCount >= this.linkedSpans.Length)
			{
				LinkedVoxelSpan[] array = this.linkedSpans;
				int num3 = this.linkedSpanCount;
				int num4 = this.removedStackCount;
				this.linkedSpans = new LinkedVoxelSpan[this.linkedSpans.Length * 2];
				this.ResetLinkedVoxelSpans();
				this.linkedSpanCount = num3;
				this.removedStackCount = num4;
				for (int i = 0; i < this.linkedSpanCount; i++)
				{
					this.linkedSpans[i] = array[i];
				}
				Debug.Log("Layer estimate too low, doubling size of buffer.\nThis message is harmless.");
			}
			int num5;
			if (this.removedStackCount > 0)
			{
				this.removedStackCount--;
				num5 = this.removedStack[this.removedStackCount];
			}
			else
			{
				num5 = this.linkedSpanCount;
				this.linkedSpanCount++;
			}
			if (num != -1)
			{
				this.linkedSpans[num5] = new LinkedVoxelSpan(bottom, top, area, this.linkedSpans[num].next);
				this.linkedSpans[num].next = num5;
			}
			else
			{
				this.linkedSpans[num5] = this.linkedSpans[num2];
				this.linkedSpans[num2] = new LinkedVoxelSpan(bottom, top, area, num5);
			}
		}

		// Token: 0x040004DE RID: 1246
		public const uint MaxHeight = 65536u;

		// Token: 0x040004DF RID: 1247
		public const int MaxHeightInt = 65536;

		// Token: 0x040004E0 RID: 1248
		public const uint InvalidSpanValue = 4294967295u;

		// Token: 0x040004E1 RID: 1249
		public const float AvgSpanLayerCountEstimate = 8f;

		// Token: 0x040004E2 RID: 1250
		public readonly int width;

		// Token: 0x040004E3 RID: 1251
		public readonly int depth;

		// Token: 0x040004E4 RID: 1252
		public CompactVoxelSpan[] compactSpans;

		// Token: 0x040004E5 RID: 1253
		public CompactVoxelCell[] compactCells;

		// Token: 0x040004E6 RID: 1254
		public int compactSpanCount;

		// Token: 0x040004E7 RID: 1255
		public ushort[] tmpUShortArr;

		// Token: 0x040004E8 RID: 1256
		public int[] areaTypes;

		// Token: 0x040004E9 RID: 1257
		public ushort[] dist;

		// Token: 0x040004EA RID: 1258
		public ushort maxDistance;

		// Token: 0x040004EB RID: 1259
		public int maxRegions;

		// Token: 0x040004EC RID: 1260
		public int[] DirectionX;

		// Token: 0x040004ED RID: 1261
		public int[] DirectionZ;

		// Token: 0x040004EE RID: 1262
		public Vector3[] VectorDirection;

		// Token: 0x040004EF RID: 1263
		private int linkedSpanCount;

		// Token: 0x040004F0 RID: 1264
		public LinkedVoxelSpan[] linkedSpans;

		// Token: 0x040004F1 RID: 1265
		private int[] removedStack = new int[128];

		// Token: 0x040004F2 RID: 1266
		private int removedStackCount;
	}
}
