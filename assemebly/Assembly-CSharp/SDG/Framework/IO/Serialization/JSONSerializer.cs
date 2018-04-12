using System;
using System.IO;
using Newtonsoft.Json;

namespace SDG.Framework.IO.Serialization
{
	// Token: 0x020001C5 RID: 453
	public class JSONSerializer : ISerializer
	{
		// Token: 0x06000D86 RID: 3462 RVA: 0x000607D8 File Offset: 0x0005EBD8
		public void serialize<T>(T instance, byte[] data, int index, out int size, bool isFormatted)
		{
			MemoryStream memoryStream = new MemoryStream(data, index, data.Length - index);
			this.serialize<T>(instance, memoryStream, isFormatted);
			size = (int)memoryStream.Position;
			memoryStream.Close();
			memoryStream.Dispose();
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00060814 File Offset: 0x0005EC14
		public void serialize<T>(T instance, MemoryStream memoryStream, bool isFormatted)
		{
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			JsonWriter jsonWriter = (!isFormatted) ? new JsonTextWriterFormatted(streamWriter) : new JsonTextWriter(streamWriter);
			JsonSerializer jsonSerializer = new JsonSerializer();
			try
			{
				jsonSerializer.Serialize(jsonWriter, instance);
				jsonWriter.Flush();
			}
			finally
			{
				jsonWriter.Close();
				streamWriter.Close();
				streamWriter.Dispose();
			}
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00060884 File Offset: 0x0005EC84
		public void serialize<T>(T instance, string path, bool isFormatted)
		{
			StreamWriter streamWriter = new StreamWriter(path);
			JsonWriter jsonWriter = new JsonTextWriterFormatted(streamWriter);
			JsonSerializer jsonSerializer = new JsonSerializer();
			jsonSerializer.Formatting = ((!isFormatted) ? Formatting.None : Formatting.Indented);
			try
			{
				jsonSerializer.Serialize(jsonWriter, instance);
				jsonWriter.Flush();
			}
			finally
			{
				jsonWriter.Close();
				streamWriter.Close();
				streamWriter.Dispose();
			}
		}
	}
}
