using System;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002DD RID: 733
	public class Sleek2Scrollbar : Sleek2Element
	{
		// Token: 0x0600151D RID: 5405 RVA: 0x00081A8C File Offset: 0x0007FE8C
		public Sleek2Scrollbar()
		{
			base.gameObject.name = "Scrollbar";
			this.imageComponent = base.gameObject.AddComponent<Image>();
			this.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Button_Background");
			this.imageComponent.type = Image.Type.Sliced;
			this.scrollbarComponent = base.gameObject.AddComponent<Scrollbar>();
			this.scrollbarComponent.direction = Scrollbar.Direction.BottomToTop;
			this.handle = new Sleek2ScrollbarHandle();
			this.addElement(this.handle);
			this.handle.transform.reset();
			this.scrollbarComponent.handleRect = this.handle.transform;
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x00081B3B File Offset: 0x0007FF3B
		// (set) Token: 0x0600151F RID: 5407 RVA: 0x00081B43 File Offset: 0x0007FF43
		public Image imageComponent { get; protected set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x00081B4C File Offset: 0x0007FF4C
		// (set) Token: 0x06001521 RID: 5409 RVA: 0x00081B54 File Offset: 0x0007FF54
		public Scrollbar scrollbarComponent { get; protected set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x00081B5D File Offset: 0x0007FF5D
		// (set) Token: 0x06001523 RID: 5411 RVA: 0x00081B65 File Offset: 0x0007FF65
		public Sleek2ScrollbarHandle handle { get; protected set; }
	}
}
