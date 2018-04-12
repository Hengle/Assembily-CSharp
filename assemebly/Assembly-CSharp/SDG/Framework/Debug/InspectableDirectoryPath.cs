using System;
using System.Runtime.InteropServices;

namespace SDG.Framework.Debug
{
	// Token: 0x0200010A RID: 266
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct InspectableDirectoryPath : IInspectablePath
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0004CB1C File Offset: 0x0004AF1C
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x0004CB24 File Offset: 0x0004AF24
		public string absolutePath { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0004CB2D File Offset: 0x0004AF2D
		public bool isValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.absolutePath);
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0004CB3D File Offset: 0x0004AF3D
		public override string ToString()
		{
			return this.absolutePath;
		}
	}
}
