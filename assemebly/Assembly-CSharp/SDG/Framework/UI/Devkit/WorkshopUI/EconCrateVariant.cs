using System;

namespace SDG.Framework.UI.Devkit.WorkshopUI
{
	// Token: 0x020002AE RID: 686
	public class EconCrateVariant : EconVariant
	{
		// Token: 0x06001400 RID: 5120 RVA: 0x0007F8AA File Offset: 0x0007DCAA
		public EconCrateVariant(int Effect, bool IsCommodity, bool IsGenerated, int Quality) : base(Quality)
		{
			this.Effect = Effect;
			this.IsCommodity = IsCommodity;
			this.IsGenerated = IsGenerated;
		}

		// Token: 0x04000B86 RID: 2950
		public int Effect;

		// Token: 0x04000B87 RID: 2951
		public bool IsCommodity;

		// Token: 0x04000B88 RID: 2952
		public bool IsGenerated;
	}
}
