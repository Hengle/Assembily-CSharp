using System;
using System.Collections.Generic;
using SDG.Framework.UI.Sleek2;

namespace SDG.Framework.UI.Devkit
{
	// Token: 0x02000238 RID: 568
	public class DevkitToolbarBranch
	{
		// Token: 0x060010B8 RID: 4280 RVA: 0x0006DCD0 File Offset: 0x0006C0D0
		public DevkitToolbarBranch()
		{
			this.tree = new Dictionary<string, DevkitToolbarBranch>();
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0006DCE3 File Offset: 0x0006C0E3
		// (set) Token: 0x060010BA RID: 4282 RVA: 0x0006DCEB File Offset: 0x0006C0EB
		public Dictionary<string, DevkitToolbarBranch> tree { get; protected set; }

		// Token: 0x04000A13 RID: 2579
		public Sleek2HoverDropdown dropdown;
	}
}
