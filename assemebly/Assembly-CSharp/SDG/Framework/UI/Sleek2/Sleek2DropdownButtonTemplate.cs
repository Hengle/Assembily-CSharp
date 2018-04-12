using System;
using UnityEngine;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002BC RID: 700
	public class Sleek2DropdownButtonTemplate : Sleek2ImageButton
	{
		// Token: 0x06001453 RID: 5203 RVA: 0x0006BFA4 File Offset: 0x0006A3A4
		public Sleek2DropdownButtonTemplate()
		{
			base.transform.sizeDelta = new Vector2(0f, (float)Sleek2Config.bodyHeight);
			base.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Hover_Background");
			this.label = new Sleek2Label();
			this.label.transform.reset();
			this.addElement(this.label);
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x0006C00E File Offset: 0x0006A40E
		// (set) Token: 0x06001455 RID: 5205 RVA: 0x0006C016 File Offset: 0x0006A416
		public Sleek2Label label { get; protected set; }
	}
}
