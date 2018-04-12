using System;
using SDG.Framework.IO.Deserialization;
using SDG.Framework.IO.Serialization;
using UnityEngine;

namespace SDG.Framework.IO
{
	// Token: 0x020001C3 RID: 451
	public class IOUtility
	{
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x000606EC File Offset: 0x0005EAEC
		public static string rootPath
		{
			get
			{
				if (string.IsNullOrEmpty(IOUtility._rootPath))
				{
					RuntimePlatform platform = Application.platform;
					switch (platform)
					{
					case RuntimePlatform.OSXEditor:
					case RuntimePlatform.WindowsEditor:
						IOUtility._rootPath = Environment.CurrentDirectory + "/Builds/Shared";
						goto IL_A7;
					case RuntimePlatform.OSXPlayer:
						IOUtility._rootPath = Environment.CurrentDirectory;
						goto IL_A7;
					case RuntimePlatform.WindowsPlayer:
						break;
					default:
						if (platform != RuntimePlatform.LinuxPlayer)
						{
							IOUtility._rootPath = Environment.CurrentDirectory;
							Debug.LogError("Unable to find root path on unsupported platform: " + Application.platform);
							goto IL_A7;
						}
						break;
					}
					IOUtility._rootPath = Environment.CurrentDirectory;
				}
				IL_A7:
				return IOUtility._rootPath;
			}
		}

		// Token: 0x040008EC RID: 2284
		public static IDeserializer jsonDeserializer = new JSONDeserializer();

		// Token: 0x040008ED RID: 2285
		public static ISerializer jsonSerializer = new JSONSerializer();

		// Token: 0x040008EE RID: 2286
		public static IDeserializer xmlDeserializer = new XMLDeserializer();

		// Token: 0x040008EF RID: 2287
		public static ISerializer xmlSerializer = new XMLSerializer();

		// Token: 0x040008F0 RID: 2288
		private static string _rootPath;
	}
}
