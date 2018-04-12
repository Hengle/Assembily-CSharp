using System;

namespace SDG.Unturned
{
	// Token: 0x02000703 RID: 1795
	public class SleekSlider : Sleek
	{
		// Token: 0x0600333A RID: 13114 RVA: 0x0014D09A File Offset: 0x0014B49A
		public SleekSlider()
		{
			base.init();
			this.backgroundTint = ESleekTint.BACKGROUND;
			this.orientation = ESleekOrientation.VERTICAL;
			this.size = 0.25f;
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x0600333B RID: 13115 RVA: 0x0014D0C1 File Offset: 0x0014B4C1
		// (set) Token: 0x0600333C RID: 13116 RVA: 0x0014D0C9 File Offset: 0x0014B4C9
		public float state
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
				this.scroll = this.state * (1f - this.size);
			}
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x0014D0EC File Offset: 0x0014B4EC
		public override void draw(bool ignoreCulling)
		{
			float num = SleekRender.drawSlider(base.frame, this.orientation, this.scroll, this.size, base.backgroundColor);
			if (num != this.scroll)
			{
				this._state = num / (1f - this.size);
				if (this.state < 0f)
				{
					this.state = 0f;
				}
				else if (this.state > 1f)
				{
					this.state = 1f;
				}
				if (this.onDragged != null)
				{
					this.onDragged(this, this.state);
				}
			}
			this.scroll = num;
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x040022BE RID: 8894
		public Dragged onDragged;

		// Token: 0x040022BF RID: 8895
		public ESleekOrientation orientation;

		// Token: 0x040022C0 RID: 8896
		public float size;

		// Token: 0x040022C1 RID: 8897
		private float scroll;

		// Token: 0x040022C2 RID: 8898
		private float _state;
	}
}
