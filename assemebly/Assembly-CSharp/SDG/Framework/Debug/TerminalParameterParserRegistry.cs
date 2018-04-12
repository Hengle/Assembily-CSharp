using System;
using System.Collections.Generic;

namespace SDG.Framework.Debug
{
	// Token: 0x02000120 RID: 288
	public class TerminalParameterParserRegistry
	{
		// Token: 0x060008C4 RID: 2244 RVA: 0x0004D658 File Offset: 0x0004BA58
		public object parse(Type type, string input)
		{
			ITerminalParameterParser terminalParameterParser;
			if (this.parsers.TryGetValue(type, out terminalParameterParser))
			{
				return terminalParameterParser.parse(input);
			}
			return null;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0004D681 File Offset: 0x0004BA81
		public void add(Type type, ITerminalParameterParser parser)
		{
			this.parsers.Add(type, parser);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0004D690 File Offset: 0x0004BA90
		public void remove(Type type)
		{
			this.parsers.Remove(type);
		}

		// Token: 0x040006F4 RID: 1780
		private Dictionary<Type, ITerminalParameterParser> parsers = new Dictionary<Type, ITerminalParameterParser>();
	}
}
