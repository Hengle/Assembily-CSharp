using System;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.ObjectBrowserUI
{
	// Token: 0x02000282 RID: 642
	public class ObjectBrowserAssetButton : Sleek2ImageLabelButton
	{
		// Token: 0x060012DE RID: 4830 RVA: 0x0007892C File Offset: 0x00076D2C
		public ObjectBrowserAssetButton(ObjectAsset newAsset)
		{
			this.asset = newAsset;
			base.transform.anchorMin = new Vector2(0f, 1f);
			base.transform.anchorMax = new Vector2(1f, 1f);
			base.transform.pivot = new Vector2(0.5f, 1f);
			base.transform.sizeDelta = new Vector2(0f, 30f);
			base.label.textComponent.text = this.asset.objectName;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x000789C9 File Offset: 0x00076DC9
		// (set) Token: 0x060012E0 RID: 4832 RVA: 0x000789D1 File Offset: 0x00076DD1
		public ObjectAsset asset { get; protected set; }
	}
}
