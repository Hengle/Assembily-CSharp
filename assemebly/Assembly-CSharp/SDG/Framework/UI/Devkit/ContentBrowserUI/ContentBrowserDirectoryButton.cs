using System;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.ContentBrowserUI
{
	// Token: 0x02000230 RID: 560
	public class ContentBrowserDirectoryButton : Sleek2ImageButton
	{
		// Token: 0x06001087 RID: 4231 RVA: 0x0006CCD4 File Offset: 0x0006B0D4
		public ContentBrowserDirectoryButton(ContentDirectory newDirectory)
		{
			this.directory = newDirectory;
			Sleek2Label sleek2Label = new Sleek2Label();
			sleek2Label.transform.reset();
			sleek2Label.textComponent.text = "/" + this.directory.name;
			sleek2Label.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(sleek2Label);
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x0006CD36 File Offset: 0x0006B136
		// (set) Token: 0x06001089 RID: 4233 RVA: 0x0006CD3E File Offset: 0x0006B13E
		public ContentDirectory directory { get; protected set; }
	}
}
