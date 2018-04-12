using System;
using System.Reflection;

namespace SDG.Framework.Debug
{
	// Token: 0x02000115 RID: 277
	public class TerminalCommand
	{
		// Token: 0x06000894 RID: 2196 RVA: 0x0004D375 File Offset: 0x0004B775
		public TerminalCommand(TerminalCommandMethodInfo newMethod, TerminalCommandParameterInfo[] newParameters, MethodInfo newCurrentValue = null)
		{
			this.method = newMethod;
			this.parameters = newParameters;
			this.currentValue = newCurrentValue;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0004D392 File Offset: 0x0004B792
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x0004D39A File Offset: 0x0004B79A
		public TerminalCommandMethodInfo method { get; protected set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0004D3A3 File Offset: 0x0004B7A3
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x0004D3AB File Offset: 0x0004B7AB
		public TerminalCommandParameterInfo[] parameters { get; protected set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0004D3B4 File Offset: 0x0004B7B4
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x0004D3BC File Offset: 0x0004B7BC
		public MethodInfo currentValue { get; protected set; }
	}
}
