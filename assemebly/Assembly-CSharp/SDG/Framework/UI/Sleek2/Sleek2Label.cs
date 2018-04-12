using System;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002D3 RID: 723
	public class Sleek2Label : Sleek2Element
	{
		// Token: 0x060014E4 RID: 5348 RVA: 0x00081530 File Offset: 0x0007F930
		public Sleek2Label()
		{
			base.gameObject.name = "Label";
			this.textComponent = base.gameObject.AddComponent<Text>();
			this.textComponent.font = Sleek2Label.defaultLabelFont;
			this.textComponent.alignment = TextAnchor.MiddleCenter;
			this.textComponent.color = Sleek2Config.lightTextColor;
			this.textComponent.fontSize = Sleek2Config.bodyFontSize;
			this.shadowComponent = base.gameObject.AddComponent<Shadow>();
			this.shadowComponent.effectColor = new Color(0f, 0f, 0f, 0.5f);
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x000815D5 File Offset: 0x0007F9D5
		public static Font defaultLabelFont
		{
			get
			{
				if (Sleek2Label._defaultLabelFont == null)
				{
					Sleek2Label._defaultLabelFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
				}
				return Sleek2Label._defaultLabelFont;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x000815FB File Offset: 0x0007F9FB
		// (set) Token: 0x060014E7 RID: 5351 RVA: 0x00081603 File Offset: 0x0007FA03
		public Text textComponent { get; protected set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0008160C File Offset: 0x0007FA0C
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x00081614 File Offset: 0x0007FA14
		public Shadow shadowComponent { get; protected set; }

		// Token: 0x04000BD9 RID: 3033
		protected static Font _defaultLabelFont;
	}
}
