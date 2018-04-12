using System;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.WorkshopUI
{
	// Token: 0x020002B0 RID: 688
	public class UGCUploadPublishedFileButton : Sleek2ImageButton
	{
		// Token: 0x06001405 RID: 5125 RVA: 0x0007FCF8 File Offset: 0x0007E0F8
		public UGCUploadPublishedFileButton(SteamPublished newFile)
		{
			this.file = newFile;
			this.label = new Sleek2Label();
			this.label.transform.anchorMin = new Vector2(0f, 0f);
			this.label.transform.anchorMax = new Vector2(1f, 1f);
			this.label.transform.offsetMin = new Vector2(5f, 5f);
			this.label.transform.offsetMax = new Vector2(-5f, -5f);
			this.label.textComponent.text = string.Concat(new object[]
			{
				this.file.name,
				" [",
				this.file.id,
				"]"
			});
			this.label.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(this.label);
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x0007FE06 File Offset: 0x0007E206
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x0007FE0E File Offset: 0x0007E20E
		public Sleek2Label label { get; protected set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x0007FE17 File Offset: 0x0007E217
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x0007FE1F File Offset: 0x0007E21F
		public SteamPublished file { get; protected set; }
	}
}
