using System;

namespace Pathfinding
{
	// Token: 0x02000098 RID: 152
	public class QuadtreeNodeHolder
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x0002E604 File Offset: 0x0002CA04
		public void GetNodes(GraphNodeDelegateCancelable del)
		{
			if (this.node != null)
			{
				del(this.node);
				return;
			}
			this.c0.GetNodes(del);
			this.c1.GetNodes(del);
			this.c2.GetNodes(del);
			this.c3.GetNodes(del);
		}

		// Token: 0x0400044A RID: 1098
		public QuadtreeNode node;

		// Token: 0x0400044B RID: 1099
		public QuadtreeNodeHolder c0;

		// Token: 0x0400044C RID: 1100
		public QuadtreeNodeHolder c1;

		// Token: 0x0400044D RID: 1101
		public QuadtreeNodeHolder c2;

		// Token: 0x0400044E RID: 1102
		public QuadtreeNodeHolder c3;
	}
}
