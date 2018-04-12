using System;
using System.IO;

namespace SDG.Framework.IO.Deserialization
{
	// Token: 0x020001B4 RID: 436
	public interface IDeserializer
	{
		// Token: 0x06000D04 RID: 3332
		T deserialize<T>(byte[] data, int offset);

		// Token: 0x06000D05 RID: 3333
		T deserialize<T>(MemoryStream memoryStream);

		// Token: 0x06000D06 RID: 3334
		T deserialize<T>(string path);
	}
}
