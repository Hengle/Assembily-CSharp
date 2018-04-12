using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005C6 RID: 1478
	public class VehicleManager : SteamCaller
	{
		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x0600295D RID: 10589 RVA: 0x000FDA18 File Offset: 0x000FBE18
		public static VehicleManager instance
		{
			get
			{
				return VehicleManager.manager;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x0600295E RID: 10590 RVA: 0x000FDA1F File Offset: 0x000FBE1F
		public static List<InteractableVehicle> vehicles
		{
			get
			{
				return VehicleManager._vehicles;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x0600295F RID: 10591 RVA: 0x000FDA28 File Offset: 0x000FBE28
		public static uint maxInstances
		{
			get
			{
				switch (Level.info.size)
				{
				case ELevelSize.TINY:
					return Provider.modeConfigData.Vehicles.Max_Instances_Tiny;
				case ELevelSize.SMALL:
					return Provider.modeConfigData.Vehicles.Max_Instances_Small;
				case ELevelSize.MEDIUM:
					return Provider.modeConfigData.Vehicles.Max_Instances_Medium;
				case ELevelSize.LARGE:
					return Provider.modeConfigData.Vehicles.Max_Instances_Large;
				case ELevelSize.INSANE:
					return Provider.modeConfigData.Vehicles.Max_Instances_Insane;
				default:
					return 0u;
				}
			}
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x000FDAB0 File Offset: 0x000FBEB0
		public static byte getVehicleRandomTireAliveMask(VehicleAsset asset)
		{
			if (asset.canTiresBeDamaged)
			{
				int num = 0;
				for (byte b = 0; b < 8; b += 1)
				{
					if (UnityEngine.Random.value < Provider.modeConfigData.Vehicles.Has_Tire_Chance)
					{
						int num2 = 1 << (int)b;
						num |= num2;
					}
				}
				return (byte)num;
			}
			return byte.MaxValue;
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x000FDB0C File Offset: 0x000FBF0C
		public static void getVehiclesInRadius(Vector3 center, float sqrRadius, List<InteractableVehicle> result)
		{
			if (VehicleManager.vehicles == null)
			{
				return;
			}
			for (int i = 0; i < VehicleManager.vehicles.Count; i++)
			{
				InteractableVehicle interactableVehicle = VehicleManager.vehicles[i];
				if (!interactableVehicle.isDead)
				{
					if ((interactableVehicle.transform.position - center).sqrMagnitude < sqrRadius)
					{
						result.Add(interactableVehicle);
					}
				}
			}
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x000FDB84 File Offset: 0x000FBF84
		public static InteractableVehicle getVehicle(uint instanceID)
		{
			ushort num = 0;
			while ((int)num < VehicleManager.vehicles.Count)
			{
				if (VehicleManager.vehicles[(int)num].instanceID == instanceID)
				{
					return VehicleManager.vehicles[(int)num];
				}
				num += 1;
			}
			return null;
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x000FDBD0 File Offset: 0x000FBFD0
		public static void damage(InteractableVehicle vehicle, float damage, float times, bool canRepair)
		{
			if (vehicle == null || vehicle.asset == null)
			{
				return;
			}
			if (!vehicle.isDead)
			{
				if (!vehicle.asset.isVulnerable && !vehicle.asset.isVulnerableToExplosions && !vehicle.asset.isVulnerableToEnvironment)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Somehow tried to damage completely invulnerable vehicle: ",
						vehicle,
						" ",
						damage,
						" ",
						times,
						" ",
						canRepair
					}));
					return;
				}
				times *= Provider.modeConfigData.Vehicles.Armor_Multiplier;
				ushort amount = (ushort)(damage * times);
				vehicle.askDamage(amount, canRepair);
			}
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000FDCA0 File Offset: 0x000FC0A0
		public static void repair(InteractableVehicle vehicle, float damage, float times)
		{
			if (vehicle == null)
			{
				return;
			}
			if (!vehicle.isExploded && !vehicle.isRepaired)
			{
				ushort amount = (ushort)(damage * times);
				vehicle.askRepair(amount);
			}
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x000FDCDC File Offset: 0x000FC0DC
		public static void spawnVehicle(ushort id, Vector3 point, Quaternion angle)
		{
			VehicleAsset vehicleAsset = (VehicleAsset)Assets.find(EAssetType.VEHICLE, id);
			if (vehicleAsset != null)
			{
				VehicleManager.manager.addVehicle(id, 0, 0, 0f, point, angle, false, false, false, false, vehicleAsset.fuel, false, vehicleAsset.health, 10000, CSteamID.Nil, CSteamID.Nil, false, null, null, VehicleManager.instanceCount += 1u, byte.MaxValue);
				VehicleManager.manager.channel.openWrite();
				VehicleManager.manager.sendVehicle(VehicleManager.vehicles[VehicleManager.vehicles.Count - 1]);
				VehicleManager.manager.channel.closeWrite("tellVehicle", ESteamCall.OTHERS, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
				BarricadeManager.askPlants(VehicleManager.vehicles[VehicleManager.vehicles.Count - 1].transform);
			}
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x000FDDAC File Offset: 0x000FC1AC
		public static void enterVehicle(InteractableVehicle vehicle)
		{
			ushort num = 0;
			while ((int)num < VehicleManager.vehicles.Count)
			{
				if (vehicle == VehicleManager.vehicles[(int)num])
				{
					VehicleManager.manager.channel.send("askEnterVehicle", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
					{
						vehicle.instanceID,
						vehicle.asset.hash,
						(byte)vehicle.asset.engine
					});
					return;
				}
				num += 1;
			}
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x000FDE3C File Offset: 0x000FC23C
		public static void exitVehicle()
		{
			if (Player.player.movement.getVehicle() != null)
			{
				VehicleManager.manager.channel.send("askExitVehicle", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					Player.player.movement.getVehicle().GetComponent<Rigidbody>().velocity
				});
			}
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x000FDEA4 File Offset: 0x000FC2A4
		public static void swapVehicle(byte toSeat)
		{
			if (Player.player.movement.getVehicle() != null)
			{
				VehicleManager.manager.channel.send("askSwapVehicle", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					toSeat
				});
			}
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000FDEF1 File Offset: 0x000FC2F1
		public static void sendVehicleLock()
		{
			if (Player.player.movement.getVehicle() != null)
			{
				VehicleManager.manager.channel.send("askVehicleLock", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
			}
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x000FDF2A File Offset: 0x000FC32A
		public static void sendVehicleSkin()
		{
			if (Player.player.movement.getVehicle() != null)
			{
				VehicleManager.manager.channel.send("askVehicleSkin", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
			}
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000FDF63 File Offset: 0x000FC363
		public static void sendVehicleHeadlights()
		{
			if (Player.player.movement.getVehicle() != null)
			{
				VehicleManager.manager.channel.send("askVehicleHeadlights", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
			}
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x000FDF9C File Offset: 0x000FC39C
		public static void sendVehicleBonus()
		{
			if (Player.player.movement.getVehicle() != null)
			{
				VehicleManager.manager.channel.send("askVehicleBonus", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
			}
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x000FDFD5 File Offset: 0x000FC3D5
		public static void sendVehicleStealBattery()
		{
			if (Player.player.movement.getVehicle() != null)
			{
				VehicleManager.manager.channel.send("askVehicleStealBattery", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
			}
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000FE00E File Offset: 0x000FC40E
		public static void sendVehicleHorn()
		{
			if (Player.player.movement.getVehicle() != null)
			{
				VehicleManager.manager.channel.send("askVehicleHorn", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
			}
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000FE048 File Offset: 0x000FC448
		public void sendVehicle(InteractableVehicle vehicle)
		{
			Vector3 position;
			if (vehicle.asset.engine == EEngine.TRAIN)
			{
				position = new Vector3(vehicle.roadPosition, 0f, 0f);
			}
			else
			{
				position = vehicle.transform.position;
			}
			base.channel.write(new object[]
			{
				vehicle.id,
				vehicle.skinID,
				vehicle.mythicID,
				position,
				MeasurementTool.angleToByte2(vehicle.transform.rotation.eulerAngles.x),
				MeasurementTool.angleToByte2(vehicle.transform.rotation.eulerAngles.y),
				MeasurementTool.angleToByte2(vehicle.transform.rotation.eulerAngles.z),
				vehicle.sirensOn,
				vehicle.isBlimpFloating,
				vehicle.headlightsOn,
				vehicle.taillightsOn,
				vehicle.fuel,
				vehicle.isExploded,
				vehicle.health,
				vehicle.batteryCharge,
				vehicle.lockedOwner,
				vehicle.lockedGroup,
				vehicle.isLocked,
				vehicle.instanceID,
				vehicle.tireAliveMask
			});
			base.channel.write((byte)vehicle.passengers.Length);
			byte b = 0;
			while ((int)b < vehicle.passengers.Length)
			{
				Passenger passenger = vehicle.passengers[(int)b];
				if (passenger.player != null)
				{
					base.channel.write(passenger.player.playerID.steamID);
				}
				else
				{
					base.channel.write(CSteamID.Nil);
				}
				b += 1;
			}
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000FE298 File Offset: 0x000FC698
		[SteamCall]
		public void tellVehicleLock(CSteamID steamID, uint instanceID, CSteamID owner, CSteamID group, bool locked)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						VehicleManager.vehicles[i].tellLocked(owner, group, locked);
						return;
					}
				}
			}
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000FE300 File Offset: 0x000FC700
		[SteamCall]
		public void tellVehicleSkin(CSteamID steamID, uint instanceID, ushort skinID, ushort mythicID)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						VehicleManager.vehicles[i].tellSkin(skinID, mythicID);
						return;
					}
				}
			}
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000FE364 File Offset: 0x000FC764
		[SteamCall]
		public void tellVehicleSirens(CSteamID steamID, uint instanceID, bool on)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						VehicleManager.vehicles[i].tellSirens(on);
						return;
					}
				}
			}
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x000FE3C8 File Offset: 0x000FC7C8
		[SteamCall]
		public void tellVehicleBlimp(CSteamID steamID, uint instanceID, bool on)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						VehicleManager.vehicles[i].tellBlimp(on);
						return;
					}
				}
			}
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000FE42C File Offset: 0x000FC82C
		[SteamCall]
		public void tellVehicleHeadlights(CSteamID steamID, uint instanceID, bool on)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						VehicleManager.vehicles[i].tellHeadlights(on);
						return;
					}
				}
			}
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x000FE490 File Offset: 0x000FC890
		[SteamCall]
		public void tellVehicleHorn(CSteamID steamID, uint instanceID)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						VehicleManager.vehicles[i].tellHorn();
						return;
					}
				}
			}
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x000FE4F0 File Offset: 0x000FC8F0
		[SteamCall]
		public void tellVehicleFuel(CSteamID steamID, uint instanceID, ushort newFuel)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						VehicleManager.vehicles[i].tellFuel(newFuel);
						return;
					}
				}
			}
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000FE554 File Offset: 0x000FC954
		[SteamCall]
		public void tellVehicleBatteryCharge(CSteamID steamID, uint instanceID, ushort newBatteryCharge)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						VehicleManager.vehicles[i].tellBatteryCharge(newBatteryCharge);
						return;
					}
				}
			}
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000FE5B8 File Offset: 0x000FC9B8
		[SteamCall]
		public void tellVehicleTireAliveMask(CSteamID steamID, uint instanceID, byte newTireAliveMask)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						VehicleManager.vehicles[i].tireAliveMask = newTireAliveMask;
						return;
					}
				}
			}
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000FE61C File Offset: 0x000FCA1C
		[SteamCall]
		public void tellVehicleExploded(CSteamID steamID, uint instanceID)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						if (!VehicleManager.vehicles[i].isExploded)
						{
							BarricadeManager.trimPlant(VehicleManager.vehicles[i].transform);
							if (VehicleManager.vehicles[i].trainCars != null)
							{
								for (int j = 1; j < VehicleManager.vehicles[i].trainCars.Length; j++)
								{
									BarricadeManager.uprootPlant(VehicleManager.vehicles[i].trainCars[j].root);
								}
							}
						}
						VehicleManager.vehicles[i].tellExploded();
						return;
					}
				}
			}
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x000FE6FC File Offset: 0x000FCAFC
		[SteamCall]
		public void tellVehicleHealth(CSteamID steamID, uint instanceID, ushort newHealth)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						VehicleManager.vehicles[i].tellHealth(newHealth);
						return;
					}
				}
			}
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000FE760 File Offset: 0x000FCB60
		[SteamCall]
		public void tellVehicleRecov(CSteamID steamID, uint instanceID, Vector3 newPosition, int newRecov)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						VehicleManager.vehicles[i].tellRecov(newPosition, newRecov);
						return;
					}
				}
			}
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x000FE7C4 File Offset: 0x000FCBC4
		[SteamCall]
		public void tellVehicleStates(CSteamID steamID)
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
				for (ushort num3 = 0; num3 < num2; num3 += 1)
				{
					object[] array = base.channel.read(new Type[]
					{
						Types.UINT32_TYPE,
						Types.VECTOR3_TYPE,
						Types.BYTE_TYPE,
						Types.BYTE_TYPE,
						Types.BYTE_TYPE,
						Types.BYTE_TYPE,
						Types.BYTE_TYPE,
						Types.BYTE_TYPE
					});
					uint num4 = (uint)array[0];
					for (int i = 0; i < VehicleManager.vehicles.Count; i++)
					{
						if (VehicleManager.vehicles[i].instanceID == num4)
						{
							VehicleManager.vehicles[i].tellState((Vector3)array[1], (byte)array[2], (byte)array[3], (byte)array[4], (byte)array[5], (byte)array[6], (byte)array[7]);
							break;
						}
					}
				}
				base.channel.useCompression = false;
			}
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x000FE930 File Offset: 0x000FCD30
		[SteamCall]
		public void tellVehicleDestroy(CSteamID steamID, uint instanceID)
		{
			if (base.channel.checkServer(steamID))
			{
				InteractableVehicle interactableVehicle = null;
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						interactableVehicle = VehicleManager.vehicles[i];
						VehicleManager.vehicles.RemoveAt(i);
						break;
					}
				}
				if (interactableVehicle == null)
				{
					return;
				}
				BarricadeManager.uprootPlant(interactableVehicle.transform);
				if (interactableVehicle.trainCars != null)
				{
					for (int j = 1; j < interactableVehicle.trainCars.Length; j++)
					{
						BarricadeManager.uprootPlant(interactableVehicle.trainCars[j].root);
					}
				}
				UnityEngine.Object.Destroy(interactableVehicle.gameObject);
				VehicleManager.respawnVehicleIndex -= 1;
			}
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x000FEA04 File Offset: 0x000FCE04
		[SteamCall]
		public void tellVehicleDestroyAll(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = VehicleManager.vehicles.Count - 1; i >= 0; i--)
				{
					BarricadeManager.uprootPlant(VehicleManager.vehicles[i].transform);
					if (VehicleManager.vehicles[i].trainCars != null)
					{
						for (int j = 1; j < VehicleManager.vehicles[i].trainCars.Length; j++)
						{
							BarricadeManager.uprootPlant(VehicleManager.vehicles[i].trainCars[j].root);
						}
					}
					UnityEngine.Object.Destroy(VehicleManager.vehicles[i].gameObject);
					VehicleManager.vehicles.RemoveAt(i);
				}
				VehicleManager.respawnVehicleIndex = 0;
				VehicleManager.vehicles.Clear();
			}
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x000FEAD8 File Offset: 0x000FCED8
		public static void askVehicleDestroy(InteractableVehicle vehicle)
		{
			if (Provider.isServer)
			{
				vehicle.forceRemoveAllPlayers();
				VehicleManager.manager.channel.send("tellVehicleDestroy", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					vehicle.instanceID
				});
			}
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x000FEB18 File Offset: 0x000FCF18
		public static void askVehicleDestroyAll()
		{
			if (Provider.isServer)
			{
				for (int i = VehicleManager.vehicles.Count - 1; i >= 0; i--)
				{
					InteractableVehicle interactableVehicle = VehicleManager.vehicles[i];
					interactableVehicle.forceRemoveAllPlayers();
				}
				VehicleManager.manager.channel.send("tellVehicleDestroyAll", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
			}
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x000FEB7C File Offset: 0x000FCF7C
		[SteamCall]
		public void tellVehicle(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID))
			{
				object[] array = base.channel.read(new Type[]
				{
					Types.UINT16_TYPE,
					Types.UINT16_TYPE,
					Types.UINT16_TYPE,
					Types.VECTOR3_TYPE,
					Types.BYTE_TYPE,
					Types.BYTE_TYPE,
					Types.BYTE_TYPE,
					Types.BOOLEAN_TYPE,
					Types.BOOLEAN_TYPE,
					Types.BOOLEAN_TYPE,
					Types.BOOLEAN_TYPE,
					Types.UINT16_TYPE,
					Types.BOOLEAN_TYPE,
					Types.UINT16_TYPE,
					Types.UINT16_TYPE,
					Types.STEAM_ID_TYPE,
					Types.STEAM_ID_TYPE,
					Types.BOOLEAN_TYPE,
					Types.UINT32_TYPE,
					Types.BYTE_TYPE
				});
				CSteamID[] array2 = new CSteamID[(int)((byte)base.channel.read(Types.BYTE_TYPE))];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = (CSteamID)base.channel.read(Types.STEAM_ID_TYPE);
				}
				this.addVehicle((ushort)array[0], (ushort)array[1], (ushort)array[2], ((Vector3)array[3]).x, (Vector3)array[3], Quaternion.Euler(MeasurementTool.byteToAngle2((byte)array[4]), MeasurementTool.byteToAngle2((byte)array[5]), MeasurementTool.byteToAngle2((byte)array[6])), (bool)array[7], (bool)array[8], (bool)array[9], (bool)array[10], (ushort)array[11], (bool)array[12], (ushort)array[13], (ushort)array[14], (CSteamID)array[15], (CSteamID)array[16], (bool)array[17], array2, null, (uint)array[18], (byte)array[19]);
			}
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000FED80 File Offset: 0x000FD180
		[SteamCall]
		public void tellVehicles(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID))
			{
				ushort num = (ushort)base.channel.read(Types.UINT16_TYPE);
				for (int i = 0; i < (int)num; i++)
				{
					this.tellVehicle(steamID);
				}
				Level.isLoadingVehicles = false;
			}
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000FEDD4 File Offset: 0x000FD1D4
		[SteamCall]
		public void askVehicles(CSteamID steamID)
		{
			SteamPlayer steamPlayer = PlayerTool.getSteamPlayer(steamID);
			if (steamPlayer == null)
			{
				return;
			}
			if (steamPlayer.rpcHasAskedVehicles)
			{
				return;
			}
			steamPlayer.rpcHasAskedVehicles = true;
			base.channel.openWrite();
			base.channel.write((ushort)VehicleManager.vehicles.Count);
			for (int i = 0; i < VehicleManager.vehicles.Count; i++)
			{
				InteractableVehicle vehicle = VehicleManager.vehicles[i];
				this.sendVehicle(vehicle);
			}
			base.channel.closeWrite("tellVehicles", steamID, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
			BarricadeManager.askPlants(steamID);
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x000FEE70 File Offset: 0x000FD270
		[SteamCall]
		public void tellEnterVehicle(CSteamID steamID, uint instanceID, byte seat, CSteamID player)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						InteractableVehicle interactableVehicle = VehicleManager.vehicles[i];
						interactableVehicle.addPlayer(seat, player);
						return;
					}
				}
			}
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x000FEED8 File Offset: 0x000FD2D8
		[SteamCall]
		public void tellExitVehicle(CSteamID steamID, uint instanceID, byte seat, Vector3 point, byte angle, bool forceUpdate)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						InteractableVehicle interactableVehicle = VehicleManager.vehicles[i];
						interactableVehicle.removePlayer(seat, point, angle, forceUpdate);
						return;
					}
				}
			}
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x000FEF44 File Offset: 0x000FD344
		[SteamCall]
		public void tellSwapVehicle(CSteamID steamID, uint instanceID, byte fromSeat, byte toSeat)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						InteractableVehicle interactableVehicle = VehicleManager.vehicles[i];
						interactableVehicle.swapPlayer(fromSeat, toSeat);
						return;
					}
				}
			}
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x000FEFAC File Offset: 0x000FD3AC
		public static void unlockVehicle(InteractableVehicle vehicle, Player instigatingPlayer)
		{
			if (vehicle == null)
			{
				return;
			}
			bool flag = true;
			if (VehicleManager.onVehicleLockpicked != null)
			{
				VehicleManager.onVehicleLockpicked(vehicle, instigatingPlayer, ref flag);
			}
			if (!flag)
			{
				return;
			}
			VehicleManager.manager.channel.send("tellVehicleLock", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				vehicle.instanceID,
				CSteamID.Nil,
				CSteamID.Nil,
				false
			});
			EffectManager.sendEffect(8, EffectManager.SMALL, vehicle.transform.position);
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x000FF04C File Offset: 0x000FD44C
		[SteamCall]
		public void askVehicleLock(CSteamID steamID)
		{
			if (Provider.isServer)
			{
				Player player = PlayerTool.getPlayer(steamID);
				if (player == null)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				InteractableVehicle vehicle = player.movement.getVehicle();
				if (vehicle == null)
				{
					return;
				}
				if (!vehicle.checkDriver(steamID))
				{
					return;
				}
				VehicleManager.manager.channel.send("tellVehicleLock", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					vehicle.instanceID,
					player.channel.owner.playerID.steamID,
					player.quests.groupID,
					!vehicle.isLocked
				});
				EffectManager.sendEffect(8, EffectManager.SMALL, vehicle.transform.position);
			}
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x000FF12C File Offset: 0x000FD52C
		[SteamCall]
		public void askVehicleSkin(CSteamID steamID)
		{
			if (Provider.isServer)
			{
				Player player = PlayerTool.getPlayer(steamID);
				if (player == null)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				InteractableVehicle vehicle = player.movement.getVehicle();
				if (vehicle == null)
				{
					return;
				}
				if (!vehicle.checkDriver(steamID))
				{
					return;
				}
				int item = 0;
				ushort num = 0;
				ushort num2 = 0;
				if (player.channel.owner.skinItems != null && player.channel.owner.vehicleSkins != null && player.channel.owner.vehicleSkins.TryGetValue(vehicle.asset.sharedSkinLookupID, out item))
				{
					num = Provider.provider.economyService.getInventorySkinID(item);
					num2 = Provider.provider.economyService.getInventoryMythicID(item);
				}
				if (num != 0)
				{
					if (num == vehicle.skinID && num2 == vehicle.mythicID)
					{
						num = 0;
						num2 = 0;
					}
				}
				else if (!vehicle.isSkinned)
				{
					return;
				}
				VehicleManager.manager.channel.send("tellVehicleSkin", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					vehicle.instanceID,
					num,
					num2
				});
			}
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x000FF278 File Offset: 0x000FD678
		[SteamCall]
		public void askVehicleHeadlights(CSteamID steamID)
		{
			if (Provider.isServer)
			{
				Player player = PlayerTool.getPlayer(steamID);
				if (player == null)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				InteractableVehicle vehicle = player.movement.getVehicle();
				if (vehicle == null)
				{
					return;
				}
				if (!vehicle.canTurnOnLights)
				{
					return;
				}
				if (!vehicle.checkDriver(steamID))
				{
					return;
				}
				if (!vehicle.asset.hasHeadlights)
				{
					return;
				}
				VehicleManager.manager.channel.send("tellVehicleHeadlights", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					vehicle.instanceID,
					!vehicle.headlightsOn
				});
				EffectManager.sendEffect(8, EffectManager.SMALL, vehicle.transform.position);
			}
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x000FF344 File Offset: 0x000FD744
		[SteamCall]
		public void askVehicleBonus(CSteamID steamID)
		{
			if (Provider.isServer)
			{
				Player player = PlayerTool.getPlayer(steamID);
				if (player == null)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				InteractableVehicle vehicle = player.movement.getVehicle();
				if (vehicle == null)
				{
					return;
				}
				if (!vehicle.checkDriver(steamID))
				{
					return;
				}
				if (vehicle.asset.hasSirens)
				{
					if (!vehicle.canTurnOnLights)
					{
						return;
					}
					VehicleManager.manager.channel.send("tellVehicleSirens", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						vehicle.instanceID,
						!vehicle.sirensOn
					});
					EffectManager.sendEffect(8, EffectManager.SMALL, vehicle.transform.position);
				}
				else if (vehicle.asset.hasHook)
				{
					vehicle.useHook();
				}
				else if (vehicle.asset.engine == EEngine.BLIMP)
				{
					VehicleManager.manager.channel.send("tellVehicleBlimp", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						vehicle.instanceID,
						!vehicle.isBlimpFloating
					});
				}
			}
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x000FF47C File Offset: 0x000FD87C
		[SteamCall]
		public void askVehicleStealBattery(CSteamID steamID)
		{
			if (Provider.isServer)
			{
				Player player = PlayerTool.getPlayer(steamID);
				if (player == null)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				InteractableVehicle vehicle = player.movement.getVehicle();
				if (vehicle == null)
				{
					return;
				}
				if (!vehicle.checkDriver(steamID))
				{
					return;
				}
				if (!vehicle.usesBattery)
				{
					return;
				}
				if (!vehicle.hasBattery)
				{
					return;
				}
				vehicle.stealBattery(player);
			}
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000FF4F8 File Offset: 0x000FD8F8
		[SteamCall]
		public void askVehicleHorn(CSteamID steamID)
		{
			if (Provider.isServer)
			{
				Player player = PlayerTool.getPlayer(steamID);
				if (player == null)
				{
					return;
				}
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				InteractableVehicle vehicle = player.movement.getVehicle();
				if (vehicle == null)
				{
					return;
				}
				if (!vehicle.canUseHorn)
				{
					return;
				}
				if (!vehicle.checkDriver(steamID))
				{
					return;
				}
				VehicleManager.manager.channel.send("tellVehicleHorn", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					vehicle.instanceID
				});
			}
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x000FF58C File Offset: 0x000FD98C
		[SteamCall]
		public void askEnterVehicle(CSteamID steamID, uint instanceID, byte[] hash, byte engine)
		{
			if (Provider.isServer)
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
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if (player.equipment.isSelected && !player.equipment.isEquipped)
				{
					return;
				}
				if (player.movement.getVehicle() != null)
				{
					return;
				}
				InteractableVehicle interactableVehicle = null;
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i].instanceID == instanceID)
					{
						interactableVehicle = VehicleManager.vehicles[i];
						break;
					}
				}
				if (interactableVehicle == null)
				{
					return;
				}
				if (interactableVehicle.asset.shouldVerifyHash && !Hash.verifyHash(hash, interactableVehicle.asset.hash))
				{
					return;
				}
				if ((EEngine)engine != interactableVehicle.asset.engine)
				{
					return;
				}
				if ((interactableVehicle.transform.position - player.transform.position).sqrMagnitude > 100f)
				{
					return;
				}
				if (!interactableVehicle.checkEnter(player.channel.owner.playerID.steamID, player.quests.groupID))
				{
					return;
				}
				byte b;
				if (interactableVehicle.tryAddPlayer(out b, player))
				{
					base.channel.send("tellEnterVehicle", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						instanceID,
						b,
						steamID
					});
				}
			}
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x000FF748 File Offset: 0x000FDB48
		[SteamCall]
		public void askExitVehicle(CSteamID steamID, Vector3 velocity)
		{
			if (Provider.isServer)
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
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				InteractableVehicle vehicle = player.movement.getVehicle();
				if (vehicle == null)
				{
					return;
				}
				byte b;
				Vector3 vector;
				byte b2;
				if (vehicle.tryRemovePlayer(out b, steamID, out vector, out b2))
				{
					base.channel.send("tellExitVehicle", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						vehicle.instanceID,
						b,
						vector,
						b2,
						false
					});
					if (b == 0 && Dedicator.isDedicated)
					{
						vehicle.GetComponent<Rigidbody>().velocity = velocity;
					}
				}
			}
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x000FF834 File Offset: 0x000FDC34
		public static void forceRemovePlayer(InteractableVehicle vehicle, CSteamID player)
		{
			byte b;
			Vector3 vector;
			byte b2;
			if (vehicle.forceRemovePlayer(out b, player, out vector, out b2))
			{
				VehicleManager.manager.channel.send("tellExitVehicle", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					vehicle.instanceID,
					b,
					vector,
					b2,
					true
				});
			}
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x000FF8A4 File Offset: 0x000FDCA4
		[SteamCall]
		public void askSwapVehicle(CSteamID steamID, byte toSeat)
		{
			if (Provider.isServer)
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
				if (!player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if (player.equipment.isSelected && !player.equipment.isEquipped)
				{
					return;
				}
				InteractableVehicle vehicle = player.movement.getVehicle();
				if (vehicle == null)
				{
					return;
				}
				if (Time.realtimeSinceStartup - vehicle.lastSeat < 1f)
				{
					return;
				}
				vehicle.lastSeat = Time.realtimeSinceStartup;
				byte b;
				if (vehicle.trySwapPlayer(player, toSeat, out b))
				{
					base.channel.send("tellSwapVehicle", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						vehicle.instanceID,
						b,
						toSeat
					});
				}
			}
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000FF9A4 File Offset: 0x000FDDA4
		public static void sendExitVehicle(InteractableVehicle vehicle, byte seat, Vector3 point, byte angle, bool forceUpdate)
		{
			VehicleManager.manager.channel.send("tellExitVehicle", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				vehicle.instanceID,
				seat,
				point,
				angle,
				forceUpdate
			});
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000FFA01 File Offset: 0x000FDE01
		public static void sendVehicleFuel(InteractableVehicle vehicle, ushort newFuel)
		{
			VehicleManager.manager.channel.send("tellVehicleFuel", ESteamCall.CLIENTS, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				vehicle.instanceID,
				newFuel
			});
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000FFA37 File Offset: 0x000FDE37
		public static void sendVehicleBatteryCharge(InteractableVehicle vehicle, ushort newBatteryCharge)
		{
			VehicleManager.manager.channel.send("tellVehicleBatteryCharge", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				vehicle.instanceID,
				newBatteryCharge
			});
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000FFA6D File Offset: 0x000FDE6D
		public static void sendVehicleTireAliveMask(InteractableVehicle vehicle, byte newTireAliveMask)
		{
			VehicleManager.manager.channel.send("tellVehicleTireAliveMask", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				vehicle.instanceID,
				newTireAliveMask
			});
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000FFAA3 File Offset: 0x000FDEA3
		public static void sendVehicleExploded(InteractableVehicle vehicle)
		{
			VehicleManager.manager.channel.send("tellVehicleExploded", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				vehicle.instanceID
			});
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x000FFAD0 File Offset: 0x000FDED0
		public static void sendVehicleHealth(InteractableVehicle vehicle, ushort newHealth)
		{
			VehicleManager.manager.channel.send("tellVehicleHealth", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				vehicle.instanceID,
				newHealth
			});
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000FFB08 File Offset: 0x000FDF08
		public static void sendVehicleRecov(InteractableVehicle vehicle, Vector3 newPosition, int newRecov)
		{
			if (vehicle.passengers[0].player != null)
			{
				VehicleManager.manager.channel.send("tellVehicleRecov", vehicle.passengers[0].player.playerID.steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					vehicle.instanceID,
					newPosition,
					newRecov
				});
			}
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000FFB7C File Offset: 0x000FDF7C
		private InteractableVehicle addVehicle(ushort id, ushort skinID, ushort mythicID, float roadPosition, Vector3 point, Quaternion angle, bool sirens, bool blimp, bool headlights, bool taillights, ushort fuel, bool isExploded, ushort health, ushort batteryCharge, CSteamID owner, CSteamID group, bool locked, CSteamID[] passengers, byte[][] turrets, uint instanceID, byte tireAliveMask)
		{
			if (id == 0)
			{
				return null;
			}
			VehicleAsset vehicleAsset = (VehicleAsset)Assets.find(EAssetType.VEHICLE, id);
			if (vehicleAsset != null)
			{
				Transform transform;
				if (Dedicator.isDedicated && vehicleAsset.clip != null)
				{
					transform = UnityEngine.Object.Instantiate<GameObject>(vehicleAsset.clip).transform;
				}
				else
				{
					transform = UnityEngine.Object.Instantiate<GameObject>(vehicleAsset.vehicle).transform;
				}
				transform.name = id.ToString();
				transform.parent = LevelVehicles.models;
				transform.position = point;
				transform.rotation = angle;
				transform.GetComponent<Rigidbody>().useGravity = true;
				transform.GetComponent<Rigidbody>().isKinematic = false;
				InteractableVehicle interactableVehicle = transform.gameObject.AddComponent<InteractableVehicle>();
				interactableVehicle.roadPosition = roadPosition;
				interactableVehicle.instanceID = instanceID;
				interactableVehicle.id = id;
				interactableVehicle.skinID = skinID;
				interactableVehicle.mythicID = mythicID;
				interactableVehicle.fuel = fuel;
				interactableVehicle.isExploded = isExploded;
				interactableVehicle.health = health;
				interactableVehicle.batteryCharge = batteryCharge;
				interactableVehicle.init();
				interactableVehicle.tellSirens(sirens);
				interactableVehicle.tellBlimp(blimp);
				interactableVehicle.tellHeadlights(headlights);
				interactableVehicle.tellTaillights(taillights);
				interactableVehicle.tellLocked(owner, group, locked);
				interactableVehicle.tireAliveMask = tireAliveMask;
				if (Provider.isServer)
				{
					if (turrets != null && turrets.Length == interactableVehicle.turrets.Length)
					{
						byte b = 0;
						while ((int)b < interactableVehicle.turrets.Length)
						{
							interactableVehicle.turrets[(int)b].state = turrets[(int)b];
							b += 1;
						}
					}
					else
					{
						byte b2 = 0;
						while ((int)b2 < interactableVehicle.turrets.Length)
						{
							ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, vehicleAsset.turrets[(int)b2].itemID);
							if (itemAsset != null)
							{
								interactableVehicle.turrets[(int)b2].state = itemAsset.getState();
							}
							else
							{
								interactableVehicle.turrets[(int)b2].state = null;
							}
							b2 += 1;
						}
					}
				}
				if (passengers != null)
				{
					byte b3 = 0;
					while ((int)b3 < passengers.Length)
					{
						if (passengers[(int)b3] != CSteamID.Nil)
						{
							interactableVehicle.addPlayer(b3, passengers[(int)b3]);
						}
						b3 += 1;
					}
				}
				if (vehicleAsset.trunkStorage_Y > 0)
				{
					interactableVehicle.trunkItems = new Items(PlayerInventory.STORAGE);
					interactableVehicle.trunkItems.resize(vehicleAsset.trunkStorage_X, vehicleAsset.trunkStorage_Y);
				}
				VehicleManager.vehicles.Add(interactableVehicle);
				BarricadeManager.waterPlant(transform);
				if (interactableVehicle.trainCars != null)
				{
					for (int i = 1; i < interactableVehicle.trainCars.Length; i++)
					{
						BarricadeManager.waterPlant(interactableVehicle.trainCars[i].root);
					}
				}
				return interactableVehicle;
			}
			if (!Provider.isServer)
			{
				Provider.connectionFailureInfo = ESteamConnectionFailureInfo.VEHICLE;
				Provider.disconnect();
			}
			return null;
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000FFE60 File Offset: 0x000FE260
		private void respawnVehicles()
		{
			if (Level.info == null || Level.info.type == ELevelType.ARENA)
			{
				return;
			}
			if ((int)VehicleManager.respawnVehicleIndex >= VehicleManager.vehicles.Count)
			{
				VehicleManager.respawnVehicleIndex = (ushort)(VehicleManager.vehicles.Count - 1);
			}
			InteractableVehicle interactableVehicle = VehicleManager.vehicles[(int)VehicleManager.respawnVehicleIndex];
			VehicleManager.respawnVehicleIndex += 1;
			if ((int)VehicleManager.respawnVehicleIndex >= VehicleManager.vehicles.Count)
			{
				VehicleManager.respawnVehicleIndex = 0;
			}
			if ((interactableVehicle.isExploded && Time.realtimeSinceStartup - interactableVehicle.lastExploded > Provider.modeConfigData.Vehicles.Respawn_Time) || (interactableVehicle.isDrowned && Time.realtimeSinceStartup - interactableVehicle.lastUnderwater > Provider.modeConfigData.Vehicles.Respawn_Time))
			{
				if (interactableVehicle.asset.engine == EEngine.TRAIN)
				{
					return;
				}
				if (!interactableVehicle.isEmpty)
				{
					return;
				}
				VehicleSpawnpoint vehicleSpawnpoint = null;
				if ((long)VehicleManager.vehicles.Count < (long)((ulong)VehicleManager.maxInstances))
				{
					vehicleSpawnpoint = LevelVehicles.spawns[UnityEngine.Random.Range(0, LevelVehicles.spawns.Count)];
					ushort num = 0;
					while ((int)num < VehicleManager.vehicles.Count)
					{
						if ((VehicleManager.vehicles[(int)num].transform.position - vehicleSpawnpoint.point).sqrMagnitude < 64f)
						{
							return;
						}
						num += 1;
					}
				}
				VehicleManager.askVehicleDestroy(interactableVehicle);
				if (vehicleSpawnpoint != null)
				{
					Vector3 point = vehicleSpawnpoint.point;
					point.y += 0.5f;
					ushort vehicle = LevelVehicles.getVehicle(vehicleSpawnpoint);
					VehicleAsset vehicleAsset = (VehicleAsset)Assets.find(EAssetType.VEHICLE, vehicle);
					if (vehicleAsset != null)
					{
						this.addVehicle(vehicle, 0, 0, 0f, point, Quaternion.Euler(0f, vehicleSpawnpoint.angle, 0f), false, false, false, false, ushort.MaxValue, false, ushort.MaxValue, ushort.MaxValue, CSteamID.Nil, CSteamID.Nil, false, null, null, VehicleManager.instanceCount += 1u, VehicleManager.getVehicleRandomTireAliveMask(vehicleAsset));
						VehicleManager.manager.channel.openWrite();
						VehicleManager.manager.sendVehicle(VehicleManager.vehicles[VehicleManager.vehicles.Count - 1]);
						VehicleManager.manager.channel.closeWrite("tellVehicle", ESteamCall.OTHERS, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
					}
				}
			}
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x001000C4 File Offset: 0x000FE4C4
		private void onLevelLoaded(int level)
		{
			if (level > Level.SETUP)
			{
				this.seq = 0u;
				VehicleManager._vehicles = new List<InteractableVehicle>();
				VehicleManager.instanceCount = 0u;
				VehicleManager.respawnVehicleIndex = 0;
				BarricadeManager.clearPlants();
				if (Provider.isServer)
				{
					if (Level.info != null && Level.info.type != ELevelType.ARENA)
					{
						VehicleManager.load();
						if (LevelVehicles.spawns.Count > 0)
						{
							List<VehicleSpawnpoint> list = new List<VehicleSpawnpoint>();
							for (int i = 0; i < LevelVehicles.spawns.Count; i++)
							{
								list.Add(LevelVehicles.spawns[i]);
							}
							while ((long)VehicleManager.vehicles.Count < (long)((ulong)VehicleManager.maxInstances) && list.Count > 0)
							{
								int index = UnityEngine.Random.Range(0, list.Count);
								VehicleSpawnpoint vehicleSpawnpoint = list[index];
								list.RemoveAt(index);
								bool flag = true;
								ushort num = 0;
								while ((int)num < VehicleManager.vehicles.Count)
								{
									if ((VehicleManager.vehicles[(int)num].transform.position - vehicleSpawnpoint.point).sqrMagnitude < 64f)
									{
										flag = false;
										break;
									}
									num += 1;
								}
								if (flag)
								{
									Vector3 point = vehicleSpawnpoint.point;
									point.y += 0.5f;
									ushort vehicle = LevelVehicles.getVehicle(vehicleSpawnpoint);
									VehicleAsset vehicleAsset = (VehicleAsset)Assets.find(EAssetType.VEHICLE, vehicle);
									if (vehicleAsset != null)
									{
										this.addVehicle(vehicle, 0, 0, 0f, point, Quaternion.Euler(0f, vehicleSpawnpoint.angle, 0f), false, false, false, false, ushort.MaxValue, false, ushort.MaxValue, ushort.MaxValue, CSteamID.Nil, CSteamID.Nil, false, null, null, VehicleManager.instanceCount += 1u, VehicleManager.getVehicleRandomTireAliveMask(vehicleAsset));
									}
								}
							}
						}
						foreach (LevelTrainAssociation levelTrainAssociation in Level.info.configData.Trains)
						{
							bool flag2 = false;
							foreach (InteractableVehicle interactableVehicle in VehicleManager.vehicles)
							{
								if (interactableVehicle.id == levelTrainAssociation.VehicleID)
								{
									flag2 = true;
									break;
								}
							}
							if (!flag2)
							{
								Road road = LevelRoads.getRoad((int)levelTrainAssociation.RoadIndex);
								if (road == null)
								{
									Debug.LogError(string.Concat(new object[]
									{
										"Failed to find track ",
										levelTrainAssociation.RoadIndex,
										" for train ",
										levelTrainAssociation.VehicleID,
										"!"
									}));
								}
								else
								{
									float trackSampledLength = road.trackSampledLength;
									float num2 = UnityEngine.Random.Range(0.1f, 0.9f);
									float roadPosition = trackSampledLength * num2;
									VehicleAsset vehicleAsset2 = (VehicleAsset)Assets.find(EAssetType.VEHICLE, levelTrainAssociation.VehicleID);
									if (vehicleAsset2 != null)
									{
										this.addVehicle(levelTrainAssociation.VehicleID, 0, 0, roadPosition, Vector3.zero, Quaternion.identity, false, false, false, false, ushort.MaxValue, false, ushort.MaxValue, ushort.MaxValue, CSteamID.Nil, CSteamID.Nil, false, null, null, VehicleManager.instanceCount += 1u, VehicleManager.getVehicleRandomTireAliveMask(vehicleAsset2));
									}
									else
									{
										Debug.LogError("Failed to find asset for train " + levelTrainAssociation.VehicleID + "!");
									}
								}
							}
						}
					}
					else
					{
						Level.isLoadingVehicles = false;
					}
					if (VehicleManager.vehicles != null)
					{
						for (int j = 0; j < VehicleManager.vehicles.Count; j++)
						{
							if (VehicleManager.vehicles[j] != null)
							{
								Rigidbody component = VehicleManager.vehicles[j].GetComponent<Rigidbody>();
								if (component != null)
								{
									component.constraints = RigidbodyConstraints.FreezeAll;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x00100508 File Offset: 0x000FE908
		private void onPostLevelLoaded(int level)
		{
			if (level > Level.SETUP && Provider.isServer)
			{
				for (int i = 0; i < VehicleManager.vehicles.Count; i++)
				{
					if (VehicleManager.vehicles[i] != null)
					{
						Rigidbody component = VehicleManager.vehicles[i].GetComponent<Rigidbody>();
						if (component != null)
						{
							component.constraints = RigidbodyConstraints.None;
						}
					}
				}
			}
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x0010057F File Offset: 0x000FE97F
		private void onClientConnected()
		{
			base.channel.send("askVehicles", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x0010059C File Offset: 0x000FE99C
		private void onServerDisconnected(CSteamID player)
		{
			if (Provider.isServer)
			{
				ushort num = 0;
				while ((int)num < VehicleManager.vehicles.Count)
				{
					InteractableVehicle interactableVehicle = VehicleManager.vehicles[(int)num];
					byte b;
					Vector3 vector;
					byte b2;
					if (interactableVehicle.forceRemovePlayer(out b, player, out vector, out b2))
					{
						base.channel.send("tellExitVehicle", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							interactableVehicle.instanceID,
							b,
							vector,
							b2,
							true
						});
					}
					num += 1;
				}
			}
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x0010063C File Offset: 0x000FEA3C
		private void Update()
		{
			if (!Provider.isServer || !Level.isLoaded)
			{
				return;
			}
			if (VehicleManager.vehicles == null || VehicleManager.vehicles.Count == 0)
			{
				return;
			}
			if (Dedicator.isDedicated && Time.realtimeSinceStartup - VehicleManager.lastTick > Provider.UPDATE_TIME)
			{
				VehicleManager.lastTick += Provider.UPDATE_TIME;
				if (Time.realtimeSinceStartup - VehicleManager.lastTick > Provider.UPDATE_TIME)
				{
					VehicleManager.lastTick = Time.realtimeSinceStartup;
				}
				base.channel.useCompression = true;
				this.seq += 1u;
				for (int i = 0; i < Provider.clients.Count; i++)
				{
					SteamPlayer steamPlayer = Provider.clients[i];
					if (steamPlayer != null && !(steamPlayer.player == null))
					{
						base.channel.openWrite();
						base.channel.write(this.seq);
						ushort num = 0;
						int step = base.channel.step;
						base.channel.write(num);
						int num2 = 0;
						for (int j = 0; j < VehicleManager.vehicles.Count; j++)
						{
							if (num >= 70)
							{
								break;
							}
							InteractableVehicle interactableVehicle = VehicleManager.vehicles[j];
							if (!(interactableVehicle == null) && interactableVehicle.updates != null && interactableVehicle.updates.Count != 0)
							{
								if (!interactableVehicle.checkDriver(steamPlayer.playerID.steamID))
								{
									if ((interactableVehicle.transform.position - steamPlayer.player.transform.position).sqrMagnitude > 331776f)
									{
										if ((ulong)(this.seq % 8u) == (ulong)((long)num2))
										{
											VehicleStateUpdate vehicleStateUpdate = interactableVehicle.updates[interactableVehicle.updates.Count - 1];
											base.channel.write(new object[]
											{
												interactableVehicle.instanceID,
												vehicleStateUpdate.pos,
												MeasurementTool.angleToByte2(vehicleStateUpdate.rot.eulerAngles.x),
												MeasurementTool.angleToByte2(vehicleStateUpdate.rot.eulerAngles.y),
												MeasurementTool.angleToByte2(vehicleStateUpdate.rot.eulerAngles.z),
												(byte)(Mathf.Clamp(interactableVehicle.speed, -100f, 100f) + 128f),
												(byte)(Mathf.Clamp(interactableVehicle.physicsSpeed, -100f, 100f) + 128f),
												(byte)(interactableVehicle.turn + 1)
											});
											num += 1;
										}
										num2++;
									}
									else
									{
										for (int k = 0; k < interactableVehicle.updates.Count; k++)
										{
											VehicleStateUpdate vehicleStateUpdate2 = interactableVehicle.updates[k];
											base.channel.write(new object[]
											{
												interactableVehicle.instanceID,
												vehicleStateUpdate2.pos,
												MeasurementTool.angleToByte2(vehicleStateUpdate2.rot.eulerAngles.x),
												MeasurementTool.angleToByte2(vehicleStateUpdate2.rot.eulerAngles.y),
												MeasurementTool.angleToByte2(vehicleStateUpdate2.rot.eulerAngles.z),
												(byte)(Mathf.Clamp(interactableVehicle.speed, -100f, 100f) + 128f),
												(byte)(Mathf.Clamp(interactableVehicle.physicsSpeed, -100f, 100f) + 128f),
												(byte)(interactableVehicle.turn + 1)
											});
										}
										num += (ushort)interactableVehicle.updates.Count;
									}
								}
							}
						}
						if (num != 0)
						{
							int step2 = base.channel.step;
							base.channel.step = step;
							base.channel.write(num);
							base.channel.step = step2;
							base.channel.closeWrite("tellVehicleStates", steamPlayer.playerID.steamID, ESteamPacket.UPDATE_UNRELIABLE_CHUNK_BUFFER);
						}
					}
				}
				base.channel.useCompression = false;
				for (int l = 0; l < VehicleManager.vehicles.Count; l++)
				{
					InteractableVehicle interactableVehicle2 = VehicleManager.vehicles[l];
					if (!(interactableVehicle2 == null) && interactableVehicle2.updates != null && interactableVehicle2.updates.Count != 0)
					{
						interactableVehicle2.updates.Clear();
					}
				}
			}
			if (LevelVehicles.spawns == null || LevelVehicles.spawns.Count == 0)
			{
				return;
			}
			this.respawnVehicles();
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x00100B74 File Offset: 0x000FEF74
		private void Start()
		{
			VehicleManager.manager = this;
			Level.onPrePreLevelLoaded = (PrePreLevelLoaded)Delegate.Combine(Level.onPrePreLevelLoaded, new PrePreLevelLoaded(this.onLevelLoaded));
			Level.onPostLevelLoaded = (PostLevelLoaded)Delegate.Combine(Level.onPostLevelLoaded, new PostLevelLoaded(this.onPostLevelLoaded));
			Provider.onClientConnected = (Provider.ClientConnected)Delegate.Combine(Provider.onClientConnected, new Provider.ClientConnected(this.onClientConnected));
			Provider.onServerDisconnected = (Provider.ServerDisconnected)Delegate.Combine(Provider.onServerDisconnected, new Provider.ServerDisconnected(this.onServerDisconnected));
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x00100C08 File Offset: 0x000FF008
		public static void load()
		{
			if (LevelSavedata.fileExists("/Vehicles.dat") && Level.info.type == ELevelType.SURVIVAL)
			{
				River river = LevelSavedata.openRiver("/Vehicles.dat", true);
				byte b = river.readByte();
				if (b > 2)
				{
					ushort num = river.readUInt16();
					for (ushort num2 = 0; num2 < num; num2 += 1)
					{
						ushort id = river.readUInt16();
						ushort skinID;
						if (b < 8)
						{
							skinID = 0;
						}
						else
						{
							skinID = river.readUInt16();
						}
						ushort mythicID;
						if (b < 9)
						{
							mythicID = 0;
						}
						else
						{
							mythicID = river.readUInt16();
						}
						float roadPosition;
						if (b < 10)
						{
							roadPosition = 0f;
						}
						else
						{
							roadPosition = river.readSingle();
						}
						Vector3 point = river.readSingleVector3();
						Quaternion angle = river.readSingleQuaternion();
						ushort fuel = river.readUInt16();
						ushort health = river.readUInt16();
						ushort batteryCharge = 10000;
						if (b > 5)
						{
							batteryCharge = river.readUInt16();
						}
						byte tireAliveMask = byte.MaxValue;
						if (b > 6)
						{
							tireAliveMask = river.readByte();
						}
						CSteamID owner = CSteamID.Nil;
						CSteamID group = CSteamID.Nil;
						bool locked = false;
						if (b > 4)
						{
							owner = river.readSteamID();
							group = river.readSteamID();
							locked = river.readBoolean();
						}
						byte[][] array = null;
						if (b > 3)
						{
							array = new byte[(int)river.readByte()][];
							byte b2 = 0;
							while ((int)b2 < array.Length)
							{
								array[(int)b2] = river.readBytes();
								b2 += 1;
							}
						}
						point.y += 0.02f;
						bool flag = b >= 11 && river.readBoolean();
						ItemJar[] array2 = null;
						if (flag)
						{
							array2 = new ItemJar[(int)river.readByte()];
							byte b3 = 0;
							while ((int)b3 < array2.Length)
							{
								byte new_x = river.readByte();
								byte new_y = river.readByte();
								byte newRot = river.readByte();
								ushort num3 = river.readUInt16();
								byte newAmount = river.readByte();
								byte newQuality = river.readByte();
								byte[] newState = river.readBytes();
								ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, num3);
								if (itemAsset != null)
								{
									Item newItem = new Item(num3, newAmount, newQuality, newState);
									array2[(int)b3] = new ItemJar(new_x, new_y, newRot, newItem);
								}
								b3 += 1;
							}
						}
						VehicleAsset vehicleAsset = (VehicleAsset)Assets.find(EAssetType.VEHICLE, id);
						if (vehicleAsset != null)
						{
							InteractableVehicle interactableVehicle = VehicleManager.manager.addVehicle(id, skinID, mythicID, roadPosition, point, angle, false, false, false, false, fuel, false, health, batteryCharge, owner, group, locked, null, array, VehicleManager.instanceCount += 1u, tireAliveMask);
							if (flag && array2 != null && array2.Length > 0 && interactableVehicle.trunkItems != null && interactableVehicle.trunkItems.height > 0)
							{
								byte b4 = 0;
								while ((int)b4 < array2.Length)
								{
									ItemJar itemJar = array2[(int)b4];
									if (itemJar != null)
									{
										interactableVehicle.trunkItems.loadItem(itemJar.x, itemJar.y, itemJar.rot, itemJar.item);
									}
									b4 += 1;
								}
							}
						}
					}
				}
				else
				{
					ushort num4 = river.readUInt16();
					for (ushort num5 = 0; num5 < num4; num5 += 1)
					{
						ushort id2 = river.readUInt16();
						river.readColor();
						Vector3 point2 = river.readSingleVector3();
						Quaternion angle2 = river.readSingleQuaternion();
						ushort fuel2 = river.readUInt16();
						ushort health2 = ushort.MaxValue;
						ushort maxValue = ushort.MaxValue;
						byte maxValue2 = byte.MaxValue;
						id2 = (ushort)UnityEngine.Random.Range(1, 51);
						if (b > 1)
						{
							health2 = river.readUInt16();
						}
						point2.y += 0.02f;
						VehicleAsset vehicleAsset2 = (VehicleAsset)Assets.find(EAssetType.VEHICLE, id2);
						if (vehicleAsset2 != null)
						{
							VehicleManager.manager.addVehicle(id2, 0, 0, 0f, point2, angle2, false, false, false, false, fuel2, false, health2, maxValue, CSteamID.Nil, CSteamID.Nil, false, null, null, VehicleManager.instanceCount += 1u, maxValue2);
						}
					}
				}
			}
			Level.isLoadingVehicles = false;
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x00101004 File Offset: 0x000FF404
		public static void save()
		{
			River river = LevelSavedata.openRiver("/Vehicles.dat", false);
			river.writeByte(VehicleManager.SAVEDATA_VERSION);
			ushort num = 0;
			ushort num2 = 0;
			while ((int)num2 < VehicleManager.vehicles.Count)
			{
				InteractableVehicle interactableVehicle = VehicleManager.vehicles[(int)num2];
				if (!interactableVehicle.isAutoClearable)
				{
					num += 1;
				}
				num2 += 1;
			}
			river.writeUInt16(num);
			ushort num3 = 0;
			while ((int)num3 < VehicleManager.vehicles.Count)
			{
				InteractableVehicle interactableVehicle2 = VehicleManager.vehicles[(int)num3];
				if (!interactableVehicle2.isAutoClearable)
				{
					river.writeUInt16(interactableVehicle2.id);
					river.writeUInt16(interactableVehicle2.skinID);
					river.writeUInt16(interactableVehicle2.mythicID);
					river.writeSingle(interactableVehicle2.roadPosition);
					river.writeSingleVector3(interactableVehicle2.transform.position);
					river.writeSingleQuaternion(interactableVehicle2.transform.rotation);
					river.writeUInt16(interactableVehicle2.fuel);
					river.writeUInt16(interactableVehicle2.health);
					river.writeUInt16(interactableVehicle2.batteryCharge);
					river.writeByte(interactableVehicle2.tireAliveMask);
					river.writeSteamID(interactableVehicle2.lockedOwner);
					river.writeSteamID(interactableVehicle2.lockedGroup);
					river.writeBoolean(interactableVehicle2.isLocked);
					river.writeByte((byte)interactableVehicle2.turrets.Length);
					byte b = 0;
					while ((int)b < interactableVehicle2.turrets.Length)
					{
						river.writeBytes(interactableVehicle2.turrets[(int)b].state);
						b += 1;
					}
					if (interactableVehicle2.trunkItems != null && interactableVehicle2.trunkItems.height > 0)
					{
						river.writeBoolean(true);
						river.writeByte(interactableVehicle2.trunkItems.getItemCount());
						for (byte b2 = 0; b2 < interactableVehicle2.trunkItems.getItemCount(); b2 += 1)
						{
							ItemJar item = interactableVehicle2.trunkItems.getItem(b2);
							river.writeByte(item.x);
							river.writeByte(item.y);
							river.writeByte(item.rot);
							river.writeUInt16(item.item.id);
							river.writeByte(item.item.amount);
							river.writeByte(item.item.quality);
							river.writeBytes(item.item.state);
						}
					}
					else
					{
						river.writeBoolean(false);
					}
				}
				num3 += 1;
			}
			river.closeRiver();
		}

		// Token: 0x040019BC RID: 6588
		public static readonly byte SAVEDATA_VERSION = 11;

		// Token: 0x040019BD RID: 6589
		public static VehicleLockpickedSignature onVehicleLockpicked;

		// Token: 0x040019BE RID: 6590
		private static VehicleManager manager;

		// Token: 0x040019BF RID: 6591
		private static List<InteractableVehicle> _vehicles;

		// Token: 0x040019C0 RID: 6592
		private static uint instanceCount;

		// Token: 0x040019C1 RID: 6593
		private static ushort respawnVehicleIndex;

		// Token: 0x040019C2 RID: 6594
		private static float lastTick;

		// Token: 0x040019C3 RID: 6595
		private uint seq;
	}
}
