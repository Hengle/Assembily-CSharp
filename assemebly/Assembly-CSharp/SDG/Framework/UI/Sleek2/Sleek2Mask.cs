using System;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002E0 RID: 736
	public class Sleek2Mask : Sleek2Element
	{
		// Token: 0x06001536 RID: 5430 RVA: 0x00081F90 File Offset: 0x00080390
		public Sleek2Mask()
		{
			base.gameObject.name = "Mask";
			this.maskComponent = base.gameObject.AddComponent<Mask>();
			this.maskComponent.showMaskGraphic = false;
			this.imageComponent = base.gameObject.AddComponent<Image>();
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x00081FE1 File Offset: 0x000803E1
		// (set) Token: 0x06001538 RID: 5432 RVA: 0x00081FE9 File Offset: 0x000803E9
		public Mask maskComponent { get; protected set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x00081FF2 File Offset: 0x000803F2
		// (set) Token: 0x0600153A RID: 5434 RVA: 0x00081FFA File Offset: 0x000803FA
		public Image imageComponent { get; protected set; }
	}
}
