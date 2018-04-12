using System;
using UnityEngine;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002F3 RID: 755
	public class Sleek2VerticalScrollviewContents : Sleek2Element
	{
		// Token: 0x06001596 RID: 5526 RVA: 0x00082BF8 File Offset: 0x00080FF8
		public Sleek2VerticalScrollviewContents()
		{
			base.name = "Panel";
			base.transform.anchorMin = new Vector2(0f, 1f);
			base.transform.anchorMax = new Vector2(1f, 1f);
			base.transform.pivot = new Vector2(0.5f, 1f);
			base.transform.sizeDelta = new Vector2(0f, 0f);
		}

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06001597 RID: 5527 RVA: 0x00082C80 File Offset: 0x00081080
		// (remove) Token: 0x06001598 RID: 5528 RVA: 0x00082CB8 File Offset: 0x000810B8
		public event ScrollviewContentsResized resized;

		// Token: 0x06001599 RID: 5529 RVA: 0x00082CEE File Offset: 0x000810EE
		public override void addElement(Sleek2Element element)
		{
			base.addElement(element);
			this.shiftElements();
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00082CFD File Offset: 0x000810FD
		public override void removeElement(Sleek2Element element)
		{
			base.removeElement(element);
			this.shiftElements();
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00082D0C File Offset: 0x0008110C
		protected virtual void shiftElements()
		{
			float num = 0f;
			for (int i = 0; i < base.elements.Count; i++)
			{
				base.elements[i].transform.anchoredPosition = new Vector2(0f, -num);
				num += base.elements[i].transform.rect.height;
			}
			base.transform.sizeDelta = new Vector2(0f, num);
			this.triggerResized();
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00082D9A File Offset: 0x0008119A
		protected virtual void triggerResized()
		{
			if (this.resized != null)
			{
				this.resized(this);
			}
		}
	}
}
