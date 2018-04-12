using System;
using System.IO;

namespace SDG.Framework.IO.Serialization
{
	// Token: 0x020001C4 RID: 452
	public interface ISerializer
	{
		// Token: 0x06000D82 RID: 3458
		void serialize<T>(T instance, byte[] data, int offset, out int size, bool isFormatted);

		// Token: 0x06000D83 RID: 3459
		void serialize<T>(T instance, MemoryStream memoryStream, bool isFormatted);

		// Token: 0x06000D84 RID: 3460
		void serialize<T>(T instance, string path, bool isFormatted);
	}
}
