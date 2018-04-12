using System;
using UnityEngine;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002D0 RID: 720
	public class Sleek2ImageTranslatedLabelButton : Sleek2ImageButton
	{
		// Token: 0x060014D5 RID: 5333 RVA: 0x00081390 File Offset: 0x0007F790
		public Sleek2ImageTranslatedLabelButton()
		{
			this.label = new Sleek2TranslatedLabel();
			this.label.transform.reset();
			this.label.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(this.label);
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x000813DF File Offset: 0x0007F7DF
		// (set) Token: 0x060014D7 RID: 5335 RVA: 0x000813E7 File Offset: 0x0007F7E7
		public Sleek2TranslatedLabel label { get; protected set; }
	}
}
