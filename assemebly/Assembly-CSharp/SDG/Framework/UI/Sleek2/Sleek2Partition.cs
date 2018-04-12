using System;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002D6 RID: 726
	public class Sleek2Partition : Sleek2Element
	{
		// Token: 0x060014F6 RID: 5366 RVA: 0x00081760 File Offset: 0x0007FB60
		public Sleek2Partition()
		{
			base.name = "Partition";
			this.image = base.gameObject.AddComponent<Image>();
			this.mask = base.gameObject.AddComponent<Mask>();
			this.mask.showMaskGraphic = false;
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x000817AC File Offset: 0x0007FBAC
		// (set) Token: 0x060014F8 RID: 5368 RVA: 0x000817B4 File Offset: 0x0007FBB4
		public Image image { get; protected set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x000817BD File Offset: 0x0007FBBD
		// (set) Token: 0x060014FA RID: 5370 RVA: 0x000817C5 File Offset: 0x0007FBC5
		public Mask mask { get; protected set; }
	}
}
