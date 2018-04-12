using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006DB RID: 1755
	public class SleekGrid : Sleek
	{
		// Token: 0x0600327F RID: 12927 RVA: 0x00147FEB File Offset: 0x001463EB
		public SleekGrid()
		{
			base.init();
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x00147FF9 File Offset: 0x001463F9
		public override void draw(bool ignoreCulling)
		{
			if (SleekRender.drawGrid(base.frame, this.texture, base.backgroundColor) && this.onClickedGrid != null)
			{
				this.onClickedGrid(this);
			}
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x04002262 RID: 8802
		public Texture texture;

		// Token: 0x04002263 RID: 8803
		public ClickedGrid onClickedGrid;
	}
}
