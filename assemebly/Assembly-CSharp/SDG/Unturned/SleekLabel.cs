using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006F0 RID: 1776
	public class SleekLabel : Sleek
	{
		// Token: 0x060032DF RID: 13023 RVA: 0x00145488 File Offset: 0x00143888
		public SleekLabel()
		{
			base.init();
			this.foregroundTint = ESleekTint.FONT;
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.calculateContent();
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060032E0 RID: 13024 RVA: 0x001454DD File Offset: 0x001438DD
		// (set) Token: 0x060032E1 RID: 13025 RVA: 0x001454E5 File Offset: 0x001438E5
		public string text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
				this.calculateContent();
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060032E2 RID: 13026 RVA: 0x001454F4 File Offset: 0x001438F4
		// (set) Token: 0x060032E3 RID: 13027 RVA: 0x001454FC File Offset: 0x001438FC
		public string tooltip
		{
			get
			{
				return this._tooltip;
			}
			set
			{
				this._tooltip = value;
				this.calculateContent();
			}
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x0014550C File Offset: 0x0014390C
		protected virtual void calculateContent()
		{
			this.content = new GUIContent(this.text, this.tooltip);
			if (this.isRich)
			{
				this.content2 = new GUIContent(Regex.Replace(this.text, "</*color.*?>", string.Empty), Regex.Replace(this.tooltip, "<.*?>", string.Empty));
			}
			else
			{
				this.content2 = null;
			}
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x0014557C File Offset: 0x0014397C
		public override void draw(bool ignoreCulling)
		{
			SleekRender.drawLabel(base.frame, this.fontStyle, this.fontAlignment, this.fontSize, this.content2, base.foregroundColor, this.content);
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x0400228C RID: 8844
		private string _text = string.Empty;

		// Token: 0x0400228D RID: 8845
		private string _tooltip = string.Empty;

		// Token: 0x0400228E RID: 8846
		public FontStyle fontStyle;

		// Token: 0x0400228F RID: 8847
		public TextAnchor fontAlignment;

		// Token: 0x04002290 RID: 8848
		public int fontSize;

		// Token: 0x04002291 RID: 8849
		public GUIContent content;

		// Token: 0x04002292 RID: 8850
		protected GUIContent content2;
	}
}
