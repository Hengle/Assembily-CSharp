using System;
using System.Collections.Generic;
using System.IO;

namespace SDG.Framework.IO.FormattedFiles.KeyValueTables
{
	// Token: 0x020001C2 RID: 450
	public class LimitedKeyValueTableReader : KeyValueTableReader
	{
		// Token: 0x06000D79 RID: 3449 RVA: 0x00060685 File Offset: 0x0005EA85
		public LimitedKeyValueTableReader()
		{
			this.limit = null;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00060694 File Offset: 0x0005EA94
		public LimitedKeyValueTableReader(StreamReader input) : this(null, input)
		{
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0006069E File Offset: 0x0005EA9E
		public LimitedKeyValueTableReader(string newLimit, StreamReader input) : base(input)
		{
			this.limit = newLimit;
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x000606AE File Offset: 0x0005EAAE
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x000606B6 File Offset: 0x0005EAB6
		public string limit { get; protected set; }

		// Token: 0x06000D7E RID: 3454 RVA: 0x000606BF File Offset: 0x0005EABF
		protected override bool canContinueReadDictionary(StreamReader input, Dictionary<string, object> scope)
		{
			return !(this.dictionaryKey == this.limit) && base.canContinueReadDictionary(input, scope);
		}
	}
}
