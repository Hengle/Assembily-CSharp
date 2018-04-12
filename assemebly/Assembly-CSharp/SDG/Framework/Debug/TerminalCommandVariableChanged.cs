using System;

namespace SDG.Framework.Debug
{
	// Token: 0x0200011B RID: 283
	// (Invoke) Token: 0x060008AF RID: 2223
	public delegate void TerminalCommandVariableChanged<T>(TerminalCommandVariable<T> variable, T oldValue, T newValue);
}
