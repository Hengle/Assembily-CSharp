using System;
using System.Runtime.InteropServices;

namespace SDG.Framework.Debug
{
	// Token: 0x0200010C RID: 268
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct InspectableFilePath : IInspectablePath
	{
		// Token: 0x0600083F RID: 2111 RVA: 0x0004CB54 File Offset: 0x0004AF54
		public InspectableFilePath(string newExtension)
		{
			this.absolutePath = string.Empty;
			this.extension = newExtension;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0004CB68 File Offset: 0x0004AF68
		// (set) Token: 0x06000841 RID: 2113 RVA: 0x0004CB70 File Offset: 0x0004AF70
		public string absolutePath { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0004CB79 File Offset: 0x0004AF79
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x0004CB81 File Offset: 0x0004AF81
		public string extension { get; private set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0004CB8A File Offset: 0x0004AF8A
		public bool isValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.absolutePath);
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0004CB9A File Offset: 0x0004AF9A
		public override string ToString()
		{
			return this.absolutePath;
		}
	}
}
