using System;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000146 RID: 326
	public interface IDirtyable
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060009D3 RID: 2515
		// (set) Token: 0x060009D4 RID: 2516
		bool isDirty { get; set; }

		// Token: 0x060009D5 RID: 2517
		void save();
	}
}
