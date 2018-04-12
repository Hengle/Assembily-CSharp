using System;
using System.IO;
using System.Xml.Serialization;

namespace SDG.Framework.IO.Deserialization
{
	// Token: 0x020001B6 RID: 438
	public class XMLDeserializer : IDeserializer
	{
		// Token: 0x06000D0C RID: 3340 RVA: 0x0005F9D0 File Offset: 0x0005DDD0
		public T deserialize<T>(byte[] data, int offset)
		{
			MemoryStream memoryStream = new MemoryStream(data, offset, data.Length - offset);
			T result = this.deserialize<T>(memoryStream);
			memoryStream.Close();
			memoryStream.Dispose();
			return result;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0005FA00 File Offset: 0x0005DE00
		public T deserialize<T>(MemoryStream memoryStream)
		{
			T result = default(T);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			try
			{
				result = (T)((object)xmlSerializer.Deserialize(memoryStream));
			}
			finally
			{
			}
			return result;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0005FA4C File Offset: 0x0005DE4C
		public T deserialize<T>(string path)
		{
			T result = default(T);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			StreamReader streamReader = new StreamReader(path);
			try
			{
				result = (T)((object)xmlSerializer.Deserialize(streamReader));
			}
			finally
			{
				streamReader.Close();
				streamReader.Dispose();
			}
			return result;
		}
	}
}
