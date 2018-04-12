using System;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002D5 RID: 725
	public class Sleek2LongField : Sleek2Field
	{
		// Token: 0x060014EE RID: 5358 RVA: 0x0008161D File Offset: 0x0007FA1D
		public Sleek2LongField()
		{
			base.gameObject.name = "Long_Field";
			this.updateText();
		}

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x060014EF RID: 5359 RVA: 0x0008163C File Offset: 0x0007FA3C
		// (remove) Token: 0x060014F0 RID: 5360 RVA: 0x00081674 File Offset: 0x0007FA74
		public event LongFieldSubmittedHandler longSubmitted;

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x000816AA File Offset: 0x0007FAAA
		// (set) Token: 0x060014F2 RID: 5362 RVA: 0x000816B2 File Offset: 0x0007FAB2
		public long value
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

		// Token: 0x060014F3 RID: 5363 RVA: 0x000816D0 File Offset: 0x0007FAD0
		protected virtual void updateText()
		{
			base.fieldComponent.text = this.value.ToString();
			base.fieldComponent.textComponent.text = base.fieldComponent.text;
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00081717 File Offset: 0x0007FB17
		protected virtual void triggerLongSubmitted(long value)
		{
			if (this.longSubmitted != null)
			{
				this.longSubmitted(this, value);
			}
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00081731 File Offset: 0x0007FB31
		protected override void triggerSubmitted(string text)
		{
			if (long.TryParse(text, out this._value))
			{
				this.triggerLongSubmitted(this.value);
			}
			else
			{
				this.value = 0L;
			}
		}

		// Token: 0x04000BDD RID: 3037
		protected long _value;
	}
}
