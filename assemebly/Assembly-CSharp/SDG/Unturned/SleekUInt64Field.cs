using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200070D RID: 1805
	public class SleekUInt64Field : SleekBox
	{
		// Token: 0x06003365 RID: 13157 RVA: 0x0014D692 File Offset: 0x0014BA92
		public SleekUInt64Field()
		{
			base.init();
			this.state = 0UL;
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.calculateContent();
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06003366 RID: 13158 RVA: 0x0014D6C7 File Offset: 0x0014BAC7
		// (set) Token: 0x06003367 RID: 13159 RVA: 0x0014D6D0 File Offset: 0x0014BAD0
		public ulong state
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

		// Token: 0x06003368 RID: 13160 RVA: 0x0014D6FE File Offset: 0x0014BAFE
		protected override void calculateContent()
		{
			this.content = new GUIContent(string.Empty, base.tooltip);
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x0014D718 File Offset: 0x0014BB18
		public override void draw(bool ignoreCulling)
		{
			SleekRender.drawBox(base.frame, base.backgroundColor, this.content);
			string text = SleekRender.drawField(base.frame, this.fontStyle, this.fontAlignment, this.fontSize, base.backgroundColor, base.foregroundColor, base.text, 64, false);
			ulong num;
			if (text != base.text && ulong.TryParse(text, out num))
			{
				this._state = num;
				if (this.onTypedUInt64 != null)
				{
					this.onTypedUInt64(this, num);
				}
			}
			base.text = text;
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x040022D0 RID: 8912
		public TypedUInt64 onTypedUInt64;

		// Token: 0x040022D1 RID: 8913
		private ulong _state;
	}
}
