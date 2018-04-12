using System;
using SDG.Unturned;

namespace SDG.Framework.Devkit.Tools
{
	// Token: 0x02000176 RID: 374
	public class DevkitSelectionToolObjectInstantiationInfo : DevkitSelectionToolInstantiationInfoBase
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00059DD7 File Offset: 0x000581D7
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x00059DDF File Offset: 0x000581DF
		public virtual ObjectAsset asset { get; set; }

		// Token: 0x06000B43 RID: 2883 RVA: 0x00059DE8 File Offset: 0x000581E8
		public override void instantiate()
		{
			DevkitObjectFactory.instantiate(this.asset, this.position, this.rotation, this.scale);
		}
	}
}
