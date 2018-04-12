using System;

namespace SDG.Unturned
{
	// Token: 0x020006AE RID: 1710
	public class PlaySettings
	{
		// Token: 0x060031CE RID: 12750 RVA: 0x00143E7C File Offset: 0x0014227C
		public static void load()
		{
			if (ReadWrite.fileExists("/Play.dat", true))
			{
				Block block = ReadWrite.readBlock("/Play.dat", true, 0);
				if (block != null)
				{
					byte b = block.readByte();
					if (b > 1)
					{
						PlaySettings.connectIP = block.readString();
						PlaySettings.connectPort = block.readUInt16();
						PlaySettings.connectPassword = block.readString();
						if (b > 3)
						{
							PlaySettings.serversName = block.readString();
						}
						else
						{
							PlaySettings.serversName = string.Empty;
						}
						PlaySettings.serversPassword = block.readString();
						PlaySettings.singleplayerMode = (EGameMode)block.readByte();
						if (b < 8)
						{
							PlaySettings.singleplayerMode = EGameMode.NORMAL;
						}
						if (b > 10)
						{
							PlaySettings.matchmakingMode = (EGameMode)block.readByte();
						}
						else
						{
							PlaySettings.matchmakingMode = EGameMode.NORMAL;
						}
						if (b < 7)
						{
							PlaySettings.singleplayerCheats = false;
						}
						else
						{
							PlaySettings.singleplayerCheats = block.readBoolean();
						}
						if (b > 4)
						{
							PlaySettings.singleplayerMap = block.readString();
							PlaySettings.editorMap = block.readString();
						}
						else
						{
							PlaySettings.singleplayerMap = string.Empty;
							PlaySettings.editorMap = string.Empty;
						}
						if (b > 10)
						{
							PlaySettings.matchmakingMap = block.readString();
						}
						else
						{
							PlaySettings.matchmakingMap = string.Empty;
						}
						if (b > 5)
						{
							PlaySettings.isVR = block.readBoolean();
							if (b < 9)
							{
								PlaySettings.isVR = false;
							}
						}
						else
						{
							PlaySettings.isVR = false;
						}
						if (b < 10)
						{
							PlaySettings.singleplayerCategory = ESingleplayerMapCategory.OFFICIAL;
						}
						else
						{
							PlaySettings.singleplayerCategory = (ESingleplayerMapCategory)block.readByte();
						}
						return;
					}
				}
			}
			PlaySettings.connectIP = "127.0.0.1";
			PlaySettings.connectPort = 27015;
			PlaySettings.connectPassword = string.Empty;
			PlaySettings.serversName = string.Empty;
			PlaySettings.serversPassword = string.Empty;
			PlaySettings.singleplayerMode = EGameMode.NORMAL;
			PlaySettings.matchmakingMode = EGameMode.NORMAL;
			PlaySettings.singleplayerCheats = false;
			PlaySettings.singleplayerMap = string.Empty;
			PlaySettings.matchmakingMap = string.Empty;
			PlaySettings.editorMap = string.Empty;
			PlaySettings.singleplayerCategory = ESingleplayerMapCategory.OFFICIAL;
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x00144064 File Offset: 0x00142464
		public static void save()
		{
			Block block = new Block();
			block.writeByte(PlaySettings.SAVEDATA_VERSION);
			block.writeString(PlaySettings.connectIP);
			block.writeUInt16(PlaySettings.connectPort);
			block.writeString(PlaySettings.connectPassword);
			block.writeString(PlaySettings.serversName);
			block.writeString(PlaySettings.serversPassword);
			block.writeByte((byte)PlaySettings.singleplayerMode);
			block.writeByte((byte)PlaySettings.matchmakingMode);
			block.writeBoolean(PlaySettings.singleplayerCheats);
			block.writeString(PlaySettings.singleplayerMap);
			block.writeString(PlaySettings.matchmakingMap);
			block.writeString(PlaySettings.editorMap);
			block.writeBoolean(PlaySettings.isVR);
			block.writeByte((byte)PlaySettings.singleplayerCategory);
			ReadWrite.writeBlock("/Play.dat", true, block);
		}

		// Token: 0x040021AF RID: 8623
		public static readonly byte SAVEDATA_VERSION = 11;

		// Token: 0x040021B0 RID: 8624
		public static string connectIP;

		// Token: 0x040021B1 RID: 8625
		public static ushort connectPort;

		// Token: 0x040021B2 RID: 8626
		public static string connectPassword;

		// Token: 0x040021B3 RID: 8627
		public static string serversName;

		// Token: 0x040021B4 RID: 8628
		public static string serversPassword;

		// Token: 0x040021B5 RID: 8629
		public static EGameMode singleplayerMode;

		// Token: 0x040021B6 RID: 8630
		public static EGameMode matchmakingMode;

		// Token: 0x040021B7 RID: 8631
		public static bool singleplayerCheats;

		// Token: 0x040021B8 RID: 8632
		public static string singleplayerMap;

		// Token: 0x040021B9 RID: 8633
		public static string matchmakingMap;

		// Token: 0x040021BA RID: 8634
		public static string editorMap;

		// Token: 0x040021BB RID: 8635
		public static bool isVR;

		// Token: 0x040021BC RID: 8636
		public static ESingleplayerMapCategory singleplayerCategory;
	}
}
