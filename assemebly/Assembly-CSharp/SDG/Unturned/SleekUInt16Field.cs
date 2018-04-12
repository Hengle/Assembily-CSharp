using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000709 RID: 1801
	public class SleekUInt16Field : SleekBox
	{
		// Token: 0x06003353 RID: 13139 RVA: 0x0014D443 File Offset: 0x0014B843
		public SleekUInt16Field()
		{
			base.init();
			this.state = 0;
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.calculateContent();
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x0014D477 File Offset: 0x0014B877
		// (set) Token: 0x06003355 RID: 13141 RVA: 0x0014D480 File Offset: 0x0014B880
		public ushort state
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

		// Token: 0x06003356 RID: 13142 RVA: 0x0014D4AE File Offset: 0x0014B8AE
		protected override void calculateContent()
		{
			this.content = new GUIContent(string.Empty, base.tooltip);
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x0014D4C8 File Offset: 0x0014B8C8
		public override void draw(bool ignoreCulling)
		{
			SleekRender.drawBox(base.frame, base.backgroundColor, this.content);
			string text = SleekRender.drawField(base.frame, this.fontStyle, this.fontAlignment, this.fontSize, base.backgroundColor, base.foregroundColor, base.text, 64, false);
			ushort num;
			if (text != base.text && ushort.TryParse(text, out num))
			{
				this._state = num;
				if (this.onTypedUInt16 != null)
				{
					this.onTypedUInt16(this, num);
				}
			}
			base.text = text;
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x040022CC RID: 8908
		public TypedUInt16 onTypedUInt16;

		// Token: 0x040022CD RID: 8909
		private ushort _state;
	}
}
