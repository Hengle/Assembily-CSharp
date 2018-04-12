using System;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Components
{
	// Token: 0x02000221 RID: 545
	public class HoverTranslatedLabelTooltip : HoverTooltip
	{
		// Token: 0x06001023 RID: 4131 RVA: 0x0006A38C File Offset: 0x0006878C
		protected override Sleek2Element triggerBeginTooltip()
		{
			Sleek2TranslatedLabel sleek2TranslatedLabel = new Sleek2TranslatedLabel();
			sleek2TranslatedLabel.transform.anchorMin = Vector2.zero;
			sleek2TranslatedLabel.transform.anchorMax = Vector2.zero;
			sleek2TranslatedLabel.transform.pivot = new Vector2(0f, 1f);
			sleek2TranslatedLabel.transform.sizeDelta = new Vector2(200f, 50f);
			sleek2TranslatedLabel.textComponent.alignment = TextAnchor.UpperLeft;
			sleek2TranslatedLabel.translation = this.translation;
			sleek2TranslatedLabel.translation.format();
			DevkitCanvas.tooltip = sleek2TranslatedLabel;
			return sleek2TranslatedLabel;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0006A41E File Offset: 0x0006881E
		protected override void triggerEndTooltip(Sleek2Element element)
		{
			UnityEngine.Object.Destroy(element.gameObject);
			DevkitCanvas.tooltip = null;
		}

		// Token: 0x040009C3 RID: 2499
		public TranslatedText translation;
	}
}
