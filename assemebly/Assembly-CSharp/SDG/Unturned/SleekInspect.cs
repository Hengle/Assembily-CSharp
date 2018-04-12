using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006E2 RID: 1762
	public class SleekInspect : SleekBox
	{
		// Token: 0x06003297 RID: 12951 RVA: 0x0014820C File Offset: 0x0014660C
		public SleekInspect(string path)
		{
			base.init();
			RenderTexture renderTexture = (RenderTexture)Resources.Load(path);
			this.renderImage = new SleekImageMaterial();
			this.renderImage.sizeScale_X = 1f;
			this.renderImage.sizeScale_Y = 1f;
			this.renderImage.constraint = ESleekConstraint.X;
			this.renderImage.constrain_Y = renderTexture.height;
			this.renderImage.texture = renderTexture;
			this.renderImage.material = (Material)Resources.Load("Materials/RenderTexture");
			base.add(this.renderImage);
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.calculateContent();
		}

		// Token: 0x0400226E RID: 8814
		public SleekImageMaterial renderImage;
	}
}
