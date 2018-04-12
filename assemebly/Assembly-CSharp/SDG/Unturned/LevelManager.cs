using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005AA RID: 1450
	public class LevelManager : SteamCaller
	{
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002868 RID: 10344 RVA: 0x000F4FBD File Offset: 0x000F33BD
		public static LevelManager instance
		{
			get
			{
				return LevelManager.manager;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002869 RID: 10345 RVA: 0x000F4FC4 File Offset: 0x000F33C4
		public static ELevelType levelType
		{
			get
			{
				return LevelManager._levelType;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x0600286A RID: 10346 RVA: 0x000F4FCB File Offset: 0x000F33CB
		public static Vector3 arenaCurrentCenter
		{
			get
			{
				return LevelManager._arenaCurrentCenter;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600286B RID: 10347 RVA: 0x000F4FD2 File Offset: 0x000F33D2
		public static Vector3 arenaOriginCenter
		{
			get
			{
				return LevelManager._arenaOriginCenter;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x0600286C RID: 10348 RVA: 0x000F4FD9 File Offset: 0x000F33D9
		public static Vector3 arenaTargetCenter
		{
			get
			{
				return LevelManager._arenaTargetCenter;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x0600286D RID: 10349 RVA: 0x000F4FE0 File Offset: 0x000F33E0
		public static float arenaCurrentRadius
		{
			get
			{
				return LevelManager._arenaCurrentRadius;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600286E RID: 10350 RVA: 0x000F4FE7 File Offset: 0x000F33E7
		public static float arenaOriginRadius
		{
			get
			{
				return LevelManager._arenaOriginRadius;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x0600286F RID: 10351 RVA: 0x000F4FEE File Offset: 0x000F33EE
		public static float arenaTargetRadius
		{
			get
			{
				return LevelManager._arenaTargetRadius;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x000F4FF5 File Offset: 0x000F33F5
		public static float arenaCompactorSpeed
		{
			get
			{
				return LevelManager._arenaCompactorSpeed;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002871 RID: 10353 RVA: 0x000F4FFC File Offset: 0x000F33FC
		private static uint minPlayers
		{
			get
			{
				if (Dedicator.isDedicated)
				{
					return Provider.modeConfigData.Events.Arena_Min_Players;
				}
				return 1u;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06002872 RID: 10354 RVA: 0x000F501C File Offset: 0x000F341C
		public static float compactorSpeed
		{
			get
			{
				switch (Level.info.size)
				{
				case ELevelSize.TINY:
					return Provider.modeConfigData.Events.Arena_Compactor_Speed_Tiny;
				case ELevelSize.SMALL:
					return Provider.modeConfigData.Events.Arena_Compactor_Speed_Small;
				case ELevelSize.MEDIUM:
					return Provider.modeConfigData.Events.Arena_Compactor_Speed_Medium;
				case ELevelSize.LARGE:
					return Provider.modeConfigData.Events.Arena_Compactor_Speed_Large;
				case ELevelSize.INSANE:
					return Provider.modeConfigData.Events.Arena_Compactor_Speed_Insane;
				default:
					return 0f;
				}
			}
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000F50A8 File Offset: 0x000F34A8
		public static bool isPlayerInArena(Player player)
		{
			if (LevelManager.arenaState == EArenaState.CLEAR || LevelManager.arenaState == EArenaState.PLAY || LevelManager.arenaState == EArenaState.FINALE || LevelManager.arenaState == EArenaState.RESTART)
			{
				foreach (ArenaPlayer arenaPlayer in LevelManager.arenaPlayers)
				{
					if (arenaPlayer.steamPlayer != null && arenaPlayer.steamPlayer.player == player)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000F5154 File Offset: 0x000F3554
		private void findGroups()
		{
			LevelManager.nonGroups = 0;
			LevelManager.arenaGroups.Clear();
			for (int i = 0; i < Provider.clients.Count; i++)
			{
				SteamPlayer steamPlayer = Provider.clients[i];
				if (steamPlayer != null && !(steamPlayer.player == null) && !steamPlayer.player.life.isDead)
				{
					if (!steamPlayer.player.quests.isMemberOfAGroup)
					{
						LevelManager.nonGroups++;
					}
					else if (!LevelManager.arenaGroups.Contains(steamPlayer.player.quests.groupID))
					{
						LevelManager.arenaGroups.Add(steamPlayer.player.quests.groupID);
					}
				}
			}
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000F5228 File Offset: 0x000F3628
		private void updateGroups(SteamPlayer steamPlayer)
		{
			if (!steamPlayer.player.quests.isMemberOfAGroup)
			{
				LevelManager.nonGroups--;
			}
			else
			{
				for (int i = LevelManager.arenaPlayers.Count - 1; i >= 0; i--)
				{
					ArenaPlayer arenaPlayer = LevelManager.arenaPlayers[i];
					if (arenaPlayer.steamPlayer.player.quests.isMemberOfSameGroupAs(steamPlayer.player))
					{
						return;
					}
				}
				LevelManager.arenaGroups.Remove(steamPlayer.player.quests.groupID);
			}
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x000F52C0 File Offset: 0x000F36C0
		private void arenaLobby()
		{
			this.findGroups();
			if ((long)(LevelManager.nonGroups + LevelManager.arenaGroups.Count) < (long)((ulong)LevelManager.minPlayers))
			{
				if (LevelManager.arenaMessage != EArenaMessage.LOBBY)
				{
					base.channel.send("tellArenaMessage", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						0
					});
				}
				return;
			}
			LevelManager.arenaState = EArenaState.CLEAR;
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x000F5324 File Offset: 0x000F3724
		private void getArenaTarget(Vector3 currentCenter, float currentRadius, out Vector3 targetCenter, out float targetRadius)
		{
			targetCenter = currentCenter;
			targetRadius = currentRadius * Provider.modeConfigData.Events.Arena_Compactor_Shrink_Factor;
			float f = UnityEngine.Random.Range(0f, 6.28318548f);
			float num = Mathf.Cos(f);
			float num2 = Mathf.Sin(f);
			float num3 = UnityEngine.Random.Range(0f, currentRadius - targetRadius);
			targetCenter += new Vector3(num * num3, 0f, num2 * num3);
			if (targetCenter.x - targetRadius < (float)(-Level.size / 2 + Level.border))
			{
				targetRadius = targetCenter.x - (float)(-Level.size / 2 + Level.border);
			}
			if (targetCenter.x + targetRadius > (float)(Level.size / 2 - Level.border))
			{
				targetRadius = (float)(Level.size / 2 - Level.border) - targetCenter.x;
			}
			if (targetCenter.z - targetRadius < (float)(-Level.size / 2 + Level.border))
			{
				targetRadius = targetCenter.z - (float)(-Level.size / 2 + Level.border);
			}
			if (targetCenter.z + targetRadius > (float)(Level.size / 2 - Level.border))
			{
				targetRadius = (float)(Level.size / 2 - Level.border) - targetCenter.z;
			}
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x000F5474 File Offset: 0x000F3874
		private void arenaClear()
		{
			VehicleManager.askVehicleDestroyAll();
			BarricadeManager.askClearAllBarricades();
			StructureManager.askClearAllStructures();
			ItemManager.askClearAllItems();
			EffectManager.askEffectClearAll();
			ObjectManager.askClearAllObjects();
			LevelManager.arenaPlayers.Clear();
			Vector3 vector = Vector3.zero;
			float num = (float)Level.size / 2f;
			if (Level.info.configData.Use_Arena_Compactor)
			{
				if (LevelManager.arenaNodes.Count > 0)
				{
					ArenaNode arenaNode = LevelManager.arenaNodes[UnityEngine.Random.Range(0, LevelManager.arenaNodes.Count)];
					vector = arenaNode.point;
					vector.y = 0f;
					num = arenaNode.radius;
				}
			}
			else
			{
				num = 16384f;
			}
			float compactorSpeed = LevelManager.compactorSpeed;
			Vector3 vector2;
			float num2;
			if (Level.info.configData.Use_Arena_Compactor)
			{
				if (Provider.modeConfigData.Events.Arena_Use_Compactor_Pause)
				{
					this.getArenaTarget(vector, num, out vector2, out num2);
				}
				else
				{
					vector2 = vector;
					num2 = 0.5f;
				}
			}
			else
			{
				vector2 = vector;
				num2 = num;
			}
			base.channel.send("tellArenaOrigin", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				vector,
				num,
				vector,
				num,
				vector2,
				num2,
				compactorSpeed,
				(byte)(Provider.modeConfigData.Events.Arena_Clear_Timer + Provider.modeConfigData.Events.Arena_Compactor_Delay_Timer)
			});
			LevelManager.arenaState = EArenaState.WARMUP;
			base.channel.send("tellLevelTimer", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				(byte)Provider.modeConfigData.Events.Arena_Clear_Timer
			});
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000F5630 File Offset: 0x000F3A30
		private void arenaWarmUp()
		{
			if (LevelManager.arenaMessage != EArenaMessage.WARMUP)
			{
				base.channel.send("tellArenaMessage", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					1
				});
			}
			if (LevelManager.countTimerMessages >= 0)
			{
				return;
			}
			this.findGroups();
			if ((long)(LevelManager.nonGroups + LevelManager.arenaGroups.Count) < (long)((ulong)LevelManager.minPlayers))
			{
				LevelManager.arenaState = EArenaState.LOBBY;
			}
			else
			{
				LevelManager.arenaState = EArenaState.SPAWN;
			}
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000F56AC File Offset: 0x000F3AAC
		private void arenaSpawn()
		{
			for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
			{
				for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
				{
					if (LevelItems.spawns[(int)b, (int)b2].Count > 0)
					{
						for (int i = 0; i < LevelItems.spawns[(int)b, (int)b2].Count; i++)
						{
							ItemSpawnpoint itemSpawnpoint = LevelItems.spawns[(int)b, (int)b2][i];
							ushort item = LevelItems.getItem(itemSpawnpoint);
							if (item != 0)
							{
								Item item2 = new Item(item, EItemOrigin.ADMIN);
								ItemManager.dropItem(item2, itemSpawnpoint.point, false, false, false);
							}
						}
					}
				}
			}
			List<VehicleSpawnpoint> spawns = LevelVehicles.spawns;
			for (int j = 0; j < spawns.Count; j++)
			{
				VehicleSpawnpoint vehicleSpawnpoint = spawns[j];
				ushort vehicle = LevelVehicles.getVehicle(vehicleSpawnpoint);
				if (vehicle != 0)
				{
					Vector3 point = vehicleSpawnpoint.point;
					point.y += 1f;
					VehicleManager.spawnVehicle(vehicle, point, Quaternion.Euler(0f, vehicleSpawnpoint.angle, 0f));
				}
			}
			List<PlayerSpawnpoint> altSpawns = LevelPlayers.getAltSpawns();
			float num = LevelManager.arenaCurrentRadius - SafezoneNode.MIN_SIZE;
			num *= num;
			for (int k = altSpawns.Count - 1; k >= 0; k--)
			{
				PlayerSpawnpoint playerSpawnpoint = altSpawns[k];
				float num2 = Mathf.Pow(playerSpawnpoint.point.x - LevelManager.arenaCurrentCenter.x, 2f) + Mathf.Pow(playerSpawnpoint.point.z - LevelManager.arenaCurrentCenter.z, 2f);
				if (num2 > num)
				{
					altSpawns.RemoveAt(k);
				}
			}
			for (int l = 0; l < Provider.clients.Count; l++)
			{
				if (altSpawns.Count == 0)
				{
					break;
				}
				SteamPlayer steamPlayer = Provider.clients[l];
				if (steamPlayer != null && !(steamPlayer.player == null) && !steamPlayer.player.life.isDead)
				{
					int index = UnityEngine.Random.Range(0, altSpawns.Count);
					PlayerSpawnpoint playerSpawnpoint2 = altSpawns[index];
					altSpawns.RemoveAt(index);
					ArenaPlayer arenaPlayer = new ArenaPlayer(steamPlayer);
					arenaPlayer.steamPlayer.player.life.sendRevive();
					arenaPlayer.steamPlayer.player.sendTeleport(playerSpawnpoint2.point, MeasurementTool.angleToByte(playerSpawnpoint2.angle));
					LevelManager.arenaPlayers.Add(arenaPlayer);
					foreach (ArenaLoadout arenaLoadout in Level.info.configData.Arena_Loadouts)
					{
						for (ushort num3 = 0; num3 < arenaLoadout.Amount; num3 += 1)
						{
							ushort newID = SpawnTableTool.resolve(arenaLoadout.Table_ID);
							arenaPlayer.steamPlayer.player.inventory.forceAddItemAuto(new Item(newID, true), true, false, true, false);
						}
					}
				}
			}
			this.arenaAirdrop();
			LevelManager.arenaState = EArenaState.PLAY;
			base.channel.send("tellLevelNumber", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				(byte)LevelManager.arenaPlayers.Count
			});
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x000F5A3C File Offset: 0x000F3E3C
		private void arenaAirdrop()
		{
			Vector3 arenaTargetCenter = LevelManager.arenaTargetCenter;
			float arenaTargetRadius = LevelManager.arenaTargetRadius;
			float num = arenaTargetRadius * arenaTargetRadius;
			List<AirdropNode> list = new List<AirdropNode>();
			foreach (AirdropNode airdropNode in LevelManager.airdropNodes)
			{
				if ((airdropNode.point - arenaTargetCenter).sqrMagnitude < num)
				{
					list.Add(airdropNode);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			AirdropNode airdropNode2 = list[UnityEngine.Random.Range(0, list.Count)];
			LevelManager.airdrop(airdropNode2.point, airdropNode2.id, Provider.modeConfigData.Events.Airdrop_Speed);
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000F5B10 File Offset: 0x000F3F10
		private void arenaPlay()
		{
			if (LevelManager.arenaMessage != EArenaMessage.PLAY)
			{
				base.channel.send("tellArenaMessage", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					2
				});
			}
			if ((long)(LevelManager.nonGroups + LevelManager.arenaGroups.Count) < (long)((ulong)LevelManager.minPlayers))
			{
				LevelManager.arenaState = EArenaState.FINALE;
				LevelManager.lastFinaleMessage = Time.realtimeSinceStartup;
				if (LevelManager.arenaPlayers.Count > 0)
				{
					ulong[] array = new ulong[LevelManager.arenaPlayers.Count];
					for (int i = 0; i < LevelManager.arenaPlayers.Count; i++)
					{
						array[i] = LevelManager.arenaPlayers[i].steamPlayer.playerID.steamID.m_SteamID;
					}
					LevelManager.arenaMessage = EArenaMessage.LOSE;
					base.channel.send("tellArenaPlayer", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						array,
						5
					});
				}
				else
				{
					base.channel.send("tellArenaMessage", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						6
					});
				}
			}
			else
			{
				for (int j = LevelManager.arenaPlayers.Count - 1; j >= 0; j--)
				{
					ArenaPlayer arenaPlayer = LevelManager.arenaPlayers[j];
					if (arenaPlayer.steamPlayer == null || arenaPlayer.steamPlayer.player == null)
					{
						ulong[] array2 = new ulong[]
						{
							arenaPlayer.steamPlayer.playerID.steamID.m_SteamID
						};
						base.channel.send("tellArenaPlayer", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							array2,
							4
						});
						LevelManager.arenaPlayers.RemoveAt(j);
						this.updateGroups(arenaPlayer.steamPlayer);
						base.channel.send("tellLevelNumber", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							(byte)LevelManager.arenaPlayers.Count
						});
					}
					else
					{
						if (Time.realtimeSinceStartup - arenaPlayer.lastAreaDamage > 1f)
						{
							float num = Mathf.Pow(arenaPlayer.steamPlayer.player.transform.position.x - LevelManager.arenaCurrentCenter.x, 2f) + Mathf.Pow(arenaPlayer.steamPlayer.player.transform.position.z - LevelManager.arenaCurrentCenter.z, 2f);
							if (num > LevelManager.arenaSqrRadius || LevelManager.arenaCurrentRadius < 1f)
							{
								EPlayerKill eplayerKill;
								arenaPlayer.steamPlayer.player.life.askDamage((byte)Provider.modeConfigData.Events.Arena_Compactor_Damage, Vector3.up * 10f, EDeathCause.ARENA, ELimb.SPINE, CSteamID.Nil, out eplayerKill);
								arenaPlayer.lastAreaDamage = Time.realtimeSinceStartup;
							}
						}
						if (arenaPlayer.hasDied)
						{
							ulong[] array3 = new ulong[]
							{
								arenaPlayer.steamPlayer.playerID.steamID.m_SteamID
							};
							base.channel.send("tellArenaPlayer", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								array3,
								3
							});
							LevelManager.arenaPlayers.RemoveAt(j);
							this.updateGroups(arenaPlayer.steamPlayer);
							base.channel.send("tellLevelNumber", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								(byte)LevelManager.arenaPlayers.Count
							});
						}
					}
				}
			}
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000F5E9F File Offset: 0x000F429F
		private void arenaFinale()
		{
			if (Time.realtimeSinceStartup - LevelManager.lastFinaleMessage > Provider.modeConfigData.Events.Arena_Finale_Timer)
			{
				LevelManager.arenaState = EArenaState.RESTART;
			}
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000F5EC8 File Offset: 0x000F42C8
		private void arenaRestart()
		{
			LevelManager.arenaState = EArenaState.INTERMISSION;
			base.channel.send("tellLevelTimer", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				(byte)Provider.modeConfigData.Events.Arena_Restart_Timer
			});
			for (int i = 0; i < LevelManager.arenaPlayers.Count; i++)
			{
				ArenaPlayer arenaPlayer = LevelManager.arenaPlayers[i];
				if (!arenaPlayer.hasDied && arenaPlayer.steamPlayer != null && !(arenaPlayer.steamPlayer.player == null))
				{
					arenaPlayer.steamPlayer.player.sendStat(EPlayerStat.ARENA_WINS);
				}
			}
			for (int j = 0; j < Provider.clients.Count; j++)
			{
				SteamPlayer steamPlayer = Provider.clients[j];
				if (steamPlayer != null && !(steamPlayer.player == null) && !steamPlayer.player.life.isDead && !steamPlayer.player.movement.isSafe)
				{
					EPlayerKill eplayerKill;
					steamPlayer.player.life.askDamage(101, Vector3.up * 101f, EDeathCause.ARENA, ELimb.SPINE, CSteamID.Nil, out eplayerKill);
				}
			}
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x000F6010 File Offset: 0x000F4410
		private void arenaIntermission()
		{
			if (LevelManager.arenaMessage != EArenaMessage.INTERMISSION)
			{
				base.channel.send("tellArenaMessage", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					7
				});
			}
			if (LevelManager.countTimerMessages >= 0)
			{
				return;
			}
			LevelManager.arenaState = EArenaState.LOBBY;
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x000F605C File Offset: 0x000F445C
		private void arenaTick()
		{
			if (Time.realtimeSinceStartup > LevelManager.nextAreaModify)
			{
				LevelManager._arenaCurrentRadius = LevelManager.arenaCurrentRadius - Time.deltaTime * LevelManager.arenaCompactorSpeed;
				if (LevelManager.arenaCurrentRadius < LevelManager.arenaTargetRadius)
				{
					LevelManager._arenaCurrentRadius = LevelManager.arenaTargetRadius;
					if (Provider.isServer && Level.info.configData.Use_Arena_Compactor && Provider.modeConfigData.Events.Arena_Use_Compactor_Pause)
					{
						float compactorSpeed = LevelManager.compactorSpeed;
						Vector3 vector;
						float num;
						this.getArenaTarget(LevelManager.arenaTargetCenter, LevelManager.arenaTargetRadius, out vector, out num);
						base.channel.send("tellArenaOrigin", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							LevelManager.arenaTargetCenter,
							LevelManager.arenaTargetRadius,
							LevelManager.arenaTargetCenter,
							LevelManager.arenaTargetRadius,
							vector,
							num,
							compactorSpeed,
							(byte)Provider.modeConfigData.Events.Arena_Compactor_Pause_Timer
						});
					}
				}
				LevelManager.arenaSqrRadius = LevelManager.arenaCurrentRadius * LevelManager.arenaCurrentRadius;
				float t = Mathf.InverseLerp(LevelManager.arenaTargetRadius, LevelManager.arenaOriginRadius, LevelManager.arenaCurrentRadius);
				LevelManager._arenaCurrentCenter = Vector3.Lerp(LevelManager.arenaTargetCenter, LevelManager.arenaOriginCenter, t);
			}
			if (!Dedicator.isDedicated)
			{
				if (LevelManager.arenaCurrentArea != null)
				{
					LevelManager.arenaCurrentArea.position = LevelManager.arenaCurrentCenter;
					LevelManager.arenaCurrentArea.localScale = new Vector3(LevelManager.arenaCurrentRadius, LevelManager.arenaCurrentRadius, Level.HEIGHT);
				}
				if (LevelManager.arenaTargetArea != null)
				{
					LevelManager.arenaTargetArea.position = LevelManager.arenaTargetCenter;
					LevelManager.arenaTargetArea.localScale = new Vector3(LevelManager.arenaTargetRadius, LevelManager.arenaTargetRadius, Level.HEIGHT);
				}
			}
			if (LevelManager.countTimerMessages >= 0 && Time.realtimeSinceStartup - LevelManager.lastTimerMessage > 1f)
			{
				if (LevelManager.onLevelNumberUpdated != null)
				{
					LevelManager.onLevelNumberUpdated(LevelManager.countTimerMessages);
				}
				LevelManager.lastTimerMessage = Time.realtimeSinceStartup;
				LevelManager.countTimerMessages--;
				if (LevelManager.arenaMessage == EArenaMessage.WARMUP && !Dedicator.isDedicated && MainCamera.instance != null && OptionsSettings.timer)
				{
					MainCamera.instance.GetComponent<AudioSource>().PlayOneShot(LevelManager.timer, 1f);
				}
			}
			if (Provider.isServer)
			{
				switch (LevelManager.arenaState)
				{
				case EArenaState.LOBBY:
					this.arenaLobby();
					break;
				case EArenaState.CLEAR:
					this.arenaClear();
					break;
				case EArenaState.WARMUP:
					this.arenaWarmUp();
					break;
				case EArenaState.SPAWN:
					this.arenaSpawn();
					break;
				case EArenaState.PLAY:
					this.arenaPlay();
					break;
				case EArenaState.FINALE:
					this.arenaFinale();
					break;
				case EArenaState.RESTART:
					this.arenaRestart();
					break;
				case EArenaState.INTERMISSION:
					this.arenaIntermission();
					break;
				}
			}
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000F6360 File Offset: 0x000F4760
		private void arenaInit()
		{
			LevelManager._arenaCurrentCenter = Vector3.zero;
			LevelManager._arenaTargetCenter = Vector3.zero;
			LevelManager._arenaCurrentRadius = 16384f;
			LevelManager._arenaTargetRadius = 16384f;
			LevelManager._arenaCompactorSpeed = 0f;
			if (!Dedicator.isDedicated && !Level.isEditor)
			{
				LevelManager.arenaCurrentArea = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Level/Arena_Area_Current"))).transform;
				LevelManager.arenaCurrentArea.name = "Arena_Area_Current";
				LevelManager.arenaCurrentArea.localRotation = Quaternion.Euler(-90f, 0f, 0f);
				LevelManager.arenaCurrentArea.parent = Level.clips;
				LevelManager.arenaTargetArea = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Level/Arena_Area_Target"))).transform;
				LevelManager.arenaTargetArea.name = "Arena_Area_Target";
				LevelManager.arenaTargetArea.localRotation = Quaternion.Euler(-90f, 0f, 0f);
				LevelManager.arenaTargetArea.parent = Level.clips;
			}
			if (Provider.isServer)
			{
				LevelManager.arenaState = EArenaState.LOBBY;
				LevelManager.arenaGroups = new List<CSteamID>();
				LevelManager.arenaPlayers = new List<ArenaPlayer>();
				LevelManager.arenaNodes = new List<ArenaNode>();
				for (int i = 0; i < LevelNodes.nodes.Count; i++)
				{
					Node node = LevelNodes.nodes[i];
					if (node.type == ENodeType.ARENA)
					{
						LevelManager.arenaNodes.Add((ArenaNode)node);
					}
				}
			}
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000F64D8 File Offset: 0x000F48D8
		[SteamCall]
		public void tellArenaOrigin(CSteamID steamID, Vector3 newArenaCurrentCenter, float newArenaCurrentRadius, Vector3 newArenaOriginCenter, float newArenaOriginRadius, Vector3 newArenaTargetCenter, float newArenaTargetRadius, float newArenaCompactorSpeed, byte delay)
		{
			if (base.channel.checkServer(steamID))
			{
				LevelManager._arenaCurrentCenter = newArenaCurrentCenter;
				LevelManager._arenaCurrentRadius = newArenaCurrentRadius;
				LevelManager.arenaSqrRadius = LevelManager.arenaCurrentRadius * LevelManager.arenaCurrentRadius;
				LevelManager._arenaOriginCenter = newArenaOriginCenter;
				LevelManager._arenaOriginRadius = newArenaOriginRadius;
				LevelManager._arenaTargetCenter = newArenaTargetCenter;
				LevelManager._arenaTargetRadius = newArenaTargetRadius;
				LevelManager._arenaCompactorSpeed = newArenaCompactorSpeed;
				if (delay == 0)
				{
					LevelManager.nextAreaModify = 0f;
				}
				else
				{
					LevelManager.nextAreaModify = Time.realtimeSinceStartup + (float)delay;
				}
			}
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000F6559 File Offset: 0x000F4959
		[SteamCall]
		public void tellArenaMessage(CSteamID steamID, byte newArenaMessage)
		{
			if (base.channel.checkServer(steamID))
			{
				LevelManager.arenaMessage = (EArenaMessage)newArenaMessage;
				if (LevelManager.onArenaMessageUpdated != null)
				{
					LevelManager.onArenaMessageUpdated(LevelManager.arenaMessage);
				}
			}
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000F658B File Offset: 0x000F498B
		[SteamCall]
		public void tellArenaPlayer(CSteamID steamID, ulong[] newPlayerIDs, byte newArenaMessage)
		{
			if (base.channel.checkServer(steamID) && LevelManager.onArenaPlayerUpdated != null)
			{
				LevelManager.onArenaPlayerUpdated(newPlayerIDs, (EArenaMessage)newArenaMessage);
			}
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x000F65B4 File Offset: 0x000F49B4
		[SteamCall]
		public void tellLevelNumber(CSteamID steamID, byte newLevelNumber)
		{
			if (base.channel.checkServer(steamID))
			{
				LevelManager.countTimerMessages = -1;
				if (LevelManager.onLevelNumberUpdated != null)
				{
					LevelManager.onLevelNumberUpdated((int)newLevelNumber);
				}
			}
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x000F65E2 File Offset: 0x000F49E2
		[SteamCall]
		public void tellLevelTimer(CSteamID steamID, byte newTimerCount)
		{
			if (base.channel.checkServer(steamID))
			{
				LevelManager.countTimerMessages = (int)newTimerCount;
			}
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x000F65FC File Offset: 0x000F49FC
		[SteamCall]
		public void askArenaState(CSteamID steamID)
		{
			base.channel.send("tellArenaOrigin", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				LevelManager.arenaCurrentCenter,
				LevelManager.arenaCurrentRadius,
				LevelManager.arenaOriginCenter,
				LevelManager.arenaOriginRadius,
				LevelManager.arenaTargetCenter,
				LevelManager.arenaTargetRadius,
				LevelManager.arenaCompactorSpeed,
				0
			});
			base.channel.send("tellArenaMessage", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				(byte)LevelManager.arenaMessage
			});
			if (LevelManager.countTimerMessages > 0)
			{
				base.channel.send("tellLevelTimer", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					(byte)LevelManager.countTimerMessages
				});
			}
			else
			{
				base.channel.send("tellLevelNumber", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					(byte)LevelManager.arenaPlayers.Count
				});
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002888 RID: 10376 RVA: 0x000F6710 File Offset: 0x000F4B10
		public static bool hasAirdrop
		{
			get
			{
				return LevelManager._hasAirdrop;
			}
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000F6718 File Offset: 0x000F4B18
		public static void airdrop(Vector3 point, ushort id, float speed)
		{
			if (id == 0)
			{
				return;
			}
			Vector3 vector = Vector3.zero;
			if (UnityEngine.Random.value < 0.5f)
			{
				vector.x = (float)(Level.size / 2) * -Mathf.Sign(point.x);
				vector.z = (float)UnityEngine.Random.Range(0, (int)(Level.size / 2)) * -Mathf.Sign(point.z);
			}
			else
			{
				vector.x = (float)UnityEngine.Random.Range(0, (int)(Level.size / 2)) * -Mathf.Sign(point.x);
				vector.z = (float)(Level.size / 2) * -Mathf.Sign(point.z);
			}
			point.y = 0f;
			Vector3 normalized = (point - vector).normalized;
			vector += normalized * -2048f;
			float num = (point - vector).magnitude / speed;
			vector.y = 1024f;
			float airdrop_Force = Provider.modeConfigData.Events.Airdrop_Force;
			LevelManager.manager.airdropSpawn(id, vector, normalized, speed, airdrop_Force, num);
			LevelManager.manager.channel.send("tellAirdropState", ESteamCall.OTHERS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				vector,
				normalized,
				speed,
				airdrop_Force,
				num
			});
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x000F6880 File Offset: 0x000F4C80
		private void airdropTick()
		{
			for (int i = LevelManager.airdrops.Count - 1; i >= 0; i--)
			{
				AirdropInfo airdropInfo = LevelManager.airdrops[i];
				airdropInfo.state += airdropInfo.direction * airdropInfo.speed * Time.deltaTime;
				airdropInfo.delay -= Time.deltaTime;
				if (airdropInfo.model != null)
				{
					airdropInfo.model.position = airdropInfo.state;
				}
				if (airdropInfo.dropped)
				{
					if (Mathf.Abs(airdropInfo.state.x) > (float)(Level.size / 2 + 2048) || Mathf.Abs(airdropInfo.state.z) > (float)(Level.size / 2 + 2048))
					{
						if (airdropInfo.model != null)
						{
							UnityEngine.Object.Destroy(airdropInfo.model.gameObject);
						}
						LevelManager.airdrops.RemoveAt(i);
					}
				}
				else if (airdropInfo.delay <= 0f)
				{
					airdropInfo.dropped = true;
					Transform transform;
					if (Dedicator.isDedicated)
					{
						transform = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Level/Carepackage_Server"))).transform;
					}
					else
					{
						transform = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Level/Carepackage_Client"))).transform;
					}
					transform.name = "Carepackage";
					transform.parent = Level.effects;
					transform.position = airdropInfo.state;
					transform.rotation = Quaternion.identity;
					if (Provider.isServer)
					{
						transform.GetComponent<Carepackage>().id = airdropInfo.id;
					}
					transform.GetComponent<ConstantForce>().force = new Vector3(0f, airdropInfo.force, 0f);
					if (Dedicator.isDedicated)
					{
						LevelManager.airdrops.RemoveAt(i);
					}
				}
			}
			if (Provider.isServer && LevelManager.levelType == ELevelType.SURVIVAL && LevelManager.airdropNodes.Count > 0)
			{
				if (!LevelManager.hasAirdrop)
				{
					LevelManager.airdropFrequency = (uint)(UnityEngine.Random.Range(Provider.modeConfigData.Events.Airdrop_Frequency_Min, Provider.modeConfigData.Events.Airdrop_Frequency_Max) * LightingManager.cycle);
					LevelManager._hasAirdrop = true;
					LevelManager.lastAirdrop = Time.realtimeSinceStartup;
				}
				if (LevelManager.airdropFrequency > 0u)
				{
					if (Time.realtimeSinceStartup - LevelManager.lastAirdrop > 1f)
					{
						LevelManager.airdropFrequency -= 1u;
						LevelManager.lastAirdrop = Time.realtimeSinceStartup;
					}
				}
				else
				{
					AirdropNode airdropNode = LevelManager.airdropNodes[UnityEngine.Random.Range(0, LevelManager.airdropNodes.Count)];
					LevelManager.airdrop(airdropNode.point, airdropNode.id, Provider.modeConfigData.Events.Airdrop_Speed);
					LevelManager._hasAirdrop = false;
				}
			}
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x000F6B5C File Offset: 0x000F4F5C
		private void airdropInit()
		{
			LevelManager.lastAirdrop = Time.realtimeSinceStartup;
			LevelManager.airdrops = new List<AirdropInfo>();
			if (Provider.isServer)
			{
				LevelManager.airdropNodes = new List<AirdropNode>();
				for (int i = 0; i < LevelNodes.nodes.Count; i++)
				{
					Node node = LevelNodes.nodes[i];
					if (node.type == ENodeType.AIRDROP)
					{
						AirdropNode airdropNode = (AirdropNode)node;
						if (airdropNode.id != 0)
						{
							LevelManager.airdropNodes.Add(airdropNode);
						}
					}
				}
				LevelManager.load();
			}
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000F6BE8 File Offset: 0x000F4FE8
		private void airdropSpawn(ushort id, Vector3 state, Vector3 direction, float speed, float force, float delay)
		{
			AirdropInfo airdropInfo = new AirdropInfo();
			airdropInfo.id = id;
			airdropInfo.state = state;
			airdropInfo.direction = direction;
			airdropInfo.speed = speed;
			airdropInfo.force = force;
			airdropInfo.delay = delay;
			airdropInfo.dropped = false;
			if (!Dedicator.isDedicated)
			{
				Transform transform = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Level/Dropship"))).transform;
				transform.name = "Dropship";
				transform.parent = Level.effects;
				transform.position = state;
				transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(-90f, 180f, 0f);
				airdropInfo.model = transform;
			}
			LevelManager.airdrops.Add(airdropInfo);
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000F6CA7 File Offset: 0x000F50A7
		[SteamCall]
		public void tellAirdropState(CSteamID steamID, Vector3 state, Vector3 direction, float speed, float force, float delay)
		{
			if (base.channel.checkServer(steamID))
			{
				this.airdropSpawn(0, state, direction, speed, force, delay);
			}
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000F6CCC File Offset: 0x000F50CC
		[SteamCall]
		public void askAirdropState(CSteamID steamID)
		{
			for (int i = 0; i < LevelManager.airdrops.Count; i++)
			{
				AirdropInfo airdropInfo = LevelManager.airdrops[i];
				base.channel.send("tellAirdropState", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					airdropInfo.state,
					airdropInfo.direction,
					airdropInfo.speed,
					airdropInfo.force,
					airdropInfo.delay
				});
			}
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000F6D60 File Offset: 0x000F5160
		private void onClientConnected()
		{
			if (Level.info.type == ELevelType.ARENA)
			{
				base.channel.send("askArenaState", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
			}
			else if (Level.info.type == ELevelType.SURVIVAL)
			{
				base.channel.send("askAirdropState", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
			}
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x000F6DC4 File Offset: 0x000F51C4
		private void onLevelLoaded(int level)
		{
			LevelManager.isInit = false;
			if (level > Level.SETUP && Level.info != null)
			{
				LevelManager.isInit = true;
				LevelManager._levelType = Level.info.type;
				if (LevelManager.levelType == ELevelType.ARENA)
				{
					this.arenaInit();
				}
				if (LevelManager.levelType != ELevelType.HORDE)
				{
					this.airdropInit();
				}
			}
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x000F6E23 File Offset: 0x000F5223
		private void Update()
		{
			if (!LevelManager.isInit)
			{
				return;
			}
			if (LevelManager.levelType == ELevelType.ARENA)
			{
				this.arenaTick();
			}
			if (LevelManager.levelType != ELevelType.HORDE)
			{
				this.airdropTick();
			}
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000F6E54 File Offset: 0x000F5254
		private void Start()
		{
			LevelManager.manager = this;
			if (!Dedicator.isDedicated)
			{
				LevelManager.timer = (AudioClip)Resources.Load("Sounds/General/Timer");
			}
			Level.onLevelLoaded = (LevelLoaded)Delegate.Combine(Level.onLevelLoaded, new LevelLoaded(this.onLevelLoaded));
			Provider.onClientConnected = (Provider.ClientConnected)Delegate.Combine(Provider.onClientConnected, new Provider.ClientConnected(this.onClientConnected));
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000F6EC8 File Offset: 0x000F52C8
		public static void load()
		{
			if (LevelSavedata.fileExists("/Events.dat"))
			{
				River river = LevelSavedata.openRiver("/Events.dat", true);
				byte b = river.readByte();
				if (b > 0)
				{
					LevelManager.airdropFrequency = river.readUInt32();
					LevelManager._hasAirdrop = river.readBoolean();
					return;
				}
			}
			LevelManager._hasAirdrop = false;
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000F6F1C File Offset: 0x000F531C
		public static void save()
		{
			River river = LevelSavedata.openRiver("/Events.dat", false);
			river.writeByte(LevelManager.SAVEDATA_VERSION);
			river.writeUInt32(LevelManager.airdropFrequency);
			river.writeBoolean(LevelManager.hasAirdrop);
		}

		// Token: 0x04001946 RID: 6470
		public static readonly byte SAVEDATA_VERSION = 1;

		// Token: 0x04001947 RID: 6471
		private static LevelManager manager;

		// Token: 0x04001948 RID: 6472
		private static bool isInit;

		// Token: 0x04001949 RID: 6473
		private static ELevelType _levelType;

		// Token: 0x0400194A RID: 6474
		private static AudioClip timer;

		// Token: 0x0400194B RID: 6475
		private static float lastFinaleMessage;

		// Token: 0x0400194C RID: 6476
		private static float lastTimerMessage;

		// Token: 0x0400194D RID: 6477
		private static float nextAreaModify;

		// Token: 0x0400194E RID: 6478
		private static int countTimerMessages;

		// Token: 0x0400194F RID: 6479
		public static EArenaState arenaState;

		// Token: 0x04001950 RID: 6480
		public static EArenaMessage arenaMessage;

		// Token: 0x04001951 RID: 6481
		private static int nonGroups;

		// Token: 0x04001952 RID: 6482
		public static List<CSteamID> arenaGroups;

		// Token: 0x04001953 RID: 6483
		public static List<ArenaPlayer> arenaPlayers;

		// Token: 0x04001954 RID: 6484
		private static List<ArenaNode> arenaNodes;

		// Token: 0x04001955 RID: 6485
		private static Vector3 _arenaCurrentCenter;

		// Token: 0x04001956 RID: 6486
		private static Vector3 _arenaOriginCenter;

		// Token: 0x04001957 RID: 6487
		private static Vector3 _arenaTargetCenter;

		// Token: 0x04001958 RID: 6488
		private static float _arenaCurrentRadius;

		// Token: 0x04001959 RID: 6489
		private static float _arenaOriginRadius;

		// Token: 0x0400195A RID: 6490
		private static float _arenaTargetRadius;

		// Token: 0x0400195B RID: 6491
		private static float _arenaCompactorSpeed;

		// Token: 0x0400195C RID: 6492
		private static float arenaSqrRadius;

		// Token: 0x0400195D RID: 6493
		private static Transform arenaCurrentArea;

		// Token: 0x0400195E RID: 6494
		private static Transform arenaTargetArea;

		// Token: 0x0400195F RID: 6495
		public static ArenaMessageUpdated onArenaMessageUpdated;

		// Token: 0x04001960 RID: 6496
		public static ArenaPlayerUpdated onArenaPlayerUpdated;

		// Token: 0x04001961 RID: 6497
		public static LevelNumberUpdated onLevelNumberUpdated;

		// Token: 0x04001962 RID: 6498
		private static List<AirdropNode> airdropNodes;

		// Token: 0x04001963 RID: 6499
		private static List<AirdropInfo> airdrops;

		// Token: 0x04001964 RID: 6500
		public static uint airdropFrequency;

		// Token: 0x04001965 RID: 6501
		private static bool _hasAirdrop;

		// Token: 0x04001966 RID: 6502
		private static float lastAirdrop;
	}
}
