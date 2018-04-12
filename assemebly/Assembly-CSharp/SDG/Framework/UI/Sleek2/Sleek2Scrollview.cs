using System;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002DF RID: 735
	public class Sleek2Scrollview : Sleek2Element
	{
		// Token: 0x06001527 RID: 5415 RVA: 0x00081BD8 File Offset: 0x0007FFD8
		public Sleek2Scrollview()
		{
			base.gameObject.name = "Scrollview";
			this.scrollrectComponent = base.gameObject.AddComponent<ScrollRect>();
			this.scrollrectComponent.horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			this.scrollrectComponent.horizontalScrollbarSpacing = 5f;
			this.scrollrectComponent.horizontal = false;
			this.scrollrectComponent.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			this.scrollrectComponent.verticalScrollbarSpacing = 5f;
			this.scrollrectComponent.vertical = false;
			this.mask = new Sleek2Mask();
			this.mask.transform.reset();
			this.mask.transform.pivot = new Vector2(0f, 1f);
			this.addElement(this.mask);
			this.scrollrectComponent.viewport = this.mask.transform;
			this.horizontalScrollbar = new Sleek2Scrollbar();
			this.horizontalScrollbar.transform.anchorMin = new Vector2(0f, 0f);
			this.horizontalScrollbar.transform.anchorMax = new Vector2(1f, 0f);
			this.horizontalScrollbar.transform.pivot = new Vector2(0f, 0f);
			this.horizontalScrollbar.transform.offsetMin = new Vector2(0f, 0f);
			this.horizontalScrollbar.transform.offsetMax = new Vector2(0f, 20f);
			this.horizontalScrollbar.scrollbarComponent.direction = Scrollbar.Direction.LeftToRight;
			this.horizontalScrollbar.gameObject.SetActive(false);
			this.addElement(this.horizontalScrollbar);
			this.verticalScrollbar = new Sleek2Scrollbar();
			this.verticalScrollbar.transform.anchorMin = new Vector2(1f, 0f);
			this.verticalScrollbar.transform.anchorMax = new Vector2(1f, 1f);
			this.verticalScrollbar.transform.pivot = new Vector2(1f, 1f);
			this.verticalScrollbar.transform.offsetMin = new Vector2(-20f, 0f);
			this.verticalScrollbar.transform.offsetMax = new Vector2(0f, 0f);
			this.verticalScrollbar.scrollbarComponent.direction = Scrollbar.Direction.BottomToTop;
			this.verticalScrollbar.gameObject.SetActive(false);
			this.addElement(this.verticalScrollbar);
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x00081E60 File Offset: 0x00080260
		// (set) Token: 0x06001529 RID: 5417 RVA: 0x00081E68 File Offset: 0x00080268
		public ScrollRect scrollrectComponent { get; protected set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x00081E71 File Offset: 0x00080271
		// (set) Token: 0x0600152B RID: 5419 RVA: 0x00081E79 File Offset: 0x00080279
		public Sleek2Mask mask { get; protected set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x00081E82 File Offset: 0x00080282
		// (set) Token: 0x0600152D RID: 5421 RVA: 0x00081E8C File Offset: 0x0008028C
		public Sleek2Element panel
		{
			get
			{
				return this._panel;
			}
			set
			{
				if (this.panel != null)
				{
					this.mask.removeElement(this.panel);
				}
				this._panel = value;
				if (this.panel != null)
				{
					this.mask.addElement(this.panel);
					this.scrollrectComponent.content = this.panel.transform;
				}
				else
				{
					this.scrollrectComponent.content = null;
				}
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00081EFF File Offset: 0x000802FF
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x00081F12 File Offset: 0x00080312
		public bool horizontal
		{
			get
			{
				return this.scrollrectComponent.horizontalScrollbar != null;
			}
			set
			{
				this.scrollrectComponent.horizontalScrollbar = ((!value) ? null : this.horizontalScrollbar.scrollbarComponent);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x00081F36 File Offset: 0x00080336
		// (set) Token: 0x06001531 RID: 5425 RVA: 0x00081F49 File Offset: 0x00080349
		public bool vertical
		{
			get
			{
				return this.scrollrectComponent.verticalScrollbar != null;
			}
			set
			{
				this.scrollrectComponent.verticalScrollbar = ((!value) ? null : this.verticalScrollbar.scrollbarComponent);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x00081F6D File Offset: 0x0008036D
		// (set) Token: 0x06001533 RID: 5427 RVA: 0x00081F75 File Offset: 0x00080375
		public Sleek2Scrollbar horizontalScrollbar { get; protected set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x00081F7E File Offset: 0x0008037E
		// (set) Token: 0x06001535 RID: 5429 RVA: 0x00081F86 File Offset: 0x00080386
		public Sleek2Scrollbar verticalScrollbar { get; protected set; }

		// Token: 0x04000BEF RID: 3055
		protected Sleek2Element _panel;
	}
}
