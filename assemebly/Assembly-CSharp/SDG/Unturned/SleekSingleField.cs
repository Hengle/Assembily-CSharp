using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000700 RID: 1792
	public class SleekSingleField : SleekBox
	{
		// Token: 0x06003330 RID: 13104 RVA: 0x0014C956 File Offset: 0x0014AD56
		public SleekSingleField()
		{
			base.init();
			this.state = 0f;
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.calculateContent();
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06003331 RID: 13105 RVA: 0x0014C98E File Offset: 0x0014AD8E
		// (set) Token: 0x06003332 RID: 13106 RVA: 0x0014C998 File Offset: 0x0014AD98
		public float state
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

		// Token: 0x06003333 RID: 13107 RVA: 0x0014C9C5 File Offset: 0x0014ADC5
		protected override void calculateContent()
		{
			this.content = new GUIContent(string.Empty, base.tooltip);
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x0014C9E0 File Offset: 0x0014ADE0
		public override void draw(bool ignoreCulling)
		{
			SleekRender.drawBox(base.frame, base.backgroundColor, this.content);
			string text = SleekRender.drawField(base.frame, this.fontStyle, this.fontAlignment, this.fontSize, base.backgroundColor, base.foregroundColor, base.text, 64, false);
			float num;
			if (text != base.text && float.TryParse(text, out num))
			{
				this._state = num;
				if (this.onTypedSingle != null)
				{
					this.onTypedSingle(this, num);
				}
			}
			base.text = text;
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x040022BC RID: 8892
		public TypedSingle onTypedSingle;

		// Token: 0x040022BD RID: 8893
		private float _state;
	}
}
