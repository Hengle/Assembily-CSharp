using System;
using SDG.Framework.Foliage;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.FoliageUI
{
	// Token: 0x02000249 RID: 585
	public class FoliageToolFoliageAssetButton : Sleek2ImageButton
	{
		// Token: 0x06001110 RID: 4368 RVA: 0x0006FDA0 File Offset: 0x0006E1A0
		public FoliageToolFoliageAssetButton(FoliageInfoAsset newAsset)
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

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x0006FE60 File Offset: 0x0006E260
		// (set) Token: 0x06001112 RID: 4370 RVA: 0x0006FE68 File Offset: 0x0006E268
		public FoliageInfoAsset asset { get; protected set; }
	}
}
