using System;
using System.Collections.Generic;
using System.Text;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000589 RID: 1417
	public class BarricadeManager : SteamCaller
	{
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x0600271E RID: 10014 RVA: 0x000E8AD2 File Offset: 0x000E6ED2
		public static BarricadeManager instance
		{
			get
			{
				return BarricadeManager.manager;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x0600271F RID: 10015 RVA: 0x000E8AD9 File Offset: 0x000E6ED9
		// (set) Token: 0x06002720 RID: 10016 RVA: 0x000E8AE0 File Offset: 0x000E6EE0
		public static BarricadeRegion[,] regions { get; private set; }

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002721 RID: 10017 RVA: 0x000E8AE8 File Offset: 0x000E6EE8
		// (set) Token: 0x06002722 RID: 10018 RVA: 0x000E8AEF File Offset: 0x000E6EEF
		public static BarricadeRegion[,] BarricadeRegions
		{
			get
			{
				return BarricadeManager.regions;
			}
			set
			{
				BarricadeManager.regions = value;
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002723 RID: 10019 RVA: 0x000E8AF7 File Offset: 0x000E6EF7
		// (set) Token: 0x06002724 RID: 10020 RVA: 0x000E8AFE File Offset: 0x000E6EFE
		public static List<BarricadeRegion> plants { get; private set; }

		// Token: 0x06002725 RID: 10021 RVA: 0x000E8B08 File Offset: 0x000E6F08
		public static void getBarricadesInRadius(Vector3 center, float sqrRadius, List<RegionCoordinate> search, List<Transform> result)
		{
			if (BarricadeManager.regions == null)
			{
				return;
			}
			for (int i = 0; i < search.Count; i++)
			{
				RegionCoordinate regionCoordinate = search[i];
				if (BarricadeManager.regions[(int)regionCoordinate.x, (int)regionCoordinate.y] != null)
				{
					for (int j = 0; j < BarricadeManager.regions[(int)regionCoordinate.x, (int)regionCoordinate.y].drops.Count; j++)
					{
						Transform model = BarricadeManager.regions[(int)regionCoordinate.x, (int)regionCoordinate.y].drops[j].model;
						if ((model.position - center).sqrMagnitude < sqrRadius)
						{
							result.Add(model);
						}
					}
				}
			}
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000E8BE0 File Offset: 0x000E6FE0
		public static void getBarricadesInRadius(Vector3 center, float sqrRadius, ushort plant, List<Transform> result)
		{
			if (BarricadeManager.plants == null)
			{
				return;
			}
			if ((int)plant >= BarricadeManager.plants.Count)
			{
				return;
			}
			for (int i = 0; i < BarricadeManager.plants[(int)plant].drops.Count; i++)
			{
				Transform model = BarricadeManager.plants[(int)plant].drops[i].model;
				if ((model.position - center).sqrMagnitude < sqrRadius)
				{
					result.Add(model);
				}
			}
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000E8C6C File Offset: 0x000E706C
		public static void getBarricadesInRadius(Vector3 center, float sqrRadius, List<Transform> result)
		{
			if (BarricadeManager.plants == null)
			{
				return;
			}
			ushort num = 0;
			while ((int)num < BarricadeManager.plants.Count)
			{
				BarricadeManager.getBarricadesInRadius(center, sqrRadius, num, result);
				num += 1;
			}
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x000E8CAC File Offset: 0x000E70AC
		public static void transformBarricade(Transform barricade, Vector3 point, float angle_x, float angle_y, float angle_z)
		{
			angle_x = (float)(Mathf.RoundToInt(angle_x / 2f) * 2);
			angle_y = (float)(Mathf.RoundToInt(angle_y / 2f) * 2);
			angle_z = (float)(Mathf.RoundToInt(angle_z / 2f) * 2);
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			BarricadeDrop barricadeDrop;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion, out barricadeDrop))
			{
				BarricadeManager.manager.channel.send("askTransformBarricade", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					barricadeDrop.instanceID,
					point,
					MeasurementTool.angleToByte(angle_x),
					MeasurementTool.angleToByte(angle_y),
					MeasurementTool.angleToByte(angle_z)
				});
			}
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x000E8D80 File Offset: 0x000E7180
		[SteamCall]
		public void tellTransformBarricade(CSteamID steamID, byte x, byte y, ushort plant, uint instanceID, Vector3 point, byte angle_x, byte angle_y, byte angle_z)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				BarricadeData barricadeData = null;
				BarricadeDrop barricadeDrop = null;
				ushort num = 0;
				while ((int)num < barricadeRegion.drops.Count)
				{
					if (barricadeRegion.drops[(int)num].instanceID == instanceID)
					{
						if (Provider.isServer)
						{
							barricadeData = barricadeRegion.barricades[(int)num];
						}
						barricadeDrop = barricadeRegion.drops[(int)num];
						break;
					}
					num += 1;
				}
				if (barricadeDrop == null)
				{
					return;
				}
				barricadeDrop.model.position = point;
				barricadeDrop.model.rotation = Quaternion.Euler((float)(angle_x * 2), (float)(angle_y * 2), (float)(angle_z * 2));
				if (plant == 65535)
				{
					byte b;
					byte b2;
					if (Regions.tryGetCoordinate(point, out b, out b2) && (x != b || y != b2))
					{
						BarricadeRegion barricadeRegion2 = BarricadeManager.regions[(int)b, (int)b2];
						barricadeRegion.drops.RemoveAt((int)num);
						if (barricadeRegion2.isNetworked || Provider.isServer)
						{
							barricadeRegion2.drops.Add(barricadeDrop);
						}
						else if (!Provider.isServer)
						{
							UnityEngine.Object.Destroy(barricadeDrop.model.gameObject);
						}
						if (Provider.isServer)
						{
							barricadeRegion.barricades.RemoveAt((int)num);
							barricadeRegion2.barricades.Add(barricadeData);
						}
					}
					if (Provider.isServer)
					{
						barricadeData.point = point;
						barricadeData.angle_x = angle_x;
						barricadeData.angle_y = angle_y;
						barricadeData.angle_z = angle_z;
					}
				}
				else if (Provider.isServer)
				{
					barricadeData.point = barricadeDrop.model.localPosition;
					Vector3 eulerAngles = barricadeDrop.model.localRotation.eulerAngles;
					barricadeData.angle_x = MeasurementTool.angleToByte((float)(Mathf.RoundToInt(eulerAngles.x / 2f) * 2));
					barricadeData.angle_y = MeasurementTool.angleToByte((float)(Mathf.RoundToInt(eulerAngles.y / 2f) * 2));
					barricadeData.angle_z = MeasurementTool.angleToByte((float)(Mathf.RoundToInt(eulerAngles.z / 2f) * 2));
				}
			}
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x000E8FC8 File Offset: 0x000E73C8
		[SteamCall]
		public void askTransformBarricade(CSteamID steamID, byte x, byte y, ushort plant, uint instanceID, Vector3 point, byte angle_x, byte angle_y, byte angle_z)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if (plant == 65535)
				{
					BarricadeManager.manager.channel.send("tellTransformBarricade", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						x,
						y,
						plant,
						instanceID,
						point,
						angle_x,
						angle_y,
						angle_z
					});
				}
				else
				{
					BarricadeManager.manager.channel.send("tellTransformBarricade", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						x,
						y,
						plant,
						instanceID,
						point,
						angle_x,
						angle_y,
						angle_z
					});
				}
			}
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x000E9118 File Offset: 0x000E7518
		public static void poseMannequin(Transform barricade, byte poseComp)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askPoseMannequin", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2,
					poseComp
				});
			}
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x000E9184 File Offset: 0x000E7584
		[SteamCall]
		public void tellPoseMannequin(CSteamID steamID, byte x, byte y, ushort plant, ushort index, byte poseComp)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableMannequin interactableMannequin = barricadeRegion.drops[(int)index].interactable as InteractableMannequin;
				if (interactableMannequin != null)
				{
					interactableMannequin.setPose(poseComp);
				}
			}
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x000E9208 File Offset: 0x000E7608
		[SteamCall]
		public void askPoseMannequin(CSteamID steamID, byte x, byte y, ushort plant, ushort index, byte poseComp)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableMannequin interactableMannequin = barricadeRegion.drops[(int)index].interactable as InteractableMannequin;
				if (interactableMannequin != null && interactableMannequin.checkUpdate(player.channel.owner.playerID.steamID, player.quests.groupID))
				{
					if (plant == 65535)
					{
						BarricadeManager.manager.channel.send("tellPoseMannequin", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							poseComp
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellPoseMannequin", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							poseComp
						});
					}
					interactableMannequin.rebuildState();
				}
			}
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x000E93BC File Offset: 0x000E77BC
		[SteamCall]
		public void tellUpdateMannequin(CSteamID steamID, byte x, byte y, ushort plant, ushort index, byte[] state)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableMannequin interactableMannequin = barricadeRegion.drops[(int)index].interactable as InteractableMannequin;
				if (interactableMannequin != null)
				{
					interactableMannequin.updateState(state);
				}
			}
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000E9440 File Offset: 0x000E7840
		public static void updateMannequin(Transform barricade, EMannequinUpdateMode updateMode)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askUpdateMannequin", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2,
					(byte)updateMode
				});
			}
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x000E94B0 File Offset: 0x000E78B0
		[SteamCall]
		public void askUpdateMannequin(CSteamID steamID, byte x, byte y, ushort plant, ushort index, byte mode)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if (player.equipment.isBusy)
				{
					return;
				}
				if (player.equipment.isSelected && !player.equipment.isEquipped)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableMannequin interactableMannequin = barricadeRegion.drops[(int)index].interactable as InteractableMannequin;
				if (interactableMannequin != null && interactableMannequin.isUpdatable && interactableMannequin.checkUpdate(player.channel.owner.playerID.steamID, player.quests.groupID))
				{
					switch (mode)
					{
					case 0:
						interactableMannequin.updateVisuals(player.clothing.visualShirt, player.clothing.visualPants, player.clothing.visualHat, player.clothing.visualBackpack, player.clothing.visualVest, player.clothing.visualMask, player.clothing.visualGlasses);
						if (interactableMannequin.shirt != 0)
						{
							player.inventory.forceAddItem(new Item(interactableMannequin.shirt, 1, interactableMannequin.shirtQuality, interactableMannequin.shirtState), false);
						}
						if (interactableMannequin.pants != 0)
						{
							player.inventory.forceAddItem(new Item(interactableMannequin.pants, 1, interactableMannequin.pantsQuality, interactableMannequin.pantsState), false);
						}
						if (interactableMannequin.hat != 0)
						{
							player.inventory.forceAddItem(new Item(interactableMannequin.hat, 1, interactableMannequin.hatQuality, interactableMannequin.hatState), false);
						}
						if (interactableMannequin.backpack != 0)
						{
							player.inventory.forceAddItem(new Item(interactableMannequin.backpack, 1, interactableMannequin.backpackQuality, interactableMannequin.backpackState), false);
						}
						if (interactableMannequin.vest != 0)
						{
							player.inventory.forceAddItem(new Item(interactableMannequin.vest, 1, interactableMannequin.vestQuality, interactableMannequin.vestState), false);
						}
						if (interactableMannequin.mask != 0)
						{
							player.inventory.forceAddItem(new Item(interactableMannequin.mask, 1, interactableMannequin.maskQuality, interactableMannequin.maskState), false);
						}
						if (interactableMannequin.glasses != 0)
						{
							player.inventory.forceAddItem(new Item(interactableMannequin.glasses, 1, interactableMannequin.glassesQuality, interactableMannequin.glassesState), false);
						}
						interactableMannequin.clearClothes();
						break;
					case 1:
					{
						if (!player.equipment.isSelected || !player.equipment.isEquipped || player.equipment.isBusy || player.equipment.asset == null || !(player.equipment.useable is UseableClothing))
						{
							return;
						}
						ItemJar item = player.inventory.getItem(player.equipment.equippedPage, player.inventory.getIndex(player.equipment.equippedPage, player.equipment.equipped_x, player.equipment.equipped_y));
						if (item == null || item.item == null)
						{
							return;
						}
						interactableMannequin.clearVisuals();
						switch (player.equipment.asset.type)
						{
						case EItemType.HAT:
							if (interactableMannequin.hat != 0)
							{
								player.inventory.forceAddItem(new Item(interactableMannequin.hat, 1, interactableMannequin.hatQuality, interactableMannequin.hatState), false);
							}
							interactableMannequin.clothes.hat = item.item.id;
							interactableMannequin.hatQuality = item.item.quality;
							interactableMannequin.hatState = item.item.state;
							break;
						case EItemType.PANTS:
							if (interactableMannequin.pants != 0)
							{
								player.inventory.forceAddItem(new Item(interactableMannequin.pants, 1, interactableMannequin.pantsQuality, interactableMannequin.pantsState), false);
							}
							interactableMannequin.clothes.pants = item.item.id;
							interactableMannequin.pantsQuality = item.item.quality;
							interactableMannequin.pantsState = item.item.state;
							break;
						case EItemType.SHIRT:
							if (interactableMannequin.shirt != 0)
							{
								player.inventory.forceAddItem(new Item(interactableMannequin.shirt, 1, interactableMannequin.shirtQuality, interactableMannequin.shirtState), false);
							}
							interactableMannequin.clothes.shirt = item.item.id;
							interactableMannequin.shirtQuality = item.item.quality;
							interactableMannequin.shirtState = item.item.state;
							break;
						case EItemType.MASK:
							if (interactableMannequin.mask != 0)
							{
								player.inventory.forceAddItem(new Item(interactableMannequin.mask, 1, interactableMannequin.maskQuality, interactableMannequin.maskState), false);
							}
							interactableMannequin.clothes.mask = item.item.id;
							interactableMannequin.maskQuality = item.item.quality;
							interactableMannequin.maskState = item.item.state;
							break;
						case EItemType.BACKPACK:
							if (interactableMannequin.backpack != 0)
							{
								player.inventory.forceAddItem(new Item(interactableMannequin.backpack, 1, interactableMannequin.backpackQuality, interactableMannequin.backpackState), false);
							}
							interactableMannequin.clothes.backpack = item.item.id;
							interactableMannequin.backpackQuality = item.item.quality;
							interactableMannequin.backpackState = item.item.state;
							break;
						case EItemType.VEST:
							if (interactableMannequin.vest != 0)
							{
								player.inventory.forceAddItem(new Item(interactableMannequin.vest, 1, interactableMannequin.vestQuality, interactableMannequin.vestState), false);
							}
							interactableMannequin.clothes.vest = item.item.id;
							interactableMannequin.vestQuality = item.item.quality;
							interactableMannequin.vestState = item.item.state;
							break;
						case EItemType.GLASSES:
							if (interactableMannequin.glasses != 0)
							{
								player.inventory.forceAddItem(new Item(interactableMannequin.glasses, 1, interactableMannequin.glassesQuality, interactableMannequin.glassesState), false);
							}
							interactableMannequin.clothes.glasses = item.item.id;
							interactableMannequin.glassesQuality = item.item.quality;
							interactableMannequin.glassesState = item.item.state;
							break;
						default:
							return;
						}
						player.equipment.use();
						break;
					}
					case 2:
						interactableMannequin.clearVisuals();
						if (interactableMannequin.shirt != 0)
						{
							player.inventory.forceAddItem(new Item(interactableMannequin.shirt, 1, interactableMannequin.shirtQuality, interactableMannequin.shirtState), true, false);
						}
						if (interactableMannequin.pants != 0)
						{
							player.inventory.forceAddItem(new Item(interactableMannequin.pants, 1, interactableMannequin.pantsQuality, interactableMannequin.pantsState), true, false);
						}
						if (interactableMannequin.hat != 0)
						{
							player.inventory.forceAddItem(new Item(interactableMannequin.hat, 1, interactableMannequin.hatQuality, interactableMannequin.hatState), true, false);
						}
						if (interactableMannequin.backpack != 0)
						{
							player.inventory.forceAddItem(new Item(interactableMannequin.backpack, 1, interactableMannequin.backpackQuality, interactableMannequin.backpackState), true, false);
						}
						if (interactableMannequin.vest != 0)
						{
							player.inventory.forceAddItem(new Item(interactableMannequin.vest, 1, interactableMannequin.vestQuality, interactableMannequin.vestState), true, false);
						}
						if (interactableMannequin.mask != 0)
						{
							player.inventory.forceAddItem(new Item(interactableMannequin.mask, 1, interactableMannequin.maskQuality, interactableMannequin.maskState), true, false);
						}
						if (interactableMannequin.glasses != 0)
						{
							player.inventory.forceAddItem(new Item(interactableMannequin.glasses, 1, interactableMannequin.glassesQuality, interactableMannequin.glassesState), true, false);
						}
						interactableMannequin.clearClothes();
						break;
					case 3:
					{
						interactableMannequin.clearVisuals();
						ushort shirt = player.clothing.shirt;
						byte shirtQuality = player.clothing.shirtQuality;
						byte[] shirtState = player.clothing.shirtState;
						ushort pants = player.clothing.pants;
						byte pantsQuality = player.clothing.pantsQuality;
						byte[] pantsState = player.clothing.pantsState;
						ushort hat = player.clothing.hat;
						byte hatQuality = player.clothing.hatQuality;
						byte[] hatState = player.clothing.hatState;
						ushort backpack = player.clothing.backpack;
						byte backpackQuality = player.clothing.backpackQuality;
						byte[] backpackState = player.clothing.backpackState;
						ushort vest = player.clothing.vest;
						byte vestQuality = player.clothing.vestQuality;
						byte[] vestState = player.clothing.vestState;
						ushort mask = player.clothing.mask;
						byte maskQuality = player.clothing.maskQuality;
						byte[] maskState = player.clothing.maskState;
						ushort glasses = player.clothing.glasses;
						byte glassesQuality = player.clothing.glassesQuality;
						byte[] glassesState = player.clothing.glassesState;
						player.clothing.updateClothes(interactableMannequin.shirt, interactableMannequin.shirtQuality, interactableMannequin.shirtState, interactableMannequin.pants, interactableMannequin.pantsQuality, interactableMannequin.pantsState, interactableMannequin.hat, interactableMannequin.hatQuality, interactableMannequin.hatState, interactableMannequin.backpack, interactableMannequin.backpackQuality, interactableMannequin.backpackState, interactableMannequin.vest, interactableMannequin.vestQuality, interactableMannequin.vestState, interactableMannequin.mask, interactableMannequin.maskQuality, interactableMannequin.maskState, interactableMannequin.glasses, interactableMannequin.glassesQuality, interactableMannequin.glassesState);
						interactableMannequin.updateClothes(shirt, shirtQuality, shirtState, pants, pantsQuality, pantsState, hat, hatQuality, hatState, backpack, backpackQuality, backpackState, vest, vestQuality, vestState, mask, maskQuality, maskState, glasses, glassesQuality, glassesState);
						break;
					}
					default:
						return;
					}
					interactableMannequin.rebuildState();
					if (plant == 65535)
					{
						BarricadeManager.manager.channel.send("tellUpdateMannequin", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							barricadeRegion.barricades[(int)index].barricade.state
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellUpdateMannequin", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							barricadeRegion.barricades[(int)index].barricade.state
						});
					}
					EffectManager.sendEffect(9, EffectManager.SMALL, interactableMannequin.transform.position);
				}
			}
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x000E9FDC File Offset: 0x000E83DC
		public static void rotDisplay(Transform barricade, byte rotComp)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askRotDisplay", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2,
					rotComp
				});
			}
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x000EA048 File Offset: 0x000E8448
		[SteamCall]
		public void tellRotDisplay(CSteamID steamID, byte x, byte y, ushort plant, ushort index, byte rotComp)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableStorage interactableStorage = barricadeRegion.drops[(int)index].interactable as InteractableStorage;
				if (interactableStorage != null)
				{
					interactableStorage.setRotation(rotComp);
				}
			}
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x000EA0CC File Offset: 0x000E84CC
		[SteamCall]
		public void askRotDisplay(CSteamID steamID, byte x, byte y, ushort plant, ushort index, byte rotComp)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableStorage interactableStorage = barricadeRegion.drops[(int)index].interactable as InteractableStorage;
				if (interactableStorage != null && interactableStorage.checkRot(player.channel.owner.playerID.steamID, player.quests.groupID) && interactableStorage.isDisplay)
				{
					if (plant == 65535)
					{
						BarricadeManager.manager.channel.send("tellRotDisplay", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							rotComp
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellRotDisplay", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							rotComp
						});
					}
					interactableStorage.rebuildState();
				}
			}
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x000EA288 File Offset: 0x000E8688
		[SteamCall]
		public void tellBarricadeHealth(CSteamID steamID, byte x, byte y, ushort plant, ushort index, byte hp)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				Interactable2HP component = barricadeRegion.drops[(int)index].model.GetComponent<Interactable2HP>();
				if (component != null)
				{
					component.hp = hp;
				}
			}
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x000EA30C File Offset: 0x000E870C
		public static void salvageBarricade(Transform barricade)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askSalvageBarricade", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2
				});
			}
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x000EA370 File Offset: 0x000E8770
		[SteamCall]
		public void askSalvageBarricade(CSteamID steamID, byte x, byte y, ushort plant, ushort index)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if (!OwnershipTool.checkToggle(player.channel.owner.playerID.steamID, barricadeRegion.barricades[(int)index].owner, player.quests.groupID, barricadeRegion.barricades[(int)index].group))
				{
					return;
				}
				bool flag = true;
				if (BarricadeManager.onSalvageBarricadeRequested != null)
				{
					BarricadeManager.onSalvageBarricadeRequested(steamID, x, y, plant, index, ref flag);
				}
				if (!flag)
				{
					return;
				}
				ItemBarricadeAsset itemBarricadeAsset = (ItemBarricadeAsset)Assets.find(EAssetType.ITEM, barricadeRegion.barricades[(int)index].barricade.id);
				if (itemBarricadeAsset != null)
				{
					if (itemBarricadeAsset.isUnpickupable)
					{
						return;
					}
					if (barricadeRegion.barricades[(int)index].barricade.health >= itemBarricadeAsset.health)
					{
						player.inventory.forceAddItem(new Item(barricadeRegion.barricades[(int)index].barricade.id, EItemOrigin.NATURE), true);
					}
					else if (itemBarricadeAsset.isSalvageable)
					{
						for (int i = 0; i < itemBarricadeAsset.blueprints.Count; i++)
						{
							Blueprint blueprint = itemBarricadeAsset.blueprints[i];
							if (blueprint.outputs.Length == 1 && blueprint.outputs[0].id == itemBarricadeAsset.id)
							{
								ushort id = blueprint.supplies[UnityEngine.Random.Range(0, blueprint.supplies.Length)].id;
								player.inventory.forceAddItem(new Item(id, EItemOrigin.NATURE), true);
								break;
							}
						}
					}
				}
				barricadeRegion.barricades.RemoveAt((int)index);
				if (plant == 65535)
				{
					BarricadeManager.manager.channel.send("tellTakeBarricade", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						x,
						y,
						plant,
						index
					});
				}
				else
				{
					BarricadeManager.manager.channel.send("tellTakeBarricade", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						x,
						y,
						plant,
						index
					});
				}
			}
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x000EA61C File Offset: 0x000E8A1C
		[SteamCall]
		public void tellTank(CSteamID steamID, byte x, byte y, ushort plant, ushort index, ushort amount)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableTank interactableTank = barricadeRegion.drops[(int)index].interactable as InteractableTank;
				if (interactableTank != null)
				{
					interactableTank.updateAmount(amount);
				}
			}
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000EA6A0 File Offset: 0x000E8AA0
		public static void updateTank(Transform barricade, ushort amount)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				if (num == 65535)
				{
					BarricadeManager.manager.channel.send("tellTank", ESteamCall.ALL, b, b2, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						num,
						num2,
						amount
					});
				}
				else
				{
					BarricadeManager.manager.channel.send("tellTank", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						num,
						num2,
						amount
					});
				}
				byte[] bytes = BitConverter.GetBytes(amount);
				barricadeRegion.barricades[(int)num2].barricade.state[0] = bytes[0];
				barricadeRegion.barricades[(int)num2].barricade.state[1] = bytes[1];
			}
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000EA7B0 File Offset: 0x000E8BB0
		public static void updateSign(Transform barricade, string newText)
		{
			if (newText.Contains("<size"))
			{
				return;
			}
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askUpdateSign", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2,
					newText
				});
			}
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x000EA828 File Offset: 0x000E8C28
		[SteamCall]
		public void tellUpdateSign(CSteamID steamID, byte x, byte y, ushort plant, ushort index, string newText)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableSign interactableSign = barricadeRegion.drops[(int)index].interactable as InteractableSign;
				if (interactableSign != null)
				{
					interactableSign.updateText(newText);
				}
			}
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000EA8AC File Offset: 0x000E8CAC
		[SteamCall]
		public void askUpdateSign(CSteamID steamID, byte x, byte y, ushort plant, ushort index, string newText)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				int byteCount = Encoding.UTF8.GetByteCount(newText);
				if (byteCount > 255)
				{
					return;
				}
				if (newText.Contains("<size"))
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableSign interactableSign = barricadeRegion.drops[(int)index].interactable as InteractableSign;
				if (interactableSign != null && interactableSign.checkUpdate(player.channel.owner.playerID.steamID, player.quests.groupID))
				{
					if (plant == 65535)
					{
						BarricadeManager.manager.channel.send("tellUpdateSign", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							newText
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellUpdateSign", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							newText
						});
					}
					byte[] state = barricadeRegion.barricades[(int)index].barricade.state;
					byte[] bytes = Encoding.UTF8.GetBytes(newText);
					byte[] array = new byte[17 + bytes.Length];
					Buffer.BlockCopy(state, 0, array, 0, 16);
					array[16] = (byte)bytes.Length;
					if (bytes.Length > 0)
					{
						Buffer.BlockCopy(bytes, 0, array, 17, bytes.Length);
					}
					barricadeRegion.barricades[(int)index].barricade.state = array;
				}
			}
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000EAAFC File Offset: 0x000E8EFC
		public static void updateStereoTrack(Transform barricade, Guid newTrack)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askUpdateStereoTrack", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2,
					newTrack
				});
			}
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000EAB68 File Offset: 0x000E8F68
		[SteamCall]
		public void tellUpdateStereoTrack(CSteamID steamID, byte x, byte y, ushort plant, ushort index, Guid newTrack)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableStereo interactableStereo = barricadeRegion.drops[(int)index].interactable as InteractableStereo;
				if (interactableStereo != null)
				{
					interactableStereo.updateTrack(newTrack);
				}
			}
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000EABEC File Offset: 0x000E8FEC
		[SteamCall]
		public void askUpdateStereoTrack(CSteamID steamID, byte x, byte y, ushort plant, ushort index, Guid newTrack)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableStereo x2 = barricadeRegion.drops[(int)index].interactable as InteractableStereo;
				if (x2 != null)
				{
					if (plant == 65535)
					{
						BarricadeManager.manager.channel.send("tellUpdateStereoTrack", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							newTrack
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellUpdateStereoTrack", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							newTrack
						});
					}
					byte[] state = barricadeRegion.barricades[(int)index].barricade.state;
					GuidBuffer guidBuffer = new GuidBuffer(newTrack);
					guidBuffer.Write(state, 0);
				}
			}
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000EAD98 File Offset: 0x000E9198
		public static void updateStereoVolume(Transform barricade, byte newVolume)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askUpdateStereoVolume", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2,
					newVolume
				});
			}
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000EAE04 File Offset: 0x000E9204
		[SteamCall]
		public void tellUpdateStereoVolume(CSteamID steamID, byte x, byte y, ushort plant, ushort index, byte newVolume)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableStereo interactableStereo = barricadeRegion.drops[(int)index].interactable as InteractableStereo;
				if (interactableStereo != null)
				{
					interactableStereo.compressedVolume = newVolume;
				}
			}
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x000EAE88 File Offset: 0x000E9288
		[SteamCall]
		public void askUpdateStereoVolume(CSteamID steamID, byte x, byte y, ushort plant, ushort index, byte newVolume)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableStereo x2 = barricadeRegion.drops[(int)index].interactable as InteractableStereo;
				if (x2 != null)
				{
					if (plant == 65535)
					{
						BarricadeManager.manager.channel.send("tellUpdateStereoVolume", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							newVolume
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellUpdateStereoVolume", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							newVolume
						});
					}
					byte[] state = barricadeRegion.barricades[(int)index].barricade.state;
					state[16] = newVolume;
				}
			}
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x000EB028 File Offset: 0x000E9428
		public static void transferLibrary(Transform barricade, byte transaction, uint delta)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askTransferLibrary", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2,
					transaction,
					delta
				});
			}
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000EB0A0 File Offset: 0x000E94A0
		[SteamCall]
		public void tellTransferLibrary(CSteamID steamID, byte x, byte y, ushort plant, ushort index, uint newAmount)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableLibrary interactableLibrary = barricadeRegion.drops[(int)index].interactable as InteractableLibrary;
				if (interactableLibrary != null)
				{
					interactableLibrary.updateAmount(newAmount);
				}
			}
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000EB124 File Offset: 0x000E9524
		[SteamCall]
		public void askTransferLibrary(CSteamID steamID, byte x, byte y, ushort plant, ushort index, byte transaction, uint delta)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableLibrary interactableLibrary = barricadeRegion.drops[(int)index].interactable as InteractableLibrary;
				if (interactableLibrary != null && interactableLibrary.checkTransfer(player.channel.owner.playerID.steamID, player.quests.groupID))
				{
					uint num3;
					if (transaction == 0)
					{
						uint num = (uint)Math.Ceiling(delta * ((double)interactableLibrary.tax / 100.0));
						uint num2 = delta - num;
						if (delta > player.skills.experience || num2 + interactableLibrary.amount > interactableLibrary.capacity)
						{
							return;
						}
						num3 = interactableLibrary.amount + num2;
						player.skills.askSpend(delta);
					}
					else
					{
						if (delta > interactableLibrary.amount)
						{
							return;
						}
						num3 = interactableLibrary.amount - delta;
						player.skills.askAward(delta);
					}
					if (plant == 65535)
					{
						BarricadeManager.manager.channel.send("tellTransferLibrary", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							num3
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellTransferLibrary", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							num3
						});
					}
					Buffer.BlockCopy(BitConverter.GetBytes(num3), 0, barricadeRegion.barricades[(int)index].barricade.state, 16, 4);
				}
			}
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x000EB38C File Offset: 0x000E978C
		public static void toggleSafezone(Transform barricade)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askToggleSafezone", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2
				});
			}
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000EB3F0 File Offset: 0x000E97F0
		[SteamCall]
		public void tellToggleSafezone(CSteamID steamID, byte x, byte y, ushort plant, ushort index, bool isPowered)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableSafezone interactableSafezone = barricadeRegion.drops[(int)index].interactable as InteractableSafezone;
				if (interactableSafezone != null)
				{
					interactableSafezone.updatePowered(isPowered);
				}
			}
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000EB474 File Offset: 0x000E9874
		[SteamCall]
		public void askToggleSafezone(CSteamID steamID, byte x, byte y, ushort plant, ushort index)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableSafezone interactableSafezone = barricadeRegion.drops[(int)index].interactable as InteractableSafezone;
				if (interactableSafezone != null)
				{
					if (plant == 65535)
					{
						BarricadeManager.manager.channel.send("tellToggleSafezone", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							!interactableSafezone.isPowered
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellToggleSafezone", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							!interactableSafezone.isPowered
						});
					}
					barricadeRegion.barricades[(int)index].barricade.state[0] = ((!interactableSafezone.isPowered) ? 0 : 1);
					EffectManager.sendEffect(8, EffectManager.SMALL, interactableSafezone.transform.position);
				}
			}
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000EB644 File Offset: 0x000E9A44
		public static void toggleOxygenator(Transform barricade)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askToggleOxygenator", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2
				});
			}
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000EB6A8 File Offset: 0x000E9AA8
		[SteamCall]
		public void tellToggleOxygenator(CSteamID steamID, byte x, byte y, ushort plant, ushort index, bool isPowered)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableOxygenator interactableOxygenator = barricadeRegion.drops[(int)index].interactable as InteractableOxygenator;
				if (interactableOxygenator != null)
				{
					interactableOxygenator.updatePowered(isPowered);
				}
			}
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000EB72C File Offset: 0x000E9B2C
		[SteamCall]
		public void askToggleOxygenator(CSteamID steamID, byte x, byte y, ushort plant, ushort index)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableOxygenator interactableOxygenator = barricadeRegion.drops[(int)index].interactable as InteractableOxygenator;
				if (interactableOxygenator != null)
				{
					if (plant == 65535)
					{
						BarricadeManager.manager.channel.send("tellToggleOxygenator", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							!interactableOxygenator.isPowered
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellToggleOxygenator", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							!interactableOxygenator.isPowered
						});
					}
					barricadeRegion.barricades[(int)index].barricade.state[0] = ((!interactableOxygenator.isPowered) ? 0 : 1);
					EffectManager.sendEffect(8, EffectManager.SMALL, interactableOxygenator.transform.position);
				}
			}
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x000EB8FC File Offset: 0x000E9CFC
		public static void toggleSpot(Transform barricade)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askToggleSpot", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2
				});
			}
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x000EB960 File Offset: 0x000E9D60
		[SteamCall]
		public void tellToggleSpot(CSteamID steamID, byte x, byte y, ushort plant, ushort index, bool isPowered)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableSpot interactableSpot = barricadeRegion.drops[(int)index].interactable as InteractableSpot;
				if (interactableSpot != null)
				{
					interactableSpot.updatePowered(isPowered);
				}
			}
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000EB9E4 File Offset: 0x000E9DE4
		[SteamCall]
		public void askToggleSpot(CSteamID steamID, byte x, byte y, ushort plant, ushort index)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableSpot interactableSpot = barricadeRegion.drops[(int)index].interactable as InteractableSpot;
				if (interactableSpot != null)
				{
					if (plant == 65535)
					{
						BarricadeManager.manager.channel.send("tellToggleSpot", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							!interactableSpot.isPowered
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellToggleSpot", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							!interactableSpot.isPowered
						});
					}
					barricadeRegion.barricades[(int)index].barricade.state[0] = ((!interactableSpot.isPowered) ? 0 : 1);
					EffectManager.sendEffect(8, EffectManager.SMALL, interactableSpot.transform.position);
				}
			}
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x000EBBB4 File Offset: 0x000E9FB4
		public static void sendFuel(Transform barricade, ushort fuel)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				if (num == 65535)
				{
					BarricadeManager.manager.channel.send("tellFuel", ESteamCall.ALL, b, b2, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						num,
						num2,
						fuel
					});
				}
				else
				{
					BarricadeManager.manager.channel.send("tellFuel", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						num,
						num2,
						fuel
					});
				}
			}
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x000EBC84 File Offset: 0x000EA084
		[SteamCall]
		public void tellFuel(CSteamID steamID, byte x, byte y, ushort plant, ushort index, ushort fuel)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableGenerator interactableGenerator = barricadeRegion.drops[(int)index].interactable as InteractableGenerator;
				if (interactableGenerator != null)
				{
					interactableGenerator.tellFuel(fuel);
				}
			}
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x000EBD08 File Offset: 0x000EA108
		public static void toggleGenerator(Transform barricade)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askToggleGenerator", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2
				});
			}
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x000EBD6C File Offset: 0x000EA16C
		[SteamCall]
		public void tellToggleGenerator(CSteamID steamID, byte x, byte y, ushort plant, ushort index, bool isPowered)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableGenerator interactableGenerator = barricadeRegion.drops[(int)index].interactable as InteractableGenerator;
				if (interactableGenerator != null)
				{
					interactableGenerator.updatePowered(isPowered);
				}
			}
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x000EBDF0 File Offset: 0x000EA1F0
		[SteamCall]
		public void askToggleGenerator(CSteamID steamID, byte x, byte y, ushort plant, ushort index)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableGenerator interactableGenerator = barricadeRegion.drops[(int)index].interactable as InteractableGenerator;
				if (interactableGenerator != null)
				{
					if (plant == 65535)
					{
						BarricadeManager.manager.channel.send("tellToggleGenerator", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							!interactableGenerator.isPowered
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellToggleGenerator", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							!interactableGenerator.isPowered
						});
					}
					barricadeRegion.barricades[(int)index].barricade.state[0] = ((!interactableGenerator.isPowered) ? 0 : 1);
					EffectManager.sendEffect(8, EffectManager.SMALL, interactableGenerator.transform.position);
				}
			}
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000EBFC0 File Offset: 0x000EA3C0
		public static void toggleFire(Transform barricade)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askToggleFire", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2
				});
			}
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000EC024 File Offset: 0x000EA424
		[SteamCall]
		public void tellToggleFire(CSteamID steamID, byte x, byte y, ushort plant, ushort index, bool isLit)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableFire interactableFire = barricadeRegion.drops[(int)index].interactable as InteractableFire;
				if (interactableFire != null)
				{
					interactableFire.updateLit(isLit);
				}
			}
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x000EC0A8 File Offset: 0x000EA4A8
		[SteamCall]
		public void askToggleFire(CSteamID steamID, byte x, byte y, ushort plant, ushort index)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableFire interactableFire = barricadeRegion.drops[(int)index].interactable as InteractableFire;
				if (interactableFire != null)
				{
					if (plant == 65535)
					{
						BarricadeManager.manager.channel.send("tellToggleFire", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							!interactableFire.isLit
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellToggleFire", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							!interactableFire.isLit
						});
					}
					barricadeRegion.barricades[(int)index].barricade.state[0] = ((!interactableFire.isLit) ? 0 : 1);
				}
			}
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000EC264 File Offset: 0x000EA664
		public static void toggleOven(Transform barricade)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askToggleOven", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2
				});
			}
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000EC2C8 File Offset: 0x000EA6C8
		[SteamCall]
		public void tellToggleOven(CSteamID steamID, byte x, byte y, ushort plant, ushort index, bool isLit)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableOven interactableOven = barricadeRegion.drops[(int)index].interactable as InteractableOven;
				if (interactableOven != null)
				{
					interactableOven.updateLit(isLit);
				}
			}
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000EC34C File Offset: 0x000EA74C
		[SteamCall]
		public void askToggleOven(CSteamID steamID, byte x, byte y, ushort plant, ushort index)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableOven interactableOven = barricadeRegion.drops[(int)index].interactable as InteractableOven;
				if (interactableOven != null)
				{
					if (plant == 65535)
					{
						BarricadeManager.manager.channel.send("tellToggleOven", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							!interactableOven.isLit
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellToggleOven", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							!interactableOven.isLit
						});
					}
					barricadeRegion.barricades[(int)index].barricade.state[0] = ((!interactableOven.isLit) ? 0 : 1);
				}
			}
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000EC508 File Offset: 0x000EA908
		public static void farm(Transform barricade)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askFarm", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2
				});
			}
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000EC56C File Offset: 0x000EA96C
		[SteamCall]
		public void tellFarm(CSteamID steamID, byte x, byte y, ushort plant, ushort index, uint planted)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableFarm interactableFarm = barricadeRegion.drops[(int)index].interactable as InteractableFarm;
				if (interactableFarm != null)
				{
					interactableFarm.updatePlanted(planted);
				}
			}
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000EC5F0 File Offset: 0x000EA9F0
		[SteamCall]
		public void askFarm(CSteamID steamID, byte x, byte y, ushort plant, ushort index)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableFarm interactableFarm = barricadeRegion.drops[(int)index].interactable as InteractableFarm;
				if (interactableFarm != null && interactableFarm.checkFarm())
				{
					player.inventory.forceAddItem(new Item(interactableFarm.grow, EItemOrigin.NATURE), true);
					if (UnityEngine.Random.value < player.skills.mastery(2, 5))
					{
						player.inventory.forceAddItem(new Item(interactableFarm.grow, EItemOrigin.NATURE), true);
					}
					BarricadeManager.damage(interactableFarm.transform, 2f, 1f, false);
					player.sendStat(EPlayerStat.FOUND_PLANTS);
					player.skills.askPay(1u);
				}
			}
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000EC73C File Offset: 0x000EAB3C
		public static void updateFarm(Transform barricade, uint planted, bool shouldSend)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				if (shouldSend)
				{
					if (num == 65535)
					{
						BarricadeManager.manager.channel.send("tellFarm", ESteamCall.ALL, b, b2, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							b,
							b2,
							num,
							num2,
							planted
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellFarm", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							b,
							b2,
							num,
							num2,
							planted
						});
					}
				}
				BitConverter.GetBytes(planted).CopyTo(barricadeRegion.barricades[(int)num2].barricade.state, 0);
			}
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x000EC834 File Offset: 0x000EAC34
		[SteamCall]
		public void tellOil(CSteamID steamID, byte x, byte y, ushort plant, ushort index, ushort fuel)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableOil interactableOil = barricadeRegion.drops[(int)index].interactable as InteractableOil;
				if (interactableOil != null)
				{
					interactableOil.tellFuel(fuel);
				}
			}
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x000EC8B8 File Offset: 0x000EACB8
		public static void sendOil(Transform barricade, ushort fuel)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				if (num == 65535)
				{
					BarricadeManager.manager.channel.send("tellOil", ESteamCall.ALL, b, b2, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						num,
						num2,
						fuel
					});
				}
				else
				{
					BarricadeManager.manager.channel.send("tellOil", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						num,
						num2,
						fuel
					});
				}
			}
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000EC988 File Offset: 0x000EAD88
		[SteamCall]
		public void tellRainBarrel(CSteamID steamID, byte x, byte y, ushort plant, ushort index, bool isFull)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableRainBarrel interactableRainBarrel = barricadeRegion.drops[(int)index].interactable as InteractableRainBarrel;
				if (interactableRainBarrel != null)
				{
					interactableRainBarrel.updateFull(isFull);
				}
			}
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000ECA0C File Offset: 0x000EAE0C
		public static void updateRainBarrel(Transform barricade, bool isFull, bool shouldSend)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				if (shouldSend)
				{
					if (num == 65535)
					{
						BarricadeManager.manager.channel.send("tellRainBarrel", ESteamCall.ALL, b, b2, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							b,
							b2,
							num,
							num2,
							isFull
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellRainBarrel", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							b,
							b2,
							num,
							num2,
							isFull
						});
					}
				}
				barricadeRegion.barricades[(int)num2].barricade.state[0] = ((!isFull) ? 0 : 1);
			}
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000ECB08 File Offset: 0x000EAF08
		public static void sendStorageDisplay(Transform barricade, Item item, ushort skin, ushort mythic, string tags, string dynamicProps)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				if (item != null)
				{
					BarricadeManager.manager.channel.send("tellStorageDisplay", ESteamCall.CLIENTS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						num,
						num2,
						item.id,
						item.quality,
						item.state,
						skin,
						mythic,
						tags,
						dynamicProps
					});
				}
				else
				{
					BarricadeManager.manager.channel.send("tellStorageDisplay", ESteamCall.CLIENTS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						num,
						num2,
						0,
						0,
						0,
						skin,
						mythic,
						tags,
						dynamicProps
					});
				}
			}
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000ECC38 File Offset: 0x000EB038
		[SteamCall]
		public void tellStorageDisplay(CSteamID steamID, byte x, byte y, ushort plant, ushort index, ushort id, byte quality, byte[] state, ushort skin, ushort mythic, string tags, string dynamicProps)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableStorage interactableStorage = barricadeRegion.drops[(int)index].interactable as InteractableStorage;
				if (interactableStorage != null)
				{
					interactableStorage.setDisplay(id, quality, state, skin, mythic, tags, dynamicProps);
				}
			}
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x000ECCC8 File Offset: 0x000EB0C8
		public static void storeStorage(Transform barricade, bool quickGrab)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askStoreStorage", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2,
					quickGrab
				});
			}
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x000ECD34 File Offset: 0x000EB134
		[SteamCall]
		public void askStoreStorage(CSteamID steamID, byte x, byte y, ushort plant, ushort index, bool quickGrab)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if (player.inventory.isStoring)
				{
					return;
				}
				if (player.animator.gesture == EPlayerGesture.ARREST_START)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableStorage interactableStorage = barricadeRegion.drops[(int)index].interactable as InteractableStorage;
				if (interactableStorage != null)
				{
					if (interactableStorage.checkStore(player.channel.owner.playerID.steamID, player.quests.groupID))
					{
						if (interactableStorage.isDisplay && quickGrab)
						{
							if (interactableStorage.displayItem != null)
							{
								player.inventory.forceAddItem(interactableStorage.displayItem, true);
								interactableStorage.displayItem = null;
								interactableStorage.displaySkin = 0;
								interactableStorage.displayMythic = 0;
								interactableStorage.displayTags = string.Empty;
								interactableStorage.displayDynamicProps = string.Empty;
								interactableStorage.items.removeItem(0);
							}
						}
						else
						{
							interactableStorage.isOpen = true;
							interactableStorage.opener = player;
							player.inventory.isStoring = true;
							player.inventory.isStorageTrunk = false;
							player.inventory.storage = interactableStorage;
							player.inventory.updateItems(PlayerInventory.STORAGE, interactableStorage.items);
							player.inventory.sendStorage();
						}
					}
					else
					{
						player.sendMessage(EPlayerMessage.BUSY);
					}
				}
			}
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000ECF1C File Offset: 0x000EB31C
		public static void toggleDoor(Transform barricade)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askToggleDoor", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2
				});
			}
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000ECF80 File Offset: 0x000EB380
		[SteamCall]
		public void tellToggleDoor(CSteamID steamID, byte x, byte y, ushort plant, ushort index, bool isOpen)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableDoor interactableDoor = barricadeRegion.drops[(int)index].interactable as InteractableDoor;
				if (interactableDoor != null)
				{
					interactableDoor.updateToggle(isOpen);
				}
			}
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x000ED004 File Offset: 0x000EB404
		[SteamCall]
		public void askToggleDoor(CSteamID steamID, byte x, byte y, ushort plant, ushort index)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableDoor interactableDoor = barricadeRegion.drops[(int)index].interactable as InteractableDoor;
				if (interactableDoor != null && interactableDoor.isOpenable && interactableDoor.checkToggle(player.channel.owner.playerID.steamID, player.quests.groupID))
				{
					if (plant == 65535)
					{
						BarricadeManager.manager.channel.send("tellToggleDoor", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							!interactableDoor.isOpen
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellToggleDoor", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							x,
							y,
							plant,
							index,
							!interactableDoor.isOpen
						});
					}
					barricadeRegion.barricades[(int)index].barricade.state[16] = ((!interactableDoor.isOpen) ? 0 : 1);
				}
			}
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x000ED1F8 File Offset: 0x000EB5F8
		public static bool tryGetBed(CSteamID owner, out Vector3 point, out byte angle)
		{
			point = Vector3.zero;
			angle = 0;
			for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
			{
				for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
				{
					BarricadeRegion barricadeRegion = BarricadeManager.regions[(int)b, (int)b2];
					ushort num = 0;
					while ((int)num < BarricadeManager.regions[(int)b, (int)b2].barricades.Count)
					{
						if (barricadeRegion.barricades[(int)num].barricade.state.Length > 0)
						{
							if ((int)num >= barricadeRegion.drops.Count)
							{
								break;
							}
							InteractableBed interactableBed = barricadeRegion.drops[(int)num].interactable as InteractableBed;
							if (interactableBed != null && interactableBed.owner == owner && Level.checkSafeIncludingClipVolumes(interactableBed.transform.position))
							{
								point = interactableBed.transform.position;
								angle = MeasurementTool.angleToByte((float)(barricadeRegion.barricades[(int)num].angle_y * 2 + 90));
								int num2 = Physics.OverlapCapsuleNonAlloc(point + new Vector3(0f, PlayerStance.RADIUS, 0f), point + new Vector3(0f, 2.5f - PlayerStance.RADIUS, 0f), PlayerStance.RADIUS, BarricadeManager.checkColliders, RayMasks.BLOCK_STANCE, QueryTriggerInteraction.Ignore);
								for (int i = 0; i < num2; i++)
								{
									if (BarricadeManager.checkColliders[i].gameObject != interactableBed.gameObject)
									{
										return false;
									}
								}
								return true;
							}
						}
						num += 1;
					}
				}
			}
			ushort num3 = 0;
			while ((int)num3 < BarricadeManager.plants.Count)
			{
				BarricadeRegion barricadeRegion2 = BarricadeManager.plants[(int)num3];
				ushort num4 = 0;
				while ((int)num4 < barricadeRegion2.barricades.Count)
				{
					if (barricadeRegion2.barricades[(int)num4].barricade.state.Length > 0)
					{
						if ((int)num4 >= barricadeRegion2.drops.Count)
						{
							break;
						}
						InteractableBed interactableBed2 = barricadeRegion2.drops[(int)num4].interactable as InteractableBed;
						if (interactableBed2 != null && interactableBed2.owner == owner && Level.checkSafeIncludingClipVolumes(interactableBed2.transform.position))
						{
							point = interactableBed2.transform.position;
							angle = MeasurementTool.angleToByte((float)(barricadeRegion2.barricades[(int)num4].angle_y * 2 + 90));
							int num5 = Physics.OverlapCapsuleNonAlloc(point + new Vector3(0f, PlayerStance.RADIUS, 0f), point + new Vector3(0f, 2.5f - PlayerStance.RADIUS, 0f), PlayerStance.RADIUS, BarricadeManager.checkColliders, RayMasks.BLOCK_STANCE, QueryTriggerInteraction.Ignore);
							for (int j = 0; j < num5; j++)
							{
								if (BarricadeManager.checkColliders[j].gameObject != interactableBed2.gameObject)
								{
									return false;
								}
							}
							return true;
						}
					}
					num4 += 1;
				}
				num3 += 1;
			}
			return false;
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x000ED55C File Offset: 0x000EB95C
		public static void unclaimBeds(CSteamID owner)
		{
			for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
			{
				for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
				{
					BarricadeRegion barricadeRegion = BarricadeManager.regions[(int)b, (int)b2];
					ushort num = 0;
					while ((int)num < barricadeRegion.barricades.Count)
					{
						if (barricadeRegion.barricades[(int)num].barricade.state.Length > 0)
						{
							if ((int)num >= barricadeRegion.drops.Count)
							{
								break;
							}
							InteractableBed interactableBed = barricadeRegion.drops[(int)num].interactable as InteractableBed;
							if (interactableBed != null && interactableBed.owner == owner)
							{
								BarricadeManager.manager.channel.send("tellClaimBed", ESteamCall.ALL, b, b2, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
								{
									b,
									b2,
									ushort.MaxValue,
									num,
									CSteamID.Nil
								});
								BitConverter.GetBytes(interactableBed.owner.m_SteamID).CopyTo(barricadeRegion.barricades[(int)num].barricade.state, 0);
								return;
							}
						}
						num += 1;
					}
				}
			}
			ushort num2 = 0;
			while ((int)num2 < BarricadeManager.plants.Count)
			{
				BarricadeRegion barricadeRegion2 = BarricadeManager.plants[(int)num2];
				ushort num3 = 0;
				while ((int)num3 < barricadeRegion2.barricades.Count)
				{
					if (barricadeRegion2.barricades[(int)num3].barricade.state.Length > 0)
					{
						if ((int)num3 >= barricadeRegion2.drops.Count)
						{
							break;
						}
						InteractableBed interactableBed2 = barricadeRegion2.drops[(int)num3].interactable as InteractableBed;
						if (interactableBed2 != null && interactableBed2.owner == owner)
						{
							BarricadeManager.manager.channel.send("tellClaimBed", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								byte.MaxValue,
								byte.MaxValue,
								num2,
								num3,
								CSteamID.Nil
							});
							BitConverter.GetBytes(interactableBed2.owner.m_SteamID).CopyTo(barricadeRegion2.barricades[(int)num3].barricade.state, 0);
							return;
						}
					}
					num3 += 1;
				}
				num2 += 1;
			}
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x000ED804 File Offset: 0x000EBC04
		public static void claimBed(Transform barricade)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				BarricadeManager.manager.channel.send("askClaimBed", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					b,
					b2,
					num,
					num2
				});
			}
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x000ED868 File Offset: 0x000EBC68
		[SteamCall]
		public void tellClaimBed(CSteamID steamID, byte x, byte y, ushort plant, ushort index, CSteamID owner)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableBed interactableBed = barricadeRegion.drops[(int)index].interactable as InteractableBed;
				if (interactableBed != null)
				{
					interactableBed.updateClaim(owner);
				}
			}
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x000ED8EC File Offset: 0x000EBCEC
		[SteamCall]
		public void askClaimBed(CSteamID steamID, byte x, byte y, ushort plant, ushort index)
		{
			BarricadeRegion barricadeRegion;
			if (Provider.isServer && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
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
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((barricadeRegion.drops[(int)index].model.transform.position - player.transform.position).sqrMagnitude > 400f)
				{
					return;
				}
				InteractableBed interactableBed = barricadeRegion.drops[(int)index].interactable as InteractableBed;
				if (interactableBed != null && interactableBed.isClaimable && interactableBed.checkClaim(player.channel.owner.playerID.steamID))
				{
					if (interactableBed.isClaimed)
					{
						if (plant == 65535)
						{
							BarricadeManager.manager.channel.send("tellClaimBed", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								x,
								y,
								plant,
								index,
								CSteamID.Nil
							});
						}
						else
						{
							BarricadeManager.manager.channel.send("tellClaimBed", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								x,
								y,
								plant,
								index,
								CSteamID.Nil
							});
						}
					}
					else
					{
						BarricadeManager.unclaimBeds(player.channel.owner.playerID.steamID);
						if (plant == 65535)
						{
							BarricadeManager.manager.channel.send("tellClaimBed", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								x,
								y,
								plant,
								index,
								player.channel.owner.playerID.steamID
							});
						}
						else
						{
							BarricadeManager.manager.channel.send("tellClaimBed", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								x,
								y,
								plant,
								index,
								player.channel.owner.playerID.steamID
							});
						}
					}
					BitConverter.GetBytes(interactableBed.owner.m_SteamID).CopyTo(barricadeRegion.barricades[(int)index].barricade.state, 0);
				}
			}
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000EDBD0 File Offset: 0x000EBFD0
		[SteamCall]
		public void tellShootSentry(CSteamID steamID, byte x, byte y, ushort plant, ushort index)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableSentry interactableSentry = barricadeRegion.drops[(int)index].interactable as InteractableSentry;
				if (interactableSentry != null)
				{
					interactableSentry.shoot();
				}
			}
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x000EDC54 File Offset: 0x000EC054
		public static void sendShootSentry(Transform barricade)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				if (num == 65535)
				{
					BarricadeManager.manager.channel.send("tellShootSentry", ESteamCall.ALL, b, b2, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						num,
						num2
					});
				}
				else
				{
					BarricadeManager.manager.channel.send("tellShootSentry", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						num,
						num2
					});
				}
			}
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x000EDD10 File Offset: 0x000EC110
		[SteamCall]
		public void tellAlertSentry(CSteamID steamID, byte x, byte y, ushort plant, ushort index, byte yaw, byte pitch)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				InteractableSentry interactableSentry = barricadeRegion.drops[(int)index].interactable as InteractableSentry;
				if (interactableSentry != null)
				{
					interactableSentry.alert(MeasurementTool.byteToAngle(yaw), MeasurementTool.byteToAngle(pitch));
				}
			}
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x000EDDA0 File Offset: 0x000EC1A0
		public static void sendAlertSentry(Transform barricade, float yaw, float pitch)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion))
			{
				if (num == 65535)
				{
					BarricadeManager.manager.channel.send("tellAlertSentry", ESteamCall.ALL, b, b2, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						num,
						num2,
						MeasurementTool.angleToByte(yaw),
						MeasurementTool.angleToByte(pitch)
					});
				}
				else
				{
					BarricadeManager.manager.channel.send("tellAlertSentry", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
					{
						b,
						b2,
						num,
						num2,
						MeasurementTool.angleToByte(yaw),
						MeasurementTool.angleToByte(pitch)
					});
				}
			}
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x000EDE94 File Offset: 0x000EC294
		public static void damage(Transform barricade, float damage, float times, bool armor)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion) && !barricadeRegion.barricades[(int)num2].barricade.isDead)
			{
				if (armor)
				{
					times *= Provider.modeConfigData.Barricades.Armor_Multiplier;
				}
				ushort amount = (ushort)(damage * times);
				barricadeRegion.barricades[(int)num2].barricade.askDamage(amount);
				if (barricadeRegion.barricades[(int)num2].barricade.isDead)
				{
					ItemBarricadeAsset itemBarricadeAsset = (ItemBarricadeAsset)Assets.find(EAssetType.ITEM, barricadeRegion.barricades[(int)num2].barricade.id);
					if (itemBarricadeAsset != null && itemBarricadeAsset.explosion != 0)
					{
						if (num == 65535)
						{
							EffectManager.sendEffect(itemBarricadeAsset.explosion, b, b2, BarricadeManager.BARRICADE_REGIONS, barricade.position + Vector3.down * itemBarricadeAsset.offset);
						}
						else
						{
							EffectManager.sendEffect(itemBarricadeAsset.explosion, EffectManager.MEDIUM, barricade.position + Vector3.down * itemBarricadeAsset.offset);
						}
					}
					barricadeRegion.barricades.RemoveAt((int)num2);
					if (num == 65535)
					{
						BarricadeManager.manager.channel.send("tellTakeBarricade", ESteamCall.ALL, b, b2, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							b,
							b2,
							num,
							num2
						});
					}
					else
					{
						BarricadeManager.manager.channel.send("tellTakeBarricade", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							b,
							b2,
							num,
							num2
						});
					}
				}
				else
				{
					for (int i = 0; i < Provider.clients.Count; i++)
					{
						if (OwnershipTool.checkToggle(Provider.clients[i].playerID.steamID, barricadeRegion.barricades[(int)num2].owner, Provider.clients[i].player.quests.groupID, barricadeRegion.barricades[(int)num2].group))
						{
							if (num == 65535)
							{
								if (Provider.clients[i].player != null && Regions.checkArea(b, b2, Provider.clients[i].player.movement.region_x, Provider.clients[i].player.movement.region_y, BarricadeManager.BARRICADE_REGIONS))
								{
									BarricadeManager.manager.channel.send("tellBarricadeHealth", Provider.clients[i].playerID.steamID, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
									{
										b,
										b2,
										num,
										num2,
										(byte)((float)barricadeRegion.barricades[(int)num2].barricade.health / (float)barricadeRegion.barricades[(int)num2].barricade.asset.health * 100f)
									});
								}
							}
							else
							{
								BarricadeManager.manager.channel.send("tellBarricadeHealth", Provider.clients[i].playerID.steamID, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
								{
									b,
									b2,
									num,
									num2,
									(byte)((float)barricadeRegion.barricades[(int)num2].barricade.health / (float)barricadeRegion.barricades[(int)num2].barricade.asset.health * 100f)
								});
							}
						}
					}
				}
			}
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x000EE29C File Offset: 0x000EC69C
		public static void repair(Transform barricade, float damage, float times)
		{
			byte b;
			byte b2;
			ushort num;
			ushort num2;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out num2, out barricadeRegion) && !barricadeRegion.barricades[(int)num2].barricade.isDead && !barricadeRegion.barricades[(int)num2].barricade.isRepaired)
			{
				ushort amount = (ushort)(damage * times);
				barricadeRegion.barricades[(int)num2].barricade.askRepair(amount);
				for (int i = 0; i < Provider.clients.Count; i++)
				{
					if (OwnershipTool.checkToggle(Provider.clients[i].playerID.steamID, barricadeRegion.barricades[(int)num2].owner, Provider.clients[i].player.quests.groupID, barricadeRegion.barricades[(int)num2].group))
					{
						if (num == 65535)
						{
							if (Provider.clients[i].player != null && Regions.checkArea(b, b2, Provider.clients[i].player.movement.region_x, Provider.clients[i].player.movement.region_y, BarricadeManager.BARRICADE_REGIONS))
							{
								BarricadeManager.manager.channel.send("tellBarricadeHealth", Provider.clients[i].playerID.steamID, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
								{
									b,
									b2,
									num,
									num2,
									(byte)Mathf.RoundToInt((float)barricadeRegion.barricades[(int)num2].barricade.health / (float)barricadeRegion.barricades[(int)num2].barricade.asset.health * 100f)
								});
							}
						}
						else
						{
							BarricadeManager.manager.channel.send("tellBarricadeHealth", Provider.clients[i].playerID.steamID, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
							{
								b,
								b2,
								num,
								num2,
								(byte)Mathf.RoundToInt((float)barricadeRegion.barricades[(int)num2].barricade.health / (float)barricadeRegion.barricades[(int)num2].barricade.asset.health * 100f)
							});
						}
					}
				}
			}
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000EE548 File Offset: 0x000EC948
		public static Transform dropBarricade(Barricade barricade, Transform hit, Vector3 point, float angle_x, float angle_y, float angle_z, ulong owner, ulong group)
		{
			ItemBarricadeAsset itemBarricadeAsset = (ItemBarricadeAsset)Assets.find(EAssetType.ITEM, barricade.id);
			Transform result = null;
			if (itemBarricadeAsset != null)
			{
				Vector3 eulerAngles = BarricadeManager.getRotation(itemBarricadeAsset, angle_x, angle_y, angle_z).eulerAngles;
				angle_x = (float)(Mathf.RoundToInt(eulerAngles.x / 2f) * 2);
				angle_y = (float)(Mathf.RoundToInt(eulerAngles.y / 2f) * 2);
				angle_z = (float)(Mathf.RoundToInt(eulerAngles.z / 2f) * 2);
				byte b3;
				byte b4;
				BarricadeRegion barricadeRegion2;
				if (hit != null && hit.transform.CompareTag("Vehicle"))
				{
					byte b;
					byte b2;
					ushort num;
					BarricadeRegion barricadeRegion;
					if (BarricadeManager.tryGetPlant(hit, out b, out b2, out num, out barricadeRegion))
					{
						BarricadeData barricadeData = new BarricadeData(barricade, point, MeasurementTool.angleToByte(angle_x), MeasurementTool.angleToByte(angle_y), MeasurementTool.angleToByte(angle_z), owner, group, Provider.time);
						barricadeRegion.barricades.Add(barricadeData);
						uint num2 = BarricadeManager.instanceCount += 1u;
						result = BarricadeManager.manager.spawnBarricade(barricadeRegion, barricade.id, barricade.state, barricadeData.point, barricadeData.angle_x, barricadeData.angle_y, barricadeData.angle_z, 100, barricadeData.owner, barricadeData.group, num2);
						BarricadeManager.manager.channel.send("tellBarricade", ESteamCall.OTHERS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							b,
							b2,
							num,
							barricade.id,
							barricade.state,
							barricadeData.point,
							barricadeData.angle_x,
							barricadeData.angle_y,
							barricadeData.angle_z,
							barricadeData.owner,
							barricadeData.group,
							num2
						});
					}
				}
				else if (Regions.tryGetCoordinate(point, out b3, out b4) && BarricadeManager.tryGetRegion(b3, b4, 65535, out barricadeRegion2))
				{
					BarricadeData barricadeData2 = new BarricadeData(barricade, point, MeasurementTool.angleToByte(angle_x), MeasurementTool.angleToByte(angle_y), MeasurementTool.angleToByte(angle_z), owner, group, Provider.time);
					barricadeRegion2.barricades.Add(barricadeData2);
					uint num3 = BarricadeManager.instanceCount += 1u;
					result = BarricadeManager.manager.spawnBarricade(barricadeRegion2, barricade.id, barricade.state, barricadeData2.point, barricadeData2.angle_x, barricadeData2.angle_y, barricadeData2.angle_z, 100, barricadeData2.owner, barricadeData2.group, num3);
					BarricadeManager.manager.channel.send("tellBarricade", ESteamCall.OTHERS, b3, b4, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						b3,
						b4,
						ushort.MaxValue,
						barricade.id,
						barricade.state,
						barricadeData2.point,
						barricadeData2.angle_x,
						barricadeData2.angle_y,
						barricadeData2.angle_z,
						barricadeData2.owner,
						barricadeData2.group,
						num3
					});
				}
			}
			return result;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000EE8BC File Offset: 0x000ECCBC
		[SteamCall]
		public void tellTakeBarricade(CSteamID steamID, byte x, byte y, ushort plant, ushort index)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				if ((int)index >= barricadeRegion.drops.Count)
				{
					return;
				}
				IManualOnDestroy component = barricadeRegion.drops[(int)index].model.GetComponent<IManualOnDestroy>();
				if (component != null)
				{
					component.ManualOnDestroy();
				}
				UnityEngine.Object.Destroy(barricadeRegion.drops[(int)index].model.gameObject);
				barricadeRegion.drops[(int)index].model.position = Vector3.zero;
				barricadeRegion.drops.RemoveAt((int)index);
			}
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000EE97C File Offset: 0x000ECD7C
		[SteamCall]
		public void tellClearRegionBarricades(CSteamID steamID, byte x, byte y)
		{
			if (base.channel.checkServer(steamID))
			{
				if (!Provider.isServer && !BarricadeManager.regions[(int)x, (int)y].isNetworked)
				{
					return;
				}
				BarricadeRegion barricadeRegion = BarricadeManager.regions[(int)x, (int)y];
				barricadeRegion.destroy();
			}
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x000EE9D0 File Offset: 0x000ECDD0
		public static void askClearRegionBarricades(byte x, byte y)
		{
			if (Provider.isServer)
			{
				if (!Regions.checkSafe((int)x, (int)y))
				{
					return;
				}
				BarricadeRegion barricadeRegion = BarricadeManager.regions[(int)x, (int)y];
				if (barricadeRegion.barricades.Count > 0)
				{
					barricadeRegion.barricades.Clear();
					BarricadeManager.manager.channel.send("tellClearRegionBarricades", ESteamCall.ALL, x, y, BarricadeManager.BARRICADE_REGIONS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						x,
						y
					});
				}
			}
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x000EEA54 File Offset: 0x000ECE54
		public static void askClearAllBarricades()
		{
			if (Provider.isServer)
			{
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						BarricadeManager.askClearRegionBarricades(b, b2);
					}
				}
			}
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x000EEAA0 File Offset: 0x000ECEA0
		public static Quaternion getRotation(ItemBarricadeAsset asset, float angle_x, float angle_y, float angle_z)
		{
			Quaternion lhs = Quaternion.Euler(0f, angle_y, 0f);
			lhs *= Quaternion.Euler((float)((asset.build != EBuild.DOOR && asset.build != EBuild.GATE && asset.build != EBuild.SHUTTER && asset.build != EBuild.HATCH) ? -90 : 0) + angle_x, 0f, 0f);
			return lhs * Quaternion.Euler(0f, angle_z, 0f);
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x000EEB2C File Offset: 0x000ECF2C
		private Transform spawnBarricade(BarricadeRegion region, ushort id, byte[] state, Vector3 point, byte angle_x, byte angle_y, byte angle_z, byte hp, ulong owner, ulong group, uint instanceID)
		{
			if (id == 0)
			{
				return null;
			}
			ItemBarricadeAsset itemBarricadeAsset;
			try
			{
				itemBarricadeAsset = (ItemBarricadeAsset)Assets.find(EAssetType.ITEM, id);
			}
			catch
			{
				itemBarricadeAsset = null;
			}
			if (itemBarricadeAsset != null)
			{
				Transform barricade = BarricadeTool.getBarricade(region.parent, hp, owner, group, point, Quaternion.Euler((float)(angle_x * 2), (float)(angle_y * 2), (float)(angle_z * 2)), id, state, itemBarricadeAsset);
				BarricadeManager.barricadeColliders.Clear();
				barricade.GetComponentsInChildren<Collider>(BarricadeManager.barricadeColliders);
				if (itemBarricadeAsset.build == EBuild.MANNEQUIN)
				{
					Rigidbody rigidbody = barricade.GetComponent<Rigidbody>();
					if (rigidbody == null)
					{
						rigidbody = barricade.gameObject.AddComponent<Rigidbody>();
						rigidbody.useGravity = false;
						rigidbody.isKinematic = true;
					}
				}
				else if (region.parent != LevelBarricades.models || itemBarricadeAsset.build == EBuild.DOOR || itemBarricadeAsset.build == EBuild.GATE || itemBarricadeAsset.build == EBuild.SHUTTER || itemBarricadeAsset.build == EBuild.HATCH)
				{
					for (int i = 0; i < BarricadeManager.barricadeColliders.Count; i++)
					{
						if (BarricadeManager.barricadeColliders[i] is MeshCollider)
						{
							BarricadeManager.barricadeColliders[i].enabled = false;
						}
						Rigidbody rigidbody2 = BarricadeManager.barricadeColliders[i].GetComponent<Rigidbody>();
						if (rigidbody2 == null)
						{
							rigidbody2 = BarricadeManager.barricadeColliders[i].gameObject.AddComponent<Rigidbody>();
							rigidbody2.useGravity = false;
							rigidbody2.isKinematic = true;
						}
						if (BarricadeManager.barricadeColliders[i] is MeshCollider)
						{
							BarricadeManager.barricadeColliders[i].enabled = true;
						}
					}
				}
				if (region.parent != LevelBarricades.models)
				{
					barricade.gameObject.SetActive(false);
					barricade.gameObject.SetActive(true);
					BarricadeManager.vehicleColliders.Clear();
					region.parent.GetComponents<Collider>(BarricadeManager.vehicleColliders);
					for (int j = 0; j < region.parent.childCount; j++)
					{
						Transform child = region.parent.GetChild(j);
						if (!(child == null) && (!(child.name != "Clip") || !(child.name != "Block")))
						{
							BarricadeManager.vehicleSubColliders.Clear();
							child.GetComponents<Collider>(BarricadeManager.vehicleSubColliders);
							if (BarricadeManager.vehicleSubColliders.Count != 0)
							{
								foreach (Collider item in BarricadeManager.vehicleSubColliders)
								{
									BarricadeManager.vehicleColliders.Add(item);
								}
							}
						}
					}
					for (int k = 0; k < BarricadeManager.barricadeColliders.Count; k++)
					{
						if (BarricadeManager.barricadeColliders[k].gameObject.layer == LayerMasks.BARRICADE)
						{
							BarricadeManager.barricadeColliders[k].gameObject.layer = LayerMasks.RESOURCE;
						}
						for (int l = 0; l < BarricadeManager.vehicleColliders.Count; l++)
						{
							Physics.IgnoreCollision(BarricadeManager.vehicleColliders[l], BarricadeManager.barricadeColliders[k], true);
						}
					}
				}
				BarricadeDrop item2 = new BarricadeDrop(barricade, barricade.GetComponent<Interactable>(), instanceID);
				region.drops.Add(item2);
				return barricade;
			}
			if (!Provider.isServer)
			{
				Provider.connectionFailureInfo = ESteamConnectionFailureInfo.BARRICADE;
				Provider.disconnect();
			}
			return null;
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000EEEE0 File Offset: 0x000ED2E0
		[SteamCall]
		public void tellBarricade(CSteamID steamID, byte x, byte y, ushort plant, ushort id, byte[] state, Vector3 point, byte angle_x, byte angle_y, byte angle_z, ulong owner, ulong group, uint instanceID)
		{
			BarricadeRegion barricadeRegion;
			if (base.channel.checkServer(steamID) && BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (!Provider.isServer && !barricadeRegion.isNetworked)
				{
					return;
				}
				this.spawnBarricade(barricadeRegion, id, state, point, angle_x, angle_y, angle_z, 100, owner, group, instanceID);
			}
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000EEF40 File Offset: 0x000ED340
		[SteamCall]
		public void tellBarricades(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID))
			{
				byte x = (byte)base.channel.read(Types.BYTE_TYPE);
				byte y = (byte)base.channel.read(Types.BYTE_TYPE);
				ushort plant = (ushort)base.channel.read(Types.UINT16_TYPE);
				BarricadeRegion barricadeRegion;
				if (BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
				{
					if ((byte)base.channel.read(Types.BYTE_TYPE) == 0)
					{
						if (barricadeRegion.isNetworked)
						{
							return;
						}
					}
					else if (!barricadeRegion.isNetworked)
					{
						return;
					}
					barricadeRegion.isNetworked = true;
					ushort num = (ushort)base.channel.read(Types.UINT16_TYPE);
					for (int i = 0; i < (int)num; i++)
					{
						object[] array = base.channel.read(new Type[]
						{
							Types.UINT16_TYPE,
							Types.BYTE_ARRAY_TYPE,
							Types.VECTOR3_TYPE,
							Types.BYTE_TYPE,
							Types.BYTE_TYPE,
							Types.BYTE_TYPE,
							Types.UINT64_TYPE,
							Types.UINT64_TYPE,
							Types.UINT32_TYPE
						});
						ulong owner = (ulong)array[6];
						ulong group = (ulong)array[7];
						uint instanceID = (uint)array[8];
						byte hp = (byte)base.channel.read(Types.BYTE_TYPE);
						this.spawnBarricade(barricadeRegion, (ushort)array[0], (byte[])array[1], (Vector3)array[2], (byte)array[3], (byte)array[4], (byte)array[5], hp, owner, group, instanceID);
					}
				}
				Level.isLoadingBarricades = false;
			}
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000EF104 File Offset: 0x000ED504
		public void askBarricades(CSteamID steamID, byte x, byte y, ushort plant)
		{
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetRegion(x, y, plant, out barricadeRegion))
			{
				if (barricadeRegion.barricades.Count > 0 && barricadeRegion.drops.Count == barricadeRegion.barricades.Count)
				{
					byte b = 0;
					int i = 0;
					int j = 0;
					while (i < barricadeRegion.barricades.Count)
					{
						int num = 0;
						while (j < barricadeRegion.barricades.Count)
						{
							num += 38 + barricadeRegion.barricades[j].barricade.state.Length;
							j++;
							if (num > Block.BUFFER_SIZE / 2)
							{
								break;
							}
						}
						base.channel.openWrite();
						base.channel.write(x);
						base.channel.write(y);
						base.channel.write(plant);
						base.channel.write(b);
						base.channel.write((ushort)(j - i));
						while (i < j)
						{
							BarricadeData barricadeData = barricadeRegion.barricades[i];
							InteractableStorage interactableStorage = barricadeRegion.drops[i].interactable as InteractableStorage;
							if (interactableStorage != null)
							{
								byte[] array;
								if (interactableStorage.isDisplay)
								{
									byte[] bytes = Encoding.UTF8.GetBytes(interactableStorage.displayTags);
									byte[] bytes2 = Encoding.UTF8.GetBytes(interactableStorage.displayDynamicProps);
									array = new byte[20 + ((interactableStorage.displayItem == null) ? 0 : interactableStorage.displayItem.state.Length) + 4 + 1 + bytes.Length + 1 + bytes2.Length + 1];
									if (interactableStorage.displayItem != null)
									{
										Array.Copy(BitConverter.GetBytes(interactableStorage.displayItem.id), 0, array, 16, 2);
										array[18] = interactableStorage.displayItem.quality;
										array[19] = (byte)interactableStorage.displayItem.state.Length;
										Array.Copy(interactableStorage.displayItem.state, 0, array, 20, interactableStorage.displayItem.state.Length);
										Array.Copy(BitConverter.GetBytes(interactableStorage.displaySkin), 0, array, 20 + interactableStorage.displayItem.state.Length, 2);
										Array.Copy(BitConverter.GetBytes(interactableStorage.displayMythic), 0, array, 20 + interactableStorage.displayItem.state.Length + 2, 2);
										array[20 + interactableStorage.displayItem.state.Length + 4] = (byte)bytes.Length;
										Array.Copy(bytes, 0, array, 20 + interactableStorage.displayItem.state.Length + 5, bytes.Length);
										array[20 + interactableStorage.displayItem.state.Length + 5 + bytes.Length] = (byte)bytes2.Length;
										Array.Copy(bytes2, 0, array, 20 + interactableStorage.displayItem.state.Length + 5 + bytes.Length + 1, bytes2.Length);
										array[20 + interactableStorage.displayItem.state.Length + 5 + bytes.Length + 1 + bytes2.Length] = interactableStorage.rot_comp;
									}
								}
								else
								{
									array = new byte[16];
								}
								Array.Copy(barricadeData.barricade.state, 0, array, 0, 16);
								base.channel.write(new object[]
								{
									barricadeData.barricade.id,
									array,
									barricadeData.point,
									barricadeData.angle_x,
									barricadeData.angle_y,
									barricadeData.angle_z,
									barricadeData.owner,
									barricadeData.group,
									barricadeRegion.drops[i].instanceID
								});
							}
							else
							{
								base.channel.write(new object[]
								{
									barricadeData.barricade.id,
									barricadeData.barricade.state,
									barricadeData.point,
									barricadeData.angle_x,
									barricadeData.angle_y,
									barricadeData.angle_z,
									barricadeData.owner,
									barricadeData.group,
									barricadeRegion.drops[i].instanceID
								});
							}
							base.channel.write((byte)Mathf.RoundToInt((float)barricadeData.barricade.health / (float)barricadeData.barricade.asset.health * 100f));
							i++;
						}
						base.channel.closeWrite("tellBarricades", steamID, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
						b += 1;
					}
				}
				else
				{
					base.channel.openWrite();
					base.channel.write(x);
					base.channel.write(y);
					base.channel.write(plant);
					base.channel.write(0);
					base.channel.write(0);
					base.channel.closeWrite("tellBarricades", steamID, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
				}
			}
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x000EF67A File Offset: 0x000EDA7A
		public static void clearPlants()
		{
			BarricadeManager.plants = new List<BarricadeRegion>();
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x000EF688 File Offset: 0x000EDA88
		public static void waterPlant(Transform parent)
		{
			BarricadeRegion barricadeRegion = new BarricadeRegion(parent);
			barricadeRegion.isNetworked = false;
			BarricadeManager.plants.Add(barricadeRegion);
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x000EF6B0 File Offset: 0x000EDAB0
		public static void uprootPlant(Transform parent)
		{
			ushort num = 0;
			while ((int)num < BarricadeManager.plants.Count)
			{
				BarricadeRegion barricadeRegion = BarricadeManager.plants[(int)num];
				if (barricadeRegion.parent == parent)
				{
					barricadeRegion.barricades.Clear();
					barricadeRegion.destroy();
					BarricadeManager.plants.RemoveAt((int)num);
					return;
				}
				num += 1;
			}
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000EF714 File Offset: 0x000EDB14
		public static void trimPlant(Transform parent)
		{
			ushort num = 0;
			while ((int)num < BarricadeManager.plants.Count)
			{
				BarricadeRegion barricadeRegion = BarricadeManager.plants[(int)num];
				if (barricadeRegion.parent == parent)
				{
					barricadeRegion.barricades.Clear();
					barricadeRegion.destroy();
					return;
				}
				num += 1;
			}
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000EF76C File Offset: 0x000EDB6C
		public static void askPlants(CSteamID steamID)
		{
			ushort num = 0;
			while ((int)num < BarricadeManager.plants.Count)
			{
				BarricadeManager.manager.askBarricades(steamID, byte.MaxValue, byte.MaxValue, num);
				num += 1;
			}
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000EF7AC File Offset: 0x000EDBAC
		public static void askPlants(Transform parent)
		{
			byte b;
			byte b2;
			ushort num;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetPlant(parent, out b, out b2, out num, out barricadeRegion))
			{
				BarricadeManager.manager.channel.openWrite();
				BarricadeManager.manager.channel.write(b);
				BarricadeManager.manager.channel.write(b2);
				BarricadeManager.manager.channel.write(num);
				BarricadeManager.manager.channel.write(0);
				BarricadeManager.manager.channel.write(0);
				BarricadeManager.manager.channel.closeWrite("tellBarricades", ESteamCall.OTHERS, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
			}
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x000EF85C File Offset: 0x000EDC5C
		public static bool tryGetInfo(Transform barricade, out byte x, out byte y, out ushort plant, out ushort index, out BarricadeRegion region)
		{
			x = 0;
			y = 0;
			plant = 0;
			index = 0;
			region = null;
			if (BarricadeManager.tryGetRegion(barricade, out x, out y, out plant, out region))
			{
				index = 0;
				while ((int)index < region.drops.Count)
				{
					if (barricade == region.drops[(int)index].model)
					{
						return true;
					}
					index += 1;
				}
			}
			return false;
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x000EF8D4 File Offset: 0x000EDCD4
		public static bool tryGetInfo(Transform barricade, out byte x, out byte y, out ushort plant, out ushort index, out BarricadeRegion region, out BarricadeDrop drop)
		{
			x = 0;
			y = 0;
			plant = 0;
			index = 0;
			region = null;
			drop = null;
			if (BarricadeManager.tryGetRegion(barricade, out x, out y, out plant, out region))
			{
				index = 0;
				while ((int)index < region.drops.Count)
				{
					if (barricade == region.drops[(int)index].model)
					{
						drop = region.drops[(int)index];
						return true;
					}
					index += 1;
				}
			}
			return false;
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000EF964 File Offset: 0x000EDD64
		public static bool tryGetPlant(Transform parent, out byte x, out byte y, out ushort plant, out BarricadeRegion region)
		{
			x = byte.MaxValue;
			y = byte.MaxValue;
			plant = ushort.MaxValue;
			region = null;
			if (parent == null)
			{
				return false;
			}
			plant = 0;
			while ((int)plant < BarricadeManager.plants.Count)
			{
				region = BarricadeManager.plants[(int)plant];
				if (region.parent == parent)
				{
					return true;
				}
				plant += 1;
			}
			return false;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000EF9E0 File Offset: 0x000EDDE0
		public static bool tryGetRegion(Transform barricade, out byte x, out byte y, out ushort plant, out BarricadeRegion region)
		{
			x = 0;
			y = 0;
			plant = 0;
			region = null;
			if (barricade == null)
			{
				return false;
			}
			if (barricade.parent.CompareTag("Vehicle"))
			{
				plant = 0;
				while ((int)plant < BarricadeManager.plants.Count)
				{
					region = BarricadeManager.plants[(int)plant];
					if (region.parent == barricade.parent)
					{
						return true;
					}
					plant += 1;
				}
			}
			else
			{
				plant = ushort.MaxValue;
				if (Regions.tryGetCoordinate(barricade.position, out x, out y))
				{
					region = BarricadeManager.regions[(int)x, (int)y];
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x000EFA98 File Offset: 0x000EDE98
		public static InteractableVehicle getVehicleFromPlant(ushort plant)
		{
			if ((int)plant < BarricadeManager.plants.Count)
			{
				return DamageTool.getVehicle(BarricadeManager.plants[(int)plant].parent);
			}
			return null;
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x000EFAC4 File Offset: 0x000EDEC4
		public static bool tryGetRegion(byte x, byte y, ushort plant, out BarricadeRegion region)
		{
			region = null;
			if (plant < 65535)
			{
				if ((int)plant < BarricadeManager.plants.Count)
				{
					region = BarricadeManager.plants[(int)plant];
					return true;
				}
				return false;
			}
			else
			{
				if (Regions.checkSafe((int)x, (int)y))
				{
					region = BarricadeManager.regions[(int)x, (int)y];
					return true;
				}
				return false;
			}
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000EFB20 File Offset: 0x000EDF20
		public static void updateState(Transform barricade, byte[] state, int size)
		{
			byte b;
			byte b2;
			ushort num;
			ushort index;
			BarricadeRegion barricadeRegion;
			if (BarricadeManager.tryGetInfo(barricade, out b, out b2, out num, out index, out barricadeRegion))
			{
				if (barricadeRegion.barricades[(int)index].barricade.state.Length != size)
				{
					barricadeRegion.barricades[(int)index].barricade.state = new byte[size];
				}
				Array.Copy(state, barricadeRegion.barricades[(int)index].barricade.state, size);
			}
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000EFB9C File Offset: 0x000EDF9C
		private static void updateActivity(BarricadeRegion region, CSteamID owner, CSteamID group)
		{
			ushort num = 0;
			while ((int)num < region.barricades.Count)
			{
				BarricadeData barricadeData = region.barricades[(int)num];
				if (OwnershipTool.checkToggle(owner, barricadeData.owner, group, barricadeData.group))
				{
					barricadeData.objActiveDate = Provider.time;
				}
				num += 1;
			}
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000EFBF8 File Offset: 0x000EDFF8
		private static void updateActivity(CSteamID owner, CSteamID group)
		{
			for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
			{
				for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
				{
					BarricadeRegion region = BarricadeManager.regions[(int)b, (int)b2];
					BarricadeManager.updateActivity(region, owner, group);
				}
			}
			ushort num = 0;
			while ((int)num < BarricadeManager.plants.Count)
			{
				BarricadeRegion region2 = BarricadeManager.plants[(int)num];
				BarricadeManager.updateActivity(region2, owner, group);
				num += 1;
			}
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x000EFC7C File Offset: 0x000EE07C
		private void onLevelLoaded(int level)
		{
			if (level > Level.SETUP)
			{
				BarricadeManager.regions = new BarricadeRegion[(int)Regions.WORLD_SIZE, (int)Regions.WORLD_SIZE];
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						BarricadeManager.regions[(int)b, (int)b2] = new BarricadeRegion(LevelBarricades.models);
					}
				}
				BarricadeManager.barricadeColliders = new List<Collider>();
				BarricadeManager.vehicleColliders = new List<Collider>();
				BarricadeManager.vehicleSubColliders = new List<Collider>();
				BarricadeManager.version = BarricadeManager.SAVEDATA_VERSION;
				BarricadeManager.instanceCount = 0u;
				if (Provider.isServer)
				{
					BarricadeManager.load();
				}
			}
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x000EFD2C File Offset: 0x000EE12C
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
							if (player.movement.loadedRegions[(int)b, (int)b2].isBarricadesLoaded && !Regions.checkArea(b, b2, new_x, new_y, BarricadeManager.BARRICADE_REGIONS))
							{
								player.movement.loadedRegions[(int)b, (int)b2].isBarricadesLoaded = false;
							}
						}
						else if (player.channel.isOwner && BarricadeManager.regions[(int)b, (int)b2].isNetworked && !Regions.checkArea(b, b2, new_x, new_y, BarricadeManager.BARRICADE_REGIONS))
						{
							BarricadeManager.regions[(int)b, (int)b2].destroy();
							BarricadeManager.regions[(int)b, (int)b2].isNetworked = false;
						}
					}
				}
			}
			if (step == 2 && Dedicator.isDedicated && Regions.checkSafe((int)new_x, (int)new_y))
			{
				for (int i = (int)(new_x - BarricadeManager.BARRICADE_REGIONS); i <= (int)(new_x + BarricadeManager.BARRICADE_REGIONS); i++)
				{
					for (int j = (int)(new_y - BarricadeManager.BARRICADE_REGIONS); j <= (int)(new_y + BarricadeManager.BARRICADE_REGIONS); j++)
					{
						if (Regions.checkSafe((int)((byte)i), (int)((byte)j)) && !player.movement.loadedRegions[i, j].isBarricadesLoaded)
						{
							player.movement.loadedRegions[i, j].isBarricadesLoaded = true;
							this.askBarricades(player.channel.owner.playerID.steamID, (byte)i, (byte)j, ushort.MaxValue);
						}
					}
				}
			}
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000EFEEC File Offset: 0x000EE2EC
		private void onPlayerCreated(Player player)
		{
			PlayerMovement movement = player.movement;
			movement.onRegionUpdated = (PlayerRegionUpdated)Delegate.Combine(movement.onRegionUpdated, new PlayerRegionUpdated(this.onRegionUpdated));
			if (Provider.isServer)
			{
				SteamPlayerID playerID = player.channel.owner.playerID;
				BarricadeManager.updateActivity(playerID.steamID, player.quests.groupID);
			}
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000EFF54 File Offset: 0x000EE354
		private void Start()
		{
			BarricadeManager.manager = this;
			Level.onPreLevelLoaded = (PreLevelLoaded)Delegate.Combine(Level.onPreLevelLoaded, new PreLevelLoaded(this.onLevelLoaded));
			Player.onPlayerCreated = (PlayerCreated)Delegate.Combine(Player.onPlayerCreated, new PlayerCreated(this.onPlayerCreated));
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000EFFA8 File Offset: 0x000EE3A8
		public static void load()
		{
			bool flag = false;
			if (LevelSavedata.fileExists("/Barricades.dat") && Level.info.type == ELevelType.SURVIVAL)
			{
				River river = LevelSavedata.openRiver("/Barricades.dat", true);
				BarricadeManager.version = river.readByte();
				if (BarricadeManager.version > 6)
				{
					BarricadeManager.serverActiveDate = river.readUInt32();
				}
				else
				{
					BarricadeManager.serverActiveDate = Provider.time;
				}
				if (BarricadeManager.version > 0)
				{
					for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
					{
						for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
						{
							BarricadeRegion region = BarricadeManager.regions[(int)b, (int)b2];
							BarricadeManager.loadRegion(BarricadeManager.version, river, region);
						}
					}
					if (BarricadeManager.version > 1)
					{
						ushort num = river.readUInt16();
						num = (ushort)Mathf.Min((int)num, BarricadeManager.plants.Count);
						for (int i = 0; i < (int)num; i++)
						{
							BarricadeRegion region2 = BarricadeManager.plants[i];
							BarricadeManager.loadRegion(BarricadeManager.version, river, region2);
						}
					}
				}
				if (BarricadeManager.version < 11)
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
				for (byte b3 = 0; b3 < Regions.WORLD_SIZE; b3 += 1)
				{
					for (byte b4 = 0; b4 < Regions.WORLD_SIZE; b4 += 1)
					{
						List<LevelBuildableObject> list = LevelObjects.buildables[(int)b3, (int)b4];
						if (list != null && list.Count != 0)
						{
							BarricadeRegion barricadeRegion = BarricadeManager.regions[(int)b3, (int)b4];
							for (int j = 0; j < list.Count; j++)
							{
								LevelBuildableObject levelBuildableObject = list[j];
								if (levelBuildableObject != null)
								{
									ItemBarricadeAsset itemBarricadeAsset = levelBuildableObject.asset as ItemBarricadeAsset;
									if (itemBarricadeAsset != null)
									{
										Vector3 eulerAngles = levelBuildableObject.rotation.eulerAngles;
										byte newAngle_X = MeasurementTool.angleToByte((float)(Mathf.RoundToInt(eulerAngles.x / 2f) * 2));
										byte newAngle_Y = MeasurementTool.angleToByte((float)(Mathf.RoundToInt(eulerAngles.y / 2f) * 2));
										byte newAngle_Z = MeasurementTool.angleToByte((float)(Mathf.RoundToInt(eulerAngles.z / 2f) * 2));
										Barricade barricade = new Barricade(itemBarricadeAsset.id, itemBarricadeAsset.health, itemBarricadeAsset.getState(), itemBarricadeAsset);
										BarricadeData barricadeData = new BarricadeData(barricade, levelBuildableObject.point, newAngle_X, newAngle_Y, newAngle_Z, 0UL, 0UL, uint.MaxValue);
										barricadeRegion.barricades.Add(barricadeData);
										BarricadeManager.manager.spawnBarricade(barricadeRegion, barricade.id, barricade.state, barricadeData.point, barricadeData.angle_x, barricadeData.angle_y, barricadeData.angle_z, (byte)Mathf.RoundToInt((float)barricade.health / (float)itemBarricadeAsset.health * 100f), 0UL, 0UL, BarricadeManager.instanceCount += 1u);
									}
								}
							}
						}
					}
				}
			}
			Level.isLoadingBarricades = false;
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000F02B4 File Offset: 0x000EE6B4
		public static void save()
		{
			River river = LevelSavedata.openRiver("/Barricades.dat", false);
			river.writeByte(BarricadeManager.SAVEDATA_VERSION);
			river.writeUInt32(Provider.time);
			for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
			{
				for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
				{
					BarricadeRegion region = BarricadeManager.regions[(int)b, (int)b2];
					BarricadeManager.saveRegion(river, region);
				}
			}
			ushort num = 0;
			ushort num2 = 0;
			while ((int)num2 < BarricadeManager.plants.Count)
			{
				InteractableVehicle vehicleFromPlant = BarricadeManager.getVehicleFromPlant(num2);
				if (vehicleFromPlant != null && !vehicleFromPlant.isAutoClearable)
				{
					num += 1;
				}
				num2 += 1;
			}
			river.writeUInt16(num);
			for (int i = 0; i < BarricadeManager.plants.Count; i++)
			{
				InteractableVehicle vehicleFromPlant2 = BarricadeManager.getVehicleFromPlant((ushort)i);
				if (vehicleFromPlant2 != null && !vehicleFromPlant2.isAutoClearable)
				{
					BarricadeManager.saveRegion(river, BarricadeManager.plants[i]);
				}
			}
			river.closeRiver();
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000F03D4 File Offset: 0x000EE7D4
		private static void loadRegion(byte version, River river, BarricadeRegion region)
		{
			ushort num = river.readUInt16();
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				ushort num3 = river.readUInt16();
				ushort num4 = river.readUInt16();
				byte[] array = river.readBytes();
				Vector3 vector = river.readSingleVector3();
				byte b = 0;
				if (version > 2)
				{
					b = river.readByte();
				}
				byte b2 = river.readByte();
				byte b3 = 0;
				if (version > 3)
				{
					b3 = river.readByte();
				}
				ulong num5 = 0UL;
				ulong num6 = 0UL;
				if (version > 4)
				{
					num5 = river.readUInt64();
					num6 = river.readUInt64();
				}
				uint newObjActiveDate;
				if (version > 5)
				{
					newObjActiveDate = river.readUInt32();
					if (Provider.time - BarricadeManager.serverActiveDate > Provider.modeConfigData.Barricades.Decay_Time / 2u)
					{
						newObjActiveDate = Provider.time;
					}
				}
				else
				{
					newObjActiveDate = Provider.time;
				}
				ItemBarricadeAsset itemBarricadeAsset;
				try
				{
					itemBarricadeAsset = (ItemBarricadeAsset)Assets.find(EAssetType.ITEM, num3);
				}
				catch
				{
					itemBarricadeAsset = null;
				}
				if (itemBarricadeAsset != null)
				{
					if (itemBarricadeAsset.type == EItemType.TANK && array.Length < 2)
					{
						array = itemBarricadeAsset.getState(EItemOrigin.ADMIN);
					}
					if (itemBarricadeAsset.build == EBuild.OIL && array.Length < 2)
					{
						array = itemBarricadeAsset.getState(EItemOrigin.ADMIN);
					}
					if (version < 10)
					{
						Vector3 eulerAngles = BarricadeManager.getRotation(itemBarricadeAsset, (float)(b * 2), (float)(b2 * 2), (float)(b3 * 2)).eulerAngles;
						b = MeasurementTool.angleToByte((float)(Mathf.RoundToInt(eulerAngles.x / 2f) * 2));
						b2 = MeasurementTool.angleToByte((float)(Mathf.RoundToInt(eulerAngles.y / 2f) * 2));
						b3 = MeasurementTool.angleToByte((float)(Mathf.RoundToInt(eulerAngles.z / 2f) * 2));
					}
					region.barricades.Add(new BarricadeData(new Barricade(num3, num4, array, itemBarricadeAsset), vector, b, b2, b3, num5, num6, newObjActiveDate));
					if (Provider.isServer)
					{
						BarricadeManager.manager.spawnBarricade(region, num3, array, vector, b, b2, b3, (byte)Mathf.RoundToInt((float)num4 / (float)itemBarricadeAsset.health * 100f), num5, num6, BarricadeManager.instanceCount += 1u);
					}
				}
			}
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000F0610 File Offset: 0x000EEA10
		private static void saveRegion(River river, BarricadeRegion region)
		{
			uint time = Provider.time;
			ushort num = 0;
			ushort num2 = 0;
			while ((int)num2 < region.barricades.Count)
			{
				BarricadeData barricadeData = region.barricades[(int)num2];
				if ((!Dedicator.isDedicated || Provider.modeConfigData.Barricades.Decay_Time == 0u || time < barricadeData.objActiveDate || time - barricadeData.objActiveDate < Provider.modeConfigData.Barricades.Decay_Time) && barricadeData.barricade.asset.isSaveable)
				{
					num += 1;
				}
				num2 += 1;
			}
			river.writeUInt16(num);
			ushort num3 = 0;
			while ((int)num3 < region.barricades.Count)
			{
				BarricadeData barricadeData2 = region.barricades[(int)num3];
				if ((!Dedicator.isDedicated || Provider.modeConfigData.Barricades.Decay_Time == 0u || time < barricadeData2.objActiveDate || time - barricadeData2.objActiveDate < Provider.modeConfigData.Barricades.Decay_Time) && barricadeData2.barricade.asset.isSaveable)
				{
					river.writeUInt16(barricadeData2.barricade.id);
					river.writeUInt16(barricadeData2.barricade.health);
					river.writeBytes(barricadeData2.barricade.state);
					river.writeSingleVector3(barricadeData2.point);
					river.writeByte(barricadeData2.angle_x);
					river.writeByte(barricadeData2.angle_y);
					river.writeByte(barricadeData2.angle_z);
					river.writeUInt64(barricadeData2.owner);
					river.writeUInt64(barricadeData2.group);
					river.writeUInt32(barricadeData2.objActiveDate);
				}
				num3 += 1;
			}
		}

		// Token: 0x040018B8 RID: 6328
		private static Collider[] checkColliders = new Collider[2];

		// Token: 0x040018B9 RID: 6329
		public static readonly byte SAVEDATA_VERSION = 13;

		// Token: 0x040018BA RID: 6330
		public static readonly byte BARRICADE_REGIONS = 2;

		// Token: 0x040018BB RID: 6331
		public static SalvageBarricadeRequestHandler onSalvageBarricadeRequested;

		// Token: 0x040018BC RID: 6332
		private static BarricadeManager manager;

		// Token: 0x040018BD RID: 6333
		public static byte version = BarricadeManager.SAVEDATA_VERSION;

		// Token: 0x040018C0 RID: 6336
		private static List<Collider> barricadeColliders;

		// Token: 0x040018C1 RID: 6337
		private static List<Collider> vehicleColliders;

		// Token: 0x040018C2 RID: 6338
		private static List<Collider> vehicleSubColliders;

		// Token: 0x040018C3 RID: 6339
		private static uint instanceCount;

		// Token: 0x040018C4 RID: 6340
		private static uint serverActiveDate;
	}
}
