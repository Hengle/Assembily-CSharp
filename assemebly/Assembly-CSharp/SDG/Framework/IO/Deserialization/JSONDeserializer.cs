using System;
using System.IO;
using Newtonsoft.Json;

namespace SDG.Framework.IO.Deserialization
{
	// Token: 0x020001B5 RID: 437
	public class JSONDeserializer : IDeserializer
	{
		// Token: 0x06000D08 RID: 3336 RVA: 0x0005F8D8 File Offset: 0x0005DCD8
		public T deserialize<T>(byte[] data, int offset)
		{
			MemoryStream memoryStream = new MemoryStream(data, offset, data.Length - offset);
			T result = this.deserialize<T>(memoryStream);
			memoryStream.Close();
			memoryStream.Dispose();
			return result;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0005F908 File Offset: 0x0005DD08
		public T deserialize<T>(MemoryStream memoryStream)
		{
			T result = default(T);
			StreamReader streamReader = new StreamReader(memoryStream);
			JsonReader jsonReader = new JsonTextReader(streamReader);
			JsonSerializer jsonSerializer = new JsonSerializer();
			try
			{
				result = jsonSerializer.Deserialize<T>(jsonReader);
			}
			finally
			{
				jsonReader.Close();
				streamReader.Close();
				streamReader.Dispose();
			}
			return result;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0005F968 File Offset: 0x0005DD68
		public T deserialize<T>(string path)
		{
			T result = default(T);
			StreamReader streamReader = new StreamReader(path);
			JsonReader jsonReader = new JsonTextReader(streamReader);
			JsonSerializer jsonSerializer = new JsonSerializer();
			try
			{
				result = jsonSerializer.Deserialize<T>(jsonReader);
			}
			finally
			{
				jsonReader.Close();
				streamReader.Close();
				streamReader.Dispose();
			}
			return result;
		}
	}
}
