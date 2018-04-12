using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006D5 RID: 1749
	public class SleekDoubleField : SleekBox
	{
		// Token: 0x06003267 RID: 12903 RVA: 0x00147C89 File Offset: 0x00146089
		public SleekDoubleField()
		{
			base.init();
			this.state = 0.0;
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.calculateContent();
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06003268 RID: 12904 RVA: 0x00147CC5 File Offset: 0x001460C5
		// (set) Token: 0x06003269 RID: 12905 RVA: 0x00147CD0 File Offset: 0x001460D0
		public double state
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
				base.text = this.state.ToString("F3");
			}
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x00147CFD File Offset: 0x001460FD
		protected override void calculateContent()
		{
			this.content = new GUIContent(string.Empty, base.tooltip);
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x00147D18 File Offset: 0x00146118
		public override void draw(bool ignoreCulling)
		{
			SleekRender.drawBox(base.frame, base.backgroundColor, this.content);
			string text = SleekRender.drawField(base.frame, this.fontStyle, this.fontAlignment, this.fontSize, base.backgroundColor, base.foregroundColor, base.text, 64, false);
			double num;
			if (text != base.text && double.TryParse(text, out num))
			{
				this._state = num;
				if (this.onTypedDouble != null)
				{
					this.onTypedDouble(this, num);
				}
			}
			base.text = text;
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x04002258 RID: 8792
		public TypedDouble onTypedDouble;

		// Token: 0x04002259 RID: 8793
		private double _state;
	}
}
