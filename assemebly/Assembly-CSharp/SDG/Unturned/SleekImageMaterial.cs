using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006E0 RID: 1760
	public class SleekImageMaterial : SleekImageTexture
	{
		// Token: 0x0600328F RID: 12943 RVA: 0x001481B5 File Offset: 0x001465B5
		public SleekImageMaterial()
		{
			base.init();
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x001481C3 File Offset: 0x001465C3
		public SleekImageMaterial(Texture newTexture, Material newMaterial)
		{
			base.init();
			this.texture = newTexture;
			this.material = newMaterial;
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x001481DF File Offset: 0x001465DF
		public override void draw(bool ignoreCulling)
		{
			if (!this.isHidden)
			{
				SleekRender.drawImageMaterial(base.frame, this.texture, this.material);
			}
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x04002269 RID: 8809
		public Material material;
	}
}
