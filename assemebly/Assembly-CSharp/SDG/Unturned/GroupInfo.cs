using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x020005F0 RID: 1520
	public class GroupInfo
	{
		// Token: 0x06002A59 RID: 10841 RVA: 0x00107F02 File Offset: 0x00106302
		public GroupInfo(CSteamID newGroupID, string newName, uint newMembers)
		{
			this.groupID = newGroupID;
			this.name = newName;
			this.members = newMembers;
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002A5A RID: 10842 RVA: 0x00107F1F File Offset: 0x0010631F
		// (set) Token: 0x06002A5B RID: 10843 RVA: 0x00107F27 File Offset: 0x00106327
		public CSteamID groupID { get; private set; }

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002A5C RID: 10844 RVA: 0x00107F30 File Offset: 0x00106330
		public bool useMaxGroupMembersLimit
		{
			get
			{
				return Provider.modeConfigData.Gameplay.Max_Group_Members > 0u;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002A5D RID: 10845 RVA: 0x00107F44 File Offset: 0x00106344
		public bool hasSpaceForMoreMembersInGroup
		{
			get
			{
				return !this.useMaxGroupMembersLimit || this.members < Provider.modeConfigData.Gameplay.Max_Group_Members;
			}
		}

		// Token: 0x04001B59 RID: 7001
		public string name;

		// Token: 0x04001B5A RID: 7002
		public uint members;
	}
}
