using System;
using System.Collections.Generic;
using SDG.Framework.Utilities;
using SDG.Framework.Water;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200072C RID: 1836
	public class DamageTool
	{
		// Token: 0x060033D3 RID: 13267 RVA: 0x001508AC File Offset: 0x0014ECAC
		public static ELimb getLimb(Transform limb)
		{
			if (limb.CompareTag("Player") || limb.CompareTag("Enemy") || limb.CompareTag("Zombie") || limb.CompareTag("Animal"))
			{
				string name = limb.name;
				switch (name)
				{
				case "Left_Foot":
					return ELimb.LEFT_FOOT;
				case "Left_Leg":
					return ELimb.LEFT_LEG;
				case "Right_Foot":
					return ELimb.RIGHT_FOOT;
				case "Right_Leg":
					return ELimb.RIGHT_LEG;
				case "Left_Hand":
					return ELimb.LEFT_HAND;
				case "Left_Arm":
					return ELimb.LEFT_ARM;
				case "Right_Hand":
					return ELimb.RIGHT_HAND;
				case "Right_Arm":
					return ELimb.RIGHT_ARM;
				case "Left_Back":
					return ELimb.LEFT_BACK;
				case "Right_Back":
					return ELimb.RIGHT_BACK;
				case "Left_Front":
					return ELimb.LEFT_FRONT;
				case "Right_Front":
					return ELimb.RIGHT_FRONT;
				case "Spine":
					return ELimb.SPINE;
				case "Skull":
					return ELimb.SKULL;
				}
			}
			return ELimb.SPINE;
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x00150A44 File Offset: 0x0014EE44
		public static Player getPlayer(Transform limb)
		{
			Player player = null;
			if (limb.CompareTag("Player"))
			{
				player = limb.GetComponent<Player>();
			}
			else
			{
				string name = limb.name;
				switch (name)
				{
				case "Left_Foot":
					player = limb.parent.parent.parent.parent.parent.GetComponent<Player>();
					break;
				case "Left_Leg":
					player = limb.parent.parent.parent.parent.GetComponent<Player>();
					break;
				case "Right_Foot":
					player = limb.parent.parent.parent.parent.parent.GetComponent<Player>();
					break;
				case "Right_Leg":
					player = limb.parent.parent.parent.parent.GetComponent<Player>();
					break;
				case "Left_Hand":
					player = limb.parent.parent.parent.parent.parent.parent.GetComponent<Player>();
					break;
				case "Left_Arm":
					player = limb.parent.parent.parent.parent.parent.GetComponent<Player>();
					break;
				case "Right_Hand":
					player = limb.parent.parent.parent.parent.parent.parent.GetComponent<Player>();
					break;
				case "Right_Arm":
					player = limb.parent.parent.parent.parent.parent.GetComponent<Player>();
					break;
				case "Spine":
					player = limb.parent.parent.parent.GetComponent<Player>();
					break;
				case "Skull":
					player = limb.parent.parent.parent.parent.GetComponent<Player>();
					break;
				}
			}
			if (player != null && player.life.isDead)
			{
				player = null;
			}
			return player;
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x00150CD4 File Offset: 0x0014F0D4
		public static Zombie getZombie(Transform limb)
		{
			Zombie zombie = null;
			if (limb.CompareTag("Agent"))
			{
				zombie = limb.GetComponent<Zombie>();
			}
			else
			{
				string name = limb.name;
				switch (name)
				{
				case "Left_Foot":
					zombie = limb.parent.parent.parent.parent.parent.GetComponent<Zombie>();
					break;
				case "Left_Leg":
					zombie = limb.parent.parent.parent.parent.GetComponent<Zombie>();
					break;
				case "Right_Foot":
					zombie = limb.parent.parent.parent.parent.parent.GetComponent<Zombie>();
					break;
				case "Right_Leg":
					zombie = limb.parent.parent.parent.parent.GetComponent<Zombie>();
					break;
				case "Left_Hand":
					zombie = limb.parent.parent.parent.parent.parent.parent.GetComponent<Zombie>();
					break;
				case "Left_Arm":
					zombie = limb.parent.parent.parent.parent.parent.GetComponent<Zombie>();
					break;
				case "Right_Hand":
					zombie = limb.parent.parent.parent.parent.parent.parent.GetComponent<Zombie>();
					break;
				case "Right_Arm":
					zombie = limb.parent.parent.parent.parent.parent.GetComponent<Zombie>();
					break;
				case "Spine":
					zombie = limb.parent.parent.parent.GetComponent<Zombie>();
					break;
				case "Skull":
					zombie = limb.parent.parent.parent.parent.GetComponent<Zombie>();
					break;
				}
			}
			if (zombie != null && zombie.isDead)
			{
				zombie = null;
			}
			return zombie;
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x00150F60 File Offset: 0x0014F360
		public static Animal getAnimal(Transform limb)
		{
			Animal animal = null;
			if (limb.CompareTag("Agent"))
			{
				animal = limb.GetComponent<Animal>();
			}
			else
			{
				string name = limb.name;
				if (name != null)
				{
					if (!(name == "Left_Back"))
					{
						if (!(name == "Right_Back"))
						{
							if (!(name == "Left_Front"))
							{
								if (!(name == "Right_Front"))
								{
									if (!(name == "Spine"))
									{
										if (name == "Skull")
										{
											animal = limb.parent.parent.parent.parent.GetComponent<Animal>();
										}
									}
									else
									{
										animal = limb.parent.parent.parent.GetComponent<Animal>();
									}
								}
								else
								{
									animal = limb.parent.parent.parent.parent.GetComponent<Animal>();
								}
							}
							else
							{
								animal = limb.parent.parent.parent.parent.GetComponent<Animal>();
							}
						}
						else
						{
							animal = limb.parent.parent.parent.GetComponent<Animal>();
						}
					}
					else
					{
						animal = limb.parent.parent.parent.GetComponent<Animal>();
					}
				}
			}
			if (animal != null && animal.isDead)
			{
				animal = null;
			}
			return animal;
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x001510C8 File Offset: 0x0014F4C8
		public static InteractableVehicle getVehicle(Transform model)
		{
			InteractableVehicle component = model.GetComponent<InteractableVehicle>();
			if (component != null)
			{
				return component;
			}
			VehicleRef component2 = model.GetComponent<VehicleRef>();
			if (component2 != null)
			{
				return component2.vehicle;
			}
			return null;
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x00151108 File Offset: 0x0014F508
		public static void damage(Player player, EDeathCause cause, ELimb limb, CSteamID killer, Vector3 direction, float damage, float times, out EPlayerKill kill)
		{
			if (player == null)
			{
				kill = EPlayerKill.NONE;
				return;
			}
			bool flag = true;
			if (DamageTool.playerDamaged != null)
			{
				DamageTool.playerDamaged(player, ref cause, ref limb, ref killer, ref direction, ref damage, ref times, ref flag);
			}
			if (!flag)
			{
				kill = EPlayerKill.NONE;
				return;
			}
			times *= Provider.modeConfigData.Players.Armor_Multiplier;
			byte b = (byte)(damage * times);
			player.life.askDamage(b, direction * (float)b, cause, limb, killer, out kill);
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x0015118B File Offset: 0x0014F58B
		public static void damage(Player player, EDeathCause cause, ELimb limb, CSteamID killer, Vector3 direction, PlayerDamageMultiplier multiplier, float times, bool armor, out EPlayerKill kill)
		{
			if (player == null)
			{
				kill = EPlayerKill.NONE;
				return;
			}
			if (armor)
			{
				times *= multiplier.armor(limb, player);
			}
			DamageTool.damage(player, cause, limb, killer, direction, multiplier.multiply(limb), times, out kill);
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x001511CC File Offset: 0x0014F5CC
		public static void damage(Zombie zombie, Vector3 direction, float damage, float times, out EPlayerKill kill, out uint xp)
		{
			if (zombie == null)
			{
				kill = EPlayerKill.NONE;
				xp = 0u;
				return;
			}
			byte b = (byte)(damage * times);
			zombie.askDamage((ushort)b, direction * (float)b, out kill, out xp, true, true);
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x00151208 File Offset: 0x0014F608
		public static void damage(Zombie zombie, ELimb limb, Vector3 direction, ZombieDamageMultiplier multiplier, float times, bool armor, out EPlayerKill kill, out uint xp)
		{
			if (zombie == null)
			{
				kill = EPlayerKill.NONE;
				xp = 0u;
				return;
			}
			if (armor)
			{
				times *= multiplier.armor(limb, zombie);
				if ((double)Vector3.Dot(zombie.transform.forward, direction) > 0.5)
				{
					times *= 1.25f;
				}
			}
			DamageTool.damage(zombie, direction, multiplier.multiply(limb), times, out kill, out xp);
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x0015127C File Offset: 0x0014F67C
		public static void damage(Animal animal, Vector3 direction, float damage, float times, out EPlayerKill kill, out uint xp)
		{
			if (animal == null)
			{
				kill = EPlayerKill.NONE;
				xp = 0u;
				return;
			}
			byte b = (byte)(damage * times);
			animal.askDamage(b, direction * (float)b, out kill, out xp, true);
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x001512B7 File Offset: 0x0014F6B7
		public static void damage(Animal animal, ELimb limb, Vector3 direction, AnimalDamageMultiplier multiplier, float times, out EPlayerKill kill, out uint xp)
		{
			if (animal == null)
			{
				kill = EPlayerKill.NONE;
				xp = 0u;
				return;
			}
			DamageTool.damage(animal, direction, multiplier.multiply(limb), times, out kill, out xp);
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x001512E4 File Offset: 0x0014F6E4
		public static void damage(InteractableVehicle vehicle, bool damageTires, Vector3 position, bool isRepairing, float vehicleDamage, float times, bool canRepair, out EPlayerKill kill)
		{
			kill = EPlayerKill.NONE;
			if (vehicle == null)
			{
				return;
			}
			if (isRepairing)
			{
				if (!vehicle.isExploded && !vehicle.isRepaired)
				{
					VehicleManager.repair(vehicle, vehicleDamage, times);
				}
			}
			else
			{
				if (!vehicle.isDead)
				{
					VehicleManager.damage(vehicle, vehicleDamage, times, canRepair);
				}
				if (damageTires && !vehicle.isExploded)
				{
					int hitTireIndex = vehicle.getHitTireIndex(position);
					if (hitTireIndex != -1)
					{
						vehicle.askDamageTire(hitTireIndex);
					}
				}
			}
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x0015136B File Offset: 0x0014F76B
		public static void damage(Transform barricade, bool isRepairing, float barricadeDamage, float times, out EPlayerKill kill)
		{
			kill = EPlayerKill.NONE;
			if (barricade == null)
			{
				return;
			}
			if (isRepairing)
			{
				BarricadeManager.repair(barricade, barricadeDamage, times);
			}
			else
			{
				BarricadeManager.damage(barricade, barricadeDamage, times, true);
			}
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x0015139A File Offset: 0x0014F79A
		public static void damage(Transform structure, bool isRepairing, Vector3 direction, float structureDamage, float times, out EPlayerKill kill)
		{
			kill = EPlayerKill.NONE;
			if (structure == null)
			{
				return;
			}
			if (isRepairing)
			{
				StructureManager.repair(structure, structureDamage, times);
			}
			else
			{
				StructureManager.damage(structure, direction, structureDamage, times, true);
			}
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x001513CC File Offset: 0x0014F7CC
		public static void damage(Transform resource, Vector3 direction, float resourceDamage, float times, float drops, out EPlayerKill kill, out uint xp)
		{
			if (resource == null)
			{
				kill = EPlayerKill.NONE;
				xp = 0u;
				return;
			}
			ResourceManager.damage(resource, direction, resourceDamage, times, drops, out kill, out xp);
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x001513F2 File Offset: 0x0014F7F2
		public static void damage(Transform obj, Vector3 direction, byte section, float objectDamage, float times, out EPlayerKill kill, out uint xp)
		{
			if (obj == null)
			{
				kill = EPlayerKill.NONE;
				xp = 0u;
				return;
			}
			ObjectManager.damage(obj, direction, section, objectDamage, times, out kill, out xp);
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x00151418 File Offset: 0x0014F818
		public static void explode(Vector3 point, float damageRadius, EDeathCause cause, CSteamID killer, float playerDamage, float zombieDamage, float animalDamage, float barricadeDamage, float structureDamage, float vehicleDamage, float resourceDamage, float objectDamage, out List<EPlayerKill> kills, EExplosionDamageType damageType = EExplosionDamageType.CONVENTIONAL, float alertRadius = 32f, bool playImpactEffect = true, bool penetrateBuildables = false)
		{
			DamageTool.explosionKills.Clear();
			kills = DamageTool.explosionKills;
			DamageTool.explosionRangeComparator.point = point;
			float num = damageRadius * damageRadius;
			DamageTool.regionsInRadius.Clear();
			Regions.getRegionsInRadius(point, damageRadius, DamageTool.regionsInRadius);
			int layerMask;
			if (penetrateBuildables)
			{
				layerMask = RayMasks.BLOCK_EXPLOSION_PENETRATE_BUILDABLES;
			}
			else
			{
				layerMask = RayMasks.BLOCK_EXPLOSION;
			}
			if (structureDamage > 0.5f)
			{
				DamageTool.structuresInRadius.Clear();
				StructureManager.getStructuresInRadius(point, num, DamageTool.regionsInRadius, DamageTool.structuresInRadius);
				DamageTool.structuresInRadius.Sort(DamageTool.explosionRangeComparator);
				for (int i = 0; i < DamageTool.structuresInRadius.Count; i++)
				{
					Transform transform = DamageTool.structuresInRadius[i];
					if (!(transform == null))
					{
						ushort id;
						if (ushort.TryParse(transform.name, out id))
						{
							ItemStructureAsset itemStructureAsset = (ItemStructureAsset)Assets.find(EAssetType.ITEM, id);
							if (itemStructureAsset != null && !itemStructureAsset.proofExplosion)
							{
								Vector3 a = transform.transform.position - point;
								float magnitude = a.magnitude;
								Vector3 direction = a / magnitude;
								if (magnitude > 0.5f)
								{
									RaycastHit raycastHit;
									PhysicsUtility.raycast(new Ray(point, direction), out raycastHit, magnitude - 0.5f, layerMask, QueryTriggerInteraction.UseGlobal);
									if (raycastHit.transform != null && raycastHit.transform != transform.transform)
									{
										goto IL_17D;
									}
								}
								StructureManager.damage(transform, a.normalized, structureDamage, 1f - magnitude / damageRadius, true);
							}
						}
					}
					IL_17D:;
				}
			}
			if (resourceDamage > 0.5f)
			{
				DamageTool.resourcesInRadius.Clear();
				ResourceManager.getResourcesInRadius(point, num, DamageTool.regionsInRadius, DamageTool.resourcesInRadius);
				DamageTool.resourcesInRadius.Sort(DamageTool.explosionRangeComparator);
				for (int j = 0; j < DamageTool.resourcesInRadius.Count; j++)
				{
					Transform transform2 = DamageTool.resourcesInRadius[j];
					if (!(transform2 == null))
					{
						Vector3 a2 = transform2.transform.position - point;
						float magnitude2 = a2.magnitude;
						Vector3 direction2 = a2 / magnitude2;
						if (magnitude2 > 0.5f)
						{
							RaycastHit raycastHit;
							PhysicsUtility.raycast(new Ray(point, direction2), out raycastHit, magnitude2 - 0.5f, layerMask, QueryTriggerInteraction.UseGlobal);
							if (raycastHit.transform != null && raycastHit.transform != transform2.transform)
							{
								goto IL_2A2;
							}
						}
						EPlayerKill eplayerKill;
						uint num2;
						ResourceManager.damage(transform2, a2.normalized, resourceDamage, 1f - magnitude2 / damageRadius, 1f, out eplayerKill, out num2);
						if (eplayerKill != EPlayerKill.NONE)
						{
							kills.Add(eplayerKill);
						}
					}
					IL_2A2:;
				}
			}
			if (objectDamage > 0.5f)
			{
				DamageTool.objectsInRadius.Clear();
				ObjectManager.getObjectsInRadius(point, num, DamageTool.regionsInRadius, DamageTool.objectsInRadius);
				DamageTool.objectsInRadius.Sort(DamageTool.explosionRangeComparator);
				for (int k = 0; k < DamageTool.objectsInRadius.Count; k++)
				{
					Transform transform3 = DamageTool.objectsInRadius[k];
					if (!(transform3 == null))
					{
						InteractableObjectRubble component = transform3.GetComponent<InteractableObjectRubble>();
						if (!(component == null))
						{
							if (!component.asset.rubbleProofExplosion)
							{
								for (byte b = 0; b < component.getSectionCount(); b += 1)
								{
									Transform section = component.getSection(b);
									Vector3 a3 = section.position - point;
									if (a3.sqrMagnitude < num)
									{
										float magnitude3 = a3.magnitude;
										Vector3 direction3 = a3 / magnitude3;
										if (magnitude3 > 0.5f)
										{
											RaycastHit raycastHit;
											PhysicsUtility.raycast(new Ray(point, direction3), out raycastHit, magnitude3 - 0.5f, layerMask, QueryTriggerInteraction.UseGlobal);
											if (raycastHit.transform != null && raycastHit.transform != transform3.transform)
											{
												goto IL_40B;
											}
										}
										EPlayerKill eplayerKill;
										uint num2;
										ObjectManager.damage(transform3, a3.normalized, b, objectDamage, 1f - magnitude3 / damageRadius, out eplayerKill, out num2);
										if (eplayerKill != EPlayerKill.NONE)
										{
											kills.Add(eplayerKill);
										}
									}
									IL_40B:;
								}
							}
						}
					}
				}
			}
			if (barricadeDamage > 0.5f)
			{
				DamageTool.barricadesInRadius.Clear();
				BarricadeManager.getBarricadesInRadius(point, num, DamageTool.regionsInRadius, DamageTool.barricadesInRadius);
				BarricadeManager.getBarricadesInRadius(point, num, DamageTool.barricadesInRadius);
				DamageTool.barricadesInRadius.Sort(DamageTool.explosionRangeComparator);
				for (int l = 0; l < DamageTool.barricadesInRadius.Count; l++)
				{
					Transform transform4 = DamageTool.barricadesInRadius[l];
					if (!(transform4 == null))
					{
						Vector3 a4 = transform4.transform.position - point;
						float magnitude4 = a4.magnitude;
						Vector3 direction4 = a4 / magnitude4;
						if (magnitude4 > 0.5f)
						{
							RaycastHit raycastHit;
							PhysicsUtility.raycast(new Ray(point, direction4), out raycastHit, magnitude4 - 0.5f, layerMask, QueryTriggerInteraction.UseGlobal);
							if (raycastHit.transform != null && raycastHit.transform != transform4.transform)
							{
								goto IL_568;
							}
						}
						ushort id2;
						if (ushort.TryParse(transform4.name, out id2))
						{
							ItemBarricadeAsset itemBarricadeAsset = (ItemBarricadeAsset)Assets.find(EAssetType.ITEM, id2);
							if (itemBarricadeAsset != null && !itemBarricadeAsset.proofExplosion)
							{
								BarricadeManager.damage(transform4, barricadeDamage, 1f - magnitude4 / damageRadius, true);
							}
						}
					}
					IL_568:;
				}
			}
			if ((Provider.isPvP || damageType == EExplosionDamageType.ZOMBIE_ACID || damageType == EExplosionDamageType.ZOMBIE_FIRE || damageType == EExplosionDamageType.ZOMBIE_ELECTRIC) && playerDamage > 0.5f)
			{
				DamageTool.playersInRadius.Clear();
				PlayerTool.getPlayersInRadius(point, num, DamageTool.playersInRadius);
				for (int m = 0; m < DamageTool.playersInRadius.Count; m++)
				{
					Player player = DamageTool.playersInRadius[m];
					if (!(player == null) && !player.life.isDead)
					{
						if (damageType != EExplosionDamageType.ZOMBIE_FIRE || player.clothing.shirtAsset == null || !player.clothing.shirtAsset.proofFire || player.clothing.pantsAsset == null || !player.clothing.pantsAsset.proofFire)
						{
							Vector3 a5 = player.transform.position - point;
							float magnitude5 = a5.magnitude;
							Vector3 vector = a5 / magnitude5;
							if (magnitude5 > 0.5f)
							{
								RaycastHit raycastHit;
								PhysicsUtility.raycast(new Ray(point, vector), out raycastHit, magnitude5 - 0.5f, layerMask, QueryTriggerInteraction.UseGlobal);
								if (raycastHit.transform != null && raycastHit.transform != player.transform)
								{
									goto IL_7AA;
								}
							}
							if (playImpactEffect)
							{
								EffectManager.sendEffect(5, EffectManager.SMALL, player.transform.position + Vector3.up, -vector);
								EffectManager.sendEffect(5, EffectManager.SMALL, player.transform.position + Vector3.up, Vector3.up);
							}
							float num3 = 1f - Mathf.Pow(magnitude5 / damageRadius, 2f);
							if (player.movement.getVehicle() != null && player.movement.getVehicle().asset != null)
							{
								num3 *= player.movement.getVehicle().asset.passengerExplosionArmor;
							}
							EPlayerKill eplayerKill;
							DamageTool.damage(player, cause, ELimb.SPINE, killer, vector, playerDamage, num3, out eplayerKill);
							if (eplayerKill != EPlayerKill.NONE)
							{
								kills.Add(eplayerKill);
							}
						}
					}
					IL_7AA:;
				}
			}
			if (damageType == EExplosionDamageType.ZOMBIE_FIRE || zombieDamage > 0.5f)
			{
				DamageTool.zombiesInRadius.Clear();
				ZombieManager.getZombiesInRadius(point, num, DamageTool.zombiesInRadius);
				for (int n = 0; n < DamageTool.zombiesInRadius.Count; n++)
				{
					Zombie zombie = DamageTool.zombiesInRadius[n];
					if (!(zombie == null) && !zombie.isDead)
					{
						if (damageType == EExplosionDamageType.ZOMBIE_FIRE)
						{
							if (zombie.speciality == EZombieSpeciality.NORMAL)
							{
								ZombieManager.sendZombieSpeciality(zombie, EZombieSpeciality.BURNER);
							}
						}
						else
						{
							Vector3 a6 = zombie.transform.position - point;
							float magnitude6 = a6.magnitude;
							Vector3 vector2 = a6 / magnitude6;
							if (magnitude6 > 0.5f)
							{
								RaycastHit raycastHit;
								PhysicsUtility.raycast(new Ray(point, vector2), out raycastHit, magnitude6 - 0.5f, layerMask, QueryTriggerInteraction.UseGlobal);
								if (raycastHit.transform != null && raycastHit.transform != zombie.transform)
								{
									goto IL_964;
								}
							}
							if (playImpactEffect)
							{
								EffectManager.sendEffect((!zombie.isRadioactive) ? 5 : 95, EffectManager.SMALL, zombie.transform.position + Vector3.up, -vector2);
								EffectManager.sendEffect((!zombie.isRadioactive) ? 5 : 95, EffectManager.SMALL, zombie.transform.position + Vector3.up, Vector3.up);
							}
							EPlayerKill eplayerKill;
							uint num2;
							DamageTool.damage(zombie, vector2, zombieDamage, 1f - magnitude6 / damageRadius, out eplayerKill, out num2);
							if (eplayerKill != EPlayerKill.NONE)
							{
								kills.Add(eplayerKill);
							}
						}
					}
					IL_964:;
				}
			}
			if (animalDamage > 0.5f)
			{
				DamageTool.animalsInRadius.Clear();
				AnimalManager.getAnimalsInRadius(point, num, DamageTool.animalsInRadius);
				for (int num4 = 0; num4 < DamageTool.animalsInRadius.Count; num4++)
				{
					Animal animal = DamageTool.animalsInRadius[num4];
					if (!(animal == null) && !animal.isDead)
					{
						Vector3 a7 = animal.transform.position - point;
						float magnitude7 = a7.magnitude;
						Vector3 vector3 = a7 / magnitude7;
						if (magnitude7 > 0.5f)
						{
							RaycastHit raycastHit;
							PhysicsUtility.raycast(new Ray(point, vector3), out raycastHit, magnitude7 - 0.5f, layerMask, QueryTriggerInteraction.UseGlobal);
							if (raycastHit.transform != null && raycastHit.transform != animal.transform)
							{
								goto IL_ACC;
							}
						}
						if (playImpactEffect)
						{
							EffectManager.sendEffect(5, EffectManager.SMALL, animal.transform.position + Vector3.up, -vector3);
							EffectManager.sendEffect(5, EffectManager.SMALL, animal.transform.position + Vector3.up, Vector3.up);
						}
						EPlayerKill eplayerKill;
						uint num2;
						DamageTool.damage(animal, vector3, animalDamage, 1f - magnitude7 / damageRadius, out eplayerKill, out num2);
						if (eplayerKill != EPlayerKill.NONE)
						{
							kills.Add(eplayerKill);
						}
					}
					IL_ACC:;
				}
			}
			if (vehicleDamage > 0.5f)
			{
				DamageTool.vehiclesInRadius.Clear();
				VehicleManager.getVehiclesInRadius(point, num, DamageTool.vehiclesInRadius);
				for (int num5 = 0; num5 < DamageTool.vehiclesInRadius.Count; num5++)
				{
					InteractableVehicle interactableVehicle = DamageTool.vehiclesInRadius[num5];
					if (!(interactableVehicle == null) && !interactableVehicle.isDead)
					{
						if (interactableVehicle.asset != null && interactableVehicle.asset.isVulnerableToExplosions)
						{
							Vector3 a8 = interactableVehicle.transform.position - point;
							float magnitude8 = a8.magnitude;
							Vector3 direction5 = a8 / magnitude8;
							if (magnitude8 > 0.5f)
							{
								RaycastHit raycastHit;
								PhysicsUtility.raycast(new Ray(point, direction5), out raycastHit, magnitude8 - 0.5f, layerMask, QueryTriggerInteraction.UseGlobal);
								if (raycastHit.transform != null && raycastHit.transform != interactableVehicle.transform)
								{
									goto IL_BED;
								}
							}
							VehicleManager.damage(interactableVehicle, vehicleDamage, 1f - magnitude8 / damageRadius, false);
						}
					}
					IL_BED:;
				}
			}
			AlertTool.alert(point, alertRadius);
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x00152031 File Offset: 0x00150431
		public static EPhysicsMaterial getMaterial(Vector3 point, Transform transform, Collider collider)
		{
			if (WaterUtility.isPointUnderwater(point))
			{
				return EPhysicsMaterial.WATER_STATIC;
			}
			if (transform.CompareTag("Ground"))
			{
				return PhysicsTool.checkMaterial(point);
			}
			return PhysicsTool.checkMaterial(collider);
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x0015205E File Offset: 0x0015045E
		public static void impact(Vector3 point, Vector3 normal, EPhysicsMaterial material, bool forceDynamic)
		{
			DamageTool.impact(point, normal, material, forceDynamic, CSteamID.Nil, point);
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x00152070 File Offset: 0x00150470
		public static void impact(Vector3 point, Vector3 normal, EPhysicsMaterial material, bool forceDynamic, CSteamID spectatorID, Vector3 spectatorPoint)
		{
			if (material == EPhysicsMaterial.NONE)
			{
				return;
			}
			ushort id = 0;
			if (material == EPhysicsMaterial.CLOTH_DYNAMIC || material == EPhysicsMaterial.TILE_DYNAMIC || material == EPhysicsMaterial.CONCRETE_DYNAMIC)
			{
				id = 38;
			}
			else if (material == EPhysicsMaterial.CLOTH_STATIC || material == EPhysicsMaterial.TILE_STATIC || material == EPhysicsMaterial.CONCRETE_STATIC)
			{
				id = ((!forceDynamic) ? 13 : 38);
			}
			else if (material == EPhysicsMaterial.FLESH_DYNAMIC)
			{
				id = 5;
			}
			else if (material == EPhysicsMaterial.GRAVEL_DYNAMIC)
			{
				id = 44;
			}
			else if (material == EPhysicsMaterial.GRAVEL_STATIC)
			{
				id = ((!forceDynamic) ? 14 : 44);
			}
			else if (material == EPhysicsMaterial.METAL_DYNAMIC)
			{
				id = 18;
			}
			else if (material == EPhysicsMaterial.METAL_STATIC || material == EPhysicsMaterial.METAL_SLIP)
			{
				id = ((!forceDynamic) ? 12 : 18);
			}
			else if (material == EPhysicsMaterial.WOOD_DYNAMIC)
			{
				id = 17;
			}
			else if (material == EPhysicsMaterial.WOOD_STATIC)
			{
				id = ((!forceDynamic) ? 2 : 17);
			}
			else if (material == EPhysicsMaterial.FOLIAGE_STATIC || material == EPhysicsMaterial.FOLIAGE_DYNAMIC)
			{
				id = 15;
			}
			else if (material == EPhysicsMaterial.SNOW_STATIC || material == EPhysicsMaterial.ICE_STATIC)
			{
				id = 41;
			}
			else if (material == EPhysicsMaterial.WATER_STATIC)
			{
				id = 16;
			}
			else if (material == EPhysicsMaterial.ALIEN_DYNAMIC)
			{
				id = 95;
			}
			DamageTool.impact(point, normal, id, spectatorID, spectatorPoint);
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x001521C0 File Offset: 0x001505C0
		public static void impact(Vector3 point, Vector3 normal, ushort id, CSteamID spectatorID, Vector3 spectatorPoint)
		{
			if (id == 0)
			{
				return;
			}
			point += normal * UnityEngine.Random.Range(0.04f, 0.06f);
			EffectManager.sendEffect(id, EffectManager.SMALL, point, normal);
			if (spectatorID != CSteamID.Nil && (spectatorPoint - point).sqrMagnitude >= EffectManager.SMALL * EffectManager.SMALL)
			{
				EffectManager.sendEffect(id, spectatorID, point, normal);
			}
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x00152238 File Offset: 0x00150638
		public static RaycastInfo raycast(Ray ray, float range, int mask)
		{
			RaycastHit hit;
			PhysicsUtility.raycast(ray, out hit, range, mask, QueryTriggerInteraction.UseGlobal);
			RaycastInfo raycastInfo = new RaycastInfo(hit);
			raycastInfo.direction = ray.direction;
			if (hit.transform != null)
			{
				if (hit.transform.CompareTag("Enemy"))
				{
					raycastInfo.player = DamageTool.getPlayer(raycastInfo.transform);
				}
				if (hit.transform.CompareTag("Zombie"))
				{
					raycastInfo.zombie = DamageTool.getZombie(raycastInfo.transform);
				}
				if (hit.transform.CompareTag("Animal"))
				{
					raycastInfo.animal = DamageTool.getAnimal(raycastInfo.transform);
				}
				raycastInfo.limb = DamageTool.getLimb(raycastInfo.transform);
				if (hit.transform.CompareTag("Vehicle"))
				{
					raycastInfo.vehicle = DamageTool.getVehicle(raycastInfo.transform);
				}
				if (raycastInfo.zombie != null && raycastInfo.zombie.isRadioactive)
				{
					raycastInfo.material = EPhysicsMaterial.ALIEN_DYNAMIC;
				}
				else
				{
					raycastInfo.material = DamageTool.getMaterial(hit.point, hit.transform, hit.collider);
				}
			}
			return raycastInfo;
		}

		// Token: 0x0400233C RID: 9020
		public static DamageToolPlayerDamagedHandler playerDamaged;

		// Token: 0x0400233D RID: 9021
		private static List<RegionCoordinate> regionsInRadius = new List<RegionCoordinate>(4);

		// Token: 0x0400233E RID: 9022
		private static List<Player> playersInRadius = new List<Player>();

		// Token: 0x0400233F RID: 9023
		private static List<Zombie> zombiesInRadius = new List<Zombie>();

		// Token: 0x04002340 RID: 9024
		private static List<Animal> animalsInRadius = new List<Animal>();

		// Token: 0x04002341 RID: 9025
		private static List<Transform> barricadesInRadius = new List<Transform>();

		// Token: 0x04002342 RID: 9026
		private static List<Transform> structuresInRadius = new List<Transform>();

		// Token: 0x04002343 RID: 9027
		private static List<InteractableVehicle> vehiclesInRadius = new List<InteractableVehicle>();

		// Token: 0x04002344 RID: 9028
		private static List<Transform> resourcesInRadius = new List<Transform>();

		// Token: 0x04002345 RID: 9029
		private static List<Transform> objectsInRadius = new List<Transform>();

		// Token: 0x04002346 RID: 9030
		private static ExplosionRangeComparator explosionRangeComparator = new ExplosionRangeComparator();

		// Token: 0x04002347 RID: 9031
		private static List<EPlayerKill> explosionKills = new List<EPlayerKill>();
	}
}
