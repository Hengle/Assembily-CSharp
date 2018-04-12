using System;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit
{
	// Token: 0x0200023E RID: 574
	public class DevkitToolbarUndoButton : Sleek2ImageButton
	{
		// Token: 0x060010CE RID: 4302 RVA: 0x0006E170 File Offset: 0x0006C570
		public DevkitToolbarUndoButton()
		{
			base.transform.sizeDelta = new Vector2(0f, (float)Sleek2Config.bodyHeight);
			base.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Hover_Background");
			this.label = new Sleek2TranslatedLabel();
			this.label.transform.reset();
			this.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Toolbar.Edit.Undo"));
			this.label.translation.format();
			this.addElement(this.label);
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0006E20A File Offset: 0x0006C60A
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x0006E212 File Offset: 0x0006C612
		public Sleek2TranslatedLabel label { get; protected set; }

		// Token: 0x060010D1 RID: 4305 RVA: 0x0006E21B File Offset: 0x0006C61B
		protected override void handleButtonClick()
		{
			DevkitTransactionManager.undo();
			base.handleButtonClick();
		}
	}
}
