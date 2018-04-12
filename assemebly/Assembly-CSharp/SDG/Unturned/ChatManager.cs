using System;
using System.Collections.Generic;
using SDG.Framework.Debug;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000596 RID: 1430
	public class ChatManager : SteamCaller
	{
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060027C3 RID: 10179 RVA: 0x000F0AD3 File Offset: 0x000EEED3
		public static ChatManager instance
		{
			get
			{
				return ChatManager.manager;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060027C4 RID: 10180 RVA: 0x000F0ADA File Offset: 0x000EEEDA
		public static Chat[] chat
		{
			get
			{
				return ChatManager._chat;
			}
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x000F0AE4 File Offset: 0x000EEEE4
		public static string replace(string text, int index, int count, char mask)
		{
			string text2 = text.Substring(0, index);
			for (int i = 0; i < count; i++)
			{
				text2 += mask;
			}
			return text2 + text.Substring(index + count, text.Length - index - count);
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x000F0B34 File Offset: 0x000EEF34
		public static string filter(string text)
		{
			string text2 = text.ToLower();
			if (text.Length > 0)
			{
				bool flag = text.IndexOf(' ') != -1;
				for (int i = 0; i < ChatManager.SWEARS.Length; i++)
				{
					string text3 = ChatManager.SWEARS[i];
					int num = text2.IndexOf(text3, 0);
					while (num != -1)
					{
						if (!flag || ((num == 0 || !char.IsLetterOrDigit(text2[num - 1])) && (num == text2.Length - text3.Length || !char.IsLetterOrDigit(text2[num + text3.Length]))))
						{
							text2 = ChatManager.replace(text2, num, text3.Length, '#');
							text = ChatManager.replace(text, num, text3.Length, '#');
							num = text2.IndexOf(text3, num);
						}
						else
						{
							num = text2.IndexOf(text3, num + 1);
						}
					}
				}
			}
			return text;
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x000F0C2C File Offset: 0x000EF02C
		public static void list(CSteamID steamID, EChatMode mode, Color color, bool isRich, string text)
		{
			text = text.Trim();
			if (OptionsSettings.filter)
			{
				text = ChatManager.filter(text);
			}
			if (OptionsSettings.streamer)
			{
				color = Color.white;
			}
			SteamPlayer steamPlayer = null;
			string text2;
			if (steamID == CSteamID.Nil)
			{
				text2 = Provider.localization.format("Say");
			}
			else
			{
				steamPlayer = PlayerTool.getSteamPlayer(steamID);
				if (steamPlayer == null)
				{
					return;
				}
				if (!OptionsSettings.chatText && steamPlayer.playerID.steamID != Provider.client)
				{
					return;
				}
				if (steamPlayer.player.quests.isMemberOfSameGroupAs(Player.player))
				{
					if (steamPlayer.playerID.nickName != string.Empty && steamPlayer.playerID.steamID != Provider.client)
					{
						text2 = steamPlayer.playerID.nickName;
					}
					else
					{
						text2 = steamPlayer.playerID.characterName;
					}
				}
				else
				{
					text2 = steamPlayer.playerID.characterName;
				}
			}
			for (int i = ChatManager.chat.Length - 1; i > 0; i--)
			{
				if (ChatManager.chat[i - 1] != null)
				{
					if (ChatManager.chat[i] == null)
					{
						ChatManager.chat[i] = new Chat(ChatManager.chat[i - 1].player, ChatManager.chat[i - 1].mode, ChatManager.chat[i - 1].color, ChatManager.chat[i - 1].isRich, ChatManager.chat[i - 1].speaker, ChatManager.chat[i - 1].text);
					}
					else
					{
						ChatManager.chat[i].player = ChatManager.chat[i - 1].player;
						ChatManager.chat[i].mode = ChatManager.chat[i - 1].mode;
						ChatManager.chat[i].color = ChatManager.chat[i - 1].color;
						ChatManager.chat[i].isRich = ChatManager.chat[i - 1].isRich;
						ChatManager.chat[i].speaker = ChatManager.chat[i - 1].speaker;
						ChatManager.chat[i].text = ChatManager.chat[i - 1].text;
					}
				}
			}
			if (ChatManager.chat[0] == null)
			{
				ChatManager.chat[0] = new Chat(steamPlayer, mode, color, isRich, text2, text);
			}
			else
			{
				ChatManager.chat[0].player = steamPlayer;
				ChatManager.chat[0].mode = mode;
				ChatManager.chat[0].color = color;
				ChatManager.chat[0].isRich = isRich;
				ChatManager.chat[0].speaker = text2;
				ChatManager.chat[0].text = text;
			}
			if (ChatManager.onListed != null)
			{
				ChatManager.onListed();
			}
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x000F0F00 File Offset: 0x000EF300
		public static bool process(SteamPlayer player, string text)
		{
			bool flag = false;
			bool result = true;
			string a = text.Substring(0, 1);
			if ((a == "@" || a == "/") && player.isAdmin)
			{
				flag = true;
				result = false;
			}
			if (ChatManager.onCheckPermissions != null)
			{
				ChatManager.onCheckPermissions(player, text, ref flag, ref result);
			}
			if (flag)
			{
				Commander.execute(player.playerID.steamID, text.Substring(1));
			}
			return result;
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x000F0F84 File Offset: 0x000EF384
		[SteamCall]
		public void tellVoteStart(CSteamID steamID, CSteamID origin, CSteamID target, byte votesNeeded)
		{
			if (base.channel.checkServer(steamID))
			{
				SteamPlayer steamPlayer = PlayerTool.getSteamPlayer(origin);
				if (steamPlayer == null)
				{
					return;
				}
				SteamPlayer steamPlayer2 = PlayerTool.getSteamPlayer(target);
				if (steamPlayer2 == null)
				{
					return;
				}
				ChatManager.needsVote = true;
				ChatManager.hasVote = false;
				if (ChatManager.onVotingStart != null)
				{
					ChatManager.onVotingStart(steamPlayer, steamPlayer2, votesNeeded);
				}
			}
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x000F0FE2 File Offset: 0x000EF3E2
		[SteamCall]
		public void tellVoteUpdate(CSteamID steamID, byte voteYes, byte voteNo)
		{
			if (base.channel.checkServer(steamID) && ChatManager.onVotingUpdate != null)
			{
				ChatManager.onVotingUpdate(voteYes, voteNo);
			}
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x000F100B File Offset: 0x000EF40B
		[SteamCall]
		public void tellVoteStop(CSteamID steamID, byte message)
		{
			if (base.channel.checkServer(steamID))
			{
				ChatManager.needsVote = false;
				if (ChatManager.onVotingStop != null)
				{
					ChatManager.onVotingStop((EVotingMessage)message);
				}
			}
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x000F1039 File Offset: 0x000EF439
		[SteamCall]
		public void tellVoteMessage(CSteamID steamID, byte message)
		{
			if (base.channel.checkServer(steamID) && ChatManager.onVotingMessage != null)
			{
				ChatManager.onVotingMessage((EVotingMessage)message);
			}
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x000F1064 File Offset: 0x000EF464
		[SteamCall]
		public void askVote(CSteamID steamID, bool vote)
		{
			if (Provider.isServer)
			{
				SteamPlayer steamPlayer = PlayerTool.getSteamPlayer(steamID);
				if (steamPlayer == null)
				{
					return;
				}
				if (!ChatManager.isVoting)
				{
					return;
				}
				if (!steamPlayer.player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if (ChatManager.votes.Contains(steamID))
				{
					return;
				}
				ChatManager.votes.Add(steamID);
				if (vote)
				{
					ChatManager.voteYes += 1;
				}
				else
				{
					ChatManager.voteNo += 1;
				}
				ChatManager.manager.channel.send("tellVoteUpdate", ESteamCall.CLIENTS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					ChatManager.voteYes,
					ChatManager.voteNo
				});
			}
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x000F1120 File Offset: 0x000EF520
		[SteamCall]
		public void askCallVote(CSteamID steamID, CSteamID target)
		{
			if (Provider.isServer)
			{
				if (ChatManager.isVoting)
				{
					return;
				}
				SteamPlayer steamPlayer = PlayerTool.getSteamPlayer(steamID);
				if (steamPlayer == null || Time.realtimeSinceStartup < steamPlayer.nextVote)
				{
					ChatManager.manager.channel.send("tellVoteMessage", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						1
					});
					return;
				}
				if (!steamPlayer.player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if (!ChatManager.voteAllowed)
				{
					ChatManager.manager.channel.send("tellVoteMessage", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						0
					});
					return;
				}
				SteamPlayer steamPlayer2 = PlayerTool.getSteamPlayer(target);
				if (steamPlayer2 == null || steamPlayer2.isAdmin)
				{
					return;
				}
				if (Provider.clients.Count < (int)ChatManager.votePlayers)
				{
					ChatManager.manager.channel.send("tellVoteMessage", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						2
					});
					return;
				}
				CommandWindow.Log(Provider.localization.format("Vote_Kick", new object[]
				{
					steamPlayer.playerID.characterName,
					steamPlayer.playerID.playerName,
					steamPlayer2.playerID.characterName,
					steamPlayer2.playerID.playerName
				}));
				ChatManager.lastVote = Time.realtimeSinceStartup;
				ChatManager.isVoting = true;
				ChatManager.voteYes = 0;
				ChatManager.voteNo = 0;
				ChatManager.votesPossible = (byte)Provider.clients.Count;
				ChatManager.votesNeeded = (byte)Mathf.Ceil((float)ChatManager.votesPossible * ChatManager.votePercentage);
				ChatManager.voteOrigin = steamPlayer;
				ChatManager.voteTarget = target;
				ChatManager.votes = new List<CSteamID>();
				P2PSessionState_t p2PSessionState_t;
				if (SteamGameServerNetworking.GetP2PSessionState(ChatManager.voteTarget, out p2PSessionState_t))
				{
					ChatManager.voteIP = p2PSessionState_t.m_nRemoteIP;
				}
				else
				{
					ChatManager.voteIP = 0u;
				}
				ChatManager.manager.channel.send("tellVoteStart", ESteamCall.CLIENTS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					steamID,
					target,
					ChatManager.votesNeeded
				});
			}
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x000F132B File Offset: 0x000EF72B
		public static void sendVote(bool vote)
		{
			ChatManager.manager.channel.send("askVote", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				vote
			});
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x000F1353 File Offset: 0x000EF753
		public static void sendCallVote(CSteamID target)
		{
			ChatManager.manager.channel.send("askCallVote", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				target
			});
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x000F137B File Offset: 0x000EF77B
		[SteamCall]
		public void tellChat(CSteamID steamID, CSteamID owner, byte mode, Color color, bool rich, string text)
		{
			if (base.channel.checkServer(steamID))
			{
				ChatManager.list(owner, (EChatMode)mode, color, rich, text);
			}
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x000F139C File Offset: 0x000EF79C
		[SteamCall]
		public void askChat(CSteamID steamID, byte mode, string text)
		{
			if (Provider.isServer)
			{
				SteamPlayer steamPlayer = PlayerTool.getSteamPlayer(steamID);
				if (steamPlayer == null || steamPlayer.player == null)
				{
					return;
				}
				if (Time.realtimeSinceStartup - steamPlayer.lastChat < ChatManager.chatrate)
				{
					return;
				}
				steamPlayer.lastChat = Time.realtimeSinceStartup;
				if (!steamPlayer.player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if (text.Length < 2)
				{
					return;
				}
				if (text.Length > ChatManager.LENGTH)
				{
					text = text.Substring(0, ChatManager.LENGTH);
				}
				text = text.Trim();
				if (mode == 0)
				{
					if (CommandWindow.shouldLogChat)
					{
						CommandWindow.Log(Provider.localization.format("Global", new object[]
						{
							steamPlayer.playerID.characterName,
							steamPlayer.playerID.playerName,
							text
						}));
					}
				}
				else if (mode == 1)
				{
					if (CommandWindow.shouldLogChat)
					{
						CommandWindow.Log(Provider.localization.format("Local", new object[]
						{
							steamPlayer.playerID.characterName,
							steamPlayer.playerID.playerName,
							text
						}));
					}
				}
				else
				{
					if (mode != 2)
					{
						return;
					}
					if (CommandWindow.shouldLogChat)
					{
						CommandWindow.Log(Provider.localization.format("Group", new object[]
						{
							steamPlayer.playerID.characterName,
							steamPlayer.playerID.playerName,
							text
						}));
					}
				}
				Color color = Color.white;
				if (steamPlayer.isAdmin && !Provider.hideAdmins)
				{
					color = Palette.ADMIN;
				}
				else if (steamPlayer.isPro)
				{
					color = Palette.PRO;
				}
				bool flag = false;
				bool flag2 = true;
				if (ChatManager.onChatted != null)
				{
					ChatManager.onChatted(steamPlayer, (EChatMode)mode, ref color, ref flag, text, ref flag2);
				}
				if (ChatManager.process(steamPlayer, text) && flag2)
				{
					if (mode == 0)
					{
						base.channel.send("tellChat", ESteamCall.OTHERS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							steamID,
							mode,
							color,
							flag,
							text
						});
					}
					else if (mode == 1)
					{
						base.channel.send("tellChat", ESteamCall.OTHERS, steamPlayer.player.transform.position, EffectManager.MEDIUM, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							steamID,
							mode,
							color,
							flag,
							text
						});
					}
					else if (mode == 2 && steamPlayer.player.quests.groupID != CSteamID.Nil)
					{
						for (int i = 0; i < Provider.clients.Count; i++)
						{
							SteamPlayer steamPlayer2 = Provider.clients[i];
							if (steamPlayer2.player != null && steamPlayer.player.quests.isMemberOfSameGroupAs(steamPlayer2.player))
							{
								base.channel.send("tellChat", steamPlayer2.playerID.steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
								{
									steamID,
									mode,
									color,
									flag,
									text
								});
							}
						}
					}
				}
			}
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000F1711 File Offset: 0x000EFB11
		[TerminalCommandMethod("chat.send", "broadcast message in chat")]
		public static void terminalChat([TerminalCommandParameter("message", "text to send")] string message)
		{
			ChatManager.sendChat(EChatMode.GLOBAL, message);
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000F171C File Offset: 0x000EFB1C
		public static void sendChat(EChatMode mode, string text)
		{
			if (!Provider.isServer)
			{
				ChatManager.manager.channel.send("askChat", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					(byte)mode,
					text
				});
			}
			else if (!Dedicator.isDedicated)
			{
				SteamPlayer steamPlayer = PlayerTool.getSteamPlayer(Provider.client);
				if (steamPlayer == null)
				{
					return;
				}
				Color color = (!Provider.isPro) ? Color.white : Palette.PRO;
				bool isRich = false;
				bool flag = true;
				if (ChatManager.onChatted != null)
				{
					ChatManager.onChatted(steamPlayer, mode, ref color, ref isRich, text, ref flag);
				}
				if (ChatManager.process(steamPlayer, text) && flag)
				{
					ChatManager.list(Provider.client, mode, color, isRich, text);
				}
			}
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x000F17DA File Offset: 0x000EFBDA
		public static void say(CSteamID target, string text, Color color, bool isRich = false)
		{
			ChatManager.say(target, text, color, EChatMode.WELCOME, isRich);
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x000F17E8 File Offset: 0x000EFBE8
		public static void say(CSteamID target, string text, Color color, EChatMode mode, bool isRich = false)
		{
			if (Provider.isServer)
			{
				if (text.Length > ChatManager.LENGTH)
				{
					text = text.Substring(0, ChatManager.LENGTH);
				}
				if (Dedicator.isDedicated)
				{
					ChatManager.manager.channel.send("tellChat", target, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						CSteamID.Nil,
						(byte)mode,
						color,
						isRich,
						text
					});
				}
				else
				{
					ChatManager.list(CSteamID.Nil, mode, color, isRich, text);
				}
			}
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x000F1888 File Offset: 0x000EFC88
		public static void say(string text, Color color, bool isRich = false)
		{
			if (Provider.isServer)
			{
				if (text.Length > ChatManager.LENGTH)
				{
					text = text.Substring(0, ChatManager.LENGTH);
				}
				if (Dedicator.isDedicated)
				{
					ChatManager.manager.channel.send("tellChat", ESteamCall.OTHERS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						CSteamID.Nil,
						4,
						color,
						isRich,
						text
					});
				}
				else
				{
					ChatManager.list(CSteamID.Nil, EChatMode.SAY, color, isRich, text);
				}
			}
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x000F1924 File Offset: 0x000EFD24
		private void onLevelLoaded(int level)
		{
			if (level > Level.SETUP)
			{
				for (int i = 0; i < ChatManager.chat.Length; i++)
				{
					ChatManager.chat[i] = null;
				}
			}
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x000F195C File Offset: 0x000EFD5C
		private void onServerConnected(CSteamID steamID)
		{
			if (Provider.isServer && ChatManager.welcomeText != string.Empty)
			{
				SteamPlayer steamPlayer = PlayerTool.getSteamPlayer(steamID);
				ChatManager.say(steamPlayer.playerID.steamID, string.Format(ChatManager.welcomeText, steamPlayer.playerID.characterName), ChatManager.welcomeColor, false);
			}
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x000F19BC File Offset: 0x000EFDBC
		private void Update()
		{
			if (ChatManager.isVoting && (Time.realtimeSinceStartup - ChatManager.lastVote > ChatManager.voteDuration || ChatManager.voteYes >= ChatManager.votesNeeded || ChatManager.voteNo > ChatManager.votesPossible - ChatManager.votesNeeded))
			{
				ChatManager.isVoting = false;
				if (ChatManager.voteYes >= ChatManager.votesNeeded)
				{
					if (ChatManager.voteOrigin != null)
					{
						ChatManager.voteOrigin.nextVote = Time.realtimeSinceStartup + ChatManager.votePassCooldown;
					}
					CommandWindow.Log(Provider.localization.format("Vote_Pass"));
					ChatManager.manager.channel.send("tellVoteStop", ESteamCall.CLIENTS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						3
					});
					SteamBlacklist.ban(ChatManager.voteTarget, ChatManager.voteIP, CSteamID.Nil, "you were vote kicked", SteamBlacklist.TEMPORARY);
				}
				else
				{
					if (ChatManager.voteOrigin != null)
					{
						ChatManager.voteOrigin.nextVote = Time.realtimeSinceStartup + ChatManager.voteFailCooldown;
					}
					CommandWindow.Log(Provider.localization.format("Vote_Fail"));
					ChatManager.manager.channel.send("tellVoteStop", ESteamCall.CLIENTS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						4
					});
				}
			}
			if (ChatManager.needsVote && !ChatManager.hasVote)
			{
				if (Input.GetKeyDown(KeyCode.F1))
				{
					ChatManager.needsVote = false;
					ChatManager.hasVote = true;
					ChatManager.sendVote(true);
				}
				else if (Input.GetKeyDown(KeyCode.F2))
				{
					ChatManager.needsVote = false;
					ChatManager.hasVote = true;
					ChatManager.sendVote(false);
				}
			}
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x000F1B54 File Offset: 0x000EFF54
		private void Start()
		{
			ChatManager.manager = this;
			ChatManager._chat = new Chat[16];
			Level.onLevelLoaded = (LevelLoaded)Delegate.Combine(Level.onLevelLoaded, new LevelLoaded(this.onLevelLoaded));
			Provider.onServerConnected = (Provider.ServerConnected)Delegate.Combine(Provider.onServerConnected, new Provider.ServerConnected(this.onServerConnected));
		}

		// Token: 0x040018D7 RID: 6359
		public static readonly int LENGTH = 90;

		// Token: 0x040018D8 RID: 6360
		public static Listed onListed;

		// Token: 0x040018D9 RID: 6361
		public static Chatted onChatted;

		// Token: 0x040018DA RID: 6362
		public static CheckPermissions onCheckPermissions;

		// Token: 0x040018DB RID: 6363
		public static VotingStart onVotingStart;

		// Token: 0x040018DC RID: 6364
		public static VotingUpdate onVotingUpdate;

		// Token: 0x040018DD RID: 6365
		public static VotingStop onVotingStop;

		// Token: 0x040018DE RID: 6366
		public static VotingMessage onVotingMessage;

		// Token: 0x040018DF RID: 6367
		public static string welcomeText = string.Empty;

		// Token: 0x040018E0 RID: 6368
		public static Color welcomeColor = Palette.SERVER;

		// Token: 0x040018E1 RID: 6369
		public static float chatrate = 0.25f;

		// Token: 0x040018E2 RID: 6370
		public static bool voteAllowed = false;

		// Token: 0x040018E3 RID: 6371
		public static float votePassCooldown = 5f;

		// Token: 0x040018E4 RID: 6372
		public static float voteFailCooldown = 60f;

		// Token: 0x040018E5 RID: 6373
		public static float voteDuration = 15f;

		// Token: 0x040018E6 RID: 6374
		public static float votePercentage = 0.75f;

		// Token: 0x040018E7 RID: 6375
		public static byte votePlayers = 3;

		// Token: 0x040018E8 RID: 6376
		private static float lastVote;

		// Token: 0x040018E9 RID: 6377
		private static bool isVoting;

		// Token: 0x040018EA RID: 6378
		private static bool needsVote;

		// Token: 0x040018EB RID: 6379
		private static bool hasVote;

		// Token: 0x040018EC RID: 6380
		private static byte voteYes;

		// Token: 0x040018ED RID: 6381
		private static byte voteNo;

		// Token: 0x040018EE RID: 6382
		private static byte votesPossible;

		// Token: 0x040018EF RID: 6383
		private static byte votesNeeded;

		// Token: 0x040018F0 RID: 6384
		private static SteamPlayer voteOrigin;

		// Token: 0x040018F1 RID: 6385
		private static CSteamID voteTarget;

		// Token: 0x040018F2 RID: 6386
		private static uint voteIP;

		// Token: 0x040018F3 RID: 6387
		private static List<CSteamID> votes;

		// Token: 0x040018F4 RID: 6388
		private static ChatManager manager;

		// Token: 0x040018F5 RID: 6389
		private static Chat[] _chat;

		// Token: 0x040018F6 RID: 6390
		public static readonly string[] SWEARS = new string[]
		{
			"bitch",
			"clit",
			"cunt",
			"dick",
			"pussy",
			"penis",
			"vagina",
			"fuck",
			"fucking",
			"fuckd",
			"fucked",
			"shit",
			"shiting",
			"shitting",
			"shat",
			"damn",
			"damned",
			"hell",
			"cock",
			"whore",
			"fag",
			"faggot",
			"nigger",
			"suka",
			"cuka",
			"cyka",
			"сука",
			"blyat",
			"блят",
			"блять"
		};
	}
}
