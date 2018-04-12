using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006D1 RID: 1745
	public class SleekChat : Sleek
	{
		// Token: 0x06003251 RID: 12881 RVA: 0x00147580 File Offset: 0x00145980
		public SleekChat()
		{
			base.init();
			this.avatarImage = new SleekImageTexture();
			this.avatarImage.positionOffset_Y = 4;
			this.avatarImage.sizeOffset_X = 32;
			this.avatarImage.sizeOffset_Y = 32;
			this.avatarImage.shouldDestroyTexture = true;
			base.add(this.avatarImage);
			this.repImage = new SleekImageTexture();
			this.repImage.positionOffset_X = 37;
			this.repImage.positionOffset_Y = 4;
			this.repImage.sizeOffset_X = 32;
			this.repImage.sizeOffset_Y = 32;
			base.add(this.repImage);
			this.msg = new SleekLabel();
			this.msg.positionOffset_X = 74;
			this.msg.sizeOffset_X = 400;
			this.msg.sizeOffset_Y = 40;
			this.msg.fontSize = 14;
			this.msg.fontAlignment = TextAnchor.UpperLeft;
			this.msg.foregroundTint = ESleekTint.NONE;
			base.add(this.msg);
		}

		// Token: 0x0400224C RID: 8780
		public SleekImageTexture avatarImage;

		// Token: 0x0400224D RID: 8781
		public SleekImageTexture repImage;

		// Token: 0x0400224E RID: 8782
		public SleekLabel msg;
	}
}
