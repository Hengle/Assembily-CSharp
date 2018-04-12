using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006C6 RID: 1734
	public class SleekButton : SleekLabel
	{
		// Token: 0x06003223 RID: 12835 RVA: 0x001455B4 File Offset: 0x001439B4
		public SleekButton()
		{
			base.init();
			this.backgroundTint = ESleekTint.BACKGROUND;
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.calculateContent();
			this.isClickable = true;
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x001455F0 File Offset: 0x001439F0
		public override void draw(bool ignoreCulling)
		{
			if (!this.isHidden)
			{
				if (this.isClickable)
				{
					if (SleekRender.drawButton(base.frame, base.backgroundColor) && this.onClickedButton != null)
					{
						this.onClickedButton(this);
					}
				}
				else
				{
					SleekRender.drawBox(base.frame, base.backgroundColor);
				}
				SleekRender.drawLabel(base.frame, this.fontStyle, this.fontAlignment, this.fontSize, this.content2, base.foregroundColor, this.content);
			}
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x04002237 RID: 8759
		public ClickedButton onClickedButton;

		// Token: 0x04002238 RID: 8760
		public bool isClickable;
	}
}
