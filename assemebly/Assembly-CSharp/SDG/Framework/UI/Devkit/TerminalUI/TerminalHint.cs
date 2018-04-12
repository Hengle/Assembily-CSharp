using System;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.TerminalUI
{
	// Token: 0x0200028B RID: 651
	public class TerminalHint
	{
		// Token: 0x06001316 RID: 4886 RVA: 0x0007994F File Offset: 0x00077D4F
		public TerminalHint(GameObject newHint, Text newTextComponent)
		{
			this.hint = newHint;
			this.textComponent = newTextComponent;
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x00079965 File Offset: 0x00077D65
		// (set) Token: 0x06001318 RID: 4888 RVA: 0x00079972 File Offset: 0x00077D72
		public bool isVisible
		{
			get
			{
				return this.hint.activeSelf;
			}
			set
			{
				if (this.hint.activeSelf != value)
				{
					this.hint.SetActive(value);
				}
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x00079991 File Offset: 0x00077D91
		// (set) Token: 0x0600131A RID: 4890 RVA: 0x0007999E File Offset: 0x00077D9E
		public string text
		{
			get
			{
				return this.textComponent.text;
			}
			set
			{
				this.textComponent.text = value;
			}
		}

		// Token: 0x04000AF0 RID: 2800
		protected GameObject hint;

		// Token: 0x04000AF1 RID: 2801
		protected Text textComponent;
	}
}
