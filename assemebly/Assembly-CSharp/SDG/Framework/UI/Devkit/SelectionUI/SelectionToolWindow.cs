using System;
using SDG.Framework.Devkit;
using SDG.Framework.Devkit.Tools;
using SDG.Framework.Translations;
using SDG.Framework.UI.Devkit.InspectorUI;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.SelectionUI
{
	// Token: 0x02000287 RID: 647
	public class SelectionToolWindow : Sleek2Window
	{
		// Token: 0x060012FF RID: 4863 RVA: 0x00079614 File Offset: 0x00077A14
		public SelectionToolWindow()
		{
			base.gameObject.name = "Selection_Tool";
			base.tab.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Selection_Tool.Title"));
			base.tab.label.translation.format();
			this.inspector = new Sleek2Inspector();
			this.inspector.transform.anchorMin = new Vector2(0f, 0f);
			this.inspector.transform.anchorMax = new Vector2(1f, 1f);
			this.inspector.transform.pivot = new Vector2(0f, 1f);
			this.inspector.transform.offsetMin = new Vector2(5f, 5f);
			this.inspector.transform.offsetMax = new Vector2(-5f, -5f);
			this.addElement(this.inspector);
			this.inspector.inspect(DevkitSelectionToolOptions.instance);
			DevkitHotkeys.registerTool(1, this);
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x0007973A File Offset: 0x00077B3A
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x00079742 File Offset: 0x00077B42
		public Sleek2Inspector inspector { get; protected set; }

		// Token: 0x06001302 RID: 4866 RVA: 0x0007974C File Offset: 0x00077B4C
		protected override void triggerFocused()
		{
			if (DevkitEquipment.instance != null)
			{
				if (this.isActive)
				{
					DevkitEquipment.instance.equip(Activator.CreateInstance(typeof(DevkitSelectionTool)) as IDevkitTool);
				}
				else
				{
					DevkitEquipment.instance.dequip();
				}
			}
			base.triggerFocused();
		}
	}
}
