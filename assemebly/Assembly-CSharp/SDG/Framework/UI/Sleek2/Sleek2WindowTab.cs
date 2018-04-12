using System;
using SDG.Framework.UI.Components;
using SDG.Framework.UI.Devkit;
using UnityEngine;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002FC RID: 764
	public class Sleek2WindowTab : Sleek2ImageButton
	{
		// Token: 0x060015F1 RID: 5617 RVA: 0x00083C9C File Offset: 0x0008209C
		public Sleek2WindowTab(Sleek2Window window)
		{
			this.window = window;
			base.name = "Tab";
			base.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Tab");
			base.transform.anchorMin = Vector2.zero;
			base.transform.anchorMax = new Vector2(0f, 1f);
			base.transform.pivot = new Vector2(0f, 0.5f);
			base.transform.sizeDelta = new Vector2((float)Sleek2Config.tabWidth, 0f);
			this.label = new Sleek2TranslatedLabel();
			this.label.transform.anchorMin = Vector2.zero;
			this.label.transform.anchorMax = Vector2.one;
			this.label.transform.sizeDelta = Vector2.zero;
			this.addElement(this.label);
			this.dragable = base.gameObject.AddComponent<DragableTab>();
			this.dragable.target = base.transform;
			this.dragable.source = this;
			this.dragable.popoutTab += this.handlePopoutTab;
			this.context = base.gameObject.AddComponent<ContextDropdownButton>();
			this.context.element = this;
			this.context.opened += this.handleContextDropdownOpened;
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x00083E06 File Offset: 0x00082206
		// (set) Token: 0x060015F3 RID: 5619 RVA: 0x00083E0E File Offset: 0x0008220E
		public Sleek2Window window { get; protected set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x00083E17 File Offset: 0x00082217
		// (set) Token: 0x060015F5 RID: 5621 RVA: 0x00083E1F File Offset: 0x0008221F
		public Sleek2TranslatedLabel label { get; protected set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00083E28 File Offset: 0x00082228
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x00083E30 File Offset: 0x00082230
		public DragableTab dragable { get; protected set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00083E39 File Offset: 0x00082239
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x00083E41 File Offset: 0x00082241
		public ContextDropdownButton context { get; protected set; }

		// Token: 0x060015FA RID: 5626 RVA: 0x00083E4C File Offset: 0x0008224C
		protected virtual void handlePopoutTab(DragableTab tab, Vector2 position)
		{
			this.window.dock.removeWindow(this.window);
			position.x /= (float)Screen.width;
			position.y /= (float)Screen.height;
			Sleek2PopoutWindowContainer sleek2PopoutWindowContainer = DevkitWindowManager.addContainer<Sleek2PopoutWindowContainer>();
			sleek2PopoutWindowContainer.transform.anchorMin = new Vector2(Mathf.Max(position.x - 0.25f, 0f), Mathf.Max(position.y - 0.25f, 0f));
			sleek2PopoutWindowContainer.transform.anchorMax = new Vector2(Mathf.Min(position.x + 0.25f, 1f), Mathf.Min(position.y + 0.25f, 1f));
			sleek2PopoutWindowContainer.partition.addWindow(this.window);
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00083F2B File Offset: 0x0008232B
		protected virtual void handleCloseButtonClicked(Sleek2ImageButton button)
		{
			this.window.dock.removeWindow(this.window);
			this.window.destroy();
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00083F50 File Offset: 0x00082350
		protected virtual void handleContextDropdownOpened(ContextDropdownButton button, Sleek2HoverDropdown dropdown)
		{
			Sleek2DropdownButtonTemplate sleek2DropdownButtonTemplate = new Sleek2DropdownButtonTemplate();
			sleek2DropdownButtonTemplate.label.textComponent.text = "Close";
			sleek2DropdownButtonTemplate.clicked += this.handleCloseButtonClicked;
			dropdown.addElement(sleek2DropdownButtonTemplate);
		}
	}
}
