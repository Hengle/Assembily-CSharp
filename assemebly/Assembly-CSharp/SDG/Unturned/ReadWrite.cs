using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004AC RID: 1196
	public class ReadWrite
	{
		// Token: 0x06001FF5 RID: 8181 RVA: 0x000B08B4 File Offset: 0x000AECB4
		public static bool appIn(byte[] h, byte p)
		{
			Block block = ReadWrite.readBlock("/Bundles/Sources/Animation/appout.log", false, 0);
			byte[] hash_ = block.readByteArray();
			byte[] hash_2 = block.readByteArray();
			byte[] hash_3 = block.readByteArray();
			byte[] hash_4 = block.readByteArray();
			byte[] hash_5 = block.readByteArray();
			byte[] hash_6 = block.readByteArray();
			byte[] hash_7 = block.readByteArray();
			if (p == 0)
			{
				if (Hash.verifyHash(h, hash_))
				{
					return true;
				}
			}
			else if (p == 1)
			{
				if (Hash.verifyHash(h, hash_))
				{
					return true;
				}
				if (Hash.verifyHash(h, hash_2))
				{
					return true;
				}
				if (Hash.verifyHash(h, hash_3))
				{
					return true;
				}
			}
			else if (p == 2)
			{
				if (Hash.verifyHash(h, hash_4))
				{
					return true;
				}
				if (Hash.verifyHash(h, hash_5))
				{
					return true;
				}
			}
			else if (p == 3)
			{
				if (Hash.verifyHash(h, hash_6))
				{
					return true;
				}
				if (Hash.verifyHash(h, hash_7))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x000B09A4 File Offset: 0x000AEDA4
		public static byte[] appOut()
		{
			FileStream fileStream = new FileStream(ReadWrite.PATH + "/Unturned_Data/Managed/Assembly-CSharp.dll", FileMode.Open, FileAccess.Read, FileShare.Read);
			byte[] array = new byte[fileStream.Length];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			fileStream.Dispose();
			return Hash.SHA1(array);
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x000B09F4 File Offset: 0x000AEDF4
		public static T deserializeJSON<T>(string path, bool useCloud)
		{
			return ReadWrite.deserializeJSON<T>(path, useCloud, true);
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x000B0A00 File Offset: 0x000AEE00
		public static T deserializeJSON<T>(string path, bool useCloud, bool usePath)
		{
			T result = default(T);
			byte[] array = ReadWrite.readBytes(path, useCloud, usePath);
			if (array == null)
			{
				return result;
			}
			string @string = Encoding.UTF8.GetString(array);
			if (@string == null)
			{
				return result;
			}
			return JsonConvert.DeserializeObject<T>(@string);
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x000B0A44 File Offset: 0x000AEE44
		public static byte[] cloudFileRead(string path)
		{
			if (!ReadWrite.cloudFileExists(path))
			{
				return null;
			}
			int num;
			Provider.provider.cloudService.getSize(path, out num);
			byte[] array = new byte[num];
			if (!Provider.provider.cloudService.read(path, array))
			{
				Debug.LogError("Failed to read the correct file size.");
				return null;
			}
			return array;
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x000B0A9B File Offset: 0x000AEE9B
		public static void cloudFileWrite(string path, byte[] bytes, int size)
		{
			if (!Provider.provider.cloudService.write(path, bytes, size))
			{
				Debug.LogError("Failed to write file.");
			}
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x000B0ABE File Offset: 0x000AEEBE
		public static void cloudFileDelete(string path)
		{
			Provider.provider.cloudService.delete(path);
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x000B0AD4 File Offset: 0x000AEED4
		public static bool cloudFileExists(string path)
		{
			bool result;
			Provider.provider.cloudService.exists(path, out result);
			return result;
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x000B0AF5 File Offset: 0x000AEEF5
		public static void serializeJSON<T>(string path, bool useCloud, T instance)
		{
			ReadWrite.serializeJSON<T>(path, useCloud, true, instance);
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x000B0B00 File Offset: 0x000AEF00
		public static void serializeJSON<T>(string path, bool useCloud, bool usePath, T instance)
		{
			string s = JsonConvert.SerializeObject(instance, Newtonsoft.Json.Formatting.Indented);
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			ReadWrite.writeBytes(path, useCloud, usePath, bytes, bytes.Length);
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x000B0B32 File Offset: 0x000AEF32
		public static T deserializeXML<T>(string path, bool useCloud)
		{
			return ReadWrite.deserializeXML<T>(path, useCloud, true);
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x000B0B3C File Offset: 0x000AEF3C
		public static T deserializeXML<T>(string path, bool useCloud, bool usePath)
		{
			T result = default(T);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			if (useCloud)
			{
				MemoryStream memoryStream = new MemoryStream(ReadWrite.cloudFileRead(path));
				try
				{
					result = (T)((object)xmlSerializer.Deserialize(memoryStream));
				}
				finally
				{
					memoryStream.Close();
					memoryStream.Dispose();
				}
				return result;
			}
			if (usePath)
			{
				path += ReadWrite.PATH;
			}
			if (!Directory.Exists(Path.GetDirectoryName(path)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(path));
			}
			if (!File.Exists(path))
			{
				Debug.Log("Failed to find file at: " + path);
				return result;
			}
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

		// Token: 0x06002001 RID: 8193 RVA: 0x000B0C2C File Offset: 0x000AF02C
		public static void serializeXML<T>(string path, bool useCloud, T instance)
		{
			ReadWrite.serializeXML<T>(path, useCloud, true, instance);
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x000B0C38 File Offset: 0x000AF038
		public static void serializeXML<T>(string path, bool useCloud, bool usePath, T instance)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			if (useCloud)
			{
				MemoryStream memoryStream = new MemoryStream();
				XmlWriter xmlWriter = XmlWriter.Create(memoryStream, ReadWrite.XML_WRITER_SETTINGS);
				try
				{
					xmlSerializer.Serialize(xmlWriter, instance, ReadWrite.XML_SERIALIZER_NAMESPACES);
					ReadWrite.cloudFileWrite(path, memoryStream.GetBuffer(), (int)memoryStream.Length);
				}
				finally
				{
					xmlWriter.Close();
					memoryStream.Close();
					memoryStream.Dispose();
				}
			}
			else
			{
				if (usePath)
				{
					path = ReadWrite.PATH + path;
				}
				if (!Directory.Exists(Path.GetDirectoryName(path)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(path));
				}
				StreamWriter streamWriter = new StreamWriter(path);
				try
				{
					xmlSerializer.Serialize(streamWriter, instance, ReadWrite.XML_SERIALIZER_NAMESPACES);
				}
				finally
				{
					streamWriter.Close();
					streamWriter.Dispose();
				}
			}
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x000B0D28 File Offset: 0x000AF128
		public static byte[] readBytes(string path, bool useCloud)
		{
			return ReadWrite.readBytes(path, useCloud, true);
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x000B0D34 File Offset: 0x000AF134
		public static byte[] readBytes(string path, bool useCloud, bool usePath)
		{
			if (useCloud)
			{
				return ReadWrite.cloudFileRead(path);
			}
			if (usePath)
			{
				path = ReadWrite.PATH + path;
			}
			if (!Directory.Exists(Path.GetDirectoryName(path)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(path));
			}
			if (!File.Exists(path))
			{
				Debug.Log("Failed to find file at: " + path);
				return null;
			}
			FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			byte[] array = new byte[fileStream.Length];
			int num = fileStream.Read(array, 0, array.Length);
			if (num != array.Length)
			{
				Debug.LogError("Failed to read the correct file size.");
				return null;
			}
			fileStream.Close();
			fileStream.Dispose();
			return array;
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x000B0DDF File Offset: 0x000AF1DF
		public static Data readData(string path, bool useCloud)
		{
			return ReadWrite.readData(path, useCloud, true);
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x000B0DEC File Offset: 0x000AF1EC
		public static Data readData(string path, bool useCloud, bool usePath)
		{
			byte[] array = ReadWrite.readBytes(path, useCloud, usePath);
			if (array == null)
			{
				return null;
			}
			string @string = Encoding.UTF8.GetString(array);
			if (@string == string.Empty)
			{
				return new Data();
			}
			return new Data(@string);
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x000B0E32 File Offset: 0x000AF232
		public static Block readBlock(string path, bool useCloud, byte prefix)
		{
			return ReadWrite.readBlock(path, useCloud, true, prefix);
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x000B0E40 File Offset: 0x000AF240
		public static Block readBlock(string path, bool useCloud, bool usePath, byte prefix)
		{
			byte[] array = ReadWrite.readBytes(path, useCloud, usePath);
			if (array == null)
			{
				return null;
			}
			return new Block((int)prefix, array);
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x000B0E65 File Offset: 0x000AF265
		public static void writeBytes(string path, bool useCloud, byte[] bytes)
		{
			ReadWrite.writeBytes(path, useCloud, true, bytes, bytes.Length);
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x000B0E73 File Offset: 0x000AF273
		public static void writeBytes(string path, bool useCloud, byte[] bytes, int size)
		{
			ReadWrite.writeBytes(path, useCloud, true, bytes, size);
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x000B0E7F File Offset: 0x000AF27F
		public static void writeBytes(string path, bool useCloud, bool usePath, byte[] bytes)
		{
			ReadWrite.writeBytes(path, useCloud, usePath, bytes, bytes.Length);
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x000B0E90 File Offset: 0x000AF290
		public static void writeBytes(string path, bool useCloud, bool usePath, byte[] bytes, int size)
		{
			if (useCloud)
			{
				ReadWrite.cloudFileWrite(path, bytes, size);
			}
			else
			{
				if (usePath)
				{
					path = ReadWrite.PATH + path;
				}
				if (!Directory.Exists(Path.GetDirectoryName(path)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(path));
				}
				FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
				fileStream.Write(bytes, 0, size);
				fileStream.SetLength((long)size);
				fileStream.Flush();
				fileStream.Close();
				fileStream.Dispose();
			}
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x000B0F0D File Offset: 0x000AF30D
		public static void writeData(string path, bool useCloud, Data data)
		{
			ReadWrite.writeData(path, useCloud, true, data);
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x000B0F18 File Offset: 0x000AF318
		public static void writeData(string path, bool useCloud, bool usePath, Data data)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(data.getFile());
			ReadWrite.writeBytes(path, useCloud, usePath, bytes);
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x000B0F3F File Offset: 0x000AF33F
		public static void writeBlock(string path, bool useCloud, Block block)
		{
			ReadWrite.writeBlock(path, useCloud, true, block);
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x000B0F4C File Offset: 0x000AF34C
		public static void writeBlock(string path, bool useCloud, bool usePath, Block block)
		{
			int size;
			byte[] bytes = block.getBytes(out size);
			ReadWrite.writeBytes(path, useCloud, usePath, bytes, size);
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x000B0F6C File Offset: 0x000AF36C
		public static void deleteFile(string path, bool useCloud)
		{
			ReadWrite.deleteFile(path, useCloud, true);
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x000B0F76 File Offset: 0x000AF376
		public static void deleteFile(string path, bool useCloud, bool usePath)
		{
			if (useCloud)
			{
				ReadWrite.cloudFileDelete(path);
			}
			else
			{
				if (usePath)
				{
					path = ReadWrite.PATH + path;
				}
				File.Delete(path);
			}
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x000B0FA2 File Offset: 0x000AF3A2
		public static void deleteFolder(string path)
		{
			ReadWrite.deleteFolder(path, true);
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x000B0FAB File Offset: 0x000AF3AB
		public static void deleteFolder(string path, bool usePath)
		{
			if (usePath)
			{
				path = ReadWrite.PATH + path;
			}
			Directory.Delete(path, true);
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x000B0FC7 File Offset: 0x000AF3C7
		public static void moveFolder(string origin, string target)
		{
			ReadWrite.moveFolder(origin, target, true);
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x000B0FD1 File Offset: 0x000AF3D1
		public static void moveFolder(string origin, string target, bool usePath)
		{
			if (usePath)
			{
				origin = ReadWrite.PATH + origin;
				target = ReadWrite.PATH + target;
			}
			Directory.Move(origin, target);
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x000B0FFA File Offset: 0x000AF3FA
		public static void createFolder(string path)
		{
			ReadWrite.createFolder(path, true);
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x000B1003 File Offset: 0x000AF403
		public static void createFolder(string path, bool usePath)
		{
			if (usePath)
			{
				path = ReadWrite.PATH + path;
			}
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x000B102A File Offset: 0x000AF42A
		public static void createHidden(string path)
		{
			ReadWrite.createHidden(path, true);
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x000B1034 File Offset: 0x000AF434
		public static void createHidden(string path, bool usePath)
		{
			if (usePath)
			{
				path = ReadWrite.PATH + path;
			}
			if (!Directory.Exists(path))
			{
				DirectoryInfo directoryInfo = Directory.CreateDirectory(path);
				directoryInfo.Attributes = (FileAttributes.Directory | FileAttributes.Hidden);
			}
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x000B106E File Offset: 0x000AF46E
		public static string folderName(string path)
		{
			return new DirectoryInfo(path).Name;
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x000B107B File Offset: 0x000AF47B
		public static string folderPath(string path)
		{
			return Path.GetDirectoryName(path);
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x000B1083 File Offset: 0x000AF483
		public static void renameFile(string path_0, string path_1)
		{
			path_0 = ReadWrite.PATH + path_0;
			path_1 = ReadWrite.PATH + path_1;
			File.Move(path_0, path_1);
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x000B10A6 File Offset: 0x000AF4A6
		public static string fileName(string path)
		{
			return Path.GetFileNameWithoutExtension(path);
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x000B10AE File Offset: 0x000AF4AE
		public static bool fileExists(string path, bool useCloud)
		{
			return ReadWrite.fileExists(path, useCloud, true);
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x000B10B8 File Offset: 0x000AF4B8
		public static bool fileExists(string path, bool useCloud, bool usePath)
		{
			if (useCloud)
			{
				return ReadWrite.cloudFileExists(path);
			}
			if (usePath)
			{
				path = ReadWrite.PATH + path;
			}
			return File.Exists(path);
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x000B10E0 File Offset: 0x000AF4E0
		public static string folderFound(string path)
		{
			return ReadWrite.folderFound(path, true);
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x000B10EC File Offset: 0x000AF4EC
		public static string folderFound(string path, bool usePath)
		{
			if (usePath)
			{
				path = ReadWrite.PATH + path;
			}
			string[] directories = Directory.GetDirectories(path);
			if (directories.Length > 0)
			{
				return directories[0];
			}
			return null;
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x000B1121 File Offset: 0x000AF521
		public static bool folderExists(string path)
		{
			return ReadWrite.folderExists(path, true);
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x000B112A File Offset: 0x000AF52A
		public static bool folderExists(string path, bool usePath)
		{
			if (usePath)
			{
				path = ReadWrite.PATH + path;
			}
			return Directory.Exists(path);
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x000B1145 File Offset: 0x000AF545
		public static string[] getFolders(string path)
		{
			return ReadWrite.getFolders(path, true);
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x000B114E File Offset: 0x000AF54E
		public static string[] getFolders(string path, bool usePath)
		{
			if (usePath)
			{
				path = ReadWrite.PATH + path;
			}
			return Directory.GetDirectories(path);
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x000B1169 File Offset: 0x000AF569
		public static string[] getFiles(string path)
		{
			return ReadWrite.getFiles(path, true);
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x000B1172 File Offset: 0x000AF572
		public static string[] getFiles(string path, bool usePath)
		{
			if (usePath)
			{
				path = ReadWrite.PATH + path;
			}
			return Directory.GetFiles(path);
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x000B118D File Offset: 0x000AF58D
		public static void copyFile(string source, string destination)
		{
			source = ReadWrite.PATH + source;
			destination = ReadWrite.PATH + destination;
			if (!Directory.Exists(Path.GetDirectoryName(destination)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(destination));
			}
			File.Copy(source, destination);
		}

		// Token: 0x04001319 RID: 4889
		public static readonly string PATH = new DirectoryInfo(Application.dataPath).Parent.ToString();

		// Token: 0x0400131A RID: 4890
		private static readonly XmlSerializerNamespaces XML_SERIALIZER_NAMESPACES = new XmlSerializerNamespaces(new XmlQualifiedName[]
		{
			XmlQualifiedName.Empty
		});

		// Token: 0x0400131B RID: 4891
		private static readonly XmlWriterSettings XML_WRITER_SETTINGS = new XmlWriterSettings
		{
			Indent = true,
			OmitXmlDeclaration = true,
			Encoding = new UTF8Encoding()
		};
	}
}
