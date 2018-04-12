using System;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002CC RID: 716
	public class Sleek2Image : Sleek2Element
	{
		// Token: 0x060014C4 RID: 5316 RVA: 0x0006BACF File Offset: 0x00069ECF
		public Sleek2Image()
		{
			base.name = "Image";
			this.imageComponent = base.gameObject.AddComponent<Image>();
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x0006BAF3 File Offset: 0x00069EF3
		// (set) Token: 0x060014C6 RID: 5318 RVA: 0x0006BAFB File Offset: 0x00069EFB
		public Image imageComponent { get; protected set; }
	}
}
