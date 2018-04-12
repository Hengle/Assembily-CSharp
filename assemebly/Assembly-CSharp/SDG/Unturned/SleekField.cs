using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006D9 RID: 1753
	public class SleekField : SleekBox
	{
		// Token: 0x06003278 RID: 12920 RVA: 0x00147DBA File Offset: 0x001461BA
		public SleekField()
		{
			base.init();
			this.replace = ' ';
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.maxLength = 16;
			this.calculateContent();
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x00147DF7 File Offset: 0x001461F7
		protected override void calculateContent()
		{
			this.content = new GUIContent(string.Empty, base.tooltip);
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x00147E10 File Offset: 0x00146210
		public override void draw(bool ignoreCulling)
		{
			SleekRender.drawBox(base.frame, base.backgroundColor, this.content);
			if (this.control != null && this.control.Length > 0)
			{
				GUI.SetNextControlName(this.control);
			}
			string text;
			if (this.replace != ' ')
			{
				text = SleekRender.drawField(base.frame, this.fontStyle, this.fontAlignment, this.fontSize, base.backgroundColor, base.foregroundColor, base.text, this.maxLength, this.hint, this.replace);
			}
			else
			{
				text = SleekRender.drawField(base.frame, this.fontStyle, this.fontAlignment, this.fontSize, base.backgroundColor, base.foregroundColor, base.text, this.maxLength, this.hint, this.multiline);
			}
			if (text != base.text && this.onTyped != null)
			{
				this.onTyped(this, text);
			}
			base.text = text;
			if (this.control != null && this.control.Length > 0 && GUI.GetNameOfFocusedControl() == this.control && Event.current.isKey && Event.current.type == EventType.KeyUp)
			{
				if (Event.current.keyCode == KeyCode.Escape || Event.current.keyCode == ControlsSettings.dashboard)
				{
					if (this.onEscaped != null)
					{
						this.onEscaped(this);
					}
					GUI.FocusControl(string.Empty);
				}
				else if (Event.current.keyCode == KeyCode.Return && this.onEntered != null)
				{
					this.onEntered(this);
				}
			}
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x0400225A RID: 8794
		public Escaped onEscaped;

		// Token: 0x0400225B RID: 8795
		public Entered onEntered;

		// Token: 0x0400225C RID: 8796
		public Typed onTyped;

		// Token: 0x0400225D RID: 8797
		public char replace;

		// Token: 0x0400225E RID: 8798
		public string hint;

		// Token: 0x0400225F RID: 8799
		public string control;

		// Token: 0x04002260 RID: 8800
		public bool multiline;

		// Token: 0x04002261 RID: 8801
		public int maxLength;
	}
}
