using System;

namespace SDG.Framework.IO.FormattedFiles
{
	// Token: 0x020001BA RID: 442
	public interface IFormattedFileWriter
	{
		// Token: 0x06000D28 RID: 3368
		void writeKey(string key);

		// Token: 0x06000D29 RID: 3369
		void writeValue(string key, string value);

		// Token: 0x06000D2A RID: 3370
		void writeValue(string value);

		// Token: 0x06000D2B RID: 3371
		void writeValue(string key, object value);

		// Token: 0x06000D2C RID: 3372
		void writeValue(object value);

		// Token: 0x06000D2D RID: 3373
		void writeValue<T>(string key, T value);

		// Token: 0x06000D2E RID: 3374
		void writeValue<T>(T value);

		// Token: 0x06000D2F RID: 3375
		void beginObject();

		// Token: 0x06000D30 RID: 3376
		void beginObject(string key);

		// Token: 0x06000D31 RID: 3377
		void endObject();

		// Token: 0x06000D32 RID: 3378
		void beginArray(string key);

		// Token: 0x06000D33 RID: 3379
		void beginArray();

		// Token: 0x06000D34 RID: 3380
		void endArray();
	}
}
