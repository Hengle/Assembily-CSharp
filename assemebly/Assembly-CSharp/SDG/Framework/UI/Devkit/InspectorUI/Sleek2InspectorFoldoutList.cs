using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI
{
	// Token: 0x02000259 RID: 601
	public class Sleek2InspectorFoldoutList : Sleek2InspectorFoldout
	{
		// Token: 0x060011A0 RID: 4512 RVA: 0x0007290C File Offset: 0x00070D0C
		public Sleek2InspectorFoldoutList(int newIndex)
		{
			this.index = newIndex;
			base.name = "Foldout_List";
			base.label.transform.offsetMax = new Vector2((float)(-(float)Sleek2Config.bodyHeight - 5), 0f);
			this.removeButton = new Sleek2ImageLabelButton();
			this.removeButton.transform.anchorMin = new Vector2(1f, 1f);
			this.removeButton.transform.anchorMax = new Vector2(1f, 1f);
			this.removeButton.transform.pivot = new Vector2(1f, 1f);
			this.removeButton.transform.sizeDelta = new Vector2((float)Sleek2Config.bodyHeight, (float)Sleek2Config.bodyHeight);
			this.removeButton.label.textComponent.text = "-";
			base.title.addElement(this.removeButton);
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x00072A08 File Offset: 0x00070E08
		// (set) Token: 0x060011A2 RID: 4514 RVA: 0x00072A10 File Offset: 0x00070E10
		public int index { get; protected set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x00072A19 File Offset: 0x00070E19
		// (set) Token: 0x060011A4 RID: 4516 RVA: 0x00072A21 File Offset: 0x00070E21
		public Sleek2ImageLabelButton removeButton { get; protected set; }
	}
}
