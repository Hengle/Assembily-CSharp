using System;
using Newtonsoft.Json;

namespace SDG.Framework.Modules
{
	// Token: 0x020001F1 RID: 497
	public class ModuleDependency
	{
		// Token: 0x06000EDB RID: 3803 RVA: 0x00065B8F File Offset: 0x00063F8F
		public ModuleDependency()
		{
			this.Name = string.Empty;
			this.Version = "1.0.0.0";
		}

		// Token: 0x0400095D RID: 2397
		public string Name;

		// Token: 0x0400095E RID: 2398
		public string Version;

		// Token: 0x0400095F RID: 2399
		[JsonIgnore]
		public uint Version_Internal;
	}
}
