using System;
using UnityEngine;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002E8 RID: 744
	public class Sleek2ToolbarButton : Sleek2HoverDropdownButton
	{
		// Token: 0x06001566 RID: 5478 RVA: 0x000826B4 File Offset: 0x00080AB4
		public Sleek2ToolbarButton()
		{
			base.transform.anchorMin = Vector2.zero;
			base.transform.anchorMax = new Vector2(0f, 1f);
			base.transform.pivot = new Vector2(0f, 0.5f);
			base.transform.sizeDelta = new Vector2((float)Sleek2Config.tabWidth, 0f);
			this.label = new Sleek2Label();
			this.label.transform.anchorMin = Vector2.zero;
			this.label.transform.anchorMax = Vector2.one;
			this.label.transform.sizeDelta = Vector2.zero;
			this.addElement(this.label);
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x0008277C File Offset: 0x00080B7C
		// (set) Token: 0x06001568 RID: 5480 RVA: 0x00082784 File Offset: 0x00080B84
		public Sleek2Label label { get; protected set; }
	}
}
