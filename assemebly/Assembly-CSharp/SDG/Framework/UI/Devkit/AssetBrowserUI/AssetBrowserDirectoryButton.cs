using System;
using SDG.Framework.UI.Components;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.AssetBrowserUI
{
	// Token: 0x0200022C RID: 556
	public class AssetBrowserDirectoryButton : Sleek2ImageButton
	{
		// Token: 0x0600106F RID: 4207 RVA: 0x0006C07C File Offset: 0x0006A47C
		public AssetBrowserDirectoryButton(AssetDirectory newDirectory)
		{
			this.directory = newDirectory;
			Sleek2Label sleek2Label = new Sleek2Label();
			sleek2Label.transform.reset();
			sleek2Label.textComponent.text = "/" + this.directory.name;
			sleek2Label.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(sleek2Label);
			this.context = base.gameObject.AddComponent<ContextDropdownButton>();
			this.context.element = this;
			this.context.opened += this.handleContextOpened;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0006C112 File Offset: 0x0006A512
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x0006C11A File Offset: 0x0006A51A
		public AssetDirectory directory { get; protected set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0006C123 File Offset: 0x0006A523
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x0006C12B File Offset: 0x0006A52B
		public ContextDropdownButton context { get; protected set; }

		// Token: 0x06001074 RID: 4212 RVA: 0x0006C134 File Offset: 0x0006A534
		protected void handleContextOpened(ContextDropdownButton button, Sleek2HoverDropdown dropdown)
		{
			AssetBrowserContextCreateAssetHandler.handleContextDropdownOpened(dropdown, this.directory);
		}
	}
}
