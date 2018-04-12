using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006FC RID: 1788
	public class SleekScrollBox : Sleek
	{
		// Token: 0x06003322 RID: 13090 RVA: 0x0014C05B File Offset: 0x0014A45B
		public SleekScrollBox()
		{
			this.state = new Vector2(0f, 0f);
			this.local = true;
			this.backgroundTint = ESleekTint.BACKGROUND;
			base.init();
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x0014C08C File Offset: 0x0014A48C
		public override Rect getCullingRect()
		{
			return new Rect(this.state.x, this.state.y, base.frame.width, base.frame.height);
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x0014C0D0 File Offset: 0x0014A4D0
		public override void draw(bool ignoreCulling)
		{
			GUI.backgroundColor = base.backgroundColor;
			float uiLayoutScale = GraphicsSettings.uiLayoutScale;
			Rect viewRect = this.area;
			viewRect.size *= uiLayoutScale;
			this.state = GUI.BeginScrollView(base.frame, this.state, viewRect);
			base.drawChildren(ignoreCulling);
			GUI.EndScrollView(true);
		}

		// Token: 0x040022B4 RID: 8884
		public Rect area;

		// Token: 0x040022B5 RID: 8885
		public Vector2 state;
	}
}
