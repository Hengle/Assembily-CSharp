using System;

namespace SDG.Framework.Debug
{
	// Token: 0x02000119 RID: 281
	public class TerminalCommandParameterInfo
	{
		// Token: 0x060008A4 RID: 2212 RVA: 0x0004D441 File Offset: 0x0004B841
		public TerminalCommandParameterInfo(string newName, string newDescription, Type newType, object newDefaultValue)
		{
			this.name = newName;
			this.description = newDescription;
			this.type = newType;
			this.defaultValue = newDefaultValue;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0004D466 File Offset: 0x0004B866
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x0004D46E File Offset: 0x0004B86E
		public string name { get; protected set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0004D477 File Offset: 0x0004B877
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x0004D47F File Offset: 0x0004B87F
		public string description { get; protected set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0004D488 File Offset: 0x0004B888
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x0004D490 File Offset: 0x0004B890
		public Type type { get; protected set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0004D499 File Offset: 0x0004B899
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x0004D4A1 File Offset: 0x0004B8A1
		public object defaultValue { get; protected set; }
	}
}
