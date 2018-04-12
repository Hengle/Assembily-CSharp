using System;

namespace Pathfinding
{
	// Token: 0x02000052 RID: 82
	public class NNConstraint
	{
		// Token: 0x0600035B RID: 859 RVA: 0x0001A9F3 File Offset: 0x00018DF3
		public virtual bool SuitableGraph(int graphIndex, NavGraph graph)
		{
			return (this.graphMask >> graphIndex & 1) != 0;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001AA08 File Offset: 0x00018E08
		public virtual bool Suitable(GraphNode node)
		{
			return (!this.constrainWalkability || node.Walkable == this.walkable) && (!this.constrainArea || this.area < 0 || (ulong)node.Area == (ulong)((long)this.area)) && (!this.constrainTags || (this.tags >> (int)node.Tag & 1) != 0);
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0001AA84 File Offset: 0x00018E84
		public static NNConstraint Default
		{
			get
			{
				return new NNConstraint();
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0001AA8C File Offset: 0x00018E8C
		public static NNConstraint None
		{
			get
			{
				return new NNConstraint
				{
					constrainWalkability = false,
					constrainArea = false,
					constrainTags = false,
					constrainDistance = false,
					graphMask = -1
				};
			}
		}

		// Token: 0x0400029D RID: 669
		public int graphMask = -1;

		// Token: 0x0400029E RID: 670
		public bool constrainArea;

		// Token: 0x0400029F RID: 671
		public int area = -1;

		// Token: 0x040002A0 RID: 672
		public bool constrainWalkability = true;

		// Token: 0x040002A1 RID: 673
		public bool walkable = true;

		// Token: 0x040002A2 RID: 674
		public bool distanceXZ;

		// Token: 0x040002A3 RID: 675
		public bool constrainTags = true;

		// Token: 0x040002A4 RID: 676
		public int tags = -1;

		// Token: 0x040002A5 RID: 677
		public bool constrainDistance = true;
	}
}
