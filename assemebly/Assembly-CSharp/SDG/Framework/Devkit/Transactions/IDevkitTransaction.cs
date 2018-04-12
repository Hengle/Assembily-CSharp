using System;

namespace SDG.Framework.Devkit.Transactions
{
	// Token: 0x02000184 RID: 388
	public interface IDevkitTransaction
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000BAA RID: 2986
		bool delta { get; }

		// Token: 0x06000BAB RID: 2987
		void undo();

		// Token: 0x06000BAC RID: 2988
		void redo();

		// Token: 0x06000BAD RID: 2989
		void begin();

		// Token: 0x06000BAE RID: 2990
		void end();

		// Token: 0x06000BAF RID: 2991
		void forget();
	}
}
