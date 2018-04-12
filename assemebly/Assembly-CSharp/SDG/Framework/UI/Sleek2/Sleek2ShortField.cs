using System;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002E3 RID: 739
	public class Sleek2ShortField : Sleek2Field
	{
		// Token: 0x06001546 RID: 5446 RVA: 0x0008213E File Offset: 0x0008053E
		public Sleek2ShortField()
		{
			base.gameObject.name = "Short_Field";
			this.updateText();
		}

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06001547 RID: 5447 RVA: 0x0008215C File Offset: 0x0008055C
		// (remove) Token: 0x06001548 RID: 5448 RVA: 0x00082194 File Offset: 0x00080594
		public event ShortFieldSubmittedHandler shortSubmitted;

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x000821CA File Offset: 0x000805CA
		// (set) Token: 0x0600154A RID: 5450 RVA: 0x000821D2 File Offset: 0x000805D2
		public short value
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

		// Token: 0x0600154B RID: 5451 RVA: 0x000821F0 File Offset: 0x000805F0
		protected virtual void updateText()
		{
			base.fieldComponent.text = this.value.ToString();
			base.fieldComponent.textComponent.text = base.fieldComponent.text;
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00082237 File Offset: 0x00080637
		protected virtual void triggerShortSubmitted(short value)
		{
			if (this.shortSubmitted != null)
			{
				this.shortSubmitted(this, value);
			}
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00082251 File Offset: 0x00080651
		protected override void triggerSubmitted(string text)
		{
			if (short.TryParse(text, out this._value))
			{
				this.triggerShortSubmitted(this.value);
			}
			else
			{
				this.value = 0;
			}
		}

		// Token: 0x04000BF7 RID: 3063
		protected short _value;
	}
}
