using System;
using SDG.Framework.UI.Components;
using UnityEngine;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002E4 RID: 740
	public class Sleek2Titlebar : Sleek2Element
	{
		// Token: 0x0600154E RID: 5454 RVA: 0x0008227C File Offset: 0x0008067C
		public Sleek2Titlebar()
		{
			base.name = "Titlebar";
			base.transform.reset();
			this.titleLabel = new Sleek2TranslatedLabel();
			this.titleLabel.transform.reset();
			this.titleLabel.transform.offsetMin = new Vector2(5f, 0f);
			this.titleLabel.transform.offsetMax = new Vector2((float)(-5 - Sleek2Config.bodyHeight), 0f);
			this.titleLabel.textComponent.alignment = TextAnchor.MiddleLeft;
			this.addElement(this.titleLabel);
			this.exitButton = new Sleek2ImageButton();
			this.exitButton.transform.anchorMin = Vector2.one;
			this.exitButton.transform.anchorMax = Vector2.one;
			this.exitButton.transform.pivot = Vector2.one;
			this.exitButton.transform.sizeDelta = new Vector2((float)Sleek2Config.bodyHeight, (float)Sleek2Config.bodyHeight);
			this.exitButton.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Exit");
			this.addElement(this.exitButton);
			this.dragableComponent = base.gameObject.AddComponent<DragablePopoutContainer>();
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x000823C1 File Offset: 0x000807C1
		// (set) Token: 0x06001550 RID: 5456 RVA: 0x000823C9 File Offset: 0x000807C9
		public Sleek2TranslatedLabel titleLabel { get; protected set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x000823D2 File Offset: 0x000807D2
		// (set) Token: 0x06001552 RID: 5458 RVA: 0x000823DA File Offset: 0x000807DA
		public Sleek2ImageButton exitButton { get; protected set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x000823E3 File Offset: 0x000807E3
		// (set) Token: 0x06001554 RID: 5460 RVA: 0x000823EB File Offset: 0x000807EB
		public DragablePopoutContainer dragableComponent { get; protected set; }
	}
}
