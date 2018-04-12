using System;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.ContentBrowserUI
{
	// Token: 0x02000232 RID: 562
	public class ContentBrowserRootDirectoryButton : Sleek2ImageButton
	{
		// Token: 0x0600108F RID: 4239 RVA: 0x0006CE4C File Offset: 0x0006B24C
		public ContentBrowserRootDirectoryButton(RootContentDirectory newDirectory)
		{
			this.directory = newDirectory;
			base.transform.anchorMin = new Vector2(0f, 1f);
			base.transform.anchorMax = new Vector2(1f, 1f);
			base.transform.pivot = new Vector2(0.5f, 1f);
			base.transform.sizeDelta = new Vector2(0f, 30f);
			Sleek2Label sleek2Label = new Sleek2Label();
			sleek2Label.transform.reset();
			sleek2Label.textComponent.text = this.directory.name;
			sleek2Label.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(sleek2Label);
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x0006CF0C File Offset: 0x0006B30C
		// (set) Token: 0x06001091 RID: 4241 RVA: 0x0006CF14 File Offset: 0x0006B314
		public RootContentDirectory directory { get; protected set; }
	}
}
