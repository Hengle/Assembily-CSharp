using System;
using System.Collections.Generic;
using SDG.Framework.Translations;

namespace SDG.Framework.Devkit.Transactions
{
	// Token: 0x0200017D RID: 381
	public class DevkitTransactionGroup
	{
		// Token: 0x06000B6C RID: 2924 RVA: 0x0005A59B File Offset: 0x0005899B
		public DevkitTransactionGroup(TranslatedText newName)
		{
			this.name = newName;
			this.transactions = new List<IDevkitTransaction>();
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0005A5B5 File Offset: 0x000589B5
		// (set) Token: 0x06000B6E RID: 2926 RVA: 0x0005A5BD File Offset: 0x000589BD
		public TranslatedText name { get; protected set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0005A5C6 File Offset: 0x000589C6
		// (set) Token: 0x06000B70 RID: 2928 RVA: 0x0005A5CE File Offset: 0x000589CE
		public List<IDevkitTransaction> transactions { get; protected set; }

		// Token: 0x06000B71 RID: 2929 RVA: 0x0005A5D7 File Offset: 0x000589D7
		public void record(IDevkitTransaction transaction)
		{
			transaction.begin();
			this.transactions.Add(transaction);
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0005A5EC File Offset: 0x000589EC
		public bool delta
		{
			get
			{
				for (int i = this.transactions.Count - 1; i >= 0; i--)
				{
					if (!this.transactions[i].delta)
					{
						this.transactions.RemoveAt(i);
					}
				}
				return this.transactions.Count > 0;
			}
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0005A648 File Offset: 0x00058A48
		public void undo()
		{
			for (int i = 0; i < this.transactions.Count; i++)
			{
				this.transactions[i].undo();
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0005A684 File Offset: 0x00058A84
		public void redo()
		{
			for (int i = 0; i < this.transactions.Count; i++)
			{
				this.transactions[i].redo();
			}
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0005A6C0 File Offset: 0x00058AC0
		public void end()
		{
			for (int i = 0; i < this.transactions.Count; i++)
			{
				this.transactions[i].end();
			}
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0005A6FC File Offset: 0x00058AFC
		public void forget()
		{
			for (int i = 0; i < this.transactions.Count; i++)
			{
				this.transactions[i].forget();
			}
		}
	}
}
