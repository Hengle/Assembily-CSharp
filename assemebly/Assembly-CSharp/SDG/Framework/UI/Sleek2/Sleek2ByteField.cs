using System;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002B4 RID: 692
	public class Sleek2ByteField : Sleek2Field
	{
		// Token: 0x06001415 RID: 5141 RVA: 0x0008077E File Offset: 0x0007EB7E
		public Sleek2ByteField()
		{
			base.gameObject.name = "Byte_Field";
			this.updateText();
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06001416 RID: 5142 RVA: 0x0008079C File Offset: 0x0007EB9C
		// (remove) Token: 0x06001417 RID: 5143 RVA: 0x000807D4 File Offset: 0x0007EBD4
		public event ByteFieldSubmittedHandler byteSubmitted;

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0008080A File Offset: 0x0007EC0A
		// (set) Token: 0x06001419 RID: 5145 RVA: 0x00080812 File Offset: 0x0007EC12
		public byte value
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

		// Token: 0x0600141A RID: 5146 RVA: 0x00080830 File Offset: 0x0007EC30
		protected virtual void updateText()
		{
			base.fieldComponent.text = this.value.ToString();
			base.fieldComponent.textComponent.text = base.fieldComponent.text;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00080877 File Offset: 0x0007EC77
		protected virtual void triggerByteSubmitted(byte value)
		{
			if (this.byteSubmitted != null)
			{
				this.byteSubmitted(this, value);
			}
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00080891 File Offset: 0x0007EC91
		protected override void triggerSubmitted(string text)
		{
			if (byte.TryParse(text, out this._value))
			{
				this.triggerByteSubmitted(this.value);
			}
			else
			{
				this.value = 0;
			}
		}

		// Token: 0x04000BA4 RID: 2980
		protected byte _value;
	}
}
