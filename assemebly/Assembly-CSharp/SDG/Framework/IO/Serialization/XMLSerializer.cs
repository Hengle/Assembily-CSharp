using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SDG.Framework.IO.Serialization
{
	// Token: 0x020001C7 RID: 455
	public class XMLSerializer : ISerializer
	{
		// Token: 0x06000D8C RID: 3468 RVA: 0x00060930 File Offset: 0x0005ED30
		public void serialize<T>(T instance, byte[] data, int index, out int size, bool isFormatted)
		{
			MemoryStream memoryStream = new MemoryStream(data, index, data.Length - index);
			this.serialize<T>(instance, memoryStream, isFormatted);
			size = (int)memoryStream.Position;
			memoryStream.Close();
			memoryStream.Dispose();
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0006096C File Offset: 0x0005ED6C
		public void serialize<T>(T instance, MemoryStream memoryStream, bool isFormatted)
		{
			XmlWriter xmlWriter = XmlWriter.Create(memoryStream, (!isFormatted) ? XMLSerializer.XML_WRITER_SETTINGS_UNFORMATTED : XMLSerializer.XML_WRITER_SETTINGS_FORMATTED);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			try
			{
				xmlSerializer.Serialize(xmlWriter, instance, XMLSerializer.XML_SERIALIZER_NAMESPACES);
				xmlWriter.Flush();
			}
			finally
			{
			}
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x000609D4 File Offset: 0x0005EDD4
		public void serialize<T>(T instance, string path, bool isFormatted)
		{
			StreamWriter streamWriter = new StreamWriter(path);
			XmlWriter xmlWriter = XmlWriter.Create(streamWriter, (!isFormatted) ? XMLSerializer.XML_WRITER_SETTINGS_UNFORMATTED : XMLSerializer.XML_WRITER_SETTINGS_FORMATTED);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			try
			{
				xmlSerializer.Serialize(xmlWriter, instance, XMLSerializer.XML_SERIALIZER_NAMESPACES);
				xmlWriter.Flush();
			}
			finally
			{
				streamWriter.Close();
				streamWriter.Dispose();
			}
		}

		// Token: 0x040008F1 RID: 2289
		private static readonly XmlSerializerNamespaces XML_SERIALIZER_NAMESPACES = new XmlSerializerNamespaces(new XmlQualifiedName[]
		{
			XmlQualifiedName.Empty
		});

		// Token: 0x040008F2 RID: 2290
		private static readonly XmlWriterSettings XML_WRITER_SETTINGS_FORMATTED = new XmlWriterSettings
		{
			Indent = true,
			OmitXmlDeclaration = true,
			Encoding = new UTF8Encoding()
		};

		// Token: 0x040008F3 RID: 2291
		private static readonly XmlWriterSettings XML_WRITER_SETTINGS_UNFORMATTED = new XmlWriterSettings
		{
			Indent = false,
			OmitXmlDeclaration = true,
			Encoding = new UTF8Encoding()
		};
	}
}
