using System;
using System.Collections.Generic;

namespace SDG.Framework.Debug
{
	// Token: 0x0200011F RID: 287
	public class TerminalLogMessageTimestampComparer : IComparer<TerminalLogMessage>
	{
		// Token: 0x060008C2 RID: 2242 RVA: 0x0004D630 File Offset: 0x0004BA30
		public int Compare(TerminalLogMessage x, TerminalLogMessage y)
		{
			return x.timestamp.CompareTo(y.timestamp);
		}
	}
}
