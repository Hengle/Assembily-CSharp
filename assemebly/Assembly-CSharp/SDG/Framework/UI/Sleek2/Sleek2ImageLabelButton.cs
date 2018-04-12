using System;
using UnityEngine;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002CF RID: 719
	public class Sleek2ImageLabelButton : Sleek2ImageButton
	{
		// Token: 0x060014D2 RID: 5330 RVA: 0x0007498C File Offset: 0x00072D8C
		public Sleek2ImageLabelButton()
		{
			this.label = new Sleek2Label();
			this.label.transform.reset();
			this.label.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(this.label);
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x000749DB File Offset: 0x00072DDB
		// (set) Token: 0x060014D4 RID: 5332 RVA: 0x000749E3 File Offset: 0x00072DE3
		public Sleek2Label label { get; protected set; }
	}
}
