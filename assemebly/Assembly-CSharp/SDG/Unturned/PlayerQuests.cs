using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000641 RID: 1601
	public class PlayerQuests : PlayerCaller
	{
		// Token: 0x06002D9D RID: 11677 RVA: 0x001259B8 File Offset: 0x00123DB8
		private static void triggerGroupUpdated(PlayerQuests sender)
		{
			GroupUpdatedHandler groupUpdatedHandler = PlayerQuests.groupUpdated;
			if (groupUpdatedHandler != null)
			{
				groupUpdatedHandler(sender);
			}
		}

		// Token: 0x14000083 RID: 131
		// (add) Token: 0x06002D9E RID: 11678 RVA: 0x001259D8 File Offset: 0x00123DD8
		// (remove) Token: 0x06002D9F RID: 11679 RVA: 0x00125A10 File Offset: 0x00123E10
		public event TrackedQuestUpdated TrackedQuestUpdated;

		// Token: 0x06002DA0 RID: 11680 RVA: 0x00125A46 File Offset: 0x00123E46
		private void TriggerTrackedQuestUpdated()
		{
			if (this.TrackedQuestUpdated == null)
			{
				return;
			}
			this.TrackedQuestUpdated(this);
		}

		// Token: 0x14000084 RID: 132
		// (add) Token: 0x06002DA1 RID: 11681 RVA: 0x00125A60 File Offset: 0x00123E60
		// (remove) Token: 0x06002DA2 RID: 11682 RVA: 0x00125A98 File Offset: 0x00123E98
		public event GroupIDChangedHandler groupIDChanged;

		// Token: 0x06002DA3 RID: 11683 RVA: 0x00125AD0 File Offset: 0x00123ED0
		private void triggerGroupIDChanged(CSteamID oldGroupID, CSteamID newGroupID)
		{
			GroupIDChangedHandler groupIDChangedHandler = this.groupIDChanged;
			if (groupIDChangedHandler != null)
			{
				groupIDChangedHandler(this, oldGroupID, newGroupID);
			}
		}

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x06002DA4 RID: 11684 RVA: 0x00125AF4 File Offset: 0x00123EF4
		// (remove) Token: 0x06002DA5 RID: 11685 RVA: 0x00125B2C File Offset: 0x00123F2C
		public event GroupRankChangedHandler groupRankChanged;

		// Token: 0x06002DA6 RID: 11686 RVA: 0x00125B64 File Offset: 0x00123F64
		private void triggerGroupRankChanged(EPlayerGroupRank oldGroupRank, EPlayerGroupRank newGroupRank)
		{
			GroupRankChangedHandler groupRankChangedHandler = this.groupRankChanged;
			if (groupRankChangedHandler != null)
			{
				groupRankChangedHandler(this, oldGroupRank, newGroupRank);
			}
		}

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x06002DA7 RID: 11687 RVA: 0x00125B88 File Offset: 0x00123F88
		// (remove) Token: 0x06002DA8 RID: 11688 RVA: 0x00125BC0 File Offset: 0x00123FC0
		public event GroupInvitesChangedHandler groupInvitesChanged;

		// Token: 0x06002DA9 RID: 11689 RVA: 0x00125BF8 File Offset: 0x00123FF8
		private void triggerGroupInvitesChanged()
		{
			GroupInvitesChangedHandler groupInvitesChangedHandler = this.groupInvitesChanged;
			if (groupInvitesChangedHandler != null)
			{
				groupInvitesChangedHandler(this);
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002DAA RID: 11690 RVA: 0x00125C19 File Offset: 0x00124019
		// (set) Token: 0x06002DAB RID: 11691 RVA: 0x00125C21 File Offset: 0x00124021
		public List<PlayerQuestFlag> flagsList { get; private set; }

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002DAC RID: 11692 RVA: 0x00125C2A File Offset: 0x0012402A
		// (set) Token: 0x06002DAD RID: 11693 RVA: 0x00125C32 File Offset: 0x00124032
		public ushort TrackedQuestID { get; private set; }

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x00125C3B File Offset: 0x0012403B
		// (set) Token: 0x06002DAF RID: 11695 RVA: 0x00125C43 File Offset: 0x00124043
		public List<PlayerQuest> questsList { get; private set; }

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x00125C4C File Offset: 0x0012404C
		// (set) Token: 0x06002DB1 RID: 11697 RVA: 0x00125C54 File Offset: 0x00124054
		public bool isMarkerPlaced { get; private set; }

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x00125C5D File Offset: 0x0012405D
		// (set) Token: 0x06002DB3 RID: 11699 RVA: 0x00125C65 File Offset: 0x00124065
		public Vector3 markerPosition { get; private set; }

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x00125C6E File Offset: 0x0012406E
		// (set) Token: 0x06002DB5 RID: 11701 RVA: 0x00125C76 File Offset: 0x00124076
		public uint radioFrequency { get; private set; }

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x00125C7F File Offset: 0x0012407F
		// (set) Token: 0x06002DB7 RID: 11703 RVA: 0x00125C87 File Offset: 0x00124087
		public CSteamID groupID { get; private set; }

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x00125C90 File Offset: 0x00124090
		// (set) Token: 0x06002DB9 RID: 11705 RVA: 0x00125C98 File Offset: 0x00124098
		public EPlayerGroupRank groupRank { get; private set; }

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002DBA RID: 11706 RVA: 0x00125CA1 File Offset: 0x001240A1
		// (set) Token: 0x06002DBB RID: 11707 RVA: 0x00125CA9 File Offset: 0x001240A9
		public HashSet<CSteamID> groupInvites { get; private set; }

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x00125CB2 File Offset: 0x001240B2
		public bool useMaxGroupMembersLimit
		{
			get
			{
				return Provider.modeConfigData.Gameplay.Max_Group_Members > 0u;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06002DBD RID: 11709 RVA: 0x00125CC8 File Offset: 0x001240C8
		public bool hasSpaceForMoreMembersInGroup
		{
			get
			{
				if (this.useMaxGroupMembersLimit)
				{
					GroupInfo groupInfo = GroupManager.getGroupInfo(this.groupID);
					return groupInfo != null && groupInfo.hasSpaceForMoreMembersInGroup;
				}
				return true;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x00125CFD File Offset: 0x001240FD
		public bool canChangeGroupMembership
		{
			get
			{
				return !LevelManager.isPlayerInArena(base.player);
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002DBF RID: 11711 RVA: 0x00125D0D File Offset: 0x0012410D
		public bool hasPermissionToChangeName
		{
			get
			{
				return this.groupRank == EPlayerGroupRank.OWNER;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x00125D18 File Offset: 0x00124118
		public bool hasPermissionToChangeRank
		{
			get
			{
				return this.groupRank == EPlayerGroupRank.OWNER;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x00125D23 File Offset: 0x00124123
		public bool hasPermissionToInviteMembers
		{
			get
			{
				return this.groupRank == EPlayerGroupRank.ADMIN || this.groupRank == EPlayerGroupRank.OWNER;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x00125D3D File Offset: 0x0012413D
		public bool hasPermissionToKickMembers
		{
			get
			{
				return this.groupRank == EPlayerGroupRank.ADMIN || this.groupRank == EPlayerGroupRank.OWNER;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x00125D57 File Offset: 0x00124157
		public bool hasPermissionToCreateGroup
		{
			get
			{
				return Provider.modeConfigData.Gameplay.Allow_Dynamic_Groups;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x00125D68 File Offset: 0x00124168
		public bool hasPermissionToLeaveGroup
		{
			get
			{
				if (!Provider.modeConfigData.Gameplay.Allow_Dynamic_Groups)
				{
					return false;
				}
				if (this.groupRank == EPlayerGroupRank.OWNER)
				{
					GroupInfo groupInfo = GroupManager.getGroupInfo(this.groupID);
					if (groupInfo != null && groupInfo.members > 1u)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002DC5 RID: 11717 RVA: 0x00125DB8 File Offset: 0x001241B8
		public bool hasPermissionToDeleteGroup
		{
			get
			{
				return Provider.modeConfigData.Gameplay.Allow_Dynamic_Groups && !this.inMainGroup && this.groupRank == EPlayerGroupRank.OWNER;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x00125DE7 File Offset: 0x001241E7
		public bool canBeKickedFromGroup
		{
			get
			{
				return this.groupRank != EPlayerGroupRank.OWNER;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002DC7 RID: 11719 RVA: 0x00125DF5 File Offset: 0x001241F5
		public bool isMemberOfAGroup
		{
			get
			{
				return this.groupID != CSteamID.Nil;
			}
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x00125E07 File Offset: 0x00124207
		public bool isMemberOfGroup(CSteamID groupID)
		{
			return this.isMemberOfAGroup && this.groupID == groupID;
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x00125E23 File Offset: 0x00124223
		public bool isMemberOfSameGroupAs(Player player)
		{
			return player.quests.isMemberOfGroup(this.groupID);
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x00125E36 File Offset: 0x00124236
		[SteamCall]
		public void tellSetMarker(CSteamID steamID, bool newIsMarkerPlaced, Vector3 newMarkerPosition)
		{
			if (base.channel.checkServer(steamID))
			{
				this.isMarkerPlaced = newIsMarkerPlaced;
				this.markerPosition = newMarkerPosition;
			}
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x00125E58 File Offset: 0x00124258
		[SteamCall]
		public void askSetMarker(CSteamID steamID, bool newIsMarkerPlaced, Vector3 newMarkerPosition)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (!base.player.tryToPerformRateLimitedAction())
				{
					return;
				}
				base.channel.send("tellSetMarker", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					newIsMarkerPlaced,
					newMarkerPosition
				});
			}
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x00125EBC File Offset: 0x001242BC
		public void sendSetMarker(bool newIsMarkerPlaced, Vector3 newMarkerPosition)
		{
			base.channel.send("askSetMarker", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				newIsMarkerPlaced,
				newMarkerPosition
			});
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x00125EE9 File Offset: 0x001242E9
		[SteamCall]
		public void tellSetRadioFrequency(CSteamID steamID, uint newRadioFrequency)
		{
			if (base.channel.checkServer(steamID))
			{
				this.radioFrequency = newRadioFrequency;
			}
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x00125F04 File Offset: 0x00124304
		[SteamCall]
		public void askSetRadioFrequency(CSteamID steamID, uint newRadioFrequency)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (!base.player.tryToPerformRateLimitedAction())
				{
					return;
				}
				base.channel.send("tellSetRadioFrequency", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					newRadioFrequency
				});
			}
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x00125F5F File Offset: 0x0012435F
		public void sendSetRadioFrequency(uint newRadioFrequency)
		{
			base.channel.send("askSetRadioFrequency", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				newRadioFrequency
			});
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x00125F84 File Offset: 0x00124384
		[SteamCall]
		public void tellSetGroup(CSteamID steamID, CSteamID newGroupID, byte newGroupRank)
		{
			if (base.channel.checkServer(steamID))
			{
				CSteamID groupID = this.groupID;
				this.groupID = newGroupID;
				EPlayerGroupRank groupRank = this.groupRank;
				this.groupRank = (EPlayerGroupRank)newGroupRank;
				if (groupID != newGroupID)
				{
					this.triggerGroupIDChanged(groupID, newGroupID);
				}
				if (groupRank != this.groupRank)
				{
					this.triggerGroupRankChanged(groupRank, this.groupRank);
				}
				PlayerQuests.triggerGroupUpdated(this);
			}
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x00125FF1 File Offset: 0x001243F1
		private bool removeGroupInvite(CSteamID newGroupID)
		{
			if (this.groupInvites.Remove(newGroupID))
			{
				this.triggerGroupInvitesChanged();
				return true;
			}
			return false;
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x0012600D File Offset: 0x0012440D
		public void changeRank(EPlayerGroupRank newRank)
		{
			base.channel.send("tellSetGroup", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				this.groupID,
				(byte)newRank
			});
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x00126040 File Offset: 0x00124440
		[SteamCall]
		public void askJoinGroupInvite(CSteamID steamID, CSteamID newGroupID)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (!this.canChangeGroupMembership)
				{
					return;
				}
				if (newGroupID == base.channel.owner.playerID.group)
				{
					if (!Provider.modeConfigData.Gameplay.Allow_Static_Groups)
					{
						return;
					}
					base.channel.send("tellSetGroup", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						newGroupID,
						0
					});
					this.inMainGroup = true;
				}
				else
				{
					if (!this.removeGroupInvite(newGroupID))
					{
						return;
					}
					GroupInfo groupInfo = GroupManager.getGroupInfo(newGroupID);
					if (groupInfo != null && groupInfo.hasSpaceForMoreMembersInGroup)
					{
						base.channel.send("tellSetGroup", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							newGroupID,
							0
						});
						this.inMainGroup = false;
						groupInfo.members += 1u;
						GroupManager.sendGroupInfo(groupInfo);
					}
				}
			}
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x0012614B File Offset: 0x0012454B
		public void sendJoinGroupInvite(CSteamID newGroupID)
		{
			base.channel.send("askJoinGroupInvite", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				newGroupID
			});
			this.removeGroupInvite(newGroupID);
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x00126177 File Offset: 0x00124577
		[SteamCall]
		public void askIgnoreGroupInvite(CSteamID steamID, CSteamID newGroupID)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				this.removeGroupInvite(newGroupID);
			}
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x0012619C File Offset: 0x0012459C
		public void sendIgnoreGroupInvite(CSteamID newGroupID)
		{
			base.channel.send("askIgnoreGroupInvite", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				newGroupID
			});
			this.removeGroupInvite(newGroupID);
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x001261C8 File Offset: 0x001245C8
		public void leaveGroup(bool force = false)
		{
			if (!force)
			{
				if (!this.canChangeGroupMembership)
				{
					return;
				}
				if (!this.hasPermissionToLeaveGroup)
				{
					return;
				}
			}
			GroupInfo groupInfo = GroupManager.getGroupInfo(this.groupID);
			if (groupInfo != null)
			{
				if (groupInfo.members > 0u)
				{
					groupInfo.members -= 1u;
				}
				GroupManager.sendGroupInfo(groupInfo);
			}
			base.channel.send("tellSetGroup", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				CSteamID.Nil,
				0
			});
			this.inMainGroup = false;
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x0012625B File Offset: 0x0012465B
		[SteamCall]
		public void askLeaveGroup(CSteamID steamID)
		{
			if (base.channel.checkOwner(steamID))
			{
				this.leaveGroup(false);
			}
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x00126275 File Offset: 0x00124675
		public void sendLeaveGroup()
		{
			base.channel.send("askLeaveGroup", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x00126290 File Offset: 0x00124690
		public void deleteGroup()
		{
			if (!this.canChangeGroupMembership)
			{
				return;
			}
			if (!this.hasPermissionToDeleteGroup)
			{
				return;
			}
			GroupInfo groupInfo = GroupManager.getGroupInfo(this.groupID);
			if (groupInfo != null)
			{
				groupInfo.members = 0u;
			}
			foreach (SteamPlayer steamPlayer in Provider.clients)
			{
				if (!(steamPlayer.player == null) && !(steamPlayer.player.quests == null))
				{
					if (steamPlayer.player.quests.isMemberOfSameGroupAs(base.player))
					{
						steamPlayer.player.quests.leaveGroup(true);
					}
				}
			}
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x00126374 File Offset: 0x00124774
		[SteamCall]
		public void askDeleteGroup(CSteamID steamID)
		{
			if (base.channel.checkOwner(steamID))
			{
				this.deleteGroup();
			}
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x0012638D File Offset: 0x0012478D
		public void sendDeleteGroup()
		{
			base.channel.send("askDeleteGroup", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x001263A8 File Offset: 0x001247A8
		[SteamCall]
		public void askCreateGroup(CSteamID steamID)
		{
			if (base.channel.checkOwner(steamID))
			{
				if (!this.canChangeGroupMembership)
				{
					return;
				}
				if (!this.hasPermissionToCreateGroup)
				{
					return;
				}
				CSteamID csteamID = GroupManager.generateUniqueGroupID();
				GroupInfo groupInfo = GroupManager.addGroup(csteamID, base.channel.owner.playerID.playerName + "'s Group");
				groupInfo.members += 1u;
				GroupManager.sendGroupInfo(steamID, groupInfo);
				base.channel.send("tellSetGroup", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					csteamID,
					2
				});
				this.inMainGroup = false;
			}
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x00126451 File Offset: 0x00124851
		public void sendCreateGroup()
		{
			base.channel.send("askCreateGroup", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x0012646C File Offset: 0x0012486C
		private void addGroupInvite(CSteamID newGroupID)
		{
			this.groupInvites.Add(newGroupID);
			this.triggerGroupInvitesChanged();
			PlayerQuests.triggerGroupUpdated(this);
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x00126487 File Offset: 0x00124887
		[SteamCall]
		public void tellAddGroupInvite(CSteamID steamID, CSteamID newGroupID)
		{
			if (base.channel.checkServer(steamID))
			{
				this.addGroupInvite(newGroupID);
			}
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x001264A4 File Offset: 0x001248A4
		public void sendAddGroupInvite(CSteamID newGroupID)
		{
			if (this.groupInvites.Contains(newGroupID))
			{
				return;
			}
			this.addGroupInvite(newGroupID);
			GroupInfo groupInfo = GroupManager.getGroupInfo(newGroupID);
			if (groupInfo != null)
			{
				GroupManager.sendGroupInfo(base.channel.owner.playerID.steamID, groupInfo);
			}
			if (!base.channel.isOwner)
			{
				base.channel.send("tellAddGroupInvite", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					newGroupID
				});
			}
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x00126524 File Offset: 0x00124924
		[SteamCall]
		public void askAddGroupInvite(CSteamID steamID, CSteamID targetID)
		{
			if (base.channel.checkOwner(steamID))
			{
				if (!this.isMemberOfAGroup)
				{
					return;
				}
				if (!this.hasPermissionToInviteMembers)
				{
					return;
				}
				if (!this.hasSpaceForMoreMembersInGroup)
				{
					return;
				}
				Player player = PlayerTool.getPlayer(targetID);
				if (player == null)
				{
					return;
				}
				if (player.quests.isMemberOfAGroup)
				{
					return;
				}
				player.quests.sendAddGroupInvite(this.groupID);
			}
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x0012659C File Offset: 0x0012499C
		public void sendAskAddGroupInvite(CSteamID targetID)
		{
			base.channel.send("askAddGroupInvite", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				targetID
			});
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x001265C0 File Offset: 0x001249C0
		[SteamCall]
		public void askPromote(CSteamID steamID, CSteamID targetID)
		{
			if (base.channel.checkOwner(steamID))
			{
				if (!this.isMemberOfAGroup)
				{
					return;
				}
				if (!this.hasPermissionToChangeRank)
				{
					return;
				}
				Player player = PlayerTool.getPlayer(targetID);
				if (player == null)
				{
					return;
				}
				if (!player.quests.isMemberOfSameGroupAs(base.player))
				{
					return;
				}
				if (player.quests.groupRank == EPlayerGroupRank.OWNER)
				{
					CommandWindow.LogWarning("Request to promote owner of group?");
					return;
				}
				player.quests.changeRank(player.quests.groupRank + 1);
				if (player.quests.groupRank == EPlayerGroupRank.OWNER)
				{
					this.changeRank(EPlayerGroupRank.ADMIN);
				}
			}
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x0012666D File Offset: 0x00124A6D
		public void sendPromote(CSteamID targetID)
		{
			base.channel.send("askPromote", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				targetID
			});
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x00126694 File Offset: 0x00124A94
		[SteamCall]
		public void askDemote(CSteamID steamID, CSteamID targetID)
		{
			if (base.channel.checkOwner(steamID))
			{
				if (!this.isMemberOfAGroup)
				{
					return;
				}
				if (!this.hasPermissionToChangeRank)
				{
					return;
				}
				Player player = PlayerTool.getPlayer(targetID);
				if (player == null)
				{
					return;
				}
				if (!player.quests.isMemberOfSameGroupAs(base.player))
				{
					return;
				}
				if (player.quests.groupRank != EPlayerGroupRank.ADMIN)
				{
					CommandWindow.LogWarning("Request to demote non-admin member of group?");
					return;
				}
				player.quests.changeRank(player.quests.groupRank - 1);
			}
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x00126729 File Offset: 0x00124B29
		public void sendDemote(CSteamID targetID)
		{
			base.channel.send("askDemote", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				targetID
			});
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x00126750 File Offset: 0x00124B50
		[SteamCall]
		public void askKickFromGroup(CSteamID steamID, CSteamID targetID)
		{
			if (base.channel.checkOwner(steamID))
			{
				if (!this.isMemberOfAGroup)
				{
					return;
				}
				if (!this.hasPermissionToKickMembers)
				{
					return;
				}
				Player player = PlayerTool.getPlayer(targetID);
				if (player == null)
				{
					return;
				}
				if (!player.quests.isMemberOfSameGroupAs(base.player))
				{
					return;
				}
				if (!player.quests.canBeKickedFromGroup)
				{
					return;
				}
				player.quests.leaveGroup(false);
			}
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x001267CE File Offset: 0x00124BCE
		public void sendKickFromGroup(CSteamID targetID)
		{
			base.channel.send("askKickFromGroup", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				targetID
			});
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x001267F4 File Offset: 0x00124BF4
		[SteamCall]
		public void askRenameGroup(CSteamID steamID, string newName)
		{
			if (!base.channel.checkOwner(steamID))
			{
				return;
			}
			if (!base.player.tryToPerformRateLimitedAction())
			{
				return;
			}
			if (newName.Length > 32)
			{
				newName = newName.Substring(0, 32);
			}
			if (!this.isMemberOfAGroup)
			{
				return;
			}
			if (!this.hasPermissionToChangeName)
			{
				return;
			}
			GroupInfo groupInfo = GroupManager.getGroupInfo(this.groupID);
			groupInfo.name = newName;
			GroupManager.sendGroupInfo(groupInfo);
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x0012686D File Offset: 0x00124C6D
		public void sendRenameGroup(string newName)
		{
			base.channel.send("askRenameGroup", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				newName
			});
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x0012688C File Offset: 0x00124C8C
		public void setFlag(ushort id, short value)
		{
			PlayerQuestFlag playerQuestFlag;
			if (this.flagsMap.TryGetValue(id, out playerQuestFlag))
			{
				playerQuestFlag.value = value;
			}
			else
			{
				playerQuestFlag = new PlayerQuestFlag(id, value);
				this.flagsMap.Add(id, playerQuestFlag);
				int num = this.flagsList.BinarySearch(playerQuestFlag, PlayerQuests.flagComparator);
				num = ~num;
				this.flagsList.Insert(num, playerQuestFlag);
			}
			if (base.channel.isOwner)
			{
				if (id == 29)
				{
					bool flag;
					if (value >= 1 && Provider.provider.achievementsService.getAchievement("Ensign", out flag) && !flag)
					{
						Provider.provider.achievementsService.setAchievement("Ensign");
					}
					bool flag2;
					if (value >= 2 && Provider.provider.achievementsService.getAchievement("Lieutenant", out flag2) && !flag2)
					{
						Provider.provider.achievementsService.setAchievement("Lieutenant");
					}
					bool flag3;
					if (value >= 3 && Provider.provider.achievementsService.getAchievement("Major", out flag3) && !flag3)
					{
						Provider.provider.achievementsService.setAchievement("Major");
					}
				}
				if (this.onFlagUpdated != null)
				{
					this.onFlagUpdated(id);
				}
				this.TriggerTrackedQuestUpdated();
			}
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x001269DC File Offset: 0x00124DDC
		public bool getFlag(ushort id, out short value)
		{
			PlayerQuestFlag playerQuestFlag;
			if (this.flagsMap.TryGetValue(id, out playerQuestFlag))
			{
				value = playerQuestFlag.value;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x00126A0C File Offset: 0x00124E0C
		public void removeFlag(ushort id)
		{
			PlayerQuestFlag item;
			if (this.flagsMap.TryGetValue(id, out item))
			{
				int num = this.flagsList.BinarySearch(item, PlayerQuests.flagComparator);
				if (num >= 0)
				{
					this.flagsMap.Remove(id);
					this.flagsList.RemoveAt(num);
					if (base.channel.isOwner)
					{
						if (this.onFlagUpdated != null)
						{
							this.onFlagUpdated(id);
						}
						this.TriggerTrackedQuestUpdated();
					}
				}
			}
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x00126A8C File Offset: 0x00124E8C
		public void addQuest(ushort id)
		{
			if (!this.questsMap.ContainsKey(id))
			{
				PlayerQuest playerQuest = new PlayerQuest(id);
				int num = this.questsList.BinarySearch(playerQuest, PlayerQuests.questComparator);
				if (num < 0)
				{
					this.questsMap.Add(id, playerQuest);
					num = ~num;
					this.questsList.Insert(num, playerQuest);
				}
			}
			this.trackQuest(id);
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x00126AEE File Offset: 0x00124EEE
		public bool getQuest(ushort id, out PlayerQuest quest)
		{
			if (this.questsMap.TryGetValue(id, out quest))
			{
				return true;
			}
			quest = null;
			return false;
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x00126B08 File Offset: 0x00124F08
		public ENPCQuestStatus getQuestStatus(ushort id)
		{
			PlayerQuest playerQuest;
			if (this.getQuest(id, out playerQuest))
			{
				if (playerQuest.asset.areConditionsMet(base.player))
				{
					return ENPCQuestStatus.READY;
				}
				return ENPCQuestStatus.ACTIVE;
			}
			else
			{
				short num;
				if (this.getFlag(id, out num))
				{
					return ENPCQuestStatus.COMPLETED;
				}
				return ENPCQuestStatus.NONE;
			}
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x00126B50 File Offset: 0x00124F50
		public void removeQuest(ushort id)
		{
			PlayerQuest item;
			if (this.questsMap.TryGetValue(id, out item))
			{
				int num = this.questsList.BinarySearch(item, PlayerQuests.questComparator);
				if (num >= 0)
				{
					this.questsMap.Remove(id);
					this.questsList.RemoveAt(num);
				}
			}
			if (this.TrackedQuestID == id)
			{
				if (this.questsList.Count > 0)
				{
					this.trackQuest(this.questsList[0].id);
				}
				else
				{
					this.trackQuest(0);
				}
			}
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x00126BE4 File Offset: 0x00124FE4
		public void trackHordeKill()
		{
			for (int i = 0; i < this.questsList.Count; i++)
			{
				PlayerQuest playerQuest = this.questsList[i];
				if (playerQuest != null && playerQuest.asset != null && playerQuest.asset.conditions != null)
				{
					for (int j = 0; j < playerQuest.asset.conditions.Length; j++)
					{
						NPCHordeKillsCondition npchordeKillsCondition = playerQuest.asset.conditions[j] as NPCHordeKillsCondition;
						if (npchordeKillsCondition != null)
						{
							if (npchordeKillsCondition.nav == base.player.movement.nav)
							{
								short num;
								this.getFlag(npchordeKillsCondition.id, out num);
								num += 1;
								this.sendSetFlag(npchordeKillsCondition.id, num);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x00126CBC File Offset: 0x001250BC
		public void trackZombieKill(Zombie zombie)
		{
			if (zombie == null)
			{
				return;
			}
			for (int i = 0; i < this.questsList.Count; i++)
			{
				PlayerQuest playerQuest = this.questsList[i];
				if (playerQuest != null && playerQuest.asset != null && playerQuest.asset.conditions != null)
				{
					for (int j = 0; j < playerQuest.asset.conditions.Length; j++)
					{
						NPCZombieKillsCondition npczombieKillsCondition = playerQuest.asset.conditions[j] as NPCZombieKillsCondition;
						if (npczombieKillsCondition != null)
						{
							if (npczombieKillsCondition.nav == base.player.movement.bound && (npczombieKillsCondition.zombie == EZombieSpeciality.NONE || npczombieKillsCondition.zombie == zombie.speciality))
							{
								short num;
								this.getFlag(npczombieKillsCondition.id, out num);
								num += 1;
								this.sendSetFlag(npczombieKillsCondition.id, num);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x00126DBC File Offset: 0x001251BC
		public void trackAnimalKill(Animal animal)
		{
			if (animal == null)
			{
				return;
			}
			for (int i = 0; i < this.questsList.Count; i++)
			{
				PlayerQuest playerQuest = this.questsList[i];
				if (playerQuest != null && playerQuest.asset != null && playerQuest.asset.conditions != null)
				{
					for (int j = 0; j < playerQuest.asset.conditions.Length; j++)
					{
						NPCAnimalKillsCondition npcanimalKillsCondition = playerQuest.asset.conditions[j] as NPCAnimalKillsCondition;
						if (npcanimalKillsCondition != null)
						{
							if (npcanimalKillsCondition.animal == animal.id)
							{
								short num;
								this.getFlag(npcanimalKillsCondition.id, out num);
								num += 1;
								this.sendSetFlag(npcanimalKillsCondition.id, num);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x00126E98 File Offset: 0x00125298
		public void completeQuest(ushort id, bool ignoreNPC = false)
		{
			if (!ignoreNPC)
			{
				if (this.checkNPC == null)
				{
					return;
				}
				if ((this.checkNPC.transform.position - base.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
			}
			PlayerQuest playerQuest;
			if (!this.getQuest(id, out playerQuest))
			{
				return;
			}
			if (!playerQuest.asset.areConditionsMet(base.player))
			{
				return;
			}
			this.removeQuest(id);
			this.setFlag(id, 1);
			playerQuest.asset.applyConditions(base.player, false);
			playerQuest.asset.grantRewards(base.player, false);
			bool flag;
			if (base.channel.isOwner && Provider.provider.achievementsService.getAchievement("Quest", out flag) && !flag)
			{
				Provider.provider.achievementsService.setAchievement("Quest");
			}
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x00126F90 File Offset: 0x00125390
		[SteamCall]
		public void askSellToVendor(CSteamID steamID, ushort id, byte index)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (this.checkNPC == null)
				{
					return;
				}
				if ((this.checkNPC.transform.position - base.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				VendorAsset vendorAsset = Assets.find(EAssetType.NPC, id) as VendorAsset;
				if (vendorAsset == null || vendorAsset.buying == null || (int)index >= vendorAsset.buying.Length)
				{
					return;
				}
				VendorBuying vendorBuying = vendorAsset.buying[(int)index];
				if (vendorBuying == null || !vendorBuying.canSell(base.player) || !vendorBuying.areConditionsMet(base.player))
				{
					return;
				}
				vendorBuying.applyConditions(base.player, true);
				vendorBuying.sell(base.player);
			}
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x00127076 File Offset: 0x00125476
		public void sendSellToVendor(ushort id, byte index)
		{
			base.channel.send("askSellToVendor", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				id,
				index
			});
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x001270A4 File Offset: 0x001254A4
		[SteamCall]
		public void askBuyFromVendor(CSteamID steamID, ushort id, byte index)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (this.checkNPC == null)
				{
					return;
				}
				if ((this.checkNPC.transform.position - base.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				VendorAsset vendorAsset = Assets.find(EAssetType.NPC, id) as VendorAsset;
				if (vendorAsset == null || vendorAsset.selling == null || (int)index >= vendorAsset.selling.Length)
				{
					return;
				}
				VendorSelling vendorSelling = vendorAsset.selling[(int)index];
				if (vendorSelling == null || !vendorSelling.canBuy(base.player) || !vendorSelling.areConditionsMet(base.player))
				{
					return;
				}
				vendorSelling.applyConditions(base.player, true);
				vendorSelling.buy(base.player);
			}
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x0012718A File Offset: 0x0012558A
		public void sendBuyFromVendor(ushort id, byte index)
		{
			base.channel.send("askBuyFromVendor", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				id,
				index
			});
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x001271B7 File Offset: 0x001255B7
		[SteamCall]
		public void tellSetFlag(CSteamID steamID, ushort id, short value)
		{
			if (base.channel.checkServer(steamID))
			{
				this.setFlag(id, value);
			}
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x001271D4 File Offset: 0x001255D4
		public void sendSetFlag(ushort id, short value)
		{
			this.setFlag(id, value);
			if (!base.channel.isOwner)
			{
				base.channel.send("tellSetFlag", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					id,
					value
				});
			}
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x00127224 File Offset: 0x00125624
		[SteamCall]
		public void tellRemoveFlag(CSteamID steamID, ushort id)
		{
			if (base.channel.checkServer(steamID))
			{
				this.removeFlag(id);
			}
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x0012723E File Offset: 0x0012563E
		public void sendRemoveFlag(ushort id)
		{
			this.removeFlag(id);
			if (!base.channel.isOwner)
			{
				base.channel.send("tellRemoveFlag", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					id
				});
			}
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x00127279 File Offset: 0x00125679
		[SteamCall]
		public void tellAddQuest(CSteamID steamID, ushort id)
		{
			if (base.channel.checkServer(steamID))
			{
				this.addQuest(id);
			}
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x00127293 File Offset: 0x00125693
		public void sendAddQuest(ushort id)
		{
			this.addQuest(id);
			if (!base.channel.isOwner)
			{
				base.channel.send("tellAddQuest", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					id
				});
			}
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x001272CE File Offset: 0x001256CE
		[SteamCall]
		public void tellRemoveQuest(CSteamID steamID, ushort id)
		{
			if (base.channel.checkServer(steamID))
			{
				this.removeQuest(id);
			}
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x001272E8 File Offset: 0x001256E8
		public void sendRemoveQuest(ushort id)
		{
			this.removeQuest(id);
			if (!base.channel.isOwner)
			{
				base.channel.send("tellRemoveQuest", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					id
				});
			}
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x00127323 File Offset: 0x00125723
		public void trackQuest(ushort id)
		{
			if (this.TrackedQuestID == id)
			{
				this.TrackedQuestID = 0;
			}
			else
			{
				this.TrackedQuestID = id;
			}
			if (base.channel.isOwner)
			{
				this.TriggerTrackedQuestUpdated();
			}
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x0012735A File Offset: 0x0012575A
		[SteamCall]
		public void askTrackQuest(CSteamID steamID, ushort id)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (!base.player.tryToPerformRateLimitedAction())
				{
					return;
				}
				this.trackQuest(id);
			}
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x0012738F File Offset: 0x0012578F
		public void sendTrackQuest(ushort id)
		{
			base.channel.send("askTrackQuest", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id
			});
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x001273B3 File Offset: 0x001257B3
		public void abandonQuest(ushort id)
		{
			this.removeQuest(id);
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x001273BC File Offset: 0x001257BC
		[SteamCall]
		public void askAbandonQuest(CSteamID steamID, ushort id)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (!base.player.tryToPerformRateLimitedAction())
				{
					return;
				}
				this.abandonQuest(id);
			}
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x001273F1 File Offset: 0x001257F1
		public void sendAbandonQuest(ushort id)
		{
			base.channel.send("askAbandonQuest", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id
			});
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x00127418 File Offset: 0x00125818
		public void registerMessage(ushort id)
		{
			if (this.checkNPC == null)
			{
				return;
			}
			if ((this.checkNPC.transform.position - base.transform.position).sqrMagnitude > 400f)
			{
				return;
			}
			DialogueAsset dialogueAsset = Assets.find(EAssetType.NPC, id) as DialogueAsset;
			if (dialogueAsset == null)
			{
				return;
			}
			int availableMessage = dialogueAsset.getAvailableMessage(base.player);
			if (availableMessage == -1)
			{
				return;
			}
			DialogueMessage dialogueMessage = dialogueAsset.messages[availableMessage];
			if (dialogueMessage == null || dialogueMessage.conditions == null || dialogueMessage.rewards == null)
			{
				return;
			}
			dialogueMessage.applyConditions(base.player, false);
			dialogueMessage.grantRewards(base.player, false);
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x001274D4 File Offset: 0x001258D4
		[SteamCall]
		public void askRegisterMessage(CSteamID steamID, ushort id)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				this.registerMessage(id);
			}
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x001274F8 File Offset: 0x001258F8
		public void sendRegisterMessage(ushort id)
		{
			base.channel.send("askRegisterMessage", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id
			});
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x0012751C File Offset: 0x0012591C
		public void registerResponse(ushort id, byte index)
		{
			if (this.checkNPC == null)
			{
				return;
			}
			if ((this.checkNPC.transform.position - base.transform.position).sqrMagnitude > 400f)
			{
				return;
			}
			DialogueAsset dialogueAsset = Assets.find(EAssetType.NPC, id) as DialogueAsset;
			if (dialogueAsset == null || dialogueAsset.responses == null || (int)index >= dialogueAsset.responses.Length)
			{
				return;
			}
			int availableMessage = dialogueAsset.getAvailableMessage(base.player);
			if (availableMessage == -1)
			{
				return;
			}
			DialogueMessage dialogueMessage = dialogueAsset.messages[availableMessage];
			if (dialogueMessage == null)
			{
				return;
			}
			if (dialogueMessage.responses != null && dialogueMessage.responses.Length > 0)
			{
				bool flag = false;
				for (int i = 0; i < dialogueMessage.responses.Length; i++)
				{
					if (index == dialogueMessage.responses[i])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return;
				}
			}
			DialogueResponse dialogueResponse = dialogueAsset.responses[(int)index];
			if (dialogueResponse == null || dialogueResponse.conditions == null || dialogueResponse.rewards == null || !dialogueResponse.areConditionsMet(base.player))
			{
				return;
			}
			if (dialogueResponse.messages != null && dialogueResponse.messages.Length > 0)
			{
				bool flag2 = false;
				for (int j = 0; j < dialogueResponse.messages.Length; j++)
				{
					if (availableMessage == (int)dialogueResponse.messages[j])
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					return;
				}
			}
			dialogueResponse.applyConditions(base.player, false);
			dialogueResponse.grantRewards(base.player, false);
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x001276CD File Offset: 0x00125ACD
		[SteamCall]
		public void askRegisterResponse(CSteamID steamID, ushort id, byte index)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				this.registerResponse(id, index);
			}
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x001276F2 File Offset: 0x00125AF2
		public void sendRegisterResponse(ushort id, byte index)
		{
			base.channel.send("askRegisterResponse", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				index
			});
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x00127720 File Offset: 0x00125B20
		[SteamCall]
		public void tellQuests(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID))
			{
				this.isMarkerPlaced = (bool)base.channel.read(Types.BOOLEAN_TYPE);
				this.markerPosition = (Vector3)base.channel.read(Types.VECTOR3_TYPE);
				this.radioFrequency = (uint)base.channel.read(Types.UINT32_TYPE);
				this.groupID = (CSteamID)base.channel.read(Types.STEAM_ID_TYPE);
				this.groupRank = (EPlayerGroupRank)((byte)base.channel.read(Types.BYTE_TYPE));
				if (base.channel.isOwner)
				{
					ushort num = (ushort)base.channel.read(Types.UINT16_TYPE);
					for (ushort num2 = 0; num2 < num; num2 += 1)
					{
						ushort num3 = (ushort)base.channel.read(Types.UINT16_TYPE);
						short newValue = (short)base.channel.read(Types.INT16_TYPE);
						PlayerQuestFlag playerQuestFlag = new PlayerQuestFlag(num3, newValue);
						this.flagsMap.Add(num3, playerQuestFlag);
						this.flagsList.Add(playerQuestFlag);
					}
					ushort num4 = (ushort)base.channel.read(Types.UINT16_TYPE);
					for (ushort num5 = 0; num5 < num4; num5 += 1)
					{
						ushort num6 = (ushort)base.channel.read(Types.UINT16_TYPE);
						PlayerQuest playerQuest = new PlayerQuest(num6);
						this.questsMap.Add(num6, playerQuest);
						this.questsList.Add(playerQuest);
					}
					this.TrackedQuestID = (ushort)base.channel.read(Types.UINT16_TYPE);
					if (this.onFlagsUpdated != null)
					{
						this.onFlagsUpdated();
					}
					this.TriggerTrackedQuestUpdated();
				}
			}
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x001278F0 File Offset: 0x00125CF0
		[SteamCall]
		public void askQuests(CSteamID steamID)
		{
			if (Provider.isServer)
			{
				if (this.isMemberOfAGroup)
				{
					GroupInfo groupInfo = GroupManager.getGroupInfo(this.groupID);
					if (groupInfo != null)
					{
						GroupManager.sendGroupInfo(steamID, groupInfo);
					}
				}
				base.channel.openWrite();
				base.channel.write(this.isMarkerPlaced);
				base.channel.write(this.markerPosition);
				base.channel.write(this.radioFrequency);
				base.channel.write(this.groupID);
				base.channel.write((byte)this.groupRank);
				if (base.channel.checkOwner(steamID))
				{
					base.channel.write((ushort)this.flagsList.Count);
					ushort num = 0;
					while ((int)num < this.flagsList.Count)
					{
						PlayerQuestFlag playerQuestFlag = this.flagsList[(int)num];
						base.channel.write(playerQuestFlag.id);
						base.channel.write(playerQuestFlag.value);
						num += 1;
					}
					base.channel.write((ushort)this.questsList.Count);
					ushort num2 = 0;
					while ((int)num2 < this.questsList.Count)
					{
						PlayerQuest playerQuest = this.questsList[(int)num2];
						base.channel.write(playerQuest.id);
						num2 += 1;
					}
					base.channel.write(this.TrackedQuestID);
				}
				base.channel.closeWrite("tellQuests", steamID, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
			}
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x00127AAF File Offset: 0x00125EAF
		private void OnPlayerNavChanged(PlayerMovement sender, byte oldNav, byte newNav)
		{
			if (newNav == 255)
			{
				return;
			}
			ZombieManager.regions[(int)newNav].UpdateBoss();
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x00127AC9 File Offset: 0x00125EC9
		private void onExperienceUpdated(uint experience)
		{
			this.TriggerTrackedQuestUpdated();
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x00127AD1 File Offset: 0x00125ED1
		private void onReputationUpdated(int reputation)
		{
			this.TriggerTrackedQuestUpdated();
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x00127AD9 File Offset: 0x00125ED9
		private void onInventoryStateUpdated()
		{
			this.TriggerTrackedQuestUpdated();
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x00127AE1 File Offset: 0x00125EE1
		private void onDayNightUpdated(bool isDaytime)
		{
			if (this.onExternalConditionsUpdated != null)
			{
				this.onExternalConditionsUpdated();
			}
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x00127AF9 File Offset: 0x00125EF9
		public void init()
		{
			base.channel.send("askQuests", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x00127B14 File Offset: 0x00125F14
		private void Start()
		{
			this.flagsMap = new Dictionary<ushort, PlayerQuestFlag>();
			this.flagsList = new List<PlayerQuestFlag>();
			this.questsMap = new Dictionary<ushort, PlayerQuest>();
			this.questsList = new List<PlayerQuest>();
			this.groupInvites = new HashSet<CSteamID>();
			if (Provider.isServer)
			{
				this.load();
				base.player.movement.PlayerNavChanged += this.OnPlayerNavChanged;
				if (base.channel.isOwner && this.onFlagsUpdated != null)
				{
					this.onFlagsUpdated();
				}
			}
			else
			{
				base.Invoke("init", 0.1f);
			}
			if (base.channel.isOwner)
			{
				PlayerSkills skills = base.player.skills;
				skills.onExperienceUpdated = (ExperienceUpdated)Delegate.Combine(skills.onExperienceUpdated, new ExperienceUpdated(this.onExperienceUpdated));
				PlayerSkills skills2 = base.player.skills;
				skills2.onReputationUpdated = (ReputationUpdated)Delegate.Combine(skills2.onReputationUpdated, new ReputationUpdated(this.onReputationUpdated));
				PlayerInventory inventory = base.player.inventory;
				inventory.onInventoryStateUpdated = (InventoryStateUpdated)Delegate.Combine(inventory.onInventoryStateUpdated, new InventoryStateUpdated(this.onInventoryStateUpdated));
				LightingManager.onDayNightUpdated = (DayNightUpdated)Delegate.Combine(LightingManager.onDayNightUpdated, new DayNightUpdated(this.onDayNightUpdated));
			}
			if ((base.channel.isOwner || Provider.isServer) && Player.onPlayerCreated != null)
			{
				Player.onPlayerCreated(base.player);
			}
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x00127CA7 File Offset: 0x001260A7
		private void OnDestroy()
		{
			if (base.channel.isOwner)
			{
				LightingManager.onDayNightUpdated = (DayNightUpdated)Delegate.Remove(LightingManager.onDayNightUpdated, new DayNightUpdated(this.onDayNightUpdated));
			}
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x00127CDC File Offset: 0x001260DC
		public void load()
		{
			this.isMarkerPlaced = false;
			this.markerPosition = Vector3.zero;
			this.radioFrequency = PlayerQuests.DEFAULT_RADIO_FREQUENCY;
			if (PlayerSavedata.fileExists(base.channel.owner.playerID, "/Player/Quests.dat") && Level.info.type == ELevelType.SURVIVAL)
			{
				River river = PlayerSavedata.openRiver(base.channel.owner.playerID, "/Player/Quests.dat", true);
				byte b = river.readByte();
				if (b > 0)
				{
					if (b > 6)
					{
						this.isMarkerPlaced = river.readBoolean();
						this.markerPosition = river.readSingleVector3();
					}
					if (b > 5)
					{
						this.radioFrequency = river.readUInt32();
					}
					if (b > 2)
					{
						this.groupID = river.readSteamID();
					}
					else
					{
						this.groupID = CSteamID.Nil;
					}
					if (b > 3)
					{
						this.groupRank = (EPlayerGroupRank)river.readByte();
					}
					else
					{
						this.groupRank = EPlayerGroupRank.MEMBER;
					}
					if (b > 4)
					{
						this.inMainGroup = river.readBoolean();
					}
					else
					{
						this.inMainGroup = false;
					}
					ushort num = river.readUInt16();
					for (ushort num2 = 0; num2 < num; num2 += 1)
					{
						ushort num3 = river.readUInt16();
						short newValue = river.readInt16();
						PlayerQuestFlag playerQuestFlag = new PlayerQuestFlag(num3, newValue);
						this.flagsMap.Add(num3, playerQuestFlag);
						this.flagsList.Add(playerQuestFlag);
					}
					ushort num4 = river.readUInt16();
					for (ushort num5 = 0; num5 < num4; num5 += 1)
					{
						ushort num6 = river.readUInt16();
						PlayerQuest playerQuest = new PlayerQuest(num6);
						this.questsMap.Add(num6, playerQuest);
						this.questsList.Add(playerQuest);
					}
					if (b > 1)
					{
						this.TrackedQuestID = river.readUInt16();
					}
					else
					{
						this.TrackedQuestID = 0;
					}
				}
				river.closeRiver();
			}
			if (Provider.modeConfigData.Gameplay.Allow_Dynamic_Groups)
			{
				if (this.groupID == CSteamID.Nil)
				{
					if (base.channel.owner.lobbyID != CSteamID.Nil)
					{
						this.groupID = base.channel.owner.lobbyID;
						bool flag;
						GroupInfo orAddGroup = GroupManager.getOrAddGroup(this.groupID, base.channel.owner.playerID.playerName + "'s Group", out flag);
						orAddGroup.members += 1u;
						this.groupRank = ((!flag) ? EPlayerGroupRank.MEMBER : EPlayerGroupRank.OWNER);
						this.inMainGroup = false;
						GroupManager.sendGroupInfo(orAddGroup);
					}
					else
					{
						this.loadMainGroup();
					}
				}
				else if (this.inMainGroup)
				{
					if (Provider.modeConfigData.Gameplay.Allow_Static_Groups)
					{
						if (this.groupID != base.channel.owner.playerID.group)
						{
							this.loadMainGroup();
						}
					}
					else
					{
						this.loadMainGroup();
					}
				}
				else if (GroupManager.getGroupInfo(this.groupID) == null)
				{
					this.loadMainGroup();
				}
			}
			else
			{
				this.loadMainGroup();
			}
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x00128000 File Offset: 0x00126400
		private void loadMainGroup()
		{
			if (Provider.modeConfigData.Gameplay.Allow_Static_Groups)
			{
				this.groupID = base.channel.owner.playerID.group;
				this.inMainGroup = (this.groupID != CSteamID.Nil);
			}
			else
			{
				this.groupID = CSteamID.Nil;
				this.inMainGroup = false;
			}
			this.groupRank = EPlayerGroupRank.MEMBER;
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x00128070 File Offset: 0x00126470
		public void save()
		{
			River river = PlayerSavedata.openRiver(base.channel.owner.playerID, "/Player/Quests.dat", false);
			river.writeByte(PlayerQuests.SAVEDATA_VERSION);
			river.writeBoolean(this.isMarkerPlaced);
			river.writeSingleVector3(this.markerPosition);
			river.writeUInt32(this.radioFrequency);
			river.writeSteamID(this.groupID);
			river.writeByte((byte)this.groupRank);
			river.writeBoolean(this.inMainGroup);
			river.writeUInt16((ushort)this.flagsList.Count);
			ushort num = 0;
			while ((int)num < this.flagsList.Count)
			{
				PlayerQuestFlag playerQuestFlag = this.flagsList[(int)num];
				river.writeUInt16(playerQuestFlag.id);
				river.writeInt16(playerQuestFlag.value);
				num += 1;
			}
			river.writeUInt16((ushort)this.questsList.Count);
			ushort num2 = 0;
			while ((int)num2 < this.questsList.Count)
			{
				PlayerQuest playerQuest = this.questsList[(int)num2];
				river.writeUInt16(playerQuest.id);
				num2 += 1;
			}
			river.writeUInt16(this.TrackedQuestID);
			river.closeRiver();
		}

		// Token: 0x04001D83 RID: 7555
		public static readonly byte SAVEDATA_VERSION = 7;

		// Token: 0x04001D84 RID: 7556
		public static readonly uint DEFAULT_RADIO_FREQUENCY = 460327u;

		// Token: 0x04001D85 RID: 7557
		private static PlayerQuestFlagComparator flagComparator = new PlayerQuestFlagComparator();

		// Token: 0x04001D86 RID: 7558
		private static PlayerQuestComparator questComparator = new PlayerQuestComparator();

		// Token: 0x04001D87 RID: 7559
		public InteractableObjectNPC checkNPC;

		// Token: 0x04001D88 RID: 7560
		private Dictionary<ushort, PlayerQuestFlag> flagsMap;

		// Token: 0x04001D89 RID: 7561
		public ExternalConditionsUpdated onExternalConditionsUpdated;

		// Token: 0x04001D8A RID: 7562
		public FlagsUpdated onFlagsUpdated;

		// Token: 0x04001D8B RID: 7563
		public FlagUpdated onFlagUpdated;

		// Token: 0x04001D8C RID: 7564
		public static GroupUpdatedHandler groupUpdated;

		// Token: 0x04001D92 RID: 7570
		private Dictionary<ushort, PlayerQuest> questsMap;

		// Token: 0x04001D9B RID: 7579
		private bool inMainGroup;
	}
}
