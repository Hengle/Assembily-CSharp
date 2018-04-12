using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000705 RID: 1797
	public class SleekTile : Sleek
	{
		// Token: 0x06003347 RID: 13127 RVA: 0x0014D3B0 File Offset: 0x0014B7B0
		public SleekTile()
		{
			base.init();
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x0014D3BE File Offset: 0x0014B7BE
		public override void draw(bool ignoreCulling)
		{
			SleekRender.drawTile(base.frame, this.texture, base.backgroundColor);
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x040022C9 RID: 8905
		public Texture texture;
	}
}
