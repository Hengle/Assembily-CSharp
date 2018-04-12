using System;
using System.IO;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.FileBrowserUI
{
	// Token: 0x02000248 RID: 584
	public class FileBrowserFileButton : Sleek2ImageButton
	{
		// Token: 0x0600110B RID: 4363 RVA: 0x0006FCA0 File Offset: 0x0006E0A0
		public FileBrowserFileButton(FileInfo newFile)
		{
			this.file = newFile;
			this.label = new Sleek2Label();
			this.label.transform.anchorMin = new Vector2(0f, 0f);
			this.label.transform.anchorMax = new Vector2(1f, 1f);
			this.label.transform.offsetMin = new Vector2(5f, 5f);
			this.label.transform.offsetMax = new Vector2(-5f, -5f);
			this.label.textComponent.text = this.file.Name;
			this.label.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(this.label);
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x0006FD7D File Offset: 0x0006E17D
		// (set) Token: 0x0600110D RID: 4365 RVA: 0x0006FD85 File Offset: 0x0006E185
		public Sleek2Label label { get; protected set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x0006FD8E File Offset: 0x0006E18E
		// (set) Token: 0x0600110F RID: 4367 RVA: 0x0006FD96 File Offset: 0x0006E196
		public FileInfo file { get; protected set; }
	}
}
