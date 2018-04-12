using System;

namespace SDG.Framework.Debug
{
	// Token: 0x02000118 RID: 280
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
	public class TerminalCommandParameterAttribute : Attribute
	{
		// Token: 0x060008A3 RID: 2211 RVA: 0x0004D42B File Offset: 0x0004B82B
		public TerminalCommandParameterAttribute(string newName, string newDescription)
		{
			this.name = newName;
			this.description = newDescription;
		}

		// Token: 0x040006E0 RID: 1760
		public string name;

		// Token: 0x040006E1 RID: 1761
		public string description;
	}
}
