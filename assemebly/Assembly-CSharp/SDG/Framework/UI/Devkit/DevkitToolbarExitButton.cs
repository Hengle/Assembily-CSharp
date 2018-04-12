using System;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit
{
	// Token: 0x0200023A RID: 570
	public class DevkitToolbarExitButton : Sleek2ImageButton
	{
		// Token: 0x060010BF RID: 4287 RVA: 0x0006DDE0 File Offset: 0x0006C1E0
		public DevkitToolbarExitButton()
		{
			base.transform.sizeDelta = new Vector2(0f, (float)Sleek2Config.bodyHeight);
			base.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Hover_Background");
			this.label = new Sleek2TranslatedLabel();
			this.label.transform.reset();
			this.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Toolbar.File.Exit"));
			this.label.translation.format();
			this.addElement(this.label);
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x0006DE7A File Offset: 0x0006C27A
		// (set) Token: 0x060010C1 RID: 4289 RVA: 0x0006DE82 File Offset: 0x0006C282
		public Sleek2TranslatedLabel label { get; protected set; }

		// Token: 0x060010C2 RID: 4290 RVA: 0x0006DE8B File Offset: 0x0006C28B
		protected override void handleButtonClick()
		{
			if (Level.isEditor)
			{
				Level.exit();
			}
			else
			{
				Application.Quit();
			}
			base.handleButtonClick();
		}
	}
}
