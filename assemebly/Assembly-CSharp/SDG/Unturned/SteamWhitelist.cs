using System;
using System.Collections.Generic;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000686 RID: 1670
	public class SteamWhitelist
	{
		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x060030A2 RID: 12450 RVA: 0x0013F0A2 File Offset: 0x0013D4A2
		public static List<SteamWhitelistID> list
		{
			get
			{
				return SteamWhitelist._list;
			}
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x0013F0AC File Offset: 0x0013D4AC
		public static void whitelist(CSteamID steamID, string tag, CSteamID judgeID)
		{
			for (int i = 0; i < SteamWhitelist.list.Count; i++)
			{
				if (SteamWhitelist.list[i].steamID == steamID)
				{
					SteamWhitelist.list[i].tag = tag;
					SteamWhitelist.list[i].judgeID = judgeID;
					return;
				}
			}
			SteamWhitelist.list.Add(new SteamWhitelistID(steamID, tag, judgeID));
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x0013F124 File Offset: 0x0013D524
		public static bool unwhitelist(CSteamID steamID)
		{
			for (int i = 0; i < SteamWhitelist.list.Count; i++)
			{
				if (SteamWhitelist.list[i].steamID == steamID)
				{
					if (Provider.isWhitelisted)
					{
						Provider.kick(steamID, "Removed from whitelist.");
					}
					SteamWhitelist.list.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x0013F18C File Offset: 0x0013D58C
		public static bool checkWhitelisted(CSteamID steamID)
		{
			for (int i = 0; i < SteamWhitelist.list.Count; i++)
			{
				if (SteamWhitelist.list[i].steamID == steamID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x0013F1D4 File Offset: 0x0013D5D4
		public static void load()
		{
			SteamWhitelist._list = new List<SteamWhitelistID>();
			if (ServerSavedata.fileExists("/Server/Whitelist.dat"))
			{
				River river = ServerSavedata.openRiver("/Server/Whitelist.dat", true);
				byte b = river.readByte();
				if (b > 1)
				{
					ushort num = river.readUInt16();
					for (ushort num2 = 0; num2 < num; num2 += 1)
					{
						CSteamID newSteamID = river.readSteamID();
						string newTag = river.readString();
						CSteamID newJudgeID = river.readSteamID();
						SteamWhitelistID item = new SteamWhitelistID(newSteamID, newTag, newJudgeID);
						SteamWhitelist.list.Add(item);
					}
				}
			}
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x0013F260 File Offset: 0x0013D660
		public static void save()
		{
			River river = ServerSavedata.openRiver("/Server/Whitelist.dat", false);
			river.writeByte(SteamWhitelist.SAVEDATA_VERSION);
			river.writeUInt16((ushort)SteamWhitelist.list.Count);
			ushort num = 0;
			while ((int)num < SteamWhitelist.list.Count)
			{
				SteamWhitelistID steamWhitelistID = SteamWhitelist.list[(int)num];
				river.writeSteamID(steamWhitelistID.steamID);
				river.writeString(steamWhitelistID.tag);
				river.writeSteamID(steamWhitelistID.judgeID);
				num += 1;
			}
			river.closeRiver();
		}

		// Token: 0x04002015 RID: 8213
		public static readonly byte SAVEDATA_VERSION = 2;

		// Token: 0x04002016 RID: 8214
		private static List<SteamWhitelistID> _list;
	}
}
