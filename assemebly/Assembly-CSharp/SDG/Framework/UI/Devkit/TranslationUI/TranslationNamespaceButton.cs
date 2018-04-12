using System;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.TranslationUI
{
	// Token: 0x02000295 RID: 661
	public class TranslationNamespaceButton : Sleek2ImageLabelButton
	{
		// Token: 0x06001373 RID: 4979 RVA: 0x0007B9FC File Offset: 0x00079DFC
		public TranslationNamespaceButton(Translation newTranslation)
		{
			base.name = "Namespace";
			this.translation = newTranslation;
			base.label.textComponent.text = this.translation.ns;
			this.layoutComponent = base.gameObject.AddComponent<LayoutElement>();
			this.layoutComponent.preferredHeight = (float)Sleek2Config.bodyHeight;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x0007BA5E File Offset: 0x00079E5E
		// (set) Token: 0x06001375 RID: 4981 RVA: 0x0007BA66 File Offset: 0x00079E66
		public Translation translation { get; protected set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x0007BA6F File Offset: 0x00079E6F
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x0007BA77 File Offset: 0x00079E77
		public LayoutElement layoutComponent { get; protected set; }
	}
}
