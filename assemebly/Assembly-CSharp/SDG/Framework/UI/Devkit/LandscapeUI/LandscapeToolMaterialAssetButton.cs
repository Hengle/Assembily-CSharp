using System;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.LandscapeUI
{
	// Token: 0x0200027B RID: 635
	public class LandscapeToolMaterialAssetButton : Sleek2ImageButton
	{
		// Token: 0x060012A6 RID: 4774 RVA: 0x00076E64 File Offset: 0x00075264
		public LandscapeToolMaterialAssetButton(LandscapeMaterialAsset newAsset)
		{
			this.asset = newAsset;
			base.transform.anchorMin = new Vector2(0f, 1f);
			base.transform.anchorMax = new Vector2(1f, 1f);
			base.transform.pivot = new Vector2(0.5f, 1f);
			base.transform.sizeDelta = new Vector2(0f, 30f);
			Sleek2Label sleek2Label = new Sleek2Label();
			sleek2Label.transform.reset();
			sleek2Label.textComponent.text = this.asset.name;
			sleek2Label.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(sleek2Label);
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x00076F24 File Offset: 0x00075324
		// (set) Token: 0x060012A8 RID: 4776 RVA: 0x00076F2C File Offset: 0x0007532C
		public LandscapeMaterialAsset asset { get; protected set; }
	}
}
