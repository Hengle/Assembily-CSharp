using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002CA RID: 714
	public class Sleek2HoverDropdown : Sleek2Element
	{
		// Token: 0x060014B9 RID: 5305 RVA: 0x00081140 File Offset: 0x0007F540
		public Sleek2HoverDropdown()
		{
			base.elements = new List<Sleek2Element>();
			base.gameObject.name = "Dropdown";
			base.transform.sizeDelta = Vector2.zero;
			this.imageComponent = base.gameObject.AddComponent<Image>();
			this.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Hover_Background");
			this.imageComponent.type = Image.Type.Sliced;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x000811B0 File Offset: 0x0007F5B0
		// (set) Token: 0x060014BB RID: 5307 RVA: 0x000811B8 File Offset: 0x0007F5B8
		public Image imageComponent { get; protected set; }

		// Token: 0x060014BC RID: 5308 RVA: 0x000811C4 File Offset: 0x0007F5C4
		public override void addElement(Sleek2Element element)
		{
			base.addElement(element);
			element.transform.anchorMin = new Vector2(0f, 1f);
			element.transform.anchorMax = Vector2.one;
			element.transform.pivot = new Vector2(0.5f, 1f);
			this.shiftElements();
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00081222 File Offset: 0x0007F622
		public override void removeElement(Sleek2Element element)
		{
			base.removeElement(element);
			this.shiftElements();
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00081234 File Offset: 0x0007F634
		protected void shiftElements()
		{
			float num = 0f;
			for (int i = 0; i < base.elements.Count; i++)
			{
				base.elements[i].transform.anchoredPosition = new Vector2(0f, -num);
				num += base.elements[i].transform.rect.height;
			}
			base.transform.sizeDelta = new Vector2(base.transform.sizeDelta.x, num);
		}
	}
}
