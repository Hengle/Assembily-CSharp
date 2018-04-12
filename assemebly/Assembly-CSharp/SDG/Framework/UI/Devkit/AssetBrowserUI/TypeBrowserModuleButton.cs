using System;
using SDG.Framework.Modules;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.AssetBrowserUI
{
	// Token: 0x02000298 RID: 664
	public class TypeBrowserModuleButton : Sleek2ImageButton
	{
		// Token: 0x0600138B RID: 5003 RVA: 0x0007CFEC File Offset: 0x0007B3EC
		public TypeBrowserModuleButton(Module newModule, Type[] newTypes)
		{
			this.module = newModule;
			this.types = newTypes;
			base.transform.anchorMin = new Vector2(0f, 1f);
			base.transform.anchorMax = new Vector2(1f, 1f);
			base.transform.pivot = new Vector2(0.5f, 1f);
			base.transform.sizeDelta = new Vector2(0f, 30f);
			Sleek2Label sleek2Label = new Sleek2Label();
			sleek2Label.transform.reset();
			if (this.module != null)
			{
				sleek2Label.textComponent.text = this.module.config.Name;
			}
			else
			{
				sleek2Label.textComponent.text = "Core";
			}
			sleek2Label.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(sleek2Label);
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x0007D0D8 File Offset: 0x0007B4D8
		// (set) Token: 0x0600138D RID: 5005 RVA: 0x0007D0E0 File Offset: 0x0007B4E0
		public Module module { get; protected set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0007D0E9 File Offset: 0x0007B4E9
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x0007D0F1 File Offset: 0x0007B4F1
		public Type[] types { get; protected set; }
	}
}
