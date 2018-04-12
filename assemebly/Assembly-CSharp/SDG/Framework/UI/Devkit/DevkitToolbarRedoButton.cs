using System;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit
{
	// Token: 0x0200023D RID: 573
	public class DevkitToolbarRedoButton : Sleek2ImageButton
	{
		// Token: 0x060010CA RID: 4298 RVA: 0x0006E0B4 File Offset: 0x0006C4B4
		public DevkitToolbarRedoButton()
		{
			base.transform.sizeDelta = new Vector2(0f, (float)Sleek2Config.bodyHeight);
			base.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Hover_Background");
			this.label = new Sleek2TranslatedLabel();
			this.label.transform.reset();
			this.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Toolbar.Edit.Redo"));
			this.label.translation.format();
			this.addElement(this.label);
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x0006E14E File Offset: 0x0006C54E
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x0006E156 File Offset: 0x0006C556
		public Sleek2TranslatedLabel label { get; protected set; }

		// Token: 0x060010CD RID: 4301 RVA: 0x0006E15F File Offset: 0x0006C55F
		protected override void handleButtonClick()
		{
			DevkitTransactionManager.redo();
			base.handleButtonClick();
		}
	}
}
