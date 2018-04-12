using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SDG.Framework.Modules
{
	// Token: 0x020001F0 RID: 496
	public class ModuleConfig
	{
		// Token: 0x06000EDA RID: 3802 RVA: 0x00065B52 File Offset: 0x00063F52
		public ModuleConfig()
		{
			this.IsEnabled = true;
			this.Name = string.Empty;
			this.Version = "1.0.0.0";
			this.Dependencies = new List<ModuleDependency>(0);
			this.Assemblies = new List<ModuleAssembly>(0);
		}

		// Token: 0x04000955 RID: 2389
		public bool IsEnabled;

		// Token: 0x04000956 RID: 2390
		[JsonIgnore]
		public string DirectoryPath;

		// Token: 0x04000957 RID: 2391
		[JsonIgnore]
		public string FilePath;

		// Token: 0x04000958 RID: 2392
		public string Name;

		// Token: 0x04000959 RID: 2393
		public string Version;

		// Token: 0x0400095A RID: 2394
		[JsonIgnore]
		public uint Version_Internal;

		// Token: 0x0400095B RID: 2395
		public List<ModuleDependency> Dependencies;

		// Token: 0x0400095C RID: 2396
		public List<ModuleAssembly> Assemblies;
	}
}
