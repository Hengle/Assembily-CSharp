using System;

namespace SDG.Framework.Debug
{
	// Token: 0x02000116 RID: 278
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class TerminalCommandMethodAttribute : Attribute
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x0004D3C5 File Offset: 0x0004B7C5
		public TerminalCommandMethodAttribute(string newCommand, string newDescription)
		{
			this.command = newCommand;
			this.description = newDescription;
		}

		// Token: 0x040006DB RID: 1755
		public string command;

		// Token: 0x040006DC RID: 1756
		public string description;
	}
}
