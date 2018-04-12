using System;
using SDG.Framework.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002CB RID: 715
	public class Sleek2HoverDropdownButton : Sleek2Element
	{
		// Token: 0x060014BF RID: 5311 RVA: 0x000812CC File Offset: 0x0007F6CC
		public Sleek2HoverDropdownButton()
		{
			base.gameObject.name = "Hover_Dropdown_Button";
			this.imageComponent = base.gameObject.AddComponent<Image>();
			this.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Hover_Background");
			this.imageComponent.type = Image.Type.Sliced;
			this.panel = new Sleek2HoverDropdown();
			this.addElement(this.panel);
			base.gameObject.AddComponent<Selectable>();
			this.dropdown = base.gameObject.AddComponent<HoverDropdownButton>();
			this.dropdown.dropdown = this.panel.transform;
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x0008136B File Offset: 0x0007F76B
		// (set) Token: 0x060014C1 RID: 5313 RVA: 0x00081373 File Offset: 0x0007F773
		public Image imageComponent { get; protected set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x0008137C File Offset: 0x0007F77C
		// (set) Token: 0x060014C3 RID: 5315 RVA: 0x00081384 File Offset: 0x0007F784
		public Sleek2HoverDropdown panel { get; protected set; }

		// Token: 0x04000BD1 RID: 3025
		protected HoverDropdownButton dropdown;
	}
}
