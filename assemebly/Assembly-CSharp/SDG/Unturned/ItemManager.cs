using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005A1 RID: 1441
	public class ItemManager : SteamCaller
	{
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x000F35CD File Offset: 0x000F19CD
		public static ItemManager instance
		{
			get
			{
				return ItemManager.manager;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x0600283B RID: 10299 RVA: 0x000F35D4 File Offset: 0x000F19D4
		// (set) Token: 0x0600283C RID: 10300 RVA: 0x000F35DB File Offset: 0x000F19DB
		public static ItemRegion[,] regions { get; private set; }

		// Token: 0x0600283D RID: 10301 RVA: 0x000F35E4 File Offset: 0x000F19E4
		public static void getItemsInRadius(Vector3 center, float sqrRadius, List<RegionCoordinate> search, List<InteractableItem> result)
		{
			if (ItemManager.regions == null)
			{
				return;
			}
			for (int i = 0; i < search.Count; i++)
			{
				RegionCoordinate regionCoordinate = search[i];
				if (ItemManager.regions[(int)regionCoordinate.x, (int)regionCoordinate.y] != null)
				{
					for (int j = 0; j < ItemManager.regions[(int)regionCoordinate.x, (int)regionCoordinate.y].drops.Count; j++)
					{
						ItemDrop itemDrop = ItemManager.regions[(int)regionCoordinate.x, (int)regionCoordinate.y].drops[j];
						if ((itemDrop.model.position - center).sqrMagnitude < sqrRadius)
						{
							result.Add(itemDrop.interactableItem);
						}
					}
				}
			}
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000F36C0 File Offset: 0x000F1AC0
		public static void takeItem(Transform item, byte to_x, byte to_y, byte to_rot, byte to_page)
		{
			byte b;
			byte b2;
			if (Regions.tryGetCoordinate(item.position, out b, out b2))
			{
				ItemRegion itemRegion = ItemManager.regions[(int)b, (int)b2];
				for (int i = 0; i < itemRegion.drops.Count; i++)
				{
					if (itemRegion.drops[i].model == item)
					{
						ItemManager.manager.channel.send("askTakeItem", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
						{
							b,
							b2,
							itemRegion.drops[i].instanceID,
							to_x,
							to_y,
							to_rot,
							to_page
						});
						return;
					}
				}
			}
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x000F3794 File Offset: 0x000F1B94
		public static void dropItem(Item item, Vector3 point, bool playEffect, bool isDropped, bool wideSpread)
		{
			if (ItemManager.regions == null || ItemManager.manager == null)
			{
				return;
			}
			if (wideSpread)
			{
				point.x += UnityEngine.Random.Range(-0.75f, 0.75f);
				point.z += UnityEngine.Random.Range(-0.75f, 0.75f);
			}
			else
			{
				point.x += UnityEngine.Random.Range(-0.125f, 0.125f);
				point.z += UnityEngine.Random.Range(-0.125f, 0.125f);
			}
			byte b;
			byte b2;
			if (Regions.tryGetCoordinate(point, out b, out b2))
			{
				ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item.id);
				if (itemAsset != null && !itemAsset.isPro)
				{
					if (playEffect)
					{
						EffectManager.sendEffect(6, EffectManager.SMALL, point);
					}
					if (point.y > 0f)
					{
						RaycastHit raycastHit;
						Physics.Raycast(point + Vector3.up, Vector3.down, out raycastHit, Mathf.Min(point.y + 1f, Level.HEIGHT), RayMasks.BLOCK_ITEM);
						if (raycastHit.collider != null)
						{
							point.y = raycastHit.point.y;
						}
					}
					ItemData itemData = new ItemData(item, ItemManager.instanceCount += 1u, point, isDropped);
					ItemManager.regions[(int)b, (int)b2].items.Add(itemData);
					ItemManager.manager.channel.send("tellItem", ESteamCall.CLIENTS, b, b2, ItemManager.ITEM_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						item.id,
						item.amount,
						item.quality,
						item.state,
						point,
						itemData.instanceID
					});
				}
			}
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x000F39A0 File Offset: 0x000F1DA0
		[SteamCall]
		public void tellTakeItem(CSteamID steamID, byte x, byte y, uint instanceID)
		{
			if (base.channel.checkServer(steamID))
			{
				if (!Provider.isServer && !ItemManager.regions[(int)x, (int)y].isNetworked)
				{
					return;
				}
				ItemRegion itemRegion = ItemManager.regions[(int)x, (int)y];
				ushort num = 0;
				while ((int)num < itemRegion.drops.Count)
				{
					if (itemRegion.drops[(int)num].instanceID == instanceID)
					{
						if (ItemManager.onItemDropRemoved != null)
						{
							ItemManager.onItemDropRemoved(itemRegion.drops[(int)num].model, itemRegion.drops[(int)num].interactableItem);
						}
						UnityEngine.Object.Destroy(itemRegion.drops[(int)num].model.gameObject);
						itemRegion.drops.RemoveAt((int)num);
						return;
					}
					num += 1;
				}
			}
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x000F3A80 File Offset: 0x000F1E80
		[SteamCall]
		public void askTakeItem(CSteamID steamID, byte x, byte y, uint instanceID, byte to_x, byte to_y, byte to_rot, byte to_page)
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
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if (player.animator.gesture == EPlayerGesture.ARREST_START)
				{
					return;
				}
				ItemRegion itemRegion = ItemManager.regions[(int)x, (int)y];
				ushort num = 0;
				while ((int)num < itemRegion.items.Count)
				{
					if (itemRegion.items[(int)num].instanceID == instanceID)
					{
						if (Dedicator.isDedicated && (itemRegion.items[(int)num].point - player.transform.position).sqrMagnitude > 400f)
						{
							return;
						}
						bool flag;
						if (to_page == 255)
						{
							flag = player.inventory.tryAddItem(ItemManager.regions[(int)x, (int)y].items[(int)num].item, true);
						}
						else
						{
							flag = player.inventory.tryAddItem(ItemManager.regions[(int)x, (int)y].items[(int)num].item, to_x, to_y, to_page, to_rot);
						}
						if (flag)
						{
							if (!player.equipment.wasTryingToSelect && !player.equipment.isSelected)
							{
								player.animator.sendGesture(EPlayerGesture.PICKUP, true);
							}
							EffectManager.sendEffect(7, EffectManager.SMALL, ItemManager.regions[(int)x, (int)y].items[(int)num].point);
							ItemManager.regions[(int)x, (int)y].items.RemoveAt((int)num);
							player.sendStat(EPlayerStat.FOUND_ITEMS);
							base.channel.send("tellTakeItem", ESteamCall.CLIENTS, x, y, ItemManager.ITEM_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								x,
								y,
								instanceID
							});
						}
						else
						{
							player.sendMessage(EPlayerMessage.SPACE);
						}
						return;
					}
					else
					{
						num += 1;
					}
				}
			}
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000F3C9C File Offset: 0x000F209C
		[SteamCall]
		public void tellClearRegionItems(CSteamID steamID, byte x, byte y)
		{
			if (base.channel.checkServer(steamID))
			{
				if (!Provider.isServer && !ItemManager.regions[(int)x, (int)y].isNetworked)
				{
					return;
				}
				ItemRegion itemRegion = ItemManager.regions[(int)x, (int)y];
				itemRegion.destroy();
			}
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x000F3CF0 File Offset: 0x000F20F0
		public static void askClearRegionItems(byte x, byte y)
		{
			if (Provider.isServer)
			{
				if (!Regions.checkSafe((int)x, (int)y))
				{
					return;
				}
				ItemRegion itemRegion = ItemManager.regions[(int)x, (int)y];
				if (itemRegion.items.Count > 0)
				{
					itemRegion.items.Clear();
					ItemManager.manager.channel.send("tellClearRegionItems", ESteamCall.CLIENTS, x, y, ItemManager.ITEM_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						x,
						y
					});
				}
			}
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000F3D74 File Offset: 0x000F2174
		public static void askClearAllItems()
		{
			if (Provider.isServer)
			{
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						ItemManager.askClearRegionItems(b, b2);
					}
				}
			}
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000F3DC0 File Offset: 0x000F21C0
		private void spawnItem(byte x, byte y, ushort id, byte amount, byte quality, byte[] state, Vector3 point, uint instanceID)
		{
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, id);
			if (itemAsset != null)
			{
				Transform transform = new GameObject().transform;
				transform.name = id.ToString();
				transform.transform.parent = LevelItems.models;
				transform.transform.position = point;
				Transform item = ItemTool.getItem(id, 0, quality, state, false, itemAsset, null);
				item.parent = transform;
				InteractableItem interactableItem = item.gameObject.AddComponent<InteractableItem>();
				interactableItem.item = new Item(id, amount, quality, state);
				interactableItem.asset = itemAsset;
				item.position = point + Vector3.up * 0.75f;
				item.rotation = Quaternion.Euler((float)(-90 + UnityEngine.Random.Range(-15, 15)), (float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(-15, 15));
				item.gameObject.AddComponent<Rigidbody>();
				item.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
				item.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
				item.GetComponent<Rigidbody>().drag = 0.5f;
				item.GetComponent<Rigidbody>().angularDrag = 0.1f;
				if (LevelObjects.loads[(int)x, (int)y] != -1)
				{
					item.GetComponent<Rigidbody>().useGravity = false;
					item.GetComponent<Rigidbody>().isKinematic = true;
				}
				ItemDrop item2 = new ItemDrop(transform, interactableItem, instanceID);
				ItemManager.regions[(int)x, (int)y].drops.Add(item2);
				if (ItemManager.onItemDropAdded != null)
				{
					ItemManager.onItemDropAdded(item, interactableItem);
				}
			}
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x000F3F48 File Offset: 0x000F2348
		[SteamCall]
		public void tellItem(CSteamID steamID, byte x, byte y, ushort id, byte amount, byte quality, byte[] state, Vector3 point, uint instanceID)
		{
			if (base.channel.checkServer(steamID))
			{
				if (!Regions.checkSafe((int)x, (int)y))
				{
					return;
				}
				if (!ItemManager.regions[(int)x, (int)y].isNetworked)
				{
					return;
				}
				this.spawnItem(x, y, id, amount, quality, state, point, instanceID);
			}
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000F3FA0 File Offset: 0x000F23A0
		[SteamCall]
		public void tellItems(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID))
			{
				byte b = (byte)base.channel.read(Types.BYTE_TYPE);
				byte b2 = (byte)base.channel.read(Types.BYTE_TYPE);
				if (!Regions.checkSafe((int)b, (int)b2))
				{
					return;
				}
				if ((byte)base.channel.read(Types.BYTE_TYPE) == 0)
				{
					if (ItemManager.regions[(int)b, (int)b2].isNetworked)
					{
						return;
					}
				}
				else if (!ItemManager.regions[(int)b, (int)b2].isNetworked)
				{
					return;
				}
				ItemManager.regions[(int)b, (int)b2].isNetworked = true;
				ushort num = (ushort)base.channel.read(Types.UINT16_TYPE);
				for (int i = 0; i < (int)num; i++)
				{
					object[] array = base.channel.read(Types.UINT16_TYPE, Types.BYTE_TYPE, Types.BYTE_TYPE, Types.BYTE_ARRAY_TYPE, Types.VECTOR3_TYPE, Types.UINT32_TYPE);
					this.spawnItem(b, b2, (ushort)array[0], (byte)array[1], (byte)array[2], (byte[])array[3], (Vector3)array[4], (uint)array[5]);
				}
			}
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000F40F0 File Offset: 0x000F24F0
		public void askItems(CSteamID steamID, byte x, byte y)
		{
			if (ItemManager.regions[(int)x, (int)y].items.Count > 0)
			{
				byte b = 0;
				int i = 0;
				int j = 0;
				while (i < ItemManager.regions[(int)x, (int)y].items.Count)
				{
					int num = 0;
					while (j < ItemManager.regions[(int)x, (int)y].items.Count)
					{
						num += 4 + ItemManager.regions[(int)x, (int)y].items[j].item.state.Length + 12 + 4;
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
						ItemData itemData = ItemManager.regions[(int)x, (int)y].items[i];
						base.channel.write(itemData.item.id, itemData.item.amount, itemData.item.quality, itemData.item.state, itemData.point, itemData.instanceID);
						i++;
					}
					base.channel.closeWrite("tellItems", steamID, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
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
				base.channel.closeWrite("tellItems", steamID, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
			}
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000F4304 File Offset: 0x000F2704
		private bool despawnItems()
		{
			if (Level.info == null || Level.info.type == ELevelType.ARENA)
			{
				return false;
			}
			if (ItemManager.regions[(int)ItemManager.despawnItems_X, (int)ItemManager.despawnItems_Y].items.Count > 0)
			{
				for (int i = 0; i < ItemManager.regions[(int)ItemManager.despawnItems_X, (int)ItemManager.despawnItems_Y].items.Count; i++)
				{
					if (Time.realtimeSinceStartup - ItemManager.regions[(int)ItemManager.despawnItems_X, (int)ItemManager.despawnItems_Y].items[i].lastDropped > ((!ItemManager.regions[(int)ItemManager.despawnItems_X, (int)ItemManager.despawnItems_Y].items[i].isDropped) ? Provider.modeConfigData.Items.Despawn_Natural_Time : Provider.modeConfigData.Items.Despawn_Dropped_Time))
					{
						uint instanceID = ItemManager.regions[(int)ItemManager.despawnItems_X, (int)ItemManager.despawnItems_Y].items[i].instanceID;
						ItemManager.regions[(int)ItemManager.despawnItems_X, (int)ItemManager.despawnItems_Y].items.RemoveAt(i);
						base.channel.send("tellTakeItem", ESteamCall.CLIENTS, ItemManager.despawnItems_X, ItemManager.despawnItems_Y, ItemManager.ITEM_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							ItemManager.despawnItems_X,
							ItemManager.despawnItems_Y,
							instanceID
						});
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x000F4490 File Offset: 0x000F2890
		private bool respawnItems()
		{
			if (Level.info == null || Level.info.type == ELevelType.ARENA)
			{
				return false;
			}
			if (LevelItems.spawns[(int)ItemManager.respawnItems_X, (int)ItemManager.respawnItems_Y].Count > 0)
			{
				if (Time.realtimeSinceStartup - ItemManager.regions[(int)ItemManager.respawnItems_X, (int)ItemManager.respawnItems_Y].lastRespawn > Provider.modeConfigData.Items.Respawn_Time && (float)ItemManager.regions[(int)ItemManager.respawnItems_X, (int)ItemManager.respawnItems_Y].items.Count < (float)LevelItems.spawns[(int)ItemManager.respawnItems_X, (int)ItemManager.respawnItems_Y].Count * Provider.modeConfigData.Items.Spawn_Chance)
				{
					ItemManager.regions[(int)ItemManager.respawnItems_X, (int)ItemManager.respawnItems_Y].lastRespawn = Time.realtimeSinceStartup;
					ItemSpawnpoint itemSpawnpoint = LevelItems.spawns[(int)ItemManager.respawnItems_X, (int)ItemManager.respawnItems_Y][UnityEngine.Random.Range(0, LevelItems.spawns[(int)ItemManager.respawnItems_X, (int)ItemManager.respawnItems_Y].Count)];
					if (!SafezoneManager.checkPointValid(itemSpawnpoint.point))
					{
						return false;
					}
					ushort num = 0;
					while ((int)num < ItemManager.regions[(int)ItemManager.respawnItems_X, (int)ItemManager.respawnItems_Y].items.Count)
					{
						if ((ItemManager.regions[(int)ItemManager.respawnItems_X, (int)ItemManager.respawnItems_Y].items[(int)num].point - itemSpawnpoint.point).sqrMagnitude < 4f)
						{
							return false;
						}
						num += 1;
					}
					ushort item = LevelItems.getItem(itemSpawnpoint);
					ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item);
					if (itemAsset != null)
					{
						Item item2 = new Item(item, EItemOrigin.WORLD);
						ItemData itemData = new ItemData(item2, ItemManager.instanceCount += 1u, itemSpawnpoint.point, false);
						ItemManager.regions[(int)ItemManager.respawnItems_X, (int)ItemManager.respawnItems_Y].items.Add(itemData);
						ItemManager.manager.channel.send("tellItem", ESteamCall.CLIENTS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							ItemManager.respawnItems_X,
							ItemManager.respawnItems_Y,
							item2.id,
							item2.quality,
							item2.state,
							itemSpawnpoint.point,
							itemData.instanceID
						});
					}
					else
					{
						CommandWindow.LogError(string.Concat(new object[]
						{
							"Failed to respawn an item with ID ",
							item,
							" from type ",
							itemSpawnpoint.type,
							"!"
						}));
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x000F475C File Offset: 0x000F2B5C
		private void generateItems(byte x, byte y)
		{
			if (Level.info == null || Level.info.type == ELevelType.ARENA)
			{
				return;
			}
			List<ItemData> list = new List<ItemData>();
			if (LevelItems.spawns[(int)x, (int)y].Count > 0)
			{
				List<ItemSpawnpoint> list2 = new List<ItemSpawnpoint>();
				for (int i = 0; i < LevelItems.spawns[(int)x, (int)y].Count; i++)
				{
					ItemSpawnpoint itemSpawnpoint = LevelItems.spawns[(int)x, (int)y][i];
					if (SafezoneManager.checkPointValid(itemSpawnpoint.point))
					{
						list2.Add(itemSpawnpoint);
					}
				}
				while ((float)list.Count < (float)LevelItems.spawns[(int)x, (int)y].Count * Provider.modeConfigData.Items.Spawn_Chance && list2.Count > 0)
				{
					int index = UnityEngine.Random.Range(0, list2.Count);
					ItemSpawnpoint itemSpawnpoint2 = list2[index];
					list2.RemoveAt(index);
					ushort item = LevelItems.getItem(itemSpawnpoint2);
					ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item);
					if (itemAsset != null)
					{
						Item newItem = new Item(item, EItemOrigin.WORLD);
						list.Add(new ItemData(newItem, ItemManager.instanceCount += 1u, itemSpawnpoint2.point, false));
					}
					else
					{
						CommandWindow.LogError(string.Concat(new object[]
						{
							"Failed to generate an item with ID ",
							item,
							" from type ",
							itemSpawnpoint2.type,
							"!"
						}));
					}
				}
			}
			for (int j = 0; j < ItemManager.regions[(int)x, (int)y].items.Count; j++)
			{
				if (ItemManager.regions[(int)x, (int)y].items[j].isDropped)
				{
					list.Add(ItemManager.regions[(int)x, (int)y].items[j]);
				}
			}
			ItemManager.regions[(int)x, (int)y].items = list;
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x000F4960 File Offset: 0x000F2D60
		private void onLevelLoaded(int level)
		{
			if (level > Level.SETUP)
			{
				ItemManager.regions = new ItemRegion[(int)Regions.WORLD_SIZE, (int)Regions.WORLD_SIZE];
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						ItemManager.regions[(int)b, (int)b2] = new ItemRegion();
					}
				}
				ItemManager.clampedItems = new List<InteractableItem>();
				ItemManager.instanceCount = 0u;
				ItemManager.clampItemIndex = 0;
				ItemManager.despawnItems_X = 0;
				ItemManager.despawnItems_Y = 0;
				ItemManager.respawnItems_X = 0;
				ItemManager.respawnItems_Y = 0;
				if (Dedicator.isDedicated)
				{
					for (byte b3 = 0; b3 < Regions.WORLD_SIZE; b3 += 1)
					{
						for (byte b4 = 0; b4 < Regions.WORLD_SIZE; b4 += 1)
						{
							this.generateItems(b3, b4);
						}
					}
				}
			}
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x000F4A3C File Offset: 0x000F2E3C
		private void onRegionActivated(byte x, byte y)
		{
			if (ItemManager.regions != null && ItemManager.regions[(int)x, (int)y] != null)
			{
				for (int i = 0; i < ItemManager.regions[(int)x, (int)y].drops.Count; i++)
				{
					ItemDrop itemDrop = ItemManager.regions[(int)x, (int)y].drops[i];
					if (itemDrop != null && !(itemDrop.interactableItem == null))
					{
						Rigidbody component = itemDrop.interactableItem.GetComponent<Rigidbody>();
						if (!(component == null))
						{
							component.useGravity = true;
							component.isKinematic = false;
						}
					}
				}
			}
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000F4AEC File Offset: 0x000F2EEC
		private void onRegionUpdated(Player player, byte old_x, byte old_y, byte new_x, byte new_y, byte step, ref bool canIncrementIndex)
		{
			if (step == 0)
			{
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						if (player.channel.isOwner && ItemManager.regions[(int)b, (int)b2].isNetworked && !Regions.checkArea(b, b2, new_x, new_y, ItemManager.ITEM_REGIONS))
						{
							ItemManager.regions[(int)b, (int)b2].destroy();
							ItemManager.regions[(int)b, (int)b2].isNetworked = false;
						}
						if (Provider.isServer && player.movement.loadedRegions[(int)b, (int)b2].isItemsLoaded && !Regions.checkArea(b, b2, new_x, new_y, ItemManager.ITEM_REGIONS))
						{
							player.movement.loadedRegions[(int)b, (int)b2].isItemsLoaded = false;
						}
					}
				}
			}
			if (step == 5 && Provider.isServer && Regions.checkSafe((int)new_x, (int)new_y))
			{
				for (int i = (int)(new_x - ItemManager.ITEM_REGIONS); i <= (int)(new_x + ItemManager.ITEM_REGIONS); i++)
				{
					for (int j = (int)(new_y - ItemManager.ITEM_REGIONS); j <= (int)(new_y + ItemManager.ITEM_REGIONS); j++)
					{
						if (Regions.checkSafe((int)((byte)i), (int)((byte)j)) && !player.movement.loadedRegions[i, j].isItemsLoaded)
						{
							if (player.channel.isOwner)
							{
								this.generateItems((byte)i, (byte)j);
							}
							player.movement.loadedRegions[i, j].isItemsLoaded = true;
							this.askItems(player.channel.owner.playerID.steamID, (byte)i, (byte)j);
						}
					}
				}
			}
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000F4CBA File Offset: 0x000F30BA
		private void onPlayerCreated(Player player)
		{
			PlayerMovement movement = player.movement;
			movement.onRegionUpdated = (PlayerRegionUpdated)Delegate.Combine(movement.onRegionUpdated, new PlayerRegionUpdated(this.onRegionUpdated));
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000F4CE4 File Offset: 0x000F30E4
		private void Update()
		{
			if (!Provider.isServer && ItemManager.clampedItems != null && ItemManager.clampedItems.Count > 0)
			{
				if (ItemManager.clampItemIndex >= ItemManager.clampedItems.Count)
				{
					ItemManager.clampItemIndex = 0;
				}
				InteractableItem interactableItem = ItemManager.clampedItems[ItemManager.clampItemIndex];
				if (interactableItem != null)
				{
					interactableItem.clampRange();
				}
				ItemManager.clampItemIndex++;
			}
			if (!Dedicator.isDedicated || !Level.isLoaded)
			{
				return;
			}
			bool flag = true;
			while (flag)
			{
				flag = this.despawnItems();
				ItemManager.despawnItems_X += 1;
				if (ItemManager.despawnItems_X >= Regions.WORLD_SIZE)
				{
					ItemManager.despawnItems_X = 0;
					ItemManager.despawnItems_Y += 1;
					if (ItemManager.despawnItems_Y >= Regions.WORLD_SIZE)
					{
						ItemManager.despawnItems_Y = 0;
						flag = false;
					}
				}
			}
			bool flag2 = true;
			while (flag2)
			{
				flag2 = this.respawnItems();
				ItemManager.respawnItems_X += 1;
				if (ItemManager.respawnItems_X >= Regions.WORLD_SIZE)
				{
					ItemManager.respawnItems_X = 0;
					ItemManager.respawnItems_Y += 1;
					if (ItemManager.respawnItems_Y >= Regions.WORLD_SIZE)
					{
						ItemManager.respawnItems_Y = 0;
						flag2 = false;
					}
				}
			}
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000F4E28 File Offset: 0x000F3228
		private void Start()
		{
			ItemManager.manager = this;
			Level.onLevelLoaded = (LevelLoaded)Delegate.Combine(Level.onLevelLoaded, new LevelLoaded(this.onLevelLoaded));
			LevelObjects.onRegionActivated = (RegionActivated)Delegate.Combine(LevelObjects.onRegionActivated, new RegionActivated(this.onRegionActivated));
			Player.onPlayerCreated = (PlayerCreated)Delegate.Combine(Player.onPlayerCreated, new PlayerCreated(this.onPlayerCreated));
		}

		// Token: 0x04001917 RID: 6423
		public static readonly byte ITEM_REGIONS = 1;

		// Token: 0x04001918 RID: 6424
		public static ItemDropAdded onItemDropAdded;

		// Token: 0x04001919 RID: 6425
		public static ItemDropRemoved onItemDropRemoved;

		// Token: 0x0400191A RID: 6426
		private static ItemManager manager;

		// Token: 0x0400191C RID: 6428
		public static List<InteractableItem> clampedItems;

		// Token: 0x0400191D RID: 6429
		private static uint instanceCount;

		// Token: 0x0400191E RID: 6430
		private static int clampItemIndex;

		// Token: 0x0400191F RID: 6431
		private static byte despawnItems_X;

		// Token: 0x04001920 RID: 6432
		private static byte despawnItems_Y;

		// Token: 0x04001921 RID: 6433
		private static byte respawnItems_X;

		// Token: 0x04001922 RID: 6434
		private static byte respawnItems_Y;
	}
}
