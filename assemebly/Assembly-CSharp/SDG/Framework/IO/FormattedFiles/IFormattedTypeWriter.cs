using System;

namespace SDG.Framework.IO.FormattedFiles
{
	// Token: 0x020001BC RID: 444
	public interface IFormattedTypeWriter
	{
		// Token: 0x06000D36 RID: 3382
		void write(IFormattedFileWriter writer, object value);
	}
}
