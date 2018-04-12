using System;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit
{
	// Token: 0x0200023B RID: 571
	public class DevkitToolbarHelpButton : Sleek2ImageButton
	{
		// Token: 0x060010C3 RID: 4291 RVA: 0x0006DEAC File Offset: 0x0006C2AC
		public DevkitToolbarHelpButton(string text, string url)
		{
			this.url = url;
			base.transform.sizeDelta = new Vector2(0f, (float)Sleek2Config.bodyHeight);
			base.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Hover_Background");
			this.label = new Sleek2Label();
			this.label.transform.reset();
			this.label.textComponent.text = text;
			this.addElement(this.label);
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x0006DF2E File Offset: 0x0006C32E
		// (set) Token: 0x060010C5 RID: 4293 RVA: 0x0006DF36 File Offset: 0x0006C336
		public Sleek2Label label { get; protected set; }

		// Token: 0x060010C6 RID: 4294 RVA: 0x0006DF3F File Offset: 0x0006C33F
		protected override void handleButtonClick()
		{
			Provider.provider.browserService.open(this.url);
			Debug.Log("URL: " + this.url);
			base.handleButtonClick();
		}

		// Token: 0x04000A18 RID: 2584
		protected string url;
	}
}
