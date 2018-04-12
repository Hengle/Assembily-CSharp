using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000711 RID: 1809
	public class SleekViewBox : Sleek
	{
		// Token: 0x06003376 RID: 13174 RVA: 0x0014DDDE File Offset: 0x0014C1DE
		public SleekViewBox()
		{
			this.state = new Vector2(0f, 0f);
			this.local = true;
			this.backgroundTint = ESleekTint.BACKGROUND;
			base.init();
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x0014DE10 File Offset: 0x0014C210
		public override Rect getCullingRect()
		{
			return new Rect(this.state.x, this.state.y, base.frame.width, base.frame.height);
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x0014DE54 File Offset: 0x0014C254
		public override void draw(bool ignoreCulling)
		{
			GUI.backgroundColor = base.backgroundColor;
			float uiLayoutScale = GraphicsSettings.uiLayoutScale;
			Rect viewRect = this.area;
			viewRect.size *= uiLayoutScale;
			this.state = GUI.BeginScrollView(base.frame, this.state, viewRect);
			base.drawChildren(ignoreCulling);
			GUI.EndScrollView(false);
		}

		// Token: 0x040022D8 RID: 8920
		public Rect area;

		// Token: 0x040022D9 RID: 8921
		public Vector2 state;
	}
}
