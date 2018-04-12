using System;
using SDG.Framework.Devkit;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.SaveUI
{
	// Token: 0x02000286 RID: 646
	public class Sleek2Saveable : Sleek2Element
	{
		// Token: 0x060012F7 RID: 4855 RVA: 0x00079410 File Offset: 0x00077810
		public Sleek2Saveable(IDirtyable newDirtyable)
		{
			this.dirtyable = newDirtyable;
			this.toggle = new Sleek2Toggle();
			this.toggle.transform.anchorMin = new Vector2(0f, 0f);
			this.toggle.transform.anchorMax = new Vector2(0f, 0f);
			this.toggle.transform.pivot = new Vector2(0f, 0f);
			this.toggle.transform.sizeDelta = new Vector2((float)Sleek2Config.bodyHeight, (float)Sleek2Config.bodyHeight);
			this.toggle.toggleComponent.isOn = DirtyManager.checkSaveable(this.dirtyable);
			this.toggle.toggled += this.handleToggleToggled;
			this.addElement(this.toggle);
			this.label = new Sleek2Label();
			this.label.transform.anchorMin = new Vector2(0f, 0f);
			this.label.transform.anchorMax = new Vector2(1f, 1f);
			this.label.transform.pivot = new Vector2(0f, 0f);
			this.label.transform.offsetMin = new Vector2((float)(Sleek2Config.bodyHeight + 5), 0f);
			this.label.transform.offsetMax = new Vector2(0f, 0f);
			this.label.textComponent.text = this.dirtyable.ToString();
			this.label.textComponent.alignment = TextAnchor.MiddleLeft;
			this.addElement(this.label);
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x000795D3 File Offset: 0x000779D3
		// (set) Token: 0x060012F9 RID: 4857 RVA: 0x000795DB File Offset: 0x000779DB
		public Sleek2Toggle toggle { get; protected set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x000795E4 File Offset: 0x000779E4
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x000795EC File Offset: 0x000779EC
		public Sleek2Label label { get; protected set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x000795F5 File Offset: 0x000779F5
		// (set) Token: 0x060012FD RID: 4861 RVA: 0x000795FD File Offset: 0x000779FD
		public IDirtyable dirtyable { get; protected set; }

		// Token: 0x060012FE RID: 4862 RVA: 0x00079606 File Offset: 0x00077A06
		protected virtual void handleToggleToggled(Sleek2Toggle toggle, bool isOn)
		{
			DirtyManager.toggleSaveable(this.dirtyable);
		}
	}
}
