using System;
using UnityEngine;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002C9 RID: 713
	public class Sleek2HorizontalScrollviewContents : Sleek2Element
	{
		// Token: 0x060014B5 RID: 5301 RVA: 0x00081074 File Offset: 0x0007F474
		public Sleek2HorizontalScrollviewContents()
		{
			base.name = "Panel";
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x00081087 File Offset: 0x0007F487
		public override void addElement(Sleek2Element element, int insertIndex)
		{
			base.addElement(element, insertIndex);
			this.shiftElements();
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x00081097 File Offset: 0x0007F497
		public override void removeElement(Sleek2Element element)
		{
			base.removeElement(element);
			this.shiftElements();
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x000810A8 File Offset: 0x0007F4A8
		protected void shiftElements()
		{
			float num = 0f;
			for (int i = 0; i < base.elements.Count; i++)
			{
				base.elements[i].transform.anchoredPosition = new Vector2(num, 0f);
				num += base.elements[i].transform.rect.width;
			}
			base.transform.sizeDelta = new Vector2(num, base.transform.sizeDelta.y);
		}
	}
}
