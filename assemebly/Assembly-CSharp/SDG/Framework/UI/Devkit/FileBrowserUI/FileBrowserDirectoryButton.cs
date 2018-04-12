using System;
using System.IO;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.FileBrowserUI
{
	// Token: 0x02000247 RID: 583
	public class FileBrowserDirectoryButton : Sleek2ImageButton
	{
		// Token: 0x06001106 RID: 4358 RVA: 0x0006FBA0 File Offset: 0x0006DFA0
		public FileBrowserDirectoryButton(DirectoryInfo newDirectory)
		{
			this.directory = newDirectory;
			this.label = new Sleek2Label();
			this.label.transform.anchorMin = new Vector2(0f, 0f);
			this.label.transform.anchorMax = new Vector2(1f, 1f);
			this.label.transform.offsetMin = new Vector2(5f, 5f);
			this.label.transform.offsetMax = new Vector2(-5f, -5f);
			this.label.textComponent.text = this.directory.Name;
			this.label.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(this.label);
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x0006FC7D File Offset: 0x0006E07D
		// (set) Token: 0x06001108 RID: 4360 RVA: 0x0006FC85 File Offset: 0x0006E085
		public Sleek2Label label { get; protected set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x0006FC8E File Offset: 0x0006E08E
		// (set) Token: 0x0600110A RID: 4362 RVA: 0x0006FC96 File Offset: 0x0006E096
		public DirectoryInfo directory { get; protected set; }
	}
}
