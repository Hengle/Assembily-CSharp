using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002CE RID: 718
	public class Sleek2ImageButton : Sleek2Image
	{
		// Token: 0x060014CB RID: 5323 RVA: 0x0006BB04 File Offset: 0x00069F04
		public Sleek2ImageButton()
		{
			base.gameObject.name = "Button";
			base.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Button_Background");
			base.imageComponent.type = Image.Type.Sliced;
			this.buttonComponent = base.gameObject.AddComponent<Button>();
			this.buttonComponent.onClick.AddListener(new UnityAction(this.handleButtonClick));
		}

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x060014CC RID: 5324 RVA: 0x0006BB78 File Offset: 0x00069F78
		// (remove) Token: 0x060014CD RID: 5325 RVA: 0x0006BBB0 File Offset: 0x00069FB0
		public event ButtonClickedHandler clicked;

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x0006BBE6 File Offset: 0x00069FE6
		// (set) Token: 0x060014CF RID: 5327 RVA: 0x0006BBEE File Offset: 0x00069FEE
		public Button buttonComponent { get; protected set; }

		// Token: 0x060014D0 RID: 5328 RVA: 0x0006BBF7 File Offset: 0x00069FF7
		protected virtual void triggerClicked()
		{
			if (this.clicked != null)
			{
				this.clicked(this);
			}
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0006BC10 File Offset: 0x0006A010
		protected virtual void handleButtonClick()
		{
			this.triggerClicked();
		}
	}
}
