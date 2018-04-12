using System;
using SDG.Framework.Debug;
using SDG.Framework.UI.Devkit.FileBrowserUI;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000260 RID: 608
	public class Sleek2FilePathInspector : Sleek2PathInspector
	{
		// Token: 0x060011DB RID: 4571 RVA: 0x00073F81 File Offset: 0x00072381
		public Sleek2FilePathInspector()
		{
			base.name = "File_Path_Inspector";
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00073F94 File Offset: 0x00072394
		protected override void handleBrowseButtonClicked(Sleek2ImageButton button)
		{
			InspectableFilePath inspectableFilePath = (InspectableFilePath)base.inspectable.value;
			FileBrowserContainer fileBrowserContainer = DevkitWindowManager.addContainer<FileBrowserContainer>();
			fileBrowserContainer.transform.anchorMin = new Vector2(0.25f, 0.25f);
			fileBrowserContainer.transform.anchorMax = new Vector2(0.75f, 0.75f);
			fileBrowserContainer.mode = EFileBrowserMode.FILE;
			fileBrowserContainer.searchPattern = inspectableFilePath.extension;
			fileBrowserContainer.selected = new FileBrowserSelectedHandler(this.handlePathSelected);
		}
	}
}
