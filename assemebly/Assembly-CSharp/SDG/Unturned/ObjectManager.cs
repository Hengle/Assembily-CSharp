using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005B4 RID: 1460
	public class ObjectManager : SteamCaller
	{
		// Token: 0x060028CE RID: 10446 RVA: 0x000F8024 File Offset: 0x000F6424
		public static void getObjectsInRadius(Vector3 center, float sqrRadius, List<RegionCoordinate> search, List<Transform> result)
		{
			if (LevelObjects.objects == null)
			{
				return;
			}
			for (int i = 0; i < search.Count; i++)
			{
				RegionCoordinate regionCoordinate = search[i];
				if (LevelObjects.objects[(int)regionCoordinate.x, (int)regionCoordinate.y] != null)
				{
					for (int j = 0; j < LevelObjects.objects[(int)regionCoordinate.x, (int)regionCoordinate.y].Count; j++)
					{
						LevelObject levelObject = LevelObjects.objects[(int)regionCoordinate.x, (int)regionCoordinate.y][j];
						if (!(levelObject.transform == null))
						{
							if ((levelObject.transform.position - center).sqrMagnitude < sqrRadius)
							{
								result.Add(levelObject.transform);
							}
						}
					}
				}
			}
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x000F810C File Offset: 0x000F650C
		[SteamCall]
		public void tellObjectRubble(CSteamID steamID, byte x, byte y, ushort index, byte section, bool isAlive, Vector3 ragdoll)
		{
			if (base.channel.checkServer(steamID))
			{
				if (!Regions.checkSafe((int)x, (int)y))
				{
					return;
				}
				ObjectRegion objectRegion = ObjectManager.regions[(int)x, (int)y];
				if (!Provider.isServer && !objectRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= LevelObjects.objects[(int)x, (int)y].Count)
				{
					return;
				}
				InteractableObjectRubble rubble = LevelObjects.objects[(int)x, (int)y][(int)index].rubble;
				if (rubble != null)
				{
					rubble.updateRubble(section, isAlive, true, ragdoll);
				}
			}
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000F81A8 File Offset: 0x000F65A8
		public static void damage(Transform obj, Vector3 direction, byte section, float damage, float times, out EPlayerKill kill, out uint xp)
		{
			kill = EPlayerKill.NONE;
			xp = 0u;
			byte b;
			byte b2;
			ushort num;
			if (ObjectManager.tryGetRegion(obj, out b, out b2, out num))
			{
				InteractableObjectRubble rubble = LevelObjects.objects[(int)b, (int)b2][(int)num].rubble;
				if (rubble != null && !rubble.isSectionDead(section))
				{
					ushort num2 = (ushort)(damage * times);
					rubble.askDamage(section, num2);
					if (rubble.isSectionDead(section))
					{
						kill = EPlayerKill.OBJECT;
						if (LevelObjects.objects[(int)b, (int)b2][(int)num].asset != null)
						{
							xp = LevelObjects.objects[(int)b, (int)b2][(int)num].asset.rubbleRewardXP;
						}
						byte[] state = LevelObjects.objects[(int)b, (int)b2][(int)num].state;
						if (section == 255)
						{
							state[state.Length - 1] = 0;
						}
						else
						{
							state[state.Length - 1] = (state[state.Length - 1] & ~Types.SHIFTS[(int)section]);
						}
						ObjectManager.manager.channel.send("tellObjectRubble", ESteamCall.ALL, b, b2, ObjectManager.OBJECT_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							b,
							b2,
							num,
							section,
							false,
							direction * (float)num2
						});
					}
				}
			}
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000F830C File Offset: 0x000F670C
		public static void useObjectNPC(Transform transform)
		{
			byte b;
			byte b2;
			ushort num;
			if (ObjectManager.tryGetRegion(transform, out b, out b2, out num))
			{
				ObjectManager.manager.channel.send("askUseObjectNPC", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num
				});
			}
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x000F8364 File Offset: 0x000F6764
		[SteamCall]
		public void askUseObjectNPC(CSteamID steamID, byte x, byte y, ushort index)
		{
			if (Provider.isServer)
			{
				if (!Regions.checkSafe((int)x, (int)y))
				{
					return;
				}
				Player player = PlayerTool.getPlayer(steamID);
				if (player == null)
				{
					return;
				}
				if (player.life.isDead)
				{
					return;
				}
				if ((int)index >= LevelObjects.objects[(int)x, (int)y].Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((LevelObjects.objects[(int)x, (int)y][(int)index].transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableObjectNPC interactableObjectNPC = LevelObjects.objects[(int)x, (int)y][(int)index].interactable as InteractableObjectNPC;
				if (interactableObjectNPC != null)
				{
					if (!interactableObjectNPC.objectAsset.areConditionsMet(player))
					{
						return;
					}
					player.quests.checkNPC = interactableObjectNPC;
				}
			}
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000F845C File Offset: 0x000F685C
		public static void useObjectQuest(Transform transform)
		{
			byte b;
			byte b2;
			ushort num;
			if (ObjectManager.tryGetRegion(transform, out b, out b2, out num))
			{
				ObjectManager.manager.channel.send("askUseObjectQuest", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num
				});
			}
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000F84B4 File Offset: 0x000F68B4
		[SteamCall]
		public void askUseObjectQuest(CSteamID steamID, byte x, byte y, ushort index)
		{
			if (Provider.isServer)
			{
				if (!Regions.checkSafe((int)x, (int)y))
				{
					return;
				}
				Player player = PlayerTool.getPlayer(steamID);
				if (player == null)
				{
					return;
				}
				if (player.life.isDead)
				{
					return;
				}
				if ((int)index >= LevelObjects.objects[(int)x, (int)y].Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((LevelObjects.objects[(int)x, (int)y][(int)index].transform.position - player.transform.position).sqrMagnitude > 1600f)
				{
					return;
				}
				InteractableObject interactable = LevelObjects.objects[(int)x, (int)y][(int)index].interactable;
				if (interactable != null && (interactable is InteractableObjectQuest || interactable is InteractableObjectNote))
				{
					if (!interactable.objectAsset.areConditionsMet(player))
					{
						return;
					}
					if (!interactable.objectAsset.areInteractabilityConditionsMet(player))
					{
						return;
					}
					interactable.objectAsset.applyInteractabilityConditions(player, false);
					interactable.objectAsset.grantInteractabilityRewards(player, false);
				}
			}
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000F85DC File Offset: 0x000F69DC
		public static void useObjectDropper(Transform transform)
		{
			byte b;
			byte b2;
			ushort num;
			if (ObjectManager.tryGetRegion(transform, out b, out b2, out num))
			{
				ObjectManager.manager.channel.send("askUseObjectDropper", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num
				});
			}
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000F8634 File Offset: 0x000F6A34
		[SteamCall]
		public void askUseObjectDropper(CSteamID steamID, byte x, byte y, ushort index)
		{
			if (Provider.isServer)
			{
				if (!Regions.checkSafe((int)x, (int)y))
				{
					return;
				}
				Player player = PlayerTool.getPlayer(steamID);
				if (player == null)
				{
					return;
				}
				if (player.life.isDead)
				{
					return;
				}
				if ((int)index >= LevelObjects.objects[(int)x, (int)y].Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((LevelObjects.objects[(int)x, (int)y][(int)index].transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableObjectDropper interactableObjectDropper = LevelObjects.objects[(int)x, (int)y][(int)index].interactable as InteractableObjectDropper;
				if (interactableObjectDropper != null && interactableObjectDropper.isUsable)
				{
					if (!interactableObjectDropper.objectAsset.areConditionsMet(player))
					{
						return;
					}
					if (!interactableObjectDropper.objectAsset.areInteractabilityConditionsMet(player))
					{
						return;
					}
					interactableObjectDropper.objectAsset.applyInteractabilityConditions(player, true);
					interactableObjectDropper.objectAsset.grantInteractabilityRewards(player, true);
					interactableObjectDropper.drop();
				}
			}
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000F875C File Offset: 0x000F6B5C
		[SteamCall]
		public void tellObjectResource(CSteamID steamID, byte x, byte y, ushort index, ushort amount)
		{
			if (base.channel.checkServer(steamID))
			{
				if (!Regions.checkSafe((int)x, (int)y))
				{
					return;
				}
				ObjectRegion objectRegion = ObjectManager.regions[(int)x, (int)y];
				if (!Provider.isServer && !objectRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= LevelObjects.objects[(int)x, (int)y].Count)
				{
					return;
				}
				InteractableObjectResource interactableObjectResource = LevelObjects.objects[(int)x, (int)y][(int)index].interactable as InteractableObjectResource;
				if (interactableObjectResource != null)
				{
					interactableObjectResource.updateAmount(amount);
				}
			}
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000F87F8 File Offset: 0x000F6BF8
		public static void updateObjectResource(Transform transform, ushort amount, bool shouldSend)
		{
			byte b;
			byte b2;
			ushort num;
			if (ObjectManager.tryGetRegion(transform, out b, out b2, out num))
			{
				if (shouldSend)
				{
					ObjectManager.manager.channel.send("tellObjectResource", ESteamCall.ALL, b, b2, ObjectManager.OBJECT_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						num,
						amount
					});
				}
				byte[] bytes = BitConverter.GetBytes(amount);
				LevelObjects.objects[(int)b, (int)b2][(int)num].state[0] = bytes[0];
				LevelObjects.objects[(int)b, (int)b2][(int)num].state[1] = bytes[1];
			}
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000F88A4 File Offset: 0x000F6CA4
		public static void forceObjectBinaryState(Transform transform, bool isUsed)
		{
			byte b;
			byte b2;
			ushort num;
			if (ObjectManager.tryGetRegion(transform, out b, out b2, out num))
			{
				InteractableObjectBinaryState interactableObjectBinaryState = LevelObjects.objects[(int)b, (int)b2][(int)num].interactable as InteractableObjectBinaryState;
				if (interactableObjectBinaryState != null && interactableObjectBinaryState.isUsable)
				{
					ObjectManager.manager.channel.send("tellToggleObjectBinaryState", ESteamCall.ALL, b, b2, ObjectManager.OBJECT_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						num,
						isUsed
					});
					LevelObjects.objects[(int)b, (int)b2][(int)num].state[0] = ((!interactableObjectBinaryState.isUsed) ? 0 : 1);
				}
			}
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000F896C File Offset: 0x000F6D6C
		public static void toggleObjectBinaryState(Transform transform)
		{
			byte b;
			byte b2;
			ushort num;
			if (ObjectManager.tryGetRegion(transform, out b, out b2, out num))
			{
				ObjectManager.manager.channel.send("askToggleObjectBinaryState", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num
				});
			}
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000F89C4 File Offset: 0x000F6DC4
		[SteamCall]
		public void tellToggleObjectBinaryState(CSteamID steamID, byte x, byte y, ushort index, bool isUsed)
		{
			if (base.channel.checkServer(steamID))
			{
				if (!Regions.checkSafe((int)x, (int)y))
				{
					return;
				}
				ObjectRegion objectRegion = ObjectManager.regions[(int)x, (int)y];
				if (!Provider.isServer && !objectRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= LevelObjects.objects[(int)x, (int)y].Count)
				{
					return;
				}
				InteractableObjectBinaryState interactableObjectBinaryState = LevelObjects.objects[(int)x, (int)y][(int)index].interactable as InteractableObjectBinaryState;
				if (interactableObjectBinaryState != null)
				{
					interactableObjectBinaryState.updateToggle(isUsed);
				}
			}
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000F8A60 File Offset: 0x000F6E60
		[SteamCall]
		public void askToggleObjectBinaryState(CSteamID steamID, byte x, byte y, ushort index)
		{
			if (Provider.isServer)
			{
				if (!Regions.checkSafe((int)x, (int)y))
				{
					return;
				}
				Player player = PlayerTool.getPlayer(steamID);
				if (player == null)
				{
					return;
				}
				if (player.life.isDead)
				{
					return;
				}
				if ((int)index >= LevelObjects.objects[(int)x, (int)y].Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((LevelObjects.objects[(int)x, (int)y][(int)index].transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableObjectBinaryState interactableObjectBinaryState = LevelObjects.objects[(int)x, (int)y][(int)index].interactable as InteractableObjectBinaryState;
				if (interactableObjectBinaryState != null && interactableObjectBinaryState.isUsable && !interactableObjectBinaryState.objectAsset.interactabilityRemote)
				{
					if (!interactableObjectBinaryState.objectAsset.areConditionsMet(player))
					{
						return;
					}
					if (!interactableObjectBinaryState.objectAsset.areInteractabilityConditionsMet(player))
					{
						return;
					}
					interactableObjectBinaryState.objectAsset.applyInteractabilityConditions(player, true);
					interactableObjectBinaryState.objectAsset.grantInteractabilityRewards(player, true);
					ObjectManager.manager.channel.send("tellToggleObjectBinaryState", ESteamCall.ALL, x, y, ObjectManager.OBJECT_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						x,
						y,
						index,
						!interactableObjectBinaryState.isUsed
					});
					LevelObjects.objects[(int)x, (int)y][(int)index].state[0] = ((!interactableObjectBinaryState.isUsed) ? 0 : 1);
				}
			}
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x000F8C10 File Offset: 0x000F7010
		[SteamCall]
		public void tellClearRegionObjects(CSteamID steamID, byte x, byte y)
		{
			if (base.channel.checkServer(steamID))
			{
				if (!Provider.isServer && !ObjectManager.regions[(int)x, (int)y].isNetworked)
				{
					return;
				}
				for (int i = 0; i < LevelObjects.objects[(int)x, (int)y].Count; i++)
				{
					LevelObject levelObject = LevelObjects.objects[(int)x, (int)y][i];
					if (levelObject.state != null && levelObject.state.Length > 0)
					{
						levelObject.state = levelObject.asset.getState();
						if (levelObject.interactable != null)
						{
							levelObject.interactable.updateState(levelObject.asset, levelObject.state);
						}
						if (levelObject.rubble != null)
						{
							levelObject.rubble.updateState(levelObject.asset, levelObject.state);
						}
					}
				}
			}
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000F8D00 File Offset: 0x000F7100
		public static void askClearRegionObjects(byte x, byte y)
		{
			if (Provider.isServer)
			{
				if (!Regions.checkSafe((int)x, (int)y))
				{
					return;
				}
				if (LevelObjects.objects[(int)x, (int)y].Count > 0)
				{
					ObjectManager.manager.channel.send("tellClearRegionObjects", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						x,
						y
					});
				}
			}
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000F8D6C File Offset: 0x000F716C
		public static void askClearAllObjects()
		{
			if (Provider.isServer)
			{
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						ObjectManager.askClearRegionObjects(b, b2);
					}
				}
			}
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x000F8DB8 File Offset: 0x000F71B8
		[SteamCall]
		public void tellObjects(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID))
			{
				byte b = (byte)base.channel.read(Types.BYTE_TYPE);
				byte b2 = (byte)base.channel.read(Types.BYTE_TYPE);
				if (!Regions.checkSafe((int)b, (int)b2))
				{
					return;
				}
				if (ObjectManager.regions[(int)b, (int)b2].isNetworked)
				{
					return;
				}
				ObjectManager.regions[(int)b, (int)b2].isNetworked = true;
				for (;;)
				{
					ushort num = (ushort)base.channel.read(Types.UINT16_TYPE);
					if (num == 65535)
					{
						break;
					}
					byte[] state = (byte[])base.channel.read(Types.BYTE_ARRAY_TYPE);
					LevelObject levelObject = LevelObjects.objects[(int)b, (int)b2][(int)num];
					if (levelObject.interactable != null)
					{
						levelObject.interactable.updateState(levelObject.asset, state);
					}
					if (levelObject.rubble != null)
					{
						levelObject.rubble.updateState(levelObject.asset, state);
					}
				}
			}
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000F8EDC File Offset: 0x000F72DC
		public void askObjects(CSteamID steamID, byte x, byte y)
		{
			base.channel.openWrite();
			base.channel.write(x);
			base.channel.write(y);
			ushort num = 0;
			while ((int)num < LevelObjects.objects[(int)x, (int)y].Count)
			{
				LevelObject levelObject = LevelObjects.objects[(int)x, (int)y][(int)num];
				if (levelObject.state != null && levelObject.state.Length > 0)
				{
					base.channel.write(num);
					base.channel.write(levelObject.state);
				}
				num += 1;
			}
			base.channel.write(ushort.MaxValue);
			base.channel.closeWrite("tellObjects", steamID, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000F8FB0 File Offset: 0x000F73B0
		public static LevelObject getObject(byte x, byte y, ushort index)
		{
			if (!Regions.checkSafe((int)x, (int)y))
			{
				return null;
			}
			List<LevelObject> list = LevelObjects.objects[(int)x, (int)y];
			if ((int)index >= list.Count)
			{
				return null;
			}
			return list[(int)index];
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000F8FF0 File Offset: 0x000F73F0
		public static bool tryGetRegion(Transform transform, out byte x, out byte y, out ushort index)
		{
			x = 0;
			y = 0;
			index = 0;
			if (Regions.tryGetCoordinate(transform.position, out x, out y))
			{
				List<LevelObject> list = LevelObjects.objects[(int)x, (int)y];
				index = 0;
				while ((int)index < list.Count)
				{
					if (transform == list[(int)index].transform)
					{
						return true;
					}
					index += 1;
				}
			}
			return false;
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000F9060 File Offset: 0x000F7460
		private bool updateObjects()
		{
			if (Level.info == null || Level.info.type == ELevelType.ARENA)
			{
				return false;
			}
			if (LevelObjects.objects[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y].Count <= 0)
			{
				return true;
			}
			if ((int)ObjectManager.regions[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y].updateObjectIndex >= LevelObjects.objects[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y].Count)
			{
				ObjectManager.regions[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y].updateObjectIndex = (ushort)(LevelObjects.objects[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y].Count - 1);
			}
			LevelObject levelObject = LevelObjects.objects[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y][(int)ObjectManager.regions[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y].updateObjectIndex];
			if (levelObject == null || levelObject.asset == null)
			{
				return false;
			}
			if (levelObject.interactable != null && levelObject.asset.interactabilityReset >= 1f)
			{
				if (levelObject.asset.interactability == EObjectInteractability.BINARY_STATE)
				{
					if (((InteractableObjectBinaryState)levelObject.interactable).checkCanReset(Provider.modeConfigData.Objects.Binary_State_Reset_Multiplier))
					{
						ObjectManager.manager.channel.send("tellToggleObjectBinaryState", ESteamCall.ALL, ObjectManager.updateObjects_X, ObjectManager.updateObjects_Y, ObjectManager.OBJECT_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							ObjectManager.updateObjects_X,
							ObjectManager.updateObjects_Y,
							ObjectManager.regions[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y].updateObjectIndex,
							false
						});
						LevelObjects.objects[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y][(int)ObjectManager.regions[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y].updateObjectIndex].state[0] = 0;
					}
				}
				else if ((levelObject.asset.interactability == EObjectInteractability.WATER || levelObject.asset.interactability == EObjectInteractability.FUEL) && ((InteractableObjectResource)levelObject.interactable).checkCanReset((levelObject.asset.interactability != EObjectInteractability.WATER) ? Provider.modeConfigData.Objects.Fuel_Reset_Multiplier : Provider.modeConfigData.Objects.Water_Reset_Multiplier))
				{
					ushort num = (ushort)Mathf.Min((int)(((InteractableObjectResource)levelObject.interactable).amount + ((levelObject.asset.interactability != EObjectInteractability.WATER) ? 500 : 1)), (int)((InteractableObjectResource)levelObject.interactable).capacity);
					ObjectManager.manager.channel.send("tellObjectResource", ESteamCall.ALL, ObjectManager.updateObjects_X, ObjectManager.updateObjects_Y, ObjectManager.OBJECT_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						ObjectManager.updateObjects_X,
						ObjectManager.updateObjects_Y,
						ObjectManager.regions[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y].updateObjectIndex,
						num
					});
					byte[] bytes = BitConverter.GetBytes(num);
					LevelObjects.objects[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y][(int)ObjectManager.regions[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y].updateObjectIndex].state[0] = bytes[0];
					LevelObjects.objects[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y][(int)ObjectManager.regions[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y].updateObjectIndex].state[1] = bytes[1];
				}
			}
			if (levelObject.rubble != null && levelObject.asset.rubbleReset >= 1f && levelObject.asset.rubble == EObjectRubble.DESTROY)
			{
				byte b = levelObject.rubble.checkCanReset(Provider.modeConfigData.Objects.Rubble_Reset_Multiplier);
				if (b != 255)
				{
					byte[] state = LevelObjects.objects[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y][(int)ObjectManager.regions[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y].updateObjectIndex].state;
					state[state.Length - 1] = (state[state.Length - 1] | Types.SHIFTS[(int)b]);
					ObjectManager.manager.channel.send("tellObjectRubble", ESteamCall.ALL, ObjectManager.updateObjects_X, ObjectManager.updateObjects_Y, ObjectManager.OBJECT_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						ObjectManager.updateObjects_X,
						ObjectManager.updateObjects_Y,
						ObjectManager.regions[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y].updateObjectIndex,
						b,
						true,
						Vector3.zero
					});
				}
			}
			return false;
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000F953C File Offset: 0x000F793C
		private void onLevelLoaded(int level)
		{
			if (level > Level.SETUP)
			{
				ObjectManager.regions = new ObjectRegion[(int)Regions.WORLD_SIZE, (int)Regions.WORLD_SIZE];
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						ObjectManager.regions[(int)b, (int)b2] = new ObjectRegion();
					}
				}
				ObjectManager.updateObjects_X = 0;
				ObjectManager.updateObjects_Y = 0;
				if (Provider.isServer)
				{
					ObjectManager.load();
				}
			}
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000F95C4 File Offset: 0x000F79C4
		private void onRegionUpdated(Player player, byte old_x, byte old_y, byte new_x, byte new_y, byte step, ref bool canIncrementIndex)
		{
			if (step == 0)
			{
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						if (Provider.isServer)
						{
							if (player.movement.loadedRegions[(int)b, (int)b2].isObjectsLoaded && !Regions.checkArea(b, b2, new_x, new_y, ObjectManager.OBJECT_REGIONS))
							{
								player.movement.loadedRegions[(int)b, (int)b2].isObjectsLoaded = false;
							}
						}
						else if (player.channel.isOwner && ObjectManager.regions[(int)b, (int)b2].isNetworked && !Regions.checkArea(b, b2, new_x, new_y, ObjectManager.OBJECT_REGIONS))
						{
							ObjectManager.regions[(int)b, (int)b2].isNetworked = false;
						}
					}
				}
			}
			if (step == 4 && Dedicator.isDedicated && Regions.checkSafe((int)new_x, (int)new_y))
			{
				for (int i = (int)(new_x - ObjectManager.OBJECT_REGIONS); i <= (int)(new_x + ObjectManager.OBJECT_REGIONS); i++)
				{
					for (int j = (int)(new_y - ObjectManager.OBJECT_REGIONS); j <= (int)(new_y + ObjectManager.OBJECT_REGIONS); j++)
					{
						if (Regions.checkSafe((int)((byte)i), (int)((byte)j)) && !player.movement.loadedRegions[i, j].isObjectsLoaded)
						{
							player.movement.loadedRegions[i, j].isObjectsLoaded = true;
							this.askObjects(player.channel.owner.playerID.steamID, (byte)i, (byte)j);
						}
					}
				}
			}
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000F976C File Offset: 0x000F7B6C
		private void onPlayerCreated(Player player)
		{
			PlayerMovement movement = player.movement;
			movement.onRegionUpdated = (PlayerRegionUpdated)Delegate.Combine(movement.onRegionUpdated, new PlayerRegionUpdated(this.onRegionUpdated));
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x000F9798 File Offset: 0x000F7B98
		private void Update()
		{
			if (!Level.isLoaded)
			{
				return;
			}
			if (!Provider.isServer)
			{
				return;
			}
			bool flag = true;
			while (flag)
			{
				flag = this.updateObjects();
				ObjectRegion objectRegion = ObjectManager.regions[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y];
				objectRegion.updateObjectIndex += 1;
				if ((int)ObjectManager.regions[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y].updateObjectIndex >= LevelObjects.objects[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y].Count)
				{
					ObjectManager.regions[(int)ObjectManager.updateObjects_X, (int)ObjectManager.updateObjects_Y].updateObjectIndex = 0;
				}
				ObjectManager.updateObjects_X += 1;
				if (ObjectManager.updateObjects_X >= Regions.WORLD_SIZE)
				{
					ObjectManager.updateObjects_X = 0;
					ObjectManager.updateObjects_Y += 1;
					if (ObjectManager.updateObjects_Y >= Regions.WORLD_SIZE)
					{
						ObjectManager.updateObjects_Y = 0;
						flag = false;
					}
				}
			}
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000F9888 File Offset: 0x000F7C88
		private void Start()
		{
			ObjectManager.manager = this;
			Level.onLevelLoaded = (LevelLoaded)Delegate.Combine(Level.onLevelLoaded, new LevelLoaded(this.onLevelLoaded));
			Player.onPlayerCreated = (PlayerCreated)Delegate.Combine(Player.onPlayerCreated, new PlayerCreated(this.onPlayerCreated));
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x000F98DC File Offset: 0x000F7CDC
		public static void load()
		{
			if (LevelSavedata.fileExists("/Objects.dat") && Level.info.type == ELevelType.SURVIVAL)
			{
				River river = LevelSavedata.openRiver("/Objects.dat", true);
				river.readByte();
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						ObjectManager.loadRegion(river, LevelObjects.objects[(int)b, (int)b2]);
					}
				}
			}
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x000F995C File Offset: 0x000F7D5C
		public static void save()
		{
			River river = LevelSavedata.openRiver("/Objects.dat", false);
			river.writeByte(ObjectManager.SAVEDATA_VERSION);
			for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
			{
				for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
				{
					ObjectManager.saveRegion(river, LevelObjects.objects[(int)b, (int)b2]);
				}
			}
			river.closeRiver();
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x000F99C8 File Offset: 0x000F7DC8
		private static void loadRegion(River river, List<LevelObject> objects)
		{
			for (;;)
			{
				ushort num = river.readUInt16();
				if (num == 65535)
				{
					break;
				}
				ushort num2 = river.readUInt16();
				byte[] array = river.readBytes();
				if ((int)num >= objects.Count)
				{
					return;
				}
				LevelObject levelObject = objects[(int)num];
				if (num2 == levelObject.id)
				{
					levelObject.state = array;
					if (!(levelObject.transform == null) && levelObject.asset != null)
					{
						if (levelObject.interactable != null)
						{
							if (levelObject.interactable is InteractableObjectBinaryState)
							{
								if (levelObject.asset.interactabilityReset >= 1f)
								{
									array[0] = 0;
								}
							}
							else if (levelObject.interactable is InteractableObjectResource)
							{
								if (levelObject.asset.rubble == EObjectRubble.DESTROY)
								{
									if (array.Length < 3)
									{
										array = levelObject.asset.getState();
										levelObject.state = array;
									}
								}
								else if (array.Length < 2)
								{
									array = levelObject.asset.getState();
									levelObject.state = array;
								}
							}
							levelObject.interactable.updateState(levelObject.asset, array);
						}
						if (levelObject.rubble != null)
						{
							array[array.Length - 1] = byte.MaxValue;
							levelObject.rubble.updateState(levelObject.asset, array);
						}
					}
				}
			}
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x000F9B2C File Offset: 0x000F7F2C
		private static void saveRegion(River river, List<LevelObject> objects)
		{
			ushort num = 0;
			while ((int)num < objects.Count)
			{
				LevelObject levelObject = objects[(int)num];
				if (levelObject.state != null && levelObject.state.Length > 0)
				{
					river.writeUInt16(num);
					river.writeUInt16(levelObject.id);
					river.writeBytes(levelObject.state);
				}
				num += 1;
			}
			river.writeUInt16(ushort.MaxValue);
		}

		// Token: 0x04001986 RID: 6534
		public static readonly byte SAVEDATA_VERSION = 1;

		// Token: 0x04001987 RID: 6535
		public static readonly byte OBJECT_REGIONS = 2;

		// Token: 0x04001988 RID: 6536
		private static ObjectManager manager;

		// Token: 0x04001989 RID: 6537
		private static ObjectRegion[,] regions;

		// Token: 0x0400198A RID: 6538
		private static byte updateObjects_X;

		// Token: 0x0400198B RID: 6539
		private static byte updateObjects_Y;
	}
}
