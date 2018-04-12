using System;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002F1 RID: 753
	public class Sleek2UShortField : Sleek2Field
	{
		// Token: 0x0600158A RID: 5514 RVA: 0x00082AB9 File Offset: 0x00080EB9
		public Sleek2UShortField()
		{
			base.gameObject.name = "UShort_Field";
			this.updateText();
		}

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x0600158B RID: 5515 RVA: 0x00082AD8 File Offset: 0x00080ED8
		// (remove) Token: 0x0600158C RID: 5516 RVA: 0x00082B10 File Offset: 0x00080F10
		public event UShortFieldSubmittedHandler ushortSubmitted;

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x00082B46 File Offset: 0x00080F46
		// (set) Token: 0x0600158E RID: 5518 RVA: 0x00082B4E File Offset: 0x00080F4E
		public ushort value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (this._value == value)
				{
					return;
				}
				this._value = value;
				this.updateText();
			}
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00082B6C File Offset: 0x00080F6C
		protected virtual void updateText()
		{
			base.fieldComponent.text = this.value.ToString();
			base.fieldComponent.textComponent.text = base.fieldComponent.text;
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00082BB3 File Offset: 0x00080FB3
		protected virtual void triggerUShortSubmitted(ushort value)
		{
			if (this.ushortSubmitted != null)
			{
				this.ushortSubmitted(this, value);
			}
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00082BCD File Offset: 0x00080FCD
		protected override void triggerSubmitted(string text)
		{
			if (ushort.TryParse(text, out this._value))
			{
				this.triggerUShortSubmitted(this.value);
			}
			else
			{
				this.value = 0;
			}
		}

		// Token: 0x04000C05 RID: 3077
		protected ushort _value;
	}
}
