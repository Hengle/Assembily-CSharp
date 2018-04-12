using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006CE RID: 1742
	public class SleekByteField : SleekBox
	{
		// Token: 0x06003244 RID: 12868 RVA: 0x00147161 File Offset: 0x00145561
		public SleekByteField()
		{
			base.init();
			this.state = 0;
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.calculateContent();
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06003245 RID: 12869 RVA: 0x00147195 File Offset: 0x00145595
		// (set) Token: 0x06003246 RID: 12870 RVA: 0x001471A0 File Offset: 0x001455A0
		public byte state
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
				base.text = this.state.ToString();
			}
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x001471CE File Offset: 0x001455CE
		protected override void calculateContent()
		{
			this.content = new GUIContent(string.Empty, base.tooltip);
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x001471E8 File Offset: 0x001455E8
		public override void draw(bool ignoreCulling)
		{
			SleekRender.drawBox(base.frame, base.backgroundColor, this.content);
			string text = SleekRender.drawField(base.frame, this.fontStyle, this.fontAlignment, this.fontSize, base.backgroundColor, base.foregroundColor, base.text, 3, false);
			byte b;
			if (text != base.text && byte.TryParse(text, out b))
			{
				this._state = b;
				if (this.onTypedByte != null)
				{
					this.onTypedByte(this, b);
				}
			}
			base.text = text;
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x04002244 RID: 8772
		public TypedByte onTypedByte;

		// Token: 0x04002245 RID: 8773
		private byte _state;
	}
}
