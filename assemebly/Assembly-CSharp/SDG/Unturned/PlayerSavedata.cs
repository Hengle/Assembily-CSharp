﻿using System;

namespace SDG.Unturned
{
	// Token: 0x020004AB RID: 1195
	public class PlayerSavedata
	{
		// Token: 0x06001FEC RID: 8172 RVA: 0x000B01E0 File Offset: 0x000AE5E0
		public static void writeData(SteamPlayerID playerID, string path, Data data)
		{
			if (PlayerSavedata.hasSync)
			{
				ReadWrite.writeData(string.Concat(new object[]
				{
					"/Sync/",
					playerID.steamID,
					"_",
					playerID.characterID,
					"/",
					Level.info.name,
					path
				}), false, data);
			}
			else
			{
				ServerSavedata.writeData(string.Concat(new object[]
				{
					"/Players/",
					playerID.steamID,
					"_",
					playerID.characterID,
					"/",
					Level.info.name,
					path
				}), data);
			}
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x000B02AC File Offset: 0x000AE6AC
		public static Data readData(SteamPlayerID playerID, string path)
		{
			if (PlayerSavedata.hasSync)
			{
				return ReadWrite.readData(string.Concat(new object[]
				{
					"/Sync/",
					playerID.steamID,
					"_",
					playerID.characterID,
					"/",
					Level.info.name,
					path
				}), false);
			}
			return ServerSavedata.readData(string.Concat(new object[]
			{
				"/Players/",
				playerID.steamID,
				"_",
				playerID.characterID,
				"/",
				Level.info.name,
				path
			}));
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x000B0370 File Offset: 0x000AE770
		public static void writeBlock(SteamPlayerID playerID, string path, Block block)
		{
			if (PlayerSavedata.hasSync)
			{
				ReadWrite.writeBlock(string.Concat(new object[]
				{
					"/Sync/",
					playerID.steamID,
					"_",
					playerID.characterID,
					"/",
					Level.info.name,
					path
				}), false, block);
			}
			else
			{
				ServerSavedata.writeBlock(string.Concat(new object[]
				{
					"/Players/",
					playerID.steamID,
					"_",
					playerID.characterID,
					"/",
					Level.info.name,
					path
				}), block);
			}
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x000B043C File Offset: 0x000AE83C
		public static Block readBlock(SteamPlayerID playerID, string path, byte prefix)
		{
			if (PlayerSavedata.hasSync)
			{
				return ReadWrite.readBlock(string.Concat(new object[]
				{
					"/Sync/",
					playerID.steamID,
					"_",
					playerID.characterID,
					"/",
					Level.info.name,
					path
				}), false, prefix);
			}
			return ServerSavedata.readBlock(string.Concat(new object[]
			{
				"/Players/",
				playerID.steamID,
				"_",
				playerID.characterID,
				"/",
				Level.info.name,
				path
			}), prefix);
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x000B0504 File Offset: 0x000AE904
		public static River openRiver(SteamPlayerID playerID, string path, bool isReading)
		{
			if (PlayerSavedata.hasSync)
			{
				return new River(string.Concat(new object[]
				{
					"/Sync/",
					playerID.steamID,
					"_",
					playerID.characterID,
					"/",
					Level.info.name,
					path
				}), true, false, isReading);
			}
			return ServerSavedata.openRiver(string.Concat(new object[]
			{
				"/Players/",
				playerID.steamID,
				"_",
				playerID.characterID,
				"/",
				Level.info.name,
				path
			}), isReading);
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x000B05CC File Offset: 0x000AE9CC
		public static void deleteFile(SteamPlayerID playerID, string path)
		{
			if (PlayerSavedata.hasSync)
			{
				ReadWrite.deleteFile(string.Concat(new object[]
				{
					"/Sync/",
					playerID.steamID,
					"_",
					playerID.characterID,
					"/",
					Level.info.name,
					path
				}), false);
			}
			else
			{
				ServerSavedata.deleteFile(string.Concat(new object[]
				{
					"/Players/",
					playerID.steamID,
					"_",
					playerID.characterID,
					"/",
					Level.info.name,
					path
				}));
			}
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x000B0694 File Offset: 0x000AEA94
		public static bool fileExists(SteamPlayerID playerID, string path)
		{
			if (PlayerSavedata.hasSync)
			{
				return ReadWrite.fileExists(string.Concat(new object[]
				{
					"/Sync/",
					playerID.steamID,
					"_",
					playerID.characterID,
					"/",
					Level.info.name,
					path
				}), false);
			}
			return ServerSavedata.fileExists(string.Concat(new object[]
			{
				"/Players/",
				playerID.steamID,
				"_",
				playerID.characterID,
				"/",
				Level.info.name,
				path
			}));
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x000B0758 File Offset: 0x000AEB58
		public static void deleteFolder(SteamPlayerID playerID)
		{
			if (PlayerSavedata.hasSync)
			{
				for (byte b = 0; b < Customization.FREE_CHARACTERS + Customization.PRO_CHARACTERS; b += 1)
				{
					if (ReadWrite.folderExists(string.Concat(new object[]
					{
						"/Sync/",
						playerID.steamID,
						"_",
						playerID.characterID
					}), false))
					{
						ReadWrite.deleteFolder(string.Concat(new object[]
						{
							"/Sync/",
							playerID.steamID,
							"_",
							playerID.characterID
						}), false);
					}
				}
			}
			else
			{
				for (byte b2 = 0; b2 < Customization.FREE_CHARACTERS + Customization.PRO_CHARACTERS; b2 += 1)
				{
					if (ServerSavedata.folderExists(string.Concat(new object[]
					{
						"/Players/",
						playerID.steamID,
						"_",
						playerID.characterID
					})))
					{
						ServerSavedata.deleteFolder(string.Concat(new object[]
						{
							"/Players/",
							playerID.steamID,
							"_",
							playerID.characterID
						}));
					}
				}
			}
		}

		// Token: 0x04001318 RID: 4888
		public static bool hasSync;
	}
}
