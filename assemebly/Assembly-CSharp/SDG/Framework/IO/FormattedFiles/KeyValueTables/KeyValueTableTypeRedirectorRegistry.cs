using System;
using System.Collections.Generic;
using SDG.Framework.Devkit;

namespace SDG.Framework.IO.FormattedFiles.KeyValueTables
{
	// Token: 0x020001BF RID: 447
	public static class KeyValueTableTypeRedirectorRegistry
	{
		// Token: 0x06000D5E RID: 3422 RVA: 0x00060220 File Offset: 0x0005E620
		static KeyValueTableTypeRedirectorRegistry()
		{
			KeyValueTableTypeRedirectorRegistry.add("SDG.Framework.Landscapes.PlayerClipVolume, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", typeof(PlayerClipVolume).AssemblyQualifiedName);
			KeyValueTableTypeRedirectorRegistry.add("SDG.Framework.Foliage.KillVolume, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", typeof(KillVolume).AssemblyQualifiedName);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00060260 File Offset: 0x0005E660
		public static string chase(string assemblyQualifiedName)
		{
			string assemblyQualifiedName2;
			if (KeyValueTableTypeRedirectorRegistry.redirects.TryGetValue(assemblyQualifiedName, out assemblyQualifiedName2))
			{
				return KeyValueTableTypeRedirectorRegistry.chase(assemblyQualifiedName2);
			}
			return assemblyQualifiedName;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00060287 File Offset: 0x0005E687
		public static void add(string oldAssemblyQualifiedName, string newAssemblyQualifiedName)
		{
			KeyValueTableTypeRedirectorRegistry.redirects.Add(oldAssemblyQualifiedName, newAssemblyQualifiedName);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00060295 File Offset: 0x0005E695
		public static void remove(string oldAssemblyQualifiedName)
		{
			KeyValueTableTypeRedirectorRegistry.redirects.Remove(oldAssemblyQualifiedName);
		}

		// Token: 0x040008E5 RID: 2277
		private static Dictionary<string, string> redirects = new Dictionary<string, string>();
	}
}
