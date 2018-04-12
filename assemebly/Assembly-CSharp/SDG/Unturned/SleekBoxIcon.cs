using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006C4 RID: 1732
	public class SleekBoxIcon : SleekBox
	{
		// Token: 0x0600321C RID: 12828 RVA: 0x001467F4 File Offset: 0x00144BF4
		public SleekBoxIcon(Texture2D newIcon, int newSize)
		{
			base.init();
			this.iconImage = new SleekImageTexture();
			this.iconSize = newSize;
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

		// Token: 0x0600321D RID: 12829 RVA: 0x00146890 File Offset: 0x00144C90
		public SleekBoxIcon(Texture2D newIcon)
		{
			base.init();
			this.iconImage = new SleekImageTexture();
			this.iconSize = 0;
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

		// Token: 0x17000A21 RID: 2593
		// (set) Token: 0x0600321E RID: 12830 RVA: 0x00146958 File Offset: 0x00144D58
		public Texture2D icon
		{
			set
			{
				this.iconImage.texture = value;
				if (this.iconSize == 0 && this.iconImage.texture != null)
				{
					this.iconImage.sizeOffset_X = this.iconImage.texture.width;
					this.iconImage.sizeOffset_Y = this.iconImage.texture.height;
				}
			}
		}

		// Token: 0x04002235 RID: 8757
		public SleekImageTexture iconImage;

		// Token: 0x04002236 RID: 8758
		private int iconSize;
	}
}
