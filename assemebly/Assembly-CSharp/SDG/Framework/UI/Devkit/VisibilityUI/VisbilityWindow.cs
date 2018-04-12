using System;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.Translations;
using SDG.Framework.UI.Devkit.InspectorUI;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.VisibilityUI
{
	// Token: 0x0200029D RID: 669
	public class VisbilityWindow : Sleek2Window
	{
		// Token: 0x060013AC RID: 5036 RVA: 0x0007D928 File Offset: 0x0007BD28
		public VisbilityWindow()
		{
			base.name = "Visibility";
			base.tab.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Visibility.Title"));
			base.tab.label.translation.format();
			this.inspector = new Sleek2Inspector();
			this.inspector.transform.anchorMin = new Vector2(0f, 0f);
			this.inspector.transform.anchorMax = new Vector2(1f, 1f);
			this.inspector.transform.pivot = new Vector2(0f, 1f);
			this.inspector.transform.offsetMin = new Vector2(5f, 5f);
			this.inspector.transform.offsetMax = new Vector2(-5f, -5f);
			this.addElement(this.inspector);
			this.inspector.collapseFoldoutsByDefault = true;
			this.inspector.inspect(VisibilityManager.groups);
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0007DA4E File Offset: 0x0007BE4E
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x0007DA56 File Offset: 0x0007BE56
		public Sleek2Inspector inspector { get; protected set; }
	}
}
