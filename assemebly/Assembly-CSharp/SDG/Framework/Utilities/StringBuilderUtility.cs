using System;
using System.Text;

namespace SDG.Framework.Utilities
{
	// Token: 0x0200030E RID: 782
	public class StringBuilderUtility
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x00084A91 File Offset: 0x00082E91
		public static StringBuilder instance
		{
			get
			{
				if (StringBuilderUtility._instance == null)
				{
					StringBuilderUtility._instance = new StringBuilder();
				}
				StringBuilderUtility._instance.Length = 0;
				return StringBuilderUtility._instance;
			}
		}

		// Token: 0x04000C37 RID: 3127
		private static StringBuilder _instance;
	}
}
