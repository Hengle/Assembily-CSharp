using System;

namespace SDG.Framework.Debug
{
	// Token: 0x0200011A RID: 282
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class TerminalCommandPropertyAttribute : Attribute
	{
		// Token: 0x060008AD RID: 2221 RVA: 0x0004D4AA File Offset: 0x0004B8AA
		public TerminalCommandPropertyAttribute(string newCommand, string newDescription, object newDefaultValue)
		{
			this.command = newCommand;
			this.description = newDescription;
			this.defaultValue = newDefaultValue;
		}

		// Token: 0x040006E6 RID: 1766
		public string command;

		// Token: 0x040006E7 RID: 1767
		public string description;

		// Token: 0x040006E8 RID: 1768
		public object defaultValue;
	}
}
