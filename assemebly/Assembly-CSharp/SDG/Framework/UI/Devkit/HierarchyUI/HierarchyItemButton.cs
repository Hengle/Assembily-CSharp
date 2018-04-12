using System;
using SDG.Framework.Devkit;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.HierarchyUI
{
	// Token: 0x0200024D RID: 589
	public class HierarchyItemButton : Sleek2ImageButton
	{
		// Token: 0x0600112D RID: 4397 RVA: 0x00070CC4 File Offset: 0x0006F0C4
		public HierarchyItemButton(IDevkitHierarchyItem newItem)
		{
			this.item = newItem;
			base.transform.anchorMin = new Vector2(0f, 1f);
			base.transform.anchorMax = new Vector2(1f, 1f);
			base.transform.pivot = new Vector2(0.5f, 1f);
			base.transform.sizeDelta = new Vector2(0f, 30f);
			Sleek2TranslatedLabel sleek2TranslatedLabel = new Sleek2TranslatedLabel();
			sleek2TranslatedLabel.transform.reset();
			if (this.item is UnityEngine.Object)
			{
				sleek2TranslatedLabel.translation = new TranslatedTextFallback((this.item as UnityEngine.Object).name);
			}
			else
			{
				sleek2TranslatedLabel.translation = this.item.GetType().getTranslatedNameText();
			}
			sleek2TranslatedLabel.translation.format();
			sleek2TranslatedLabel.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(sleek2TranslatedLabel);
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00070DC0 File Offset: 0x0006F1C0
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x00070DC8 File Offset: 0x0006F1C8
		public IDevkitHierarchyItem item { get; protected set; }
	}
}
