using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005C1 RID: 1473
	public class StructureManager : SteamCaller
	{
		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002929 RID: 10537 RVA: 0x000FB43F File Offset: 0x000F983F
		public static StructureManager instance
		{
			get
			{
				return StructureManager.manager;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x0600292A RID: 10538 RVA: 0x000FB446 File Offset: 0x000F9846
		// (set) Token: 0x0600292B RID: 10539 RVA: 0x000FB44D File Offset: 0x000F984D
		public static StructureRegion[,] regions { get; private set; }

		// Token: 0x0600292C RID: 10540 RVA: 0x000FB458 File Offset: 0x000F9858
		public static void getStructuresInRadius(Vector3 center, float sqrRadius, List<RegionCoordinate> search, List<Transform> result)
		{
			if (StructureManager.regions == null)
			{
				return;
			}
			for (int i = 0; i < search.Count; i++)
			{
				RegionCoordinate regionCoordinate = search[i];
				if (StructureManager.regions[(int)regionCoordinate.x, (int)regionCoordinate.y] != null)
				{
					for (int j = 0; j < StructureManager.regions[(int)regionCoordinate.x, (int)regionCoordinate.y].drops.Count; j++)
					{
						Transform model = StructureManager.regions[(int)regionCoordinate.x, (int)regionCoordinate.y].drops[j].model;
						if ((model.position - center).sqrMagnitude < sqrRadius)
						{
							result.Add(model);
						}
					}
				}
			}
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x000FB530 File Offset: 0x000F9930
		public static void transformStructure(Transform structure, Vector3 point, float angle_x, float angle_y, float angle_z)
		{
			angle_x = (float)(Mathf.RoundToInt(angle_x / 2f) * 2);
			angle_y = (float)(Mathf.RoundToInt(angle_y / 2f) * 2);
			angle_z = (float)(Mathf.RoundToInt(angle_z / 2f) * 2);
			byte b;
			byte b2;
			ushort num;
			StructureRegion structureRegion;
			StructureDrop structureDrop;
			if (StructureManager.tryGetInfo(structure, out b, out b2, out num, out structureRegion, out structureDrop))
			{
				StructureManager.manager.channel.send("askTransformStructure", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					structureDrop.instanceID,
					point,
					MeasurementTool.angleToByte(angle_x),
					MeasurementTool.angleToByte(angle_y),
					MeasurementTool.angleToByte(angle_z)
				});
			}
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x000FB5F8 File Offset: 0x000F99F8
		[SteamCall]
		public void tellTransformStructure(CSteamID steamID, byte x, byte y, uint instanceID, Vector3 point, byte angle_x, byte angle_y, byte angle_z)
		{
			StructureRegion structureRegion;
			if (base.channel.checkServer(steamID) && StructureManager.tryGetRegion(x, y, out structureRegion))
			{
				if (!Provider.isServer && !structureRegion.isNetworked)
				{
					return;
				}
				StructureData structureData = null;
				StructureDrop structureDrop = null;
				ushort num = 0;
				while ((int)num < structureRegion.drops.Count)
				{
					if (structureRegion.drops[(int)num].instanceID == instanceID)
					{
						if (Provider.isServer)
						{
							structureData = structureRegion.structures[(int)num];
						}
						structureDrop = structureRegion.drops[(int)num];
						break;
					}
					num += 1;
				}
				if (structureDrop == null)
				{
					return;
				}
				structureDrop.model.position = point;
				structureDrop.model.rotation = Quaternion.Euler((float)(angle_x * 2), (float)(angle_y * 2), (float)(angle_z * 2));
				byte b;
				byte b2;
				if (Regions.tryGetCoordinate(point, out b, out b2) && (x != b || y != b2))
				{
					StructureRegion structureRegion2 = StructureManager.regions[(int)b, (int)b2];
					structureRegion.drops.RemoveAt((int)num);
					if (structureRegion2.isNetworked || Provider.isServer)
					{
						structureRegion2.drops.Add(structureDrop);
					}
					else if (!Provider.isServer)
					{
						UnityEngine.Object.Destroy(structureDrop.model.gameObject);
					}
					if (Provider.isServer)
					{
						structureRegion.structures.RemoveAt((int)num);
						structureRegion2.structures.Add(structureData);
					}
				}
				if (Provider.isServer)
				{
					structureData.point = point;
					structureData.angle_x = angle_x;
					structureData.angle_y = angle_y;
					structureData.angle_z = angle_z;
				}
			}
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x000FB79C File Offset: 0x000F9B9C
		[SteamCall]
		public void askTransformStructure(CSteamID steamID, byte x, byte y, uint instanceID, Vector3 point, byte angle_x, byte angle_y, byte angle_z)
		{
			StructureRegion structureRegion;
			if (Provider.isServer && StructureManager.tryGetRegion(x, y, out structureRegion))
			{
				Player player = PlayerTool.getPlayer(steamID);
				if (player == null)
				{
					return;
				}
				if (player.life.isDead)
				{
					return;
				}
				if (!player.channel.owner.isAdmin)
				{
					return;
				}
				StructureManager.manager.channel.send("tellTransformStructure", ESteamCall.ALL, x, y, StructureManager.STRUCTURE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					x,
					y,
					instanceID,
					point,
					angle_x,
					angle_y,
					angle_z
				});
			}
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x000FB864 File Offset: 0x000F9C64
		[SteamCall]
		public void tellStructureHealth(CSteamID steamID, byte x, byte y, ushort index, byte hp)
		{
			StructureRegion structureRegion;
			if (base.channel.checkServer(steamID) && StructureManager.tryGetRegion(x, y, out structureRegion))
			{
				if (!Provider.isServer && !structureRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= structureRegion.drops.Count)
				{
					return;
				}
				Interactable2HP component = structureRegion.drops[(int)index].model.GetComponent<Interactable2HP>();
				if (component != null)
				{
					component.hp = hp;
				}
			}
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000FB8E8 File Offset: 0x000F9CE8
		public static void salvageStructure(Transform structure)
		{
			byte b;
			byte b2;
			ushort num;
			StructureRegion structureRegion;
			if (StructureManager.tryGetInfo(structure, out b, out b2, out num, out structureRegion))
			{
				StructureManager.manager.channel.send("askSalvageStructure", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num
				});
			}
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x000FB940 File Offset: 0x000F9D40
		[SteamCall]
		public void askSalvageStructure(CSteamID steamID, byte x, byte y, ushort index)
		{
			StructureRegion structureRegion;
			if (Provider.isServer && StructureManager.tryGetRegion(x, y, out structureRegion))
			{
				Player player = PlayerTool.getPlayer(steamID);
				if (player == null)
				{
					return;
				}
				if (player.life.isDead)
				{
					return;
				}
				if ((int)index >= structureRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if (!OwnershipTool.checkToggle(player.channel.owner.playerID.steamID, structureRegion.structures[(int)index].owner, player.quests.groupID, structureRegion.structures[(int)index].group))
				{
					return;
				}
				bool flag = true;
				if (StructureManager.onSalvageStructureRequested != null)
				{
					StructureManager.onSalvageStructureRequested(steamID, x, y, index, ref flag);
				}
				if (!flag)
				{
					return;
				}
				ItemStructureAsset itemStructureAsset = (ItemStructureAsset)Assets.find(EAssetType.ITEM, structureRegion.structures[(int)index].structure.id);
				if (itemStructureAsset != null)
				{
					if (itemStructureAsset.isUnpickupable)
					{
						return;
					}
					if (structureRegion.structures[(int)index].structure.health >= itemStructureAsset.health)
					{
						player.inventory.forceAddItem(new Item(structureRegion.structures[(int)index].structure.id, EItemOrigin.NATURE), true);
					}
					else if (itemStructureAsset.isSalvageable)
					{
						for (int i = 0; i < itemStructureAsset.blueprints.Count; i++)
						{
							Blueprint blueprint = itemStructureAsset.blueprints[i];
							if (blueprint.outputs.Length == 1 && blueprint.outputs[0].id == itemStructureAsset.id)
							{
								ushort id = blueprint.supplies[UnityEngine.Random.Range(0, blueprint.supplies.Length)].id;
								player.inventory.forceAddItem(new Item(id, EItemOrigin.NATURE), true);
								break;
							}
						}
					}
				}
				structureRegion.structures.RemoveAt((int)index);
				StructureManager.manager.channel.send("tellTakeStructure", ESteamCall.ALL, x, y, StructureManager.STRUCTURE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					x,
					y,
					index,
					(structureRegion.drops[(int)index].model.position - player.transform.position).normalized * 100f
				});
			}
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x000FBBCC File Offset: 0x000F9FCC
		public static void damage(Transform structure, Vector3 direction, float damage, float times, bool armor)
		{
			byte b;
			byte b2;
			ushort num;
			StructureRegion structureRegion;
			if (StructureManager.tryGetInfo(structure, out b, out b2, out num, out structureRegion) && !structureRegion.structures[(int)num].structure.isDead)
			{
				if (armor)
				{
					times *= Provider.modeConfigData.Structures.Armor_Multiplier;
				}
				ushort num2 = (ushort)(damage * times);
				structureRegion.structures[(int)num].structure.askDamage(num2);
				if (structureRegion.structures[(int)num].structure.isDead)
				{
					ItemStructureAsset itemStructureAsset = (ItemStructureAsset)Assets.find(EAssetType.ITEM, structureRegion.structures[(int)num].structure.id);
					if (itemStructureAsset != null && itemStructureAsset.explosion != 0)
					{
						EffectManager.sendEffect(itemStructureAsset.explosion, EffectManager.SMALL, structure.position + Vector3.down * StructureManager.HEIGHT);
					}
					structureRegion.structures.RemoveAt((int)num);
					StructureManager.manager.channel.send("tellTakeStructure", ESteamCall.ALL, b, b2, StructureManager.STRUCTURE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						num,
						direction * (float)num2
					});
				}
				else
				{
					for (int i = 0; i < Provider.clients.Count; i++)
					{
						if (Provider.clients[i].player != null && OwnershipTool.checkToggle(Provider.clients[i].playerID.steamID, structureRegion.structures[(int)num].owner, Provider.clients[i].player.quests.groupID, structureRegion.structures[(int)num].group) && Regions.checkArea(b, b2, Provider.clients[i].player.movement.region_x, Provider.clients[i].player.movement.region_y, StructureManager.STRUCTURE_REGIONS))
						{
							StructureManager.manager.channel.send("tellStructureHealth", Provider.clients[i].playerID.steamID, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
							{
								b,
								b2,
								num,
								(byte)Mathf.RoundToInt((float)structureRegion.structures[(int)num].structure.health / (float)structureRegion.structures[(int)num].structure.asset.health * 100f)
							});
						}
					}
				}
			}
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x000FBE90 File Offset: 0x000FA290
		public static void repair(Transform structure, float damage, float times)
		{
			byte b;
			byte b2;
			ushort num;
			StructureRegion structureRegion;
			if (StructureManager.tryGetInfo(structure, out b, out b2, out num, out structureRegion) && !structureRegion.structures[(int)num].structure.isDead && !structureRegion.structures[(int)num].structure.isRepaired)
			{
				ushort amount = (ushort)(damage * times);
				structureRegion.structures[(int)num].structure.askRepair(amount);
				for (int i = 0; i < Provider.clients.Count; i++)
				{
					if (Provider.clients[i].player != null && OwnershipTool.checkToggle(Provider.clients[i].playerID.steamID, structureRegion.structures[(int)num].owner, Provider.clients[i].player.quests.groupID, structureRegion.structures[(int)num].group) && Regions.checkArea(b, b2, Provider.clients[i].player.movement.region_x, Provider.clients[i].player.movement.region_y, StructureManager.STRUCTURE_REGIONS))
					{
						StructureManager.manager.channel.send("tellStructureHealth", Provider.clients[i].playerID.steamID, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
						{
							b,
							b2,
							num,
							(byte)Mathf.RoundToInt((float)structureRegion.structures[(int)num].structure.health / (float)structureRegion.structures[(int)num].structure.asset.health * 100f)
						});
					}
				}
			}
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x000FC078 File Offset: 0x000FA478
		public static bool tryGetInfo(Transform structure, out byte x, out byte y, out ushort index, out StructureRegion region)
		{
			x = 0;
			y = 0;
			index = 0;
			region = null;
			if (StructureManager.tryGetRegion(structure, out x, out y, out region))
			{
				index = 0;
				while ((int)index < region.drops.Count)
				{
					if (structure == region.drops[(int)index].model)
					{
						return true;
					}
					index += 1;
				}
			}
			return false;
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000FC0E8 File Offset: 0x000FA4E8
		public static bool tryGetInfo(Transform structure, out byte x, out byte y, out ushort index, out StructureRegion region, out StructureDrop drop)
		{
			x = 0;
			y = 0;
			index = 0;
			region = null;
			drop = null;
			if (StructureManager.tryGetRegion(structure, out x, out y, out region))
			{
				index = 0;
				while ((int)index < region.drops.Count)
				{
					if (structure == region.drops[(int)index].model)
					{
						drop = region.drops[(int)index];
						return true;
					}
					index += 1;
				}
			}
			return false;
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x000FC16C File Offset: 0x000FA56C
		public static bool tryGetRegion(Transform structure, out byte x, out byte y, out StructureRegion region)
		{
			x = 0;
			y = 0;
			region = null;
			if (structure == null)
			{
				return false;
			}
			if (Regions.tryGetCoordinate(structure.position, out x, out y))
			{
				region = StructureManager.regions[(int)x, (int)y];
				return true;
			}
			return false;
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000FC1AA File Offset: 0x000FA5AA
		public static bool tryGetRegion(byte x, byte y, out StructureRegion region)
		{
			region = null;
			if (Regions.checkSafe((int)x, (int)y))
			{
				region = StructureManager.regions[(int)x, (int)y];
				return true;
			}
			return false;
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000FC1CC File Offset: 0x000FA5CC
		public static void dropStructure(Structure structure, Vector3 point, float angle_x, float angle_y, float angle_z, ulong owner, ulong group)
		{
			ItemStructureAsset itemStructureAsset = (ItemStructureAsset)Assets.find(EAssetType.ITEM, structure.id);
			if (itemStructureAsset != null)
			{
				Vector3 eulerAngles = Quaternion.Euler(-90f, angle_y, 0f).eulerAngles;
				angle_x = (float)(Mathf.RoundToInt(eulerAngles.x / 2f) * 2);
				angle_y = (float)(Mathf.RoundToInt(eulerAngles.y / 2f) * 2);
				angle_z = (float)(Mathf.RoundToInt(eulerAngles.z / 2f) * 2);
				byte b;
				byte b2;
				StructureRegion structureRegion;
				if (Regions.tryGetCoordinate(point, out b, out b2) && StructureManager.tryGetRegion(b, b2, out structureRegion))
				{
					StructureData structureData = new StructureData(structure, point, MeasurementTool.angleToByte(angle_x), MeasurementTool.angleToByte(angle_y), MeasurementTool.angleToByte(angle_z), owner, group, Provider.time);
					structureRegion.structures.Add(structureData);
					StructureManager.manager.channel.send("tellStructure", ESteamCall.ALL, b, b2, StructureManager.STRUCTURE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						structure.id,
						structureData.point,
						structureData.angle_x,
						structureData.angle_y,
						structureData.angle_z,
						owner,
						group,
						StructureManager.instanceCount += 1u
					});
				}
			}
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000FC350 File Offset: 0x000FA750
		[SteamCall]
		public void tellTakeStructure(CSteamID steamID, byte x, byte y, ushort index, Vector3 ragdoll)
		{
			StructureRegion structureRegion;
			if (base.channel.checkServer(steamID) && StructureManager.tryGetRegion(x, y, out structureRegion))
			{
				if (!Provider.isServer && !structureRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= structureRegion.drops.Count)
				{
					return;
				}
				if (Dedicator.isDedicated || !GraphicsSettings.debris)
				{
					UnityEngine.Object.Destroy(structureRegion.drops[(int)index].model.gameObject);
					structureRegion.drops[(int)index].model.position = Vector3.zero;
				}
				else
				{
					ItemStructureAsset itemStructureAsset = (ItemStructureAsset)Assets.find(EAssetType.ITEM, ushort.Parse(structureRegion.drops[(int)index].model.name));
					if (itemStructureAsset != null && itemStructureAsset.construct != EConstruct.FLOOR && itemStructureAsset.construct != EConstruct.ROOF && itemStructureAsset.construct != EConstruct.FLOOR_POLY && itemStructureAsset.construct != EConstruct.ROOF_POLY)
					{
						ragdoll.y += 8f;
						ragdoll.x += UnityEngine.Random.Range(-16f, 16f);
						ragdoll.z += UnityEngine.Random.Range(-16f, 16f);
						ragdoll *= 2f;
						structureRegion.drops[(int)index].model.parent = Level.effects;
						MeshCollider component = structureRegion.drops[(int)index].model.GetComponent<MeshCollider>();
						if (component != null)
						{
							component.convex = true;
						}
						structureRegion.drops[(int)index].model.tag = "Debris";
						structureRegion.drops[(int)index].model.gameObject.layer = LayerMasks.DEBRIS;
						Rigidbody rigidbody = structureRegion.drops[(int)index].model.gameObject.GetComponent<Rigidbody>();
						if (rigidbody == null)
						{
							rigidbody = structureRegion.drops[(int)index].model.gameObject.AddComponent<Rigidbody>();
						}
						rigidbody.useGravity = true;
						rigidbody.isKinematic = false;
						rigidbody.AddForce(ragdoll);
						rigidbody.drag = 0.5f;
						rigidbody.angularDrag = 0.1f;
						structureRegion.drops[(int)index].model.localScale *= 0.75f;
						UnityEngine.Object.Destroy(structureRegion.drops[(int)index].model.gameObject, 8f);
						if (Provider.isServer)
						{
							UnityEngine.Object.Destroy(structureRegion.drops[(int)index].model.FindChild("Nav").gameObject);
						}
						for (int i = 0; i < structureRegion.drops[(int)index].model.childCount; i++)
						{
							Transform child = structureRegion.drops[(int)index].model.GetChild(i);
							if (!(child == null))
							{
								if (child.CompareTag("Logic"))
								{
									UnityEngine.Object.Destroy(child.gameObject);
								}
							}
						}
					}
					else
					{
						UnityEngine.Object.Destroy(structureRegion.drops[(int)index].model.gameObject);
						structureRegion.drops[(int)index].model.position = Vector3.zero;
					}
				}
				structureRegion.drops.RemoveAt((int)index);
			}
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000FC6DC File Offset: 0x000FAADC
		[SteamCall]
		public void tellClearRegionStructures(CSteamID steamID, byte x, byte y)
		{
			if (base.channel.checkServer(steamID))
			{
				if (!Provider.isServer && !StructureManager.regions[(int)x, (int)y].isNetworked)
				{
					return;
				}
				StructureRegion structureRegion = StructureManager.regions[(int)x, (int)y];
				structureRegion.destroy();
			}
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000FC730 File Offset: 0x000FAB30
		public static void askClearRegionStructures(byte x, byte y)
		{
			if (Provider.isServer)
			{
				if (!Regions.checkSafe((int)x, (int)y))
				{
					return;
				}
				StructureRegion structureRegion = StructureManager.regions[(int)x, (int)y];
				if (structureRegion.structures.Count > 0)
				{
					structureRegion.structures.Clear();
					StructureManager.manager.channel.send("tellClearRegionStructures", ESteamCall.ALL, x, y, StructureManager.STRUCTURE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						x,
						y
					});
				}
			}
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000FC7B4 File Offset: 0x000FABB4
		public static void askClearAllStructures()
		{
			if (Provider.isServer)
			{
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						StructureManager.askClearRegionStructures(b, b2);
					}
				}
			}
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000FC800 File Offset: 0x000FAC00
		private Transform spawnStructure(StructureRegion region, ushort id, Vector3 point, byte angle_x, byte angle_y, byte angle_z, byte hp, ulong owner, ulong group, uint instanceID)
		{
			if (id == 0)
			{
				return null;
			}
			ItemStructureAsset itemStructureAsset;
			try
			{
				itemStructureAsset = (ItemStructureAsset)Assets.find(EAssetType.ITEM, id);
			}
			catch
			{
				itemStructureAsset = null;
			}
			if (itemStructureAsset != null)
			{
				Transform structure = StructureTool.getStructure(id, hp, owner, group, itemStructureAsset);
				structure.parent = LevelStructures.models;
				structure.position = point;
				structure.rotation = Quaternion.Euler((float)(angle_x * 2), (float)(angle_y * 2), (float)(angle_z * 2));
				if (!Dedicator.isDedicated && (itemStructureAsset.construct == EConstruct.FLOOR || itemStructureAsset.construct == EConstruct.FLOOR_POLY))
				{
					LevelGround.bewilder(point);
				}
				region.drops.Add(new StructureDrop(structure, instanceID));
				StructureManager.structureColliders.Clear();
				structure.GetComponentsInChildren<Collider>(StructureManager.structureColliders);
				for (int i = 0; i < StructureManager.structureColliders.Count; i++)
				{
					if (StructureManager.structureColliders[i] is MeshCollider)
					{
						StructureManager.structureColliders[i].enabled = false;
					}
					if (StructureManager.structureColliders[i] is MeshCollider)
					{
						StructureManager.structureColliders[i].enabled = true;
					}
				}
				return structure;
			}
			if (!Provider.isServer)
			{
				Provider.connectionFailureInfo = ESteamConnectionFailureInfo.STRUCTURE;
				Provider.disconnect();
			}
			return null;
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000FC950 File Offset: 0x000FAD50
		[SteamCall]
		public void tellStructure(CSteamID steamID, byte x, byte y, ushort id, Vector3 point, byte angle_x, byte angle_y, byte angle_z, ulong owner, ulong group, uint instanceID)
		{
			StructureRegion structureRegion;
			if (base.channel.checkServer(steamID) && StructureManager.tryGetRegion(x, y, out structureRegion))
			{
				if (!Provider.isServer && !structureRegion.isNetworked)
				{
					return;
				}
				this.spawnStructure(structureRegion, id, point, angle_x, angle_y, angle_z, 100, owner, group, instanceID);
			}
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x000FC9AC File Offset: 0x000FADAC
		[SteamCall]
		public void tellStructures(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID))
			{
				byte x = (byte)base.channel.read(Types.BYTE_TYPE);
				byte y = (byte)base.channel.read(Types.BYTE_TYPE);
				StructureRegion structureRegion;
				if (StructureManager.tryGetRegion(x, y, out structureRegion))
				{
					if ((byte)base.channel.read(Types.BYTE_TYPE) == 0)
					{
						if (structureRegion.isNetworked)
						{
							return;
						}
					}
					else if (!structureRegion.isNetworked)
					{
						return;
					}
					structureRegion.isNetworked = true;
					ushort num = (ushort)base.channel.read(Types.UINT16_TYPE);
					for (int i = 0; i < (int)num; i++)
					{
						object[] array = base.channel.read(new Type[]
						{
							Types.UINT16_TYPE,
							Types.VECTOR3_TYPE,
							Types.BYTE_TYPE,
							Types.BYTE_TYPE,
							Types.BYTE_TYPE,
							Types.UINT64_TYPE,
							Types.UINT64_TYPE,
							Types.UINT32_TYPE
						});
						ulong owner = (ulong)array[5];
						ulong group = (ulong)array[6];
						uint instanceID = (uint)array[7];
						byte hp = (byte)base.channel.read(Types.BYTE_TYPE);
						this.spawnStructure(structureRegion, (ushort)array[0], (Vector3)array[1], (byte)array[2], (byte)array[3], (byte)array[4], hp, owner, group, instanceID);
					}
					Level.isLoadingStructures = false;
				}
			}
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x000FCB44 File Offset: 0x000FAF44
		[SteamCall]
		public void askStructures(CSteamID steamID, byte x, byte y)
		{
			StructureRegion structureRegion;
			if (StructureManager.tryGetRegion(x, y, out structureRegion))
			{
				if (structureRegion.structures.Count > 0)
				{
					byte b = 0;
					int i = 0;
					int j = 0;
					while (i < structureRegion.structures.Count)
					{
						int num = 0;
						while (j < structureRegion.structures.Count)
						{
							num += 38;
							j++;
							if (num > Block.BUFFER_SIZE / 2)
							{
								break;
							}
						}
						base.channel.openWrite();
						base.channel.write(x);
						base.channel.write(y);
						base.channel.write(b);
						base.channel.write((ushort)(j - i));
						while (i < j)
						{
							StructureData structureData = structureRegion.structures[i];
							base.channel.write(new object[]
							{
								structureData.structure.id,
								structureData.point,
								structureData.angle_x,
								structureData.angle_y,
								structureData.angle_z,
								structureData.owner,
								structureData.group,
								structureRegion.drops[i].instanceID
							});
							base.channel.write((byte)Mathf.RoundToInt((float)structureData.structure.health / (float)structureData.structure.asset.health * 100f));
							i++;
						}
						base.channel.closeWrite("tellStructures", steamID, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
						b += 1;
					}
				}
				else
				{
					base.channel.openWrite();
					base.channel.write(x);
					base.channel.write(y);
					base.channel.write(0);
					base.channel.write(0);
					base.channel.closeWrite("tellStructures", steamID, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
				}
			}
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x000FCD8C File Offset: 0x000FB18C
		private static void updateActivity(StructureRegion region, CSteamID owner, CSteamID group)
		{
			ushort num = 0;
			while ((int)num < region.structures.Count)
			{
				StructureData structureData = region.structures[(int)num];
				if (OwnershipTool.checkToggle(owner, structureData.owner, group, structureData.group))
				{
					structureData.objActiveDate = Provider.time;
				}
				num += 1;
			}
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x000FCDE8 File Offset: 0x000FB1E8
		private static void updateActivity(CSteamID owner, CSteamID group)
		{
			for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
			{
				for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
				{
					StructureRegion region = StructureManager.regions[(int)b, (int)b2];
					StructureManager.updateActivity(region, owner, group);
				}
			}
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x000FCE38 File Offset: 0x000FB238
		private void onLevelLoaded(int level)
		{
			if (level > Level.SETUP)
			{
				StructureManager.regions = new StructureRegion[(int)Regions.WORLD_SIZE, (int)Regions.WORLD_SIZE];
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						StructureManager.regions[(int)b, (int)b2] = new StructureRegion();
					}
				}
				StructureManager.structureColliders = new List<Collider>();
				StructureManager.instanceCount = 0u;
				if (Provider.isServer)
				{
					StructureManager.load();
				}
			}
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x000FCEC4 File Offset: 0x000FB2C4
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
							if (player.movement.loadedRegions[(int)b, (int)b2].isStructuresLoaded && !Regions.checkArea(b, b2, new_x, new_y, StructureManager.STRUCTURE_REGIONS))
							{
								player.movement.loadedRegions[(int)b, (int)b2].isStructuresLoaded = false;
							}
						}
						else if (player.channel.isOwner && StructureManager.regions[(int)b, (int)b2].isNetworked && !Regions.checkArea(b, b2, new_x, new_y, StructureManager.STRUCTURE_REGIONS))
						{
							StructureManager.regions[(int)b, (int)b2].destroy();
							StructureManager.regions[(int)b, (int)b2].isNetworked = false;
						}
					}
				}
			}
			if (step == 1 && Dedicator.isDedicated && Regions.checkSafe((int)new_x, (int)new_y))
			{
				for (int i = (int)(new_x - StructureManager.STRUCTURE_REGIONS); i <= (int)(new_x + StructureManager.STRUCTURE_REGIONS); i++)
				{
					for (int j = (int)(new_y - StructureManager.STRUCTURE_REGIONS); j <= (int)(new_y + StructureManager.STRUCTURE_REGIONS); j++)
					{
						if (Regions.checkSafe((int)((byte)i), (int)((byte)j)) && !player.movement.loadedRegions[i, j].isStructuresLoaded)
						{
							player.movement.loadedRegions[i, j].isStructuresLoaded = true;
							this.askStructures(player.channel.owner.playerID.steamID, (byte)i, (byte)j);
						}
					}
				}
			}
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x000FD080 File Offset: 0x000FB480
		private void onPlayerCreated(Player player)
		{
			PlayerMovement movement = player.movement;
			movement.onRegionUpdated = (PlayerRegionUpdated)Delegate.Combine(movement.onRegionUpdated, new PlayerRegionUpdated(this.onRegionUpdated));
			if (Provider.isServer)
			{
				SteamPlayerID playerID = player.channel.owner.playerID;
				StructureManager.updateActivity(playerID.steamID, player.quests.groupID);
			}
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x000FD0E8 File Offset: 0x000FB4E8
		private void Start()
		{
			StructureManager.manager = this;
			Level.onLevelLoaded = (LevelLoaded)Delegate.Combine(Level.onLevelLoaded, new LevelLoaded(this.onLevelLoaded));
			Player.onPlayerCreated = (PlayerCreated)Delegate.Combine(Player.onPlayerCreated, new PlayerCreated(this.onPlayerCreated));
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x000FD13C File Offset: 0x000FB53C
		public static void load()
		{
			bool flag = false;
			if (LevelSavedata.fileExists("/Structures.dat") && Level.info.type == ELevelType.SURVIVAL)
			{
				River river = LevelSavedata.openRiver("/Structures.dat", true);
				byte b = river.readByte();
				if (b > 3)
				{
					StructureManager.serverActiveDate = river.readUInt32();
				}
				else
				{
					StructureManager.serverActiveDate = Provider.time;
				}
				if (b > 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						for (byte b3 = 0; b3 < Regions.WORLD_SIZE; b3 += 1)
						{
							StructureRegion region = StructureManager.regions[(int)b2, (int)b3];
							StructureManager.loadRegion(b, river, region);
						}
					}
				}
				if (b < 6)
				{
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
			if (flag && LevelObjects.buildables != null)
			{
				for (byte b4 = 0; b4 < Regions.WORLD_SIZE; b4 += 1)
				{
					for (byte b5 = 0; b5 < Regions.WORLD_SIZE; b5 += 1)
					{
						List<LevelBuildableObject> list = LevelObjects.buildables[(int)b4, (int)b5];
						if (list != null && list.Count != 0)
						{
							StructureRegion structureRegion = StructureManager.regions[(int)b4, (int)b5];
							for (int i = 0; i < list.Count; i++)
							{
								LevelBuildableObject levelBuildableObject = list[i];
								if (levelBuildableObject != null)
								{
									ItemStructureAsset itemStructureAsset = levelBuildableObject.asset as ItemStructureAsset;
									if (itemStructureAsset != null)
									{
										Vector3 eulerAngles = levelBuildableObject.rotation.eulerAngles;
										byte newAngle_X = MeasurementTool.angleToByte((float)(Mathf.RoundToInt(eulerAngles.x / 2f) * 2));
										byte newAngle_Y = MeasurementTool.angleToByte((float)(Mathf.RoundToInt(eulerAngles.y / 2f) * 2));
										byte newAngle_Z = MeasurementTool.angleToByte((float)(Mathf.RoundToInt(eulerAngles.z / 2f) * 2));
										Structure structure = new Structure(itemStructureAsset.id, itemStructureAsset.health, itemStructureAsset);
										StructureData structureData = new StructureData(structure, levelBuildableObject.point, newAngle_X, newAngle_Y, newAngle_Z, 0UL, 0UL, uint.MaxValue);
										structureRegion.structures.Add(structureData);
										StructureManager.manager.spawnStructure(structureRegion, structure.id, structureData.point, structureData.angle_x, structureData.angle_y, structureData.angle_z, (byte)Mathf.RoundToInt((float)structure.health / (float)itemStructureAsset.health * 100f), 0UL, 0UL, StructureManager.instanceCount += 1u);
									}
								}
							}
						}
					}
				}
			}
			Level.isLoadingStructures = false;
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000FD3D0 File Offset: 0x000FB7D0
		public static void save()
		{
			River river = LevelSavedata.openRiver("/Structures.dat", false);
			river.writeByte(StructureManager.SAVEDATA_VERSION);
			river.writeUInt32(Provider.time);
			for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
			{
				for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
				{
					StructureRegion region = StructureManager.regions[(int)b, (int)b2];
					StructureManager.saveRegion(river, region);
				}
			}
			river.closeRiver();
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000FD448 File Offset: 0x000FB848
		private static void loadRegion(byte version, River river, StructureRegion region)
		{
			ushort num = river.readUInt16();
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				ushort num3 = river.readUInt16();
				ushort num4 = river.readUInt16();
				Vector3 vector = river.readSingleVector3();
				byte b = 0;
				if (version > 4)
				{
					b = river.readByte();
				}
				byte b2 = river.readByte();
				byte b3 = 0;
				if (version > 4)
				{
					b3 = river.readByte();
				}
				ulong num5 = 0UL;
				ulong num6 = 0UL;
				if (version > 2)
				{
					num5 = river.readUInt64();
					num6 = river.readUInt64();
				}
				uint newObjActiveDate;
				if (version > 3)
				{
					newObjActiveDate = river.readUInt32();
					if (Provider.time - StructureManager.serverActiveDate > Provider.modeConfigData.Structures.Decay_Time / 2u)
					{
						newObjActiveDate = Provider.time;
					}
				}
				else
				{
					newObjActiveDate = Provider.time;
				}
				ItemStructureAsset itemStructureAsset;
				try
				{
					itemStructureAsset = (ItemStructureAsset)Assets.find(EAssetType.ITEM, num3);
				}
				catch
				{
					itemStructureAsset = null;
				}
				if (itemStructureAsset != null)
				{
					if (version < 5)
					{
						Vector3 eulerAngles = Quaternion.Euler(-90f, (float)(b2 * 2), 0f).eulerAngles;
						b = MeasurementTool.angleToByte((float)(Mathf.RoundToInt(eulerAngles.x / 2f) * 2));
						b2 = MeasurementTool.angleToByte((float)(Mathf.RoundToInt(eulerAngles.y / 2f) * 2));
						b3 = MeasurementTool.angleToByte((float)(Mathf.RoundToInt(eulerAngles.z / 2f) * 2));
					}
					region.structures.Add(new StructureData(new Structure(num3, num4, itemStructureAsset), vector, b, b2, b3, num5, num6, newObjActiveDate));
					StructureManager.manager.spawnStructure(region, num3, vector, b, b2, b3, (byte)Mathf.RoundToInt((float)num4 / (float)itemStructureAsset.health * 100f), num5, num6, StructureManager.instanceCount += 1u);
				}
			}
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000FD624 File Offset: 0x000FBA24
		private static void saveRegion(River river, StructureRegion region)
		{
			uint time = Provider.time;
			ushort num = 0;
			ushort num2 = 0;
			while ((int)num2 < region.structures.Count)
			{
				StructureData structureData = region.structures[(int)num2];
				if ((!Dedicator.isDedicated || Provider.modeConfigData.Structures.Decay_Time == 0u || time < structureData.objActiveDate || time - structureData.objActiveDate < Provider.modeConfigData.Structures.Decay_Time) && structureData.structure.asset.isSaveable)
				{
					num += 1;
				}
				num2 += 1;
			}
			river.writeUInt16(num);
			ushort num3 = 0;
			while ((int)num3 < region.structures.Count)
			{
				StructureData structureData2 = region.structures[(int)num3];
				if ((!Dedicator.isDedicated || Provider.modeConfigData.Structures.Decay_Time == 0u || time < structureData2.objActiveDate || time - structureData2.objActiveDate < Provider.modeConfigData.Structures.Decay_Time) && structureData2.structure.asset.isSaveable)
				{
					river.writeUInt16(structureData2.structure.id);
					river.writeUInt16(structureData2.structure.health);
					river.writeSingleVector3(structureData2.point);
					river.writeByte(structureData2.angle_x);
					river.writeByte(structureData2.angle_y);
					river.writeByte(structureData2.angle_z);
					river.writeUInt64(structureData2.owner);
					river.writeUInt64(structureData2.group);
					river.writeUInt32(structureData2.objActiveDate);
				}
				num3 += 1;
			}
		}

		// Token: 0x040019AA RID: 6570
		public static readonly byte SAVEDATA_VERSION = 6;

		// Token: 0x040019AB RID: 6571
		public static readonly byte STRUCTURE_REGIONS = 2;

		// Token: 0x040019AC RID: 6572
		public static readonly float WALL = 3f;

		// Token: 0x040019AD RID: 6573
		public static readonly float PILLAR = 3.1f;

		// Token: 0x040019AE RID: 6574
		public static readonly float HEIGHT = 2.125f;

		// Token: 0x040019AF RID: 6575
		public static SalvageStructureRequestHandler onSalvageStructureRequested;

		// Token: 0x040019B0 RID: 6576
		private static StructureManager manager;

		// Token: 0x040019B2 RID: 6578
		private static List<Collider> structureColliders;

		// Token: 0x040019B3 RID: 6579
		private static uint instanceCount;

		// Token: 0x040019B4 RID: 6580
		private static uint serverActiveDate;
	}
}
