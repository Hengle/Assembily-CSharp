using System;
using System.IO;
using Newtonsoft.Json;

namespace SDG.Framework.IO.Serialization
{
	// Token: 0x020001C6 RID: 454
	public class JsonTextWriterFormatted : JsonTextWriter
	{
		// Token: 0x06000D89 RID: 3465 RVA: 0x000608F4 File Offset: 0x0005ECF4
		public JsonTextWriterFormatted(TextWriter textWriter) : base(textWriter)
		{
			base.IndentChar = '\t';
			base.Indentation = 1;
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0006090C File Offset: 0x0005ED0C
		public override void WriteStartArray()
		{
			base.Formatting = Formatting.None;
			base.WriteIndent();
			base.WriteStartArray();
			base.Formatting = Formatting.Indented;
		}
	}
}
