using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit
{
	// Token: 0x02000239 RID: 569
	public class DevkitToolbarContainerButton : Sleek2ImageButton
	{
		// Token: 0x060010BB RID: 4283 RVA: 0x0006DCF4 File Offset: 0x0006C0F4
		public DevkitToolbarContainerButton(Type type)
		{
			this.type = type;
			base.transform.sizeDelta = new Vector2(0f, (float)Sleek2Config.bodyHeight);
			base.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Hover_Background");
			this.label = new Sleek2Label();
			this.label.transform.reset();
			this.label.textComponent.text = type.Name;
			this.addElement(this.label);
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x0006DD7B File Offset: 0x0006C17B
		// (set) Token: 0x060010BD RID: 4285 RVA: 0x0006DD83 File Offset: 0x0006C183
		public Sleek2Label label { get; protected set; }

		// Token: 0x060010BE RID: 4286 RVA: 0x0006DD8C File Offset: 0x0006C18C
		protected override void handleButtonClick()
		{
			Sleek2PopoutContainer sleek2PopoutContainer = DevkitWindowManager.addContainer(this.type);
			sleek2PopoutContainer.transform.anchorMin = new Vector2(0.25f, 0.25f);
			sleek2PopoutContainer.transform.anchorMax = new Vector2(0.75f, 0.75f);
			base.handleButtonClick();
		}

		// Token: 0x04000A15 RID: 2581
		protected Type type;
	}
}
