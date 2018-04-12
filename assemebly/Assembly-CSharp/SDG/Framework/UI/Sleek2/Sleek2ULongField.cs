using System;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002EF RID: 751
	public class Sleek2ULongField : Sleek2Field
	{
		// Token: 0x0600157E RID: 5502 RVA: 0x00082978 File Offset: 0x00080D78
		public Sleek2ULongField()
		{
			base.gameObject.name = "ULong_Field";
			this.updateText();
		}

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x0600157F RID: 5503 RVA: 0x00082998 File Offset: 0x00080D98
		// (remove) Token: 0x06001580 RID: 5504 RVA: 0x000829D0 File Offset: 0x00080DD0
		public event ULongFieldSubmittedHandler ulongSubmitted;

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x00082A06 File Offset: 0x00080E06
		// (set) Token: 0x06001582 RID: 5506 RVA: 0x00082A0E File Offset: 0x00080E0E
		public ulong value
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

		// Token: 0x06001583 RID: 5507 RVA: 0x00082A2C File Offset: 0x00080E2C
		protected virtual void updateText()
		{
			base.fieldComponent.text = this.value.ToString();
			base.fieldComponent.textComponent.text = base.fieldComponent.text;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00082A73 File Offset: 0x00080E73
		protected virtual void triggerULongSubmitted(ulong value)
		{
			if (this.ulongSubmitted != null)
			{
				this.ulongSubmitted(this, value);
			}
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00082A8D File Offset: 0x00080E8D
		protected override void triggerSubmitted(string text)
		{
			if (ulong.TryParse(text, out this._value))
			{
				this.triggerULongSubmitted(this.value);
			}
			else
			{
				this.value = 0UL;
			}
		}

		// Token: 0x04000C03 RID: 3075
		protected ulong _value;
	}
}
