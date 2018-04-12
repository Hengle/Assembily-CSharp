using System;

namespace SDG.Unturned
{
	// Token: 0x020005BA RID: 1466
	public class ResourceRegion
	{
		// Token: 0x0600290F RID: 10511 RVA: 0x000FB13F File Offset: 0x000F953F
		public ResourceRegion()
		{
			this.isNetworked = false;
			this.respawnResourceIndex = 0;
		}

		// Token: 0x04001999 RID: 6553
		public bool isNetworked;

		// Token: 0x0400199A RID: 6554
		public ushort respawnResourceIndex;
	}
}
