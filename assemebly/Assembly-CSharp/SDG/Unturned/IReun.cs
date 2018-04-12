using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004A1 RID: 1185
	public interface IReun
	{
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001F43 RID: 8003
		int step { get; }

		// Token: 0x06001F44 RID: 8004
		Transform redo();

		// Token: 0x06001F45 RID: 8005
		void undo();
	}
}
