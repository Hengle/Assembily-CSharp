using System;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002C8 RID: 712
	public class Sleek2GUIDField : Sleek2Field
	{
		// Token: 0x060014AD RID: 5293 RVA: 0x00080F17 File Offset: 0x0007F317
		public Sleek2GUIDField()
		{
			base.gameObject.name = "GUID_Field";
			this.updateText();
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x060014AE RID: 5294 RVA: 0x00080F38 File Offset: 0x0007F338
		// (remove) Token: 0x060014AF RID: 5295 RVA: 0x00080F70 File Offset: 0x0007F370
		public event GUIDFieldSubmittedHandler GUIDSubmitted;

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x00080FA6 File Offset: 0x0007F3A6
		// (set) Token: 0x060014B1 RID: 5297 RVA: 0x00080FAE File Offset: 0x0007F3AE
		public Guid value
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

		// Token: 0x060014B2 RID: 5298 RVA: 0x00080FD0 File Offset: 0x0007F3D0
		protected virtual void updateText()
		{
			base.fieldComponent.text = this.value.ToString("N");
			base.fieldComponent.textComponent.text = base.fieldComponent.text;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00081016 File Offset: 0x0007F416
		protected virtual void triggerGUIDSubmitted(Guid value)
		{
			if (this.GUIDSubmitted != null)
			{
				this.GUIDSubmitted(this, value);
			}
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00081030 File Offset: 0x0007F430
		protected override void triggerSubmitted(string text)
		{
			try
			{
				Guid value = new Guid(text);
				this.triggerGUIDSubmitted(value);
			}
			catch
			{
				this.value = Guid.Empty;
			}
		}

		// Token: 0x04000BCD RID: 3021
		protected Guid _value;
	}
}
