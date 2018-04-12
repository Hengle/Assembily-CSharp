using System;
using System.Collections.Generic;

namespace SDG.Framework.Debug
{
	// Token: 0x0200011D RID: 285
	public class TerminalLogCategory
	{
		// Token: 0x060008B8 RID: 2232 RVA: 0x0004D5BC File Offset: 0x0004B9BC
		public TerminalLogCategory(string newInternalName, string newDisplayName, bool defaultIsVisible = true)
		{
			this.isVisible = defaultIsVisible;
			this.internalName = newInternalName;
			this.displayName = newDisplayName;
			this.messages = new List<TerminalLogMessage>();
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0004D5E4 File Offset: 0x0004B9E4
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x0004D5EC File Offset: 0x0004B9EC
		public bool isVisible { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0004D5F5 File Offset: 0x0004B9F5
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x0004D5FD File Offset: 0x0004B9FD
		public string internalName { get; protected set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0004D606 File Offset: 0x0004BA06
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x0004D60E File Offset: 0x0004BA0E
		public string displayName { get; protected set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x0004D617 File Offset: 0x0004BA17
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x0004D61F File Offset: 0x0004BA1F
		public List<TerminalLogMessage> messages { get; protected set; }
	}
}
