using System;
using SDG.Framework.Translations;

namespace SDG.Framework.Debug
{
	// Token: 0x0200011C RID: 284
	public class TerminalCommandVariable<T>
	{
		// Token: 0x060008B2 RID: 2226 RVA: 0x0004D4C7 File Offset: 0x0004B8C7
		public TerminalCommandVariable(T defaultValue, TranslatedText changeText)
		{
			this.value = defaultValue;
			this.text = changeText;
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060008B3 RID: 2227 RVA: 0x0004D4E0 File Offset: 0x0004B8E0
		// (remove) Token: 0x060008B4 RID: 2228 RVA: 0x0004D518 File Offset: 0x0004B918
		public event TerminalCommandVariableChanged<T> changed;

		// Token: 0x060008B5 RID: 2229 RVA: 0x0004D54E File Offset: 0x0004B94E
		public T getValue()
		{
			return this.value;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0004D558 File Offset: 0x0004B958
		public void setValue(T newValue, bool print = false)
		{
			T oldValue = this.value;
			this.value = newValue;
			if (print)
			{
				TerminalUtility.printCommandPass(this.text.format(this.value));
			}
			this.triggerChanged(oldValue, this.value);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0004D5A1 File Offset: 0x0004B9A1
		protected virtual void triggerChanged(T oldValue, T newValue)
		{
			if (this.changed != null)
			{
				this.changed(this, oldValue, newValue);
			}
		}

		// Token: 0x040006EA RID: 1770
		protected T value;

		// Token: 0x040006EB RID: 1771
		protected TranslatedText text;
	}
}
