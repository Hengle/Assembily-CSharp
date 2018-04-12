using System;
using System.Collections.Generic;

namespace SDG.Framework.Modules
{
	// Token: 0x020001EF RID: 495
	public class ModuleComparer : IComparer<ModuleConfig>
	{
		// Token: 0x06000ED9 RID: 3801 RVA: 0x00065AAC File Offset: 0x00063EAC
		public int Compare(ModuleConfig x, ModuleConfig y)
		{
			for (int i = 0; i < y.Dependencies.Count; i++)
			{
				ModuleDependency moduleDependency = y.Dependencies[i];
				if (moduleDependency.Name == x.Name)
				{
					return -1;
				}
			}
			for (int j = 0; j < x.Dependencies.Count; j++)
			{
				ModuleDependency moduleDependency2 = x.Dependencies[j];
				if (moduleDependency2.Name == y.Name)
				{
					return 1;
				}
			}
			return x.Dependencies.Count - y.Dependencies.Count;
		}
	}
}
