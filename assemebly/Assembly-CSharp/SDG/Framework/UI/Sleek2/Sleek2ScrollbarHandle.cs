using System;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002DE RID: 734
	public class Sleek2ScrollbarHandle : Sleek2Element
	{
		// Token: 0x06001524 RID: 5412 RVA: 0x00081B70 File Offset: 0x0007FF70
		public Sleek2ScrollbarHandle()
		{
			base.gameObject.name = "Handle";
			this.imageComponent = base.gameObject.AddComponent<Image>();
			this.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Button_Background");
			this.imageComponent.type = Image.Type.Sliced;
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00081BC5 File Offset: 0x0007FFC5
		// (set) Token: 0x06001526 RID: 5414 RVA: 0x00081BCD File Offset: 0x0007FFCD
		public Image imageComponent { get; protected set; }
	}
}
