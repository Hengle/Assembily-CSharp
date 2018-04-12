using System;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.LandscapeUI
{
	// Token: 0x0200027C RID: 636
	public class LandscapeToolRemoveMaterialButton : Sleek2ImageButton
	{
		// Token: 0x060012A9 RID: 4777 RVA: 0x00076F38 File Offset: 0x00075338
		public LandscapeToolRemoveMaterialButton()
		{
			base.transform.anchorMin = new Vector2(0f, 1f);
			base.transform.anchorMax = new Vector2(1f, 1f);
			base.transform.pivot = new Vector2(0.5f, 1f);
			base.transform.sizeDelta = new Vector2(0f, 30f);
			this.label = new Sleek2Label();
			this.label.transform.reset();
			this.label.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(this.label);
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x00076FEF File Offset: 0x000753EF
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x00076FF7 File Offset: 0x000753F7
		public int layer { get; protected set; }

		// Token: 0x060012AC RID: 4780 RVA: 0x00077000 File Offset: 0x00075400
		public void update(int newLayer, AssetReference<LandscapeMaterialAsset> newAsset)
		{
			this.layer = newLayer;
			LandscapeMaterialAsset landscapeMaterialAsset = Assets.find<LandscapeMaterialAsset>(newAsset);
			if (landscapeMaterialAsset != null)
			{
				this.label.textComponent.text = landscapeMaterialAsset.name;
			}
			else
			{
				this.label.textComponent.text = "---";
			}
		}

		// Token: 0x04000AB5 RID: 2741
		protected Sleek2Label label;
	}
}
