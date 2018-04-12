using System;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002C6 RID: 710
	public class Sleek2FloatField : Sleek2Field
	{
		// Token: 0x060014A1 RID: 5281 RVA: 0x00080DD6 File Offset: 0x0007F1D6
		public Sleek2FloatField()
		{
			base.gameObject.name = "Float_Field";
			this.updateText();
		}

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x060014A2 RID: 5282 RVA: 0x00080DF4 File Offset: 0x0007F1F4
		// (remove) Token: 0x060014A3 RID: 5283 RVA: 0x00080E2C File Offset: 0x0007F22C
		public event FloatFieldSubmittedHandler floatSubmitted;

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x00080E62 File Offset: 0x0007F262
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x00080E6A File Offset: 0x0007F26A
		public float value
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

		// Token: 0x060014A6 RID: 5286 RVA: 0x00080E88 File Offset: 0x0007F288
		protected virtual void updateText()
		{
			base.fieldComponent.text = this.value.ToString("F3");
			base.fieldComponent.textComponent.text = base.fieldComponent.text;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00080ECE File Offset: 0x0007F2CE
		protected virtual void triggerFloatSubmitted(float value)
		{
			if (this.floatSubmitted != null)
			{
				this.floatSubmitted(this, value);
			}
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00080EE8 File Offset: 0x0007F2E8
		protected override void triggerSubmitted(string text)
		{
			if (float.TryParse(text, out this._value))
			{
				this.triggerFloatSubmitted(this.value);
			}
			else
			{
				this.value = 0f;
			}
		}

		// Token: 0x04000BCB RID: 3019
		protected float _value;
	}
}
