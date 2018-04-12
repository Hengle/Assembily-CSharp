using System;

namespace SDG.Framework.Devkit.Tools
{
	// Token: 0x02000178 RID: 376
	public class DevkitSelectionToolTypeInstantiationInfo : DevkitSelectionToolInstantiationInfoBase
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0005A089 File Offset: 0x00058489
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x0005A091 File Offset: 0x00058491
		public virtual Type type { get; set; }

		// Token: 0x06000B4E RID: 2894 RVA: 0x0005A09A File Offset: 0x0005849A
		public override void instantiate()
		{
			DevkitTypeFactory.instantiate(this.type, this.position, this.rotation, this.scale);
		}
	}
}
