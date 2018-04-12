using System;

namespace Pathfinding
{
	// Token: 0x02000053 RID: 83
	public class PathNNConstraint : NNConstraint
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0001AACC File Offset: 0x00018ECC
		public new static PathNNConstraint Default
		{
			get
			{
				return new PathNNConstraint
				{
					constrainArea = true
				};
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001AAE7 File Offset: 0x00018EE7
		public virtual void SetStart(GraphNode node)
		{
			if (node != null)
			{
				this.area = (int)node.Area;
			}
			else
			{
				this.constrainArea = false;
			}
		}
	}
}
