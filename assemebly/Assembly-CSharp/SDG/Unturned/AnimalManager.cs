using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000584 RID: 1412
	public class AnimalManager : SteamCaller
	{
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x060026F0 RID: 9968 RVA: 0x000E7566 File Offset: 0x000E5966
		public static List<Animal> animals
		{
			get
			{
				return AnimalManager._animals;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x000E756D File Offset: 0x000E596D
		public static List<PackInfo> packs
		{
			get
			{
				return AnimalManager._packs;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060026F2 RID: 9970 RVA: 0x000E7574 File Offset: 0x000E5974
		public static List<Animal> tickingAnimals
		{
			get
			{
				return AnimalManager._tickingAnimals;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x000E757C File Offset: 0x000E597C
		public static uint maxInstances
		{
			get
			{
				switch (Level.info.size)
				{
				case ELevelSize.TINY:
					return Provider.modeConfigData.Animals.Max_Instances_Tiny;
				case ELevelSize.SMALL:
					return Provider.modeConfigData.Animals.Max_Instances_Small;
				case ELevelSize.MEDIUM:
					return Provider.modeConfigData.Animals.Max_Instances_Medium;
				case ELevelSize.LARGE:
					return Provider.modeConfigData.Animals.Max_Instances_Large;
				case ELevelSize.INSANE:
					return Provider.modeConfigData.Animals.Max_Instances_Insane;
				default:
					return 0u;
				}
			}
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x000E7604 File Offset: 0x000E5A04
		public static bool giveAnimal(Player player, ushort id)
		{
			AnimalAsset animalAsset = (AnimalAsset)Assets.find(EAssetType.ANIMAL, id);
			if (animalAsset != null)
			{
				Vector3 vector = player.transform.position + player.transform.forward * 6f;
				RaycastHit raycastHit;
				Physics.Raycast(vector + Vector3.up * 16f, Vector3.down, out raycastHit, 32f, RayMasks.BLOCK_VEHICLE);
				if (raycastHit.collider != null)
				{
					vector = raycastHit.point;
				}
				AnimalManager.spawnAnimal(id, vector, player.transform.rotation);
				return true;
			}
			return false;
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x000E76A8 File Offset: 0x000E5AA8
		public static void spawnAnimal(ushort id, Vector3 point, Quaternion angle)
		{
			AnimalAsset animalAsset = (AnimalAsset)Assets.find(EAssetType.ANIMAL, id);
			if (animalAsset != null)
			{
				Animal animal = AnimalManager.manager.addAnimal(id, point, angle.eulerAngles.y, false);
				AnimalSpawnpoint item = new AnimalSpawnpoint(0, point);
				PackInfo packInfo = new PackInfo();
				animal.pack = packInfo;
				packInfo.animals.Add(animal);
				packInfo.spawns.Add(item);
				AnimalManager.packs.Add(packInfo);
				AnimalManager.manager.channel.openWrite();
				AnimalManager.manager.sendAnimal(animal);
				AnimalManager.manager.channel.closeWrite("tellAnimal", ESteamCall.OTHERS, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
			}
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x000E7754 File Offset: 0x000E5B54
		public static void getAnimalsInRadius(Vector3 center, float sqrRadius, List<Animal> result)
		{
			if (AnimalManager.animals == null)
			{
				return;
			}
			for (int i = 0; i < AnimalManager.animals.Count; i++)
			{
				Animal animal = AnimalManager.animals[i];
				if ((animal.transform.position - center).sqrMagnitude < sqrRadius)
				{
					result.Add(animal);
				}
			}
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x000E77B9 File Offset: 0x000E5BB9
		[SteamCall]
		public void tellAnimalAlive(CSteamID steamID, ushort index, Vector3 newPosition, byte newAngle)
		{
			if (base.channel.checkServer(steamID))
			{
				if ((int)index >= AnimalManager.animals.Count)
				{
					return;
				}
				AnimalManager.animals[(int)index].tellAlive(newPosition, newAngle);
			}
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x000E77F0 File Offset: 0x000E5BF0
		[SteamCall]
		public void tellAnimalDead(CSteamID steamID, ushort index, Vector3 newRagdoll)
		{
			if (base.channel.checkServer(steamID))
			{
				if ((int)index >= AnimalManager.animals.Count)
				{
					return;
				}
				AnimalManager.animals[(int)index].tellDead(newRagdoll);
			}
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x000E7828 File Offset: 0x000E5C28
		[SteamCall]
		public void tellAnimalStates(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID))
			{
				uint num = (uint)base.channel.read(Types.UINT32_TYPE);
				if (num <= this.seq)
				{
					return;
				}
				this.seq = num;
				base.channel.useCompression = true;
				ushort num2 = (ushort)base.channel.read(Types.UINT16_TYPE);
				for (int i = 0; i < (int)num2; i++)
				{
					object[] array = base.channel.read(Types.UINT16_TYPE, Types.VECTOR3_TYPE, Types.BYTE_TYPE);
					if ((int)((ushort)array[0]) >= AnimalManager.animals.Count)
					{
						break;
					}
					AnimalManager.animals[(int)((ushort)array[0])].tellState((Vector3)array[1], (byte)array[2]);
				}
				base.channel.useCompression = false;
			}
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x000E790E File Offset: 0x000E5D0E
		[SteamCall]
		public void askAnimalStartle(CSteamID steamID, ushort index)
		{
			if (base.channel.checkServer(steamID))
			{
				if ((int)index >= AnimalManager.animals.Count)
				{
					return;
				}
				AnimalManager.animals[(int)index].askStartle();
			}
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x000E7942 File Offset: 0x000E5D42
		[SteamCall]
		public void askAnimalAttack(CSteamID steamID, ushort index)
		{
			if (base.channel.checkServer(steamID))
			{
				if ((int)index >= AnimalManager.animals.Count)
				{
					return;
				}
				AnimalManager.animals[(int)index].askAttack();
			}
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x000E7976 File Offset: 0x000E5D76
		[SteamCall]
		public void askAnimalPanic(CSteamID steamID, ushort index)
		{
			if (base.channel.checkServer(steamID))
			{
				if ((int)index >= AnimalManager.animals.Count)
				{
					return;
				}
				AnimalManager.animals[(int)index].askPanic();
			}
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x000E79AC File Offset: 0x000E5DAC
		[SteamCall]
		public void tellAnimals(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID))
			{
				ushort num = (ushort)base.channel.read(Types.UINT16_TYPE);
				for (int i = 0; i < (int)num; i++)
				{
					object[] array = base.channel.read(Types.UINT16_TYPE, Types.VECTOR3_TYPE, Types.BYTE_TYPE, Types.BOOLEAN_TYPE);
					this.addAnimal((ushort)array[0], (Vector3)array[1], (float)((byte)array[2] * 2), (bool)array[3]);
				}
			}
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x000E7A3C File Offset: 0x000E5E3C
		[SteamCall]
		public void tellAnimal(CSteamID steamID)
		{
			if (!base.channel.checkServer(steamID))
			{
				return;
			}
			object[] array = base.channel.read(Types.UINT16_TYPE, Types.VECTOR3_TYPE, Types.BYTE_TYPE, Types.BOOLEAN_TYPE);
			this.addAnimal((ushort)array[0], (Vector3)array[1], (float)((byte)array[2] * 2), (bool)array[3]);
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x000E7AA8 File Offset: 0x000E5EA8
		[SteamCall]
		public void askAnimals(CSteamID steamID)
		{
			base.channel.openWrite();
			base.channel.write((ushort)AnimalManager.animals.Count);
			for (int i = 0; i < AnimalManager.animals.Count; i++)
			{
				Animal animal = AnimalManager.animals[i];
				this.sendAnimal(animal);
			}
			base.channel.closeWrite("tellAnimals", steamID, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x000E7B1C File Offset: 0x000E5F1C
		public void sendAnimal(Animal animal)
		{
			base.channel.write(animal.id, animal.transform.position, MeasurementTool.angleToByte(animal.transform.rotation.eulerAngles.y), animal.isDead);
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x000E7B7F File Offset: 0x000E5F7F
		public static void sendAnimalAlive(Animal animal, Vector3 newPosition, byte newAngle)
		{
			AnimalManager.manager.channel.send("tellAnimalAlive", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				animal.index,
				newPosition,
				newAngle
			});
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x000E7BBE File Offset: 0x000E5FBE
		public static void sendAnimalDead(Animal animal, Vector3 newRagdoll)
		{
			AnimalManager.manager.channel.send("tellAnimalDead", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				animal.index,
				newRagdoll
			});
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x000E7BF4 File Offset: 0x000E5FF4
		public static void sendAnimalStartle(Animal animal)
		{
			AnimalManager.manager.channel.send("askAnimalStartle", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				animal.index
			});
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x000E7C21 File Offset: 0x000E6021
		public static void sendAnimalAttack(Animal animal)
		{
			AnimalManager.manager.channel.send("askAnimalAttack", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				animal.index
			});
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x000E7C4E File Offset: 0x000E604E
		public static void sendAnimalPanic(Animal animal)
		{
			AnimalManager.manager.channel.send("askAnimalPanic", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				animal.index
			});
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x000E7C7C File Offset: 0x000E607C
		public static void dropLoot(Animal animal)
		{
			if (animal.asset.rewardID != 0)
			{
				int num = UnityEngine.Random.Range((int)animal.asset.rewardMin, (int)(animal.asset.rewardMax + 1));
				for (int i = 0; i < num; i++)
				{
					ushort num2 = SpawnTableTool.resolve(animal.asset.rewardID);
					if (num2 != 0)
					{
						ItemManager.dropItem(new Item(num2, EItemOrigin.NATURE), animal.transform.position, false, Dedicator.isDedicated, true);
					}
				}
			}
			else
			{
				if (animal.asset.meat != 0)
				{
					int num3 = UnityEngine.Random.Range(2, 5);
					for (int j = 0; j < num3; j++)
					{
						ItemManager.dropItem(new Item(animal.asset.meat, EItemOrigin.NATURE), animal.transform.position, false, Dedicator.isDedicated, true);
					}
				}
				if (animal.asset.pelt != 0)
				{
					int num4 = UnityEngine.Random.Range(2, 5);
					for (int k = 0; k < num4; k++)
					{
						ItemManager.dropItem(new Item(animal.asset.pelt, EItemOrigin.NATURE), animal.transform.position, false, Dedicator.isDedicated, true);
					}
				}
			}
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x000E7DB4 File Offset: 0x000E61B4
		private Animal addAnimal(ushort id, Vector3 point, float angle, bool isDead)
		{
			AnimalAsset animalAsset = (AnimalAsset)Assets.find(EAssetType.ANIMAL, id);
			if (animalAsset != null)
			{
				Transform transform;
				if (Dedicator.isDedicated)
				{
					transform = UnityEngine.Object.Instantiate<GameObject>(animalAsset.dedicated).transform;
				}
				else if (Provider.isServer)
				{
					transform = UnityEngine.Object.Instantiate<GameObject>(animalAsset.server).transform;
				}
				else
				{
					transform = UnityEngine.Object.Instantiate<GameObject>(animalAsset.client).transform;
				}
				transform.name = id.ToString();
				transform.parent = LevelAnimals.models;
				transform.position = point;
				transform.rotation = Quaternion.Euler(0f, angle, 0f);
				Animal animal = transform.gameObject.AddComponent<Animal>();
				animal.index = (ushort)AnimalManager.animals.Count;
				animal.id = id;
				animal.isDead = isDead;
				AnimalManager.animals.Add(animal);
				return animal;
			}
			return null;
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x000E7E9A File Offset: 0x000E629A
		public static Animal getAnimal(ushort index)
		{
			if ((int)index >= AnimalManager.animals.Count)
			{
				return null;
			}
			return AnimalManager.animals[(int)index];
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000E7EBC File Offset: 0x000E62BC
		private void respawnAnimals()
		{
			if ((int)AnimalManager.respawnPackIndex >= AnimalManager.packs.Count)
			{
				AnimalManager.respawnPackIndex = (ushort)(AnimalManager.packs.Count - 1);
			}
			PackInfo packInfo = AnimalManager.packs[(int)AnimalManager.respawnPackIndex];
			AnimalManager.respawnPackIndex += 1;
			if ((int)AnimalManager.respawnPackIndex >= AnimalManager.packs.Count)
			{
				AnimalManager.respawnPackIndex = 0;
			}
			if (packInfo == null)
			{
				return;
			}
			for (int i = 0; i < packInfo.animals.Count; i++)
			{
				Animal animal = packInfo.animals[i];
				if (animal == null || !animal.isDead || Time.realtimeSinceStartup - animal.lastDead < Provider.modeConfigData.Animals.Respawn_Time)
				{
					return;
				}
			}
			List<AnimalSpawnpoint> list = new List<AnimalSpawnpoint>();
			for (int j = 0; j < packInfo.spawns.Count; j++)
			{
				list.Add(packInfo.spawns[j]);
			}
			for (int k = 0; k < packInfo.animals.Count; k++)
			{
				Animal animal2 = packInfo.animals[k];
				if (!(animal2 == null))
				{
					int index = UnityEngine.Random.Range(0, list.Count);
					AnimalSpawnpoint animalSpawnpoint = list[index];
					list.RemoveAt(index);
					Vector3 point = animalSpawnpoint.point;
					point.y += 0.1f;
					animal2.sendRevive(point, UnityEngine.Random.Range(0f, 360f));
				}
			}
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x000E805C File Offset: 0x000E645C
		private void onLevelLoaded(int level)
		{
			if (level > Level.SETUP)
			{
				this.seq = 0u;
				AnimalManager._animals = new List<Animal>();
				AnimalManager._packs = null;
				AnimalManager.updates = 0;
				AnimalManager.tickIndex = 0;
				AnimalManager._tickingAnimals = new List<Animal>();
				if (Provider.isServer)
				{
					AnimalManager._packs = new List<PackInfo>();
					if (LevelAnimals.spawns.Count > 0)
					{
						for (int i = 0; i < LevelAnimals.spawns.Count; i++)
						{
							AnimalSpawnpoint animalSpawnpoint = LevelAnimals.spawns[i];
							int num = -1;
							for (int j = AnimalManager.packs.Count - 1; j >= 0; j--)
							{
								List<AnimalSpawnpoint> spawns = AnimalManager.packs[j].spawns;
								for (int k = 0; k < spawns.Count; k++)
								{
									AnimalSpawnpoint animalSpawnpoint2 = spawns[k];
									if ((animalSpawnpoint2.point - animalSpawnpoint.point).sqrMagnitude < 256f)
									{
										if (num == -1)
										{
											spawns.Add(animalSpawnpoint);
										}
										else
										{
											List<AnimalSpawnpoint> spawns2 = AnimalManager.packs[num].spawns;
											for (int l = 0; l < spawns2.Count; l++)
											{
												spawns.Add(spawns2[l]);
											}
											AnimalManager.packs.RemoveAtFast(num);
										}
										num = j;
										break;
									}
								}
							}
							if (num == -1)
							{
								PackInfo packInfo = new PackInfo();
								packInfo.spawns.Add(animalSpawnpoint);
								AnimalManager.packs.Add(packInfo);
							}
						}
						List<AnimalManager.ValidAnimalSpawnsInfo> list = new List<AnimalManager.ValidAnimalSpawnsInfo>();
						for (int m = 0; m < AnimalManager.packs.Count; m++)
						{
							PackInfo packInfo2 = AnimalManager.packs[m];
							List<AnimalSpawnpoint> list2 = new List<AnimalSpawnpoint>();
							for (int n = 0; n < packInfo2.spawns.Count; n++)
							{
								list2.Add(packInfo2.spawns[n]);
							}
							list.Add(new AnimalManager.ValidAnimalSpawnsInfo
							{
								spawns = list2,
								pack = packInfo2
							});
						}
						while ((long)AnimalManager.animals.Count < (long)((ulong)AnimalManager.maxInstances) && list.Count > 0)
						{
							int index = UnityEngine.Random.Range(0, list.Count);
							AnimalManager.ValidAnimalSpawnsInfo validAnimalSpawnsInfo = list[index];
							int index2 = UnityEngine.Random.Range(0, validAnimalSpawnsInfo.spawns.Count);
							AnimalSpawnpoint animalSpawnpoint3 = validAnimalSpawnsInfo.spawns[index2];
							validAnimalSpawnsInfo.spawns.RemoveAt(index2);
							if (validAnimalSpawnsInfo.spawns.Count == 0)
							{
								list.RemoveAt(index);
							}
							Vector3 point = animalSpawnpoint3.point;
							point.y += 0.1f;
							ushort id;
							if (validAnimalSpawnsInfo.pack.animals.Count > 0)
							{
								id = validAnimalSpawnsInfo.pack.animals[0].id;
							}
							else
							{
								id = LevelAnimals.getAnimal(animalSpawnpoint3);
							}
							Animal animal = this.addAnimal(id, point, UnityEngine.Random.Range(0f, 360f), false);
							if (animal != null)
							{
								animal.pack = validAnimalSpawnsInfo.pack;
								validAnimalSpawnsInfo.pack.animals.Add(animal);
							}
						}
						for (int num2 = AnimalManager.packs.Count - 1; num2 >= 0; num2--)
						{
							PackInfo packInfo3 = AnimalManager.packs[num2];
							if (packInfo3.animals.Count <= 0)
							{
								AnimalManager.packs.RemoveAt(num2);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x000E840B File Offset: 0x000E680B
		private void onClientConnected()
		{
			base.channel.send("askAnimals", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x000E8428 File Offset: 0x000E6828
		private void OnDrawGizmos()
		{
			if (AnimalManager.packs == null)
			{
				return;
			}
			for (int i = 0; i < AnimalManager.packs.Count; i++)
			{
				PackInfo packInfo = AnimalManager.packs[i];
				if (packInfo != null && packInfo.spawns != null && packInfo.animals != null)
				{
					Vector3 averageSpawnPoint = packInfo.getAverageSpawnPoint();
					Vector3 averageAnimalPoint = packInfo.getAverageAnimalPoint();
					Vector3 wanderDirection = packInfo.getWanderDirection();
					Gizmos.color = Color.gray;
					for (int j = 0; j < packInfo.spawns.Count; j++)
					{
						AnimalSpawnpoint animalSpawnpoint = packInfo.spawns[j];
						if (animalSpawnpoint != null)
						{
							Gizmos.DrawLine(averageSpawnPoint, animalSpawnpoint.point);
						}
					}
					Gizmos.color = Color.yellow;
					Gizmos.DrawLine(averageSpawnPoint, averageAnimalPoint);
					for (int k = 0; k < packInfo.animals.Count; k++)
					{
						Animal animal = packInfo.animals[k];
						if (!(animal == null))
						{
							Gizmos.color = ((!animal.isDead) ? Color.green : Color.red);
							Gizmos.DrawLine(averageAnimalPoint, animal.transform.position);
							if (!animal.isDead)
							{
								Gizmos.color = Color.magenta;
								Gizmos.DrawLine(animal.transform.position, animal.target);
							}
						}
					}
					Gizmos.color = Color.cyan;
					Gizmos.DrawLine(averageAnimalPoint, averageAnimalPoint + wanderDirection * 4f);
				}
			}
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x000E85C4 File Offset: 0x000E69C4
		private void Update()
		{
			if (!Provider.isServer || !Level.isLoaded)
			{
				return;
			}
			if (AnimalManager.animals == null || AnimalManager.animals.Count == 0)
			{
				return;
			}
			if (AnimalManager.tickingAnimals == null)
			{
				return;
			}
			int num;
			int num2;
			if (Dedicator.isDedicated)
			{
				if (AnimalManager.tickIndex >= AnimalManager.tickingAnimals.Count)
				{
					AnimalManager.tickIndex = 0;
				}
				num = AnimalManager.tickIndex;
				num2 = num + 25;
				if (num2 >= AnimalManager.tickingAnimals.Count)
				{
					num2 = AnimalManager.tickingAnimals.Count;
				}
				AnimalManager.tickIndex = num2;
			}
			else
			{
				num = 0;
				num2 = AnimalManager.tickingAnimals.Count;
			}
			for (int i = num2 - 1; i >= num; i--)
			{
				Animal animal = AnimalManager.tickingAnimals[i];
				if (animal == null)
				{
					Debug.LogError("Missing animal " + i);
				}
				else
				{
					animal.tick();
				}
			}
			if (Dedicator.isDedicated && Time.realtimeSinceStartup - AnimalManager.lastTick > Provider.UPDATE_TIME)
			{
				AnimalManager.lastTick += Provider.UPDATE_TIME;
				if (Time.realtimeSinceStartup - AnimalManager.lastTick > Provider.UPDATE_TIME)
				{
					AnimalManager.lastTick = Time.realtimeSinceStartup;
				}
				base.channel.useCompression = true;
				this.seq += 1u;
				for (int j = 0; j < Provider.clients.Count; j++)
				{
					SteamPlayer steamPlayer = Provider.clients[j];
					if (steamPlayer != null && !(steamPlayer.player == null))
					{
						base.channel.openWrite();
						base.channel.write(this.seq);
						ushort num3 = 0;
						int step = base.channel.step;
						base.channel.write(num3);
						int num4 = 0;
						for (int k = 0; k < AnimalManager.animals.Count; k++)
						{
							if (num3 >= 64)
							{
								break;
							}
							Animal animal2 = AnimalManager.animals[k];
							if (!(animal2 == null) && animal2.isUpdated)
							{
								if ((animal2.transform.position - steamPlayer.player.transform.position).sqrMagnitude > 331776f)
								{
									if ((ulong)(this.seq % 8u) == (ulong)((long)num4))
									{
										base.channel.write(animal2.index, animal2.transform.position, MeasurementTool.angleToByte(animal2.transform.rotation.eulerAngles.y));
										num3 += 1;
									}
									num4++;
								}
								else
								{
									base.channel.write(animal2.index, animal2.transform.position, MeasurementTool.angleToByte(animal2.transform.rotation.eulerAngles.y));
									num3 += 1;
								}
							}
						}
						if (num3 != 0)
						{
							int step2 = base.channel.step;
							base.channel.step = step;
							base.channel.write(num3);
							base.channel.step = step2;
							base.channel.closeWrite("tellAnimalStates", steamPlayer.playerID.steamID, ESteamPacket.UPDATE_UNRELIABLE_CHUNK_BUFFER);
						}
					}
				}
				base.channel.useCompression = false;
				for (int l = 0; l < AnimalManager.animals.Count; l++)
				{
					Animal animal3 = AnimalManager.animals[l];
					if (!(animal3 == null))
					{
						animal3.isUpdated = false;
					}
				}
			}
			this.respawnAnimals();
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000E89C8 File Offset: 0x000E6DC8
		private void Start()
		{
			AnimalManager.manager = this;
			Level.onLevelLoaded = (LevelLoaded)Delegate.Combine(Level.onLevelLoaded, new LevelLoaded(this.onLevelLoaded));
			Provider.onClientConnected = (Provider.ClientConnected)Delegate.Combine(Provider.onClientConnected, new Provider.ClientConnected(this.onClientConnected));
		}

		// Token: 0x040018A2 RID: 6306
		private static AnimalManager manager;

		// Token: 0x040018A3 RID: 6307
		private static List<Animal> _animals;

		// Token: 0x040018A4 RID: 6308
		private static List<PackInfo> _packs;

		// Token: 0x040018A5 RID: 6309
		private static int tickIndex;

		// Token: 0x040018A6 RID: 6310
		private static List<Animal> _tickingAnimals;

		// Token: 0x040018A7 RID: 6311
		public static ushort updates;

		// Token: 0x040018A8 RID: 6312
		private static ushort respawnPackIndex;

		// Token: 0x040018A9 RID: 6313
		private static float lastTick;

		// Token: 0x040018AA RID: 6314
		private uint seq;

		// Token: 0x02000585 RID: 1413
		private class ValidAnimalSpawnsInfo
		{
			// Token: 0x040018AB RID: 6315
			public List<AnimalSpawnpoint> spawns;

			// Token: 0x040018AC RID: 6316
			public PackInfo pack;
		}
	}
}
