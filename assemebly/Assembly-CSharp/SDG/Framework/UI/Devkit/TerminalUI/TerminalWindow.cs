using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.TerminalUI
{
	// Token: 0x0200028D RID: 653
	public class TerminalWindow : Sleek2Window
	{
		// Token: 0x06001330 RID: 4912 RVA: 0x0007A54C File Offset: 0x0007894C
		public TerminalWindow()
		{
			base.gameObject.name = "Terminal";
			base.tab.label.textComponent.text = "Terminal";
			TerminalWindow.prefab = (UnityEngine.Object.Instantiate(Resources.Load("UI/Terminal")) as GameObject);
			TerminalWindow.prefab.name = "Prefab";
			TerminalWindow.prefab.getRectTransform().SetParent(base.transform, false);
		}

		// Token: 0x04000B03 RID: 2819
		private static GameObject prefab;
	}
}
