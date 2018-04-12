using System;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002DC RID: 732
	public class Sleek2SByteField : Sleek2Field
	{
		// Token: 0x06001515 RID: 5397 RVA: 0x0008194B File Offset: 0x0007FD4B
		public Sleek2SByteField()
		{
			base.gameObject.name = "SByte_Field";
			this.updateText();
		}

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06001516 RID: 5398 RVA: 0x0008196C File Offset: 0x0007FD6C
		// (remove) Token: 0x06001517 RID: 5399 RVA: 0x000819A4 File Offset: 0x0007FDA4
		public event SByteFieldSubmittedHandler sbyteSubmitted;

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x000819DA File Offset: 0x0007FDDA
		// (set) Token: 0x06001519 RID: 5401 RVA: 0x000819E2 File Offset: 0x0007FDE2
		public sbyte value
		{
			get
			{
				return this._value;
			}
			set
			{
				if ((int)this._value == (int)value)
				{
					return;
				}
				this._value = value;
				this.updateText();
			}
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x00081A00 File Offset: 0x0007FE00
		protected virtual void updateText()
		{
			base.fieldComponent.text = this.value.ToString();
			base.fieldComponent.textComponent.text = base.fieldComponent.text;
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00081A47 File Offset: 0x0007FE47
		protected virtual void triggerSByteSubmitted(sbyte value)
		{
			if (this.sbyteSubmitted != null)
			{
				this.sbyteSubmitted(this, value);
			}
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00081A61 File Offset: 0x0007FE61
		protected override void triggerSubmitted(string text)
		{
			if (sbyte.TryParse(text, out this._value))
			{
				this.triggerSByteSubmitted(this.value);
			}
			else
			{
				this.value = 0;
			}
		}

		// Token: 0x04000BE8 RID: 3048
		protected sbyte _value;
	}
}
