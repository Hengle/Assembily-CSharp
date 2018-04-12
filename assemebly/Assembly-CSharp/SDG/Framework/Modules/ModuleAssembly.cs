using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SDG.Framework.Modules
{
	// Token: 0x020001EE RID: 494
	public class ModuleAssembly
	{
		// Token: 0x06000ED7 RID: 3799 RVA: 0x00065A88 File Offset: 0x00063E88
		public ModuleAssembly()
		{
			this.Path = string.Empty;
			this.Role = EModuleRole.None;
		}

		// Token: 0x04000953 RID: 2387
		public string Path;

		// Token: 0x04000954 RID: 2388
		[JsonConverter(typeof(StringEnumConverter))]
		public EModuleRole Role;
	}
}
