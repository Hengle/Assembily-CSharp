using System;
using UnityEngine;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002E7 RID: 743
	public class Sleek2Toolbar : Sleek2Element
	{
		// Token: 0x06001562 RID: 5474 RVA: 0x00082603 File Offset: 0x00080A03
		public Sleek2Toolbar()
		{
			base.name = "Toolbar";
			base.transform.reset();
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00082621 File Offset: 0x00080A21
		public override void addElement(Sleek2Element element)
		{
			base.addElement(element);
			this.shiftElements();
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00082630 File Offset: 0x00080A30
		public override void removeElement(Sleek2Element element)
		{
			base.removeElement(element);
			this.shiftElements();
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00082640 File Offset: 0x00080A40
		protected virtual void shiftElements()
		{
			float num = 0f;
			for (int i = 0; i < base.elements.Count; i++)
			{
				base.elements[i].transform.anchoredPosition = new Vector2(num, 0f);
				num += base.elements[i].transform.rect.width;
			}
		}
	}
}
