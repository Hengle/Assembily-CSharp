using System;
using System.Collections.Generic;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200059C RID: 1436
	public class GroupManager : SteamCaller
	{
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06002817 RID: 10263 RVA: 0x000F309A File Offset: 0x000F149A
		public static GroupManager instance
		{
			get
			{
				return GroupManager.manager;
			}
		}

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x06002818 RID: 10264 RVA: 0x000F30A4 File Offset: 0x000F14A4
		// (remove) Token: 0x06002819 RID: 10265 RVA: 0x000F30D8 File Offset: 0x000F14D8
		public static event GroupInfoReadyHandler groupInfoReady;

		// Token: 0x0600281A RID: 10266 RVA: 0x000F310C File Offset: 0x000F150C
		public static CSteamID generateUniqueGroupID()
		{
			CSteamID result = GroupManager.availableGroupID;
			GroupManager.availableGroupID = new CSteamID(result.m_SteamID + 1UL);
			return result;
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x000F3134 File Offset: 0x000F1534
		public static GroupInfo addGroup(CSteamID groupID, string name)
		{
			GroupInfo groupInfo = new GroupInfo(groupID, name, 0u);
			GroupManager.knownGroups.Add(groupID, groupInfo);
			return groupInfo;
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x000F3158 File Offset: 0x000F1558
		public static GroupInfo getGroupInfo(CSteamID groupID)
		{
			GroupInfo result = null;
			GroupManager.knownGroups.TryGetValue(groupID, out result);
			return result;
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000F3178 File Offset: 0x000F1578
		public static GroupInfo getOrAddGroup(CSteamID groupID, string name, out bool wasCreated)
		{
			wasCreated = false;
			GroupInfo groupInfo = GroupManager.getGroupInfo(groupID);
			if (groupInfo == null)
			{
				groupInfo = GroupManager.addGroup(groupID, name);
				wasCreated = true;
			}
			return groupInfo;
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000F31A4 File Offset: 0x000F15A4
		private static void triggerGroupInfoReady(GroupInfo group)
		{
			GroupInfoReadyHandler groupInfoReadyHandler = GroupManager.groupInfoReady;
			if (groupInfoReadyHandler != null)
			{
				groupInfoReadyHandler(group);
			}
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x000F31C4 File Offset: 0x000F15C4
		public static void sendGroupInfo(CSteamID steamID, GroupInfo group)
		{
			GroupManager.manager.channel.send("tellGroupInfo", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				group.groupID,
				group.name,
				group.members
			});
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000F3214 File Offset: 0x000F1614
		public static void sendGroupInfo(GroupInfo group)
		{
			foreach (SteamPlayer steamPlayer in Provider.clients)
			{
				if (steamPlayer.player.quests.isMemberOfGroup(group.groupID))
				{
					GroupManager.sendGroupInfo(steamPlayer.playerID.steamID, group);
				}
			}
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000F3294 File Offset: 0x000F1694
		[SteamCall]
		public void tellGroupInfo(CSteamID steamID, CSteamID groupID, string name, uint members)
		{
			if (!base.channel.checkServer(steamID))
			{
				return;
			}
			GroupInfo groupInfo = GroupManager.getGroupInfo(groupID);
			if (groupInfo == null)
			{
				groupInfo = new GroupInfo(groupID, name, members);
				GroupManager.knownGroups.Add(groupInfo.groupID, groupInfo);
			}
			else
			{
				groupInfo.name = name;
				groupInfo.members = members;
			}
			GroupManager.triggerGroupInfoReady(groupInfo);
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000F32F5 File Offset: 0x000F16F5
		private void onLevelLoaded(int level)
		{
			if (level > Level.SETUP)
			{
				GroupManager.availableGroupID = new CSteamID(1UL);
				GroupManager.knownGroups = new Dictionary<CSteamID, GroupInfo>();
				if (Provider.isServer)
				{
					GroupManager.load();
				}
			}
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000F3327 File Offset: 0x000F1727
		private void Start()
		{
			GroupManager.manager = this;
			Level.onLevelLoaded = (LevelLoaded)Delegate.Combine(Level.onLevelLoaded, new LevelLoaded(this.onLevelLoaded));
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000F3350 File Offset: 0x000F1750
		public static void load()
		{
			if (LevelSavedata.fileExists("/Groups.dat"))
			{
				River river = LevelSavedata.openRiver("/Groups.dat", true);
				byte b = river.readByte();
				if (b > 0)
				{
					GroupManager.availableGroupID = river.readSteamID();
					if (b > 1)
					{
						int num = river.readInt32();
						for (int i = 0; i < num; i++)
						{
							CSteamID csteamID = river.readSteamID();
							string text = river.readString();
							uint num2 = river.readUInt32();
							if (num2 >= 1u && !string.IsNullOrEmpty(text))
							{
								if (!GroupManager.knownGroups.ContainsKey(csteamID))
								{
									GroupManager.knownGroups.Add(csteamID, new GroupInfo(csteamID, text, num2));
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x000F3410 File Offset: 0x000F1810
		public static void save()
		{
			River river = LevelSavedata.openRiver("/Groups.dat", false);
			river.writeByte(GroupManager.SAVEDATA_VERSION);
			river.writeSteamID(GroupManager.availableGroupID);
			Dictionary<CSteamID, GroupInfo>.ValueCollection values = GroupManager.knownGroups.Values;
			List<GroupInfo> list = new List<GroupInfo>();
			foreach (GroupInfo groupInfo in values)
			{
				if (groupInfo.members >= 1u && !string.IsNullOrEmpty(groupInfo.name))
				{
					list.Add(groupInfo);
				}
			}
			river.writeInt32(list.Count);
			foreach (GroupInfo groupInfo2 in list)
			{
				river.writeSteamID(groupInfo2.groupID);
				river.writeString(groupInfo2.name);
				river.writeUInt32(groupInfo2.members);
			}
		}

		// Token: 0x0400190A RID: 6410
		public static readonly byte SAVEDATA_VERSION = 2;

		// Token: 0x0400190B RID: 6411
		private static GroupManager manager;

		// Token: 0x0400190D RID: 6413
		private static CSteamID availableGroupID;

		// Token: 0x0400190E RID: 6414
		private static Dictionary<CSteamID, GroupInfo> knownGroups;
	}
}
