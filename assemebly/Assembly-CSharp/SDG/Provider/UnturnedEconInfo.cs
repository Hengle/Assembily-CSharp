using System;

namespace SDG.Provider
{
	// Token: 0x0200035D RID: 861
	public class UnturnedEconInfo
	{
		// Token: 0x060017BE RID: 6078 RVA: 0x0008839C File Offset: 0x0008679C
		public UnturnedEconInfo()
		{
			this.name = string.Empty;
			this.type = string.Empty;
			this.description = string.Empty;
			this.name_color = string.Empty;
			this.itemdefid = 0;
			this.item_id = 0;
			this.item_skin = 0;
			this.item_effect = 0;
			this.vehicle_id = 0;
		}

		// Token: 0x04000CB6 RID: 3254
		public string name;

		// Token: 0x04000CB7 RID: 3255
		public string type;

		// Token: 0x04000CB8 RID: 3256
		public string description;

		// Token: 0x04000CB9 RID: 3257
		public string name_color;

		// Token: 0x04000CBA RID: 3258
		public int itemdefid;

		// Token: 0x04000CBB RID: 3259
		public bool marketable;

		// Token: 0x04000CBC RID: 3260
		public bool scrapable;

		// Token: 0x04000CBD RID: 3261
		public int item_id;

		// Token: 0x04000CBE RID: 3262
		public int item_skin;

		// Token: 0x04000CBF RID: 3263
		public int item_effect;

		// Token: 0x04000CC0 RID: 3264
		public int vehicle_id;
	}
}
