using System;
using System.Collections.Generic;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000128 RID: 296
	public class DevkitBrowserSelection
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0004E523 File Offset: 0x0004C923
		public static List<string> selectedPaths
		{
			get
			{
				return DevkitBrowserSelection._selectedPaths;
			}
		}

		// Token: 0x04000705 RID: 1797
		private static List<string> _selectedPaths = new List<string>();
	}
}
