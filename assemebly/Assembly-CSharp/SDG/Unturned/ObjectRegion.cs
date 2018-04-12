using System;

namespace SDG.Unturned
{
	// Token: 0x020005B5 RID: 1461
	public class ObjectRegion
	{
		// Token: 0x060028EF RID: 10479 RVA: 0x000F9BAA File Offset: 0x000F7FAA
		public ObjectRegion()
		{
			this.isNetworked = false;
			this.updateObjectIndex = 0;
		}

		// Token: 0x0400198C RID: 6540
		public bool isNetworked;

		// Token: 0x0400198D RID: 6541
		public ushort updateObjectIndex;
	}
}
