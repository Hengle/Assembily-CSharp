using System;

namespace SDG.Framework.UI.Devkit.WorkshopUI
{
	// Token: 0x020002A8 RID: 680
	public class EconItemDefinition
	{
		// Token: 0x04000B71 RID: 2929
		public EconName ItemName;

		// Token: 0x04000B72 RID: 2930
		public EconName SkinName;

		// Token: 0x04000B73 RID: 2931
		public string Description;

		// Token: 0x04000B74 RID: 2932
		public int Type;

		// Token: 0x04000B75 RID: 2933
		public ushort ItemID;

		// Token: 0x04000B76 RID: 2934
		public ushort SkinID;

		// Token: 0x04000B77 RID: 2935
		public int DefinitionID;

		// Token: 0x04000B78 RID: 2936
		public string[] WorkshopNames;

		// Token: 0x04000B79 RID: 2937
		public ulong[] WorkshopIDs;

		// Token: 0x04000B7A RID: 2938
		public bool IsWorkshopLinked;

		// Token: 0x04000B7B RID: 2939
		public bool IsLuminescent;

		// Token: 0x04000B7C RID: 2940
		public bool IsDynamic;

		// Token: 0x04000B7D RID: 2941
		public EconVariant[] Variants;

		// Token: 0x04000B7E RID: 2942
		public bool IsMarketable;

		// Token: 0x04000B7F RID: 2943
		public bool IsTradable;
	}
}
