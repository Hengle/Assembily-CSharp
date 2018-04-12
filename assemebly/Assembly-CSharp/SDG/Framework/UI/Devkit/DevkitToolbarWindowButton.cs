using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit
{
	// Token: 0x0200023F RID: 575
	public class DevkitToolbarWindowButton : Sleek2ImageButton
	{
		// Token: 0x060010D2 RID: 4306 RVA: 0x0006E22C File Offset: 0x0006C62C
		public DevkitToolbarWindowButton(Type type)
		{
			this.type = type;
			base.transform.sizeDelta = new Vector2(0f, (float)Sleek2Config.bodyHeight);
			base.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Hover_Background");
			this.label = new Sleek2Label();
			this.label.transform.reset();
			this.label.textComponent.text = type.Name;
			this.addElement(this.label);
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0006E2B3 File Offset: 0x0006C6B3
		// (set) Token: 0x060010D4 RID: 4308 RVA: 0x0006E2BB File Offset: 0x0006C6BB
		public Sleek2Label label { get; protected set; }

		// Token: 0x060010D5 RID: 4309 RVA: 0x0006E2C4 File Offset: 0x0006C6C4
		protected override void handleButtonClick()
		{
			Sleek2Window window = Activator.CreateInstance(this.type) as Sleek2Window;
			DevkitWindowManager.addWindow(window);
			base.handleButtonClick();
		}

		// Token: 0x04000A1D RID: 2589
		protected Type type;
	}
}
