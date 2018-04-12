using System;

namespace SDG.Unturned
{
	// Token: 0x020006AF RID: 1711
	public class PreferenceData
	{
		// Token: 0x060031D1 RID: 12753 RVA: 0x00144129 File Offset: 0x00142529
		public PreferenceData()
		{
			this.Graphics = new GraphicsPreferenceData();
			this.Viewmodel = new ViewmodelPreferenceData();
		}

		// Token: 0x040021BD RID: 8637
		public GraphicsPreferenceData Graphics;

		// Token: 0x040021BE RID: 8638
		public ViewmodelPreferenceData Viewmodel;
	}
}
