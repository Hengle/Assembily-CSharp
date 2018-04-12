using System;

namespace SDG.Unturned
{
	// Token: 0x020004AE RID: 1198
	public class ServerSavedata
	{
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x000B1BBB File Offset: 0x000AFFBB
		public static string directory
		{
			get
			{
				if (Dedicator.isDedicated)
				{
					return "/Servers";
				}
				return "/Worlds";
			}
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x000B1BD2 File Offset: 0x000AFFD2
		public static string transformPath(string path)
		{
			return ServerSavedata.directory + "/" + Provider.serverID + path;
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x000B1BE9 File Offset: 0x000AFFE9
		public static void serializeJSON<T>(string path, T instance)
		{
			ReadWrite.serializeJSON<T>(ServerSavedata.directory + "/" + Provider.serverID + path, false, instance);
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x000B1C07 File Offset: 0x000B0007
		public static T deserializeJSON<T>(string path)
		{
			return ReadWrite.deserializeJSON<T>(ServerSavedata.directory + "/" + Provider.serverID + path, false);
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000B1C24 File Offset: 0x000B0024
		public static void writeData(string path, Data data)
		{
			ReadWrite.writeData(ServerSavedata.directory + "/" + Provider.serverID + path, false, data);
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000B1C42 File Offset: 0x000B0042
		public static Data readData(string path)
		{
			return ReadWrite.readData(ServerSavedata.directory + "/" + Provider.serverID + path, false);
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x000B1C5F File Offset: 0x000B005F
		public static void writeBlock(string path, Block block)
		{
			ReadWrite.writeBlock(ServerSavedata.directory + "/" + Provider.serverID + path, false, block);
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x000B1C7D File Offset: 0x000B007D
		public static Block readBlock(string path, byte prefix)
		{
			return ReadWrite.readBlock(ServerSavedata.directory + "/" + Provider.serverID + path, false, prefix);
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x000B1C9B File Offset: 0x000B009B
		public static River openRiver(string path, bool isReading)
		{
			return new River(ServerSavedata.directory + "/" + Provider.serverID + path, true, false, isReading);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x000B1CBA File Offset: 0x000B00BA
		public static void deleteFile(string path)
		{
			ReadWrite.deleteFile(ServerSavedata.directory + "/" + Provider.serverID + path, false);
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000B1CD7 File Offset: 0x000B00D7
		public static bool fileExists(string path)
		{
			return ReadWrite.fileExists(ServerSavedata.directory + "/" + Provider.serverID + path, false);
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x000B1CF4 File Offset: 0x000B00F4
		public static void createFolder(string path)
		{
			ReadWrite.createFolder(ServerSavedata.directory + "/" + Provider.serverID + path);
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x000B1D10 File Offset: 0x000B0110
		public static void deleteFolder(string path)
		{
			ReadWrite.deleteFolder(ServerSavedata.directory + "/" + Provider.serverID + path);
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x000B1D2C File Offset: 0x000B012C
		public static bool folderExists(string path)
		{
			return ReadWrite.folderExists(ServerSavedata.directory + "/" + Provider.serverID + path);
		}
	}
}
