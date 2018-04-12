using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006E4 RID: 1764
	public class SleekInt32Field : SleekBox
	{
		// Token: 0x0600329C RID: 12956 RVA: 0x001482CA File Offset: 0x001466CA
		public SleekInt32Field()
		{
			base.init();
			this.state = 0;
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.calculateContent();
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x0600329D RID: 12957 RVA: 0x001482FE File Offset: 0x001466FE
		// (set) Token: 0x0600329E RID: 12958 RVA: 0x00148308 File Offset: 0x00146708
		public int state
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

		// Token: 0x0600329F RID: 12959 RVA: 0x00148336 File Offset: 0x00146736
		protected override void calculateContent()
		{
			this.content = new GUIContent(string.Empty, base.tooltip);
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x00148350 File Offset: 0x00146750
		public override void draw(bool ignoreCulling)
		{
			SleekRender.drawBox(base.frame, base.backgroundColor, this.content);
			string text = SleekRender.drawField(base.frame, this.fontStyle, this.fontAlignment, this.fontSize, base.backgroundColor, base.foregroundColor, base.text, 64, false);
			int num;
			if (text != base.text && int.TryParse(text, out num))
			{
				this._state = num;
				if (this.onTypedInt != null)
				{
					this.onTypedInt(this, num);
				}
			}
			base.text = text;
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x0400226F RID: 8815
		public TypedInt32 onTypedInt;

		// Token: 0x04002270 RID: 8816
		private int _state;
	}
}
