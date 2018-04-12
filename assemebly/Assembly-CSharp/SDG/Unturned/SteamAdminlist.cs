using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200066A RID: 1642
	public class SteamAdminlist
	{
		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x0013B415 File Offset: 0x00139815
		public static List<SteamAdminID> list
		{
			get
			{
				return SteamAdminlist._list;
			}
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x0013B41C File Offset: 0x0013981C
		public static void admin(CSteamID playerID, CSteamID judgeID)
		{
			for (int i = 0; i < SteamAdminlist.list.Count; i++)
			{
				if (SteamAdminlist.list[i].playerID == playerID)
				{
					SteamAdminlist.list[i].judgeID = judgeID;
					return;
				}
			}
			SteamAdminlist.list.Add(new SteamAdminID(playerID, judgeID));
			for (int j = 0; j < Provider.clients.Count; j++)
			{
				if (Provider.clients[j].playerID.steamID == playerID)
				{
					Provider.clients[j].isAdmin = true;
					for (int k = 0; k < Provider.clients.Count; k++)
					{
						if (k == j || !Provider.hideAdmins)
						{
							Provider.send(Provider.clients[k].playerID.steamID, ESteamPacket.ADMINED, new byte[]
							{
								7,
								(byte)j
							}, 2, 0);
						}
					}
					break;
				}
			}
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x0013B52C File Offset: 0x0013992C
		public static void unadmin(CSteamID playerID)
		{
			for (int i = 0; i < SteamAdminlist.list.Count; i++)
			{
				if (SteamAdminlist.list[i].playerID == playerID)
				{
					for (int j = 0; j < Provider.clients.Count; j++)
					{
						if (Provider.clients[j].playerID.steamID == playerID)
						{
							Provider.clients[j].isAdmin = false;
							for (int k = 0; k < Provider.clients.Count; k++)
							{
								if (k == i || !Provider.hideAdmins)
								{
									Provider.send(Provider.clients[k].playerID.steamID, ESteamPacket.UNADMINED, new byte[]
									{
										8,
										(byte)j
									}, 2, 0);
								}
							}
							break;
						}
					}
					SteamAdminlist.list.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x0013B624 File Offset: 0x00139A24
		public static bool checkAC(CSteamID playerID)
		{
			Debug.Log(playerID);
			byte[] array = Hash.SHA1(playerID);
			string text = string.Empty;
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0)
				{
					text += ", ";
				}
				text += array[i];
			}
			Debug.Log(text);
			return false;
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x0013B688 File Offset: 0x00139A88
		public static bool checkAdmin(CSteamID playerID)
		{
			if (playerID == SteamAdminlist.ownerID)
			{
				return true;
			}
			for (int i = 0; i < SteamAdminlist.list.Count; i++)
			{
				if (SteamAdminlist.list[i].playerID == playerID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x0013B6E0 File Offset: 0x00139AE0
		public static void load()
		{
			SteamAdminlist._list = new List<SteamAdminID>();
			SteamAdminlist.ownerID = CSteamID.Nil;
			if (ServerSavedata.fileExists("/Server/Adminlist.dat"))
			{
				River river = ServerSavedata.openRiver("/Server/Adminlist.dat", true);
				byte b = river.readByte();
				if (b > 1)
				{
					ushort num = river.readUInt16();
					for (ushort num2 = 0; num2 < num; num2 += 1)
					{
						CSteamID newPlayerID = river.readSteamID();
						CSteamID newJudgeID = river.readSteamID();
						SteamAdminID item = new SteamAdminID(newPlayerID, newJudgeID);
						SteamAdminlist.list.Add(item);
					}
				}
			}
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x0013B76C File Offset: 0x00139B6C
		public static void save()
		{
			River river = ServerSavedata.openRiver("/Server/Adminlist.dat", false);
			river.writeByte(SteamAdminlist.SAVEDATA_VERSION);
			river.writeUInt16((ushort)SteamAdminlist.list.Count);
			ushort num = 0;
			while ((int)num < SteamAdminlist.list.Count)
			{
				SteamAdminID steamAdminID = SteamAdminlist.list[(int)num];
				river.writeSteamID(steamAdminID.playerID);
				river.writeSteamID(steamAdminID.judgeID);
				num += 1;
			}
			river.closeRiver();
		}

		// Token: 0x04001F89 RID: 8073
		public static readonly byte SAVEDATA_VERSION = 2;

		// Token: 0x04001F8A RID: 8074
		private static List<SteamAdminID> _list;

		// Token: 0x04001F8B RID: 8075
		public static CSteamID ownerID;
	}
}
