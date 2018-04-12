using System;
using SDG.Framework.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002B7 RID: 695
	public class Sleek2DockVisualizer : Sleek2Element
	{
		// Token: 0x06001436 RID: 5174 RVA: 0x00080AD8 File Offset: 0x0007EED8
		public Sleek2DockVisualizer()
		{
			base.gameObject.name = "Dock_Visualizer";
			this.imageComponent = base.gameObject.AddComponent<Image>();
			this.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Dock_Visualization");
			this.imageComponent.type = Image.Type.Sliced;
			this.imageComponent.fillCenter = false;
			this.imageComponent.enabled = false;
			this.destination = base.gameObject.AddComponent<DragableWindowDestination>();
			Sleek2DragManager.itemChanged += this.handleDragItemChanged;
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x00080B68 File Offset: 0x0007EF68
		// (set) Token: 0x06001438 RID: 5176 RVA: 0x00080B70 File Offset: 0x0007EF70
		public Image imageComponent { get; protected set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x00080B79 File Offset: 0x0007EF79
		// (set) Token: 0x0600143A RID: 5178 RVA: 0x00080B81 File Offset: 0x0007EF81
		public DragableWindowDestination destination { get; protected set; }

		// Token: 0x0600143B RID: 5179 RVA: 0x00080B8A File Offset: 0x0007EF8A
		protected virtual void handleDragItemChanged()
		{
			if (this.imageComponent == null)
			{
				return;
			}
			this.imageComponent.color = Sleek2Config.dockColor;
			this.imageComponent.enabled = (Sleek2DragManager.item is Sleek2WindowTab);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x00080BC6 File Offset: 0x0007EFC6
		public override void destroy()
		{
			Sleek2DragManager.itemChanged -= this.handleDragItemChanged;
			base.destroy();
		}
	}
}
