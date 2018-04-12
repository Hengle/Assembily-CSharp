using System;
using SDG.Framework.Debug;
using SDG.Framework.UI.Devkit.FileBrowserUI;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000262 RID: 610
	public abstract class Sleek2PathInspector : Sleek2KeyValueInspector
	{
		// Token: 0x060011E3 RID: 4579 RVA: 0x00073CB4 File Offset: 0x000720B4
		public Sleek2PathInspector()
		{
			base.name = "Path_Inspector";
			this.pathButton = new Sleek2ImageLabelButton();
			this.pathButton.transform.anchorMin = new Vector2(0f, 0f);
			this.pathButton.transform.anchorMax = new Vector2(1f, 1f);
			this.pathButton.transform.offsetMin = new Vector2(0f, 0f);
			this.pathButton.transform.offsetMax = new Vector2(-50f, 0f);
			base.valuePanel.addElement(this.pathButton);
			this.browseButton = new Sleek2ImageLabelButton();
			this.browseButton.transform.anchorMin = new Vector2(1f, 0f);
			this.browseButton.transform.anchorMax = new Vector2(1f, 1f);
			this.browseButton.transform.offsetMin = new Vector2(-50f, 0f);
			this.browseButton.transform.offsetMax = new Vector2(0f, 0f);
			this.browseButton.label.textComponent.text = "...";
			this.browseButton.clicked += this.handleBrowseButtonClicked;
			base.valuePanel.addElement(this.browseButton);
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x00073E34 File Offset: 0x00072234
		// (set) Token: 0x060011E5 RID: 4581 RVA: 0x00073E3C File Offset: 0x0007223C
		public Sleek2ImageLabelButton pathButton { get; protected set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x00073E45 File Offset: 0x00072245
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x00073E4D File Offset: 0x0007224D
		public Sleek2ImageLabelButton browseButton { get; protected set; }

		// Token: 0x060011E8 RID: 4584 RVA: 0x00073E56 File Offset: 0x00072256
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00073E6C File Offset: 0x0007226C
		public override void refresh()
		{
			if (base.inspectable == null || !base.inspectable.canRead)
			{
				return;
			}
			IInspectablePath inspectablePath = (IInspectablePath)base.inspectable.value;
			this.pathButton.label.textComponent.text = inspectablePath.absolutePath;
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00073EC4 File Offset: 0x000722C4
		protected virtual void handlePathSelected(FileBrowserContainer container, string absolutePath)
		{
			IInspectablePath inspectablePath = (IInspectablePath)base.inspectable.value;
			inspectablePath.absolutePath = absolutePath;
			base.inspectable.value = inspectablePath;
		}

		// Token: 0x060011EB RID: 4587
		protected abstract void handleBrowseButtonClicked(Sleek2ImageButton button);
	}
}
