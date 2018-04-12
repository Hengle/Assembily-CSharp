using System;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002ED RID: 749
	public class Sleek2UIntField : Sleek2Field
	{
		// Token: 0x06001572 RID: 5490 RVA: 0x00082838 File Offset: 0x00080C38
		public Sleek2UIntField()
		{
			base.gameObject.name = "UInt_Field";
			this.updateText();
		}

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06001573 RID: 5491 RVA: 0x00082858 File Offset: 0x00080C58
		// (remove) Token: 0x06001574 RID: 5492 RVA: 0x00082890 File Offset: 0x00080C90
		public event UIntFieldSubmittedHandler uintSubmitted;

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x000828C6 File Offset: 0x00080CC6
		// (set) Token: 0x06001576 RID: 5494 RVA: 0x000828CE File Offset: 0x00080CCE
		public uint value
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

		// Token: 0x06001577 RID: 5495 RVA: 0x000828EC File Offset: 0x00080CEC
		protected virtual void updateText()
		{
			base.fieldComponent.text = this.value.ToString();
			base.fieldComponent.textComponent.text = base.fieldComponent.text;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x00082933 File Offset: 0x00080D33
		protected virtual void triggerUIntSubmitted(uint value)
		{
			if (this.uintSubmitted != null)
			{
				this.uintSubmitted(this, value);
			}
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0008294D File Offset: 0x00080D4D
		protected override void triggerSubmitted(string text)
		{
			if (uint.TryParse(text, out this._value))
			{
				this.triggerUIntSubmitted(this.value);
			}
			else
			{
				this.value = 0u;
			}
		}

		// Token: 0x04000C01 RID: 3073
		protected uint _value;
	}
}
