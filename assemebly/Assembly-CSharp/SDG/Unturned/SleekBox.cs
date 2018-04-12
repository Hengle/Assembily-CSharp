using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006C3 RID: 1731
	public class SleekBox : SleekLabel
	{
		// Token: 0x0600321A RID: 12826 RVA: 0x0014675F File Offset: 0x00144B5F
		public SleekBox()
		{
			base.init();
			this.backgroundTint = ESleekTint.BACKGROUND;
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.calculateContent();
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x00146794 File Offset: 0x00144B94
		public override void draw(bool ignoreCulling)
		{
			if (!this.isHidden)
			{
				SleekRender.drawBox(base.frame, base.backgroundColor);
				SleekRender.drawLabel(base.frame, this.fontStyle, this.fontAlignment, this.fontSize, this.content2, base.foregroundColor, this.content);
			}
			base.drawChildren(ignoreCulling);
		}
	}
}
