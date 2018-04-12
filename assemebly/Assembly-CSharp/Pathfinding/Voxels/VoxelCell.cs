using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020000B2 RID: 178
	public struct VoxelCell
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x0003988C File Offset: 0x00037C8C
		public void AddSpan(uint bottom, uint top, int area, int voxelWalkableClimb)
		{
			VoxelSpan voxelSpan = new VoxelSpan(bottom, top, area);
			if (this.firstSpan == null)
			{
				this.firstSpan = voxelSpan;
				return;
			}
			VoxelSpan voxelSpan2 = null;
			VoxelSpan voxelSpan3 = this.firstSpan;
			while (voxelSpan3 != null)
			{
				if (voxelSpan3.bottom > voxelSpan.top)
				{
					break;
				}
				if (voxelSpan3.top < voxelSpan.bottom)
				{
					voxelSpan2 = voxelSpan3;
					voxelSpan3 = voxelSpan3.next;
				}
				else
				{
					if (voxelSpan3.bottom < bottom)
					{
						voxelSpan.bottom = voxelSpan3.bottom;
					}
					if (voxelSpan3.top > top)
					{
						voxelSpan.top = voxelSpan3.top;
					}
					if (AstarMath.Abs((int)(voxelSpan.top - voxelSpan3.top)) <= voxelWalkableClimb)
					{
						voxelSpan.area = AstarMath.Max(voxelSpan.area, voxelSpan3.area);
					}
					VoxelSpan next = voxelSpan3.next;
					if (voxelSpan2 != null)
					{
						voxelSpan2.next = next;
					}
					else
					{
						this.firstSpan = next;
					}
					voxelSpan3 = next;
				}
			}
			if (voxelSpan2 != null)
			{
				voxelSpan.next = voxelSpan2.next;
				voxelSpan2.next = voxelSpan;
			}
			else
			{
				voxelSpan.next = this.firstSpan;
				this.firstSpan = voxelSpan;
			}
		}

		// Token: 0x04000506 RID: 1286
		public VoxelSpan firstSpan;
	}
}
