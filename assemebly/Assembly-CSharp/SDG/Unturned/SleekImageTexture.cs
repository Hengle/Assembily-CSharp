using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006E1 RID: 1761
	public class SleekImageTexture : Sleek
	{
		// Token: 0x06003292 RID: 12946 RVA: 0x001480ED File Offset: 0x001464ED
		public SleekImageTexture()
		{
			base.init();
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x001480FB File Offset: 0x001464FB
		public SleekImageTexture(Texture newTexture)
		{
			base.init();
			this.texture = newTexture;
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x00148110 File Offset: 0x00146510
		public void updateTexture(Texture2D newTexture)
		{
			this.texture = newTexture;
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x0014811C File Offset: 0x0014651C
		public override void draw(bool ignoreCulling)
		{
			if (!this.isHidden)
			{
				if (this.isAngled)
				{
					SleekRender.drawAngledImageTexture(base.frame, this.texture, this.angle, base.backgroundColor);
				}
				else
				{
					SleekRender.drawImageTexture(base.frame, this.texture, base.backgroundColor);
				}
			}
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x0014817F File Offset: 0x0014657F
		public override void destroy()
		{
			if (this.shouldDestroyTexture && this.texture != null)
			{
				UnityEngine.Object.DestroyImmediate(this.texture);
				this.texture = null;
			}
			base.destroyChildren();
		}

		// Token: 0x0400226A RID: 8810
		public Texture texture;

		// Token: 0x0400226B RID: 8811
		public float angle;

		// Token: 0x0400226C RID: 8812
		public bool isAngled;

		// Token: 0x0400226D RID: 8813
		public bool shouldDestroyTexture;
	}
}
