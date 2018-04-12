using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200070B RID: 1803
	public class SleekUInt32Field : SleekBox
	{
		// Token: 0x0600335C RID: 13148 RVA: 0x0014D56A File Offset: 0x0014B96A
		public SleekUInt32Field()
		{
			base.init();
			this.state = 0u;
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.calculateContent();
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x0600335D RID: 13149 RVA: 0x0014D59E File Offset: 0x0014B99E
		// (set) Token: 0x0600335E RID: 13150 RVA: 0x0014D5A8 File Offset: 0x0014B9A8
		public uint state
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

		// Token: 0x0600335F RID: 13151 RVA: 0x0014D5D6 File Offset: 0x0014B9D6
		protected override void calculateContent()
		{
			this.content = new GUIContent(string.Empty, base.tooltip);
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x0014D5F0 File Offset: 0x0014B9F0
		public override void draw(bool ignoreCulling)
		{
			SleekRender.drawBox(base.frame, base.backgroundColor, this.content);
			string text = SleekRender.drawField(base.frame, this.fontStyle, this.fontAlignment, this.fontSize, base.backgroundColor, base.foregroundColor, base.text, 64, false);
			uint num;
			if (text != base.text && uint.TryParse(text, out num))
			{
				this._state = num;
				if (this.onTypedUInt32 != null)
				{
					this.onTypedUInt32(this, num);
				}
			}
			base.text = text;
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x040022CE RID: 8910
		public TypedUInt32 onTypedUInt32;

		// Token: 0x040022CF RID: 8911
		private uint _state;
	}
}
