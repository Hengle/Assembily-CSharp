using System;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002B9 RID: 697
	public class Sleek2DoubleField : Sleek2Field
	{
		// Token: 0x06001441 RID: 5185 RVA: 0x00080BE0 File Offset: 0x0007EFE0
		public Sleek2DoubleField()
		{
			base.gameObject.name = "Double_Field";
			this.updateText();
		}

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06001442 RID: 5186 RVA: 0x00080C00 File Offset: 0x0007F000
		// (remove) Token: 0x06001443 RID: 5187 RVA: 0x00080C38 File Offset: 0x0007F038
		public event DoubleFieldSubmittedHandler doubleSubmitted;

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00080C6E File Offset: 0x0007F06E
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x00080C76 File Offset: 0x0007F076
		public double value
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

		// Token: 0x06001446 RID: 5190 RVA: 0x00080C94 File Offset: 0x0007F094
		protected virtual void updateText()
		{
			base.fieldComponent.text = this.value.ToString("F3");
			base.fieldComponent.textComponent.text = base.fieldComponent.text;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x00080CDA File Offset: 0x0007F0DA
		protected virtual void triggerDoubleSubmitted(double value)
		{
			if (this.doubleSubmitted != null)
			{
				this.doubleSubmitted(this, value);
			}
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x00080CF4 File Offset: 0x0007F0F4
		protected override void triggerSubmitted(string text)
		{
			if (double.TryParse(text, out this._value))
			{
				this.triggerDoubleSubmitted(this.value);
			}
			else
			{
				this.value = 0.0;
			}
		}

		// Token: 0x04000BB6 RID: 2998
		protected double _value;
	}
}
