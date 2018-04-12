using System;

namespace Pathfinding
{
	// Token: 0x02000057 RID: 87
	[Serializable]
	public class TagMask
	{
		// Token: 0x0600036C RID: 876 RVA: 0x0001AC01 File Offset: 0x00019001
		public TagMask()
		{
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001AC09 File Offset: 0x00019009
		public TagMask(int change, int set)
		{
			this.tagsChange = change;
			this.tagsSet = set;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001AC20 File Offset: 0x00019020
		public void SetValues(object boxedTagMask)
		{
			TagMask tagMask = (TagMask)boxedTagMask;
			this.tagsChange = tagMask.tagsChange;
			this.tagsSet = tagMask.tagsSet;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001AC4C File Offset: 0x0001904C
		public override string ToString()
		{
			return string.Empty + Convert.ToString(this.tagsChange, 2) + "\n" + Convert.ToString(this.tagsSet, 2);
		}

		// Token: 0x040002AC RID: 684
		public int tagsChange;

		// Token: 0x040002AD RID: 685
		public int tagsSet;
	}
}
