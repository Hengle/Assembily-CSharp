using System;
using SDG.Framework.UI.Components;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002DA RID: 730
	public class Sleek2Resize : Sleek2Element
	{
		// Token: 0x0600150C RID: 5388 RVA: 0x000818D8 File Offset: 0x0007FCD8
		public Sleek2Resize()
		{
			base.gameObject.name = "Resize";
			this.handle = base.gameObject.AddComponent<ResizeHandle>();
			this.image = base.gameObject.AddComponent<Image>();
			base.gameObject.AddComponent<Selectable>();
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x00081929 File Offset: 0x0007FD29
		// (set) Token: 0x0600150E RID: 5390 RVA: 0x00081931 File Offset: 0x0007FD31
		public ResizeHandle handle { get; protected set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x0008193A File Offset: 0x0007FD3A
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x00081942 File Offset: 0x0007FD42
		public Image image { get; protected set; }
	}
}
