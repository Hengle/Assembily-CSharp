using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006DF RID: 1759
	public class SleekImageButton : Sleek
	{
		// Token: 0x0600328D RID: 12941 RVA: 0x00148035 File Offset: 0x00146435
		public SleekImageButton()
		{
			base.init();
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x00148044 File Offset: 0x00146444
		public override void draw(bool ignoreCulling)
		{
			if (!this.isHidden)
			{
				if (SleekRender.drawImageButton(base.frame, this.texture, base.backgroundColor))
				{
					if (this.onClickedImage != null)
					{
						this.onClickedImage(this);
					}
					if (!this.isHeld)
					{
						this.isHeld = true;
						if (this.onClickImageStarted != null)
						{
							this.onClickImageStarted(this);
						}
					}
				}
				else if (this.isHeld)
				{
					this.isHeld = false;
					if (this.onClickImageStopped != null)
					{
						this.onClickImageStopped(this);
					}
				}
			}
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x04002264 RID: 8804
		public ClickedImage onClickedImage;

		// Token: 0x04002265 RID: 8805
		public ClickImageStarted onClickImageStarted;

		// Token: 0x04002266 RID: 8806
		public ClickImageStopped onClickImageStopped;

		// Token: 0x04002267 RID: 8807
		public Texture texture;

		// Token: 0x04002268 RID: 8808
		private bool isHeld;
	}
}
