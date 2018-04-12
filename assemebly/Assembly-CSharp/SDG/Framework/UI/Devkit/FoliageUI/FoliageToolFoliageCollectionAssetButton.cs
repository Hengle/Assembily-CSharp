using System;
using SDG.Framework.Foliage;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.FoliageUI
{
	// Token: 0x0200024A RID: 586
	public class FoliageToolFoliageCollectionAssetButton : Sleek2ImageButton
	{
		// Token: 0x06001113 RID: 4371 RVA: 0x0006FE74 File Offset: 0x0006E274
		public FoliageToolFoliageCollectionAssetButton(FoliageInfoCollectionAsset newAsset)
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

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x0006FF34 File Offset: 0x0006E334
		// (set) Token: 0x06001115 RID: 4373 RVA: 0x0006FF3C File Offset: 0x0006E33C
		public FoliageInfoCollectionAsset asset { get; protected set; }
	}
}
