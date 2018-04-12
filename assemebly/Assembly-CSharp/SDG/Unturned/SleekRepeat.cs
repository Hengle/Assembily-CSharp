using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006FB RID: 1787
	public class SleekRepeat : SleekLabel
	{
		// Token: 0x06003320 RID: 13088 RVA: 0x0014BF61 File Offset: 0x0014A361
		public SleekRepeat()
		{
			base.init();
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.calculateContent();
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x0014BF90 File Offset: 0x0014A390
		public override void draw(bool ignoreCulling)
		{
			if (!this.isHidden)
			{
				if (SleekRender.drawRepeat(base.frame, base.backgroundColor))
				{
					if (!this.isHeld)
					{
						this.isHeld = true;
						if (this.onStartedButton != null)
						{
							this.onStartedButton(this);
						}
					}
				}
				else if (Event.current.type == EventType.Repaint && this.isHeld)
				{
					this.isHeld = false;
					if (this.onStoppedButton != null)
					{
						this.onStoppedButton(this);
					}
				}
				SleekRender.drawLabel(base.frame, this.fontStyle, this.fontAlignment, this.fontSize, this.content2, base.foregroundColor, this.content);
			}
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x040022B1 RID: 8881
		public StartedButton onStartedButton;

		// Token: 0x040022B2 RID: 8882
		public StoppedButton onStoppedButton;

		// Token: 0x040022B3 RID: 8883
		private bool isHeld;
	}
}
