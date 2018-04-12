using System;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002D2 RID: 722
	public class Sleek2IntField : Sleek2Field
	{
		// Token: 0x060014DC RID: 5340 RVA: 0x000813F0 File Offset: 0x0007F7F0
		public Sleek2IntField()
		{
			base.gameObject.name = "Int_Field";
			this.updateText();
		}

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x060014DD RID: 5341 RVA: 0x00081410 File Offset: 0x0007F810
		// (remove) Token: 0x060014DE RID: 5342 RVA: 0x00081448 File Offset: 0x0007F848
		public event IntFieldSubmittedHandler intSubmitted;

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x0008147E File Offset: 0x0007F87E
		// (set) Token: 0x060014E0 RID: 5344 RVA: 0x00081486 File Offset: 0x0007F886
		public int value
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

		// Token: 0x060014E1 RID: 5345 RVA: 0x000814A4 File Offset: 0x0007F8A4
		protected virtual void updateText()
		{
			base.fieldComponent.text = this.value.ToString();
			base.fieldComponent.textComponent.text = base.fieldComponent.text;
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x000814EB File Offset: 0x0007F8EB
		protected virtual void triggerIntSubmitted(int value)
		{
			if (this.intSubmitted != null)
			{
				this.intSubmitted(this, value);
			}
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x00081505 File Offset: 0x0007F905
		protected override void triggerSubmitted(string text)
		{
			if (int.TryParse(text, out this._value))
			{
				this.triggerIntSubmitted(this.value);
			}
			else
			{
				this.value = 0;
			}
		}

		// Token: 0x04000BD8 RID: 3032
		protected int _value;
	}
}
