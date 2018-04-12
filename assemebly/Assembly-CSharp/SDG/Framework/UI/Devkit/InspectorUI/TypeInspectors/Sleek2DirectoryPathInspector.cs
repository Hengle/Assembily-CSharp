using System;
using SDG.Framework.Debug;
using SDG.Framework.UI.Devkit.FileBrowserUI;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x0200025F RID: 607
	public class Sleek2DirectoryPathInspector : Sleek2PathInspector
	{
		// Token: 0x060011D9 RID: 4569 RVA: 0x00073EF5 File Offset: 0x000722F5
		public Sleek2DirectoryPathInspector()
		{
			base.name = "Directory_Path_Inspector";
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00073F08 File Offset: 0x00072308
		protected override void handleBrowseButtonClicked(Sleek2ImageButton button)
		{
			InspectableDirectoryPath inspectableDirectoryPath = (InspectableDirectoryPath)base.inspectable.value;
			FileBrowserContainer fileBrowserContainer = DevkitWindowManager.addContainer<FileBrowserContainer>();
			fileBrowserContainer.transform.anchorMin = new Vector2(0.25f, 0.25f);
			fileBrowserContainer.transform.anchorMax = new Vector2(0.75f, 0.75f);
			fileBrowserContainer.mode = EFileBrowserMode.DIRECTORY;
			fileBrowserContainer.searchPattern = null;
			fileBrowserContainer.selected = new FileBrowserSelectedHandler(this.handlePathSelected);
		}
	}
}
