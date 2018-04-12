using System;

namespace SDG.Unturned
{
	// Token: 0x020004A8 RID: 1192
	public class LevelSavedata
	{
		// Token: 0x06001FD4 RID: 8148 RVA: 0x000AFB4F File Offset: 0x000ADF4F
		public static string transformName(string name)
		{
			return ServerSavedata.transformPath("/Level/" + name);
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x000AFB61 File Offset: 0x000ADF61
		public static void writeData(string path, Data data)
		{
			ServerSavedata.writeData("/Level/" + Level.info.name + path, data);
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x000AFB7E File Offset: 0x000ADF7E
		public static Data readData(string path)
		{
			return ServerSavedata.readData("/Level/" + Level.info.name + path);
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x000AFB9A File Offset: 0x000ADF9A
		public static void writeBlock(string path, Block block)
		{
			ServerSavedata.writeBlock("/Level/" + Level.info.name + path, block);
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x000AFBB7 File Offset: 0x000ADFB7
		public static Block readBlock(string path, byte prefix)
		{
			return ServerSavedata.readBlock("/Level/" + Level.info.name + path, prefix);
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x000AFBD4 File Offset: 0x000ADFD4
		public static River openRiver(string path, bool isReading)
		{
			return ServerSavedata.openRiver("/Level/" + Level.info.name + path, isReading);
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x000AFBF1 File Offset: 0x000ADFF1
		public static void deleteFile(string path)
		{
			ServerSavedata.deleteFile("/Level/" + Level.info.name + path);
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x000AFC0D File Offset: 0x000AE00D
		public static bool fileExists(string path)
		{
			return ServerSavedata.fileExists("/Level/" + Level.info.name + path);
		}
	}
}
