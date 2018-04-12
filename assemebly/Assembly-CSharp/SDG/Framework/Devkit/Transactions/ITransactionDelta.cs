using System;

namespace SDG.Framework.Devkit.Transactions
{
	// Token: 0x02000185 RID: 389
	public interface ITransactionDelta
	{
		// Token: 0x06000BB0 RID: 2992
		void undo(object instance);

		// Token: 0x06000BB1 RID: 2993
		void redo(object instance);
	}
}
