using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002F9 RID: 761
	public class Sleek2WindowDock : Sleek2Partition
	{
		// Token: 0x060015C0 RID: 5568 RVA: 0x00082DB4 File Offset: 0x000811B4
		public Sleek2WindowDock(Sleek2WindowPartition partition)
		{
			this.partition = partition;
			base.name = "Window_Dock";
			this.windows = new List<Sleek2Window>();
			this.dock = new Sleek2WindowTabDock();
			this.dock.destination.tabDocked += this.handleTabDocked;
			this.dock.destination.dock = this;
			this.addElement(this.dock);
			this.panel = new Sleek2Element();
			this.panel.name = "Panel";
			this.panel.transform.pivot = Vector2.zero;
			this.panel.transform.anchorMin = Vector2.zero;
			this.panel.transform.anchorMax = Vector2.one;
			this.panel.transform.offsetMin = Vector2.zero;
			this.panel.transform.offsetMax = new Vector2(0f, -this.dock.transform.sizeDelta.y);
			this.addElement(this.panel);
			this.dockVisualization = new Sleek2DockVisualizer();
			this.dockVisualization.transform.pivot = Vector2.zero;
			this.dockVisualization.transform.anchorMin = Vector2.zero;
			this.dockVisualization.transform.anchorMax = Vector2.one;
			this.dockVisualization.transform.offsetMin = Vector2.zero;
			this.dockVisualization.transform.offsetMax = new Vector2(0f, -this.dock.transform.sizeDelta.y);
			this.dockVisualization.destination.windowDocked += this.handleWindowDocked;
			this.dockVisualization.destination.dock = this;
			this.addElement(this.dockVisualization);
		}

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x060015C1 RID: 5569 RVA: 0x00082FA4 File Offset: 0x000813A4
		// (remove) Token: 0x060015C2 RID: 5570 RVA: 0x00082FDC File Offset: 0x000813DC
		public event DockedWindowAddedHandler dockedWindowAdded;

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x060015C3 RID: 5571 RVA: 0x00083014 File Offset: 0x00081414
		// (remove) Token: 0x060015C4 RID: 5572 RVA: 0x0008304C File Offset: 0x0008144C
		public event DockedWindowRemovedHandler dockedWindowRemoved;

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x00083082 File Offset: 0x00081482
		// (set) Token: 0x060015C6 RID: 5574 RVA: 0x0008308A File Offset: 0x0008148A
		public List<Sleek2Window> windows { get; protected set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x00083093 File Offset: 0x00081493
		// (set) Token: 0x060015C8 RID: 5576 RVA: 0x0008309B File Offset: 0x0008149B
		public Sleek2WindowTabDock dock { get; protected set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x000830A4 File Offset: 0x000814A4
		// (set) Token: 0x060015CA RID: 5578 RVA: 0x000830AC File Offset: 0x000814AC
		public Sleek2Element panel { get; protected set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x000830B5 File Offset: 0x000814B5
		// (set) Token: 0x060015CC RID: 5580 RVA: 0x000830BD File Offset: 0x000814BD
		public Sleek2DockVisualizer dockVisualization { get; protected set; }

		// Token: 0x060015CD RID: 5581 RVA: 0x000830C6 File Offset: 0x000814C6
		public void shiftWindow(Sleek2Window window, int insertIndex)
		{
			this.windows.Remove(window);
			insertIndex = Mathf.Clamp(insertIndex, 0, this.windows.Count);
			this.windows.Insert(insertIndex, window);
			this.updateTabs(insertIndex);
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00083100 File Offset: 0x00081500
		public void addWindow(Sleek2Window window)
		{
			int count = this.windows.Count;
			this.addWindow(window, count);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00083124 File Offset: 0x00081524
		public void addWindow(Sleek2Window window, int insertIndex)
		{
			insertIndex = Mathf.Clamp(insertIndex, 0, this.windows.Count);
			window.dock = this;
			window.transform.reset();
			this.dock.addElement(window.tab);
			this.windows.Insert(insertIndex, window);
			this.panel.addElement(window);
			window.tab.clicked += this.handleTabClicked;
			window.activityChanged += this.handleActivityChanged;
			this.updateTabs(insertIndex);
			this.triggerDockedWindowAdded(window);
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x000831BC File Offset: 0x000815BC
		public void removeWindow(Sleek2Window window)
		{
			window.dock = null;
			this.panel.removeElement(window);
			this.dock.removeElement(window.tab);
			this.windows.Remove(window);
			window.tab.clicked -= this.handleTabClicked;
			window.activityChanged -= this.handleActivityChanged;
			this.updateTabs(0);
			this.triggerDockedWindowRemoved(window);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00083234 File Offset: 0x00081634
		protected virtual void updateTabs(int activeIndex)
		{
			for (int i = 0; i < this.windows.Count; i++)
			{
				this.windows[i].isActive = (i == activeIndex);
				this.windows[i].tab.transform.anchoredPosition = new Vector2((float)i * ((float)Sleek2Config.tabWidth * 0.9f), 0f);
			}
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x000832A6 File Offset: 0x000816A6
		protected virtual void handleTabClicked(Sleek2ImageButton button)
		{
			(button as Sleek2WindowTab).window.isActive = true;
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x000832BC File Offset: 0x000816BC
		protected virtual void handleActivityChanged(Sleek2Window window)
		{
			if (!window.isActive)
			{
				return;
			}
			for (int i = 0; i < this.windows.Count; i++)
			{
				this.windows[i].isActive = (this.windows[i] == window);
			}
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00083314 File Offset: 0x00081714
		protected virtual void handleTabDocked(Sleek2WindowDock dock, Sleek2WindowTab tab, float offset)
		{
			int insertIndex = (int)(offset / ((float)Sleek2Config.tabWidth * 0.9f));
			if (tab.window.dock == dock)
			{
				this.shiftWindow(tab.window, insertIndex);
			}
			else
			{
				tab.window.dock.removeWindow(tab.window);
				dock.addWindow(tab.window, insertIndex);
			}
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00083378 File Offset: 0x00081778
		protected virtual void handleWindowDocked(Sleek2WindowDock dock, Sleek2WindowTab tab, ESleek2PartitionDirection direction)
		{
			Sleek2WindowPartition sleek2WindowPartition;
			Sleek2WindowPartition sleek2WindowPartition2;
			this.partition.split(direction, out sleek2WindowPartition, out sleek2WindowPartition2);
			tab.window.dock.removeWindow(tab.window);
			sleek2WindowPartition2.dock.addWindow(tab.window);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x000833BC File Offset: 0x000817BC
		protected virtual void triggerDockedWindowAdded(Sleek2Window window)
		{
			if (this.dockedWindowAdded != null)
			{
				this.dockedWindowAdded(this, window);
			}
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x000833D6 File Offset: 0x000817D6
		protected virtual void triggerDockedWindowRemoved(Sleek2Window window)
		{
			if (this.dockedWindowRemoved != null)
			{
				this.dockedWindowRemoved(this, window);
			}
		}

		// Token: 0x04000C0D RID: 3085
		public Sleek2WindowPartition partition;
	}
}
