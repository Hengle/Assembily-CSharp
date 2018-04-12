using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006F7 RID: 1783
	public class SleekProgress : Sleek
	{
		// Token: 0x060032FD RID: 13053 RVA: 0x0014B338 File Offset: 0x00149738
		public SleekProgress(string newSuffix)
		{
			base.init();
			this.background = new SleekImageTexture();
			this.background.sizeScale_X = 1f;
			this.background.sizeScale_Y = 1f;
			this.background.texture = (Texture2D)Resources.Load("Materials/Pixel");
			base.add(this.background);
			this.foreground = new SleekImageTexture();
			this.foreground.sizeScale_X = 1f;
			this.foreground.sizeScale_Y = 1f;
			this.foreground.texture = (Texture2D)Resources.Load("Materials/Pixel");
			base.add(this.foreground);
			this.label = new SleekLabel();
			this.label.sizeScale_X = 1f;
			this.label.positionScale_Y = 0.5f;
			this.label.positionOffset_Y = -15;
			this.label.sizeOffset_Y = 30;
			this.label.foregroundTint = ESleekTint.NONE;
			base.add(this.label);
			this.suffix = newSuffix;
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060032FE RID: 13054 RVA: 0x0014B457 File Offset: 0x00149857
		// (set) Token: 0x060032FF RID: 13055 RVA: 0x0014B460 File Offset: 0x00149860
		public float state
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = Mathf.Clamp01(value);
				this.foreground.sizeScale_X = this.state;
				if (this.suffix.Length == 0)
				{
					this.label.text = Mathf.RoundToInt(this.foreground.sizeScale_X * 100f) + "%";
				}
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (set) Token: 0x06003300 RID: 13056 RVA: 0x0014B4CA File Offset: 0x001498CA
		public int measure
		{
			set
			{
				if (this.suffix.Length != 0)
				{
					this.label.text = value + this.suffix;
				}
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x0014B4F8 File Offset: 0x001498F8
		// (set) Token: 0x06003302 RID: 13058 RVA: 0x0014B508 File Offset: 0x00149908
		public Color color
		{
			get
			{
				return this.foreground.backgroundColor;
			}
			set
			{
				Color backgroundColor = value;
				backgroundColor.a = 0.5f;
				this.background.backgroundColor = backgroundColor;
				this.foreground.backgroundColor = value;
			}
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x0014B53B File Offset: 0x0014993B
		public override void draw(bool ignoreCulling)
		{
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x040022A8 RID: 8872
		private SleekImageTexture background;

		// Token: 0x040022A9 RID: 8873
		private SleekImageTexture foreground;

		// Token: 0x040022AA RID: 8874
		private SleekLabel label;

		// Token: 0x040022AB RID: 8875
		private string suffix;

		// Token: 0x040022AC RID: 8876
		private float _state;
	}
}
