using System;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.AssetBrowserUI
{
	// Token: 0x0200022D RID: 557
	public class AssetBrowserRootDirectoryButton : Sleek2ImageButton
	{
		// Token: 0x06001075 RID: 4213 RVA: 0x0006C144 File Offset: 0x0006A544
		public AssetBrowserRootDirectoryButton(RootAssetDirectory newDirectory)
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

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0006C204 File Offset: 0x0006A604
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x0006C20C File Offset: 0x0006A60C
		public RootAssetDirectory directory { get; protected set; }
	}
}
