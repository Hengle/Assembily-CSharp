using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006CA RID: 1738
	public class SleekButtonIcon : SleekButton
	{
		// Token: 0x06003232 RID: 12850 RVA: 0x001469C8 File Offset: 0x00144DC8
		public SleekButtonIcon(Texture2D newIcon, int newSize)
		{
			base.init();
			this.backgroundTint = ESleekTint.BACKGROUND;
			this.iconImage = new SleekImageTexture();
			this.iconSize = newSize;
			this.iconScale = false;
			this.iconImage.positionOffset_X = 5;
			this.iconImage.positionOffset_Y = 5;
			this.iconImage.sizeOffset_X = this.iconSize;
			this.iconImage.sizeOffset_Y = this.iconSize;
			this.iconImage.texture = newIcon;
			base.add(this.iconImage);
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.calculateContent();
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x00146A74 File Offset: 0x00144E74
		public SleekButtonIcon(Texture2D newIcon)
		{
			base.init();
			this.backgroundTint = ESleekTint.BACKGROUND;
			this.iconImage = new SleekImageTexture();
			this.iconSize = 0;
			this.iconScale = false;
			this.iconImage.positionOffset_X = 5;
			this.iconImage.positionOffset_Y = 5;
			this.iconImage.texture = newIcon;
			base.add(this.iconImage);
			if (this.iconImage.texture != null)
			{
				this.iconImage.sizeOffset_X = this.iconImage.texture.width;
				this.iconImage.sizeOffset_Y = this.iconImage.texture.height;
			}
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.calculateContent();
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x00146B48 File Offset: 0x00144F48
		public SleekButtonIcon(Texture2D newIcon, bool newScale)
		{
			base.init();
			this.backgroundTint = ESleekTint.BACKGROUND;
			this.iconImage = new SleekImageTexture();
			this.iconSize = 0;
			this.iconScale = newScale;
			this.iconImage.positionOffset_X = 5;
			this.iconImage.positionOffset_Y = 5;
			if (this.iconScale)
			{
				this.iconImage.sizeOffset_X = -10;
				this.iconImage.sizeOffset_Y = -10;
				this.iconImage.sizeScale_X = 1f;
				this.iconImage.sizeScale_Y = 1f;
			}
			this.iconImage.texture = newIcon;
			base.add(this.iconImage);
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.calculateContent();
		}

		// Token: 0x17000A22 RID: 2594
		// (set) Token: 0x06003235 RID: 12853 RVA: 0x00146C18 File Offset: 0x00145018
		public Texture2D icon
		{
			set
			{
				this.iconImage.texture = value;
				if (this.iconSize == 0 && !this.iconScale && this.iconImage.texture != null)
				{
					this.iconImage.sizeOffset_X = this.iconImage.texture.width;
					this.iconImage.sizeOffset_Y = this.iconImage.texture.height;
				}
			}
		}

		// Token: 0x0400223D RID: 8765
		public SleekImageTexture iconImage;

		// Token: 0x0400223E RID: 8766
		private int iconSize;

		// Token: 0x0400223F RID: 8767
		private bool iconScale;
	}
}
