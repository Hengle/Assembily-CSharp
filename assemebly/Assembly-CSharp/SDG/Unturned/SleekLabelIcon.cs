using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006F1 RID: 1777
	public class SleekLabelIcon : SleekLabel
	{
		// Token: 0x060032E6 RID: 13030 RVA: 0x00149BC8 File Offset: 0x00147FC8
		public SleekLabelIcon(Texture2D newIcon)
		{
			base.init();
			this.iconImage = new SleekImageTexture();
			this.iconImage.positionOffset_X = 5;
			this.iconImage.positionOffset_Y = -10;
			this.iconImage.positionScale_Y = 0.5f;
			this.iconImage.sizeOffset_X = 20;
			this.iconImage.sizeOffset_Y = 20;
			this.iconImage.texture = newIcon;
			base.add(this.iconImage);
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.calculateContent();
		}

		// Token: 0x17000A33 RID: 2611
		// (set) Token: 0x060032E7 RID: 13031 RVA: 0x00149C66 File Offset: 0x00148066
		public Texture2D icon
		{
			set
			{
				this.iconImage.texture = value;
			}
		}

		// Token: 0x04002293 RID: 8851
		private SleekImageTexture iconImage;
	}
}
