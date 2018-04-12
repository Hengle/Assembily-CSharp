﻿using System;
using System.Collections.Generic;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200066B RID: 1643
	public class SteamBlacklist
	{
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002FA9 RID: 12201 RVA: 0x0013B7F7 File Offset: 0x00139BF7
		public static List<SteamBlacklistID> list
		{
			get
			{
				return SteamBlacklist._list;
			}
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x0013B7FE File Offset: 0x00139BFE
		[Obsolete]
		public static void ban(CSteamID playerID, CSteamID judgeID, string reason, uint duration)
		{
			SteamBlacklist.ban(playerID, 0u, judgeID, reason, duration);
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x0013B80C File Offset: 0x00139C0C
		public static void ban(CSteamID playerID, uint ip, CSteamID judgeID, string reason, uint duration)
		{
			Provider.ban(playerID, reason, duration);
			for (int i = 0; i < SteamBlacklist.list.Count; i++)
			{
				if (SteamBlacklist.list[i].playerID == playerID)
				{
					SteamBlacklist.list[i].judgeID = judgeID;
					SteamBlacklist.list[i].reason = reason;
					SteamBlacklist.list[i].duration = duration;
					SteamBlacklist.list[i].banned = Provider.time;
					return;
				}
			}
			SteamBlacklist.list.Add(new SteamBlacklistID(playerID, ip, judgeID, reason, duration, Provider.time));
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x0013B8BC File Offset: 0x00139CBC
		public static bool unban(CSteamID playerID)
		{
			for (int i = 0; i < SteamBlacklist.list.Count; i++)
			{
				if (SteamBlacklist.list[i].playerID == playerID)
				{
					SteamBlacklist.list.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x0013B90D File Offset: 0x00139D0D
		[Obsolete]
		public static bool checkBanned(CSteamID playerID, out SteamBlacklistID blacklistID)
		{
			return SteamBlacklist.checkBanned(playerID, 0u, out blacklistID);
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x0013B918 File Offset: 0x00139D18
		public static bool checkBanned(CSteamID playerID, uint ip, out SteamBlacklistID blacklistID)
		{
			blacklistID = null;
			int i = 0;
			while (i < SteamBlacklist.list.Count)
			{
				if (SteamBlacklist.list[i].playerID == playerID || (SteamBlacklist.list[i].ip == ip && ip != 0u))
				{
					if (SteamBlacklist.list[i].isExpired)
					{
						SteamBlacklist.list.RemoveAt(i);
						return false;
					}
					blacklistID = SteamBlacklist.list[i];
					return true;
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x0013B9AC File Offset: 0x00139DAC
		public static void load()
		{
			SteamBlacklist._list = new List<SteamBlacklistID>();
			if (ServerSavedata.fileExists("/Server/Blacklist.dat"))
			{
				River river = ServerSavedata.openRiver("/Server/Blacklist.dat", true);
				byte b = river.readByte();
				if (b > 1)
				{
					ushort num = river.readUInt16();
					for (ushort num2 = 0; num2 < num; num2 += 1)
					{
						CSteamID newPlayerID = river.readSteamID();
						uint newIP;
						if (b > 2)
						{
							newIP = river.readUInt32();
						}
						else
						{
							newIP = 0u;
						}
						CSteamID newJudgeID = river.readSteamID();
						string newReason = river.readString();
						uint newDuration = river.readUInt32();
						uint newBanned = river.readUInt32();
						SteamBlacklistID steamBlacklistID = new SteamBlacklistID(newPlayerID, newIP, newJudgeID, newReason, newDuration, newBanned);
						if (!steamBlacklistID.isExpired)
						{
							SteamBlacklist.list.Add(steamBlacklistID);
						}
					}
				}
			}
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x0013BA70 File Offset: 0x00139E70
		public static void save()
		{
			River river = ServerSavedata.openRiver("/Server/Blacklist.dat", false);
			river.writeByte(SteamBlacklist.SAVEDATA_VERSION);
			river.writeUInt16((ushort)SteamBlacklist.list.Count);
			ushort num = 0;
			while ((int)num < SteamBlacklist.list.Count)
			{
				SteamBlacklistID steamBlacklistID = SteamBlacklist.list[(int)num];
				river.writeSteamID(steamBlacklistID.playerID);
				river.writeUInt32(steamBlacklistID.ip);
				river.writeSteamID(steamBlacklistID.judgeID);
				river.writeString(steamBlacklistID.reason);
				river.writeUInt32(steamBlacklistID.duration);
				river.writeUInt32(steamBlacklistID.banned);
				num += 1;
			}
			river.closeRiver();
		}

		// Token: 0x04001F8C RID: 8076
		public static readonly byte SAVEDATA_VERSION = 3;

		// Token: 0x04001F8D RID: 8077
		public static readonly uint PERMANENT = 31536000u;

		// Token: 0x04001F8E RID: 8078
		public static readonly uint TEMPORARY = 180u;

		// Token: 0x04001F8F RID: 8079
		private static List<SteamBlacklistID> _list;
	}
}
