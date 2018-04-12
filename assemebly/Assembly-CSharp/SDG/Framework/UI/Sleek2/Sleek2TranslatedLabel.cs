using System;
using SDG.Framework.Translations;
using SDG.Framework.UI.Components;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002EB RID: 747
	public class Sleek2TranslatedLabel : Sleek2Label
	{
		// Token: 0x0600156B RID: 5483 RVA: 0x000827E8 File Offset: 0x00080BE8
		public Sleek2TranslatedLabel()
		{
			base.name = "Label_Translated";
			this.translationComponent = base.gameObject.AddComponent<TranslatedLabel>();
			this.translationComponent.textComponent = base.textComponent;
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x0008281D File Offset: 0x00080C1D
		// (set) Token: 0x0600156D RID: 5485 RVA: 0x0008282A File Offset: 0x00080C2A
		public TranslatedText translation
		{
			get
			{
				return this.translationComponent.translation;
			}
			set
			{
				this.translationComponent.translation = value;
			}
		}

		// Token: 0x04000BFF RID: 3071
		protected TranslatedLabel translationComponent;
	}
}
