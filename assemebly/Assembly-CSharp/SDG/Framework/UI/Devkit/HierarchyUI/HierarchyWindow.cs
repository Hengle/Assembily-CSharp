using System;
using SDG.Framework.Devkit;
using SDG.Framework.Translations;
using SDG.Framework.UI.Devkit.InspectorUI;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.HierarchyUI
{
	// Token: 0x0200024E RID: 590
	public class HierarchyWindow : Sleek2Window
	{
		// Token: 0x06001130 RID: 4400 RVA: 0x00070DD4 File Offset: 0x0006F1D4
		public HierarchyWindow()
		{
			base.gameObject.name = "Hierarchy";
			base.tab.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Hierarchy.Title"));
			base.tab.label.translation.format();
			this.itemsView = new Sleek2Scrollview();
			this.itemsView.transform.reset();
			this.itemsView.transform.offsetMin = new Vector2(5f, 5f);
			this.itemsView.transform.offsetMax = new Vector2(-5f, -5f);
			this.itemsView.vertical = true;
			this.itemsPanel = new Sleek2Element();
			this.itemsPanel.name = "Panel";
			VerticalLayoutGroup verticalLayoutGroup = this.itemsPanel.gameObject.AddComponent<VerticalLayoutGroup>();
			verticalLayoutGroup.spacing = 5f;
			verticalLayoutGroup.childControlHeight = false;
			verticalLayoutGroup.childForceExpandHeight = false;
			ContentSizeFitter contentSizeFitter = this.itemsPanel.gameObject.AddComponent<ContentSizeFitter>();
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			this.itemsPanel.transform.reset();
			this.itemsPanel.transform.pivot = new Vector2(0f, 1f);
			this.itemsView.panel = this.itemsPanel;
			this.addElement(this.itemsView);
			LevelHierarchy.itemAdded += this.handleItemAdded;
			LevelHierarchy.itemRemoved += this.handleItemRemoved;
			LevelHierarchy.loaded += this.handleLoaded;
			this.refresh();
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00070F7C File Offset: 0x0006F37C
		protected void refresh()
		{
			this.itemsPanel.clearElements();
			for (int i = 0; i < LevelHierarchy.instance.items.Count; i++)
			{
				IDevkitHierarchyItem newItem = LevelHierarchy.instance.items[i];
				HierarchyItemButton hierarchyItemButton = new HierarchyItemButton(newItem);
				hierarchyItemButton.clicked += this.handleItemButtonClicked;
				this.itemsPanel.addElement(hierarchyItemButton);
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00070FEC File Offset: 0x0006F3EC
		protected void handleItemButtonClicked(Sleek2ImageButton button)
		{
			IDevkitHierarchyItem item = (button as HierarchyItemButton).item;
			InspectorWindow.inspect(item);
			Component component = item as Component;
			if (component != null)
			{
				DevkitSelectionManager.select(new DevkitSelection(component.gameObject, component.GetComponent<Collider>()));
			}
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00071034 File Offset: 0x0006F434
		protected virtual void handleItemAdded(IDevkitHierarchyItem item)
		{
			this.refresh();
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0007103C File Offset: 0x0006F43C
		protected virtual void handleItemRemoved(IDevkitHierarchyItem item)
		{
			this.refresh();
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00071044 File Offset: 0x0006F444
		protected virtual void handleLoaded()
		{
			this.refresh();
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0007104C File Offset: 0x0006F44C
		protected override void triggerDestroyed()
		{
			LevelHierarchy.itemAdded -= this.handleItemAdded;
			LevelHierarchy.itemRemoved -= this.handleItemAdded;
			LevelHierarchy.loaded -= this.handleLoaded;
			base.triggerDestroyed();
		}

		// Token: 0x04000A4B RID: 2635
		protected Sleek2Element itemsPanel;

		// Token: 0x04000A4C RID: 2636
		protected Sleek2Scrollview itemsView;
	}
}
