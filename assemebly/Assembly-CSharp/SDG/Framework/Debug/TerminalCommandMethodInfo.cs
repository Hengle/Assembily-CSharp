using System;
using System.Reflection;

namespace SDG.Framework.Debug
{
	// Token: 0x02000117 RID: 279
	public class TerminalCommandMethodInfo
	{
		// Token: 0x0600089C RID: 2204 RVA: 0x0004D3DB File Offset: 0x0004B7DB
		public TerminalCommandMethodInfo(string newCommand, string newDescription, MethodInfo newInfo)
		{
			this.command = newCommand;
			this.description = newDescription;
			this.info = newInfo;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0004D3F8 File Offset: 0x0004B7F8
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x0004D400 File Offset: 0x0004B800
		public string command { get; protected set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0004D409 File Offset: 0x0004B809
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x0004D411 File Offset: 0x0004B811
		public string description { get; protected set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0004D41A File Offset: 0x0004B81A
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x0004D422 File Offset: 0x0004B822
		public MethodInfo info { get; protected set; }
	}
}
