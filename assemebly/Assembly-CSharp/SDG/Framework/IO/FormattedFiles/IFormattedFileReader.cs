using System;
using System.Collections.Generic;

namespace SDG.Framework.IO.FormattedFiles
{
	// Token: 0x020001B8 RID: 440
	public interface IFormattedFileReader
	{
		// Token: 0x06000D10 RID: 3344
		IEnumerable<string> getKeys();

		// Token: 0x06000D11 RID: 3345
		bool containsKey(string key);

		// Token: 0x06000D12 RID: 3346
		void readKey(string key);

		// Token: 0x06000D13 RID: 3347
		int readArrayLength(string key);

		// Token: 0x06000D14 RID: 3348
		int readArrayLength();

		// Token: 0x06000D15 RID: 3349
		void readArrayIndex(string key, int index);

		// Token: 0x06000D16 RID: 3350
		void readArrayIndex(int index);

		// Token: 0x06000D17 RID: 3351
		string readValue(string key);

		// Token: 0x06000D18 RID: 3352
		string readValue(int index);

		// Token: 0x06000D19 RID: 3353
		string readValue(string key, int index);

		// Token: 0x06000D1A RID: 3354
		string readValue();

		// Token: 0x06000D1B RID: 3355
		object readValue(Type type, string key);

		// Token: 0x06000D1C RID: 3356
		object readValue(Type type, int index);

		// Token: 0x06000D1D RID: 3357
		object readValue(Type type, string key, int index);

		// Token: 0x06000D1E RID: 3358
		object readValue(Type type);

		// Token: 0x06000D1F RID: 3359
		T readValue<T>(string key);

		// Token: 0x06000D20 RID: 3360
		T readValue<T>(int index);

		// Token: 0x06000D21 RID: 3361
		T readValue<T>(string key, int index);

		// Token: 0x06000D22 RID: 3362
		T readValue<T>();

		// Token: 0x06000D23 RID: 3363
		IFormattedFileReader readObject(string key);

		// Token: 0x06000D24 RID: 3364
		IFormattedFileReader readObject(int index);

		// Token: 0x06000D25 RID: 3365
		IFormattedFileReader readObject(string key, int index);

		// Token: 0x06000D26 RID: 3366
		IFormattedFileReader readObject();
	}
}
